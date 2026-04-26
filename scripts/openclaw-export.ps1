# OpenClaw Export
param(
  [string]$OutDir = ".\\backups"
)

$ErrorActionPreference = 'Stop'
New-Item -ItemType Directory -Force -Path $OutDir | Out-Null
$stamp = Get-Date -Format 'yyyyMMdd-HHmmss'
$dest = Join-Path $OutDir "export-$stamp"
New-Item -ItemType Directory -Force -Path $dest | Out-Null

$manifest = [ordered]@{
  exported_utc = (Get-Date).ToUniversalTime().ToString('o')
  machine = $env:COMPUTERNAME
  user = $env:USERNAME
  items = @()
}

$paths = @(
  @{ Name = 'skills'; Path = 'skills' },
  @{ Name = 'scripts'; Path = 'scripts' },
  @{ Name = 'config'; Path = 'config' },
  @{ Name = 'manifests'; Path = 'manifests' }
)

foreach ($p in $paths) {
  if (Test-Path $p.Path) {
    Copy-Item $p.Path -Destination (Join-Path $dest $p.Name) -Recurse -Force
    $manifest.items += $p.Name
  }
}

$manifestPath = Join-Path $dest 'manifest.runtime.json'
$manifest | ConvertTo-Json -Depth 6 | Set-Content -Encoding UTF8 $manifestPath
Write-Host "Exported to $dest"
