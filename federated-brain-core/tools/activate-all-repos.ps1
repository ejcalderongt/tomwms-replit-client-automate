param(
  [string]$ReposRoot = "C:\Users\yejc2\source\repos",
  [string]$CorePath = "C:\Users\yejc2\source\repos\wms-brain\federated-brain-core",
  [switch]$WhatIf
)

$ErrorActionPreference = 'Stop'

$skip = @('_codex_build','_codex_build_fix','.git')
$dirs = Get-ChildItem -Path $ReposRoot -Directory | Where-Object { $skip -notcontains $_.Name }
$activated = @()

foreach ($d in $dirs) {
  $repoPath = $d.FullName

  $hasSln = @(Get-ChildItem -Path $repoPath -Filter '*.sln' -File -ErrorAction SilentlyContinue).Count -gt 0
  $hasCsproj = @(Get-ChildItem -Path $repoPath -Filter '*.csproj' -File -ErrorAction SilentlyContinue).Count -gt 0
  $isCandidate = (Test-Path (Join-Path $repoPath '.git')) -or
                 $hasSln -or
                 $hasCsproj -or
                 (Test-Path (Join-Path $repoPath 'AGENTS.md')) -or
                 (Test-Path (Join-Path $repoPath 'CLAUDE.md'))

  if (-not $isCandidate) { continue }

  $brainDir = Join-Path $repoPath 'brain'
  $domainsDir = Join-Path $brainDir 'domains'
  $handoffsDir = Join-Path $brainDir 'handoffs'
  $overlayPath = Join-Path $brainDir 'project-overlay.yml'
  $setupPath = Join-Path $repoPath 'BRAIN-FEDERATED-SETUP.md'

  if (-not $WhatIf) {
    New-Item -ItemType Directory -Force -Path $brainDir, $domainsDir, $handoffsDir | Out-Null

    if (-not (Test-Path $overlayPath)) {
      $overlayLines = @(
        'version: 1',
        'project:',
        "  name: $($d.Name)",
        "  repo_path: $repoPath",
        '  stack: TODO',
        '',
        'inherits:',
        "  router: $CorePath\\brain-router.yml",
        '',
        'domains_available:',
        '  - domain-database',
        '  - domain-jira'
      )
      Set-Content -Path $overlayPath -Value ($overlayLines -join "`r`n") -Encoding UTF8
    }

    if (-not (Test-Path $setupPath)) {
      $setupLines = @(
        "# Federated Brain Bootstrap ($($d.Name))",
        '',
        'Global core:',
        "- $CorePath",
        '',
        'Local overlay:',
        "- $overlayPath",
        '',
        'Quick resolve:',
        "- $CorePath\\tools\\resolve-context.ps1 -RepoPath $repoPath -Trigger jira"
      )
      Set-Content -Path $setupPath -Value ($setupLines -join "`r`n") -Encoding UTF8
    }
  }

  $activated += $d.Name
}

$manifest = Join-Path $CorePath ('catalog\\activation-manifest-' + (Get-Date -Format yyyyMMdd-HHmmss) + '.txt')
$activated | Sort-Object | Set-Content -Path $manifest -Encoding UTF8
Write-Output ('Activated repos: ' + $activated.Count)
Write-Output ('Manifest: ' + $manifest)
