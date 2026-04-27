---
protocolVersion: 1
id: Q-003
title: Por que BB tiene 110k INGRESOS pendientes en outbox (PEND-12)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: critical
status: pending
tags: [outbox, navsync, BB, bandera-roja, PEND-12]
targets:
  - codename: BB
    environment: PRD
    minRows: 1000
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
suggestedQueries:
  - id: q1-edad-ingresos-pendientes
    description: Distribucion temporal de los 110k INGRESOS pendientes
    sql: |
      SELECT
        YEAR(fec_agr) AS anio,
        MONTH(fec_agr) AS mes,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 0 AND tipo_transaccion = 'INGRESO'
      GROUP BY YEAR(fec_agr), MONTH(fec_agr)
      ORDER BY anio, mes;
  - id: q2-ingresos-enviados-cuando
    description: Los 107 INGRESOS que SI se enviaron, en que fechas fueron
    sql: |
      SELECT
        idtransaccion, fec_agr, fec_mod, no_pedido, codigo_producto, cantidad,
        IdTipoDocumento, idordencompra, idrecepcionenc
      FROM i_nav_transacciones_out
      WHERE enviado = 1 AND tipo_transaccion = 'INGRESO'
      ORDER BY fec_mod ASC;
  - id: q3-comparacion-tipo-doc-pendiente-vs-enviado
    description: Hay diferencia de IdTipoDocumento entre los que se procesan y los que no
    sql: |
      SELECT
        IdTipoDocumento,
        enviado,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE tipo_transaccion = 'INGRESO'
      GROUP BY IdTipoDocumento, enviado
      ORDER BY IdTipoDocumento, enviado;
  - id: q4-otra-tabla-de-ingresos
    description: Buscar tablas alternativas de integracion ingresos (otro outbox)
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE 'i_%ingreso%'
         OR TABLE_NAME LIKE 'i_%recepcion%'
         OR TABLE_NAME LIKE 'i_nav_%'
         OR TABLE_NAME LIKE 'i_%entrada%'
      ORDER BY TABLE_NAME;
expectedOutputs:
  - id: q1-edad-ingresos-pendientes
    type: table
  - id: q2-ingresos-enviados-cuando
    type: table
  - id: q3-comparacion-tipo-doc-pendiente-vs-enviado
    type: table
  - id: q4-otra-tabla-de-ingresos
    type: table
followUp:
  ifFinding: Si q3 muestra IdTipoDocumento distinto entre pendientes y enviados → criterio de filtro de NavSync
  thenAsk: Q-XXX (definicion exacta del WHERE de NavSync para INGRESOS)
estimatedTimeMinutes: 8
---

## Contexto

En la pasada 9b vimos que `BB.i_nav_transacciones_out` tiene **110,795
INGRESOS con enviado=0** vs solo **107 enviados** (0.10% procesado),
mientras las SALIDAS estan al 65.6%. Es bandera roja: o NavSync no
maneja INGRESOS, o tiene un bug, o los INGRESOS van por otro canal.

## Pregunta concreta

¿Por que el outbox de BB tiene 110k INGRESOS pendientes y casi ninguno
enviado? Tres hipotesis a discriminar:

1. **NavSync solo procesa SALIDAS** (los INGRESOS van por otro canal:
   WCF directo, batch nightly, manual, otra tabla).
2. **NavSync tiene un filtro WHERE estricto** (por IdTipoDocumento u
   otro campo) que descarta el 99.9%.
3. **NavSync esta caido para INGRESOS desde hace tiempo** y nadie noto.

## Que se espera del operador

1. `Invoke-WmsBrainQuestion -Id Q-003 -Profile BB-PRD`.
2. Revisar q1 (edad de pendientes — si todos son viejos, hipotesis 3).
3. Revisar q2 (los 107 que si se procesaron — buscar patron).
4. Revisar q3 (IdTipoDocumento — si los enviados son todos un tipo
   especifico, hipotesis 2).
5. Revisar q4 (otra tabla — si existe `i_nav_ingresos_in` u otra,
   hipotesis 1).
6. Anotar interpretacion y, si conoces, el codigo de NavSync que filtra
   los INGRESOS.
