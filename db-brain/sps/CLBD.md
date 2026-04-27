---
id: db-brain-sp-clbd
type: db-sp
title: dbo.CLBD
schema: dbo
name: CLBD
kind: sp
modify_date: 2016-08-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.CLBD`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2016-08-04 |

## Consume

- `Appointments`
- `arancel`
- `bodega`
- `bodega_area`
- `bodega_monitor_parametro`
- `bodega_muelles`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `camara`
- `cliente`
- `cliente_bodega`
- `cliente_direccion`
- `cliente_tiempos`
- `cliente_tipo`
- `contenedor`
- `empresa`
- `empresa_transporte`
- `empresa_transporte_bodega`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `horario_laboral_det`
- `horario_laboral_enc`
- `impresora`
- `indice_rotacion`
- `jornada_laboral`
- `mensaje_regla`
- `menu_rol`
- `menu_rol_op`
- `menu_sistema`
- `menu_sistema_op`
- `montacarga`
- `montacarga_bodega`
- `montacarga_servicio_enc`
- `montacarga_tipoFalla`
- `motivo_anulacion`
- `motivo_anulacion_bodega`
- `motivo_devolucion`
- `motivo_devolucion_bodega`
- `motivo_ubicacion`
- `operador`
- `operador_bodega`
- `p_parametro`
- `pais_departamento`
- `pais_municipio`
- `pais_region`
- `paises`
- `perfil_serializado`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_codis_barra`
- `producto_estado`
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
- `propietario_reglas_det`
- `propietario_reglas_enc`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `reglas_recepcion`
- `Resources`
- `road_p_vendedor`
- `road_ruta`
- `rol`
- `rol_bodega`
- `rol_menu`
- `rol_operador`
- `simbologias_codigos_barra`
- `sis_estado_tarea_hh`
- `sis_obs_log`
- `sis_prioridad_tarea_hh`
- `sis_tipo_accion`
- `sis_tipo_tarea`
- `stock`
- `stock_parametro`
- `stock_rec`
- `stock_res`
- `stock_res_se`
- `stock_se`
- `stock_se_rec`
- `tablas_sync`
- `tarea_hh`
- `tarimas`
- `tipo_actualizacion_costo`
- `tipo_contenedor`
- `tipo_rotacion`
- `tipo_tarima`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_inventario_det`

_... +39 más_

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE CLBD
AS
BEGIN
/****** Object:  Table usuario_bodega    Script Date: 04/08/2016 11:59:07 a.m. ******/
DELETE FROM usuario_bodega

/****** Object:  Table usuario    Script Date: 04/08/2016 11:59:07 a.m. ******/
DELETE FROM usuario

/****** Object:  Table unidad_medida_conversiones    Script Date: 04/08/2016 11:59:08 a.m. ******/
DELETE FROM unidad_medida_conversiones

/****** Object:  Table unidad_medida    Script Date: 04/08/2016 11:59:08 a.m. ******/
DELETE FROM unidad_medida

/****** Object:  Table turno    Script Date: 04/08/2016 11:59:08 a.m. ******/
DELETE FROM turno

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

/****** Object:  Table trans_re_tr    Script Date: 04/08/2016 11:59:11 a.m. ******/
DELETE FROM trans_re_tr

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

/****** Object:  Table trans_pe_tipo    Script Date: 04/08/2016 11:59:16 a.m. ******/
DELETE FROM trans_pe_tipo

/****** Object:  Table trans_pe_enc    Script Date: 04/08/2016 11:59:16 a.m. ******/
DELETE FROM trans_pe_enc

/****** Object:  Table trans_pe_det    Script Date: 04/08/2016 11:59:16 a.m. ******/
DELETE FROM trans_pe_det

/****** Object:  Table trans_oc_ti    Script Date: 04/08/2016 11:59:17 a.m. ******/
DELETE FROM trans_oc_ti

/****** Object:  Table trans_oc_pol    Script Date: 04/08/2016 11:59:17 a.m. ******/
DELETE FROM trans_oc_pol

/****** Object:  Table trans_oc_imagen    Script Date: 04/08/2016 11:59:17 a.m. ******/
DELETE FROM trans_oc_imagen

/****** Object:  Table trans_oc_estado    Script Date: 04/08/2016 11:59:18 a.m. ******/
DELETE FROM trans_oc_estado

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

/****** Object:  Table tipo_tarima    Script Date: 04/08/2016 11:59:20 a.m. ******/
DELETE FROM tipo_tarima

/****** Object:  Table tipo_rotacion    Script Date: 04/08/2016 11:59:21 a.m. ******/
DELETE FROM tipo_rotacion

/****** Object:  Table tipo_contenedor    Script Date: 04/08/2016 11:59:21 a.m. ******/
DELETE FROM tipo_contenedor

/****** Object:  Table tipo_actualizacion_costo    Script Date: 04/08/2016 11:59:21 a.m. ******/
DELETE FROM tipo_actualizacion_costo

/****** Object:  Table tarimas    Script Date: 04/08/2016 11:59:22 a.m. ******/
DELETE FROM tarimas

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

/****** Object:  Table sis_tipo_tarea    Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM sis_tipo_tarea

/****** Object:  Table sis_tipo_accion    Script Date: 04/08/2016 11:59:25 a.m. ******/
DELETE FROM sis_tipo_accion

/****** Object:  Table sis_prioridad_tarea_hh    Script Date: 04/08/2016 11:59:26 a.m. ******/
DELETE FROM sis_prioridad_tarea_hh

/****** Object:  Table sis_obs_log    Script Date: 04/08/2016 11:59:26 a.m. ******/
DELETE FROM sis_obs_log

/****** Object:  Table sis_estado_tarea_hh    Script Date: 04/08/2016 11:59:26 a.m. ******/
DELETE FROM sis_estado_tarea_hh

/****** Object:  Table simbologias_codi_barra    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM simbologias_codigos_barra

/****** Object:  Table rol_operador    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM rol_operador

/****** Object:  Table rol_menu    Script Date: 04/08/2016 11:59:27 a.m. ******/
DELETE FROM rol_menu

/****** Object:  Table rol_bodega    Script Date: 04/08/2016 11:59:28 a.m. ******/
DELETE FROM rol_bodega

/****** Object:  Table rol    Script Date: 04/08/2016 11:59:28 a.m. ******/
DELETE FROM rol

/****** Object:  Table road_ruta    Script Date: 04/08/2016 11:59:28 a.m. ******/
DELETE FROM road_ruta

/****** Object:  Table road_p_vendedor    Script Date: 04/08/2016 11:59:29 a.m. ******/
DELETE FROM road_p_vendedor

/****** Object:  Table Resources    Script Date: 04/08/2016 11:59:29 a.m. ******/
DELETE FROM Resources

/****** Object:  Table reglas_recepcion    Script Date: 04/08/2016 11:59:29 a.m. ******/
DELETE FROM reglas_recepcion

/****** Object:  Table proveedor_bodega    Script Date: 04/08/2016 11:59:30 a.m. ******/
DELETE FROM proveedor_bodega

/****** Object:  Table proveedor    Script Date: 04/08/2016 11:59:30 a.m. ******/
DELETE FROM proveedor

/****** Object:  Table propietarios    Script Date: 04/08/2016 11:59:31 a.m. ******/
DELETE FROM propietarios

/****** Object:  Table propietario_reglas_enc    Script Date: 04/08/2016 11:59:31 a.m. ******/
DELETE FROM propietario_reglas_enc

/****** Object:  Table propietario_reglas_det    Script Date: 04/08/2016 11:59:31 a.m. ******/
DELETE FROM propietario_reglas_det

/****** Object:  Table propietario_destinatario    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM propietario_destinatario

/****** Object:  Table propietario_bodega    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM propietario_bodega

/****** Object:  Table producto_tipo    Script Date: 04/08/2016 11:59:32 a.m. ******/
DELETE FROM producto_tipo

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

/****** Object:  Table producto_estado    Script Date: 04/08/2016 11:59:35 a.m. ******/
DELETE FROM producto_estado

/****** Object:  Table producto_codis_barra    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_codis_barra

/****** Object:  Table producto_clasificacion    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_clasificacion

/****** Object:  Table producto_bodega    Script Date: 04/08/2016 11:59:36 a.m. ******/
DELETE FROM producto_bodega

/****** Object:  Table producto    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM producto

/****** Object:  Table perfil_serializado    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM perfil_serializado

/****** Object:  Table paises    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM paises

/****** Object:  Table pais_region    Script Date: 04/08/2016 11:59:37 a.m. ******/
DELETE FROM pais_region

/****** Object:  Table pais_municipio    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM pais_municipio

/****** Object:  Table pais_departamento    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM pais_departamento

/****** Object:  Table p_parametro    Script Date: 04/08/2016 11:59:38 a.m. ******/
DELETE FROM p_parametro

/****** Object:  Table operador_bodega    Script Date: 04/08/2016 11:59:39 a.m. ******/
DELETE FROM operador_bodega

/****** Object:  Table operador    Script Date: 04/08/2016 11:59:39 a.m. ******/
DELETE FROM operador

/****** Object:  Table motivo_ubicacion    Script Date: 04/08/2016 11:59:39 a.m. ******/
DELETE FROM motivo_ubicacion

/****** Object:  Table motivo_devolucion_bodega    Script Date: 04/08/2016 11:59:40 a.m. ******/
DELETE FROM motivo_devolucion_bodega

/****** Object:  Table motivo_devolucion    Script Date: 04/08/2016 11:59:40 a.m. ******/
DELETE FROM motivo_devolucion

/****** Object:  Table motivo_anulacion_bodega    Script Date: 04/08/2016 11:59:40 a.m. ******/
DELETE FROM motivo_anulacion_bodega

/****** Object:  Table motivo_anulacion    Script Date: 04/08/2016 11:59:41 a.m. ******/
DELETE FROM motivo_anulacion

/****** Object:  Table montacarga_tipoFalla    Script Date: 04/08/2016 11:59:41 a.m. ******/
DELETE FROM montacarga_tipoFalla

/****** Object:  Table montacarga_servicio_enc    Script Date: 04/08/2016 11:59:41 a.m. ******/
DELETE FROM montacarga_servicio_enc

/****** Object:  Table montacarga_bodega    Script Date: 04/08/2016 11:59:42 a.m. ******/
DELETE FROM montacarga_bodega

/****** Object:  Table montacarga    Script Date: 04/08/2016 11:59:42 a.m. ******/
DELETE FROM montacarga

/****** Object:  Table menu_sistema_op    Script Date: 04/08/2016 11:59:42 a.m. ******/
DELETE FROM menu_sistema_op

/****** Object:  Table menu_sistema    Script Date: 04/08/2016 11:59:43 a.m. ******/
DELETE FROM menu_sistema

/****** Object:  Table menu_rol_op    Script Date: 04/08/2016 11:59:43 a.m. ******/
DELETE FROM menu_rol_op

/****** Object:  Table menu_rol    Script Date: 04/08/2016 11:59:43 a.m. ******/
DELETE FROM menu_rol

/****** Object:  Table mensaje_regla    Script Date: 04/08/2016 11:59:44 a.m. ******/
DELETE FROM mensaje_regla

/****** Object:  Table jornada_laboral    Script Date: 04/08/2016 11:59:44 a.m. ******/
DELETE FROM jornada_laboral

/****** Object:  Table indice_rotacion    Script Date: 04/08/2016 11:59:45 a.m. ******/
DELETE FROM indice_rotacion

/****** Object:  Table impresora    Script Date: 04/08/2016 11:59:46 a.m. ******/
DELETE FROM impresora

/****** Object:  Table horario_laboral_enc    Script Date: 04/08/2016 11:59:46 a.m. ******/
DELETE FROM horario_laboral_enc

/****** Object:  Table horario_laboral_det    Script Date: 04/08/2016 11:59:47 a.m. ******/
DELETE FROM horario_laboral_det

/****** Object:  Table empresa_transporte_vehiculos    Script Date: 04/08/2016 11:59:47 a.m. ******/
DELETE FROM empresa_transporte_vehiculos

/****** Object:  Table empresa_transporte_pilotos    Script Date: 04/08/2016 11:59:47 a.m. ******/
DELETE FROM empresa_transporte_pilotos

/****** Object:  Table empresa_transporte_bodega    Script Date: 04/08/2016 11:59:48 a.m. ******/
DELETE FROM empresa_transporte_bodega

/****** Object:  Table empresa_transporte    Script Date: 04/08/2016 11:59:48 a.m. ******/
DELETE FROM empresa_transporte

/****** Object:  Table empresa    Script Date: 04/08/2016 11:59:48 a.m. ******/
DELETE FROM empresa

/****** Object:  Table contenedor    Script Date: 04/08/2016 11:59:49 a.m. ******/
DELETE FROM contenedor

/****** Object:  Table cliente_tipo    Script Date: 04/08/2016 11:59:49 a.m. ******/
DELETE FROM cliente_tipo

/****** Object:  Table cliente_tiempos    Script Date: 04/08/2016 11:59:49 a.m. ******/
DELETE FROM cliente_tiempos

/****** Object:  Table cliente_direccion    Script Date: 04/08/2016 11:59:50 a.m. ******/
DELETE FROM cliente_direccion

/****** Object:  Table cliente_bodega    Script Date: 04/08/2016 11:59:51 a.m. ******/
DELETE FROM cliente_bodega

/****** Object:  Table cliente    Script Date: 04/08/2016 11:59:51 a.m. ******/
DELETE FROM cliente

/****** Object:  Table camara    Script Date: 04/08/2016 11:59:51 a.m. ******/
DELETE FROM camara

/****** Object:  Table bodega_ubicacion    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_ubicacion

/****** Object:  Table bodega_tramo    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_tramo

/****** Object:  Table bodega_sector    Script Date: 04/08/2016 11:59:52 a.m. ******/
DELETE FROM bodega_sector

/****** Object:  Table bodega_muelles    Script Date: 04/08/2016 11:59:53 a.m. ******/
DELETE FROM bodega_muelles

/****** Object:  Table bodega_monitor_parametro    Script Date: 04/08/2016 11:59:54 a.m. ******/
DELETE FROM bodega_monitor_parametro

/****** Object:  Table bodega_area    Script Date: 04/08/2016 11:59:54 a.m. ******/
DELETE FROM bodega_area

/****** Object:  Table bodega    Script Date: 04/08/2016 11:59:55 a.m. ******/
DELETE FROM bodega

/****** Object:  Table arancel    Script Date: 04/08/2016 11:59:55 a.m. ******/
DELETE FROM arancel

/****** Object:  Table Appointments    Script Date: 04/08/2016 11:59:56 a.m. ******/
DELETE FROM Appointments



END
```
