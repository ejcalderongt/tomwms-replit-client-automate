param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string]$ScopePath = "TOMIMSV4",
  [ValidateSet("report-only","safe-fix")][string]$Mode = "report-only",
  [string]$BrainRepoRoot = "C:\Users\yejc2\source\repos\wms-brain"
)
$ErrorActionPreference = "Stop"

$scripts = "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\skills\wms-bof-governance-sweeper\scripts"
$outDir = Join-Path $BrainRepoRoot "wms-brain/brain/handoffs/2026-05-27-bof-governance-sweeper"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }

$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$scopeSlug = ($ScopePath -replace '[^A-Za-z0-9]+', '-').Trim('-').ToLowerInvariant()
if ([string]::IsNullOrWhiteSpace($scopeSlug)) { $scopeSlug = "scope" }

$report = Join-Path $outDir "$ts-$scopeSlug-bof-gs-report.txt"
$summary = Join-Path $outDir "$ts-$scopeSlug-bof-gs-summary.yml"

& (Join-Path $scripts "wms-bof-gs-scan.ps1") -RepoPath $RepoPath -ScopePath $ScopePath -OutPath $report

$reportText = Get-Content -Raw -Path $report
$reportLines = Get-Content -Path $report

function Count-SectionItems {
  param([string[]]$Lines, [string]$SectionTitle)
  $start = -1
  for ($i = 0; $i -lt $Lines.Count; $i++) {
    if ($Lines[$i] -eq "=== $SectionTitle ===") { $start = $i + 1; break }
  }
  if ($start -lt 0) { return 0 }
  $count = 0
  for ($j = $start; $j -lt $Lines.Count; $j++) {
    $line = $Lines[$j]
    if ($line -like "=== * ===") { break }
    if ([string]::IsNullOrWhiteSpace($line)) { continue }
    if ($line -eq "(none)") { continue }
    $count++
  }
  return $count
}

$unused = Count-SectionItems -Lines $reportLines -SectionTitle "UNUSED_PRIVATE_CANDIDATES"
$dup = Count-SectionItems -Lines $reportLines -SectionTitle "DUPLICATE_SQL_FRAGMENT_CANDIDATES"
$heavy = Count-SectionItems -Lines $reportLines -SectionTitle "HEAVY_QUERY_CANDIDATES"
$round = Count-SectionItems -Lines $reportLines -SectionTitle "ROUNDTRIP_HOTSPOTS"
$rule = Count-SectionItems -Lines $reportLines -SectionTitle "BUSINESS_RULE_RISK_CANDIDATES"
$vbScanned = [regex]::Match($reportText, '(?m)^vb_files_scanned:\s+(\d+)').Groups[1].Value
if ([string]::IsNullOrWhiteSpace($vbScanned)) { $vbScanned = "0" }

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("id: bof-gs-$ts")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("mode: $Mode")
$lines.Add("repo_path: '$RepoPath'")
$lines.Add("scope_path: '$ScopePath'")
$lines.Add("summary:")
$lines.Add("  vb_files_scanned: $vbScanned")
$lines.Add("  unused_private_candidates: $unused")
$lines.Add("  duplicate_query_candidates: $dup")
$lines.Add("  heavy_query_candidates: $heavy")
$lines.Add("  roundtrip_hotspots: $round")
$lines.Add("  business_rule_risk_candidates: $rule")
$lines.Add("artifacts:")
$lines.Add("  report: '$report'")
$lines.Add("status: done")
[System.IO.File]::WriteAllLines($summary, $lines, [System.Text.UTF8Encoding]::new($false))

Write-Output "Generated:"
Write-Output $report
Write-Output $summary
