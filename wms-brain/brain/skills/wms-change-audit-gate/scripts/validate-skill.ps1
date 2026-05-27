$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
  "SKILL.md",
  "references/policy.md",
  "templates/change-audit-report-template.yml",
  "scripts/wms-cag-run.ps1",
  "scripts/wms-cag-null-safety.ps1",
  "scripts/wms-cag-business-rules.ps1",
  "scripts/wms-cag-db-correlation.ps1",
  "scripts/wms-cag-evaluate.ps1"
)
foreach ($f in $required) {
  if (-not (Test-Path (Join-Path $skillRoot $f))) { throw "Missing required file: $f" }
}
python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot

