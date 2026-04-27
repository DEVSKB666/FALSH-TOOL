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

// ---- ROM dump (chunked Shinden read) --------------------------------

const ROM_FRAME_TIMEOUT: Duration = Duration::from_millis(300);

/// Dump the upper 48 KB of the Shinden ROM (`start_hi=0x40`, total
/// 49 152 bytes). Mirror of `app/src-tauri/src/eeprom.rs::dump_rom_shinden_48k`.
pub fn dump_rom_shinden_48k(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<Vec<u8>, TransportError> {
    dump_rom_shinden_inner(t, log, /*page=*/ 0x00, /*start_hi=*/ 0x40, /*total=*/ 49_152, "ROM Dump 48K")
}

/// Dump the upper 32 KB of the Shinden ROM (label "64K" in the
/// original UI; `start_hi=0x80`, total 32 768 bytes). Mirror of
/// `app/src-tauri/src/eeprom.rs::dump_rom_shinden_64k`.
pub fn dump_rom_shinden_64k(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<Vec<u8>, TransportError> {
    dump_rom_shinden_inner(t, log, /*page=*/ 0x00, /*start_hi=*/ 0x80, /*total=*/ 32_768, "ROM Dump 64K")
}

/// **Experimental** ≤128 KB dump - byte 4 of the read frame (which
/// the Shinden 48K/64K paths leave at `0x00`) is repurposed as a page
/// selector and we sweep pages `0x00..0x03`. Each page reads the
/// proven `0x8000..0xFFFF` upper-half range (32 KB) so the first page
/// is effectively the existing `dump_rom_shinden_64k`.
///
/// This is a guess - the original C# tool only references 256K+ ROM
/// sizes for FLASH-write, never READ. Outcomes:
///
/// - **Pages 0/1/2/3 differ** → ECU actually bank-switches on
///   byte[4] and you have a real 128 KB dump
/// - **All four pages identical** → byte[4] is ignored and the ECU
///   handed you the same 32 KB four times in a row (probe data)
/// - **Page 0 returns 0 bytes** → the ECU did not ACK at all; the
///   chunked-read protocol does not apply to this device (different
///   family - probably Keihin / SH7058 - which needs a separate
///   dump routine we haven't implemented)
pub fn dump_rom_shinden_256k_experimental(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<Vec<u8>, TransportError> {
    log.push(
        "[shinden] ROM Dump 256K (experimental) - sweeping 4 pages × 32 KB on byte[4]".into(),
    );
    let mut out: Vec<u8> = Vec::with_capacity(131_072);
    for page in 0u8..4u8 {
        let label = format!("ROM Dump 256K page {page:#04X}");
        let chunk = dump_rom_shinden_inner(
            t,
            log,
            /*page=*/ page,
            /*start_hi=*/ 0x80,
            /*total=*/ 32_768,
            &label,
        )?;
        if chunk.is_empty() {
            log.push(format!(
                "[shinden] page {page:#04X} returned no bytes - aborting sweep"
            ));
            break;
        }
        out.extend_from_slice(&chunk);
    }
    log.push(format!("[shinden] ROM Dump 256K (experimental) total: {} bytes", out.len()));
    Ok(out)
}

fn dump_rom_shinden_inner(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
    page: u8,
    start_hi: u8,
    total: usize,
    label: &str,
) -> Result<Vec<u8>, TransportError> {
    log.push(format!(
        "[shinden] {} starting (page=0x{:02X}, start_hi=0x{:02X})",
        label, page, start_hi
    ));

    // Standard Shinden init - same prologue as `read_eeprom_shinden`.
    try_send(t, &WAKEUP,        log, "sh-wakeup")?;    sleep_ms(STEP_PAUSE_MS);
    try_send(t, &ESTABLISH,     log, "sh-establish")?; sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_HELLO,      log, "sh-hello")?;     sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_WAREYOU,    log, "sh-wareyou")?;   sleep_ms(STEP_PAUSE_MS);
    try_send(t, &SH_START_ADDR, log, "sh-startaddr")?;

    let mut out: Vec<u8> = Vec::with_capacity(total);
    // Frame layout: [82 82 00 09 <page> <addr_lo> <addr_hi> 12 <chk>]
    // For 48K/64K dumps `page` is always 0x00; the 256K experiment
    // increments it to probe whether the ECU bank-switches.
    let mut frame: [u8; 9] = [0x82, 0x82, 0x00, 0x09, page, 0x00, start_hi, 0x0C, 0x00];
    let mut offset: usize = 0;

    while out.len() < total {
        frame[5] = (offset & 0xFF) as u8;
        frame[6] = start_hi.wrapping_add((offset / 256) as u8) & 0xFF;
        frame[7] = 0x0C;
        let sum: u32 = frame[..8].iter().map(|b| *b as u32).sum();
        frame[8] = (256u32.wrapping_sub(sum & 0xFF) & 0xFF) as u8;

        let resp = match t.round_trip(&frame, ROM_FRAME_TIMEOUT) {
            Ok(r) => r,
            Err(TransportError::Io(msg)) if msg.contains("recv") => {
                log.push(format!("[shinden] silent at offset {offset} ({msg}) - stopping"));
                break;
            }
            Err(e) => return Err(e),
        };

        // The C# original keeps bytes 13..=24 (12 data bytes per packet)
        // after the 13-byte header that includes the echo + ACK.
        if resp.len() <= 24 {
            log.push(format!(
                "[shinden] short response at offset {offset} (len={}) - stopping",
                resp.len()
            ));
            break;
        }
        let take_end = resp.len().min(25);
        out.extend_from_slice(&resp[13..take_end]);

        offset += 12;
        if offset % 1_536 == 0 {
            log.push(format!(
                "[shinden] {} progress: {}/{} bytes",
                label,
                out.len(),
                total
            ));
        }
        sleep_ms(15);
    }

    log.push(format!("[shinden] {} done: {} bytes", label, out.len()));
    Ok(out)
}
