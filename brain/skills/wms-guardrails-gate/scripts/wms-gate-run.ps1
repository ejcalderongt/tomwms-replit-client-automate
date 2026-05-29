param(
  [Parameter(Mandatory = $true)][ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")][string]$Process,
  [Parameter(Mandatory = $true)][string]$TargetRepoPath,
  [Parameter(Mandatory = $true)][string[]]$Files,
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)
$ErrorActionPreference = "Stop"
$rgScript = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-regression-guardian\scripts\wms-rg-run.ps1"
$smaScript = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-state-machine-auditor\scripts\wms-sma-run.ps1"
$tlScript = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-telemetry-lite\scripts\wms-tl-run.ps1"
$evalScript = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-guardrails-gate\scripts\wms-gate-evaluate.ps1"

& $rgScript -Process $Process -TargetRepoPath $TargetRepoPath -Files $Files
& $smaScript -Process $Process -TargetRepoPath $TargetRepoPath
& $tlScript -TargetRepoPath $TargetRepoPath -SeedSymbol $Process

$evidence = @()
$evidence += (Get-ChildItem -Path (Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-regression-guardian") | Sort-Object LastWriteTime -Descending | Select-Object -First 1).FullName
$evidence += (Get-ChildItem -Path (Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-state-machine-auditor") | Sort-Object LastWriteTime -Descending | Select-Object -First 1).FullName
$evidence += (Get-ChildItem -Path (Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-telemetry-lite") | Sort-Object LastWriteTime -Descending | Select-Object -First 1).FullName

$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-guardrails-gate"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }
$outPath = Join-Path $outDir "$(Get-Date -Format 'yyyyMMdd-HHmmss')-$Process-gate-report.yml"
& $evalScript -EvidenceFiles $evidence -OutPath $outPath
Write-Output "Generated gate report: $outPath"

