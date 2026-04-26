"use client";

import { useMemo } from "react";
import { motion, AnimatePresence } from "framer-motion";
import {
  Activity,
  Battery,
  Download,
  Droplets,
  Flame,
  Gauge,
  Pause,
  Play,
  RotateCcw,
  Square,
  Sun,
  Thermometer,
  Timer,
  TrendingUp,
  Wind,
  Zap,
} from "lucide-react";
import Link from "next/link";
import { AppShell } from "@/components/app-shell";
import { Card, CardContent } from "@/components/ui/card";
import {
  formatSensor,
  samplesToCsv,
  sensorConfig,
  toneFor,
  useLiveData,
  type SensorConfig,
  type SensorId,
  type Tone,
} from "@/lib/livedata";
import { useConnection } from "@/lib/connection";
import { cn } from "@/lib/utils";
import { sound } from "@/lib/sounds";
import { toast } from "@/components/toast";
import { isTauri } from "@/lib/tauri";
import { AlertCircle, CheckCircle2, Power } from "lucide-react";

// Icon for each sensor (Lucide).
const SENSOR_ICON: Record<SensorId, typeof Gauge> = {
  rpm:     Gauge,
  tps_deg: Wind,
  tps_v:   Zap,
  ect:     Thermometer,
  o2:      Droplets,
  afr:     Flame,
  ltft:    TrendingUp,
  battery: Battery,
  inj_ms:  Timer,
  ign_deg: Sun,
};

const TONE_BG: Record<Tone, string> = {
  ok:     "bg-emerald-500/10 ring-emerald-500/30",
  warn:   "bg-amber-500/10 ring-amber-500/30",
  danger: "bg-red-500/15 ring-red-500/40",
};
const TONE_TEXT: Record<Tone, string> = {
  ok:     "text-emerald-300",
  warn:   "text-amber-300",
  danger: "text-red-300",
};
const TONE_LINE: Record<Tone, string> = {
  ok:     "stroke-emerald-400",
  warn:   "stroke-amber-400",
  danger: "stroke-red-400",
};

const RATE_OPTIONS: Array<{ ms: number; label: string }> = [
  { ms: 2000, label: "0.5 Hz" },
  { ms: 1000, label: "1 Hz" },
  { ms: 500,  label: "2 Hz" },
  { ms: 200,  label: "5 Hz" },
];

export default function LiveDataPage() {
  const values    = useLiveData((s) => s.values);
  const minmax    = useLiveData((s) => s.minmax);
  const history   = useLiveData((s) => s.history);
  const running   = useLiveData((s) => s.running);
  const recording = useLiveData((s) => s.recording);
  const recordedSamples = useLiveData((s) => s.recordedSamples);
  const intervalMs = useLiveData((s) => s.intervalMs);
  const source    = useLiveData((s) => s.source);
  const setRunning  = useLiveData((s) => s.setRunning);
  const setRecording = useLiveData((s) => s.setRecording);
  const setIntervalMs = useLiveData((s) => s.setIntervalMs);
  const resetMinMax = useLiveData((s) => s.resetMinMax);

  // Connection state - drives the DEMO vs LIVE banner.
  const connStatus = useConnection((s) => s.status);
  const connected  = connStatus === "connected";

  function togglePause() {
    sound.click();
    setRunning(!running);
    toast.info(running ? "หยุดชั่วคราว" : "เริ่มสตรีม");
  }

  function onReset() {
    sound.click();
    resetMinMax();
    toast.info("รีเซ็ต Min/Max แล้ว");
  }

  function onToggleRecord() {
    if (recording) {
      // stopping - prompt for export
      setRecording(false);
      sound.success();
      toast.success("หยุดบันทึก", `เก็บ ${recordedSamples.length} samples · กด Export เพื่อบันทึก CSV`);
    } else {
      sound.click();
      setRecording(true);
      toast.info("เริ่มบันทึก");
    }
  }

  async function onExport() {
    if (recordedSamples.length === 0) {
      sound.warning();
      toast.warning("ยังไม่มีข้อมูล", "กด ● บันทึก เพื่อเริ่มเก็บ sample แล้ว Export");
      return;
    }
    const csv  = samplesToCsv(recordedSamples);
    const date = new Date().toISOString().slice(0, 19).replace(/[:T]/g, "-");
    const name = `livedata_${date}.csv`;

    if (!isTauri) {
      // Browser fallback - download via blob.
      const blob = new Blob([csv], { type: "text/csv" });
      const url  = URL.createObjectURL(blob);
      const a    = document.createElement("a");
      a.href = url; a.download = name; a.click();
      URL.revokeObjectURL(url);
      sound.success();
      toast.success("ดาวน์โหลดแล้ว", name);
      return;
    }
    try {
      const { save } = await import("@tauri-apps/plugin-dialog");
      const { writeTextFile } = await import("@tauri-apps/plugin-fs");
      const path = await save({ defaultPath: name, filters: [{ name: "CSV", extensions: ["csv"] }] });
      if (typeof path !== "string") return;
      await writeTextFile(path, csv);
      sound.success();
      toast.success("Export สำเร็จ", path);
    } catch (e) {
      sound.error();
      toast.error("Export ล้มเหลว", String(e));
    }
  }

  return (
    <AppShell>
      <div className="space-y-5">
        {/* Header + controls */}
        <header className="flex flex-wrap items-center justify-between gap-3">
          <div>
            <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">LIVE DATA</p>
            <h1 className="mt-1 flex items-center gap-2 text-3xl font-bold tracking-tight">
              <Activity className="h-7 w-7 text-primary" />
              ข้อมูลสด
              <SourceBadge source={source} />
            </h1>
            <p className="mt-1 text-sm text-muted-foreground">
              Sensor stream {RATE_OPTIONS.find((r) => r.ms === intervalMs)?.label} · {history.length} sample buffer
            </p>
          </div>

          <div className="flex flex-wrap items-center gap-2">
            {/* Rate selector */}
            <div className="inline-flex overflow-hidden rounded-lg border border-border/50">
              {RATE_OPTIONS.map((opt) => (
                <button
                  key={opt.ms}
                  type="button"
                  onClick={() => { sound.click(); setIntervalMs(opt.ms); }}
                  className={cn(
                    "px-3 py-1.5 text-xs font-medium transition",
                    intervalMs === opt.ms ? "bg-primary text-primary-foreground" : "bg-card/40 text-muted-foreground hover:bg-card/70",
                  )}
                >
                  {opt.label}
                </button>
              ))}
            </div>

            <button
              type="button"
              onClick={togglePause}
              className={cn(
                "inline-flex items-center gap-1.5 rounded-lg border px-3 py-1.5 text-xs font-medium transition",
                running
                  ? "border-border/50 bg-card/50 text-muted-foreground hover:bg-card/80 hover:text-foreground"
                  : "border-amber-500/40 bg-amber-500/15 text-amber-200",
              )}
            >
              {running ? <><Pause className="h-3.5 w-3.5"/> หยุดชั่วคราว</> : <><Play className="h-3.5 w-3.5"/> เริ่มต่อ</>}
            </button>

            <button
              type="button"
              onClick={onReset}
              className="inline-flex items-center gap-1.5 rounded-lg border border-border/50 bg-card/50 px-3 py-1.5 text-xs font-medium text-muted-foreground transition hover:bg-card/80 hover:text-foreground"
            >
              <RotateCcw className="h-3.5 w-3.5" /> Reset Min/Max
            </button>

            <button
              type="button"
              onClick={onToggleRecord}
              className={cn(
                "inline-flex items-center gap-1.5 rounded-lg border px-3 py-1.5 text-xs font-medium transition",
                recording
                  ? "border-red-500/40 bg-red-500/15 text-red-200"
                  : "border-border/50 bg-card/50 text-muted-foreground hover:bg-card/80 hover:text-foreground",
              )}
            >
              {recording
                ? <><Square className="h-3.5 w-3.5 fill-current animate-pulse"/> หยุดบันทึก ({recordedSamples.length})</>
                : <><span className="h-2 w-2 rounded-full bg-red-500"/> บันทึก</>}
            </button>

            <button
              type="button"
              onClick={onExport}
              disabled={recordedSamples.length === 0}
              className={cn(
                "inline-flex items-center gap-1.5 rounded-lg border border-border/50 bg-card/50 px-3 py-1.5 text-xs font-medium transition",
                recordedSamples.length > 0
                  ? "text-foreground hover:bg-card/80"
                  : "cursor-not-allowed text-muted-foreground/40",
              )}
            >
              <Download className="h-3.5 w-3.5" /> Export CSV
            </button>
          </div>
        </header>

        {/* Source banner - explains DEMO vs LIVE state in plain Thai. */}
        <SourceBanner connected={connected} source={source} />

        {/* Main grid: large RPM card on the left + 9 sensor cards */}
        <div className="grid grid-cols-1 gap-4 lg:grid-cols-[minmax(280px,1fr)_2fr]">
          {/* Big RPM gauge */}
          <RpmCard
            value={values.rpm}
            history={history.map((h) => h.values.rpm)}
            min={minmax.rpm.min}
            max={minmax.rpm.max}
          />

          {/* 9 sensor cards in 3-col grid */}
          <div className="grid grid-cols-1 gap-3 sm:grid-cols-2 xl:grid-cols-3">
            {(["tps_deg","ect","o2","afr","ltft","battery","tps_v","inj_ms","ign_deg"] as SensorId[]).map((id) => (
              <SensorCard
                key={id}
                cfg={sensorConfig(id)}
                value={values[id]}
                min={minmax[id].min}
                max={minmax[id].max}
                history={history.map((h) => h.values[id])}
              />
            ))}
          </div>
        </div>
      </div>
    </AppShell>
  );
}

// ============================================================
// Source badge - shows "LIVE" (green pulsing) or "DEMO MODE" (amber)
// ============================================================

function SourceBadge({ source }: { source: "demo" | "real" }) {
  if (source === "real") {
    return (
      <span className="ml-2 inline-flex items-center gap-1.5 rounded-full bg-emerald-500/15 px-2.5 py-0.5 text-[10px] font-mono uppercase tracking-widest text-emerald-300 ring-1 ring-emerald-500/40">
        <motion.span
          className="h-1.5 w-1.5 rounded-full bg-emerald-400 shadow-[0_0_6px_rgba(52,211,153,0.7)]"
          animate={{ opacity: [1, 0.4, 1] }}
          transition={{ duration: 1.4, repeat: Infinity, ease: "easeInOut" }}
        />
        LIVE
      </span>
    );
  }
  return (
    <span className="ml-2 inline-flex items-center gap-1 rounded-full bg-amber-500/15 px-2 py-0.5 text-[10px] font-mono uppercase tracking-widest text-amber-300 ring-1 ring-amber-500/40">
      DEMO MODE
    </span>
  );
}

// ============================================================
// Source banner - tells the user why they're in DEMO vs LIVE
// and how to switch
// ============================================================

function SourceBanner({
  connected,
  source,
}: {
  connected: boolean;
  source: "demo" | "real";
}) {
  // Three states:
  //   - not connected     -> amber: "go to Dashboard and connect"
  //   - connected + demo  -> red:   "ECU silent / wrong protocol"
  //   - connected + real  -> green: "live data flowing"

  if (source === "real" && connected) {
    return (
      <motion.div
        initial={{ opacity: 0, y: -6 }}
        animate={{ opacity: 1, y: 0  }}
        transition={{ duration: 0.25 }}
        className="flex items-center gap-3 rounded-xl border border-emerald-500/40 bg-emerald-500/10 px-4 py-3 text-sm"
      >
        <CheckCircle2 className="h-5 w-5 shrink-0 text-emerald-400" />
        <div className="flex-1 min-w-0">
          <p className="font-medium text-emerald-200">
            กำลังรับข้อมูลจริงจาก ECU
          </p>
          <p className="text-xs text-emerald-200/70">
            TyN Shop K-Line protocol · TABLE_16 + TABLE_20 polling · ทุกค่าด้านล่างคือค่าจริง
          </p>
        </div>
      </motion.div>
    );
  }

  if (!connected) {
    return (
      <motion.div
        initial={{ opacity: 0, y: -6 }}
        animate={{ opacity: 1, y: 0  }}
        transition={{ duration: 0.25 }}
        className="flex items-center gap-3 rounded-xl border border-amber-500/40 bg-amber-500/10 px-4 py-3 text-sm"
      >
        <AlertCircle className="h-5 w-5 shrink-0 text-amber-400" />
        <div className="flex-1 min-w-0">
          <p className="font-medium text-amber-200">
            ยังไม่ได้เชื่อมต่อ ECM — ตอนนี้แสดงข้อมูลจำลอง (Simulator)
          </p>
          <p className="text-xs text-amber-200/70">
            ไปหน้า Dashboard กดปุ่ม <span className="font-mono">▶ เชื่อมต่อ</span> เพื่อรับข้อมูลจริงจาก ECU
          </p>
        </div>
        <Link
          href="/"
          className="inline-flex items-center gap-1.5 rounded-lg border border-amber-500/40 bg-amber-500/15 px-3 py-1.5 text-xs font-medium text-amber-100 transition hover:bg-amber-500/25"
        >
          <Power className="h-3.5 w-3.5" />
          ไปหน้า Dashboard
        </Link>
      </motion.div>
    );
  }

  // connected but ECU not responding → fallback to simulator
  return (
    <motion.div
      initial={{ opacity: 0, y: -6 }}
      animate={{ opacity: 1, y: 0  }}
      transition={{ duration: 0.25 }}
      className="flex items-center gap-3 rounded-xl border border-red-500/40 bg-red-500/10 px-4 py-3 text-sm"
    >
      <AlertCircle className="h-5 w-5 shrink-0 text-red-400" />
      <div className="flex-1 min-w-0">
        <p className="font-medium text-red-200">
          ECU ไม่ตอบสนอง — fallback มาที่ Simulator
        </p>
        <p className="text-xs text-red-200/70">
          เชื่อมต่อสายสำเร็จ แต่ ECU ไม่ส่งข้อมูล TABLE_16/TABLE_20 กลับมา ·
          ตรวจสอบ: <span className="font-mono">สวิตซ์รถ ON</span> · กล่อง ECU รุ่นที่รองรับโปรโตคอล TyN K-Line
        </p>
      </div>
    </motion.div>
  );
}

// ============================================================
// Big RPM gauge card - real tachometer style
// ============================================================

/** Map a 0..1 fraction of the gauge arc back to an angle. */
const RPM_START_ANGLE = 135;   // bottom-left
const RPM_END_ANGLE   = 405;   // bottom-right (= 45° past 360)
const RPM_SPAN        = RPM_END_ANGLE - RPM_START_ANGLE; // 270°
const fracToAngle = (f: number) => RPM_START_ANGLE + RPM_SPAN * Math.max(0, Math.min(1, f));

function RpmCard({
  value,
  history,
  min,
  max,
}: {
  value: number;
  history: number[];
  min: number;
  max: number;
}) {
  const cfg  = sensorConfig("rpm");
  const tone = toneFor(cfg, value);
  const frac = Math.max(0, Math.min(1, (value - cfg.min) / (cfg.max - cfg.min)));
  const needleAngle = fracToAngle(frac);

  // Major tick stops at 0, 3k, 6k, 9k, 12k, 15k.
  const STOPS = [0, 3000, 6000, 9000, 12000, 15000];

  // Color zones (frac of arc).
  const fracOf  = (rpm: number) => (rpm - cfg.min) / (cfg.max - cfg.min);
  const warnLo  = fracOf(9000);    // amber starts
  const dangerLo = fracOf(12000);  // red starts

  return (
    <Card className="relative overflow-hidden">
      <CardContent className="flex flex-col gap-4 p-5">
        {/* Header */}
        <div className="flex items-center gap-2">
          <Gauge className={cn("h-4 w-4", TONE_TEXT[tone])} />
          <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
            ENGINE RPM
          </p>
          <ToneDot tone={tone} className="ml-auto" />
        </div>

        {/* SVG gauge */}
        <div className="relative mx-auto aspect-square w-full max-w-[300px]">
          <svg viewBox="0 0 220 220" className="h-full w-full">
            <defs>
              {/* Subtle radial backdrop */}
              <radialGradient id="rpm-bg" cx="50%" cy="50%" r="50%">
                <stop offset="0%"   stopColor="hsl(var(--card))"          stopOpacity="0.4" />
                <stop offset="100%" stopColor="hsl(var(--background))"    stopOpacity="0" />
              </radialGradient>
              {/* Arc filled portion gradient */}
              <linearGradient id="rpm-fill" x1="0" y1="0" x2="1" y2="0">
                <stop offset="0%"  stopColor="hsl(var(--primary))" />
                <stop offset="60%" stopColor="hsl(var(--primary))" />
                <stop offset="80%" stopColor="#fbbf24" />
                <stop offset="100%" stopColor="#ef4444" />
              </linearGradient>
              {/* Glow used behind the active fill */}
              <filter id="rpm-glow" x="-30%" y="-30%" width="160%" height="160%">
                <feGaussianBlur stdDeviation="3" />
              </filter>
            </defs>

            {/* Backdrop disc */}
            <circle cx={110} cy={110} r={102} fill="url(#rpm-bg)" />
            <circle cx={110} cy={110} r={102} fill="none" className="stroke-border/60" strokeWidth={1} />

            {/* Outer track */}
            <Arc cx={110} cy={110} r={88} startAngle={RPM_START_ANGLE} endAngle={RPM_END_ANGLE} thick={14} className="stroke-muted/30" />

            {/* Color zones inside the track */}
            <Arc cx={110} cy={110} r={88}
              startAngle={RPM_START_ANGLE} endAngle={fracToAngle(warnLo)}
              thick={14} className="stroke-emerald-500/15" />
            <Arc cx={110} cy={110} r={88}
              startAngle={fracToAngle(warnLo)} endAngle={fracToAngle(dangerLo)}
              thick={14} className="stroke-amber-500/20" />
            <Arc cx={110} cy={110} r={88}
              startAngle={fracToAngle(dangerLo)} endAngle={RPM_END_ANGLE}
              thick={14} className="stroke-red-500/30" />

            {/* Active fill (glow) */}
            <Arc cx={110} cy={110} r={88}
              startAngle={RPM_START_ANGLE} endAngle={needleAngle}
              thick={14} stroke="url(#rpm-fill)" filter="url(#rpm-glow)" />
            {/* Active fill (sharp) */}
            <Arc cx={110} cy={110} r={88}
              startAngle={RPM_START_ANGLE} endAngle={needleAngle}
              thick={6} stroke="url(#rpm-fill)" />

            {/* Major + minor ticks */}
            {Array.from({ length: 31 }).map((_, i) => {
              const f = i / 30;
              const ang = fracToAngle(f);
              const isMajor = i % 6 === 0; // 0,6,12,18,24,30 → 0,3k,6k,9k,12k,15k
              const r1 = isMajor ? 72 : 78;
              const r2 = 82;
              const x1 = 110 + r1 * Math.cos((ang * Math.PI) / 180);
              const y1 = 110 + r1 * Math.sin((ang * Math.PI) / 180);
              const x2 = 110 + r2 * Math.cos((ang * Math.PI) / 180);
              const y2 = 110 + r2 * Math.sin((ang * Math.PI) / 180);
              return (
                <line
                  key={i}
                  x1={x1} y1={y1} x2={x2} y2={y2}
                  className={isMajor ? "stroke-foreground/80" : "stroke-muted-foreground/40"}
                  strokeWidth={isMajor ? 1.5 : 1}
                />
              );
            })}

            {/* Major tick labels */}
            {STOPS.map((rpm) => {
              const ang = fracToAngle(fracOf(rpm));
              const r   = 60;
              const x   = 110 + r * Math.cos((ang * Math.PI) / 180);
              const y   = 110 + r * Math.sin((ang * Math.PI) / 180);
              const isRedline = rpm >= 12000;
              const isWarn    = rpm >= 9000;
              return (
                <text
                  key={rpm}
                  x={x} y={y}
                  textAnchor="middle"
                  dominantBaseline="middle"
                  className={cn(
                    "font-mono fill-current",
                    isRedline ? "text-red-400" : isWarn ? "text-amber-300" : "text-foreground/80",
                  )}
                  fontSize={9}
                  fontWeight={isRedline ? 700 : 500}
                >
                  {rpm >= 1000 ? `${rpm / 1000}k` : rpm}
                </text>
              );
            })}

            {/* Needle */}
            <Needle cx={110} cy={110} angle={needleAngle} length={72}
              tone={tone} />

            {/* Hub */}
            <circle cx={110} cy={110} r={9}
              className={cn(
                tone === "danger" ? "fill-red-500" :
                tone === "warn"   ? "fill-amber-400" :
                                    "fill-primary",
              )}
            />
            <circle cx={110} cy={110} r={9} className="stroke-background/60" strokeWidth={1.5} fill="none" />
          </svg>

          {/* Center digital readout - sits in the lower void of the dial */}
          <div className="pointer-events-none absolute inset-x-0 bottom-2 flex flex-col items-center">
            <motion.span
              key={Math.round(value / 25) * 25}
              initial={{ opacity: 0.5, y: 4 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.18 }}
              className={cn("text-3xl font-bold leading-none tabular-nums tracking-tight", TONE_TEXT[tone])}
            >
              {Math.round(value).toLocaleString()}
            </motion.span>
            <span className="mt-0.5 font-mono text-[10px] uppercase tracking-widest text-muted-foreground">RPM</span>
          </div>
        </div>

        {/* Sparkline */}
        <Sparkline values={history} cfg={cfg} tone={tone} className="h-12" />

        {/* Min / Max footer */}
        <div className="grid grid-cols-2 gap-3 pt-1 text-xs">
          <div className="rounded-md border border-border/40 bg-background/40 px-3 py-1.5">
            <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">MIN RPM</p>
            <p className="mt-0.5 text-base font-semibold tabular-nums">{Math.round(min).toLocaleString()}</p>
          </div>
          <div className="rounded-md border border-border/40 bg-background/40 px-3 py-1.5 text-right">
            <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">MAX RPM</p>
            <p className="mt-0.5 text-base font-semibold tabular-nums">{Math.round(max).toLocaleString()}</p>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}

// ============================================================
// Compact sensor card
// ============================================================

function SensorCard({
  cfg,
  value,
  min,
  max,
  history,
}: {
  cfg: SensorConfig;
  value: number;
  min: number;
  max: number;
  history: number[];
}) {
  const tone = toneFor(cfg, value);
  const Icon = SENSOR_ICON[cfg.id];

  return (
    <motion.div
      whileHover={{ y: -2 }}
      transition={{ type: "spring", stiffness: 300, damping: 24 }}
    >
      <Card className={cn("relative overflow-hidden ring-1", TONE_BG[tone])}>
        <CardContent className="space-y-3 p-4">
          {/* Header */}
          <div className="flex items-center gap-2">
            <Icon className={cn("h-4 w-4", TONE_TEXT[tone])} />
            <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
              {cfg.label}
            </p>
            <ToneDot tone={tone} className="ml-auto" />
          </div>

          {/* Value */}
          <div className="flex items-baseline justify-center gap-1 py-2">
            <AnimatePresence mode="popLayout">
              <motion.span
                key={Math.round(value * 100)}
                initial={{ opacity: 0, y: 4 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{    opacity: 0, y: -4 }}
                transition={{ duration: 0.15 }}
                className={cn("text-3xl font-bold tabular-nums", TONE_TEXT[tone])}
              >
                {formatSensor(cfg, value)}
              </motion.span>
            </AnimatePresence>
            <span className={cn("text-base font-bold", TONE_TEXT[tone])}>{cfg.unit}</span>
          </div>

          {/* Sparkline */}
          <Sparkline values={history} cfg={cfg} tone={tone} className="h-8" />

          {/* Min / Max footer */}
          <div className="flex items-center justify-between pt-1 font-mono text-[10px] text-muted-foreground">
            <span>min {formatSensor(cfg, min)}</span>
            <span>max {formatSensor(cfg, max)}</span>
          </div>
        </CardContent>
      </Card>
    </motion.div>
  );
}

// ============================================================
// SVG primitives
// ============================================================

function ToneDot({ tone, className }: { tone: Tone; className?: string }) {
  const cls =
    tone === "danger" ? "bg-red-400 shadow-[0_0_6px_rgba(248,113,113,0.7)]" :
    tone === "warn"   ? "bg-amber-400 shadow-[0_0_6px_rgba(251,191,36,0.7)]" :
                        "bg-emerald-400 shadow-[0_0_6px_rgba(52,211,153,0.7)]";
  return <span className={cn("h-1.5 w-1.5 rounded-full", cls, className)} />;
}

/** Render a path that traces an arc on a circle. Optionally takes
 *  `stroke` / `filter` for paint customisation, and a `thick` value
 *  for stroke width. */
function Arc({
  cx, cy, r,
  startAngle, endAngle,
  className,
  thick = 6,
  stroke,
  filter,
}: {
  cx: number;
  cy: number;
  r: number;
  startAngle: number;
  endAngle:   number;
  className?: string;
  thick?: number;
  stroke?: string;
  filter?: string;
}) {
  if (Math.abs(endAngle - startAngle) < 0.001) return null;
  const sx = cx + r * Math.cos((startAngle * Math.PI) / 180);
  const sy = cy + r * Math.sin((startAngle * Math.PI) / 180);
  const ex = cx + r * Math.cos((endAngle   * Math.PI) / 180);
  const ey = cy + r * Math.sin((endAngle   * Math.PI) / 180);
  const sweep = endAngle - startAngle;
  const largeArc = Math.abs(sweep) > 180 ? 1 : 0;
  const path = `M ${sx} ${sy} A ${r} ${r} 0 ${largeArc} 1 ${ex} ${ey}`;
  return (
    <path
      d={path}
      fill="none"
      strokeWidth={thick}
      strokeLinecap="round"
      className={className}
      stroke={stroke}
      filter={filter}
    />
  );
}

function Needle({
  cx,
  cy,
  angle,
  length,
  tone = "ok",
}: {
  cx: number;
  cy: number;
  angle: number;
  length: number;
  tone?: Tone;
}) {
  const ex = cx + length * Math.cos((angle * Math.PI) / 180);
  const ey = cy + length * Math.sin((angle * Math.PI) / 180);
  // Tail in the opposite direction (counterweight visual).
  const tx = cx - 14 * Math.cos((angle * Math.PI) / 180);
  const ty = cy - 14 * Math.sin((angle * Math.PI) / 180);
  const stroke =
    tone === "danger" ? "stroke-red-400" :
    tone === "warn"   ? "stroke-amber-300" :
                        "stroke-foreground";
  return (
    <motion.g
      style={{ originX: `${cx}px`, originY: `${cy}px` }}
      animate={{ rotate: 0 }}
      transition={{ type: "spring", stiffness: 80, damping: 12 }}
    >
      {/* Glow / shadow */}
      <line
        x1={tx} y1={ty} x2={ex} y2={ey}
        strokeWidth={6}
        strokeLinecap="round"
        className={cn(stroke, "opacity-30")}
      />
      <motion.line
        x1={tx} y1={ty}
        animate={{ x2: ex, y2: ey }}
        transition={{ type: "spring", stiffness: 80, damping: 12 }}
        strokeWidth={2.4}
        strokeLinecap="round"
        className={stroke}
      />
    </motion.g>
  );
}

function Sparkline({
  values,
  cfg,
  tone,
  className,
}: {
  values: number[];
  cfg: SensorConfig;
  tone: Tone;
  className?: string;
}) {
  const data = useMemo(() => values.slice(-60), [values]);
  if (data.length < 2) {
    return <div className={cn("h-1 rounded-full bg-muted/40", className)} />;
  }
  // Map value → 0..1, then to viewBox y (inverted because SVG y grows down).
  const W = 200;
  const H = 40;
  const denom = cfg.max - cfg.min || 1;
  const points = data
    .map((v, i) => {
      const x = (i / (data.length - 1)) * W;
      const y = H - ((v - cfg.min) / denom) * H;
      return `${x.toFixed(1)},${y.toFixed(1)}`;
    })
    .join(" ");
  // Area path - close along the bottom for a fill.
  const area = `M 0 ${H} L ${points.split(" ").join(" L ")} L ${W} ${H} Z`;
  return (
    <svg viewBox={`0 0 ${W} ${H}`} preserveAspectRatio="none" className={cn("w-full", className)}>
      <defs>
        <linearGradient id={`spark-${cfg.id}-${tone}`} x1="0" y1="0" x2="0" y2="1">
          <stop offset="0%" stopColor="currentColor" stopOpacity="0.45" />
          <stop offset="100%" stopColor="currentColor" stopOpacity="0" />
        </linearGradient>
      </defs>
      <path d={area} className={cn("fill-current", TONE_TEXT[tone])} fill={`url(#spark-${cfg.id}-${tone})`} />
      <polyline points={points} fill="none" strokeWidth={1.5} strokeLinecap="round" strokeLinejoin="round" className={cn(TONE_LINE[tone])} />
    </svg>
  );
}

