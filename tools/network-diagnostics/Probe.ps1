# #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS
# Windows PowerShell 5.1 / PowerShell 7. One worker per target; no retries.
param([string]$Kind, $Target, [string]$Output, [datetime]$Deadline,
      [int]$Interval, [int]$Timeout, [pscredential]$SqlCredential)
$ErrorActionPreference = 'Stop'
$inv = [Globalization.CultureInfo]::InvariantCulture
function Ms($value) { [math]::Round(([double]::Parse([string]$value,$inv) * 1000),3) }
do {
    $watch = [Diagnostics.Stopwatch]::StartNew()
    $row = [ordered]@{timestampUtc=[datetime]::UtcNow.ToString('o');kind=$Kind;target=$Target.name;
        success=$false;status='error';totalMs=$null;dnsMs=$null;tcpMs=$null;tlsMs=$null;
        waitFirstByteMs=$null;ttfbMs=$null;downloadMs=$null;httpStatus=$null;remoteIp=$null;
        connectMs=$null;queryMs=$null;detail=''}
    try {
        switch ($Kind) {
            'icmp' {
                $p = New-Object Net.NetworkInformation.Ping
                try {
                    $r = $p.Send([string]$Target.host, $Timeout*1000)
                    $row.status = [string]$r.Status
                    $row.success = $r.Status -eq 'Success'
                    if ($row.success) { $row.totalMs=$r.RoundtripTime; $row.remoteIp=[string]$r.Address }
                } finally { $p.Dispose() }
            }
            'tcp' {
                $c = New-Object Net.Sockets.TcpClient
                try {
                    $ar = $c.BeginConnect([string]$Target.host,[int]$Target.port,$null,$null)
                    if (-not $ar.AsyncWaitHandle.WaitOne($Timeout*1000)) { throw [TimeoutException]::new('connect_timeout') }
                    $c.EndConnect($ar)
                    $row.totalMs=[math]::Round($watch.Elapsed.TotalMilliseconds,3)
                    $row.remoteIp=[string]$c.Client.RemoteEndPoint.Address
                    $row.success=$true; $row.status='connected'
                } finally { $c.Close() }
            }
            'dns' {
                # Explicit resolver, bypass OS cache; cold recursive lookup is NOT guaranteed.
                $r = @(Resolve-DnsName -Name $Target.host -Server $Target.server -Type A -DnsOnly -NoHostsFile -QuickTimeout -ErrorAction Stop)
                $row.remoteIp=(@($r | Where-Object IPAddress | Select-Object -ExpandProperty IPAddress) -join '|')
                if (-not $row.remoteIp) { throw 'no_A_answer' }
                $row.totalMs=[math]::Round($watch.Elapsed.TotalMilliseconds,3)
                $row.success=$true; $row.status='resolved'
            }
            'http' {
                $body=[IO.Path]::GetTempFileName()
                $err=[IO.Path]::GetTempFileName()
                try {
                    $format='%{http_code}|%{remote_ip}|%{time_namelookup}|%{time_connect}|%{time_appconnect}|%{time_pretransfer}|%{time_starttransfer}|%{time_total}'
                    $curlArgs=@('--disable','--silent','--show-error','--connect-timeout',"$Timeout",'--max-time',"$Timeout",'--max-filesize','2097152','--output',$body,'--write-out',$format,'--request',$Target.method,'--proto','=http,https')
                    if ($Target.headersFile) { $curlArgs+=@('--header',('@'+$Target.headersFile)) }
                    if ($Target.bodyFile) { $curlArgs+=@('--data-binary',('@'+$Target.bodyFile)) }
                    $curlArgs+=@('--url',$Target.url)
                    # PS5 turns native stderr into ErrorRecords even when redirected.
                    # Keep curl's timeout/status metrics instead of aborting before reading them.
                    $savedPreference=$ErrorActionPreference
                    try {
                        $ErrorActionPreference='Continue'
                        $raw = & curl.exe @curlArgs 2> $err
                        $exitCode=$LASTEXITCODE
                    } finally { $ErrorActionPreference=$savedPreference }
                    $v=([string]($raw -join '')).Split('|')
                    if ($v.Count -eq 8) {
                        $row.httpStatus=$v[0]; $row.remoteIp=$v[1]
                        $row.dnsMs=Ms $v[2]
                        $row.tcpMs=[math]::Max(0,((Ms $v[3])-(Ms $v[2])))
                        $row.tlsMs=if ((Ms $v[4]) -gt 0) { [math]::Max(0,((Ms $v[4])-(Ms $v[3]))) } else { 0 }
                        $row.waitFirstByteMs=[math]::Max(0,((Ms $v[6])-(Ms $v[5])))
                        $row.ttfbMs=Ms $v[6]; $row.totalMs=Ms $v[7]
                        $row.downloadMs=[math]::Max(0,((Ms $v[7])-(Ms $v[6])))
                    }
                    $row.status="curl_$exitCode"
                    # Never write response bodies, credentials, URL parameters or curl stderr to evidence.
                    if ($exitCode -eq 0) {
                        $row.success=(@($Target.expectedStatus) -contains [int]$row.httpStatus)
                        $row.status=if ($row.success) { 'http_ok' } else { 'unexpected_http_status' }
                        $content=[IO.File]::ReadAllText($body)
                        if ($content -match '<(?:[\w.-]+:)?Fault(?:\s|>)') { $row.success=$false; $row.status='soap_fault' }
                        elseif ($Target.expectedContains -and -not $content.Contains([string]$Target.expectedContains)) {
                            $row.success=$false; $row.status='expected_content_missing'
                        }
                    }
                } finally { Remove-Item -LiteralPath $body,$err -Force -ErrorAction SilentlyContinue }
            }
            'sql' {
                $b=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
                # Explicit keys avoid PowerShell's IDictionary adapter treating property names as keywords.
                $b['Data Source']=$Target.server; $b['Initial Catalog']=$Target.database
                $b['Connect Timeout']=$Timeout; $b['Pooling']=$false
                $b['Encrypt']=[bool]$Target.encrypt; $b['TrustServerCertificate']=[bool]$Target.trustServerCertificate
                $b['Application Name']='WMS-Network-Diagnostics'
                $b['Integrated Security']=$Target.authentication -eq 'Windows'
                $cn=New-Object System.Data.SqlClient.SqlConnection
                $cn.ConnectionString=$b.ConnectionString
                if ($Target.authentication -ne 'Windows') {
                    if (-not $SqlCredential) { throw 'sql_credential_missing' }
                    $cn.Credential=New-Object System.Data.SqlClient.SqlCredential($SqlCredential.UserName,$SqlCredential.Password)
                }
                try {
                    $cn.Open(); $row.connectMs=[math]::Round($watch.Elapsed.TotalMilliseconds,3)
                    $q=$cn.CreateCommand(); $q.CommandTimeout=$Timeout
                    $q.CommandText='SELECT CAST(1 AS int);'
                    $qw=[Diagnostics.Stopwatch]::StartNew()
                    try { $answer=$q.ExecuteScalar() } finally { $q.Dispose() }
                    $row.queryMs=[math]::Round($qw.Elapsed.TotalMilliseconds,3)
                    $row.success=$answer -eq 1; $row.status='select_1'
                    $row.totalMs=[math]::Round($watch.Elapsed.TotalMilliseconds,3)
                } finally { $cn.Dispose() }
            }
        }
    } catch {
        # Exception text may contain endpoint names or server messages: store type only.
        $row.detail=$_.Exception.GetType().FullName
        if ($row.status -eq 'error') { $row.status='probe_error' }
        $cause=$_.Exception
        while ($cause) {
            if ($cause -is [System.Data.SqlClient.SqlException]) { $row.status='sql_'+$cause.Number; break }
            if ($cause -is [TimeoutException]) { $row.status='timeout'; break }
            if ($cause -is [Net.Sockets.SocketException]) { $row.status='socket_'+$cause.SocketErrorCode; break }
            $cause=$cause.InnerException
        }
    }
    if ($null -eq $row.totalMs) { $row.totalMs=[math]::Round($watch.Elapsed.TotalMilliseconds,3) }
    [pscustomobject]$row | Export-Csv -LiteralPath $Output -Append -NoTypeInformation -Encoding UTF8
    $delay=[math]::Max(0,($Interval*1000-$watch.ElapsedMilliseconds))
    $remaining=($Deadline.ToUniversalTime()-[datetime]::UtcNow).TotalMilliseconds
    if ($delay -gt 0 -and $remaining -gt 0) { Start-Sleep -Milliseconds ([int][math]::Min($delay,$remaining)) }
} while ([datetime]::UtcNow -lt $Deadline.ToUniversalTime())
