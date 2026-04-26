"use client";

/**
 * `<LiveDataPump>` is the single driver that ticks the live-data
 * source and pushes samples into the global `useLiveData` store.
 *
 * Mode selection:
 *   - **Real**: when `useConnection.status === "connected"`, query
 *     the ECU via the `read_live_sample` Tauri command (TyN Shop K-Line
 *     protocol from `TynShop.adx`). Parse the raw bytes with the
 *     ADX equations.
 *   - **Demo (simulator)**: when not connected, fall back to the
 *     deterministic simulator so the UI still has something to show.
 *
 * Mounted once in the root layout so the navbar battery + livedata
 * page share the same stream.
 */

import { useEffect } from "react";
import { parseTynLiveData, simulateSample, useLiveData } from "@/lib/livedata";
import { useConnection } from "@/lib/connection";
import { isTauri, tauri } from "@/lib/tauri";

let pumpRunning = false;

export function LiveDataPump() {
  const intervalMs = useLiveData((s) => s.intervalMs);
  const running    = useLiveData((s) => s.running);

  useEffect(() => {
    // Module-level guard so React StrictMode dev-double-mounting can't
    // create two pumps writing twice the samples.
    if (pumpRunning) return;
    pumpRunning = true;

    let cancelled = false;
    let timer: ReturnType<typeof setTimeout> | null = null;
    // Avoid concurrent in-flight Tauri calls (a slow K-Line poll could
    // overlap the next tick at high refresh rates).
    let inFlight = false;

    const tick = async () => {
      if (cancelled) return;
      const state = useLiveData.getState();

      if (state.running && !inFlight) {
        inFlight = true;
        try {
          // Use real data if connected to a port and we're in Tauri.
          const conn = useConnection.getState();
          const dev  = conn.selectedDevice();
          // Bridge devices can't be polled by the local Tauri command
          // (it would open the *local* FTDI instead of forwarding the
          // poll). Until we add a bridge-side live-data RPC, fall back
          // to the simulator so the user still sees moving values
          // without spamming "[d2xx] opening device" in the K-Line log.
          const useReal =
            isTauri &&
            conn.status === "connected" &&
            !!dev &&
            dev.backend !== "bridge";

          if (useReal && dev && dev.backend !== "bridge") {
            try {
              const r = await tauri.readLiveSample(dev.index, dev.backend);
              if ((r.table16.length > 0) || (r.table20.length > 0)) {
                state.pushSample(parseTynLiveData(r.table16, r.table20, state.values));
                if (state.source !== "real") state.setSource("real");
              } else {
                // ECU silent - back off to simulator so values still move.
                state.pushSample(simulateSample());
                if (state.source !== "demo") state.setSource("demo");
              }
            } catch {
              // Backend error (port busy, USB hiccup, etc.) - drop this
              // tick; we'll try again on the next interval.
              state.pushSample(simulateSample());
              if (state.source !== "demo") state.setSource("demo");
            }
          } else {
            // Not connected, or running through the bridge → simulator
            // stream. Bridge polling will be wired through a dedicated
            // `read_live_sample_via_bridge` command in a future update.
            state.pushSample(simulateSample());
            if (state.source !== "demo") state.setSource("demo");
          }
        } finally {
          inFlight = false;
        }
      }

      timer = setTimeout(tick, useLiveData.getState().intervalMs);
    };

    timer = setTimeout(tick, intervalMs);

    return () => {
      cancelled = true;
      if (timer) clearTimeout(timer);
      pumpRunning = false;
    };
  }, [intervalMs, running]);

  return null;
}
