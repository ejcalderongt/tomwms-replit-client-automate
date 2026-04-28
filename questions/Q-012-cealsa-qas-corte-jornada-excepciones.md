---
protocolVersion: 2
id: Q-012
title: CEALSA QAS - excepciones del corte de jornada (cruce dia)
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: medium
status: pending
tags: [cealsa, corte-jornada, trans_pe_enc, trans_despacho_enc, pasada-8a, sub-Q4]
targets:
  - codename: C9
    environment: QAS
    minRows: 0
relatedDocs:
  - brain/wms-specific-process-flow/queries-pasada-8a.md
  - brain/wms-specific-process-flow/respuestas-pasada-7.md
expectedOutputs:
  - Lista de pedidos que cruzaron jornada (las 'excepciones' que Carol mencion).
  - Distribucion: si la mayoria es 0-1 dias, normal. Si hay 3+ dias, son las excepciones reales.
suggestedQueries:
  - id: q1-pedidos-cruzan-jornada-detalle
    description: TOP 50 pedidos que cruzaron jornada con dias de diferencia
    sql: |
      SELECT TOP 50
        pe.IdPedidoEnc,
        pe.estado,
        pe.fec_agr AS creado,
        de.fec_agr AS despachado,
        DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_diferencia,
        pe.IdTipoPedido,
        t.Nombre AS tipo_nombre
      FROM dbo.trans_pe_enc pe
      JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
      JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
      JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
      WHERE DATEDIFF(day, pe.fec_agr, de.fec_agr) > 0
      ORDER BY dias_diferencia DESC, pe.fec_agr DESC;
  - id: q2-distribucion-dias-cruce
    description: Histograma de dias-de-cruce para detectar tipico vs anomalo
    sql: |
      SELECT
        DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_cruce,
        COUNT(*) AS pedidos
      FROM dbo.trans_pe_enc pe
      JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
      JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
      GROUP BY DATEDIFF(day, pe.fec_agr, de.fec_agr)
      ORDER BY dias_cruce;
---

# Q-012 - CEALSA QAS - excepciones del corte de jornada (cruce dia)

## Contexto

Carol mencion en P-22 que hay procesos con excepciones en el corte de jornada CEALSA pero no entro en detalle. Esta query reconstruye las excepciones via SQL: pedidos cuyo despacho cruza el dia (creados un dia, despachados otro).

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-pasada-8a.md`
- `brain/wms-specific-process-flow/respuestas-pasada-7.md`

## Queries sugeridas (READ-ONLY)

### `q1-pedidos-cruzan-jornada-detalle` - TOP 50 pedidos que cruzaron jornada con dias de diferencia

```sql
SELECT TOP 50
  pe.IdPedidoEnc,
  pe.estado,
  pe.fec_agr AS creado,
  de.fec_agr AS despachado,
  DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_diferencia,
  pe.IdTipoPedido,
  t.Nombre AS tipo_nombre
FROM dbo.trans_pe_enc pe
JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
WHERE DATEDIFF(day, pe.fec_agr, de.fec_agr) > 0
ORDER BY dias_diferencia DESC, pe.fec_agr DESC;
```

### `q2-distribucion-dias-cruce` - Histograma de dias-de-cruce para detectar tipico vs anomalo

```sql
SELECT
  DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_cruce,
  COUNT(*) AS pedidos
FROM dbo.trans_pe_enc pe
JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
GROUP BY DATEDIFF(day, pe.fec_agr, de.fec_agr)
ORDER BY dias_cruce;
```

## Outputs esperados

- Lista de pedidos que cruzaron jornada (las 'excepciones' que Carol mencion).
- Distribucion: si la mayoria es 0-1 dias, normal. Si hay 3+ dias, son las excepciones reales.

## Como ejecutar

```powershell
# Por cada target (C9-QAS):
Invoke-WmsBrainQuestion -Id Q-012 -Profile C9-QAS
```

Genera CSVs en `answers/Q-012/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Pasada 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-pasada-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-pasada-7.md sin requerir nueva intervencion humana.
