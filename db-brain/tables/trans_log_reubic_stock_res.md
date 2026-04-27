---
id: db-brain-table-trans-log-reubic-stock-res
type: db-table
title: dbo.trans_log_reubic_stock_res
schema: dbo
name: trans_log_reubic_stock_res
kind: table
rows: 84
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_log_reubic_stock_res`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 84 |
| Schema modify_date | 2023-08-21 |
| Columnas | 25 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdLogReubicStockRes` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdPickingUbic` | `int` | ✓ |  |
| 4 | `IdPickingDet` | `int` | ✓ |  |
| 5 | `IdPedidoEnc` | `int` | ✓ |  |
| 6 | `IdPedidoDet` | `int` | ✓ |  |
| 7 | `IdStock` | `int` | ✓ |  |
| 8 | `IdStockRes` | `int` | ✓ |  |
| 9 | `IdUbicacion` | `int` | ✓ |  |
| 10 | `IdUsuario` | `int` | ✓ |  |
| 11 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 12 | `Lote` | `nvarchar(50)` | ✓ |  |
| 13 | `Lic_Plate` | `nvarchar(50)` | ✓ |  |
| 14 | `Fecha_Vence` | `datetime` | ✓ |  |
| 15 | `Cantidad` | `float` | ✓ |  |
| 16 | `Peso` | `float` | ✓ |  |
| 17 | `Referencia` | `nvarchar(50)` | ✓ |  |
| 18 | `Observacion` | `nvarchar(250)` | ✓ |  |
| 19 | `IdProductoBodega` | `int` | ✓ |  |
| 20 | `IdProductoEstado` | `int` | ✓ |  |
| 21 | `IdPropietarioBodega` | `int` | ✓ |  |
| 22 | `IdUnidadMedida` | `int` | ✓ |  |
| 23 | `IdPresentacion` | `int` | ✓ |  |
| 24 | `Fecha_Sistema` | `datetime` | ✓ |  |
| 25 | `IdUbicacionAnterior` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_log_reubic_stock_res` | CLUSTERED · **PK** | IdLogReubicStockRes |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

