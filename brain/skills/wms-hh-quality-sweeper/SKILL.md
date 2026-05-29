---
name: wms-hh-quality-sweeper
description: Continuous Android HH quality sweeper for IDE problems/warnings, dependency compatibility checks, visual hygiene checks, and safe code cleanup with report-only and safe-fix modes.
---

# WMS HH Quality Sweeper

## Purpose

Improve TOMHH2025 gradually and safely using a repeatable quality loop aligned with operational stability.

## Modes

- `report-only` (default): detect and prioritize warnings without code edits.
- `safe-fix`: apply low-risk mechanical fixes only.

## Coverage

1. IDE-like code smells (`field can be local`, unused private fields, null-check anti-patterns).
2. Null safety normalization (`TextUtils.isEmpty`, guard consistency).
3. Dependency compatibility snapshot (`gradle`, `AGP`, `androidx` markers).
4. Visual hygiene hints (hardcoded text, focus handling patterns).
5. Cleanup candidates (unused fields/imports).

## Scripts

- `scripts/wms-hh-qs-run.ps1`
- `scripts/wms-hh-qs-report.ps1`
- `scripts/wms-hh-qs-safe-fix.ps1`
- `scripts/wms-hh-qs-deps.ps1`
- `scripts/install-skill.ps1`
- `scripts/validate-skill.ps1`

## Output

Artifacts under:
`wms-brain/brain/handoffs/2026-05-27-hh-quality-sweeper/`

## Tags

Use `#EJCYYYYMMDD`.

