'use client';

/**
 * 60 Hz interpolation layer for the live-data store.
 *
 * The K-Line poll only completes every ~400 ms, so subscribing the gauge
 * widgets directly to `useLiveData((s) => s.values)` makes the needles
 * jump in big steps and feel choppy. `useSmoothedValues` sits between the
 * raw store and the UI: it samples the latest target values on every
 * `requestAnimationFrame` tick and exponentially decays the displayed
 * value toward the target, giving a buttery-smooth animation regardless
 * of the underlying sample rate.
 *
 * Sensor-specific time constants (`tau`) are tuned so:
 *   - RPM (fast-moving, big numbers) → ~120 ms (snappy)
 *   - Throttle / injection / ignition → ~150 ms
 *   - ECT / Battery (slow physical signals) → ~400 ms (gentle filter)
 *
 * The function returns a stable object reference each frame; consumers
 * should pass it straight to gauge components without `useMemo`.
 */

import { useEffect, useRef, useState } from 'react';
import { SENSORS, useLiveData, type SensorId } from './livedata';

/** Per-sensor smoothing time constant (seconds). Smaller = snappier. */
const TAU: Record<SensorId, number> = {
  rpm: 0.12,
  tps_deg: 0.1,
  tps_v: 0.1,
  ect: 0.4,
  o2: 0.15,
  afr: 0.18,
  ltft: 0.3,
  battery: 0.4,
  inj_ms: 0.12,
  ign_deg: 0.15,
};

/** Threshold (per-sensor units) below which we stop re-rendering. Keeps
 *  React idle when the displayed value has effectively converged. */
const EPSILON: Record<SensorId, number> = {
  rpm: 0.5,
  tps_deg: 0.05,
  tps_v: 0.001,
  ect: 0.05,
  o2: 0.001,
  afr: 0.005,
  ltft: 0.02,
  battery: 0.005,
  inj_ms: 0.005,
  ign_deg: 0.05,
};

export function useSmoothedValues(): Record<SensorId, number> {
  // The most recent target values from the store. We pull them via a
  // ref-tracking effect rather than a state subscription so the RAF
  // loop always sees the freshest value without being scheduled by it.
  const targetsRef = useRef<Record<SensorId, number>>(useLiveData.getState().values);
  useEffect(() => {
    return useLiveData.subscribe((s) => {
      targetsRef.current = s.values;
    });
  }, []);

  // Displayed (interpolated) values. Initial = current store snapshot
  // so we don't spend the first frame easing in from zero.
  const [smoothed, setSmoothed] = useState<Record<SensorId, number>>(
    () => ({ ...targetsRef.current }),
  );
  const smoothedRef = useRef(smoothed);
  smoothedRef.current = smoothed;

  useEffect(() => {
    let raf = 0;
    let last = performance.now();

    const tick = (now: number) => {
      const dt = Math.min(0.1, (now - last) / 1000); // clamp at 100 ms catch-up
      last = now;

      const cur = smoothedRef.current;
      const tgt = targetsRef.current;
      let next: Record<SensorId, number> | null = null;
      let changed = false;

      for (const cfg of SENSORS) {
        const id = cfg.id;
        const c = cur[id];
        const t = tgt[id];
        if (c === t) continue;
        // Exponential approach: alpha = 1 - exp(-dt / tau).
        const alpha = 1 - Math.exp(-dt / TAU[id]);
        const v = c + (t - c) * alpha;
        // If we are very close to the target, snap to it so the value
        // settles cleanly instead of asymptotically drifting forever.
        const snapped = Math.abs(t - v) < EPSILON[id] ? t : v;
        if (snapped !== c) {
          if (!next) next = { ...cur };
          next[id] = snapped;
          changed = true;
        }
      }

      if (changed && next) {
        smoothedRef.current = next;
        setSmoothed(next);
      }

      raf = requestAnimationFrame(tick);
    };

    raf = requestAnimationFrame(tick);
    return () => cancelAnimationFrame(raf);
  }, []);

  return smoothed;
}
