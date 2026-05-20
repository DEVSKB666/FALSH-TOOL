'use client';

import { useState } from 'react';
import {
  CheckCircle2,
  Eye,
  Image as ImageIcon,
  Loader2,
  Network,
  Palette,
  RotateCcw,
  Sparkles,
  Type,
  XCircle,
} from 'lucide-react';
import { AppShell } from '@/components/app-shell';
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Switch } from '@/components/ui/switch';
import {
  useSettings,
  type ThemePreset,
  type BackgroundPreset,
} from '@/lib/settings';
import { tauri, isTauri } from '@/lib/tauri';
import { sound } from '@/lib/sounds';
import { toast } from '@/components/toast';
import { cn } from '@/lib/utils';

const THEMES: { id: ThemePreset; label: string; preview: string }[] = [
  { id: 'light', label: 'Light', preview: 'bg-white' },
  { id: 'dark', label: 'Dark', preview: 'bg-zinc-900' },
  {
    id: 'racing',
    label: 'Racing',
    preview: 'bg-gradient-to-br from-red-600 to-zinc-900',
  },
  {
    id: 'neon',
    label: 'Neon',
    preview: 'bg-gradient-to-br from-fuchsia-500 to-cyan-400',
  },
];

const BACKGROUNDS: { id: BackgroundPreset; label: string }[] = [
  { id: 'particles', label: 'Particles' },
  { id: 'circuit', label: 'Circuit' },
  { id: 'scanlines', label: 'Scan-lines' },
  { id: 'aurora', label: 'Aurora' },
  { id: 'off', label: 'Off' },
];

export default function SettingsPage() {
  const s = useSettings();

  function onLogoFile(e: React.ChangeEvent<HTMLInputElement>) {
    const file = e.target.files?.[0];
    if (!file) return;
    const fr = new FileReader();
    fr.onload = () =>
      s.setLogoDataUrl(typeof fr.result === 'string' ? fr.result : null);
    fr.readAsDataURL(file);
  }

  return (
    <AppShell>
      <div className="space-y-8">
        <header>
          <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">
            SETTINGS
          </p>
          <h1 className="mt-1 text-3xl font-bold tracking-tight">การตั้งค่า</h1>
          <p className="mt-1 text-muted-foreground">
            ปรับแต่ง brand / theme / background ตามใจ
          </p>
        </header>

        {/* Brand */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Type className="h-5 w-5 text-primary" /> Brand
            </CardTitle>
            <CardDescription>
              เปลี่ยนชื่อโปรแกรม + โลโก้ที่แสดงในหัวเรื่องและ splash
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-5">
            <div className="space-y-1.5">
              <Label htmlFor="appname">ชื่อโปรแกรม</Label>
              <Input
                id="appname"
                value={s.appName}
                onChange={(e) => s.setAppName(e.target.value)}
              />
            </div>
            <div className="space-y-1.5">
              <Label>โลโก้ (PNG / JPG / SVG)</Label>
              <div className="flex items-center gap-4">
                {s.logoDataUrl ? (
                  <img
                    src={s.logoDataUrl}
                    alt=""
                    className="h-14 w-14 rounded-lg object-cover ring-1 ring-border"
                  />
                ) : (
                  <div className="flex h-14 w-14 items-center justify-center rounded-lg border border-dashed border-border">
                    <ImageIcon className="h-5 w-5 text-muted-foreground" />
                  </div>
                )}
                <div className="flex-1">
                  <Input
                    type="file"
                    accept="image/*"
                    onChange={onLogoFile}
                    className="cursor-pointer"
                  />
                </div>
                {s.logoDataUrl && (
                  <Button
                    variant="outline"
                    onClick={() => s.setLogoDataUrl(null)}
                  >
                    <RotateCcw className="h-4 w-4" /> ลบ
                  </Button>
                )}
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Theme */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Palette className="h-5 w-5 text-primary" /> Theme
            </CardTitle>
            <CardDescription>เลือกชุดสีหลักของโปรแกรม</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-2 gap-3 md:grid-cols-4">
              {THEMES.map((t) => (
                <button
                  key={t.id}
                  onClick={() => s.setTheme(t.id)}
                  className={cn(
                    'group relative overflow-hidden rounded-xl border p-4 text-left transition',
                    s.theme === t.id
                      ? 'border-primary ring-2 ring-primary/40'
                      : 'border-border/40 hover:border-primary/50',
                  )}
                >
                  <div className={cn('h-20 w-full rounded-lg', t.preview)} />
                  <p className="mt-3 text-sm font-medium">{t.label}</p>
                  {s.theme === t.id && (
                    <div className="absolute right-2 top-2 rounded-full bg-primary px-1.5 py-0.5 text-[10px] font-bold text-primary-foreground">
                      ACTIVE
                    </div>
                  )}
                </button>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Background */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Sparkles className="h-5 w-5 text-primary" /> Animated Background
            </CardTitle>
            <CardDescription>
              พื้นหลังเคลื่อนไหวอย่างนุ่มนวลด้านหลังโปรแกรม
            </CardDescription>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-2 gap-3 md:grid-cols-5">
              {BACKGROUNDS.map((b) => (
                <button
                  key={b.id}
                  onClick={() => s.setBackground(b.id)}
                  className={cn(
                    'rounded-xl border px-4 py-3 text-sm font-medium transition',
                    s.background === b.id
                      ? 'border-primary bg-primary/10 text-primary ring-2 ring-primary/30'
                      : 'border-border/40 hover:border-primary/50',
                  )}
                >
                  {b.label}
                </button>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Splash */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Eye className="h-5 w-5 text-primary" /> Splash Screen
            </CardTitle>
            <CardDescription>หน้าจอโหลดก่อนเข้าโปรแกรมทุกครั้ง</CardDescription>
          </CardHeader>
          <CardContent className="flex items-center justify-between">
            <Label htmlFor="splash" className="cursor-pointer">
              แสดง Splash ตอนเปิดโปรแกรม
            </Label>
            <Switch
              id="splash"
              checked={s.showSplash}
              onCheckedChange={s.setShowSplash}
            />
          </CardContent>
        </Card>

        {/* Live data alerts */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Sparkles className="h-5 w-5 text-amber-400" /> Live-data Alerts
            </CardTitle>
            <CardDescription>
              แจ้งเตือน toast + เสียง เมื่อค่า sensor เข้าโซน{' '}
              <span className="text-amber-400">danger</span> (ค่า threshold
              ตั้งไว้ที่ <code className="font-mono">lib/livedata.ts</code>{' '}
              ของแต่ละ sensor)
            </CardDescription>
          </CardHeader>
          <CardContent className="flex items-center justify-between">
            <Label htmlFor="live-alerts" className="cursor-pointer">
              เปิดการแจ้งเตือนเมื่อค่าผิดปกติ
            </Label>
            <Switch
              id="live-alerts"
              checked={s.liveAlertsEnabled}
              onCheckedChange={s.setLiveAlertsEnabled}
            />
          </CardContent>
        </Card>

        {/* Auto-backup before flash */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <RotateCcw className="h-5 w-5 text-emerald-400" /> Auto-Backup
              ก่อน Flash
            </CardTitle>
            <CardDescription>
              ก่อนเขียนไฟล์ลงกล่อง ECU จะ dump ROM ปัจจุบันออกมาเป็น
              <code className="font-mono"> .bin</code> เก็บไว้ก่อนทุกครั้ง —
              กันเหนียวกรณีไฟล์ใหม่ไม่ทำงาน
            </CardDescription>
          </CardHeader>
          <CardContent className="flex items-center justify-between">
            <Label htmlFor="auto-backup" className="cursor-pointer">
              เปิด Auto-Backup ก่อนการอัดไฟล์
            </Label>
            <Switch
              id="auto-backup"
              checked={s.autoBackupBeforeFlash}
              onCheckedChange={s.setAutoBackupBeforeFlash}
            />
          </CardContent>
        </Card>

        {/* Remote Bridge */}
        <RemoteBridgeCard />

        <div className="flex justify-end">
          <Button variant="destructive" onClick={s.reset}>
            <RotateCcw className="h-4 w-4" /> รีเซ็ตเป็นค่าเริ่มต้น
          </Button>
        </div>
      </div>
    </AppShell>
  );
}

/* ============================================================
 *  Remote Bridge config card
 * ============================================================ */

function RemoteBridgeCard() {
  const bridgeUrl = useSettings((s) => s.bridgeUrl);
  const bridgeEnabled = useSettings((s) => s.bridgeEnabled);
  const setBridgeUrl = useSettings((s) => s.setBridgeUrl);
  const setBridgeEnabled = useSettings((s) => s.setBridgeEnabled);

  const [testing, setTesting] = useState(false);
  const [lastCheck, setLastCheck] = useState<'ok' | 'fail' | null>(null);
  const [lastErr, setLastErr] = useState<string | null>(null);

  async function onTest() {
    if (!bridgeUrl.trim()) {
      sound.error();
      toast.error('ยังไม่ได้กรอก Bridge URL');
      return;
    }
    if (!isTauri) {
      sound.warning();
      toast.warning('ใช้ได้เฉพาะใน Tauri', 'เปิดผ่าน npm run tauri:dev');
      return;
    }
    setTesting(true);
    setLastErr(null);
    sound.click();
    try {
      const ok = await tauri.bridgePing(bridgeUrl.trim());
      setLastCheck(ok ? 'ok' : 'fail');
      if (ok) {
        sound.success();
        toast.success('Bridge ตอบสนอง', `${bridgeUrl} พร้อมใช้งาน`);
      } else {
        sound.error();
        toast.error('Bridge ไม่ตอบ');
      }
    } catch (e) {
      const msg = String(e);
      setLastCheck('fail');
      setLastErr(msg);
      sound.error();
      toast.error('เชื่อมต่อ bridge ล้มเหลว', msg);
    } finally {
      setTesting(false);
    }
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Network className="h-5 w-5 text-primary" /> Remote Bridge
        </CardTitle>
        <CardDescription>
          ส่งคำสั่ง K-Line ผ่าน{' '}
          <code className="font-mono">loy-bridge.exe</code>{' '}
          ที่รันอยู่บนเครื่องอื่น (notebook + FTDI) ผ่าน WiFi/LAN
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-5">
        {/* URL */}
        <div className="space-y-1.5">
          <Label htmlFor="bridge-url">Bridge URL</Label>
          <Input
            id="bridge-url"
            placeholder="192.168.1.50:7878  หรือ tcp://192.168.1.50:7878"
            value={bridgeUrl}
            onChange={(e) => setBridgeUrl(e.target.value)}
            className="font-mono"
          />
          <p className="text-[11px] text-muted-foreground">
            รูปแบบ: <code className="font-mono">host:port</code> · ถ้าไม่ใส่
            port จะใช้ <code className="font-mono">7878</code> · ตัวอย่าง:{' '}
            <code className="font-mono">192.168.1.50</code> หรือ{' '}
            <code className="font-mono">tcp://10.0.0.5:7878</code>
          </p>
        </div>

        {/* Toggle */}
        <div className="flex items-center justify-between rounded-md border border-border/40 bg-background/40 p-3">
          <div>
            <Label
              htmlFor="bridge-enabled"
              className="cursor-pointer text-sm font-medium"
            >
              เปิดใช้งาน Bridge
            </Label>
            <p className="mt-0.5 text-[11px] text-muted-foreground">
              เมื่อเปิดแล้ว ทุกการ scan / connect / read EEPROM จะถูกส่งผ่าน
              bridge แทน FTDI ในเครื่องนี้
            </p>
          </div>
          <Switch
            id="bridge-enabled"
            checked={bridgeEnabled}
            onCheckedChange={setBridgeEnabled}
            disabled={!bridgeUrl.trim()}
          />
        </div>

        {/* Test connection */}
        <div className="flex flex-wrap items-center gap-3">
          <Button onClick={onTest} disabled={testing || !bridgeUrl.trim()}>
            {testing ? (
              <Loader2 className="h-4 w-4 animate-spin" />
            ) : (
              <Network className="h-4 w-4" />
            )}
            ทดสอบการเชื่อมต่อ
          </Button>
          {lastCheck === 'ok' && (
            <span className="inline-flex items-center gap-1.5 rounded-full bg-emerald-500/15 px-3 py-1 text-xs font-medium text-emerald-300 ring-1 ring-emerald-500/40">
              <CheckCircle2 className="h-3.5 w-3.5" /> เชื่อมต่อได้
            </span>
          )}
          {lastCheck === 'fail' && (
            <span className="inline-flex items-center gap-1.5 rounded-full bg-red-500/15 px-3 py-1 text-xs font-medium text-red-300 ring-1 ring-red-500/40">
              <XCircle className="h-3.5 w-3.5" /> เชื่อมต่อไม่ได้
            </span>
          )}
        </div>

        {lastErr && (
          <pre className="overflow-x-auto rounded-md border border-red-500/40 bg-red-500/10 p-2 font-mono text-[11px] text-red-200">
            {lastErr}
          </pre>
        )}

        <details className="rounded-md border border-border/40 bg-background/40 p-3">
          <summary className="cursor-pointer text-xs text-muted-foreground">
            วิธีใช้ + คำเตือน
          </summary>
          <ul className="mt-2 list-disc space-y-1 pl-5 text-[11px] text-muted-foreground">
            <li>
              รัน{' '}
              <code className="font-mono">
                loy-bridge.exe --bind 0.0.0.0:7878
              </code>{' '}
              บน notebook ที่ต่อ FTDI
            </li>
            <li>
              ใช้คำสั่ง <code className="font-mono">ipconfig</code> เพื่อหา IP
              ของ notebook
            </li>
            <li>กรอก IP:port ในช่อง Bridge URL ด้านบน → กดทดสอบ</li>
            <li>
              เปิด toggle → ทุกหน้า GUI จะใช้ bridge แทน FTDI ในเครื่องนี้
            </li>
            <li>
              ⚠ Bridge ไม่มี authentication — bind LAN ที่เชื่อถือเท่านั้น
            </li>
          </ul>
        </details>
      </CardContent>
    </Card>
  );
}
