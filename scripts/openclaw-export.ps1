# OpenClaw Export
param(
  [string]$OutDir = ".\\backups"
)

New-Item -ItemType Directory -Force -Path $OutDir | Out-Null
Write-Host "Export placeholder -> $OutDir"
