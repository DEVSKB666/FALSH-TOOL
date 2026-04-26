//! Helpers for enumerating attached FTDI devices.
//!
//! There are two possible drivers on Windows:
//!
//!   * `libftd2xx` -> talks to FTDI's official D2XX driver
//!   * `rusb`      -> talks via libusb (works once Zadig has flipped the
//!                    driver to WinUSB or libusbK)
//!
//! Both lists are merged into a single [`FtdiDevice`] vector with a
//! `backend` field that tells us which transport to construct later.

use crate::commands::FtdiDevice;
use crate::libusb_ftdi;

pub fn list_devices() -> anyhow::Result<Vec<FtdiDevice>> {
    let mut out: Vec<FtdiDevice> = Vec::new();

    // ---- Backend #1: FTDI D2XX -----------------------------------
    match libftd2xx::list_devices() {
        Ok(infos) => {
            for (i, d) in infos.into_iter().enumerate() {
                out.push(FtdiDevice {
                    index: i as u32,
                    serial: d.serial_number,
                    description: d.description,
                    backend: "d2xx".to_string(),
                    daemon_backend: None,
                });
            }
        }
        Err(e) => {
            // D2XX failing usually just means "driver not bound to any
            // device on this PC" - we surface it as a debug log.
            eprintln!("[ftdi] D2XX list failed: {e}");
        }
    }

    // ---- Backend #2: libusb (WinUSB / libusbK) -------------------
    let lib_devs = libusb_ftdi::list();
    for (i, d) in lib_devs.into_iter().enumerate() {
        out.push(FtdiDevice {
            index: i as u32,
            serial: d.serial,
            description: format!("{} [libusb]", d.description),
            backend: "libusb".to_string(),
            daemon_backend: None,
        });
    }

    Ok(out)
}
