"use client";

/**
 * Lightweight toast/alert system.
 *
 *   import { toast } from "@/components/toast";
 *   toast.success("เชื่อมต่อสำเร็จ");
 *   toast.error("ไม่พบ FTDI", "ตรวจสอบสาย USB");
 *
 * Mount `<Toaster />` once near the root (in `<ThemeProvider>` works).
 * Each `toast.*()` call automatically plays a matching sound from
 * `@/lib/sounds`.
 */

import { AnimatePresence, motion } from "framer-motion";
import { CheckCircle2, AlertTriangle, Info, XCircle, X } from "lucide-react";
import { useEffect, useState } from "react";
import { sound } from "@/lib/sounds";
import { cn } from "@/lib/utils";

export type ToastKind = "success" | "error" | "warning" | "info";

interface ToastItem {
  id: number;
  kind: ToastKind;
  title: string;
  description?: string;
  duration: number;
}

let nextId = 1;
let externalListeners: Array<(items: ToastItem[]) => void> = [];
let queue: ToastItem[] = [];

function emit() {
  for (const fn of externalListeners) fn([...queue]);
}

function push(kind: ToastKind, title: string, description?: string, duration = 3500) {
  const item: ToastItem = { id: nextId++, kind, title, description, duration };
  queue = [...queue, item];
  emit();
  // Match toast kind to the sound preset
  const map: Record<ToastKind, () => void> = {
    success: sound.success,
    error: sound.error,
    warning: sound.warning,
    info: sound.info,
  };
  try { map[kind](); } catch { /* ignore */ }
  // Auto-dismiss
  if (duration > 0) {
    setTimeout(() => dismiss(item.id), duration);
  }
  return item.id;
}

function dismiss(id: number) {
  queue = queue.filter((t) => t.id !== id);
  emit();
}

/** Public toast API. */
export const toast = {
  success: (title: string, description?: string, duration?: number) =>
    push("success", title, description, duration),
  error: (title: string, description?: string, duration?: number) =>
    push("error", title, description, duration ?? 5000),
  warning: (title: string, description?: string, duration?: number) =>
    push("warning", title, description, duration),
  info: (title: string, description?: string, duration?: number) =>
    push("info", title, description, duration),
  dismiss,
};

/** Toaster — render once at the root. */
export function Toaster() {
  const [items, setItems] = useState<ToastItem[]>([]);

  useEffect(() => {
    externalListeners.push(setItems);
    return () => {
      externalListeners = externalListeners.filter((l) => l !== setItems);
    };
  }, []);

  return (
    <div className="pointer-events-none fixed right-5 top-5 z-[100] flex w-[360px] max-w-[calc(100vw-2rem)] flex-col gap-3">
      <AnimatePresence>
        {items.map((t) => (
          <ToastCard key={t.id} item={t} />
        ))}
      </AnimatePresence>
    </div>
  );
}

function ToastCard({ item }: { item: ToastItem }) {
  const palette: Record<ToastKind, { Icon: typeof CheckCircle2; bar: string; iconCls: string }> = {
    success: { Icon: CheckCircle2, bar: "bg-emerald-500", iconCls: "text-emerald-400" },
    error:   { Icon: XCircle,      bar: "bg-red-500",     iconCls: "text-red-400" },
    warning: { Icon: AlertTriangle,bar: "bg-amber-500",   iconCls: "text-amber-400" },
    info:    { Icon: Info,         bar: "bg-sky-500",     iconCls: "text-sky-400" },
  };
  const { Icon, bar, iconCls } = palette[item.kind];

  return (
    <motion.div
      layout
      initial={{ opacity: 0, x: 60, scale: 0.95 }}
      animate={{ opacity: 1, x: 0, scale: 1 }}
      exit={{ opacity: 0, x: 60, scale: 0.95 }}
      transition={{ type: "spring", stiffness: 360, damping: 28 }}
      className="pointer-events-auto relative overflow-hidden rounded-xl border border-border/60 bg-card/95 shadow-2xl ring-1 ring-black/10 backdrop-blur-xl"
    >
      <div className={cn("absolute inset-y-0 left-0 w-1", bar)} />
      <div className="flex items-start gap-3 p-4 pl-5">
        <Icon className={cn("mt-0.5 h-5 w-5 shrink-0", iconCls)} />
        <div className="min-w-0 flex-1">
          <p className="text-sm font-semibold leading-snug">{item.title}</p>
          {item.description && (
            <p className="mt-1 text-xs leading-relaxed text-muted-foreground">{item.description}</p>
          )}
        </div>
        <button
          type="button"
          aria-label="dismiss"
          onClick={() => dismiss(item.id)}
          className="rounded-md p-1 text-muted-foreground transition hover:bg-accent hover:text-foreground"
        >
          <X className="h-3.5 w-3.5" />
        </button>
      </div>
      {item.duration > 0 && (
        <motion.div
          className={cn("absolute bottom-0 left-0 h-0.5", bar)}
          initial={{ width: "100%" }}
          animate={{ width: 0 }}
          transition={{ duration: item.duration / 1000, ease: "linear" }}
        />
      )}
    </motion.div>
  );
}
