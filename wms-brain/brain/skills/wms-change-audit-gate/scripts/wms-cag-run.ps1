param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string[]]$Files = @(),
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain",
  [string]$SqlServer = "",
  [string]$SqlDatabase = "",
  [string]$SqlUser = "",
  [string]$SqlPassword = ""
)
$ErrorActionPreference = "Stop"

Push-Location $RepoPath
try {
  if ($Files.Count -eq 0) {
    $changed = git diff --name-only HEAD
    $Files = @($changed | Where-Object { $_ -match '\.(vb|java|sql)$' })
  }
}
finally { Pop-Location }

if ($Files.Count -eq 0) { throw "No candidate files found for audit." }

$scripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-change-audit-gate\scripts"
$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-change-audit-gate"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }
$ts = Get-Date -Format "yyyyMMdd-HHmmss"

$nullOut = Join-Path $outDir "$ts-null-safety.txt"
$bizOut = Join-Path $outDir "$ts-business-rules.txt"
$dbOut = Join-Path $outDir "$ts-db-correlation.txt"
$evalOut = Join-Path $outDir "$ts-change-audit-report.yml"

& (Join-Path $scripts "wms-cag-null-safety.ps1") -RepoPath $RepoPath -Files $Files -OutPath $nullOut
& (Join-Path $scripts "wms-cag-business-rules.ps1") -RepoPath $RepoPath -Files $Files -OutPath $bizOut
& (Join-Path $scripts "wms-cag-db-correlation.ps1") -RepoPath $RepoPath -Files $Files -OutPath $dbOut -SqlServer $SqlServer -SqlDatabase $SqlDatabase -SqlUser $SqlUser -SqlPassword $SqlPassword
& (Join-Path $scripts "wms-cag-evaluate.ps1") -NullSafetyFile $nullOut -BusinessRulesFile $bizOut -DbCorrelationFile $dbOut -OutPath $evalOut

Write-Output "Generated:"
Write-Output $nullOut
Write-Output $bizOut
Write-Output $dbOut
Write-Output $evalOut
