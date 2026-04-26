"use client";

import { useEffect } from "react";
import { useSettings } from "@/lib/settings";
import { setSoundEnabled } from "@/lib/sounds";

/**
 * Tiny invisible component that mirrors the `soundEnabled` setting
 * to the audio module's local flag. Mounted once at the root.
 */
export function SoundSync() {
  const enabled = useSettings((s) => s.soundEnabled);
  useEffect(() => {
    setSoundEnabled(enabled);
  }, [enabled]);
  return null;
}
