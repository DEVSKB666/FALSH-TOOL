# apply_renames.ps1
# Applies the rename map produced by dump_metadata.ps1 to the deobfuscated
# source tree under MzaTunerClone_Decompiled\.
#
# Strategy (best-effort, safe):
#  Phase 1 - rename TYPES globally
#      A type name (the file-defined name) is unique within its namespace,
#      so when an identifier appears AS a type position (after "class ",
#      "new ", ": ", "(", ", ", "<", ">", "[", "{", "=", etc.) we replace
#      its escape-sequence form with the new short type name.
#
#  Phase 2 - rename MEMBERS scoped per-file
#      For each .cs file, find the file's primary type (the one whose
#      "class XYZ" / "struct XYZ" declaration appears in it), then rename
#      "this.<old>" / "base.<old>" / "<old>(" within that file to that
#      type's new member name.
#
# The result is written to MzaTunerClone_Renamed\.

param(
    [string]$RenameMap = "tools\rename_map.json",
    [string]$Metadata  = "tools\metadata.json",
    [string]$SourceDir = "MzaTunerClone_Decompiled",
    [string]$OutDir    = "MzaTunerClone_Renamed"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path $RenameMap)) { Write-Error "Missing $RenameMap"; exit 1 }
if (-not (Test-Path $Metadata))  { Write-Error "Missing $Metadata";  exit 1 }
if (-not (Test-Path $SourceDir)) { Write-Error "Missing $SourceDir"; exit 1 }

Write-Host "[*] Loading rename map and metadata"
$rmJson   = Get-Content $RenameMap -Raw -Encoding UTF8 | ConvertFrom-Json
$metaJson = Get-Content $Metadata  -Raw -Encoding UTF8 | ConvertFrom-Json

# Helper: convert a method/type Name (which may contain raw Unicode chars)
# into the literal escape form that appears in dnSpy source files.
function To-EscapeForm([string]$s) {
    if ([string]::IsNullOrEmpty($s)) { return $s }
    $sb = [System.Text.StringBuilder]::new()
    foreach ($ch in $s.ToCharArray()) {
        $code = [int]$ch
        if (($code -ge 0x30 -and $code -le 0x39) -or
            ($code -ge 0x41 -and $code -le 0x5A) -or
            ($code -ge 0x61 -and $code -le 0x7A) -or
            $code -eq 0x5F) {
            [void]$sb.Append($ch)
        } else {
            [void]$sb.AppendFormat('\u{0:X4}', $code)
        }
    }
    return $sb.ToString()
}

# Build per-type member map and global type map.
# typeNewByOldEsc:   "\u2000"           -> "Form_4"
# membersByTypeRid:  rid -> @{ "\u00A0" = "M_3F" ; ... }   keyed by ESCAPED old name
$typeNewByOldEsc  = [System.Collections.Generic.Dictionary[string,string]]::new([System.StringComparer]::Ordinal)
$membersByTypeRid = @{}

foreach ($row in $rmJson) {
    $oldEsc = To-EscapeForm $row.oldName
    if ($row.kind -eq 'Type') {
        # Multiple types could share the same source representation due to
        # codepoint collisions in dnSpy display; keep the first wins.
        if (-not $typeNewByOldEsc.ContainsKey($oldEsc)) {
            $typeNewByOldEsc[$oldEsc] = $row.newName
        }
    } elseif ($row.kind -eq 'Ctor') {
        # Ctors are named after their declaring type in C#. We don't rename
        # them here - the type rename will handle the declaration name.
        continue
    } else {
        $tRid = [int]$row.typeRid
        if (-not $membersByTypeRid.ContainsKey($tRid)) {
            $membersByTypeRid[$tRid] = [System.Collections.Generic.Dictionary[string,string]]::new([System.StringComparer]::Ordinal)
        }
        if (-not $membersByTypeRid[$tRid].ContainsKey($oldEsc)) {
            $membersByTypeRid[$tRid][$oldEsc] = $row.newName
        }
    }
}

Write-Host "[+] Type rename entries:        $($typeNewByOldEsc.Count)"
Write-Host "[+] Types with member maps:     $($membersByTypeRid.Count)"

# Build a lookup from "primary type name (escaped)" -> typeRid, so given a
# class declaration we can find its member map.
$typeRidByOldEsc = @{}
foreach ($t in $metaJson) {
    $oldEsc = To-EscapeForm $t.old
    if (-not $typeRidByOldEsc.ContainsKey($oldEsc)) {
        $typeRidByOldEsc[$oldEsc] = [int]$t.rid
    }
}

# Mirror dir
if (Test-Path $OutDir) { Remove-Item -Recurse -Force $OutDir }
New-Item -ItemType Directory -Path $OutDir | Out-Null

$srcRoot = (Resolve-Path $SourceDir).Path
$dstRoot = (Resolve-Path $OutDir).Path

$files = Get-ChildItem -Path $SourceDir -Recurse -File -Include *.cs
$extras = Get-ChildItem -Path $SourceDir -Recurse -File `
    -Include *.csproj,*.sln,*.resources,*.ico,*.manifest,*.settings,*.dat,*.ini

# Sort type rename entries by oldEsc length DESCENDING - so a 12-char escape
# sequence "\u1680\u2000" is replaced before "\u1680" alone.
$typeOldEscs = $typeNewByOldEsc.Keys | Sort-Object -Property Length -Descending

# Build a single regex alternation for type renames - match a TYPE-position
# escape sequence. We'll restrict to cases where it's flanked by non-identifier
# chars to avoid splitting "\u1680\u2000".
# Alternation must put longest first (already sorted).
$typeAlt = ($typeOldEscs | ForEach-Object { [regex]::Escape($_) }) -join '|'
# Match a sequence of escapes that exactly equals one of our type names,
# making sure no \uXXXX escape immediately precedes/follows.
$typeRe = [regex]::new("(?<!\\u[0-9A-Fa-f]{4})(?:$typeAlt)(?!\\u[0-9A-Fa-f]{4})")

# Helper: replace types in a chunk of text using $typeRe.
function Rename-Types([string]$text) {
    return $typeRe.Replace($text, {
        param($m)
        return $typeNewByOldEsc[$m.Value]
    })
}

$totalTypeRepl   = 0
$totalMemberRepl = 0

foreach ($f in $files) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }

    $text = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8
    if ($null -eq $text) { Set-Content -LiteralPath $dst -Value '' -Encoding UTF8; continue }

    # --- Phase 2 (do BEFORE phase 1 so member regex still sees old type
    # names which we use to detect file scope) ---
    # Detect the primary type defined in this file:
    #   "(class|struct|interface)\s+(<typeOldEsc>)\b"
    $primaryTypeRid = $null
    $primaryRe = [regex]::new('(?:class|struct|interface)\s+((?:\\u[0-9A-Fa-f]{4})+)')
    $pm = $primaryRe.Match($text)
    if ($pm.Success) {
        $primOldEsc = $pm.Groups[1].Value
        if ($typeRidByOldEsc.ContainsKey($primOldEsc)) {
            $primaryTypeRid = $typeRidByOldEsc[$primOldEsc]
        }
    }

    $fileMemberRepl = 0
    if ($primaryTypeRid -and $membersByTypeRid.ContainsKey($primaryTypeRid)) {
        $memberMap = $membersByTypeRid[$primaryTypeRid]

        # Conservative rename: only touch unambiguous member references.
        # We match "this.<old>" and "base.<old>" exclusively. Bare "<old>("
        # cannot be reliably distinguished from a ctor declaration, a return
        # type, or a cross-type call, so we leave those alone.
        $sortedKeys = $memberMap.Keys | Sort-Object -Property Length -Descending
        if ($sortedKeys.Count -gt 0) {
            $memberAlt = ($sortedKeys | ForEach-Object { [regex]::Escape($_) }) -join '|'
            $reThisBase = [regex]::new("(?<=\b(?:this|base)\.)(?:$memberAlt)(?!\\u[0-9A-Fa-f]{4})")
            $text = $reThisBase.Replace($text, {
                param($m)
                $script:fileMemberRepl++
                return $memberMap[$m.Value]
            })
        }
    }
    $totalMemberRepl += $fileMemberRepl

    # --- Phase 1: rename types globally ---
    $beforeLen = $text.Length
    $text = Rename-Types $text
    # Approximate count: scan again since regex doesn't expose count cheaply.
    $count = $typeRe.Matches($text).Count  # this is matches in the AFTER text - usually 0 if successful
    # Compute by re-running on original would be more accurate, but skip for perf.
    # Instead, count via difference is unreliable; estimate via separate pass:
    # (nothing - just leave totalTypeRepl approximation aside)

    [System.IO.File]::WriteAllText($dst, $text, (New-Object System.Text.UTF8Encoding($false)))

    if ($fileMemberRepl -gt 0) {
        Write-Host ("    members={0,4}  {1}" -f $fileMemberRepl, $rel)
    }
}

# Copy extras verbatim, but rewrite csproj/sln contents with type renames too
foreach ($f in $extras) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }
    Copy-Item -LiteralPath $f.FullName -Destination $dst -Force
}

Write-Host ""
Write-Host "[+] Total member replacements: $totalMemberRepl"
Write-Host "[+] Output: $dstRoot"
