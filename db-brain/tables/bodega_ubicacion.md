---
id: db-brain-table-bodega-ubicacion
type: db-table
title: dbo.bodega_ubicacion
schema: dbo
name: bodega_ubicacion
kind: table
rows: 9510
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 9.510 |
| Schema modify_date | 2024-07-02 |
| Columnas | 38 |
| Índices | 9 |
| FKs | out:2 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdTramo` | `int` |  |  |
| 3 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `ancho` | `float` | ✓ |  |
| 5 | `largo` | `float` | ✓ |  |
| 6 | `alto` | `float` | ✓ |  |
| 7 | `nivel` | `int` | ✓ |  |
| 8 | `indice_x` | `int` | ✓ |  |
| 9 | `IdIndiceRotacion` | `int` | ✓ |  |
| 10 | `IdTipoRotacion` | `int` | ✓ |  |
| 11 | `sistema` | `bit` | ✓ |  |
| 12 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 13 | `codigo_barra2` | `nvarchar(50)` | ✓ |  |
| 14 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `dañado` | `bit` | ✓ |  |
| 19 | `activo` | `bit` | ✓ |  |
| 20 | `bloqueada` | `bit` | ✓ |  |
| 21 | `acepta_pallet` | `bit` | ✓ |  |
| 22 | `ubicacion_picking` | `bit` | ✓ |  |
| 23 | `ubicacion_recepcion` | `bit` | ✓ |  |
| 24 | `ubicacion_despacho` | `bit` | ✓ |  |
| 25 | `ubicacion_merma` | `bit` | ✓ |  |
| 26 | `margen_izquierdo` | `float` | ✓ |  |
| 27 | `margen_derecho` | `float` | ✓ |  |
| 28 | `margen_superior` | `float` | ✓ |  |
| 29 | `margen_inferior` | `float` | ✓ |  |
| 30 | `orientacion_pos` | `nvarchar(50)` | ✓ |  |
| 31 | `ubicacion_virtual` | `bit` | ✓ |  |
| 33 | `ubicacion_ne` | `bit` | ✓ |  |
| 34 | `IdBodega` | `int` |  |  |
| 35 | `IdArea` | `int` |  |  |
| 36 | `IdSector` | `int` |  |  |
| 37 | `posicion_x` | `float` |  |  |
| 38 | `posicion_y` | `float` |  |  |
| 39 | `ubicacion_muelle` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_BodegaUbicacionId` | CLUSTERED · **PK** | IdUbicacion, IdBodega |
| `IX_bodega_ubicacion` | NONCLUSTERED | IdUbicacion |
| `NCLI_Bodega_Ubicacion_20191210_EJC` | NONCLUSTERED | IdUbicacion, IdTramo, IdArea, IdBodega, IdSector |
| `NCLI_bodega_ubicacion_20200204_EJC` | NONCLUSTERED | IdUbicacion, descripcion, ancho, largo, alto, nivel, indice_x, orientacion_pos, IdTramo, IdBodega, IdSector |
| `NCLI_Bodega_Ubicacion_20200204_EJC_A` | NONCLUSTERED | IdUbicacion, descripcion, ancho, largo, alto, nivel, indice_x, orientacion_pos, IdTramo, IdBodega, IdArea, IdSector |
| `NCL_BODEGA_UBICACION_20191104_EJC` | NONCLUSTERED | IdUbicacion, descripcion, IdBodega |
| `NCLI_Bodega_Ubicacion_EJC20210128` | NONCLUSTERED | IdTramo, descripcion, nivel, indice_x, codigo_barra, bloqueada, acepta_pallet, IdArea, IdSector, activo, ubicacion_picking |
| `NCLI_BODEGA_UBICACION_20210217_EJC` | NONCLUSTERED | IdUbicacion, ubicacion_despacho |
| `NCLI_EJC_20211025` | NONCLUSTERED | IdUbicacion, descripcion, ancho, largo, alto, nivel, indice_x, IdIndiceRotacion, IdTipoRotacion, sistema, codigo_barra, codigo_barra2, user_agr, fec_agr, user_mod, fec_mod, dañado, activo, bloqueada, acepta_pallet, ubicacion_picking, ubicacion_recepcion, ubicacion_despacho, ubicacion_merma, margen_izquierdo, margen_derecho, margen_superior, margen_inferior, orientacion_pos, ubicacion_virtual, ubicacion_ne, IdArea, IdSector, IdTramo, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_ubicacion_bodega_tramo` → `bodega_tramo`
- `FK_bodega_ubicacion_tipo_rotacion` → `tipo_rotacion`

### Entrantes (otra tabla → esta)

- `stock` (`FK_stock_bodega_ubicacion`)
- `stock` (`FK_stock_bodega_ubicacion1`)
- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_bodega_ubic_dest`)
- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_bodega_ubic_orig`)

## Quién la referencia

**95** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `Get_Codigo_Area_By_IdUbicacion` (scalar_function)
- `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` (stored_procedure)
- `Nombre_Completo_Ubicacion` (scalar_function)
- `Nombre_Completo_Ubicacion_1` (scalar_function)
- `Nombre_Completo_Ubicacion_2` (scalar_function)
- `Nombre_Completo_Ubicacion_3` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_1` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_2` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_3` (scalar_function)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` (stored_procedure)
- `VW_BodegaMuelle` (view)
- `VW_BodegaUbicacion` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Fiscal_Valorización` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Licencias_Por_Ubicacion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_Picking` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_Producto_Estado_Ubic_Bodega_HH` (view)
- `VW_ProductoEstadoUbic` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_ProductoEstadoUbicDefecto` (view)
- `VW_ProductoRellenado` (view)
- `VW_ProximosVencimiento` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_RevisionProducto` (view)
- `VW_rptMinimosMaximos` (view)
- `VW_rptMinimosMaximos_v2` (view)
- `VW_Stock` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Recep` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_res_jornada` (view)
- `VW_Stock_Res_Pedido` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Stock_SP` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_Ubicaciones_Picking` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_Ubicaciones_Tramo_Disponibles` (view)
- `VW_UbicacionPicking` (view)
- `VW_Valorizacion_OC` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)
- `WMS_Existencia` (view)

