param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $files = @("build.gradle","settings.gradle","gradle.properties","app/build.gradle","app/build.gradle.kts")
  foreach ($f in $files) {
    if (Test-Path $f) {
      $lines.Add("=== $f ===")
      $hits = rg -n "com.android.tools.build:gradle|androidx|compileSdk|minSdk|targetSdk|kotlin|classpath|implementation|api" $f
      if ($hits) { foreach ($h in $hits) { $lines.Add($h) } } else { $lines.Add("(sin hallazgos)") }
      $lines.Add("")
    }
  }
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

