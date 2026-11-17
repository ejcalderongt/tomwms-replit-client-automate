param([string]$Output,[string]$Label='manual',[switch]$Light)
$ErrorActionPreference='Continue'
$stamp=[datetime]::UtcNow.ToString('o')
"`r`n=== $stamp $Label ===" | Add-Content (Join-Path $Output 'wifi.txt')
netsh wlan show interfaces 2>&1 | Add-Content (Join-Path $Output 'wifi.txt')
try {
    Get-NetAdapterStatistics -ErrorAction Stop | Select-Object @{n='timestampUtc';e={$stamp}},Name,ReceivedBytes,SentBytes,ReceivedDiscardedPackets,OutboundDiscardedPackets,ReceivedPacketErrors,OutboundPacketErrors |
        Export-Csv (Join-Path $Output 'nic.csv') -Append -NoTypeInformation -Encoding UTF8
} catch { "$stamp NIC unavailable" | Add-Content (Join-Path $Output 'snapshot-errors.txt') }
if (-not $Light) {
    foreach ($entry in @(@('ipconfig',{ipconfig /all}),@('routes',{route print}),@('clock',{w32tm /query /status}),@('proxy',{netsh winhttp show proxy}),@('curl',{curl.exe --version}))) {
        try { & $entry[1] 2>&1 | Out-File (Join-Path $Output ("$Label-$($entry[0]).txt")) -Encoding UTF8 } catch { }
    }
    Get-NetAdapter -ErrorAction SilentlyContinue | Select-Object Name,InterfaceDescription,Status,LinkSpeed,MacAddress |
        Export-Csv (Join-Path $Output "$Label-adapters.csv") -NoTypeInformation -Encoding UTF8
}
