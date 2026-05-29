$ErrorActionPreference = "Stop"

$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
    "SKILL.md",
    "references/workflow.md",
    "scripts/wms-rca-run.ps1",
    "scripts/wms-rca-drift.ps1",
    "scripts/wms-rca-null-scan.ps1",
    "scripts/wms-rca-postfix-verify.ps1",
    "templates/rca-report-template.yml"
)

foreach ($file in $required) {
    $path = Join-Path $skillRoot $file
    if (-not (Test-Path $path)) {
        throw "Missing required file: $file"
    }
}

python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot
