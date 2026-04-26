//! mza-tuner-rs
//!
//! Rust port of MZA-TUNER (Honda Keihin/Shinden ECU flasher), based on the
//! deobfuscated C#/.NET source under `MzaTunerClone_Final/`.
//!
//! ## Status
//!
//! | Module          | Status         | Source-of-truth                              |
//! |-----------------|----------------|----------------------------------------------|
//! | [`crypto`]      | Working        | `Form3_AcgEcuToBin.cs`                       |
//! | [`xdf`]         | Working        | `Form4_XdfAdxBreaker.cs`                     |
//! | [`ecu_db`]      | Working        | `C:\MZATUNER\data.ini`                       |
//! | [`kline`]       | Skeleton       | `Form_EepromTool.cs`, `MainForm` (`⁚.2.cs`)  |
//! | [`ftdi`]        | Skeleton (feature `ftdi`) | `Type_44` in `⁎.2.cs`             |
//!
//! ## Quick example
//!
//! ```no_run
//! use mza_tuner::crypto::{decrypt_ecu_file, EcuVariant};
//! use std::fs;
//!
//! let cipher = fs::read_to_string("foo.ECU")?;
//! let plain  = decrypt_ecu_file(&cipher, EcuVariant::Keihin, None)?;
//! fs::write("foo.bin", plain)?;
//! # Ok::<_, anyhow::Error>(())
//! ```

#![warn(rust_2018_idioms, missing_docs)]

pub mod crypto;

pub mod ecu_db;
pub mod kline;
pub mod xdf;

#[cfg(feature = "ftdi")]
pub mod ftdi;

/// Library version string baked in at build time.
pub const VERSION: &str = env!("CARGO_PKG_VERSION");
