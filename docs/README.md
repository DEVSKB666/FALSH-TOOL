# MZA_TUNER_FLASH_2026 — Deobfuscated Source

โครงการนี้เป็น **decompiled + deobfuscated source** ของไฟล์ `MZA_TUNER_FLASH_2026.exe`
(พบที่ `C:\MZATUNER\MZA_TUNER_FLASH_2026.exe`, ขนาด 1.5 MB)

> ⚠️ **เกี่ยวกับลิขสิทธิ์**: โปรแกรมต้นฉบับมี copyright notice ระบุชัด และเป็น repack ของ
> `Honda_Flash_Tools.Net` (ผลงานของชาวอินโดนีเซีย) อีกต่อหนึ่ง นำไป
> redistribute หรือใช้ใน production เพื่อ เข้าใจ protocol

---

## โปรแกรมต้นฉบับทำอะไร

**MZA-TUNER** คือ **Honda ECU flasher / remapper** สำหรับมอเตอร์ไซค์ Honda
รองรับกล่อง ECU 2 ตระกูล:

| ตระกูล      | ย่อ | ตัวอย่าง                                          |
| ----------- | --- | ------------------------------------------------- |
| **Keihin**  | Kh  | PCX, Click, ADV, Lead, Zoomer-X, Wave, Scoopy ... |
| **Shinden** | Sh  | PCX160 NOABS/ABS, ADV150/160, Click160 ...        |

ดูรายการ part code ทั้ง 246+23 รุ่นได้จาก `C:\MZATUNER\data.ini`

### ฟีเจอร์หลัก

- **READ EEPROM** (Kh, Sh)
- **WRITE / FORMAT EEPROM** (`0x00`, `0xFF`)
- **ลบจำนวนการอัด** (reset flash counter — Kh, Sh)
- **ดูดไฟล์กล่อง 48K / 64K** (full ROM dump)
- **แปลงไฟล์ `.ACG / .ECU` → `.BIN`** (Form3 — Rijndael+MD5+ECB+Base64)
- **ปลดรหัส `.XDF / .ADX`** (Form4 — XDF/ADX PASSWORD BREAKER, ผลงาน "RED_MATRIX")
- **คลาวด์แชร์ริ่ง** ผ่าน `https://mza-tuner.site/`
- **License key + HWID blacklist** (`MZA_SECRET_2026_PRO`)

### ที่มา

| สิ่ง | รายละเอียด |
| ------------------ | --------------------------------------------------------------- | |
| Author signature | `By.ช่างลิงกล่องซิ่ง` (ใน `C:\MZATUNER\data\`) |
| Facebook page | `facebook.com/profile.php?id=100086932872601` |
| Upstream | `Honda_Flash_Tools.Net` (อินโดนีเซีย) — ฝังเป็น resource ใน exe |
| Comments ภาษาอินโด | `Input tidak boleh kosong`, `Data tidak cukup...` |

ดังนั้น MZA-TUNER = repack ของ Honda_Flash_Tools.Net + UI ภาษาไทย + branding + License system

---

## โครงสร้างโฟลเดอร์

```
MAZA/
├── tools/                              # สคริปต์ deobfuscation (PowerShell)
│   ├── extract_strings.ps1             # ดึง string ผ่าน reflection (1000 strings)
│   ├── rewrite_source.ps1              # แทน method calls → string literal
│   ├── dump_metadata.ps1               # dump types/methods/fields
│   ├── apply_renames_v2.ps1            # rename Unicode identifiers
│   ├── generate_class_index.ps1        # gen CLASS_INDEX.md
│   ├── strings.json                    # 1000 decoded strings
│   ├── strings.txt                     # อ่านง่าย (sorted by codepoints)
│   ├── metadata.json                   # type/member metadata
│   └── rename_map.json                 # old name → new name
│
├── MZA_TUNER_FLASH_2026/                  # ★ Source เดิม (จาก dnSpy)
├── MZA_TUNER_FLASH_2026.cs ... .51.cs      # ── เป็น Unicode-obfuscated
│
├── MzaTunerClone_Decompiled/           # ★ Strings แทนแล้ว (1568 replacements)
│                                       #   Method calls → string literal
│
└── MzaTunerClone_Renamed/              # ★ Renamed identifiers (this dir)
                                        #   Type_X / Form_X / M_X / f_X / Attr_X
                                        #   ดู CLASS_INDEX.md
```

> ไฟล์ shell scripts รันใหม่ได้ — ใช้ `powershell.exe -File tools\<script>.ps1`

---

## วิธีอ่าน source

ในไฟล์ `.cs` คุณจะเห็น:

| สิ่งที่เห็น                      | ความหมาย                                 |
| -------------------------------- | ---------------------------------------- |
| `Form_4`, `Form_8`, `Form_A` ฯลฯ | **Form ใน WinForms** — RID เป็นเลข hex   |
| `Type_NN`                        | คลาสปกติ (non-Form)                      |
| `Attr_NN`                        | Attribute class                          |
| `M_NN()`                         | Method (NN = method token RID hex)       |
| `f_NN`                           | Field (NN = field token RID hex)         |
| `Ctor`, `P_NN`, `Ev_NN`          | Constructor / Property / Event           |
| `\u00A0`, `\u1680` ฯลฯ           | identifier ที่ rename ไม่ได้ (collision) |

ถ้าเจอ `\u00A0` ที่ยังไม่ถูก rename — เปิด `CLASS_INDEX.md` แล้วหา **token RID**
ของ method/field นั้น (จาก dnSpy comment เช่น `// Token: 0x06000003 RID: 3`)

---

## โครงสร้างหลักของโปรแกรม

ดู `ARCHITECTURE.md` สำหรับรายละเอียดทั้งหมด ในที่นี้สรุปคร่าวๆ:

### Forms

| ไฟล์                                   | RID    | Class   | Form Title                   | หน้าที่                         |
| -------------------------------------- | ------ | ------- | ---------------------------- | ------------------------------- |
| `MZA_TUNER_FLASH_2026.3.cs`            | 4      | Form_4  | "แปลงไฟล์ .ACG&.ECUเป็น BIN" | ACG/ECU → BIN converter         |
| `MZA_TUNER_FLASH_2026.7.cs`            | 8      | Form_8  | "ADX & XDF ปลดรหัส"          | XDF/ADX password breaker        |
| `MZA_TUNER_FLASH_2026.8.cs`            | 9      | Form_9  | "รหัสเปิดXDFและADX"          | OpenPassword input dialog       |
| `MZA_TUNER_FLASH_2026.9.cs`            | 10 (A) | Form_A  | EEPROM Tool                  | READ/WRITE/FORMAT + ดูด 48K/64K |
| `MZA_TUNER_FLASH_2026.11.cs`           | ?      | ?       | License key UI               | "รหัสปลดล็อค (LICENSE KEY)"     |
| `MZA_TUNER_FLASH_2026/⁚.2.cs` (268 KB) | 245    | Form_F5 | **MainForm**                 | UI หลัก, K-Line stack           |

### FTDI / Hardware

- `MZA_TUNER_FLASH_2026/⁎.2.cs` — `Type_44` = P/Invoke wrappers (`FT_Open`, `FT_Read`, `FT_Write`, ฯลฯ)
- ใช้ `FTD2XX_NET.dll` (managed wrapper) สำหรับ high-level
- ใช้ `ftd2xx.dll` ตรงๆ (P/Invoke) สำหรับ low-level
- Init: 921600 baud → 10400 baud (Honda K-Line ISO 14230 KWP2000)
- 8N1 data, 50ms timeout, 8 latency, BitMode toggle (0/1)

### License / Anti-piracy

- Secret: `MZA_SECRET_2026_PRO`
- HWID check: `http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=<HWID>`
- AppData folder: `%APPDATA%\RemapMZA_Pro\`
- License entered via Form มีหน้าตา GUI สีดำ Consolas

### Cloud sharing

- Server: `https://mza-tuner.site/Sharefiles/` (fallback IP `82.26.104.124`)
- DataGrid columns: ลำดับ / ผู้จูน-สำนัก / รหัสกล่อง ECU / Action
- Web scrape: regex `href="([^"/]+)"`

### Hardcoded encryption keys

ใน `MZA_TUNER_FLASH_2026.3.cs`:

```csharp
// ASCII MD5 → AES-128-ECB → Base64
private string M_4 = "@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!";

// BigEndianUnicode MD5 → AES-128-ECB → Base64 → UTF7 decode
private string M_6 = "@Shinden@9919";
```

อัลกอริทึมเข้ารหัสไฟล์ `.ECU`:

1. Take password (ASCII bytes), MD5 → 16 bytes
2. Pad to 32 bytes (ECB does not need IV anyway)
3. AES-128-ECB encrypt UTF8 plaintext
4. Base64 encode → write `.ECU` file
5. Chunk size 43712 bytes per encrypted block

---

## Honda K-Line Protocol (จากที่อ่าน source)

จาก `MZA_TUNER_FLASH_2026.9.cs`:

### Init sequence (Keihin)

```
[FE 04 72 8C]            wakeup byte / address
[72 05 00 F0 99]          comm establishment
[91 91 0D DF 9E 8D 9A 86 90 8A 8C 9B 88]   start diag (Keihin signature)
[91 91 0D DF 92 9E 86 96 8B 8D 86 C0 6A]   ?
[91 91 07 40 00 00 00]    read ECM ID
```

### Init sequence (Shinden)

```
[FE 04 72 8C]            wakeup
[72 05 00 F0 99]          comm establishment
[27 0B E0 48 65 6C 6C 6F 48 6F 43]  start diag ("HelloHoC" - ASCII)
[27 0B E0 77 41 72 65 59 6F 75 22]  ?  ("wAreYou\"")
[7E 06 01 01 00 7A]       ?
[82 82 10 06 00 E6]       seed read?
```

### Read EEPROM byte-by-byte

ใช้ command `[82 82 10 06 <addr_lo> <chksum>]` วน 0..255 — ขนาด EEPROM = 256 bytes

---

## คำเตือน + ข้อจำกัดในการ build

โครงสร้าง source ที่ rename แล้ว **ยัง build ตรงๆไม่ได้** เพราะ:

1. **ชื่อ Type / Member ยังมีบาง `\u0XXX`** ที่ rename ไม่ได้ (collision)
2. **ชื่อไฟล์เป็น Unicode** เช่น `⁚.cs`, `‮.cs` — Windows path บางอย่างไม่ชอบ
3. **`<ApplicationManifest>app.manifest</ApplicationManifest>` ใน csproj** อ้างอิง path เดิม
4. **`HintPath` ของ FTD2XX_NET.dll** ชี้ไปที่ `..\..\..\..\..\MZATUNER\FTD2XX_NET.dll`
   ต้องแก้เป็น `C:\MZATUNER\FTD2XX_NET.dll` หรือ relative ที่ใช้ได้
5. **`[SuppressIldasm]` + module attr `[module: \u1680(11)]`** ต้องเอาออก
6. **โปรแกรมยังมี anti-debug** ฝังใน module class — ลบเองตามต้องการ

ถ้าจะ build ใหม่จริง — ดูคำแนะนำใน `ARCHITECTURE.md` หัวข้อ "Building"

---

## วิธี re-run ทุก step

```cmd
cd c:\Users\rovki\Downloads\MAZA

:: 1. ดึง strings ทั้งหมดจาก exe (1000 strings)
powershell.exe -ExecutionPolicy Bypass -File tools\extract_strings.ps1

:: 2. แทน method calls ด้วย string literal
powershell.exe -ExecutionPolicy Bypass -File tools\rewrite_source.ps1

:: 3. dump metadata ทั้งหมด
powershell.exe -ExecutionPolicy Bypass -File tools\dump_metadata.ps1

:: 4. apply rename (Type/Method/Field/Ctor)
powershell.exe -ExecutionPolicy Bypass -File tools\apply_renames_v2.ps1

:: 5. gen Markdown class index
powershell.exe -ExecutionPolicy Bypass -File tools\generate_class_index.ps1
```

ผลลัพธ์ใน `MzaTunerClone_Renamed/` พร้อม `CLASS_INDEX.md`

---

## เครดิต

- **Original** Honda_Flash_Tools.Net (Indonesia)
- **XDF/ADX breaker** ป้าย "RED_MATRIX" ในตัว Form4
- **Deobfuscation** ทำผ่าน .NET Reflection (PowerShell) — ไม่ใช้ de4dot
