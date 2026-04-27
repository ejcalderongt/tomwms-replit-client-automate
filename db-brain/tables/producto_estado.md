---
id: db-brain-table-producto-estado
type: db-table
title: dbo.producto_estado
schema: dbo
name: producto_estado
kind: table
rows: 18
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_estado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 18 |
| Schema modify_date | 2025-02-11 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:2 in:19 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEstado` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 5 | `utilizable` | `bit` | ✓ |  |
| 6 | `activo` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `dañado` | `bit` | ✓ |  |
| 13 | `sistema` | `bit` | ✓ |  |
| 14 | `codigo_bodega_erp` | `nvarchar(25)` |  |  |
| 15 | `dias_vencimiento_clasificacion` | `int` |  |  |
| 16 | `tolerancia_dias_vencimiento` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_estado` | CLUSTERED · **PK** | IdEstado |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_estado_propietarios` → `propietarios`
- `FK_producto_estado_propietarios1` → `propietarios`

### Entrantes (otra tabla → esta)

- `producto_estado_ubic` (`FK_producto_estado_ubic_producto_estado`)
- `producto_rellenado` (`FK_producto_rellenado_producto_estado`)
- `regla_ubic_det_pe` (`FK_regla_ubic_det_pe_producto_estado`)
- `stock_hist` (`FK_stock_hist_producto_estado`)
- `stock_hist` (`FK_stock_hist_producto_estado_rec`)
- `stock_jornada` (`FK_stock_jornada_producto_estado`)
- `stock_jornada` (`FK_stock_jornada_producto_estado_rec`)
- `stock_rec` (`FK_stock_rec_producto_estado`)
- `stock` (`FK_stock_producto_estado`)
- `stock` (`FK_stock_producto_estado_rec`)
- `trans_movimientos` (`FK_trans_movimientos_producto_estado`)
- `trans_movimientos` (`FK_trans_movimientos_producto_estado1`)
- `trans_re_det` (`FK_trans_recepcion_det_producto_estado`)
- `trans_tras_det` (`FK_trans_tras_det_producto_estado`)
- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_producto_estado_destino`)
- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_producto_estado_orig`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_estado`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_producto_estado_rec`)
- `transacciones_log` (`FK_transacciones_log_producto_estado`)

## Quién la referencia

**85** objetos:

- `CLBD` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_Packing` (view)
- `VW_PackingDespachado` (view)
- `VW_Pedido` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_Producto_Estado_Ubic_Bodega_HH` (view)
- `VW_ProductoEstado` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_ProductoEstadoUbicDefecto` (view)
- `VW_ProductoRellenado` (view)
- `VW_ProximosVencimiento` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Revision_Producto` (view)
- `VW_RevisionProducto` (view)
- `VW_Rol_Usuario_Estado` (view)
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
- `VW_StockEstadosProducto` (view)
- `VW_Trans_Inv_Conteo` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicHhDet` (view)
- `VW_UbicacionPicking` (view)
- `VW_Valorizacion_OC` (view)

