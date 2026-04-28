# Proposal 20260428-1900-H01-tras-wms-reservastock-muerto

**Status**: proposed
**Generado**: 2026-04-28T19:45:00-03:00
**Generado por**: agente brain (sesion replit)

## Hallazgo origen

Ver `brain/_inbox/20260428-1900-H01-tras-wms-reservastock-muerto.json`.

## Titulo

TRAS_WMS.ReservaStock - reservado-para-futuro, NO eliminar

## Decision recomendada

Aplicar ADR-011 (opcion c).

## Razonamiento

Triple confirmacion (Erik tanda-1, Carol pasada-7, SQL agente tanda-2) de que la bandera no se valida hoy. Eliminarla quemaria la vision arquitectonica futura del bucket/abastecimiento policy-driven que Erik documento. La opcion mas conservadora y compatible con el patron historico de Erik ("dejar el codigo extensible") es documentar como reservado-para-futuro y rechazar el campo cuando alguien intente poblarlo en TRAS_WMS hoy.

## Acciones propuestas

- Crear ADR-011 (DONE en este commit)
- Cuando se haga scaffold de reserva-webapi (P-16b), aplicar el schema con marca @reservado-para-futuro segun ADR-011
- Agregar test que verifica el rechazo HTTP 400 cuando IdTipoPedido=TRAS_WMS y ReservaStock=true
- Documentar en openapi.yaml con description extendida que linkea al ADR

## Archivos a editar (candidatos)

- `brain/architecture/adr/ADR-011-tras-wms-reservastock-decision.md (creado)`
- `Repo del WebAPI nuevo cuando exista (post P-16b)`
- `brain/wms-specific-process-flow/respuestas-pasada-7.md (agregar nota de cierre en P-18 apuntando a ADR-011)`

## Ratificacion

Erik debe confirmar antes del primer release del WebAPI. Si prefiere otra opcion, hacer rollback del ADR.

## Como cerrar este proposal

Cuando Erik (o el equipo correspondiente) ratifica o rechaza la decision:

1. Mover `brain/_inbox/20260428-1900-H01-tras-wms-reservastock-muerto.json` a `brain/_processed/20260428-1900-H01-tras-wms-reservastock-muerto.json`.
2. Actualizar el campo `decision` del evento (`accepted` | `rejected` | `modified`).
3. Cerrar este proposal moviendolo a `brain/_processed/_proposals/` (subfolder a crear cuando haya el primer cierre).
4. Si `accepted`: ejecutar las acciones propuestas en orden.
5. Si `rejected`: documentar la razon en el field `decision` del evento y archivar.
6. Si `modified`: registrar las modificaciones en el field `decision` y proceder con la version modificada.
