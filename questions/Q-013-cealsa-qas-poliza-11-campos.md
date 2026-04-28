---
protocolVersion: 2
id: Q-013
title: CEALSA QAS - validar 11 campos obligatorios de poliza fiscal
createdBy: agent-replit
createdAt: 2026-04-28T20:30:00-03:00
priority: medium
status: pending
tags: [cealsa, poliza, trans_pe_pol, fiscal, ciclo-8a, sub-Q5]
targets:
  - codename: C9
    environment: QAS
    minRows: 0
relatedDocs:
  - brain/wms-specific-process-flow/queries-ciclo-8a.md
  - brain/wms-specific-process-flow/respuestas-ciclo-7.md
expectedOutputs:
  - Estructura confirma cuales son los 11 campos obligatorios reales (vs los que Carol describio).
  - Si pedidos_sin_poliza = 0, afirmacion validada. Si > 0, refinar sub-Q (cuales y por que).
suggestedQueries:
  - id: q1-estructura-poliza
    description: Estructura de columnas de trans_pe_pol (validar los 11 campos)
    sql: |
      SELECT
        c.name AS columna,
        t.name AS tipo,
        c.is_nullable,
        c.max_length
      FROM sys.columns c
      JOIN sys.tables tbl ON tbl.object_id = c.object_id
      JOIN sys.types t ON t.user_type_id = c.user_type_id
      WHERE tbl.name = 'trans_pe_pol'
      ORDER BY c.column_id;
  - id: q2-pedidos-fiscales-con-y-sin-poliza
    description: Conteo pedidos fiscales con poliza vs sin poliza
    sql: |
      SELECT
        COUNT(DISTINCT pe.IdPedidoEnc) AS pedidos_fiscales,
        COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_con_poliza,
        COUNT(DISTINCT pe.IdPedidoEnc) - COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_sin_poliza
      FROM dbo.trans_pe_enc pe
      LEFT JOIN dbo.trans_pe_pol pp ON pp.IdPedidoEnc = pe.IdPedidoEnc
      JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
      WHERE t.control_poliza = 1;
---

# Q-013 - CEALSA QAS - validar 11 campos obligatorios de poliza fiscal

## Contexto

Carol mencion en P-11 que los 11 campos de poliza son obligatorios para CEALSA. Esta query valida que ningun pedido fiscal este incompleto. Resultado define si la afirmacion 'todos los fiscales tienen poliza' se cumple en CEALSA QAS.

## Documentos relacionados

- `brain/wms-specific-process-flow/queries-ciclo-8a.md`
- `brain/wms-specific-process-flow/respuestas-ciclo-7.md`

## Queries sugeridas (READ-ONLY)

### `q1-estructura-poliza` - Estructura de columnas de trans_pe_pol (validar los 11 campos)

```sql
SELECT
  c.name AS columna,
  t.name AS tipo,
  c.is_nullable,
  c.max_length
FROM sys.columns c
JOIN sys.tables tbl ON tbl.object_id = c.object_id
JOIN sys.types t ON t.user_type_id = c.user_type_id
WHERE tbl.name = 'trans_pe_pol'
ORDER BY c.column_id;
```

### `q2-pedidos-fiscales-con-y-sin-poliza` - Conteo pedidos fiscales con poliza vs sin poliza

```sql
SELECT
  COUNT(DISTINCT pe.IdPedidoEnc) AS pedidos_fiscales,
  COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_con_poliza,
  COUNT(DISTINCT pe.IdPedidoEnc) - COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_sin_poliza
FROM dbo.trans_pe_enc pe
LEFT JOIN dbo.trans_pe_pol pp ON pp.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
WHERE t.control_poliza = 1;
```

## Outputs esperados

- Estructura confirma cuales son los 11 campos obligatorios reales (vs los que Carol describio).
- Si pedidos_sin_poliza = 0, afirmacion validada. Si > 0, refinar sub-Q (cuales y por que).

## Como ejecutar

```powershell
# Por cada target (C9-QAS):
Invoke-WmsBrainQuestion -Id Q-013 -Profile C9-QAS
```

Genera CSVs en `answers/Q-013/` y un draft `answer-draft.md` para revisar/completar.

## Origen

Esta card forma parte de la **Ciclo 8a SQL READ-ONLY autonoma** documentada en `brain/wms-specific-process-flow/queries-ciclo-8a.md` (rama `wms-brain`). Generada por el agente brain en sesion replit (28 abril 2026) para destrabar las sub-preguntas abiertas de la consolidacion-ciclo-7.md sin requerir nueva intervencion humana.
