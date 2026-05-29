param(
    [string]$RepoRoot = "C:\Users\yejc2\source\repos\wms-brain",
    [string]$Branch = "origin/wms-db-brain",
    [string]$Destination = "wms-brain/brain/db-brain",
    [switch]$Fetch,
    [switch]$Force
)

$ErrorActionPreference = "Stop"

Push-Location $RepoRoot
try {
    if ($Fetch) {
        git fetch origin wms-db-brain | Out-Host
    }

    $destFull = Join-Path $RepoRoot $Destination
    if ((Test-Path $destFull) -and -not $Force) {
        throw "Destination exists: $Destination. Re-run with -Force to replace it."
    }

    $repoFull = (Resolve-Path $RepoRoot).Path
    $destParent = Split-Path -Parent $destFull
    if (-not (Test-Path $destParent)) {
        New-Item -ItemType Directory -Path $destParent | Out-Null
    }

    if (Test-Path $destFull) {
        $resolvedDest = (Resolve-Path $destFull).Path
        if (-not $resolvedDest.StartsWith($repoFull, [System.StringComparison]::OrdinalIgnoreCase)) {
            throw "Refusing to remove destination outside repo: $resolvedDest"
        }
        Remove-Item -LiteralPath $destFull -Recurse -Force
    }

    $tmp = Join-Path ([System.IO.Path]::GetTempPath()) ("wms-db-brain-" + [guid]::NewGuid().ToString("N"))
    New-Item -ItemType Directory -Path $tmp | Out-Null
    try {
        git archive $Branch db-brain | tar -x -C $tmp
        Move-Item -LiteralPath (Join-Path $tmp "db-brain") -Destination $destFull
    }
    finally {
        if (Test-Path $tmp) {
            Remove-Item -LiteralPath $tmp -Recurse -Force
        }
    }

    Write-Host "Materialized $Branch`:db-brain to $Destination"
}
finally {
    Pop-Location
}
