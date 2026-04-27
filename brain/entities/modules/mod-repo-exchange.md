---
id: mod-repo-exchange
type: module
title: Repo de intercambio tomwms-replit-client-automate
status: estable
sources:
  - skill: wms-tomwms §2
  - github: ejcalderongt/tomwms-replit-client-automate
  - validated_at: 2026-04-27
---

# tomwms-replit-client-automate — repo de intercambio

| Atributo | Valor |
|---|---|
| Hosting | GitHub `ejcalderongt` |
| Lenguajes | TypeScript, Node, Python, markdown |
| Token | secret `GITHUB_TOKEN` |
| Branches | `main`, `wms-brain`, `wms-db-brain` (nuevo desde 2026-04-27) |

## Rol por branch

### `main` — bundles + scripts del productor
- Bundles `.zip` con patches firmados que el consumidor aplica al WMS.
- Scripts: `scripts/hello_sync.mjs`, `scripts/brain_bridge.mjs`.
- Skill productor de bundles (si existe).
- `entregables_ajuste/AGENTS.md` — contrato de bundles.

### `wms-brain` — conocimiento estructurado
- `brain/entities/{cases,modules,rules,decisions}/` — entities markdown.
- `brain/skills/{wms-tomwms,wms-tomhh2025}/` — skills canónicos.
- `brain/agent-context/` — convenciones del agente.
- `brain/wms-agent/` — CLI Python `wmsa`.
- `brain/sql-catalog/` — extractor del catálogo SQL → Brain API REST.

### `wms-db-brain` — catálogo BD + parametrización (NUEVO)
- `db-brain/{tables,views,sps,functions}/` — entity por objeto SQL.
- `db-brain/parametrizacion/` — flags por cliente + matrices comparativas.
- `db-brain/dependencias/` — grafo cross-objeto.
- Cross-link con entities de `wms-brain` por convención `db-brain://...`.

## Reglas críticas

- El repo de intercambio **NO contiene código WMS**. Solo bundles + scripts + brain.
- Push autorizado desde Replit con `GITHUB_TOKEN` (workaround documentado: scripts Node con `spawnSync git` en `/tmp` por restricción de operaciones git destructivas en el agente principal).

## Cross-refs
- `modules/mod-arquitectura-solution`
- `decisions/dec-formato-commits`
- `db-brain://README`
