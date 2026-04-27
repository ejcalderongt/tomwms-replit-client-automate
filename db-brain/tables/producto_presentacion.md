---
id: db-brain-table-producto-presentacion
type: db-table
title: dbo.producto_presentacion
schema: dbo
name: producto_presentacion
kind: table
rows: 295
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_presentacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 295 |
| Schema modify_date | 2025-02-11 |
| Columnas | 29 |
| Índices | 3 |
| FKs | out:2 in:25 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPresentacion` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `imprime_barra` | `bit` | ✓ |  |
| 6 | `peso` | `float` | ✓ |  |
| 7 | `alto` | `float` | ✓ |  |
| 8 | `largo` | `float` | ✓ |  |
| 9 | `ancho` | `float` | ✓ |  |
| 10 | `factor` | `float` |  |  |
| 11 | `MinimoExistencia` | `float` | ✓ |  |
| 12 | `MaximoExistencia` | `float` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `activo` | `bit` | ✓ |  |
| 18 | `EsPallet` | `bit` | ✓ |  |
| 19 | `Precio` | `float` | ✓ |  |
| 20 | `MinimoPeso` | `float` | ✓ |  |
| 21 | `MaximoPeso` | `float` | ✓ |  |
| 22 | `Costo` | `float` | ✓ |  |
| 23 | `CamasPorTarima` | `float` | ✓ |  |
| 24 | `CajasPorCama` | `float` | ✓ |  |
| 27 | `genera_lp_auto` | `bit` | ✓ |  |
| 28 | `permitir_paletizar` | `bit` | ✓ |  |
| 29 | `sistema` | `bit` | ✓ |  |
| 30 | `IdPresentacionPallet` | `int` | ✓ |  |
| 31 | `codigo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_presentacion_1` | CLUSTERED · **PK** | IdPresentacion |
| `NCLI_Producto_Presentacion_20210825_EJC` | NONCLUSTERED | nombre, peso, IdProducto |
| `NCL_AT20241212_ProductoPresentacion` | NONCLUSTERED | codigo_barra, nombre, factor, activo, IdProducto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__producto___IdPre__60B4AB0C` → `producto_presentacion`
- `FK_producto_presentacion_producto` → `producto`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_producto_presentacion`)
- `producto_presentacion_tarima` (`FK_producto_presentacion_tarima_producto_presentacion`)
- `producto_presentacion` (`FK__producto___IdPre__60B4AB0C`)
- `producto_presentaciones_conversiones` (`FK_producto_presentaciones_conversiones_producto_presentacion`)
- `producto_presentaciones_conversiones` (`FK_producto_presentaciones_conversiones_producto_presentacion1`)
- `producto_rellenado` (`FK_producto_rellenado_producto_presentacion`)
- `producto_sustituto` (`FK_producto_sustituto_producto_presentacion`)
- `producto_sustituto` (`FK_producto_sustituto_producto_presentacion1`)
- `regla_ubic_det_pp` (`FK_regla_ubic_det_pp_producto_presentacion`)
- `stock_hist` (`FK_stock_hist_producto_presentacion`)
- `stock_hist` (`FK_stock_hist_producto_presentacion_rec`)
- `stock_jornada` (`FK_stock_jornada_producto_presentacion`)
- `stock_jornada` (`FK_stock_jornada_producto_presentacion_rec`)
- `stock_rec` (`FK_stock_rec_producto_presentacion`)
- `stock` (`FK_stock_producto_presentacion`)
- `stock` (`FK_stock_producto_presentacion_rec`)
- `trans_movimientos` (`FK_trans_movimientos_producto_presentacion`)
- `trans_oc_det` (`FK_trans_orden_compra_det_producto_presentacion`)
- `trans_pe_det` (`FK_trans_pedido_det_producto_presentacion`)
- `trans_re_det_infraccion` (`FK_trans_re_det_infraccion_producto_presentacion`)
- `trans_re_det` (`FK_trans_recepcion_det_producto_presentacion`)
- `trans_tras_det` (`FK_trans_tras_det_producto_presentacion`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_presentacion`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_presentacion_rec`)
- `transacciones_log` (`FK_transacciones_log_producto_presentacion`)

## Quién la referencia

**107** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Ajustes` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_ControlCalidad_CambioEstado` (view)
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
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Indicador_Ingresos` (view)
- `vw_Indicador_Picking` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_MinimosMaximosPorPresentacion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_20211205` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_Packing` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedido` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Presentacion_Tarima` (view)
- `VW_Productividad_Picking` (view)
- `VW_ProductoDimension` (view)
- `VW_ProductoPresentacion` (view)
- `VW_ProductoRellenado` (view)
- `VW_ProductoSI` (view)
- `VW_ProductoSustituto` (view)
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
- `VW_Stock_Res_US` (view)
- `VW_Stock_Res_V1` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Stock_Serie_Parametro` (view)
- `VW_Stock_SP` (view)
- `VW_Stock_Transito` (view)
- `VW_StockPresentaciones` (view)
- `VW_Trans_Inv_Conteo` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)

_... +7 más_

