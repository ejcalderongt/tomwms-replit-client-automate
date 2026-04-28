---
protocolVersion: 2
id: Q-010
title: Killios - trans_reabastecimiento_log pre-2024 vs post-2024
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: medium
status: pending
tags: [reabasto, trans_reabastecimiento_log, killios, limpieza-instalacion, pasada-8a, C-04]
targets:
  - codename: K7
    environment: PRD
    minRows: 1218
relatedDocs:
  - brain/wms-specific-process-flow/queries-pasada-8a.md
  - brain/_inbox/20260428-1901-H02-clbd-prc-falta-reabasto-log.json
expectedOutputs:
  - Si todo es pre-2024, Carol gana. SP CLBD_PRC arregla el caso.
  - Si volumen significativo post-2024, modulo activo. Decision adicional: apagar deteccion automatica.
suggestedQueries:
  - id: q1-reabasto-por-periodo
    description: Conteo agrupado por periodo + rango temporal + cardinalidad
    sql: |
      SELECT
        CASE
          WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
          WHEN fec_agr < '2025-01-01' THEN '2024'
          ELSE '2025+'
        END AS periodo,
        COUNT(*) AS filas,
        MIN(fec_agr) AS mas_vieja,
        MAX(fec_agr) AS mas_nueva,
        COUNT(DISTINCT IdProductoConfigInfoCli) AS productos_distintos,
        COUNT(DISTINCT IdUbicacion) AS ubicaciones_distintas
      FROM dbo.trans_reabastecimiento_log
      GROUP BY
        CASE
          WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
          WHEN fec_agr < '2025-01-01' THEN '2024'
          ELSE '2025+'
        END
      ORDER BY periodo;
---

# Q-010 - Killios - trans_reabastecimiento_log pre-2024 vs post-2024

## Contexto

Killios tiene 1218 filas en trans_reabastecimiento_log aunque NO usa el modulo. Carol (P-24) dice que es basura no limpiada en instalacion. SQL agente (tanda-2) sospecha que el modulo de deteccion sigue activo. Esta query separa pre-2024 (basura instalacion) de post-2024 (modulo activo). Define si la accion atomica del SP CLBD_PRC alcanza, o si hay que apagar el modulo.

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-pasada-8a.md`
- `brain/_inbox/20260428-1901-H02-clbd-prc-falta-reabasto-log.json`

## Queries sugeridas (READ-ONLY)

### `q1-reabasto-por-periodo` - Conteo agrupado por periodo + rango temporal + cardinalidad

```sql
SELECT
  CASE
    WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
    WHEN fec_agr < '2025-01-01' THEN '2024'
    ELSE '2025+'
  END AS periodo,
  COUNT(*) AS filas,
  MIN(fec_agr) AS mas_vieja,
  MAX(fec_agr) AS mas_nueva,
  COUNT(DISTINCT IdProductoConfigInfoCli) AS productos_distintos,
  COUNT(DISTINCT IdUbicacion) AS ubicaciones_distintas
FROM dbo.trans_reabastecimiento_log
GROUP BY
  CASE
    WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
    WHEN fec_agr < '2025-01-01' THEN '2024'
    ELSE '2025+'
  END
ORDER BY periodo;
```

## Outputs esperados

- Si todo es pre-2024, Carol gana. SP CLBD_PRC arregla el caso.
- Si volumen significativo post-2024, modulo activo. Decision adicional: apagar deteccion automatica.

## Como ejecutar

```powershell
# Por cada target (K7-PRD):
Invoke-WmsBrainQuestion -Id Q-010 -Profile K7-PRD
```

Genera CSVs en `answers/Q-010/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Pasada 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-pasada-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-pasada-7.md sin requerir nueva intervencion humana.
