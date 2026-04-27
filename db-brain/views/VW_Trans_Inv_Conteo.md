---
id: db-brain-view-vw-trans-inv-conteo
type: db-view
title: dbo.VW_Trans_Inv_Conteo
schema: dbo
name: VW_Trans_Inv_Conteo
kind: view
modify_date: 2018-04-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Trans_Inv_Conteo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-04-27 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` | ✓ |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Producto` | `nvarchar(100)` | ✓ |  |
| 4 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 6 | `Estado_Producto` | `nvarchar(50)` | ✓ |  |
| 7 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 8 | `lote` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_vence` | `datetime` | ✓ |  |
| 10 | `Cantidad_Conteo` | `float` | ✓ |  |
| 11 | `Peso_Conteo` | `float` | ✓ |  |
| 12 | `Cantidad_Stock` | `float` | ✓ |  |
| 13 | `Peso_Stock` | `float` | ✓ |  |
| 14 | `Fecha_Ingreso` | `datetime` | ✓ |  |
| 15 | `IdStock` | `int` | ✓ |  |

## Consume

- `bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietarios`
- `trans_inv_ciclico`
- `trans_inv_enc`
- `trans_inv_stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Trans_Inv_Conteo]
AS
SELECT dbo.trans_inv_enc.idinventarioenc, dbo.producto.codigo, dbo.producto.nombre AS Producto, dbo.bodega.nombre AS Bodega, 
dbo.propietarios.nombre_comercial AS Propietario, dbo.producto_estado.nombre AS Estado_Producto, dbo.producto_presentacion.nombre AS Presentacion, 
dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence, SUM(dbo.trans_inv_ciclico.cantidad) AS Cantidad_Conteo, SUM(dbo.trans_inv_ciclico.peso) 
AS Peso_Conteo, dbo.trans_inv_stock.cantidad AS Cantidad_Stock, dbo.trans_inv_stock.peso AS Peso_Stock, dbo.trans_inv_enc.fec_agr AS Fecha_Ingreso, 
dbo.trans_inv_ciclico.IdStock
FROM dbo.bodega LEFT OUTER JOIN
dbo.trans_inv_ciclico INNER JOIN
dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
dbo.producto ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
dbo.propietarios ON dbo.trans_inv_enc.idpropietario = dbo.propietarios.IdPropietario LEFT OUTER JOIN
dbo.trans_inv_stock ON dbo.trans_inv_ciclico.IdStock = dbo.trans_inv_stock.IdStock ON dbo.bodega.IdBodega = dbo.trans_inv_enc.idbodega LEFT OUTER JOIN
dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.trans_inv_enc.idinventarioenc, dbo.producto.codigo, dbo.producto.nombre, dbo.bodega.nombre, dbo.propietarios.nombre_comercial, 
dbo.producto_estado.nombre, dbo.producto_presentacion.nombre, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_enc.fec_agr, 
dbo.trans_inv_ciclico.IdStock, dbo.trans_inv_stock.cantidad, dbo.trans_inv_stock.peso
```
