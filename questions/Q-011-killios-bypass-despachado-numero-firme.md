---
protocolVersion: 2
id: Q-011
title: Killios - bypass estado=Despachado sin fila de despacho real (P-16b refinada)
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: high
status: pending
tags: [bypass, trans_pe_enc, trans_despacho_det, killios, ADR-012, pasada-8a, D-03]
targets:
  - codename: K7
    environment: PRD
    minRows: 0
relatedDocs:
  - brain/wms-specific-process-flow/queries-pasada-8a.md
  - brain/architecture/adr/ADR-012-bypass-estado-despachado.md
  - brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json
expectedOutputs:
  - Si bypass_sin_despacho_real coincide con 43, ADR-012 ratificado tal cual.
  - Si es muy mayor (200+), ADR-012 explicita volumetria y agrega rate-limit.
  - Si es ~0, simplificar ADR-012 a 'permitir con razon, sin permiso especial'.
  - Distribucion temporal: si bypass solo aparece pre-2025, es legacy. Si aparece todos los meses, uso continuo.
suggestedQueries:
  - id: q1-bypass-count-killios
    description: Total bypass + porcentaje sobre el universo de pedidos despachados
    sql: |
      WITH pedidos_marcados_despachados AS (
        SELECT IdPedidoEnc, fec_agr, fec_mod
        FROM dbo.trans_pe_enc
        WHERE estado = 'Despachado'
      ),
      pedidos_con_despacho_real AS (
        SELECT DISTINCT d.IdPedidoEnc
        FROM dbo.trans_despacho_det d
      )
      SELECT
        COUNT(*) AS pedidos_estado_despachado_total,
        SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) AS bypass_sin_despacho_real,
        SUM(CASE WHEN dr.IdPedidoEnc IS NOT NULL THEN 1 ELSE 0 END) AS con_despacho_real,
        CAST(
          100.0 * SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) / NULLIF(COUNT(*), 0)
          AS DECIMAL(5,2)
        ) AS pct_bypass
      FROM pedidos_marcados_despachados pm
      LEFT JOIN pedidos_con_despacho_real dr ON dr.IdPedidoEnc = pm.IdPedidoEnc;
  - id: q2-bypass-distribucion-temporal
    description: Distribucion temporal por mes para detectar si es legacy o uso continuo
    sql: |
      WITH pedidos_marcados_despachados AS (
        SELECT IdPedidoEnc, fec_agr, fec_mod
        FROM dbo.trans_pe_enc
        WHERE estado = 'Despachado'
      ),
      pedidos_con_despacho_real AS (
        SELECT DISTINCT d.IdPedidoEnc
        FROM dbo.trans_despacho_det d
      )
      SELECT TOP 36
        FORMAT(pm.fec_mod, 'yyyy-MM') AS mes,
        COUNT(*) AS bypass_count
      FROM pedidos_marcados_despachados pm
      LEFT JOIN pedidos_con_despacho_real dr ON dr.IdPedidoEnc = pm.IdPedidoEnc
      WHERE dr.IdPedidoEnc IS NULL
      GROUP BY FORMAT(pm.fec_mod, 'yyyy-MM')
      ORDER BY mes DESC;
---

# Q-011 - Killios - bypass estado=Despachado sin fila de despacho real (P-16b refinada)

## Contexto

Carol reporto 43 pedidos con estado=Despachado sin fila en trans_despacho_enc (P-16). ADR-012 (provisional) propone permitir el bypass con permiso+razon+auditoria. Esta query da el numero firme para calibrar el ADR antes de ratificar. Si el numero coincide con 43, ADR-012 sigue. Si es muy diferente, recalibrar volumetria.

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-pasada-8a.md`
- `brain/architecture/adr/ADR-012-bypass-estado-despachado.md`
- `brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json`

## Queries sugeridas (READ-ONLY)

### `q1-bypass-count-killios` - Total bypass + porcentaje sobre el universo de pedidos despachados

```sql
WITH pedidos_marcados_despachados AS (
  SELECT IdPedidoEnc, fec_agr, fec_mod
  FROM dbo.trans_pe_enc
  WHERE estado = 'Despachado'
),
pedidos_con_despacho_real AS (
  SELECT DISTINCT d.IdPedidoEnc
  FROM dbo.trans_despacho_det d
)
SELECT
  COUNT(*) AS pedidos_estado_despachado_total,
  SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) AS bypass_sin_despacho_real,
  SUM(CASE WHEN dr.IdPedidoEnc IS NOT NULL THEN 1 ELSE 0 END) AS con_despacho_real,
  CAST(
    100.0 * SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) / NULLIF(COUNT(*), 0)
    AS DECIMAL(5,2)
  ) AS pct_bypass
FROM pedidos_marcados_despachados pm
LEFT JOIN pedidos_con_despacho_real dr ON dr.IdPedidoEnc = pm.IdPedidoEnc;
```

### `q2-bypass-distribucion-temporal` - Distribucion temporal por mes para detectar si es legacy o uso continuo

```sql
WITH pedidos_marcados_despachados AS (
  SELECT IdPedidoEnc, fec_agr, fec_mod
  FROM dbo.trans_pe_enc
  WHERE estado = 'Despachado'
),
pedidos_con_despacho_real AS (
  SELECT DISTINCT d.IdPedidoEnc
  FROM dbo.trans_despacho_det d
)
SELECT TOP 36
  FORMAT(pm.fec_mod, 'yyyy-MM') AS mes,
  COUNT(*) AS bypass_count
FROM pedidos_marcados_despachados pm
LEFT JOIN pedidos_con_despacho_real dr ON dr.IdPedidoEnc = pm.IdPedidoEnc
WHERE dr.IdPedidoEnc IS NULL
GROUP BY FORMAT(pm.fec_mod, 'yyyy-MM')
ORDER BY mes DESC;
```

## Outputs esperados

- Si bypass_sin_despacho_real coincide con 43, ADR-012 ratificado tal cual.
- Si es muy mayor (200+), ADR-012 explicita volumetria y agrega rate-limit.
- Si es ~0, simplificar ADR-012 a 'permitir con razon, sin permiso especial'.
- Distribucion temporal: si bypass solo aparece pre-2025, es legacy. Si aparece todos los meses, uso continuo.

## Como ejecutar

```powershell
# Por cada target (K7-PRD):
Invoke-WmsBrainQuestion -Id Q-011 -Profile K7-PRD
```

Genera CSVs en `answers/Q-011/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Pasada 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-pasada-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-pasada-7.md sin requerir nueva intervencion humana.
