---
id: db-brain-table-trans-picking-ubic-stock
type: db-table
title: dbo.trans_picking_ubic_stock
schema: dbo
name: trans_picking_ubic_stock
kind: table
rows: 20437
modify_date: 2025-04-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_ubic_stock`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 20.437 |
| Schema modify_date | 2025-04-21 |
| Columnas | 47 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbicStock` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdPickingUbic` | `int` |  |  |
| 4 | `IdPickingDet` | `int` |  |  |
| 5 | `IdUbicacion` | `int` | ✓ |  |
| 6 | `IdStock` | `int` | ✓ |  |
| 7 | `IdStockRes` | `int` |  |  |
| 8 | `IdPropietarioBodega` | `int` | ✓ |  |
| 9 | `IdProductoBodega` | `int` | ✓ |  |
| 10 | `IdProductoEstado` | `int` | ✓ |  |
| 11 | `IdPresentacion` | `int` | ✓ |  |
| 12 | `IdUnidadMedida` | `int` | ✓ |  |
| 13 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 14 | `IdRecepcion` | `bigint` | ✓ |  |
| 15 | `IdPedidoEnc` | `int` | ✓ |  |
| 16 | `IdPedidoDet` | `int` | ✓ |  |
| 17 | `idpickingenc` | `int` | ✓ |  |
| 18 | `IdOperadorBodega` | `int` | ✓ |  |
| 19 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 20 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 21 | `lote` | `nvarchar(35)` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `fecha_minima` | `datetime` | ✓ |  |
| 24 | `serial` | `nvarchar(35)` | ✓ |  |
| 25 | `licencia` | `nvarchar(50)` | ✓ |  |
| 26 | `cantidad_recibida` | `float` | ✓ |  |
| 27 | `cantidad_verificada` | `float` | ✓ |  |
| 28 | `peso_pickeado` | `float` | ✓ |  |
| 29 | `peso_verificado` | `float` | ✓ |  |
| 30 | `fecha_picking` | `datetime` | ✓ |  |
| 31 | `fecha_verificado` | `datetime` | ✓ |  |
| 32 | `fecha_despachado` | `datetime` | ✓ |  |
| 33 | `cantidad_despachada` | `float` | ✓ |  |
| 34 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 35 | `fec_agr` | `datetime` | ✓ |  |
| 36 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 37 | `fec_mod` | `datetime` | ✓ |  |
| 38 | `activo` | `bit` | ✓ |  |
| 39 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 40 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |
| 41 | `IdRecepcionDet` | `int` | ✓ |  |
| 42 | `procesado_bof` | `bit` |  |  |
| 43 | `IdUsuario_bof_pickeo` | `int` | ✓ |  |
| 44 | `fecha_procesado_bof` | `date` | ✓ |  |
| 45 | `cantidad_pickeada` | `float` | ✓ |  |
| 46 | `host` | `nvarchar(50)` | ✓ |  |
| 47 | `IdMovimiento` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_ubic_stock` | CLUSTERED · **PK** | IdPickingUbicStock |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

