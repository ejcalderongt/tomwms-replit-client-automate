param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string]$TargetFile,
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"

$fullPath = Join-Path $RepoPath $TargetFile
if (-not (Test-Path $fullPath)) { throw "File not found: $TargetFile" }

$content = Get-Content -Path $fullPath -Raw
$applied = New-Object System.Collections.Generic.List[string]

# Safe mechanical normalization: string null/empty checks
$pattern = '([A-Za-z0-9_\.]+)\.equals\(\"\"\)\s*\|\|\s*\1\.isEmpty\(\)\s*\|\|\s*\1==null'
if ($content -match $pattern) {
  $content = [regex]::Replace($content, $pattern, 'TextUtils.isEmpty($1)')
  $applied.Add("Normalized equals/isEmpty/null triple-check to TextUtils.isEmpty(...)")
}

$pattern2 = '([A-Za-z0-9_\.]+)\.equals\(\"\"\)\s*\|\|\s*\1==null'
if ($content -match $pattern2) {
  $content = [regex]::Replace($content, $pattern2, 'TextUtils.isEmpty($1)')
  $applied.Add("Normalized equals/null check to TextUtils.isEmpty(...)")
}

# Ensure import exists if used
if ($content -match 'TextUtils\.isEmpty' -and $content -notmatch 'import android\.text\.TextUtils;') {
  $content = $content -replace "import android\.text\.InputType;", "import android.text.InputType;`r`nimport android.text.TextUtils;"
  $applied.Add("Added import android.text.TextUtils")
}

if ($applied.Count -gt 0) {
  [System.IO.File]::WriteAllText($fullPath, $content, [System.Text.UTF8Encoding]::new($false))
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("target: $TargetFile")
$lines.Add("safe_fixes_applied: $($applied.Count)")
if ($applied.Count -eq 0) { $lines.Add("actions: [none]") } else { foreach ($a in $applied) { $lines.Add("action: $a") } }

if ($OutPath) {
  $dir = Split-Path -Parent $OutPath
  if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
  [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
  Write-Output "Wrote $OutPath"
} else { $lines | Out-String }

