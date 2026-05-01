---
id: 20260428-1909-H10-25-pedidos-fiscales-sin-poliza-cealsa-qas-1-7-pct
tipo: proposal
estado: vigente
titulo: "20260428-1909-H10 - C9-QAS: 25 de 1441 pedidos fiscales (1.7%) NO tienen registro en trans_pe_pol. Confirma H-04."
clientes: [cealsa]
tags: [proposal, cliente/cealsa]
---

# 20260428-1909-H10 - C9-QAS: 25 de 1441 pedidos fiscales (1.7%) NO tienen registro en trans_pe_pol. Confirma H-04.

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Ciclo 8a via ejecucion live SQL.

## Contexto

Q-013 ejecutada contra IMS4MB_CEALSA_QAS (28-abr-2026): de 1441 pedidos cuyo tipo tiene control_poliza=1, solo 1416 tienen registro en trans_pe_pol (98.3%). 25 pedidos fiscales SIN poliza captada (1.7%). Esto VALIDA empiricamente H-04 (poliza fiscal con dual-state frontend-only): si la captura de poliza ocurre solo en UI sin commit transaccional al pedido, es esperable que ~1-2% se pierda. Implicaciones: (a) la nueva WebAPI debe emitir alerta cuando un pedido fiscal se cierra sin trans_pe_pol; (b) auditar esos 25 pedidos especificos para entender si fueron creados por el path de devolucion (donde la poliza no aplica) o si efectivamente se 'colaron' sin poliza; (c) reforzar la propuesta P3-FISCAL-LOCK con validacion server-side bloqueante.

## Modulos tocados

- `trans_pe_pol`
- `trans_pe_enc`
- `trans_pe_tipo`
- `control_poliza`
- `validacion-fiscal`

## Decision provisional

`accepted_refuerza_p3_fiscal_lock_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1909-H10-25-pedidos-fiscales-sin-poliza-cealsa-qas-1-7-pct.json`
- Tags: `validated-via-live-sql`, `cealsa`, `poliza-fiscal`, `data-quality`, `refuerza-h04`, `q-013`, `concrete-action`
- Preguntas origen: `Q-013`
- Respondedoras: `SQL-agente`, `schema-validator`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
