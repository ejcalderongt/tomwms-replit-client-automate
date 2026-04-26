# wms-brain

Rama del repo de intercambio que contiene el **brain del agente Replit** sobre el WMS.

## Que vive aca

Conocimiento estructurado y herramientas que el agente productor (Replit) usa para trabajar en TOMWMS:

- `replit.md` — indice maestro: producto, repos Azure DevOps, equipo, reglas vinculantes, acceso SQL Server, mapa al WikiHub Portal.
- `agent-context/` — protocolo operativo del agente: `AGENTS.md` y `CASE_INTAKE_TEMPLATE.md`.
- `sql-catalog/` — extractor del catalogo de BD del WMS (`extract.sql` + `extract_sql_catalog.py`).
- `wms-agent/` — herramienta Python `wmsa` (CLI del agente).
- `tasks-historicas/` — planes de tasks ejecutadas que sirven como referencia.

## Que NO vive aca (por contrato)

Siguiendo la regla establecida en `entregables_ajuste/AGENTS.md` (rama main):

- **Codigo fuente del WMS** (`*.vb`, `*.Designer.vb`, `.resx`, etc.): vive en Azure DevOps repo `TOMWMS_BOF`. No se duplica aca.
- **Bundles operativos** (`entregables_ajuste/<fecha>/vNN_bundle/`): viven en `main`.
- **Datos productivos crudos** (`*.xlsx` con datos reales, capturas de UI): no son brain estructurado, son input.
- **Secrets** de cualquier tipo (PAT, passwords, tokens): jamas. Solo se referencian por nombre.

## Relacion con otras fuentes de conocimiento

| Fuente | Rol | Acceso |
|---|---|---|
| **Esta rama (`wms-brain`)** | Brain estructurado del agente, versionado | git |
| **WikiHub Portal** `https://tomwms-wikidev.replit.app` | Wiki humana profunda (Jira, releases, BD, modulos) | API REST |
| **Azure DevOps** | Codigo fuente WMS y HH | PAT |
| **SQL Server EC2** | Datos productivos | password en secret |
| **Workspace Replit** del agente | Working copy efimera + entorno productor | sesion del agente |

## Politica de actualizacion

- Esta rama se actualiza explicitamente cuando hay un cambio significativo en `replit.md`, en los protocolos de `agent-context/`, o en las herramientas (`sql-catalog`, `wms-agent`).
- No se mergea a `main` ni `main` se mergea aca. Son canales paralelos con propositos distintos.
- El consumidor (openclaw) puede leer esta rama si necesita entender contexto, pero **no la necesita para aplicar bundles**. Para aplicar bundles alcanza con `main` (AGENTS.md + scripts + bundle vNN).

## Inconsistencia conocida

El `replit.md` referencia el skill local `.local/skills/wms-tomwms/SKILL.md` con playbooks (arquitectura, protocolo HH<->WS, convenciones JSON, modelo de datos). **Ese archivo no existe en disco actualmente.** Es deuda tecnica del brain — pendiente de reconstruir.
