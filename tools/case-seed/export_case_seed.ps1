param(
  [Parameter(Mandatory=$true)][string]$CaseId,
  [Parameter(Mandatory=$true)][string]$CaseType,
  [Parameter(Mandatory=$true)][datetime]$From,
  [Parameter(Mandatory=$true)][datetime]$To,
  [string]$Sku = "",
  [int]$IdProducto = 0,
  [int]$IdBodega = 0,
  [string]$OutDir = ".\out",
  [switch]$ZipOnly
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function New-SeedConnectionString {
  $server = $env:WMS_KILLIOS_DB_HOST
  $db = $env:WMS_KILLIOS_DB_NAME
  $user = $env:WMS_KILLIOS_DB_USER
  $pwd = $env:WMS_KILLIOS_DB_PASSWORD

  if ([string]::IsNullOrWhiteSpace($server) -or [string]::IsNullOrWhiteSpace($db) -or [string]::IsNullOrWhiteSpace($user) -or [string]::IsNullOrWhiteSpace($pwd)) {
    throw "Faltan variables de entorno DB. Requiere: WMS_KILLIOS_DB_HOST, WMS_KILLIOS_DB_NAME, WMS_KILLIOS_DB_USER, WMS_KILLIOS_DB_PASSWORD"
  }

  return "Server=$server;Database=$db;User ID=$user;Password=$pwd;Encrypt=False;TrustServerCertificate=True;"
}

function Invoke-SeedQuery {
  param(
    [Parameter(Mandatory=$true)][string]$ConnectionString,
    [Parameter(Mandatory=$true)][string]$SqlText
  )

  $conn = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
  try {
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.CommandTimeout = 180
    $cmd.CommandText = $SqlText

    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($cmd)
    $dt = New-Object System.Data.DataTable
    [void]$adapter.Fill($dt)

    $rows = @()
    foreach ($row in $dt.Rows) {
      $obj = [ordered]@{}
      foreach ($col in $dt.Columns) {
        $obj[$col.ColumnName] = $row[$col.ColumnName]
      }
      $rows += [pscustomobject]$obj
    }
    return $rows
  }
  finally {
    if ($conn.State -ne 'Closed') { $conn.Close() }
  }
}

function Expand-Template {
  param([string]$SqlRaw)

  $s = $SqlRaw
  $s = $s.Replace('{{FROM}}', $From.ToString('yyyy-MM-dd HH:mm:ss'))
  $s = $s.Replace('{{TO}}', $To.ToString('yyyy-MM-dd HH:mm:ss'))
  $s = $s.Replace('{{SKU}}', $Sku.Replace("'","''"))
  $s = $s.Replace('{{IDPRODUCTO}}', [string]$IdProducto)
  $s = $s.Replace('{{IDBODEGA}}', [string]$IdBodega)
  return $s
}

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$queryDir = Join-Path $root "queries\$CaseType"
if (-not (Test-Path $queryDir)) {
  throw "No existe carpeta de queries para CaseType='$CaseType': $queryDir"
}

$seedDir = Join-Path $OutDir ("seed_" + $CaseId + "_" + (Get-Date -Format "yyyyMMdd_HHmmss"))
New-Item -ItemType Directory -Path $seedDir -Force | Out-Null

$cs = New-SeedConnectionString
$queryFiles = Get-ChildItem -Path $queryDir -Filter *.sql | Sort-Object Name
if (-not $queryFiles) { throw "No hay .sql en $queryDir" }

$manifest = [ordered]@{
  caseId = $CaseId
  caseType = $CaseType
  generatedAt = (Get-Date).ToString('s')
  range = @{ from = $From.ToString('s'); to = $To.ToString('s') }
  filters = @{ sku = $Sku; idProducto = $IdProducto; idBodega = $IdBodega }
  db = @{ host = $env:WMS_KILLIOS_DB_HOST; database = $env:WMS_KILLIOS_DB_NAME; user = $env:WMS_KILLIOS_DB_USER }
  files = @()
}

Write-Host "Exportando seed: $seedDir"

foreach ($qf in $queryFiles) {
  Write-Host " -> $($qf.Name)"
  $raw = Get-Content -Path $qf.FullName -Raw -Encoding UTF8
  $sql = Expand-Template -SqlRaw $raw

  $rows = Invoke-SeedQuery -ConnectionString $cs -SqlText $sql

  $base = [System.IO.Path]::GetFileNameWithoutExtension($qf.Name)
  $jsonPath = Join-Path $seedDir ($base + '.json')
  $csvPath = Join-Path $seedDir ($base + '.csv')
  $sqlPath = Join-Path $seedDir ($base + '.resolved.sql')

  Set-Content -Path $sqlPath -Value $sql -Encoding UTF8
  ($rows | ConvertTo-Json -Depth 8) | Set-Content -Path $jsonPath -Encoding UTF8
  $rows | Export-Csv -Path $csvPath -NoTypeInformation -Encoding UTF8

  $manifest.files += [ordered]@{
    query = $qf.Name
    rows = @($rows).Count
    json = [System.IO.Path]::GetFileName($jsonPath)
    csv = [System.IO.Path]::GetFileName($csvPath)
    sql = [System.IO.Path]::GetFileName($sqlPath)
  }
}

$manifestPath = Join-Path $seedDir 'manifest.json'
($manifest | ConvertTo-Json -Depth 8) | Set-Content -Path $manifestPath -Encoding UTF8

$zipPath = "$seedDir.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path (Join-Path $seedDir '*') -DestinationPath $zipPath

Write-Host ""
Write-Host "SEED OK"
Write-Host "Carpeta: $seedDir"
Write-Host "ZIP:     $zipPath"
Write-Host ""
Write-Host "Siguiente paso: subir el ZIP a Replit/chat junto al CASE_INTAKE_TEMPLATE lleno."
