param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string]$SeedSymbol,
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("seed_symbol: $SeedSymbol")
  $lines.Add("hits:")
  $hits = rg -n --no-ignore -g *.vb -g *.java -g *.sql $SeedSymbol .
  if ([string]::IsNullOrWhiteSpace(($hits | Out-String))) {
    $lines.Add("  - '(sin hallazgos)'")
  } else {
    foreach ($h in $hits) { $lines.Add("  - '$h'") }
  }
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

