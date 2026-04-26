$j = Get-Content tools\strings.json -Raw -Encoding UTF8 | ConvertFrom-Json
Write-Host "Type:    $($j.GetType().Name)"
Write-Host "Count:   $($j.Count)"
$dups = $j | Group-Object codepoints | Where-Object { $_.Count -gt 1 }
Write-Host "Duplicate codepoints groups: $($dups.Count)"
$dups | Select-Object -First 5 | ForEach-Object {
    Write-Host ("  {0}  x{1}" -f $_.Name, $_.Count)
    $_.Group | ForEach-Object { Write-Host "      => $($_.value)" }
}
$dupMethods = $j | Group-Object method | Where-Object { $_.Count -gt 1 }
Write-Host "Duplicate method-name groups: $($dupMethods.Count)"
