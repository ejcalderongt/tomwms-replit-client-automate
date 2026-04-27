---
id: db-brain-table-producto
type: db-table
title: dbo.producto
schema: dbo
name: producto
kind: table
rows: 319
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 319 |
| Schema modify_date | 2025-05-13 |
| Columnas | 60 |
| Índices | 7 |
| FKs | out:12 in:8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProducto` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdClasificacion` | `int` | ✓ |  |
| 4 | `IdFamilia` | `int` | ✓ |  |
| 5 | `IdMarca` | `int` | ✓ |  |
| 6 | `IdTipoProducto` | `int` | ✓ |  |
| 7 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 8 | `IdCamara` | `int` | ✓ |  |
| 9 | `IdTipoRotacion` | `int` | ✓ |  |
| 10 | `IdPerfilSerializado` | `int` | ✓ |  |
| 11 | `IdIndiceRotacion` | `int` | ✓ |  |
| 12 | `IdSimbologia` | `int` | ✓ |  |
| 13 | `IdArancel` | `int` | ✓ |  |
| 14 | `codigo` | `nvarchar(50)` | ✓ |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |
| 16 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 17 | `precio` | `float` | ✓ |  |
| 18 | `existencia_min` | `float` | ✓ |  |
| 19 | `existencia_max` | `float` | ✓ |  |
| 20 | `costo` | `float` | ✓ |  |
| 21 | `peso_referencia` | `float` | ✓ |  |
| 22 | `peso_tolerancia` | `float` | ✓ |  |
| 23 | `temperatura_referencia` | `float` | ✓ |  |
| 24 | `temperatura_tolerancia` | `float` | ✓ |  |
| 25 | `activo` | `bit` | ✓ |  |
| 26 | `serializado` | `bit` | ✓ |  |
| 27 | `genera_lote` | `bit` | ✓ |  |
| 28 | `genera_lp_old` | `bit` | ✓ |  |
| 29 | `control_vencimiento` | `bit` | ✓ |  |
| 30 | `control_lote` | `bit` | ✓ |  |
| 31 | `peso_recepcion` | `bit` | ✓ |  |
| 32 | `peso_despacho` | `bit` | ✓ |  |
| 33 | `temperatura_recepcion` | `bit` | ✓ |  |
| 34 | `temperatura_despacho` | `bit` | ✓ |  |
| 35 | `materia_prima` | `bit` | ✓ |  |
| 36 | `kit` | `bit` | ✓ |  |
| 37 | `tolerancia` | `int` | ✓ |  |
| 38 | `ciclo_vida` | `int` | ✓ |  |
| 39 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 40 | `fec_agr` | `datetime` | ✓ |  |
| 41 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 42 | `fec_mod` | `datetime` | ✓ |  |
| 43 | `imagen` | `image` | ✓ |  |
| 44 | `noserie` | `nvarchar(50)` | ✓ |  |
| 45 | `noparte` | `nvarchar(50)` | ✓ |  |
| 46 | `fechamanufactura` | `bit` | ✓ |  |
| 47 | `capturar_aniada` | `bit` | ✓ |  |
| 48 | `control_peso` | `bit` | ✓ |  |
| 49 | `captura_arancel` | `bit` | ✓ |  |
| 50 | `es_hardware` | `bit` | ✓ |  |
| 51 | `largo` | `float` | ✓ |  |
| 52 | `alto` | `float` | ✓ |  |
| 53 | `ancho` | `float` | ✓ |  |
| 54 | `IdUnidadMedidaCobro` | `int` | ✓ |  |
| 55 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 56 | `dias_inventario_promedio` | `int` | ✓ |  |
| 57 | `IDPRODUCTOPARAMETROA` | `int` | ✓ |  |
| 58 | `IDPRODUCTOPARAMETROB` | `int` | ✓ |  |
| 59 | `IdTipoManufactura` | `int` | ✓ |  |
| 60 | `margen_impresion` | `float` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_1` | CLUSTERED · **PK** | IdProducto |
| `IX_producto` | NONCLUSTERED · UNIQUE | codigo |
| `NCLI_Producto_20191210_EJC` | NONCLUSTERED | IdFamilia, codigo, nombre, codigo_barra, IdClasificacion |
| `NCLI_Producto_20191210A_EJC` | NONCLUSTERED | codigo, nombre, IdProducto, IdPropietario |
| `NCL_Producto_20200115_ejc` | NONCLUSTERED | codigo, nombre, codigo_barra, IdUnidadMedidaBasica |
| `NCL_Producto_20191122_EJC` | NONCLUSTERED | codigo, nombre, IdPropietario |
| `NCL_PRODUCTO_20191104` | NONCLUSTERED | IdProducto, codigo, nombre, IdPropietario |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_Arancel` → `arancel`
- `FK_producto_camara` → `camara`
- `FK_producto_indice_rotacion` → `indice_rotacion`
- `FK_producto_perfil_serializado` → `perfil_serializado`
- `FK_producto_producto_clasificacion` → `producto_clasificacion`
- `FK_producto_producto_familia` → `producto_familia`
- `FK_producto_producto_marca` → `producto_marca`
- `FK_producto_producto_tipo` → `producto_tipo`
- `FK_producto_propietarios` → `propietarios`
- `FK_producto_simbologias_codigo_barra` → `simbologias_codigo_barra`
- `FK_producto_tipo_rotacion` → `tipo_rotacion`
- `FK_producto_unidad_medida` → `unidad_medida`

### Entrantes (otra tabla → esta)

- `configuracion_qa` (`FK_configuracion_qa_producto`)
- `producto_bodega` (`FK_producto_bodega_producto`)
- `producto_parametros` (`FK_producto_parametros_producto`)
- `producto_presentacion` (`FK_producto_presentacion_producto`)
- `producto_sustituto` (`FK_producto_sustituto_producto`)
- `producto_sustituto` (`FK_producto_sustituto_producto1`)
- `producto_talla_color` (`FK__producto___IdPro__57B61C0E`)
- `trans_tras_det` (`FK_trans_tras_det_producto`)

## Quién la referencia

**119** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` (stored_procedure)
- `VW_Ajustes` (view)
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
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Indicador_Ingresos` (view)
- `vw_Indicador_Picking` (view)
- `VW_Indicador_Picking_Detalle` (view)
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
- `VW_Producto` (view)
- `VW_Producto_Subtareas` (view)
- `VW_ProductoBodegaParametro` (view)
- `VW_ProductoDimension` (view)
- `VW_ProductoOC` (view)
- `VW_ProductoParametro` (view)
- `VW_ProductoPresentacion` (view)
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

_... +19 más_

