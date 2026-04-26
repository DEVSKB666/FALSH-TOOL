"use client";

import { Navbar } from "@/components/navbar";

/**
 * AppShell wraps every page with the persistent top navbar. The
 * connection bar + log drawer live ONLY on the home page (see
 * `app/page.tsx`); their underlying state is in Zustand stores so it
 * persists across navigation regardless of UI mount.
 */
export function AppShell({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex h-screen w-screen flex-col overflow-hidden">
      <Navbar />
      <main className="flex-1 overflow-auto">
        <div className="mx-auto max-w-7xl px-6 py-6 md:px-8 md:py-8">{children}</div>
      </main>
    </div>
  );
}
