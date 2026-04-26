"use client";

import { useEffect, useRef, useState } from "react";
import { motion } from "framer-motion";
import {
  Activity,
  CheckCircle2,
  ChevronRight,
  Cpu,
  Download,
  FileLock2,
  FolderOpen,
  Gauge,
  Play,
  ShieldCheck,
  Sparkles,
  Trash2,
  Volume2,
  VolumeX,
  Wand2,
  Wrench,
  Zap,
  ZapOff,
} from "lucide-react";
import { useSettings } from "@/lib/settings";
import { useConnection } from "@/lib/connection";
import { sound } from "@/lib/sounds";
import { toast } from "@/components/toast";
import { AppShell } from "@/components/app-shell";
import { SplashScreen } from "@/components/splash-screen";
import { ConnectionPanel } from "@/components/connection-panel";
import { KlineLogPanel } from "@/components/kline-log-panel";
import { Card, CardContent } from "@/components/ui/card";
import { cn } from "@/lib/utils";

export default function HomePage() {
  const showSplash = useSettings((s) => s.showSplash);
  const seen = useSettings((s) => s.splashSeenThisRun);
  const markSeen = useSettings((s) => s.markSplashSeen);
  const [splashing, setSplashing] = useState(showSplash && !seen);

  return (
    <>
      {splashing && (
        <SplashScreen
          onComplete={() => {
            markSeen();
            setSplashing(false);
          }}
        />
      )}
      <AppShell>
        <Dashboard />
      </AppShell>
    </>
  );
}

function Dashboard() {
  const appName    = useSettings((s) => s.appName);
  const tagline    = useSettings((s) => s.tagline);
  const logo       = useSettings((s) => s.logoDataUrl);

  const status     = useConnection((s) => s.status);
  const flashCount = useConnection((s) => s.flashCount);
  const ecuId      = useConnection((s) => s.ecuId);

  // File picker state - kept local since it doesn't need to be global yet.
  const [binFile, setBinFile]   = useState<File | null>(null);
  const [progress, setProgress] = useState(0);
  const [running, setRunning]   = useState(false);

  const connected = status === "connected";

  function onPickBin(file: File | null) {
    if (file) {
      setBinFile(file);
      sound.click();
      toast.success("เลือกไฟล์แล้ว", file.name);
    }
  }

  async function startFlash() {
    if (!connected) {
      sound.error();
      toast.error("ยังไม่ได้เชื่อมต่อ ECM", "กดปุ่มเชื่อมต่อก่อนเริ่ม");
      return;
    }
    if (!binFile) {
      sound.error();
      toast.error("ยังไม่ได้เลือกไฟล์ BIN", "กดปุ่ม 'เลือกไฟล์' ด้านขวา");
      return;
    }
    setRunning(true);
    setProgress(0);
    sound.click();
    toast.info("เริ่มอัดไฟล์", binFile.name);
    // Mock progress (replace with real Tauri event listener later)
    for (let p = 0; p <= 100; p += 5) {
      await new Promise((r) => setTimeout(r, 90));
      setProgress(p);
    }
    setRunning(false);
    sound.success();
    toast.success("อัดไฟล์เสร็จสมบูรณ์", `${binFile.name} (${(binFile.size / 1024).toFixed(1)} KB)`);
  }

  return (
    <div className="flex h-full flex-col gap-5">
      {/* Top status pills row */}
      <TopStatusRow />

      {/* Main 2-column area */}
      <div className="grid flex-1 grid-cols-1 gap-5 lg:grid-cols-[1.15fr_1fr]">
        <BannerCard logo={logo} appName={appName} />
        <EcuInfoCard ecuId={ecuId} appName={appName} tagline={tagline} flashCount={flashCount} />
      </div>

      {/* Connection bar - lives ONLY on the home page; the underlying
          Zustand store is global so the connection itself persists when
          the user navigates to /ecu, /files etc. */}
      <ConnectionPanel />

      {/* Live K-Line traffic log + start action */}
      <div className="grid grid-cols-1 gap-5 xl:grid-cols-[2fr_1fr]">
        <KlineLogPanel />
        <StartActionCard
          running={running}
          progress={progress}
          binFile={binFile}
          ecuId={ecuId}
          onStart={startFlash}
        />
      </div>

      {/* File picker */}
      <FilePickerCard
        binFile={binFile}
        onPick={onPickBin}
        onRemove={() => {
          if (binFile) {
            sound.warning();
            toast.warning("ลบไฟล์ออกจากรายการ", binFile.name);
          }
          setBinFile(null);
        }}
      />
    </div>
  );
}

// ---------------- Top status row ---------------------

function TopStatusRow() {
  const autoConnect    = useSettings((s) => s.autoConnect);
  const setAutoConnect = useSettings((s) => s.setAutoConnect);
  const soundEnabled    = useSettings((s) => s.soundEnabled);
  const setSoundEnabled = useSettings((s) => s.setSoundEnabled);

  return (
    <div className="flex flex-wrap items-center gap-2 rounded-2xl border border-border/60 bg-card/50 p-3 ring-1 ring-black/10 backdrop-blur-md">
      <Pill icon={ShieldCheck} label="License" value="Active" tone="ok" />
      <Pill icon={Wrench}      label="Fix HW"  value="พร้อม"   tone="ok" />
      <Pill icon={Sparkles}    label="Speed Up" value="High" tone="ok" />
      <div className="ml-auto flex items-center gap-2">
        <ToggleChip
          icon={autoConnect ? Zap : ZapOff}
          active={autoConnect}
          label={autoConnect ? "Auto-connect" : "Manual"}
          onClick={() => {
            sound.click();
            setAutoConnect(!autoConnect);
            toast.info(autoConnect ? "ปิด Auto-connect" : "เปิด Auto-connect");
          }}
        />
        <ToggleChip
          icon={soundEnabled ? Volume2 : VolumeX}
          active={soundEnabled}
          label={soundEnabled ? "Sound On" : "Mute"}
          onClick={() => {
            // Play one click only when ENABLING (so muting is silent).
            if (!soundEnabled) sound.click();
            setSoundEnabled(!soundEnabled);
            toast.info(soundEnabled ? "ปิดเสียง" : "เปิดเสียง");
          }}
        />
      </div>
    </div>
  );
}

function Pill({
  icon: Icon,
  label,
  value,
  tone,
}: {
  icon: typeof ShieldCheck;
  label: string;
  value: string;
  tone: "ok" | "warn" | "err";
}) {
  const dot =
    tone === "ok"   ? "bg-emerald-500 shadow-[0_0_6px_rgba(16,185,129,0.7)]" :
    tone === "warn" ? "bg-amber-500"  : "bg-red-500";
  return (
    <div className="inline-flex items-center gap-2 rounded-full bg-muted/50 px-3 py-1 text-xs font-medium ring-1 ring-border/40">
      <Icon className="h-3.5 w-3.5 text-muted-foreground" />
      <span className="text-muted-foreground">{label}</span>
      <span>{value}</span>
      <span className={cn("h-1.5 w-1.5 rounded-full", dot)} />
    </div>
  );
}

function ToggleChip({
  icon: Icon,
  label,
  active,
  onClick,
}: {
  icon: typeof Zap;
  label: string;
  active: boolean;
  onClick: () => void;
}) {
  return (
    <button
      type="button"
      onClick={onClick}
      className={cn(
        "inline-flex items-center gap-1.5 rounded-full px-3 py-1 text-xs font-medium transition ring-1",
        active
          ? "bg-primary/15 text-primary ring-primary/40"
          : "bg-muted/40 text-muted-foreground ring-border/40 hover:text-foreground",
      )}
    >
      <Icon className="h-3.5 w-3.5" />
      {label}
    </button>
  );
}

// ---------------- Banner card ---------------------

function BannerCard({ logo, appName }: { logo: string | null; appName: string }) {
  return (
    <Card className="relative overflow-hidden">
      {/* Gradient halo */}
      <div className="pointer-events-none absolute inset-0 bg-radial-fade opacity-70" />
      <div className="pointer-events-none absolute -left-10 -top-10 h-40 w-40 rounded-full bg-primary/30 blur-3xl" />
      <div className="pointer-events-none absolute -right-10 -bottom-10 h-40 w-40 rounded-full bg-accent/40 blur-3xl" />

      <CardContent className="relative flex h-full min-h-[280px] flex-col items-center justify-center gap-4 p-8">
        {logo ? (
          <img
            src={logo}
            alt={appName}
            className="max-h-44 w-auto rounded-xl object-contain ring-1 ring-primary/30"
          />
        ) : (
          <motion.div
            className="flex h-32 w-32 items-center justify-center rounded-2xl bg-gradient-to-br from-primary to-primary/60 shadow-[0_0_60px_hsl(var(--primary)/0.6)]"
            animate={{ rotate: [0, 4, -4, 0] }}
            transition={{ duration: 6, repeat: Infinity }}
          >
            <Zap className="h-16 w-16 text-primary-foreground" strokeWidth={2.4} />
          </motion.div>
        )}
        <div className="text-center">
          <p className="font-mono text-[10px] uppercase tracking-[0.4em] text-primary">Tuner</p>
          <h2 className="mt-1 text-2xl font-bold tracking-tight">{appName}</h2>
        </div>
      </CardContent>
    </Card>
  );
}

// ---------------- ECU info card ---------------------

function EcuInfoCard({
  ecuId,
  appName,
  tagline,
  flashCount,
}: {
  ecuId: string | null;
  appName: string;
  tagline: string;
  flashCount: number;
}) {
  const rows: Array<[string, string]> = [
    ["Brand",     appName],
    ["Family",    "Honda Keihin / Shinden"],
    ["ECM ID",    ecuId ?? "ยังไม่ได้อ่าน"],
    ["Flashes",   String(flashCount).padStart(4, "0")],
    ["Protocol",  "K-Line / KWP2000"],
    ["Baud",      "10,400 bps"],
  ];

  return (
    <Card className="relative overflow-hidden">
      <CardContent className="flex h-full flex-col gap-5 p-7">
        <div>
          <p className="font-mono text-[10px] uppercase tracking-[0.4em] text-primary">ECU Info</p>
          <h3 className="mt-1 text-xl font-bold">{tagline}</h3>
        </div>

        <div className="grid grid-cols-1 gap-2.5 sm:grid-cols-2">
          {rows.map(([k, v]) => (
            <div
              key={k}
              className="rounded-lg border border-border/40 bg-background/40 px-3 py-2"
            >
              <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">{k}</p>
              <p className="mt-0.5 truncate text-sm font-medium">{v}</p>
            </div>
          ))}
        </div>

        <div className="mt-auto flex items-center gap-2 text-xs text-muted-foreground">
          <Activity className="h-3.5 w-3.5" />
          ทุกการเชื่อมต่อใช้ FTDI D2XX driver, ผ่าน USB-Serial 921600 → 10400 baud
        </div>
      </CardContent>
    </Card>
  );
}

// ---------------- File picker ---------------------

function FilePickerCard({
  binFile,
  onPick,
  onRemove,
}: {
  binFile: File | null;
  onPick: (f: File | null) => void;
  onRemove: () => void;
}) {
  const inputRef = useRef<HTMLInputElement>(null);

  return (
    <Card>
      <CardContent className="flex flex-col gap-3 p-4 sm:flex-row sm:items-center">
        <FileLock2 className="hidden h-5 w-5 text-muted-foreground sm:block" />
        <div className="min-w-0 flex-1 truncate rounded-lg border border-dashed border-border/60 bg-background/40 px-3 py-2 text-sm">
          {binFile ? (
            <span className="truncate">
              <span className="text-primary">{binFile.name}</span>
              <span className="ml-2 text-xs text-muted-foreground">
                ({(binFile.size / 1024).toFixed(1)} KB)
              </span>
            </span>
          ) : (
            <span className="text-muted-foreground">( กรุณาเลือกไฟล์ BIN )</span>
          )}
        </div>
        <div className="flex gap-2">
          <button
            type="button"
            onClick={onRemove}
            disabled={!binFile}
            className={cn(
              "inline-flex h-9 items-center gap-1.5 rounded-lg border border-border/60 bg-background/40 px-3 text-sm font-medium transition",
              binFile ? "hover:bg-destructive/15 hover:text-destructive" : "opacity-50",
            )}
          >
            <Trash2 className="h-4 w-4" />
            ลบ
          </button>
          <button
            type="button"
            onClick={() => inputRef.current?.click()}
            className="inline-flex h-9 items-center gap-1.5 rounded-lg bg-primary px-3 text-sm font-semibold text-primary-foreground shadow hover:bg-primary/90"
          >
            <FolderOpen className="h-4 w-4" />
            เลือกไฟล์
          </button>
          <input
            ref={inputRef}
            type="file"
            accept=".bin,.BIN,.ecu,.ECU,.acg,.ACG"
            className="hidden"
            onChange={(e) => onPick(e.target.files?.[0] ?? null)}
          />
        </div>
      </CardContent>
    </Card>
  );
}

// ---------------- Start button + progress ---------------------

function StartActionCard({
  running,
  progress,
  binFile,
  ecuId,
  onStart,
}: {
  running: boolean;
  progress: number;
  binFile: File | null;
  ecuId: string | null;
  onStart: () => void;
}) {
  return (
    <Card>
      <CardContent className="flex h-full flex-col gap-3 p-4">
        <div className="flex items-center gap-3">
          <Gauge className="h-5 w-5 text-primary" />
          <p className="font-mono text-[11px] uppercase tracking-widest text-muted-foreground">
            สถานะ
          </p>
          <p className="ml-auto text-xs text-muted-foreground">
            {running
              ? `กำลังอัดไฟล์… ${progress}%`
              : binFile
              ? "พร้อมอัดไฟล์"
              : "รอการอัดไฟล์ (0KB)/(0kB)"}
          </p>
        </div>

        <div className="h-2 overflow-hidden rounded-full bg-muted/60">
          <motion.div
            className="h-full bg-gradient-to-r from-primary via-primary to-primary/70"
            initial={{ width: 0 }}
            animate={{ width: `${progress}%` }}
            transition={{ ease: "linear", duration: 0.1 }}
          />
        </div>

        <motion.button
          type="button"
          onClick={onStart}
          disabled={running}
          whileTap={{ scale: 0.97 }}
          className={cn(
            "mt-1 inline-flex h-12 items-center justify-center gap-2 rounded-xl font-semibold text-primary-foreground transition",
            running
              ? "bg-primary/60"
              : "bg-primary shadow-[0_0_30px_hsl(var(--primary)/0.55)] hover:bg-primary/90",
          )}
        >
          <Play className="h-5 w-5" fill="currentColor" />
          {running ? "กำลังทำงาน…" : "เริ่ม"}
        </motion.button>
      </CardContent>
    </Card>
  );
}
