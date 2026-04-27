---
id: db-brain-table-trans-oc-embarcador
type: db-table
title: dbo.trans_oc_embarcador
schema: dbo
name: trans_oc_embarcador
kind: table
rows: 0
modify_date: 2022-03-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_embarcador`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-03-07 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmbarcador` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(150)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_oc_embarcador` | CLUSTERED · **PK** | IdEmbarcador |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**5** objetos:

- `VW_Fiscal_historico` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)

