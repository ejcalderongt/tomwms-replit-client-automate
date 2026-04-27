---
id: db-brain-table-producto-bodega
type: db-table
title: dbo.producto_bodega
schema: dbo
name: producto_bodega
kind: table
rows: 1914
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.914 |
| Schema modify_date | 2024-07-02 |
| Columnas | 9 |
| Índices | 4 |
| FKs | out:2 in:23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBodega` | `int` |  |  |
| 2 | `IdProducto` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `sistema` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_bodega` | CLUSTERED · **PK** | IdProductoBodega |
| `NCLI_PRODUCTO_BODEGA_20191118_EJC` | NONCLUSTERED | IdProducto, IdBodega |
| `NCLI_Producto_Bodega_20191210_EJC` | NONCLUSTERED | IdProducto |
| `NCL_PRODUCTO_BODEGA_20191104_EJC` | NONCLUSTERED | IdProductoBodega, IdProducto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_bodega_bodega` → `bodega`
- `FK_producto_bodega_producto` → `producto`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_producto_bodega`)
- `producto_subtarea` (`FK_producto_subtarea_producto_bodega`)
- `stock_hist` (`FK_stock_hist_producto_bodega`)
- `stock_hist` (`FK_stock_hist_producto_bodega_rec`)
- `stock_jornada` (`FK_stock_jornada_producto_bodega`)
- `stock_jornada` (`FK_stock_jornada_producto_bodega_rec`)
- `stock_rec` (`FK_stock_rec_producto_bodega`)
- `stock_se_rec` (`FK_stock_se_rec_producto_bodega`)
- `stock_se` (`FK_stock_se_producto_bodega`)
- `stock` (`FK_stock_producto_bodega`)
- `stock` (`FK_stock_producto_bodega_rec`)
- `trans_inv_ciclico` (`FK_trans_inv_ciclico_producto_bodega`)
- `trans_inv_reconteo` (`FK_trans_inv_reconteo_producto_bodega`)
- `trans_manufactura_det` (`FK_trans_manufactura_det_producto_bodega`)
- `trans_manufactura_picking` (`FK_trans_manufactura_picking_producto_bodega`)
- `trans_movimientos` (`FK_trans_movimientos_producto_bodega`)
- `trans_oc_det` (`FK_trans_oc_det_producto_bodega`)
- `trans_pe_det` (`FK_trans_pedido_det_producto_bodega`)
- `trans_re_det_infraccion` (`FK_trans_re_det_infraccion_producto_bodega`)
- `trans_re_det` (`FK_trans_re_det_producto_bodega`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_bodega`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_bodega_rec`)
- `transacciones_log` (`FK_transacciones_log_producto_bodega`)

## Quién la referencia

**122** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `GetCantidadPesoByProductoBodega` (stored_procedure)
- `GetListaStockByProductoBodega` (stored_procedure)
- `GetResumenStockCantidad` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` (stored_procedure)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_CodigoBarra_OC` (view)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_Conteo_By_Operador` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Get_Pedido_Det` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Inventario_prg_por_tipo` (view)
- `VW_MinimosMaximosPorPresentacion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_20211205` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_OrdenCompraPreIngreso` (view)
- `VW_Packing` (view)
- `VW_PackingDespachado` (view)
- `VW_Pedido` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Producto_Subtareas` (view)
- `VW_ProductoBodegaParametro` (view)
- `VW_ProductoDimension` (view)
- `VW_ProductoOC` (view)
- `VW_ProductoPresentacion` (view)
- `VW_ProductoRellenado` (view)
- `VW_ProductoSI` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_ProximosVencimiento` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Revision_Producto` (view)
- `VW_RevisionProducto` (view)
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

_... +22 más_

