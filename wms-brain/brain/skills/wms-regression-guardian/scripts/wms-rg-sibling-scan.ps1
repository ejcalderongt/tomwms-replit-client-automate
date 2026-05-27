param(
    [Parameter(Mandatory = $true)]
    [string]$RepoPath,
    [Parameter(Mandatory = $true)]
    [string[]]$Patterns,
    [string]$Globs = "*.vb,*.java",
    [int]$MaxPerPattern = 80,
    [string]$OutPath = ""
)

$ErrorActionPreference = "Stop"

$globList = $Globs.Split(",") | ForEach-Object { $_.Trim() } | Where-Object { $_ }

Push-Location $RepoPath
try {
    $report = New-Object System.Collections.Generic.List[string]
    foreach ($pat in $Patterns) {
        $report.Add("=== PATTERN: $pat ===")
        $args = @("-n", "--no-ignore", "--max-count", "$MaxPerPattern")
        foreach ($g in $globList) {
            $args += @("-g", $g)
        }
        $args += @($pat, ".")
        $result = & rg @args
        if ([string]::IsNullOrWhiteSpace(($result | Out-String))) {
            $report.Add("(sin hallazgos)")
        } else {
            foreach ($line in $result) { $report.Add($line) }
        }
        $report.Add("")
    }

    if ($OutPath) {
        $dir = Split-Path -Parent $OutPath
        if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
        [System.IO.File]::WriteAllLines($OutPath, $report, [System.Text.UTF8Encoding]::new($false))
        Write-Output "Wrote $OutPath"
    } else {
        $report | Out-String
    }
}
finally {
    Pop-Location
}
