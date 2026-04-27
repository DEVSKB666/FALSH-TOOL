//! K-Line transport over an FTDI USB-Serial adapter.
//!
//! Two backends are compiled in conditionally:
//!   * `hardware` (default) - real I/O via `libftd2xx`
//!   * `mock` (test)        - in-memory stub that returns canned bytes
//!
//! Both implement the same [`KLine`] trait so the rest of the daemon
//! never has to care which one is in use.

use std::time::Duration;
use thiserror::Error;

#[derive(Debug, Error)]
#[allow(dead_code)] // some variants only appear in the `hardware` build
pub enum TransportError {
    /// Underlying FTDI error message (chip removed, bad index, etc.).
    #[error("ftdi: {0}")]
    Ftdi(String),

    /// No FTDI device was found at the requested index.
    #[error("no FTDI device at index {0}")]
    NoDevice(u32),

    /// Generic I/O error wrapped as a string for transport-agnostic
    /// reporting.
    #[error("io: {0}")]
    Io(String),
}

/// Minimal trait the high-level Honda code talks to. Mirrors the
/// equivalent in `app/src-tauri/src/transport.rs` so we can reuse the
/// same EEPROM-read logic verbatim.
pub trait KLine {
    /// Push raw bytes onto the wire.
    fn send(&mut self, frame: &[u8]) -> Result<(), TransportError>;

    /// Read whatever the ECU returns. `expected_min` is a soft target -
    /// the caller is happy as long as something arrives within
    /// `timeout`.
    fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError>;

    /// Convenience: send a frame and immediately read the reply.
    fn round_trip(&mut self, frame: &[u8], timeout: Duration) -> Result<Vec<u8>, TransportError> {
        self.send(frame)?;
        self.recv(0, timeout)
    }

    /// Drop any bytes still buffered in the FTDI's RX/TX queues.
    /// Used between chunks of long bulk-read sessions to mirror the
    /// `FT_Purge(handle, 3)` call in the C# original `method_22`. The
    /// default no-op keeps mock transports working without changes.
    fn purge(&mut self) -> Result<(), TransportError> {
        Ok(())
    }
}

/// One entry returned by [`list_ports`].
#[derive(Debug, Clone)]
pub struct DeviceInfo {
    /// Numeric index used as the `device_index` argument elsewhere.
    /// **Indices are scoped per backend** - `(backend = "d2xx", index = 0)`
    /// and `(backend = "libusb", index = 0)` may refer to the same
    /// physical cable bound to two different drivers.
    pub index: u32,
    /// FTDI EEPROM serial number.
    pub serial: String,
    /// FTDI EEPROM product description.
    pub description: String,
    /// Which transport opens this entry: `"d2xx"` or `"libusb"`.
    pub backend: String,
}

// ---------------------------------------------------------------------
// Real (libftd2xx) backend
// ---------------------------------------------------------------------

#[cfg(all(feature = "hardware", not(feature = "mock")))]
mod real {
    use super::*;
    use std::time::Instant;
    use libftd2xx::{
        BitMode, BitsPerWord, Ftdi, FtdiCommon, Parity,
        StopBits,
    };

    /// FTDI driver wrapper that implements [`KLine`].
    pub struct FtdiKLine {
        ftdi: Ftdi,
    }

    impl FtdiKLine {
        /// Open device `index` and run the wakeup + 10_400 baud handover.
        pub fn open(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
            Self::open_inner(index, log, true)
        }

        /// Open without the bit-bang wakeup pulse (TyN-Shop live-data
        /// path - the ECU stays silent if it's been pulsed into KWP
        /// fast-init mode but expects bare UART `K_CONNECT`).
        pub fn open_bare(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
            Self::open_inner(index, log, false)
        }

        fn open_inner(index: u32, log: &mut Vec<String>, wakeup_pulse: bool) -> Result<Self, TransportError> {
            // Preserve the underlying FTDI error string so the operator
            // can distinguish "DEVICE_NOT_FOUND" (cable unplugged) from
            // "DEVICE_NOT_OPENED" (held by VCP / another process) -
            // both surface here, but they need different fixes.
            let mut ftdi = match Ftdi::with_index(index as i32) {
                Ok(d) => d,
                Err(e) => {
                    let msg = e.to_string();
                    log.push(format!("[d2xx] open #{index} failed: {msg}"));
                    return Err(TransportError::Ftdi(format!(
                        "open device #{index}: {msg} \
                         (hint: if list_ports sees the device, the most \
                         common cause is the VCP driver holding the \
                         handle - Device Manager → USB Serial Converter \
                         → Properties → Advanced → uncheck 'Load VCP', \
                         then unplug + replug the FTDI cable)"
                    )));
                }
            };
            log.push(format!("[d2xx] opened device #{index}"));

            ftdi.purge_all()                                       .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.set_bit_mode(0x00, BitMode::Reset)                .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.set_data_characteristics(BitsPerWord::Bits8, StopBits::Bits1, Parity::No)
                .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.set_baud_rate(921_600)                            .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.set_timeouts(Duration::from_millis(50), Duration::from_millis(50))
                .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.set_latency_timer(Duration::from_millis(8))       .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            log.push("[d2xx] 921600 baud, 8N1, latency 8ms".into());

            if wakeup_pulse {
                // Honda KWP fast-init wakeup pulse - byte-for-byte port
                // of `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_30`.
                // 70 ms LOW (NOT 25 ms) followed immediately by HIGH.
                ftdi.set_bit_mode(0x01, BitMode::AsyncBitbang)         .map_err(|e| TransportError::Ftdi(e.to_string()))?;
                let _ = ftdi.write_all(&[0x00]);
                std::thread::sleep(Duration::from_millis(70));
                let _ = ftdi.write_all(&[0x01]);
                ftdi.set_bit_mode(0x00, BitMode::Reset)                .map_err(|e| TransportError::Ftdi(e.to_string()))?;
                log.push("[d2xx] sent KWP fast-init wakeup pulse (70 ms LOW)".into());
            } else {
                log.push("[d2xx] skipping wakeup pulse (bare K-Line open)".into());
            }

            // Back to UART at K-Line speed; the 130 ms post-handover
            // `Thread.Sleep` from the C# original is what actually
            // gets the ECU listening before the first KWP frame.
            ftdi.set_baud_rate(10_400)                             .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            ftdi.purge_all()                                       .map_err(|e| TransportError::Ftdi(e.to_string()))?;
            if wakeup_pulse {
                std::thread::sleep(Duration::from_millis(130));
            }
            log.push("[d2xx] handover to 10400 baud K-Line".into());

            Ok(Self { ftdi })
        }
    }

    impl KLine for FtdiKLine {
        fn purge(&mut self) -> Result<(), TransportError> {
            // FT_Purge(handle, 3) - mirrors the cleanup the C# original
            // does after every method_22 call.
            self.ftdi.purge_all().map_err(|e| TransportError::Ftdi(e.to_string()))
        }
        fn send(&mut self, frame: &[u8]) -> Result<(), TransportError> {
            self.ftdi.write_all(frame).map_err(|e| TransportError::Io(e.to_string()))
        }

        fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError> {
            let started = Instant::now();
            let mut buf = Vec::with_capacity(64.max(expected_min));
            let mut tmp = [0u8; 256];
            while started.elapsed() < timeout {
                match self.ftdi.read_all(&mut tmp[..1]) {
                    Ok(()) => {
                        buf.push(tmp[0]);
                        if let Ok(extra) = self.ftdi.queue_status() {
                            if extra > 0 {
                                let n = extra.min(tmp.len());
                                if self.ftdi.read_all(&mut tmp[..n]).is_ok() {
                                    buf.extend_from_slice(&tmp[..n]);
                                }
                            }
                        }
                        if expected_min > 0 && buf.len() >= expected_min {
                            return Ok(buf);
                        }
                    }
                    Err(_) => std::thread::sleep(Duration::from_millis(2)),
                }
            }
            if buf.is_empty() {
                return Err(TransportError::Io("recv: no bytes within timeout".into()));
            }
            Ok(buf)
        }
    }

    /// Enumerate FTDI devices visible to D2XX. Errors are returned as
    /// the standard `TransportError::Ftdi` variant - the caller can
    /// decide whether to merge with a libusb list or surface the
    /// failure.
    pub fn list_ports_d2xx() -> Result<Vec<DeviceInfo>, TransportError> {
        let infos = libftd2xx::list_devices()
            .map_err(|e| TransportError::Ftdi(e.to_string()))?;
        Ok(infos
            .into_iter()
            .enumerate()
            .map(|(i, d)| DeviceInfo {
                index: i as u32,
                serial: d.serial_number,
                description: d.description,
                backend: "d2xx".to_string(),
            })
            .collect())
    }
}

// ---------------------------------------------------------------------
// Mock backend
// ---------------------------------------------------------------------

#[cfg(feature = "mock")]
mod mock {
    use super::*;

    /// Mock implementation of [`KLine`]. The `recv` side returns
    /// canned 13-byte frames whose bytes 11+12 form a slowly-changing
    /// pattern, so callers see plausible "EEPROM data" without any
    /// hardware. Useful for end-to-end testing of the JSON protocol.
    pub struct FtdiKLine {
        tick: u32,
    }

    impl FtdiKLine {
        /// Pretend to open device `index`. Always succeeds.
        pub fn open(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
            log.push(format!("[mock] opened device #{index}"));
            log.push("[mock] no real K-Line - returning canned bytes".into());
            Ok(Self { tick: 0 })
        }

        /// Mock companion to `FtdiKLine::open_bare` - identical behaviour
        /// since there's no real K-Line to pulse anyway.
        pub fn open_bare(index: u32, log: &mut Vec<String>) -> Result<Self, TransportError> {
            Self::open(index, log)
        }
    }

    impl KLine for FtdiKLine {
        fn send(&mut self, _frame: &[u8]) -> Result<(), TransportError> {
            // Pretend the bytes flew off the wire.
            Ok(())
        }

        fn recv(&mut self, _expected_min: usize, _timeout: Duration) -> Result<Vec<u8>, TransportError> {
            // 13-byte plausible response with bytes [11]+[12] varying so
            // the caller's data buffer has visible structure.
            self.tick = self.tick.wrapping_add(1);
            let lo = (self.tick      & 0xFF) as u8;
            let hi = ((self.tick / 7) & 0xFF) as u8;
            Ok(vec![
                0x91, 0x91, 0x0D, 0xDF, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, hi,   lo,
            ])
        }
    }

    /// Pretend to enumerate one fake FTDI device. The mock backend
    /// reports itself under the synthetic `"mock"` tag so the daemon's
    /// dispatch logic still has something to match on (and so the GUI
    /// can render a `[MOCK]` chip in the port dropdown).
    pub fn list_ports_mock() -> Result<Vec<DeviceInfo>, TransportError> {
        Ok(vec![DeviceInfo {
            index: 0,
            serial: "MOCK-FT-0".into(),
            description: "Mock FTDI USB-Serial (loy-bridge --features mock)".into(),
            backend: "mock".into(),
        }])
    }
}

// ---------------------------------------------------------------------
// Public surface - dispatches between the D2XX, libusb and mock
// backends at runtime based on the operator's `backend` selection.
// ---------------------------------------------------------------------

#[cfg(not(any(feature = "hardware", feature = "mock")))]
compile_error!("loy-bridge requires either the `hardware` (default) or `mock` feature");

/// Enumerate every FTDI device visible to *any* backend the daemon
/// was compiled with. The list is the union of:
///   * D2XX (FTDI's official driver)         - `backend = "d2xx"`
///   * libusb (WinUSB / libusbK after Zadig) - `backend = "libusb"`
///   * mock (only when `--features mock`)    - `backend = "mock"`
///
/// Indices are scoped per backend, so two entries with the same
/// `index` may co-exist as long as they carry different `backend`
/// tags.
pub fn list_ports() -> Result<Vec<DeviceInfo>, TransportError> {
    let mut out: Vec<DeviceInfo> = Vec::new();

    #[cfg(all(feature = "hardware", not(feature = "mock")))]
    {
        // D2XX side. Errors (driver missing, etc.) are demoted to a
        // warning and an empty contribution so a half-broken D2XX
        // install can't hide the libusb devices.
        match real::list_ports_d2xx() {
            Ok(mut v) => out.append(&mut v),
            Err(e) => tracing::warn!(error = %e, "D2XX enumeration failed - skipping"),
        }

        // libusb side - errors during enumeration are already swallowed
        // inside `libusb_ftdi::list()` (returns an empty Vec).
        let libusb_devs = crate::libusb_ftdi::list();
        for (i, d) in libusb_devs.into_iter().enumerate() {
            out.push(DeviceInfo {
                index: i as u32,
                serial: d.serial,
                description: d.description,
                backend: "libusb".to_string(),
            });
        }
    }

    #[cfg(feature = "mock")]
    {
        out.append(&mut mock::list_ports_mock()?);
    }

    Ok(out)
}

/// A trait object you can drop straight into the EEPROM helpers.
///
/// `+ Send` is required so the daemon can keep an open transport in a
/// process-wide `Mutex` (the persistent live-data session in
/// `server.rs`) and hand it to whichever client thread happens to
/// service the next `read_live_sample`.
pub type DynKLine = Box<dyn KLine + Send>;

/// Open the right transport for `(backend, index)` with the standard
/// KWP fast-init wakeup pulse. Use this for the EEPROM tool and any
/// other Honda KWP-2000 session.
pub fn open_kline(
    index: u32,
    backend: &str,
    log: &mut Vec<String>,
) -> Result<DynKLine, TransportError> {
    open_kline_inner(index, backend, log, true)
}

/// Open the transport without the bit-bang wakeup pulse - the K-Line
/// stays at its idle level so the ECU never enters Honda KWP fast-init
/// mode. Reserved for any future protocol that talks over plain UART
/// (e.g. TyN-Shop S.exe `K_CONNECT`); not currently wired into any
/// caller because the proven Honda live-data path uses the pulse.
#[allow(dead_code)]
pub fn open_kline_bare(
    index: u32,
    backend: &str,
    log: &mut Vec<String>,
) -> Result<DynKLine, TransportError> {
    open_kline_inner(index, backend, log, false)
}

fn open_kline_inner(
    index: u32,
    backend: &str,
    log: &mut Vec<String>,
    wakeup_pulse: bool,
) -> Result<DynKLine, TransportError> {
    match backend {
        #[cfg(all(feature = "hardware", not(feature = "mock")))]
        "libusb" => Ok(if wakeup_pulse {
            Box::new(crate::libusb_ftdi::LibusbKLine::open(index, log)?)
        } else {
            Box::new(crate::libusb_ftdi::LibusbKLine::open_bare(index, log)?)
        }),

        // "d2xx" is the historical default - keep it as the fallback
        // so requests that omit the backend field still work.
        #[cfg(all(feature = "hardware", not(feature = "mock")))]
        "d2xx" | "" => Ok(if wakeup_pulse {
            Box::new(real::FtdiKLine::open(index, log)?)
        } else {
            Box::new(real::FtdiKLine::open_bare(index, log)?)
        }),

        #[cfg(feature = "mock")]
        _ => Ok(Box::new(mock::FtdiKLine::open(index, log)?)),

        #[cfg(not(feature = "mock"))]
        other => Err(TransportError::Io(format!(
            "unknown backend {other:?} - expected \"d2xx\" or \"libusb\""
        ))),
    }
}
