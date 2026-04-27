---
id: db-brain-table-arancel
type: db-table
title: dbo.arancel
schema: dbo
name: arancel
kind: table
rows: 3
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.arancel`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2018-01-11 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdArancel` | `int` |  |  |
| 2 | `nombre` | `nvarchar(150)` | ✓ |  |
| 3 | `porcentaje` | `float` | ✓ |  |
| 4 | `fec_agr` | `datetime` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_mod` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Arancel` | CLUSTERED · **PK** | IdArancel |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_Arancel`)
- `trans_oc_det` (`FK_trans_orden_compra_det_Arancel`)

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `VW_ProductoSI` (view)
- `VW_RecepcionCostoArancel` (view)

