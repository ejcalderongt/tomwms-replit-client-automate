param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string[]]$Files,
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
$patterns = @(
  "isEmpty\(\)",
  "\.size\(\)\s*>\s*0",
  "==\s*null",
  "!=\s*null",
  "catch\s*\(Exception",
  "printStackTrace\(",
  "Nothing",
  "First\(\)",
  "FirstOrDefault\(\)"
)
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("check: null_safety")
  foreach ($f in $Files) {
    $lines.Add("file: $f")
    foreach ($p in $patterns) {
      $hits = rg -n $p $f
      if ($hits) { foreach ($h in $hits) { $lines.Add("  $h") } }
    }
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

