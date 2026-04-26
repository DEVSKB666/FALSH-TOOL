"use client";

/**
 * Tiny synthesiser - generates UI feedback tones via Web Audio API.
 * No assets, no MP3s, just oscillators. Each call is fire-and-forget.
 *
 * Volume can be tweaked via `setSoundVolume(0..1)` and globally muted
 * via `setSoundEnabled(false)` (both persist to localStorage).
 */

type ToneSpec = {
  freq: number;        // Hz
  duration: number;    // seconds
  type?: OscillatorType;
  /** Where in the master envelope this tone starts (sec). */
  startAt?: number;
  /** Optional amplitude multiplier for this segment (0..1). */
  gain?: number;
};

const STORAGE_KEY = "mza-tuner.sound";

let ctx: AudioContext | null = null;
let enabled = true;
let volume = 0.35;

if (typeof window !== "undefined") {
  try {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (raw) {
      const parsed = JSON.parse(raw);
      if (typeof parsed.enabled === "boolean") enabled = parsed.enabled;
      if (typeof parsed.volume === "number") volume = parsed.volume;
    }
  } catch {
    /* ignore */
  }
}

function persist() {
  if (typeof window === "undefined") return;
  try {
    localStorage.setItem(STORAGE_KEY, JSON.stringify({ enabled, volume }));
  } catch {
    /* ignore */
  }
}

function getCtx(): AudioContext | null {
  if (typeof window === "undefined") return null;
  if (!ctx) {
    const Cls =
      (window as unknown as { AudioContext?: typeof AudioContext }).AudioContext ??
      (window as unknown as { webkitAudioContext?: typeof AudioContext }).webkitAudioContext;
    if (!Cls) return null;
    ctx = new Cls();
  }
  // Browsers suspend the context until a user gesture.
  if (ctx.state === "suspended") void ctx.resume();
  return ctx;
}

function playTones(tones: ToneSpec[]) {
  if (!enabled) return;
  const ac = getCtx();
  if (!ac) return;

  const now = ac.currentTime;
  for (const tone of tones) {
    const osc = ac.createOscillator();
    const gain = ac.createGain();
    osc.type = tone.type ?? "sine";
    osc.frequency.setValueAtTime(tone.freq, now + (tone.startAt ?? 0));
    const a = volume * (tone.gain ?? 1);
    // Soft attack + release to avoid clicks.
    gain.gain.setValueAtTime(0, now + (tone.startAt ?? 0));
    gain.gain.linearRampToValueAtTime(a, now + (tone.startAt ?? 0) + 0.01);
    gain.gain.exponentialRampToValueAtTime(
      0.0001,
      now + (tone.startAt ?? 0) + tone.duration
    );
    osc.connect(gain).connect(ac.destination);
    osc.start(now + (tone.startAt ?? 0));
    osc.stop(now + (tone.startAt ?? 0) + tone.duration + 0.02);
  }
}

// ---------------------- preset palette ----------------------

export const sound = {
  /** Two-step ascending major-third, used for "connected". */
  connect() {
    playTones([
      { freq: 660, duration: 0.12, type: "triangle" },
      { freq: 880, duration: 0.18, type: "triangle", startAt: 0.1 },
    ]);
  },
  /** Two-step descending, used for "disconnected". */
  disconnect() {
    playTones([
      { freq: 880, duration: 0.12, type: "triangle" },
      { freq: 587, duration: 0.18, type: "triangle", startAt: 0.1 },
    ]);
  },
  /** Quick high tick - generic UI feedback. */
  click() {
    playTones([{ freq: 1100, duration: 0.04, type: "square", gain: 0.5 }]);
  },
  /** Bright bell, used for "task succeeded". */
  success() {
    playTones([
      { freq: 880, duration: 0.1, type: "sine" },
      { freq: 1320, duration: 0.18, type: "sine", startAt: 0.08 },
    ]);
  },
  /** Buzzy fall, used for "error". */
  error() {
    playTones([
      { freq: 320, duration: 0.16, type: "sawtooth", gain: 0.7 },
      { freq: 196, duration: 0.22, type: "sawtooth", startAt: 0.14, gain: 0.7 },
    ]);
  },
  /** Soft alert chirp, used for "warning". */
  warning() {
    playTones([
      { freq: 660, duration: 0.1, type: "triangle" },
      { freq: 660, duration: 0.1, type: "triangle", startAt: 0.18 },
    ]);
  },
  /** Soft information ping. */
  info() {
    playTones([{ freq: 740, duration: 0.18, type: "sine" }]);
  },
  /** Heartbeat-like ECU detected sound. */
  scan() {
    playTones([
      { freq: 1000, duration: 0.05, type: "square", gain: 0.4 },
      { freq: 1500, duration: 0.05, type: "square", startAt: 0.1, gain: 0.4 },
    ]);
  },
};

// ---------------------- volume / mute API ----------------------

export function setSoundEnabled(v: boolean) {
  enabled = v;
  persist();
}
export function setSoundVolume(v: number) {
  volume = Math.max(0, Math.min(1, v));
  persist();
}
export function isSoundEnabled() {
  return enabled;
}
export function getSoundVolume() {
  return volume;
}
