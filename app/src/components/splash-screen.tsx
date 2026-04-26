"use client";

import { motion, AnimatePresence } from "framer-motion";
import { useEffect, useState } from "react";
import { Cpu, Zap, ShieldCheck, Wifi, CircuitBoard, Loader2 } from "lucide-react";
import { useSettings } from "@/lib/settings";
import { cn } from "@/lib/utils";

/**
 * Boot-time splash screen.
 *
 * Mirrors the original C# splash messages (see ARCHITECTURE.md s2):
 *
 *   > INITIALIZING MZA-TUNER 2026...
 *   > SEARCHING FTDI INTERFACE...
 *   > FTDI D2XX DRIVER DETECTED [OK]
 *   > AUTHENTICATING LICENSE KEY...
 *   > MEMORY OFFSET MAPPING: 0x8000-0xFFFF
 *   > K-LINE PROTOCOL STACK LOADED
 *   > WARMING UP ISO-9141 BUS... [OK]
 */
const STEPS = [
  { icon: Cpu,            label: "INITIALIZING MZA-TUNER 2026" },
  { icon: Wifi,           label: "SEARCHING FTDI INTERFACE" },
  { icon: CircuitBoard,   label: "FTDI D2XX DRIVER DETECTED" },
  { icon: ShieldCheck,    label: "AUTHENTICATING LICENSE KEY" },
  { icon: CircuitBoard,   label: "MEMORY OFFSET MAPPING: 0x8000-0xFFFF" },
  { icon: Zap,            label: "K-LINE PROTOCOL STACK LOADED" },
  { icon: Zap,            label: "WARMING UP ISO-9141 BUS" },
] as const;

export interface SplashScreenProps {
  /** Called when the user clicks "Skip" or the boot sequence finishes. */
  onComplete: () => void;
}

export function SplashScreen({ onComplete }: SplashScreenProps) {
  const appName = useSettings((s) => s.appName);
  const logo = useSettings((s) => s.logoDataUrl);
  const [step, setStep] = useState(0);
  const [done, setDone] = useState(false);

  // Step through the messages with a sub-second cadence so the screen
  // stays brisk; total ~3.5s.
  useEffect(() => {
    if (step >= STEPS.length) {
      const t = setTimeout(() => setDone(true), 350);
      return () => clearTimeout(t);
    }
    const t = setTimeout(() => setStep((s) => s + 1), 480);
    return () => clearTimeout(t);
  }, [step]);

  // Once the fade-out has played, hand control back to the parent.
  useEffect(() => {
    if (!done) return;
    const t = setTimeout(onComplete, 600);
    return () => clearTimeout(t);
  }, [done, onComplete]);

  return (
    <AnimatePresence>
      {!done && (
        <motion.div
          key="splash"
          initial={{ opacity: 1 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0, transition: { duration: 0.5 } }}
          className="fixed inset-0 z-50 flex flex-col items-center justify-center bg-background/95 backdrop-blur-sm"
        >
          {/* Brand */}
          <motion.div
            initial={{ y: -10, opacity: 0 }}
            animate={{ y: 0, opacity: 1 }}
            transition={{ duration: 0.6, delay: 0.1 }}
            className="mb-10 flex flex-col items-center gap-4"
          >
            {logo ? (
              <img
                src={logo}
                alt=""
                className="h-20 w-20 rounded-2xl object-cover ring-2 ring-primary/40 shadow-2xl"
              />
            ) : (
              <BrandMark />
            )}
            <h1 className="text-balance bg-gradient-to-b from-foreground to-foreground/70 bg-clip-text text-center font-sans text-4xl font-bold tracking-tight text-transparent">
              {appName}
            </h1>
            <p className="font-mono text-xs uppercase tracking-[0.3em] text-muted-foreground">
              Honda Keihin / Shinden Tuning Suite
            </p>
          </motion.div>

          {/* Progress bar */}
          <div className="relative h-1.5 w-80 overflow-hidden rounded-full bg-muted">
            <motion.div
              className="absolute inset-y-0 left-0 rounded-full bg-primary"
              initial={{ width: 0 }}
              animate={{ width: `${(step / STEPS.length) * 100}%` }}
              transition={{ duration: 0.4 }}
            />
          </div>

          {/* Boot log */}
          <div className="mt-8 h-44 w-[28rem] max-w-[90vw] overflow-hidden rounded-xl border border-border/60 bg-card/40 p-4 font-mono text-xs leading-relaxed text-muted-foreground shadow-2xl backdrop-blur-md">
            {STEPS.slice(0, step + 1).map((s, i) => {
              const Icon = s.icon;
              const completed = i < step;
              const active = i === step && !completed;
              return (
                <div key={i} className={cn("flex items-center gap-2", completed && "text-foreground/80")}>
                  <Icon className="h-3.5 w-3.5 shrink-0 text-primary" />
                  <span className="truncate">&gt; {s.label}</span>
                  <span className="ml-auto shrink-0">
                    {active ? (
                      <Loader2 className="h-3.5 w-3.5 animate-spin text-primary" />
                    ) : completed ? (
                      <span className="text-primary">[OK]</span>
                    ) : null}
                  </span>
                </div>
              );
            })}
          </div>

          {/* Skip control */}
          <button
            onClick={() => setDone(true)}
            className="mt-8 font-mono text-xs uppercase tracking-widest text-muted-foreground/70 transition hover:text-foreground"
          >
            Press to continue ›
          </button>
        </motion.div>
      )}
    </AnimatePresence>
  );
}

function BrandMark() {
  return (
    <div className="relative">
      <div className="absolute inset-0 animate-pulse-ring rounded-2xl bg-primary/40" />
      <div className="relative flex h-20 w-20 items-center justify-center rounded-2xl bg-gradient-to-br from-primary to-primary/60 shadow-2xl ring-2 ring-primary/40">
        <Zap className="h-10 w-10 text-primary-foreground" strokeWidth={2.5} />
      </div>
    </div>
  );
}
