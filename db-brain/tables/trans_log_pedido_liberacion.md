---
id: db-brain-table-trans-log-pedido-liberacion
type: db-table
title: dbo.trans_log_pedido_liberacion
schema: dbo
name: trans_log_pedido_liberacion
kind: table
rows: 715
modify_date: 2023-02-09
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_log_pedido_liberacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 715 |
| Schema modify_date | 2023-02-09 |
| Columnas | 23 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdLogLiberacionStock` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdPedidoEnc` | `int` | ✓ |  |
| 4 | `IdPedidoDet` | `int` | ✓ |  |
| 5 | `IdUsuario` | `int` | ✓ |  |
| 6 | `Fecha` | `datetime` | ✓ |  |
| 7 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 8 | `Lote` | `nvarchar(50)` | ✓ |  |
| 9 | `Lic_Plate` | `nvarchar(50)` | ✓ |  |
| 10 | `Fecha_Vence` | `datetime` | ✓ |  |
| 11 | `Cantidad` | `float` | ✓ |  |
| 12 | `Peso` | `float` | ✓ |  |
| 13 | `Referencia` | `nvarchar(50)` | ✓ |  |
| 14 | `Observacion` | `nvarchar(250)` | ✓ |  |
| 15 | `IdStock` | `int` | ✓ |  |
| 16 | `IdProductoBodega` | `int` | ✓ |  |
| 17 | `IdProductoEstado` | `int` | ✓ |  |
| 18 | `IdPropietarioBodega` | `int` | ✓ |  |
| 19 | `IdUnidadMedida` | `int` | ✓ |  |
| 20 | `IdUbicacion` | `int` | ✓ |  |
| 21 | `IdPickingUbic` | `int` | ✓ |  |
| 22 | `IdPickingDet` | `int` | ✓ |  |
| 23 | `IdPresentacion` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_log_pedido_liberacion` | CLUSTERED · **PK** | IdLogLiberacionStock |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

