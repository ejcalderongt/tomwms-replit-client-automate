---
name: wms-db-brain
description: Local SQL catalog skill for TOMWMS/Killios database analysis. Use when the task asks about SQL Server schema, tables, views, SPs, functions, columns, flags, parametrizacion, row counts, DB impact, trans_movimientos, stock, picking, reservas, verification, replacement, or when mining the wms-db-brain Git branch as a reusable data-brain for the WMS agent.
---

# WMS DB Brain

## Core Rule

Use `origin/wms-db-brain` as the raw SQL snapshot source and the local `wms-brain` branch as the interpretation/governance layer.

Do not merge `wms-db-brain` directly into `wms-brain`: the branches are orphan-style and have different roots. Import selectively through scripts or read objects directly with `git show`.

## Source Of Truth

- Snapshot branch: `origin/wms-db-brain`
- Snapshot root in that branch: `db-brain/`
- Current snapshot: Killios PRD, extracted 2026-04-27
- Catalog size: 621 SQL objects
- Object groups: `tables/`, `views/`, `sps/`, `functions/`, `parametrizacion/`, `_meta/`

## DBA Script Repository Rule

`#EJC20260615` Schema scripts, ALTERs, CREATE TYPE, CREATE OR ALTER PROCEDURE,
views, rollback scripts, and migration files do not belong in TOMWMS_BOF or
temporary GitHub mirrors. They must be routed to the canonical DBA repo:

- Local repo: `C:/Users/yejc2/source/repos/DBA`
- GitHub repo: `https://github.com/ejcalderongt/DBA`
- Project overlay: `C:/Users/yejc2/source/repos/DBA/brain/project-overlay.yml`

Before committing or pushing any database artifact, run:

```powershell
scripts/wms-db-brain-dba-route-check.ps1
```

Use `wms-db-brain` for catalog lookup and impact context; use `DBA` for the
actual versioned scripts.

## Safety

- Production DB is read-only from this agent.
- Prefer the markdown snapshot before querying production.
- Never expose full `module.definition` bodies in final answers unless Erik explicitly asks for local internal review.
- Treat `parametrizacion/` as curated operational knowledge.
- Tag new durable notes with `#EJCYYYYMMDD`.

## Workflow

1. Search the snapshot first:
   - `scripts/wms-db-brain-find.ps1 -Query trans_movimientos`
   - `scripts/wms-db-brain-show.ps1 -Object trans_movimientos`
2. Load only the object markdown needed for the task.
3. Cross-check operational flow in `wms-operational-agent` when DB behavior affects HH/BOF/WS.
4. If a schema change is confirmed, regenerate/import the DB snapshot through the extractor workflow, not by hand-editing generated object files.
5. If a reusable rule is discovered, document it in `wms-brain` domain files or traces and link with `db-brain://...`.

## High-Value Entry Points

- `db-brain/_meta/stats.md`: object counts, top row-count tables, persistent findings.
- `db-brain/parametrizacion/README.md`: multi-client flag model.
- `db-brain/parametrizacion/flags-bodega.md`: bodega flags, including replacement and HH behavior toggles.
- `db-brain/parametrizacion/flags-producto.md`: product behavior flags.
- `db-brain/parametrizacion/matriz-killios.md`: Killios active flag matrix.
- `db-brain/tables/trans_movimientos.md`: movement persistence.
- `db-brain/tables/stock.md` and `db-brain/tables/stock_res.md`: stock and reservation state.
- `db-brain/tables/trans_picking_ubic.md`: picking by location.

## Scripts

- `scripts/wms-db-brain-inventory.ps1`: create/update a compact local manifest from `origin/wms-db-brain`.
- `scripts/wms-db-brain-find.ps1`: search object paths and optional content in the snapshot branch.
- `scripts/wms-db-brain-show.ps1`: show one object or db-brain URI from the snapshot branch.
- `scripts/wms-db-brain-materialize.ps1`: optional full local copy into `brain/db-brain/`.
- `scripts/wms-db-brain-dba-route-check.ps1`: verify the canonical DBA repo,
  remote, branch, and working-tree state before publishing SQL artifacts.

Run scripts from `C:/Users/yejc2/source/repos/wms-brain` unless a script says otherwise.
