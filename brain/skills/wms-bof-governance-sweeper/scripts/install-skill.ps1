$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$skillName = Split-Path -Leaf $skillRoot
$codexHome = if ($env:CODEX_HOME) { $env:CODEX_HOME } else { Join-Path $HOME ".codex" }
$target = Join-Path (Join-Path $codexHome "skills") $skillName
New-Item -ItemType Directory -Force -Path (Split-Path -Parent $target) | Out-Null
if (Test-Path $target) { Remove-Item -LiteralPath $target -Recurse -Force }
Copy-Item -LiteralPath $skillRoot -Destination $target -Recurse
Write-Output "Installed $skillName to $target"
