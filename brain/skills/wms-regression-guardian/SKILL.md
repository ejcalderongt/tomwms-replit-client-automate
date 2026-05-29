---
name: wms-regression-guardian
description: Regression prevention skill for TOMWMS. Use after a fix to build a risk footprint, find sibling risk zones, generate process checklists, and produce residual-risk reports before closing incidents.
---

# WMS Regression Guardian

## Purpose

Prevent "fix one, break another" by running a lightweight guardrail cycle:

1. Build risk footprint.
2. Scan sibling zones.
3. Generate process checklist.
4. Emit residual-risk report.

## Inputs

- Process: `picking`, `cambio_ubicacion`, `verificacion`, `recepcion`, `existencias`, `inventario`, `packing`.
- Fix scope: methods/files/tables/flags touched.
- Branch context: target + baseline branches.

## Outputs

- Guard artifacts under:
  `wms-brain/brain/handoffs/2026-05-27-regression-guardian/`
- Machine-readable footprint YAML.
- Sibling risk findings from repo scan.
- Process checklist.
- Residual-risk summary for closure.

## Scripts

- `scripts/wms-rg-run.ps1`
  Orchestrates full guard flow and writes consolidated report.
- `scripts/wms-rg-footprint.ps1`
  Generates fix footprint from touched files and optional refs.
- `scripts/wms-rg-sibling-scan.ps1`
  Finds similar risky patterns in sibling modules/files.
- `scripts/wms-rg-checklist.ps1`
  Generates process-specific regression checklist.
- `scripts/wms-rg-risk-report.ps1`
  Builds final residual-risk summary from artifacts.
- `scripts/install-skill.ps1`
  Installs skill into local Codex runtime.
- `scripts/validate-skill.ps1`
  Validates required files and structure.

## Pairing

- With `wms-root-cause-accelerator`: incident intake and causal mapping.
- With `wms-db-brain`: DB/table/flag impact cross-check.
- With `wms-operational-agent`: operational routing and closure discipline.

## Tags

Use durable tags `#EJCYYYYMMDD`.
