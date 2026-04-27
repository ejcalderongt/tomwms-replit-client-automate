---
protocolVersion: 1
id: Q-010
title: BB tiene 145k SALIDAS IdTipoDocumento=1 y 30k tipo=4 sin procesar — son devoluciones?
createdBy: agent-replit
createdAt: 2026-04-27T19:00:00Z
priority: high
status: pending
tags: [outbox, navsync, BB, devoluciones, bandera-roja, follow-up-A-003, follow-up-A-008]
targets:
  - codename: BB
    environment: PRD
relatedDocs:
  - brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md
  - brain/wms-brain-client/answers/A-008-devoluciones-frente-nuevo.md
  - brain/learnings/L-013-outbox-granularidad-por-linea.md
suggestedQueries:
  - id: q1-distribucion-temporal-tipo1
    description: Distribucion mensual de SALIDAs IdTipoDocumento=1 pendientes
    sql: |
      SELECT
        YEAR(fec_agr) AS anio,
        MONTH(fec_agr) AS mes,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 0
        AND tipo_transaccion = 'SALIDA'
        AND IdTipoDocumento = 1
      GROUP BY YEAR(fec_agr), MONTH(fec_agr)
      ORDER BY anio, mes;
  - id: q2-sample-tipo1-y-tipo4
    description: Muestra de filas con metadata clave
    sql: |
      SELECT TOP 30
        idtransaccion, IdTipoDocumento, no_pedido,
        no_documento_salida_ref_devol, IdPedidoEncDevol,
        codigo_producto, cantidad, fec_agr,
        empresa_transporte, observacion
      FROM i_nav_transacciones_out
      WHERE enviado = 0
        AND tipo_transaccion = 'SALIDA'
        AND IdTipoDocumento IN (1, 4)
      ORDER BY fec_agr DESC;
  - id: q3-catalogo-tipo-documento
    description: Buscar catalogo de tipos de documento
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE '%tipo%doc%' OR TABLE_NAME LIKE 'p_tipo%'
      ORDER BY TABLE_NAME;
  - id: q4-tipo1-vs-tipo3-comparativa
    description: Comparar campos no nulos en tipo 1 vs tipo 3 (lo que sí se procesa)
    sql: |
      SELECT
        IdTipoDocumento,
        SUM(CASE WHEN no_documento_salida_ref_devol IS NOT NULL AND no_documento_salida_ref_devol <> '' THEN 1 ELSE 0 END) AS con_ref_devol,
        SUM(CASE WHEN IdPedidoEncDevol IS NOT NULL AND IdPedidoEncDevol > 0 THEN 1 ELSE 0 END) AS con_pedido_devol,
        SUM(CASE WHEN observacion IS NOT NULL AND observacion <> '' THEN 1 ELSE 0 END) AS con_observacion,
        COUNT(*) AS total
      FROM i_nav_transacciones_out
      WHERE tipo_transaccion = 'SALIDA' AND IdTipoDocumento IN (1, 3, 4)
      GROUP BY IdTipoDocumento
      ORDER BY IdTipoDocumento;
expectedOutputs:
  - id: q1-distribucion-temporal-tipo1
    type: table
    columns: [anio, mes, cnt]
  - id: q2-sample-tipo1-y-tipo4
    type: table
    columns: [idtransaccion, IdTipoDocumento, no_pedido, no_documento_salida_ref_devol, IdPedidoEncDevol, codigo_producto, cantidad, fec_agr, empresa_transporte, observacion]
followUp:
  ifFinding: Si todos los tipo=1 tienen `IdPedidoEncDevol` o `no_documento_salida_ref_devol` no nulo → son devoluciones. Si tipo=4 tiene `observacion` con keyword "ajuste" → son ajustes de inventario. Esto define si bridge debe cubrirlos.
  thenAsk: Si son devoluciones reales sin procesar → abrir Q sobre canal de devoluciones BB (ERP nativo o requiere extension de NavSync).
estimatedTimeMinutes: 7
allowFreeFormNotes: true
---

## Contexto

A-003 confirmo que NavSync de BB solo procesa SALIDAS
`IdTipoDocumento=3` (99.99% enviadas, 277,309 filas). Pero
quedaron 2 tipos no procesados:

- **IdTipoDocumento=1 SALIDA**: 115,165 pendientes / 1 enviado
- **IdTipoDocumento=4 SALIDA**: 29,924 pendientes / 0 enviados

Esto es ~145k SALIDAs sin reportar al ERP. La hipotesis es que
son devoluciones (tipo 1) y ajustes (tipo 4), pero no se confirmo.
Tampoco se sabe si **deberian** procesarse o si son "por diseño".

## Pregunta concreta

1. ¿Que representan IdTipoDocumento=1 y 4 (devoluciones, ajustes,
   traslados)?
2. ¿Por que NavSync no las procesa — filtro explicito o flujo
   alterno?
3. ¿Hay backlog operativo (necesitan procesarse) o son
   correctamente ignoradas?

## Que se espera del operador (Erik)

1. `Sync-WmsBrain` → `Show-WmsBrainQuestion -Id Q-010`.
2. `Invoke-WmsBrainQuestion -Id Q-010 -Profile BB-PRD`.
3. Cruzar con conocimiento operativo: ¿que es tipo=1 y tipo=4?
4. `Submit-WmsBrainAnswer -Id Q-010 -Verdict <confirmed|partial> -Confidence <high|medium>`.

## Notas tecnicas

- Si la query q3 devuelve catalogo claro, anexarlo al answer card.
- Las queries son read-only.
