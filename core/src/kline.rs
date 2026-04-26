//! Honda K-Line / KWP2000 (ISO 14230) protocol constants and frame builders.
//!
//! All byte sequences here are taken **directly from the deobfuscated
//! source** (`Form_EepromTool.cs` and the splash screen in `MainForm`
//! `⁚.2.cs`). No active code yet - this module just documents the wire
//! format so the FTDI layer can be wired up cleanly.
//!
//! ## FTDI baud sequence (from `MainForm`)
//!
//! 1. `FT_Open(0)`
//! 2. `FT_Purge(TX|RX)`
//! 3. `FT_SetBitMode(0, 0)` (reset)
//! 4. `FT_SetDataCharacteristics(8N1)`
//! 5. `FT_SetBaudRate(921_600)` (high-speed for bit-bang wakeup)
//! 6. `FT_SetTimeouts(50, 0)`
//! 7. `FT_SetLatencyTimer(8)`
//! 8. `FT_SetBitMode(1, 1)` (bit-bang)
//! 9. write a "wakeup byte" via bit-bang at 5 baud
//! 10. `FT_SetBitMode(0, 0)` (back to UART)
//! 11. `FT_SetBaudRate(10_400)` (Honda K-Line standard)
//! 12. `FT_Purge(TX|RX)`

#![allow(missing_docs)]

/// FTDI baud rate used for the bit-bang wakeup phase.
pub const BAUD_BIT_BANG: u32 = 921_600;
/// Honda K-Line communication baud rate (ISO 14230 / KWP2000).
pub const BAUD_KLINE: u32 = 10_400;
/// FTDI latency timer used by the original (ms).
pub const FTDI_LATENCY_TIMER_MS: u8 = 8;
/// FTDI read timeout (ms).
pub const FTDI_READ_TIMEOUT_MS: u32 = 50;

/// Common init bytes that both Keihin and Shinden flows send.
pub mod init {
    /// Wakeup / address byte sequence.
    pub const WAKEUP: [u8; 4] = [0xFE, 0x04, 0x72, 0x8C];
    /// Start communication / link establishment.
    pub const ESTABLISHMENT: [u8; 5] = [0x72, 0x05, 0x00, 0xF0, 0x99];
}

/// Keihin (Kh) ECU protocol.
pub mod keihin {
    /// Start-diagnostic frame #1 (signature `91 91 0D DF ...`).
    pub const START_DIAG_1: [u8; 13] = [
        0x91, 0x91, 0x0D, 0xDF, 0x9E, 0x8D, 0x9A, 0x86, 0x90, 0x8A, 0x8C, 0x9B, 0x88,
    ];
    /// Start-diagnostic frame #2.
    pub const START_DIAG_2: [u8; 13] = [
        0x91, 0x91, 0x0D, 0xDF, 0x92, 0x9E, 0x86, 0x96, 0x8B, 0x8D, 0x86, 0xC0, 0x6A,
    ];
    /// Read-EEPROM-byte frame template. The original program sends the same
    /// 7-byte frame in a loop and reads the response: bytes 11 and 12 of the
    /// answer hold the data byte (so you walk the address by changing what
    /// the ECU returns, not the request - hence the constant frame).
    pub const READ_EEPROM_FRAME: [u8; 7] = [0x91, 0x91, 0x07, 0x40, 0x00, 0x00, 0x00];
}

/// Shinden (Sh) ECU protocol.
pub mod shinden {
    /// Security-Access "HelloHoC" challenge - service id 0x27 = SecurityAccess
    /// in KWP2000.
    pub const AUTH_HELLO_HOC: [u8; 11] = [
        0x27, 0x0B, 0xE0, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x48, 0x6F, 0x43,
    ];
    /// Security-Access "wAreYou\"" challenge.
    pub const AUTH_WAREYOU: [u8; 11] = [
        0x27, 0x0B, 0xE0, 0x77, 0x41, 0x72, 0x65, 0x59, 0x6F, 0x75, 0x22,
    ];
    /// Seed request.
    pub const SEED_GET: [u8; 6] = [0x7E, 0x06, 0x01, 0x01, 0x00, 0x7A];
    /// Read-memory-by-address base frame; the loop swaps the last two bytes
    /// to address each EEPROM cell.
    pub const READ_BASE: [u8; 6] = [0x82, 0x82, 0x10, 0x06, 0x00, 0xE6];

    /// Build the 6-byte read-byte frame for offset `i` (0..=255).
    ///
    /// Matches `array8[4] = (byte)i; array8[5] = (byte)(0xE6 - i);` from
    /// `Form_EepromTool.cs`.
    pub const fn read_eeprom_frame(offset: u8) -> [u8; 6] {
        [0x82, 0x82, 0x10, 0x06, offset, 0xE6u8.wrapping_sub(offset)]
    }
}

/// Available high-level operations exposed by the EEPROM tool form.
#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum EepromOp {
    ReadKeihin,
    ReadShinden,
    ResetFlashCountKeihin,
    ResetFlashCountShinden,
    FormatZero,
    FormatFf,
    Dump48k,
    Dump64k,
}

impl EepromOp {
    /// Human-readable Thai label used in the original ComboBox.
    pub fn label_th(self) -> &'static str {
        match self {
            EepromOp::ReadKeihin => "READ EEPROM KH",
            EepromOp::ReadShinden => "READ EEPROM Sh",
            EepromOp::ResetFlashCountKeihin => "ลบจำนวนการอัด Kh",
            EepromOp::ResetFlashCountShinden => "ลบจำนวนการอัด Sh",
            EepromOp::FormatZero => "Format EEPROM 0x00",
            EepromOp::FormatFf => "Format EEPROM 0xFF",
            EepromOp::Dump48k => "ดูดไฟล์กล่อง 48K",
            EepromOp::Dump64k => "ดูดไฟล์กล่อง 64K",
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn read_eeprom_frame_checksum() {
        // For each byte i, last byte must equal 0xE6 - i (mod 256).
        for i in 0u8..=255 {
            let f = shinden::read_eeprom_frame(i);
            let expected = 0xE6u8.wrapping_sub(i);
            assert_eq!(f[5], expected, "mismatch at i={i}");
            assert_eq!(f[4], i);
        }
    }

    #[test]
    fn op_labels_match_source() {
        assert_eq!(EepromOp::Dump48k.label_th(), "ดูดไฟล์กล่อง 48K");
        assert_eq!(EepromOp::Dump64k.label_th(), "ดูดไฟล์กล่อง 64K");
    }
}
