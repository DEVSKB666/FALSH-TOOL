# apply_renames_v2.ps1
# Position-aware identifier rename. Strategy:
#
# 1. PER-FILE pass: discover the primary type declared in the file. Within
#    that file, rename members of that type in unambiguous positions only:
#       - this.<old>          -> this.<new>
#       - base.<old>          -> base.<new>
#       - <modifiers> <retType>? <old> (   -> declaration site
#       - = <old>;            (field initializer style)
#    Also rename event handler "+= this.<old>" patterns.
#
# 2. GLOBAL pass: rename TYPES only in known type positions:
#       - class | struct | interface | enum
#       - base list ": <T> {" or ": <T>,"
#       - "new <T>("  or  "new <T>["
#       - "namespace <T>"  /  "using <T>;"
#       - "<T>." when followed by an identifier (qualified ref)
#       - "(<T>)" cast
#       - "<<T>>" generic argument
#       - "this <T> = ..."  field/local declaration: "(modifiers) <T> name"
#
# We never do greedy global rename of bare \uXXXX identifiers, which avoids
# clobbering members that happen to share their declaring-type's Unicode name.

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

# ----- Build maps -----
$typeNewByOldEsc  = [System.Collections.Generic.Dictionary[string,string]]::new([System.StringComparer]::Ordinal)
$membersByTypeRid = @{}

foreach ($row in $rmJson) {
    $oldEsc = To-EscapeForm $row.oldName
    if ($row.kind -eq 'Type') {
        if (-not $typeNewByOldEsc.ContainsKey($oldEsc)) {
            $typeNewByOldEsc[$oldEsc] = $row.newName
        }
    } elseif ($row.kind -eq 'Ctor') {
        # ctor name follows the type name in C#; skip
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

# typeOldEsc -> typeRid
$typeRidByOldEsc = @{}
foreach ($t in $metaJson) {
    $oldEsc = To-EscapeForm $t.old
    if (-not $typeRidByOldEsc.ContainsKey($oldEsc)) {
        $typeRidByOldEsc[$oldEsc] = [int]$t.rid
    }
}

Write-Host "[+] Type rename entries:    $($typeNewByOldEsc.Count)"
Write-Host "[+] Types with member maps: $($membersByTypeRid.Count)"

# ----- Mirror dirs -----
if (Test-Path $OutDir) { Remove-Item -Recurse -Force $OutDir }
New-Item -ItemType Directory -Path $OutDir | Out-Null

$srcRoot = (Resolve-Path $SourceDir).Path
$dstRoot = (Resolve-Path $OutDir).Path

$files  = Get-ChildItem -Path $SourceDir -Recurse -File -Include *.cs
$extras = Get-ChildItem -Path $SourceDir -Recurse -File `
    -Include *.csproj,*.sln,*.resources,*.ico,*.manifest,*.settings,*.dat,*.ini

# Sort all type olds by length DESC so longer escape sequences win first
$typeOldEscs = @($typeNewByOldEsc.Keys | Sort-Object -Property Length -Descending)
if ($typeOldEscs.Count -gt 0) {
    $typeAlt = ($typeOldEscs | ForEach-Object { [regex]::Escape($_) }) -join '|'
} else {
    $typeAlt = ''
}

# Build a SINGLE regex that matches a type name in any of the legal type
# positions, capturing the matched type name. We use named groups for clarity.
# We rely on .NET's variable-length lookbehind. We will use multiple patterns
# instead of one mega-pattern for clarity and to keep regex compilation fast.
$typePatterns = @()
if ($typeAlt) {
    $typePatterns += [pscustomobject]@{ name='decl';   re=[regex]::new("(?<=\b(?:class|struct|interface|enum)\s+)($typeAlt)(?!\\u[0-9A-Fa-f]{4})") }
    $typePatterns += [pscustomobject]@{ name='ns';     re=[regex]::new("(?<=\bnamespace\s+)($typeAlt)(?!\\u[0-9A-Fa-f]{4})") }
    $typePatterns += [pscustomobject]@{ name='using';  re=[regex]::new("(?<=\busing\s+)($typeAlt)(?=\s*;)") }
    $typePatterns += [pscustomobject]@{ name='base';   re=[regex]::new("(?<=:\s*)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*[,{\r\n])") }
    $typePatterns += [pscustomobject]@{ name='new';    re=[regex]::new("(?<=\bnew\s+)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*[\(\[])") }
    $typePatterns += [pscustomobject]@{ name='cast';   re=[regex]::new("(?<=\()($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\))") }
    $typePatterns += [pscustomobject]@{ name='generic';re=[regex]::new("(?<=[<,]\s*)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*[,>])") }
    $typePatterns += [pscustomobject]@{ name='qualified';re=[regex]::new("(?<=\.)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\.)") }
    $typePatterns += [pscustomobject]@{ name='global';re=[regex]::new("(?<=\bglobal::)($typeAlt)(?!\\u[0-9A-Fa-f]{4})") }
    # typeof(<T>)
    $typePatterns += [pscustomobject]@{ name='typeof';re=[regex]::new("(?<=\btypeof\(\s*)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*\))") }
    # is/as <T>
    $typePatterns += [pscustomobject]@{ name='isas';  re=[regex]::new("(?<=\b(?:is|as)\s+)($typeAlt)(?!\\u[0-9A-Fa-f]{4})") }
    # default(<T>)
    $typePatterns += [pscustomobject]@{ name='default';re=[regex]::new("(?<=\bdefault\(\s*)($typeAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*\))") }
}

function Apply-TypeRenames([string]$text) {
    $count = 0
    foreach ($p in $typePatterns) {
        $text = $p.re.Replace($text, {
            param($m)
            $script:_typeReplCount++
            return $typeNewByOldEsc[$m.Groups[1].Value]
        })
    }
    return $text
}

# Apply ctor-declaration rename within a file given the file's primary type.
# Patterns:
#   <accessmod> [static|extern|unsafe...] <ClassOldEsc>(   ->   <accessmod> ... <ClassNewName>(
#   static <ClassOldEsc>()                                 ->   static <ClassNewName>()
# Distinguished from method decl by absence of a return type between modifier
# and identifier. Also covers ": this(...)"/": base(...)" if any.
function Apply-CtorRenames([string]$text, [string]$primOldEsc, [string]$primNewName) {
    if (-not $primOldEsc) { return $text }
    if (-not $primNewName) { return $text }
    $escOld = [regex]::Escape($primOldEsc)

    # 1) <accessmod> (modifiers)* <ClassOldEsc> (
    $re1 = [regex]::new("(?<=\b(?:public|private|protected|internal)(?:\s+(?:static|partial|extern|unsafe|async|sealed|abstract|virtual|override|new))*\s+)$escOld(?=\s*\()")
    $text = $re1.Replace($text, { param($m); $script:_ctorReplCount++; return $primNewName })

    # 2) static <ClassOldEsc> (   - static ctor
    $re2 = [regex]::new("(?<=\bstatic\s+)$escOld(?=\s*\(\s*\))")
    $text = $re2.Replace($text, { param($m); $script:_ctorReplCount++; return $primNewName })

    return $text
}

# Member renames per file
function Apply-MemberRenames([string]$text, [System.Collections.Generic.Dictionary[string,string]]$memberMap, [string]$primTypeOldEsc) {
    if ($null -eq $memberMap -or $memberMap.Count -eq 0) { return $text }
    $sortedKeys = @($memberMap.Keys | Sort-Object -Property Length -Descending)
    $memberAlt  = ($sortedKeys | ForEach-Object { [regex]::Escape($_) }) -join '|'

    # 1) this.<old> / base.<old>
    $reAccess = [regex]::new("(?<=\b(?:this|base)\.)($memberAlt)(?!\\u[0-9A-Fa-f]{4})")
    $text = $reAccess.Replace($text, {
        param($m); $script:_memReplCount++
        return $memberMap[$m.Groups[1].Value]
    })

    # 2) Event subscription style:  += this.<old>;   (already covered by 1)
    #    +-= delegate-method-group references like:  this.<old>.Click += this.<other>;
    #    Already handled by 1.

    # 3) Method/field DECLARATIONS (within the primary type):
    #    Something like  (public|private|...)+ (static|...)* <retType> <old>(
    #                or  (public|private|...) <fieldType> <old> (= ... | ;)
    # We only need to be careful that <old> isn't equal to the primary type
    # (in which case it's a ctor declaration).
    $excluded = $null
    if ($primTypeOldEsc) { $excluded = $primTypeOldEsc }

    # Collect method-style declaration sites:
    # Match a line like:  modifiers? returnType? <old>( ... )  {
    # using a heuristic: preceded by whitespace/newline and a type-ish token
    $reDeclMethod = [regex]::new("(?m)(?<=\b(?:public|private|protected|internal|static|virtual|override|abstract|sealed|partial|async|extern|new|unsafe)\b[\w\.\[\]<>,\s\?]+?\s)($memberAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*\()")
    $text = $reDeclMethod.Replace($text, {
        param($m)
        $name = $m.Groups[1].Value
        if ($excluded -and $name -eq $excluded) { return $name }
        $script:_memReplCount++
        return $memberMap[$name]
    })

    # Field declarations:  (modifier+) <type> <old>  (= ...)? ;
    $reDeclField = [regex]::new("(?m)(?<=\b(?:public|private|protected|internal|static|readonly|volatile|const)\b[\w\.\[\]<>,\?\s]+?\s)($memberAlt)(?!\\u[0-9A-Fa-f]{4})(?=\s*[=;])")
    $text = $reDeclField.Replace($text, {
        param($m)
        $name = $m.Groups[1].Value
        if ($excluded -and $name -eq $excluded) { return $name }
        $script:_memReplCount++
        return $memberMap[$name]
    })

    return $text
}

$totalText      = ''
$_typeReplCount = 0
$_memReplCount  = 0
$_ctorReplCount = 0

foreach ($f in $files) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }

    $text = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8
    if ($null -eq $text) { Set-Content -LiteralPath $dst -Value '' -Encoding UTF8; continue }

    # Detect primary type
    $primaryTypeRid    = $null
    $primaryTypeOldEsc = $null
    $primaryRe = [regex]::new('(?:class|struct|interface)\s+((?:\\u[0-9A-Fa-f]{4})+)')
    $pm = $primaryRe.Match($text)
    if ($pm.Success) {
        $primaryTypeOldEsc = $pm.Groups[1].Value
        if ($typeRidByOldEsc.ContainsKey($primaryTypeOldEsc)) {
            $primaryTypeRid = $typeRidByOldEsc[$primaryTypeOldEsc]
        }
    }

    $countBefore = $_memReplCount
    if ($primaryTypeRid -and $membersByTypeRid.ContainsKey($primaryTypeRid)) {
        $text = Apply-MemberRenames $text $membersByTypeRid[$primaryTypeRid] $primaryTypeOldEsc
    }
    $fileMemRepl = $_memReplCount - $countBefore

    # Ctor declarations (must run BEFORE type rename so type rename does not
    # collapse class name into method declarations that share its codepoints)
    $countBefore = $_ctorReplCount
    if ($primaryTypeOldEsc -and $typeNewByOldEsc.ContainsKey($primaryTypeOldEsc)) {
        $primNewName = $typeNewByOldEsc[$primaryTypeOldEsc]
        $text = Apply-CtorRenames $text $primaryTypeOldEsc $primNewName
    }
    $fileCtorRepl = $_ctorReplCount - $countBefore

    $countBefore = $_typeReplCount
    $text = Apply-TypeRenames $text
    $fileTypeRepl = $_typeReplCount - $countBefore

    [System.IO.File]::WriteAllText($dst, $text, (New-Object System.Text.UTF8Encoding($false)))

    if (($fileMemRepl + $fileTypeRepl) -gt 0) {
        Write-Host ("    M={0,4}  T={1,4}  {2}" -f $fileMemRepl, $fileTypeRepl, $rel)
    }
}

# Copy extras (csproj/sln/resources/etc) verbatim
foreach ($f in $extras) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }
    Copy-Item -LiteralPath $f.FullName -Destination $dst -Force
}

Write-Host ""
Write-Host "[+] Total member replacements: $_memReplCount"
Write-Host "[+] Total ctor replacements:   $_ctorReplCount"
Write-Host "[+] Total type replacements:   $_typeReplCount"
Write-Host "[+] Output: $dstRoot"
