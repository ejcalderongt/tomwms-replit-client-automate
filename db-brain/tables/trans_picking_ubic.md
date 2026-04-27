---
id: db-brain-table-trans-picking-ubic
type: db-table
title: dbo.trans_picking_ubic
schema: dbo
name: trans_picking_ubic
kind: table
rows: 26567
modify_date: 2025-07-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_ubic`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 26.567 |
| Schema modify_date | 2025-07-16 |
| Columnas | 52 |
| Índices | 14 |
| FKs | out:2 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbic` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdPickingDet` | `int` |  |  |
| 4 | `IdUbicacion` | `int` | ✓ |  |
| 5 | `IdStock` | `int` | ✓ |  |
| 6 | `IdPropietarioBodega` | `int` | ✓ |  |
| 7 | `IdProductoBodega` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacionAnterior` | `int` | ✓ |  |
| 12 | `IdRecepcion` | `bigint` | ✓ |  |
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha_vence` | `datetime` | ✓ |  |
| 15 | `fecha_minima` | `datetime` | ✓ |  |
| 16 | `serial` | `nvarchar(35)` | ✓ |  |
| 17 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 18 | `acepto` | `bit` | ✓ |  |
| 19 | `peso_solicitado` | `float` | ✓ |  |
| 20 | `peso_recibido` | `float` | ✓ |  |
| 21 | `peso_verificado` | `float` | ✓ |  |
| 22 | `peso_despachado` | `float` | ✓ |  |
| 23 | `cantidad_solicitada` | `float` | ✓ |  |
| 24 | `cantidad_recibida` | `float` | ✓ |  |
| 25 | `cantidad_verificada` | `float` | ✓ |  |
| 26 | `encontrado` | `bit` | ✓ |  |
| 27 | `dañado_verificacion` | `bit` | ✓ |  |
| 28 | `fecha_real_vence` | `datetime` | ✓ |  |
| 29 | `no_packing` | `nvarchar(50)` | ✓ |  |
| 30 | `fecha_picking` | `datetime` | ✓ |  |
| 31 | `fecha_verificado` | `datetime` | ✓ |  |
| 32 | `fecha_packing` | `datetime` | ✓ |  |
| 33 | `fecha_despachado` | `datetime` | ✓ |  |
| 34 | `cantidad_despachada` | `float` | ✓ |  |
| 35 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 36 | `fec_agr` | `datetime` | ✓ |  |
| 37 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 38 | `fec_mod` | `datetime` | ✓ |  |
| 39 | `activo` | `bit` | ✓ |  |
| 40 | `IdPedidoDet` | `int` | ✓ |  |
| 41 | `dañado_picking` | `bit` | ✓ |  |
| 42 | `IdStockRes` | `int` |  |  |
| 43 | `lic_plate_reemplazo` | `nvarchar(50)` | ✓ |  |
| 44 | `IdUbicacion_reemplazo` | `int` | ✓ |  |
| 45 | `IdStock_reemplazo` | `int` | ✓ |  |
| 46 | `IdBodega` | `int` | ✓ |  |
| 48 | `IdOperadorBodega_Pickeo` | `int` | ✓ |  |
| 49 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 50 | `IdPedidoEnc` | `int` | ✓ |  |
| 51 | `no_encontrado` | `bit` |  |  |
| 52 | `IdUbicacionTemporal` | `int` | ✓ |  |
| 53 | `IdOperadorBodega_Asignado` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_ubic` | CLUSTERED · **PK** | IdPickingUbic |
| `NCLI_Trans_Picking_Ubic_IdPickingDet_EJC` | NONCLUSTERED | IdPickingDet |
| `NCLI_trans_picking_ubic_20210908_EJC` | NONCLUSTERED | IdPickingUbic, IdPickingDet, IdUbicacion, IdStock, IdPropietarioBodega, IdPresentacion, IdUnidadMedida, IdRecepcion, lote, fecha_vence, acepto, peso_recibido, peso_verificado, cantidad_recibida, cantidad_verificada, encontrado, IdPedidoDet, IdProductoBodega, IdProductoEstado |
| `NCLI_Trans_Picking_Ubic_EJC_20220403` | NONCLUSTERED | IdPickingEnc, IdPickingDet, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdRecepcion, lote, fecha_vence, fecha_minima, lic_plate, peso_solicitado, peso_recibido, peso_verificado, peso_despachado, cantidad_solicitada, cantidad_recibida, cantidad_verificada, encontrado, dañado_verificacion, fecha_real_vence, no_packing, fecha_picking, fecha_verificado, fecha_packing, fecha_despachado, cantidad_despachada, user_agr, fec_agr, user_mod, fec_mod, activo, dañado_picking, lic_plate_reemplazo, IdUbicacion_reemplazo, IdStock_reemplazo, IdBodega, IdOperadorBodega_Pickeo, no_encontrado, IdUbicacionTemporal, IdUbicacion, IdStockRes |
| `NCLI_Trans_Picking_ubic_202210051625_EJC` | NONCLUSTERED | IdPickingDet, IdUbicacion, IdStock, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, lote, fecha_vence, lic_plate, cantidad_recibida, cantidad_verificada, fecha_picking, cantidad_despachada, IdBodega, dañado_verificacion, dañado_picking, no_encontrado |
| `NCLI_trans_picking_ubic_202210051634` | NONCLUSTERED | IdPickingDet, IdUbicacion, cantidad_solicitada, cantidad_recibida, cantidad_verificada, cantidad_despachada, IdStockRes, dañado_verificacion, dañado_picking, no_encontrado |
| `NCLI_trans_picking_ubic_202210051635_EJC` | NONCLUSTERED | IdPickingDet, cantidad_solicitada, cantidad_recibida, cantidad_verificada, cantidad_despachada, IdUbicacion, dañado_verificacion, dañado_picking, IdStockRes, no_encontrado |
| `NCLI_Trans_Picking_Ubic_202308081128_EJC` | NONCLUSTERED | IdPickingDet, IdUbicacion, IdStock, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacionAnterior, IdRecepcion, lote, fecha_vence, fecha_minima, serial, lic_plate, acepto, peso_solicitado, peso_recibido, peso_verificado, peso_despachado, cantidad_solicitada, cantidad_recibida, cantidad_verificada, encontrado, dañado_verificacion, fecha_real_vence, no_packing, fecha_picking, fecha_verificado, fecha_packing, fecha_despachado, cantidad_despachada, user_agr, fec_agr, user_mod, fec_mod, activo, IdPedidoDet, dañado_picking, IdStockRes, lic_plate_reemplazo, IdUbicacion_reemplazo, IdStock_reemplazo, IdBodega, IdOperadorBodega_Pickeo, IdOperadorBodega_Verifico, IdPedidoEnc, no_encontrado, IdUbicacionTemporal, IdOperadorBodega_Asignado, IdPickingEnc |
| `NCLI_Trans_Picking_Ubic_20240122_EJC` | NONCLUSTERED | IdPickingEnc, IdPropietarioBodega, IdProductoBodega, IdPresentacion, IdUnidadMedida, lic_plate, cantidad_recibida, cantidad_despachada, IdBodega, IdOperadorBodega_Verifico, activo, IdOperadorBodega_Pickeo, IdPedidoEnc |
| `NCLI_2401221558_VW_Tbleron` | NONCLUSTERED | IdPickingEnc, IdPropietarioBodega, IdProductoBodega, IdPresentacion, IdUnidadMedida, lic_plate, cantidad_recibida, cantidad_despachada, IdBodega, IdOperadorBodega_Pickeo, IdOperadorBodega_Verifico, IdPedidoEnc, activo |
| `NCI_CKFK_20250203_Pciking_Ubic` | NONCLUSTERED | IdPickingEnc, IdProductoBodega, IdProductoEstado, fecha_vence, lic_plate, cantidad_verificada, fecha_verificado, IdOperadorBodega_Verifico, IdPedidoEnc, dañado_verificacion, dañado_picking, no_encontrado, cantidad_recibida |
| `NCL_CKFK_20250324_PICKING` | NONCLUSTERED | IdPickingEnc, IdStock, IdProductoBodega, IdProductoEstado, fecha_vence, lic_plate, cantidad_recibida, fecha_picking, IdPedidoDet, IdOperadorBodega_Pickeo, IdPedidoEnc, dañado_verificacion, dañado_picking, no_encontrado |
| `NCL_CKFK_20250324_PICKING2` | NONCLUSTERED | IdPickingEnc, IdStock, IdProductoBodega, IdProductoEstado, fecha_vence, lic_plate, cantidad_recibida, fecha_picking, IdPedidoDet, IdOperadorBodega_Pickeo, dañado_verificacion, dañado_picking, IdPedidoEnc, no_encontrado |
| `NCL_CKFK_20250714_PickingUbic` | NONCLUSTERED | IdProductoBodega, cantidad_despachada, dañado_verificacion, dañado_picking, IdBodega, IdPedidoEnc, no_encontrado, cantidad_verificada |

## Check constraints

- `Stock_NonNegative_20250228_CKFK`: `([cantidad_solicitada]>(0))`

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_ubic_trans_picking_det` → `trans_picking_det`
- `FK_trans_picking_ubic_unidad_medida` → `unidad_medida`

### Entrantes (otra tabla → esta)

- `trans_despacho_det` (`FK_trans_despacho_det_trans_picking_ubic`)

## Quién la referencia

**36** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `Get_Porcentaje_Avance_Pedido` (scalar_function)
- `Get_Porcentaje_Avance_Picking` (scalar_function)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `Stock_NonNegative_20250228_CKFK` (check_constraint)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Lotes_Despacho` (view)
- `VW_Pedido` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Tareas_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)
- `VW_UbicacionPicking` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

