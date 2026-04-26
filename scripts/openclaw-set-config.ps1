# OpenClaw Set Config
param(
  [string]$Source = ".\\config",
  [string]$Target = $env:USERPROFILE,
  [switch]$DryRun
)

$ErrorActionPreference = 'Stop'
Write-Host "Source: $Source"
Write-Host "Target: $Target"
Write-Host "DryRun: $DryRun"

if (-not (Test-Path $Source)) { throw "Config source not found: $Source" }

$items = Get-ChildItem -Path $Source -File -Recurse
foreach ($item in $items) {
  $relative = $item.FullName.Substring((Resolve-Path $Source).Path.Length).TrimStart('\\','/')
  $targetPath = Join-Path $Target $relative
  Write-Host "Would sync: $relative -> $targetPath"
}
