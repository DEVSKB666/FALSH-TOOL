//! Honda Keihin / Shinden EEPROM read.
//!
//! Direct port of the routines in `app/src-tauri/src/eeprom.rs` minus
//! the Tauri-specific event logging. Every byte sequence here was
//! lifted from the deobfuscated `Form_EepromTool.cs` source under
//! `MzaTunerClone_Final/` (see the JOURNEY notes in the main repo).

use crate::transport::{KLine, TransportError};
use std::time::Duration;

const FRAME_TIMEOUT: Duration = Duration::from_millis(180);
const STEP_PAUSE_MS: u64 = 150;

/// Tolerant `round_trip` - silent ECU (recv timeout) is logged and
/// returns an empty vec instead of bubbling. Real I/O / driver faults
/// still propagate. Mirrors the behaviour of the C# original where
/// every send was guarded with `if (M_3C(...))`.
fn try_send(
    t: &mut dyn KLine,
    frame: &[u8],
    log: &mut Vec<String>,
    label: &str,
) -> Result<Vec<u8>, TransportError> {
    match t.round_trip(frame, FRAME_TIMEOUT) {
        Ok(r) => Ok(r),
        Err(TransportError::Io(msg)) if msg.contains("recv") => {
            log.push(format!("[{label}] no reply ({msg})"));
            Ok(Vec::new())
        }
        Err(e) => Err(e),
    }
}

fn sleep_ms(ms: u64) {
    std::thread::sleep(Duration::from_millis(ms));
}

// ---- Common Honda init payloads -------------------------------------

const WAKEUP:        [u8; 4]  = [0xFE, 0x04, 0x72, 0x8C];
const ESTABLISH:     [u8; 5]  = [0x72, 0x05, 0x00, 0xF0, 0x99];

const KH_START_DIAG: [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x9E, 0x8D, 0x9A, 0x86, 0x90, 0x8A, 0x8C, 0x9B, 0x88];
const KH_FOLLOWUP:   [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x92, 0x9E, 0x86, 0x96, 0x8B, 0x8D, 0x86, 0xC0, 0x6A];

const SH_HELLO:      [u8; 11] = [0x27, 0x0B, 0xE0, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x48, 0x6F, 0x43];
const SH_WAREYOU:    [u8; 11] = [0x27, 0x0B, 0xE0, 0x77, 0x41, 0x72, 0x65, 0x59, 0x6F, 0x75, 0x22];
const SH_SEED:       [u8; 6]  = [0x7E, 0x06, 0x01, 0x01, 0x00, 0x7A];
const SH_START_ADDR: [u8; 6]  = [0x82, 0x82, 0x10, 0x06, 0x00, 0xE6];

/// Read 256 EEPROM cells from a Keihin ECU - returns 512 bytes total.
pub fn read_eeprom_keihin(t: &mut dyn KLine, log: &mut Vec<String>) -> Result<Vec<u8>, TransportError> {
    log.push("[keihin] init sequence".into());
    try_send(t, &WAKEUP,        log, "kh-wakeup")?;    sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "kh-establish")?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &KH_START_DIAG, log, "kh-startdiag")?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &KH_FOLLOWUP,   log, "kh-followup")?;

    log.push("[keihin] reading 256 cells".into());
    let mut out = Vec::with_capacity(512);
    let req: [u8; 7] = [0x91, 0x91, 0x07, 0x40, 0x00, 0x00, 0x00];
    for i in 0u32..256 {
        let resp = try_send(t, &req, log, "kh-cell")?;
        sleep_ms(100);
        if resp.len() > 12 {
            out.push(resp[11]);
            out.push(resp[12]);
        }
        if i % 32 == 31 {
            log.push(format!("[keihin] {}/256 cells", i + 1));
        }
    }
    log.push(format!("[keihin] done - {} bytes", out.len()));
    Ok(out)
}

/// Read 256 EEPROM cells from a Shinden ECU - returns 512 bytes total.
pub fn read_eeprom_shinden(t: &mut dyn KLine, log: &mut Vec<String>) -> Result<Vec<u8>, TransportError> {
    log.push("[shinden] init sequence".into());
    try_send(t, &WAKEUP,        log, "sh-wakeup")?;    sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "sh-establish")?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,      log, "sh-hello")?;     sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU,    log, "sh-wareyou")?;
    try_send(t, &SH_SEED,       log, "sh-seed")?;
    try_send(t, &SH_START_ADDR, log, "sh-startaddr")?;

    log.push("[shinden] reading 256 cells".into());
    let mut out = Vec::with_capacity(512);
    for i in 0u8..=u8::MAX {
        let frame: [u8; 6] = [0x82, 0x82, 0x10, 0x06, i, 0xE6u8.wrapping_sub(i)];
        let resp = try_send(t, &frame, log, "sh-cell")?;
        if resp.len() > 11 {
            out.push(resp[10]);
            out.push(resp[11]);
        }
        if i % 32 == 31 {
            log.push(format!("[shinden] {}/256 cells", (i as u32) + 1));
        }
        sleep_ms(15);
    }
    log.push(format!("[shinden] done - {} bytes", out.len()));
    Ok(out)
}
