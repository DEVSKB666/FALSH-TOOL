'use client';

/**
 * Live ECU sensor stream.
 *
 * Backend wiring is not implemented yet (Honda K-Line live data needs
 * a Mode 22 PID poller that we haven't ported), so this module ships
 * with a deterministic-but-realistic simulator that produces idle-ish
 * values plus small organic noise. The UI driver doesn't care - it
 * subscribes to `useLiveData()` and gets updates regardless of source.
 */

import { create } from 'zustand';

/** Stable identifier for each sensor. */
export type SensorId =
  | 'rpm'
  | 'tps_deg'
  | 'tps_v'
  | 'ect'
  | 'o2'
  | 'afr'
  | 'ltft'
  | 'battery'
  | 'inj_ms'
  | 'ign_deg';

export interface SensorConfig {
  id: SensorId;
  label: string;
  unit: string;
  /** Display range for gauges / progress bars. */
  min: number;
  max: number;
  /** Decimal places when formatting. */
  digits: number;
  /** Idle baseline used by the simulator. */
  idle: number;
  /** Random noise amplitude per tick. */
  noise: number;
  /** Optional warning band (out-of-range → amber). */
  warn?: [number, number];
  /** Optional danger band (out-of-range → red). */
  danger?: [number, number];
}

export const SENSORS: SensorConfig[] = [
  {
    id: 'rpm',
    label: 'Engine RPM',
    unit: 'rpm',
    min: 0,
    max: 15000,
    digits: 0,
    idle: 1100,
    noise: 35,
    warn: [600, 9000],
    danger: [400, 12000],
  },
  {
    id: 'tps_deg',
    label: 'TPS Sensor',
    unit: '°',
    min: 0,
    max: 90,
    digits: 1,
    idle: 4,
    noise: 0.4,
  },
  {
    id: 'ect',
    label: 'ECT Sensor',
    unit: '°C',
    min: -40,
    max: 130,
    digits: 1,
    idle: 88,
    noise: 0.2,
    warn: [50, 105],
    danger: [40, 115],
  },
  {
    id: 'o2',
    label: 'O2 Sensor',
    unit: 'V',
    min: 0,
    max: 1.5,
    digits: 3,
    idle: 0.45,
    noise: 0.05,
  },
  {
    id: 'afr',
    label: 'Air Fuel Ratio',
    unit: ':1',
    min: 10,
    max: 25,
    digits: 2,
    idle: 14.7,
    noise: 0.15,
    warn: [13, 16],
    danger: [12, 17],
  },
  {
    id: 'ltft',
    label: 'LTFT',
    unit: '%',
    min: -25,
    max: 25,
    digits: 1,
    idle: 0.5,
    noise: 0.4,
    warn: [-15, 15],
    danger: [-20, 20],
  },
  {
    id: 'battery',
    label: 'Battery',
    unit: 'V',
    min: 8,
    max: 16,
    digits: 2,
    idle: 13.8,
    noise: 0.05,
    warn: [11.5, 14.7],
    danger: [11, 15.2],
  },
  {
    id: 'tps_v',
    label: 'TPS Voltage',
    unit: 'V',
    min: 0,
    max: 5,
    digits: 3,
    idle: 0.65,
    noise: 0.01,
  },
  {
    id: 'inj_ms',
    label: 'Injection',
    unit: 'ms',
    min: 0,
    max: 30,
    digits: 2,
    idle: 2.4,
    noise: 0.05,
  },
  {
    id: 'ign_deg',
    label: 'Ignition',
    unit: '°',
    min: -10,
    max: 60,
    digits: 1,
    idle: 18,
    noise: 0.3,
  },
];

/** Look up the config for a sensor id. */
export const sensorConfig = (id: SensorId): SensorConfig =>
  SENSORS.find((s) => s.id === id)!;

/** A single sample - one row across all sensors. */
export interface LiveSample {
  ts: number;
  values: Record<SensorId, number>;
}

const MAX_HISTORY = 120; // 2 minutes at 1 Hz

interface LiveState {
  /** Most recent values keyed by sensor id. */
  values: Record<SensorId, number>;
  /** Min/max tracker (auto). */
  minmax: Record<SensorId, { min: number; max: number }>;
  /** Recent samples - capped at MAX_HISTORY (oldest dropped). */
  history: LiveSample[];
  /** Whether the simulator / poller is ticking. */
  running: boolean;
  /** Whether we're recording samples for CSV export. */
  recording: boolean;
  recordedAt: number | null;
  recordedSamples: LiveSample[];
  /** Refresh interval (ms) */
  intervalMs: number;
  /** "real" if backend hooked up, "demo" if simulator. */
  source: 'demo' | 'real';

  setRunning: (v: boolean) => void;
  setRecording: (v: boolean) => void;
  setIntervalMs: (ms: number) => void;
  setSource: (s: 'demo' | 'real') => void;
  resetMinMax: () => void;
  pushSample: (sample: LiveSample) => void;
  clearHistory: () => void;
}

const zeroValues = SENSORS.reduce(
  (acc, s) => {
    acc[s.id] = s.idle;
    return acc;
  },
  {} as Record<SensorId, number>,
);
const initialMinMax = SENSORS.reduce(
  (acc, s) => {
    acc[s.id] = { min: s.idle, max: s.idle };
    return acc;
  },
  {} as Record<SensorId, { min: number; max: number }>,
);

export const useLiveData = create<LiveState>((set) => ({
  values: { ...zeroValues },
  minmax: { ...initialMinMax },
  history: [],
  running: true,
  recording: false,
  recordedAt: null,
  recordedSamples: [],
  intervalMs: 500, // 2 Hz default
  source: 'demo',

  setRunning: (v) => set({ running: v }),
  setRecording: (v) =>
    set((state) => ({
      recording: v,
      recordedAt: v ? Date.now() : state.recordedAt,
      recordedSamples: v ? [] : state.recordedSamples,
    })),
  setIntervalMs: (ms) => set({ intervalMs: ms }),
  setSource: (s) => set({ source: s }),
  resetMinMax: () =>
    set((state) => ({
      minmax: SENSORS.reduce(
        (acc, s) => {
          acc[s.id] = { min: state.values[s.id], max: state.values[s.id] };
          return acc;
        },
        {} as Record<SensorId, { min: number; max: number }>,
      ),
    })),

  pushSample: (sample) =>
    set((state) => {
      // Update min/max trackers.
      const minmax: typeof state.minmax = { ...state.minmax };
      for (const s of SENSORS) {
        const v = sample.values[s.id];
        const cur = minmax[s.id];
        minmax[s.id] = {
          min: v < cur.min ? v : cur.min,
          max: v > cur.max ? v : cur.max,
        };
      }
      // Append to scrolling history.
      const next = [...state.history, sample];
      if (next.length > MAX_HISTORY) next.splice(0, next.length - MAX_HISTORY);
      // Append to recording buffer if active.
      const recordedSamples = state.recording
        ? [...state.recordedSamples, sample]
        : state.recordedSamples;
      return {
        values: sample.values,
        minmax,
        history: next,
        recordedSamples,
      };
    }),

  clearHistory: () => set({ history: [], minmax: { ...initialMinMax } }),
}));

// ------------------------------------------------------------------
// Simulator
// ------------------------------------------------------------------

/** Small clamped Gaussian-ish noise. */
function noise(amp: number): number {
  // Box-Muller approximation collapsed to one sample, clamped to ±2σ.
  const u = Math.random() * 2 - 1;
  const v = Math.random() * 2 - 1;
  const r = (u + v) / 1.4;
  return Math.max(-2, Math.min(2, r)) * amp;
}

let simT = 0;

/**
 * Generate one realistic sample. Values orbit each sensor's idle baseline
 * with light correlated noise. RPM gets a slow sine breathing pattern so
 * the gauge looks alive even without a real ECU.
 */
export function simulateSample(): LiveSample {
  simT += 1;
  const values = {} as Record<SensorId, number>;
  for (const s of SENSORS) {
    let v = s.idle + noise(s.noise);
    // RPM: gentle breathing
    if (s.id === 'rpm') v += Math.sin(simT * 0.06) * 80;
    // ECT: very slow drift
    if (s.id === 'ect') v += Math.sin(simT * 0.012) * 0.4;
    // AFR: light oscillation
    if (s.id === 'afr') v += Math.sin(simT * 0.18) * 0.3;
    // O2: oscillates a bit faster
    if (s.id === 'o2') v += Math.sin(simT * 0.32) * 0.08;
    // INJ: tracks RPM mildly
    if (s.id === 'inj_ms') v += Math.sin(simT * 0.06) * 0.15;
    values[s.id] = clamp(v, s.min, s.max);
  }
  return { ts: Date.now(), values };
}

function clamp(v: number, lo: number, hi: number): number {
  return Math.max(lo, Math.min(hi, v));
}

// ------------------------------------------------------------------
// Helpers for the UI
// ------------------------------------------------------------------

export type Tone = 'ok' | 'warn' | 'danger';

/** Decide whether `value` is in the OK / warn / danger band for the
 *  given sensor. If no warn/danger band is configured, returns "ok". */
export function toneFor(cfg: SensorConfig, value: number): Tone {
  const inside = (band?: [number, number]) =>
    band ? value >= band[0] && value <= band[1] : true;
  if (cfg.danger && !inside(cfg.danger)) return 'danger';
  if (cfg.warn && !inside(cfg.warn)) return 'warn';
  return 'ok';
}

/** Format a sensor value with its configured precision. */
export function formatSensor(cfg: SensorConfig, value: number): string {
  return value.toFixed(cfg.digits);
}

// ------------------------------------------------------------------
// Real-data parser (TyN Shop K-Line protocol from `TynShop.adx`)
// ------------------------------------------------------------------
//
// `TynShop.adx` ships these LISTEN packet definitions:
//   LISTEN16  packetbodylength=29  packetoffsetinbody=5  packetsize=24
//   LISTEN_20 packetbodylength=13  packetoffsetinbody=5  packetsize=8
//
// `packetbodylength` includes the 5-byte K-Line TX echo. The Rust side
// (`livedata.rs::try_send`) already strips that echo before handing the
// bytes to us, so the ADX `packetoffset` value of each ADXVALUE is the
// absolute index inside the buffers we receive here (`table16` /
// `table20`).

const T16_OFF = {
  rpm: 0x04, // ENGINSPEED, 16-bit BE,  X
  tps_v: 0x06, // TP_SENSOR_VOLT, 8-bit,  (X/255)*4.98
  tps_pct: 0x07, // TPSANGLE,       8-bit,  (X/170)*85
  ect_v: 0x08, // ECT_SENSOR_VOL, 8-bit,  (X/255)*4.98
  ect_c: 0x09, // ECTSENSOR,      8-bit,  X-40
  battery: 0x0e, // BATTERY_V,      8-bit,  X/10
  inj_ms_hi: 0x0f, // INJECTOR,       16-bit BE, X/250
  ign_deg: 0x11, // SPARKADVANCE,   8-bit,  (X*0.5)-64
} as const;

// TABLE_20 reply payload (8 bytes, after echo strip).
const T20_OFF = {
  o2: 0x04, // O2_SENSOR_VOLT,  8-bit, (X/128)*2.5
  // AFR shares the same byte but applies a different equation.
  afr: 0x04, // AFR_1,           8-bit, X*-0.17857 + 20
  ltft: 0x05, // STFT_1,          8-bit, (X/128)*0.998
} as const;

/** Parse the raw responses into one LiveSample. Bytes that are missing
 *  are silently kept at the previous value via the `prev` argument so
 *  a single dropped reply doesn't make the whole UI flicker. */
export function parseTynLiveData(
  table16: number[],
  table20: number[],
  prev: Record<SensorId, number>,
): LiveSample {
  const v: Record<SensorId, number> = { ...prev };

  // ---- TABLE_16 ----
  // Payload after echo strip is 24 bytes per ADX (LISTEN16 packetsize).
  // Ignition is the deepest field at offset 0x11 (=17) so we need >= 18.
  if (table16.length >= 18) {
    const rpmRaw = (table16[T16_OFF.rpm] << 8) | table16[T16_OFF.rpm + 1];
    v.rpm = rpmRaw; // raw rpm
    v.tps_v = (table16[T16_OFF.tps_v] / 255) * 4.98;
    v.tps_deg = (table16[T16_OFF.tps_pct] / 170) * 85; // ADX labels this "TPS Sensor (%)" but the unit ° in our store
    // ECT: this firmware sometimes reports 0xFF (= "sensor invalid /
    // not yet measured") on the calibrated-celsius byte. Treat that as
    // "no new reading" and keep the previous value rather than showing
    // a bogus 215 °C spike on the dashboard.
    const ectRaw = table16[T16_OFF.ect_c];
    if (ectRaw !== 0xff) v.ect = ectRaw - 40;
    v.battery = table16[T16_OFF.battery] / 10;
    const injRaw =
      (table16[T16_OFF.inj_ms_hi] << 8) | table16[T16_OFF.inj_ms_hi + 1];
    v.inj_ms = injRaw / 250;
    v.ign_deg = table16[T16_OFF.ign_deg] * 0.5 - 64;
  }

  // ---- TABLE_20 ----
  // Payload after echo strip is 8 bytes per ADX (LISTEN_20 packetsize).
  // LTFT is the deepest field at offset 0x05 (=5) so we need >= 6.
  if (table20.length >= 6) {
    const o2Raw = table20[T20_OFF.o2];
    v.o2 = (o2Raw / 128) * 2.5;
    v.afr = o2Raw * -0.17857 + 20;
    v.ltft = (table20[T20_OFF.ltft] / 128) * 0.998;
  }

  return { ts: Date.now(), values: v };
}

/** Convert recorded samples to CSV. Header has a `ts` column followed
 *  by every sensor id; each row is one sample. */
export function samplesToCsv(samples: LiveSample[]): string {
  const header = ['ts', ...SENSORS.map((s) => s.id)].join(',');
  const rows = samples.map((s) => {
    const cols = [
      s.ts.toString(),
      ...SENSORS.map((cfg) => s.values[cfg.id].toFixed(cfg.digits)),
    ];
    return cols.join(',');
  });
  return [header, ...rows].join('\n');
}
