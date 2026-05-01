---
id: FLAGS-CALLSITES
tipo: reference
estado: vigente
tags: [code-deep-flow/flags, code-deep-flow/callsites]
---

# Flags `i_nav_config_enc` — callsites en código

> Generado por F3 del Atlas BOF/HH 2023↔2028 (2026-04-30).
> Para cada flag, lista los archivos que lo mencionan en cada rama.
> Permite responder: ¿qué se rompe si activamos/desactivamos este flag?

## Resumen — flags más referenciados

| Flag | BOF-2023 | BOF-2028 | Δ | HH-2023 | HH-2028 |
|---|---:|---:|---:|---:|---:|
| `IdIndiceRotacion` | 118 | 132 | +14 | 4 | 5 |
| `IdTipoRotacion` | 100 | 113 | +13 | 2 | 2 |
| `rotacion` | 61 | 71 | +10 | 3 | 3 |
| `producto_estado` | 55 | 59 | +4 | 4 | 5 |
| `serializado` | 45 | 46 | +1 | 0 | 0 |
| `genera_lp` | 23 | 30 | +7 | 1 | 2 |
| `control_vencimiento` | 15 | 23 | +8 | 0 | 0 |
| `AJCANTN` | 20 | 22 | +2 | 1 | 1 |
| `control_lote` | 15 | 21 | +6 | 0 | 0 |
| `control_peso` | 15 | 20 | +5 | 5 | 6 |
| `genera_lote` | 14 | 18 | +4 | 0 | 0 |
| `IdProductoEstado_NC` | 8 | 11 | +3 | 0 | 0 |
| `genera_lp_old` | 6 | 10 | +4 | 0 | 0 |
| `Ejecutar_En_Despacho_Automaticamente` | 7 | 9 | +2 | 0 | 0 |
| `considerar_paletizado_en_reabasto` | 5 | 8 | +3 | 0 | 0 |
| `equiparar_cliente_con_propietario_en_doc_salida` | 5 | 7 | +2 | 0 | 0 |
| `dias_vida_defecto_perecederos` | 5 | 6 | +1 | 0 | 0 |
| `push_ingreso_nav_desde_hh` | 5 | 6 | +1 | 0 | 0 |
| `generar_pedido_ingreso_bodega_destino` | 4 | 6 | +2 | 0 | 0 |
| `generar_recepcion_auto_bodega_destino` | 3 | 6 | +3 | 0 | 0 |
| `explosion_automatica` | 3 | 4 | +1 | 0 | 0 |
| `explosion_automatica_desde_ubicacion_picking` | 3 | 4 | +1 | 0 | 0 |
| `interface_sap` | 2 | 4 | +2 | 0 | 0 |
| `rechazar_pedido_incompleto` | 3 | 4 | +1 | 0 | 0 |
| `cantidad_en_presentacion_transacciones_out` | 0 | 4 | +4 | 0 | 0 |
| `codigo_bodega_erp_nc` | 2 | 3 | +1 | 0 | 0 |
| `convertir_decimales_a_umbas` | 2 | 3 | +1 | 0 | 0 |
| `crear_recepcion_de_compra_nav` | 2 | 3 | +1 | 0 | 0 |
| `crear_recepcion_de_transferencia_nav` | 2 | 3 | +1 | 0 | 0 |
| `despachar_existencia_parcial` | 2 | 3 | +1 | 0 | 0 |
| `cealsa_vwacuerdocomercialenc` | 2 | 2 | 0 | 0 | 0 |
| `bodega_facturacion` | 1 | 2 | +1 | 0 | 0 |
| `bodega_prorrateo` | 1 | 2 | +1 | 0 | 0 |
| `bodega_prorrateo1` | 1 | 2 | +1 | 0 | 0 |
| `conservar_zona_picking_clavaud` | 1 | 2 | +1 | 0 | 0 |
| `equiparar_productos` | 1 | 2 | +1 | 0 | 0 |
| `excluir_ubicaciones_reabasto` | 1 | 2 | +1 | 0 | 0 |
| `explosion_automatica_nivel_max` | 1 | 2 | +1 | 0 | 0 |
| `implosion_automatica` | 1 | 2 | +1 | 0 | 0 |
| `inferir_bonificacion_pedido_sap` | 1 | 2 | +1 | 0 | 0 |
| `rechazar_bonificacion_incompleta` | 1 | 2 | +1 | 0 | 0 |
| `reservar_umbas_primero` | 1 | 2 | +1 | 0 | 0 |
| `sap_control_draft_ajustes` | 1 | 2 | +1 | 0 | 0 |
| `sap_control_draft_traslados` | 1 | 2 | +1 | 0 | 0 |
| `valida_solo_codigo` | 1 | 2 | +1 | 0 | 0 |
| `vence_defecto_nc` | 1 | 2 | +1 | 0 | 0 |
| `bodega_faltante` | 0 | 1 | +1 | 0 | 0 |
| `considerar_disponibilidad_ubicacion_reabasto` | 0 | 1 | +1 | 0 | 0 |
| `excluir_recepcion_picking` | 0 | 1 | +1 | 0 | 0 |
| `danado_picking` | 0 | 0 | 0 | 3 | 3 |

## Detalle por flag

### `IdIndiceRotacion`

**BOF-2023** (118 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `DMS/Clases/IMS.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases Interface Sync/Producto/clsSyncNavProducto.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `PrintsService/Clases/IMS.vb`
- `Quick_Tag/Clases_AP/IMS.vb`
- `SAPMI3PC/Clases Interface Sync/Producto/clsSyncSAPProducto.vb`
- _(+108 más)_

**BOF-2028** (132 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `DMS/Clases/IMS.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases Interface Sync/Producto/clsSyncNavProducto.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `PrintsService/Clases/IMS.vb`
- `Quick_Tag/Clases_AP/IMS.vb`
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- _(+122 más)_

**HH-2023** (4 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Bodega/clsBeBodega_ubicacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`
- `app/src/main/java/com/dts/classes/Transacciones/Stock/Stock_res/clsBeVW_stock_res.java`

**HH-2028** (5 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Bodega/clsBeBodega_ubicacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`
- `app/src/main/java/com/dts/classes/Transacciones/Stock/Stock_res/clsBeVW_stock_res.java`
- `app/src/main/java/com/dts/tom/Transacciones/CambioUbicacion/frm_cambio_ubicacion_ciega.java`

---

### `IdTipoRotacion`

**BOF-2023** (100 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `DMS/Clases/IMS.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DMS/Clases/clsTabla.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases Interface Sync/Producto/clsSyncNavProducto.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `MES/Clases_AP/clsTabla.vb`
- `PrintsService/Clases/IMS.vb`
- _(+90 más)_

**BOF-2028** (113 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `DMS/Clases/IMS.vb`
- `DMS/Clases/clsTabla.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases Interface Sync/Producto/clsSyncNavProducto.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `MES/Clases_AP/clsTabla.vb`
- `PrintsService/Clases/IMS.vb`
- _(+103 más)_

**HH-2023** (2 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Bodega/clsBeBodega_ubicacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`

**HH-2028** (2 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Bodega/clsBeBodega_ubicacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`

---

### `rotacion`

**BOF-2023** (61 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `CEALSAMI3/DAL/DAL.vbproj`
- `CEALSAMI3/Entity/Entity.vbproj`
- `DMS/Clases/IMS.vb`
- `DMS/Clases/clsTabla.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `MES/Clases_AP/clsTabla.vb`
- `PrintsService/Clases/IMS.vb`
- _(+51 más)_

**BOF-2028** (71 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `CEALSAMI3/Entity/Entity.vbproj`
- `CEALSAMI3/DAL/DAL.vbproj`
- `DMS/Clases/IMS.vb`
- `DMS/Clases/clsTabla.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `MES/Clases_AP/clsTabla.vb`
- `PrintsService/Clases/IMS.vb`
- _(+61 más)_

**HH-2023** (3 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacionList.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`

**HH-2028** (3 archivos):
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacion.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Indice_Rotacion/clsBeIndice_rotacionList.java`
- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`

---

### `producto_estado`

**BOF-2023** (55 archivos):
- `MI3/Producto_Estado_Ubic/ProductoEstadoUbicacion.svc.vb`
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/wsTest.asmx.vb`
- `SW/Service References/WCFRecepcion/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Detalle/clsLnTrans_inv_detalle_grid.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Resumen/clsLnTrans_inv_resumen_grid.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Stock/clsLnTrans_inv_stock_partial.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_enc_reconteo_Partial.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- _(+45 más)_

**BOF-2028** (59 archivos):
- `MI3/Producto_Estado_Ubic/ProductoEstadoUbicacion.svc.vb`
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/wsTest.asmx.vb`
- `SW/Service References/WCFRecepcion/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Detalle/clsLnTrans_inv_detalle_grid.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Resumen/clsLnTrans_inv_resumen_grid.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Stock/clsLnTrans_inv_stock_partial.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_enc_reconteo_Partial.vb`
- _(+49 más)_

**HH-2023** (4 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Recepcion/Trans_re_det/clsBeTrans_re_det.java`
- `app/src/main/java/com/dts/ladapt/list_adapt_detalle_rec_prod.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_list_rec_prod_detalle.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

**HH-2028** (5 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Recepcion/Trans_re_det/clsBeTrans_re_det.java`
- `app/src/main/java/com/dts/ladapt/list_adapt_detalle_rec_prod.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_list_rec_prod_detalle.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos_original.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

---

### `serializado`

**BOF-2023** (45 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `CEALSAMI3/DAL/DAL.vbproj`
- `CEALSAMI3/Entity/Entity.vbproj`
- `DMS/Clases/IMS.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `PrintsService/Clases/IMS.vb`
- `Quick_Tag/Clases_AP/IMS.vb`
- `SAPMI3PC/Clases/IMS.vb`
- _(+35 más)_

**BOF-2028** (46 archivos):
- `CEALSAMI3/Clases/IMS.vb`
- `CEALSAMI3/Entity/Entity.vbproj`
- `CEALSAMI3/DAL/DAL.vbproj`
- `DMS/Clases/IMS.vb`
- `DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb`
- `DynamicsNavInterface/Clases/IMS.vb`
- `MES/Clases_AP/IMS.vb`
- `PrintsService/Clases/IMS.vb`
- `Quick_Tag/Clases_AP/IMS.vb`
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- _(+36 más)_

---

### `genera_lp`

**BOF-2023** (23 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoPresentacion/clsLnProducto_presentacion.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoPresentacion/clsLnProducto_presentacion_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/ProductoPresentacion/clsBeProducto_presentacion.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+13 más)_

**BOF-2028** (30 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoPresentacion/clsLnProducto_presentacion.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoPresentacion/clsLnProducto_presentacion_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/ProductoPresentacion/clsBeProducto_presentacion.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+20 más)_

**HH-2023** (1 archivos):
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

**HH-2028** (2 archivos):
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos_original.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

---

### `control_vencimiento`

**BOF-2023** (15 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+5 más)_

**BOF-2028** (23 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+13 más)_

---

### `AJCANTN`

**BOF-2023** (20 archivos):
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_Partial.vb`
- `TOMIMSV4/DAL/Sistema_Jornada/clsLnJornada_sistema_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`
- `TOMIMSV4/Entity/Inventario/clsDataContractDI.vb`
- `TOMIMSV4/Entity/Transacciones/Movimiento/clsBeVW_Movimientos_Poliza.vb`
- `TOMIMSV4/Entity/Transacciones/Stock/Retroactivo/clsBeVW_MovimientosRetro.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Analítica/frmAnaliticaA.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/MI3_Log/frmLogMI3.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Movimiento/frmMovimientosPoliza.vb`
- _(+10 más)_

**BOF-2028** (22 archivos):
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_Partial.vb`
- `TOMIMSV4/DAL/Sistema_Jornada/clsLnJornada_sistema_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`
- `TOMIMSV4/Entity/Inventario/clsDataContractDI.vb`
- `TOMIMSV4/Entity/Transacciones/Movimiento/clsBeVW_Movimientos_Poliza.vb`
- `TOMIMSV4/Entity/Transacciones/Stock/Retroactivo/clsBeVW_MovimientosRetro.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Analítica/frmAnaliticaA.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/MI3_Log/frmLogMI3.vb`
- `TOMIMSV4/TOMIMSV4/Reportes/Movimiento/frmMovimientosDoc.vb`
- _(+12 más)_

**HH-2023** (1 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Movimiento/clsBeVW_Movimientos/clsBeVW_Movimientos.java`

**HH-2028** (1 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Movimiento/clsBeVW_Movimientos/clsBeVW_Movimientos.java`

---

### `control_lote`

**BOF-2023** (15 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+5 más)_

**BOF-2028** (21 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+11 más)_

---

### `control_peso`

**BOF-2023** (15 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Inventario/Ciclico/clsBeTrans_inv_ciclico_vw.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+5 más)_

**BOF-2028** (20 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Inventario/Ciclico/clsBeTrans_inv_ciclico_vw.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- _(+10 más)_

**HH-2023** (5 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Inventario/InventarioReconteo/clsBe_inv_reconteo_data.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_add.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_conteo.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioInicial/frm_inv_ini_conteo.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

**HH-2028** (6 archivos):
- `app/src/main/java/com/dts/classes/Transacciones/Inventario/InventarioReconteo/clsBe_inv_reconteo_data.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_conteo.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_add.java`
- `app/src/main/java/com/dts/tom/Transacciones/InventarioInicial/frm_inv_ini_conteo.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos_original.java`
- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

---

### `genera_lote`

**BOF-2023** (14 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- `TOMIMSV4/TOMIMSV4/Service References/wsTOMHH/Reference.vb`
- _(+4 más)_

**BOF-2028** (18 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TESTWCFHH/Web References/ServiceGenClient/Reference.vb`
- `TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_vw_Partial.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_SelectionList.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto_Partial.vb`
- `TOMIMSV4/Entity/Mantenimientos/Producto/Producto/clsBeProducto.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- `TOMIMSV4/TOMIMSV4/Service References/wsTOMHH/Reference.vb`
- _(+8 más)_

---

### `IdProductoEstado_NC`

**BOF-2023** (8 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `SAPMI3PC/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `SAPSYNCCUMBRE/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `SAPSYNCMAMPA/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `SAPSYNC_Killios/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`

**BOF-2028** (11 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `SAPMI3PC/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `SAPSYNCCUMBRE/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `SAPSYNCMAMPA/Clases Interface Sync/Factura_Reserva/clsSyncSapFacturaReserva.vb`
- `SAPSYNC_Killios/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- _(+1 más)_

---

### `genera_lp_old`

**BOF-2023** (6 archivos):
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`
- `WMSWebAPI/Dal/Producto/clsLnProducto.cs`
- `WMSWebAPI/Entity/Producto/clsBeProducto.cs`

**BOF-2028** (10 archivos):
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/TOMIMSV4/Configuracion_QADataSet2.Designer.vb`
- `WMS.DALCore/Producto/clsLnProducto.cs`
- `WMS.EntityCore/Dtos/Productos/Producto3PL_Dto.cs`
- `WMS.EntityCore/Dtos/Productos/ProductoDto.cs`
- `WMS.EntityCore/Producto/clsBeProducto.cs`
- `WMS.EntityCore/Producto/clsBeProducto_3PL.cs`
- `WMS.EntityCore/Producto/ProductoMi3/clsBeProductoMi3.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `Ejecutar_En_Despacho_Automaticamente`

**BOF-2023** (7 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Transacciones/Despacho/frmDespacho.vb`
- `TOMIMSV4/Transacciones/Despacho/frmDespacho.vb`
- `WSHHRN/TOMHHWS.asmx.vb`

**BOF-2028** (9 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Transacciones/Despacho/frmDespacho.vb`
- `TOMIMSV4/Transacciones/Despacho/frmDespacho.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMS.EntityCore/Interface/clsBeI_nav_config_enc.cs`
- `WSHHRN/TOMHHWS.asmx.vb`

---

### `considerar_paletizado_en_reabasto`

**BOF-2023** (5 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`

**BOF-2028** (8 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMS.DALCore/Stock/clsLnStock_res.cs`
- `WMS.EntityCore/Interface/clsBeI_nav_config_enc.cs`

---

### `equiparar_cliente_con_propietario_en_doc_salida`

**BOF-2023** (5 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Transacciones/Pedido/frmPedido.vb`
- `TOMIMSV4/Transacciones/Pedido/frmPedido.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (7 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Transacciones/Pedido/frmPedido.vb`
- `TOMIMSV4/Transacciones/Pedido/frmPedido.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMS.EntityCore/Interface/clsBeI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `dias_vida_defecto_perecederos`

**BOF-2023** (5 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`

**BOF-2028** (6 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `push_ingreso_nav_desde_hh`

**BOF-2023** (5 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`

**BOF-2028** (6 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb`
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `generar_pedido_ingreso_bodega_destino`

**BOF-2023** (4 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Service References/wsTOMHH/Reference.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (6 archivos):
- `DynamicsNavInterface/Web References/WebReference/Reference.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Service References/wsTOMHH/Reference.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMS.DALCore/Pedido/clsLnTrans_pe_tipo.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `generar_recepcion_auto_bodega_destino`

**BOF-2023** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/TipoPedido/clsLnTrans_pe_tipo.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (6 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/TipoPedido/clsLnTrans_pe_tipo.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMS.DALCore/Pedido/clsLnTrans_pe_tipo.cs`
- `WMS.EntityCore/Pedido/clsBeTrans_pe_tipo.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `explosion_automatica`

**BOF-2023** (3 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`

**BOF-2028** (4 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `explosion_automatica_desde_ubicacion_picking`

**BOF-2023** (3 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`

**BOF-2028** (4 archivos):
- `DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.Designer.vb`
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.Designer.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `interface_sap`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Bodega/Bodega/clsLnBodega.vb`

**BOF-2028** (4 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Bodega/Bodega/clsLnBodega.vb`
- `WMS.DALCore/Datos_Maestros/clsLnBodega.cs`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `rechazar_pedido_incompleto`

**BOF-2023** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (4 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `cantidad_en_presentacion_transacciones_out`

**BOF-2028** (4 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Interface/Transacciones_Out/clsLnI_nav_transacciones_out_partial.vb`
- `TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Configuracion_Interface/frmConfiguracion.vb`

---

### `codigo_bodega_erp_nc`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoEstado/clsLnProducto_estado_Partial.vb`

**BOF-2028** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `TOMIMSV4/DAL/Mantenimientos/Producto/ProductoEstado/clsLnProducto_estado_Partial.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `convertir_decimales_a_umbas`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `crear_recepcion_de_compra_nav`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `crear_recepcion_de_transferencia_nav`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `despachar_existencia_parcial`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (3 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `cealsa_vwacuerdocomercialenc`

**BOF-2023** (2 archivos):
- `TOMIMSV4/DAL/Interface/Cealsa/Acuerdo_Enc/clsLnCEALSA_acuerdoscomerciales.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Cealsa/Acuerdo_Enc/clsLnCEALSA_acuerdoscomerciales.vb`
- `WMSPortal/Library/Database/ApplicationDbContext.cs`

---

### `bodega_facturacion`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `bodega_prorrateo`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `bodega_prorrateo1`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `conservar_zona_picking_clavaud`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `equiparar_productos`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `excluir_ubicaciones_reabasto`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `explosion_automatica_nivel_max`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `implosion_automatica`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `inferir_bonificacion_pedido_sap`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `rechazar_bonificacion_incompleta`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `reservar_umbas_primero`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `sap_control_draft_ajustes`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `sap_control_draft_traslados`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `valida_solo_codigo`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `vence_defecto_nc`

**BOF-2023** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

**BOF-2028** (2 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `bodega_faltante`

**BOF-2028** (1 archivos):
- `TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc.vb`

---

### `considerar_disponibilidad_ubicacion_reabasto`

**BOF-2028** (1 archivos):
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `excluir_recepcion_picking`

**BOF-2028** (1 archivos):
- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs`

---

### `danado_picking`

**HH-2023** (3 archivos):
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_danado_picking.java`
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_list_prod_reemplazo_picking.java`
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_picking_datos.java`

**HH-2028** (3 archivos):
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_danado_picking.java`
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_list_prod_reemplazo_picking.java`
- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_picking_datos.java`

---

