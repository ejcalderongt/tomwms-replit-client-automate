---
protocolVersion: 1
id: Q-002
title: Decimales en cantidades enviadas a SAP B1 (K7)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: high
status: pending
tags: [sap, decimales, K7, SAPSYNCKILLIOS]
targets:
  - codename: K7
    environment: PRD
    minRows: 100
relatedDocs:
  - brain/wms-specific-process-flow/preguntas-ciclo-7.md#P-04
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
suggestedQueries:
  - id: q1-cantidades-fraccional-vs-entera
    description: Cuenta cuantos registros tienen cantidad fraccional (no entera) en outbox enviado
    sql: |
      SELECT
        CASE WHEN cantidad = FLOOR(cantidad) THEN 'entera' ELSE 'fraccional' END AS tipo_cantidad,
        tipo_transaccion,
        COUNT(*) AS cnt,
        MIN(cantidad) AS min_val,
        MAX(cantidad) AS max_val
      FROM i_nav_transacciones_out
      WHERE enviado = 1
      GROUP BY
        CASE WHEN cantidad = FLOOR(cantidad) THEN 'entera' ELSE 'fraccional' END,
        tipo_transaccion
      ORDER BY tipo_cantidad, tipo_transaccion;
  - id: q2-precision-decimal
    description: Detecta cantidades con mas de 2 decimales (potencial truncado en SAP)
    sql: |
      SELECT TOP 50
        idtransaccion,
        codigo_producto,
        cantidad,
        cantidad - FLOOR(cantidad) AS parte_decimal,
        unidad_medida,
        tipo_transaccion,
        fec_agr
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND ABS(cantidad - ROUND(cantidad, 2)) > 0.001
      ORDER BY ABS(cantidad - ROUND(cantidad, 2)) DESC;
  - id: q3-unidades-fraccionables
    description: Identifica unidades de medida que pueden tener cantidad fraccional (ej. KG, LT)
    sql: |
      SELECT DISTINCT
        unidad_medida,
        codigo_variante,
        idunidadmedida,
        COUNT(*) AS cnt_uso
      FROM i_nav_transacciones_out
      GROUP BY unidad_medida, codigo_variante, idunidadmedida
      ORDER BY cnt_uso DESC;
expectedOutputs:
  - id: q1-cantidades-fraccional-vs-entera
    type: table
  - id: q2-precision-decimal
    type: table
  - id: q3-unidades-fraccionables
    type: table
followUp:
  ifFinding: Si hay cantidades con >2 decimales y SAP solo acepta 2, necesitamos politica de redondeo documentada
  thenAsk: Q-XXX (politica de redondeo SAP — confirmar con responsable de interface)
estimatedTimeMinutes: 5
---

## Contexto

P-04 viene desde el ciclo 7: SAP B1 historicamente acepta cantidades con
2 decimales en la mayoria de campos, pero algunos clientes (K7) manejan
unidades fraccionables (kg, litros) en el WMS que pueden generar mas
precision. Si SAPSYNCKILLIOS no normaliza, SAP puede truncar/redondear
silenciosamente y generar diferencias.

## Pregunta concreta

¿Hay cantidades con mas de 2 decimales saliendo del WMS hacia SAP en K7?
Si las hay, ¿de que productos/unidades son?

## Que se espera del operador

1. `Invoke-WmsBrainQuestion -Id Q-002 -Profile K7-PRD`
2. Revisar q1 (proporcion fraccional vs entera).
3. Revisar q2 (top 50 con mas precision).
4. Si q2 tiene >0 filas, anotar las unidades de medida involucradas.
5. (Bonus) Confirmar con quien programo SAPSYNCKILLIOS si hay
   redondeo explicito en el codigo.
