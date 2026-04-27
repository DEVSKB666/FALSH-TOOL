//! libusb-based FTDI driver for the bridge daemon.
//!
//! Mirrors `app/src-tauri/src/libusb_ftdi.rs` but with all Tauri-only
//! plumbing (AppHandle / streaming logger) removed - the bridge has no
//! UI to stream into. Per-step messages are pushed into a `Vec<String>`
//! that the caller already passes through to the JSON response.
//!
//! Used when the FTDI cable is bound to WinUSB / libusbK (typically via
//! Zadig) instead of FTDI's own D2XX driver. Implements just enough of
//! the FTDI vendor protocol to drive the K-Line transport: reset,
//! set-baud, set-data, set-bitmode, set-latency and bulk read/write.

use crate::transport::{KLine, TransportError};
use rusb::{Context, Device, DeviceHandle, UsbContext};
use std::time::{Duration, Instant};

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
#[derive(Debug, Clone)]
pub struct LibusbDeviceInfo {
    pub serial: String,
    pub description: String,
}

/// Enumerate all FTDI devices visible via libusb. Errors during context
/// creation or device walk return an empty list - the caller still
/// gets a usable `Vec` and the daemon's D2XX list will fill in the gap.
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
        out.push(LibusbDeviceInfo { serial, description });
    }
    out
}

// ---------------------------------------------------------------------

/// libusb-backed K-Line transport - mirrors `transport::FtdiKLine`'s
/// public surface so the dispatch in `transport::open_kline` can pick
/// either implementation at runtime.
pub struct LibusbKLine {
    handle: DeviceHandle<Context>,
    /// Cache of the latest IN packets (FTDI prepends 2 status bytes per
    /// 64-byte chunk; this struct strips them on read).
    rx_buf: Vec<u8>,
}

impl LibusbKLine {
    /// Open the `index`-th FTDI device with the standard Honda KWP
    /// fast-init pulse on the K-Line bus. Required for the EEPROM
    /// tool's KWP-2000 session.
    pub fn open(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
        Self::open_inner(index, log, true)
    }

    /// Open without the bit-bang wakeup pulse - just configures UART
    /// at 10_400 baud. Required for the TyN-Shop live-data protocol,
    /// which sends its own `K_CONNECT` byte sequence over plain UART
    /// and gets confused (i.e. the ECU stays silent) if the bus has
    /// already been pulsed into KWP fast-init mode.
    pub fn open_bare(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
        Self::open_inner(index, log, false)
    }

    fn open_inner(index: u32, log: &mut Vec<String>, wakeup_pulse: bool) -> Result<Self, TransportError> {
        log.push(format!("[libusb] opening device #{index}"));

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
            // current driver is not WinUSB / libusbK. Make that
            // actionable so the operator knows what to do next.
            let extra = match e {
                rusb::Error::NotSupported => " (driver != WinUSB/libusbK - run Zadig and replace the driver, or use the d2xx backend)",
                rusb::Error::Access       => " (insufficient permissions - run as administrator)",
                rusb::Error::Busy         => " (already opened by another process)",
                _ => "",
            };
            log.push(format!("[libusb] open #{index} failed: {e}{extra}"));
            TransportError::Io(format!("libusb open: {e}{extra}"))
        })?;

        // Detach the kernel driver on Linux / macOS so we can claim the
        // interface. Windows uses WinUSB / libusbK explicitly so this is
        // a no-op there.
        #[cfg(any(target_os = "linux", target_os = "macos"))]
        let _ = handle.set_auto_detach_kernel_driver(true);

        handle.claim_interface(0).map_err(|e| TransportError::Io(format!("claim_interface: {e}")))?;
        log.push("[libusb] claimed interface 0".into());

        let me = Self { handle, rx_buf: Vec::with_capacity(512) };
        me.reset()?;
        me.set_data_8n1()?;
        me.set_baud(921_600)?;
        me.set_latency(8)?;
        log.push("[libusb] initial config: 921600 8N1, latency 8ms".into());

        if wakeup_pulse {
            // Honda KWP fast-init wakeup pulse - byte-for-byte port of
            // `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_30`. 70 ms
            // LOW (NOT 25 ms - Honda needs the longer pulse) followed
            // immediately by HIGH and a bit-mode reset.
            me.set_bitmode(0x01, BITMODE_BITBANG)?;
            let _ = me.bulk_write(&[0x00]);
            std::thread::sleep(Duration::from_millis(70));
            let _ = me.bulk_write(&[0x01]);
            me.set_bitmode(0x00, BITMODE_RESET)?;
            log.push("[libusb] sent KWP fast-init wakeup pulse (70 ms LOW)".into());
        } else {
            log.push("[libusb] skipping wakeup pulse (bare K-Line open)".into());
        }

        // Down-shift to K-Line speed, purge, then 130 ms settle - the
        // post-handover `Thread.Sleep(130)` from the C# original.
        // Skipping it leaves the ECU in a half-initialised state and
        // the first KWP frame goes into the void.
        me.set_baud(10_400)?;
        me.purge_all()?;
        if wakeup_pulse {
            std::thread::sleep(Duration::from_millis(130));
        }
        log.push("[libusb] handover to 10400 baud K-Line".into());

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
    fn send(&mut self, frame: &[u8]) -> Result<(), TransportError> {
        let n = self.bulk_write(frame)?;
        if n != frame.len() {
            return Err(TransportError::Io(format!("short write {n}/{}", frame.len())));
        }
        Ok(())
    }
    fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError> {
        self.bulk_read(expected_min, timeout)
    }
}

// ---------------------------------------------------------------------

/// FTDI baud-rate divisor encoder.
///
/// Reference implementation lifted from `libftdi` (LGPL) - see
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
