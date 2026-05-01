---
id: 20260428-1901-H02-clbd-prc-falta-reabasto-log
tipo: proposal
estado: vigente
titulo: Proposal 20260428-1901-H02-clbd-prc-falta-reabasto-log
tags: [proposal]
---

# Proposal 20260428-1901-H02-clbd-prc-falta-reabasto-log

**Status**: proposed
**Generado**: 2026-04-28T19:45:00-03:00
**Generado por**: agente brain (sesion replit)

## Hallazgo origen

Ver `brain/_inbox/20260428-1901-H02-clbd-prc-falta-reabasto-log.json`.

## Titulo

Agregar trans_reabastecimiento_log al SP CLBD_PRC

## Decision recomendada

Accion atomica con dueno claro - no requiere ADR.

## Razonamiento

Carol (ciclo-7) propone una accion atomica concreta: agregar la tabla a la lista del SP de limpieza para que clientes nuevos no hereden basura. La sub-pregunta abierta es secundaria (cuantas filas son pre-2024 vs post-2024). Ambas conviven: la accion atomica beneficia a futuros clientes, la sub-pregunta diagnostica el caso Killios actual.

## Acciones propuestas

- Equipo de instalacion: agregar 'trans_reabastecimiento_log' a la lista de tablas que limpia el SP CLBD_PRC. Repo del SP esta fuera del brain (en repo del WMS legacy).
- Ciclo 8a SQL: ejecutar query de C-04 sub-Q (pre-2024 vs post-2024 en Killios) para diagnosticar caso actual.
- Si la sub-Q revela que el modulo de DETECCION sigue activo en Killios, abrir nueva pregunta: vale la pena apagarlo?

## Archivos a editar (candidatos)

- `Repo del SP CLBD_PRC (fuera del brain, en repo del WMS)`
- `brain/wms-specific-process-flow/queries-ciclo-8a.md (creado en este commit)`
- `brain/wms-specific-process-flow/respuestas-ciclo-7.md (agregar nota de cierre en P-24)`

## Ratificacion

Erik o el equipo de instalacion decide cuando aplicar el cambio al SP.

## Como cerrar este proposal

Cuando Erik (o el equipo correspondiente) ratifica o rechaza la decision:

1. Mover `brain/_inbox/20260428-1901-H02-clbd-prc-falta-reabasto-log.json` a `brain/_processed/20260428-1901-H02-clbd-prc-falta-reabasto-log.json`.
2. Actualizar el campo `decision` del evento (`accepted` | `rejected` | `modified`).
3. Cerrar este proposal moviendolo a `brain/_processed/_proposals/` (subfolder a crear cuando haya el primer cierre).
4. Si `accepted`: ejecutar las acciones propuestas en orden.
5. Si `rejected`: documentar la razon en el field `decision` del evento y archivar.
6. Si `modified`: registrar las modificaciones en el field `decision` y proceder con la version modificada.
