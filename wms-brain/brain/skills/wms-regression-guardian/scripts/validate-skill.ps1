$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
    "SKILL.md",
    "references/playbook.md",
    "templates/footprint-template.yml",
    "templates/residual-risk-template.yml",
    "scripts/wms-rg-run.ps1",
    "scripts/wms-rg-footprint.ps1",
    "scripts/wms-rg-sibling-scan.ps1",
    "scripts/wms-rg-checklist.ps1",
    "scripts/wms-rg-risk-report.ps1"
)

foreach ($file in $required) {
    if (-not (Test-Path (Join-Path $skillRoot $file))) {
        throw "Missing required file: $file"
    }
}

python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot
