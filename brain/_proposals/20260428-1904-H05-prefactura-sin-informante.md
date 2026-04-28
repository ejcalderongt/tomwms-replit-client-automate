# Proposal 20260428-1904-H05-prefactura-sin-informante

**Status**: proposed
**Generado**: 2026-04-28T19:45:00-03:00
**Generado por**: agente brain (sesion replit)

## Hallazgo origen

Ver `brain/_inbox/20260428-1904-H05-prefactura-sin-informante.json`.

## Titulo

Prefactura CEALSA - identificar respondedor alternativo

## Decision recomendada

Bloqueado por falta de informante. Iniciar busqueda de respondedor.

## Razonamiento

Carol no conoce el proceso de prefactura CEALSA. Sin informante el dominio billing queda fuera del bridge para CEALSA. Los datos en BD existen (trans_prefactura_enc/det/mov, trans_pe_servicios) pero la logica de negocio (agregacion, tarifas, periodicidad) es desconocida.

## Acciones propuestas

- Erik: identificar candidato respondedor entre: (a) finanzas/billing CEALSA, (b) consultor previo del proyecto WMS-CEALSA, (c) operaciones senior CEALSA.
- Si en 2 semanas no hay respondedor humano, derivar via SQL del agente brain (analisis de estructura y patrones de uso de trans_prefactura_*).
- Reservar P-23 para Ciclo 8 con respondedor identificado.
- Hasta tener respuesta: el WebAPI no incluye endpoints de prefactura para CEALSA. El bridge marca el dominio billing CEALSA como out-of-scope explicito.

## Archivos a editar (candidatos)

- `brain/replit.md (anotar gap de conocimiento billing CEALSA)`
- `brain/_inbox/<futuro>-ciclo-8-prefactura.json (cuando se identifique respondedor)`
- `brain/architecture/adr/ADR-009-cealsa-3pl-jornada-prefactura.md (existente, agregar caveat de prefactura sin informante)`

## Ratificacion

Erik debe identificar el respondedor o autorizar la derivacion via SQL.

## Como cerrar este proposal

Cuando Erik (o el equipo correspondiente) ratifica o rechaza la decision:

1. Mover `brain/_inbox/20260428-1904-H05-prefactura-sin-informante.json` a `brain/_processed/20260428-1904-H05-prefactura-sin-informante.json`.
2. Actualizar el campo `decision` del evento (`accepted` | `rejected` | `modified`).
3. Cerrar este proposal moviendolo a `brain/_processed/_proposals/` (subfolder a crear cuando haya el primer cierre).
4. Si `accepted`: ejecutar las acciones propuestas en orden.
5. Si `rejected`: documentar la razon en el field `decision` del evento y archivar.
6. Si `modified`: registrar las modificaciones en el field `decision` y proceder con la version modificada.
