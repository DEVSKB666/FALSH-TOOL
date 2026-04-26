# dump_metadata.ps1
# Walks every type/member of the assembly via reflection and emits a
# complete metadata dump (tools\metadata.json) plus a per-type name-map
# (tools\rename_map.json) used to deobfuscate the Unicode identifiers
# in source files.

param(
    [string]$ExePath = "C:\MZATUNER\MZA_TUNER_FLASH_2026.exe",
    [string]$OutDir  = "tools"
)

$ErrorActionPreference = "Stop"
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir | Out-Null }

Write-Host "[*] Loading $ExePath"
$asm = [System.Reflection.Assembly]::LoadFile((Resolve-Path $ExePath))

$bfAll = [System.Reflection.BindingFlags]"Public, NonPublic, Static, Instance, DeclaredOnly"

# Codepoint helper - turns "\u00A0\u1680" actual chars into "U+00A0_U+1680" key
function To-Codepoints([string]$s) {
    if ([string]::IsNullOrEmpty($s)) { return "" }
    return (($s.ToCharArray() | ForEach-Object { 'U+{0:X4}' -f [int][char]$_ }) -join '_')
}

# Determine if a name is "obfuscated" - i.e. consists entirely of non-ASCII letters
# (the Obfuscar trick uses Unicode whitespace/format chars).
function Is-Obfuscated([string]$s) {
    if ([string]::IsNullOrEmpty($s)) { return $false }
    foreach ($c in $s.ToCharArray()) {
        $code = [int]$c
        # ASCII identifier chars and underscore are NOT obfuscated
        if (($code -ge 0x30 -and $code -le 0x39) -or
            ($code -ge 0x41 -and $code -le 0x5A) -or
            ($code -ge 0x61 -and $code -le 0x7A) -or
            $code -eq 0x5F) { return $false }
    }
    return $true
}

# Reserved C# keywords and common class names we must not collide with.
$reserved = @{}
@(
    'abstract','as','base','bool','break','byte','case','catch','char','checked','class','const','continue',
    'decimal','default','delegate','do','double','else','enum','event','explicit','extern','false','finally',
    'fixed','float','for','foreach','goto','if','implicit','in','int','interface','internal','is','lock',
    'long','namespace','new','null','object','operator','out','override','params','private','protected',
    'public','readonly','ref','return','sbyte','sealed','short','sizeof','stackalloc','static','string',
    'struct','switch','this','throw','true','try','typeof','uint','ulong','unchecked','unsafe','ushort',
    'using','virtual','void','volatile','while'
) | ForEach-Object { $reserved[$_] = $true }

# Suggest a readable name from a member, given its kind, return type, and signature.
# Falls back to <kind>_<rid> if no better hint.
function Suggest-MemberName([string]$kind, $member, [int]$rid) {
    # Forms expose original control names through field tokens that often map to
    # human-readable strings via decoded constants - but those mappings live in
    # IL bodies, not metadata. So just use a stable scheme.
    $base = ""
    switch ($kind) {
        "Type"     { $base = "Type" }
        "Method"   { $base = "M" }
        "Ctor"     { $base = "Ctor" }
        "Field"    { $base = "f" }
        "Prop"     { $base = "Prop" }
        "Event"    { $base = "Evt" }
        "Param"    { $base = "p" }
        default    { $base = "x" }
    }
    return ("{0}_{1:X}" -f $base, $rid)
}

# Walk every type
$types     = $asm.GetTypes() | Sort-Object { $_.MetadataToken }
$typeRows  = New-Object System.Collections.ArrayList
$nameMap   = New-Object System.Collections.ArrayList   # rows with: typeRid, scope, oldName, newName, kind
$methodSigs = New-Object System.Collections.ArrayList  # for documenting overloads

# Detect the string decryptor type so we can skip it in the rename (already
# handled by the string rewriter).
$decryptorFullName = $null
foreach ($t in $types) {
    $f4 = $t.GetField("4", $bfAll)
    $f5 = $t.GetField("5", $bfAll)
    if ($f4 -and $f5 -and $f4.FieldType -eq [byte[]] -and $f5.FieldType -eq [string[]]) {
        $decryptorFullName = $t.FullName
        break
    }
}
Write-Host "[+] Detected string decryptor: $decryptorFullName"

# Track new names already used inside a (typeRid, kind) bucket to avoid collisions.
$usedInScope = @{}

foreach ($t in $types) {
    $tRid    = $t.MetadataToken -band 0xFFFFFF
    $oldType = $t.Name           # short name as it appears (mostly) in source
    $oldFull = $t.FullName
    $isObf   = Is-Obfuscated $oldType
    $newType = if ($isObf) {
        # Use a friendlier prefix for Forms / Attributes
        if ([System.Windows.Forms.Form].IsAssignableFrom($t)) { "Form_$($tRid.ToString('X'))" }
        elseif ([Attribute].IsAssignableFrom($t))             { "Attr_$($tRid.ToString('X'))" }
        elseif ($t.IsEnum)                                    { "Enum_$($tRid.ToString('X'))" }
        elseif ($t.IsValueType)                               { "Struct_$($tRid.ToString('X'))" }
        else                                                  { "Type_$($tRid.ToString('X'))" }
    } else {
        $oldType
    }

    [void]$typeRows.Add([pscustomobject]@{
        rid          = $tRid
        old          = $oldType
        oldFull      = $oldFull
        new          = $newType
        codepoints   = (To-Codepoints $oldType)
        isObfuscated = $isObf
        baseType     = $t.BaseType.FullName
        isForm       = [System.Windows.Forms.Form].IsAssignableFrom($t)
        isAttr       = [Attribute].IsAssignableFrom($t)
        isNested     = $t.IsNested
        members      = $null   # populated below
    })

    if ($isObf -and $oldFull -ne $decryptorFullName) {
        [void]$nameMap.Add([pscustomobject]@{
            typeRid    = $tRid
            scope      = 'Type'
            kind       = 'Type'
            oldName    = $oldType
            newName    = $newType
            codepoints = (To-Codepoints $oldType)
        })
    }

    # Members
    $members = $t.GetMembers($bfAll)
    $perTypeMethodGroups = @{}
    $memberRows = New-Object System.Collections.ArrayList
    foreach ($m in $members) {
        $rid = $m.MetadataToken -band 0xFFFFFF
        $name = $m.Name
        $isMObf = Is-Obfuscated $name
        $kind = "Other"
        if ($m -is [System.Reflection.MethodInfo])      { $kind = if ($m.IsSpecialName) { 'Method' } else { 'Method' } }
        elseif ($m -is [System.Reflection.ConstructorInfo]) { $kind = 'Ctor' }
        elseif ($m -is [System.Reflection.FieldInfo])   { $kind = 'Field' }
        elseif ($m -is [System.Reflection.PropertyInfo]){ $kind = 'Prop' }
        elseif ($m -is [System.Reflection.EventInfo])   { $kind = 'Event' }
        elseif ($m -is [System.Type])                   { $kind = 'NestedType' }

        # Build a signature string for methods so we can show overloads.
        $sig = $name
        $retType = $null
        if ($m -is [System.Reflection.MethodInfo]) {
            $retType = $m.ReturnType.ToString()
            $params  = ($m.GetParameters() | ForEach-Object { "$($_.ParameterType.Name) $($_.Name)" }) -join ', '
            $sig     = "$retType $name($params)"
        } elseif ($m -is [System.Reflection.ConstructorInfo]) {
            $params  = ($m.GetParameters() | ForEach-Object { "$($_.ParameterType.Name) $($_.Name)" }) -join ', '
            $sig     = "ctor($params)"
        } elseif ($m -is [System.Reflection.FieldInfo]) {
            $sig     = "$($m.FieldType.Name) $name"
        } elseif ($m -is [System.Reflection.PropertyInfo]) {
            $sig     = "$($m.PropertyType.Name) $name"
        } elseif ($m -is [System.Reflection.EventInfo]) {
            $sig     = "event $($m.EventHandlerType.Name) $name"
        }

        # New name: keep ASCII names as-is; for obfuscated names allocate a unique
        # name per (typeRid, kind, oldName) - so OVERLOADS share the same new name
        # which is exactly what C# expects.
        $scopeKey = "$tRid|$kind|$name"
        $newName = $null
        if (-not $isMObf) {
            $newName = $name
        } else {
            if (-not $usedInScope.ContainsKey($scopeKey)) {
                # Generate proposed name, then ensure no collision in this class
                $prefix = switch ($kind) {
                    'Method'     { 'M' }
                    'Ctor'       { 'Ctor' }
                    'Field'      { 'f' }
                    'Prop'       { 'P' }
                    'Event'      { 'Ev' }
                    'NestedType' { 'N' }
                    default      { 'x' }
                }
                $candidate = "${prefix}_$('{0:X}' -f $rid)"
                # Ensure unique within type
                $taken = $false
                foreach ($k in $usedInScope.Keys) {
                    $v = $usedInScope[$k]
                    if ($k -like "$tRid|*" -and $v -eq $candidate) { $taken = $true; break }
                }
                $i = 0
                while ($taken) {
                    $i++
                    $candidate = "${prefix}_$('{0:X}' -f $rid)_$i"
                    $taken = $false
                    foreach ($k in $usedInScope.Keys) {
                        $v = $usedInScope[$k]
                        if ($k -like "$tRid|*" -and $v -eq $candidate) { $taken = $true; break }
                    }
                }
                $usedInScope[$scopeKey] = $candidate
            }
            $newName = $usedInScope[$scopeKey]
        }

        [void]$memberRows.Add([pscustomobject]@{
            rid        = $rid
            kind       = $kind
            old        = $name
            new        = $newName
            sig        = $sig
            codepoints = (To-Codepoints $name)
            isObf      = $isMObf
        })

        if ($isMObf -and $oldFull -ne $decryptorFullName) {
            [void]$nameMap.Add([pscustomobject]@{
                typeRid    = $tRid
                typeName   = $oldType
                typeFull   = $oldFull
                scope      = 'Member'
                kind       = $kind
                oldName    = $name
                newName    = $newName
                codepoints = (To-Codepoints $name)
            })
        }
    }
    $typeRows[$typeRows.Count-1].members = $memberRows
}

$metaPath = Join-Path $OutDir "metadata.json"
$mapPath  = Join-Path $OutDir "rename_map.json"

$typeRows | ConvertTo-Json -Depth 6 | Out-File $metaPath -Encoding UTF8
$nameMap  | ConvertTo-Json -Depth 4 | Out-File $mapPath  -Encoding UTF8

Write-Host "[+] Types:        $($typeRows.Count)"
Write-Host "[+] Rename rows:  $($nameMap.Count)"
Write-Host "[+] Wrote $metaPath"
Write-Host "[+] Wrote $mapPath"
