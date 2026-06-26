param(
  [string]$Root = (Get-Location).Path
)

$rootScript = Join-Path $Root "scripts\bootstrap-session.ps1"
if (-not (Test-Path $rootScript)) {
  throw "Missing root bootstrap script: $rootScript"
}

& $rootScript -Domain wms -Root $Root
