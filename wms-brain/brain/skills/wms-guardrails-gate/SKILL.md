---
name: wms-guardrails-gate
description: Pre-close quality gate that orchestrates RCA, regression guard, state-machine checks, and telemetry-lite outputs. Use to prevent high-risk fixes from closing without evidence.
---

# WMS Guardrails Gate

## Purpose

Execute consistent pre-close checks and produce a go/no-go decision with evidence links.

## Scripts

- `scripts/wms-gate-run.ps1`
- `scripts/wms-gate-evaluate.ps1`
- `scripts/install-skill.ps1`
- `scripts/validate-skill.ps1`

