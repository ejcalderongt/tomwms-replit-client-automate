---
tipo: other
ramas_afectadas: [dev_2023_estable, dev_2028_merge]
---
# TOM WMS Atlas — BOF/HH 2023↔2028 + Cross-Cliente

> Generado: 2026-05-27 | Ramas: `dev_2028_merge` (activa) · `dev_2023_estable` (baseline)

## Índice

| Sección | Path | Estado |
|---|---|---|
| F1: Inventario de código | `code-deep-flow/INVENTARIO-STATS.md` | DONE |
| F2: Diffs 2023↔2028 BOF | `code-deep-flow/DIFF-2023-VS-2028-BOF.md` | DONE |
| F2: Diffs 2023↔2028 HH | `code-deep-flow/DIFF-2023-VS-2028-HH.md` | DONE |
| F4: Snapshots cross-cliente | `data-deep-dive/<cliente>/` | DONE |
| F4: Cross-comparativa | `data-deep-dive/CROSS-COMPARATIVA.md` | DONE |
| Bugs conocidos | `wms-known-issues/` | DONE |
| Learnings | `learnings/` | DONE |

## Resumen ejecutivo BOF 2023→2028

| Métrica | Valor |
|---|---|
| Archivos 2023 | 2,342 |
| Archivos 2028 | 2,447 |
| Archivos nuevos | 107 |
| Archivos eliminados | 2 |
| Archivos modificados | 464 |
| Funciones netas | +987 / -291 |

### Top 15 archivos BOF más modificados (2023→2028)

| Archivo | Líneas 2023 | Líneas 2028 | Delta | +funcs/-funcs |
|---|---|---|---|---|
| `frmAjusteStock.vb` | 4004 | 7854 | +3850 | +8/-7 |
| `frmPedido_List.vb` | 1472 | 3773 | +2301 | +13/-17 |
| `clsLnStock_res_Partial.vb` | 34221 | 35958 | +1737 | +0/-0 |
| `clsLnTrans_ubic_hh_det_Partial.vb` | 1277 | 2840 | +1563 | +0/-0 |
| `frmPedido.vb` | 12347 | 13559 | +1212 | +5/-7 |
| `frmCargaExcel.vb` | 5101 | 6311 | +1210 | +2/-2 |
| `clsLnI_nav_ped_traslado_enc_Partial.vb` | 3463 | 4550 | +1087 | +3/-0 |
| `clsLnProducto_talla_color.vb` | 540 | 1360 | +820 | +0/-0 |
| `frmInventarioImport.vb` | 1014 | 1714 | +700 | +12/-13 |
| `frmBodegaSelUbic.vb` | 1543 | 2239 | +696 | +2/-2 |
| `clsLnTrans_re_enc_Partial.vb` | 8930 | 9603 | +673 | +0/-0 |
| `clsLnProducto_Partial.vb` | 10803 | 11437 | +634 | +0/-0 |
| `clsLnI_nav_barras_pallet.vb` | 929 | 1518 | +589 | +0/-0 |
| `clsLnTrans_pe_det_Partial.vb` | 4937 | 5470 | +533 | +0/-0 |
| `clsLnTrans_re_det_Partial.vb` | 3053 | 3585 | +532 | +0/-0 |

## Resumen ejecutivo HH 2023→2028

| Métrica | Valor |
|---|---|
| Archivos 2023 | 373 |
| Archivos 2028 | 411 |
| Archivos nuevos (activities/classes) | 38 |
| Archivos modificados | 108 |

### Top 15 archivos HH más modificados (2023→2028)

| Archivo | Líneas 2023 | Líneas 2028 | Delta |
|---|---|---|---|
| `frm_recepcion_datos.java` | 10449 | 11924 | +1475 |
| `frm_cambio_ubicacion_ciega.java` | 5090 | 5713 | +623 |
| `frm_consulta_stock.java` | 1199 | 1680 | +481 |
| `frm_inv_cic_add.java` | 2377 | 2740 | +363 |
| `MainActivity.java` | 2013 | 2371 | +358 |
| `Mainmenu.java` | 994 | 1237 | +243 |
| `frm_list_rec_prod.java` | 2174 | 2395 | +221 |
| `frm_detalle_tareas_picking.java` | 1606 | 1750 | +144 |
| `frm_preparacion_packing.java` | 1468 | 1609 | +141 |
| `XMLObject.java` | 397 | 518 | +121 |
| `frm_list_rec_prod_detalle.java` | 1059 | 1175 | +116 |
| `appGlobals.java` | 322 | 409 | +87 |
| `frm_Packing.java` | 2988 | 3060 | +72 |
| `frm_list_prod_reemplazo_picking.java` | 1024 | 1082 | +58 |
| `frm_datos_reabastecimiento.java` | 751 | 801 | +50 |

## Archivos nuevos en HH-2028 (no existían en 2023)

```
LocalBroadcastManager.java
ProgressDialog.java
clsBeColor.java
clsBeColorList.java
clsBeProducto_talla_color.java
clsBeProducto_talla_colorList.java
clsBeTalla.java
clsBeTallaList.java
clsBeTrans_inv_ciclico_rfid.java
clsBeI_nav_barras_rfid_det.java
clsBeI_nav_barras_rfid_detList.java
clsBeI_nav_barras_rfid_enc.java
list_adapt_consulta_detalle_gondola.java
list_adapt_detalle_ciclico_cm.java
list_adapt_detalle_picking_cm.java
list_adapt_detalle_recepcion_cm.java
InventarioRfidAdapter.java
InventarioRfidViewHolder.java
RfidAdapter.java
RfidProcesarAdapter.java
InventarioRfidReaderManager.java
InventarioRfidSearchManager.java
InventarioRfidSelectionManager.java
RFIDController.java
RecepcionRfidReaderManager.java
RecepcionRfidSessionManager.java
RfidToneFeedback.java
RfidSqlite.java
frm_conteo_caja_master.java
frm_detalle_gondola.java
frm_lista_inventario_rfid.java
frm_settings_rfid.java
frm_existencias_rfid.java
frm_guardar_rfid.java
frm_menu_rfid.java
frm_procesar_rfid.java
frm_recepcion_rfid.java
frm_recepcion_rfid_2.java
```

## Archivos nuevos en BOF-2028 (primeras 30)

```
clsLnTrans_inv_ciclico_rfid.vb
clsLnLog_sincronizacion_fallos.vb
clsLnLog_sincronizacion_nube.vb
ConfigManager.vb
clsLnBodegaNotificacionContacto.vb
clsLnNotificacion.vb
clsLnNotificacionCola.vb
clsLnNotificacionContacto.vb
clsLnNotificacionContactoBodega.vb
clsLnNotificacionDestinatarioRegla.vb
clsLnNotificacionEvento.vb
clsLnNotificacionEventoVariable.vb
clsLnNotificacionLayout.vb
clsLnNotificacionPlantilla.vb
clsLnI_nav_barras_rfid_mov.vb
clsLnI_nav_barras_rfid_tipo_mov.vb
clsLnTrans_pe_ref_mi3.vb
clsLnI_nav_barras_rfid_det.vb
clsLnI_nav_barras_rfid_enc.vb
clsLnI_nav_barras_rfid_stock.vb
clsLnLog_verificacion_bof.vb
clsLnVerificacion_estado.vb
clsLnVerificacion_motivo.vb
clsBeTrans_inv_ciclico_rfid.vb
clsBeLog_sincronizacion_fallos.vb
clsBeLog_sincronizacion_nube.vb
clsBeTrans_ajuste_enc_Partial.vb
Smtp_Configuracion.vb
clsBEBodegaNotificacionContacto.vb
clsBENotificacionCola.vb
```

## Clientes con snapshots

| Cliente | Path snapshot |
|---|---|
| `becofarma` | `data-deep-dive/becofarma/snapshot-2026-05-05.md` |
| `byb` | `data-deep-dive/byb/snapshot-2026-05-05.md` |
| `cealsa` | `data-deep-dive/cealsa/snapshot-2026-05-05.md` |
| `killios` | `data-deep-dive/killios/snapshot-2026-05-05.md` |
| `killios_2026` | `data-deep-dive/killios_2026/snapshot-2026-05-05.md` |
| `mampa` | `data-deep-dive/mampa/snapshot-2026-05-05.md` |
| `mercopan` | `data-deep-dive/mercopan/snapshot-2026-05-05.md` |
| `merhonsa` | `data-deep-dive/merhonsa/snapshot-2026-05-05.md` |

## Bugs conocidos

| ID | Descripción | Estado |
|---|---|---|
| BUG-001 | VW_stock_res — stock_ajuste sin AJCANTN | Documentado, fix VW en curso |
| BUG-002 | FK_stock_rec_trans_re_det — identity desincronizado | **FIXED** — 4 commits dev_2028_merge |

## Pendiente Atlas

- F3 flags i_nav_config_enc: patrones no encontrados en código → revisar tabla `i_nav_config_det` directamente en BD
- Cherry-pick 4 commits BUG-002 a `dev_2026_mampa` (awaiting EJC)
- Verificar `GuardarHH_S` y `GuardarHH_CM` para FK recepción