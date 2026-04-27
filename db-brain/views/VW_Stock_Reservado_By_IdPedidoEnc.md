---
id: db-brain-view-vw-stock-reservado-by-idpedidoenc
type: db-view
title: dbo.VW_Stock_Reservado_By_IdPedidoEnc
schema: dbo
name: VW_Stock_Reservado_By_IdPedidoEnc
kind: view
modify_date: 2022-07-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Reservado_By_IdPedidoEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-07-13 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedido` | `bigint` | ✓ |  |
| 2 | `IdPedidoDet` | `int` |  |  |
| 3 | `IdStockRes` | `int` |  |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Producto` | `nvarchar(100)` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `lote` | `nvarchar(50)` | ✓ |  |
| 8 | `fecha_vence` | `datetime` | ✓ |  |
| 9 | `Cantidad` | `float` | ✓ |  |
| 10 | `Cantidad_Presentacion` | `float` | ✓ |  |
| 11 | `Peso` | `float` | ✓ |  |
| 12 | `IdUbicacion` | `int` |  |  |
| 13 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |
| 14 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 15 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 16 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 17 | `IdStock` | `int` |  |  |
| 18 | `host` | `nvarchar(50)` | ✓ |  |
| 19 | `no_linea` | `int` | ✓ |  |
| 20 | `Columna` | `int` | ✓ |  |
| 21 | `Nivel` | `int` | ✓ |  |
| 22 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 23 | `Estructura` | `nvarchar(156)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `stock_res`
- `trans_pe_det`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Reservado_By_IdPedidoEnc]
AS
SELECT 
  dbo.stock_res.IdPedido, dbo.stock_res.IdPedidoDet, dbo.stock_res.IdStockRes, dbo.producto.codigo, dbo.producto.nombre AS Producto, dbo.producto_estado.nombre AS Estado, dbo.stock_res.lote, dbo.stock_res.fecha_vence, 
                         SUM(dbo.stock_res.cantidad) AS Cantidad, (CASE WHEN stock_res.IdPresentacion > 0 THEN SUM(stock_res.cantidad) / producto_presentacion.factor ELSE 0 END) AS Cantidad_Presentacion, SUM(dbo.stock_res.peso) AS Peso, 
                         dbo.stock_res.IdUbicacion, dbo.Nombre_Completo_Ubicacion(dbo.stock_res.IdUbicacion, dbo.stock_res.idbodega) AS Nombre_Completo, dbo.unidad_medida.Nombre AS UMBas, 
                         dbo.producto_presentacion.nombre AS Presentacion, dbo.stock_res.lic_plate, dbo.stock_res.IdStock, dbo.stock_res.host, dbo.trans_pe_det.no_linea, 
						 dbo.bodega_ubicacion.indice_x AS Columna, 
                         dbo.bodega_ubicacion.nivel AS Nivel, dbo.bodega_tramo.descripcion AS Tramo,
						 dbo.bodega_tramo.descripcion + ' -C' + CONVERT(NVARCHAR(50),dbo.bodega_ubicacion.indice_x) + ' -N' + CONVERT(NVARCHAR(50),dbo.bodega_ubicacion.nivel) AS Estructura
FROM            dbo.stock_res INNER JOIN
                dbo.trans_pe_det ON dbo.stock_res.IdPedidoDet = dbo.trans_pe_det.IdPedidoDet AND stock_res.IdPedido = trans_pe_det.IdPedidoEnc INNER JOIN
                dbo.producto_bodega ON dbo.producto_bodega.IdProductoBodega = dbo.trans_pe_det.IdProductoBodega INNER JOIN
                dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto LEFT OUTER JOIN
                dbo.producto_presentacion ON dbo.stock_res.IdPresentacion = dbo.producto_presentacion.IdPresentacion INNER JOIN
                dbo.unidad_medida ON dbo.stock_res.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                dbo.producto_estado ON dbo.producto_estado.IdEstado = dbo.stock_res.IdProductoEstado INNER JOIN
                dbo.bodega_ubicacion ON dbo.stock_res.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.stock_res.idbodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega
GROUP BY dbo.stock_res.IdStockRes, dbo.producto.codigo, dbo.stock_res.lote, dbo.stock_res.fecha_vence, dbo.stock_res.IdUbicacion, dbo.producto.nombre, dbo.producto_presentacion.nombre, dbo.unidad_medida.Nombre, 
                         dbo.stock_res.lic_plate, dbo.producto_estado.nombre, dbo.producto_presentacion.factor, dbo.stock_res.IdPresentacion, dbo.stock_res.IdPedido, dbo.stock_res.idbodega, dbo.stock_res.IdPedidoDet, dbo.stock_res.IdStock, 
                         dbo.stock_res.host, dbo.trans_pe_det.no_linea, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_tramo.descripcion
```
