---
id: db-brain-table-stock-res
type: db-table
title: dbo.stock_res
schema: dbo
name: stock_res
kind: table
rows: 454
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_res`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 454 |
| Schema modify_date | 2023-08-21 |
| Columnas | 35 |
| Índices | 3 |

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
| 17 | `cantidad` | `float` |  |  |
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
| 34 | `IdBodega` | `int` |  |  |
| 35 | `pallet_no_estandar` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_pe` | CLUSTERED · **PK** | IdStockRes |
| `NCLI_stock_res_EJC20220510` | NONCLUSTERED | cantidad, peso, fecha_vence, IdPedido, host, IdBodega, IdPedidoDet, IdStock, IdProductoEstado, IdPresentacion, IdUbicacion, lote, lic_plate, IdUnidadMedida |
| `IX_STOCK_RES_20220619` | NONCLUSTERED | IdTransaccion, IdPedidoDet, IdStock, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, ubicacion_ant, IdRecepcion, lote, lic_plate, serial, cantidad, peso, estado, fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto, IdPicking, IdDespacho, user_agr, fec_agr, user_mod, fec_mod, host, añada, fecha_manufactura, pallet_no_estandar, Indicador, IdBodega, IdPedido |

## Check constraints

- `Cons_restriccion`: `([cantidad]>(0))`

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**29** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `Cons_restriccion` (check_constraint)
- `GetResumenStockCantidad` (stored_procedure)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_Revision_Producto` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Enc` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Res_Pedido` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_V1` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)
- `VW_Valorizacion_OC` (view)

