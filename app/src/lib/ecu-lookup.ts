'use client';

/**
 * ECU-database lookup helper.
 *
 * Loads every `(family, ecm_id, part_code)` entry from `data.ini`
 * once at app boot (via `tauri.listEcus()`) and exposes a fast
 * O(1) lookup keyed by 5-byte ECM signature. Used by the dashboard
 * to surface the matched part code right under the live "ECM ID"
 * row, and by future flash flows to auto-fill `start_offset` /
 * `cksum_offset` so the operator no longer has to look them up by
 * hand.
 *
 * The ECM-id strings in `data.ini` are 10 hex characters (no
 * spaces), e.g. `0104080F01` for the K2TA-T02 LEAD125 entry. The
 * `read_ecm_id` Tauri command returns the same shape so we can
 * compare uppercase-hex strings directly.
 */

import { create } from 'zustand';
import { tauri, type EcuEntry } from '@/lib/tauri';

interface EcuLookupState {
  entries: EcuEntry[];
  byEcmId: Record<string, EcuEntry>;
  loading: boolean;
  error: string | null;
  /** Force a fresh load. Returns the new entry list. */
  refresh: () => Promise<EcuEntry[]>;
  /**
   * Best-effort match for a 5-byte / 10-hex ECM signature.
   * Accepts either `"0104080F01"` (`read_ecm_id` style) or any
   * spaced/lowercase variant - we normalise before lookup.
   * Returns `null` when nothing matches.
   */
  lookupByEcmId: (ecmId: string | null | undefined) => EcuEntry | null;
}

function normalise(id: string): string {
  return id.replace(/[^0-9a-fA-F]/g, '').toUpperCase();
}

export const useEcuLookup = create<EcuLookupState>((set, get) => ({
  entries: [],
  byEcmId: {},
  loading: false,
  error: null,

  async refresh() {
    set({ loading: true, error: null });
    try {
      const list = await tauri.listEcus();
      // Build a flat map for O(1) lookup. Multiple entries can share
      // the same ECM-id across families (rare but possible) - keep
      // the *first* match because the database is already sorted by
      // family then numeric id, which lines up with the lookup
      // priority operators expect.
      const byEcmId: Record<string, EcuEntry> = {};
      for (const e of list) {
        const key = normalise(e.ecm_id);
        if (key && !byEcmId[key]) byEcmId[key] = e;
      }
      set({ entries: list, byEcmId, loading: false });
      return list;
    } catch (e) {
      set({ loading: false, error: String(e) });
      return [];
    }
  },

  lookupByEcmId(ecmId) {
    if (!ecmId) return null;
    return get().byEcmId[normalise(ecmId)] ?? null;
  },
}));
