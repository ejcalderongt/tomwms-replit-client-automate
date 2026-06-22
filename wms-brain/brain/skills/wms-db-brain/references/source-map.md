# wms-db-brain Source Map

`#EJC20260527` This skill maps the historical `wms-db-brain` branch into the local Codex/Brain workflow without reviving bridge/OpenClaw mechanics.

## Branch Roles

| Branch | Role | Use |
|---|---|---|
| `wms-brain` | Operational doctrine, skills, traces, governance | Active Codex agent memory |
| `wms-db-brain` | Raw SQL Server snapshot for Killios PRD | Local catalog and parametrizacion lookup |
| `wms-brain-client` | Historical client-agent experiment | Mine ideas only |
| `openclaw-control-ui` | Historical bootstrap/control UI | Obsolete as active workflow |

## Canonical DBA Script Repo

`#EJC20260615` The GitHub repo for versioned DBA scripts is not the
`wms-db-brain` snapshot branch. It is:

| Repo | Role | Rule |
|---|---|---|
| `ejcalderongt/DBA` | Versioned DDL, ALTERs, SPs, TVPs, views, migrations and rollback scripts | Commit/push DB scripts here |
| `origin/wms-db-brain` | Generated markdown snapshot of an extracted database catalog | Read/search only; refresh through extractor |
| TOMWMS_BOF / temporary mirrors | Application code or temporary sync mirrors | Do not publish DBA artifacts here |

Local route:

```yaml
dba_repo_local: C:/Users/yejc2/source/repos/DBA
dba_repo_github: https://github.com/ejcalderongt/DBA
dba_overlay: C:/Users/yejc2/source/repos/DBA/brain/project-overlay.yml
pre_publish_check: brain/skills/wms-db-brain/scripts/wms-db-brain-dba-route-check.ps1
```

## Why Not Merge Directly

`wms-db-brain` is a separate snapshot branch with its own root. A normal merge would mix generated DB markdown with operational doctrine and create noisy diffs. The recommended path is:

1. Read by `git show origin/wms-db-brain:db-brain/...`.
2. Generate compact indexes in `wms-brain/brain/_index/`.
3. Materialize full `db-brain/` locally only when offline search is worth the size.

## URI Convention

Use `db-brain://` links in Brain notes:

```yaml
db_brain_refs:
  - db-brain://tables/trans_movimientos
  - db-brain://parametrizacion/flags-bodega#reemplazo_opcional
```

The scripts resolve those URIs to `origin/wms-db-brain:db-brain/<path>.md`.
