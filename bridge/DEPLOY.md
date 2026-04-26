# 🚀 LOY-Bridge Deployment Guide

วิธีติดตั้ง / ใช้งาน `loy-bridge` กับเครื่อง 2 เครื่อง:

```
┌─ Notebook (อยู่กับรถ + ECU) ──┐         ┌─ Main PC (อยู่บ้าน/ห้อง) ──┐
│  loy-bridge.exe              │ ←TCP→   │  LOY-TUNER GUI               │
│  + FTDI driver               │  :7878  │  (Tauri app, no FTDI needed) │
│  + สาย FTDI ต่อ ECU            │  WiFi   │                              │
└──────────────────────────────┘         └──────────────────────────────┘
```

---

## 📦 ไฟล์ที่ต้องเอาไปเครื่อง notebook (1 ไฟล์เดียว!)

### Build บน dev machine (เครื่องที่ผมพัฒนา)

```cmd
cd C:\Users\rovki\Desktop\MzaTuner
cargo build -p loy-bridge --release
```

**ผลลัพธ์:** `target\release\loy-bridge.exe` (ขนาด ~1.6 MB)

### Copy ไป notebook

วิธีที่ง่ายที่สุด:

| วิธี | คำสั่ง / ขั้นตอน |
|---|---|
| USB drive | คัดลอก `loy-bridge.exe` → USB → เสียบ notebook → copy ลงโฟลเดอร์ใดก็ได้ |
| Network share | `xcopy target\release\loy-bridge.exe \\NOTEBOOK\C$\loy-bridge\` |
| Cloud | Google Drive / Dropbox |

**โฟลเดอร์แนะนำบน notebook:** `C:\loy-bridge\loy-bridge.exe`

---

## 🔧 บน Notebook — ตั้งค่าครั้งเดียว

### 1. ติดตั้ง FTDI D2XX driver

ถ้ายังไม่มี:
- Download: https://ftdichip.com/drivers/d2xx-drivers/
- หรือ Windows อาจ auto-install เมื่อเสียบสาย FTDI ครั้งแรก

ตรวจว่าเห็นสายแล้ว:
```cmd
:: ใน Device Manager ควรเห็น "USB Serial Converter" หรือ "FT232R USB UART"
devmgmt.msc
```

### 2. ทดสอบ binary

```cmd
C:\loy-bridge\loy-bridge.exe --help
```

ควรเห็น:
```
TCP bridge daemon for LOY-TUNER (FTDI K-Line forwarder)

Usage: loy-bridge.exe [OPTIONS]

Options:
      --bind <BIND>  Address:port to listen on [default: 0.0.0.0:7878]
  -v, --verbose      Increase log verbosity
  -h, --help         Print help
  -V, --version      Print version
```

### 3. หา IP ของ notebook ใน WiFi

```cmd
ipconfig
```

มอง: `Wireless LAN adapter Wi-Fi:` → `IPv4 Address. . . . . . . . . . . : 192.168.1.50` (เลขนี้แตกต่างกัน)

### 4. รัน daemon

```cmd
C:\loy-bridge\loy-bridge.exe --bind 0.0.0.0:7878 --verbose
```

ครั้งแรก Windows Firewall จะเด้งถาม → กด **Allow access** สำหรับ Private network

จะเห็น:
```
INFO loy-bridge starting version=0.1.0
INFO listening on 0.0.0.0:7878
```

ปล่อยหน้าต่างนี้ไว้ เปิดทิ้งไว้ตลอดที่ใช้งาน

---

## 💻 บน Main PC — ใช้งาน

### Option A: ใช้ test client (ใช้ได้เลยตอนนี้)

```powershell
# จาก main PC
cd C:\Users\rovki\Desktop\MzaTuner\bridge

.\test-client.ps1 ping              -Host_ 192.168.1.50
.\test-client.ps1 list_ports        -Host_ 192.168.1.50
.\test-client.ps1 read_eeprom '{"variant":"keihin"}' -Host_ 192.168.1.50
```

`192.168.1.50` แทนด้วย IP จริงของ notebook

### Option B: ใช้กับ LOY-TUNER GUI (กำลังพัฒนา)

> **TODO:** Integration เข้า main app ยังพัฒนาอยู่ — เมื่อเสร็จจะมี:
> - ตั้งค่า "Bridge URL" ใน `/settings`
> - GUI auto-route คำสั่งทั้งหมดผ่าน bridge

---

## 🔐 ความปลอดภัย

⚠️ **daemon ไม่มี authentication** — ใครเชื่อมต่อ network เดียวกันก็ใช้ได้

| สถานการณ์ | คำแนะนำ |
|---|---|
| WiFi บ้านส่วนตัว | OK — ใช้ `--bind 0.0.0.0:7878` ได้เลย |
| WiFi public (ร้าน, สนามบิน) | ❌ **อย่าทำ** — มี risk |
| ต่อ direct (notebook ↔ PC ผ่าน LAN cable) | ปลอดภัยที่สุด |
| Local เครื่องเดียว | ใช้ `--bind 127.0.0.1:7878` |

---

## 🔄 Auto-start บน notebook (optional)

ให้ daemon รันเองทุกครั้งที่เปิดเครื่อง:

### วิธี 1: Task Scheduler (GUI)

1. กด `Win+R` → `taskschd.msc`
2. **Create Basic Task** → ตั้งชื่อ "LOY Bridge"
3. Trigger: **When I log on**
4. Action: **Start a program** → `C:\loy-bridge\loy-bridge.exe`
5. Arguments: `--bind 0.0.0.0:7878`
6. Finish

### วิธี 2: Startup folder (ง่ายสุด)

1. กด `Win+R` → `shell:startup`
2. Right-click → New → Shortcut → ใส่ path:
   ```
   C:\loy-bridge\loy-bridge.exe --bind 0.0.0.0:7878
   ```

---

## 🐛 Troubleshooting

### Q: รันแล้วขึ้น "Address already in use"

อาจมี process เก่ายังรันอยู่:
```cmd
netstat -ano | findstr :7878
taskkill /F /PID <PID จากบรรทัดบน>
```

### Q: ลูกค้าจาก PC อื่นเชื่อมต่อไม่ได้

1. ตรวจ Windows Firewall:
   ```cmd
   netsh advfirewall firewall add rule name="LOY Bridge" dir=in action=allow protocol=TCP localport=7878
   ```
2. ตรวจว่า bind เป็น `0.0.0.0:7878` ไม่ใช่ `127.0.0.1:7878`
3. ทั้ง 2 เครื่อง ping กันได้:
   ```cmd
   :: จาก main PC
   ping 192.168.1.50
   ```

### Q: `read_eeprom` ตอบ `"no FTDI device at index 0"`

- ตรวจสายเสียบครบ (USB ไป notebook + K-Line ไป ECU)
- เปิดสวิตซ์รถ
- ตรวจ Device Manager เห็นสายไหม
- ลอง index อื่น: `{"variant":"keihin","device_index":1}`

### Q: Binary มี dependency อะไรบ้าง

`loy-bridge.exe` เป็น **single-file Rust binary** — ไม่ต้องการ runtime, .NET, Python, ฯลฯ

ที่ต้องการ:
- Windows 10/11 (x86_64)
- FTDI D2XX driver (ใช้สาย)
- Network connectivity

---

## 📊 Performance

| Operation | เวลา (real ECU) | Bandwidth (LAN) |
|---|---|---|
| `ping` | < 5 ms | ~100 bytes |
| `list_ports` | ~50 ms | ~200 bytes |
| `read_eeprom` (Keihin/Shinden) | ~26-30 sec | ~5 KB JSON |

WiFi 802.11n ขึ้นไปก็ใช้ได้สบาย — payload เล็กมาก

---

## 📋 Quick Reference

| ทำอะไร | คำสั่ง |
|---|---|
| Build | `cargo build -p loy-bridge --release` |
| Build (mock) | `cargo build -p loy-bridge --no-default-features --features mock --release` |
| Run on notebook | `loy-bridge.exe --bind 0.0.0.0:7878 --verbose` |
| Run local-only | `loy-bridge.exe --bind 127.0.0.1:7878` |
| Test from PC | `.\test-client.ps1 ping -Host_ 192.168.x.x` |
| หา IP | `ipconfig` (Wireless LAN adapter Wi-Fi → IPv4) |
| Stop | กด `Ctrl+C` ในหน้าต่าง daemon |
