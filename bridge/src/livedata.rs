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

const HONDA_WAKEUP:    [u8; 4] = [0xFE, 0x04, 0x72, 0x8C];
const HONDA_ESTABLISH: [u8; 5] = [0x72, 0x05, 0x00, 0xF0, 0x99];

/// Try-send wrapper that tolerates a recv timeout (silent ECU) by
/// returning an empty Vec instead of propagating the error. The Honda
/// K-Line is a single-wire bus, so the FTDI reads back the bytes it
/// just transmitted *before* the ECU's reply - we strip that TX echo
/// here so callers see only the actual ECU response.
fn try_send(t: &mut dyn KLine, frame: &[u8]) -> Result<Vec<u8>, TransportError> {
    match t.round_trip(frame, FRAME_TIMEOUT) {
        Ok(mut r) => {
            if r.len() >= frame.len() && r[..frame.len()] == *frame {
                r.drain(..frame.len());
            }
            Ok(r)
        }
        Err(TransportError::Io(msg)) if msg.contains("recv") => Ok(Vec::new()),
        Err(e) => Err(e),
    }
}

fn sleep_ms(ms: u64) {
    std::thread::sleep(Duration::from_millis(ms));
}

/// One full live-data poll: WAKEUP + ESTABLISH + TABLE_16 + TABLE_20.
/// Returns the raw (echo-stripped) ECU replies. Either may be empty if
/// the ECU did not answer that particular query.
pub fn poll_once(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    let table_16 = with_checksum(frame(TABLE_16_BODY));
    let table_20 = with_checksum(frame(TABLE_20_BODY));

    log.push(format!(
        "[livedata] poll - WAKEUP/ESTABLISH then {} {}",
        hex(&table_16),
        hex(&table_20)
    ));

    // Honda KWP wakeup/establish (best-effort).
    let _ = try_send(t, &HONDA_WAKEUP)?;
    sleep_ms(30);
    let _ = try_send(t, &HONDA_ESTABLISH)?;
    sleep_ms(30);

    let resp16 = try_send(t, &table_16)?;
    sleep_ms(30);
    let resp20 = try_send(t, &table_20)?;

    Ok((resp16, resp20))
}

fn hex(bytes: &[u8]) -> String {
    bytes
        .iter()
        .map(|b| format!("{:02X}", b))
        .collect::<Vec<_>>()
        .join(" ")
}
