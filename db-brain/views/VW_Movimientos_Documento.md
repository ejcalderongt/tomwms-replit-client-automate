---
id: db-brain-view-vw-movimientos-documento
type: db-view
title: dbo.VW_Movimientos_Documento
schema: dbo
name: VW_Movimientos_Documento
kind: view
modify_date: 2022-07-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_Documento`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-07-21 |
| Columnas | 57 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMovimiento` | `int` |  |  |
| 2 | `IdTransaccion` | `int` |  |  |
| 3 | `IdBodegaOrigen` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdUbicacionOrigen` | `int` | ✓ |  |
| 7 | `IdUbicacionDestino` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `IdEstadoOrigen` | `int` | ✓ |  |
| 10 | `IdEstadoDestino` | `int` | ✓ |  |
| 11 | `IdUnidadMedida` | `int` | ✓ |  |
| 12 | `IdBodegaDestino` | `int` | ✓ |  |
| 13 | `IdRecepcion` | `int` | ✓ |  |
| 14 | `cantidad` | `float` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `fecha` | `datetime` | ✓ |  |
| 17 | `LicPlate` | `nvarchar(50)` | ✓ |  |
| 18 | `IdProducto` | `int` | ✓ |  |
| 19 | `codigo` | `nvarchar(50)` | ✓ |  |
| 20 | `CodigoBarra` | `nvarchar(35)` | ✓ |  |
| 21 | `Producto` | `nvarchar(100)` | ✓ |  |
| 22 | `Propietario` | `nvarchar(100)` |  |  |
| 23 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 24 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 25 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |
| 26 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 27 | `BodegaOrigen` | `nvarchar(50)` | ✓ |  |
| 28 | `BodegaDestino` | `nvarchar(50)` | ✓ |  |
| 29 | `UbicOrigen` | `nvarchar(200)` | ✓ |  |
| 30 | `UbicDestino` | `nvarchar(200)` | ✓ |  |
| 31 | `Contabilizar` | `bit` | ✓ |  |
| 32 | `fecha_vence` | `datetime` | ✓ |  |
| 33 | `IdArea` | `int` |  |  |
| 34 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 35 | `factor` | `float` | ✓ |  |
| 36 | `Clasificacion` | `nvarchar(50)` |  |  |
| 37 | `familia` | `nvarchar(50)` | ✓ |  |
| 38 | `IdBodega` | `int` | ✓ |  |
| 39 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 40 | `No_Linea` | `int` | ✓ |  |
| 41 | `valor_iva` | `float` | ✓ |  |
| 42 | `valor_dai` | `float` | ✓ |  |
| 43 | `oc` | `int` |  |  |
| 44 | `numero_referencia` | `nvarchar(100)` | ✓ |  |
| 45 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 46 | `RegimenIngreso` | `nvarchar(500)` | ✓ |  |
| 47 | `IdTicket` | `int` | ✓ |  |
| 48 | `Tipo_Operacion` | `nvarchar(50)` | ✓ |  |
| 49 | `fecha_ingreso` | `datetime` | ✓ |  |
| 50 | `IdTipoTarea` | `int` |  |  |
| 51 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 52 | `CantidadDespachada` | `float` | ✓ |  |
| 53 | `Fecha_Salida` | `datetime` | ✓ |  |
| 54 | `poliza_salida` | `nvarchar(50)` | ✓ |  |
| 55 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 56 | `idregimensalida` | `int` | ✓ |  |
| 57 | `regimen_salida` | `nvarchar(500)` | ✓ |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `regimen_fiscal`
- `sis_tipo_tarea`
- `tms_ticket`
- `trans_despacho_det`
- `trans_movimientos`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_pe_pol`
- `trans_re_det`
- `trans_re_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
--GT21072022: Vista para reporte movimientos por doc cealsa.

create view VW_Movimientos_Documento as

select  mov.IdMovimiento,mov.IdTransaccion,mov.IdBodegaOrigen
,mov.IdPropietarioBodega,mov.IdProductoBodega
,mov.IdUbicacionOrigen,mov.IdUbicacionDestino,mov.IdPresentacion,mov.IdEstadoOrigen,mov.IdEstadoDestino,mov.IdUnidadMedida
,mov.IdBodegaDestino,mov.IdRecepcion,mov.cantidad,mov.lote,mov.fecha,mov.barra_pallet as LicPlate
,pb.IdProducto,p.codigo,p.codigo_barra AS CodigoBarra,p.nombre as Producto
,prop.nombre_comercial Propietario
,p_pres.nombre as Presentación, pe1.nombre as EstadoOrigen, pe2.nombre as EstadoDestino
,um.Nombre as UMBas, bd1.nombre as BodegaOrigen, bd2.nombre as BodegaDestino
,dbo.Nombre_Completo_Ubicacion(bu1.IdUbicacion, bu1.IdBodega) AS UbicOrigen, dbo.Nombre_Completo_Ubicacion(bu2.IdUbicacion, bu2.IdBodega) AS UbicDestino
	,stt.Contabilizar,mov.fecha_vence
,bu1.IdArea,dbo.Nombre_Area(bu1.IdArea, mov.IdBodegaOrigen) AS NombreArea
,p_pres.factor, isnull(p_clas.nombre,'') as Clasificacion, p_fam.nombre as familia, re_enc.IdBodega
,re_det.lic_plate, oc_det.No_Linea,oc_det.valor_iva,oc_det.valor_dai
,oc_enc.IdOrdenCompraEnc oc
,oc_enc.Referencia as numero_referencia, oc_pol.codigo_poliza as Poliza,reg.descripcion as RegimenIngreso
,tms.IdTicket,tms.Tipo_Operacion,
case oc_enc.no_ticket_tms
when 0 then oc_enc.Fecha_Creacion
else tms.fecha_asignado end fecha_ingreso
,stt.IdTipoTarea,stt.Nombre AS TipoTarea
,desp.CantidadDespachada
,desp.fec_agr AS Fecha_Salida
,pe_pol.NoPoliza as poliza_salida,pe_pol.numero_orden numero_orden,pe_pol.IdRegimen idregimensalida, reg2.descripcion regimen_salida
from trans_movimientos mov 
INNER JOIN producto_bodega pb on mov.IdProductoBodega=pb.IdProductoBodega and mov.IdBodegaOrigen = pb.IdBodega
INNER JOIN producto p on pb.IdProducto = p.IdProducto
INNER JOIN propietario_bodega prb on mov.IdPropietarioBodega=prb.IdPropietarioBodega
INNER JOIN propietarios prop on prb.IdPropietario = prop.IdPropietario
LEFT OUTER JOIN producto_presentacion p_pres on mov.IdPresentacion = p_pres.IdPresentacion
INNER JOIN dbo.producto_estado AS pe1 ON mov.IdEstadoOrigen = pe1.IdEstado 
INNER JOIN dbo.producto_estado as pe2 on mov.IdEstadoDestino = pe2.IdEstado
INNER JOIN dbo.unidad_medida AS um ON mov.IdUnidadMedida = um.IdUnidadMedida
INNER JOIN dbo.bodega as bd1 on  mov.IdBodegaOrigen = bd1.IdBodega
INNER JOIN dbo.bodega as bd2 on  mov.IdBodegaDestino = bd2.IdBodega
INNER JOIN dbo.bodega_ubicacion bu1 on mov.IdUbicacionOrigen = bu1.IdUbicacion and mov.IdBodegaOrigen = bu1.IdBodega
INNER JOIN dbo.bodega_ubicacion bu2 on mov.IdUbicacionDestino = bu2.IdUbicacion and mov.IdBodegaDestino = bu2.IdBodega
INNER JOIN dbo.sis_tipo_tarea AS stt ON mov.IdTipoTarea = stt.IdTipoTarea
LEFT OUTER JOIN dbo.producto_clasificacion p_clas on p.IdClasificacion = p_clas.IdClasificacion and p_clas.IdPropietario = prop.IdPropietario
LEFT OUTER JOIN dbo.producto_familia p_fam ON p.IdFamilia = p_fam.IdFamilia and p.IdPropietario = p_fam.IdPropietario
INNER JOIN dbo.trans_re_enc  as re_enc on mov.IdRecepcion = re_enc.IdRecepcionEnc
INNER JOIN dbo.trans_re_det as re_det on re_enc.IdRecepcionEnc = re_det.IdRecepcionEnc 
			and mov.IdProductoBodega = re_det.IdProductoBodega 
			and mov.barra_pallet collate Modern_Spanish_CI_AS = re_det.lic_plate
INNER JOIN  dbo.trans_oc_det oc_det on re_det.IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc and re_det.IdOrdenCompraDet=oc_det.IdOrdenCompraDet
INNER JOIN dbo.trans_oc_enc oc_enc on re_det.IdOrdenCompraEnc = oc_enc.IdOrdenCompraEnc
LEFT OUTER JOIN dbo.trans_oc_pol oc_pol on oc_enc.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc
LEFT OUTER JOIN dbo.regimen_fiscal reg on oc_pol.IdRegimen = reg.IdRegimen
LEFT OUTER JOIN dbo.tms_ticket tms on oc_enc.no_ticket_tms = tms.IdTicket
LEFT OUTER JOIN dbo.trans_despacho_det desp on mov.IdTransaccion = desp.IdDespachoEnc and re_enc.IdRecepcionEnc = mov.IdRecepcion
LEFT OUTER JOIN dbo.trans_pe_pol pe_pol on desp.IdPedidoEnc= pe_pol.IdOrdenPedidoEnc
LEFT OUTER JOIN dbo.regimen_fiscal reg2 on pe_pol.IdRegimen = reg2.IdRegimen
```
