//! JSON wire protocol used between the main app and the bridge daemon.
//!
//! Each message is one JSON object on its own line (LF-terminated).
//! The shape is loosely based on JSON-RPC 2.0 but intentionally
//! simplified - we control both ends of the wire so we don't need
//! batching, named-vs-positional params, etc.

use serde::{Deserialize, Serialize};

/// Inbound request from the client. The `id` is echoed in the reply
/// so the client can match request → response over a long-lived
/// connection.
#[derive(Debug, Deserialize)]
pub struct Request {
    /// Caller-chosen identifier (any JSON value, but typically u64).
    pub id: serde_json::Value,
    /// Method name - one of [`Method`].
    pub method: String,
    /// Method-specific parameters (deserialised lazily inside handlers).
    #[serde(default)]
    pub params: serde_json::Value,
}

/// Outbound reply. Either `result` *or* `error` is set, never both.
#[derive(Debug, Serialize)]
pub struct Response {
    /// Echoes the request id.
    pub id: serde_json::Value,
    /// Result payload (method-specific).
    #[serde(skip_serializing_if = "Option::is_none")]
    pub result: Option<serde_json::Value>,
    /// Error description if the call failed.
    #[serde(skip_serializing_if = "Option::is_none")]
    pub error: Option<String>,
}

impl Response {
    /// Build a successful response.
    pub fn ok(id: serde_json::Value, result: serde_json::Value) -> Self {
        Self { id, result: Some(result), error: None }
    }
    /// Build an error response.
    pub fn err(id: serde_json::Value, error: impl Into<String>) -> Self {
        Self { id, result: None, error: Some(error.into()) }
    }
}

// ---- Method-specific param / result types ---------------------------

/// Parameters for `read_eeprom`.
#[derive(Debug, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ReadEepromParams {
    /// Honda family - Keihin or Shinden.
    pub variant: VariantArg,
    /// Optional FTDI device index (defaults to 0). Indices are scoped
    /// to a single backend - see [`PortInfo::backend`].
    #[serde(default)]
    pub device_index: Option<u32>,
    /// Which transport to open the device through. `"d2xx"` (FTDI's
    /// official driver) or `"libusb"` (WinUSB / libusbK after Zadig).
    /// Omit / null defaults to `"d2xx"` for backward compatibility
    /// with older clients.
    #[serde(default)]
    pub backend: Option<String>,
}

/// Tagged union for the ECU variant.
#[derive(Debug, Deserialize)]
#[serde(rename_all = "lowercase")]
pub enum VariantArg {
    Keihin,
    Shinden,
}

/// Item returned in the `list_ports` array.
#[derive(Debug, Serialize)]
pub struct PortInfo {
    /// FTDI driver index (used as `device_index` in subsequent calls).
    /// Scoped per [`PortInfo::backend`].
    pub index: u32,
    /// Hardware serial number reported by the FTDI EEPROM.
    pub serial: String,
    /// Human-friendly product description.
    pub description: String,
    /// Which transport the daemon used to discover this entry:
    /// `"d2xx"`, `"libusb"`, or `"mock"`. The client must echo this
    /// back in the `backend` field of subsequent calls so the daemon
    /// opens the device through the right driver.
    pub backend: String,
}

/// Result payload of `read_eeprom`.
#[derive(Debug, Serialize)]
pub struct EepromReadResult {
    /// Echoes the requested ECU family ("Keihin" / "Shinden").
    pub family: String,
    /// EEPROM contents (typically 512 bytes - 256 cells × 2 bytes).
    pub bytes: Vec<u8>,
    /// End-to-end duration in milliseconds.
    pub duration_ms: u64,
    /// Per-step log lines collected during the operation - useful for
    /// debugging the K-Line conversation from the client side.
    pub log: Vec<String>,
}

/// Parameters for `read_live_sample`. Same shape as `ReadEepromParams`
/// minus the `variant` field (live data uses a fixed Honda KWP poll).
#[derive(Debug, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ReadLiveSampleParams {
    #[serde(default)]
    pub device_index: Option<u32>,
    #[serde(default)]
    pub backend: Option<String>,
}

/// Parameters for `dump_rom`.
#[derive(Debug, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct DumpRomParams {
    /// Dump preset - currently `"48K"` (upper half) or `"64K"` (upper
    /// quarter). Same labels the local Tauri `dump_rom` accepts.
    pub size: String,
    #[serde(default)]
    pub device_index: Option<u32>,
    #[serde(default)]
    pub backend: Option<String>,
}

/// Result payload of `dump_rom`.
#[derive(Debug, Serialize)]
pub struct DumpRomResult {
    /// Echoes the requested size label so the client doesn't have to
    /// remember which call this reply belongs to.
    pub size: String,
    /// Raw ROM bytes - 49 152 (48K) or 32 768 (64K) on success, fewer
    /// when the ECU went silent mid-dump.
    pub bytes: Vec<u8>,
    /// End-to-end duration in milliseconds (open + init + chunked read).
    pub duration_ms: u64,
    /// Per-step log lines, identical in spirit to `EepromReadResult.log`.
    pub log: Vec<String>,
}

/// Result payload of `read_live_sample`. Mirrors the local
/// `LiveSampleDto` so the frontend parser can be shared verbatim.
#[derive(Debug, Serialize)]
pub struct ReadLiveSampleResult {
    /// 24-byte echo-stripped reply to TABLE_17 (or empty if ECU silent).
    pub table16: Vec<u8>,
    /// 8-byte echo-stripped reply to TABLE_20 (or empty if ECU silent).
    pub table20: Vec<u8>,
    /// End-to-end duration in milliseconds (open + init + poll).
    pub duration_ms: u64,
    /// Per-step log lines, identical in spirit to `EepromReadResult.log`.
    pub log: Vec<String>,
}
