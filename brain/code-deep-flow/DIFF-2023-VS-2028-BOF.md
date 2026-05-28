---
tipo: other
clientes: [mampa]
ramas_afectadas: [dev_2023_estable, dev_2028_merge]
---
# DIFF BOF 2023 (dev_2023_estable) vs 2028 (dev_2028_merge)

> Generado: 2026-05-27 — 2,342 archivos base → 2,447 archivos actuales

## Estadísticas

| Categoría | Archivos | Funciones |
|---|---|---|
| ADDED (nuevos en 2028) | 107 | +674 |
| REMOVED (solo en 2023) | 2 | -29 |
| MODIFIED | 464 | +313 / -262 |

## Archivos ADDED (nuevos en 2028)

| Archivo | Líneas | Funciones |
|---|---|---|
| `frmPreFactura - Copia.vb` | 5177 | 12 |
| `frmVerificacionBOF.vb` | 1479 | 19 |
| `frmImportarAjusteExcel.vb` | 1362 | 17 |
| `frmImportarAjusteExcel.vb` | 1357 | 17 |
| `frmImpresion_OC.vb` | 1343 | 14 |
| `rptDespachoMampa.Designer.vb` | 1334 | 1 |
| `rptPackingTallaColor.Designer.vb` | 1333 | 1 |
| `frmPreFactura - Copia.Designer.vb` | 1264 | 1 |
| `frmIndicadorAjusteProveedor.vb` | 1185 | 15 |
| `clsLnI_nav_barras_rfid_enc.vb` | 1183 | 0 |
| `frmImpresion_OC_RFID.vb` | 1001 | 15 |
| `frmInventarioRFID.Designer.vb` | 971 | 1 |
| `frmInventarioRFID.vb` | 964 | 17 |
| `frmImpresion_OC.Designer.vb` | 962 | 1 |
| `rptControlCalidadCambioEstadoMampa.Designer.vb` | 927 | 1 |
| `clsLnTrans_inv_ciclico_rfid.vb` | 868 | 0 |
| `FrmNotificacionDestinatarioReglaMnt.Designer.vb` | 798 | 1 |
| `FrmNotificacionContactoBodegaMnt.vb` | 726 | 15 |
| `frmImportarAjusteExcel.Designer.vb` | 720 | 1 |
| `FrmNotificacionPlantillaMnt.Designer.vb` | 709 | 1 |
| `clsLnNotificacionDestinatarioRegla.vb` | 674 | 0 |
| `FrmNotificacionLayoutMnt.Designer.vb` | 632 | 1 |
| `clsLnNotificacionCola.vb` | 608 | 0 |
| `FrmNotificacionEventoMnt.Designer.vb` | 577 | 1 |
| `FrmNotificacionContactoBodegaMnt.Designer.vb` | 572 | 1 |
| `rptAjuste_TallaColor.Designer.vb` | 531 | 1 |
| `clsLnI_nav_barras_rfid_det.vb` | 519 | 0 |
| `frmVerificacionBOF.Designer.vb` | 502 | 1 |
| `frmPropietarioReglasMensajes.vb` | 471 | 22 |
| `frmIndicadorAjusteProveedor.Designer.vb` | 447 | 1 |
| `clsLnNotificacionContactoBodega.vb` | 445 | 0 |
| `clsLnLog_verificacion_bof.vb` | 431 | 0 |
| `FrmNotificacionEventoVariableMnt.Designer.vb` | 431 | 1 |
| `clsLnLog_sincronizacion_nube.vb` | 414 | 0 |
| `clsLnNotificacionPlantilla.vb` | 403 | 0 |
| `clsLnTrans_pe_ref_mi3.vb` | 400 | 0 |
| `clsLnNotificacionLayout.vb` | 391 | 0 |
| `clsLnI_nav_barras_rfid_stock.vb` | 391 | 0 |
| `clsLnVerificacion_motivo.vb` | 383 | 0 |
| `clsLnI_nav_barras_rfid_mov.vb` | 381 | 0 |
| `FrmNotificacionDestinatarioReglaMnt.vb` | 373 | 15 |
| `clsLnI_nav_barras_rfid_tipo_mov.vb` | 351 | 0 |
| `clsLnBodegaNotificacionContacto.vb` | 345 | 0 |
| `frmPropietarioReglasMensajes.Designer.vb` | 345 | 1 |
| `clsLnNotificacionEvento.vb` | 343 | 0 |
| `FrmNotificacionPlantillaMnt.vb` | 337 | 16 |
| `clsLnLog_sincronizacion_fallos.vb` | 336 | 0 |
| `clsLnVerificacion_estado.vb` | 326 | 0 |
| `frmDocIngresoRFID.Designer.vb` | 326 | 1 |
| `frmDocSalidaRFID.Designer.vb` | 326 | 1 |
| `FrmNotificacionLayoutMnt.vb` | 313 | 16 |
| `clsLnNotificacionEventoVariable.vb` | 299 | 0 |
| `FrmNotificacionEventoMnt.vb` | 293 | 16 |
| `FrmNotificacionEventoVariableMnt.vb` | 273 | 16 |
| `clsLnNotificacion.vb` | 260 | 0 |
| `frmImpresion_OC_RFID.Designer.vb` | 255 | 1 |
| `clsLnBodegaProxy.vb` | 242 | 19 |
| `frmExistenciasRFID.Designer.vb` | 242 | 1 |
| `frmInventarioProductosRFID.vb` | 236 | 9 |
| `clsLnNotificacionContacto.vb` | 230 | 0 |
| `frmExistenciasRFID.vb` | 221 | 9 |
| `frmInventarioRFID_Lista.vb` | 220 | 9 |
| `frmDocIngresoRFID_List.Designer.vb` | 216 | 1 |
| `frmDocSalidaRFID_List.Designer.vb` | 216 | 1 |
| `frmInventarioProductosRFID.Designer.vb` | 215 | 1 |
| `frmInventarioRFID_Lista.Designer.vb` | 203 | 1 |
| `clsBENotificacionCola.vb` | 187 | 22 |
| `frmDocIngresoRFID.vb` | 181 | 7 |
| `frmDocSalidaRFID.vb` | 175 | 7 |
| `frmDocIngresoRFID_List.vb` | 167 | 8 |
| `frmDocSalidaRFID_List.vb` | 163 | 7 |
| `DGridLegacyAccess.vb` | 153 | 8 |
| `frmResultadoValidacionCambioUbic.Designer.vb` | 142 | 1 |
| `frmResultadoValidacionCambioUbic.vb` | 120 | 8 |
| `ConfigManager.vb` | 53 | 0 |
| `clsBeTrans_inv_ciclico_rfid.vb` | 37 | 24 |
| `clsBeTrans_packing_enc_Partial.vb` | 34 | 5 |
| `clsBeI_nav_barras_rfid_stock.vb` | 31 | 20 |
| `clsBeI_nav_barras_rfid_mov.vb` | 29 | 18 |
| `clsBeTrans_pe_ref_mi3.vb` | 28 | 17 |
| `clsBeI_nav_barras_rfid_enc.vb` | 26 | 14 |
| `clsBeI_nav_barras_rfid_tipo_mov.vb` | 23 | 12 |
| `clsBeLog_verificacion_bof.vb` | 23 | 16 |
| `clsBeLog_sincronizacion_fallos.vb` | 21 | 9 |
| `clsBeLog_sincronizacion_nube.vb` | 21 | 11 |
| `clsBENotificacionDestinatarioRegla.vb` | 21 | 18 |
| `CryptoHelper.vb` | 20 | 0 |
| `clsBEBodegaNotificacionContacto.vb` | 19 | 17 |
| `clsBeVerificacion_motivo.vb` | 19 | 8 |
| `clsBENotificacionPlantilla.vb` | 18 | 15 |
| `clsBeI_nav_barras_rfid_det.vb` | 18 | 7 |
| `clsBeVerificacion_estado.vb` | 18 | 7 |
| `clsBENotificacionContacto.vb` | 17 | 14 |
| `clsBENotificacionLayout.vb` | 15 | 12 |
| `rptDespachoMampa.vb` | 14 | 3 |
| `rptPackingTallaColor.vb` | 13 | 3 |
| `clsBENotificacionEvento.vb` | 12 | 10 |
| `clsBENotificacionContactoBodega.vb` | 11 | 8 |
| `clsBENotificacionEventoVariable.vb` | 10 | 7 |
| `dsRepAjustes.vb` | 10 | 0 |
| `Smtp_Configuracion.vb` | 8 | 6 |
| `clsBeResultadoValidacionCambioUbic.vb` | 8 | 6 |
| `clsBeI_nav_barras_rfid_enc_Partial.vb` | 7 | 4 |
| `clsBeI_nav_barras_rfid_det_Partial.vb` | 6 | 2 |
| `clsBeTrans_ajuste_enc_Partial.vb` | 5 | 0 |
| `rptAjuste_TallaColor.vb` | 3 | 0 |
| `rptControlCalidadCambioEstadoMampa.vb` | 3 | 0 |

## Archivos MODIFIED top 50 (por delta de líneas)

| Archivo | L:2023 | L:2028 | ΔL | +F/-F | Directorio |
|---|---|---|---|---|---|
| `frmAjusteStock.vb` | 4004 | 7854 | +3850 | +8/-7 | `TOMIMSV4/Transacciones/Ajustes` |
| `frmPedido_List.vb` | 1472 | 3773 | +2301 | +13/-17 | `TOMIMSV4/Transacciones/Pedido` |
| `clsLnStock_res_Partial.vb` | 34221 | 35958 | +1737 | +0/-0 | `DAL/Transacciones/Stock_Reservado` |
| `clsLnTrans_ubic_hh_det_Partial.vb` | 1277 | 2840 | +1563 | +0/-0 | `DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det` |
| `frmPedido.vb` | 12347 | 13559 | +1212 | +5/-7 | `TOMIMSV4/Transacciones/Pedido` |
| `frmCargaExcel.vb` | 5101 | 6311 | +1210 | +2/-2 | `TOMIMSV4/Mantenimientos/Carga_Excel` |
| `clsLnI_nav_ped_traslado_enc_Partial.vb` | 3463 | 4550 | +1087 | +3/-0 | `DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc` |
| `clsLnProducto_talla_color.vb` | 540 | 1360 | +820 | +0/-0 | `DAL/Mantenimientos/Producto/Producto_Talla_Color` |
| `clsLnTrans_picking_det_Partial.vb` | 3006 | 2194 | -812 | +0/-0 | `DAL/Transacciones/Picking` |
| `frmInventarioImport.vb` | 1014 | 1714 | +700 | +12/-13 | `TOMIMSV4/Transacciones/Inventario` |
| `frmBodegaSelUbic.vb` | 1543 | 2239 | +696 | +2/-2 | `TOMIMSV4/Mantenimientos/Bodega` |
| `clsLnTrans_re_enc_Partial.vb` | 8930 | 9603 | +673 | +0/-0 | `DAL/Transacciones/Recepcion/Recepcion_Encabezado` |
| `clsLnProducto_Partial.vb` | 10803 | 11437 | +634 | +0/-0 | `DAL/Mantenimientos/Producto/Producto` |
| `clsLnI_nav_barras_pallet.vb` | 929 | 1518 | +589 | +0/-0 | `DAL/Interface/Barras_Pallet` |
| `clsLnTrans_pe_det_Partial.vb` | 4937 | 5470 | +533 | +0/-0 | `DAL/Transacciones/Pedido` |
| `clsLnTrans_re_det_Partial.vb` | 3053 | 3585 | +532 | +0/-0 | `DAL/Transacciones/Recepcion/Recepcion_Detalle` |
| `frmRecepcion.vb` | 12501 | 13033 | +532 | +2/-2 | `TOMIMSV4/Transacciones/Recepcion` |
| `clsLnTrans_pe_enc_Partial.vb` | 7232 | 7737 | +505 | +0/-0 | `DAL/Transacciones/Pedido` |
| `clsLnI_nav_ped_compra_enc_Partial.vb` | 4146 | 3689 | -457 | +0/-0 | `DAL/Interface/Pedido_Compra/Pedido_Compra_Enc` |
| `frmPicking.vb` | 5636 | 6079 | +443 | +3/-3 | `TOMIMSV4/Transacciones/Picking` |
| `frmListaStockControlCalidad.vb` | 1025 | 1456 | +431 | +3/-2 | `TOMIMSV4/Transacciones/Control_Calidad/Cambio_Estado` |
| `clsLnTrans_inv_stock_prod_Partial.vb` | 738 | 1162 | +424 | +0/-0 | `DAL/Inventario/Inicial/Stock_Prod` |
| `clsLnTrans_oc_det_Partial.vb` | 2628 | 3025 | +397 | +0/-0 | `DAL/Transacciones/OrdenCompra/OC_Detalle` |
| `frmProducto_List.vb` | 1234 | 1608 | +374 | +2/-3 | `TOMIMSV4/Mantenimientos/Producto` |
| `clsLnI_nav_ped_compra_enc.vb` | 520 | 884 | +364 | +0/-0 | `DAL/Interface/Pedido_Compra/Pedido_Compra_Enc` |
| `frmBodega.Designer.vb` | 6891 | 7242 | +351 | +0/-0 | `TOMIMSV4/Mantenimientos/Bodega` |
| `clsLnTrans_ajuste_enc_Partial.vb` | 778 | 1125 | +347 | +0/-0 | `DAL/Mantenimientos/Ajustes` |
| `frmOrdenCompra.vb` | 8706 | 9041 | +335 | +0/-0 | `TOMIMSV4/Transacciones/Orden_Compra` |
| `clsLnTrans_picking_det.vb` | 884 | 1190 | +306 | +0/-0 | `DAL/Transacciones/Picking` |
| `frmMenu.vb` | 5412 | 5714 | +302 | +0/-0 | `TOMIMSV4/Mantenimientos/Principales` |
| `clsLnTrans_pe_det_log_reserva_partial.vb` | 227 | 517 | +290 | +0/-0 | `DAL/Transacciones/Pedido/Log_Reserva` |
| `frmAjustePositivo.vb` | 271 | 559 | +288 | +7/-6 | `TOMIMSV4/Transacciones/Ajustes/Ajustes_Positivos` |
| `clsLnTrans_inv_enc_Partial.vb` | 3541 | 3823 | +282 | +0/-0 | `DAL/Inventario` |
| `frmBodega.vb` | 5262 | 5543 | +281 | +2/-4 | `TOMIMSV4/Mantenimientos/Bodega` |
| `clsLnTalla.vb` | 653 | 930 | +277 | +0/-0 | `DAL/Mantenimientos/Producto/Producto_Talla` |
| `frmAjusteStock.Designer.vb` | 1243 | 1514 | +271 | +0/-0 | `TOMIMSV4/Transacciones/Ajustes` |
| `clsLnColor.vb` | 509 | 774 | +265 | +0/-0 | `DAL/Mantenimientos/Producto/Producto_Color` |
| `frmDespacho.vb` | 3971 | 4236 | +265 | +3/-5 | `TOMIMSV4/Transacciones/Despacho` |
| `clsLnI_nav_ped_traslado_enc.vb` | 704 | 967 | +263 | +0/-0 | `DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc` |
| `frmPreFactura.vb` | 5128 | 5388 | +260 | +5/-7 | `TOMIMSV4/Transacciones/PreFactura` |
| `IMS.vb` | 3470 | 3708 | +238 | +0/-0 | `TOMIMSV4/Clases_AP` |
| `frmRecepcionBOF.vb` | 7479 | 7263 | -216 | +0/-0 | `TOMIMSV4/Transacciones/Recepcion_BOF` |
| `clsLnTrans_pe_det.vb` | 811 | 600 | -211 | +0/-0 | `DAL/Transacciones/Pedido` |
| `clsLnI_nav_transacciones_out_partial.vb` | 4329 | 4536 | +207 | +0/-0 | `DAL/Interface/Transacciones_Out` |
| `clsLnStock_Partial.vb` | 17053 | 16847 | -206 | +0/-0 | `DAL/Transacciones/Stock` |
| `clsLnTrans_oc_enc_Partial.vb` | 3968 | 4165 | +197 | +0/-0 | `DAL/Transacciones/OrdenCompra/OC_Encabezado` |
| `clsLnProducto.vb` | 745 | 937 | +192 | +0/-0 | `DAL/Mantenimientos/Producto/Producto` |
| `clsLnTrans_ajuste_det_borrador.vb` | 373 | 561 | +188 | +0/-0 | `DAL/Mantenimientos/Ajustes` |
| `clsLnTrans_picking_ubic.vb` | 1225 | 1412 | +187 | +0/-0 | `DAL/Transacciones/Picking` |
| `frmOrdenCompra.designer.vb` | 3937 | 4120 | +183 | +0/-0 | `TOMIMSV4/Transacciones/Orden_Compra` |