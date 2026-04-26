"use client";

import { motion } from "framer-motion";
import {
  Cable,
  CheckCircle2,
  Loader2,
  Power,
  RefreshCw,
  Usb,
  XCircle,
} from "lucide-react";
import { useEffect, useRef } from "react";
import { useConnection, portKey } from "@/lib/connection";
import { useSettings } from "@/lib/settings";
import { sound } from "@/lib/sounds";
import { toast } from "@/components/toast";
import { cn } from "@/lib/utils";

// Module-level flag so the bootstrap (initial scan + auto-connect) runs
// at most once per JS module lifetime - i.e. once per app session.
// `useRef` would reset on every component remount which happens whenever
// the user navigates between pages, causing the auto-scan to wipe an
// already-established "connected" status.
let bootstrappedOnce = false;

/**
 * Persistent connection bar (lives in `<AppShell>`). Drives the global
 * `useConnection` store and announces every state change as a toast +
 * sound.
 */
export function ConnectionPanel() {
  const autoConnect = useSettings((s) => s.autoConnect);

  const status      = useConnection((s) => s.status);
  const ports       = useConnection((s) => s.ports);
  const selectedKey = useConnection((s) => s.selectedKey);
  const error       = useConnection((s) => s.error);

  const scanPorts  = useConnection((s) => s.scanPorts);
  const connect    = useConnection((s) => s.connect);
  const disconnect = useConnection((s) => s.disconnect);
  const autoFn     = useConnection((s) => s.autoConnect);
  const selectKey  = useConnection((s) => s.selectKey);

  // Bootstrap once per session. Skip entirely if we already have a live
  // connection (user navigated back to the home page).
  useEffect(() => {
    if (bootstrappedOnce) return;
    const snapshot = useConnection.getState();
    if (snapshot.status === "connected" || snapshot.ports.length > 0) {
      bootstrappedOnce = true;
      return;
    }
    bootstrappedOnce = true;

    let cancelled = false;
    (async () => {
      const found = await scanPorts();
      if (cancelled) return;
      if (found.length === 0) {
        toast.warning("ไม่พบอุปกรณ์ FTDI", "เสียบสายแล้วกด ⟳ เพื่อสแกนใหม่");
        return;
      }
      toast.info(
        `พบ ${found.length} พอร์ต`,
        found.map((p) => p.description).join(" · ")
      );
      if (autoConnect && found.length === 1) {
        const result = await autoFn();
        if (cancelled) return;
        if (result.ok) {
          sound.connect();
          toast.success("เชื่อมต่ออัตโนมัติ", `${found[0].description}`);
        } else if (result.reason) {
          toast.warning("Auto-connect ไม่สำเร็จ", result.reason);
        }
      }
    })();
    return () => { cancelled = true; };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // Re-scan whenever the bridge URL or enable toggle changes. This is
  // what swaps the port list from "local FTDI" to "what the bridge
  // sees" the moment the user flips the switch in Settings.
  const bridgeEnabled = useSettings((s) => s.bridgeEnabled);
  const bridgeUrl     = useSettings((s) => s.bridgeUrl);
  const lastBridgeRef = useRef<string>(`${bridgeEnabled}:${bridgeUrl}`);
  useEffect(() => {
    const key = `${bridgeEnabled}:${bridgeUrl}`;
    if (lastBridgeRef.current === key) return;          // initial mount
    lastBridgeRef.current = key;

    if (status === "connected") {
      // Bridge config changed under our feet - drop the old "connected"
      // flag because the previous handle was opened against a different
      // backend.
      disconnect();
    }
    let cancelled = false;
    (async () => {
      const found  = await scanPorts();
      if (cancelled) return;
      const errored = useConnection.getState();
      const where = bridgeEnabled && bridgeUrl ? `bridge ${bridgeUrl}` : "local FTDI";
      if (errored.status === "error" && errored.error) {
        toast.error(`${where} เชื่อมไม่ได้`, errored.error);
      } else if (found.length === 0) {
        toast.warning(
          "ไม่พบอุปกรณ์",
          `${where} ไม่ส่ง port กลับมา - เสียบสายแล้วลองกดสแกนอีกครั้ง`,
        );
      } else {
        toast.info(
          `พบ ${found.length} พอร์ต`,
          `${where} · ${found.map((p) => p.description).join(" · ")}`,
        );
      }
    })();
    return () => { cancelled = true; };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [bridgeEnabled, bridgeUrl]);

  async function handleScan() {
    sound.click();
    const found  = await scanPorts();
    const state  = useConnection.getState();
    const usingBridge = bridgeEnabled && bridgeUrl.trim().length > 0;

    if (state.status === "error" && state.error) {
      // scanPorts swallowed an exception - typically a bridge connect
      // failure. Surface that exact message instead of the generic one.
      toast.error(usingBridge ? "เชื่อม Bridge ไม่ได้" : "สแกนล้มเหลว", state.error);
      return;
    }
    if (found.length === 0) {
      if (usingBridge) {
        toast.warning(
          "Bridge ตอบ - แต่ไม่มี FTDI",
          `${bridgeUrl} ไม่เจอ FTDI - เช็คสายที่ notebook หรือ build daemon ด้วย --features mock เพื่อทดสอบ`,
        );
      } else {
        toast.warning("ไม่พบอุปกรณ์", "ตรวจสอบสาย FTDI ของคุณ");
      }
    } else {
      toast.info(`พบ ${found.length} พอร์ต`);
    }
  }

  async function handleConnect() {
    if (status === "connected") {
      sound.disconnect();
      disconnect();
      toast.info("ตัดการเชื่อมต่อแล้ว");
      return;
    }
    if (selectedKey === null) {
      sound.error();
      toast.error("ยังไม่ได้เลือกพอร์ต", "กด ⟳ เพื่อสแกน หรือเลือกพอร์ตจากรายการ");
      return;
    }
    const result = await connect(selectedKey);
    if (result.ok) {
      sound.connect();
      toast.success("เชื่อมต่อ ECM สำเร็จ");
    } else {
      sound.error();
      toast.error("เชื่อมต่อล้มเหลว", result.reason ?? error ?? "unknown");
    }
  }

  const connected = status === "connected";
  const busy      = status === "scanning" || status === "connecting";

  return (
    <div className="flex flex-wrap items-center gap-3 rounded-2xl border border-border/60 bg-card/70 p-3 ring-1 ring-black/10 backdrop-blur-md">
      {/* Status dot */}
      <div className="flex items-center gap-2 px-2">
        <StatusDot status={status} />
        <p className="font-mono text-[11px] uppercase tracking-widest text-muted-foreground">
          {labelFor(status)}
        </p>
      </div>

      {/* Port selector */}
      <div className="flex flex-1 items-center gap-2 rounded-lg border border-border/60 bg-background/40 px-3 py-2 min-w-[260px]">
        <Usb className="h-4 w-4 text-muted-foreground" />
        <select
          className="w-full appearance-none bg-transparent text-sm outline-none"
          value={selectedKey ?? ""}
          onChange={(e) => {
            const v = e.target.value;
            if (v) selectKey(v);
            sound.click();
          }}
          disabled={busy}
        >
          {ports.length === 0 && <option value="">— ไม่พบพอร์ต FTDI —</option>}
          {ports.map((p) => (
            <option key={portKey(p)} value={portKey(p)}>
              [{p.backend.toUpperCase()}] {p.description}
              {p.serial ? ` · ${p.serial}` : ""}
            </option>
          ))}
        </select>
      </div>

      {/* Scan */}
      <button
        type="button"
        onClick={handleScan}
        disabled={busy}
        className={cn(
          "inline-flex h-10 items-center gap-2 rounded-lg border border-border/60 bg-background/40 px-3 text-sm font-medium transition",
          "hover:bg-accent hover:text-foreground",
          busy && "opacity-60",
        )}
        title="สแกนพอร์ตอีกครั้ง"
      >
        <RefreshCw className={cn("h-4 w-4", status === "scanning" && "animate-spin")} />
        สแกน
      </button>

      {/* Connect / Disconnect */}
      <motion.button
        type="button"
        onClick={handleConnect}
        disabled={busy}
        whileTap={{ scale: 0.97 }}
        className={cn(
          "relative inline-flex h-10 min-w-[140px] items-center justify-center gap-2 overflow-hidden rounded-lg px-4 text-sm font-semibold transition",
          connected
            ? "bg-emerald-600 text-white shadow-[0_0_20px_rgba(16,185,129,0.45)] hover:bg-emerald-500"
            : "bg-primary text-primary-foreground shadow-[0_0_20px_hsl(var(--primary)/0.45)] hover:bg-primary/90",
          busy && "opacity-60",
        )}
      >
        {status === "connecting" ? (
          <>
            <Loader2 className="h-4 w-4 animate-spin" />
            กำลังเชื่อมต่อ…
          </>
        ) : connected ? (
          <>
            <CheckCircle2 className="h-4 w-4" />
            เชื่อมต่อแล้ว
          </>
        ) : (
          <>
            <Power className="h-4 w-4" />
            เชื่อมต่อ
          </>
        )}
      </motion.button>
    </div>
  );
}

function StatusDot({ status }: { status: ReturnType<typeof useConnection.getState>["status"] }) {
  const map: Record<string, string> = {
    idle:       "bg-muted-foreground/60",
    scanning:   "bg-amber-500 animate-pulse",
    connecting: "bg-amber-500 animate-pulse",
    connected:  "bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.7)]",
    error:      "bg-red-500 shadow-[0_0_8px_rgba(239,68,68,0.7)]",
  };
  return <span className={cn("h-2.5 w-2.5 rounded-full", map[status] ?? "bg-muted")} />;
}

function labelFor(s: string): string {
  switch (s) {
    case "idle":       return "พร้อมใช้งาน";
    case "scanning":   return "กำลังสแกน…";
    case "connecting": return "กำลังเชื่อมต่อ…";
    case "connected":  return "เชื่อมต่อแล้ว";
    case "error":      return "เกิดข้อผิดพลาด";
    default:           return s;
  }
}

/** Mini status pill for the top row. Reuses the same store. */
export function ConnectionPill() {
  const status = useConnection((s) => s.status);
  const connected = status === "connected";
  return (
    <div
      className={cn(
        "inline-flex items-center gap-2 rounded-full px-3 py-1 text-xs font-medium ring-1",
        connected
          ? "bg-emerald-500/15 text-emerald-300 ring-emerald-500/40"
          : status === "error"
          ? "bg-red-500/15 text-red-300 ring-red-500/40"
          : "bg-muted/40 text-muted-foreground ring-border/40",
      )}
    >
      <Cable className="h-3.5 w-3.5" />
      {labelFor(status)}
    </div>
  );
}
