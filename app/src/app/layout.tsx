import type { Metadata, Viewport } from 'next';
import { Prompt, JetBrains_Mono } from 'next/font/google';
import { ThemeProvider } from '@/components/theme-provider';
import { AnimatedBackground } from '@/components/animated-background';
import { Toaster } from '@/components/toast';
import { SoundSync } from '@/components/sound-sync';
import { KlineLogBridge } from '@/components/kline-log-bridge';
import { LiveDataPump } from '@/components/livedata-pump';
import { LiveDataAlerts } from '@/components/livedata-alerts';
import './globals.css';

const prompt = Prompt({
  subsets: ['latin', 'thai'],
  weight: ['300', '400', '500', '600', '700'],
  variable: '--font-prompt',
  display: 'swap',
});

const jetbrains = JetBrains_Mono({
  subsets: ['latin'],
  weight: ['400', '500', '700'],
  variable: '--font-jetbrains',
  display: 'swap',
});

export const metadata: Metadata = {
  title: 'LOY-TUNER 2026',
  description: 'Honda Keihin / Shinden ECU flasher (Tauri 2 + Next.js)',
};

export const viewport: Viewport = {
  themeColor: '#000000',
  width: 'device-width',
  initialScale: 1,
  maximumScale: 1,
  userScalable: false,
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="th" suppressHydrationWarning>
      <body
        className={`${prompt.variable} ${jetbrains.variable} font-sans antialiased`}
      >
        <ThemeProvider>
          {/* Background lives behind everything (z-0). */}
          <AnimatedBackground />
          {/* Sync the sound-enabled flag from Zustand to the audio module. */}
          <SoundSync />
          {/* Subscribe to kline-log + kline-progress events globally so
              the log persists across page navigation. */}
          <KlineLogBridge />
          {/* Drive the live-data simulator/poller globally so the
              navbar battery + livedata page share one stream. */}
          <LiveDataPump />
          {/* Watch for sensor readings entering the danger band and
              surface them as toasts + warning sound. No-op when the
              user disables it in Settings. */}
          <LiveDataAlerts />
          {/* App content. */}
          <div className="relative z-10 flex h-screen w-screen flex-col">
            {children}
          </div>
          {/* Global toast container - renders on top of everything. */}
          <Toaster />
        </ThemeProvider>
      </body>
    </html>
  );
}
