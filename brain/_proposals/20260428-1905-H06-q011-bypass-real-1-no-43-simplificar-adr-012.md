# 20260428-1905-H06 - Q-011 cerrada: bypass real es 1 pedido (no 43 reportados por Carol). ADR-012 sobre-dimensionado.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Pasada 8a via ejecucion live SQL.

## Contexto

Ejecucion live contra TOMWMS_KILLIOS_PRD (sa, 28-abr-2026): de 3989 pedidos en estado='Despachado', solo 1 tiene 0 filas en trans_despacho_det. pct_bypass=0.03%. Carol (P-19, KKKL) reporto 43 casos. Discrepancia: 43x. El bypass es un evento RARISIMO, no un patron. Accion: simplificar ADR-012 - quitar permiso especial, quitar rate-limit, quitar IS_BYPASS_DESPACHO_PERMITIDO. Mantener solo: registro de auditoria liviano cuando ocurre + alerta. Frecuencia esperada <1 caso/anio.

## Modulos tocados

- `ADR-012`
- `trans_despacho_det`
- `trans_pe_enc`
- `estado-despachado`

## Decision provisional

`accepted_simplify_adr_012_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012.json`
- Tags: `validated-via-live-sql`, `recalibrar-decision`, `killios`, `adr-revision`, `concrete-action`, `q-011`
- Preguntas origen: `P-19`, `Q-011`
- Respondedoras: `KKKL-Carol`
- Pasada: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-pasada-8a.md`
