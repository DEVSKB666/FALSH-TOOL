//! Persistent live-data session.
//!
//! Tauri commands `livedata_start`, `livedata_poll`, `livedata_stop` use
//! a single FTDI handle held in shared state (guarded by a Mutex), which
//! eliminates the per-poll cost of:
//!
//! - Re-opening the FTDI device (~70 ms on libusb)
//! - Bit-bang K-Line wakeup (~50 ms)
//! - 921600 -> 10400 baud handover (~70 ms)
//! - Honda KWP `WAKEUP` + `ESTABLISH` (~360 ms with our 180 ms recv timeout)
//!
//! Net effect: an ongoing poll cycle drops from ~1 s to ~420 ms (just
//! TABLE_16 + TABLE_20), giving the live-data UI a smooth ~2 Hz feed
//! without any code changes on the frontend other than swapping
//! `read_live_sample` for `livedata_poll`.

use std::sync::Mutex;
use std::time::Instant;
use serde::Serialize;
use tauri::{AppHandle, State};

use crate::kline_log;
use crate::libusb_ftdi::LibusbKLine;
use crate::livedata as livedata_mod;
use crate::transport::{FtdiKLine, KLine};

/// One live-data sample returned by `livedata_poll`.
#[derive(Serialize, Clone)]
pub struct LivePollDto {
    pub table16:     Vec<u8>,
    pub table20:     Vec<u8>,
    pub duration_ms: u64,
    /// Whether the backend re-ran the WAKEUP/ESTABLISH handshake on this
    /// poll (because the previous one came back empty - useful diagnostic
    /// for the UI to show "reconnecting" indicators).
    pub re_established: bool,
}

/// Internal per-session state - the live FTDI handle and a watermark
/// of when the last successful TABLE reply landed (used to decide
/// whether to re-issue the KWP handshake).
struct Session {
    transport:        Box<dyn KLine + Send>,
    last_reply_at:    Instant,
    /// True until the very first successful reply lands, after which
    /// re-init is only triggered on consecutive empty polls.
    pending_first:    bool,
}

/// Tauri-managed shared state.
#[derive(Default)]
pub struct LiveSessionState(pub Mutex<Option<Session>>);

/// Open the FTDI device and run Honda KWP wakeup+establish. Idempotent:
/// calling this while a session is already active will tear down the
/// old one first (so the user can switch backends without restarting).
#[tauri::command(async)]
pub fn livedata_start(
    app:          AppHandle,
    state:        State<'_, LiveSessionState>,
    device_index: Option<u32>,
    backend:      Option<String>,
) -> Result<(), String> {
    let idx     = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let mut log = Vec::new();

    kline_log::info(&app, format!("--- Live data session START on {} #{} ---", backend, idx));

    let mut transport: Box<dyn KLine + Send> = match backend.as_str() {
        "libusb" => Box::new(LibusbKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
        _        => Box::new(FtdiKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
    };

    livedata_mod::establish(transport.as_mut(), &mut log).map_err(|e| e.to_string())?;

    let mut guard = state.0.lock().map_err(|e| e.to_string())?;
    *guard = Some(Session {
        transport,
        last_reply_at: Instant::now(),
        pending_first: true,
    });

    Ok(())
}

/// Poll TABLE_16 + TABLE_20 once. If both come back empty (ECU session
/// expired) we transparently re-issue WAKEUP + ESTABLISH and retry the
/// tables a single time before reporting empty results to the caller.
#[tauri::command(async)]
pub fn livedata_poll(
    app:   AppHandle,
    state: State<'_, LiveSessionState>,
) -> Result<LivePollDto, String> {
    let started = Instant::now();
    let mut log = Vec::new();
    let mut re_established = false;

    let mut guard = state.0.lock().map_err(|e| e.to_string())?;
    let session = guard.as_mut().ok_or_else(|| "live session not started".to_string())?;

    // First pass - just hit the tables.
    let (mut t16, mut t20) = livedata_mod::poll_tables(session.transport.as_mut(), &mut log)
        .map_err(|e| e.to_string())?;

    // If both empty, the ECU has dropped the session. Re-establish and
    // try once more. We only ever do this once per call to keep the
    // worst-case latency bounded.
    if t16.is_empty() && t20.is_empty() {
        kline_log::info(&app, "[livedata] empty reply -> re-establishing");
        livedata_mod::establish(session.transport.as_mut(), &mut log).map_err(|e| e.to_string())?;
        let (a, b) = livedata_mod::poll_tables(session.transport.as_mut(), &mut log)
            .map_err(|e| e.to_string())?;
        t16 = a;
        t20 = b;
        re_established = true;
    }

    if !t16.is_empty() || !t20.is_empty() {
        session.last_reply_at = Instant::now();
        session.pending_first = false;
    }

    Ok(LivePollDto {
        table16:        t16,
        table20:        t20,
        duration_ms:    started.elapsed().as_millis() as u64,
        re_established,
    })
}

/// Drop the FTDI handle and clear session state. Safe to call even if
/// no session is currently open.
#[tauri::command(async)]
pub fn livedata_stop(
    app:   AppHandle,
    state: State<'_, LiveSessionState>,
) -> Result<(), String> {
    let mut guard = state.0.lock().map_err(|e| e.to_string())?;
    if guard.is_some() {
        kline_log::info(&app, "--- Live data session STOP ---");
    }
    *guard = None;
    Ok(())
}
