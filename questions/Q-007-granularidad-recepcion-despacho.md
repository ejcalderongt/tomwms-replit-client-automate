---
protocolVersion: 1
id: Q-007
title: Granularidad de IdRecepcionDet vs encabezado (PEND-09)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: low
status: pending
tags: [outbox, recepcion, despacho, granularidad, K7, PEND-09]
targets:
  - codename: K7
    environment: PRD
suggestedQueries:
  - id: q1-distribucion-iddet
    description: Cuantos registros del outbox tienen IdRecepcionDet/IdDespachoDet seteados
    sql: |
      SELECT
        tipo_transaccion,
        SUM(CASE WHEN IdRecepcionDet IS NOT NULL THEN 1 ELSE 0 END) AS con_recepcion_det,
        SUM(CASE WHEN IdDespachoDet IS NOT NULL THEN 1 ELSE 0 END) AS con_despacho_det,
        SUM(CASE WHEN IdRecepcionDet IS NULL AND IdDespachoDet IS NULL THEN 1 ELSE 0 END) AS sin_ningun_det,
        COUNT(*) AS total
      FROM i_nav_transacciones_out
      GROUP BY tipo_transaccion;
  - id: q2-relacion-encabezado-detalle
    description: Cuantas filas de outbox por encabezado tipico
    sql: |
      SELECT TOP 20
        idrecepcionenc,
        COUNT(*) AS lineas_outbox
      FROM i_nav_transacciones_out
      WHERE tipo_transaccion = 'INGRESO'
        AND idrecepcionenc > 0
      GROUP BY idrecepcionenc
      ORDER BY lineas_outbox DESC;
  - id: q3-cantidad-parcial
    description: Casos donde cantidad_enviada es != cantidad (envio parcial)
    sql: |
      SELECT TOP 30
        idtransaccion,
        tipo_transaccion,
        cantidad,
        cantidad_enviada,
        cantidad_pendiente,
        enviado,
        fec_agr,
        fec_mod
      FROM i_nav_transacciones_out
      WHERE cantidad_enviada > 0 AND cantidad_enviada <> cantidad
      ORDER BY fec_agr DESC;
expectedOutputs:
  - id: q1-distribucion-iddet
    type: table
  - id: q2-relacion-encabezado-detalle
    type: table
  - id: q3-cantidad-parcial
    type: table
followUp:
  ifFinding: Si q3 retorna filas → el outbox soporta envios parciales reales (no solo schema)
  thenAsk: Q-XXX (politica de split — cuando NavSync decide partir un envio)
estimatedTimeMinutes: 4
---

## Contexto

PEND-09: detectamos cols `IdRecepcionDet`, `IdDespachoDet`,
`cantidad_enviada`, `cantidad_pendiente` en el outbox. Sugieren que cada
fila del outbox puede ser:

- Una linea de recepcion/despacho (granularidad detalle).
- O una **fraccion** de una linea (envio parcial real).

## Pregunta concreta

¿Como se usa la granularidad en datos reales? ¿Outbox = N lineas por
encabezado siempre? ¿Hay envios parciales documentados?
