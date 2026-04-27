'use client';

/**
 * `<LiveDataPump>` is the single driver that ticks the live-data
 * source and pushes samples into the global `useLiveData` store.
 *
 * Mode selection:
 *   - **Real**: when `useConnection.status === "connected"`, hold a
 *     persistent K-Line session open via `livedataStart` and poll
 *     TABLE_16 / TABLE_20 every tick with `livedataPoll`. Parsing
 *     uses the ADX equations from `TynShop.adx`.
 *   - **Demo (simulator)**: when not connected, fall back to the
 *     deterministic simulator so the UI still has something to show.
 *
 * Mounted once in the root layout so the navbar battery + livedata
 * page share the same stream. Re-opens the session whenever the
 * selected device or connection state changes, and stops it cleanly
 * when the user disconnects.
 */

import { useEffect } from 'react';
import { parseTynLiveData, simulateSample, useLiveData } from '@/lib/livedata';
import { useConnection } from '@/lib/connection';
import { useSettings } from '@/lib/settings';
import { isTauri, tauri } from '@/lib/tauri';

let pumpRunning = false;

export function LiveDataPump() {
  const intervalMs = useLiveData((s) => s.intervalMs);
  const running = useLiveData((s) => s.running);
  // Re-key the pump effect on the connected device so we re-open the
  // session whenever the user picks a different FTDI or reconnects.
  const sessionKey = useConnection((s) => {
    const d = s.selectedDevice();
    return s.status === 'connected' && d && d.backend !== 'bridge'
      ? `${d.backend}:${d.index}`
      : '';
  });

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
    // Whether we've successfully opened the persistent session for the
    // currently-selected device. Reset when sessionKey changes.
    let sessionOpen = false;

    const tryStartSession = async () => {
      const conn = useConnection.getState();
      const dev = conn.selectedDevice();
      if (
        !isTauri ||
        conn.status !== 'connected' ||
        !dev ||
        dev.backend === 'bridge'
      ) {
        return false;
      }
      try {
        await tauri.livedataStart(dev.index, dev.backend);
        sessionOpen = true;
        return true;
      } catch {
        sessionOpen = false;
        return false;
      }
    };

    /** Body of one tick. Factored out so its early `return`s only exit
     *  the poll, never the outer `tick` (otherwise the loop dies). */
    const runOnePoll = async () => {
      const state = useLiveData.getState();
      const conn = useConnection.getState();
      const dev = conn.selectedDevice();
      const wantReal = isTauri && conn.status === 'connected' && !!dev;

      if (!wantReal || !dev) {
        state.pushSample(simulateSample());
        if (state.source !== 'demo') state.setSource('demo');
        return;
      }

      try {
        let r: { table16: number[]; table20: number[] };
        if (dev.backend === 'bridge') {
          // Bridge devices: one-shot RPC to the daemon. The daemon
          // opens its own FTDI, runs the full poll cycle and closes
          // per call (no persistent session yet) so each tick costs
          // ~1 s end-to-end.
          const url = useSettings.getState().bridgeUrl.trim();
          if (!url) {
            // Bridge selected but URL missing - the user has the
            // settings tab to fix this. Fall back to simulator.
            state.pushSample(simulateSample());
            if (state.source !== 'demo') state.setSource('demo');
            return;
          }
          r = await tauri.readLiveSampleViaBridge(
            url,
            dev.index,
            dev.daemon_backend ?? 'd2xx',
          );
        } else {
          // Local d2xx / libusb: persistent-session path.
          if (!sessionOpen) await tryStartSession();
          if (!sessionOpen) {
            // Couldn't open the session this tick - keep the UI alive
            // with simulator output and retry next tick.
            state.pushSample(simulateSample());
            if (state.source !== 'demo') state.setSource('demo');
            return;
          }
          r = await tauri.livedataPoll();
        }

        if (r.table16.length > 0 || r.table20.length > 0) {
          state.pushSample(
            parseTynLiveData(r.table16, r.table20, state.values),
          );
          if (state.source !== 'real') state.setSource('real');
        } else {
          // ECU silent - back off to simulator so values still move.
          state.pushSample(simulateSample());
          if (state.source !== 'demo') state.setSource('demo');
        }
      } catch {
        // Session / RPC blew up (FTDI yanked, USB reset, daemon
        // unreachable, etc.). Drop the local session so the next tick
        // re-opens cleanly; bridge is stateless so nothing to clean up.
        if (sessionOpen) {
          sessionOpen = false;
          try {
            await tauri.livedataStop();
          } catch {
            /* ignore */
          }
        }
        state.pushSample(simulateSample());
        if (state.source !== 'demo') state.setSource('demo');
      }
    };

    const tick = async () => {
      if (cancelled) return;

      if (useLiveData.getState().running && !inFlight) {
        inFlight = true;
        try {
          await runOnePoll();
        } finally {
          inFlight = false;
        }
      }

      if (!cancelled) {
        timer = setTimeout(tick, useLiveData.getState().intervalMs);
      }
    };

    timer = setTimeout(tick, intervalMs);

    return () => {
      cancelled = true;
      if (timer) clearTimeout(timer);
      pumpRunning = false;
      // Best-effort cleanup so the FTDI handle is freed when the page
      // unmounts or the device changes. The backend tolerates calling
      // stop on an already-closed session.
      if (sessionOpen) {
        sessionOpen = false;
        if (isTauri) {
          tauri.livedataStop().catch(() => {
            /* ignore */
          });
        }
      }
    };
  }, [intervalMs, running, sessionKey]);

  return null;
}
