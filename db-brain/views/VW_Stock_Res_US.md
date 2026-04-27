---
id: db-brain-view-vw-stock-res-us
type: db-view
title: dbo.VW_Stock_Res_US
schema: dbo
name: VW_Stock_Res_US
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res_US`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 40 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `IdProducto` | `int` | ✓ |  |
| 4 | `nombre` | `nvarchar(100)` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdStock` | `int` | ✓ |  |
| 7 | `IdUnidadMedida` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 11 | `lote` | `nvarchar(50)` | ✓ |  |
| 12 | `fecha_vence` | `datetime` | ✓ |  |
| 13 | `Cantidad_UMBas` | `float` | ✓ |  |
| 14 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 15 | `IdIndiceRotacion` | `int` | ✓ |  |
| 16 | `IdTramo` | `int` |  |  |
| 17 | `Ancho_ubicacion` | `float` | ✓ |  |
| 18 | `Largo_ubicacion` | `float` | ✓ |  |
| 19 | `Alto_ubicacion` | `float` | ✓ |  |
| 20 | `Existencia_min_umbas` | `float` | ✓ |  |
| 21 | `Existencia_max_umbas` | `float` | ✓ |  |
| 22 | `Existencia_min_pres` | `float` | ✓ |  |
| 23 | `Existencia_max_pres` | `float` | ✓ |  |
| 24 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 25 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 26 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 27 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 28 | `activo` | `bit` | ✓ |  |
| 29 | `bloqueada` | `bit` | ✓ |  |
| 30 | `ubicacion_merma` | `bit` | ✓ |  |
| 31 | `Familia` | `nvarchar(50)` | ✓ |  |
| 32 | `Area` | `nvarchar(200)` | ✓ |  |
| 33 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 34 | `IdTipoProducto` | `int` | ✓ |  |
| 35 | `IdMarca` | `int` | ✓ |  |
| 36 | `cantidad` | `float` | ✓ |  |
| 37 | `es_rack` | `bit` | ✓ |  |
| 38 | `IdArea` | `int` |  |  |
| 39 | `IdProductoEstado` | `int` | ✓ |  |
| 40 | `IdTipoRotacion` | `int` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Area`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_marca`
- `producto_presentacion`
- `producto_tipo`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_det`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Res_US]
AS
SELECT dbo.bodega_ubicacion.IdBodega, dbo.bodega_ubicacion.IdUbicacion, dbo.producto.IdProducto, dbo.producto.nombre, dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock, dbo.unidad_medida.IdUnidadMedida, 
                  dbo.stock.IdPresentacion, dbo.producto.codigo, dbo.producto.codigo_barra, dbo.stock.lote, dbo.stock.fecha_vence, dbo.stock.cantidad AS Cantidad_UMBas, dbo.stock.lic_plate, dbo.producto.IdIndiceRotacion, 
                  dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS Ancho_ubicacion, dbo.bodega_ubicacion.largo AS Largo_ubicacion, dbo.bodega_ubicacion.alto AS Alto_ubicacion, dbo.producto.existencia_min AS Existencia_min_umbas, 
                  dbo.producto.existencia_max AS Existencia_max_umbas, dbo.producto_presentacion.MinimoExistencia AS Existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS Existencia_max_pres, 
                  dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre, dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, 
                  dbo.bodega_ubicacion.activo, dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.ubicacion_merma, dbo.producto_familia.nombre AS Familia, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega) 
                  AS Area, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_tipo.IdTipoProducto, dbo.producto_marca.IdMarca, dbo.stock.cantidad, dbo.bodega_tramo.es_rack, ISNULL(dbo.bodega_ubicacion.IdArea, 0) AS IdArea, 
                  dbo.stock.IdProductoEstado, dbo.producto.IdTipoRotacion
FROM     dbo.producto_marca RIGHT OUTER JOIN
                  dbo.bodega_ubicacion LEFT OUTER JOIN
                  dbo.bodega INNER JOIN
                  dbo.producto_clasificacion RIGHT OUTER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                  dbo.unidad_medida INNER JOIN
                  dbo.propietarios INNER JOIN
                  dbo.stock INNER JOIN
                  dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON 
                  dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega ON dbo.producto_clasificacion.IdClasificacion = dbo.producto.IdClasificacion ON 
                  dbo.bodega.IdBodega = dbo.stock.IdBodega ON dbo.bodega_ubicacion.IdBodega = dbo.stock.IdBodega AND dbo.bodega_ubicacion.IdUbicacion = dbo.stock.IdUbicacion LEFT OUTER JOIN
                  dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_ubicacion.IdArea = dbo.bodega_tramo.IdArea AND 
                  dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
                  dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                  dbo.producto_familia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia ON dbo.producto_marca.IdMarca = dbo.producto.IdMarca AND dbo.producto_marca.IdPropietario = dbo.producto.IdPropietario LEFT OUTER JOIN
                  dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto AND dbo.producto.IdPropietario = dbo.producto_marca.IdPropietario LEFT OUTER JOIN
                  dbo.stock_det ON dbo.stock.IdStock = dbo.stock_det.IdStock
GROUP BY dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.producto.IdProducto, dbo.producto.codigo, dbo.stock.lote, dbo.stock.fecha_vence, 
                  dbo.stock.IdPresentacion, dbo.stock.lic_plate, dbo.producto.IdIndiceRotacion, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto, dbo.producto.existencia_min, 
                  dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.stock.cantidad, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, 
                  dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.producto_familia.nombre, dbo.bodega_ubicacion.IdArea, dbo.producto_clasificacion.nombre, dbo.producto_tipo.IdTipoProducto, dbo.producto_marca.IdMarca, 
                  dbo.bodega_ubicacion.activo, dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.ubicacion_merma, dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.IdArea, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, 
                  dbo.bodega_ubicacion.IdBodega), dbo.bodega_ubicacion.IdBodega, dbo.stock.IdProductoEstado, dbo.producto.nombre, dbo.producto.IdTipoRotacion
```
