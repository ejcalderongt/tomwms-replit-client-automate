param([Parameter(Mandatory)][string]$Capture,[string]$WiresharkDirectory="${env:ProgramFiles}\Wireshark")
$ErrorActionPreference='Stop'
$tshark=Join-Path $WiresharkDirectory 'tshark.exe'
if (-not (Test-Path -LiteralPath $tshark)) { throw 'TShark not found' }
$Capture=(Resolve-Path -LiteralPath $Capture).Path
$base=$Capture+'.analysis'
try {
    $ErrorActionPreference='Continue'
    & $tshark -n -r $Capture -q -z io,stat,1 -z conv,tcp 2> "$base.errors.txt" |
        Set-Content "$base.stats.txt" -Encoding UTF8
    $statsExit=$LASTEXITCODE
} finally { $ErrorActionPreference='Stop' }
if ($statsExit -ne 0) { throw 'TShark statistics failed; inspect errors file' }
$filter='tcp.analysis.retransmission || tcp.analysis.fast_retransmission || tcp.analysis.spurious_retransmission || tcp.analysis.lost_segment || tcp.analysis.duplicate_ack || tcp.analysis.zero_window || tcp.flags.reset == 1 || dns.flags.rcode != 0'
try {
$ErrorActionPreference='Continue'
& $tshark -n -r $Capture -Y $filter -T fields -E header=y -E separator=, -E quote=d -E occurrence=f `
    -e frame.number -e frame.time_epoch -e ip.src -e ipv6.src -e tcp.srcport -e ip.dst -e ipv6.dst -e tcp.dstport `
    -e tcp.stream -e tcp.analysis.retransmission -e tcp.analysis.fast_retransmission -e tcp.analysis.spurious_retransmission `
    -e tcp.analysis.lost_segment -e tcp.analysis.duplicate_ack -e tcp.analysis.zero_window -e tcp.flags.reset -e dns.flags.rcode `
    2> "$base.indicator-errors.txt" | Set-Content "$base.indicators.csv" -Encoding UTF8
$indicatorsExit=$LASTEXITCODE
} finally { $ErrorActionPreference='Stop' }
if ($indicatorsExit -ne 0) { throw 'TShark indicators failed; inspect errors file' }
Write-Host 'Indicators are hypotheses: inspect dumpcap drop counts, capture point, truncation and offload before attributing packet loss.'
