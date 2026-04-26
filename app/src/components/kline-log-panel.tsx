"use client";

/**
 * Live K-Line traffic console.
 *
 * Reads from the global `useKlineLog` store (fed by `<KlineLogBridge>`
 * which subscribes to `kline-log` Tauri events ONCE in the root layout).
 * This decouples the visual panel from the underlying event stream so
 * lines persist across page navigation.
 */

import { useEffect, useRef, useState } from "react";
import { ArrowDown, ArrowUp, CircleAlert, Eraser, Info, Pause, Play, Terminal } from "lucide-react";
import { isTauri, type KlineLogLine } from "@/lib/tauri";
import { useKlineLog } from "@/lib/log";
import { Card, CardContent } from "@/components/ui/card";
import { cn } from "@/lib/utils";

const MAX_LINES = 800;

export function KlineLogPanel() {
  const lines     = useKlineLog((s) => s.lines);
  const paused    = useKlineLog((s) => s.paused);
  const setPaused = useKlineLog((s) => s.setPaused);
  const clear     = useKlineLog((s) => s.clear);

  const [showTauri, setShowTauri] = useState(false);
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    setShowTauri(isTauri);
  }, []);

  // Auto-scroll to bottom whenever new lines arrive (unless paused).
  useEffect(() => {
    if (!paused && scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [lines, paused]);

  return (
    <Card className="flex h-full flex-col overflow-hidden">
      <CardContent className="flex flex-1 flex-col gap-2 p-3">
        {/* Header */}
        <div className="flex items-center gap-2">
          <Terminal className="h-4 w-4 text-primary" />
          <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
            K-Line Traffic Log
          </p>
          <span className="ml-auto inline-flex items-center gap-1 text-[10px] text-muted-foreground">
            <span className={cn("inline-block h-1.5 w-1.5 rounded-full",
              paused ? "bg-amber-500"
                     : showTauri ? "bg-emerald-500 shadow-[0_0_6px_rgba(16,185,129,0.7)]"
                                 : "bg-muted-foreground/60")} />
            {paused ? "หยุดชั่วคราว" : showTauri ? "live" : "browser"}
            <span className="ml-1.5 font-mono">{lines.length}/{MAX_LINES}</span>
          </span>
          <button
            type="button"
            onClick={() => setPaused(!paused)}
            title={paused ? "Resume" : "Pause"}
            className="rounded p-1 text-muted-foreground transition hover:bg-accent hover:text-foreground"
          >
            {paused ? <Play className="h-3.5 w-3.5" /> : <Pause className="h-3.5 w-3.5" />}
          </button>
          <button
            type="button"
            onClick={clear}
            title="Clear"
            className="rounded p-1 text-muted-foreground transition hover:bg-accent hover:text-foreground"
          >
            <Eraser className="h-3.5 w-3.5" />
          </button>
        </div>

        {/* Lines */}
        <div
          ref={scrollRef}
          className="custom-scrollbar h-full min-h-[180px] overflow-y-auto rounded-lg border border-border/40 bg-black/40 p-2 font-mono text-[11px] leading-relaxed"
        >
          {lines.length === 0 ? (
            <p className="px-2 py-1 text-muted-foreground/70">
              {showTauri
                ? "ยังไม่มี traffic — กด ▶ เชื่อมต่อ หรือเริ่มอ่าน EEPROM"
                : "Browser dev mode — เปิดผ่าน `npm run tauri:dev` เพื่อเห็น traffic จริง"}
            </p>
          ) : (
            lines.map((l, i) => <LogRow key={`${l.ts}-${i}`} line={l} />)
          )}
        </div>
      </CardContent>
    </Card>
  );
}

function LogRow({ line }: { line: KlineLogLine }) {
  const t = new Date(line.ts);
  const hh = t.getHours().toString().padStart(2, "0");
  const mm = t.getMinutes().toString().padStart(2, "0");
  const ss = t.getSeconds().toString().padStart(2, "0");
  const ms = t.getMilliseconds().toString().padStart(3, "0");

  const palette =
    line.dir === "tx"   ? { Icon: ArrowUp,    cls: "text-sky-400",     label: "TX" } :
    line.dir === "rx"   ? { Icon: ArrowDown,  cls: "text-emerald-400", label: "RX" } :
    line.dir === "err"  ? { Icon: CircleAlert,cls: "text-red-400",     label: "ER" } :
                          { Icon: Info,       cls: "text-amber-300",   label: "..." };
  const { Icon, cls, label } = palette;

  return (
    <div className="flex items-start gap-2 rounded px-2 py-0.5 hover:bg-white/5">
      <span className="shrink-0 text-muted-foreground/60">
        {hh}:{mm}:{ss}.{ms}
      </span>
      <Icon className={cn("mt-0.5 h-3 w-3 shrink-0", cls)} />
      <span className={cn("shrink-0 font-bold", cls)}>{label}</span>
      {line.dir === "tx" || line.dir === "rx" ? (
        <>
          <span className="shrink-0 text-muted-foreground/60">[{line.len}B]</span>
          <span className="break-all text-foreground/90">{line.hex}</span>
        </>
      ) : (
        <span className="break-all text-foreground/80">{line.msg}</span>
      )}
    </div>
  );
}
