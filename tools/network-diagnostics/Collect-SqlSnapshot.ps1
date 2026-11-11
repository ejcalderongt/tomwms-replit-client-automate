# Optional low-frequency snapshot. Requires existing diagnostic permissions; never grants them.
param([Parameter(Mandatory)][string]$Run,[string]$ConfigPath="$PSScriptRoot/config.local.json")
$ErrorActionPreference='Stop'
if (-not (Test-Path (Join-Path $Run 'session.json'))) { throw 'Existing diagnostics session required' }
$s=(Get-Content -LiteralPath $ConfigPath -Raw | ConvertFrom-Json).sql
if (-not $s.enabled -or -not $s.server -or -not $s.database -or $s.authentication -notin @('Windows','SqlPrompt')) { throw 'Configure SQL target first' }
$b=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$b['Data Source']=$s.server; $b['Initial Catalog']=$s.database; $b['Connect Timeout']=5; $b['Pooling']=$false
$b['Encrypt']=[bool]$s.encrypt; $b['TrustServerCertificate']=[bool]$s.trustServerCertificate
$b['Integrated Security']=$s.authentication -eq 'Windows'; $b['Application Name']='WMS-Network-Diagnostics'
$cn=New-Object System.Data.SqlClient.SqlConnection
$cn.ConnectionString=$b.ConnectionString
if ($s.authentication -ne 'Windows') {
    $cred=Get-Credential -Message 'SQL diagnostic account; no credentials are saved'
    $cn.Credential=New-Object System.Data.SqlClient.SqlCredential($cred.UserName,$cred.Password)
}
$stamp=[datetime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff')
try {
    $cn.Open()
    $queries=[ordered]@{
        requests=@'
SELECT SYSUTCDATETIME() AS server_utc, session_id, status, command, database_id,
       blocking_session_id, wait_type, wait_time, wait_resource, cpu_time,
       total_elapsed_time, reads, writes, logical_reads
FROM sys.dm_exec_requests
WHERE session_id <> @@SPID AND session_id > 50;
'@
        io=@'
SELECT SYSUTCDATETIME() AS server_utc, database_id, file_id, sample_ms,
       num_of_reads, num_of_bytes_read, io_stall_read_ms,
       num_of_writes, num_of_bytes_written, io_stall_write_ms
FROM sys.dm_io_virtual_file_stats(DB_ID(), NULL);
'@
        waits=@'
SELECT SYSUTCDATETIME() AS server_utc, wait_type, waiting_tasks_count,
       wait_time_ms, signal_wait_time_ms
FROM sys.dm_os_wait_stats WHERE waiting_tasks_count > 0;
'@
    }
    foreach ($name in $queries.Keys) {
        $q=$cn.CreateCommand(); $q.CommandTimeout=5; $q.CommandText=$queries[$name]
        try {
            $table=New-Object Data.DataTable
            $reader=$q.ExecuteReader()
            try { $table.Load($reader) } finally { $reader.Dispose() }
            $table | Export-Csv (Join-Path $Run "sql-$stamp-$name.snapshot.csv") -NoTypeInformation -Encoding UTF8
        } catch {
            "${name}: unavailable ($($_.Exception.GetType().Name)); visibility may be limited by permissions" |
                Add-Content (Join-Path $Run "sql-$stamp.snapshot-errors.txt")
        } finally { $q.Dispose() }
    }
} finally { $cn.Dispose() }
Write-Host 'Snapshot saved. Empty requests may reflect restricted visibility. Compare IO/wait deltas, not lifetime totals.'
