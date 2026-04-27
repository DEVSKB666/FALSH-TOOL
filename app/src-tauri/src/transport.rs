//! K-Line transport over an FTDI USB-Serial adapter.
//!
//! Mirrors the trait that the `mza_tuner` library exposes (we don't take a
//! direct dependency on it from here so we can keep this driver
//! tauri-specific - logging is collected into a frontend-visible `Vec`).

use libftd2xx::{BitMode, BitsPerWord, FtStatus, Ftdi, FtdiCommon, Parity, StopBits, TimeoutError};
use std::time::{Duration, Instant};
use tauri::AppHandle;
use thiserror::Error;

use crate::kline_log;

#[derive(Debug, Error)]
pub enum TransportError {
    #[error("ftdi: {0}")]
    Ftdi(#[from] FtStatus),
    #[error("ftdi-timeout: {0}")]
    Timeout(#[from] TimeoutError),
    #[error("no FTDI device at index {0}")]
    NoDevice(u32),
    #[error("io: {0}")]
    Io(String),
}

/// Minimal trait that high-level Honda code (`crate::eeprom`) speaks against.
///
/// `Send` is required so the trait object can live inside a Tauri-managed
/// `Mutex<Option<Box<dyn KLine + Send>>>` (used by the persistent live-data
/// session). The handle is only ever touched by one thread at a time
/// because the Mutex guards every access; we don't need `Sync`.
pub trait KLine: Send {
    fn send(&mut self, frame: &[u8]) -> Result<(), TransportError>;
    fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError>;

    /// Send a frame and read whatever the ECU echoes back within the timeout.
    fn round_trip(&mut self, frame: &[u8], timeout: Duration) -> Result<Vec<u8>, TransportError> {
        self.send(frame)?;
        self.recv(0, timeout)
    }
}

/// Concrete implementation that drives the FTDI chip directly.
pub struct FtdiKLine {
    ftdi: Ftdi,
    /// Optional Tauri app handle - when set, every TX/RX byte block is
    /// streamed to the frontend as a `kline-log` event.
    logger: Option<AppHandle>,
}

impl FtdiKLine {
    /// Open `index` with the standard Honda KWP fast-init wakeup pulse -
    /// what `Form_EepromTool::Initialize()` does in the original C# code
    /// and what the EEPROM tool path needs.
    pub fn open(
        index: u32,
        log: &mut Vec<String>,
        logger: Option<AppHandle>,
    ) -> Result<Self, TransportError> {
        Self::open_inner(index, log, logger, true)
    }

    /// Open without the bit-bang wakeup pulse. Reserved for any
    /// future protocol that talks over plain UART (e.g. TyN-Shop
    /// S.exe `K_CONNECT`); not currently wired into any caller because
    /// the proven Honda live-data path uses the pulsed `open()`.
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
            kline_log::info(app, format!("[d2xx] opening device #{index}"));
        }
        let mut ftdi = Ftdi::with_index(index as i32).map_err(|_| TransportError::NoDevice(index))?;
        log.push(format!("[d2xx] opened device #{index}"));

        ftdi.purge_all()?;
        ftdi.set_bit_mode(0x00, BitMode::Reset)?;
        ftdi.set_data_characteristics(BitsPerWord::Bits8, StopBits::Bits1, Parity::No)?;
        ftdi.set_baud_rate(921_600)?;
        ftdi.set_timeouts(Duration::from_millis(50), Duration::from_millis(50))?;
        ftdi.set_latency_timer(Duration::from_millis(8))?;
        if let Some(app) = &logger {
            kline_log::info(app, "[d2xx] 921600 baud, 8N1, latency 8ms");
        }

        if wakeup_pulse {
            // Honda KWP fast-init wakeup pulse - byte-for-byte port of
            // `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_30` (the
            // proven C# original). Pulls K-Line LOW for 70 ms (NOT
            // 25 ms - Honda ECUs need the longer pulse to register).
            ftdi.set_bit_mode(0x01, BitMode::AsyncBitbang)?;
            let _ = ftdi.write_all(&[0x00]);
            std::thread::sleep(Duration::from_millis(70));
            let _ = ftdi.write_all(&[0x01]);
            ftdi.set_bit_mode(0x00, BitMode::Reset)?;
            log.push("[d2xx] sent KWP fast-init wakeup pulse (70 ms LOW)".to_string());
            if let Some(app) = &logger {
                kline_log::info(app, "[d2xx] sent KWP fast-init wakeup pulse (70 ms LOW)");
            }
        } else {
            log.push("[d2xx] skipping wakeup pulse (bare K-Line open)".to_string());
            if let Some(app) = &logger {
                kline_log::info(app, "[d2xx] skipping wakeup pulse (bare K-Line open)");
            }
        }

        // Back to UART at K-Line speed; the 130 ms post-handover
        // `Thread.Sleep` from the C# original lets the ECU settle
        // before the first KWP frame hits the bus.
        ftdi.set_baud_rate(10_400)?;
        ftdi.purge_all()?;
        if wakeup_pulse {
            std::thread::sleep(Duration::from_millis(130));
        }
        log.push("[d2xx] handover to 10400 baud K-Line".to_string());
        if let Some(app) = &logger {
            kline_log::info(app, "[d2xx] handover to 10400 baud K-Line - ready");
        }

        Ok(Self { ftdi, logger })
    }
}

impl KLine for FtdiKLine {
    fn send(&mut self, frame: &[u8]) -> Result<(), TransportError> {
        if let Some(app) = &self.logger {
            kline_log::tx(app, frame);
        }
        self.ftdi.write_all(frame)?;
        Ok(())
    }

    fn recv(&mut self, expected_min: usize, timeout: Duration) -> Result<Vec<u8>, TransportError> {
        let started = Instant::now();
        let mut buf = Vec::with_capacity(64.max(expected_min));
        let mut tmp = [0u8; 256];
        while started.elapsed() < timeout {
            // poll using a tiny non-blocking style read; ignore individual timeout
            match self.ftdi.read_all(&mut tmp[..1]) {
                Ok(()) => {
                    buf.push(tmp[0]);
                    // Drain the rest of what's available
                    if let Ok(extra) = self.ftdi.queue_status() {
                        if extra > 0 {
                            let n = extra.min(tmp.len());
                            if self.ftdi.read_all(&mut tmp[..n]).is_ok() {
                                buf.extend_from_slice(&tmp[..n]);
                            }
                        }
                    }
                    if expected_min > 0 && buf.len() >= expected_min {
                        if let Some(app) = &self.logger {
                            kline_log::rx(app, &buf);
                        }
                        return Ok(buf);
                    }
                }
                Err(_) => {
                    // timed out for this poll; try again until outer timeout
                    std::thread::sleep(Duration::from_millis(2));
                }
            }
        }
        if buf.is_empty() {
            if let Some(app) = &self.logger {
                kline_log::err(app, "recv timeout (no bytes from ECU)");
            }
            return Err(TransportError::Io("recv: no bytes within timeout".into()));
        }
        if let Some(app) = &self.logger {
            kline_log::rx(app, &buf);
        }
        Ok(buf)
    }
}
