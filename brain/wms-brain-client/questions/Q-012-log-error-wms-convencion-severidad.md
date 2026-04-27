---
protocolVersion: 1
id: Q-012
title: Convencion de severidad / prefijos en log_error_wms (PEND-11 follow-up)
createdBy: agent-replit
createdAt: 2026-04-27T19:00:00Z
priority: medium
status: pending
tags: [errores, log, K7, BB, monitoreo, PEND-11, follow-up-A-004]
targets:
  - codename: K7
    environment: PRD
  - codename: BB
    environment: PRD
relatedDocs:
  - brain/wms-brain-client/answers/A-004-log-error-wms.md
  - brain/learnings/L-011-log-error-wms-es-bitacora.md
suggestedQueries:
  - id: q1-prefijos-en-uso
    description: Top prefijos (primeros 30 chars hasta primer ':') en MensajeError
    sql: |
      SELECT TOP 50
        LEFT(MensajeError, CASE WHEN CHARINDEX(':', MensajeError) > 0 THEN CHARINDEX(':', MensajeError) - 1 ELSE 30 END) AS prefijo,
        COUNT(*) AS cnt
      FROM log_error_wms
      WHERE MensajeError IS NOT NULL
      GROUP BY LEFT(MensajeError, CASE WHEN CHARINDEX(':', MensajeError) > 0 THEN CHARINDEX(':', MensajeError) - 1 ELSE 30 END)
      ORDER BY cnt DESC;
  - id: q2-clasificacion-aviso-vs-error
    description: Conteo por categoria semantica (AVISO_*, Error_*, *_HH:, otros)
    sql: |
      SELECT
        CASE
          WHEN MensajeError LIKE 'AVISO_%' THEN 'AVISO'
          WHEN MensajeError LIKE 'Error_%' THEN 'Error_marcado'
          WHEN MensajeError LIKE '%_HH:%' THEN 'Traza_HH'
          WHEN MensajeError LIKE '%Referencia a objeto%' THEN 'NullReferenceException'
          WHEN MensajeError LIKE '%Picking_Anulado%' THEN 'Estado_Picking'
          ELSE 'OTRO'
        END AS categoria,
        COUNT(*) AS cnt
      FROM log_error_wms
      GROUP BY
        CASE
          WHEN MensajeError LIKE 'AVISO_%' THEN 'AVISO'
          WHEN MensajeError LIKE 'Error_%' THEN 'Error_marcado'
          WHEN MensajeError LIKE '%_HH:%' THEN 'Traza_HH'
          WHEN MensajeError LIKE '%Referencia a objeto%' THEN 'NullReferenceException'
          WHEN MensajeError LIKE '%Picking_Anulado%' THEN 'Estado_Picking'
          ELSE 'OTRO'
        END
      ORDER BY cnt DESC;
  - id: q3-fk-vs-mensaje-correlation
    description: De los mensajes que mencionan picking/recepcion, cuantos rellenan la FK correspondiente
    sql: |
      SELECT
        CASE WHEN MensajeError LIKE '%Picking%' THEN 'menciona_picking' ELSE 'no' END AS menciona,
        SUM(CASE WHEN IdPickingEnc > 0 THEN 1 ELSE 0 END) AS con_fk_picking,
        COUNT(*) AS total
      FROM log_error_wms
      GROUP BY CASE WHEN MensajeError LIKE '%Picking%' THEN 'menciona_picking' ELSE 'no' END;
expectedOutputs:
  - id: q1-prefijos-en-uso
    type: table
    columns: [prefijo, cnt]
  - id: q2-clasificacion-aviso-vs-error
    type: table
    columns: [categoria, cnt]
followUp:
  ifFinding: Si q1 muestra <20 prefijos cubriendo 80% del log → la convencion de prefijo es factible. Si q3 muestra que el codigo no rellena FK aun cuando menciona el modulo → bug a corregir.
  thenAsk: Definir nuevo doc `log_error_wms_convention.md` con prefijos formales y abrir Q sobre vista `VW_LogErrorReal`.
estimatedTimeMinutes: 5
allowFreeFormNotes: true
---

## Contexto

A-004 / L-011 mostraron que `log_error_wms` mezcla avisos
(`AVISO_*`), trazas (`Aplica_*_HH:`) y errores reales sin
columna de severidad. K7 acumula ~16k filas/mes y BB tiene
204k historicas. El monitoreo automatizado **no puede usar
COUNT** porque siempre dispararia.

Necesitamos consolidar:
1. Que prefijos estan ya en uso (de facto)
2. Que % corresponde a cada categoria semantica
3. Si el codigo de los SPs usa correctamente las FK
   (`IdPedidoEnc`, `IdPickingEnc`, `IdRecepcionEnc`) o si todo
   queda enterrado en el texto de `MensajeError`

## Pregunta concreta

1. ¿Cuales son los prefijos top (cubren 80% del log)?
2. ¿Como se distribuye el log entre avisos vs trazas vs errores
   reales?
3. ¿Las FK reflejan el modulo o estan en NULL aunque el mensaje
   lo mencione?

## Que se espera del operador (Erik)

1. `Sync-WmsBrain` → `Show-WmsBrainQuestion -Id Q-012`.
2. `Invoke-WmsBrainQuestion -Id Q-012 -Profile K7-PRD`.
3. (Bonus) repetir contra BB-PRD para validar consistencia.
4. Anotar propuesta de prefijos formales (ej.
   `[ERR]/[WARN]/[INFO]/[TRACE]`) en free-form.
5. `Submit-WmsBrainAnswer -Id Q-012`.

## Notas tecnicas

- Output esperado: short list de ~20 prefijos cubriendo 80%.
- Si la categoria 'OTRO' es >50%, refinar las clausulas WHEN del
  q2 antes de cerrar.
