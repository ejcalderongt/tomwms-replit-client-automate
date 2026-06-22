param(
  [string]$DbaRepoPath = "C:\Users\yejc2\source\repos\DBA",
  [string]$ExpectedRemote = "https://github.com/ejcalderongt/DBA.git",
  [switch]$RequireClean
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path (Join-Path $DbaRepoPath '.git'))) {
  throw "DBA repo not found or not a git repo: $DbaRepoPath"
}

$overlayPath = Join-Path $DbaRepoPath 'brain\project-overlay.yml'
if (-not (Test-Path $overlayPath)) {
  throw "DBA project overlay not found: $overlayPath"
}

$origin = (& git -C $DbaRepoPath remote get-url origin).Trim()
if ($origin -ne $ExpectedRemote) {
  throw "Unexpected DBA origin. Expected '$ExpectedRemote' but found '$origin'"
}

$branch = (& git -C $DbaRepoPath branch --show-current).Trim()
$head = (& git -C $DbaRepoPath rev-parse HEAD).Trim()
$status = @(& git -C $DbaRepoPath status --short)

if ($RequireClean -and $status.Count -gt 0) {
  throw "DBA worktree is not clean. Review local changes before publishing."
}

Write-Output "route=DBA_CANONICAL"
Write-Output "repo_path=$DbaRepoPath"
Write-Output "origin=$origin"
Write-Output "branch=$branch"
Write-Output "head=$head"
Write-Output "overlay=$overlayPath"
Write-Output ("status_count=" + $status.Count)
if ($status.Count -gt 0) {
  Write-Output "status:"
  $status | ForEach-Object { Write-Output $_ }
}
