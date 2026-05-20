'use client';

import { useState } from 'react';
import {
  AlertTriangle,
  CheckCircle2,
  Eye,
  EyeOff,
  FileLock2,
  FilePlus2,
  FolderOpen,
  KeyRound,
  Loader2,
  Save,
  Sigma,
  Unlock,
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
import { Select } from '@/components/ui/select';
import { tauri, isTauri, type XdfBreakResult } from '@/lib/tauri';
import {
  fixChecksum,
  guessChecksumOffset,
  sumMod256,
  verifyChecksum,
} from '@/lib/checksum';
import { sound } from '@/lib/sounds';
import { toast } from '@/components/toast';

type Variant = 'Keihin' | 'Shinden';

export default function FilesPage() {
  return (
    <AppShell>
      <div className="space-y-8">
        <header>
          <p className="font-mono text-xs uppercase tracking-[0.3em] text-primary">
            FILE TOOLS
          </p>
          <h1 className="mt-1 text-3xl font-bold tracking-tight">
            เครื่องมือไฟล์
          </h1>
          <p className="mt-1 text-muted-foreground">
            .ECU / .ACG ↔ .BIN converter · XDF / ADX password breaker
          </p>
        </header>

        <EcuConverterCard />
        <ChecksumFixerCard />
        <XdfBreakerCard />
      </div>
    </AppShell>
  );
}

// ============================================================
// ECU file converter (.ECU/.ACG <-> .bin)
// ============================================================

function EcuConverterCard() {
  const [mode, setMode] = useState<'decrypt' | 'encrypt'>('decrypt');
  const [variant, setVariant] = useState<Variant>('Keihin');
  const [input, setInput] = useState('');
  const [output, setOutput] = useState('');
  const [password, setPassword] = useState('');
  const [busy, setBusy] = useState(false);
  const [msg, setMsg] = useState<string | null>(null);

  async function pickInput() {
    if (!isTauri) return;
    const { open } = await import('@tauri-apps/plugin-dialog');
    const filters =
      mode === 'decrypt'
        ? [
            { name: 'ECU file', extensions: ['ECU', 'ecu', 'ACG', 'acg'] },
            { name: 'All', extensions: ['*'] },
          ]
        : [
            { name: 'Binary', extensions: ['bin', 'BIN'] },
            { name: 'All', extensions: ['*'] },
          ];
    const path = await open({ multiple: false, filters });
    if (typeof path === 'string') setInput(path);
  }

  async function pickOutput() {
    if (!isTauri) return;
    const { save } = await import('@tauri-apps/plugin-dialog');
    const path = await save({
      defaultPath:
        mode === 'decrypt'
          ? input.replace(/\.(ECU|ecu|ACG|acg)$/, '') + '.bin'
          : input + '.ECU',
    });
    if (typeof path === 'string') setOutput(path);
  }

  async function run() {
    setBusy(true);
    setMsg(null);
    try {
      const pwd = password.trim() || undefined;
      const bytes =
        mode === 'decrypt'
          ? await tauri.decryptEcuFile(input, output, variant, pwd)
          : await tauri.encryptEcuFile(input, output, variant, pwd);
      setMsg(
        `✔ สำเร็จ — ${mode === 'decrypt' ? 'ถอดรหัส' : 'เข้ารหัส'} ${bytes} bytes`,
      );
      sound.success();
    } catch (e) {
      setMsg(`✘ ผิดพลาด: ${String(e)}`);
      sound.error();
    } finally {
      setBusy(false);
    }
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <FileLock2 className="h-5 w-5 text-primary" />
          ECU File Converter
        </CardTitle>
        <CardDescription>รองรับ AES-256-ECB + MD5 + Base64</CardDescription>
      </CardHeader>
      <CardContent className="space-y-5">
        <div className="flex gap-2">
          {(['decrypt', 'encrypt'] as const).map((m) => (
            <Button
              key={m}
              variant={mode === m ? 'default' : 'outline'}
              onClick={() => setMode(m)}
            >
              {m === 'decrypt' ? 'ถอดรหัส .ECU → .bin' : 'เข้ารหัส .bin → .ECU'}
            </Button>
          ))}
        </div>

        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
          <div className="space-y-1.5">
            <Label htmlFor="variant">ECU Family</Label>
            <Select
              id="variant"
              value={variant}
              onChange={(e) => setVariant(e.target.value as Variant)}
            >
              <option value="Keihin">Keihin</option>
              <option value="Shinden">Shinden</option>
            </Select>
          </div>
          <div className="space-y-1.5">
            <Label htmlFor="pwd">Password (เว้นว่าง = ใช้ default)</Label>
            <Input
              id="pwd"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="• • • • • •"
            />
          </div>
        </div>

        <div className="space-y-1.5">
          <Label>ไฟล์ Input</Label>
          <div className="flex gap-2">
            <Input
              value={input}
              onChange={(e) => setInput(e.target.value)}
              placeholder="เลือกไฟล์…"
            />
            <Button variant="outline" onClick={pickInput}>
              <FolderOpen className="h-4 w-4" /> Browse
            </Button>
          </div>
        </div>

        <div className="space-y-1.5">
          <Label>ไฟล์ Output</Label>
          <div className="flex gap-2">
            <Input
              value={output}
              onChange={(e) => setOutput(e.target.value)}
              placeholder="เลือกที่บันทึก…"
            />
            <Button variant="outline" onClick={pickOutput}>
              <FilePlus2 className="h-4 w-4" /> Save as
            </Button>
          </div>
        </div>

        <div className="flex items-center gap-3">
          <Button onClick={run} disabled={busy || !input || !output}>
            {busy ? <Loader2 className="h-4 w-4 animate-spin" /> : null}
            เริ่ม
          </Button>
          {msg && (
            <span
              className={
                msg.startsWith('✔') ? 'text-emerald-500' : 'text-destructive'
              }
            >
              {msg}
            </span>
          )}
        </div>
      </CardContent>
    </Card>
  );
}

// ============================================================
// XDF / ADX Password Breaker (Form4 "RED_MATRIX")
// ============================================================

function XdfBreakerCard() {
  const [filename, setFilename] = useState<string>('');
  const [fileBytes, setFileBytes] = useState<Uint8Array | null>(null);
  const [password, setPassword] = useState('');
  const [showPwd, setShowPwd] = useState(false);
  const [busy, setBusy] = useState(false);
  const [result, setResult] = useState<XdfBreakResult | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function pickFile() {
    if (!isTauri) {
      // Browser fallback - use <input type="file" />
      const inp = document.createElement('input');
      inp.type = 'file';
      inp.accept = '.xdf,.adx,.XDF,.ADX';
      inp.onchange = async () => {
        const f = inp.files?.[0];
        if (!f) return;
        setFilename(f.name);
        const buf = await f.arrayBuffer();
        setFileBytes(new Uint8Array(buf));
      };
      inp.click();
      return;
    }
    const { open } = await import('@tauri-apps/plugin-dialog');
    const { readFile } = await import('@tauri-apps/plugin-fs');
    const path = await open({
      multiple: false,
      filters: [
        { name: 'XDF or ADX', extensions: ['xdf', 'adx', 'XDF', 'ADX'] },
      ],
    });
    if (typeof path !== 'string') return;
    const bytes = await readFile(path);
    setFilename(path);
    setFileBytes(new Uint8Array(bytes));
    setResult(null);
    setError(null);
  }

  async function runBreak() {
    if (!fileBytes) {
      sound.error();
      toast.error('ยังไม่ได้เลือกไฟล์', 'กดปุ่มเลือกไฟล์ก่อน');
      return;
    }
    // Quick magic check (so we can fail fast with a friendlier message).
    if (
      fileBytes.length < 264 ||
      fileBytes[0] !== 0x05 ||
      fileBytes[1] !== 0x22 ||
      fileBytes[2] !== 0x97
    ) {
      sound.warning();
      toast.warning(
        'ไฟล์นี้ไม่มีรหัสพาสเวิร์ด',
        'Magic bytes 05 22 97 ไม่ตรง — เลือกไฟล์ที่ติดรหัสผ่าน',
      );
      return;
    }

    setBusy(true);
    setError(null);
    setResult(null);
    try {
      const r = await tauri.breakXdfAdx(
        fileBytes,
        filename,
        password.trim() || undefined,
      );
      setResult(r);
      sound.success();
      toast.success(`ปลดรหัส ${r.variant} สำเร็จ`, 'พร้อมเซฟไฟล์');
    } catch (e) {
      const raw = String(e);
      setError(raw);
      sound.error();
      const friendly =
        raw.includes('WrongPassword') || raw.includes('non-XML')
          ? 'รหัสพาสเวิร์ดไม่ถูกต้อง — ลองกรอกรหัสเอง'
          : raw.includes('BadMagic')
            ? 'ไฟล์นี้ไม่มีรหัสพาสเวิร์ด'
            : raw;
      toast.error('ปลดรหัสล้มเหลว', friendly);
    } finally {
      setBusy(false);
    }
  }

  async function saveResult() {
    if (!result) return;
    if (!isTauri) {
      // Browser fallback - download via blob
      const blob = new Blob([result.plaintext], { type: 'text/xml' });
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download =
        filename.replace(/\.(adx|xdf)$/i, '.cleaned.$1') ||
        `cleaned.${result.variant.toLowerCase()}`;
      a.click();
      URL.revokeObjectURL(url);
      sound.success();
      toast.success('บันทึกไฟล์แล้ว');
      return;
    }
    const { save } = await import('@tauri-apps/plugin-dialog');
    const { writeTextFile } = await import('@tauri-apps/plugin-fs');
    const ext = result.variant.toLowerCase();
    const defaultName =
      filename.replace(/\.(adx|xdf)$/i, '') + `.cleaned.${ext}`;
    const path = await save({
      defaultPath: defaultName,
      filters: [{ name: result.variant, extensions: [ext] }],
    });
    if (typeof path !== 'string') return;
    try {
      await writeTextFile(path, result.plaintext);
      sound.success();
      toast.success('บันทึกไฟล์เรียบร้อยแล้ว', path);
    } catch (e) {
      sound.error();
      toast.error('บันทึกไฟล์ล้มเหลว', String(e));
    }
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <KeyRound className="h-5 w-5 text-primary" />
          XDF / ADX Password Breaker
          <span className="ml-auto font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
            ◢ RED_MATRIX ◣
          </span>
        </CardTitle>
        <CardDescription>
          ปลดรหัสไฟล์ TunerPro `.xdf` / `.adx` — strip{' '}
          <code>{'<openpassword>'}</code> +
          <code className="ml-1">{'<modifypassword>'}</code> + reset{' '}
          <code className="ml-1">{'<flags>'}</code>
        </CardDescription>
      </CardHeader>

      <CardContent className="space-y-5">
        {/* File picker */}
        <div className="space-y-1.5">
          <Label>ไฟล์ XDF / ADX</Label>
          <div className="flex flex-wrap items-center gap-2">
            <Input
              value={filename}
              readOnly
              placeholder="ยังไม่ได้เลือกไฟล์…"
              className="flex-1 min-w-[280px]"
            />
            <Button variant="outline" onClick={pickFile}>
              <FolderOpen className="h-4 w-4" /> เลือกไฟล์
            </Button>
            {fileBytes && (
              <span className="font-mono text-xs text-muted-foreground">
                {fileBytes.length.toLocaleString()} bytes
              </span>
            )}
          </div>
        </div>

        {/* Password (optional) */}
        <div className="space-y-1.5">
          <Label htmlFor="xdf-pwd">
            Password (เว้นว่าง = ใช้ default{' '}
            <code className="font-mono">$9Ad0$!#*)1QqPow^</code>)
          </Label>
          <div className="flex gap-2">
            <div className="relative flex-1">
              <Input
                id="xdf-pwd"
                type={showPwd ? 'text' : 'password'}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="• • • • • •"
              />
              <button
                type="button"
                onClick={() => setShowPwd((v) => !v)}
                className="absolute right-2 top-1/2 -translate-y-1/2 rounded p-1 text-muted-foreground hover:bg-accent hover:text-foreground"
                title={showPwd ? 'ซ่อน' : 'แสดง'}
              >
                {showPwd ? (
                  <EyeOff className="h-4 w-4" />
                ) : (
                  <Eye className="h-4 w-4" />
                )}
              </button>
            </div>
          </div>
        </div>

        {/* Actions */}
        <div className="flex flex-wrap items-center gap-3">
          <Button onClick={runBreak} disabled={busy || !fileBytes}>
            {busy ? (
              <Loader2 className="h-4 w-4 animate-spin" />
            ) : (
              <Unlock className="h-4 w-4" />
            )}
            ปลดรหัส
          </Button>
          {result && (
            <Button variant="outline" onClick={saveResult}>
              <Save className="h-4 w-4" /> เซฟไฟล์ที่ปลดรหัสแล้ว
            </Button>
          )}
        </div>

        {error && (
          <div className="flex items-start gap-2 rounded-md border border-destructive/40 bg-destructive/10 p-3 text-sm text-destructive">
            <AlertTriangle className="mt-0.5 h-4 w-4 shrink-0" />
            <span className="break-all">{error}</span>
          </div>
        )}

        {result && (
          <div className="space-y-3 rounded-md border border-border/40 bg-muted/30 p-4">
            <div className="grid grid-cols-1 gap-3 md:grid-cols-3">
              <Stat label="Variant" value={result.variant} mono />
              <Stat
                label="Open password"
                value={result.open_password || '(ว่าง)'}
                mono
              />
              <Stat
                label="Modify password"
                value={result.modify_password || '(ว่าง)'}
                mono
              />
            </div>
            <details>
              <summary className="cursor-pointer select-none font-mono text-xs uppercase tracking-widest text-muted-foreground">
                Cleaned XML preview ({result.plaintext.length.toLocaleString()}{' '}
                chars)
              </summary>
              <pre className="custom-scrollbar mt-2 max-h-72 overflow-auto rounded bg-background/60 p-3 font-mono text-[11px] leading-relaxed text-foreground/85 selectable">
                {result.plaintext.slice(0, 4000)}
                {result.plaintext.length > 4000 &&
                  '\n\n... (ตัดหลัง 4000 ตัวอักษร)'}
              </pre>
            </details>
          </div>
        )}
      </CardContent>
    </Card>
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
    <div className="rounded border border-border/40 bg-background/40 px-3 py-2">
      <p className="font-mono text-[10px] uppercase tracking-widest text-muted-foreground">
        {label}
      </p>
      <p className={`mt-0.5 truncate text-sm ${mono ? 'font-mono' : ''}`}>
        {value}
      </p>
    </div>
  );
}

// ============================================================
// Checksum fixer (port of GForm12.cs::method_28)
// ============================================================

/** Loaded BIN snapshot held in component state. */
interface BinFile {
  name: string;
  bytes: Uint8Array;
}

function ChecksumFixerCard() {
  const [file, setFile] = useState<BinFile | null>(null);
  const [offsetHex, setOffsetHex] = useState('0');
  const [busy, setBusy] = useState(false);

  // Pure-JS: derived state recomputed on every render is cheap
  // (sumMod256 is a single pass over the buffer).
  const sum = file ? sumMod256(file.bytes) : null;
  const balanced = file ? verifyChecksum(file.bytes) : null;
  const offset = (() => {
    const n = parseInt(offsetHex, 16);
    return Number.isFinite(n) && n >= 0 ? n : 0;
  })();

  async function pickFile() {
    if (!isTauri) {
      // Browser-preview fallback - hidden <input type="file"> click
      // is wired below.
      document.getElementById('cksum-file-input')?.click();
      return;
    }
    const { open } = await import('@tauri-apps/plugin-dialog');
    const { readFile } = await import('@tauri-apps/plugin-fs');
    const path = await open({
      multiple: false,
      filters: [{ name: 'Binary', extensions: ['bin', 'BIN'] }],
    });
    if (typeof path !== 'string') return;
    const bytes = await readFile(path);
    setFile({ name: path.split('/').pop() ?? path, bytes });
    // Heuristic: pre-fill the offset that already balances the file
    // (so the user can hit "Fix" immediately if just one byte got
    // edited and they want to recompute the same slot).
    const guessed = guessChecksumOffset(bytes);
    if (guessed != null) setOffsetHex(guessed.toString(16).toUpperCase());
  }

  async function onBrowseFile(f: File | null) {
    if (!f) return;
    const buf = new Uint8Array(await f.arrayBuffer());
    setFile({ name: f.name, bytes: buf });
    const guessed = guessChecksumOffset(buf);
    if (guessed != null) setOffsetHex(guessed.toString(16).toUpperCase());
  }

  async function applyFix() {
    if (!file) return;
    setBusy(true);
    try {
      const fixed = fixChecksum(file.bytes, offset);
      // Open the OS save dialog (Tauri) or trigger a browser download
      // so the user can write the fixed file wherever they prefer.
      const baseName = file.name.replace(/\.bin$/i, '');
      const suggested = `${baseName}_fixed.bin`;
      if (isTauri) {
        const { save } = await import('@tauri-apps/plugin-dialog');
        const { writeFile } = await import('@tauri-apps/plugin-fs');
        const path = await save({
          defaultPath: suggested,
          filters: [{ name: 'Binary', extensions: ['bin'] }],
        });
        if (!path) return;
        await writeFile(path, fixed);
        sound.success();
        toast.success('บันทึก checksum-fixed BIN', path);
      } else {
        const blob = new Blob([fixed.buffer as ArrayBuffer], {
          type: 'application/octet-stream',
        });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = suggested;
        a.click();
        URL.revokeObjectURL(url);
        sound.success();
        toast.success('ดาวน์โหลด BIN ใหม่แล้ว', suggested);
      }
      // Replace state so the user sees the new sum in real time.
      setFile({ name: file.name, bytes: fixed });
    } catch (e) {
      sound.error();
      toast.error('Fix checksum ล้มเหลว', String(e));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Sigma className="h-4 w-4 text-primary" />
          Checksum Fixer (Honda BIN)
        </CardTitle>
        <CardDescription>
          ตรวจ/ซ่อม checksum ของไฟล์ BIN ให้ผลรวมทุก byte = 0 (mod 256)
          ตามมาตรฐาน Honda KWP — port มาจาก{' '}
          <code className="font-mono text-[10px]">GForm12.cs::method_28</code>
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        {/* File picker */}
        <div className="flex flex-wrap items-center gap-2">
          <Button onClick={pickFile} variant="outline" size="sm">
            <FolderOpen className="h-4 w-4" /> เลือกไฟล์ BIN
          </Button>
          <input
            id="cksum-file-input"
            type="file"
            accept=".bin,.BIN"
            className="hidden"
            onChange={(e) => onBrowseFile(e.target.files?.[0] ?? null)}
          />
          {file ? (
            <span className="truncate text-sm">
              <span className="text-primary">{file.name}</span>
              <span className="ml-2 text-xs text-muted-foreground">
                ({file.bytes.length.toLocaleString()} bytes)
              </span>
            </span>
          ) : (
            <span className="text-sm text-muted-foreground">
              ยังไม่ได้เลือกไฟล์
            </span>
          )}
        </div>

        {/* Status + offset selector */}
        {file && (
          <>
            <div className="grid grid-cols-2 gap-3 sm:grid-cols-4">
              <Stat label="Size" value={`${file.bytes.length} B`} mono />
              <Stat
                label="Sum (mod 256)"
                value={`0x${sum!.toString(16).toUpperCase().padStart(2, '0')}`}
                mono
              />
              <Stat
                label="Balanced?"
                value={balanced ? 'YES (ok)' : 'NO (ต้องแก้)'}
                mono
              />
              <Stat
                label="Slot offset"
                value={`0x${offset.toString(16).toUpperCase()}`}
                mono
              />
            </div>

            <div className="flex flex-wrap items-end gap-3">
              <div className="space-y-1.5">
                <Label htmlFor="cksum-offset">Checksum offset (hex)</Label>
                <Input
                  id="cksum-offset"
                  value={offsetHex}
                  onChange={(e) =>
                    setOffsetHex(
                      e.target.value.replace(/[^0-9a-fA-F]/g, '').toUpperCase(),
                    )
                  }
                  placeholder="0"
                  className="w-32 font-mono"
                />
              </div>
              <Button
                onClick={applyFix}
                disabled={busy || offset >= file.bytes.length}
              >
                {busy ? (
                  <Loader2 className="h-4 w-4 animate-spin" />
                ) : (
                  <Save className="h-4 w-4" />
                )}
                Fix &amp; Save
              </Button>

              {balanced != null && (
                <span
                  className={`ml-auto inline-flex items-center gap-1.5 text-sm ${balanced ? 'text-emerald-400' : 'text-amber-400'}`}
                >
                  {balanced ? (
                    <CheckCircle2 className="h-4 w-4" />
                  ) : (
                    <XCircle className="h-4 w-4" />
                  )}
                  {balanced
                    ? 'Checksum OK — ไม่ต้องแก้'
                    : 'Checksum ไม่ตรง — กด Fix เพื่อ recompute'}
                </span>
              )}
            </div>

            <p className="text-[11px] leading-relaxed text-muted-foreground">
              <AlertTriangle className="mb-0.5 mr-1 inline h-3 w-3 text-amber-500" />
              ค่าเริ่มต้น: <code>0x0</code> (ตำแหน่ง byte แรก) เป็น layout
              เดิมของ Honda Keihin C# เมื่อ <code>int_8 = 0</code>; สำหรับ
              Shinden 32K map ลองใช้ <code>0x7FF</code> หรือดู{' '}
              <code>cksumOffset</code> จาก data.ini ของรุ่นที่ใช้
            </p>
          </>
        )}
      </CardContent>
    </Card>
  );
}
