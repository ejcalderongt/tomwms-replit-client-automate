param(
    [Parameter(Mandatory = $true)]
    [string]$RepoPath,
    [Parameter(Mandatory = $true)]
    [string]$TargetBranch,
    [Parameter(Mandatory = $true)]
    [string[]]$BaselineBranches,
    [Parameter(Mandatory = $true)]
    [string]$Pattern,
    [int]$Context = 2
)

$ErrorActionPreference = "Stop"

Push-Location $RepoPath
try {
    git fetch --all | Out-Null
    foreach ($base in $BaselineBranches) {
        "=== DRIFT: $TargetBranch vs $base ==="
        git diff "$base...$TargetBranch" -- . | Select-String -Pattern $Pattern -Context $Context, $Context | ForEach-Object { $_.ToString() }
        ""
    }
}
finally {
    Pop-Location
}
