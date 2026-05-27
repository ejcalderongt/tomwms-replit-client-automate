$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
  "SKILL.md",
  "references/telemetry-schema.md",
  "templates/param-impact-template.yml",
  "scripts/wms-tl-run.ps1",
  "scripts/wms-tl-trace-map.ps1",
  "scripts/wms-tl-param-impact.ps1",
  "scripts/wms-tl-business-rule-atlas.ps1",
  "scripts/wms-tl-correlation.py"
)
foreach ($f in $required) {
  if (-not (Test-Path (Join-Path $skillRoot $f))) { throw "Missing required file: $f" }
}
python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot
