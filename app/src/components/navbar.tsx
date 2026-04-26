"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { motion } from "framer-motion";
import {
  Activity,
  Battery,
  BatteryFull,
  BatteryLow,
  BatteryMedium,
  BatteryWarning,
  Cable,
  Clock,
  Cpu,
  Database,
  FileLock2,
  Home,
  Network,
  Settings,
  Zap,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { useSettings } from "@/lib/settings";
import { useConnection } from "@/lib/connection";
import { sensorConfig, toneFor, useLiveData } from "@/lib/livedata";

// Short labels keep the navbar compact at narrower widths. Each item
// always shows its icon; the label is hidden below `xl`.
const NAV = [
  { href: "/",         label: "Home",       icon: Home,     short: "Home" },
  { href: "/ecu",      label: "ECU",        icon: Cpu,      short: "ECU"  },
  { href: "/livedata", label: "Live",       icon: Activity, short: "Live" },
  { href: "/history",  label: "History",    icon: Clock,    short: "Hist" },
  { href: "/files",    label: "Files",      icon: FileLock2,short: "File" },
  { href: "/database", label: "DB",         icon: Database, short: "DB"   },
  { href: "/settings", label: "Settings",   icon: Settings, short: "Set"  },
] as const;

/**
 * Persistent top navbar. Replaces the old sidebar - same routes, but
 * laid out horizontally with an animated underline indicator on the
 * active item and a tiny connection-status pill on the right.
 */
export function Navbar() {
  const pathname = usePathname();
  const appName  = useSettings((s) => s.appName);
  const logo     = useSettings((s) => s.logoDataUrl);
  const status   = useConnection((s) => s.status);
  const connected = status === "connected";

  return (
    <header className="sticky top-0 z-30 w-full border-b border-border/50 bg-card/70 backdrop-blur-2xl">
      <div className="mx-auto flex h-16 max-w-7xl items-center gap-2 px-4 md:gap-3 md:px-6">
        {/* Brand - logo only at narrow widths so we keep room for nav. */}
        <div className="flex shrink-0 items-center gap-2">
          {logo ? (
            <img
              src={logo}
              alt=""
              className="h-9 w-9 rounded-lg object-cover ring-1 ring-primary/40"
            />
          ) : (
            <motion.div
              className="flex h-9 w-9 items-center justify-center rounded-lg bg-primary shadow-[0_0_18px_hsl(var(--primary)/0.55)]"
              animate={{ rotate: [0, 3, -3, 0] }}
              transition={{ duration: 6, repeat: Infinity, ease: "easeInOut" }}
            >
              <Zap className="h-5 w-5 text-primary-foreground" strokeWidth={2.5} />
            </motion.div>
          )}
          {/* Brand text only on the very widest screens - everything
              else is space-constrained once you add 7 nav items + 3
              status pills. */}
          <div className="hidden min-w-0 2xl:block">
            <p className="truncate text-sm font-semibold leading-tight">{appName}</p>
            <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
              Tuner Suite
            </p>
          </div>
        </div>

        {/* Nav items - icons always visible, labels appear at xl. The
            row scrolls horizontally on tiny screens rather than wrapping
            so the navbar stays exactly one line tall. */}
        <nav className="flex flex-1 items-center gap-0.5 overflow-x-auto md:gap-1 md:overflow-visible">
          {NAV.map((item) => {
            const active =
              pathname === item.href ||
              (item.href !== "/" && pathname.startsWith(item.href));
            const Icon = item.icon;
            return (
              <Link
                key={item.href}
                href={item.href}
                title={item.label}
                className={cn(
                  "relative inline-flex shrink-0 items-center gap-1.5 whitespace-nowrap rounded-lg px-2.5 py-2 text-sm font-medium transition-colors",
                  active
                    ? "text-primary"
                    : "text-muted-foreground hover:bg-accent/40 hover:text-foreground",
                )}
              >
                <Icon className="h-4 w-4 shrink-0" />
                <span className="hidden xl:inline">{item.label}</span>
                {active && (
                  <motion.div
                    layoutId="navbar-active-pill"
                    className="absolute inset-0 -z-10 rounded-lg bg-primary/10 ring-1 ring-primary/30"
                    transition={{ type: "spring", stiffness: 380, damping: 30 }}
                  />
                )}
              </Link>
            );
          })}
        </nav>

        {/* Status pills - icon-only at narrow widths, full label at lg+. */}
        <div className="flex shrink-0 items-center gap-1.5">
          <BridgePill />
          <BatteryPill />
          <ConnectionPill status={status} connected={connected} />
        </div>
      </div>
    </header>
  );
}

function ConnectionPill({
  status,
  connected,
}: {
  status: ReturnType<typeof useConnection.getState>["status"];
  connected: boolean;
}) {
  const label =
    connected ? "เชื่อมต่อแล้ว"
    : status === "scanning"   ? "สแกน…"
    : status === "connecting" ? "เชื่อม…"
    : status === "error"      ? "ผิดพลาด"
    :                            "ไม่ได้เชื่อมต่อ";
  return (
    <Link
      href="/"
      title={`Connection: ${label} (กดเพื่อไป Dashboard)`}
      className={cn(
        "inline-flex shrink-0 items-center gap-1.5 rounded-full px-2.5 py-1 text-xs font-medium ring-1 transition",
        connected
          ? "bg-emerald-500/15 text-emerald-300 ring-emerald-500/40 hover:bg-emerald-500/20"
          : status === "error"
          ? "bg-red-500/15 text-red-300 ring-red-500/40"
          : "bg-muted/40 text-muted-foreground ring-border/40 hover:bg-muted/60",
      )}
    >
      <span
        className={cn(
          "h-1.5 w-1.5 shrink-0 rounded-full",
          connected
            ? "bg-emerald-400 shadow-[0_0_6px_rgba(16,185,129,0.7)]"
            : status === "error"
            ? "bg-red-400"
            : "bg-muted-foreground/60",
        )}
      />
      <Cable className="h-3.5 w-3.5 shrink-0" />
      <span className="hidden whitespace-nowrap lg:inline">{label}</span>
    </Link>
  );
}

/**
 * Battery voltage pill rendered next to the connection pill. Picks
 * the right battery icon and tone based on the configured warn /
 * danger bands of the `battery` sensor.
 */
function BatteryPill() {
  const cfg     = sensorConfig("battery");
  const value   = useLiveData((s) => s.values.battery);
  const tone    = toneFor(cfg, value);
  // Voltage → 0..1 percent for icon selection (12V is ~0%, 14.4V is ~100%).
  const pct     = Math.max(0, Math.min(1, (value - 11) / (14.4 - 11)));
  const Icon    = pct < 0.25 ? BatteryWarning
                : pct < 0.50 ? BatteryLow
                : pct < 0.80 ? BatteryMedium
                :              BatteryFull;
  const cls =
    tone === "danger" ? "bg-red-500/15 text-red-300 ring-red-500/40" :
    tone === "warn"   ? "bg-amber-500/15 text-amber-300 ring-amber-500/40" :
                        "bg-emerald-500/15 text-emerald-300 ring-emerald-500/40";

  return (
    <Link
      href="/livedata"
      title={`Battery ${value.toFixed(2)} V (กดเพื่อดู Live Data)`}
      className={cn(
        "inline-flex shrink-0 items-center gap-1.5 rounded-full px-2.5 py-1 font-mono text-xs font-medium ring-1 transition hover:brightness-125",
        cls,
      )}
    >
      <Icon className="h-3.5 w-3.5 shrink-0" />
      <span className="hidden whitespace-nowrap md:inline">{value.toFixed(2)}V</span>
    </Link>
  );
}

// Suppress: Battery is an unused import on purpose - we keep it on hand
// for future variants. (Hint to TS-strict: see the named-import block.)
void Battery;

/**
 * Tiny pill that surfaces "we're talking to a remote bridge" so the
 * user always knows where their commands are going. Only renders when
 * the bridge is enabled in settings.
 */
function BridgePill() {
  const enabled = useSettings((s) => s.bridgeEnabled);
  const url     = useSettings((s) => s.bridgeUrl);
  if (!enabled || !url.trim()) return null;

  // Strip protocol/port for a tidier display.
  const display = url.replace(/^tcp:\/\//, "").replace(/:\d+$/, "");

  return (
    <Link
      href="/settings"
      title={`Remote bridge: ${url} (กดเพื่อปรับใน Settings)`}
      className="inline-flex shrink-0 items-center gap-1.5 rounded-full bg-purple-500/15 px-2.5 py-1 font-mono text-[11px] font-medium text-purple-300 ring-1 ring-purple-500/40 transition hover:bg-purple-500/25"
    >
      <Network className="h-3.5 w-3.5 shrink-0" />
      <span className="hidden whitespace-nowrap lg:inline">BRIDGE</span>
      <span className="hidden whitespace-nowrap xl:inline opacity-70">{display}</span>
    </Link>
  );
}
