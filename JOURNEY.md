# Project Journey — บันทึกการพัฒนาทั้งหมด

ไฟล์นี้สรุปทุก step ของโปรเจกต์ เพื่อให้ผู้พัฒนา (รวมถึง AI assistant
รุ่นถัดไป) เปิดมาแล้ว **เข้าใจ context ทันที** โดยไม่ต้องอ่าน chat history

> **For future Cascade / AI sessions:** start by reading this file, then
> `README.md`, then `docs/ARCHITECTURE.md`. Together they describe the
> full state of the project.

---

## สิ่งที่ทำตั้งแต่ต้น (ตามลำดับเวลา)

### Stage 1 — Reverse engineering (Deobfuscation)

**Input:** `C:\MZATUNER\MZA_TUNER_FLASH_2026.exe` (1.5 MB, .NET 4.8.1
WinForms, obfuscated ด้วย **Obfuscar 1.0**)

**Steps ที่ทำ:**

1. ตรวจ obfuscation pattern → พบ string decryptor class
   `<PrivateImplementationDetails>{...}.EC2D41B1-A2F9-4664-90D8-86645EE2E753`
   ที่ใช้ XOR (`b ^= i ^ 0xAA`) บน byte blob 40,917 bytes
2. เขียน `tools/extract_strings.ps1` ที่โหลด .exe ผ่าน
   `[Reflection.Assembly]::LoadFile()` แล้ว invoke ทุก decoder method →
   ได้ **1000 strings** ใน `tools/strings.json`
3. เขียน `tools/rewrite_source.ps1` ที่ replace ทุก
   `EC2D41B1-...\uXXXX()` ใน .cs files ด้วย string literal → **1568
   replacements**
4. เขียน `tools/dump_metadata.ps1` ที่ dump ทุก type/member ด้วย
   reflection → **284 types, 2072 rename rows**
5. เขียน `tools/apply_renames_v2.ps1` ที่ position-aware rename ของ:
   - Types (ใน `class`/`new`/`:`/`<>`/`(cast)` positions)
   - Members (ใน `this.`/`base.` only — safest)
   - Ctor declarations (modifier + class-name + `(`)
   - **1106 member + 35 ctor + 3736 type replacements**
6. เขียน `tools/finalize.ps1` ที่ rename ไฟล์ตาม role (`Form3.cs` →
   `Form3_AcgEcuToBin.cs` etc) + patch `csproj`
7. Output อยู่ที่ `docs/decompiled/` + `docs/CLASS_INDEX.md` (322 KB)

**ค้นพบสำคัญ:**

- โปรแกรมจริงคือ **Honda Keihin/Shinden ECU flasher** (rebrand จาก
  Honda_Flash_Tools.Net ของชาวอินโดนีเซีย)
- Hardcoded passwords สำหรับ ECU file decrypt:
  - Keihin: `@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!`
  - Shinden: `@Shinden@9919`
- License algorithm:
  - HWID = `SHA512(WMI ProcessorId + BaseBoard.Serial + "SHARK_V8_PREMIUM_SYSTEM")[:40]`
  - Key = `SHA256(HWID + "MZA_SECRET_2026_PRO")[:20]` รูปแบบ `XXXX-XXXX-XXXX-XXXX-XXXX`
  - Storage: `%APPDATA%\RemapMZA_Pro\system.dat` (XOR + Base64)
  - Blacklist URL: `http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=`
- K-Line protocol:
  - 921600 baud bit-bang wakeup → 10400 baud (ISO 14230 KWP2000)
  - Keihin start-diag: `91 91 0D DF ...`
  - Shinden security access: `27 0B E0 48 65 6C 6C 6F 48 6F 43` ("HelloHoC")
  - Read EEPROM byte: Shinden uses `82 82 10 06 <i> <0xE6-i>`

### Stage 2 — Rust port (`core/`)

**Goal:** port logic หลักไป Rust crate ที่ standalone + tested

**Output:**

| File | สิ่งที่ทำ | Tests |
|---|---|---|
| `core/src/crypto.rs` | AES-256-ECB + MD5 + Base64 (Form3) | 6 ✅ |
| `core/src/ecu_db.rs` | data.ini parser | 1 ✅ |
| `core/src/kline.rs`  | K-Line/KWP2000 byte sequences | 2 ✅ |
| `core/src/main.rs`   | `mzactl` CLI (4 subcommands) | — |

**ผลลัพธ์:** **9 unit tests + 1 doc-test pass** — ตรวจสอบได้ผ่าน
`cargo test -p mza-tuner-rs`

### Stage 3 — Tauri GUI (`app/`)

**Stack:**

- **Tauri 2** (Rust + WebView2)
- **Next.js 14** App Router (static export)
- **Tailwind CSS** + custom shadcn-style primitives
- **Lucide React** icons (no emoji)
- **Framer Motion** animations
- **Font Prompt** (Thai-friendly Google Font)
- **Zustand** + persist middleware
- **libftd2xx** (Rust FTDI bindings)

**Pages:** `/` (Dashboard) · `/ecu` · `/files` · `/database` · `/settings`

**Theme:** 4 presets (light / dark / racing / neon)
**Background:** 5 presets (particles / circuit / scanlines / aurora / off)
**Splash:** Boot animation พร้อมข้อความ C# ต้นฉบับ (FTDI → License → K-Line)

**Tauri commands** (typed, ทั้ง backend Rust + frontend TypeScript):

- `app_info` · `list_ftdi` · `list_ecus`
- `decrypt_ecu_file` · `encrypt_ecu_file` · `read_eeprom`

**Honda protocol implementation** (`app/src-tauri/src/eeprom.rs`):

- `read_eeprom_keihin` — ส่ง 4 init frames + อ่าน 256 cells (ใช้ bytes 11/12 ของ response)
- `read_eeprom_shinden` — ส่ง 6 init frames + อ่าน 256 cells (ใช้ bytes 10/11)

### Stage 4 — Workspace consolidation (this folder)

ทุกอย่างมารวมที่ `C:\Users\rovki\Desktop\MzaTuner\` เป็น **Cargo
workspace** — `target/` shared, `Cargo.lock` shared, build เร็วขึ้น

---

## โครงสร้าง

```
MzaTuner/
├── Cargo.toml          workspace root
├── core/               Rust core (lib + CLI)
├── app/                Tauri 2 + Next.js GUI
└── docs/               deobf outputs + this journey
```

---

## How to resume work

```cmd
cd C:\Users\rovki\Desktop\MzaTuner

:: ทดสอบ Rust core ทำงานปกติ
cargo test -p mza-tuner-rs

:: เปิด GUI dev mode
cd app
npm install            (ครั้งแรก เท่านั้น)
npm run tauri:dev
```

ถ้า GUI ไม่ขึ้น:
1. เช็ค WebView2 runtime → https://developer.microsoft.com/microsoft-edge/webview2/
2. เช็ค Rust toolchain → `rustup update`
3. รัน `cargo clean` ใน workspace root → `cargo build`

---

## Pending tasks (ที่ยังไม่ทำ)

- [ ] ทดสอบกับ ECU จริงผ่าน FTDI (ต้องการ hardware)
- [ ] WRITE / FLASH operations (ตอนนี้รองรับแค่ READ)
- [ ] Seed/Key authentication algorithm (อยู่ใน `MainForm` ของ source ต้นฉบับ — ยังไม่ port)
- [ ] XDF/ADX password breaker (Form4)
- [ ] Cloud file sharing UI (`https://mza-tuner.site/Sharefiles/`)
- [ ] Progress events จาก Rust → frontend ระหว่างอ่าน EEPROM (ใช้ Tauri events)
- [ ] Persist settings ผ่าน `tauri-plugin-store` แทน `localStorage` ใน Tauri build จริง

---

## Where the original C# logic came from

ดู `docs/CLASS_INDEX.md` สำหรับ mapping ครบทุก class. สรุปคร่าว ๆ:

| Source file (decompiled, in `docs/decompiled/`) | Rust port |
|---|---|
| `Form3_AcgEcuToBin.cs`        | `core/src/crypto.rs` |
| `Form_EepromTool.cs`          | `app/src-tauri/src/eeprom.rs` |
| `MZA_TUNER_FLASH_2026/⁎.2.cs` (FTDI P/Invoke) | `app/src-tauri/src/transport.rs` |
| `MZA_TUNER_FLASH_2026/⁚.2.cs` (MainForm)      | splash messages, K-Line init |
| `License_Secrets.cs`          | (intentionally NOT ported - ลิขสิทธิ์เจ้าของ) |

---

## Original source location (read-only reference)

ของต้นฉบับ + intermediate artifacts ยังเก็บที่ `c:\Users\rovki\Downloads\MAZA\`
(ลบ build artifacts ไปแล้ว — เหลือแค่ source) เผื่อต้องเทียบ ก็เปิดดูได้

---

_Last updated: 2026-04-26 by Cascade (Claude / Anthropic)_
