---
protocolVersion: 2
id: Q-014
title: TOP15 real de tipos de tarea HH ejecutadas en las 3 BDs
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: low
status: pending
tags: [tareas-hh, tarea_hh, sis_tipo_tarea, ranking, pasada-8a, P-25]
targets:
  - codename: K7
    environment: PRD
    minRows: 0
  - codename: BB
    environment: PRD
    minRows: 0
  - codename: C9
    environment: QAS
    minRows: 0
relatedDocs:
  - brain/wms-specific-process-flow/queries-pasada-8a.md
  - brain/wms-specific-process-flow/respuestas-tanda-2.md
expectedOutputs:
  - TOP15 real por BD. Si coincide con la lista de Carol, validado.
  - Si difiere mucho, Carol describio teorico no real. Documentar discrepancia.
suggestedQueries:
  - id: q1-top15-tipos-tarea
    description: TOP15 tipos de tarea por cantidad ejecutada con porcentaje
    sql: |
      SELECT TOP 15
        st.IdTipoTarea,
        st.Nombre AS tipo_tarea,
        st.Contabilizar,
        COUNT(*) AS ejecutadas,
        CAST(100.0 * COUNT(*) / SUM(COUNT(*)) OVER () AS DECIMAL(5,2)) AS pct
      FROM dbo.tarea_hh th
      JOIN dbo.sis_tipo_tarea st ON st.IdTipoTarea = th.IdTipoTarea
      GROUP BY st.IdTipoTarea, st.Nombre, st.Contabilizar
      ORDER BY ejecutadas DESC;
---

# Q-014 - TOP15 real de tipos de tarea HH ejecutadas en las 3 BDs

## Contexto

Tanda-2 P-25 dejo este JOIN pendiente. Carol describio el TOP10 teorico (Recepcion, Cambio Ubicacion, Cambio Estado, Implosiones, Picking, Verificacion, Despacho). Esta query confirma o refuta el TOP real ejecutado en las 3 BDs.

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-pasada-8a.md`
- `brain/wms-specific-process-flow/respuestas-tanda-2.md`

## Queries sugeridas (READ-ONLY)

### `q1-top15-tipos-tarea` - TOP15 tipos de tarea por cantidad ejecutada con porcentaje

```sql
SELECT TOP 15
  st.IdTipoTarea,
  st.Nombre AS tipo_tarea,
  st.Contabilizar,
  COUNT(*) AS ejecutadas,
  CAST(100.0 * COUNT(*) / SUM(COUNT(*)) OVER () AS DECIMAL(5,2)) AS pct
FROM dbo.tarea_hh th
JOIN dbo.sis_tipo_tarea st ON st.IdTipoTarea = th.IdTipoTarea
GROUP BY st.IdTipoTarea, st.Nombre, st.Contabilizar
ORDER BY ejecutadas DESC;
```

## Outputs esperados

- TOP15 real por BD. Si coincide con la lista de Carol, validado.
- Si difiere mucho, Carol describio teorico no real. Documentar discrepancia.

## Como ejecutar

```powershell
# Por cada target (K7-PRD | BB-PRD | C9-QAS):
Invoke-WmsBrainQuestion -Id Q-014 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile C9-QAS
```

Genera CSVs en `answers/Q-014/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Pasada 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-pasada-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-pasada-7.md sin requerir nueva intervencion humana.
