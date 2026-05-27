param(
  [Parameter(Mandatory = $true)][string]$NullSafetyFile,
  [Parameter(Mandatory = $true)][string]$BusinessRulesFile,
  [Parameter(Mandatory = $true)][string]$DbCorrelationFile,
  [Parameter(Mandatory = $true)][string]$OutPath
)
$ErrorActionPreference = "Stop"

$nullText = Get-Content -Path $NullSafetyFile -Raw
$bizText = Get-Content -Path $BusinessRulesFile -Raw
$dbText = Get-Content -Path $DbCorrelationFile -Raw

$status = "PASS"
$findings = New-Object System.Collections.Generic.List[string]

if ($nullText -match "First\(\)" -and -not ($nullText -match "FirstOrDefault\(\)")) {
  $status = "WARN"
  $findings.Add("Potential unsafe First() usage without FirstOrDefault guard context.")
}
if ($dbText -match "note: DB lookup skipped") {
  if ($status -eq "PASS") { $status = "WARN" }
  $findings.Add("DB correlation tokens found but DB lookup skipped.")
}
if ($dbText -match "db_exists:" -and $dbText -match ": False") {
  if ($status -eq "PASS") { $status = "WARN" }
  $findings.Add("Some DB tokens are unresolved in current database lookup.")
}
if ($bizText -match "IdBodega=0" -or $bizText -match "Estado=0") {
  if ($status -eq "PASS") { $status = "WARN" }
  $findings.Add("Low explicit business-rule references in changed files.")
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("id: cag-$(Get-Date -Format 'yyyyMMdd-HHmmss')")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("status: $status")
$lines.Add("checks:")
$lines.Add("  null_safety: done")
$lines.Add("  business_rules: done")
$lines.Add("  db_correlation: done")
$lines.Add("  state_safety: referenced_by_state_machine_auditor")
$lines.Add("findings:")
if ($findings.Count -eq 0) { $lines.Add("  - none") } else { foreach ($f in $findings) { $lines.Add("  - '$f'") } }
$lines.Add("recommendation: 'Promote to PASS only when WARN findings are acknowledged or fixed.'")

$dir = Split-Path -Parent $OutPath
if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
[System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
Write-Output "Wrote $OutPath"
