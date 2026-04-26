# MZA-Tuner Workspace

โปรเจกต์รวม — Honda Keihin / Shinden ECU flasher ในรูปแบบ modern stack
(Rust core + Tauri 2 + Next.js GUI) — ทุกอย่างอยู่ใน workspace เดียว

## โครงสร้าง

```
MzaTuner/
├── Cargo.toml               ← Cargo workspace root (members = core + app/src-tauri)
├── README.md                ← (this file)
├── .gitignore
│
├── core/                    ★ Rust library + CLI (no hardware)
│   ├── Cargo.toml
│   ├── README.md
│   └── src/
│       ├── lib.rs
│       ├── main.rs          mzactl CLI
│       ├── crypto.rs        AES-256-ECB + MD5 (Form3 algorithm)
│       ├── ecu_db.rs        data.ini parser (269 ECUs)
│       └── kline.rs         Honda K-Line / KWP2000 byte sequences
│
├── app/                     ★ Tauri 2 + Next.js 14 GUI
│   ├── package.json         npm dependencies (Next, Tailwind, Lucide, ...)
│   ├── next.config.mjs
│   ├── tailwind.config.ts
│   ├── src/                 frontend (TypeScript, App Router)
│   │   ├── app/             pages: /, /ecu, /files, /database, /settings
│   │   ├── components/      splash, animated bg, theme, app-shell, ui/*
│   │   └── lib/             tauri.ts, settings.ts, utils.ts
│   ├── src-tauri/           Rust backend
│   │   ├── Cargo.toml       deps on `core` + libftd2xx
│   │   ├── tauri.conf.json
│   │   ├── icons/           generated (ico/icns/png)
│   │   └── src/             commands.rs, ftdi.rs, transport.rs, eeprom.rs
│   └── tools/make_icon.ps1  generate logo PNG via System.Drawing
│
└── docs/                    ★ Deobfuscation outputs + design notes
    ├── README.md            (was MzaTunerClone_Renamed/README.md)
    ├── ARCHITECTURE.md      protocol details, license algorithm
    ├── CLASS_INDEX.md       all 284 types + members from the original .exe
    ├── decompiled/          source after string + identifier rewrite
    └── deobf-tools/         PowerShell scripts that produced the above
        ├── extract_strings.ps1
        ├── rewrite_source.ps1
        ├── dump_metadata.ps1
        ├── apply_renames_v2.ps1
        ├── generate_class_index.ps1
        ├── finalize.ps1
        ├── strings.json     1000 decoded strings
        ├── metadata.json    284 types
        └── rename_map.json  2072 rename rows
```

## Prerequisites

- **Node.js 22+** + **npm 10+**
- **Rust 1.74+** (`rustup default stable`)
- **WebView2 Runtime** (มากับ Windows 11 อยู่แล้ว, Windows 10 อาจต้องลง)
- (Optional, สำหรับฮาร์ดแวร์) **FTDI D2XX driver** — https://ftdichip.com/drivers/d2xx-drivers/

## Quick start

```cmd
:: ครั้งแรก - ลง npm deps
cd C:\Users\rovki\Desktop\MzaTuner\app
npm install

:: รัน Tauri dev (เปิด native window พร้อม HMR)
npm run tauri:dev

:: build production .msi/.exe
npm run tauri:build
```

หรือถ้าจะใช้แค่ CLI / library ไม่เปิด GUI:

```cmd
cd C:\Users\rovki\Desktop\MzaTuner

:: รัน tests ของ core lib
cargo test -p mza-tuner-rs

:: รัน CLI
cargo run -p mza-tuner-rs --bin mzactl -- list-ecus --filter PCX
cargo run -p mza-tuner-rs --bin mzactl -- find-ecm 0104080F01
cargo run -p mza-tuner-rs --bin mzactl -- decrypt-ecu --input foo.ECU --output foo.bin --variant keihin
```

## Cargo workspace benefits

ทำ workspace = `target/` เดียวร่วมกัน →

- คอมไพล์ครั้งเดียว, dep ไม่ rebuild ซ้ำเวลา switch crate
- Cargo.lock เดียว → เวอร์ชัน deps สอดคล้องกันแน่ๆ
- รัน `cargo test --workspace` เพื่อ test ทุก crate รวด

## Status

| โมดูล               | สถานะ                                                                 |
| ------------------- | --------------------------------------------------------------------- |
| `core::crypto`      | ✅ tested - 6 tests pass (round-trip, key derivation, wrong password) |
| `core::ecu_db`      | ✅ tested - parses real `data.ini`                                    |
| `core::kline`       | ✅ constants + frame builders (256 frames test)                       |
| `core` CLI `mzactl` | ✅ 4 subcommands working                                              |
| `app` frontend      | ✅ 5 pages, 8 routes static-built                                     |
| `app` Tauri backend | ✅ `cargo check` clean                                                |
| `app` IPC commands  | ✅ 6 commands wired (typed end-to-end)                                |
| Hardware test       | ⏳ ต้องการ FTDI cable + ECU จริง                                      |

## Customizing your build

- เปลี่ยนชื่อแอป → `app/src-tauri/tauri.conf.json` → `productName`
- เปลี่ยนโลโก้ → ใช้ `app/tools/make_icon.ps1` หรือสร้าง 1024² PNG เอง แล้วรัน `npx tauri icon path/to/logo.png`
- เปลี่ยนข้อความ Splash → `app/src/components/splash-screen.tsx` ตัวแปร `STEPS`
- เพิ่ม Theme ใหม่ → `app/src/app/globals.css` (เพิ่ม class `.theme-XXX`) + `app/src/lib/settings.ts` (เพิ่มใน `ThemePreset`)
- เพิ่ม Background ใหม่ → `app/src/components/animated-background.tsx`

## Original sources reference

ถ้าจะดูว่า logic แต่ละอย่างมาจาก C# ตัวไหน:

- `docs/decompiled/Form3_AcgEcuToBin.cs` → `core/src/crypto.rs`
- `docs/decompiled/Form_EepromTool.cs` → `app/src-tauri/src/eeprom.rs`
- `docs/decompiled/MZA_TUNER_FLASH_2026/⁎.2.cs` (FTDI P/Invoke) → `app/src-tauri/src/transport.rs`
- `docs/decompiled/MZA_TUNER_FLASH_2026/⁚.2.cs` (MainForm) → splash messages

## Legal

**License generation algorithm** (HWID-based) อยู่ใน
`docs/ARCHITECTURE.md` เพื่อความโปร่งใสทางเทคนิค **ห้าม** สร้าง keygen
หรือเลี่ยง license
