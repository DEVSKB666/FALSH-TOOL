//! Honda DTC (Diagnostic Trouble Codes) over K-Line.
//!
//! ## Status: skeleton / educated guess
//!
//! `MZA_TUNER_FLASH_2026.exe` itself **does not** implement DTC, but the
//! companion Flutter app `MultiGaugesHondaMonitor.exe` (under
//! `C:\MZATUNER\data\By.<author>\`) does. We could see the symbol
//! names (`_clearDtc`, `_startDtcLoop`, `parseDtc`, `_DtcPageState`,
//! `package:pc_app/pages/dtc_page.dart`) and the polling cadence
//! (`[DTC] Start DTC polling (0.5s)`) but the AOT-compiled Dart
//! snapshot (`app.so`) does not store the K-Line frames as recoverable
//! byte arrays, so the values below are **educated guesses** based on:
//!
//!   * the Honda Keihin / Shinden init we already extracted from the
//!     EEPROM-read flow (see [`super::kline`]),
//!   * the standard ISO 14230 / KWP2000 service IDs (`0x18` -
//!     `readDTCByStatus`, `0x14` - `clearDiagnosticInformation`),
//!   * common Honda motorcycle MIL blink-code reference tables.
//!
//! Once we capture real K-Line traffic from a live ECU (either via the
//! bridge daemon's streaming log or a USB sniffer) we'll replace the
//! placeholder frames here. The high-level shape (`Dtc` struct,
//! `lookup_label`, `parse_response`) is API-stable - frame bytes are
//! not.

#![allow(missing_docs)]

use serde::{Deserialize, Serialize};

// ---- Frame templates -------------------------------------------------

/// Read-DTC frames per ECU family. The trailing byte of each frame is
/// the simple 8-bit-sum checksum the rest of the Honda K-Line stack
/// uses; if that turns out to be wrong on a real ECU, the comment by
/// each constant explains how to recompute it.
pub mod frames {
    /// `[91 91 06 D0 00 ck]` - "read DTC" placeholder. The checksum
    /// `ck` is `0x00` here because Keihin frames in the EEPROM read
    /// loop also send `0x00` and let the ECU echo. If the ECU rejects
    /// this, try `ck = 0x91 + 0x91 + 0x06 + 0xD0 = 0xF8`.
    pub const KEIHIN_READ_DTC: [u8; 6] = [0x91, 0x91, 0x06, 0xD0, 0x00, 0x00];

    /// `[91 91 05 D2 00]` - clear DTC placeholder. Same checksum
    /// caveat as `KEIHIN_READ_DTC`.
    pub const KEIHIN_CLEAR_DTC: [u8; 5] = [0x91, 0x91, 0x05, 0xD2, 0x00];

    /// `[82 82 10 18 02 ck]` - Shinden read DTC. `0x10` is the read
    /// service group ID also used by the EEPROM cell-read frame
    /// (`82 82 10 06 ...`), and `0x18` is the KWP2000 readDTCByStatus
    /// service. Checksum byte = `0xE6 - 0x18 = 0xCE`.
    pub const SHINDEN_READ_DTC: [u8; 6] = [0x82, 0x82, 0x10, 0x18, 0x02, 0xCE];

    /// `[82 82 10 14 FF ck]` - Shinden clear DTC, KWP2000 service
    /// `0x14` (clearDiagnosticInformation). Checksum = `0xE6 - 0x14 = 0xD2`,
    /// but we sum literally to keep things explicit.
    pub const SHINDEN_CLEAR_DTC: [u8; 6] = [0x82, 0x82, 0x10, 0x14, 0xFF, 0xD2];
}

// ---- Honda motorcycle DTC table -------------------------------------

/// One DTC entry as surfaced to the GUI / bridge response.
#[derive(Debug, Clone, Serialize, Deserialize, PartialEq, Eq)]
pub struct Dtc {
    /// Numeric Honda code (e.g. `7` -> "MIL-7"). For codes the table
    /// doesn't recognise this is still set to the raw byte.
    pub code: u8,
    /// Pretty-printed code label - "MIL-7" / "MIL-23" etc.
    pub label: String,
    /// Human-readable diagnostic note in Thai/English. `"Unknown"` if
    /// the code isn't in our lookup.
    pub description: String,
    /// `true` once the code is *active* (still flagging on the ECU)
    /// vs. *historical* (stored but not currently set). The K-Line
    /// response carries this distinction in a status byte we don't
    /// yet decode - placeholder = `true` until we see real traffic.
    pub active: bool,
}

impl Dtc {
    /// Convenience constructor used by the parser.
    pub fn from_byte(code: u8, active: bool) -> Self {
        Self {
            code,
            label: format!("MIL-{code}"),
            description: lookup_label(code).unwrap_or("Unknown DTC").to_string(),
            active,
        }
    }
}

/// Lookup table for the most common Honda motorcycle K-Line DTCs. The
/// numbers come from the standard MIL blink-code chart shared across
/// Click / Wave / PCX / ADV service manuals; we surface them as
/// `MIL-NN` since that's what mechanics already recognise. Returns
/// `None` if the byte doesn't map to a known fault.
pub fn lookup_label(code: u8) -> Option<&'static str> {
    Some(match code {
        // ---- Sensors --------------------------------------------------
        1  => "MAP sensor circuit",
        7  => "ECT (Engine Coolant Temp) sensor circuit",
        8  => "TP (Throttle Position) sensor circuit",
        9  => "IAT (Intake Air Temp) sensor circuit",
        11 => "VS (Vehicle Speed) sensor circuit",
        12 => "Injector circuit",
        13 => "O2 sensor circuit",
        14 => "IACV (Idle Air Control Valve)",
        15 => "SVCS (Honda variable cylinder system)",
        18 => "CMP (Cam Position) sensor",
        19 => "CKP (Crank Position) sensor",
        21 => "O2 sensor heater",
        23 => "O2 sensor heater (secondary)",
        25 => "Knock sensor",
        29 => "APS (Accelerator Pedal Sensor)",
        33 => "ECM internal fault",
        34 => "EEPROM (CKP signal absent)",
        35 => "EGCV (Exhaust Gas Control Valve)",
        38 => "PAIR (Pulse secondary AIR injection)",
        41 => "Lean angle sensor",
        54 => "Bank angle sensor",
        56 => "Knock sensor (secondary)",
        67 => "FI ECU - reprogram requested",
        86 => "Serial communication line",
        91 => "PCV (compression / blowby)",
        _  => return None,
    })
}

// ---- Response parsing -----------------------------------------------

/// Parse a raw K-Line read-DTC response into a list of `Dtc` entries.
///
/// **Placeholder layout** (revisit once we have a real capture):
///   * byte 0..=2 : echo of the request header (`91 91 06` or `82 82 10`)
///   * byte 3..=4 : status / count
///   * byte 5..   : pairs of `(code_byte, status_byte)` until checksum
///
/// We tolerate short responses (silent ECU returns an empty vec) by
/// returning `Vec::new()` rather than erroring - matches the rest of
/// the K-Line stack's "no reply = no DTC".
pub fn parse_response(response: &[u8]) -> Vec<Dtc> {
    if response.len() < 6 {
        return Vec::new();
    }
    let mut out = Vec::new();
    // Skip the 5-byte header, stop one byte short to leave room for
    // the trailing checksum.
    let mut i = 5;
    while i + 1 < response.len() {
        let code = response[i];
        let status = response[i + 1];
        if code == 0x00 && status == 0x00 {
            // Honda often pads "no fault" slots with zeros - we treat
            // a zero pair as the end-of-list marker.
            break;
        }
        let active = (status & 0x01) != 0;
        out.push(Dtc::from_byte(code, active));
        i += 2;
    }
    out
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn lookup_known_code() {
        assert_eq!(lookup_label(7), Some("ECT (Engine Coolant Temp) sensor circuit"));
        assert_eq!(lookup_label(12), Some("Injector circuit"));
        assert_eq!(lookup_label(99), None);
    }

    #[test]
    fn dtc_from_byte_unknown_code() {
        let d = Dtc::from_byte(99, true);
        assert_eq!(d.label, "MIL-99");
        assert_eq!(d.description, "Unknown DTC");
        assert!(d.active);
    }

    #[test]
    fn parse_response_empty_short_input() {
        assert!(parse_response(&[0x91, 0x91]).is_empty());
        assert!(parse_response(&[]).is_empty());
    }

    #[test]
    fn parse_response_skeleton_layout() {
        // Header (5 bytes) + 2 codes + zero pad
        let resp = [
            0x91, 0x91, 0x06, 0xD0, 0x02,   // header
            0x07, 0x01,                      // ECT, active
            0x12, 0x00,                      // ID 18 (CMP), historical
            0x00, 0x00,                      // pad
            0xCC,                            // ck
        ];
        let dtcs = parse_response(&resp);
        assert_eq!(dtcs.len(), 2);
        assert_eq!(dtcs[0].code, 7);
        assert!(dtcs[0].active);
        assert_eq!(dtcs[1].code, 18);
        assert!(!dtcs[1].active);
    }
}
