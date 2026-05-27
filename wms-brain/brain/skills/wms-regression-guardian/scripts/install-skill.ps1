$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$skillName = Split-Path -Leaf $skillRoot
$codexHome = if ($env:CODEX_HOME) { $env:CODEX_HOME } else { Join-Path $HOME ".codex" }
$targetRoot = Join-Path $codexHome "skills"
$target = Join-Path $targetRoot $skillName

New-Item -ItemType Directory -Force -Path $targetRoot | Out-Null
if (Test-Path $target) {
    Remove-Item -LiteralPath $target -Recurse -Force
}
Copy-Item -LiteralPath $skillRoot -Destination $target -Recurse
Write-Output "Installed $skillName to $target"
