param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string]$TargetFile,
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("target: $TargetFile")
  $checks = @(
    '==\s*null',
    '!=\s*null',
    'isEmpty\(\)',
    'equals\(\"\"\)',
    'private\s+(final\s+)?[A-Za-z0-9_<>,\[\]]+\s+[A-Za-z0-9_]+\s*(=|;)',
    'setOnFocusChangeListener',
    'setOnKeyListener',
    'setText\(\"'
  )
  foreach ($c in $checks) {
    $lines.Add("=== CHECK: $c ===")
    $hits = rg -n $c $TargetFile
    if ($hits) { foreach ($h in $hits) { $lines.Add($h) } } else { $lines.Add("(sin hallazgos)") }
    $lines.Add("")
  }
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

