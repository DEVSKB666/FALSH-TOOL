//! TCP/JSON client that forwards K-Line operations to a remote
//! `loy-bridge.exe` daemon running on a notebook with the FTDI cable.
//!
//! Wire format = newline-delimited JSON, request → response, one
//! object per line. Matches the daemon's `proto.rs`.

use serde::{Deserialize, Serialize};
use serde_json::{json, Value};
use std::io::{BufRead, BufReader, Write};
use std::net::{SocketAddr, TcpStream, ToSocketAddrs};
use std::time::Duration;
use thiserror::Error;

#[derive(Debug, Error)]
pub enum BridgeError {
    /// Could not parse / resolve the bridge URL.
    #[error("invalid bridge URL: {0}")]
    BadUrl(String),

    /// TCP connect or transport failure.
    #[error("io: {0}")]
    Io(String),

    /// Bridge daemon returned an `"error"` payload.
    #[error("bridge error: {0}")]
    Remote(String),

    /// Reply could not be deserialised into the expected shape.
    #[error("decode: {0}")]
    Decode(String),
}

impl From<std::io::Error> for BridgeError {
    fn from(e: std::io::Error) -> Self { BridgeError::Io(e.to_string()) }
}

const CONNECT_TIMEOUT: Duration = Duration::from_secs(5);
// EEPROM reads can run 50-75 s when the ECU is unresponsive (each of
// the 256 cell reads waits its 180 ms recv timeout, plus init pauses).
// Give the daemon enough headroom to finish + report a clean
// `bytes=0` response so the UI shows "ECU silent" instead of a TCP
// timeout. ROM-dump operations push this even higher so 240 s buys
// us comfortable room for the largest local routine too.
const RW_TIMEOUT:      Duration = Duration::from_secs(240);

/// Resolve a `tcp://host:port`, `host:port`, or bare `host` URL into a
/// concrete socket address. Defaults to port 7878 when the URL omits
/// it.
fn resolve(url: &str) -> Result<SocketAddr, BridgeError> {
    let trimmed = url.trim().trim_start_matches("tcp://");
    let with_port = if trimmed.contains(':') {
        trimmed.to_string()
    } else {
        format!("{trimmed}:7878")
    };
    with_port
        .to_socket_addrs()
        .map_err(|e| BridgeError::BadUrl(format!("{url:?}: {e}")))?
        .next()
        .ok_or_else(|| BridgeError::BadUrl(format!("{url:?} resolved to nothing")))
}

/// Send one `(method, params)` pair and decode the JSON `result` into
/// `T`. The caller picks `T` (e.g. `Vec<PortInfo>` or
/// `EepromReadResult`).
pub fn call<T>(url: &str, method: &str, params: Value) -> Result<T, BridgeError>
where
    T: for<'de> Deserialize<'de>,
{
    let addr = resolve(url)?;
    let stream = TcpStream::connect_timeout(&addr, CONNECT_TIMEOUT)
        .map_err(|e| BridgeError::Io(format!("connect {addr}: {e}")))?;
    stream.set_nodelay(true).ok();
    stream.set_read_timeout(Some(RW_TIMEOUT)).ok();
    stream.set_write_timeout(Some(RW_TIMEOUT)).ok();

    // Use millisecond-precision id so concurrent clients don't collide.
    let id = std::time::SystemTime::now()
        .duration_since(std::time::UNIX_EPOCH)
        .map(|d| d.as_millis() as u64)
        .unwrap_or(0);
    let req = json!({ "id": id, "method": method, "params": params });
    let mut writer = stream.try_clone()?;
    writeln!(writer, "{req}")?;
    writer.flush()?;

    let mut reader = BufReader::new(stream);
    let mut line = String::new();
    reader.read_line(&mut line).map_err(|e| BridgeError::Io(e.to_string()))?;
    if line.is_empty() {
        return Err(BridgeError::Io("empty response (server closed)".into()));
    }

    // Generic envelope mirrors the daemon's response shape. `Option<_>`
    // already defaults to `None` when the field is absent so we don't
    // need `#[serde(default)]` (which would force `R: Default`).
    #[derive(Deserialize)]
    struct Envelope<R> {
        result: Option<R>,
        error:  Option<String>,
    }
    let env: Envelope<T> = serde_json::from_str(line.trim())
        .map_err(|e| BridgeError::Decode(format!("{e} (raw: {})", line.trim())))?;
    if let Some(err) = env.error {
        return Err(BridgeError::Remote(err));
    }
    env.result.ok_or_else(|| BridgeError::Decode("empty result".into()))
}

// ---- Method-specific result types (mirror the daemon's proto.rs) ----

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct PortInfoDto {
    pub index: u32,
    pub serial: String,
    pub description: String,
    /// Daemon-side transport tag: `"d2xx"`, `"libusb"`, or `"mock"`.
    /// Older daemons (pre-libusb support) omit this field; we default
    /// to `"d2xx"` so a mismatched-version client still works.
    #[serde(default = "default_backend")]
    pub backend: String,
}

fn default_backend() -> String { "d2xx".to_string() }

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct EepromReadResultDto {
    pub family: String,
    pub bytes: Vec<u8>,
    pub duration_ms: u64,
    pub log: Vec<String>,
}

/// Convenience: `list_ports` over the bridge.
pub fn list_ports(url: &str) -> Result<Vec<PortInfoDto>, BridgeError> {
    call::<Vec<PortInfoDto>>(url, "list_ports", Value::Null)
}

/// Convenience: `read_eeprom` over the bridge. `variant` is `"keihin"`
/// or `"shinden"`. `backend` selects the daemon-side transport
/// (`"d2xx"` / `"libusb"`); pass an empty string to let the daemon
/// fall back to its default.
pub fn read_eeprom(
    url: &str,
    variant: &str,
    device_index: u32,
    backend: &str,
) -> Result<EepromReadResultDto, BridgeError> {
    call::<EepromReadResultDto>(
        url,
        "read_eeprom",
        json!({
            "variant":      variant,
            "device_index": device_index,
            "backend":      backend,
        }),
    )
}

/// Convenience: `ping` for liveness checks.
pub fn ping(url: &str) -> Result<(), BridgeError> {
    let _: Value = call(url, "ping", Value::Null)?;
    Ok(())
}

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct LiveSampleResultDto {
    /// Echo-stripped TABLE_17 reply (24 bytes when ECU is responsive).
    pub table16: Vec<u8>,
    /// Echo-stripped TABLE_20 reply (8 bytes when ECU is responsive).
    pub table20: Vec<u8>,
    /// End-to-end duration measured by the daemon.
    pub duration_ms: u64,
    /// Per-step log lines from the daemon.
    pub log: Vec<String>,
}

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DumpRomResultDto {
    /// Echoes the requested size label (`"48K"` or `"64K"`).
    pub size: String,
    /// Raw ROM bytes - 49 152 (48K) or 32 768 (64K) on success.
    pub bytes: Vec<u8>,
    /// End-to-end duration measured by the daemon.
    pub duration_ms: u64,
    /// Per-step log lines from the daemon.
    pub log: Vec<String>,
}

/// Convenience: `dump_rom` over the bridge. `size` is `"48K"` or
/// `"64K"`; `backend` selects the daemon-side transport (`"d2xx"` /
/// `"libusb"`). The chunked Shinden read can take ~1 minute even on
/// good hardware, so callers should expect a long-running RPC.
pub fn dump_rom(
    url: &str,
    size: &str,
    device_index: u32,
    backend: &str,
) -> Result<DumpRomResultDto, BridgeError> {
    call::<DumpRomResultDto>(
        url,
        "dump_rom",
        json!({
            "size":         size,
            "device_index": device_index,
            "backend":      backend,
        }),
    )
}

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct EcmIdResultDto {
    pub raw_hex:     String,
    pub ecm_id:      Option<String>,
    pub duration_ms: u64,
    pub log:         Vec<String>,
}

/// Convenience: `read_ecm_id` over the bridge. Drops any cached
/// live-data session daemon-side, runs WAKEUP+ESTABLISH and returns
/// the captured reply.
pub fn read_ecm_id(
    url: &str,
    device_index: u32,
    backend: &str,
) -> Result<EcmIdResultDto, BridgeError> {
    call::<EcmIdResultDto>(
        url,
        "read_ecm_id",
        json!({
            "device_index": device_index,
            "backend":      backend,
        }),
    )
}

/// Convenience: `read_live_sample` over the bridge. The daemon opens
/// the FTDI, runs WAKEUP + ESTABLISH + TABLE_17 + TABLE_20, and closes
/// in one shot - so each call costs ~1 s end-to-end. The frontend's
/// 60 Hz smoothing layer hides the gap; if you need higher native
/// poll rates, add a persistent-session method on the daemon.
pub fn read_live_sample(
    url: &str,
    device_index: u32,
    backend: &str,
) -> Result<LiveSampleResultDto, BridgeError> {
    call::<LiveSampleResultDto>(
        url,
        "read_live_sample",
        json!({
            "device_index": device_index,
            "backend":      backend,
        }),
    )
}
