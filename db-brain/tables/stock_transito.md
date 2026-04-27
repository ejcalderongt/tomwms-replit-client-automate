---
id: db-brain-table-stock-transito
type: db-table
title: dbo.stock_transito
schema: dbo
name: stock_transito
kind: table
rows: 6
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_transito`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2022-12-17 |
| Columnas | 28 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockTransito` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodegaOrigen` | `int` | ✓ |  |
| 4 | `IdBodegaDestino` | `int` | ✓ |  |
| 5 | `IdStock` | `int` | ✓ |  |
| 6 | `IdProductoBodegaDestino` | `int` | ✓ |  |
| 7 | `IdProductoBodegaOrigen` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacion` | `int` | ✓ |  |
| 12 | `IdRecepcionEnc` | `int` | ✓ |  |
| 13 | `IdRecepcionDet` | `int` | ✓ |  |
| 14 | `IdPedidoEnc` | `int` | ✓ |  |
| 15 | `IdPickingEnc` | `int` | ✓ |  |
| 16 | `IdDespachoEnc` | `int` | ✓ |  |
| 17 | `IdPickingUbic` | `int` | ✓ |  |
| 18 | `IdPedidoDet` | `int` | ✓ |  |
| 19 | `Lote` | `nvarchar(50)` | ✓ |  |
| 20 | `Lic_Plate` | `nvarchar(50)` | ✓ |  |
| 21 | `Cantidad` | `float` | ✓ |  |
| 22 | `Fecha_Ingreso` | `date` | ✓ |  |
| 23 | `Fecha_Vence` | `date` | ✓ |  |
| 24 | `Fecha_Manufactura` | `date` | ✓ |  |
| 25 | `Cantidad_Recibida` | `float` | ✓ |  |
| 26 | `Fecha_Agregado` | `date` | ✓ |  |
| 27 | `IdOrdenCompraEnc_BodDest` | `int` | ✓ |  |
| 28 | `IdRecepcionEnc_BodDest` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_transito` | CLUSTERED · **PK** | IdStockTransito |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

