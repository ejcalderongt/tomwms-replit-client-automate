$ErrorActionPreference = "Stop"
$skillRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$required = @(
  "SKILL.md",
  "references/state-playbook.md",
  "templates/state-matrix-template.yml",
  "scripts/wms-sma-run.ps1",
  "scripts/wms-sma-extract.ps1",
  "scripts/wms-sma-matrix.ps1",
  "scripts/wms-sma-db-snapshot.py"
)
foreach ($f in $required) {
  if (-not (Test-Path (Join-Path $skillRoot $f))) { throw "Missing required file: $f" }
}
python C:/Users/yejc2/.codex/skills/.system/skill-creator/scripts/quick_validate.py $skillRoot

