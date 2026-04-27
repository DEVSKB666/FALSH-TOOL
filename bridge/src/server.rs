//! TCP server. Each accepted connection is handled in its own
//! blocking thread (we don't expect more than a handful of concurrent
//! clients, so `std::net` is plenty - no need for tokio).

use crate::eeprom;
use crate::livedata;
use crate::proto::{
    EepromReadResult, PortInfo, ReadEepromParams, ReadLiveSampleParams,
    ReadLiveSampleResult, Request, Response, VariantArg,
};
use crate::transport::{list_ports, open_kline};
use anyhow::{Context, Result};
use serde_json::{json, Value};
use std::io::{BufRead, BufReader, Write};
use std::net::{TcpListener, TcpStream};
use std::time::Instant;
use tracing::{debug, error, info, warn};

/// Start listening on `bind_addr`. Blocks forever (or until the OS
/// closes the listening socket).
pub fn run(bind_addr: &str) -> Result<()> {
    let listener = TcpListener::bind(bind_addr)
        .with_context(|| format!("binding {bind_addr}"))?;
    info!("listening on {}", listener.local_addr()?);

    for stream in listener.incoming() {
        match stream {
            Ok(s) => {
                let peer = s.peer_addr().ok();
                info!(?peer, "accepted client");
                std::thread::spawn(move || {
                    if let Err(e) = handle_client(s) {
                        warn!(?peer, error = %e, "client disconnected");
                    } else {
                        info!(?peer, "client closed cleanly");
                    }
                });
            }
            Err(e) => error!(error = %e, "accept failed"),
        }
    }
    Ok(())
}

fn handle_client(stream: TcpStream) -> Result<()> {
    // Slightly aggressive disable of Nagle so request → response loops
    // feel snappy on a LAN.
    let _ = stream.set_nodelay(true);

    let mut writer = stream.try_clone().context("dup writer")?;
    let reader = BufReader::new(stream);

    for line in reader.lines() {
        let line = match line {
            Ok(l) if l.trim().is_empty() => continue,
            Ok(l) => l,
            Err(e) => {
                warn!(error = %e, "read error");
                break;
            }
        };
        debug!(line = %line, "<-");

        // Try to parse the request envelope. If we can't even read the id,
        // we have to give up on this line entirely - the client gets no
        // reply but the connection stays open.
        let req: Request = match serde_json::from_str(&line) {
            Ok(r) => r,
            Err(e) => {
                let resp = Response::err(json!(null), format!("bad request: {e}"));
                let _ = write_response(&mut writer, &resp);
                continue;
            }
        };

        let resp = dispatch(req);
        if let Err(e) = write_response(&mut writer, &resp) {
            warn!(error = %e, "write error");
            break;
        }
    }
    Ok(())
}

fn write_response(w: &mut impl Write, resp: &Response) -> std::io::Result<()> {
    let line = serde_json::to_string(resp).unwrap_or_else(|_| "{}".to_string());
    debug!(line = %line, "->");
    w.write_all(line.as_bytes())?;
    w.write_all(b"\n")?;
    w.flush()
}

/// Top-level method dispatcher. Each method handler returns a
/// [`Response`] directly so we can attach the original request id.
fn dispatch(req: Request) -> Response {
    let id = req.id.clone();
    let method = req.method.clone();
    info!(method = %method, "request");
    let resp = match req.method.as_str() {
        "ping"        => Response::ok(id, json!({"pong": true})),
        "list_ports"  => match handle_list_ports() {
            Ok(v)  => Response::ok(id, v),
            Err(e) => Response::err(id, e.to_string()),
        },
        "read_eeprom" => match handle_read_eeprom(req.params) {
            Ok(v)  => Response::ok(id, v),
            Err(e) => Response::err(id, e.to_string()),
        },
        "read_live_sample" => match handle_read_live_sample(req.params) {
            Ok(v)  => Response::ok(id, v),
            Err(e) => Response::err(id, e.to_string()),
        },
        other => Response::err(id, format!("unknown method: {other}")),
    };
    // Summarise the response so logs are readable at INFO without
    // dumping huge byte arrays.
    if let Some(err) = &resp.error {
        warn!(method = %method, error = %err, "response error");
    } else if method == "list_ports" {
        let n = resp.result.as_ref().and_then(|v| v.as_array()).map(|a| a.len()).unwrap_or(0);
        info!(method = %method, ports = n, "response ok");
    } else if method == "read_eeprom" {
        let n = resp.result.as_ref()
            .and_then(|v| v.get("bytes"))
            .and_then(|v| v.as_array())
            .map(|a| a.len())
            .unwrap_or(0);
        info!(method = %method, bytes = n, "response ok");
    } else if method == "read_live_sample" {
        let t16 = resp.result.as_ref()
            .and_then(|v| v.get("table16"))
            .and_then(|v| v.as_array())
            .map(|a| a.len())
            .unwrap_or(0);
        let t20 = resp.result.as_ref()
            .and_then(|v| v.get("table20"))
            .and_then(|v| v.as_array())
            .map(|a| a.len())
            .unwrap_or(0);
        info!(method = %method, table16 = t16, table20 = t20, "response ok");
    } else {
        info!(method = %method, "response ok");
    }
    resp
}

// ---- Handlers --------------------------------------------------------

fn handle_list_ports() -> Result<Value> {
    let ports = list_ports().map_err(|e| anyhow::anyhow!(e))?;
    if ports.is_empty() {
        // Help the user diagnose "0 ports despite cable plugged in".
        // Most often this means neither D2XX nor libusb is bound to
        // the device (e.g. only the VCP driver is loaded).
        warn!("list_ports returned 0 devices - check FTDI driver bindings (D2XX or WinUSB/libusbK) on this machine");
    } else {
        for p in &ports {
            info!(
                backend = %p.backend,
                index = p.index,
                serial = %p.serial,
                description = %p.description,
                "found FTDI",
            );
        }
    }
    let out: Vec<PortInfo> = ports
        .into_iter()
        .map(|d| PortInfo {
            index: d.index,
            serial: d.serial,
            description: d.description,
            backend: d.backend,
        })
        .collect();
    Ok(serde_json::to_value(out)?)
}

fn handle_read_live_sample(params: Value) -> Result<Value> {
    let p: ReadLiveSampleParams =
        serde_json::from_value(params).context("invalid params for read_live_sample")?;
    let device_index = p.device_index.unwrap_or(0);
    let backend = p.backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();

    info!(device_index, backend = %backend, "read_live_sample start");
    // Live data uses Honda KWP `WAKEUP + ESTABLISH + TABLE_xx` -
    // same byte sequence + 70 ms / 130 ms wakeup timing the C#
    // original (`GForm12.cs::method_30 + method_31`) uses, which is
    // proven on real Keihin hardware. The EEPROM path uses the same
    // `open_kline` so both share the bit-bang pulse.
    let mut transport = open_kline(device_index, &backend, &mut log)
        .map_err(|e| anyhow::anyhow!(e))?;

    let (table16, table20) = livedata::poll_once(transport.as_mut(), &mut log)
        .map_err(|e| anyhow::anyhow!(e))?;
    let elapsed = started.elapsed().as_millis() as u64;
    info!(
        table16 = table16.len(),
        table20 = table20.len(),
        elapsed_ms = elapsed,
        "read_live_sample done"
    );

    let result = ReadLiveSampleResult {
        table16,
        table20,
        duration_ms: elapsed,
        log,
    };
    Ok(serde_json::to_value(result)?)
}

fn handle_read_eeprom(params: Value) -> Result<Value> {
    let p: ReadEepromParams =
        serde_json::from_value(params).context("invalid params for read_eeprom")?;
    let device_index = p.device_index.unwrap_or(0);
    let backend = p.backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();

    info!(?p.variant, device_index, backend = %backend, "read_eeprom start");
    let mut transport = open_kline(device_index, &backend, &mut log)
        .map_err(|e| anyhow::anyhow!(e))?;

    let bytes = match p.variant {
        VariantArg::Keihin => {
            eeprom::read_eeprom_keihin(transport.as_mut(), &mut log)
                .map_err(|e| anyhow::anyhow!(e))?
        }
        VariantArg::Shinden => {
            eeprom::read_eeprom_shinden(transport.as_mut(), &mut log)
                .map_err(|e| anyhow::anyhow!(e))?
        }
    };
    let elapsed = started.elapsed().as_millis() as u64;
    info!(family = ?p.variant, bytes = bytes.len(), elapsed_ms = elapsed, "read_eeprom done");

    let result = EepromReadResult {
        family: format!("{:?}", p.variant),
        bytes,
        duration_ms: elapsed,
        log,
    };
    Ok(serde_json::to_value(result)?)
}
