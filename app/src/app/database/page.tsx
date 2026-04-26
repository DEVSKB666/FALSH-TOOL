"use client";

import { useEffect, useMemo, useState } from "react";
import {
  Database,
  Loader2,
  Plus,
  Save,
  Search,
  Trash2,
  X,
} from "lucide-react";
import { AppShell } from "@/components/app-shell";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select } from "@/components/ui/select";
import { tauri, type EcuEntry } from "@/lib/tauri";
import { sound } from "@/lib/sounds";
import { toast } from "@/components/toast";
import { cn } from "@/lib/utils";

type FamilyFilter = "All" | "Keihin" | "Shinden";

interface FormState {
  /** Empty string = "create new" mode; non-empty = "edit existing" mode. */
  id: string;
  family: "Keihin" | "Shinden";
  part_code: string;
  ecm_id: string;
  start_offset: string;  // hex string for the input
  cksum_offset: string;  // hex string for the input
}

const EMPTY_FORM: FormState = {
  id: "",
  family: "Keihin",
  part_code: "",
  ecm_id: "",
  start_offset: "0",
  cksum_offset: "0",
};

export default function DatabasePage() {
  const [entries, setEntries] = useState<EcuEntry[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError]     = useState<string | null>(null);

  const [filter, setFilter]   = useState("");
  const [family, setFamily]   = useState<FamilyFilter>("All");

  const [form, setForm]       = useState<FormState>(EMPTY_FORM);
  const [busy, setBusy]       = useState(false);

  const isEditing = form.id !== "";

  async function reload() {
    setLoading(true);
    try {
      const d = await tauri.listEcus();
      setEntries(d);
      setError(null);
    } catch (e) {
      setError(String(e));
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    reload();
  }, []);

  const filtered = useMemo(() => {
    const needle = filter.trim().toLowerCase();
    return entries.filter((e) => {
      if (family !== "All" && e.family !== family) return false;
      if (!needle) return true;
      return (
        e.part_code.toLowerCase().includes(needle) ||
        e.ecm_id.toLowerCase().includes(needle) ||
        e.id.toLowerCase().includes(needle)
      );
    });
  }, [entries, filter, family]);

  function pickRow(e: EcuEntry) {
    sound.click();
    setForm({
      id: e.id,
      family: e.family as "Keihin" | "Shinden",
      part_code: e.part_code,
      ecm_id: e.ecm_id,
      start_offset: e.start_offset.toString(16).toUpperCase(),
      cksum_offset: e.cksum_offset.toString(16).toUpperCase(),
    });
  }

  function clearForm() {
    sound.click();
    setForm(EMPTY_FORM);
  }

  function parseHex(s: string): number | null {
    const t = s.trim().replace(/^0x/i, "");
    if (!t) return 0;
    if (!/^[0-9A-Fa-f]+$/.test(t)) return null;
    const n = parseInt(t, 16);
    return Number.isFinite(n) ? n : null;
  }

  async function onAdd() {
    if (!form.part_code.trim() || !form.ecm_id.trim()) {
      sound.error();
      toast.error("กรอกข้อมูลไม่ครบ", "ต้องมีรหัสพาร์ทและ ECM_ID");
      return;
    }
    const start = parseHex(form.start_offset);
    const cksum = parseHex(form.cksum_offset);
    if (start === null || cksum === null) {
      sound.error();
      toast.error("ค่า offset ผิด", "ต้องเป็น hex (เช่น 0, 4000, 3FFF8)");
      return;
    }
    setBusy(true);
    try {
      const newId = await tauri.addEcuEntry(form.family, form.part_code, form.ecm_id, start, cksum);
      sound.success();
      toast.success("เพิ่มข้อมูลใหม่สำเร็จ", `${newId} · ${form.part_code}`);
      setForm(EMPTY_FORM);
      await reload();
    } catch (e) {
      sound.error();
      toast.error("เพิ่มข้อมูลล้มเหลว", String(e));
    } finally {
      setBusy(false);
    }
  }

  async function onUpdate() {
    if (!isEditing) return;
    const start = parseHex(form.start_offset);
    const cksum = parseHex(form.cksum_offset);
    if (start === null || cksum === null) {
      sound.error();
      toast.error("ค่า offset ผิด", "ต้องเป็น hex");
      return;
    }
    setBusy(true);
    try {
      const ok = await tauri.updateEcuEntry(form.family, form.id, form.part_code, form.ecm_id, start, cksum);
      if (ok) {
        sound.success();
        toast.success("บันทึกการแก้ไขสำเร็จ", `${form.id} · ${form.part_code}`);
        await reload();
      } else {
        sound.warning();
        toast.warning("ไม่พบ entry", `${form.id} อาจถูกลบไปแล้ว`);
      }
    } catch (e) {
      sound.error();
      toast.error("บันทึกล้มเหลว", String(e));
    } finally {
      setBusy(false);
    }
  }

  async function onDelete() {
    if (!isEditing) return;
    const ok = window.confirm(`⚠️ ลบ ${form.id} (${form.part_code}) ใช่ไหม?\n\nการกระทำนี้ย้อนกลับไม่ได้`);
    if (!ok) return;
    setBusy(true);
    try {
      const removed = await tauri.deleteEcuEntry(form.family, form.id);
      if (removed) {
        sound.success();
        toast.success("ลบข้อมูลสำเร็จ", `${form.id} · ${form.part_code}`);
        setForm(EMPTY_FORM);
        await reload();
      } else {
        sound.warning();
        toast.warning("ไม่พบ entry", form.id);
      }
    } catch (e) {
      sound.error();
      toast.error("ลบล้มเหลว", String(e));
    } finally {
      setBusy(false);
    }
  }

  return (
    <AppShell>
      <div className="space-y-6">
        <header>
          <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">ECU DATABASE</p>
          <h1 className="mt-1 text-3xl font-bold tracking-tight">ฐานข้อมูล ECU</h1>
          <p className="mt-1 text-muted-foreground">
            {loading ? "กำลังโหลด…" : `${entries.length} รุ่น · เพิ่ม / แก้ไข / ลบ ได้`}
          </p>
        </header>

        {/* Two-column layout: list (left) + form (right) */}
        <div className="grid grid-cols-1 gap-5 lg:grid-cols-[1.5fr_1fr]">
          {/* ----- Left: search + table ----- */}
          <Card>
            <CardHeader className="pb-3">
              <CardTitle className="flex items-center gap-2 text-base">
                <Database className="h-4 w-4 text-primary" /> ค้นหา / รายการ
              </CardTitle>
              <CardDescription className="text-xs">
                คลิกแถวเพื่อแก้ไขทางขวา · กรอก part code / ECM_ID / ID
              </CardDescription>
            </CardHeader>
            <CardContent className="space-y-3">
              <div className="grid grid-cols-1 gap-2 md:grid-cols-[1fr_140px]">
                <div className="relative">
                  <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                  <Input
                    className="pl-10"
                    placeholder="38770-K2F-N01 / 0104080F01"
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                  />
                </div>
                <Select value={family} onChange={(e) => setFamily(e.target.value as FamilyFilter)}>
                  <option value="All">ทั้งหมด</option>
                  <option value="Keihin">Keihin</option>
                  <option value="Shinden">Shinden</option>
                </Select>
              </div>

              {error && (
                <div className="rounded border border-destructive/40 bg-destructive/10 p-3 text-sm text-destructive">
                  {error}
                </div>
              )}

              <div className="overflow-hidden rounded-lg border border-border/40">
                <div className="grid grid-cols-[70px_50px_1fr_120px_70px_80px] gap-2 border-b border-border/40 bg-muted/40 px-3 py-2 font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
                  <div>ID</div>
                  <div>FAM</div>
                  <div>PART</div>
                  <div>ECM_ID</div>
                  <div className="text-right">START</div>
                  <div className="text-right">CKSUM</div>
                </div>
                <div className="custom-scrollbar max-h-[58vh] overflow-auto">
                  {loading ? (
                    <div className="flex items-center justify-center gap-2 p-8 text-muted-foreground">
                      <Loader2 className="h-4 w-4 animate-spin" /> กำลังโหลด…
                    </div>
                  ) : filtered.length === 0 ? (
                    <div className="p-8 text-center text-muted-foreground">ไม่พบผลลัพธ์</div>
                  ) : (
                    filtered.map((e) => {
                      const isSelected = form.id === e.id && form.family === e.family;
                      return (
                        <button
                          key={`${e.family}-${e.id}`}
                          type="button"
                          onClick={() => pickRow(e)}
                          className={cn(
                            "grid w-full grid-cols-[70px_50px_1fr_120px_70px_80px] gap-2 border-b border-border/20 px-3 py-1.5 text-left text-xs last:border-b-0 transition selectable",
                            isSelected
                              ? "bg-primary/15 text-foreground ring-1 ring-primary/40"
                              : "hover:bg-accent/30",
                          )}
                        >
                          <div className="font-mono text-[10px] text-muted-foreground">{e.id}</div>
                          <div className={e.family === "Keihin" ? "text-blue-400" : "text-pink-400"}>
                            {e.family === "Keihin" ? "Kh" : "Sh"}
                          </div>
                          <div className="truncate">{e.part_code}</div>
                          <div className="font-mono text-[10px]">{e.ecm_id}</div>
                          <div className="text-right font-mono text-[10px]">{e.start_offset.toString(16).toUpperCase()}</div>
                          <div className="text-right font-mono text-[10px]">{e.cksum_offset.toString(16).toUpperCase()}</div>
                        </button>
                      );
                    })
                  )}
                </div>
              </div>
            </CardContent>
          </Card>

          {/* ----- Right: editor form ----- */}
          <Card>
            <CardHeader className="pb-3">
              <CardTitle className="flex items-center gap-2 text-base">
                {isEditing ? "✎ แก้ไขข้อมูล" : "✚ เพิ่มข้อมูลใหม่"}
                {isEditing && (
                  <span className="ml-auto inline-flex items-center gap-1 font-mono text-[10px] text-muted-foreground">
                    {form.id}
                    <button
                      type="button"
                      onClick={clearForm}
                      title="ล้างฟอร์ม (กลับสู่โหมดเพิ่มข้อมูลใหม่)"
                      className="rounded p-0.5 hover:bg-accent hover:text-foreground"
                    >
                      <X className="h-3 w-3" />
                    </button>
                  </span>
                )}
              </CardTitle>
              <CardDescription className="text-xs">
                คลิกแถวซ้ายเพื่อโหลดข้อมูลมาแก้ · ฟอร์มว่าง = โหมดเพิ่มข้อมูลใหม่
              </CardDescription>
            </CardHeader>
            <CardContent className="space-y-4">
              {/* Family */}
              <div className="space-y-1.5">
                <Label className="text-xs uppercase tracking-widest text-muted-foreground">ตระกูล (FAMILY)</Label>
                <Select
                  value={form.family}
                  onChange={(e) => setForm((f) => ({ ...f, family: e.target.value as "Keihin" | "Shinden" }))}
                  disabled={isEditing /* family is part of the key path */}
                >
                  <option value="Keihin">Keihin (Kh)</option>
                  <option value="Shinden">Shinden (Sh)</option>
                </Select>
              </div>

              {/* Part code */}
              <FormRow label="◢ รหัสพาร์ท / รุ่นรถ ◣">
                <Input
                  value={form.part_code}
                  onChange={(e) => setForm((f) => ({ ...f, part_code: e.target.value }))}
                  placeholder="38770-K2F-N01"
                  className="font-mono"
                />
              </FormRow>

              {/* ECM id */}
              <FormRow label="◢ หมายเลขลายเซ็น ECM_ID ◣">
                <Input
                  value={form.ecm_id}
                  onChange={(e) => setForm((f) => ({ ...f, ecm_id: e.target.value.toUpperCase() }))}
                  placeholder="0104080F01"
                  className="font-mono"
                />
              </FormRow>

              {/* Start offset */}
              <FormRow label="◢ ตำแหน่งเริ่มหน่วยความจำ (HEX) ◣">
                <Input
                  value={form.start_offset}
                  onChange={(e) => setForm((f) => ({ ...f, start_offset: e.target.value.toUpperCase() }))}
                  placeholder="0"
                  className="font-mono"
                />
              </FormRow>

              {/* Cksum offset */}
              <FormRow label="◢ ตำแหน่งเช็คซัม (CHECKSUM) ◣">
                <Input
                  value={form.cksum_offset}
                  onChange={(e) => setForm((f) => ({ ...f, cksum_offset: e.target.value.toUpperCase() }))}
                  placeholder="3FFF8"
                  className="font-mono"
                />
              </FormRow>

              {/* Action buttons */}
              <div className="space-y-2 pt-2">
                <ActionButton
                  onClick={onAdd}
                  disabled={busy || isEditing}
                  tone="blue"
                  icon={Plus}
                  label="▲ เพิ่มข้อมูลใหม่ ▼"
                />
                <ActionButton
                  onClick={onUpdate}
                  disabled={busy || !isEditing}
                  tone="gray"
                  icon={Save}
                  label="▲ บันทึกการแก้ไข ▼"
                />
                <ActionButton
                  onClick={onDelete}
                  disabled={busy || !isEditing}
                  tone="red"
                  icon={Trash2}
                  label="▲ ลบข้อมูลนี้ออก ▼"
                />
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </AppShell>
  );
}

function FormRow({ label, children }: { label: string; children: React.ReactNode }) {
  return (
    <div className="space-y-1.5">
      <Label className="font-mono text-[11px] uppercase tracking-widest text-muted-foreground">{label}</Label>
      {children}
    </div>
  );
}

function ActionButton({
  onClick,
  disabled,
  tone,
  icon: Icon,
  label,
}: {
  onClick: () => void;
  disabled: boolean;
  tone: "blue" | "gray" | "red";
  icon: typeof Plus;
  label: string;
}) {
  const cls =
    tone === "blue" ? "bg-blue-600 hover:bg-blue-500 text-white shadow-[0_0_18px_rgba(37,99,235,0.45)]" :
    tone === "red"  ? "bg-red-700 hover:bg-red-600 text-white shadow-[0_0_18px_rgba(220,38,38,0.45)]" :
                      "bg-zinc-700 hover:bg-zinc-600 text-white";
  return (
    <button
      type="button"
      onClick={onClick}
      disabled={disabled}
      className={cn(
        "flex w-full items-center justify-center gap-2 rounded-lg px-4 py-2.5 text-sm font-semibold transition",
        cls,
        disabled && "cursor-not-allowed opacity-40",
      )}
    >
      <Icon className="h-4 w-4" />
      {label}
    </button>
  );
}
