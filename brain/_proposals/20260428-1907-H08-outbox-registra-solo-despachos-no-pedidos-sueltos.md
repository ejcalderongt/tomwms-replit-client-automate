# 20260428-1907-H08 - i_nav_transacciones_out NO registra pedidos sueltos: 100% de filas con idpedidoenc tienen iddespachoenc.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Ciclo 8a via ejecucion live SQL.

## Contexto

Q-009 ejecutada contra 3 BDs (28-abr-2026). K7-PRD: 24193 filas total, con_pedido=19799, con_despacho=19799 (IDENTICOS). BB-PRD: 533329 total, con_pedido=422427, con_despacho=422427 (IDENTICOS). C9-QAS: 0 filas (entorno QAS sin trafico). Lectura: el outbox NO emite eventos de 'pedido creado/modificado' - solo emite cuando hay despacho que arrastra el IdPedidoEnc. Implicaciones: (a) Navigator NO ve pedidos hasta despachados; (b) la nueva WebAPI puede simplificar el bridge - 4 tipos colapsa a 2 efectivos (recepcion + despacho con FKs adicionales); (c) Carol (P-19) tenia razon parcialmente: dijo 'recepciones y despachos' - confirmado que esos 2 dominan, OC y pedido viajan de polizon en eventos de recepcion/despacho.

## Modulos tocados

- `i_nav_transacciones_out`
- `navigator-bridge`
- `outbox-pattern`

## Decision provisional

`accepted_simplify_bridge_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos.json`
- Tags: `validated-via-live-sql`, `outbox-semantica`, `navigator`, `simplificacion-webapi`, `q-009`, `concrete-action`
- Preguntas origen: `P-19`, `Q-009`
- Respondedoras: `KKKL-Carol`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
