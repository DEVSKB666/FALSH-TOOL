"use client";

/**
 * `<KlineLogBridge>` is mounted once at the root layout. It subscribes
 * to the `kline-log` and `kline-progress` Tauri events and pumps them
 * into the global `useKlineLog` store. This decouples the listener from
 * any specific page so log lines survive navigation.
 *
 * Uses a module-level `bridged` flag to guard against React StrictMode's
 * double-mount in dev (refs are per-instance and would re-fire).
 */

import { useEffect } from "react";
import { isTauri, type KlineLogLine, type KlineProgress } from "@/lib/tauri";
import { useKlineLog } from "@/lib/log";

let bridged = false;

export function KlineLogBridge() {
  useEffect(() => {
    if (!isTauri || bridged) return;
    bridged = true;

    let cancelled = false;
    let unlistenLog:      (() => void) | null = null;
    let unlistenProgress: (() => void) | null = null;

    (async () => {
      const { listen } = await import("@tauri-apps/api/event");
      const add         = useKlineLog.getState().add;
      const setProgress = useKlineLog.getState().setProgress;

      const logFn = await listen<KlineLogLine>("kline-log", (e) => {
        add(e.payload);
      });
      const progFn = await listen<KlineProgress>("kline-progress", (e) => {
        setProgress(e.payload);
      });

      if (cancelled) {
        logFn();
        progFn();
        bridged = false;
      } else {
        unlistenLog      = logFn;
        unlistenProgress = progFn;
      }
    })();

    return () => {
      cancelled = true;
      if (unlistenLog)      unlistenLog();
      if (unlistenProgress) unlistenProgress();
      bridged = false;
    };
  }, []);

  return null;
}
