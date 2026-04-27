//! TCP server. Each accepted connection is handled in its own
//! blocking thread (we don't expect more than a handful of concurrent
//! clients, so `std::net` is plenty - no need for tokio).

use crate::eeprom;
use crate::livedata;
use crate::proto::{
    DumpRomParams, DumpRomResult, EepromReadResult, PortInfo, ReadEepromParams,
    ReadLiveSampleParams, ReadLiveSampleResult, Request, Response, VariantArg,
};
use crate::transport::{list_ports, open_kline, DynKLine};
use anyhow::{Context, Result};
use serde_json::{json, Value};
use std::io::{BufRead, BufReader, Write};
use std::net::{TcpListener, TcpStream};
use std::sync::{Mutex, OnceLock};
use std::time::{Duration, Instant};
use tracing::{debug, error, info, warn};

// ---- Persistent live-data session -----------------------------------
//
// `read_live_sample` is called ~30x/s by the GUI. Re-opening the FTDI
// every poll - which is what the daemon used to do - costs 70 ms LOW
// pulse + 130 ms settle + WAKEUP + ESTABLISH every single time, so the
// effective sample rate caps out at maybe 2 Hz with most of that being
// init traffic instead of TABLE replies.
//
// We instead cache the open transport keyed by `(backend, device_index)`
// and only re-run the handshake when:
//   - the key changes (different device or backend selected), or
//   - two consecutive polls came back fully empty (ECU dropped us),
//     and the throttle window has elapsed.

/// Re-establish throttle: don't waste 200 ms on WAKEUP/ESTABLISH more
/// than once per second when the ECU is genuinely silent (wrong
/// wiring, key off, dead bus).
const REESTABLISH_COOLDOWN: Duration = Duration::from_millis(1_000);

/// Per-session state kept alive between `read_live_sample` calls.
struct LiveSession {
    /// FTDI handle - already past the bit-bang wakeup and at 10400 baud.
    transport:           DynKLine,
    /// `(backend, device_index)` the transport was opened with. If the
    /// next request differs we tear this session down and open a fresh
    /// one.
    key:                 (String, u32),
    /// True until the very first non-empty TABLE reply lands. Used to
    /// gate the "first poll runs full WAKEUP+ESTABLISH" path.
    pending_first:       bool,
    /// Last time we ran the WAKEUP+ESTABLISH burst. Throttled so a
    /// genuinely silent ECU doesn't burn CPU on the daemon.
    last_reestablish_at: Instant,
}

static LIVE_SESSION: OnceLock<Mutex<Option<LiveSession>>> = OnceLock::new();

fn live_session() -> &'static Mutex<Option<LiveSession>> {
    LIVE_SESSION.get_or_init(|| Mutex::new(None))
}

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
        "dump_rom" => match handle_dump_rom(req.params) {
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
    } else if method == "dump_rom" {
        let n = resp.result.as_ref()
            .and_then(|v| v.get("bytes"))
            .and_then(|v| v.as_array())
            .map(|a| a.len())
            .unwrap_or(0);
        info!(method = %method, bytes = n, "response ok");
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
    let key = (backend.clone(), device_index);

    info!(device_index, backend = %backend, "read_live_sample start");

    let mut guard = live_session()
        .lock()
        .map_err(|_| anyhow::anyhow!("live-session mutex poisoned"))?;

    // If the client switched backend/device, drop the cached transport
    // so the new key reopens cleanly on the right driver.
    if let Some(s) = guard.as_ref() {
        if s.key != key {
            log.push(format!(
                "[livedata] device key changed {:?} -> {:?}, dropping session",
                s.key, key
            ));
            *guard = None;
        }
    }

    // First call (or just-dropped session): open with the bit-bang
    // wakeup pulse and run WAKEUP+ESTABLISH once. This is the only
    // slow path - subsequent polls reuse the open handle.
    if guard.is_none() {
        log.push(format!(
            "[livedata] new session backend={} index={}",
            backend, device_index
        ));
        let mut transport = open_kline(device_index, &backend, &mut log)
            .map_err(|e| anyhow::anyhow!(e))?;
        livedata::establish(transport.as_mut(), &mut log)
            .map_err(|e| anyhow::anyhow!(e))?;
        *guard = Some(LiveSession {
            transport,
            key:                 key.clone(),
            pending_first:       true,
            last_reestablish_at: Instant::now(),
        });
    }

    let session = guard.as_mut().expect("session populated above");

    // Steady-state poll - no wakeup pulse, no WAKEUP/ESTABLISH unless
    // the ECU went silent.
    let (mut table16, mut table20) =
        livedata::poll_tables(session.transport.as_mut(), &mut log)
            .map_err(|e| anyhow::anyhow!(e))?;
    let mut re_established = false;

    // Empty pair = ECU is silent (only TX echo bounced back). Fall
    // back to a full WAKEUP+ESTABLISH at most once per cooldown
    // window so a dead bus doesn't pin the daemon at 100% CPU.
    let both_empty = table16.is_empty() && table20.is_empty();
    let cooldown_elapsed =
        session.last_reestablish_at.elapsed() >= REESTABLISH_COOLDOWN;
    if both_empty && (session.pending_first || cooldown_elapsed) {
        log.push("[livedata] silent ECU - re-running WAKEUP+ESTABLISH".into());
        if let Err(e) = livedata::establish(session.transport.as_mut(), &mut log) {
            warn!(error = %e, "establish failed; tearing down session");
            *guard = None;
            return Err(anyhow::anyhow!(e));
        }
        session.last_reestablish_at = Instant::now();
        re_established = true;

        // Immediately re-poll so the caller still gets data this round.
        match livedata::poll_tables(
            guard.as_mut().unwrap().transport.as_mut(),
            &mut log,
        ) {
            Ok((t16, t20)) => {
                table16 = t16;
                table20 = t20;
            }
            Err(e) => {
                warn!(error = %e, "post-establish poll failed; tearing down session");
                *guard = None;
                return Err(anyhow::anyhow!(e));
            }
        }
    }

    if !table16.is_empty() || !table20.is_empty() {
        if let Some(s) = guard.as_mut() {
            s.pending_first = false;
        }
    }

    let elapsed = started.elapsed().as_millis() as u64;
    info!(
        table16 = table16.len(),
        table20 = table20.len(),
        elapsed_ms = elapsed,
        re_established,
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

fn handle_dump_rom(params: Value) -> Result<Value> {
    let p: DumpRomParams =
        serde_json::from_value(params).context("invalid params for dump_rom")?;
    let device_index = p.device_index.unwrap_or(0);
    let backend = p.backend.unwrap_or_else(|| "d2xx".to_string());
    let size = p.size.clone();
    let started = Instant::now();
    let mut log = Vec::new();

    info!(size = %size, device_index, backend = %backend, "dump_rom start");
    let mut transport = open_kline(device_index, &backend, &mut log)
        .map_err(|e| anyhow::anyhow!(e))?;

    let bytes = match size.as_str() {
        "48K" => eeprom::dump_rom_shinden_48k(transport.as_mut(), &mut log)
            .map_err(|e| anyhow::anyhow!(e))?,
        "64K" => eeprom::dump_rom_shinden_64k(transport.as_mut(), &mut log)
            .map_err(|e| anyhow::anyhow!(e))?,
        // EXPERIMENTAL - speculative 4-page sweep on byte[4]; see
        // `eeprom::dump_rom_shinden_256k_experimental` for caveats.
        "256K" => eeprom::dump_rom_shinden_256k_experimental(transport.as_mut(), &mut log)
            .map_err(|e| anyhow::anyhow!(e))?,
        other => {
            return Err(anyhow::anyhow!(
                "unknown ROM dump size: {other:?} (use \"48K\", \"64K\" or \"256K\")"
            ))
        }
    };
    let elapsed = started.elapsed().as_millis() as u64;
    info!(size = %size, bytes = bytes.len(), elapsed_ms = elapsed, "dump_rom done");

    let result = DumpRomResult {
        size,
        bytes,
        duration_ms: elapsed,
        log,
    };
    Ok(serde_json::to_value(result)?)
}
