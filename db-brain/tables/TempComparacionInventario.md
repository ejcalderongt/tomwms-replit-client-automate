---
id: db-brain-table-tempcomparacioninventario
type: db-table
title: dbo.TempComparacionInventario
schema: dbo
name: TempComparacionInventario
kind: table
rows: 7
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.TempComparacionInventario`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 7 |
| Schema modify_date | 2025-02-11 |
| Columnas | 21 |

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
| 14 | `Lote` | `nvarchar(50)` | ✓ |  |
| 15 | `FechaVence` | `date` | ✓ |  |
| 16 | `licencia` | `nvarchar(50)` | ✓ |  |
| 17 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 18 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |
| 19 | `UbicacionOrigen` | `nvarchar(50)` | ✓ |  |
| 20 | `UbicacionDestino` | `nvarchar(50)` | ✓ |  |
| 21 | `LoteOrigen` | `nvarchar(50)` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

