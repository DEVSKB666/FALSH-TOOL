# extract_strings.ps1
# Loads MZA_TUNER_FLASH_2026.exe and extracts all strings from the
# Obfuscar string decryptor class via reflection.
#
# Output: tools\strings.json  - mapping "MethodName" -> "decoded value"
# Output: tools\strings.txt   - human-readable preview

param(
    [string]$ExePath = "C:\MZATUNER\MZA_TUNER_FLASH_2026.exe",
    [string]$OutDir  = "tools"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path $ExePath)) {
    Write-Error "EXE not found: $ExePath"
    exit 1
}

if (-not (Test-Path $OutDir)) {
    New-Item -ItemType Directory -Path $OutDir | Out-Null
}

Write-Host "[*] Loading assembly: $ExePath"
# LoadFile loads the assembly without resolving its dependencies' bindings -
# it does NOT execute the entry point. Good for static inspection.
$asm = [System.Reflection.Assembly]::LoadFile((Resolve-Path $ExePath))
Write-Host "[+] Loaded: $($asm.FullName)"

# Find the string decryptor class. Heuristic:
#   has static non-public byte[] field "4"  (the encoded blob)
#   has static non-public string[] field "5" (cache)
$bf  = [System.Reflection.BindingFlags]"NonPublic, Static, Public"
$bfM = [System.Reflection.BindingFlags]"NonPublic, Static, Public, InvokeMethod"

Write-Host "[*] Searching for string decryptor type..."
$decryptor = $null
foreach ($t in $asm.GetTypes()) {
    try {
        $f4 = $t.GetField("4", $bf)
        $f5 = $t.GetField("5", $bf)
        if ($f4 -and $f5 -and $f4.FieldType -eq [byte[]] -and $f5.FieldType -eq [string[]]) {
            $decryptor = $t
            break
        }
    } catch {}
}

if (-not $decryptor) {
    Write-Error "Could not locate string decryptor class (with fields '4' and '5')."
    exit 2
}

Write-Host "[+] Decryptor type: $($decryptor.FullName)"

# Trigger the static constructor so the byte array field "4" gets populated
# (this runs the XOR transform once).
[System.Runtime.CompilerServices.RuntimeHelpers]::RunClassConstructor($decryptor.TypeHandle)

$f4val = $decryptor.GetField("4", $bf).GetValue($null)
Write-Host "[+] Byte blob length: $($f4val.Length) bytes"

# Enumerate all PARAMETERLESS static string-returning methods.
# Each one returns one decoded string.
$methods = $decryptor.GetMethods($bfM) | Where-Object {
    $_.ReturnType -eq [string] -and
    $_.GetParameters().Count -eq 0
}

Write-Host "[+] Candidate decoder methods: $($methods.Count)"

# Build entries directly into a list - DO NOT use a hashtable as an
# intermediate keyed by $m.Name, because PowerShell's hashtables compare
# Unicode strings case-insensitively + culture-aware, which collapses many
# distinct method-names (Unicode whitespace/format chars) into one bucket.
$entries = New-Object System.Collections.ArrayList
$ok  = 0
$bad = 0
foreach ($m in $methods) {
    try {
        $val = $m.Invoke($null, $null)
        if ($null -ne $val) {
            $codepoints = ($m.Name.ToCharArray() | ForEach-Object { 'U+{0:X4}' -f [int][char]$_ }) -join '_'
            [void]$entries.Add([pscustomobject]@{
                method     = $m.Name
                codepoints = $codepoints
                value      = [string]$val
            })
            $ok++
        }
    } catch {
        $bad++
    }
}
Write-Host "[+] Decoded: $ok    failed: $bad    entries: $($entries.Count)"

# Sanity check: ensure codepoints are unique (they must be - method names
# are unique within a type).
$dupCps = $entries | Group-Object codepoints | Where-Object { $_.Count -gt 1 }
if ($dupCps.Count -gt 0) {
    Write-Warning "[!] $($dupCps.Count) duplicate codepoints (unexpected)"
}

$jsonPath = Join-Path $OutDir "strings.json"
$txtPath  = Join-Path $OutDir "strings.txt"
$entries | ConvertTo-Json -Depth 4 | Out-File $jsonPath -Encoding UTF8
Write-Host "[+] Wrote $jsonPath"

# Human-readable preview (sorted by codepoints)
$lines = $entries |
    Sort-Object codepoints |
    ForEach-Object { '{0,-40}  {1}' -f $_.codepoints, ($_.value -replace "`r?`n", " | ") }
$lines | Out-File $txtPath -Encoding UTF8
Write-Host "[+] Wrote $txtPath"

Write-Host ""
Write-Host "Sample (first 20 strings):"
$lines | Select-Object -First 20 | ForEach-Object { Write-Host "  $_" }
