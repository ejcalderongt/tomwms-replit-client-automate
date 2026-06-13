param(
  [Parameter(Mandatory=$true)][string]$RepoPath,
  [string]$Trigger = ''
)

$ErrorActionPreference = 'Stop'
$core = 'C:\Users\yejc2\source\repos\brain-core'
$catalog = Join-Path $core 'catalog\projects.yml'

if (-not (Test-Path $catalog)) { throw "Catalog not found: $catalog" }

$projects = Get-Content $catalog -Raw
$overlay = $null

switch -Regex ($RepoPath) {
  'TOMWMS' { $overlay = 'C:\Users\yejc2\source\repos\TOMWMS\brain\project-overlay.yml'; break }
  'RoadPOD' { $overlay = 'C:\Users\yejc2\source\repos\RoadPOD\brain\project-overlay.yml'; break }
  'MCP' { $overlay = 'C:\Users\yejc2\source\repos\MCP\brain\project-overlay.yml'; break }
  default { $overlay = '' }
}

Write-Output "core_router=$core\brain-router.yml"
Write-Output "core_policies=$core\policies\jira-policy.yml;$core\policies\security-policy.yml;$core\policies\git-policy.yml"
if ($overlay) { Write-Output "project_overlay=$overlay" }
if ($Trigger) { Write-Output "trigger=$Trigger" }
