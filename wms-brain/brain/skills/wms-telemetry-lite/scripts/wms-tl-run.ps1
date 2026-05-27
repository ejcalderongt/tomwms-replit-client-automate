param(
  [Parameter(Mandatory = $true)][string]$TargetRepoPath,
  [Parameter(Mandatory = $true)][string]$SeedSymbol,
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)
$ErrorActionPreference = "Stop"
$scripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-telemetry-lite\scripts"
$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-telemetry-lite"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }
$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$trace = Join-Path $outDir "$ts-trace-map.yml"
$impact = Join-Path $outDir "$ts-param-impact.yml"
$atlas = Join-Path $outDir "$ts-business-rule-atlas.yml"
$corr = Join-Path $outDir "$ts-correlation.yml"
& (Join-Path $scripts "wms-tl-trace-map.ps1") -RepoPath $TargetRepoPath -SeedSymbol $SeedSymbol -OutPath $trace
& (Join-Path $scripts "wms-tl-param-impact.ps1") -RepoPath $TargetRepoPath -OutPath $impact
& (Join-Path $scripts "wms-tl-business-rule-atlas.ps1") -RepoPath $TargetRepoPath -OutPath $atlas
python (Join-Path $scripts "wms-tl-correlation.py") --trace-map $trace --param-impact $impact --out $corr
Write-Output "Generated:"
Write-Output $trace
Write-Output $impact
Write-Output $atlas
Write-Output $corr
