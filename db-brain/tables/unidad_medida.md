---
id: db-brain-table-unidad-medida
type: db-table
title: dbo.unidad_medida
schema: dbo
name: unidad_medida
kind: table
rows: 1
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.unidad_medida`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2024-02-01 |
| Columnas | 11 |
| Índices | 2 |
| FKs | out:1 in:16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUnidadMedida` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `codigo` | `nvarchar(25)` | ✓ |  |
| 15 | `es_um_cobro` | `bit` | ✓ |  |
| 16 | `factor` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_unidad_medida` | CLUSTERED · **PK** | IdUnidadMedida |
| `NCLI_UM_IDPROPIETARIO_20220906` | NONCLUSTERED | Nombre, IdPropietario |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_unidad_medida_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_unidad_medida`)
- `stock_hist` (`FK_stock_hist_unidad_medida`)
- `stock_hist` (`FK_stock_hist_unidad_medida_rec`)
- `stock_jornada` (`FK_stock_jornada_unidad_medida`)
- `stock_jornada` (`FK_stock_jornada_unidad_medida_rec`)
- `stock_rec` (`FK_stock_rec_unidad_medida`)
- `stock` (`FK_stock_unidad_medida`)
- `stock` (`FK_stock_unidad_medida_rec`)
- `trans_movimientos` (`FK_trans_movimientos_unidad_medida`)
- `trans_oc_det` (`FK_trans_orden_compra_det_unidad_medida`)
- `trans_pe_det` (`FK_trans_pedido_det_unidad_medida`)
- `trans_picking_ubic` (`FK_trans_picking_ubic_unidad_medida`)
- `trans_re_det` (`FK_trans_re_det_unidad_medida`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_unidad_medida`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_unidad_medida_rec`)
- `transacciones_log` (`FK_transacciones_log_unidad_medida`)

## Quién la referencia

**89** objetos:

- `CLBD` (stored_procedure)
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
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Picking` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_20211205` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_Packing` (view)
- `VW_PackingDespachado` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedido` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Producto` (view)
- `VW_ProductoDimension` (view)
- `VW_ProductoOC` (view)
- `VW_ProductoRellenado` (view)
- `VW_Progreso_Picking_By_Operador` (view)
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
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)
- `VW_UnidadMedida` (view)
- `VW_Valorizacion_OC` (view)

