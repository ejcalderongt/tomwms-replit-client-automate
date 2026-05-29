---
id: AGENTS_README
tipo: agent-context
estado: vigente
ramas: [wms-brain]
tags: [agents, coordinator, contextos-especialidad]
fecha_creacion: 2026-05-20
authored_by: Replit Agent (PrograX24)
ratificado_por: Erik Calderon
---

# `brain/agents/` — Coordinador + paquetes de contexto por dominio

## 1. Para que existe esta carpeta

Cuando este brain tenia 30 archivos, cualquier agente cargaba `_index/INDEX.md`
+ `replit.md` + 2 patterns y trabajaba con todo en RAM. Hoy el brain tiene
~330 archivos. Cargar todo es caro, lento, y satura el contexto del LLM con
informacion que no aplica a la tarea.

Esta carpeta resuelve eso con **un modelo coordinador + paquetes de
especialidad**:

- **1 coordinador** (este agente) que recibe la tarea, decide que paquetes
  cargar, y solo lee esos paquetes.
- **N paquetes de contexto** (`domain-*.yml`) que describen un dominio
  tecnico cerrado: archivos clave, patterns, tablas BD, ramas, glosario,
  cross-refs y entry-points para tareas tipicas.
- **8 paquetes de cliente** (ya existian en `brain/client-index/*.yml`):
  flags, deuda, particularidades por instancia productiva.

El coordinador NO inlinea el contenido de los paquetes en sus YAMLs. Solo
referencia los `.md` profundos via path relativo. Asi un cambio se hace en
un solo lugar (el pattern o el handoff fuente) y se propaga al cargarlo.

## 2. Ventaja sobre 1 agente por dominio con runtime propio

Tener 7 agentes con LLMs separados parece atractivo pero:

1. Las tareas reales son cross-dominio (caso BYB EA-153305 toca reserva +
   BD + WebAPI + BOF + indicadores simultaneamente). Con N runtimes hay
   que orquestar y reconciliar respuestas N veces.
2. Patterns como `PATTERNS-RESERVA-MI3-UMBAS.md` interesan a 5 dominios.
   Si vive en 5 paquetes hay 5 lugares que mantener sincronizados.
3. La latencia y costo de un agente que no entiende fuera de su silo y
   pide aclaracion 4 veces es peor que 1 agente bien dirigido.

Por eso el modelo es: **1 LLM + N paquetes de contexto curados** cargados
selectivamente. Los paquetes se pueden migrar a runtimes separados en el
futuro si se prueba el ahorro real.

## 3. Como se usa

### 3.1 Agente coordinador (Replit Agent en este replit)

1. Lee la tarea.
2. Abre `brain/agents/_index.yml` y matchea contra `triggers`.
3. Carga `coordinator.yml` + el o los `domain-*.yml` que matcheen + el o
   los `client-*.yml` (de `brain/client-index/`) si la tarea es por cliente.
4. Sigue los `entry_points` del `domain-*.yml` para saber por que archivo
   `.md` profundo arrancar.

Tipicamente: 1 coordinator + 1-2 domains + 1 client = 3-4 archivos cortos
en vez de explorar el brain entero.

### 3.2 Agente externo (Mary Jane / Codex / Claude / Cursor)

Mismo flujo, pero el agente lee los YAMLs via URL raw de GitHub
(branch `wms-brain`) o desde un clone local. Ver `handoffs/2026-05-20-
inverso-mary-jane-arquitectura-agents-replicacion-local/PROPOSAL.md` para
la replica local en `C:\Users\yejc2\source\repos\TOMWMS_BOF\AGENTS.md`.

## 4. Schema de un `domain-*.yml`

```yaml
slug: domain-<nombre>
nombre: <legible>
proposito: <una linea>
estado: <vigente | en-construccion | deprecated>
ramas_relevantes: [dev_2023_estable, dev_2028_merge, ...]
archivos_clave:
  <subgrupo>:
    - path: <ruta relativa al repo TOMWMS_BOF o TOMHH2025>
      rol: <que hace>
      funcion_principal: { nombre: <fn>, lineas: [<from>, <to>] }
patrones_brain:
  - <path .md relativo a brain/>
tablas_bd: [<lista>]
glosario:
  <termino>: <definicion>
huerfanos_evitar:
  - { path: <huerfano>, motivo: <por que no usar> }
cross_refs:
  - <slug otro domain>
entry_points:
  <slug_tarea>: <que leer primero>
ejemplos_tareas_pasadas:
  - { caso: <id>, handoff: <path>, dominios: [<lista>] }
extension_points:
  - <que falta por documentar>
```

## 5. Mapa de dominios (estado al 2026-05-20)

| slug | dominio | nro de patterns | estado |
|---|---|---|---|
| coordinator | Coordinador central | n/a | vigente |
| domain-bof | Backend VB.NET TOMIMSV4 | 3 | vigente |
| domain-integration-services | WSHHRN + WMSWebAPI + MI3 | 2 | vigente |
| domain-hh-android | Handheld Android Java | 2 | vigente |
| domain-database | Schema, SPs, views, perfiles | 1 | en-construccion |
| domain-reserva | Reserva stock MI3 y motor Core | 3 | vigente |
| domain-portal | WikiHub Portal + endpoints | 0 | vigente |
| domain-indicadores | KPIs operativos cross-dominio | 0 | en-construccion |

## 6. Que NO va aca

- **Logica del WMS**: vive en TOMWMS_BOF / TOMHH2025 en Azure DevOps.
- **Patterns operativos**: viven en `brain/code-changes/{BOF,HH}/PATTERNS-*.md`.
- **Handoffs**: viven en `brain/handoffs/<fecha>-<slug>/`.
- **Snapshots BD**: viven en `brain/data-deep-dive/<cliente>/`.
- **Catalogos por cliente**: viven en `brain/client-index/*.yml`.

Estos YAMLs son **indice + entry-points**, NO contenido. Si te encontras
inlinenando un pattern aca, parate y referencialo en lugar de copiarlo.

## 7. Mantenimiento

- Cada handoff resuelto debe agregar 1 entrada en `ejemplos_tareas_pasadas`
  del `domain-*.yml` que aplique.
- Cada pattern nuevo en `brain/code-changes/` debe agregarse a
  `patrones_brain` del o los domains que apliquen.
- Cada cambio en `_index.yml` (agregar dominio, mover trigger) requiere
  actualizar este README seccion 5.
- Periodicidad: revisar drift entre YAMLs y carpetas reales cada ~30 dias
  (script `tools/agents/check-drift.py` pendiente de crear).
