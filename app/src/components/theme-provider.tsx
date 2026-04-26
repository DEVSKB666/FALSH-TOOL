"use client";

import { useEffect } from "react";
import { useSettings } from "@/lib/settings";

/**
 * Tiny theme provider that maps `useSettings.theme` -> `<html>` class names.
 *
 * We don't pull `next-themes` because we want full control over the
 * three custom palettes (`racing`, `neon`, plus light/dark).
 */
export function ThemeProvider({ children }: { children: React.ReactNode }) {
  const theme = useSettings((s) => s.theme);

  useEffect(() => {
    const html = document.documentElement;
    // Reset all theme classes first
    html.classList.remove("dark", "theme-racing", "theme-neon");
    if (theme === "dark") html.classList.add("dark");
    else if (theme === "racing") html.classList.add("dark", "theme-racing");
    else if (theme === "neon") html.classList.add("theme-neon");
    // 'light' = no extra class
  }, [theme]);

  // Reveal body once we've applied the theme to avoid a flash of unstyled
  // content (FOUC). Tailwind's `no-fouc` class is set on <body> via globals.
  useEffect(() => {
    document.body.classList.remove("no-fouc");
    document.body.classList.add("fouc-ready");
  }, []);

  return <>{children}</>;
}
