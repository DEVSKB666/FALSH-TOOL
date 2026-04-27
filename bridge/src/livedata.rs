//! Real-time sensor poll for the Honda K-Line live-data protocol.
//!
//! Mirror of `app/src-tauri/src/livedata.rs` so the bridge daemon can
//! respond to `read_live_sample` requests with the same byte-perfect
//! behaviour as the local Tauri command.
//!
//! Honda KWP convention: every frame ends with a checksum byte chosen
//! so the **entire frame sums to 0 mod 256**.
//!
//! Init (per S.exe trace, same as the EEPROM tool):
//!     `FE 04 72 8C`            -> WAKEUP        (already 0 mod 256)
//!     `72 05 00 F0 99`         -> ESTABLISH     (5 + chk = 0 mod 256)
//!
//! Monitor cycle:
//!     `72 05 71 17 01`         -> request TABLE_17  (RPM, TPS, ECT, BAT, INJ, IGN)
//!     `72 05 71 20 F8`         -> request TABLE_20  (O2, AFR, STFT)

use crate::transport::{KLine, TransportError};
use std::time::Duration;

const FRAME_TIMEOUT: Duration = Duration::from_millis(180);

fn with_checksum<const N: usize>(payload: [u8; N]) -> [u8; N] {
    let mut buf = payload;
    let last = N - 1;
    let sum: u32 = buf[..last].iter().map(|b| *b as u32).sum();
    buf[last] = 0u8.wrapping_sub((sum & 0xFF) as u8);
    buf
}

const fn frame(prefix: [u8; 4]) -> [u8; 5] {
    [prefix[0], prefix[1], prefix[2], prefix[3], 0]
}

const TABLE_16_BODY: [u8; 4] = [0x72, 0x05, 0x71, 0x17];
const TABLE_20_BODY: [u8; 4] = [0x72, 0x05, 0x71, 0x20];

// Honda KWP wakeup + establish - byte-for-byte port of
// `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_31` (`array` + `array2`),
// the proven C# original that talks to real Honda Keihin ECUs. The
// matching 70 ms LOW pulse + 130 ms post-handover settle live in the
// transport's `open()` (see `libusb_ftdi.rs::open_inner`).
const HONDA_WAKEUP:    [u8; 4] = [0xFE, 0x04, 0x72, 0x8C];
const HONDA_ESTABLISH: [u8; 5] = [0x72, 0x05, 0x00, 0xF0, 0x99];

/// Try-send wrapper with verbose per-frame logging. The Honda K-Line
/// is a single-wire bus, so the FTDI reads back the bytes it just
/// transmitted *before* the ECU's reply - we strip that TX echo here
/// so callers see only the actual ECU response.
///
/// Logs both the raw RX (echo + reply) and the post-strip payload so
/// the operator can tell whether the ECU is silent (only echo bounces
/// back) or whether the reply layout differs from what we expected.
fn try_send(
    t: &mut dyn KLine,
    frame: &[u8],
    log: &mut Vec<String>,
    label: &str,
) -> Result<Vec<u8>, TransportError> {
    log.push(format!("[livedata] tx {label}: {}", hex(frame)));
    match t.round_trip(frame, FRAME_TIMEOUT) {
        Ok(r) => {
            log.push(format!("[livedata] rx {label} raw ({} B): {}", r.len(), hex(&r)));
            let stripped = if r.len() >= frame.len() && r[..frame.len()] == *frame {
                r[frame.len()..].to_vec()
            } else {
                // No echo prefix - either the FTDI swallowed it or the
                // bytes haven't drained yet. Pass the full buffer through
                // and let the caller / parser decide.
                r
            };
            log.push(format!(
                "[livedata] rx {label} payload ({} B): {}",
                stripped.len(),
                hex(&stripped)
            ));
            Ok(stripped)
        }
        Err(TransportError::Io(msg)) if msg.contains("recv") => {
            log.push(format!("[livedata] rx {label}: <silent>"));
            Ok(Vec::new())
        }
        Err(e) => Err(e),
    }
}

fn sleep_ms(ms: u64) {
    std::thread::sleep(Duration::from_millis(ms));
}

/// One full live-data poll: WAKEUP + ESTABLISH + TABLE_16 + TABLE_20.
/// Returns the raw (echo-stripped) ECU replies. Either may be empty
/// if the ECU did not answer that particular query.
///
/// Byte sequence is the proven C# original from
/// `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_31`. The 70 ms LOW
/// wakeup pulse + 130 ms post-handover settle (in the transport's
/// `open`) is the actual unblocker - earlier 25 ms pulses left Honda
/// ECUs asleep so every poll just bounced TX echoes around.
///
/// In production the daemon uses `establish` + `poll_tables` instead
/// (cheap steady-state polling on a persistent session), but
/// `poll_once` stays around as a self-contained reference path and
/// for ad-hoc CLI / test invocations.
#[allow(dead_code)]
pub fn poll_once(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    const POLL_PAUSE_MS: u64 = 30;

    let table_16 = with_checksum(frame(TABLE_16_BODY));
    let table_20 = with_checksum(frame(TABLE_20_BODY));

    log.push("[livedata] poll cycle begin".into());

    // Honda KWP wakeup + establish, then the two table requests. A
    // silent ECU just gives us back the TX echo; we keep going so the
    // operator sees every outgoing frame on the wire.
    let _ = try_send(t, &HONDA_WAKEUP,    log, "wakeup")?;    sleep_ms(POLL_PAUSE_MS);
    let _ = try_send(t, &HONDA_ESTABLISH, log, "establish")?; sleep_ms(POLL_PAUSE_MS);

    let resp16 = try_send(t, &table_16, log, "table16")?;
    sleep_ms(POLL_PAUSE_MS);
    let resp20 = try_send(t, &table_20, log, "table20")?;

    log.push(format!(
        "[livedata] poll cycle done - table16={} B, table20={} B",
        resp16.len(),
        resp20.len()
    ));

    Ok((resp16, resp20))
}

/// Steady-state poll: skip the WAKEUP/ESTABLISH handshake and just
/// query the two tables. Use this on every poll *after* the ECU has
/// been initialised - saves the 70 ms LOW + 130 ms settle that
/// `open()` already paid for, and which the ECU does not need on
/// every frame once it's in monitor mode.
///
/// Mirrors `app/src-tauri/src/livedata.rs::poll_tables` so the local
/// and bridge live-data feeds run at the same cadence.
pub fn poll_tables(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    const POLL_PAUSE_MS: u64 = 30;

    let table_16 = with_checksum(frame(TABLE_16_BODY));
    let table_20 = with_checksum(frame(TABLE_20_BODY));

    log.push("[livedata] poll(no-init) begin".into());
    let resp16 = try_send(t, &table_16, log, "table16")?;
    sleep_ms(POLL_PAUSE_MS);
    let resp20 = try_send(t, &table_20, log, "table20")?;

    Ok((resp16, resp20))
}

/// Honda KWP wakeup + establish handshake. Run once after `open()`
/// when the persistent session is first created, and again whenever
/// the steady-state poll comes back fully empty (which usually means
/// the ECU dropped us due to a missed timing).
pub fn establish(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(), TransportError> {
    const POLL_PAUSE_MS: u64 = 30;

    log.push("[livedata] establish - WAKEUP + ESTABLISH".into());
    let _ = try_send(t, &HONDA_WAKEUP, log, "wakeup")?;
    sleep_ms(POLL_PAUSE_MS);
    let _ = try_send(t, &HONDA_ESTABLISH, log, "establish")?;
    sleep_ms(POLL_PAUSE_MS);
    Ok(())
}

fn hex(bytes: &[u8]) -> String {
    bytes
        .iter()
        .map(|b| format!("{:02X}", b))
        .collect::<Vec<_>>()
        .join(" ")
}
