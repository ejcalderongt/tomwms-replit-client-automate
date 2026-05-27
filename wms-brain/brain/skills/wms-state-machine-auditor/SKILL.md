---
name: wms-state-machine-auditor
description: Canonical state transition auditor for WMS processes. Use to extract, validate, and compare state-machine rules across BOF/HH/WS/SQL for recepcion, picking, packing, verificacion, inventario, existencias, cambio_ubicacion, and cambio_estado.
---

# WMS State Machine Auditor

## Purpose

Build and enforce canonical state transitions so UI, services, and DB behave consistently.

## Core Outputs

- State matrix by process.
- State gating hotspots in code.
- DB snapshot for observed states.
- Drift summary between expected and observed transitions.

## Scripts

- `scripts/wms-sma-run.ps1`
- `scripts/wms-sma-extract.ps1`
- `scripts/wms-sma-matrix.ps1`
- `scripts/wms-sma-db-snapshot.py`
- `scripts/install-skill.ps1`
- `scripts/validate-skill.ps1`

## Tags

Use `#EJCYYYYMMDD`.

