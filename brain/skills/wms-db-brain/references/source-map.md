---
tipo: other
clientes: [killios]
autores: [erik]
---
# wms-db-brain Source Map

`#EJC20260527` This skill maps the historical `wms-db-brain` branch into the local Codex/Brain workflow without reviving bridge/OpenClaw mechanics.

## Branch Roles

| Branch | Role | Use |
|---|---|---|
| `wms-brain` | Operational doctrine, skills, traces, governance | Active Codex agent memory |
| `wms-db-brain` | Raw SQL Server snapshot for Killios PRD | Local catalog and parametrizacion lookup |
| `wms-brain-client` | Historical client-agent experiment | Mine ideas only |
| `openclaw-control-ui` | Historical bootstrap/control UI | Obsolete as active workflow |

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
