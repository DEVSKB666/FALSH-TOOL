'use client';

/**
 * `<LiveDataAlerts>` watches every sensor in `useLiveData` and fires
 * a toast + warning sound when any reading enters its configured
 * `danger` band. Designed to mount **once** in the root layout
 * (alongside `<LiveDataPump>`) so the alert stream is global -
 * users get notified whether they're on the dashboard, ECU page,
 * or live-data view.
 *
 * Anti-spam strategy:
 *
 * - **Edge-triggered**: only fires when a sensor *crosses into*
 *   danger from a non-danger state. A sensor stuck in the red zone
 *   does not retrigger every tick.
 * - **Per-sensor cooldown**: once an alert fires, that sensor is
 *   silenced for `COOLDOWN_MS` even if it briefly leaves and re-enters
 *   the danger band. Prevents flicker on noisy ADC channels.
 * - **Demo-source bypass**: when the live source is the simulator we
 *   suppress alerts entirely - the noise model occasionally clips
 *   into danger and shouldn't pester users running browser-only.
 *
 * The toggle lives in Settings (`liveAlertsEnabled`); when off, this
 * component is a no-op so the listener overhead disappears too.
 */

import { useEffect } from 'react';
import { AlertTriangle } from 'lucide-react';
import {
  formatSensor,
  sensorConfig,
  toneFor,
  useLiveData,
  type SensorId,
} from '@/lib/livedata';
import { useSettings } from '@/lib/settings';
import { sound } from '@/lib/sounds';
import { toast } from '@/components/toast';

const COOLDOWN_MS = 8_000;

export function LiveDataAlerts() {
  const enabled = useSettings((s) => s.liveAlertsEnabled);

  useEffect(() => {
    if (!enabled) return;
    // Track the last alert timestamp + previous tone so we can detect
    // edge transitions instead of re-firing every poll.
    const lastTone: Partial<Record<SensorId, 'ok' | 'warn' | 'danger'>> = {};
    const cooldownUntil: Partial<Record<SensorId, number>> = {};

    const unsub = useLiveData.subscribe((state, prev) => {
      // Skip when nothing actually changed (zustand calls listeners
      // for any partial update, e.g. `running` toggle).
      if (state.values === prev.values) return;
      // Suppress demo-source alerts to avoid spam in browser preview.
      if (state.source === 'demo') return;

      const now = Date.now();
      for (const id of Object.keys(state.values) as SensorId[]) {
        const cfg = sensorConfig(id);
        const v = state.values[id];
        const tone = toneFor(cfg, v);
        const prevTone = lastTone[id] ?? 'ok';
        lastTone[id] = tone;

        if (tone !== 'danger' || prevTone === 'danger') continue;
        if ((cooldownUntil[id] ?? 0) > now) continue;
        cooldownUntil[id] = now + COOLDOWN_MS;

        // Format the offending value with the sensor's own precision
        // so the toast reads the same as the gauge ("AFR 13.8" etc.).
        const human = `${cfg.label}: ${formatSensor(cfg, v)} ${cfg.unit}`;
        toast.error('⚠ ค่าผิดปกติ', human);
        sound.warning();
      }
    });

    return () => {
      unsub();
    };
  }, [enabled]);

  return null;
}

/**
 * Optional: surface the icon in the navbar for ops who want a
 * visual cue that the watcher is armed. Not currently mounted but
 * exported so /settings can render a status badge.
 */
export function LiveAlertsBadge() {
  return <AlertTriangle className="h-4 w-4 text-amber-400" />;
}
