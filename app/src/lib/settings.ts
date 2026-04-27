'use client';

import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';

/**
 * App-wide user-tunable settings.
 *
 * All state is persisted to localStorage in dev (regular Next.js) and to
 * a Tauri Store on disk when running inside Tauri. The frontend uses one
 * Zustand store either way.
 */

export type ThemePreset = 'light' | 'dark' | 'racing' | 'neon';
export type BackgroundPreset =
  | 'particles'
  | 'circuit'
  | 'scanlines'
  | 'aurora'
  | 'off';

export interface SettingsState {
  /** Brand name shown in the title bar / splash. */
  appName: string;
  /** Optional logo - data URL or path on disk. */
  logoDataUrl: string | null;
  /** Theme palette to apply (also drives Tailwind dark mode). */
  theme: ThemePreset;
  /** Animated background preset. */
  background: BackgroundPreset;
  /** Show the splash on app start. */
  showSplash: boolean;
  /** Whether the user already saw the splash this session. */
  splashSeenThisRun: boolean;
  /** Try to auto-connect to a single attached FTDI port on app open. */
  autoConnect: boolean;
  /** Toggle UI sound effects (alerts / connect tones). */
  soundEnabled: boolean;
  /** UI motto / tagline shown under the brand name. */
  tagline: string;

  /** Remote `loy-bridge` URL. When set + `bridgeEnabled = true`, the
   *  app routes K-Line operations through the bridge instead of the
   *  local FTDI driver. Format: `host:port` or `tcp://host:port`. */
  bridgeUrl: string;
  /** Master toggle for the remote bridge. */
  bridgeEnabled: boolean;

  // ---- mutators (kept thin; UI calls these directly) -----------------
  setAppName: (name: string) => void;
  setLogoDataUrl: (url: string | null) => void;
  setTheme: (theme: ThemePreset) => void;
  setBackground: (bg: BackgroundPreset) => void;
  setShowSplash: (v: boolean) => void;
  setAutoConnect: (v: boolean) => void;
  setSoundEnabled: (v: boolean) => void;
  setTagline: (v: string) => void;
  setBridgeUrl: (v: string) => void;
  setBridgeEnabled: (v: boolean) => void;
  markSplashSeen: () => void;
  reset: () => void;
}

const DEFAULTS: Pick<
  SettingsState,
  | 'appName'
  | 'logoDataUrl'
  | 'theme'
  | 'background'
  | 'showSplash'
  | 'splashSeenThisRun'
  | 'autoConnect'
  | 'soundEnabled'
  | 'tagline'
  | 'bridgeUrl'
  | 'bridgeEnabled'
> = {
  appName: 'LOY-TUNER 2026',
  logoDataUrl: null,
  theme: 'racing',
  background: 'particles',
  showSplash: true,
  splashSeenThisRun: false,
  autoConnect: true,
  soundEnabled: true,
  tagline: 'Honda Keihin / Shinden Tuner',
  bridgeUrl: '',
  bridgeEnabled: false,
};

export const useSettings = create<SettingsState>()(
  persist(
    (set) => ({
      ...DEFAULTS,
      setAppName: (appName) => set({ appName }),
      setLogoDataUrl: (logoDataUrl) => set({ logoDataUrl }),
      setTheme: (theme) => set({ theme }),
      setBackground: (background) => set({ background }),
      setShowSplash: (showSplash) => set({ showSplash }),
      setAutoConnect: (autoConnect) => set({ autoConnect }),
      setSoundEnabled: (soundEnabled) => set({ soundEnabled }),
      setTagline: (tagline) => set({ tagline }),
      setBridgeUrl: (bridgeUrl) => set({ bridgeUrl }),
      setBridgeEnabled: (bridgeEnabled) => set({ bridgeEnabled }),
      markSplashSeen: () => set({ splashSeenThisRun: true }),
      reset: () => set({ ...DEFAULTS }),
    }),
    {
      name: 'mza-tuner.settings',
      storage: createJSONStorage(() => localStorage),
      // never persist the per-session splash flag
      partialize: ({ splashSeenThisRun: _ignored, ...rest }) => rest,
    },
  ),
);
