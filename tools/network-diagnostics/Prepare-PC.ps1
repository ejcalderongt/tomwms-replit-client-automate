param([switch]$InstallWireshark)
$ErrorActionPreference='Stop'
Write-Host "PowerShell $($PSVersionTable.PSVersion); Windows $([Environment]::OSVersion.VersionString)"
foreach ($name in @('curl.exe','Resolve-DnsName','Get-NetAdapter','winget.exe')) {
    [pscustomobject]@{component=$name;available=[bool](Get-Command $name -ErrorAction SilentlyContinue)}
}
$ws="${env:ProgramFiles}\Wireshark"
foreach ($exe in @('dumpcap.exe','tshark.exe')) {
    [pscustomobject]@{component=$exe;available=(Test-Path (Join-Path $ws $exe))}
}
Get-Service npcap -ErrorAction SilentlyContinue | Select-Object Name,Status
if ($InstallWireshark) {
    if (-not (Get-Command winget.exe -ErrorAction SilentlyContinue)) { throw 'Get the signed Wireshark installer from https://www.wireshark.org/download.html' }
    & winget.exe install --id WiresharkFoundation.Wireshark --exact --source winget --interactive
    if ($LASTEXITCODE -ne 0) { throw "Wireshark installation returned $LASTEXITCODE" }
    Write-Host 'Finish Npcap installation in the installer. Then verify Start-Capture.ps1 -List. Do not assume Npcap was installed unattended.'
}
