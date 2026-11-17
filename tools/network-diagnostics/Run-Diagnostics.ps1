# #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS
[CmdletBinding()]
param([string]$ConfigPath="$PSScriptRoot/config.local.json", [string]$OutputRoot="$PSScriptRoot/evidence")
$ErrorActionPreference='Stop'
$cfg=Get-Content -LiteralPath $ConfigPath -Raw | ConvertFrom-Json
foreach ($pair in @(@('durationSeconds',1,28800),@('intervalSeconds',1,300),@('timeoutSeconds',1,30))) {
    $value=[int]$cfg.($pair[0])
    if ($value -lt $pair[1] -or $value -gt $pair[2]) { throw "Invalid $($pair[0])" }
}
if ($cfg.sql.enabled -and ([int]$cfg.sql.intervalSeconds -lt 5 -or [int]$cfg.sql.intervalSeconds -gt 300)) { throw 'SQL interval must be 5..300 seconds' }
$probes=@()
foreach ($kind in @('icmp','dns','tcp','http')) {
    foreach ($t in @($cfg.$kind)) {
        if ($null -eq $t) { continue }
        if ($t.name -notmatch '^[a-zA-Z0-9_-]{1,60}$') { throw 'Target name must be a short unique label (letters, digits, _ or -)' }
        if ($kind -ne 'http' -and [string]::IsNullOrWhiteSpace($t.host)) { throw 'Target host missing' }
        if ($kind -eq 'tcp' -and ([int]$t.port -lt 1 -or [int]$t.port -gt 65535)) { throw 'Invalid TCP port' }
        if ($kind -eq 'dns' -and -not $t.server) { throw 'DNS probe requires explicit resolver server' }
        if ($kind -eq 'http') {
            $uri=[uri]$t.url
            if (-not $uri.IsAbsoluteUri -or $uri.Scheme -notin @('http','https') -or $uri.UserInfo -or $uri.Query -or $uri.Fragment) { throw 'Use http(s) URL without credentials, query or fragment; use POST body for private parameters' }
            if ($t.method -notin @('GET','POST') -or $t.approvedReadOnly -ne $true) { throw 'HTTP probe requires GET/POST and approvedReadOnly=true after reviewing method semantics' }
            if (@($t.expectedStatus).Count -eq 0) { throw 'expectedStatus is required' }
            foreach ($field in @('headersFile','bodyFile')) {
                if ($t.$field) {
                    $path=[string]$t.$field
                    if (-not [IO.Path]::IsPathRooted($path)) { $path=Join-Path (Split-Path (Resolve-Path $ConfigPath) -Parent) $path }
                    $t.$field=(Resolve-Path -LiteralPath $path).Path
                }
            }
            if (-not (Get-Command curl.exe -ErrorAction SilentlyContinue)) { throw 'curl.exe is required for HTTP timing' }
        }
        $probes+=@{kind=$kind;target=$t;interval=[int]$cfg.intervalSeconds}
    }
}
$credential=$null
if ($cfg.sql.enabled) {
    if (-not $cfg.sql.server -or -not $cfg.sql.database -or $cfg.sql.authentication -notin @('Windows','SqlPrompt')) { throw 'Configure SQL server, database and Windows or SqlPrompt authentication' }
    if ($cfg.sql.authentication -eq 'SqlPrompt') { $credential=Get-Credential -Message 'SQL diagnostic account (SELECT 1 only); never saved' }
    $cfg.sql | Add-Member -NotePropertyName name -NotePropertyValue sql -Force
    $probes+=@{kind='sql';target=$cfg.sql;interval=[int]$cfg.sql.intervalSeconds}
}
if (-not $probes.Count) { throw 'No targets configured. Read README.md and configure local endpoints first.' }
$keys=@($probes | ForEach-Object { "$($_.kind)-$($_.target.name)" })
if (@($keys | Select-Object -Unique).Count -ne $keys.Count) { throw 'Duplicate target names within a probe kind' }
$run=Join-Path $OutputRoot ((Get-Date -Format 'yyyyMMdd-HHmmss')+'-'+[guid]::NewGuid().ToString('N').Substring(0,8))
New-Item -ItemType Directory -Path $run -Force | Out-Null
$run=(Resolve-Path $run).Path
# Inventory can take time on systems with slow management providers; start probe window after it.
& "$PSScriptRoot/Snapshot.ps1" -Output $run -Label before
$started=[datetime]::UtcNow
$deadline=$started.AddSeconds([int]$cfg.durationSeconds)
$meta=[ordered]@{tag='#EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS';site=$cfg.site;scenario=$cfg.scenario;computer=$env:COMPUTERNAME;
    startedUtc=$started.ToString('o');timezone=[TimeZoneInfo]::Local.Id;powershellVersion=$PSVersionTable.PSVersion.ToString();configSha256=(Get-FileHash $ConfigPath).Hash;
    durationSeconds=$cfg.durationSeconds;intervalSeconds=$cfg.intervalSeconds;timeoutSeconds=$cfg.timeoutSeconds;
    targets=@($probes | ForEach-Object { @{kind=$_.kind;name=$_.target.name;intervalSeconds=$_.interval;
        host=$_.target.host;port=$_.target.port;url=$_.target.url;method=$_.target.method;server=$_.target.server} })}
$meta | ConvertTo-Json -Depth 6 | Set-Content (Join-Path $run 'session.json') -Encoding UTF8
# Raw configuration may reference private headers/bodies. Do not copy it into evidence.
$jobs=@()
try {
    foreach ($p in $probes) {
        $file=Join-Path $run ("$($p.kind)-$($p.target.name).csv")
        $jobs+=Start-Job -FilePath "$PSScriptRoot/Probe.ps1" -ArgumentList $p.kind,$p.target,$file,$deadline,$p.interval,([int]$cfg.timeoutSeconds),$credential
    }
    Write-Host "Session: $run"
    Write-Host 'Collecting independent probes. Use Mark-Incident.ps1 from another terminal. Ctrl+C stops this session.'
    while (@($jobs | Where-Object State -eq Running).Count -gt 0) {
        & "$PSScriptRoot/Snapshot.ps1" -Output $run -Label periodic -Light
        $jobs | Wait-Job -Timeout 10 -ErrorAction SilentlyContinue | Out-Null
    }
} finally {
    foreach ($j in $jobs) {
        if ($j.State -eq 'Running') { Stop-Job $j }
        [pscustomobject]@{id=$j.Id;state=[string]$j.State;reason=[string]$j.ChildJobs[0].JobStateInfo.Reason} |
            Export-Csv (Join-Path $run 'workers.csv') -Append -NoTypeInformation -Encoding UTF8
        Receive-Job $j -ErrorAction Continue | Out-Null
        Remove-Job $j -Force
    }
    & "$PSScriptRoot/Snapshot.ps1" -Output $run -Label after
    & "$PSScriptRoot/Summarize.ps1" -Run $run
    Write-Host "Evidence saved: $run"
}
