# #EJC20261111_CUMBRE_CAPTURE
[CmdletBinding()]
param([string]$Interface,[string]$Filter,[string]$Run,[ValidateRange(10,3600)][int]$Seconds=300,
      [ValidateRange(96,65535)][int]$SnapLength=256,[switch]$List,
      [string]$WiresharkDirectory="${env:ProgramFiles}\Wireshark")
$ErrorActionPreference='Stop'
$dumpcap=Join-Path $WiresharkDirectory 'dumpcap.exe'
if (-not (Test-Path -LiteralPath $dumpcap)) { throw 'Install official Wireshark with TShark and Npcap first; see README.md' }
if ($List) { & $dumpcap -D; exit $LASTEXITCODE }
if (-not $Interface -or -not $Filter -or -not $Run) { throw 'Specify Interface, Filter and Run. Use -List first.' }
if (-not (Test-Path -LiteralPath (Join-Path $Run 'session.json'))) { throw 'Use an existing diagnostics session directory' }
$stem='capture-'+(Get-Date -Format 'yyyyMMdd-HHmmss')
$pcap=Join-Path $Run "$stem.pcapng"
@{startedUtc=[datetime]::UtcNow.ToString('o');interface=$Interface;filter=$Filter;seconds=$Seconds;snapLength=$SnapLength;
    rotationKB=16384;files=8;promiscuous=$false} | ConvertTo-Json | Set-Content (Join-Path $Run "$stem.json") -Encoding UTF8
# Ring buffer: ~128 MiB maximum; 5 minutes by default. No promiscuous/monitor mode.
try {
    $ErrorActionPreference='Continue'
    & $dumpcap -i $Interface -p -f $Filter -s $SnapLength -a "duration:$Seconds" -b filesize:16384 -b files:8 -w $pcap 2>&1 |
        Tee-Object -FilePath (Join-Path $Run "$stem.log")
    $captureExit=$LASTEXITCODE
} finally { $ErrorActionPreference='Stop' }
if ($captureExit -ne 0) { throw "Capture failed: exit $captureExit; inspect log and capture permissions/Npcap" }
Write-Host 'Capture completed. Run Analyze-Capture.ps1 for packet indicators, then Summarize.ps1 for final hashes.'
