---
id: 20260428-1902-H03-logs-estructurados-mudos
tipo: proposal
estado: vigente
titulo: Proposal 20260428-1902-H03-logs-estructurados-mudos
tags: [proposal]
---

# Proposal 20260428-1902-H03-logs-estructurados-mudos

**Status**: proposed
**Generado**: 2026-04-28T19:45:00-03:00
**Generado por**: agente brain (sesion replit)

## Hallazgo origen

Ver `brain/_inbox/20260428-1902-H03-logs-estructurados-mudos.json`.

## Titulo

Exponer logs estructurados al admin y definir taxonomia recuperable/fatal

## Decision recomendada

Mejora de UX + decision de roadmap. NO bloquea reserva-webapi pero la habilita.

## Razonamiento

El WMS ya tiene logs estructurados por transaccion (i_nav_ejecucion_det_error, 4021 filas en Killios) pero no se muestran al admin. Sin clasificacion explicita recuperable/fatal el bridge no puede automatizar retry inteligente. La decision tiene 2 partes: (1) UX en BOF para exponer los logs, (2) taxonomia de errores formal.

## Acciones propuestas

- Roadmap ciclo 8: definir taxonomia formal recuperable/fatal/no-recuperable con codigos numericos. Diseño en brain/architecture/error-taxonomy.md (a crear).
- BOF: agregar vista que pivote i_nav_ejecucion_det_error con filtros por transaccion, severidad, fecha. (No bloquea reserva-webapi - puede ir en paralelo.)
- reserva-webapi: cuando se implemente, escribir errores a i_nav_ejecucion_det_error con la nueva taxonomia. El bridge debe trackear que la categorizacion sea consistente entre legacy y nuevo motor.

## Archivos a editar (candidatos)

- `brain/architecture/error-taxonomy.md (a crear en ciclo 8)`
- `Repo BOF (fuera del brain)`
- `Repo del WebAPI cuando exista`
- `brain/wms-specific-process-flow/respuestas-ciclo-7.md (agregar nota de cierre en P-20)`

## Ratificacion

Erik define la taxonomia. El UX del BOF puede ser delegado.

## Como cerrar este proposal

Cuando Erik (o el equipo correspondiente) ratifica o rechaza la decision:

1. Mover `brain/_inbox/20260428-1902-H03-logs-estructurados-mudos.json` a `brain/_processed/20260428-1902-H03-logs-estructurados-mudos.json`.
2. Actualizar el campo `decision` del evento (`accepted` | `rejected` | `modified`).
3. Cerrar este proposal moviendolo a `brain/_processed/_proposals/` (subfolder a crear cuando haya el primer cierre).
4. Si `accepted`: ejecutar las acciones propuestas en orden.
5. Si `rejected`: documentar la razon en el field `decision` del evento y archivar.
6. Si `modified`: registrar las modificaciones en el field `decision` y proceder con la version modificada.
