param(
  [Parameter(Mandatory = $true)][string[]]$EvidenceFiles,
  [Parameter(Mandatory = $true)][string]$OutPath
)
$ErrorActionPreference = "Stop"
$status = "PASS"
if ($EvidenceFiles.Count -lt 3) { $status = "WARN" }
foreach ($f in $EvidenceFiles) {
  if (-not (Test-Path $f)) { $status = "FAIL" }
}
$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("id: gate-$(Get-Date -Format 'yyyyMMdd-HHmmss')")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("status: $status")
$lines.Add("evidence:")
foreach ($f in $EvidenceFiles) { $lines.Add("  - '$f'") }
$lines.Add("open_risks: []")
$lines.Add("recommendation: 'Close only if status PASS or approved WARN.'")
$dir = Split-Path -Parent $OutPath
if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
[System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
Write-Output "Wrote $OutPath"

