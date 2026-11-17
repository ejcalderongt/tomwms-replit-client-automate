#CKFK20261117_REC_CM_RCA: read-only incident evidence. No credentials on disk.
param([Parameter(Mandatory)][string]$QueryPath, [string]$OutputPath)
$ErrorActionPreference = 'Stop'
$sql = Get-Content -LiteralPath $QueryPath -Raw
if ($sql -match '(?i)\b(INSERT|UPDATE|DELETE|MERGE|EXEC|ALTER|CREATE|DROP|TRUNCATE|INTO)\b') {
    throw 'Only SELECT diagnostics are permitted.'
}
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source'] = '10.238.26.70'
$builder['Initial Catalog'] = 'TOMWMS_MAMPA_QA'
$builder['User ID'] = 'sa'
$builder['Password'] = $env:WMS_INCIDENT_PASSWORD
$builder['Encrypt'] = $false
$builder['Connect Timeout'] = 12
$conn = New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.CommandTimeout = 45
    $cmd.CommandText = $sql
    $reader = $cmd.ExecuteReader()
    $sets = [System.Collections.Generic.List[object]]::new()
    do {
        $rows = [System.Collections.Generic.List[object]]::new()
        while ($reader.Read()) {
            $row = [ordered]@{}
            for ($i = 0; $i -lt $reader.FieldCount; $i++) {
                $row[$reader.GetName($i)] = if ($reader.IsDBNull($i)) { $null } else { $reader.GetValue($i) }
            }
            $rows.Add($row)
        }
        $sets.Add(@($rows.ToArray()))
    } while ($reader.NextResult())
    $reader.Close()
    $json = ConvertTo-Json -InputObject @($sets.ToArray()) -Depth 8
    if ($OutputPath) { [IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false)) }
    $json
} finally { $conn.Dispose(); $builder.Clear() }
