//! Real-time sensor poll for the TyN Shop K-Line live-data protocol.
//!
//! Frame definitions are taken directly from the user-supplied
//! `TynShop.adx` (a TunerPro Acquisition Definition file). Each command
//! is `[..., checksum]` where `checksum = sum(bytes) mod 256` (the ADX
//! `SNDCMDCHECKSUM type="2"` convention).
//!
//! Connect (one-shot):
//!     `2C 41 4E 23 DE`         -> K_CONNECT
//!
//! Monitor cycle (poll at desired Hz):
//!     `72 05 71 17 FF`         -> request TABLE_16  (RPM, TPS, ECT, BAT, INJ, IGN)
//!     wait, read 29 byte reply
//!     `72 05 71 20 08`         -> request TABLE_20  (O2, AFR, STFT)
//!     wait, read 13 byte reply
//!
//! Disconnect:
//!     `2C 64 73 23 26`         -> K_DISCONNECT
//!
//! The two replies are concatenated (29 + 13 = 42 bytes) and returned
//! to the frontend, which knows the field offsets and conversion math.

use crate::transport::{KLine, TransportError};
use std::time::Duration;

const FRAME_TIMEOUT: Duration = Duration::from_millis(180);

/// Append a "sum mod 256" checksum byte to the supplied buffer.
fn with_checksum<const N: usize>(payload: [u8; N]) -> [u8; N] {
    // Caller must size the input as `original_len + 1`; final byte is
    // overwritten with the computed checksum.
    let mut buf = payload;
    let last = N - 1;
    let sum: u32 = buf[..last].iter().map(|b| *b as u32).sum();
    buf[last] = (sum & 0xFF) as u8;
    buf
}

/// Frames defined by `TynShop.adx`. Each is the raw 4-byte body with
/// a placeholder zero in slot 4 that the checksum routine fills in.
const fn frame(prefix: [u8; 4]) -> [u8; 5] {
    [prefix[0], prefix[1], prefix[2], prefix[3], 0]
}

const K_CONNECT_BODY:    [u8; 4] = [0x2C, 0x41, 0x4E, 0x23];
const K_DISCONNECT_BODY: [u8; 4] = [0x2C, 0x64, 0x73, 0x23];
const TABLE_16_BODY:     [u8; 4] = [0x72, 0x05, 0x71, 0x17];
const TABLE_20_BODY:     [u8; 4] = [0x72, 0x05, 0x71, 0x20];

/// Try-send wrapper that tolerates a recv timeout (silent ECU) by
/// returning an empty Vec instead of propagating the error. Real
/// device / USB faults still propagate.
fn try_send(t: &mut dyn KLine, frame: &[u8]) -> Result<Vec<u8>, TransportError> {
    match t.round_trip(frame, FRAME_TIMEOUT) {
        Ok(r) => Ok(r),
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
    let connect    = with_checksum(frame(K_CONNECT_BODY));
    let table_16   = with_checksum(frame(TABLE_16_BODY));
    let table_20   = with_checksum(frame(TABLE_20_BODY));

    log.push(format!("[livedata] poll - {} {} {}",
        hex(&connect), hex(&table_16), hex(&table_20)));

    let _ = try_send(t, &connect)?; // connect is best-effort
    sleep_ms(30);

    let resp16 = try_send(t, &table_16)?;
    sleep_ms(30);

    let resp20 = try_send(t, &table_20)?;

    Ok((resp16, resp20))
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

    /// Hand-computed checksums from TynShop.adx.
    #[test]
    fn checksum_matches_adx() {
        let connect = with_checksum(frame(K_CONNECT_BODY));
        assert_eq!(connect, [0x2C, 0x41, 0x4E, 0x23, 0xDE]);

        let t16 = with_checksum(frame(TABLE_16_BODY));
        assert_eq!(t16, [0x72, 0x05, 0x71, 0x17, 0xFF]);

        let t20 = with_checksum(frame(TABLE_20_BODY));
        assert_eq!(t20, [0x72, 0x05, 0x71, 0x20, 0x08]);

        let dis = with_checksum(frame(K_DISCONNECT_BODY));
        assert_eq!(dis, [0x2C, 0x64, 0x73, 0x23, 0x26]);
    }
}
