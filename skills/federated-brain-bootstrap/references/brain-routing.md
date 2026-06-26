# Brain Routing

This reference exists to keep routing deterministic.

## Roots

- `brain/` for shared federated maps
- `wms-brain/` for WMS operational context
- `mpos-brain/` for mPos / MCP context
- `brain/federated-index.yml` for the canonical route manifest
- `scripts/bootstrap-session.ps1` for the executable bootstrap entry point

## Default Read Order

1. Root anchors: `AGENTS.md`, `SOUL.md`, `USER.md`, `TOOLS.md`, `MEMORY.md`
2. Federated route manifest: `brain/federated-index.yml`
3. Current daily note: `memory/YYYY-MM-DD.md`
4. Project brain README
5. Task-specific YAML files
6. Trace files, scripts, and notes

## WMS Implosion Set

- `brain/atlas/implosion-mapa.yml`
- `brain/wms/recepcion/implosion/implosion-flujo.yml`
- `brain/wms/recepcion/implosion/implosion-trazas.yml`

## LP Set

- `brain/wms/recepcion/lp-flujo-analisis.yml`
- `brain/wms/recepcion/lp-endpoints.yml`
- `brain/wms/recepcion/db-probes.yml`
