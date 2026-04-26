"use client";

import { useMemo, useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import {
  AlertOctagon,
  CheckCircle2,
  ChevronRight,
  Clock,
  Cpu,
  Database,
  Download,
  Eraser,
  Eye,
  FileDown,
  FileText,
  Filter,
  Folder,
  Hash,
  Loader2,
  Power,
  RefreshCcw,
  Search,
  Trash2,
  TrendingUp,
  X,
} from "lucide-react";
import { AppShell } from "@/components/app-shell";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  formatDuration,
  formatTs,
  relativeThai,
  statsOf,
  useHistory,
  type HistoryEntry,
  type HistoryOp,
  type HistoryStatus,
} from "@/lib/history";
import { sound } from "@/lib/sounds";
import { toast } from "@/components/toast";
import { isTauri } from "@/lib/tauri";
import { hexDump, cn } from "@/lib/utils";

// ============================================================
// Page
// ============================================================

export default function HistoryPage() {
  const entries = useHistory((s) => s.entries);
  const remove  = useHistory((s) => s.remove);
  const clear   = useHistory((s) => s.clear);

  const [filter, setFilter] = useState("");
  const [statusFilter, setStatusFilter] = useState<HistoryStatus | "all">("all");
  const [opFilter, setOpFilter]         = useState<HistoryOp | "all">("all");
  const [showFilters, setShowFilters]   = useState(false);
  const [selectedId, setSelectedId]     = useState<string | null>(null);

  const filtered = useMemo(() => {
    const needle = filter.trim().toLowerCase();
    return entries.filter((e) => {
      if (statusFilter !== "all" && e.status     !== statusFilter) return false;
      if (opFilter     !== "all" && e.operation  !== opFilter)     return false;
      if (!needle) return true;
      return (
        e.ecmId.toLowerCase().includes(needle) ||
        e.ecuCode.toLowerCase().includes(needle) ||
        e.label.toLowerCase().includes(needle) ||
        (e.binFile ?? "").toLowerCase().includes(needle) ||
        (e.note    ?? "").toLowerCase().includes(needle)
      );
    });
  }, [entries, filter, statusFilter, opFilter]);

  const stats = useMemo(() => statsOf(entries), [entries]);
  const selected = useMemo(
    () => entries.find((e) => e.id === selectedId) ?? null,
    [entries, selectedId],
  );

  function pickRow(e: HistoryEntry) {
    sound.click();
    setSelectedId(e.id);
  }

  function onClear() {
    if (entries.length === 0) return;
    if (!window.confirm(`⚠️ ลบประวัติทั้งหมด ${entries.length} รายการ ใช่ไหม?`)) return;
    sound.warning();
    clear();
    toast.success("ล้างประวัติแล้ว");
  }

  return (
    <AppShell>
      <div className="space-y-6">
        {/* Header */}
        <header className="flex flex-wrap items-end justify-between gap-3">
          <div>
            <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">FLASH HISTORY</p>
            <h1 className="mt-1 flex items-center gap-2 text-3xl font-bold tracking-tight">
              <Clock className="h-7 w-7 text-primary" />
              ประวัติการอัด
            </h1>
            <p className="mt-1 text-muted-foreground">
              {entries.length === 0
                ? "ยังไม่มีประวัติ — ลองอ่าน / ดูดไฟล์ ที่หน้า ECU Tools"
                : `${entries.length} รายการทั้งหมด · ${stats.success} สำเร็จ`}
            </p>
          </div>
          {entries.length > 0 && (
            <Button variant="outline" onClick={onClear}>
              <Trash2 className="h-4 w-4" /> ล้างทั้งหมด
            </Button>
          )}
        </header>

        {/* Stats grid */}
        <div className="grid grid-cols-2 gap-3 md:grid-cols-4">
          <StatCard
            label="ประวัติทั้งหมด"
            value={String(stats.total)}
            sub="รายการ"
            icon={Clock}
            tone="red"
          />
          <StatCard
            label="อัดสำเร็จ"
            value={String(stats.success)}
            sub={`อัตราสำเร็จ ${stats.successRate}%`}
            icon={CheckCircle2}
            tone="green"
          />
          <StatCard
            label="อัดไม่สำเร็จ"
            value={String(stats.failed + stats.cancelled)}
            sub="ครั้ง"
            icon={AlertOctagon}
            tone="amber"
          />
          <StatCard
            label="รหัส ECM ที่อัดไป"
            value={String(stats.ecmCount)}
            sub="กล่อง"
            icon={Database}
            tone="blue"
          />
        </div>

        {/* Time + success-rate banner */}
        <Card className="border-border/40 bg-card/40">
          <CardContent className="flex flex-wrap items-center gap-4 px-4 py-3 text-sm">
            <div className="flex items-center gap-2">
              <Clock className="h-4 w-4 text-muted-foreground" />
              <span className="text-muted-foreground">เวลารวม:</span>
              <span className="font-semibold">{formatDuration(stats.totalMs)}</span>
            </div>
            <div className="hidden h-4 w-px bg-border md:block" />
            <div className="flex items-center gap-2">
              <TrendingUp className="h-4 w-4 text-emerald-400" />
              <span className="text-muted-foreground">อัตราสำเร็จที่อัดไป:</span>
              <span className="font-semibold text-emerald-300">{stats.successRate}%</span>
            </div>
          </CardContent>
        </Card>

        {/* Search + filter row */}
        <div className="flex flex-wrap items-center gap-2">
          <div className="relative flex-1 min-w-[260px]">
            <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
            <Input
              className="pl-10"
              placeholder="ค้นหา ECM ID, รหัส ECU, ชื่อไฟล์..."
              value={filter}
              onChange={(e) => setFilter(e.target.value)}
            />
          </div>
          <Button
            variant={showFilters ? "default" : "outline"}
            onClick={() => setShowFilters((v) => !v)}
          >
            <Filter className="h-4 w-4" /> ตัวกรอง
          </Button>
        </div>

        {/* Expandable filter panel */}
        <AnimatePresence initial={false}>
          {showFilters && (
            <motion.div
              initial={{ height: 0, opacity: 0 }}
              animate={{ height: "auto", opacity: 1 }}
              exit={{ height: 0, opacity: 0 }}
              transition={{ duration: 0.2 }}
              className="overflow-hidden"
            >
              <Card>
                <CardContent className="space-y-4 p-4">
                  {/* Status */}
                  <div>
                    <p className="mb-2 font-mono text-[10px] uppercase tracking-widest text-muted-foreground">สถานะ</p>
                    <div className="flex flex-wrap gap-2">
                      <FilterChip active={statusFilter === "all"}        onClick={() => setStatusFilter("all")}><Hash    className="h-3.5 w-3.5"/> ทั้งหมด</FilterChip>
                      <FilterChip active={statusFilter === "success"}    onClick={() => setStatusFilter("success")} tone="green"><CheckCircle2 className="h-3.5 w-3.5"/> สำเร็จ</FilterChip>
                      <FilterChip active={statusFilter === "failed"}     onClick={() => setStatusFilter("failed")}  tone="red"><AlertOctagon className="h-3.5 w-3.5"/> ล้มเหลว</FilterChip>
                      <FilterChip active={statusFilter === "cancelled"}  onClick={() => setStatusFilter("cancelled")} tone="amber"><X className="h-3.5 w-3.5"/> ยกเลิก</FilterChip>
                    </div>
                  </div>
                  {/* Operation kind */}
                  <div>
                    <p className="mb-2 font-mono text-[10px] uppercase tracking-widest text-muted-foreground">ประเภท</p>
                    <div className="flex flex-wrap gap-2">
                      <FilterChip active={opFilter === "all"}    onClick={() => setOpFilter("all")}><Hash      className="h-3.5 w-3.5"/> ทั้งหมด</FilterChip>
                      <FilterChip active={opFilter === "read"}   onClick={() => setOpFilter("read")}  ><Cpu       className="h-3.5 w-3.5"/> READ</FilterChip>
                      <FilterChip active={opFilter === "dump"}   onClick={() => setOpFilter("dump")}  ><FileDown  className="h-3.5 w-3.5"/> DUMP</FilterChip>
                      <FilterChip active={opFilter === "reset"}  onClick={() => setOpFilter("reset")} ><RefreshCcw className="h-3.5 w-3.5"/> RESET</FilterChip>
                      <FilterChip active={opFilter === "format"} onClick={() => setOpFilter("format")}><Eraser    className="h-3.5 w-3.5"/> FORMAT</FilterChip>
                      <FilterChip active={opFilter === "write"}  onClick={() => setOpFilter("write")} ><Power     className="h-3.5 w-3.5"/> WRITE</FilterChip>
                    </div>
                  </div>
                </CardContent>
              </Card>
            </motion.div>
          )}
        </AnimatePresence>

        {/* List */}
        {filtered.length === 0 ? (
          <Card>
            <CardContent className="flex flex-col items-center gap-3 py-16 text-center">
              <Clock className="h-10 w-10 text-muted-foreground/50" />
              <p className="text-sm text-muted-foreground">
                {entries.length === 0 ? "ยังไม่มีประวัติ" : "ไม่พบรายการที่ตรงตามตัวกรอง"}
              </p>
            </CardContent>
          </Card>
        ) : (
          <div className="space-y-2">
            {filtered.map((e, i) => (
              <HistoryRow key={e.id} entry={e} index={i} onClick={() => pickRow(e)} />
            ))}
          </div>
        )}
      </div>

      {/* Detail drawer */}
      <DetailDrawer
        entry={selected}
        onClose={() => setSelectedId(null)}
        onDelete={() => {
          if (!selected) return;
          if (!window.confirm(`⚠️ ลบรายการ ${selected.label} ใช่ไหม?`)) return;
          remove(selected.id);
          setSelectedId(null);
          sound.success();
          toast.success("ลบประวัติสำเร็จ");
        }}
      />
    </AppShell>
  );
}

// ============================================================
// Stat card
// ============================================================

const STAT_TONES = {
  red:    { bg: "bg-red-500/15",     ring: "ring-red-500/30",     icon: "text-red-400"   },
  green:  { bg: "bg-emerald-500/15", ring: "ring-emerald-500/30", icon: "text-emerald-400" },
  amber:  { bg: "bg-amber-500/15",   ring: "ring-amber-500/30",   icon: "text-amber-400" },
  blue:   { bg: "bg-sky-500/15",     ring: "ring-sky-500/30",     icon: "text-sky-400"   },
};

function StatCard({
  label,
  value,
  sub,
  icon: Icon,
  tone,
}: {
  label: string;
  value: string;
  sub?: string;
  icon: typeof Clock;
  tone: keyof typeof STAT_TONES;
}) {
  const t = STAT_TONES[tone];
  return (
    <motion.div
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0  }}
      transition={{ type: "spring", stiffness: 320, damping: 25 }}
    >
      <Card>
        <CardContent className="flex items-center gap-4 p-4">
          <div className="flex-1 min-w-0">
            <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">{label}</p>
            <p className="mt-1 text-3xl font-bold tracking-tight">{value}</p>
            {sub && <p className="mt-0.5 text-[11px] text-muted-foreground">{sub}</p>}
          </div>
          <div className={cn("flex h-11 w-11 items-center justify-center rounded-xl ring-1", t.bg, t.ring)}>
            <Icon className={cn("h-5 w-5", t.icon)} />
          </div>
        </CardContent>
      </Card>
    </motion.div>
  );
}

// ============================================================
// One row in the list
// ============================================================

const OP_META: Record<HistoryOp, { Icon: typeof Cpu; cls: string; label: string }> = {
  read:   { Icon: Cpu,        cls: "text-sky-400",     label: "READ"   },
  dump:   { Icon: FileDown,   cls: "text-purple-400",  label: "DUMP"   },
  reset:  { Icon: RefreshCcw, cls: "text-amber-400",   label: "RESET"  },
  format: { Icon: Eraser,     cls: "text-red-400",     label: "FORMAT" },
  write:  { Icon: Power,      cls: "text-emerald-400", label: "WRITE"  },
};

function HistoryRow({
  entry,
  index,
  onClick,
}: {
  entry: HistoryEntry;
  index: number;
  onClick: () => void;
}) {
  const meta = OP_META[entry.operation];
  const Icon = meta.Icon;
  return (
    <motion.button
      type="button"
      onClick={onClick}
      whileHover={{ x: 4 }}
      initial={{ opacity: 0, x: -8 }}
      animate={{ opacity: 1, x: 0  }}
      transition={{ delay: Math.min(index * 0.02, 0.4), type: "spring", stiffness: 320, damping: 24 }}
      className="group flex w-full items-center gap-3 rounded-xl border border-border/40 bg-card/40 p-3 text-left transition hover:border-primary/40 hover:bg-card/70"
    >
      {/* Op icon */}
      <div className={cn("flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-card ring-1 ring-border/40", meta.cls)}>
        <Icon className="h-5 w-5" />
      </div>

      {/* Main info */}
      <div className="flex-1 min-w-0">
        <div className="flex items-center gap-2">
          <p className="truncate font-semibold">{entry.label}</p>
          {entry.ecmId && (
            <span className="font-mono text-[11px] text-orange-400">{entry.ecmId}</span>
          )}
        </div>
        <p className="mt-0.5 truncate text-xs text-muted-foreground">
          {relativeThai(entry.timestamp)} · {formatDuration(entry.durationMs)}
          {typeof entry.bytes === "number" && entry.bytes > 0 && ` · ${entry.bytes.toLocaleString()} bytes`}
          {entry.note && ` · ${entry.note}`}
        </p>
      </div>

      {/* Status badge */}
      <StatusBadge status={entry.status} />

      {/* Arrow */}
      <ChevronRight className="h-4 w-4 shrink-0 text-muted-foreground transition group-hover:translate-x-1 group-hover:text-foreground" />
    </motion.button>
  );
}

function StatusBadge({ status }: { status: HistoryStatus }) {
  const map = {
    success:   { Icon: CheckCircle2, cls: "bg-emerald-500/15 text-emerald-300 ring-emerald-500/40", label: "สำเร็จ" },
    failed:    { Icon: AlertOctagon, cls: "bg-red-500/15 text-red-300 ring-red-500/40",             label: "ล้มเหลว" },
    cancelled: { Icon: X,            cls: "bg-amber-500/15 text-amber-300 ring-amber-500/40",       label: "ยกเลิก" },
  } as const;
  const m = map[status];
  return (
    <span className={cn("inline-flex items-center gap-1 rounded-full px-2.5 py-1 text-[11px] font-medium ring-1", m.cls)}>
      <m.Icon className="h-3 w-3" />
      {m.label}
    </span>
  );
}

// ============================================================
// Filter chip
// ============================================================

function FilterChip({
  active,
  onClick,
  tone,
  children,
}: {
  active: boolean;
  onClick: () => void;
  tone?: "green" | "red" | "amber";
  children: React.ReactNode;
}) {
  const activeCls =
    tone === "green" ? "bg-emerald-500/20 text-emerald-300 ring-emerald-500/50" :
    tone === "red"   ? "bg-red-500/20 text-red-300 ring-red-500/50" :
    tone === "amber" ? "bg-amber-500/20 text-amber-300 ring-amber-500/50" :
                       "bg-primary/15 text-primary ring-primary/40";
  return (
    <button
      type="button"
      onClick={onClick}
      className={cn(
        "inline-flex items-center gap-1.5 rounded-full px-3 py-1 text-xs font-medium ring-1 transition",
        active ? activeCls : "bg-muted/40 text-muted-foreground ring-border/40 hover:bg-muted/60 hover:text-foreground",
      )}
    >
      {children}
    </button>
  );
}

// ============================================================
// Detail drawer (slide in from right)
// ============================================================

function DetailDrawer({
  entry,
  onClose,
  onDelete,
}: {
  entry: HistoryEntry | null;
  onClose: () => void;
  onDelete: () => void;
}) {
  return (
    <AnimatePresence>
      {entry && (
        <motion.div
          className="fixed inset-0 z-[60]"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 0.18 }}
        >
          {/* Backdrop */}
          <motion.div
            className="absolute inset-0 bg-black/60 backdrop-blur-sm"
            onClick={onClose}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
          />

          {/* Drawer */}
          <motion.aside
            className="absolute right-0 top-0 flex h-full w-full max-w-md flex-col overflow-hidden border-l border-border/50 bg-card/95 shadow-[-30px_0_60px_-20px_rgba(0,0,0,0.6)] backdrop-blur-xl"
            initial={{ x: "100%" }}
            animate={{ x: 0     }}
            exit={{    x: "100%" }}
            transition={{ type: "spring", stiffness: 320, damping: 30 }}
          >
            {/* Header */}
            <div className="flex items-start gap-3 border-b border-border/40 px-5 py-4">
              <div className="flex-1 min-w-0">
                <p className="text-sm font-semibold">รายละเอียดการอัดไฟล์</p>
                <p className="mt-0.5 text-xs text-muted-foreground">{formatTs(entry.timestamp)}</p>
              </div>
              <button
                type="button"
                onClick={onClose}
                className="rounded-md p-1.5 text-muted-foreground hover:bg-accent hover:text-foreground"
              >
                <X className="h-4 w-4" />
              </button>
            </div>

            {/* Body */}
            <div className="custom-scrollbar flex-1 overflow-y-auto px-5 py-4 space-y-3">
              {/* Status hero */}
              <div className={cn(
                "flex items-center gap-3 rounded-xl border p-4",
                entry.status === "success"   ? "border-emerald-500/40 bg-emerald-500/10" :
                entry.status === "failed"    ? "border-red-500/40 bg-red-500/10" :
                                               "border-amber-500/40 bg-amber-500/10",
              )}>
                {entry.status === "success" ? <CheckCircle2 className="h-7 w-7 text-emerald-400"/> :
                 entry.status === "failed"  ? <AlertOctagon className="h-7 w-7 text-red-400"/> :
                                              <X            className="h-7 w-7 text-amber-400"/>}
                <div className="flex-1">
                  <p className="text-base font-semibold">
                    {entry.status === "success" ? "สำเร็จ" : entry.status === "failed" ? "ล้มเหลว" : "ยกเลิก"}
                  </p>
                  <p className="text-xs text-muted-foreground">
                    {entry.label}
                    {typeof entry.flashCount === "number" && ` · จำนวน FlashCount ล่าสุด ${entry.flashCount}`}
                  </p>
                </div>
                <StatusBadge status={entry.status} />
              </div>

              {/* Field rows */}
              <DetailRow icon={Hash} label="ECM ID"     value={entry.ecmId || "—"} mono valueClass="text-orange-400" />
              <DetailRow icon={Cpu}  label="รหัส ECU"   value={entry.ecuCode}      mono />
              <DetailRow icon={Clock} label="วันที่อัด" value={formatTs(entry.timestamp)} />
              <DetailRow icon={Loader2} label="ระยะเวลา" value={formatDuration(entry.durationMs)} />
              {entry.binFile && (
                <DetailRow icon={FileText} label="ไฟล์ BIN" value={entry.binFile} mono />
              )}
              {entry.hash && (
                <DetailRow
                  icon={Hash}
                  label="Hash"
                  value={`${entry.hash.slice(0, 10)}...${entry.hash.slice(-12)}`}
                  mono
                  full={entry.hash}
                />
              )}
              <DetailRow icon={Folder} label="การเก็บไฟล์" value={entry.storage === "saved" ? "บันทึกแล้ว" : "เก็บไว้ใน Cache"} />
              {entry.note && (
                <DetailRow icon={FileText} label="หมายเหตุ" value={entry.note} />
              )}
              {typeof entry.bytes === "number" && entry.bytes > 0 && (
                <DetailRow icon={Database} label="ขนาดข้อมูล" value={`${entry.bytes.toLocaleString()} bytes`} mono />
              )}

              {/* Hex preview */}
              {entry.preview && entry.preview.length > 0 && (
                <details className="rounded-md border border-border/40 bg-background/40">
                  <summary className="cursor-pointer select-none px-3 py-2 text-xs font-medium text-muted-foreground hover:bg-accent/30">
                    <Eye className="mr-1.5 inline h-3 w-3" />
                    Hex preview ({entry.preview.length} bytes)
                  </summary>
                  <pre className="custom-scrollbar max-h-48 overflow-auto whitespace-pre-wrap break-all bg-black/40 px-3 py-2 font-mono text-[10px] text-foreground/70">
                    {hexDump(entry.preview, " ")}
                  </pre>
                </details>
              )}
            </div>

            {/* Footer actions */}
            <div className="grid grid-cols-2 gap-2 border-t border-border/40 bg-card/60 px-5 py-3">
              <Button
                variant="default"
                className="bg-red-600 hover:bg-red-500 text-white"
                disabled={!entry.preview || entry.preview.length === 0}
                onClick={async () => {
                  if (!entry.preview) return;
                  if (!isTauri) {
                    sound.warning();
                    toast.warning("ใช้ได้เฉพาะ Tauri", "เปิดผ่าน npm run tauri:dev");
                    return;
                  }
                  try {
                    const { save } = await import("@tauri-apps/plugin-dialog");
                    const { writeFile } = await import("@tauri-apps/plugin-fs");
                    const path = await save({
                      defaultPath: entry.binFile ?? `${entry.ecuCode}_${entry.id}.bin`,
                      filters: [{ name: "Binary", extensions: ["bin"] }],
                    });
                    if (!path) return;
                    await writeFile(path, new Uint8Array(entry.preview));
                    sound.success();
                    toast.success("บันทึกไฟล์สำเร็จ", path);
                  } catch (e) {
                    sound.error();
                    toast.error("บันทึกล้มเหลว", String(e));
                  }
                }}
              >
                <Download className="h-4 w-4" /> โหลดไฟล์ BIN
              </Button>
              <Button
                variant="outline"
                onClick={async () => {
                  const text = JSON.stringify(entry, null, 2);
                  try {
                    await navigator.clipboard.writeText(text);
                    sound.success();
                    toast.success("Export สำเร็จ", "JSON ถูกคัดลอกลง clipboard");
                  } catch {
                    sound.error();
                    toast.error("Copy ล้มเหลว");
                  }
                }}
              >
                <Download className="h-4 w-4" /> Export
              </Button>
              <Button variant="outline" disabled>
                <Folder className="h-4 w-4" /> เปิดโฟลเดอร์
              </Button>
              <Button
                variant="outline"
                onClick={onDelete}
                className="border-red-500/40 text-red-300 hover:bg-red-500/10"
              >
                <Trash2 className="h-4 w-4" /> ลบประวัติ
              </Button>
            </div>
          </motion.aside>
        </motion.div>
      )}
    </AnimatePresence>
  );
}

function DetailRow({
  icon: Icon,
  label,
  value,
  mono = false,
  valueClass,
  full,
}: {
  icon: typeof Hash;
  label: string;
  value: string;
  mono?: boolean;
  valueClass?: string;
  full?: string;
}) {
  return (
    <div className="flex items-start gap-3 rounded-md border border-border/40 bg-background/40 px-3 py-2.5">
      <Icon className="mt-0.5 h-4 w-4 shrink-0 text-muted-foreground" />
      <div className="min-w-0 flex-1">
        <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">{label}</p>
        <p
          className={cn("mt-0.5 truncate text-sm", mono && "font-mono", valueClass)}
          title={full}
        >
          {value}
        </p>
      </div>
    </div>
  );
}
