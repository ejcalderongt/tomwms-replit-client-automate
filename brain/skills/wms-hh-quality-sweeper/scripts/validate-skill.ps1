$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
  "SKILL.md",
  "references/safety-policy.md",
  "templates/hh-quality-report-template.yml",
  "scripts/wms-hh-qs-run.ps1",
  "scripts/wms-hh-qs-report.ps1",
  "scripts/wms-hh-qs-safe-fix.ps1",
  "scripts/wms-hh-qs-deps.ps1"
)
foreach ($f in $required) {
  if (-not (Test-Path (Join-Path $skillRoot $f))) { throw "Missing required file: $f" }
}
python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot

