'use client';

/**
 * Connection state machine + auto-connect logic.
 *
 *   const { status, ports, connect, autoConnect } = useConnection();
 *
 * Each FTDI device is uniquely identified by the `(backend, index)` pair:
 * the same `index` may appear in both the D2XX and libusb backends, so
 * we always carry the backend tag along.
 */

import { create } from 'zustand';
import { tauri, type FtdiDevice } from '@/lib/tauri';
import { useSettings } from '@/lib/settings';

/** Read the current bridge config straight from the settings store.
 *  Bridge is "active" if it's enabled AND a non-empty URL is set. */
function bridgeConfig(): { active: boolean; url: string } {
  const s = useSettings.getState();
  return {
    active: s.bridgeEnabled && s.bridgeUrl.trim().length > 0,
    url: s.bridgeUrl.trim(),
  };
}

export type ConnStatus =
  | 'idle'
  | 'scanning'
  | 'connecting'
  | 'connected'
  | 'error';

/** Encode a (backend, index) pair as a single dropdown-friendly string. */
export const portKey = (p: Pick<FtdiDevice, 'backend' | 'index'>) =>
  `${p.backend}:${p.index}`;

export interface ConnectionState {
  status: ConnStatus;
  ports: FtdiDevice[];
  /** Composite key "<backend>:<index>" of the selected device, or null. */
  selectedKey: string | null;
  /** Free-form ECU descriptor once connected (e.g. "0100DF0002"). */
  ecuId: string | null;
  /** Cumulative successful flashes. */
  flashCount: number;
  /** Last error message (cleared on next connect attempt). */
  error: string | null;

  scanPorts: () => Promise<FtdiDevice[]>;
  connect: (key?: string) => Promise<{ ok: boolean; reason?: string }>;
  disconnect: () => void;
  autoConnect: () => Promise<{ ok: boolean; reason?: string }>;
  selectKey: (key: string) => void;
  /** Manually populate the ECM ID. Used by the Home page after the
   *  ECU returns a successful WAKEUP+ESTABLISH reply. */
  setEcuId: (id: string | null) => void;
  /** Fire WAKEUP+ESTABLISH against the currently selected port and
   *  push the resulting 5-byte signature into `ecuId`. Best-effort -
   *  silently noops if the connection isn't active. */
  refreshEcuId: () => Promise<void>;
  /** Convenience: lookup the FtdiDevice for the current `selectedKey`. */
  selectedDevice: () => FtdiDevice | null;
}

export const useConnection = create<ConnectionState>((set, get) => ({
  status: 'idle',
  ports: [],
  selectedKey: null,
  ecuId: null,
  flashCount: 0,
  error: null,

  selectKey: (key) => set({ selectedKey: key }),

  selectedDevice: () => {
    const { ports, selectedKey } = get();
    if (!selectedKey) return null;
    return ports.find((p) => portKey(p) === selectedKey) ?? null;
  },

  async scanPorts() {
    set({ status: 'scanning', error: null });
    try {
      const cfg = bridgeConfig();
      // When the bridge is enabled, ask it for its FTDI list instead
      // of probing the local USB stack. Cheaper than juggling two
      // device pools and the user only ever cares about one source.
      const ports = cfg.active
        ? await tauri.listFtdiViaBridge(cfg.url)
        : await tauri.listFtdi();
      set({
        ports,
        status: 'idle',
        // Auto-pick the first port if nothing selected yet.
        selectedKey: get().selectedKey ?? (ports[0] ? portKey(ports[0]) : null),
      });
      return ports;
    } catch (e) {
      set({ status: 'error', error: String(e) });
      return [];
    }
  },

  async connect(key) {
    const target =
      key ??
      get().selectedKey ??
      (get().ports[0] ? portKey(get().ports[0]) : null);
    if (!target) {
      set({ status: 'error', error: 'no FTDI port available' });
      return { ok: false, reason: 'ไม่พบพอร์ต FTDI' };
    }
    set({ status: 'connecting', error: null, selectedKey: target });
    try {
      const cfg = bridgeConfig();

      if (cfg.active) {
        // Bridge mode - liveness check is enough to call ourselves
        // "connected"; the daemon owns the FTDI handle on the other
        // side and any failure surfaces on the next op.
        const ports = await tauri.listFtdiViaBridge(cfg.url);
        const found = ports.find((p) => portKey(p) === target);
        if (!found) {
          set({ status: 'error', error: 'port disappeared during connect' });
          return { ok: false, reason: 'พอร์ตหายไประหว่างเชื่อมต่อ' };
        }
        await tauri.bridgePing(cfg.url);
        set({ status: 'connected', ports, ecuId: null });
        return { ok: true };
      }

      // Local mode (default) - run the real K-Line handshake via Tauri.
      const ports = await tauri.listFtdi();
      const found = ports.find((p) => portKey(p) === target);
      if (!found) {
        set({ status: 'error', error: 'port disappeared during connect' });
        return { ok: false, reason: 'พอร์ตหายไประหว่างเชื่อมต่อ' };
      }
      // Local `list_ftdi` only ever returns d2xx/libusb backends, but
      // the union widened type-side to include "bridge", so narrow
      // explicitly before calling the local-only Tauri command.
      if (found.backend === 'bridge') {
        set({
          status: 'error',
          error: 'bridge port leaked into local-mode connect',
        });
        return { ok: false, reason: 'internal: bridge port in local mode' };
      }
      await tauri.klineTest(found.index, found.backend);
      set({ status: 'connected', ports, ecuId: null });
      return { ok: true };
    } catch (e) {
      set({ status: 'error', error: String(e) });
      return { ok: false, reason: String(e) };
    }
  },

  disconnect() {
    set({ status: 'idle', ecuId: null });
  },

  setEcuId(id) {
    set({ ecuId: id });
  },

  async refreshEcuId() {
    const { status, selectedDevice } = get();
    if (status !== 'connected') return;
    const dev = selectedDevice();
    if (!dev) return;
    const cfg = bridgeConfig();
    try {
      // Pick the right transport: bridge mode → daemon-side RPC,
      // local mode → direct Tauri command. Either way we get the
      // same `{ ecm_id, raw_hex }` shape back.
      const res =
        cfg.active && dev.backend === 'bridge'
          ? await tauri.readEcmIdViaBridge(
              cfg.url,
              dev.index,
              (dev.daemon_backend ?? 'd2xx') as 'd2xx' | 'libusb',
            )
          : dev.backend !== 'bridge'
            ? await tauri.readEcmId(dev.index, dev.backend)
            : null;
      if (!res) return;
      if (res.ecm_id) {
        set({ ecuId: res.ecm_id });
      } else if (res.raw_hex) {
        // Fall back to the raw hex so the user at least sees *something*
        // in the "ECM ID" row even when our 5-byte heuristic missed.
        set({ ecuId: res.raw_hex });
      }
    } catch {
      /* best effort - don't surface to the UI */
    }
  },

  async autoConnect() {
    const ports = await get().scanPorts();
    if (ports.length === 0) {
      return { ok: false, reason: 'ไม่พบสาย FTDI' };
    }
    if (ports.length > 1) {
      return { ok: false, reason: `พบ ${ports.length} พอร์ต กรุณาเลือกเอง` };
    }
    return get().connect(portKey(ports[0]));
  },
}));
