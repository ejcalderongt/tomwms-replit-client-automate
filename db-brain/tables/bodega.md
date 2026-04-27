---
id: db-brain-table-bodega
type: db-table
title: dbo.bodega
schema: dbo
name: bodega
kind: table
rows: 6
modify_date: 2026-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2026-02-11 |
| Columnas | 102 |
| Índices | 1 |
| FKs | out:2 in:30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdPais` | `int` | ✓ |  |
| 3 | `IdEmpresa` | `int` |  |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `codigo_barra` | `nvarchar(150)` | ✓ |  |
| 6 | `nombre` | `nvarchar(50)` | ✓ |  |
| 7 | `nombre_comercial` | `nvarchar(50)` | ✓ |  |
| 9 | `direccion` | `nvarchar(250)` | ✓ |  |
| 10 | `telefono` | `nvarchar(50)` | ✓ |  |
| 11 | `email` | `nvarchar(50)` | ✓ |  |
| 12 | `encargado` | `nvarchar(50)` | ✓ |  |
| 13 | `ubic_recepcion` | `nvarchar(25)` | ✓ |  |
| 14 | `ubic_picking` | `nvarchar(25)` | ✓ |  |
| 15 | `ubic_despacho` | `nvarchar(25)` | ✓ |  |
| 16 | `ubic_merma` | `nvarchar(50)` | ✓ |  |
| 17 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 18 | `fec_agr` | `datetime` | ✓ |  |
| 19 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 20 | `fec_mod` | `datetime` | ✓ |  |
| 21 | `activo` | `bit` | ✓ |  |
| 22 | `coordenada_x` | `nvarchar(50)` | ✓ |  |
| 23 | `coordenada_y` | `nvarchar(50)` | ✓ |  |
| 24 | `largo` | `float` | ✓ |  |
| 25 | `ancho` | `float` | ✓ |  |
| 26 | `alto` | `float` | ✓ |  |
| 27 | `reservar_stocks_por_linea` | `bit` | ✓ |  |
| 28 | `rechazar_pedido_por_stock` | `bit` | ✓ |  |
| 29 | `IdTipoTransaccion` | `nvarchar(50)` | ✓ |  |
| 30 | `zoom` | `float` | ✓ |  |
| 31 | `IdMotivoUbicacionDañadoPicking` | `int` | ✓ |  |
| 33 | `cambio_ubicacion_auto` | `bit` | ✓ |  |
| 34 | `codigo_bodega_erp` | `nvarchar(25)` | ✓ |  |
| 37 | `ubic_producto_ne` | `int` | ✓ |  |
| 38 | `IdProductoEstadoNE` | `int` | ✓ |  |
| 39 | `Cuenta_Ingreso_Mercancias` | `nvarchar(50)` | ✓ |  |
| 40 | `Cuenta_Egreso_Mercancias` | `nvarchar(50)` | ✓ |  |
| 41 | `notificacion_voz` | `bit` |  |  |
| 42 | `control_tarifa_servicios` | `bit` |  |  |
| 43 | `Id_Motivo_Ubic_Reabasto` | `int` | ✓ |  |
| 44 | `operador_defecto_en_documento_ingreso` | `bit` | ✓ |  |
| 45 | `es_bodega_fiscal` | `bit` | ✓ |  |
| 46 | `habilitar_ingreso_consolidado` | `bit` | ✓ |  |
| 47 | `bloquear_lp_hh` | `bit` | ✓ |  |
| 48 | `captura_estiba_ingreso` | `bit` |  |  |
| 49 | `captura_pallet_no_estandar` | `bit` |  |  |
| 50 | `valor_porcentaje_iva` | `float` | ✓ |  |
| 51 | `Permitir_Verificacion_Consolidada` | `bit` | ✓ |  |
| 52 | `control_banderas_cliente` | `bit` | ✓ |  |
| 53 | `IdTamañoEtiquetaUbicacionDefecto` | `int` | ✓ |  |
| 54 | `priorizar_ubicrec_sobre_ubicest` | `bit` |  |  |
| 55 | `validar_disponibilidad_ubicaicon_destino` | `bit` | ✓ |  |
| 56 | `ubicar_tarimas_completas_reabasto` | `bit` | ✓ |  |
| 57 | `IdTipoTransaccionSalida` | `int` | ✓ |  |
| 58 | `permitir_eliminar_documento_salida` | `bit` | ✓ |  |
| 59 | `mostrar_area_en_hh` | `bit` | ✓ |  |
| 60 | `confirmar_codigo_en_picking` | `bit` | ✓ |  |
| 61 | `control_operador_ubicacion` | `bit` | ✓ |  |
| 62 | `inferir_origen_en_cambio_ubic` | `bit` | ✓ |  |
| 63 | `eliminar_documento_salida` | `bit` |  |  |
| 64 | `operador_picking_realiza_verificacion` | `bit` | ✓ |  |
| 65 | `permitir_cambio_ubic_producto_picking` | `bit` | ✓ |  |
| 66 | `despachar_producto_vencido` | `bit` |  |  |
| 67 | `tipo_pantalla_picking` | `int` |  |  |
| 68 | `verificacion_consolidada` | `bit` |  |  |
| 69 | `tipo_pantalla_recepcion` | `int` |  |  |
| 70 | `tipo_pantalla_verificacion` | `int` |  |  |
| 71 | `permitir_buen_estado_en_reemplazo` | `bit` |  |  |
| 72 | `industria_motriz` | `bit` |  |  |
| 73 | `restringir_vencimiento_en_reemplazo` | `bit` |  |  |
| 74 | `restringir_lote_en_reemplazo` | `bit` |  |  |
| 75 | `top_reabastecimiento_manual` | `int` |  |  |
| 76 | `permitir_decimales` | `bit` |  |  |
| 77 | `permitir_repeticiones_en_ingreso` | `bit` | ✓ |  |
| 78 | `dias_maximo_vencimiento_reemplazo` | `int` | ✓ |  |
| 79 | `validar_existencias_inv_ini` | `bit` |  |  |
| 80 | `calcular_ubicacion_sugerida_ml` | `bit` |  |  |
| 81 | `ordenar_por_nombre_completo` | `bit` |  |  |
| 82 | `ordenar_picking_descendente` | `bit` |  |  |
| 83 | `permitir_reemplazo_picking` | `bit` |  |  |
| 84 | `permitir_no_encontrado_picking` | `bit` |  |  |
| 85 | `permitir_reemplazo_verificacion` | `bit` |  |  |
| 86 | `Permitir_Reemplazo_Picking_Misma_Licencia` | `bit` |  |  |
| 87 | `dias_limite_retroactivo` | `int` | ✓ |  |
| 88 | `horario_ejecucion_historico` | `time` | ✓ |  |
| 89 | `filtrar_pedidos_usuario` | `bit` |  |  |
| 90 | `liberar_stock_despachos_parciales` | `bit` |  |  |
| 91 | `homologar_lote_vencimiento` | `bit` |  |  |
| 92 | `IdTipoEtiquetaLicencia` | `int` |  |  |
| 93 | `IdSimbologiaLicencia` | `int` |  |  |
| 94 | `escanear_licencia_picking` | `bit` |  |  |
| 95 | `restringir_areas_sap` | `bit` |  |  |
| 96 | `interface_SAP` | `bit` |  |  |
| 97 | `control_pallet_mixto` | `bit` | ✓ |  |
| 98 | `despacho_automatico_hh` | `bit` |  |  |
| 99 | `limpiar_campos` | `bit` |  |  |
| 100 | `permitir_cambio_ubic_recepcion` | `bit` |  |  |
| 101 | `ruta_cdn` | `nvarchar(100)` | ✓ |  |
| 102 | `rango_dias_documentos` | `int` |  |  |
| 103 | `agrupar_sin_lic_veri_no_cons` | `bit` |  |  |
| 104 | `advertir_mpq_umbas` | `bit` |  |  |
| 105 | `Control_Talla_Color` | `bit` |  |  |
| 106 | `Reservar_primero_almacenaje` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_bodega_1` | CLUSTERED · **PK** | IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_empresa` → `empresa`
- `FK_bodega_paises` → `paises`

### Entrantes (otra tabla → esta)

- `bodega_area` (`FK_bodega_area_bodega`)
- `bodega_monitor_parametro` (`FK_bodega_monitor_parametro_bodega`)
- `bodega_muelles` (`FK_bodega_muelles_bodega`)
- `cliente_bodega` (`FK_cliente_bodega_bodega`)
- `cliente_bodega` (`FK_cliente_bodega_bodega1`)
- `configuracion_qa` (`FK_configuracion_qa_bodega`)
- `empresa_transporte_bodega` (`FK_empresa_transporte_bodega_bodega`)
- `horario_laboral_enc` (`FK_horario_laboral_enc_bodega`)
- `i_nav_config_enc` (`FK_i_nav_config_enc_bodega`)
- `jornada_laboral` (`FK_jornada_laboral_bodega`)
- `motivo_anulacion_bodega` (`FK_motivo_anulacion_bodega_bodega`)
- `motivo_devolucion_bodega` (`FK_motivo_devolucion_bodega_bodega`)
- `operador_bodega` (`FK_operador_bodega_bodega`)
- `operador_jornada_laboral` (`FK_operador_jornada_laboral_bodega`)
- `producto_bodega` (`FK_producto_bodega_bodega`)
- `propietario_bodega` (`FK_propietario_bodega_bodega`)
- `proveedor_bodega` (`FK_proveedor_bodega_bodega`)
- `regla_vencimiento` (`FK__regla_ven__IdBod__7BD380F3`)
- `rol_bodega` (`FK_rol_bodega_bodega`)
- `tarea_hh` (`FK_tarea_hh_bodega`)
- `trans_despacho_enc` (`FK_trans_despacho_enc_bodega`)
- `trans_manufactura_enc` (`FK_trans_manufactura_enc_bodega`)
- `trans_movimientos` (`FK_trans_movimientos_bodega`)
- `trans_pe_enc` (`FK_trans_pedido_enc_bodega`)
- `trans_picking_enc` (`FK_trans_picking_enc_bodega`)
- `trans_servicio_enc` (`FK_trans_servicio_enc_bodega`)
- `trans_tras_enc` (`FK_trans_tras_enc_bodega`)
- `trans_tras_enc` (`FK_trans_tras_enc_bodega1`)
- `turno` (`FK_turno_bodega`)
- `usuario_bodega` (`FK_usuarios_empresa_bodega`)

## Quién la referencia

**76** objetos:

- `asignar_jornada_laboral` (stored_procedure)
- `CLBD` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `v_motivo_anulacion` (view)
- `v_trans_pedido` (view)
- `VW_Bodega` (view)
- `VW_BodegaArea` (view)
- `VW_BodegaMuelle` (view)
- `VW_BodegaSector` (view)
- `VW_BodegaTramo` (view)
- `VW_BodegaUbicacion` (view)
- `VW_Cantidad_Ingresos_Proveedor` (view)
- `VW_Cantidad_Pedidos_vrs_Despacho_Clientes` (view)
- `VW_Configuracioninv` (view)
- `VW_Despacho` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Indicador_Ingresos` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Movimientos_20211205` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_OrdenCompra` (view)
- `VW_PackingDespachado` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedidos_IdPickingEnc` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_ProductoEstadoUbic` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Servicio` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_res_jornada_merca` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Res_V1` (view)
- `VW_Stock_Transito` (view)
- `VW_tareas_hh` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_Trans_Inv_Conteo` (view)
- `VW_Trans_Servicios` (view)
- `VW_TransUbicacionHhEnc` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Tramo_Disponibles` (view)
- `VW_Valorizacion_OC` (view)
- `WMS_Existencia` (view)

