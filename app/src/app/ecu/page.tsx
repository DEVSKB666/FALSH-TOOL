'use client';

import { useState } from 'react';
import { motion } from 'framer-motion';
import {
  AlertTriangle,
  ArrowRight,
  Cpu,
  Eraser,
  FileDown,
  Hash,
  Loader2,
  Play,
  RefreshCcw,
  Save,
  ShieldAlert,
} from 'lucide-react';
import { AppShell } from '@/components/app-shell';
import { Card, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Progress } from '@/components/ui/progress';
import { Modal } from '@/components/ui/modal';
import {
  isTauri,
  tauri,
  type EepromReadResult,
  type OperationResult,
} from '@/lib/tauri';
import { useConnection } from '@/lib/connection';
import { useKlineLog } from '@/lib/log';
import {
  useHistory,
  sha256Hex,
  type HistoryOp,
  type HistoryStatus,
} from '@/lib/history';
import { useSettings } from '@/lib/settings';
import { sound } from '@/lib/sounds';
import { toast } from '@/components/toast';
import { hexDump, cn } from '@/lib/utils';

type Op =
  | { kind: 'read'; variant: 'Keihin' | 'Shinden' }
  | { kind: 'reset'; variant: 'Keihin' | 'Shinden' }
  | { kind: 'format'; fill: 0x00 | 0xff }
  | { kind: 'dump'; size: '48K' | '64K' | '256K' };

interface OpDef {
  id: string;
  label: string;
  thaiSubtitle: string;
  icon: typeof Cpu;
  /** Tailwind colour preset for the card icon halo. */
  tone: 'blue' | 'amber' | 'red' | 'purple';
  danger?: boolean;
  op: Op;
}

const OPERATIONS: OpDef[] = [
  {
    id: 'read-kh',
    label: 'READ EEPROM (Keihin)',
    thaiSubtitle: 'อ่าน EEPROM 256 cells (512 bytes)',
    icon: Cpu,
    tone: 'blue',
    op: { kind: 'read', variant: 'Keihin' },
  },
  {
    id: 'read-sh',
    label: 'READ EEPROM (Shinden)',
    thaiSubtitle: 'อ่าน EEPROM 256 cells (512 bytes)',
    icon: Cpu,
    tone: 'blue',
    op: { kind: 'read', variant: 'Shinden' },
  },
  {
    id: 'reset-kh',
    label: 'ลบจำนวนการอัด (Keihin)',
    thaiSubtitle: 'Reset flash counter — one-shot frame',
    icon: RefreshCcw,
    tone: 'amber',
    op: { kind: 'reset', variant: 'Keihin' },
  },
  {
    id: 'reset-sh',
    label: 'ลบจำนวนการอัด (Shinden)',
    thaiSubtitle: 'Reset flash counter — sweep 0..127',
    icon: RefreshCcw,
    tone: 'amber',
    op: { kind: 'reset', variant: 'Shinden' },
  },
  {
    id: 'format-00',
    label: 'Format EEPROM 0x00',
    thaiSubtitle: 'เขียน 0x00 ทุก byte (Shinden)',
    icon: Eraser,
    tone: 'red',
    danger: true,
    op: { kind: 'format', fill: 0x00 },
  },
  {
    id: 'format-ff',
    label: 'Format EEPROM 0xFF',
    thaiSubtitle: 'เขียน 0xFF ทุก byte (Shinden)',
    icon: Eraser,
    tone: 'red',
    danger: true,
    op: { kind: 'format', fill: 0xff },
  },
  {
    id: 'dump-48k',
    label: 'ดูดไฟล์กล่อง 48K',
    thaiSubtitle: 'ROM dump 0x4000-0xFFFF (Shinden)',
    icon: FileDown,
    tone: 'purple',
    op: { kind: 'dump', size: '48K' },
  },
  {
    id: 'dump-64k',
    label: 'ดูดไฟล์กล่อง 64K',
    thaiSubtitle: 'ROM dump 0x8000-0xFFFF (Shinden)',
    icon: FileDown,
    tone: 'purple',
    op: { kind: 'dump', size: '64K' },
  },
  {
    id: 'dump-256k',
    label: 'ดูดไฟล์กล่อง 256K (ทดลอง)',
    thaiSubtitle: '4 × 64K page sweep · อาจไม่รองรับ ECU บางรุ่น',
    icon: FileDown,
    tone: 'amber',
    op: { kind: 'dump', size: '256K' },
  },
];

const TONE_CLASSES: Record<
  OpDef['tone'],
  { bg: string; ring: string; icon: string; glow: string }
> = {
  blue: {
    bg: 'bg-sky-500/10',
    ring: 'ring-sky-500/30',
    icon: 'text-sky-400',
    glow: 'shadow-[0_0_30px_rgba(14,165,233,0.18)]',
  },
  amber: {
    bg: 'bg-amber-500/10',
    ring: 'ring-amber-500/30',
    icon: 'text-amber-400',
    glow: 'shadow-[0_0_30px_rgba(245,158,11,0.18)]',
  },
  red: {
    bg: 'bg-red-500/10',
    ring: 'ring-red-500/30',
    icon: 'text-red-400',
    glow: 'shadow-[0_0_30px_rgba(239,68,68,0.22)]',
  },
  purple: {
    bg: 'bg-purple-500/10',
    ring: 'ring-purple-500/30',
    icon: 'text-purple-400',
    glow: 'shadow-[0_0_30px_rgba(168,85,247,0.18)]',
  },
};

export default function EcuPage() {
  const [openId, setOpenId] = useState<string | null>(null);
  const status = useConnection((s) => s.status);
  const selected = useConnection((s) => s.selectedDevice());
  const connected = status === 'connected' && !!selected;

  return (
    <AppShell>
      <div className="space-y-6">
        <header>
          <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">
            ECU TOOLS
          </p>
          <h1 className="mt-1 text-3xl font-bold tracking-tight">
            เครื่องมือ ECU
          </h1>
          <p className="mt-1 text-muted-foreground">
            กดที่การ์ดเพื่อเปิดหน้าต่างทำงาน — รองรับ Keihin · Shinden
          </p>
        </header>

        {/* Connection banner */}
        {!connected && (
          <Card className="border-amber-500/40">
            <CardContent className="flex items-center gap-3 p-4 text-sm">
              <AlertTriangle className="h-4 w-4 text-amber-500" />
              <span>
                ยังไม่ได้เชื่อมต่อ ECM — ไป{' '}
                <a href="/" className="text-primary underline">
                  หน้า Dashboard
                </a>{' '}
                แล้วกด <span className="font-medium">เชื่อมต่อ</span> ก่อน
              </span>
            </CardContent>
          </Card>
        )}

        {/* Operation grid */}
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {OPERATIONS.map((op, i) => (
            <OpCard
              key={op.id}
              def={op}
              index={i}
              connected={connected}
              onClick={() => setOpenId(op.id)}
            />
          ))}
        </div>

        {/* One modal instance, contents driven by openId */}
        <OpModal
          op={OPERATIONS.find((o) => o.id === openId) ?? null}
          onClose={() => setOpenId(null)}
        />
      </div>
    </AppShell>
  );
}

// ============================================================
// One operation card
// ============================================================

function OpCard({
  def,
  index,
  connected,
  onClick,
}: {
  def: OpDef;
  index: number;
  connected: boolean;
  onClick: () => void;
}) {
  const tone = TONE_CLASSES[def.tone];
  const Icon = def.icon;

  return (
    <motion.button
      type="button"
      onClick={onClick}
      whileHover={{ y: -2 }}
      whileTap={{ scale: 0.98 }}
      initial={{ opacity: 0, y: 14 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{
        delay: index * 0.04,
        type: 'spring',
        stiffness: 320,
        damping: 24,
      }}
      className={cn(
        'group relative flex flex-col items-start gap-3 overflow-hidden rounded-2xl border border-border/50 bg-card/60 p-5 text-left ring-1 ring-black/5 backdrop-blur-md transition',
        'hover:border-primary/40 hover:bg-card/80',
        tone.glow,
      )}
    >
      {/* Animated halo */}
      <motion.div
        aria-hidden
        className={cn(
          'pointer-events-none absolute inset-0 rounded-2xl opacity-0 transition group-hover:opacity-100',
          tone.bg,
        )}
        initial={false}
      />

      {/* Icon badge */}
      <div
        className={cn(
          'relative flex h-11 w-11 items-center justify-center rounded-xl ring-1',
          tone.bg,
          tone.ring,
        )}
      >
        <Icon className={cn('h-5 w-5', tone.icon)} />
      </div>

      {/* Label */}
      <div className="relative flex-1">
        <p className="font-semibold leading-tight">
          {def.danger && <span className="mr-1 text-amber-400">⚠</span>}
          {def.label}
        </p>
        <p className="mt-1 text-xs text-muted-foreground">{def.thaiSubtitle}</p>
      </div>

      {/* Footer arrow */}
      <div className="relative mt-1 flex w-full items-center justify-between text-[11px] text-muted-foreground">
        <span className="font-mono uppercase tracking-widest">
          {connected ? 'พร้อมเริ่ม' : 'รอเชื่อมต่อ'}
        </span>
        <ArrowRight
          className={cn(
            'h-3.5 w-3.5 transition group-hover:translate-x-1',
            tone.icon,
          )}
        />
      </div>
    </motion.button>
  );
}

// ============================================================
// Modal that runs the selected operation
// ============================================================

function OpModal({ op, onClose }: { op: OpDef | null; onClose: () => void }) {
  const open = op !== null;
  return (
    <Modal
      open={open}
      onClose={() => {
        // Don't allow closing while busy - the OpModalBody manages its own busy state.
        onClose();
      }}
      width="max-w-3xl"
      accent={
        op?.tone === 'red'
          ? 'ring-red-500/40'
          : op?.tone === 'amber'
            ? 'ring-amber-500/40'
            : ''
      }
      title={
        op && (
          <div className="flex items-center gap-3">
            <div
              className={cn(
                'flex h-9 w-9 items-center justify-center rounded-lg ring-1',
                TONE_CLASSES[op.tone].bg,
                TONE_CLASSES[op.tone].ring,
              )}
            >
              <op.icon className={cn('h-4 w-4', TONE_CLASSES[op.tone].icon)} />
            </div>
            <div>
              <p>
                {op.danger && <span className="mr-1 text-amber-400">⚠</span>}
                {op.label}
              </p>
              <p className="font-mono text-[10px] font-normal uppercase tracking-widest text-muted-foreground">
                {op.thaiSubtitle}
              </p>
            </div>
          </div>
        )
      }
    >
      {op && <OpModalBody def={op} onClose={onClose} />}
    </Modal>
  );
}

function OpModalBody({ def, onClose }: { def: OpDef; onClose: () => void }) {
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [readResult, setReadResult] = useState<EepromReadResult | null>(null);
  const [opResult, setOpResult] = useState<OperationResult | null>(null);

  const status = useConnection((s) => s.status);
  const selected = useConnection((s) => s.selectedDevice());
  const connected = status === 'connected' && !!selected;
  const addHistory = useHistory((s) => s.add);

  // Live progress comes from the global log store (the bridge in layout).
  const liveProgress = useKlineLog((s) => s.progress);
  const setStoreProgress = useKlineLog((s) => s.setProgress);
  const progress = {
    pct: liveProgress?.percent ?? 0,
    label: liveProgress?.label ?? '',
  };

  const bytes = readResult?.bytes ?? opResult?.bytes ?? null;
  const op = def.op;

  async function run() {
    if (!connected) {
      sound.error();
      toast.error(
        'ยังไม่ได้เชื่อมต่อ ECM',
        'ไปหน้า Dashboard แล้วกด เชื่อมต่อ ก่อน',
      );
      return;
    }
    if (def.danger) {
      const ok = window.confirm(
        `⚠️ คำเตือน: ${def.label}\n\n` +
          'การ Format จะ **ลบทุก byte ใน EEPROM** ของกล่อง ECU\n' +
          'การกระทำนี้ย้อนกลับไม่ได้ — ควรมี backup ก่อน\n\n' +
          'ดำเนินการต่อ?',
      );
      if (!ok) return;
    }

    setBusy(true);
    setError(null);
    setReadResult(null);
    setOpResult(null);
    setStoreProgress({
      label: 'เริ่มต้น…',
      current: 0,
      total: 1,
      percent: 0,
      ts: Date.now(),
    });
    sound.click();

    // Things we'll log into the history at the end of the run. Names
    // are intentionally suffixed to avoid shadowing the outer `status`
    // and `bytes` reactive values.
    let histKind: HistoryOp = 'read';
    let histStatus: HistoryStatus = 'success';
    let histBytes: number | undefined;
    let histPreview: number[] | undefined;
    let histDuration = 0;
    let histFamily: 'Keihin' | 'Shinden' = 'Keihin';
    let histNote: string | undefined;
    const startedAt = Date.now();

    try {
      const idx = selected?.index ?? 0;
      // For local mode `selected.backend` is `"d2xx"` or `"libusb"`;
      // for bridge mode it's `"bridge"` and the *daemon-side* backend
      // is in `daemon_backend`. We pick whichever is relevant.
      const localBackend = (
        selected?.backend === 'libusb' ? 'libusb' : 'd2xx'
      ) as 'd2xx' | 'libusb';
      const daemonBackend = (selected?.daemon_backend ?? 'd2xx') as
        | 'd2xx'
        | 'libusb';
      // If the user has enabled the remote bridge, route the *read* op
      // through it. (Other ops still go through the local path until we
      // mirror them on the bridge daemon.)
      const settings = useSettings.getState();
      const useBridge =
        settings.bridgeEnabled && settings.bridgeUrl.trim().length > 0;
      switch (op.kind) {
        case 'read': {
          histKind = 'read';
          histFamily = op.variant;
          const r = useBridge
            ? await tauri.readEepromViaBridge(
                settings.bridgeUrl.trim(),
                op.variant,
                idx,
                daemonBackend,
              )
            : await tauri.readEeprom(op.variant, idx, localBackend);
          setReadResult(r);
          histBytes = r.bytes.length;
          histPreview = r.bytes.slice(0, 64);
          histDuration = r.duration_ms;
          if (r.bytes.length === 0) {
            histStatus = 'failed';
            histNote = 'ECU ไม่ตอบ';
            sound.warning();
            toast.warning('ECU ไม่ตอบสนอง', 'Cable TX ทำงาน แต่กล่องเงียบ');
          } else {
            sound.success();
            toast.success(
              'อ่าน EEPROM สำเร็จ',
              `${r.bytes.length} bytes ใน ${(r.duration_ms / 1000).toFixed(1)}s`,
            );
          }
          break;
        }
        case 'reset': {
          histKind = 'reset';
          histFamily = op.variant;
          const r = await tauri.resetFlashCount(op.variant, idx, localBackend);
          setOpResult(r);
          histDuration = r.duration_ms;
          if (r.ok) {
            sound.success();
            toast.success(
              'รีเซ็ตจำนวนการอัดสำเร็จ',
              'หมุน/กุญแจสตาร์ท ปิด-เปิด 5 วิ',
            );
          } else {
            histStatus = 'failed';
            histNote = 'no ack';
            sound.warning();
            toast.warning('ECU ไม่ตอบ ack');
          }
          break;
        }
        case 'format': {
          histKind = 'format';
          histFamily = 'Shinden';
          const r = await tauri.formatEeprom(op.fill, idx, localBackend);
          setOpResult(r);
          histDuration = r.duration_ms;
          histNote = `fill = 0x${op.fill.toString(16).toUpperCase()}`;
          if (r.ok) {
            sound.success();
            toast.success(
              `Format 0x${op.fill.toString(16).toUpperCase()} สำเร็จ`,
            );
          } else {
            histStatus = 'failed';
            sound.error();
            toast.error('Format ล้มเหลว');
          }
          break;
        }
        case 'dump': {
          histKind = 'dump';
          histFamily = 'Shinden';
          const r = useBridge
            ? await tauri.dumpRomViaBridge(
                settings.bridgeUrl.trim(),
                op.size,
                idx,
                daemonBackend,
              )
            : await tauri.dumpRom(op.size, idx, localBackend);
          setOpResult(r);
          histDuration = r.duration_ms;
          histBytes = r.bytes?.length ?? 0;
          histPreview = r.bytes?.slice(0, 64);
          histNote = `${op.size} dump`;
          if (r.ok && r.bytes && r.bytes.length > 0) {
            sound.success();
            toast.success(
              `ROM Dump ${op.size} สำเร็จ`,
              `${r.bytes.length} bytes`,
            );
          } else {
            histStatus = 'failed';
            histNote = `${op.size} dump - 0 bytes`;
            sound.warning();
            toast.warning('ECU ไม่ตอบสนอง', 'ROM Dump ได้ 0 bytes');
          }
          break;
        }
      }
      setStoreProgress({
        label: 'เสร็จสิ้น',
        current: 1,
        total: 1,
        percent: 100,
        ts: Date.now(),
      });
    } catch (e) {
      const raw = String(e);
      setError(raw);
      histStatus = 'failed';
      histNote = humaniseError(raw);
      sound.error();
      toast.error('เกิดข้อผิดพลาด', humaniseError(raw));
    } finally {
      setBusy(false);
      // Log to history (best-effort - never throws).
      try {
        let hash: string | undefined;
        if (histPreview && histPreview.length > 0) {
          hash = await sha256Hex(histPreview);
        }
        addHistory({
          ecmId: '',
          ecuCode: histFamily === 'Keihin' ? 'Honda Keihin' : 'Honda Shinden',
          family: histFamily,
          operation: histKind,
          label: def.label,
          bytes: histBytes,
          preview: histPreview,
          hash,
          durationMs: histDuration || Date.now() - startedAt,
          storage: 'cache',
          status: histStatus,
          note: histNote,
        });
      } catch {
        // ignore history errors - they are not user-facing
      }
    }
  }

  async function saveToFile() {
    if (!bytes) return;
    if (!isTauri) {
      sound.warning();
      toast.warning('ใช้ได้เฉพาะใน Tauri', 'เปิดผ่าน npm run tauri:dev');
      return;
    }
    const date = new Date().toISOString().slice(0, 10);
    const suggested = `${def.id}_${date}.bin`;
    try {
      const { save } = await import('@tauri-apps/plugin-dialog');
      const { writeFile } = await import('@tauri-apps/plugin-fs');
      const path = await save({
        defaultPath: suggested,
        filters: [{ name: 'Binary', extensions: ['bin'] }],
      });
      if (!path) return;
      await writeFile(path, new Uint8Array(bytes));
      sound.success();
      toast.success('บันทึกไฟล์สำเร็จ', path);
    } catch (e) {
      sound.error();
      toast.error('บันทึกล้มเหลว', String(e));
    }
  }

  return (
    <div className="space-y-5">
      {/* Connection state */}
      <div
        className={cn(
          'flex items-center gap-2 rounded-md border px-3 py-2 text-xs',
          connected
            ? 'border-emerald-500/30 bg-emerald-500/5 text-emerald-300'
            : 'border-amber-500/30 bg-amber-500/5 text-amber-300',
        )}
      >
        <span
          className={cn(
            'h-2 w-2 rounded-full',
            connected ? 'bg-emerald-400' : 'bg-amber-400',
          )}
        />
        {connected ? (
          <>
            เชื่อมต่อแล้ว ·{' '}
            <span className="font-mono">
              [{selected?.backend.toUpperCase()}] {selected?.description}
            </span>
          </>
        ) : (
          <>ยังไม่ได้เชื่อมต่อ — ไป Dashboard กดเชื่อมต่อก่อน</>
        )}
      </div>

      {/* Progress */}
      {(busy || progress.pct > 0) && (
        <div>
          <Progress value={progress.pct} />
          <p className="mt-1.5 flex items-center justify-between text-[11px] text-muted-foreground">
            <span>{progress.label}</span>
            <span className="font-mono">{progress.pct}%</span>
          </p>
        </div>
      )}

      {/* Action row */}
      <div className="flex flex-wrap items-center gap-3">
        <Button onClick={run} disabled={busy || !connected}>
          {busy ? (
            <Loader2 className="h-4 w-4 animate-spin" />
          ) : (
            <Play className="h-4 w-4" />
          )}
          {busy ? 'กำลังทำงาน…' : '▶ เริ่ม'}
        </Button>
        {bytes && bytes.length > 0 && (
          <Button variant="outline" onClick={saveToFile}>
            <Save className="h-4 w-4" /> บันทึก {bytes.length.toLocaleString()}
            -byte .bin
          </Button>
        )}
        {def.danger && (
          <span className="ml-auto inline-flex items-center gap-1 text-xs text-amber-400">
            <AlertTriangle className="h-3.5 w-3.5" /> destructive — มี dialog
            ยืนยัน
          </span>
        )}
      </div>

      {/* Error */}
      {error && (
        <div className="flex items-start gap-2 rounded-md border border-destructive/40 bg-destructive/10 p-3 text-sm text-destructive">
          <ShieldAlert className="mt-0.5 h-4 w-4 shrink-0" />
          <span className="break-all">{error}</span>
        </div>
      )}

      {/* Result */}
      {(readResult || opResult) && (
        <div className="space-y-3 rounded-md border border-border/40 bg-muted/20 p-4">
          <div className="grid grid-cols-2 gap-3 text-sm md:grid-cols-4">
            <Stat
              label="Operation"
              value={
                readResult
                  ? `Read EEPROM (${readResult.family})`
                  : (opResult?.label ?? '')
              }
            />
            <Stat label="Bytes" value={`${bytes?.length ?? 0}`} mono />
            <Stat
              label="Duration"
              value={`${((readResult?.duration_ms ?? opResult?.duration_ms ?? 0) / 1000).toFixed(2)}s`}
              mono
            />
            <Stat
              label="Status"
              value={readResult ? 'OK' : opResult?.ok ? 'OK' : 'NO ACK'}
              mono
            />
          </div>

          {bytes && bytes.length > 0 && (
            <details open className="rounded bg-background/60 p-3">
              <summary className="flex cursor-pointer select-none items-center gap-2 font-mono text-xs uppercase tracking-widest text-muted-foreground">
                <Hash className="h-3.5 w-3.5" />
                Hex dump (preview · 256 bytes)
              </summary>
              <pre className="custom-scrollbar mt-3 max-h-72 overflow-auto whitespace-pre-wrap break-all font-mono text-[11px] text-foreground/80 selectable">
                {hexDump(bytes.slice(0, 256), ' ')}
              </pre>
            </details>
          )}
        </div>
      )}

      {/* Footer hint */}
      <p className="pt-2 text-center text-[11px] text-muted-foreground">
        กด ESC หรือคลิกพื้นหลังเพื่อปิด · K-Line log streaming อยู่ที่หน้า
        Dashboard
      </p>
    </div>
  );
}

function Stat({
  label,
  value,
  mono = false,
}: {
  label: string;
  value: string;
  mono?: boolean;
}) {
  return (
    <div>
      <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
        {label}
      </p>
      <p className={cn('mt-0.5 truncate', mono && 'font-mono')}>{value}</p>
    </div>
  );
}

function humaniseError(raw: string): string {
  const r = raw.toLowerCase();
  if (r.includes('recv') && r.includes('timeout'))
    return 'ECU ไม่ตอบสนอง — ตรวจสอบสาย, สวิตซ์รถ';
  if (r.includes('device not found') || r.includes('ft_open'))
    return 'ไม่พบสาย FTDI';
  if (r.includes('permission') || r.includes('access')) return raw;
  return raw;
}
