param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [Parameter(Mandatory = $true)]
    [string]$TargetRepoPath,
    [Parameter(Mandatory = $true)]
    [string[]]$Files,
    [string[]]$Methods = @(),
    [string[]]$Tables = @(),
    [string[]]$Flags = @(),
    [string[]]$States = @(),
    [string[]]$Patterns = @("IdUbicacionDestino","isEmpty\\(\\)","cantidad_verificada",'estado\s*=\s*"Despachado"'),
    [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)

$ErrorActionPreference = "Stop"

$skillScripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-regression-guardian\scripts"
$outDirRel = "wms-brain/brain/handoffs/2026-05-27-regression-guardian"
$outDir = Join-Path $BrainRepoRoot $outDirRel
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }

$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$footprint = Join-Path $outDir "$ts-$Process-footprint.yml"
$siblings = Join-Path $outDir "$ts-$Process-sibling-scan.txt"
$checklist = Join-Path $outDir "$ts-$Process-checklist.yml"
$risk = Join-Path $outDir "$ts-$Process-residual-risk.yml"

& (Join-Path $skillScripts "wms-rg-footprint.ps1") -Process $Process -Files $Files -Methods $Methods -Tables $Tables -Flags $Flags -States $States -OutPath $footprint
& (Join-Path $skillScripts "wms-rg-sibling-scan.ps1") -RepoPath $TargetRepoPath -Patterns $Patterns -OutPath $siblings
& (Join-Path $skillScripts "wms-rg-checklist.ps1") -Process $Process -OutPath $checklist
& (Join-Path $skillScripts "wms-rg-risk-report.ps1") -Process $Process -FootprintPath $footprint -SiblingScanPath $siblings -ChecklistPath $checklist -RiskLevel "medium" -Recommendation "Run checklist and close only if no high-risk findings remain." -OutPath $risk

Write-Output "Generated:"
Write-Output $footprint
Write-Output $siblings
Write-Output $checklist
Write-Output $risk
