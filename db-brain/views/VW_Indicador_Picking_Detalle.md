---
id: db-brain-view-vw-indicador-picking-detalle
type: db-view
title: dbo.VW_Indicador_Picking_Detalle
schema: dbo
name: VW_Indicador_Picking_Detalle
kind: view
modify_date: 2023-06-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Indicador_Picking_Detalle`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-06-18 |
| Columnas | 22 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `estado` | `nvarchar(20)` | ✓ |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `pedido` | `int` | ✓ |  |
| 4 | `pedido_detalle` | `int` |  |  |
| 5 | `IdBodega` | `int` |  |  |
| 6 | `idubicacion` | `int` |  |  |
| 7 | `codigo` | `nvarchar(25)` | ✓ |  |
| 8 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 9 | `IdClasificacion` | `int` | ✓ |  |
| 10 | `IdFamilia` | `int` | ✓ |  |
| 11 | `IdMarca` | `int` | ✓ |  |
| 12 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 13 | `UmBas` | `nvarchar(50)` | ✓ |  |
| 14 | `factor` | `float` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `fecha_vence` | `datetime` | ✓ |  |
| 17 | `cantidad` | `float` | ✓ |  |
| 18 | `presentacion` | `nvarchar(50)` |  |  |
| 19 | `cantidad_presentacion` | `float` | ✓ |  |
| 20 | `clasificacion` | `nvarchar(50)` |  |  |
| 21 | `familia` | `nvarchar(50)` |  |  |
| 22 | `marca` | `nvarchar(50)` |  |  |

## Consume

- `producto`
- `producto_clasificacion`
- `producto_familia`
- `producto_marca`
- `producto_presentacion`
- `stock_res`
- `trans_picking_det`
- `trans_picking_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create view [dbo].[VW_Indicador_Picking_Detalle] as
select picking_enc.estado,picking_enc.IdPickingEnc,picking_det.IdPedidoEnc pedido,picking_det.IdPedidoDet pedido_detalle,picking_enc.IdBodega,st_res.idubicacion,picking_det.codigo,
pr.codigo_barra,pr.IdClasificacion,pr.IdFamilia,pr.IdMarca,pr.IdUnidadMedidaBasica,
--pp.factor,st_res.IdUnidadMedida,
um.Nombre UmBas,um.factor,st_res.lote,st_res.fecha_vence, 
picking_det.cantidad,
isnull(pp.nombre,'') presentacion,
case when um.factor >1 then (picking_det.cantidad/um.factor) 
else 0
end as cantidad_presentacion,
isnull(pc.nombre,'') clasificacion,
isnull(pf.nombre,'') familia,
isnull(pm.nombre,'') marca 
from trans_picking_enc picking_enc inner join
                                                          trans_picking_det picking_det on picking_enc.IdPickingEnc =picking_det.IdPickingEnc
                                                          inner join stock_res st_res on picking_det.IdPickingEnc= st_res.IdPicking
                                                                                           and picking_det.IdPedidoDet = st_res.IdPedidoDet 
															inner join producto pr on picking_det.codigo=pr.codigo
															left outer join producto_presentacion pp on pr.IdProducto = pp.IdProducto and pr.codigo=pp.codigo
															left outer join producto_clasificacion pc on pr.IdClasificacion=pc.IdClasificacion
															left outer join producto_familia pf on pr.IdFamilia=pf.IdFamilia
															left outer join producto_marca pm on pr.IdMarca = pm.IdMarca
															left outer join unidad_medida um on st_res.IdUnidadMedida = um.IdUnidadMedida
where picking_enc.estado in ('Nuevo', 'Pendiente')
```
