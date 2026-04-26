# mza-tuner-rs — Rust port of MZA-TUNER

> Port of the deobfuscated **`MZA_TUNER_FLASH_2026.exe`** (Honda Keihin/Shinden
> ECU flasher) from C#/.NET WinForms to Rust.

## ทำไมถึงใช้ Rust

| ปัจจัย | C# / .NET | Rust |
|---|---|---|
| Cross-platform | ❌ ผูกกับ Windows + .NET 4.8.1 | ✅ Win/Linux/Mac native |
| Real-time K-Line timing | ⚠️ GC pauses | ✅ ไม่มี GC |
| Binary size | 1.5 MB + .NET runtime ~200 MB | ~3 MB (ตัวเดียวจบ) |
| Memory safety | ✅ | ✅ + zero-cost abstractions |
| Hardware access | P/Invoke ftd2xx.dll | `libftd2xx` crate (cross-platform) |
| Obfuscation นรก | ✅ มีปัญหานี้กับเรา | ❌ source ที่เขียนใหม่ ไม่ obfuscate |

## สถานะ

| โมดูล | สถานะ | C# ต้นฉบับ |
|---|---|---|
| `crypto` (AES-256-ECB+MD5) | ✅ **working + tested** | `Form3_AcgEcuToBin.cs` |
| `ecu_db` (data.ini parser) | ✅ **working + tested** | `C:\MZATUNER\data.ini` |
| `kline` (protocol bytes) | 📝 skeleton + constants | `Form_EepromTool.cs` |
| `ftdi` (hardware) | 🚧 ยังไม่เขียน (feature `ftdi`) | `Type_44` ใน `⁎.2.cs` |
| `ui` (GUI) | 🚧 ยังไม่เขียน (แนะนำ `egui`) | MainForm + Form3..5 |
| License key gen | ❌ **ไม่ port** (ลิขสิทธิ์เจ้าของ) | `License_Secrets.cs` |

## Quick start

```cmd
:: ติดตั้ง Rust (ถ้ายังไม่มี)
:: https://rustup.rs/  หรือ  winget install Rustlang.Rustup

cd c:\Users\rovki\Downloads\MAZA\mza-tuner-rs

cargo test                         :: รัน unit-tests (ครอบ crypto + parser)
cargo build --release              :: build mzactl.exe
.\target\release\mzactl --help

:: รายการ ECU จาก data.ini
.\target\release\mzactl list-ecus --filter PCX

:: หา ECM-id
.\target\release\mzactl find-ecm 0104080F01

:: ถอดรหัสไฟล์ .ECU -> .BIN  (ใช้ password ที่ฝังใน exe เดิม)
.\target\release\mzactl decrypt-ecu --input pcx.ECU --output pcx.bin --variant keihin

:: เข้ารหัสกลับเป็น .ECU
.\target\release\mzactl encrypt-ecu --input pcx.bin --output pcx.ECU --variant keihin
```

## โครงสร้างไฟล์

```
mza-tuner-rs/
├── Cargo.toml
├── README.md
├── src/
│   ├── lib.rs           re-exports + module tree
│   ├── main.rs          mzactl CLI
│   ├── crypto.rs        Form3 algorithm: AES-256-ECB+MD5+Base64
│   ├── ecu_db.rs        data.ini parser
│   └── kline.rs         Honda K-Line/KWP2000 byte sequences
└── tests/               (auto-discovered by cargo test)
```

## Mapping ของ symbol สำคัญ

| C# class / method | Rust ที่ port มาแล้ว |
|---|---|
| `Form3.M_4(string,string)` (Keihin decrypt) | `crypto::decrypt_ecu_file(..., EcuVariant::Keihin, ..)` |
| `Form3.M_6(string,string)` (Shinden decrypt) | `crypto::decrypt_ecu_file(..., EcuVariant::Shinden, ..)` |
| `Form3.M_4 = "@ecu_homdaa..."` | `crypto::passwords::KEIHIN` |
| `Form3.M_6 = "@Shinden@9919"` | `crypto::passwords::SHINDEN` |
| INI sections `khPartCode/khEcmId/khStartOffset/khCksumOffset` | `ecu_db::EcuDatabase::keihin` |
| `Form_EepromTool` ComboBox commands | `kline::EepromOp` |
| `[91 91 0D DF ...]` Keihin start-diag | `kline::keihin::START_DIAG_1/2` |
| `[27 0B E0 48 65 6C 6C 6F 48 6F 43]` Shinden auth | `kline::shinden::AUTH_HELLO_HOC` |
| `array8[4]=i; array8[5]=0xE6-i` (read loop) | `kline::shinden::read_eeprom_frame(i)` |

## Roadmap (ขั้นต่อไป)

### 1. FTDI hardware layer (`src/ftdi.rs`)

```toml
# Cargo.toml feature flag
[features]
ftdi = ["dep:libftd2xx"]
```

ใช้ `libftd2xx` crate (cross-platform - Windows/Linux/Mac):

```rust
use libftd2xx::{Ftdi, FtdiCommon, BitMode};

let mut ft = Ftdi::new()?;
ft.set_baud_rate(921_600)?;
ft.set_bit_mode(0xFF, BitMode::Reset)?;
// ... wakeup sequence ...
ft.set_baud_rate(10_400)?;
```

### 2. K-Line transport (`src/kline_transport.rs`)

ฟังก์ชันส่ง/รับเฟรม + checksum + ISO 14230 framing:

```rust
pub trait KLine {
    fn send(&mut self, frame: &[u8]) -> Result<()>;
    fn recv(&mut self, timeout: Duration) -> Result<Vec<u8>>;
}
```

### 3. ECU operations (`src/eeprom.rs`)

```rust
pub fn read_eeprom_keihin(t: &mut impl KLine) -> Result<[u8; 256]> { ... }
pub fn read_eeprom_shinden(t: &mut impl KLine) -> Result<[u8; 256]> { ... }
pub fn dump_rom_48k(t: &mut impl KLine, mut on_progress: impl FnMut(usize)) -> Result<Vec<u8>> { ... }
pub fn dump_rom_64k(t: &mut impl KLine, mut on_progress: impl FnMut(usize)) -> Result<Vec<u8>> { ... }
```

### 4. GUI (`src/bin/mza-tuner-gui.rs`)

แนะนำ **egui** (immediate-mode, single binary, ใช้ `eframe`):

```toml
[dependencies]
eframe = "0.27"
```

```rust
fn main() -> eframe::Result<()> {
    eframe::run_native("MZA-TUNER", Default::default(),
        Box::new(|cc| Box::new(MzaApp::new(cc))))
}
```

## คำเตือน

- ✋ ห้ามใช้ port นี้ละเมิดลิขสิทธิ์ของ Honda_Flash_Tools.Net (ต้นฉบับ) หรือ
  MZA-TUNER (เจ้าของในไทย)
- การเขียน flash ลง ECU จริงเสียเสี่ยงพังกล่อง — ทดสอบกับ EEPROM dump file ก่อน
- License/HWID gen ของเดิมอยู่ใน `License_Secrets.cs` — ผม **ไม่ port** เข้าโครงการนี้
