//! Tauri `#[tauri::command]` wrappers exposed to the frontend (see
//! `src/lib/tauri.ts` for the matching TS types).
//!
//! Most of the heavy lifting is delegated to `mza_tuner` (the standalone
//! Rust library next to this app) so the frontend, the CLI tool and tests
//! all share the same implementation.

use serde::{Deserialize, Serialize};
use std::path::PathBuf;
use std::time::Instant;
use tauri::AppHandle;

use crate::bridge_client;
use crate::eeprom;
use crate::ftdi;
use crate::kline_log;
use crate::libusb_ftdi::LibusbKLine;
use crate::livedata as livedata_mod;
use crate::transport::{FtdiKLine, KLine};
use mza_tuner::crypto::{decrypt_ecu_file as core_decrypt, encrypt_ecu_file as core_encrypt, EcuVariant};
use mza_tuner::ecu_db::{EcuDatabase, Family};
use mza_tuner::xdf::{break_xdf_adx as core_break_xdf, XdfVariant};

// ---- types shared with the frontend ----------------------------------

#[derive(Serialize, Clone)]
pub struct AppInfo {
    pub version: String,
    pub rust_version: String,
    pub tauri_version: String,
}

#[derive(Serialize, Clone)]
pub struct FtdiDevice {
    pub index: u32,
    pub serial: String,
    pub description: String,
    /// Which Rust transport will be used to *open* this entry from the
    /// frontend's perspective: `"d2xx"`, `"libusb"`, or `"bridge"`.
    /// Local devices use the first two; bridge devices always set this
    /// to `"bridge"` so the frontend knows to route through the
    /// `*_via_bridge` family of commands.
    pub backend: String,
    /// **Bridge ports only** - the daemon-side transport that owns the
    /// physical FTDI on the remote machine (`"d2xx"` or `"libusb"`).
    /// Echoed back to the daemon when calling `read_eeprom_via_bridge`
    /// so the right driver is opened. Always `None` for local entries.
    #[serde(skip_serializing_if = "Option::is_none")]
    pub daemon_backend: Option<String>,
}

#[derive(Serialize, Clone)]
pub struct EcuEntry {
    pub id: String,
    pub family: String,
    pub part_code: String,
    pub ecm_id: String,
    pub start_offset: u32,
    pub cksum_offset: u32,
}

#[derive(Serialize, Clone)]
pub struct EepromReadResult {
    pub family: String,
    pub bytes: Vec<u8>,
    pub ecm_id: Option<String>,
    pub duration_ms: u64,
    pub log: Vec<String>,
}

#[derive(Deserialize)]
#[serde(rename_all = "PascalCase")]
pub enum VariantArg {
    Keihin,
    Shinden,
}
impl From<VariantArg> for EcuVariant {
    fn from(v: VariantArg) -> Self {
        match v {
            VariantArg::Keihin => EcuVariant::Keihin,
            VariantArg::Shinden => EcuVariant::Shinden,
        }
    }
}

// ---- commands --------------------------------------------------------

#[tauri::command]
pub fn app_info() -> AppInfo {
    AppInfo {
        version: env!("CARGO_PKG_VERSION").to_string(),
        rust_version: format!("rustc {}", option_env!("RUSTC_VERSION").unwrap_or("?")),
        tauri_version: tauri::VERSION.to_string(),
    }
}

#[tauri::command(async)]
pub fn list_ftdi() -> Result<Vec<FtdiDevice>, String> {
    ftdi::list_devices().map_err(|e| e.to_string())
}

#[tauri::command]
pub fn list_ecus(path: Option<PathBuf>) -> Result<Vec<EcuEntry>, String> {
    let p = ini_path(path);
    let db = EcuDatabase::load(&p)
        .map_err(|e| format!("loading {:?}: {e}", p))?;
    let mut out: Vec<EcuEntry> = Vec::with_capacity(db.keihin.len() + db.shinden.len());
    for (fam, e) in db.all() {
        out.push(EcuEntry {
            id: e.id.clone(),
            family: match fam {
                Family::Keihin => "Keihin".to_string(),
                Family::Shinden => "Shinden".to_string(),
            },
            part_code: e.part_code.clone(),
            ecm_id: e.ecm_id.clone(),
            start_offset: e.start_offset,
            cksum_offset: e.cksum_offset,
        });
    }
    Ok(out)
}

#[tauri::command]
pub fn decrypt_ecu_file(
    input: PathBuf,
    output: PathBuf,
    variant: VariantArg,
    password: Option<String>,
) -> Result<usize, String> {
    let txt = std::fs::read_to_string(&input).map_err(|e| e.to_string())?;
    let plain = core_decrypt(&txt, variant.into(), password.as_deref())
        .map_err(|e| e.to_string())?;
    std::fs::write(&output, &plain).map_err(|e| e.to_string())?;
    Ok(plain.len())
}

#[tauri::command]
pub fn encrypt_ecu_file(
    input: PathBuf,
    output: PathBuf,
    variant: VariantArg,
    password: Option<String>,
) -> Result<usize, String> {
    let bin = std::fs::read(&input).map_err(|e| e.to_string())?;
    let txt = core_encrypt(&bin, variant.into(), password.as_deref());
    std::fs::write(&output, &txt).map_err(|e| e.to_string())?;
    Ok(txt.len())
}

#[tauri::command(async)]
pub fn read_eeprom(
    app: AppHandle,
    variant: VariantArg,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<EepromReadResult, String> {
    let idx = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let core_variant: EcuVariant = match variant {
        VariantArg::Keihin => EcuVariant::Keihin,
        VariantArg::Shinden => EcuVariant::Shinden,
    };

    let started = Instant::now();
    let mut log = Vec::new();
    kline_log::info(&app, format!("--- EEPROM read started: {:?} via {} ---", core_variant, backend));

    // Pick the transport implementation based on which Windows driver is
    // currently bound to the device.
    let mut transport: Box<dyn KLine> = match backend.as_str() {
        "libusb" => Box::new(LibusbKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
        _        => Box::new(FtdiKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
    };

    let bytes = match core_variant {
        EcuVariant::Keihin => eeprom::read_eeprom_keihin(transport.as_mut(), &mut log, Some(&app))
            .map_err(|e| e.to_string())?,
        EcuVariant::Shinden => eeprom::read_eeprom_shinden(transport.as_mut(), &mut log, Some(&app))
            .map_err(|e| e.to_string())?,
    };

    let elapsed = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("--- EEPROM read complete: {} bytes in {} ms ---", bytes.len(), elapsed));

    Ok(EepromReadResult {
        family: match core_variant {
            EcuVariant::Keihin => "Keihin".to_string(),
            EcuVariant::Shinden => "Shinden".to_string(),
        },
        bytes,
        ecm_id: None, // could be extracted from the init response
        duration_ms: elapsed,
        log,
    })
}

/// Open a transport, run the K-Line wakeup + handshake, then send a
/// short Honda discovery frame so the cable's TX LED actually blinks
/// visibly and we can log whatever the ECU echoes back.
#[tauri::command(async)]
pub fn kline_test(
    app: AppHandle,
    device_index: Option<u32>,
    backend: Option<String>,
    probe: Option<bool>,
) -> Result<KlineTestResult, String> {
    let idx     = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let probe   = probe.unwrap_or(true);
    let started = Instant::now();
    let mut log = Vec::new();
    kline_log::info(&app, format!("--- Cable test on {} backend, index {} ---", backend, idx));

    let mut transport: Box<dyn KLine> = match backend.as_str() {
        "libusb" => Box::new(LibusbKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
        _        => Box::new(FtdiKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
    };
    let handshake_ms = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("[probe] handshake done in {} ms", handshake_ms));

    if probe {
        // Send the Keihin start-diag frame `FE 04 72 8C`. At 10400 baud
        // this is ~14 ms on the wire - long enough for the cable's TX LED
        // to blink visibly. If a real ECU is on the bus it will reply;
        // otherwise we just hit the recv timeout (which is also logged).
        // Helper: K-Line is single-wire, so the FTDI always reads its
        // own TX bytes back first. Anything beyond that is a real ECU
        // reply. We strip the echo before judging "silent vs answered".
        kline_log::info(&app, "[probe] TX Honda init frame (Keihin discovery)");
        let frame = [0xFEu8, 0x04, 0x72, 0x8C];
        match transport.round_trip(&frame, std::time::Duration::from_millis(400)) {
            Ok(resp) => {
                let echo_only = resp.len() <= frame.len() && resp.starts_with(&frame);
                let real_bytes = if resp.starts_with(&frame) { resp.len() - frame.len() } else { resp.len() };
                if echo_only || real_bytes == 0 {
                    kline_log::info(&app, "[probe] no ECU reply - only TX echo on bus (check key ON, K-Line pin, ground)");
                    log.push("probe: echo only, ECU silent".to_string());
                } else {
                    kline_log::info(&app, format!("[probe] ECU responded with {} byte(s) (after echo strip)", real_bytes));
                    log.push(format!("probe response: {} bytes", real_bytes));
                }
            }
            Err(e) => {
                kline_log::info(&app, format!("[probe] no reply ({}). Cable TX worked, but no ECU on bus.", e));
                log.push(format!("probe: {}", e));
            }
        }

        // Second probe - Shinden security access "Hello Honda" frame.
        kline_log::info(&app, "[probe] TX Shinden security access frame");
        let shinden = [0x27, 0x0B, 0xE0, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x48, 0x6F, 0x43];
        match transport.round_trip(&shinden, std::time::Duration::from_millis(400)) {
            Ok(resp) => {
                let echo_only = resp.len() <= shinden.len() && resp.starts_with(&shinden);
                let real_bytes = if resp.starts_with(&shinden) { resp.len() - shinden.len() } else { resp.len() };
                if echo_only || real_bytes == 0 {
                    kline_log::info(&app, "[probe] Shinden no reply - only TX echo on bus");
                } else {
                    kline_log::info(&app, format!("[probe] Shinden responded with {} byte(s) (after echo strip)", real_bytes));
                }
            }
            Err(e) => {
                kline_log::info(&app, format!("[probe] Shinden no reply ({})", e));
            }
        }
    }

    let elapsed = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("--- Cable test OK: total {} ms ---", elapsed));
    Ok(KlineTestResult {
        backend,
        index: idx,
        duration_ms: elapsed,
        log,
    })
}

#[derive(Serialize, Clone)]
pub struct KlineTestResult {
    pub backend: String,
    pub index: u32,
    pub duration_ms: u64,
    pub log: Vec<String>,
}

#[derive(Serialize, Clone)]
pub struct OperationResult {
    pub family: String,
    pub label: String,
    pub ok: bool,
    pub bytes: Option<Vec<u8>>,
    pub duration_ms: u64,
    pub log: Vec<String>,
}

// Shared helper - opens the right transport based on the chosen backend.
fn open_transport(
    app: &AppHandle,
    idx: u32,
    backend: &str,
    log: &mut Vec<String>,
) -> Result<Box<dyn KLine>, String> {
    match backend {
        "libusb" => Ok(Box::new(LibusbKLine::open(idx, log, Some(app.clone())).map_err(|e| e.to_string())?)),
        _        => Ok(Box::new(FtdiKLine::open(idx, log, Some(app.clone())).map_err(|e| e.to_string())?)),
    }
}

/// Reset the Honda flash counter (Keihin or Shinden).
///
/// On Keihin this is a one-shot 3-frame conversation. On Shinden the ECU
/// requires us to sweep 0..127 sub-addresses until it acknowledges.
#[tauri::command(async)]
pub fn reset_flash_count(
    app: AppHandle,
    variant: VariantArg,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<OperationResult, String> {
    let idx = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let core_variant: EcuVariant = match variant {
        VariantArg::Keihin => EcuVariant::Keihin,
        VariantArg::Shinden => EcuVariant::Shinden,
    };
    let started = Instant::now();
    let mut log = Vec::new();
    let label = match core_variant {
        EcuVariant::Keihin => "Reset Flash Count (Keihin)",
        EcuVariant::Shinden => "Reset Flash Count (Shinden)",
    };
    kline_log::info(&app, format!("--- {} via {} ---", label, backend));

    let mut transport = open_transport(&app, idx, &backend, &mut log)?;
    let ok = match core_variant {
        EcuVariant::Keihin => {
            eeprom::reset_flash_count_keihin(transport.as_mut(), &mut log).map_err(|e| e.to_string())?;
            true
        }
        EcuVariant::Shinden => {
            eeprom::reset_flash_count_shinden(transport.as_mut(), &mut log, Some(&app)).map_err(|e| e.to_string())?
        }
    };
    let elapsed = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("--- {} {} in {} ms ---", label, if ok { "OK" } else { "NO ACK" }, elapsed));
    Ok(OperationResult {
        family: format!("{:?}", core_variant),
        label: label.to_string(),
        ok,
        bytes: None,
        duration_ms: elapsed,
        log,
    })
}

/// **DESTRUCTIVE** - format the Shinden EEPROM by filling every cell with
/// `0x00` or `0xFF`. Caller must show a confirmation prompt to the user.
#[tauri::command(async)]
pub fn format_eeprom(
    app: AppHandle,
    fill: u8,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<OperationResult, String> {
    let idx = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();
    let label = format!("Format EEPROM 0x{:02X}", fill);
    kline_log::info(&app, format!("--- {} via {} ---", label, backend));
    kline_log::info(&app, "WARNING: this is a destructive operation");

    let mut transport = open_transport(&app, idx, &backend, &mut log)?;
    let ok = eeprom::format_eeprom_shinden(transport.as_mut(), &mut log, fill).map_err(|e| e.to_string())?;
    let elapsed = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("--- {} {} in {} ms ---", label, if ok { "OK" } else { "NO ACK" }, elapsed));
    Ok(OperationResult {
        family: "Shinden".to_string(),
        label,
        ok,
        bytes: None,
        duration_ms: elapsed,
        log,
    })
}

// ---- Remote bridge ---------------------------------------------------

/// Liveness check against a remote `loy-bridge` daemon. Returns `true`
/// if the bridge replies to `ping` within 5 seconds.
#[tauri::command(async)]
pub fn bridge_ping(url: String) -> Result<bool, String> {
    bridge_client::ping(&url).map(|_| true).map_err(|e| e.to_string())
}

/// List FTDI ports visible to a remote `loy-bridge` daemon. Returns
/// the same `FtdiDevice[]` shape the local `list_ftdi` produces, with
/// `backend = "bridge"` so the frontend can tell the source apart.
/// The daemon-side transport tag (`d2xx` / `libusb`) is preserved in
/// `daemon_backend` so the frontend can echo it back when reading.
#[tauri::command(async)]
pub fn list_ftdi_via_bridge(url: String) -> Result<Vec<FtdiDevice>, String> {
    let ports = bridge_client::list_ports(&url).map_err(|e| e.to_string())?;
    Ok(ports
        .into_iter()
        .map(|p| FtdiDevice {
            index: p.index,
            serial: p.serial,
            // Make the daemon-side driver visible in the dropdown so
            // the operator can see at a glance whether the bridge is
            // talking through D2XX or libusbK.
            description: format!("{} [{}]", p.description, p.backend.to_uppercase()),
            backend: "bridge".to_string(),
            daemon_backend: Some(p.backend),
        })
        .collect())
}

/// Read 256 EEPROM cells via a remote `loy-bridge` daemon. Same return
/// shape as the local `read_eeprom`. `daemon_backend` selects which
/// transport the daemon uses to open the device; defaults to `"d2xx"`.
#[tauri::command(async)]
pub fn read_eeprom_via_bridge(
    app: AppHandle,
    url: String,
    variant: VariantArg,
    device_index: Option<u32>,
    daemon_backend: Option<String>,
) -> Result<EepromReadResult, String> {
    let started = Instant::now();
    let v_label = match variant {
        VariantArg::Keihin  => "keihin",
        VariantArg::Shinden => "shinden",
    };
    let family_pretty = match variant {
        VariantArg::Keihin  => "Keihin".to_string(),
        VariantArg::Shinden => "Shinden".to_string(),
    };
    let backend = daemon_backend.unwrap_or_else(|| "d2xx".to_string());

    kline_log::info(&app, format!("--- EEPROM read via bridge: {v_label} @ {url} (daemon backend: {backend}) ---"));

    let res = bridge_client::read_eeprom(&url, v_label, device_index.unwrap_or(0), &backend)
        .map_err(|e| e.to_string())?;

    // Mirror every line the bridge logged into our streaming console
    // so the user sees a consistent UI no matter where the work runs.
    for line in &res.log {
        kline_log::info(&app, line.clone());
    }
    kline_log::info(
        &app,
        format!(
            "--- EEPROM read via bridge complete: {} bytes in {} ms ---",
            res.bytes.len(),
            started.elapsed().as_millis()
        ),
    );

    Ok(EepromReadResult {
        family: family_pretty,
        bytes: res.bytes,
        ecm_id: None,
        duration_ms: res.duration_ms,
        log: res.log,
    })
}

// ---- Live data poll (TyN Shop K-Line protocol) -----------------------

#[derive(Serialize, Clone)]
pub struct LiveSampleDto {
    /// 29-byte response to TABLE_16 (or empty if ECU silent).
    pub table16:     Vec<u8>,
    /// 13-byte response to TABLE_20 (or empty if ECU silent).
    pub table20:     Vec<u8>,
    /// End-to-end duration in ms.
    pub duration_ms: u64,
}

/// One live-data sample. Opens the FTDI port, sends K_CONNECT +
/// TABLE_16 + TABLE_20, returns the raw responses for the frontend
/// to parse with the TyN Shop equations.
#[tauri::command(async)]
pub fn read_live_sample(
    app: AppHandle,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<LiveSampleDto, String> {
    let idx     = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();

    let mut transport: Box<dyn KLine> = match backend.as_str() {
        "libusb" => Box::new(LibusbKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
        _        => Box::new(FtdiKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
    };

    let (t16, t20) = livedata_mod::poll_once(transport.as_mut(), &mut log)
        .map_err(|e| e.to_string())?;
    // NOTE: We deliberately do NOT send K_DISCONNECT here. The TyN
    // `2C 64 73 23` frame is not part of the Honda KWP spec - sending it
    // confuses stock ECUs and they go silent on the next poll. Just
    // closing the FTDI transport (when this scope ends) is enough; the
    // ECU's KWP session will time out naturally after ~2 s of silence
    // and the next poll's WAKEUP/ESTABLISH will re-establish it.

    Ok(LiveSampleDto {
        table16:     t16,
        table20:     t20,
        duration_ms: started.elapsed().as_millis() as u64,
    })
}

#[derive(Serialize, Clone)]
pub struct EcmIdDto {
    /// Whole hex-encoded ESTABLISH reply (after TX echo strip). Useful
    /// for debugging the on-wire bytes.
    pub raw_hex:     String,
    /// 5-byte slice that *probably* holds the Honda ECM signature.
    /// The exact offset varies between Keihin and Shinden families;
    /// the helper picks the first 5 bytes that look like a printable
    /// non-zero hex string. Empty if the ECU was silent.
    pub ecm_id:      Option<String>,
    /// End-to-end duration in ms.
    pub duration_ms: u64,
}

/// Read the Honda ECM ID off the bench. Sends WAKEUP + ESTABLISH and
/// hex-encodes the reply so the UI can display it under the "ECM ID"
/// row on the Home page.
#[tauri::command(async)]
pub fn read_ecm_id(
    app: AppHandle,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<EcmIdDto, String> {
    let idx     = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();

    let mut transport: Box<dyn KLine> = match backend.as_str() {
        "libusb" => Box::new(LibusbKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
        _        => Box::new(FtdiKLine::open(idx, &mut log, Some(app.clone())).map_err(|e| e.to_string())?),
    };

    let reply = livedata_mod::read_ecm_id(transport.as_mut(), &mut log)
        .map_err(|e| e.to_string())?;
    for line in &log {
        kline_log::info(&app, line.clone());
    }

    let raw_hex = reply
        .iter()
        .map(|b| format!("{:02X}", b))
        .collect::<Vec<_>>()
        .join(" ");

    // Heuristic: scan for the first run of 5 bytes that aren't all 0x00
    // / 0xFF padding - that's where Honda's ECU signature lives in the
    // ESTABLISH reply. The leading bytes are usually the protocol
    // header (0x72, length, service id) which we want to skip.
    let ecm_id = reply
        .windows(5)
        .skip(2)
        .find(|w| {
            let zeros = w.iter().filter(|b| **b == 0).count();
            let ones  = w.iter().filter(|b| **b == 0xFF).count();
            zeros < 3 && ones < 3
        })
        .map(|w| {
            w.iter()
                .map(|b| format!("{:02X}", b))
                .collect::<String>()
        });

    Ok(EcmIdDto {
        raw_hex,
        ecm_id,
        duration_ms: started.elapsed().as_millis() as u64,
    })
}

/// Read one live-data sample via a remote `loy-bridge` daemon. The
/// daemon opens its own FTDI, runs the Honda KWP wakeup + establish +
/// TABLE_17 + TABLE_20 sequence, and returns the raw (echo-stripped)
/// bytes. `daemon_backend` selects which transport the daemon opens
/// the device through (`"d2xx"` / `"libusb"`); defaults to `"d2xx"`.
#[tauri::command(async)]
pub fn read_live_sample_via_bridge(
    app: AppHandle,
    url: String,
    device_index: Option<u32>,
    daemon_backend: Option<String>,
) -> Result<LiveSampleDto, String> {
    let started = Instant::now();
    let backend = daemon_backend.unwrap_or_else(|| "d2xx".to_string());
    let res = bridge_client::read_live_sample(&url, device_index.unwrap_or(0), &backend)
        .map_err(|e| e.to_string())?;
    // Mirror every line the bridge logged into our streaming console
    // so the user sees a consistent UI no matter where the work runs.
    for line in &res.log {
        kline_log::info(&app, line.clone());
    }
    Ok(LiveSampleDto {
        table16:     res.table16,
        table20:     res.table20,
        duration_ms: started.elapsed().as_millis() as u64,
    })
}

// ---- ECU database CRUD ----------------------------------------------

fn family_from_str(s: &str) -> Result<Family, String> {
    match s {
        "Keihin"  => Ok(Family::Keihin),
        "Shinden" => Ok(Family::Shinden),
        other     => Err(format!("unknown family: {other}")),
    }
}

/// Resolve the path to `data.ini` for ECU-database commands.
///
/// Lookup order (first hit wins):
/// 1. Explicit `path` argument from the frontend.
/// 2. `MZA_DATA_INI` environment variable - lets ops override
///    without rebuilding (handy on Linux / macOS dev machines).
/// 3. `data.ini` next to the running executable.
/// 4. `data.ini` in the current working directory (the dev path -
///    `npm run tauri:dev` runs the binary from the repo root).
/// 5. Windows install location `C:\MZATUNER\data.ini` (the original
///    MZA_TUNER ships it there).
///
/// If nothing matches we fall back to the Windows path so the error
/// message stays consistent with the original tool.
fn ini_path(path: Option<PathBuf>) -> PathBuf {
    if let Some(p) = path {
        return p;
    }
    if let Ok(env) = std::env::var("MZA_DATA_INI") {
        let p = PathBuf::from(env);
        if p.exists() {
            return p;
        }
    }
    if let Ok(exe) = std::env::current_exe() {
        if let Some(dir) = exe.parent() {
            let p = dir.join("data.ini");
            if p.exists() {
                return p;
            }
        }
    }
    if let Ok(cwd) = std::env::current_dir() {
        // `cargo tauri dev` may launch the binary from `app/src-tauri`,
        // `app/`, or the repo root depending on the platform. Walk up
        // until we find a `data.ini` or we run out of parents - the
        // file is small enough that an existence check at each level
        // is cheap.
        let mut here: Option<&std::path::Path> = Some(&cwd);
        while let Some(dir) = here {
            let p = dir.join("data.ini");
            if p.exists() {
                return p;
            }
            here = dir.parent();
        }
    }
    PathBuf::from(r"C:\MZATUNER\data.ini")
}

/// Add a new ECU entry. Returns the auto-allocated `IDxxxx` id.
#[tauri::command]
pub fn add_ecu_entry(
    family: String,
    part_code: String,
    ecm_id: String,
    start_offset: u32,
    cksum_offset: u32,
    path: Option<PathBuf>,
) -> Result<String, String> {
    let p   = ini_path(path);
    let fam = family_from_str(&family)?;
    let mut db = EcuDatabase::load(&p).map_err(|e| e.to_string())?;
    let id = db.add(fam, &part_code, &ecm_id, start_offset, cksum_offset);
    db.save(&p).map_err(|e| e.to_string())?;
    Ok(id)
}

/// Update an existing ECU entry. Returns `true` if the entry existed.
#[tauri::command]
pub fn update_ecu_entry(
    family: String,
    id: String,
    part_code: String,
    ecm_id: String,
    start_offset: u32,
    cksum_offset: u32,
    path: Option<PathBuf>,
) -> Result<bool, String> {
    let p   = ini_path(path);
    let fam = family_from_str(&family)?;
    let mut db = EcuDatabase::load(&p).map_err(|e| e.to_string())?;
    let ok = db.update(fam, &id, &part_code, &ecm_id, start_offset, cksum_offset);
    if ok {
        db.save(&p).map_err(|e| e.to_string())?;
    }
    Ok(ok)
}

/// Delete an ECU entry. Returns `true` if it existed.
#[tauri::command]
pub fn delete_ecu_entry(
    family: String,
    id: String,
    path: Option<PathBuf>,
) -> Result<bool, String> {
    let p   = ini_path(path);
    let fam = family_from_str(&family)?;
    let mut db = EcuDatabase::load(&p).map_err(|e| e.to_string())?;
    let ok = db.delete(fam, &id);
    if ok {
        db.save(&p).map_err(|e| e.to_string())?;
    }
    Ok(ok)
}

// ---- XDF / ADX password breaker --------------------------------------

#[derive(Serialize, Clone)]
pub struct XdfBreakResultDto {
    pub variant:         String,
    pub open_password:   String,
    pub modify_password: String,
    pub plaintext:       String,
}

/// Break the password on an XDF / ADX file (port of `Form4` in the
/// original program). Returns the cleaned plaintext alongside the
/// recovered open + modify passwords.
#[tauri::command]
pub fn break_xdf_adx(
    file_bytes: Vec<u8>,
    filename: String,
    password: Option<String>,
) -> Result<XdfBreakResultDto, String> {
    let pwd_ref = password.as_deref();
    let res = core_break_xdf(&file_bytes, &filename, pwd_ref).map_err(|e| e.to_string())?;
    Ok(XdfBreakResultDto {
        variant: match res.variant {
            XdfVariant::Adx => "ADX".to_string(),
            XdfVariant::Xdf => "XDF".to_string(),
        },
        open_password:   res.open_password,
        modify_password: res.modify_password,
        plaintext:       res.plaintext,
    })
}

/// Dump the Shinden ROM. `size` selects either 48 KB ("48K dump", upper
/// half) or 32 KB ("64K dump", upper quarter - misnamed in the original).
#[tauri::command(async)]
pub fn dump_rom(
    app: AppHandle,
    size: String,
    device_index: Option<u32>,
    backend: Option<String>,
) -> Result<OperationResult, String> {
    let idx = device_index.unwrap_or(0);
    let backend = backend.unwrap_or_else(|| "d2xx".to_string());
    let started = Instant::now();
    let mut log = Vec::new();
    let label = format!("ROM Dump {}", size);
    kline_log::info(&app, format!("--- {} via {} ---", label, backend));

    let mut transport = open_transport(&app, idx, &backend, &mut log)?;
    let bytes = match size.as_str() {
        "48K" => eeprom::dump_rom_shinden_48k(transport.as_mut(), &mut log, Some(&app)).map_err(|e| e.to_string())?,
        "64K" => eeprom::dump_rom_shinden_64k(transport.as_mut(), &mut log, Some(&app)).map_err(|e| e.to_string())?,
        // EXPERIMENTAL - speculative byte[4] page sweep; see
        // `eeprom::dump_rom_shinden_256k_experimental` for caveats.
        "256K" => eeprom::dump_rom_shinden_256k_experimental(transport.as_mut(), &mut log, Some(&app)).map_err(|e| e.to_string())?,
        other => return Err(format!("unknown ROM dump size: {other} (use \"48K\", \"64K\" or \"256K\")")),
    };
    let elapsed = started.elapsed().as_millis() as u64;
    kline_log::info(&app, format!("--- {} done: {} bytes in {} ms ---", label, bytes.len(), elapsed));
    Ok(OperationResult {
        family: "Shinden".to_string(),
        label,
        ok: !bytes.is_empty(),
        bytes: Some(bytes),
        duration_ms: elapsed,
        log,
    })
}

/// Read the Honda ECM ID via a remote `loy-bridge` daemon. Same
/// return shape as the local `read_ecm_id`. `daemon_backend`
/// selects which transport the daemon uses to open the device
/// (defaults to `"d2xx"`).
#[tauri::command(async)]
pub fn read_ecm_id_via_bridge(
    app: AppHandle,
    url: String,
    device_index: Option<u32>,
    daemon_backend: Option<String>,
) -> Result<EcmIdDto, String> {
    let started = Instant::now();
    let backend = daemon_backend.unwrap_or_else(|| "d2xx".to_string());

    kline_log::info(
        &app,
        format!("--- read_ecm_id via bridge @ {} (daemon backend: {}) ---", url, backend),
    );

    let res = bridge_client::read_ecm_id(&url, device_index.unwrap_or(0), &backend)
        .map_err(|e| e.to_string())?;

    for line in &res.log {
        kline_log::info(&app, line.clone());
    }
    kline_log::info(
        &app,
        format!(
            "--- read_ecm_id via bridge done in {} ms (raw={}) ---",
            started.elapsed().as_millis(),
            res.raw_hex
        ),
    );

    Ok(EcmIdDto {
        raw_hex: res.raw_hex,
        ecm_id: res.ecm_id,
        duration_ms: res.duration_ms,
    })
}

/// ROM dump (Shinden) via a remote `loy-bridge` daemon. Same return
/// shape as the local `dump_rom`. `daemon_backend` selects which
/// transport the daemon uses to open the device (defaults to
/// `"d2xx"`).
#[tauri::command(async)]
pub fn dump_rom_via_bridge(
    app: AppHandle,
    url: String,
    size: String,
    device_index: Option<u32>,
    daemon_backend: Option<String>,
) -> Result<OperationResult, String> {
    let started = Instant::now();
    let backend = daemon_backend.unwrap_or_else(|| "d2xx".to_string());
    let label = format!("ROM Dump {}", size);

    kline_log::info(
        &app,
        format!("--- {} via bridge @ {} (daemon backend: {}) ---", label, url, backend),
    );

    let res = bridge_client::dump_rom(&url, &size, device_index.unwrap_or(0), &backend)
        .map_err(|e| e.to_string())?;

    // Mirror every line the bridge logged into our streaming console
    // so the user sees a consistent UI no matter where the work runs.
    for line in &res.log {
        kline_log::info(&app, line.clone());
    }
    kline_log::info(
        &app,
        format!(
            "--- {} via bridge complete: {} bytes in {} ms ---",
            label,
            res.bytes.len(),
            started.elapsed().as_millis()
        ),
    );

    let bytes_len = res.bytes.len();
    Ok(OperationResult {
        family: "Shinden".to_string(),
        label,
        ok: bytes_len > 0,
        bytes: Some(res.bytes),
        duration_ms: res.duration_ms,
        log: res.log,
    })
}
