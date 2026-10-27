$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$skillName = Split-Path -Leaf $skillRoot
. (Join-Path $skillRoot 'scripts/resolve-wms-paths.ps1')
$paths = Resolve-WmsPaths
$skillRoot = Join-Path $paths.BrainRoot "brain/skills/$skillName"
$codexHome = if ($env:CODEX_HOME) { $env:CODEX_HOME } else { Join-Path $HOME ".codex" }
$targetRoot = Join-Path $codexHome "skills"
$target = Join-Path $targetRoot $skillName

New-Item -ItemType Directory -Force -Path $targetRoot | Out-Null

if (Test-Path -LiteralPath $target) {
    $backupRoot = Join-Path $env:LOCALAPPDATA 'PrograX24/brain-skill-backups'
    $backup = Join-Path $backupRoot ("{0}-{1}-{2}" -f $skillName, (Get-Date -Format 'yyyyMMdd-HHmmss'), [guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Force -Path $backupRoot | Out-Null
    Copy-Item -LiteralPath $target -Destination $backup -Recurse
    Write-Output "Previous runtime preserved: $backup"
}
New-Item -ItemType Directory -Force -Path $target | Out-Null
Get-ChildItem -LiteralPath $skillRoot -Recurse -File | ForEach-Object {
    $relative = $_.FullName.Substring($skillRoot.TrimEnd('\', '/').Length).TrimStart('\', '/')
    $destination = Join-Path $target $relative
    # Preserve user-facing metadata when it differs; this task changes behavior/paths, not UI policy.
    if ($relative.Replace('\', '/') -eq 'agents/openai.yaml' -and (Test-Path -LiteralPath $destination) -and
        (Get-FileHash -LiteralPath $destination).Hash -ne (Get-FileHash -LiteralPath $_.FullName).Hash) {
        Write-Output 'Preserved existing runtime agents/openai.yaml metadata.'
    } else {
        New-Item -ItemType Directory -Force -Path (Split-Path $destination -Parent) | Out-Null
        Copy-Item -LiteralPath $_.FullName -Destination $destination -Force
    }
}
$locator = Join-Path $env:LOCALAPPDATA 'PrograX24/brain-location.json'
New-Item -ItemType Directory -Force -Path (Split-Path $locator -Parent) | Out-Null
$paths | ConvertTo-Json | Set-Content -LiteralPath $locator -Encoding utf8

Write-Output "Installed $skillName to $target"
Write-Output "Restart Codex or open a new session to reload skill metadata."
