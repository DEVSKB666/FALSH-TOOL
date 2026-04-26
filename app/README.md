# MZA-Tuner GUI (Tauri 2 + Next.js 14)

โปรแกรมหน้าตาสวย ๆ สำหรับ Honda Keihin/Shinden ECU flasher — port แบบ
modern stack แทน WinForms เดิม

## Tech Stack

| Layer | Tech |
|---|---|
| Window / runtime | **Tauri 2** (Rust + WebView2) |
| Frontend framework | **Next.js 14** (App Router, static export) |
| Styling | **Tailwind CSS** + custom shadcn-like primitives |
| Icons | **Lucide React** (no emoji ที่ไหน) |
| Animation | **Framer Motion** + canvas particles |
| Font | **Prompt** (Google Fonts ภาษาไทย) + JetBrains Mono |
| State | **Zustand** + persist middleware |
| Hardware | **libftd2xx** (cross-platform FTDI D2XX) |
| Crypto core | re-uses ของ `mza-tuner-rs` (อาคารเดียวกัน) |

## Pages

| Route | สิ่งที่ทำ |
|---|---|
| `/`         | Splash screen → Dashboard (สถานะ FTDI / License / Runtime) |
| `/ecu`      | READ EEPROM (Keihin / Shinden) + progress bar + hex preview |
| `/files`    | `.ECU` ↔ `.BIN` converter, รองรับ password override |
| `/database` | ค้นหา 269 รุ่น Honda จาก `data.ini` |
| `/settings` | ตั้ง brand name / โลโก้ / theme / animated background / splash |

## Themes

4 preset เปลี่ยนได้สด ๆ จาก Settings:

- **Light** — สำหรับใช้กลางวัน
- **Dark** — มาตรฐาน
- **Racing** — แดง-ดำ vibe นักแต่ง
- **Neon** — ฟลูออเรสเซนต์ฟ้า-ม่วง (mecha tuner)

## Animated Backgrounds

เลือกได้จาก Settings:

- **Particles** — canvas particle field + connecting lines (ใช้สี theme)
- **Circuit** — grid + glowing pulses
- **Scan-lines** — retro CRT vibe
- **Aurora** — ribbons of colour
- **Off** — solid background

## Splash Screen

ย้อนข้อความ C# ต้นฉบับ:
- `> INITIALIZING MZA-TUNER 2026...`
- `> SEARCHING FTDI INTERFACE...`
- `> FTDI D2XX DRIVER DETECTED [OK]`
- ฯลฯ

มี progress bar + animated brand mark ใช้ Framer Motion fade-in/out

## Project Layout

```
mza-tuner-app/
├── src/                          ─ Next.js 14 frontend (App Router)
│   ├── app/
│   │   ├── layout.tsx            ─ root + Prompt + theme
│   │   ├── page.tsx              ─ splash + dashboard
│   │   ├── ecu/page.tsx          ─ EEPROM read tool
│   │   ├── files/page.tsx        ─ .ECU/.BIN converter
│   │   ├── database/page.tsx     ─ Honda parts DB browser
│   │   ├── settings/page.tsx     ─ theme/brand/font tweaks
│   │   └── globals.css           ─ Tailwind + theme tokens
│   ├── components/
│   │   ├── app-shell.tsx         ─ sidebar + nav
│   │   ├── animated-background.tsx
│   │   ├── splash-screen.tsx
│   │   ├── theme-provider.tsx
│   │   └── ui/                   ─ shadcn-style primitives
│   └── lib/
│       ├── settings.ts           ─ Zustand persisted store
│       ├── tauri.ts              ─ typed IPC wrapper
│       └── utils.ts              ─ cn() / hexDump() / formatBytes()
│
├── src-tauri/                    ─ Rust backend
│   ├── Cargo.toml
│   ├── tauri.conf.json
│   ├── capabilities/default.json ─ permission set
│   ├── icons/                    ─ all platform icons (autogen)
│   └── src/
│       ├── main.rs               ─ entry point
│       ├── lib.rs                ─ Tauri builder
│       ├── commands.rs           ─ #[tauri::command] surface
│       ├── ftdi.rs               ─ enumerate FTDI devices
│       ├── transport.rs          ─ FTDI <-> KLine trait
│       └── eeprom.rs             ─ Honda Keihin/Shinden read logic
│
├── tools/
│   ├── make_icon.ps1             ─ generate logo PNG with System.Drawing
│   └── logo.png                  ─ source for `tauri icon`
│
├── package.json                  ─ npm dependencies
├── tsconfig.json
├── tailwind.config.ts
├── next.config.mjs               ─ output: 'export' for Tauri
└── postcss.config.mjs
```

## Quick start

```cmd
:: Prerequisite (one-time):
:: - Node 22+, npm 10+
:: - Rust 1.74+ (rustup default stable)
:: - Microsoft Edge WebView2 runtime (มากับ Win 11)

cd c:\Users\rovki\Downloads\MAZA\mza-tuner-app

:: 1) Dev mode - hot-reload frontend + Rust auto-rebuild
npm run tauri:dev

:: 2) Production bundle (.msi + .exe installer)
npm run tauri:build
:: -> ผลออกที่ src-tauri\target\release\bundle\
```

## วิธีต่อสาย ECU (Production)

1. ต่อ FTDI USB-Serial cable (FT232 family) เข้าคอมพิวเตอร์
2. ลง driver D2XX ของ FTDI (https://ftdichip.com/drivers/d2xx-drivers/)
3. ต่อสาย K-Line จาก FTDI TX/RX → ECU pin (Honda K-Line)
4. เปิด `MZA-Tuner` → เปิดสวิตซ์รถ ภายใน 3 วินาที → ไป `/ecu` → กด READ EEPROM

⚠️ เขียน flash ลง ECU จริง = เสี่ยงพังกล่อง — ทดสอบ READ ก่อนเสมอ

## Browser preview (ไม่มี hardware)

ถ้าจะเปิด UI ผ่าน browser ปกติ (ไม่มี Tauri):

```cmd
npm run dev
:: แล้วเปิด http://localhost:3000
```

ทุก IPC call จะ fall-back ไปที่ mock (ดู `src/lib/tauri.ts`) — UI ทำงานได้
หมด แต่จะไม่อ่าน ECU จริง

## หน้าตา (โครงสร้าง)

- **Sidebar (ซ้าย)** — โลโก้ + ชื่อโปรแกรม + nav 5 items + version
- **Main area (ขวา)** — Header (eyebrow + title + sub) → Cards
- **Background** — Animated particles หลังทุกอย่าง (z-0)
- **Splash overlay** — z-50 ขึ้นมาเหนือทุกอย่างตอนเปิด

## Customize

ทุกอย่างใน Settings:
- เปลี่ยนชื่อ → state.appName
- เปลี่ยน logo → state.logoDataUrl (Base64 data URL — เก็บใน localStorage)
- เปลี่ยน theme → state.theme
- เปลี่ยน background → state.background
- ปิด splash → state.showSplash = false

ทุก state ถูก **persist ใน localStorage** ผ่าน Zustand middleware → ค่ายังอยู่หลัง restart

## License

MIT (โครงการนี้) — แต่ logic ของ Honda Keihin/Shinden protocol สกัดมาจาก
deobfuscated source ของ MZA-TUNER (ลิขสิทธิ์เจ้าของ) ใช้เพื่อการศึกษาเท่านั้น
