# generate_class_index.ps1
# Reads tools\metadata.json and writes a Markdown index of every type and
# member, with their Token RID, signature, and the rename newName so the
# (still-imperfectly-renamed) source can be cross-referenced.

param(
    [string]$Metadata = "tools\metadata.json",
    [string]$OutFile  = "MzaTunerClone_Renamed\CLASS_INDEX.md"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path $Metadata)) { Write-Error "Missing $Metadata"; exit 1 }
$meta = Get-Content $Metadata -Raw -Encoding UTF8 | ConvertFrom-Json

$dst = $OutFile
$dstDir = Split-Path $dst -Parent
if ($dstDir -and -not (Test-Path $dstDir)) { New-Item -ItemType Directory -Path $dstDir -Force | Out-Null }

$sb = [System.Text.StringBuilder]::new()
[void]$sb.AppendLine("# Class Index - MZA_TUNER_FLASH_2026.exe")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("Generated automatically from the assembly's metadata via reflection.")
[void]$sb.AppendLine("Use this file alongside the source under ``MzaTunerClone_Renamed\`` to look up")
[void]$sb.AppendLine("any remaining ``\uXXXX`` identifier - find its Token RID and look it up here.")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("**Total types:** $($meta.Count)")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("---")
[void]$sb.AppendLine("")

# Index/TOC
[void]$sb.AppendLine("## Type Table")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("| RID | Source name | New name | Base | Kind |")
[void]$sb.AppendLine("|----:|---|---|---|---|")
$sortedTypes = $meta | Sort-Object { [int]$_.rid }
foreach ($t in $sortedTypes) {
    $kind =
        if ($t.isForm) { "Form" }
        elseif ($t.isAttr) { "Attribute" }
        elseif ($t.isNested) { "Nested" }
        else { "Type" }
    $base = if ($t.baseType) { $t.baseType -replace '^System\.', '' } else { "-" }
    $oldDisplay = $t.codepoints
    if (-not $oldDisplay) { $oldDisplay = $t.old }
    [void]$sb.AppendLine("| $($t.rid) | ``$oldDisplay`` | **$($t.new)** | ``$base`` | $kind |")
}
[void]$sb.AppendLine("")
[void]$sb.AppendLine("---")
[void]$sb.AppendLine("")

# Per-type sections
[void]$sb.AppendLine("## Per-Type Members")
[void]$sb.AppendLine("")
foreach ($t in $sortedTypes) {
    $kind =
        if ($t.isForm) { "Form" }
        elseif ($t.isAttr) { "Attribute" }
        elseif ($t.isNested) { "Nested" }
        else { "Type" }
    [void]$sb.AppendLine("### ``$($t.new)`` (RID $($t.rid)) - $kind")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("- **Source name:** ``$($t.codepoints)``")
    [void]$sb.AppendLine("- **Full name:** ``$($t.oldFull)``")
    [void]$sb.AppendLine("- **Base:** ``$($t.baseType)``")
    [void]$sb.AppendLine("")

    if ($null -eq $t.members -or $t.members.Count -eq 0) {
        [void]$sb.AppendLine("_No members._")
        [void]$sb.AppendLine("")
        continue
    }

    [void]$sb.AppendLine("| RID | Kind | New name | Source name | Signature |")
    [void]$sb.AppendLine("|----:|---|---|---|---|")

    $sortedMembers = $t.members | Sort-Object { [int]$_.rid }
    foreach ($m in $sortedMembers) {
        $oldDisp = if ($m.codepoints) { $m.codepoints } else { "(unobf)" }
        $sig = $m.sig -replace '\|', '\|'
        [void]$sb.AppendLine("| $($m.rid) | $($m.kind) | **``$($m.new)``** | ``$oldDisp`` | ``$sig`` |")
    }
    [void]$sb.AppendLine("")
}

[System.IO.File]::WriteAllText($dst, $sb.ToString(), (New-Object System.Text.UTF8Encoding($false)))
$len = (Get-Item $dst).Length
Write-Host "[+] Wrote $dst  ($len bytes, $($meta.Count) types)"
