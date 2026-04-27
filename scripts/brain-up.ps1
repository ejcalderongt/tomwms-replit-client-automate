# Brain Up MVP
param(
  [string]$ExchangeRepo = "C:\tomwms-exchange",
  [string]$BrainRepo = "C:\tomwms-brain",
  [string]$WmsRepo = "",
  [string]$SqlServer = "",
  [string]$SqlDatabase = "",
  [string]$SqlUser = "",
  [switch]$PromptSql,
  [switch]$NoPull
)

$ErrorActionPreference = 'Stop'

function Say($msg) { Write-Host $msg }
function Test-GitRepo($path) { Test-Path (Join-Path $path '.git') }
function Git($repo, $args) {
  $r = & git -C $repo @args 2>&1
  if ($LASTEXITCODE -ne 0) { throw ($r | Out-String) }
  return ($r | Out-String).Trim()
}
function CleanTree($repo) {
  $s = Git $repo @('status','--porcelain')
  return [string]::IsNullOrWhiteSpace($s)
}

Say '=== brain up MVP ==='

foreach ($repo in @($ExchangeRepo, $BrainRepo)) {
  if (-not (Test-GitRepo $repo)) { throw "No es repo git: $repo" }
}

if (-not $NoPull) {
  Say "[1/4] Sync exchange..."
  Git $ExchangeRepo @('fetch','--all','--prune') | Out-Null
  Git $ExchangeRepo @('pull','--ff-only') | Out-Null

  Say "[2/4] Sync brain..."
  Git $BrainRepo @('fetch','--all','--prune') | Out-Null
  Git $BrainRepo @('pull','--ff-only') | Out-Null
}

if (-not (CleanTree $ExchangeRepo)) { throw 'Exchange sucio' }
if (-not (CleanTree $BrainRepo)) { throw 'Brain sucio' }

Say '[3/4] Brain status'
$brainReadme = Join-Path $BrainRepo 'brain\README.md'
$bridge = Join-Path $BrainRepo 'brain\BRIDGE.md'
if (Test-Path $brainReadme) { Say ' - brain/README.md OK' }
if (Test-Path $bridge) { Say ' - brain/BRIDGE.md OK' }

if ($PromptSql -and (-not $SqlServer)) {
  $SqlServer = Read-Host 'SQL Server'
  $SqlDatabase = Read-Host 'SQL Database'
  $SqlUser = Read-Host 'SQL User'
}

if ($SqlServer) {
  Say '[4/4] SQL test...'
  $conn = "Server=$SqlServer;Database=$SqlDatabase;User Id=$SqlUser;TrustServerCertificate=True;Encrypt=True;Connection Timeout=5;"
  $script = @"
try {
  Add-Type -AssemblyName System.Data
  `$c = New-Object System.Data.SqlClient.SqlConnection '$conn'
  `$c.Open()
  Write-Output 'SQL_OK'
  `$c.Close()
} catch {
  Write-Output ('SQL_FAIL: ' + `$_.Exception.Message)
  exit 1
}
"@
  $tmp = Join-Path $env:TEMP 'brain-up-sql.ps1'
  Set-Content -Path $tmp -Value $script -Encoding UTF8
  & powershell -NoProfile -ExecutionPolicy Bypass -File $tmp | Out-Host
}

Say ''
Say 'Hello Erik'
Say '(•‿•)'
Say ' /|\'
Say ' / \'
Say ''
Say 'conexion lista'
Say 'wms-brain up & running'

$state = [ordered]@{
  updated_at_utc = (Get-Date).ToUniversalTime().ToString('o')
  exchange_repo = $ExchangeRepo
  brain_repo = $BrainRepo
  wms_repo = $WmsRepo
  sql_server = $SqlServer
  sql_database = $SqlDatabase
  status = 'ready'
}
$state | ConvertTo-Json -Depth 5 | Set-Content -Encoding UTF8 (Join-Path $PSScriptRoot '..\state\brain-up.json')
