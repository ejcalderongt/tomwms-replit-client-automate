---
protocolVersion: 2
id: Q-009
title: Outbox - alcance real de tipos de transaccion en las 3 BDs
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: high
status: pending
tags: [outbox, i_nav_transacciones_out, schema-confirmation, pasada-8a, C-03]
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
  - brain/wms-specific-process-flow/consolidacion-pasada-7.md
  - brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json
expectedOutputs:
  - Si con_pedido y con_oc son cero o casi cero en las 3 BDs, Carol confirma. Bridge se simplifica.
  - Si distinto de cero en alguna BD, hay uso silencioso de los 4 tipos. Bridge debe soportar todos.
suggestedQueries:
  - id: q1-outbox-counts-por-tipo
    description: Conteo de outbox rows con cada FK populated, por BD
    sql: |
      SELECT
        COUNT(CASE WHEN idordencompra   IS NOT NULL AND idordencompra   <> 0 THEN 1 END) AS con_oc,
        COUNT(CASE WHEN idrecepcionenc  IS NOT NULL AND idrecepcionenc  <> 0 THEN 1 END) AS con_recepcion,
        COUNT(CASE WHEN idpedidoenc     IS NOT NULL AND idpedidoenc     <> 0 THEN 1 END) AS con_pedido,
        COUNT(CASE WHEN iddespachoenc   IS NOT NULL AND iddespachoenc   <> 0 THEN 1 END) AS con_despacho,
        COUNT(*)                                                                          AS total
      FROM dbo.i_nav_transacciones_out;
---

# Q-009 - Outbox - alcance real de tipos de transaccion en las 3 BDs

## Contexto

Carol (pasada-7, P-19) afirma que el outbox solo se usa para recepciones y despachos. El schema soporta 4 tipos (idordencompra, idrecepcionenc, idpedidoenc, iddespachoenc). Esta query confirma cuales se usan realmente en cada cliente. Resultado define el alcance del bridge del WebAPI nuevo (puede simplificarse si Carol tiene razon).

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-pasada-8a.md`
- `brain/wms-specific-process-flow/consolidacion-pasada-7.md`
- `brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json`

## Queries sugeridas (READ-ONLY)

### `q1-outbox-counts-por-tipo` - Conteo de outbox rows con cada FK populated, por BD

```sql
SELECT
  COUNT(CASE WHEN idordencompra   IS NOT NULL AND idordencompra   <> 0 THEN 1 END) AS con_oc,
  COUNT(CASE WHEN idrecepcionenc  IS NOT NULL AND idrecepcionenc  <> 0 THEN 1 END) AS con_recepcion,
  COUNT(CASE WHEN idpedidoenc     IS NOT NULL AND idpedidoenc     <> 0 THEN 1 END) AS con_pedido,
  COUNT(CASE WHEN iddespachoenc   IS NOT NULL AND iddespachoenc   <> 0 THEN 1 END) AS con_despacho,
  COUNT(*)                                                                          AS total
FROM dbo.i_nav_transacciones_out;
```

## Outputs esperados

- Si con_pedido y con_oc son cero o casi cero en las 3 BDs, Carol confirma. Bridge se simplifica.
- Si distinto de cero en alguna BD, hay uso silencioso de los 4 tipos. Bridge debe soportar todos.

## Como ejecutar

```powershell
# Por cada target (K7-PRD | BB-PRD | C9-QAS):
Invoke-WmsBrainQuestion -Id Q-009 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile C9-QAS
```

Genera CSVs en `answers/Q-009/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Pasada 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-pasada-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-pasada-7.md sin requerir nueva intervencion humana.
