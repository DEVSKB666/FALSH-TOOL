//! Real-time sensor poll for the Honda K-Line live-data protocol used
//! by `S.exe` (the live-data tool that the original MZA Tuner Flash
//! ships as an embedded resource and spawns from the "ดูดาต้ารถ" menu).
//!
//! Honda KWP convention: every frame ends with a checksum byte chosen
//! so the **entire frame sums to 0 mod 256**. Equivalently:
//!     `checksum = (256 - sum_of_payload) & 0xFF`
//! This is the `SUM=0` rule, not the `SUM=checksum` rule that some
//! TunerPro ADX files use - getting this wrong makes the ECU silently
//! drop every frame (only the K-Line bus echo bounces back, no reply).
//!
//! Init (per S.exe trace, same as the EEPROM tool):
//!     `FE 04 72 8C`            -> WAKEUP        (already 0 mod 256)
//!     `72 05 00 F0 99`         -> ESTABLISH     (5 + chk = 0 mod 256)
//!
//! Monitor cycle (poll at desired Hz):
//!     `72 05 71 17 01`         -> request TABLE_17  (RPM, TPS, ECT, BAT, INJ, IGN)
//!     wait, read reply
//!     `72 05 71 20 F8`         -> request TABLE_20  (O2, AFR, STFT)
//!     wait, read reply
//!
//! The replies are returned to the frontend, which knows the field
//! offsets and conversion math (originally taken from `TynShop.adx`).

use crate::transport::{KLine, TransportError};
use std::time::Duration;

const FRAME_TIMEOUT: Duration = Duration::from_millis(180);

/// Append the Honda KWP checksum byte to the supplied buffer so that
/// the *entire* frame (payload + checksum) sums to 0 mod 256.
fn with_checksum<const N: usize>(payload: [u8; N]) -> [u8; N] {
    // Caller must size the input as `original_len + 1`; final byte is
    // overwritten with the computed checksum.
    let mut buf = payload;
    let last = N - 1;
    let sum: u32 = buf[..last].iter().map(|b| *b as u32).sum();
    buf[last] = 0u8.wrapping_sub((sum & 0xFF) as u8);
    buf
}

/// Frames defined by `TynShop.adx`. Each is the raw 4-byte body with
/// a placeholder zero in slot 4 that the checksum routine fills in.
const fn frame(prefix: [u8; 4]) -> [u8; 5] {
    [prefix[0], prefix[1], prefix[2], prefix[3], 0]
}

// TyN-Shop legacy commands - kept only so the `disconnect()` helper can
// still emit a graceful close on ECUs that recognise it. Stock Honda
// ECUs ignore these (they're not part of the S.exe protocol).
const K_DISCONNECT_BODY: [u8; 4] = [0x2C, 0x64, 0x73, 0x23];

// Live-data table requests - byte 4 is the table id. S.exe queries 0x16,
// 0x17, 0x11, 0x13, 0x20 etc.; we currently use 0x17 + 0x20 because the
// frontend parser was authored against TynShop.adx for those two.
const TABLE_16_BODY:     [u8; 4] = [0x72, 0x05, 0x71, 0x17];
const TABLE_20_BODY:     [u8; 4] = [0x72, 0x05, 0x71, 0x20];

/// Honda KWP "wakeup" + "establish communication" frames - same as the
/// EEPROM init in `eeprom.rs` and the very first frames S.exe sends.
/// Without this handshake the K-Line stays silent and the FTDI only
/// sees its own TX echo bouncing back on the single-wire bus.
const HONDA_WAKEUP:    [u8; 4] = [0xFE, 0x04, 0x72, 0x8C];
const HONDA_ESTABLISH: [u8; 5] = [0x72, 0x05, 0x00, 0xF0, 0x99];

/// Try-send wrapper that tolerates a recv timeout (silent ECU) by
/// returning an empty Vec instead of propagating the error. Real
/// device / USB faults still propagate.
///
/// The Honda K-Line is a single-wire bus, so the FTDI reads back the
/// bytes it just transmitted *before* the ECU's reply. We strip that
/// TX echo here so callers (and the frontend parser) see only the
/// actual ECU response, which matches the byte layout described in
/// `TynShop.adx`.
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

/// One full live-data poll.
///
/// Returns `(table16_response, table20_response)`. Either may be empty
/// if the ECU did not answer that particular query - the frontend
/// handles missing data gracefully and just keeps the previous values.
///
/// The caller is expected to have already opened the transport with
/// the standard FTDI / libusb backend (10400 baud K-Line). The first
/// call also runs `K_CONNECT`; subsequent rapid calls can skip it,
/// but doing it once per poll is cheap (5 bytes) and recovers the
/// session if the ECU dropped us.
pub fn poll_once(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    let table_16   = with_checksum(frame(TABLE_16_BODY));
    let table_20   = with_checksum(frame(TABLE_20_BODY));

    log.push(format!("[livedata] poll - {} {}",
        hex(&table_16), hex(&table_20)));

    // Standard Honda KWP wakeup/establish (same frames the EEPROM tool
    // and S.exe use). Both are best-effort: a silent ECU just produces
    // an empty reply and we keep going so the operator sees every
    // outgoing frame on the wire even when the bus is dead.
    let _ = try_send(t, &HONDA_WAKEUP)?;
    sleep_ms(30);
    let _ = try_send(t, &HONDA_ESTABLISH)?;
    sleep_ms(30);

    let resp16 = try_send(t, &table_16)?;
    sleep_ms(30);

    let resp20 = try_send(t, &table_20)?;

    Ok((resp16, resp20))
}

/// Like `poll_once` but skips the WAKEUP/ESTABLISH handshake. Use this
/// for steady-state polling once a session is already established (the
/// ECU stays "awake" for ~2 s after the last frame). Saves ~400 ms per
/// poll vs. `poll_once` which is the difference between a stutter-free
/// 2-3 Hz feed and a sluggish 1 Hz one.
pub fn poll_tables(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(Vec<u8>, Vec<u8>), TransportError> {
    let table_16 = with_checksum(frame(TABLE_16_BODY));
    let table_20 = with_checksum(frame(TABLE_20_BODY));

    log.push(format!("[livedata] poll(no-init) - {} {}",
        hex(&table_16), hex(&table_20)));

    let resp16 = try_send(t, &table_16)?;
    sleep_ms(30);

    let resp20 = try_send(t, &table_20)?;

    Ok((resp16, resp20))
}

/// Run just the Honda KWP wakeup+establish handshake. Called once when
/// the live-data session is opened (and again any time a poll comes
/// back fully empty, suggesting the ECU's session timed out).
pub fn establish(
    t: &mut dyn KLine,
    log: &mut Vec<String>,
) -> Result<(), TransportError> {
    log.push("[livedata] establish - WAKEUP + ESTABLISH".to_string());
    let _ = try_send(t, &HONDA_WAKEUP)?;
    sleep_ms(30);
    let _ = try_send(t, &HONDA_ESTABLISH)?;
    sleep_ms(30);
    Ok(())
}

/// Send the final `K_DISCONNECT` so the ECU drops out of monitor
/// mode cleanly. Errors are non-fatal - we always close the port
/// after this regardless.
pub fn disconnect(t: &mut dyn KLine) -> Result<(), TransportError> {
    let frame = with_checksum(frame(K_DISCONNECT_BODY));
    let _ = t.round_trip(&frame, FRAME_TIMEOUT);
    Ok(())
}

fn hex(bytes: &[u8]) -> String {
    bytes.iter().map(|b| format!("{:02X}", b)).collect::<Vec<_>>().join(" ")
}

#[cfg(test)]
mod tests {
    use super::*;

    /// Hand-computed Honda-KWP checksums (whole frame sums to 0 mod 256).
    /// Cross-checked against the byte tables found inside S.exe.
    #[test]
    fn checksum_matches_honda_kwp() {
        let t16 = with_checksum(frame(TABLE_16_BODY));
        assert_eq!(t16, [0x72, 0x05, 0x71, 0x17, 0x01]);
        assert_eq!(t16.iter().map(|b| *b as u32).sum::<u32>() % 256, 0);

        let t20 = with_checksum(frame(TABLE_20_BODY));
        assert_eq!(t20, [0x72, 0x05, 0x71, 0x20, 0xF8]);
        assert_eq!(t20.iter().map(|b| *b as u32).sum::<u32>() % 256, 0);

        let dis = with_checksum(frame(K_DISCONNECT_BODY));
        assert_eq!(dis, [0x2C, 0x64, 0x73, 0x23, 0xDA]);
        assert_eq!(dis.iter().map(|b| *b as u32).sum::<u32>() % 256, 0);
    }
}
