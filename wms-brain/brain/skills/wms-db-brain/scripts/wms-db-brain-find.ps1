param(
    [Parameter(Mandatory = $true)]
    [string]$Query,
    [string]$RepoRoot = "C:\Users\yejc2\source\repos\wms-brain",
    [string]$Branch = "origin/wms-db-brain",
    [switch]$Content,
    [int]$Max = 80
)

$ErrorActionPreference = "Stop"

Push-Location $RepoRoot
try {
    $files = git -c core.quotepath=false ls-tree -r --name-only $Branch -- db-brain
    $pathHits = @($files | Where-Object { $_ -match [regex]::Escape($Query) } | Select-Object -First $Max)

    if ($pathHits.Count -gt 0) {
        "Path hits:"
        $pathHits
    }

    if ($Content) {
        ""
        "Content hits:"
        $count = 0
        foreach ($file in $files) {
            if ($count -ge $Max) { break }
            $text = git show "$Branch`:$file" 2>$null
            if ($LASTEXITCODE -eq 0 -and ($text -match [regex]::Escape($Query))) {
                $file
                $count++
            }
        }
    }
}
finally {
    Pop-Location
}
