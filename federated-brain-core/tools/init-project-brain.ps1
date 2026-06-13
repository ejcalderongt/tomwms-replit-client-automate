param(
  [Parameter(Mandatory=$true)][string]$ProjectName,
  [Parameter(Mandatory=$true)][string]$RepoPath
)

$ErrorActionPreference = 'Stop'
$brainDir = Join-Path $RepoPath 'brain'
$domainsDir = Join-Path $brainDir 'domains'
$handoffsDir = Join-Path $brainDir 'handoffs'

New-Item -ItemType Directory -Force -Path $brainDir, $domainsDir, $handoffsDir | Out-Null

@"
version: 1
project:
  name: $ProjectName
  repo_path: $RepoPath
  stack: TODO
context:
  domains:
    - domain-database
    - domain-jira
"@ | Set-Content -Path (Join-Path $brainDir 'project-overlay.yml') -Encoding UTF8

Write-Host "Initialized brain overlay for $ProjectName at $brainDir"
