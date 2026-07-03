param(
    [Parameter(Mandatory = $true)]
    [string]$Path,

    [string]$GradleProject = "C:\Users\carol\StudioProjects\TOMHH2025",

    [switch]$SkipCompile
)

$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$inspect = Join-Path $skillRoot "scripts\Inspect-JavaEncoding.ps1"

powershell -NoProfile -ExecutionPolicy Bypass -File $inspect -Path $Path

$resolved = Resolve-Path -LiteralPath $Path
$bytes = [System.IO.File]::ReadAllBytes($resolved)
if ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
    Write-Warning "File has UTF-8 BOM. Java source usually should remain UTF-8 without BOM unless already standardized."
}

if (-not $SkipCompile) {
    if (-not (Test-Path -LiteralPath (Join-Path $GradleProject "gradlew.bat"))) {
        throw "Gradle wrapper not found in $GradleProject"
    }

    Push-Location $GradleProject
    try {
        & .\gradlew.bat :app:compileDebugJavaWithJavac
        if ($LASTEXITCODE -ne 0) {
            throw "Gradle compile failed with exit code $LASTEXITCODE"
        }
    } finally {
        Pop-Location
    }
}
