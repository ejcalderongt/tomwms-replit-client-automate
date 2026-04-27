---
id: db-brain-table-trans-inv-teorico-erp
type: db-table
title: dbo.trans_inv_teorico_erp
schema: dbo
name: trans_inv_teorico_erp
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_teorico_erp`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinvteoricoerp` | `int` |  |  |
| 2 | `idProducto` | `int` |  |  |
| 3 | `idPresentacion` | `int` |  |  |
| 4 | `cant` | `float` | ✓ |  |
| 5 | `peso` | `float` | ✓ |  |
| 6 | `idUnidadMedida` | `int` | ✓ |  |
| 7 | `lote` | `nvarchar(50)` | ✓ |  |
| 8 | `fecha_vence` | `datetime` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `idbodega` | `int` | ✓ |  |
| 11 | `idubicacion` | `int` | ✓ |  |
| 12 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 13 | `codigo_area` | `nvarchar(10)` | ✓ |  |
| 14 | `fecha_agr` | `datetime` | ✓ |  |
| 15 | `usuario_agr` | `nvarchar(25)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_teorico_erp` | CLUSTERED · **PK** | idinvteoricoerp |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

