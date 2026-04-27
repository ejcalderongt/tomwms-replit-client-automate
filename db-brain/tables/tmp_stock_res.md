---
id: db-brain-table-tmp-stock-res
type: db-table
title: dbo.tmp_stock_res
schema: dbo
name: tmp_stock_res
kind: table
rows: 400
modify_date: 2018-10-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tmp_stock_res`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 400 |
| Schema modify_date | 2018-10-08 |
| Columnas | 33 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockRes` | `int` |  |  |
| 2 | `IdTransaccion` | `int` |  |  |
| 3 | `Indicador` | `nvarchar(50)` | ✓ |  |
| 4 | `IdPedidoDet` | `int` |  |  |
| 5 | `IdStock` | `int` |  |  |
| 6 | `IdPropietarioBodega` | `int` |  |  |
| 7 | `IdProductoBodega` | `int` |  |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacion` | `int` |  |  |
| 12 | `ubicacion_ant` | `nvarchar(25)` | ✓ |  |
| 13 | `IdRecepcion` | `bigint` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` | ✓ |  |
| 15 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 16 | `serial` | `nvarchar(50)` | ✓ |  |
| 17 | `cantidad` | `float` | ✓ |  |
| 18 | `peso` | `float` | ✓ |  |
| 19 | `estado` | `nvarchar(20)` | ✓ |  |
| 20 | `fecha_ingreso` | `datetime` | ✓ |  |
| 21 | `fecha_vence` | `datetime` | ✓ |  |
| 22 | `uds_lic_plate` | `float` | ✓ |  |
| 23 | `no_bulto` | `int` | ✓ |  |
| 24 | `IdPicking` | `bigint` | ✓ |  |
| 25 | `IdPedido` | `bigint` | ✓ |  |
| 26 | `IdDespacho` | `bigint` | ✓ |  |
| 27 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 28 | `fec_agr` | `datetime` | ✓ |  |
| 29 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 30 | `fec_mod` | `datetime` | ✓ |  |
| 31 | `host` | `nvarchar(50)` | ✓ |  |
| 32 | `añada` | `int` | ✓ |  |
| 33 | `fecha_manufactura` | `datetime` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

