# wms-brain

Workspace anchor for WMS work.

## Purpose

- Keep the durable operational context for WMS in one place.
- Avoid re-explaining project routing, repo paths, and brain choices on every session.
- Collect the shared artifacts that support WMS work: notes, agents, configs, ymls, scripts, and runbooks.

## Current Scope

- Active repo: `C:\Users\yejc2\source\repos\TOMWMS`
- Primary work mode: WMS operational brain
- Secondary work mode: DB brain for SQL tuning, plans, and index work
- DBA repo for shared SQL assets: `C:\Users\yejc2\source\repos\DBA`

## Canonical Memory

- Long-term memory: `MEMORY.md`
- Daily notes: `memory/YYYY-MM-DD.md`
- Workspace bootstrap: this file

## Suggested Layout

- `agents/` for reusable agent instructions or local agent notes
- `configs/` for YAML, JSON, or environment files
- `docs/` for runbooks and operational notes
- `scripts/` for helper scripts used across WMS work
- `projects/` for references to linked repos or subprojects

## Startup Rule

Read this file first, then `MEMORY.md`, then the current daily note when resuming WMS work.

Primary bootstrap entry point:
- `wms-brain/bootstrap-session.ps1` for WMS sessions
- `scripts/bootstrap-session.ps1` for the federated root bootstrap

When a new machine or environment is created, restore the repo and run the bootstrap from this anchor before touching task files.

## KPI Tuning

- `picking` and `despacho` were tuned together because both hit `trans_picking_ubic` heavily.
- The DBA repo now carries the KPI index script in `20260618_kpi_performance_indexes.sql`.
- The handoff note for this pass lives in `C:\Users\yejc2\source\repos\DBA\brain\handoffs\20260618_wms_kpi_performance.md`.
