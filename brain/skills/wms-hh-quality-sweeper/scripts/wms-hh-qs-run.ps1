param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string]$TargetFile,
  [ValidateSet("report-only","safe-fix")][string]$Mode = "report-only",
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)
$ErrorActionPreference = "Stop"

$scripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-hh-quality-sweeper\scripts"
$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-hh-quality-sweeper"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }
$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$targetSlug = ($TargetFile -replace '[^A-Za-z0-9]+', '-').Trim('-').ToLowerInvariant()
if ([string]::IsNullOrWhiteSpace($targetSlug)) { $targetSlug = "target" }

$report = Join-Path $outDir "$ts-$targetSlug-hh-qs-report.txt"
$deps = Join-Path $outDir "$ts-$targetSlug-hh-qs-deps.txt"
$fix = Join-Path $outDir "$ts-$targetSlug-hh-qs-safe-fix.txt"
$summary = Join-Path $outDir "$ts-$targetSlug-hh-qs-summary.yml"

& (Join-Path $scripts "wms-hh-qs-report.ps1") -RepoPath $RepoPath -TargetFile $TargetFile -OutPath $report
& (Join-Path $scripts "wms-hh-qs-deps.ps1") -RepoPath $RepoPath -OutPath $deps
if ($Mode -eq "safe-fix") {
  & (Join-Path $scripts "wms-hh-qs-safe-fix.ps1") -RepoPath $RepoPath -TargetFile $TargetFile -OutPath $fix
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("id: hh-qs-$ts")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("mode: $Mode")
$lines.Add("target: '$TargetFile'")
$lines.Add("artifacts:")
$lines.Add("  report: '$report'")
$lines.Add("  deps: '$deps'")
if ($Mode -eq "safe-fix") { $lines.Add("  safe_fix: '$fix'") }
$lines.Add("status: done")
[System.IO.File]::WriteAllLines($summary, $lines, [System.Text.UTF8Encoding]::new($false))

Write-Output "Generated:"
Write-Output $report
Write-Output $deps
if ($Mode -eq "safe-fix") { Write-Output $fix }
Write-Output $summary
