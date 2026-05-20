'use client';

/**
 * Dedicated DTC (Diagnostic Trouble Codes) page.
 *
 * Currently exposes a single operation - **Clear DTC** - which sends
 * `WAKEUP + ESTABLISH + 72 05 60 03 26` against the connected Honda
 * Keihin ECU. The frame is a byte-for-byte port of
 * `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::toolStripMenuItem_35_Click`
 * (`array3 = { 114, 5, 96, 3, 38 }`).
 *
 * The C# original does not implement an explicit *read* DTC path
 * (the binary string-table also has no Thai/English DTC labels), so
 * we leave that section as a placeholder until we capture the right
 * frame off real hardware.
 */

import { useState } from 'react';
import { motion } from 'framer-motion';
import {
  AlertTriangle,
  CheckCircle2,
  Eraser,
  Loader2,
  Play,
  ShieldAlert,
  XCircle,
} from 'lucide-react';
import Link from 'next/link';
import { AppShell } from '@/components/app-shell';
import { Card, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { useConnection } from '@/lib/connection';
import { useHistory } from '@/lib/history';
import { sound } from '@/lib/sounds';
import { toast } from '@/components/toast';
import { cn } from '@/lib/utils';

export default function DtcPage() {
  const status = useConnection((s) => s.status);
  const selected = useConnection((s) => s.selectedDevice());
  const ecuId = useConnection((s) => s.ecuId);
  const clearDtc = useConnection((s) => s.clearDtc);
  const addHistory = useHistory((s) => s.add);
  const connected = status === 'connected' && !!selected;

  const [busy, setBusy] = useState(false);
  const [result, setResult] = useState<
    | { ok: true; durationMs: number; ts: number }
    | { ok: false; error: string; durationMs: number; ts: number }
    | null
  >(null);

  async function onClearDtc() {
    if (!connected) {
      sound.error();
      toast.error(
        'ยังไม่ได้เชื่อมต่อ ECM',
        'ไปหน้า Dashboard แล้วกด เชื่อมต่อ ก่อน',
      );
      return;
    }
    const yes = window.confirm(
      '⚠️ ยืนยันลบ DTC จากกล่อง ECU?\n\n' +
        'จะส่งเฟรม WAKEUP + ESTABLISH + 72 05 60 03 26\n' +
        'รหัสปัญหาทั้งหมดจะถูกลบ — ควรเช็คสาเหตุก่อนลบ\n\n' +
        'ดำเนินการต่อ?',
    );
    if (!yes) return;

    setBusy(true);
    setResult(null);
    sound.click();
    toast.info('กำลังลบ DTC…', 'ส่งคำสั่ง Erase Codes ไปที่กล่อง ECU');

    const startedAt = Date.now();
    try {
      const res = await clearDtc();
      const durationMs = Date.now() - startedAt;
      if (res.ok) {
        setResult({ ok: true, durationMs, ts: Date.now() });
        sound.success();
        toast.success(
          'ลบ DTC สำเร็จ',
          'ปิด/เปิดสวิตช์กุญแจประมาณ 5 วินาที แล้วลองสตาร์ทใหม่',
        );
        addHistory({
          ecmId: ecuId ?? '',
          ecuCode: 'Honda',
          family: 'Keihin',
          operation: 'reset',
          label: 'Clear DTC',
          durationMs,
          storage: 'cache',
          status: 'success',
          note: 'Clear DTC',
        });
      } else {
        const err = res.error ?? 'กล่องไม่ตอบกลับ';
        setResult({ ok: false, error: err, durationMs, ts: Date.now() });
        sound.error();
        toast.error('ลบ DTC ไม่สำเร็จ', err);
        addHistory({
          ecmId: ecuId ?? '',
          ecuCode: 'Honda',
          family: 'Keihin',
          operation: 'reset',
          label: 'Clear DTC',
          durationMs,
          storage: 'cache',
          status: 'failed',
          note: err,
        });
      }
    } finally {
      setBusy(false);
    }
  }

  return (
    <AppShell>
      <div className="space-y-6">
        <header>
          <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">
            DTC TOOLS
          </p>
          <h1 className="mt-1 text-3xl font-bold tracking-tight">
            ระบบ DTC — ลบรหัสปัญหา
          </h1>
          <p className="mt-1 text-muted-foreground">
            ลบโค้ดเซ็นเซอร์/ปัญหาที่กล่อง ECU ของรถ Honda Keihin / Shinden
          </p>
        </header>

        {!connected && (
          <Card className="border-amber-500/40">
            <CardContent className="flex items-center gap-3 p-4 text-sm">
              <AlertTriangle className="h-4 w-4 text-amber-500" />
              <span>
                ยังไม่ได้เชื่อมต่อ ECM — ไป{' '}
                <Link href="/" className="text-primary underline">
                  หน้า Dashboard
                </Link>{' '}
                แล้วกด <span className="font-medium">เชื่อมต่อ</span> ก่อน
              </span>
            </CardContent>
          </Card>
        )}

        <div className="grid grid-cols-1 gap-5 lg:grid-cols-[1.3fr_1fr]">
          {/* Main Clear DTC card */}
          <motion.div
            initial={{ opacity: 0, y: 8 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.25 }}
          >
            <Card className="relative overflow-hidden border-destructive/40">
              <div className="pointer-events-none absolute -left-12 -top-12 h-44 w-44 rounded-full bg-destructive/30 blur-3xl" />
              <div className="pointer-events-none absolute -right-10 -bottom-10 h-40 w-40 rounded-full bg-amber-500/20 blur-3xl" />

              <CardContent className="relative space-y-5 p-7">
                <div className="flex items-start gap-4">
                  <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl bg-destructive/15 ring-1 ring-destructive/40">
                    <Eraser className="h-6 w-6 text-destructive" />
                  </div>
                  <div className="min-w-0 flex-1">
                    <h2 className="text-xl font-bold">
                      ลบ DTC (Erase Trouble Codes)
                    </h2>
                    <p className="mt-1 text-sm text-muted-foreground">
                      ลบรหัสปัญหาทั้งหมดที่กล่องเก็บไว้ — รวมถึง MIL/Check Engine
                      flags, sensor faults
                    </p>
                  </div>
                </div>

                <div className="rounded-lg border border-border/50 bg-background/40 p-4">
                  <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
                    KWP Frame Sequence
                  </p>
                  <pre className="mt-2 whitespace-pre-wrap font-mono text-xs leading-relaxed text-foreground/90 selectable">
                    {`WAKEUP    : FE 04 72 8C
ESTABLISH : 72 05 00 F0 99
ERASE_DTC : 72 05 60 03 26   ← เฟรมหลัก`}
                  </pre>
                  <p className="mt-2 text-[11px] text-muted-foreground">
                    Port byte-for-byte จาก{' '}
                    <code className="font-mono">
                      MZA_TUNER_FLASH_2026/ns1/GForm12.cs::toolStripMenuItem_35_Click
                    </code>
                  </p>
                </div>

                <div className="flex flex-wrap items-center gap-3">
                  <Button
                    onClick={onClearDtc}
                    disabled={busy || !connected}
                    className={cn(
                      'h-11 gap-2 px-6',
                      busy || !connected
                        ? ''
                        : 'bg-destructive text-destructive-foreground hover:bg-destructive/90',
                    )}
                  >
                    {busy ? (
                      <Loader2 className="h-4 w-4 animate-spin" />
                    ) : (
                      <Play className="h-4 w-4" fill="currentColor" />
                    )}
                    {busy ? 'กำลังลบ DTC…' : '▶ ลบ DTC ทันที'}
                  </Button>
                  <span className="inline-flex items-center gap-1 text-xs text-amber-400">
                    <AlertTriangle className="h-3.5 w-3.5" /> มี dialog ยืนยันก่อน
                  </span>
                </div>

                {/* Result block */}
                {result && (
                  <div
                    className={cn(
                      'flex items-start gap-3 rounded-md border p-3 text-sm',
                      result.ok
                        ? 'border-emerald-500/40 bg-emerald-500/10 text-emerald-300'
                        : 'border-destructive/40 bg-destructive/10 text-destructive',
                    )}
                  >
                    {result.ok ? (
                      <CheckCircle2 className="mt-0.5 h-4 w-4 shrink-0" />
                    ) : (
                      <XCircle className="mt-0.5 h-4 w-4 shrink-0" />
                    )}
                    <div className="min-w-0 flex-1">
                      <p className="font-medium">
                        {result.ok ? 'ลบ DTC สำเร็จ' : 'ลบ DTC ไม่สำเร็จ'}
                      </p>
                      <p className="mt-0.5 text-xs opacity-80">
                        {result.ok
                          ? 'ปิด/เปิดสวิตช์กุญแจ ~5 วินาที แล้วลองสตาร์ทใหม่'
                          : (result as { error: string }).error}
                      </p>
                      <p className="mt-1 font-mono text-[10px] uppercase tracking-widest opacity-60">
                        {(result.durationMs / 1000).toFixed(2)}s ·{' '}
                        {new Date(result.ts).toLocaleTimeString()}
                      </p>
                    </div>
                  </div>
                )}
              </CardContent>
            </Card>
          </motion.div>

          {/* Sidebar info card */}
          <motion.div
            initial={{ opacity: 0, y: 8 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.25, delay: 0.05 }}
          >
            <Card className="h-full">
              <CardContent className="flex h-full flex-col gap-4 p-6">
                <div>
                  <p className="font-mono text-[10px] uppercase tracking-[0.4em] text-primary">
                    Status
                  </p>
                  <h3 className="mt-1 text-lg font-bold">การเชื่อมต่อปัจจุบัน</h3>
                </div>

                <div className="space-y-2 text-sm">
                  <Row
                    label="ECM"
                    value={connected ? 'Connected' : 'Disconnected'}
                    tone={connected ? 'ok' : 'warn'}
                  />
                  <Row label="ECM ID" value={ecuId ?? '—'} mono />
                  <Row
                    label="Backend"
                    value={
                      selected
                        ? `${selected.backend.toUpperCase()}#${selected.index}`
                        : '—'
                    }
                    mono
                  />
                </div>

                <div className="mt-auto rounded-lg border border-border/40 bg-muted/20 p-3 text-[11px] leading-relaxed text-muted-foreground">
                  <p className="mb-1 flex items-center gap-1.5 font-mono uppercase tracking-widest text-foreground/80">
                    <ShieldAlert className="h-3.5 w-3.5" /> ข้อแนะนำ
                  </p>
                  ก่อนลบ DTC ควร{' '}
                  <span className="text-foreground/90">
                    เช็คสาเหตุของ Check Engine
                  </span>{' '}
                  ก่อน — เพราะหลังลบ ถ้าปัญหายังไม่ได้แก้ ECU จะตั้งโค้ดใหม่ทันที
                  ที่ตรวจจับเซ็นเซอร์ผิดปกติอีก
                </div>
              </CardContent>
            </Card>
          </motion.div>
        </div>

        {/* Read DTC placeholder - we don't have the right frame yet */}
        <Card className="border-dashed border-border/50">
          <CardContent className="flex items-start gap-3 p-5 text-sm text-muted-foreground">
            <AlertTriangle className="mt-0.5 h-4 w-4 text-amber-500" />
            <div>
              <p className="font-medium text-foreground/80">
                Read DTC — ยังไม่รองรับ
              </p>
              <p className="mt-0.5">
                เครื่องมือต้นฉบับ MZA_TUNER_FLASH_2026 ไม่ได้ implement
                การอ่านโค้ด เพียงแต่มีฟังก์ชันลบ —
                หากต้องการอ่านรายละเอียดรหัสปัญหา
                ต้องใช้ scope/log จากเครื่อง OEM Honda HiM/HDS เพื่อ capture
                เฟรมก่อน
              </p>
            </div>
          </CardContent>
        </Card>
      </div>
    </AppShell>
  );
}

function Row({
  label,
  value,
  mono,
  tone,
}: {
  label: string;
  value: string;
  mono?: boolean;
  tone?: 'ok' | 'warn';
}) {
  return (
    <div className="flex items-center justify-between gap-3 rounded-md border border-border/40 bg-background/40 px-3 py-2">
      <span className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
        {label}
      </span>
      <span
        className={cn(
          'truncate text-right',
          mono && 'font-mono text-xs',
          tone === 'ok' && 'text-emerald-400',
          tone === 'warn' && 'text-amber-400',
        )}
      >
        {value}
      </span>
    </div>
  );
}
