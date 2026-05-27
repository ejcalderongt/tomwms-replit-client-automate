param(
    [string]$BrainRoot = "C:/Users/yejc2/source/repos/wms-brain/wms-brain",
    [string]$TomwmsRoot = "C:/Users/yejc2/source/repos/TOMWMS",
    [string]$CodexHome = $(if ($env:CODEX_HOME) { $env:CODEX_HOME } else { Join-Path $HOME ".codex" })
)

$ErrorActionPreference = "Stop"

$script:HasFail = $false
$script:HasWarn = $false

function Write-Section($title) {
    Write-Output ""
    Write-Output "== $title =="
}

function Ok($message) {
    Write-Output "[OK] $message"
}

function Warn($message) {
    $script:HasWarn = $true
    Write-Output "[WARN] $message"
}

function Fail($message) {
    $script:HasFail = $true
    Write-Output "[FAIL] $message"
}

function Get-RelativePath([string]$BasePath, [string]$FullPath) {
    $base = [System.IO.Path]::GetFullPath($BasePath).TrimEnd('\', '/') + [System.IO.Path]::DirectorySeparatorChar
    $full = [System.IO.Path]::GetFullPath($FullPath)
    return $full.Substring($base.Length)
}

function Get-FileSha([string]$Path) {
    return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash
}

$skillName = "wms-operational-agent"
$sourceSkill = Join-Path $BrainRoot "brain/skills/$skillName"
$runtimeSkill = Join-Path (Join-Path $CodexHome "skills") $skillName
$wrongTomwmsSkill = Join-Path $TomwmsRoot "codex/skills/$skillName"

Write-Section "WMS Skill Doctor"
Write-Output "BrainRoot: $BrainRoot"
Write-Output "TomwmsRoot: $TomwmsRoot"
Write-Output "CodexHome: $CodexHome"

Write-Section "Canonical Location"
if (Test-Path $sourceSkill) {
    Ok "Canonical skill found in wms-brain: $sourceSkill"
} else {
    Fail "Canonical skill missing from wms-brain: $sourceSkill"
}

if (Test-Path $wrongTomwmsSkill) {
    Fail "Skill exists in TOMWMS, which is the wrong repo: $wrongTomwmsSkill"
} else {
    Ok "Skill folder is absent from TOMWMS."
}

Write-Section "Runtime Install"
if (Test-Path $runtimeSkill) {
    Ok "Runtime skill found: $runtimeSkill"
} else {
    Warn "Runtime skill not installed: $runtimeSkill"
}

if ((Test-Path $sourceSkill) -and (Test-Path $runtimeSkill)) {
    $sourceFiles = Get-ChildItem -LiteralPath $sourceSkill -Recurse -File
    $runtimeFiles = Get-ChildItem -LiteralPath $runtimeSkill -Recurse -File
    $sourceMap = @{}
    $runtimeMap = @{}

    foreach ($file in $sourceFiles) {
        $sourceMap[(Get-RelativePath $sourceSkill $file.FullName)] = Get-FileSha $file.FullName
    }
    foreach ($file in $runtimeFiles) {
        $runtimeMap[(Get-RelativePath $runtimeSkill $file.FullName)] = Get-FileSha $file.FullName
    }

    $allKeys = @($sourceMap.Keys + $runtimeMap.Keys | Sort-Object -Unique)
    $diffs = @()
    foreach ($key in $allKeys) {
        $src = if ($sourceMap.ContainsKey($key)) { $sourceMap[$key] } else { "<missing>" }
        $rt = if ($runtimeMap.ContainsKey($key)) { $runtimeMap[$key] } else { "<missing>" }
        if ($src -ne $rt) {
            $diffs += $key
        }
    }

    if ($diffs.Count -eq 0) {
        Ok "Runtime skill matches canonical source by SHA256."
    } else {
        Warn "Runtime skill differs from canonical source:"
        $diffs | ForEach-Object { Write-Output "  - $_" }
    }
}

Write-Section "Skill Structure"
$required = @(
    "SKILL.md",
    "agents/openai.yaml",
    "references/architecture.md",
    "references/brain-governance.md",
    "references/checklists.md",
    "references/health-and-performance.md",
    "references/paths.md",
    "scripts/install-skill.ps1",
    "scripts/validate-wms-skill.ps1",
    "scripts/wms-preflight.ps1",
    "scripts/wms-patch-check.ps1",
    "scripts/wms-skill-doctor.ps1",
    "scripts/wms-agent-maintenance.ps1",
    "scripts/wms-build-routing-index.ps1"
)

if (Test-Path $sourceSkill) {
    foreach ($rel in $required) {
        $path = Join-Path $sourceSkill $rel
        if (Test-Path $path) {
            Ok "Required file present: $rel"
        } else {
            Fail "Required file missing: $rel"
        }
    }
}

Write-Section "Legacy Bridge/OpenClaw References"
if (Test-Path $sourceSkill) {
    $files = Get-ChildItem -LiteralPath $sourceSkill -Recurse -File -Include *.md,*.ps1,*.yaml,*.yml |
        Where-Object { (Get-RelativePath $sourceSkill $_.FullName) -ne "scripts\wms-skill-doctor.ps1" }
    $activePatterns = @(
        "active send workflow",
        "create a directive",
        "send ACK",
        "send/receive loop is required",
        "OpenClaw as active",
        "bridge workflow when"
    )

    $hits = @()
    foreach ($pattern in $activePatterns) {
        $matches = $files | Select-String -Pattern $pattern -SimpleMatch -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Line -notmatch "(?i)\bdo not\b|\bno active\b|\bobsolete\b|\bhistorical\b|\bforensic\b|not as an active|not the current|not required"
            }
        if ($matches) {
            $hits += $matches
        }
    }

    if ($hits.Count -eq 0) {
        Ok "No active bridge/OpenClaw workflow language detected."
    } else {
        Warn "Possible obsolete active bridge/OpenClaw language detected:"
        $hits | ForEach-Object {
            $rel = Get-RelativePath $sourceSkill $_.Path
            Write-Output ("  - {0}:{1}: {2}" -f $rel, $_.LineNumber, $_.Line.Trim())
        }
    }
}

Write-Section "Git Hygiene"
if (Test-Path $TomwmsRoot) {
    $tomStatus = git -C $TomwmsRoot status --short -- "codex/skills/$skillName"
    if ($tomStatus) {
        Warn "TOMWMS still has git status for old skill path:"
        $tomStatus | ForEach-Object { Write-Output "  $_" }
    } else {
        Ok "TOMWMS has no git status under old skill path."
    }
}

if (Test-Path $BrainRoot) {
    $brainStatus = git -C $BrainRoot status --short -- "brain/skills/$skillName"
    if ($brainStatus) {
        Warn "wms-brain has uncommitted skill changes:"
        $brainStatus | ForEach-Object { Write-Output "  $_" }
    } else {
        Ok "wms-brain skill path is clean."
    }
}

Write-Section "Summary"
if ($script:HasFail) {
    Write-Output "Result: FAIL"
    exit 2
}

if ($script:HasWarn) {
    Write-Output "Result: WARN"
    exit 1
}

Write-Output "Result: OK"
