<#
    Tiny PowerShell test client for loy-bridge.

    DO NOT OPEN THIS FILE IN THE IDE - some IDE features (auto-format,
    encoding-on-save, etc.) keep wiping it to 0 bytes. Run it from a
    terminal instead.

    Usage:
        .\test-client.ps1 ping
        .\test-client.ps1 list_ports
        .\test-client.ps1 read_eeprom '{"variant":"keihin"}'

        # custom server (note: -Server, not -Host - $Host is reserved)
        .\test-client.ps1 ping -Server 192.168.0.163 -Port 7878

        # show full bytes array (default truncates large arrays)
        .\test-client.ps1 read_eeprom '{"variant":"keihin"}' -Full
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string] $Method,

    [Parameter(Position = 1)]
    [string] $Params = "null",

    [string] $Server     = "127.0.0.1",
    [int]    $Port       = 7878,
    [int]    $TimeoutSec = 60,
    [switch] $Full
)

function Send-BridgeRequest {
    param(
        [string] $Server,
        [int]    $Port,
        [string] $Json,
        [int]    $TimeoutSec
    )

    $client = New-Object System.Net.Sockets.TcpClient
    $iar = $client.BeginConnect($Server, $Port, $null, $null)
    if (-not $iar.AsyncWaitHandle.WaitOne($TimeoutSec * 1000, $false)) {
        $client.Close()
        throw "TCP connect to $Server`:$Port timed out after $TimeoutSec sec"
    }
    $client.EndConnect($iar) | Out-Null

    $client.NoDelay        = $true
    $client.SendTimeout    = $TimeoutSec * 1000
    $client.ReceiveTimeout = $TimeoutSec * 1000

    $stream = $client.GetStream()
    $writer = New-Object System.IO.StreamWriter($stream, [System.Text.UTF8Encoding]::new($false))
    $writer.NewLine = "`n"
    $reader = New-Object System.IO.StreamReader($stream, [System.Text.UTF8Encoding]::new($false))
    try {
        $writer.WriteLine($Json); $writer.Flush()
        return $reader.ReadLine()
    } finally {
        $client.Close()
    }
}

$id         = [DateTimeOffset]::Now.ToUnixTimeMilliseconds()
$paramsJson = if ($Params -eq "" -or $Params -eq "null") { 'null' } else { $Params }
$req        = "{""id"":$id,""method"":""$Method"",""params"":$paramsJson}"

Write-Host "-> $req" -ForegroundColor DarkGray

try {
    $resp = Send-BridgeRequest -Server $Server -Port $Port -Json $req -TimeoutSec $TimeoutSec
} catch {
    Write-Host "X $_" -ForegroundColor Red
    exit 1
}

if (-not $resp) {
    Write-Host "X empty response (server closed connection)" -ForegroundColor Red
    exit 1
}

Write-Host "<- " -ForegroundColor DarkGray -NoNewline

try {
    $obj = $resp | ConvertFrom-Json
    if (-not $Full -and $obj.result -and $obj.result.bytes -and $obj.result.bytes.Count -gt 32) {
        $count = $obj.result.bytes.Count
        $head  = ($obj.result.bytes[0..7] | ForEach-Object { '0x{0:X2}' -f $_ }) -join ' '
        $obj.result.bytes = "<$count bytes - first 8: $head ... (use -Full to dump)>"
    }
    if ($obj.error) {
        Write-Host ($obj | ConvertTo-Json -Depth 6) -ForegroundColor Red
        exit 1
    } else {
        $obj | ConvertTo-Json -Depth 6
    }
} catch {
    $resp
}
