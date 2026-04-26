"use client";

/**
 * Flash / EEPROM operation history.
 *
 * Persisted to localStorage so we can show "what did I do this week?"
 * across app restarts. New entries are pushed by `/ecu` whenever a
 * Read / Reset / Format / Dump completes.
 */

import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

export type HistoryStatus = "success" | "failed" | "cancelled";

export type HistoryOp = "read" | "dump" | "reset" | "format" | "write";

/** One row in the flash history table. */
export interface HistoryEntry {
  /** UUID-ish unique id (we just use ts + random suffix). */
  id: string;
  /** Unix-ms when the op completed. */
  timestamp: number;
  /** ECM signature, e.g. `0100DF0D02`. Optional - filled when known. */
  ecmId: string;
  /** ECU code / part code, e.g. `KVB-S53` or `38770-K2F-N01`. */
  ecuCode: string;
  /** Honda family. */
  family: "Keihin" | "Shinden";
  /** Operation kind. */
  operation: HistoryOp;
  /** Human-readable label (e.g. "READ EEPROM (Keihin)"). */
  label: string;
  /** Filename of the bin associated with this op (saved or generated). */
  binFile?: string;
  /** Number of bytes read/written. */
  bytes?: number;
  /** SHA-256 hex of the bin (cheap fingerprint). */
  hash?: string;
  /** End-to-end duration in ms. */
  durationMs: number;
  /** "เก็บไว้ใน Cache" or "บันทึกแล้ว". */
  storage: "cache" | "saved";
  /** Operation outcome. */
  status: HistoryStatus;
  /** Optional error / note. */
  note?: string;
  /** Flash counter from the ECU (only for write/reset where we read it). */
  flashCount?: number;
  /** Optional - the bytes captured (subset). Stored as small array; */
  preview?: number[];
}

export interface HistoryState {
  entries: HistoryEntry[];
  add: (e: Omit<HistoryEntry, "id" | "timestamp">) => HistoryEntry;
  remove: (id: string) => void;
  clear: () => void;
}

function newId(): string {
  // Cheap monotonic id - we don't need RFC4122-grade uniqueness here.
  return `${Date.now().toString(36)}-${Math.random().toString(36).slice(2, 8)}`;
}

const MAX_ENTRIES = 500;

export const useHistory = create<HistoryState>()(
  persist(
    (set) => ({
      entries: [],
      add: (e) => {
        const entry: HistoryEntry = { id: newId(), timestamp: Date.now(), ...e };
        set((state) => {
          const next = [entry, ...state.entries];
          return { entries: next.length > MAX_ENTRIES ? next.slice(0, MAX_ENTRIES) : next };
        });
        return entry;
      },
      remove: (id) => set((state) => ({ entries: state.entries.filter((e) => e.id !== id) })),
      clear: () => set({ entries: [] }),
    }),
    {
      name: "mza-tuner.history",
      storage: createJSONStorage(() => localStorage),
    },
  ),
);

// ---------------- Helpers ---------------------------------------------

/** Compute aggregate stats over a list of entries. */
export function statsOf(entries: HistoryEntry[]) {
  const total      = entries.length;
  const success    = entries.filter((e) => e.status === "success").length;
  const failed     = entries.filter((e) => e.status === "failed").length;
  const cancelled  = entries.filter((e) => e.status === "cancelled").length;
  const totalMs    = entries.reduce((acc, e) => acc + e.durationMs, 0);
  const ecmCount   = new Set(entries.map((e) => e.ecmId).filter(Boolean)).size;
  const successRate = total === 0 ? 0 : Math.round((success / total) * 1000) / 10;
  return { total, success, failed, cancelled, totalMs, ecmCount, successRate };
}

/** Format a relative time like "1 วันที่ผ่านมา" or "3 ชั่วโมง". */
export function relativeThai(ts: number, now: number = Date.now()): string {
  const diff = Math.max(0, now - ts);
  const sec = Math.floor(diff / 1000);
  if (sec < 60)            return `${sec} วินาทีที่ผ่านมา`;
  const min = Math.floor(sec / 60);
  if (min < 60)            return `${min} นาทีที่ผ่านมา`;
  const hr  = Math.floor(min / 60);
  if (hr  < 24)            return `${hr} ชั่วโมงที่ผ่านมา`;
  const day = Math.floor(hr / 24);
  if (day < 30)            return `${day} วันที่ผ่านมา`;
  const mo  = Math.floor(day / 30);
  if (mo  < 12)            return `${mo} เดือนที่ผ่านมา`;
  return `${Math.floor(mo / 12)} ปีที่ผ่านมา`;
}

/** Format a timestamp like `26/04/2026 21:00:46`. */
export function formatTs(ts: number): string {
  const d = new Date(ts);
  const dd = String(d.getDate()).padStart(2, "0");
  const mm = String(d.getMonth() + 1).padStart(2, "0");
  const yy = d.getFullYear();
  const hh = String(d.getHours()).padStart(2, "0");
  const mi = String(d.getMinutes()).padStart(2, "0");
  const ss = String(d.getSeconds()).padStart(2, "0");
  return `${dd}/${mm}/${yy} ${hh}:${mi}:${ss}`;
}

/** Format a duration like `26.40s` or `2:14`. */
export function formatDuration(ms: number): string {
  if (ms < 60_000) return `${(ms / 1000).toFixed(2)}s`;
  const totalSec = Math.floor(ms / 1000);
  const m = Math.floor(totalSec / 60);
  const s = totalSec % 60;
  return `${m}:${String(s).padStart(2, "0")}`;
}

/** SHA-256 hex of a byte array using the WebCrypto API. */
export async function sha256Hex(bytes: number[] | Uint8Array): Promise<string> {
  // Always copy into a fresh ArrayBuffer-backed view so SubtleCrypto
  // accepts the input under TS's strict typing (some Uint8Array views
  // are typed as ArrayBufferLike which is wider than ArrayBuffer).
  const fresh = new Uint8Array(bytes instanceof Uint8Array ? bytes : Array.from(bytes));
  const buf = await crypto.subtle.digest("SHA-256", fresh.buffer as ArrayBuffer);
  return Array.from(new Uint8Array(buf))
    .map((b) => b.toString(16).padStart(2, "0"))
    .join("");
}
