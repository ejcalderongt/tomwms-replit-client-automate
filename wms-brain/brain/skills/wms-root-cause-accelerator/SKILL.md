---
name: wms-root-cause-accelerator
description: Fast root-cause acceleration for TOMWMS operations. Use for HH/BOF incidents when we need trace-to-path mapping, branch drift detection, null/empty/list guard scanning, and post-fix verification evidence for picking, recepcion, packing, verificacion, existencias, inventario, and cambio_ubicacion.
---

# WMS Root Cause Accelerator

## Purpose

Deliver an `X+` in diagnosis speed and fix quality by converting incident symptoms into a repeatable path:

1. trace2path
2. branch-drift-guard
3. null-risk-scanner
4. post-fix-verifier

## Inputs

- Process: `picking`, `cambio_ubicacion`, `verificacion`, `recepcion`, or `existencias`.
- Symptom: user-visible error or unexpected behavior.
- Optional refs: suspect methods, tables, SPs, flags, or branches.

## Outputs

- RCA YAML report under:
  `wms-brain/brain/handoffs/2026-05-27-root-cause-accelerator/`
- Candidate path `HH -> WS -> DAL -> SQL`.
- Risk findings for null/empty/list failures.
- Branch-drift evidence vs baseline branches.
- Post-fix checklist and validation status.

## Scripts

- `scripts/wms-rca-run.ps1`
  Generates a per-incident report with path, findings, and verification slots.
- `scripts/wms-rca-drift.ps1`
  Compares code snippets/patterns across branches and stores drift evidence.
- `scripts/wms-rca-null-scan.ps1`
  Scans Java/VB for null/empty/list guard hotspots.
- `scripts/wms-rca-postfix-verify.ps1`
  Generates a test checklist tied to the process and incident.
- `scripts/db_diag_picking_mampa.py`
  DB-side incident snapshot for MAMPA picking (`enc`, `ubic`, and verification counts).
- `scripts/install-skill.ps1`
  Installs this skill into local Codex runtime.
- `scripts/validate-skill.ps1`
  Runs structural validation.

## Required Pairing

- Use together with `wms-operational-agent` for process routing.
- Use `wms-db-brain` for SQL/table/flag correlation.

## Tags

Always tag durable notes with `#EJCYYYYMMDD`.
