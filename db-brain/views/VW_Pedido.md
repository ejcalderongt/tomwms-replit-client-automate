---
id: db-brain-view-vw-pedido
type: db-view
title: dbo.VW_Pedido
schema: dbo
name: VW_Pedido
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pedido`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 27 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Pedido` | `int` |  |  |
| 2 | `referencia` | `nvarchar(25)` | ✓ |  |
| 3 | `IdPedidoDet` | `int` |  |  |
| 4 | `Código` | `nvarchar(50)` | ✓ |  |
| 5 | `Producto` | `nvarchar(100)` | ✓ |  |
| 6 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 7 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 8 | `Estado` | `nvarchar(50)` | ✓ |  |
| 9 | `Cantidad` | `float` | ✓ |  |
| 10 | `Fecha` | `datetime` | ✓ |  |
| 11 | `IdProducto` | `int` |  |  |
| 12 | `IdPickingEnc` | `int` | ✓ |  |
| 13 | `EstadoPedido` | `nvarchar(20)` | ✓ |  |
| 14 | `IdBodega` | `int` | ✓ |  |
| 15 | `Cantidad_Pickeada` | `float` | ✓ |  |
| 16 | `Cantidad_Verificada` | `float` | ✓ |  |
| 17 | `Cantidad_Despachada` | `float` | ✓ |  |
| 18 | `IdPropietarioBodega` | `int` | ✓ |  |
| 19 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 20 | `Licencia` | `nvarchar(25)` | ✓ |  |
| 21 | `lote` | `nvarchar(50)` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 24 | `CodigoCliente` | `nvarchar(150)` | ✓ |  |
| 25 | `NombreCliente` | `nvarchar(150)` | ✓ |  |
| 26 | `IdTipoManufactura` | `int` | ✓ |  |
| 27 | `EstadoPicking` | `nvarchar(20)` | ✓ |  |

## Consume

- `cliente`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_presentacion`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Pedido]
AS
SELECT enc.IdPedidoEnc AS Pedido, enc.referencia, det.IdPedidoDet, p.codigo AS Código, p.nombre AS Producto, pp.nombre AS Presentacion, u.Nombre AS UnidadMedida, e.nombre AS Estado, det.Cantidad, enc.Fecha_Pedido AS Fecha, 
                  p.IdProducto, enc.IdPickingEnc, enc.estado AS EstadoPedido, enc.IdBodega, SUM(dbo.trans_picking_ubic.cantidad_recibida) AS Cantidad_Pickeada, SUM(dbo.trans_picking_ubic.cantidad_verificada) AS Cantidad_Verificada, 
                  SUM(dbo.trans_picking_ubic.cantidad_despachada) AS Cantidad_Despachada, enc.IdPropietarioBodega, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.trans_picking_ubic.lic_plate AS Licencia, dbo.trans_picking_ubic.lote, 
                  dbo.trans_picking_ubic.fecha_vence, enc.user_agr, dbo.cliente.codigo AS CodigoCliente, dbo.cliente.nombre_comercial AS NombreCliente, 
				  enc.IdTipoManufactura, trans_picking_enc.estado as EstadoPicking
FROM     dbo.cliente INNER JOIN
                  dbo.trans_pe_enc AS enc INNER JOIN
                  dbo.trans_pe_det AS det ON enc.IdPedidoEnc = det.IdPedidoEnc INNER JOIN
                  dbo.producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                  dbo.producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                  dbo.producto_estado AS e ON det.IdEstado = e.IdEstado INNER JOIN
                  dbo.unidad_medida AS u ON det.IdUnidadMedidaBasica = u.IdUnidadMedida ON dbo.cliente.IdCliente = enc.IdCliente LEFT OUTER JOIN
                  dbo.producto_clasificacion ON p.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                  dbo.trans_picking_ubic INNER JOIN
                  dbo.trans_picking_det ON dbo.trans_picking_ubic.IdPickingDet = dbo.trans_picking_det.IdPickingDet AND dbo.trans_picking_ubic.IdPickingEnc = dbo.trans_picking_det.IdPickingEnc ON 
                  det.IdPedidoDet = dbo.trans_picking_det.IdPedidoDet LEFT OUTER JOIN
                  dbo.trans_picking_enc ON dbo.trans_picking_det.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc AND enc.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON det.IdPresentacion = pp.IdPresentacion
GROUP BY enc.IdPedidoEnc, enc.referencia, det.IdPedidoDet, p.codigo, p.nombre, pp.nombre, u.Nombre, e.nombre, det.Cantidad, enc.Fecha_Pedido, 
         p.IdProducto, enc.IdPickingEnc,enc.estado, enc.IdBodega, enc.IdPropietarioBodega, dbo.producto_clasificacion.nombre, 
		 dbo.trans_picking_ubic.lic_plate, dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence, enc.user_agr, 
		 dbo.cliente.nombre_comercial, dbo.cliente.codigo, enc.IdTipoManufactura, trans_picking_enc.estado
```
