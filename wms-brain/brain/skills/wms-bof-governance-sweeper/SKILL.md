---
name: wms-bof-governance-sweeper
description: "BOF governance sweeper for TOMWMS_BOF (VB.NET): detects unused-code candidates, duplicated logic/query fragments, possible business-rule conflicts, roundtrip-heavy hotspots, and SQL heavy-query candidates; emits executive-ready findings."
---

# WMS BOF Governance Sweeper

## Purpose

Give BOF teams a repeatable, low-cost audit pass to find structural risks and optimization opportunities before they become incidents.

## Modes

- `report-only` (default): read-only audit and findings.
- `safe-fix`: reserved for future mechanical cleanups (currently reports only).

## Coverage

1. Unused private method candidates.
2. Duplicated SQL/query fragment candidates.
3. Heavy-query candidates (`SELECT *`, nested subqueries, wildcards, missing filters).
4. Roundtrip hotspots (high concentration of DAL/web-method style calls).
5. Business-rule risk candidates (hard-coded states/flags, parameter-gated branches).

## Scripts

- `scripts/wms-bof-gs-run.ps1`
- `scripts/wms-bof-gs-scan.ps1`
- `scripts/install-skill.ps1`
- `scripts/validate-skill.ps1`

## Output

Artifacts under:
`wms-brain/brain/handoffs/2026-05-27-bof-governance-sweeper/`

## Tags

Use `#EJCYYYYMMDD`.
