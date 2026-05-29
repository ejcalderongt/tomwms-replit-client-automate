param(
    [Parameter(Mandatory = $true)]
    [string]$Object,
    [string]$RepoRoot = "C:\Users\yejc2\source\repos\wms-brain",
    [string]$Branch = "origin/wms-db-brain"
)

$ErrorActionPreference = "Stop"

function Resolve-DbBrainPath {
    param([string]$Value)

    $clean = $Value.Trim()
    if ($clean.StartsWith("db-brain://")) {
        $clean = $clean.Substring("db-brain://".Length)
    }
    $clean = $clean.Split("#")[0].Trim("/")

    if ($clean.StartsWith("db-brain/")) {
        return $clean
    }
    if ($clean -match "^(tables|views|sps|functions|parametrizacion|_meta)/") {
        if ($clean.EndsWith(".md")) { return "db-brain/$clean" }
        return "db-brain/$clean.md"
    }
    if ($clean.EndsWith(".md")) {
        return "db-brain/$clean"
    }

    foreach ($group in @("tables", "views", "sps", "functions")) {
        $candidate = "db-brain/$group/$clean.md"
        git cat-file -e "$Branch`:$candidate" 2>$null
        if ($LASTEXITCODE -eq 0) {
            return $candidate
        }
    }

    return "db-brain/$clean.md"
}

Push-Location $RepoRoot
try {
    $path = Resolve-DbBrainPath $Object
    git show "$Branch`:$path"
}
finally {
    Pop-Location
}
