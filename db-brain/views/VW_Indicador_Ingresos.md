---
id: db-brain-view-vw-indicador-ingresos
type: db-view
title: dbo.VW_Indicador_Ingresos
schema: dbo
name: VW_Indicador_Ingresos
kind: view
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Indicador_Ingresos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-02-01 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `devolucion` | `int` | ✓ |  |
| 4 | `documento` | `nvarchar(50)` | ✓ |  |
| 5 | `fecha` | `date` | ✓ |  |
| 6 | `IdBodega` | `int` |  |  |
| 7 | `bodega` | `nvarchar(50)` | ✓ |  |
| 8 | `codigo` | `nvarchar(50)` | ✓ |  |
| 9 | `producto` | `nvarchar(100)` | ✓ |  |
| 10 | `unidades` | `float` | ✓ |  |
| 11 | `cajas/bultos` | `float` | ✓ |  |
| 12 | `propietario` | `nvarchar(100)` |  |  |
| 13 | `estado` | `nvarchar(50)` | ✓ |  |
| 14 | `presentacion` | `varchar(1)` |  |  |
| 15 | `factor` | `int` |  |  |

## Consume

- `bodega`
- `producto`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_oc_enc`
- `trans_oc_ti`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Indicador_Ingresos] as
SELECT  T.IdOrdenCompraEnc,
T.IdRecepcionEnc,
T.devolucion,
T.documento,
T.fecha,
T.IdBodega,
T.bodega,
T.codigo,
T.producto,
SUM(T.unidades) unidades,
SUM(T.[cajas/bultos]) [cajas/bultos],
T.propietario,
T.estado,
'' presentacion,
0 factor
FROM(select  oc.IdOrdenCompraEnc,
re_enc.IdRecepcionEnc,
cast(doc.es_devolucion as integer) devolucion,
doc.nombre documento,
cast(Fecha_Creacion as date) fecha,
bd.IdBodega,
bd.nombre bodega,
prod.codigo codigo,
prod.nombre producto,
isnull(presentacion.nombre,'') presentacion,ISNULL(presentacion.factor,0) factor,
round(sum(cantidad_recibida),4) as unidades,
0 [cajas/bultos],
pr.nombre_comercial propietario,
estado.nombre estado
from trans_oc_enc oc inner join trans_oc_ti doc on oc.IdTipoIngresoOC = doc.IdTipoIngresoOC
					 inner join bodega bd on oc.IdBodega=bd.IdBodega
					 inner  join trans_re_oc re_oc on  oc.IdOrdenCompraEnc=re_oc.IdOrdenCompraEnc
					 inner join trans_re_enc re_enc on re_oc.IdRecepcionEnc=re_enc.IdRecepcionEnc
					 inner join trans_re_det re_det on re_enc.IdRecepcionEnc=re_det.IdRecepcionEnc
					 inner join producto prod on re_det.codigo_producto = prod.codigo
					 inner join  propietario_bodega pb on oc.IdPropietarioBodega = pb.IdPropietarioBodega
					 inner join propietarios pr on pb.IdPropietario= pr.IdPropietario
					 left outer join producto_presentacion presentacion
                                     on re_det.IdPresentacion = presentacion.IdPresentacion
					 inner join producto_estado estado on re_det.IdProductoEstado=estado.IdEstado
													   and estado.IdPropietario=pr.IdPropietario
where oc.Activo=1 and re_enc.activo=1 AND re_det.idpresentacion is null 
and oc.IdEstadoOC<>5 and re_enc.estado<>'ANULADO'
group by doc.es_devolucion,oc.IdOrdenCompraEnc,Fecha_Creacion,bd.IdBodega,bd.nombre,
         re_enc.IdRecepcionEnc,doc.Nombre,prod.codigo,pr.nombre_comercial,presentacion.factor,
		 presentacion.nombre,prod.nombre,estado.nombre
union
select oc.IdOrdenCompraEnc,
re_enc.IdRecepcionEnc,
cast(doc.es_devolucion as integer) devolucion,
doc.nombre documento,
cast(Fecha_Creacion as date) fecha,
bd.IdBodega,
bd.nombre bodega,
prod.codigo codigo,
prod.nombre producto,
isnull(presentacion.nombre,'') presentacion,ISNULL(presentacion.factor,0) factor,
0 as unidades,
round(sum(re_det.cantidad_recibida),4) [cajas/bultos],
pr.nombre_comercial propietario,
estado.nombre estado
from trans_oc_enc oc inner join trans_oc_ti doc on oc.IdTipoIngresoOC = doc.IdTipoIngresoOC
					 inner join bodega bd on oc.IdBodega=bd.IdBodega
					 inner  join trans_re_oc re_oc on  oc.IdOrdenCompraEnc=re_oc.IdOrdenCompraEnc
					 inner join trans_re_enc re_enc on re_oc.IdRecepcionEnc=re_enc.IdRecepcionEnc
					 inner join trans_re_det re_det on re_enc.IdRecepcionEnc=re_det.IdRecepcionEnc
					 inner join producto prod on re_det.codigo_producto = prod.codigo
					 inner join  propietario_bodega pb on oc.IdPropietarioBodega = pb.IdPropietarioBodega
					 inner join propietarios pr on pb.IdPropietario= pr.IdPropietario
					 left outer join producto_presentacion presentacion
                                     on re_det.IdPresentacion = presentacion.IdPresentacion
					 inner join producto_estado estado on re_det.IdProductoEstado=estado.IdEstado
													   and estado.IdPropietario=pr.IdPropietario
where oc.Activo=1 and re_enc.activo=1 AND re_det.idpresentacion is not null 
and oc.IdEstadoOC<>5 and re_enc.estado<>'ANULADO'
group by doc.es_devolucion,oc.IdOrdenCompraEnc,Fecha_Creacion,bd.IdBodega,bd.nombre,
         re_enc.IdRecepcionEnc,doc.Nombre,prod.codigo,pr.nombre_comercial,
		 presentacion.factor,presentacion.nombre,prod.nombre,estado.nombre) AS T
GROUP BY  T.IdOrdenCompraEnc,
T.IdRecepcionEnc,
T.devolucion,
T.documento,
T.fecha,
T.IdBodega,
T.bodega,
T.codigo,
T.producto,
T.propietario,
T.estado
```
