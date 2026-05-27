---
name: wms-change-audit-gate
description: Automated incoming-change audit gate for WMS. Use to validate null safety, business-rule coverage, state safety, and DB correlation from changed files/diffs when manual code review is missing.
---

# WMS Change Audit Gate

## Purpose

Act as mandatory automated reviewer for incoming changes with `PASS/WARN/FAIL`.

## Validation Axes

1. Null safety and guard coverage.
2. Global business-rule coverage.
3. State-machine safety.
4. DB correlation (table/SP/column impact).
5. Cross-layer consistency hints (BOF/HH/WS/SQL).

## Scripts

- `scripts/wms-cag-run.ps1`
- `scripts/wms-cag-null-safety.ps1`
- `scripts/wms-cag-business-rules.ps1`
- `scripts/wms-cag-db-correlation.ps1`
- `scripts/wms-cag-evaluate.ps1`
- `scripts/install-skill.ps1`
- `scripts/validate-skill.ps1`

## Output

Artifacts under:
`wms-brain/brain/handoffs/2026-05-27-change-audit-gate/`

## Tags

Use `#EJCYYYYMMDD`.

