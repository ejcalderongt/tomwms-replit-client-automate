---
id: db-brain-table-i-nav-barras-pallet
type: db-table
title: dbo.i_nav_barras_pallet
schema: dbo
name: i_nav_barras_pallet
kind: table
rows: 1056
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_barras_pallet`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.056 |
| Schema modify_date | 2025-05-05 |
| Columnas | 21 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPallet` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `Camas_Por_Tarima` | `int` | ✓ |  |
| 5 | `Cajas_Por_Cama` | `int` | ✓ |  |
| 6 | `Cantidad_Presentacion` | `float` | ✓ |  |
| 7 | `UM_Producto` | `nvarchar(50)` | ✓ |  |
| 8 | `Lote` | `nvarchar(50)` | ✓ |  |
| 9 | `Fecha_Agregado` | `datetime` | ✓ |  |
| 10 | `Fecha_Ingreso` | `date` | ✓ |  |
| 11 | `Fecha_Vence` | `date` | ✓ |  |
| 12 | `Fecha_Produccion` | `date` | ✓ |  |
| 13 | `Activo` | `bit` | ✓ |  |
| 14 | `Recibido` | `int` | ✓ |  |
| 15 | `IdRecepcion` | `int` | ✓ |  |
| 16 | `Bodega_Origen` | `nvarchar(50)` | ✓ |  |
| 17 | `Bodega_Destino` | `nvarchar(50)` | ✓ |  |
| 18 | `Codigo_Barra` | `nvarchar(50)` | ✓ |  |
| 19 | `Cantidad_UMP` | `float` | ✓ |  |
| 22 | `Lote_Numerico` | `float` | ✓ |  |
| 23 | `fecha_procesado_erp` | `datetime` |  |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

