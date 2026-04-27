---
id: db-brain-view-vw-indicador-verificaciones
type: db-view
title: dbo.vw_Indicador_Verificaciones
schema: dbo
name: vw_Indicador_Verificaciones
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.vw_Indicador_Verificaciones`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBodega` | `int` | ✓ |  |
| 2 | `IdPropietarioBodega` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `lic_plate` | `nvarchar(25)` | ✓ |  |
| 6 | `idpresentacion` | `int` | ✓ |  |
| 7 | `presentacion` | `nvarchar(50)` | ✓ |  |
| 8 | `factor` | `float` | ✓ |  |
| 9 | `cajas/bultos` | `float` |  |  |
| 10 | `unidades` | `float` |  |  |
| 11 | `fecha` | `date` | ✓ |  |
| 12 | `IdUnidadMedida` | `int` | ✓ |  |
| 13 | `unidad` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha_picking` | `datetime` | ✓ |  |
| 15 | `IdPedidoEnc` | `int` |  |  |
| 16 | `IdPickingEnc` | `int` |  |  |
| 17 | `IdTipoPedido` | `int` | ✓ |  |
| 18 | `pedido` | `nvarchar(250)` | ✓ |  |
| 19 | `IdOperadorBodega_Verifico` | `int` | ✓ |  |
| 20 | `operador_Verifico` | `nvarchar(201)` | ✓ |  |

## Consume

- `bodega`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `trans_pe_enc`
- `trans_pe_tipo`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[vw_Indicador_Verificaciones] as

SELECT T.IdProductoBodega,T.IdPropietarioBodega,T.IdBodega,T.bodega,T.lic_plate,
T.idpresentacion,T.presentacion,T.factor,
ISNULL(sum(T.[cajas/bultos]),0)  [cajas/bultos],
ISNULL(SUM(T.unidades),0) unidades,
T.fecha,
T.IdUnidadMedida,T.unidad,
T.fecha_picking,
T.IdPedidoEnc,T.IdPickingEnc,
T.IdTipoPedido,T.pedido,
T.IdOperadorBodega_Verifico,
T.operador_Verifico
FROM (
select p_ubic.IdProductoBodega,p_ubic.IdPropietarioBodega,p_ubic.IdBodega,bd.nombre bodega,p_ubic.lic_plate,
p_ubic.idpresentacion,pp.nombre presentacion,pp.factor,
sum(cantidad_recibida)  [cajas/bultos],
0 unidades,
cast(p_enc.fecha_picking as date) fecha,
p_ubic.IdUnidadMedida,um.Nombre unidad,
p_ubic.fecha_picking,
pe_enc.IdPedidoEnc,p_ubic.IdPickingEnc,
pe_enc.IdTipoPedido,doc.Descripcion pedido
,p_ubic.IdOperadorBodega_Verifico,
op.nombres +' '+ op.apellidos as operador_verifico
from trans_picking_ubic p_ubic inner join producto_bodega pb on p_ubic.IdBodega=pb.IdBodega and p_ubic.IdProductoBodega=pb.IdProductoBodega
							   inner join producto pr on pb.IdProducto= pr.IdProducto
							   left outer join producto_presentacion pp on pr.IdProducto=pp.IdProducto and p_ubic.IdPresentacion = pp.IdPresentacion
							   left outer join unidad_medida um on p_ubic.IdUnidadMedida=um.IdUnidadMedida
							   inner join trans_picking_enc p_enc on p_ubic.IdPickingEnc=p_enc.IdPickingEnc
							   inner join trans_pe_enc pe_enc on p_enc.IdPickingEnc = pe_enc.IdPickingEnc
							   inner join trans_pe_tipo doc on pe_enc.IdTipoPedido=doc.IdTipoPedido
							   inner join bodega bd on p_ubic.IdBodega=bd.IdBodega
							   inner join operador_bodega op_bodega  on p_ubic.IdOperadorBodega_Verifico=op_bodega.IdOperadorBodega
							   inner join operador op on op_bodega.IdOperador = op.IdOperador
where p_ubic.activo=1 and p_enc.activo=1 AND p_ubic.dañado_picking = 0 and p_ubic.dañado_verificacion = 0
and p_ubic.IdPresentacion<>0
group by  p_ubic.IdProductoBodega,p_ubic.IdPropietarioBodega,p_ubic.IdBodega,p_ubic.lic_plate,
p_ubic.idpresentacion,pp.nombre,
p_ubic.IdUnidadMedida,um.Nombre,p_ubic.fecha_picking,pe_enc.IdPedidoEnc,p_ubic.IdPickingEnc,
pe_enc.IdTipoPedido,doc.Descripcion,bd.nombre,pp.factor, p_ubic.IdOperadorBodega_Verifico,
op.nombres,op.apellidos, cast(p_enc.fecha_picking as date)
union
select p_ubic.IdProductoBodega,p_ubic.IdPropietarioBodega,p_ubic.IdBodega,bd.nombre bodega,p_ubic.lic_plate,
p_ubic.idpresentacion,pp.nombre presentacion,pp.factor,
0 [cajas/bultos],
sum(cantidad_recibida) unidades,
cast(p_enc.fecha_picking as date) fecha,
p_ubic.IdUnidadMedida,um.Nombre unidad,
p_ubic.fecha_picking,
pe_enc.IdPedidoEnc,p_ubic.IdPickingEnc,
pe_enc.IdTipoPedido,doc.Descripcion pedido
,p_ubic.IdOperadorBodega_Verifico,
op.nombres +' '+ op.apellidos as operador_Verifico
from trans_picking_ubic p_ubic inner join producto_bodega pb on p_ubic.IdBodega=pb.IdBodega and p_ubic.IdProductoBodega=pb.IdProductoBodega
							   inner join producto pr on pb.IdProducto= pr.IdProducto
							   left outer join producto_presentacion pp on pr.IdProducto=pp.IdProducto and p_ubic.IdPresentacion = pp.IdPresentacion
							   left outer join unidad_medida um on p_ubic.IdUnidadMedida=um.IdUnidadMedida
							   inner join trans_picking_enc p_enc on p_ubic.IdPickingEnc=p_enc.IdPickingEnc
							   inner join trans_pe_enc pe_enc on p_enc.IdPickingEnc = pe_enc.IdPickingEnc
							   inner join trans_pe_tipo doc on pe_enc.IdTipoPedido=doc.IdTipoPedido
							   inner join bodega bd on p_ubic.IdBodega=bd.IdBodega
							   inner join operador_bodega op_bodega  on p_ubic.IdOperadorBodega_Verifico=op_bodega.IdOperadorBodega
							   inner join operador op on op_bodega.IdOperador = op.IdOperador
where p_ubic.activo=1 and p_enc.activo=1 AND p_ubic.dañado_picking = 0 and p_ubic.dañado_verificacion = 0
and p_ubic.IdPresentacion=0
group by  p_ubic.IdProductoBodega,p_ubic.IdPropietarioBodega,p_ubic.IdBodega,p_ubic.lic_plate,
p_ubic.idpresentacion,pp.nombre,
p_ubic.IdUnidadMedida,um.Nombre,p_ubic.fecha_picking,pe_enc.IdPedidoEnc,p_ubic.IdPickingEnc,
pe_enc.IdTipoPedido,doc.Descripcion,bd.nombre,pp.factor,
p_ubic.IdOperadorBodega_Verifico,
op.nombres,op.apellidos, cast(p_enc.fecha_picking as date)) AS T
GROUP BY T.IdProductoBodega,T.IdPropietarioBodega,T.IdBodega,T.bodega,T.lic_plate,
T.idpresentacion,T.presentacion,T.factor,
T.fecha,
T.IdUnidadMedida,T.unidad,
T.fecha_picking,
T.IdPedidoEnc,T.IdPickingEnc,
T.IdTipoPedido,T.pedido,
T.IdOperadorBodega_Verifico,
T.operador_Verifico
```
