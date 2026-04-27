//! libusb-based FTDI driver - works with WinUSB / libusbK after Zadig
//! has replaced the official FTDI D2XX driver.
//!
//! Implements just enough of the FTDI vendor protocol to drive the
//! K-Line transport: reset, set-baud, set-data, set-bitmode, set-latency
//! and bulk read/write.

use rusb::{Context, Device, DeviceHandle, UsbContext};
use std::time::{Duration, Instant};
use tauri::AppHandle;

use crate::kline_log;
use crate::transport::{KLine, TransportError};

// ----- FTDI vendor protocol -----------------------------------------

const FTDI_VID: u16 = 0x0403;
/// Known FTDI USB-Serial PIDs we want to enumerate.
const FTDI_PIDS: &[u16] = &[
    0x6001, // FT232R
    0x6010, // FT2232H
    0x6011, // FT4232H
    0x6014, // FT232H
    0x6015, // FT-X series
];

const REQ_TYPE_VENDOR_OUT: u8 = 0x40;
// const REQ_TYPE_VENDOR_IN:  u8 = 0xC0;

const REQ_RESET:        u8 = 0x00;
const REQ_SET_BAUD:     u8 = 0x03;
const REQ_SET_DATA:     u8 = 0x04;
const REQ_SET_LATENCY:  u8 = 0x09;
const REQ_SET_BITMODE:  u8 = 0x0B;

// Reset sub-commands (`wValue`).
const SIO_RESET_SIO:      u16 = 0;
const SIO_RESET_PURGE_RX: u16 = 1;
const SIO_RESET_PURGE_TX: u16 = 2;

// Bit-mode masks (`wValue` high byte).
const BITMODE_RESET:    u8 = 0x00;
const BITMODE_BITBANG:  u8 = 0x01;

// Endpoints used by single-channel chips (FT232R, FT-X, FT232H interface 0).
const EP_IN:  u8 = 0x81;
const EP_OUT: u8 = 0x02;

// FTDI prepends a 2-byte modem-status header to *every* read packet.
const FT_HEADER: usize = 2;

const CTRL_TIMEOUT: Duration = Duration::from_millis(200);

// ---------------------------------------------------------------------

/// Public summary of every libusb-visible FTDI device.
///
/// `bus` / `addr` / `vid` / `pid` are surfaced via the `Debug` impl
/// only - the GUI doesn't render them yet, so silence the dead-field
/// lint without dropping the data.
#[allow(dead_code)]
#[derive(Debug, Clone)]
pub struct LibusbDeviceInfo {
    pub bus:    u8,
    pub addr:   u8,
    pub vid:    u16,
    pub pid:    u16,
    pub serial: String,
    pub description: String,
}

/// Enumerate all FTDI devices visible via libusb.
pub fn list() -> Vec<LibusbDeviceInfo> {
    let ctx = match Context::new() {
        Ok(c) => c,
        Err(_) => return Vec::new(),
    };
    let devs = match ctx.devices() {
        Ok(d) => d,
        Err(_) => return Vec::new(),
    };

    let mut out = Vec::new();
    for dev in devs.iter() {
        let Ok(desc) = dev.device_descriptor() else { continue };
        if desc.vendor_id() != FTDI_VID || !FTDI_PIDS.contains(&desc.product_id()) {
            continue;
        }

        // Try to read the descriptor strings (ok to fail).
        let mut serial = String::new();
        let description = match desc.product_id() {
            0x6001 => "FT232R USB UART".to_string(),
            0x6010 => "FT2232H Dual USB UART".to_string(),
            0x6011 => "FT4232H Quad USB UART".to_string(),
            0x6014 => "FT232H Single USB UART".to_string(),
            0x6015 => "FT-X Series USB UART".to_string(),
            other  => format!("FTDI {:04x}:{:04x}", desc.vendor_id(), other),
        };
        if let Ok(handle) = dev.open() {
            if let Ok(s) = handle.read_serial_number_string_ascii(&desc) {
                serial = s;
            }
        }
        out.push(LibusbDeviceInfo {
            bus: dev.bus_number(),
            addr: dev.address(),
            vid: desc.vendor_id(),
            pid: desc.product_id(),
            serial,
            description,
        });
    }
    out
}

// ---------------------------------------------------------------------

/// libusb-backed K-Line transport.
pub struct LibusbKLine {
    handle: DeviceHandle<Context>,
    /// Cache of the latest IN packets (FTDI prepends 2 status bytes per
    /// 64-byte chunk; this struct strips them on read).
    rx_buf: Vec<u8>,
    /// Optional Tauri app handle - when set, every TX/RX block is emitted
    /// to the frontend as a `kline-log` event.
    logger: Option<AppHandle>,
}

impl LibusbKLine {
    /// Open the `index`-th FTDI device with the standard Honda KWP
    /// fast-init wakeup pulse - required for the EEPROM tool's KWP
    /// session.
    pub fn open(
        index: u32,
        log: &mut Vec<String>,
        logger: Option<AppHandle>,
    ) -> Result<Self, TransportError> {
        Self::open_inner(index, log, logger, true)
    }

    /// Open without the bit-bang wakeup pulse - just configures UART
    /// at 10_400 baud. Reserved for any future protocol that talks
    /// over plain UART (e.g. TyN-Shop S.exe `K_CONNECT`). Not wired
    /// to any caller right now because the proven Honda live-data
    /// path uses the pulsed `open()`.
    #[allow(dead_code)]
    pub fn open_bare(
        index: u32,
        log: &mut Vec<String>,
        logger: Option<AppHandle>,
    ) -> Result<Self, TransportError> {
        Self::open_inner(index, log, logger, false)
    }

    fn open_inner(
        index: u32,
        log: &mut Vec<String>,
        logger: Option<AppHandle>,
        wakeup_pulse: bool,
    ) -> Result<Self, TransportError> {
        if let Some(app) = &logger {
            kline_log::info(app, format!("[libusb] opening device #{index}"));
        }
        let ctx = Context::new().map_err(|e| TransportError::Io(e.to_string()))?;
        let devs = ctx.devices().map_err(|e| TransportError::Io(e.to_string()))?;

        let mut device: Option<Device<Context>> = None;
        let mut count: u32 = 0;
        for dev in devs.iter() {
            if let Ok(desc) = dev.device_descriptor() {
                if desc.vendor_id() == FTDI_VID && FTDI_PIDS.contains(&desc.product_id()) {
                    if count == index {
                        device = Some(dev);
                        break;
                    }
                    count += 1;
                }
            }
        }
        let device = device.ok_or(TransportError::NoDevice(index))?;
        let handle = device.open().map_err(|e| {
            // libusb on Windows returns NOT_SUPPORTED when the device's
            // current driver is not WinUSB/libusbK. Make that actionable.
            let extra = match e {
                rusb::Error::NotSupported => " (driver != WinUSB/libusbK - run Zadig and replace the driver)",
                rusb::Error::Access       => " (insufficient permissions - run as administrator)",
                rusb::Error::Busy         => " (already opened by another process)",
                _ => "",
            };
            if let Some(app) = &logger {
                kline_log::err(app, format!("libusb open failed: {e}{extra}"));
            }
            TransportError::Io(format!("libusb open: {e}{extra}"))
        })?;

        // Detach the kernel driver on Linux / macOS so we can claim the
        // interface. Windows uses WinUSB / libusbK explicitly so this is
        // a no-op there.
        #[cfg(any(target_os = "linux", target_os = "macos"))]
        let _ = handle.set_auto_detach_kernel_driver(true);

        handle.claim_interface(0).map_err(|e| TransportError::Io(format!("claim_interface: {e}")))?;
        log.push("[libusb] claimed interface 0".into());
        if let Some(app) = &logger {
            kline_log::info(app, "[libusb] claimed interface 0");
        }

        let me = Self { handle, rx_buf: Vec::with_capacity(512), logger };
        me.reset()?;
        me.set_data_8n1()?;
        me.set_baud(921_600)?;
        me.set_latency(8)?;
        log.push("[libusb] initial config: 921600 8N1, latency 8ms".into());
        if let Some(app) = &me.logger {
            kline_log::info(app, "[libusb] 921600 baud, 8N1, latency 8ms");
        }

        if wakeup_pulse {
            // Honda KWP fast-init wakeup pulse - byte-for-byte port of
            // `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_30` (the
            // proven-on-real-hardware C# original). Pulls K-Line LOW
            // for 70 ms then immediately drops bit-bang back to UART;
            // the post-handover 130 ms `Thread.Sleep` lets the ECU
            // settle into KWP-2000 diag mode before the first frame
            // hits the bus.
            me.set_bitmode(0x01, BITMODE_BITBANG)?;
            let _ = me.bulk_write(&[0x00]);
            std::thread::sleep(Duration::from_millis(70));
            let _ = me.bulk_write(&[0x01]);
            me.set_bitmode(0x00, BITMODE_RESET)?;
            log.push("[libusb] sent KWP fast-init wakeup pulse (70 ms LOW)".into());
            if let Some(app) = &me.logger {
                kline_log::info(app, "[libusb] sent KWP fast-init wakeup pulse (70 ms LOW)");
            }
        } else {
            log.push("[libusb] skipping wakeup pulse (bare K-Line open)".into());
            if let Some(app) = &me.logger {
                kline_log::info(app, "[libusb] skipping wakeup pulse (bare K-Line open)");
            }
        }

        // Down-shift to K-Line speed, purge any garbage and let the
        // ECU settle (130 ms - matches the original C# `Thread.Sleep`).
        me.set_baud(10_400)?;
        me.purge_all()?;
        if wakeup_pulse {
            std::thread::sleep(Duration::from_millis(130));
        }
        log.push("[libusb] handover to 10400 baud K-Line".into());
        if let Some(app) = &me.logger {
            kline_log::info(app, "[libusb] handover to 10400 baud K-Line - ready");
        }

        Ok(me)
    }

    // ---- FTDI control transfers --------------------------------------

    fn ctrl(&self, request: u8, value: u16, index: u16) -> Result<(), TransportError> {
        self.handle
            .write_control(REQ_TYPE_VENDOR_OUT, request, value, index, &[], CTRL_TIMEOUT)
            .map_err(|e| TransportError::Io(format!("ctrl 0x{request:02x}: {e}")))?;
        Ok(())
    }

    fn reset(&self) -> Result<(), TransportError> {
        self.ctrl(REQ_RESET, SIO_RESET_SIO, 0)
    }
    fn purge_all(&self) -> Result<(), TransportError> {
        self.ctrl(REQ_RESET, SIO_RESET_PURGE_RX, 0)?;
        self.ctrl(REQ_RESET, SIO_RESET_PURGE_TX, 0)
    }
    fn set_data_8n1(&self) -> Result<(), TransportError> {
        // 8 data bits, no parity, 1 stop bit -> wValue = 0x0008
        self.ctrl(REQ_SET_DATA, 0x0008, 0)
    }
    fn set_latency(&self, ms: u8) -> Result<(), TransportError> {
        self.ctrl(REQ_SET_LATENCY, ms as u16, 0)
    }
    fn set_bitmode(&self, mask: u8, mode: u8) -> Result<(), TransportError> {
        let value = ((mode as u16) << 8) | (mask as u16);
        self.ctrl(REQ_SET_BITMODE, value, 0)
    }
    fn set_baud(&self, baud: u32) -> Result<(), TransportError> {
        let (value, index) = baud_divisor(baud);
        self.ctrl(REQ_SET_BAUD, value, index)
    }

    // ---- bulk I/O ----------------------------------------------------

    fn bulk_write(&self, buf: &[u8]) -> Result<usize, TransportError> {
        self.handle
            .write_bulk(EP_OUT, buf, Duration::from_millis(150))
            .map_err(|e| TransportError::Io(format!("bulk_write: {e}")))
    }

    /// Read at least `min` bytes of *payload* (after stripping FTDI's
    /// 2-byte modem-status prefix from each 64-byte USB chunk).
    ///
    /// When `min == 0` (the round-trip case) we deliberately keep
    /// polling until the outer `timeout` expires - the K-Line bus
    /// echoes our TX bytes back almost immediately, but the ECU's
    /// reply lands 25-100 ms later (Honda KWP P2 timing). Returning
    /// early after just the echo would make us miss every real reply.
    fn bulk_read(&mut self, min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError> {
        let started = Instant::now();
        let mut packet = [0u8; 64];

        while started.elapsed() < timeout {
            // Cap each USB read at the remaining outer-timeout budget so
            // we don't overshoot if the bus is silent for the rest of
            // the window.
            let remaining = timeout.saturating_sub(started.elapsed());
            let chunk = remaining.min(Duration::from_millis(50));
            if chunk.is_zero() { break; }

            match self.handle.read_bulk(EP_IN, &mut packet, chunk) {
                Ok(n) => {
                    if n > FT_HEADER {
                        self.rx_buf.extend_from_slice(&packet[FT_HEADER..n]);
                    }
                    if min > 0 && self.rx_buf.len() >= min {
                        break;
                    }
                    // For `min == 0` we keep looping until the outer
                    // timeout - never break early just because *some*
                    // bytes (= TX echo) showed up.
                }
                Err(rusb::Error::Timeout) => continue,
                Err(e) => return Err(TransportError::Io(format!("bulk_read: {e}"))),
            }
        }

        if self.rx_buf.is_empty() {
            return Err(TransportError::Io("recv: no bytes within timeout".into()));
        }
        let out = std::mem::take(&mut self.rx_buf);
        Ok(out)
    }
}

impl KLine for LibusbKLine {
    fn purge(&mut self) -> Result<(), TransportError> {
        // FT_Purge(handle, 3) - mirrors the cleanup the C# original
        // does after every method_22 call. Also clears our local RX
        // cache so leftover header bytes don't bleed into the next
        // chunk.
        self.purge_all()?;
        self.rx_buf.clear();
        Ok(())
    }

    fn send(&mut self, frame: &[u8]) -> Result<(), TransportError> {
        if let Some(app) = &self.logger {
            kline_log::tx(app, frame);
        }
        let n = self.bulk_write(frame)?;
        if n != frame.len() {
            return Err(TransportError::Io(format!("short write {n}/{}", frame.len())));
        }
        Ok(())
    }
    fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError> {
        match self.bulk_read(expected_min, timeout) {
            Ok(buf) => {
                if let Some(app) = &self.logger {
                    kline_log::rx(app, &buf);
                }
                Ok(buf)
            }
            Err(e) => {
                if let Some(app) = &self.logger {
                    kline_log::err(app, format!("recv: {e}"));
                }
                Err(e)
            }
        }
    }
}

// ---------------------------------------------------------------------

/// FTDI baud-rate divisor encoder.
///
/// Reference implementation lifted from `libftdi` (LGPL) — see
/// `ftdi_convert_baudrate()` in `libftdi/src/ftdi.c`. Only the FT232R /
/// FT-X path is reproduced because that's the chip in our K-Line cables.
fn baud_divisor(baud: u32) -> (u16, u16) {
    if baud == 0 {
        return (0, 0);
    }
    if baud >= 3_000_000 {
        return (0, 0);            // 3 MHz - special encoding
    }
    if baud >= 2_000_000 {
        return (1, 0);            // 2 MHz - special encoding
    }

    // 8x oversampling so we can encode the 1/8th-bit fractional part.
    let frac_codes: [u16; 8] = [0, 3, 2, 4, 1, 5, 6, 7];
    let divisor3 = (3_000_000u32 * 8) / baud;
    let frac = (divisor3 & 0x7) as usize;
    let mut div = divisor3 >> 3;
    if div < 2 && frac == 0 { div = 1; }
    let frac_code = frac_codes[frac];

    let value = (div as u16 & 0x3FFF) | ((frac_code & 0x03) << 14);
    let index = (frac_code >> 2) & 0x01;
    (value, index)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn divisor_for_kline_speeds() {
        // Just smoke-tests - the exact value depends on rounding, but
        // 10400 baud should always give a value > 0.
        let (v, _) = baud_divisor(10_400);
        assert!(v > 0);
        let (v2, _) = baud_divisor(921_600);
        assert!(v2 > 0);
    }
}
