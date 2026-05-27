$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
    "SKILL.md",
    "agents/openai.yaml",
    "references/architecture.md",
    "references/checklists.md",
    "references/health-and-performance.md",
    "references/paths.md",
    "scripts/install-skill.ps1",
    "scripts/wms-preflight.ps1",
    "scripts/wms-patch-check.ps1",
    "scripts/wms-skill-doctor.ps1",
    "scripts/wms-agent-maintenance.ps1",
    "scripts/wms-build-routing-index.ps1"
)

foreach ($file in $required) {
    $path = Join-Path $skillRoot $file
    if (-not (Test-Path $path)) {
        throw "Missing required skill file: $file"
    }
}

python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot
