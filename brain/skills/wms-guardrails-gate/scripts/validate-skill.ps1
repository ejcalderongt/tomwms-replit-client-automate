$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
  "SKILL.md",
  "references/gate-policy.md",
  "templates/gate-report-template.yml",
  "scripts/wms-gate-run.ps1",
  "scripts/wms-gate-evaluate.ps1"
)
foreach ($f in $required) {
  if (-not (Test-Path (Join-Path $skillRoot $f))) { throw "Missing required file: $f" }
}
python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot

