---
id: db-brain-table-propietarios
type: db-table
title: dbo.propietarios
schema: dbo
name: propietarios
kind: table
rows: 1
modify_date: 2025-08-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.propietarios`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2025-08-01 |
| Columnas | 23 |
| Índices | 1 |
| FKs | out:2 in:20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPropietario` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdTipoActualizacionCosto` | `int` | ✓ |  |
| 4 | `contacto` | `nvarchar(100)` |  |  |
| 5 | `nombre_comercial` | `nvarchar(100)` |  |  |
| 6 | `imagen` | `image` | ✓ |  |
| 7 | `telefono` | `nvarchar(50)` | ✓ |  |
| 8 | `direccion` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |
| 14 | `email` | `nvarchar(100)` | ✓ |  |
| 15 | `actualiza_costo_oc` | `bit` | ✓ |  |
| 16 | `color` | `int` | ✓ |  |
| 17 | `codigo` | `nvarchar(25)` | ✓ |  |
| 18 | `sistema` | `bit` | ✓ |  |
| 19 | `NIT` | `nvarchar(50)` | ✓ |  |
| 20 | `codigo_acceso` | `nvarchar(50)` | ✓ |  |
| 21 | `clave_acceso` | `nvarchar(50)` | ✓ |  |
| 22 | `es_consolidador` | `bit` | ✓ |  |
| 23 | `controlux` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_propietarios_1` | CLUSTERED · **PK** | IdPropietario |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_propietarios_empresa` → `empresa`
- `FK_propietarios_tipo_actualizacion_costo` → `tipo_actualizacion_costo`

### Entrantes (otra tabla → esta)

- `cliente_tipo` (`FK_cliente_tipo_propietarios`)
- `cliente` (`FK_cliente_propietarios`)
- `i_nav_config_enc` (`FK_i_nav_config_enc_propietarios`)
- `motivo_devolucion` (`FK_motivo_devolucion_propietarios`)
- `producto_clasificacion` (`FK_producto_clasificacion_propietarios`)
- `producto_estado` (`FK_producto_estado_propietarios`)
- `producto_estado` (`FK_producto_estado_propietarios1`)
- `producto_familia` (`FK_producto_familia_propietarios`)
- `producto_marca` (`FK_producto_marca_propietarios`)
- `producto_tipo` (`FK_producto_tipo_propietarios`)
- `producto` (`FK_producto_propietarios`)
- `propietario_bodega` (`FK_propietario_bodega_propietarios`)
- `propietario_reglas_enc` (`FK_propietario_reglas_enc_propietarios`)
- `proveedor` (`FK_proveedor_propietarios`)
- `regla_vencimiento` (`FK__regla_ven__IdPro__7EAFED9E`)
- `tarea_hh` (`FK_tarea_hh_propietarios`)
- `tms_ticket` (`FK_tms_ticket_propietarios`)
- `trans_servicio_det` (`FK_trans_servicio_det_propietarios`)
- `trans_servicio_enc` (`FK_trans_servicio_enc_propietarios`)
- `unidad_medida` (`FK_unidad_medida_propietarios`)

## Quién la referencia

**115** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Cliente` (view)
- `VW_ClienteTipo` (view)
- `VW_Configuracioninv` (view)
- `VW_ControlCalidad_CambioEstado` (view)
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
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Single_Pedido` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_MotivoDevolucion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OrdenCompra` (view)
- `VW_Packing` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedidos_IdPickingEnc` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Producto` (view)
- `VW_Producto_Estado_Ubic_Bodega_HH` (view)
- `VW_ProductoClasificacion` (view)
- `VW_ProductoEstado` (view)
- `VW_ProductoFamilia` (view)
- `VW_ProductoMarca` (view)
- `VW_ProductoOC` (view)
- `VW_ProductoTipo` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Propietario` (view)
- `VW_Propietario_Regla_Recepcion` (view)
- `VW_Proveedor` (view)
- `VW_ProveedorBodega` (view)
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
- `VW_RevisionProducto` (view)
- `VW_Rol_Usuario_Estado` (view)
- `VW_rptMinimosMaximos` (view)
- `VW_rptMinimosMaximos_v2` (view)
- `VW_rptProductosProximosVencimiento` (view)
- `VW_RptStock` (view)
- `VW_Servicio` (view)
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

_... +15 más_

