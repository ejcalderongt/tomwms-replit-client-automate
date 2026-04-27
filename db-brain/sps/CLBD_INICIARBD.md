---
id: db-brain-sp-clbd-iniciarbd
type: db-sp
title: dbo.CLBD_INICIARBD
schema: dbo
name: CLBD_INICIARBD
kind: sp
modify_date: 2018-09-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.CLBD_INICIARBD`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-09-18 |

## Consume

- `Appointments`
- `bodega_muelles`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `cliente`
- `cliente_bodega`
- `cliente_direccion`
- `cliente_tiempos`
- `contenedor`
- `estructura_grupo`
- `estructura_tramo`
- `estructura_ubicacion`
- `licencia_item`
- `licencia_llave`
- `licencia_login`
- `licencia_solic`
- `montacarga_servicio_enc`
- `motivo_anulacion_bodega`
- `operador`
- `operador_bodega`
- `p_parametro`
- `pais_departamento`
- `pais_municipio`
- `perfil_serializado`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_codigos_barra`
- `producto_familia`
- `producto_marca`
- `producto_pallet`
- `producto_parametros`
- `producto_presentacion`
- `producto_presentacion_tarima`
- `producto_rellenado`
- `producto_sustituto`
- `producto_tipo`
- `propietario_bodega`
- `propietario_destinatario`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `regla_ubic_det_ir`
- `regla_ubic_det_pe`
- `regla_ubic_det_pp`
- `regla_ubic_det_prop`
- `regla_ubic_det_tp`
- `regla_ubic_det_tr`
- `regla_ubic_enc`
- `regla_ubic_prio_det`
- `regla_ubic_prio_enc`
- `regla_ubic_prio_producto`
- `regla_ubic_sel`
- `regla_ubic_sel_det`
- `regla_ubic_sel_enc`
- `regla_ubicacion`
- `Resources`
- `road_p_vendedor`
- `road_ruta`
- `rol_bodega`
- `rol_menu`
- `stock`
- `stock_hist`
- `stock_parametro`
- `stock_rec`
- `stock_res`
- `stock_res_se`
- `stock_se`
- `stock_se_rec`
- `tablas_sync`
- `tarea_hh`
- `trans_ajuste_det`
- `trans_ajuste_enc`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_inv_ciclico`
- `trans_inv_ciclico_ubic`
- `trans_inv_detalle`
- `trans_inv_enc`
- `trans_inv_enc_reconteo`
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
- `trans_oc_enc`
- `trans_oc_imagen`
- `trans_oc_pol`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_det_parametros`
- `trans_picking_enc`
- `trans_picking_op`

_... +19 más_

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[CLBD_INICIARBD]
AS
BEGIN

/****** Object:  Table transacciones_log    Script Date: 04/08/2016 11:59:09 a.m. ******/
DELETE FROM transacciones_log

/****** Object:  Table trans_ubic_tarima    Script Date: 04/08/2016 11:59:09 a.m. ******/
DELETE FROM trans_ubic_tarima

/****** Object:  Table trans_ubic_hh_se    Script Date: 04/08/2016 11:59:09 a.m. ******/
DELETE FROM trans_ubic_hh_se

/****** Object:  Table trans_ubic_hh_op    Script Date: 04/08/2016 11:59:09 a.m. ******/
DELETE FROM trans_ubic_hh_op

/****** Object:  Table trans_ubic_hh_enc    Script Date: 04/08/2016 11:59:10 a.m. ******/
DELETE FROM trans_ubic_hh_enc

/****** Object:  Table trans_ubic_hh_det    Script Date: 04/08/2016 11:59:10 a.m. ******/
DELETE FROM trans_ubic_hh_det

/****** Object:  Table trans_tras_op    Script Date: 04/08/2016 11:59:10 a.m. ******/
DELETE FROM trans_tras_op

/****** Object:  Table trans_tras_enc    Script Date: 04/08/2016 11:59:11 a.m. ******/
DELETE FROM trans_tras_enc

/****** Object:  Table trans_tras_det    Script Date: 04/08/2016 11:59:11 a.m. ******/
DELETE FROM trans_tras_det

/****** Object:  Table trans_re_op    Script Date: 04/08/2016 11:59:12 a.m. ******/
DELETE FROM trans_re_op

/****** Object:  Table trans_re_oc    Script Date: 04/08/2016 11:59:12 a.m. ******/
DELETE FROM trans_re_oc

/****** Object:  Table trans_re_img    Script Date: 04/08/2016 11:59:12 a.m. ******/
DELETE FROM trans_re_img

/****** Object:  Table trans_re_fact    Script Date: 04/08/2016 11:59:13 a.m. ******/
DELETE FROM trans_re_fact

/****** Object:  Table trans_re_enc    Script Date: 04/08/2016 11:59:13 a.m. ******/
DELETE FROM trans_re_enc

/****** Object:  Table trans_re_det_parametros    Script Date: 04/08/2016 11:59:13 a.m. ******/
DELETE FROM trans_re_det_parametros

/****** Object:  Table trans_re_det_infraccion    Script Date: 04/08/2016 11:59:13 a.m. ******/
DELETE FROM trans_re_det_infraccion

/****** Object:  Table trans_re_det    Script Date: 04/08/2016 11:59:14 a.m. ******/
DELETE FROM trans_re_det

/****** Object:  Table trans_picking_ubic    Script Date: 04/08/2016 11:59:14 a.m. ******/
DELETE FROM trans_picking_ubic

/****** Object:  Table trans_picking_op    Script Date: 04/08/2016 11:59:14 a.m. ******/
DELETE FROM trans_picking_op

/****** Object:  Table trans_picking_enc    Script Date: 04/08/2016 11:59:15 a.m. ******/
DELETE FROM trans_picking_enc

/****** Object:  Table trans_picking_det_parametros    Script Date: 04/08/2016 11:59:15 a.m. ******/
DELETE FROM trans_picking_det_parametros

/****** Object:  Table trans_picking_det    Script Date: 04/08/2016 11:59:15 a.m. ******/
DELETE FROM trans_picking_det

/****** Object:  Table trans_pe_det    Script Date: 04/08/2016 11:59:16 a.m. ******/
DELETE FROM trans_pe_det

/****** Object:  Table trans_pe_enc    Script Date: 04/08/2016 11:59:16 a.m. ******/
DELETE FROM trans_pe_enc

/****** Object:  Table trans_oc_pol    Script Date: 04/08/2016 11:59:17 a.m. ******/
DELETE FROM trans_oc_pol

/****** Object:  Table trans_oc_imagen    Script Date: 04/08/2016 11:59:17 a.m. ******/
DELETE FROM trans_oc_imagen

/****** Object:  Table trans_oc_enc    Script Date: 04/08/2016 11:59:18 a.m. ******/
DELETE FROM trans_oc_enc

/****** Object:  Table trans_oc_det    Script Date: 04/08/2016 11:59:18 a.m. ******/
DELETE FROM trans_oc_det

/****** Object:  Table trans_movimientos    Script Date: 04/08/2016 11:59:19 a.m. ******/
DELETE FROM trans_movimientos

/****** Object:  Table trans_inventario_enc    Script Date: 04/08/2016 11:59:19 a.m. ******/
DELETE FROM trans_inventario_enc

/****** Object:  Table trans_inventario_det    Script Date: 04/08/2016 11:59:19 a.m. ******/
DELETE FROM trans_inventario_det

/****** Object:  Table trans_despacho_enc    Script Date: 04/08/2016 11:59:20 a.m. ******/
DELETE FROM trans_despacho_enc

/****** Object:  Table trans_despacho_det    Script Date: 04/08/2016 11:59:20 a.m. ******/
DELETE FROM trans_despacho_det

/****** Object:  Table trans_inventario_enc    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inventario_enc

/****** Object:  Table trans_inventario_det    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inventario_det

/****** Object:  Table trans_inv_tramo    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_tramo

/****** Object:  Table trans_inv_stock_prod    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_stock_prod

/****** Object:  Table trans_inv_stock    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_stock

/****** Object:  Table trans_inv_resumen    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_resumen

/****** Object:  Table trans_inv_reconteo    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_reconteo

/****** Object:  Table trans_inv_operador    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_operador

/****** Object:  Table trans_inv_enc_reconteo    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_enc_reconteo

/****** Object:  Table trans_inv_enc    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_enc

/****** Object:  Table trans_inv_detalle    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_detalle

/****** Object:  Table trans_inv_ciclico_ubic    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_ciclico_ubic

/****** Object:  Table trans_inv_ciclico    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_inv_ciclico

/****** Object:  Table trans_ajuste_enc    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_ajuste_enc

/****** Object:  Table trans_ajuste_det    Script Date: 23/07/2018 06:12:16 p.m. ******/
DELETE FROM trans_ajuste_det

/****** Object:  Table [dbo].[trans_ubic_tarima]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_tarima

/****** Object:  Table [dbo].[trans_ubic_hh_stock]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_hh_stock

/****** Object:  Table [dbo].[trans_ubic_hh_se]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_hh_se

/****** Object:  Table [dbo].[trans_ubic_hh_op]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_hh_op

/****** Object:  Table [dbo].[trans_ubic_hh_enc]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_hh_enc

/****** Object:  Table [dbo].[trans_ubic_hh_det]    Script Date: 23/07/2018 06:20:07 p.m. ******/
DELETE FROM trans_ubic_hh_det

/****** Object:  Table tarea_hh    Script Date: 04/08/2016 11:59:22 a.m. ******/
DELETE FROM tarea_hh

/****** Object:  Table tablas_sync    Script Date: 04/08/2016 11:59:22 a.m. ******/
DELETE FROM tablas_sync

/****** Object:  Table stock_se_rec    Script Date: 04/08/2016 11:59:23 a.m. ******/
DELETE FROM stock_se_rec

/****** Object:  Table stock_se    Script Date: 04/08/2016 11:59:23 a.m. ******/
DELETE FROM stock_se

/****** Object:  Table stock_res_se    Script Date: 04/08/2016 11:59:23 a.m. ******/
DELETE FROM stock_res_se

/****** Object:  Table stock_res    Script Date: 04/08/2016 11:59:24 a.m. ******/
DELETE FROM stock_res

/****** Object:  Table stock_rec    Script Date: 04/08/2016 11:59:24 a.m. ******/
DELETE FROM stock_rec

/****** Object:  Table stock_parametro    Script Date: 04/08/2016 11:59:24 a.m. ******/
DELETE FROM stock_parametro

/****** Object:  Table stock    Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM stock

/****** Object:  Table stock_hist   Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM stock_hist

/****** Object:  Table regla_ubic_det_tp   Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM regla_ubic_det_tp

/****** Object:  Table regla_ubic_det_ir   Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM regla_ubic_det_ir

/****** Object:  Table regla_ubic_det_pe    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_det_pe

/****** Object:  Table regla_ubic_det_pp    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_det_pp

/****** Object:  Table regla_ubic_det_prop    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_det_prop

/****** Object:  Table regla_ubic_det_tr    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_det_tr

/****** Object:  Table regla_ubic_enc    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_enc

/****** Object:  Table regla_ubic_prio_det    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_prio_det

/****** Object:  Table regla_ubic_prio_enc    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_prio_enc

/****** Object:  Table regla_ubic_prio_producto    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_prio_producto

/****** Object:  Table regla_ubic_sel    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_sel

/****** Object:  Table regla_ubic_sel_det    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_sel_det

/****** Object:  Table regla_ubic_sel_enc    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubic_sel_enc

/****** Object:  Table regla_ubicacion    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM regla_ubicacion

/****** Object:  Table rol_menu    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM rol_menu

/****** Object:  Table rol_bodega    Script Date: 04/08/2016 11:59:28 a.m. ******/
DELETE FROM rol_bodega

/****** Object:  Table road_ruta    Script Date: 04/08/2016 11:59:28 a.m. ******/
DELETE FROM road_ruta

/****** Object:  Table road_p_vendedor    Script Date: 04/08/2016 11:59:29 a.m. ******/
DELETE FROM road_p_vendedor

/****** Object:  Table Resources    Script Date: 04/08/2016 11:59:29 a.m. ******/
DELETE FROM Resources

/****** Object:  Table proveedor_bodega    Script Date: 04/08/2016 11:59:30 a.m. ******/
DELETE FROM proveedor_bodega where idproveedor <> 1

/****** Object:  Table proveedor    Script Date: 04/08/2016 11:59:30 a.m. ******/
DELETE FROM proveedor where idproveedor <> 1

/****** Object:  Table propietarios    Script Date: 04/08/2016 11:59:31 a.m. ******/
DELETE FROM propietarios where IdPropietario <> 1

/****** Object:  Table propietario_destinatario    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM propietario_destinatario where IdPropietario <> 1

/****** Object:  Table propietario_bodega    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM propietario_bodega where IdPropietario <> 1

/****** Object:  Table producto_sustituto    Script Date: 04/08/2016 11:59:33 a.m. ******/
DELETE FROM producto_sustituto

/****** Object:  Table producto_rellenado    Script Date: 04/08/2016 11:59:33 a.m. ******/
DELETE FROM producto_rellenado

/****** Object:  Table producto_presentacion_tarima    Script Date: 04/08/2016 11:59:33 a.m. ******/
DELETE FROM producto_presentacion_tarima

/****** Object:  Table producto_presentacion    Script Date: 04/08/2016 11:59:34 a.m. ******/
DELETE FROM producto_presentacion

/****** Object:  Table producto_parametros    Script Date: 04/08/2016 11:59:34 a.m. ******/
DELETE FROM producto_parametros

/****** Object:  Table producto_pallet    Script Date: 04/08/2016 11:59:34 a.m. ******/
DELETE FROM producto_pallet

/****** Object:  Table producto_marca    Script Date: 04/08/2016 11:59:35 a.m. ******/
DELETE FROM producto_marca

/****** Object:  Table producto_familia    Script Date: 04/08/2016 11:59:35 a.m. ******/
DELETE FROM producto_familia

/****** Object:  Table producto_codis_barra    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_codigos_barra

/****** Object:  Table producto_clasificacion    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_clasificacion

/****** Object:  Table producto_bodega    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_bodega

/****** Object:  Table producto    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM producto

/****** Object:  Table producto_tipo    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM producto_tipo

/****** Object:  Table perfil_serializado    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM perfil_serializado

/****** Object:  Table pais_municipio    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM pais_municipio

/****** Object:  Table pais_departamento    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM pais_departamento

/****** Object:  Table p_parametro    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM p_parametro

/****** Object:  Table operador_bodega    Script Date: 04/08/2016 11:59:39 a.m. ******/
DELETE FROM operador_bodega where IdOperador <>1

/****** Object:  Table operador    Script Date: 04/08/2016 11:59:39 a.m. ******/
DELETE FROM operador  where IdOperador <>1

/****** Object:  Table motivo_anulacion_bodega    Script Date: 04/08/2016 11:59:40 a.m. ******/
DELETE FROM motivo_anulacion_bodega

/****** Object:  Table montacarga_servicio_enc    Script Date: 04/08/2016 11:59:41 a.m. ******/
DELETE FROM montacarga_servicio_enc

/****** Object:  Table contenedor    Script Date: 04/08/2016 11:59:49 a.m. ******/
DELETE FROM contenedor

/****** Object:  Table cliente_tiempos    Script Date: 04/08/2016 11:59:49 a.m. ******/
DELETE FROM cliente_tiempos where idcliente <>1

/****** Object:  Table cliente_direccion    Script Date: 04/08/2016 11:59:50 a.m. ******/
DELETE FROM cliente_direccion

/****** Object:  Table cliente_bodega    Script Date: 04/08/2016 11:59:51 a.m. ******/
DELETE FROM cliente_bodega

/****** Object:  Table cliente    Script Date: 04/08/2016 11:59:51 a.m. ******/
DELETE FROM cliente 

/****** Object:  Table bodega_ubicacion    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_ubicacion where idubicacion < 10153

/****** Object:  Table bodega_tramo    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_tramo WHERE idtramo not in (Select idtramo from bodega_ubicacion)

/****** Object:  Table bodega_sector    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_sector WHERE IdSector not in (Select IdSector from bodega_tramo)

/****** Object:  Table bodega_muelles    Script Date: 04/08/2016 11:59:53 a.m. ******/
DELETE FROM bodega_muelles where idmuelle <>1

/****** Object:  Table Appointments    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM Appointments

/****** Object:  Table licencia_item    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM licencia_item

/****** Object:  Table licencia_llave    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM licencia_llave

/****** Object:  Table licencia_login    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM licencia_login

/****** Object:  Table licencia_solic    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM licencia_solic

/****** Object:  Table estructura_grupo    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM estructura_grupo

/****** Object:  Table estructura_tramo    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM estructura_tramo

/****** Object:  Table estructura_ubicacion    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM estructura_ubicacion

END
```
