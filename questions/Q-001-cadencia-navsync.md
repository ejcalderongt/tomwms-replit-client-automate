---
protocolVersion: 1
id: Q-001
title: Cadencia real del job que procesa SALIDAS en BB outbox
createdBy: agent-replit
createdAt: 2026-04-27T18:00:00Z
priority: medium
status: pending
tags: [outbox, navsync, BB, cadencia, PEND-07]
targets:
  - codename: BB
    environment: PRD
    minRows: 1000
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-specific-process-flow/preguntas-pasada-7.md
suggestedQueries:
  - id: q1-histograma-hora
    description: Distribucion horaria de fec_agr en filas enviadas
    sql: |
      SELECT
        DATEPART(HOUR, fec_agr) AS hora,
        COUNT(*) AS cnt_enviados,
        AVG(CAST(DATEDIFF(SECOND, fec_agr, fec_mod) AS float)) AS avg_seg_proceso
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND tipo_transaccion = 'SALIDA'
        AND fec_agr >= DATEADD(DAY, -30, GETDATE())
      GROUP BY DATEPART(HOUR, fec_agr)
      ORDER BY hora;
  - id: q2-latencia-tipica
    description: Histograma de segundos entre fec_agr y fec_mod
    sql: |
      SELECT
        CASE
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) = 0 THEN '0 seg (instantaneo)'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 5 THEN '1-5 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 60 THEN '6-60 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 300 THEN '1-5 min'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 3600 THEN '5-60 min'
          ELSE '> 1 hora'
        END AS rango_latencia,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND tipo_transaccion = 'SALIDA'
      GROUP BY
        CASE
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) = 0 THEN '0 seg (instantaneo)'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 5 THEN '1-5 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 60 THEN '6-60 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 300 THEN '1-5 min'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 3600 THEN '5-60 min'
          ELSE '> 1 hora'
        END
      ORDER BY MIN(DATEDIFF(SECOND, fec_agr, fec_mod));
  - id: q3-batches-detectados
    description: Detecta picos sincronos (mismo segundo) que indicarian batch
    sql: |
      SELECT TOP 20
        fec_mod,
        COUNT(*) AS filas_marcadas_mismo_segundo
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND tipo_transaccion = 'SALIDA'
        AND fec_mod >= DATEADD(DAY, -7, GETDATE())
      GROUP BY fec_mod
      HAVING COUNT(*) > 1
      ORDER BY COUNT(*) DESC;
  - id: q4-sps-relacionados
    description: Identifica SP que tocan i_nav_transacciones_out con UPDATE enviado
    sql: |
      SELECT
        o.name AS sp_name,
        m.execution_count,
        m.last_execution_time,
        m.total_elapsed_time / NULLIF(m.execution_count,0) / 1000 AS avg_ms
      FROM sys.sql_modules sm
      JOIN sys.objects o ON o.object_id = sm.object_id
      LEFT JOIN sys.dm_exec_procedure_stats m ON m.object_id = o.object_id
      WHERE sm.definition LIKE '%i_nav_transacciones_out%'
        AND sm.definition LIKE '%enviado%'
        AND sm.definition LIKE '%UPDATE%'
      ORDER BY m.last_execution_time DESC;
expectedOutputs:
  - id: q1-histograma-hora
    type: table
    columns: [hora, cnt_enviados, avg_seg_proceso]
  - id: q2-latencia-tipica
    type: table
    columns: [rango_latencia, cnt]
  - id: q3-batches-detectados
    type: table
    columns: [fec_mod, filas_marcadas_mismo_segundo]
  - id: q4-sps-relacionados
    type: table
    columns: [sp_name, execution_count, last_execution_time, avg_ms]
followUp:
  ifFinding: Si el 80%+ esta en "0 seg" → es post-insert (trigger o llamada directa). Si hay batches grandes en mismo fec_mod → es scheduler periodico. Si Q4 muestra SP frecuente → identificar caller.
  thenAsk: Q-002 (identificar el caller del SP, sea Windows Scheduler o BO.NET)
estimatedTimeMinutes: 5
allowFreeFormNotes: true
---

## Contexto

En la pasada 9b detectamos que `BB.i_nav_transacciones_out` tiene
**145,117 SALIDAS pendientes** y **277,310 enviadas** (65.6% procesado),
mientras que en K7 esta al 100%. Ademas hay **110,795 INGRESOS pendientes**
en BB con casi 0% procesado, lo cual sugiere que NavSync para INGRESOS
puede no estar corriendo (eso es Q-XXX aparte).

Para SALIDAS sabemos que NavSync sí corre, pero **no sabemos su cadencia
real** (instantanea post-insert, batch periodico cada N min, on-demand
disparado por evento del WMS, etc).

## Pregunta concreta

¿Cual es la cadencia real del job que setea `enviado=1` en
`i_nav_transacciones_out` de BB para tipo_transaccion='SALIDA'?

## Que se espera del operador (Erik)

1. `Sync-WmsBrain` (asegurar repo actualizado).
2. `Show-WmsBrainQuestion -Id Q-001` para ver esta card.
3. `Invoke-WmsBrainQuestion -Id Q-001 -Profile BB-PRD`.
4. Revisar resultados en el draft generado.
5. Agregar interpretacion de 2-4 lineas (free-form notes) sobre que
   patron de cadencia muestran los datos.
6. (Bonus) Si conoces el nombre del scheduler/.exe/.SP que ejecuta NavSync,
   incluirlo en las notas — eso cierra la pregunta de un solo tiro.
7. `Submit-WmsBrainAnswer -Id Q-001 -Verdict confirmed -Confidence high`.

## Notas tecnicas

- Las 4 queries son read-only.
- Q1 usa ventana 30 dias para evitar saturar el resultado.
- Q3 detecta batches: si aparece "fec_mod=2026-04-27 03:00:00, filas=512"
  es indicio fuerte de cron job. Si todos son cnt=1 → es post-insert.
- Q4 puede no devolver nada si los SP estan firmados o cifrados.
