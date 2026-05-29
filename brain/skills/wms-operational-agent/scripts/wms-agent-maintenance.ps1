param(
    [string]$BrainRoot = "C:/Users/yejc2/source/repos/wms-brain/wms-brain",
    [string]$TomwmsRoot = "C:/Users/yejc2/source/repos/TOMWMS",
    [string]$HhRoot = "C:/Users/yejc2/StudioProjects/TOMHH2025",
    [switch]$CleanCaches,
    [switch]$InstallRuntime,
    [switch]$BuildRoutingIndex,
    [switch]$ReindexBrain
)

$ErrorActionPreference = "Stop"

function Write-Section($title) {
    Write-Output ""
    Write-Output "== $title =="
}

function Invoke-JsonPost($Url, $Token, $Body) {
    $headers = @{
        "Content-Type" = "application/json"
        "X-Brain-Token" = $Token
    }

    return Invoke-RestMethod -Method Post -Uri $Url -Headers $headers -Body ($Body | ConvertTo-Json -Compress)
}

$skillRoot = Join-Path $BrainRoot "brain/skills/wms-operational-agent"
$scriptsRoot = Join-Path $skillRoot "scripts"

Write-Section "WMS Agent Maintenance"
Write-Output "BrainRoot: $BrainRoot"
Write-Output "TomwmsRoot: $TomwmsRoot"
Write-Output "HhRoot: $HhRoot"

Write-Section "Safe Cache Scan"
$cacheRoots = @(
    (Split-Path -Parent $BrainRoot),
    $BrainRoot
) | Sort-Object -Unique

$cacheDirs = @()
foreach ($root in $cacheRoots) {
    if (Test-Path $root) {
        $cacheDirs += Get-ChildItem -LiteralPath $root -Recurse -Force -Directory -Include "__pycache__", ".pytest_cache", ".mypy_cache", ".ruff_cache" -ErrorAction SilentlyContinue
    }
}

if ($cacheDirs.Count -eq 0) {
    Write-Output "[OK] No Python/tool cache directories found."
} elseif ($CleanCaches) {
    foreach ($dir in $cacheDirs) {
        $full = [System.IO.Path]::GetFullPath($dir.FullName)
        $allowed = [System.IO.Path]::GetFullPath((Split-Path -Parent $BrainRoot))
        if (-not $full.StartsWith($allowed)) {
            throw "Refusing to remove cache outside allowed root: $full"
        }
        Remove-Item -LiteralPath $full -Recurse -Force
        Write-Output "[OK] Removed cache: $full"
    }
} else {
    Write-Output "[WARN] Cache directories found. Re-run with -CleanCaches to remove:"
    $cacheDirs | ForEach-Object { Write-Output "  - $($_.FullName)" }
}

Write-Section "Skill Validation"
& (Join-Path $scriptsRoot "validate-wms-skill.ps1")

Write-Section "Runtime Sync"
if ($InstallRuntime) {
    & (Join-Path $scriptsRoot "install-skill.ps1")
} else {
    Write-Output "[INFO] Runtime install skipped. Use -InstallRuntime to refresh Codex copy."
}

Write-Section "Skill Doctor"
& (Join-Path $scriptsRoot "wms-skill-doctor.ps1") -BrainRoot $BrainRoot -TomwmsRoot $TomwmsRoot
$doctorExit = $LASTEXITCODE
if ($doctorExit -eq 2) {
    throw "Skill doctor failed."
}

Write-Section "Local Routing Index"
if ($BuildRoutingIndex) {
    & (Join-Path $scriptsRoot "wms-build-routing-index.ps1") -BrainRoot $BrainRoot
} else {
    Write-Output "[INFO] Routing index build skipped. Use -BuildRoutingIndex to regenerate AGENT-ROUTING-INDEX.yml."
}

Write-Section "Brain/Janeway Reindex"
$brainBaseUrl = if ($env:BRAIN_BASE_URL) { $env:BRAIN_BASE_URL } else { "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain" }
$brainToken = $env:BRAIN_IMPORT_TOKEN

if (-not $ReindexBrain) {
    Write-Output "[INFO] Reindex skipped. Use -ReindexBrain after code changes or stale index warnings."
} elseif ([string]::IsNullOrWhiteSpace($brainToken)) {
    Write-Output "[WARN] BRAIN_IMPORT_TOKEN missing. Cannot reindex Brain/Janeway."
} else {
    Write-Output "[INFO] Reindexing VB repo TOMWMS_BOF..."
    Invoke-JsonPost -Url "$brainBaseUrl/index/vb" -Token $brainToken -Body @{ repos = @("TOMWMS_BOF") } | Out-Null
    Write-Output "[OK] VB reindex requested."

    Write-Output "[INFO] Reindexing Java repo TOMHH2025..."
    Invoke-JsonPost -Url "$brainBaseUrl/index/java" -Token $brainToken -Body @{ repos = @("TOMHH2025") } | Out-Null
    Write-Output "[OK] Java reindex requested."
}

Write-Section "Git Snapshot"
if (Test-Path $TomwmsRoot) {
    Write-Output "-- TOMWMS --"
    git -C $TomwmsRoot status --short
}

if (Test-Path $BrainRoot) {
    Write-Output "-- wms-brain --"
    git -C $BrainRoot status --short
}

Write-Section "Done"
Write-Output "Maintenance finished. Review WARN lines before committing."
