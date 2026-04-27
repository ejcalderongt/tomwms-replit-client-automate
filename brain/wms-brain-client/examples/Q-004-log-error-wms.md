---
protocolVersion: 1
id: Q-004
title: Estructura y uso de log_error_wms (PEND-11)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: medium
status: pending
tags: [errores, log, K7, BB, C9, PEND-11]
targets:
  - codename: K7
    environment: PRD
suggestedQueries:
  - id: q1-cols-log
    description: Estructura de log_error_wms
    sql: |
      SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
      FROM INFORMATION_SCHEMA.COLUMNS
      WHERE TABLE_NAME = 'log_error_wms'
      ORDER BY ORDINAL_POSITION;
  - id: q2-volumen-y-rango
    description: Conteo total y rango de fechas de los logs
    sql: |
      SELECT
        COUNT(*) AS total_filas,
        MIN(fec_agr) AS mas_vieja,
        MAX(fec_agr) AS mas_reciente
      FROM log_error_wms;
  - id: q3-clasificacion-errores
    description: Top tipos de errores frecuentes
    sql: |
      SELECT TOP 20
        LEFT(ISNULL(mensaje, descripcion), 100) AS error_truncado,
        COUNT(*) AS cnt,
        MIN(fec_agr) AS primero,
        MAX(fec_agr) AS ultimo
      FROM log_error_wms
      GROUP BY LEFT(ISNULL(mensaje, descripcion), 100)
      ORDER BY cnt DESC;
  - id: q4-modulos-emisores
    description: Que modulos del WMS escriben a log_error_wms
    sql: |
      SELECT
        ISNULL(modulo, '<sin_modulo>') AS modulo,
        COUNT(*) AS cnt
      FROM log_error_wms
      GROUP BY ISNULL(modulo, '<sin_modulo>')
      ORDER BY cnt DESC;
expectedOutputs:
  - id: q1-cols-log
    type: table
  - id: q2-volumen-y-rango
    type: table
  - id: q3-clasificacion-errores
    type: table
  - id: q4-modulos-emisores
    type: table
followUp:
  ifFinding: Si log_error_wms tiene cols como sp_origen/idtransaccion → permite cruzar errores con outbox
  thenAsk: Q-XXX (cruzar errores recientes con outbox.enviado=0 para reproceso)
estimatedTimeMinutes: 5
---

## Contexto

PEND-11: necesitamos entender como se loggean los errores de las
interfaces (NavSync, SAPSYNC*, MI3) para cerrar el ciclo de
observabilidad. El outbox no tiene cols de error, asi que el detalle
debe vivir en `log_error_wms` o equivalente.

## Pregunta concreta

¿Que estructura tiene `log_error_wms`? ¿Que info loggea (mensaje, modulo,
sp_origen, idtransaccion, stack)? ¿Quienes escriben (BackOffice,
NavSync, SP)?

## Notas

- Los nombres de cols `mensaje`, `descripcion`, `modulo` en las queries
  son tentativos. Si no existen, el cliente debe loggear el error y
  sugerir version alternativa (q1 da el listado real de cols).
- Si la tabla no se llama `log_error_wms`, probar `log_errores`,
  `wms_log_error`, `bo_log_error`.
