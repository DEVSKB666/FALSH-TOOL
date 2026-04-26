# rewrite_source.ps1
# Reads tools\strings.json and replaces every call to the obfuscated string
# decryptor methods with a literal C# string in every .cs file.
#
# Pattern in source:
#   EC2D41B1-A2F9-4664-90D8-86645EE2E753.<UnicodeMethodName>()
# becomes:
#   "<decoded literal>"
#
# Output is written to a parallel directory tree under $OutDir, so the
# original obfuscated source is left intact.

param(
    [string]$StringsJson = "tools\strings.json",
    [string]$SourceDir   = ".",
    [string]$OutDir      = "MzaTunerClone_Decompiled",
    [string]$TypeName    = "EC2D41B1-A2F9-4664-90D8-86645EE2E753"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path $StringsJson)) {
    Write-Error "Strings JSON not found: $StringsJson  (run extract_strings.ps1 first)"
    exit 1
}

# Build mapping: codepoints (e.g. "U+200A_U+2005") -> decoded value
# We key by codepoints (not method-name string) because PowerShell hashtables
# default to case-insensitive culture-aware comparison, which collapses many
# Unicode whitespace/format chars into the same bucket.
Write-Host "[*] Loading $StringsJson"
$json = Get-Content $StringsJson -Raw -Encoding UTF8 | ConvertFrom-Json
$map = [System.Collections.Generic.Dictionary[string,string]]::new([System.StringComparer]::Ordinal)
foreach ($e in $json) { $map[$e.codepoints] = $e.value }
Write-Host "[+] Loaded $($map.Count) string mappings"

# Escape a string as a C# regular string literal.
# Switch on integer code-points to avoid PowerShell quoting headaches.
function To-CSharpLiteral([string]$s) {
    if ($null -eq $s) { return 'null' }
    $sb = [System.Text.StringBuilder]::new($s.Length + 2)
    [void]$sb.Append('"')
    foreach ($ch in $s.ToCharArray()) {
        $code = [int]$ch
        switch ($code) {
            34 { [void]$sb.Append('\"'); break }   # "
            92 { [void]$sb.Append('\\'); break }   # \
            13 { [void]$sb.Append('\r');  break }   # CR
            10 { [void]$sb.Append('\n');  break }   # LF
             9 { [void]$sb.Append('\t');  break }   # TAB
             0 { [void]$sb.Append('\0');  break }   # NUL
            default {
                if ($code -lt 32 -or $code -eq 127) {
                    [void]$sb.AppendFormat('\u{0:X4}', $code)
                } else {
                    # Keep printable Unicode (incl. Thai) as-is - file is UTF-8.
                    [void]$sb.Append($ch)
                }
            }
        }
    }
    [void]$sb.Append('"')
    return $sb.ToString()
}

# Regex matching <Type>.<MethodName>()
# In dnSpy-decompiled source, method names appear as escape sequences like
# "\u00A0", "\u1680\u2000". Each escape is literally 6 chars (\,u,4 hex).
# So the captured group is one or more such escapes.
$escType = [regex]::Escape($TypeName)
$pattern = "$escType" + '\.((?:\\u[0-9A-Fa-f]{4})+)\(\)'
$re = [regex]::new($pattern)

# Convert literal "\u00A0\u1680" (12 chars) -> codepoints key "U+00A0_U+1680"
function ConvertTo-CodepointsKey([string]$esc) {
    $parts = New-Object System.Collections.ArrayList
    for ($i = 0; $i -lt $esc.Length; $i += 6) {
        $hex = $esc.Substring($i + 2, 4).ToUpperInvariant()
        [void]$parts.Add("U+$hex")
    }
    return ($parts -join '_')
}

# Mirror directory tree
if (Test-Path $OutDir) {
    Write-Host "[*] Removing existing $OutDir"
    Remove-Item -Recurse -Force $OutDir
}
New-Item -ItemType Directory -Path $OutDir | Out-Null

$srcRoot = (Resolve-Path $SourceDir).Path
$dstRoot = (Resolve-Path $OutDir).Path

# Collect *.cs files (skip the OutDir itself)
$files = Get-ChildItem -Path $SourceDir -Recurse -File -Include *.cs |
    Where-Object { -not $_.FullName.StartsWith($dstRoot, [StringComparison]::OrdinalIgnoreCase) }

# Also copy non-cs project files (.csproj, .sln, .resources, .ico, .manifest, .settings)
$extras = Get-ChildItem -Path $SourceDir -Recurse -File `
    -Include *.csproj,*.sln,*.resources,*.ico,*.manifest,*.settings,*.dat,*.ini |
    Where-Object { -not $_.FullName.StartsWith($dstRoot, [StringComparison]::OrdinalIgnoreCase) }

Write-Host "[+] Found $($files.Count) .cs files, $($extras.Count) extra files"

$totalRepl = 0
$missMap   = @{}

foreach ($f in $files) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }

    $text = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8
    if ($null -eq $text) {
        Set-Content -LiteralPath $dst -Value '' -Encoding UTF8
        continue
    }

    $fileRepl = 0
    $newText = $re.Replace($text, {
        param($m)
        $escName = $m.Groups[1].Value
        $key = ConvertTo-CodepointsKey $escName
        if ($map.ContainsKey($key)) {
            $script:totalRepl++
            $fileRepl++
            return (To-CSharpLiteral $map[$key])
        } else {
            if (-not $missMap.ContainsKey($key)) { $missMap[$key] = 0 }
            $missMap[$key]++
            return $m.Value
        }
    })

    [System.IO.File]::WriteAllText($dst, $newText, (New-Object System.Text.UTF8Encoding($false)))
    if ($fileRepl -gt 0) {
        Write-Host ("    {0,5}  {1}" -f $fileRepl, $rel)
    }
}

# Copy extras verbatim
foreach ($f in $extras) {
    $rel = $f.FullName.Substring($srcRoot.Length).TrimStart('\','/')
    $dst = Join-Path $dstRoot $rel
    $dstDir = Split-Path $dst -Parent
    if (-not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }
    Copy-Item -LiteralPath $f.FullName -Destination $dst -Force
}

Write-Host ""
Write-Host "[+] Total replacements: $totalRepl"
if ($missMap.Count -gt 0) {
    Write-Host "[!] $($missMap.Count) method names appeared in source but had no mapping:"
    $missMap.GetEnumerator() | Sort-Object -Property Value -Descending | Select-Object -First 20 | ForEach-Object {
        Write-Host ("    {0,5}x  {1}" -f $_.Value, $_.Key)
    }
}
Write-Host "[+] Output: $dstRoot"
