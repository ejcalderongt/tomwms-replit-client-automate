---
id: db-brain-table-tempcomparativoinventario
type: db-table
title: dbo.TempComparativoInventario
schema: dbo
name: TempComparativoInventario
kind: table
rows: 0
modify_date: 2018-09-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.TempComparativoInventario`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-09-21 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdInventario` | `int` | ✓ |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `IdProducto` | `int` | ✓ |  |
| 4 | `IdUnidadMedida` | `int` | ✓ |  |
| 5 | `Codigo` | `nvarchar(150)` | ✓ |  |
| 6 | `Producto` | `nvarchar(250)` | ✓ |  |
| 7 | `Cantidad_Stock` | `float` | ✓ |  |
| 8 | `Cantidad` | `float` | ✓ |  |
| 9 | `Peso_Stock` | `float` | ✓ |  |
| 10 | `Peso` | `float` | ✓ |  |
| 11 | `Entradas_Salidas` | `float` | ✓ |  |
| 12 | `Entradas` | `float` | ✓ |  |
| 13 | `Salidas` | `float` | ✓ |  |
| 14 | `Lote` | `nvarchar(200)` | ✓ |  |
| 15 | `FechaVence` | `date` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

