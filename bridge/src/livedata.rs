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

const HONDA_WAKEUP:    [u8; 4]  = [0xFE, 0x04, 0x72, 0x8C];
const HONDA_ESTABLISH: [u8; 5]  = [0x72, 0x05, 0x00, 0xF0, 0x99];

// Full Keihin "start diagnostic session" handshake - same bytes the
// EEPROM tool uses successfully. Adding these to the live-data init
// is needed by ECUs that drop the KWP session after ESTABLISH unless
// they see a proper StartDiagnosticSession + SecurityAccess frame.
const KH_START_DIAG: [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x9E, 0x8D, 0x9A, 0x86, 0x90, 0x8A, 0x8C, 0x9B, 0x88];
const KH_FOLLOWUP:   [u8; 13] = [0x91, 0x91, 0x0D, 0xDF, 0x92, 0x9E, 0x86, 0x96, 0x8B, 0x8D, 0x86, 0xC0, 0x6A];

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
/// Returns the raw (echo-stripped) ECU replies. Either may be empty if
/// the ECU did not answer that particular query.
///
/// Inter-frame timing matches `eeprom.rs` (`STEP_PAUSE_MS = 150`) -
/// shorter pauses occasionally cost a reply on Windows where USB
/// scheduling jitter can compress two adjacent frames into the same
/// 8 ms FTDI latency window, confusing the ECU's KWP state machine.
pub fn poll_once(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    const PAUSE_MS: u64 = 150;

    let table_16 = with_checksum(frame(TABLE_16_BODY));
    let table_20 = with_checksum(frame(TABLE_20_BODY));

    log.push("[livedata] poll cycle begin".into());

    // Full Keihin diagnostic handshake - same sequence the proven
    // EEPROM read uses. Empty payloads are best-effort: a silent ECU
    // just gives us nothing back and we keep going so the operator
    // sees every outgoing frame on the wire even when the bus is dead.
    let _ = try_send(t, &HONDA_WAKEUP,    log, "wakeup")?;    sleep_ms(PAUSE_MS);
    let _ = try_send(t, &HONDA_ESTABLISH, log, "establish")?; sleep_ms(PAUSE_MS);
    let _ = try_send(t, &KH_START_DIAG,   log, "kh-startdiag")?; sleep_ms(PAUSE_MS);
    let _ = try_send(t, &KH_FOLLOWUP,     log, "kh-followup")?;  sleep_ms(PAUSE_MS);

    let resp16 = try_send(t, &table_16, log, "table16")?;
    sleep_ms(PAUSE_MS);
    let resp20 = try_send(t, &table_20, log, "table20")?;

    log.push(format!(
        "[livedata] poll cycle done - table16={} B, table20={} B",
        resp16.len(),
        resp20.len()
    ));

    Ok((resp16, resp20))
}

fn hex(bytes: &[u8]) -> String {
    bytes
        .iter()
        .map(|b| format!("{:02X}", b))
        .collect::<Vec<_>>()
        .join(" ")
}
