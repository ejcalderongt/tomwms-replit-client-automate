---
protocolVersion: 1
id: Q-009
title: Identificar binario y host del NavSync de BB (esta corriendo?)
createdBy: agent-replit
createdAt: 2026-04-27T19:00:00Z
priority: high
status: pending
tags: [navsync, BB, infra, PEND-07, follow-up-A-001]
targets:
  - codename: BB
    environment: PRD
relatedDocs:
  - brain/wms-brain-client/answers/A-001-cadencia-navsync.md
  - brain/learnings/L-012-navsync-solo-procesa-salidas.md
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
suggestedQueries:
  - id: q1-actividad-recente-30d
    description: Confirmar si hubo actividad en los ultimos 30 dias (cualquier UPDATE enviado=1)
    sql: |
      SELECT TOP 30
        CAST(fec_mod AS date) AS dia,
        COUNT(*) AS cnt_marcados
      FROM i_nav_transacciones_out
      WHERE enviado = 1
        AND fec_mod >= DATEADD(DAY, -30, GETDATE())
      GROUP BY CAST(fec_mod AS date)
      ORDER BY dia DESC;
  - id: q2-ultima-marca-por-tipo
    description: Ultima fecha de marca por tipo_transaccion+tipo_doc
    sql: |
      SELECT
        tipo_transaccion,
        IdTipoDocumento,
        MAX(fec_mod) AS ultimo_envio,
        COUNT(*) AS total_enviados
      FROM i_nav_transacciones_out
      WHERE enviado = 1
      GROUP BY tipo_transaccion, IdTipoDocumento
      ORDER BY ultimo_envio DESC;
  - id: q3-pendientes-recientes
    description: Pendientes con fec_agr en ultimos 7 dias
    sql: |
      SELECT
        tipo_transaccion,
        IdTipoDocumento,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 0
        AND fec_agr >= DATEADD(DAY, -7, GETDATE())
      GROUP BY tipo_transaccion, IdTipoDocumento
      ORDER BY cnt DESC;
expectedOutputs:
  - id: q1-actividad-recente-30d
    type: table
    columns: [dia, cnt_marcados]
  - id: q2-ultima-marca-por-tipo
    type: table
    columns: [tipo_transaccion, IdTipoDocumento, ultimo_envio, total_enviados]
  - id: q3-pendientes-recientes
    type: table
    columns: [tipo_transaccion, IdTipoDocumento, cnt]
followUp:
  ifFinding: Si q1 muestra 0 dias → NavSync detenido y hay que reiniciarlo. Si q2 muestra ultimo_envio reciente solo para algunos tipos → confirma que el binario filtra. Si q3 muestra altos pendientes recientes → backlog activo.
  thenAsk: Si NavSync esta detenido, abrir incidente operativo (no nueva question). Si filtra por tipo, abrir Q (config interna de NavSync).
estimatedTimeMinutes: 5
allowFreeFormNotes: true
---

## Contexto

A-001 mostro que NavSync de BB tiene cadencia historica
"instantanea post-insert" (99.48% en 0 seg), pero **0 filas
marcadas en los ultimos 30 dias**. Esto puede ser real (NavSync
caido) o ventana de consulta. Ademas el writer es externo al
motor SQL — necesitamos identificar el `.exe` o servicio Windows
que ejecuta NavSync.

## Pregunta concreta

1. ¿NavSync de BB esta corriendo hoy? (ultima actividad)
2. ¿Cual es el binario / servicio Windows que ejecuta NavSync de
   BB y en que host vive?
3. ¿NavSync filtra explicitamente por `IdTipoDocumento=3` o
   procesa todo lo que tenga `tipo_transaccion='SALIDA'`?

## Que se espera del operador (Erik)

1. `Sync-WmsBrain`.
2. `Show-WmsBrainQuestion -Id Q-009`.
3. `Invoke-WmsBrainQuestion -Id Q-009 -Profile BB-PRD`.
4. (Bonus, fuera del agente) en el host de BB ejecutar:
   - `Get-Service | Where-Object { $_.Name -like '*NavSync*' -or $_.Name -like '*WMS*' }`
   - `Get-ScheduledTask | Where-Object { $_.TaskName -like '*NavSync*' }`
5. Anotar nombre del binario, host, ultimo run en free-form notes.
6. `Submit-WmsBrainAnswer -Id Q-009 -Verdict confirmed -Confidence high`.

## Notas tecnicas

- Si q1 devuelve 0 filas → NavSync detenido (incidente).
- Si q2 muestra que solo tipo_doc=3 tiene `ultimo_envio` reciente
  → confirma filtro explicito en el binario (cierra L-012).
