---
protocolVersion: 1
id: Q-008
title: Manejo de devoluciones en el outbox (frente nuevo)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: medium
status: pending
tags: [outbox, devoluciones, K7, BB, frente-nuevo]
targets:
  - codename: K7
    environment: PRD
  - codename: BB
    environment: PRD
suggestedQueries:
  - id: q1-conteo-devoluciones
    description: Cuantos registros del outbox son devoluciones
    sql: |
      SELECT
        tipo_transaccion,
        CASE WHEN IdPedidoEncDevol IS NOT NULL THEN 'devolucion' ELSE 'normal' END AS clase,
        enviado,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      GROUP BY
        tipo_transaccion,
        CASE WHEN IdPedidoEncDevol IS NOT NULL THEN 'devolucion' ELSE 'normal' END,
        enviado
      ORDER BY tipo_transaccion, clase, enviado;
  - id: q2-sample-devoluciones
    description: Top 20 devoluciones recientes
    sql: |
      SELECT TOP 20
        idtransaccion,
        tipo_transaccion,
        no_pedido,
        no_documento_salida_ref_devol,
        IdPedidoEncDevol,
        codigo_producto,
        cantidad,
        enviado,
        fec_agr
      FROM i_nav_transacciones_out
      WHERE IdPedidoEncDevol IS NOT NULL
         OR no_documento_salida_ref_devol <> ''
      ORDER BY fec_agr DESC;
  - id: q3-tabla-devoluciones-encabezado
    description: Buscar tabla de encabezado de devoluciones
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE '%devol%'
         OR TABLE_NAME LIKE '%return%'
      ORDER BY TABLE_NAME;
expectedOutputs:
  - id: q1-conteo-devoluciones
    type: table
  - id: q2-sample-devoluciones
    type: table
  - id: q3-tabla-devoluciones-encabezado
    type: table
followUp:
  ifFinding: Si hay devoluciones procesadas → state-machine de devolucion debe documentarse aparte
  thenAsk: Q-XXX (state-machine de devolucion: estados, transiciones, integracion ERP)
estimatedTimeMinutes: 5
---

## Contexto

Mientras revisaba el outbox detecte cols `IdPedidoEncDevol` (id pedido
encabezado devolucion) y `no_documento_salida_ref_devol` (numero de
documento de salida original que se esta devolviendo). Eso confirma que
el outbox soporta **devoluciones** como caso especial — un frente que
no habiamos documentado.

## Pregunta concreta

¿Como se manejan las devoluciones en TOMWMS? ¿Estan activas en algun
cliente productivo? ¿Como se sincronizan al ERP?
