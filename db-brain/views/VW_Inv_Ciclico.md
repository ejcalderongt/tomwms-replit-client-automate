---
id: db-brain-view-vw-inv-ciclico
type: db-view
title: dbo.VW_Inv_Ciclico
schema: dbo
name: VW_Inv_Ciclico
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Inv_Ciclico`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 40 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` |  |  |
| 2 | `Idinvciclico` | `int` | ✓ |  |
| 3 | `IdUbicacion` | `int` | ✓ |  |
| 4 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 5 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 6 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |
| 7 | `IdStock` | `int` |  |  |
| 8 | `codigo` | `nvarchar(50)` | ✓ |  |
| 9 | `Producto` | `nvarchar(100)` | ✓ |  |
| 10 | `Presentacion` | `nvarchar(50)` |  |  |
| 11 | `lote` | `nvarchar(50)` | ✓ |  |
| 12 | `lote_stock` | `nvarchar(50)` | ✓ |  |
| 13 | `Estado` | `nvarchar(50)` | ✓ |  |
| 14 | `Cantidad_Ciclico` | `float` | ✓ |  |
| 15 | `Peso_Ciclico` | `float` | ✓ |  |
| 16 | `IdPropietario` | `int` |  |  |
| 17 | `IdClasificacion` | `int` | ✓ |  |
| 18 | `IdFamilia` | `int` | ✓ |  |
| 19 | `IdEstado` | `int` | ✓ |  |
| 20 | `EsNuevo` | `bit` | ✓ |  |
| 21 | `IdTramo` | `int` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `IdProductoBodega` | `int` |  |  |
| 24 | `EsPallet` | `bit` | ✓ |  |
| 25 | `lic_plate` | `nvarchar(100)` | ✓ |  |
| 26 | `IdPresentacion` | `int` | ✓ |  |
| 27 | `fecha_vence_stock` | `datetime` | ✓ |  |
| 28 | `peso_stock` | `float` | ✓ |  |
| 29 | `Cantidad_Stock` | `float` | ✓ |  |
| 30 | `peso_reconteo` | `float` | ✓ |  |
| 31 | `NombreTipoProducto` | `nvarchar(50)` | ✓ |  |
| 32 | `IdProducto` | `int` |  |  |
| 33 | `Factor` | `float` |  |  |
| 34 | `Ubicacion_Nueva` | `nvarchar(200)` | ✓ |  |
| 35 | `IdProductoEst_nuevo` | `int` | ✓ |  |
| 36 | `Lote_Nuevo` | `nvarchar(50)` | ✓ |  |
| 37 | `Fecha_vence_nueva` | `datetime` | ✓ |  |
| 38 | `IdUbicacion_nuevo` | `int` | ✓ |  |
| 39 | `IdBodega` | `int` | ✓ |  |
| 40 | `EstadoNuevo` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `producto_tipo`
- `trans_inv_ciclico`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Inv_Ciclico]
AS
SELECT TOP (100) PERCENT dbo.trans_inv_ciclico.idinventarioenc, MAX(dbo.trans_inv_ciclico.idinvciclico) AS Idinvciclico, dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, 
                  dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.trans_inv_ciclico.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, 
                  dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, producto_estado_1.nombre AS Estado, SUM(dbo.trans_inv_ciclico.cantidad) 
                  AS Cantidad_Ciclico, SUM(dbo.trans_inv_ciclico.peso) AS Peso_Ciclico, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, producto_estado_1.IdEstado, dbo.trans_inv_ciclico.EsNuevo, 
                  dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.IdPresentacion, 
                  dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
                  ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, dbo.Nombre_Completo_Ubicacion(dbo.trans_inv_ciclico.IdUbicacion_nuevo, dbo.bodega_ubicacion.IdBodega) AS Ubicacion_Nueva, dbo.trans_inv_ciclico.IdProductoEst_nuevo, 
                  dbo.trans_inv_ciclico.lote AS Lote_Nuevo, dbo.trans_inv_ciclico.fecha_vence AS Fecha_vence_nueva, dbo.trans_inv_ciclico.IdUbicacion_nuevo, dbo.trans_inv_ciclico.IdBodega, dbo.producto_estado.nombre AS EstadoNuevo
FROM     dbo.trans_inv_ciclico INNER JOIN
                  dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega INNER JOIN
                  dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEst_nuevo = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                  dbo.bodega_tramo INNER JOIN
                  dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                  dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                  dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                  dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
                  dbo.producto_estado AS producto_estado_1 ON dbo.trans_inv_ciclico.IdProductoEstado = producto_estado_1.IdEstado LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
                  dbo.trans_inv_ciclico.lote, producto_estado_1.nombre, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, producto_estado_1.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, 
                  dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, dbo.trans_inv_ciclico.IdPresentacion, 
                  dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, 
                  dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, dbo.trans_inv_ciclico.IdProductoEst_nuevo, 
                  dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.IdUbicacion_nuevo, dbo.Nombre_Completo_Ubicacion(dbo.trans_inv_ciclico.IdUbicacion_nuevo, dbo.bodega_ubicacion.IdBodega), 
                  dbo.Nombre_Completo_Ubicacion(dbo.trans_inv_ciclico.IdUbicacion, dbo.bodega_ubicacion.IdBodega), dbo.trans_inv_ciclico.IdBodega, dbo.trans_inv_ciclico.idinventarioenc, dbo.producto_estado.nombre
ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos
```
