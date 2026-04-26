"use client";

import { AnimatePresence, motion } from "framer-motion";
import { X } from "lucide-react";
import { useEffect } from "react";
import { cn } from "@/lib/utils";

interface ModalProps {
  open: boolean;
  onClose: () => void;
  title?: React.ReactNode;
  description?: React.ReactNode;
  children: React.ReactNode;
  /** Footer area (action buttons). Stays sticky on long content. */
  footer?: React.ReactNode;
  /** Tailwind max-width override - default `max-w-2xl`. */
  width?: string;
  /** Optional accent ring colour, e.g. `ring-red-500/40`. */
  accent?: string;
  /** Hide the default close (×) button - useful for confirm dialogs. */
  hideClose?: boolean;
}

/**
 * Animated modal/dialog with backdrop blur. ESC closes; backdrop click
 * closes; portal-free (rendered inline at z-[60]).
 */
export function Modal({
  open,
  onClose,
  title,
  description,
  children,
  footer,
  width = "max-w-2xl",
  accent,
  hideClose = false,
}: ModalProps) {
  useEffect(() => {
    if (!open) return;
    const handler = (e: KeyboardEvent) => {
      if (e.key === "Escape") onClose();
    };
    document.addEventListener("keydown", handler);
    document.body.style.overflow = "hidden";
    return () => {
      document.removeEventListener("keydown", handler);
      document.body.style.overflow = "";
    };
  }, [open, onClose]);

  return (
    <AnimatePresence>
      {open && (
        <motion.div
          className="fixed inset-0 z-[60] flex items-center justify-center p-4"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 0.18 }}
        >
          {/* Backdrop */}
          <motion.div
            className="absolute inset-0 bg-black/70 backdrop-blur-md"
            onClick={onClose}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
          />

          {/* Card */}
          <motion.div
            role="dialog"
            aria-modal="true"
            className={cn(
              "relative flex max-h-[88vh] w-full flex-col overflow-hidden rounded-2xl border border-border/50 bg-card/95 shadow-[0_30px_80px_-20px_rgba(0,0,0,0.6)] ring-1 ring-black/10 backdrop-blur-xl",
              width,
              accent && `ring-2 ${accent}`,
            )}
            initial={{ opacity: 0, scale: 0.95, y: 12 }}
            animate={{ opacity: 1, scale: 1,    y: 0  }}
            exit={{    opacity: 0, scale: 0.96, y: 8  }}
            transition={{ type: "spring", stiffness: 320, damping: 28 }}
          >
            {/* Header */}
            {(title || description) && (
              <div className="flex items-start gap-3 border-b border-border/40 bg-card/60 px-6 py-4">
                <div className="flex-1 min-w-0">
                  {title && <div className="text-base font-semibold leading-tight">{title}</div>}
                  {description && (
                    <p className="mt-1 text-xs text-muted-foreground">{description}</p>
                  )}
                </div>
                {!hideClose && (
                  <button
                    type="button"
                    onClick={onClose}
                    className="rounded-md p-1.5 text-muted-foreground transition hover:bg-accent hover:text-foreground"
                    aria-label="Close"
                  >
                    <X className="h-4 w-4" />
                  </button>
                )}
              </div>
            )}

            {/* Body (scrollable) */}
            <div className="custom-scrollbar flex-1 overflow-y-auto px-6 py-5">
              {children}
            </div>

            {/* Footer */}
            {footer && (
              <div className="flex items-center justify-end gap-2 border-t border-border/40 bg-card/60 px-6 py-3">
                {footer}
              </div>
            )}
          </motion.div>
        </motion.div>
      )}
    </AnimatePresence>
  );
}
