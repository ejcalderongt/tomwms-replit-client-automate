---
id: db-brain-table-stock-det
type: db-table
title: dbo.stock_det
schema: dbo
name: stock_det
kind: table
rows: 0
modify_date: 2021-06-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-06-02 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `posiciones` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_det` | CLUSTERED · **PK** | IdStock |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**7** objetos:

- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Valorizacion_OC` (view)

