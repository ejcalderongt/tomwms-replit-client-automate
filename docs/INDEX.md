# wms-brain-client — Índice de la rama

> Rama orphan dedicada al **módulo PowerShell `WmsBrainClient`**. Sienta
> al cliente operativo encima del ecosistema brain existente
> (`brain_bridge.mjs` + `apply_bundle.mjs` + `hello_sync.mjs`) sin
> reinventarlo.

---

## Cómo leer esta rama

Orden recomendado (cada uno depende del anterior, top-down):

| # | Archivo                          | Para qué |
|---|----------------------------------|----------|
| 1 | [`README.md`](./README.md)       | Onboarding amigable: qué es, por qué existe, banner ACME, TL;DR. |
| 2 | [`SPEC.md`](./SPEC.md)           | Especificación: identidad, dependencias hard del ecosistema, perfiles SQL, inventario de cmdlets, exit codes, roadmap. |
| 3 | [`PROTOCOL.md`](./PROTOCOL.md)   | **Crítico**. Cómo el cliente se enchufa al `brain_bridge.mjs` real (`SCHEMA_VERSION="1"`). Política, eventos, paths, errores. |
| 4 | [`CMDLETS.md`](./CMDLETS.md)     | Catálogo de cmdlets PowerShell con firma, comportamiento y delegación a los `.mjs`. |
| 5 | [`EXTENSION-V2-PROPOSAL.md`](./EXTENSION-V2-PROPOSAL.md) | Propuesta formal de bump a `SCHEMA_VERSION="2"` con 3 tipos de evento nuevos. |
| 6 | [`ALIASES.md`](./ALIASES.md)     | Codenames de clientes (K7, BB, C9, ID, MH, MC, ...). Política anti-leak. |
| 7 | [`PROMPT-OPENCLAW.md`](./PROMPT-OPENCLAW.md) | Prompt listo para Open Claw / Claude Desktop / Claude Code que implementa el módulo. |

Material de soporte:

| Carpeta              | Contiene                                                              |
|----------------------|-----------------------------------------------------------------------|
| `examples/`          | 4 `brain_event_*.json` canónicos (1 por type relevante).              |
| `templates/`         | Templates de answer card y learning card.                             |
| `questions/`         | 8 question cards Q-001..Q-008 + `MIGRATION-NOTE.md`.                  |

---

## Ramas hermanas del repo de exchange

Este repo (`ejcalderongt/tomwms-replit-client-automate`) usa el patrón
**multi-cabeza con orphan branches**: ramas independientes que comparten
un mismo origin pero **nunca se mergean entre sí**.

| Rama                  | Contenido                                                    |
|-----------------------|--------------------------------------------------------------|
| `main`                | Scripts `.mjs` operativos (`brain_bridge`, `apply_bundle`, `hello_sync`, `brain-up.ps1`) + `entregables_ajuste/<fecha>/v*_bundle/`. |
| `openclaw-control-ui` | Control UI del consumidor openclaw (sesión Replit anterior). |
| `wms-brain`           | Doctrina del cerebro: `BRIDGE.md`, `agent-context/`, suites/, scenarios/, `_inbox/`, `_proposals/`, `_processed/`. |
| `wms-brain-client`    | **Esta rama**. Módulo PowerShell + spec.                     |
| `wms-db-brain`        | Catálogo SQL Killios (snapshots, índices).                   |

---

## Contrato versionado

| Componente             | Versión esperada (este ciclo)              |
|------------------------|---------------------------------------------|
| `WmsBrainClient`       | `0.2.0`                                     |
| `brain_bridge.mjs`     | `SCHEMA_VERSION="1"` (eventos schema 1)     |
| `apply_bundle.mjs`     | con soporte `--brain-message`               |
| `hello_sync.mjs`       | cualquier (≥ commit inicial)                |
| Doctrina `wms-brain`   | `BRIDGE.md` v0 (heurística simple, no LLM)  |

Si `Test-WmsBrainEnvironment` reporta mismatch en `SCHEMA_VERSION`, el
cliente refusa emitir eventos y avisa para upgrade coordinado.

---

## Cambios importantes vs primera ciclo (0.1.0)

1. **Bridge real reconocido**: ya no se asume "no hay bridge"; se delega.
2. **Schema unificado**: adoptado `SCHEMA_VERSION="1"` del bridge.
3. **Tipos extendidos como propuesta formal v2**, no como invento paralelo.
4. **Workaround `directive`+`tags`** para Q-NNN mientras v2 no se acepte.
5. **Cmdlets con prefijo `Invoke-`** para los que delegan a `.mjs`.
6. **Suites y scenarios viven en `wms-brain`**, no acá.

---

## Disclaimer

Las 3 BDs SQL Server productivas (K7-PRD, BB-PRD, C9-QAS) en
`52.41.114.122,1437` son **READ-ONLY estricto** desde este cliente.
Cualquier intento de DML genera excepción local antes de mandar al
server (exit code 7). Las cards y eventos **nunca** logean
`$env:WMS_KILLIOS_DB_PASSWORD` ni `$env:BRAIN_IMPORT_TOKEN`.
