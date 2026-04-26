//! Helpers for emitting K-Line traffic logs to the frontend in real time.
//!
//! Each TX or RX byte block becomes a `kline-log` Tauri event so the UI
//! can render a live scrolling console.

use serde::Serialize;
use std::time::{SystemTime, UNIX_EPOCH};
use tauri::{AppHandle, Emitter};

/// One line of K-Line traffic. Matches the TS interface in
/// `app/src/lib/tauri.ts -> KlineLogLine`.
#[derive(Serialize, Clone)]
pub struct LogLine {
    /// "tx" - cable -> ECU, "rx" - ECU -> cable, "info" - free-form.
    pub dir: String,
    /// Hex string with single-space separators e.g. "91 91 0D DF".
    pub hex: String,
    /// Number of bytes in this frame (len of the hex array).
    pub len: usize,
    /// Optional human-readable note (used for "info" dir).
    pub msg: Option<String>,
    /// UNIX-ms timestamp at the time of emission.
    pub ts: u64,
}

fn now_ms() -> u64 {
    SystemTime::now()
        .duration_since(UNIX_EPOCH)
        .map(|d| d.as_millis() as u64)
        .unwrap_or(0)
}

fn hex_str(bytes: &[u8]) -> String {
    let mut out = String::with_capacity(bytes.len() * 3);
    for (i, b) in bytes.iter().enumerate() {
        if i > 0 { out.push(' '); }
        out.push_str(&format!("{:02X}", b));
    }
    out
}

/// Emit a TX line ("cable -> ECU").
pub fn tx(app: &AppHandle, bytes: &[u8]) {
    let _ = app.emit("kline-log", LogLine {
        dir: "tx".into(),
        hex: hex_str(bytes),
        len: bytes.len(),
        msg: None,
        ts: now_ms(),
    });
}

/// Emit an RX line ("ECU -> cable").
pub fn rx(app: &AppHandle, bytes: &[u8]) {
    let _ = app.emit("kline-log", LogLine {
        dir: "rx".into(),
        hex: hex_str(bytes),
        len: bytes.len(),
        msg: None,
        ts: now_ms(),
    });
}

/// Emit a free-form info / status line (no bytes).
pub fn info(app: &AppHandle, msg: impl Into<String>) {
    let _ = app.emit("kline-log", LogLine {
        dir: "info".into(),
        hex: String::new(),
        len: 0,
        msg: Some(msg.into()),
        ts: now_ms(),
    });
}

/// Emit an error / warning line.
pub fn err(app: &AppHandle, msg: impl Into<String>) {
    let _ = app.emit("kline-log", LogLine {
        dir: "err".into(),
        hex: String::new(),
        len: 0,
        msg: Some(msg.into()),
        ts: now_ms(),
    });
}

/// Progress payload emitted on the `kline-progress` event channel.
#[derive(Serialize, Clone)]
pub struct Progress {
    pub label:   String,
    pub current: u64,
    pub total:   u64,
    pub percent: u8,
    pub ts:      u64,
}

/// Emit a progress update (used by long-running operations like ROM
/// dump / EEPROM read so the UI can render a smooth progress bar).
pub fn progress(app: &AppHandle, label: impl Into<String>, current: u64, total: u64) {
    let percent = if total == 0 { 0 } else { ((current * 100) / total).min(100) as u8 };
    let _ = app.emit("kline-progress", Progress {
        label: label.into(),
        current,
        total,
        percent,
        ts: now_ms(),
    });
}
