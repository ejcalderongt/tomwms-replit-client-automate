# Proposal 20260428-1903-H04-despacho-fantasma-bypass-estado

**Status**: proposed
**Generado**: 2026-04-28T19:45:00-03:00
**Generado por**: agente brain (sesion replit)

## Hallazgo origen

Ver `brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json`.

## Titulo

Bypass estado=Despachado - permitir con permiso + razon + auditoria

## Decision recomendada

Aplicar ADR-012 (opcion b).

## Razonamiento

Carol (pasada-7) confirma que el bypass es feature legitima del WMS legacy. Erik tiene precedente analogo (NUEVO->Pickeado para WMS-inyectados). Prohibirlo rompe operacion (43 casos historicos lo prueban). Replicarlo sin auditoria continua el problema actual de invisibilidad. La opcion b agrega trazabilidad sin romper compatibilidad.

## Acciones propuestas

- Crear ADR-012 (DONE en este commit)
- Pasada 8a SQL: ejecutar P-16b refinada para confirmar el numero exacto de bypasses en Killios PRD
- Pasada 9: crear migration para tabla aud_pedido_estado_forzado
- Cuando se haga scaffold reserva-webapi: implementar endpoint POST /pedidos/{id}/forzar-estado con los 3 requisitos de ADR-012
- BOF: agregar permiso pedidos.forzar_despachado y form modal con campo razon obligatorio

## Archivos a editar (candidatos)

- `brain/architecture/adr/ADR-012-bypass-estado-despachado.md (creado)`
- `brain/wms-specific-process-flow/queries-pasada-8a.md (P-16b query lista para ejecutar)`
- `brain/wms-specific-process-flow/state-machine-pedido.md (agregar excepcion documentada como nueva transicion auditada)`
- `brain/wms-specific-process-flow/bug-report-p16b.md (cerrar como diseño aplicado, no bug)`
- `Repo del WebAPI cuando exista`
- `Repo BOF`

## Ratificacion

Erik debe confirmar antes del primer release del WebAPI. Especialmente el shape de la tabla aud_pedido_estado_forzado y los requisitos del endpoint.

## Como cerrar este proposal

Cuando Erik (o el equipo correspondiente) ratifica o rechaza la decision:

1. Mover `brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json` a `brain/_processed/20260428-1903-H04-despacho-fantasma-bypass-estado.json`.
2. Actualizar el campo `decision` del evento (`accepted` | `rejected` | `modified`).
3. Cerrar este proposal moviendolo a `brain/_processed/_proposals/` (subfolder a crear cuando haya el primer cierre).
4. Si `accepted`: ejecutar las acciones propuestas en orden.
5. Si `rejected`: documentar la razon en el field `decision` del evento y archivar.
6. Si `modified`: registrar las modificaciones en el field `decision` y proceder con la version modificada.
