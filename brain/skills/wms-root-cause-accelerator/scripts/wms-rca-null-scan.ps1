param(
    [Parameter(Mandatory = $true)]
    [string]$RepoPath,
    [ValidateSet("java","vb","all")]
    [string]$Lang = "all",
    [int]$Max = 200
)

$ErrorActionPreference = "Stop"

Push-Location $RepoPath
try {
    $patterns = @(
        "isEmpty\(\)",
        "\.size\(\)\s*>\s*0",
        "!=\s*null",
        "==\s*null",
        "printStackTrace\(",
        "catch\s*\(Exception",
        "List\s+[A-Za-z0-9_]+\s*=",
        "IdUbicacionDestino"
    )

    $globs = @()
    if ($Lang -eq "java" -or $Lang -eq "all") { $globs += "*.java" }
    if ($Lang -eq "vb" -or $Lang -eq "all") { $globs += "*.vb" }

    foreach ($pat in $patterns) {
        "=== PATTERN: $pat ==="
        rg -n --max-count $Max -g $globs $pat .
        ""
    }
}
finally {
    Pop-Location
}
