# OpenClaw Restore
param(
  [ValidateSet("Bootstrap","Restore")]
  [string]$Mode = "Bootstrap",
  [string]$Source = "."
)

$ErrorActionPreference = 'Stop'
Write-Host "Mode: $Mode"
Write-Host "Source: $Source"

$required = @('skills','scripts','config','manifests')
foreach ($item in $required) {
  $path = Join-Path $Source $item
  if (-not (Test-Path $path)) {
    throw "Missing required path: $path"
  }
}

if ($Mode -eq 'Bootstrap') {
  Write-Host 'Bootstrap check passed.'
} else {
  Write-Host 'Restore check passed.'
}
