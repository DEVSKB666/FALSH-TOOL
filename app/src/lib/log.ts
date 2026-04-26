"use client";

/**
 * Global K-Line traffic log store.
 *
 * Lines are accumulated here at the module level so they survive page
 * navigation (the panel UI may unmount and remount, but the data stays).
 * A single subscriber in `<KlineLogBridge>` (mounted once in the root
 * layout) feeds events from the Tauri backend into this store.
 */

import { create } from "zustand";
import type { KlineLogLine, KlineProgress } from "@/lib/tauri";

const MAX_LINES = 800;

interface LogState {
  lines:    KlineLogLine[];
  paused:   boolean;
  /** Latest progress event (used by the long-running ops UI). */
  progress: KlineProgress | null;

  add:        (line: KlineLogLine) => void;
  setPaused:  (p: boolean) => void;
  clear:      () => void;
  setProgress: (p: KlineProgress | null) => void;
}

export const useKlineLog = create<LogState>((set, get) => ({
  lines:    [],
  paused:   false,
  progress: null,

  add: (line) => {
    if (get().paused) return;
    set((state) => {
      const next = [...state.lines, line];
      return { lines: next.length > MAX_LINES ? next.slice(-MAX_LINES) : next };
    });
  },

  setPaused: (p)    => set({ paused: p }),
  clear:     ()     => set({ lines: [] }),
  setProgress: (p)  => set({ progress: p }),
}));
