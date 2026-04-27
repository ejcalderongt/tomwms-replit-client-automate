---
id: db-brain-sp-clbd-prc
type: db-sp
title: dbo.CLBD_PRC
schema: dbo
name: CLBD_PRC
kind: sp
modify_date: 2018-09-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.CLBD_PRC`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-09-27 |

## Consume

- `Appointments`
- `i_nav_ejecucion_det_error`
- `i_nav_ejecucion_enc`
- `i_nav_ejecucion_res`
- `I_nav_transacciones_out`
- `producto_pallet`
- `stock`
- `stock_hist`
- `stock_parametro`
- `stock_rec`
- `stock_res`
- `stock_res_se`
- `stock_se`
- `stock_se_rec`
- `tarea_hh`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_inv_ciclico`
- `trans_inv_ciclico_ubic`
- `trans_inv_detalle`
- `trans_inv_enc`
- `trans_inv_operador`
- `trans_inv_reconteo`
- `trans_inv_resumen`
- `trans_inv_stock`
- `trans_inv_stock_prod`
- `trans_inv_tramo`
- `trans_inventario_det`
- `trans_inventario_enc`
- `trans_movimientos`
- `trans_oc_det`
- `trans_oc_det_lote`
- `trans_oc_enc`
- `trans_oc_imagen`
- `trans_oc_pol`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_det_parametros`
- `trans_picking_enc`
- `trans_picking_op`
- `trans_picking_ubic`
- `trans_re_det`
- `trans_re_det_infraccion`
- `trans_re_det_parametros`
- `trans_re_enc`
- `trans_re_fact`
- `trans_re_img`
- `trans_re_oc`
- `trans_re_op`
- `trans_tras_det`
- `trans_tras_enc`
- `trans_tras_op`
- `trans_ubic_hh_det`
- `trans_ubic_hh_enc`
- `trans_ubic_hh_op`
- `trans_ubic_hh_se`
- `trans_ubic_hh_stock`
- `trans_ubic_tarima`
- `transacciones_log`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[CLBD_PRC]
AS
BEGIN
DELETE FROM transacciones_log
DELETE FROM trans_ubic_tarima
DELETE FROM trans_ubic_hh_se
DELETE FROM trans_ubic_hh_op
DELETE FROM trans_ubic_hh_enc
DELETE FROM trans_ubic_hh_det
DELETE FROM trans_ubic_hh_stock
DELETE FROM trans_tras_op
DELETE FROM trans_tras_enc
DELETE FROM trans_tras_det
DELETE FROM trans_re_op
DELETE FROM trans_re_oc
DELETE FROM trans_re_img
DELETE FROM trans_re_fact
DELETE FROM producto_pallet
DELETE FROM trans_re_enc
DELETE FROM trans_re_det_parametros
DELETE FROM trans_re_det_infraccion
DELETE FROM trans_re_det
DELETE FROM trans_picking_ubic
DELETE FROM trans_picking_op
DELETE FROM trans_picking_enc
DELETE FROM trans_picking_det_parametros
DELETE FROM trans_picking_det
DELETE FROM trans_pe_det
DELETE FROM trans_pe_enc
DELETE FROM trans_oc_pol
DELETE FROM trans_oc_imagen
DELETE FROM trans_oc_det_lote
DELETE FROM trans_oc_det
DELETE FROM trans_oc_enc
DELETE FROM trans_movimientos
DELETE FROM trans_inventario_enc
DELETE FROM trans_inventario_det
DELETE FROM trans_despacho_enc
DELETE FROM trans_despacho_det
DELETE FROM tarea_hh
DELETE FROM stock_se_rec
DELETE FROM stock_se
DELETE FROM stock_res_se
DELETE FROM stock_res
DELETE FROM stock_rec
DELETE FROM stock_parametro
DELETE FROM stock_hist
DELETE FROM stock
DELETE FROM trans_inv_detalle
DELETE FROM trans_inv_resumen
DELETE FROM trans_inv_reconteo
DELETE FROM trans_inv_ciclico
DELETE FROM trans_inv_ciclico_ubic
DELETE FROM trans_inv_tramo
DELETE FROM trans_inv_operador
DELETE FROM trans_inv_stock
DELETE FROM trans_inv_stock_prod
DELETE FROM trans_inv_enc
DELETE FROM Appointments
DELETE FROM i_nav_ejecucion_det_error
DELETE FROM i_nav_ejecucion_res
DELETE FROM i_nav_ejecucion_enc
DELETE FROM I_nav_transacciones_out
END
```
