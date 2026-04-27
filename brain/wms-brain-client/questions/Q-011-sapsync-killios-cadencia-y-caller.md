---
protocolVersion: 1
id: Q-011
title: Cadencia y caller real del SAPSYNCKILLIOS (writer de enviado=1 en K7)
createdBy: agent-replit
createdAt: 2026-04-27T19:00:00Z
priority: medium
status: pending
tags: [sap, sapsync, K7, cadencia, PEND-07, follow-up-A-005]
targets:
  - codename: K7
    environment: PRD
relatedDocs:
  - brain/wms-brain-client/answers/A-005-sapsync-dedicado-por-cliente.md
  - brain/learnings/L-009-sapsync-killios-solo-enteros.md
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
suggestedQueries:
  - id: q1-latencia-sap-killios
    description: Histograma de latencia fec_agr->fec_mod para K7 (analogo a Q-001 pero K7)
    sql: |
      SELECT
        CASE
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) = 0 THEN '0 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 5 THEN '1-5 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 60 THEN '6-60 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 300 THEN '1-5 min'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 3600 THEN '5-60 min'
          ELSE '> 1 hora'
        END AS rango_latencia,
        tipo_transaccion,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 1
      GROUP BY
        CASE
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) = 0 THEN '0 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 5 THEN '1-5 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 60 THEN '6-60 seg'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 300 THEN '1-5 min'
          WHEN DATEDIFF(SECOND, fec_agr, fec_mod) <= 3600 THEN '5-60 min'
          ELSE '> 1 hora'
        END,
        tipo_transaccion
      ORDER BY tipo_transaccion, rango_latencia;
  - id: q2-actividad-mes-actual
    description: Conteos diarios del mes actual
    sql: |
      SELECT
        CAST(fec_mod AS date) AS dia,
        tipo_transaccion,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND fec_mod >= DATEADD(DAY, -30, GETDATE())
      GROUP BY CAST(fec_mod AS date), tipo_transaccion
      ORDER BY dia DESC, tipo_transaccion;
  - id: q3-sps-locales-killios
    description: Buscar SPs locales que actualicen enviado en K7
    sql: |
      SELECT TOP 20
        o.name AS sp_name,
        o.modify_date,
        LEN(sm.definition) AS def_len
      FROM sys.sql_modules sm
      JOIN sys.objects o ON o.object_id = sm.object_id
      WHERE sm.definition LIKE '%i_nav_transacciones_out%'
        AND sm.definition LIKE '%enviado%'
      ORDER BY o.modify_date DESC;
  - id: q4-jobs-sql-agent-killios
    description: Buscar jobs en SQL Agent
    sql: |
      SELECT j.[name] AS job_name, s.step_name, j.enabled,
             LEFT(s.command, 400) AS step_command_preview
      FROM msdb.dbo.sysjobs j
      LEFT JOIN msdb.dbo.sysjobsteps s ON s.job_id = j.job_id
      WHERE j.enabled = 1
      ORDER BY j.[name], s.step_id;
expectedOutputs:
  - id: q1-latencia-sap-killios
    type: table
    columns: [rango_latencia, tipo_transaccion, cnt]
  - id: q2-actividad-mes-actual
    type: table
    columns: [dia, tipo_transaccion, cnt]
  - id: q3-sps-locales-killios
    type: table
    columns: [sp_name, modify_date, def_len]
  - id: q4-jobs-sql-agent-killios
    type: table
    columns: [job_name, step_name, enabled, step_command_preview]
followUp:
  ifFinding: Si q1 muestra latencia >0 con bumps en multiples de N min → scheduler. Si esta en 0 seg → trigger. Si q3 muestra SP frecuente → identificar caller. Si q4 vacio → SAPSYNCKILLIOS es Windows Service externo.
  thenAsk: Si es Windows Service, abrir Q sobre version, host y log file path del binario.
estimatedTimeMinutes: 5
allowFreeFormNotes: true
---

## Contexto

A-005 confirmo que el WMS K7 tiene 27 tablas de configuracion y
13+ flags `*_sap` que parametrizan SAPSYNCKILLIOS. Pero **no se
analizo la cadencia del proceso ni se identifico al caller**. K7
hoy tiene outbox al 100% procesado (24,193 / 24,193) — opuesto a
BB. Importante saber si SAPSYNCKILLIOS sigue corriendo o si el
100% es porque ya no entran filas nuevas.

## Pregunta concreta

1. ¿Cual es la cadencia de SAPSYNCKILLIOS (instantaneo / batch /
   on-demand)?
2. ¿Sigue activo en el mes vigente?
3. ¿El writer de `enviado=1` es SP local, trigger, o externo?

## Que se espera del operador (Erik)

1. `Sync-WmsBrain` → `Show-WmsBrainQuestion -Id Q-011`.
2. `Invoke-WmsBrainQuestion -Id Q-011 -Profile K7-PRD`.
3. Si conoces el binario `SAPSYNCKILLIOS.exe` (host, version, log),
   anotalo en free-form.
4. `Submit-WmsBrainAnswer -Id Q-011`.

## Notas tecnicas

- Esta question es K7 lo que Q-001 fue para BB. Los resultados se
  comparan para validar/refinar L-012 (que es BB-only por ahora).
