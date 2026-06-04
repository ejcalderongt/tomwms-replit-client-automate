param(
    [string]$RepoRoot = "."
)

$ErrorActionPreference = "Stop"
$script:HasError = $false

function Fail([string]$msg) { Write-Host "[ERROR] $msg" -ForegroundColor Red; $script:HasError = $true }
function Warn([string]$msg) { Write-Host "[WARN]  $msg" -ForegroundColor Yellow }
function Info([string]$msg) { Write-Host "[INFO]  $msg" -ForegroundColor Cyan }

$repo = (Resolve-Path $RepoRoot).Path
$atlasPath = Join-Path $repo "brain/atlas/index.yml"
$handoffIndexPath = Join-Path $repo "brain/handoffs/_index.yml"

if (-not (Test-Path $atlasPath)) { Fail "No existe atlas: $atlasPath" }
if (-not (Test-Path $handoffIndexPath)) { Fail "No existe handoffs index: $handoffIndexPath" }
if ($script:HasError) { exit 1 }

# Atlas validation (lightweight YAML scan)
Info "Validando atlas references"
$atlasLines = Get-Content $atlasPath
$atlasRefPaths = @()
foreach ($line in $atlasLines) {
    if ($line -match '^\s*(coordinator|router|handoffs_index|automation_backlog|lint_script):\s*(.+)$') {
        $atlasRefPaths += $Matches[2].Trim()
    }
    if ($line -match '^\s*path:\s*(brain/.+)$') {
        $atlasRefPaths += $Matches[1].Trim()
    }
}

foreach ($rel in ($atlasRefPaths | Select-Object -Unique)) {
    $p = Join-Path $repo $rel
    if (-not (Test-Path $p)) { Fail "Ruta referenciada no existe: $rel" }
}

# Handoff index validation (entry-level scan)
Info "Validando handoffs index"
$lines = Get-Content $handoffIndexPath
$entries = @()
$current = $null

foreach ($line in $lines) {
    if ($line -match '^\s*-\s+id:\s*(.+)$') {
        if ($null -ne $current) { $entries += $current }
        $current = [ordered]@{ id=$Matches[1].Trim(); path=""; date=""; stale_after_days=""; keys=@{} }
        $current.keys["id"] = $true
        continue
    }

    if ($null -eq $current) { continue }

    if ($line -match '^\s+path:\s*(.+)$') { $current.path = $Matches[1].Trim(); $current.keys["path"] = $true }
    elseif ($line -match '^\s+date:\s*"?([0-9]{4}-[0-9]{2}-[0-9]{2})"?$') { $current.date = $Matches[1].Trim(); $current.keys["date"] = $true }
    elseif ($line -match '^\s+stale_after_days:\s*(\d+)') { $current.stale_after_days = [int]$Matches[1]; $current.keys["stale_after_days"] = $true }
    elseif ($line -match '^\s+domains:\s*\[') { $current.keys["domains"] = $true }
    elseif ($line -match '^\s+tags:\s*\[') { $current.keys["tags"] = $true }
    elseif ($line -match '^\s+symptoms:\s*$') { $current.keys["symptoms"] = $true }
}
if ($null -ne $current) { $entries += $current }

$ids = @{}
$today = Get-Date
foreach ($e in $entries) {
    if ([string]::IsNullOrWhiteSpace($e.id)) { Fail "Entry sin id"; continue }
    if ($ids.ContainsKey($e.id)) { Fail "Entry duplicado: $($e.id)" }
    $ids[$e.id] = $true

    foreach ($k in @("path","date","domains","tags","symptoms")) {
        if (-not $e.keys.Contains($k)) { Fail "Handoff '$($e.id)' sin campo requerido: $k" }
    }

    if (-not [string]::IsNullOrWhiteSpace($e.path)) {
        $hp = Join-Path $repo $e.path
        if (-not (Test-Path $hp)) { Fail "Handoff '$($e.id)' ruta inexistente: $($e.path)" }
    }

    if (-not [string]::IsNullOrWhiteSpace($e.date) -and $e.stale_after_days -is [int]) {
        $d = Get-Date $e.date
        $age = ($today - $d).Days
        if ($age -gt $e.stale_after_days) {
            Warn "Handoff stale: $($e.id) edad=$age dias (umbral=$($e.stale_after_days))"
        }
    }
}

if ($script:HasError) {
    Write-Host "Lint finalizado con errores" -ForegroundColor Red
    exit 1
}

Write-Host "Lint finalizado OK (entries=$($entries.Count))" -ForegroundColor Green
exit 0
