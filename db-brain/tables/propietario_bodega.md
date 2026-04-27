---
id: db-brain-table-propietario-bodega
type: db-table
title: dbo.propietario_bodega
schema: dbo
name: propietario_bodega
kind: table
rows: 6
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.propietario_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2024-09-12 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPropietarioBodega` | `int` |  |  |
| 2 | `IdPropietario` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_propietario_bodega` | CLUSTERED · **PK** | IdPropietarioBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_propietario_bodega_bodega` → `bodega`
- `FK_propietario_bodega_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_propietario_bodega`)
- `regla_ubic_det_prop` (`FK_regla_ubic_det_prop_propietario_bodega`)
- `stock_hist` (`FK_stock_hist_propietario_bodega`)
- `stock_hist` (`FK_stock_hist_propietario_bodega_rec`)
- `stock_jornada` (`FK_stock_jornada_propietario_bodega`)
- `stock_jornada` (`FK_stock_jornada_propietario_bodega_rec`)
- `stock_rec` (`FK_stock_rec_propietario_bodega`)
- `stock` (`FK_stock_propietario_bodega`)
- `stock` (`FK_stock_propietario_bodega_rec`)
- `trans_despacho_enc` (`FK_trans_despacho_enc_propietario_bodega`)
- `trans_inventario_enc` (`FK_trans_inventario_enc_propietario_bodega`)
- `trans_manufactura_det` (`FK_trans_manufactura_det_propietario_bodega`)
- `trans_manufactura_enc` (`FK_trans_manufactura_enc_propietario_bodega`)
- `trans_movimientos` (`FK_trans_movimientos_propietario_bodega`)
- `trans_oc_enc` (`FK_trans_oc_enc_propietario_bodega`)
- `trans_pe_enc` (`FK_trans_pedido_enc_propietario_bodega`)
- `trans_picking_enc` (`FK_trans_picking_enc_propietario_bodega`)
- `trans_re_enc` (`FK_trans_recepcion_enc_propietario_bodega`)
- `trans_tras_enc` (`FK_trans_tras_enc_propietario_bodega`)
- `trans_ubic_hh_enc` (`FK_trans_ubic_hh_enc_propietario_bodega`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_propietario_bodega`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_propietario_bodega_rec`)
- `transacciones_log` (`FK_transacciones_log_propietario_bodega`)

## Quién la referencia

**94** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Despacho` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Get_Single_Pedido` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OrdenCompra` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedidos_IdPickingEnc` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_ProductoOC` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_ProximosVencimiento` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Revision_Producto` (view)
- `VW_rptMinimosMaximos` (view)
- `VW_rptMinimosMaximos_v2` (view)
- `VW_rptProductosProximosVencimiento` (view)
- `VW_RptStock` (view)
- `VW_Stock` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Enc` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Recep` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Res_Pedido` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Res_V1` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Serie_Parametro` (view)
- `VW_Stock_SP` (view)
- `VW_Stock_Transito` (view)
- `VW_StockEstadosProducto` (view)
- `VW_StockPresentaciones` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicacionHhEnc` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_UbicacionPicking` (view)
- `VW_Valorizacion_OC` (view)

