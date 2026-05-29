param(
  [Parameter(Mandatory = $true)][ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")][string]$Process,
  [Parameter(Mandatory = $true)][string]$TargetRepoPath,
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)
$ErrorActionPreference = "Stop"
$scripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-state-machine-auditor\scripts"
$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-state-machine-auditor"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }
$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$extract = Join-Path $outDir "$ts-$Process-extract.txt"
$matrix = Join-Path $outDir "$ts-$Process-matrix.yml"
& (Join-Path $scripts "wms-sma-extract.ps1") -RepoPath $TargetRepoPath -Process $Process -OutPath $extract
& (Join-Path $scripts "wms-sma-matrix.ps1") -Process $Process -OutPath $matrix
Write-Output "Generated:"
Write-Output $extract
Write-Output $matrix

