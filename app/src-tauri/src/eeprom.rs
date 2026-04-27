//! Honda Keihin / Shinden EEPROM operations - direct port of the
//! `Form_EepromTool.cs` worker threads (see ARCHITECTURE.md s3.3 / s3.4).
//!
//! Every byte sequence and offset interpretation here was extracted from
//! the deobfuscated source under `MzaTunerClone_Final/`. Functions take
//! a generic `&mut dyn KLine` so callers can swap between the FTDI D2XX
//! and the libusb backend at runtime.

use crate::kline_log;
use crate::transport::{KLine, TransportError};
use std::time::Duration;
use tauri::AppHandle;

const FRAME_TIMEOUT: Duration = Duration::from_millis(180);
const STEP_PAUSE_MS: u64 = 150;

/// `try_send` is the workhorse used by every EEPROM op. It wraps
/// `t.round_trip` so that a timeout (= ECU silent) is **logged but not
/// fatal** - mirroring the original C# code where every send was guarded
/// by `if (this.M_3C(...))`. Real driver / USB errors still propagate.
fn try_send(
    t: &mut dyn KLine,
    frame: &[u8],
    log: &mut Vec<String>,
    label: &str,
    timeout: Duration,
) -> Result<Vec<u8>, TransportError> {
    match t.round_trip(frame, timeout) {
        Ok(resp) => Ok(resp),
        Err(TransportError::Io(msg)) if msg.contains("recv") => {
            log.push(format!("[{label}] no reply ({msg})"));
            Ok(Vec::new()) // soft-fail - keep going so the loop can finish
        }
        Err(e) => Err(e),
    }
}

// ---- Common init payloads --------------------------------------------

/// Honda K-Line wakeup byte. Same for both Keihin and Shinden.
const WAKEUP:        [u8; 4] = [0xFE, 0x04, 0x72, 0x8C];
/// Communication establishment frame.
const ESTABLISH:     [u8; 5] = [0x72, 0x05, 0x00, 0xF0, 0x99];

/// Keihin "start diagnostic" frame.
const KH_START_DIAG: [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x9E, 0x8D, 0x9A, 0x86, 0x90, 0x8A, 0x8C, 0x9B, 0x88];
/// Keihin "follow-up" frame.
const KH_FOLLOWUP:   [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x92, 0x9E, 0x86, 0x96, 0x8B, 0x8D, 0x86, 0xC0, 0x6A];

/// Shinden "HelloHoC" security access.
const SH_HELLO:      [u8; 11] = [0x27, 0x0B, 0xE0, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x48, 0x6F, 0x43];
/// Shinden "wAreYou\"" security access.
const SH_WAREYOU:    [u8; 11] = [0x27, 0x0B, 0xE0, 0x77, 0x41, 0x72, 0x65, 0x59, 0x6F, 0x75, 0x22];
/// Shinden "seed" / start address read.
const SH_SEED:       [u8; 6]  = [0x7E, 0x06, 0x01, 0x01, 0x00, 0x7A];
/// Shinden start-address probe.
const SH_START_ADDR: [u8; 6]  = [0x82, 0x82, 0x10, 0x06, 0x00, 0xE6];

// ----------- READ EEPROM ----------------------------------------------

/// Read 256 EEPROM cells from a Keihin ECU - returns 512 bytes total.
pub fn read_eeprom_keihin(t: &mut dyn KLine, log: &mut Vec<String>, app: Option<&AppHandle>) -> Result<Vec<u8>, TransportError> {
    log.push("[keihin] init sequence".to_string());
    try_send(t, &WAKEUP,        log, "kh-wakeup",    FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "kh-establish", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &KH_START_DIAG, log, "kh-startdiag", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &KH_FOLLOWUP,   log, "kh-followup",  FRAME_TIMEOUT)?;

    log.push("[keihin] reading 256 cells".to_string());
    let mut out = Vec::with_capacity(512);
    let req: [u8; 7] = [0x91, 0x91, 0x07, 0x40, 0x00, 0x00, 0x00];
    for i in 0u32..256 {
        let resp = try_send(t, &req, log, "kh-cell", FRAME_TIMEOUT)?;
        sleep_ms(100);
        if resp.len() > 12 {
            out.push(resp[11]);
            out.push(resp[12]);
        }
        if let Some(app) = app {
            kline_log::progress(app, "Read EEPROM Keihin", (i + 1) as u64, 256);
        }
    }
    log.push(format!("[keihin] read {} bytes", out.len()));
    Ok(out)
}

/// Read 256 EEPROM cells from a Shinden ECU - returns 512 bytes total.
pub fn read_eeprom_shinden(t: &mut dyn KLine, log: &mut Vec<String>, app: Option<&AppHandle>) -> Result<Vec<u8>, TransportError> {
    log.push("[shinden] init sequence".to_string());
    try_send(t, &WAKEUP,        log, "sh-wakeup",    FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "sh-establish", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,      log, "sh-hello",     FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU,    log, "sh-wareyou",   FRAME_TIMEOUT)?;
    try_send(t, &SH_SEED,       log, "sh-seed",      FRAME_TIMEOUT)?;
    try_send(t, &SH_START_ADDR, log, "sh-startaddr", FRAME_TIMEOUT)?;

    log.push("[shinden] reading 256 cells".to_string());
    let mut out = Vec::with_capacity(512);
    for i in 0u8..=u8::MAX {
        let frame: [u8; 6] = [0x82, 0x82, 0x10, 0x06, i, 0xE6u8.wrapping_sub(i)];
        let resp = try_send(t, &frame, log, "sh-cell", FRAME_TIMEOUT)?;
        if resp.len() > 11 {
            out.push(resp[10]);
            out.push(resp[11]);
        }
        if let Some(app) = app {
            kline_log::progress(app, "Read EEPROM Shinden", (i as u64) + 1, 256);
        }
        sleep_ms(15);
    }
    log.push(format!("[shinden] read {} bytes", out.len()));
    Ok(out)
}

// ----------- RESET FLASH COUNTER --------------------------------------

/// Keihin "ลบจำนวนการอัด" - sends start-diag + follow-up, then the
/// actual reset frame `91 91 09 41 0E 00 00 00 A8`.
pub fn reset_flash_count_keihin(t: &mut dyn KLine, log: &mut Vec<String>) -> Result<(), TransportError> {
    log.push("[keihin] reset flash counter".into());
    try_send(t, &KH_START_DIAG, log, "kh-startdiag", FRAME_TIMEOUT)?; sleep_ms(100);
    try_send(t, &KH_FOLLOWUP,   log, "kh-followup",  FRAME_TIMEOUT)?; sleep_ms(100);
    let reset: [u8; 9] = [0x91, 0x91, 0x09, 0x41, 0x0E, 0x00, 0x00, 0x00, 0xA8];
    try_send(t, &reset, log, "kh-reset", FRAME_TIMEOUT)?;
    sleep_ms(1000);
    log.push("[keihin] reset done".into());
    Ok(())
}

/// Shinden reset flash count - probes 128 sub-addresses until the ECU
/// echoes back the magic ack response.
pub fn reset_flash_count_shinden(t: &mut dyn KLine, log: &mut Vec<String>, app: Option<&AppHandle>) -> Result<bool, TransportError> {
    log.push("[shinden] reset flash counter".into());
    // Standard Shinden init.
    try_send(t, &WAKEUP,     log, "sh-wakeup",    FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,  log, "sh-establish", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,   log, "sh-hello",     FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU, log, "sh-wareyou",   FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);

    // Reset session start.
    let init_reset: [u8; 8] = [0x82, 0x82, 0x14, 0x08, 0x00, 0x01, 0x00, 0xDF];
    try_send(t, &init_reset, log, "sh-resetinit", FRAME_TIMEOUT)?;
    sleep_ms(150);

    // Sweep i=0..127 looking for the ack response.
    let ack: [u8; 13] = [0x82, 0x82, 0x14, 0x08, 0x7F, 0x00, 0x00, 0x61, 0x92, 0x92, 0x14, 0x05, 0xC3];
    for i in 0u8..=127 {
        // Build [82 82 14 08 i 00 00 <chk>] - chk = 256 - sum(0..6) & 0xFF.
        let mut frame: [u8; 8] = [0x82, 0x82, 0x14, 0x08, i, 0x00, 0x00, 0x00];
        let chk: u32 = frame[..7].iter().map(|b| *b as u32).sum();
        frame[7] = (256u32.wrapping_sub(chk & 0xFF) & 0xFF) as u8;
        let resp = try_send(t, &frame, log, "sh-resetscan", FRAME_TIMEOUT)?;
        if resp.as_slice() == &ack[..] {
            log.push(format!("[shinden] reset ack received at i={i}"));
            if let Some(app) = app {
                kline_log::progress(app, "Reset Shinden flash counter", 128, 128);
            }
            return Ok(true);
        }
        if let Some(app) = app {
            kline_log::progress(app, "Reset Shinden flash counter", (i as u64) + 1, 128);
        }
        sleep_ms(15);
    }
    log.push("[shinden] reset ack NOT received in 128 probes".into());
    Ok(false)
}

// ----------- FORMAT EEPROM (Shinden, destructive) ---------------------

/// Format the Shinden EEPROM with a constant byte (`0x00` or `0xFF`).
/// **Destructive** - the caller's UI must show a confirmation first.
///
/// Wire format:
///     fill = 0x00 -> [82 82 18 05 DF] (2x)
///     fill = 0xFF -> [82 82 19 05 DE] (2x)
/// Expected ack response:
///     0x00 -> "82 82 18 05 DF 92 92 18 05 BF"
///     0xFF -> "82 82 19 05 DE 92 92 19 05 BE"
pub fn format_eeprom_shinden(t: &mut dyn KLine, log: &mut Vec<String>, fill: u8) -> Result<bool, TransportError> {
    log.push(format!("[shinden] FORMAT EEPROM with fill = 0x{:02X}", fill));
    try_send(t, &WAKEUP,     log, "sh-wakeup",    FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,  log, "sh-establish", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,   log, "sh-hello",     FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU, log, "sh-wareyou",   FRAME_TIMEOUT)?;

    let (cmd, ack): (&[u8], &[u8]) = match fill {
        0x00 => (&[0x82, 0x82, 0x18, 0x05, 0xDF], &[0x82, 0x82, 0x18, 0x05, 0xDF, 0x92, 0x92, 0x18, 0x05, 0xBF]),
        0xFF => (&[0x82, 0x82, 0x19, 0x05, 0xDE], &[0x82, 0x82, 0x19, 0x05, 0xDE, 0x92, 0x92, 0x19, 0x05, 0xBE]),
        other => return Err(TransportError::Io(format!("format byte must be 0x00 or 0xFF, got 0x{other:02X}"))),
    };

    // Source sends the command twice (ECU sometimes ignores the first).
    let _ = try_send(t, cmd, log, "sh-format-1", FRAME_TIMEOUT);
    let resp = try_send(t, cmd, log, "sh-format-2", FRAME_TIMEOUT)?;
    sleep_ms(15);

    let ok = resp.as_slice() == ack;
    if ok {
        log.push("[shinden] format ack received".into());
    } else {
        log.push(format!("[shinden] format ack mismatch (got {} bytes, expected {})", resp.len(), ack.len()));
    }
    Ok(ok)
}

// ----------- ROM DUMP (48K / 64K Shinden) -----------------------------

/// Dump the upper 48 KB of the Shinden ROM via the `82 82 00 09 ...`
/// chunked-read protocol. Each request fetches 12 bytes; total = 49152 bytes.
pub fn dump_rom_shinden_48k(t: &mut dyn KLine, log: &mut Vec<String>, app: Option<&AppHandle>) -> Result<Vec<u8>, TransportError> {
    dump_rom_shinden_inner(t, log, app, /*page=*/ 0x00, /*start_hi=*/ 0x40, /*total=*/ 49152, "ROM Dump 48K")
}

/// Dump the upper 32 KB of the Shinden ROM (label "64K" in the original UI).
pub fn dump_rom_shinden_64k(t: &mut dyn KLine, log: &mut Vec<String>, app: Option<&AppHandle>) -> Result<Vec<u8>, TransportError> {
    dump_rom_shinden_inner(t, log, app, /*page=*/ 0x00, /*start_hi=*/ 0x80, /*total=*/ 32768, "ROM Dump 64K")
}

/// **Experimental** 256 KB dump - byte 4 of the read frame (which the
/// Shinden 48K/64K paths leave at `0x00`) is repurposed as a page
/// selector and we sweep pages `0x00..0x03`, each covering the full
/// 16-bit `0x0000..0xFFFF` address space. The original C# tool only
/// references 256K+ sizes for FLASH-write, never READ - so this is a
/// guess. If the ECU does not actually bank-switch on byte 4 the
/// resulting `.bin` will just be the same 64 KB four times over.
pub fn dump_rom_shinden_256k_experimental(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
    app: Option<&AppHandle>,
) -> Result<Vec<u8>, TransportError> {
    log.push(
        "[shinden] ROM Dump 256K (experimental) - sweeping 4 pages × 64 KB on byte[4]".into(),
    );
    let mut out: Vec<u8> = Vec::with_capacity(262_144);
    for page in 0u8..4u8 {
        let label = format!("ROM Dump 256K page {page:#04X}");
        let chunk = dump_rom_shinden_inner(
            t,
            log,
            app,
            /*page=*/ page,
            /*start_hi=*/ 0x00,
            /*total=*/ 65_536,
            &label,
        )?;
        if chunk.is_empty() {
            log.push(format!(
                "[shinden] page {page:#04X} returned no bytes - aborting sweep"
            ));
            break;
        }
        out.extend_from_slice(&chunk);
        if let Some(app) = app {
            kline_log::progress(app, "ROM Dump 256K", out.len() as u64, 262_144u64);
        }
    }
    log.push(format!(
        "[shinden] ROM Dump 256K (experimental) total: {} bytes",
        out.len()
    ));
    Ok(out)
}

fn dump_rom_shinden_inner(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
    app: Option<&AppHandle>,
    page: u8,
    start_hi: u8,
    total: usize,
    label: &str,
) -> Result<Vec<u8>, TransportError> {
    log.push(format!(
        "[shinden] {} starting (page=0x{:02X}, start_hi=0x{:02X})",
        label, page, start_hi
    ));
    // Standard Shinden init - same prologue as `read_eeprom_shinden`
    // so the ECU is in chunked-read state before we hammer it.
    try_send(t, &WAKEUP,        log, "sh-wakeup",    FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "sh-establish", FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,      log, "sh-hello",     FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU,    log, "sh-wareyou",   FRAME_TIMEOUT)?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_START_ADDR, log, "sh-startaddr", FRAME_TIMEOUT)?;

    let mut out: Vec<u8> = Vec::with_capacity(total);
    // Frame layout: [82 82 00 09 <page> <addr_lo> <addr_hi> 12 <chk>]
    // For 48K/64K dumps `page` is always 0x00; the 256K experiment
    // increments it to probe whether the ECU bank-switches on byte[4].
    let mut frame: [u8; 9] = [0x82, 0x82, 0x00, 0x09, page, 0x00, start_hi, 0x0C, 0x00];
    let mut offset: usize = 0;

    // Loop until we have the requested number of bytes.
    while out.len() < total {
        frame[5] = (offset & 0xFF) as u8;
        frame[6] = (start_hi.wrapping_add((offset / 256) as u8)) & 0xFF;
        frame[7] = 0x0C;
        let sum: u32 = frame[..8].iter().map(|b| *b as u32).sum();
        frame[8] = (256u32.wrapping_sub(sum & 0xFF) & 0xFF) as u8;

        let resp = try_send(t, &frame, log, "sh-rom", Duration::from_millis(300))?;

        // The original code expects a 26-byte response and keeps bytes 13..24
        // (12 data bytes per packet).
        if resp.len() <= 24 {
            log.push(format!("[shinden] short response at offset {offset} (len={}) - stopping", resp.len()));
            break;
        }
        let take_end = resp.len().min(25); // bytes 13..=24 inclusive
        out.extend_from_slice(&resp[13..take_end]);

        if let Some(app) = app {
            kline_log::progress(app, label, out.len() as u64, total as u64);
        }
        offset += 12;
        sleep_ms(15);
    }
    log.push(format!("[shinden] {} done: {} bytes", label, out.len()));
    Ok(out)
}

// ----------- helpers --------------------------------------------------

fn sleep_ms(ms: u64) {
    std::thread::sleep(Duration::from_millis(ms));
}
