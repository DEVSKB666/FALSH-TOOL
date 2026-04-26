# Architecture — MZA_TUNER_FLASH_2026

วิเคราะห์เชิงลึกของโครงสร้างภายในโปรแกรม จาก source ที่ deobfuscate แล้ว

---

## 1. ภาพรวมระดับสูง

```
┌─────────────────────────────────────────────────────────────┐
│                      MZA_TUNER_FLASH_2026.exe               │
│                  (.NET Framework 4.8.1 WinForms)            │
├─────────────────────────────────────────────────────────────┤
│  Obfuscar 1.0  +  Anti-ILDASM  +  Anti-debug                │
│  Math mutations  +  Modified managed entry-point            │
└─────────────────────────────────────────────────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
   ┌─────────┐          ┌─────────┐          ┌──────────┐
   │ MainForm│ ──────▶  │EEPROM   │──ftd2xx──▶│ Honda    │
   │ (Form_F5│          │Tool     │  K-Line   │ ECU      │
   │ ⁚.2.cs) │          │(Form_A) │  10400 bd │ (KH/Sh)  │
   └─────────┘          └─────────┘          └──────────┘
        │
        │
   ┌────┴────────────┬─────────────┬──────────────┐
   │                 │             │              │
   ▼                 ▼             ▼              ▼
 ┌─────┐         ┌─────┐       ┌─────┐        ┌─────┐
 │Form3│         │Form4│       │Form5│        │Cloud│
 │ACG/ │         │XDF/ │       │Lic. │        │File │
 │ECU  │         │ADX  │       │ Key │        │Share│
 │→BIN │         │unlock       └─────┘        └─────┘
 └─────┘         └─────┘
```

---

## 2. Forms

### MainForm — `MZA_TUNER_FLASH_2026/⁚.2.cs` (~268 KB)

**Class:** `Form_F5` (RID 245), namespace `Form_4` (collision หลังจาก rename — เพราะ namespace ของ form นี้ก็ชื่อ collision)

ฟีเจอร์:

- **Splash screen** ข้อความ:
  - `"> INITIALIZING MZA-TUNER 2026..."`
  - `"> SEARCHING FTDI INTERFACE..."`
  - `"> FTDI D2XX DRIVER DETECTED [OK]"`
  - `"> AUTHENTICATING LICENSE KEY..."`
  - `"> MEMORY OFFSET MAPPING: 0x8000-0xFFFF"`
  - `"> K-LINE PROTOCOL STACK LOADED"`
  - `"> WARMING UP ISO-9141 BUS... [OK]"`
- โหลด config:
  - `programName.dat` → `this.Text` (title)
  - `C:\MZATUNER\headerColor.dat` → header bg color
  - `C:\MZATUNER\Background.dat` → desktop background image (1.9 MB JPEG ใน .dat)
  - `C:\MZATUNER\buttonColors.dat` → ตั้งสี button ตามชื่อ
- "เปลี่ยนชื่อโปรแกรม" feature: prompt + เขียนกลับ `programName.dat`
- **Seed/Key authentication** กับ ECU:
  - "กำลังตรวจสอบ Seed/Key..."
  - On fail: "การตรวจสอบ ล้มเหลว Seed/Key" / "ฮอนด้า แฟลช"
- เปิด Facebook page ของ ช่างลิงกล่องซิ่ง: `https://www.facebook.com/profile.php?id=100086932872601`

### Form3 — `MZA_TUNER_FLASH_2026.3.cs`

**Class:** `Form_4`, Title: `"แปลงไฟล์ .ACG&.ECUเป็น BIN"`

ฟีเจอร์:

- **ACG TO BIN** button (`button2`)
- **HXD EDITOR** button (`button3`) → เปิด `C:\Program Files\HxD\HXD.exe`
- **ECU TO BIN** button (`button1`)
- 2 RichTextBox (input/output), 1 GroupBox

อัลกอริทึม decryption:

```csharp
// ASCII path
public static string Decrypt_ASCII(string base64Cipher, string password) {
    var aes = new RijndaelManaged();
    var md5 = new MD5CryptoServiceProvider();
    byte[] key = new byte[32];
    byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));   // 16B
    Array.Copy(hash, 0, key, 0, 16);
    Array.Copy(hash, 0, key, 15, 16);   // overlap by 1 byte at offset 15
    aes.Key  = key;
    aes.Mode = CipherMode.ECB;
    var dec  = aes.CreateDecryptor();
    byte[] ct = Convert.FromBase64String(base64Cipher);
    return Encoding.UTF8.GetString(dec.TransformFinalBlock(ct, 0, ct.Length));
}

// BigEndian-Unicode path (Shinden)
public string Decrypt_BEU(string base64Cipher, string password) {
    var hash = md5.ComputeHash(Encoding.BigEndianUnicode.GetBytes(password));
    Array.Copy(hash, 0, key, 6, 16);
    Array.Copy(hash, 0, key, 2, 16);
    // ... AES-ECB ... UTF7 decode (!?)
}
```

Hardcoded passwords ฝังใน fields (RID 2, RID 3):

```csharp
private string M_4 = "@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!";  // Keihin
private string M_6 = "@Shinden@9919";                                   // Shinden
```

Block size: **43712 bytes** ต่อ chunk encrypted

### Form4 — `MZA_TUNER_FLASH_2026.7.cs`

**Class:** `Form_8`, Title: `"ADX & XDF ปลดรหัส"`

UI ป้าย: `"◢ XDF/ADX PASSWORD BREAKER : RED_MATRIX ◣"`

อ่าน XDF/ADX file (XML-like):

```xml
<openpassword>...</openpassword>
<modifypassword>...</modifypassword>
<flags>0x...</flags>
```

ทำการ:

1. กด `◢ เลือกไฟล์ ◣` → OpenFileDialog
2. กรอก password ที่ Form_9 (`รหัสเปิดXDFและADX`)
3. กด `◢ ปลดรหัส ◣` → strip password, reset flags
4. กด `◢ เซฟไฟล์ ◣` → save back

### Form5 / OpenPassword — `MZA_TUNER_FLASH_2026.8.cs`

**Class:** `Form_9`, Title: `"รหัสเปิดXDFและADX"`

Password input dialog ใช้คู่กับ Form4. Cancel เป็น `MessageBox` "Enter password to open the file"

### EEPROM Tool — `MZA_TUNER_FLASH_2026.9.cs`

**Class:** `Form_A`, Combobox commands:

| Command              | ทำอะไร                    | Method |
| -------------------- | ------------------------- | ------ |
| `READ EEPROM KH`     | อ่าน EEPROM Keihin        | `M_46` |
| `READ EEPROM Sh`     | อ่าน EEPROM Shinden       | `M_46` |
| `ลบจำนวนการอัด Kh`   | reset flash count Keihin  | `M_47` |
| `ลบจำนวนการอัด Sh`   | reset flash count Shinden | `M_47` |
| `Format EEPROM 0x00` | format ทั้งหมดเป็น 0x00   | `M_4D` |
| `Format EEPROM 0xFF` | format ทั้งหมดเป็น 0xFF   | `M_44` |
| `ดูดไฟล์กล่อง 48K`   | full ROM dump 48 KB       | `M_50` |
| `ดูดไฟล์กล่อง 64K`   | full ROM dump 64 KB       | `M_4F` |

จะถามให้ "OFF สวิตซ์ และ ON ภายใน 3 วินาที และกด OK" ก่อนทำคำสั่ง EEPROM

แต่ละคำสั่งวิ่งใน background `Thread` (`IsBackground=true`)

Save dialog: `{Command} {dd-MM-yyyy}.bin`

---

## 3. ECU Communication Stack

### 3.1 Hardware (FTDI USB-to-Serial)

ผ่าน 2 ระดับ API:

| ไฟล์ใน source         | ระดับ      | API                        |
| --------------------- | ---------- | -------------------------- |
| `Type_44` ใน `⁎.2.cs` | Low-level  | P/Invoke `ftd2xx.dll`      |
| `Form_A` (`.9.cs`)    | High-level | `FTD2XX_NET.dll` (managed) |

DLL Imports (low-level):

```csharp
[DllImport("ftd2xx.dll", EntryPoint = "FT_Open")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_Close")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_Read")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_Write")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_GetQueueStatus")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_SetBaudRate")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_SetDataCharacteristics")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_Purge")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_SetTimeouts")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_SetLatencyTimer")]
[DllImport("ftd2xx.dll", EntryPoint = "FT_SetBitMode")]
```

### 3.2 K-Line Initialization Sequence

```
1. FT_Open(0)
2. FT_Purge(3 = TX+RX)
3. FT_SetBitMode(0, 0)            ; reset
4. FT_SetDataCharacteristics(8N1)
5. FT_SetBaudRate(921600)         ; high-speed temp
6. FT_SetTimeouts(50, 0)
7. FT_SetLatencyTimer(8)
8. FT_SetBitMode(1, 1)            ; bit-bang for K-Line wakeup
9. FT_Write(<wakeup byte 0xC8 / 0x33>)
10. Sleep(70ms)
11. FT_Write(<address byte>)
12. FT_SetBitMode(0, 0)            ; back to UART
13. FT_SetBaudRate(10400)          ; ISO 14230 K-Line
14. FT_Purge(3)
```

ที่เหลือเป็น KWP2000 / ISO 14230 frames

### 3.3 Honda Keihin Protocol Frames

Init (จากทำงานบน `Form_A`):

```
1. [FE 04 72 8C]                               wakeup, address byte
2. [72 05 00 F0 99]                            establishment
3. [91 91 0D DF 9E 8D 9A 86 90 8A 8C 9B 88]    start diagnostic
4. [91 91 0D DF 92 9E 86 96 8B 8D 86 C0 6A]    follow-up
```

Read EEPROM byte (วน 0..255):

```
TX: [91 91 07 40 00 00 00]  ; 7-byte read command
RX: [..  ..  ..  ..  ..  ..  ..  ..  ..  ..  ..  ID  CHKSUM]
                                                  ^^  ^^^^^^
                                       byte at indices 11,12 → data byte
```

### 3.4 Honda Shinden Protocol Frames

```
1. [FE 04 72 8C]                                 wakeup
2. [72 05 00 F0 99]                              establishment
3. [27 0B E0 48 65 6C 6C 6F 48 6F 43]            "HelloHoC" (ASCII auth)
4. [27 0B E0 77 41 72 65 59 6F 75 22]            "wAreYou\""
5. [7E 06 01 01 00 7A]                           seed get?
6. [82 82 10 06 00 E6]                           start address read
```

Read byte loop: TX `[82 82 10 06 <i> <0xE6 - i>]` for i in 0..255

### 3.5 Seed/Key Authentication

จาก MainForm มี:

- `"กำลังตรวจสอบ Seed/Key..."` → ระหว่างกำลัง auth
- `"การตรวจสอบ ล้มเหลว Seed/Key"` → fail message

ขั้นตอน (จาก IL):

1. ส่ง 2 ชุด init bytes (`array`, `array2`)
2. แสดง "กำลังตรวจสอบ Seed/Key..."
3. ส่ง challenge bytes (`array3`)
4. ตรวจคำตอบ — ถ้าตรงกับ expected → ผ่าน

Algorithm Seed/Key สำหรับ Honda จะอยู่ใน method `\u00A0` ของ class นั้น
(ยังอ่านไม่ได้ครบเพราะ method names ไม่สามารถ rename อย่างเฉพาะเจาะจง — ดู `CLASS_INDEX.md`)

---

## 4. License / Anti-piracy System

ดูจาก `License_Secrets.cs` (เดิม `MZA_TUNER_FLASH_2026.10.cs`) — class `Type_14`

### 4.1 Constants

```csharp
private const string  K_STORAGE   = "MZA_AUTO_99_PROTECT_ULTRA_V8";  // XOR key
private const string  K_HWID_SALT = "SHARK_V8_PREMIUM_SYSTEM";       // HWID hash salt
private const string  K_LICENSE_SALT = "MZA_SECRET_2026_PRO";        // license signing salt
public  const string  BLACKLIST_URL = "http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=";

private static readonly string AppDataDir = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "RemapMZA_Pro");
private static readonly string SystemDat  = Path.Combine(AppDataDir, "system.dat");
```

### 4.2 HWID Computation

```csharp
public static string GetHwid() {
    // 1. Read WMI
    string raw = Wmi("Win32_Processor", "ProcessorId")
               + Wmi("Win32_BaseBoard", "SerialNumber");
    if (string.IsNullOrEmpty(raw))
        raw = Environment.MachineName + Environment.UserName;   // fallback

    // 2. SHA-512 with salt, take first 40 hex chars
    using (var sha = SHA512.Create()) {
        var hex = BitConverter
            .ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(raw + "SHARK_V8_PREMIUM_SYSTEM")))
            .Replace("-", "");
        return hex.Substring(0, 40);
    }
}
```

### 4.3 License-Key Generation Algorithm

```csharp
// Format:  XXXX-XXXX-XXXX-XXXX-XXXX  (5 groups of 4 hex chars - 20 chars total)
public static string GenerateKey(string hwid) {
    using (var sha = SHA256.Create()) {
        var hex = BitConverter
            .ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(hwid + "MZA_SECRET_2026_PRO")))
            .Replace("-", "");
        var sb = new StringBuilder();
        for (int i = 0; i < 20; i++) {
            if (i > 0 && i % 4 == 0) sb.Append('-');
            sb.Append(hex[i]);
        }
        return sb.ToString().ToUpper();
    }
}
```

### 4.4 Storage Format (`system.dat`)

```csharp
// stored = Base64(plaintext XOR "MZA_AUTO_99_PROTECT_ULTRA_V8")
private static string XorEncryptB64(string plain, string key) {
    var bytes = Encoding.UTF8.GetBytes(plain);
    for (int i = 0; i < bytes.Length; i++)
        bytes[i] = (byte)(bytes[i] ^ key[i % key.Length]);
    return Convert.ToBase64String(bytes);
}
```

`system.dat` content = `XorEncryptB64(licenseKey, K_STORAGE)`

### 4.5 Validation Flow

```csharp
public static bool IsLicensed() {
    if (!File.Exists(SystemDat)) return false;
    string stored = XorDecryptB64(File.ReadAllText(SystemDat), K_STORAGE);
    return string.Equals(stored, GenerateKey(GetHwid()), StringComparison.OrdinalIgnoreCase);
}
```

### 4.6 License Key UI

`Form_LicenseKey.cs` (เดิม `MZA_TUNER_FLASH_2026.11.cs`) — UI สีดำ Consolas 15pt:

- Label "รหัสปลดล็อค (LICENSE KEY)"
- TextBox `UseSystemPasswordChar = true`, สีพื้น `(28,28,28)` ขาว, จัดกลาง
- กรอก license → เปรียบเทียบกับ `GenerateKey(GetHwid())` → ถ้าตรง: เซฟ `system.dat` แล้ว แสดง: _"✅ การยืนยันเสร็จสมบูรณ์ ระบบ MZA-TUNER ถูกปลดล็อคสำเร็จ"_

### 4.7 HWID Blacklist Check

1. HTTP GET → `http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=<HWID>`
2. ถ้า response บอกเป็น blacklist → ปิดโปรแกรม

> ⚠️ **คำเตือน**: ผมเขียน algorithm นี้ไว้เพื่อความเข้าใจเชิงเทคนิคเท่านั้น
> โปรแกรมต้นฉบับมีลิขสิทธิ์ — _ห้ามใช้_ ข้อมูลนี้สร้าง keygen หรือเลี่ยง license

---

## 5. Cloud Sharing

### 5.1 Server Endpoints

```
https://mza-tuner.site/Sharefiles/         (primary)
https://82.26.104.124/Sharefiles/          (IP fallback)
http://82.26.104.124/mza-tuner/is_blacklisted.php?hwid=<HWID>
```

### 5.2 File listing

โปรแกรม fetch HTML index แล้ว parse:

```
regex: href="([^"/]+)"
```

หา link ของไฟล์แต่ละไฟล์ใน Sharefiles directory

### 5.3 UI

DataGridView (Segoe UI Semibold) columns:

- "ลำดับ" (No)
- "ผู้จูน / สำนัก" (User)
- "รหัสกล่อง ECU" (Ecu)
- "Action"

โหมด: ดาวน์โหลด / อัปโหลด

**Status messages:**

- `"กำลังเชื่อมต่อกับเซิร์ฟเวอร์คลาวด์ MZATUNER..."`
- `"ดาวน์โหลดและบันทึกไฟล์สําเร็จแล้วครับ!"`
- `"ลืมแนบไฟล์ครับ! กรุณาเลือกไฟล์ก่อนอัพโหลด"`
- `"กรุณาใส่ชื่อผู้จูนหรือสำนักก่อนครับ"`

---

## 6. ไฟล์ Config ที่อ่านจากดิสก์

| ไฟล์                                    | Schema             | หน้าที่                                                                                                              |
| --------------------------------------- | ------------------ | -------------------------------------------------------------------------------------------------------------------- |
| `C:\MZATUNER\data.ini`                  | INI                | Master ECU database (`[khPartCode]`, `[khEcmId]`, `[khStartOffset]`, `[khCksumOffset]`, `[shPartCode]`, `[shEcmId]`) |
| `C:\MZATUNER\programName.dat`           | UTF-8 text         | program title (ใส่ Emoji ได้)                                                                                        |
| `C:\MZATUNER\buttonColors.dat`          | `<name>,<argbInt>` | สี button ต่างๆ                                                                                                      |
| `C:\MZATUNER\Background.dat`            | JPEG (รี-named)    | desktop background                                                                                                   |
| `C:\MZATUNER\headerColor.dat`           | HTML hex           | สี header bar                                                                                                        |
| `C:\MZATUNER\data\By.ช่างลิงกล่องซิ่ง\` | dir                | ลายเซ็นต์ผู้สร้าง                                                                                                    |
| `C:\MZATUNER\data\defaul.jpg`           | JPEG               | default image                                                                                                        |

---

## 7. Obfuscation ที่พบ

- **Obfuscar 1.0** — main protector
- **Anti-ILDASM** — `[assembly: SuppressIldasm]`
- **Anti-debug** — Module-level constructor + checks
- **Math mutations** — แปลง constant เป็นนิพจน์ (math) เพื่อ confuse decompilers
- **Modified managed entry-point** — ไม่ใช่ Main() ปกติ
- **Unicode chars** สำหรับ identifier ทั้งหมด (โดยเฉพาะ chars ที่มองไม่เห็น)
- **String encoding** — XOR (`b ^= i ^ 0xAA`) แล้ว UTF-8 substring

### String Decryptor

Class: `<PrivateImplementationDetails>{...}.EC2D41B1-A2F9-4664-90D8-86645EE2E753`

```csharp
private static byte[] 4;             // 40,917-byte XOR'ed blob
private static string[] 5 = new string[1000];  // cache

static cctor() {
    4 = new byte[] { ... };
    for (int i = 0; i < 4.Length; i++)
        4[i] = (byte)(4[i] ^ i ^ 0xAA);
}

private static string 6(int idx, int off, int len) {
    string s = Encoding.UTF8.GetString(4, off, len);
    5[idx] = s;
    return s;
}

public static string \u00A0() => 5[0] ?? 6(0, 0, 45);
public static string \u1680() => 5[1] ?? 6(1, 45, 13);
// ... 1000 entries total
```

ผมแก้ปัญหานี้ด้วยการโหลด assembly ผ่าน reflection แล้ว invoke ทุก method
รวบรวมผลออกเป็น `tools/strings.json`

---

## 8. Building (ถ้าต้องการ build ใหม่)

> ⚠️ ระวัง: build ออกมา **ไม่สามารถใช้ตามลิขสิทธิ์เดิม** ใช้เพื่อการศึกษาเท่านั้น

ข้อจำกัดในปัจจุบัน:

1. Source ที่ deobfuscate **ยังคอมไพล์ไม่ผ่าน** ทันทีเพราะ:
   - Member name collision (different Unicode chars rendered identically by dnSpy)
   - File names มี Unicode invisible chars
   - csproj `HintPath` ผิด
2. ต้องแก้:
   - Rename ไฟล์ Unicode → ASCII manually
   - แก้ csproj path: `<HintPath>C:\MZATUNER\FTD2XX_NET.dll</HintPath>`
   - ลบ `[assembly: SuppressIldasm]`
   - ลบ `[module: \u1680(11)]` (anti-debug stub)
   - แก้ `<StartupObject>` ใน csproj ให้ตรงกับชื่อ class จริง
3. ส่ง resources `Honda_Flash_Tools.Net.*` (resources จาก upstream) — เก็บไว้หรือไม่ก็ได้

แนะนำ: เริ่มจาก subset เล็กๆ (เช่น แค่ Form3 + crypto) เพื่อ verify ก่อน
