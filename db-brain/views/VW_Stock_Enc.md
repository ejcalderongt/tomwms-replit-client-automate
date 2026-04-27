---
id: db-brain-view-vw-stock-enc
type: db-view
title: dbo.VW_Stock_Enc
schema: dbo
name: VW_Stock_Enc
kind: view
modify_date: 2017-10-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Enc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-02 |
| Columnas | 35 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdUnidadMedida` | `int` |  |  |
| 7 | `IdProductoEstado` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `Propietario` | `nvarchar(100)` |  |  |
| 10 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 11 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 12 | `codigo` | `nvarchar(50)` | ✓ |  |
| 13 | `nombre` | `nvarchar(50)` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` | ✓ |  |
| 15 | `serial` | `nvarchar(50)` | ✓ |  |
| 16 | `CantidadSF` | `float` | ✓ |  |
| 17 | `factor` | `float` | ✓ |  |
| 18 | `Cantidad` | `float` | ✓ |  |
| 19 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 20 | `EstadoUtilizable` | `bit` | ✓ |  |
| 21 | `dañado` | `bit` | ✓ |  |
| 22 | `lic_plate` | `varchar(max)` | ✓ |  |
| 23 | `peso` | `float` | ✓ |  |
| 24 | `IdIndiceRotacion` | `int` | ✓ |  |
| 25 | `alto` | `float` | ✓ |  |
| 26 | `largo` | `float` | ✓ |  |
| 27 | `ancho` | `float` | ✓ |  |
| 28 | `CantidadReservada` | `float` | ✓ |  |
| 29 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 30 | `existencia_min_umbas` | `float` | ✓ |  |
| 31 | `existencia_max_umbas` | `float` | ✓ |  |
| 32 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 33 | `costo` | `float` | ✓ |  |
| 34 | `existencia_min_pres` | `float` | ✓ |  |
| 35 | `existencia_max_pres` | `float` | ✓ |  |

## Consume

- `indice_rotacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Stock_Enc
AS
SELECT     dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, 
                      dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, 
                      dbo.propietarios.nombre_comercial AS Propietario, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, 
                      dbo.producto.codigo, dbo.producto.nombre, dbo.stock.lote, dbo.stock.serial, SUM(dbo.stock.cantidad) AS CantidadSF, dbo.producto_presentacion.factor, 
                      SUM(dbo.stock.cantidad / dbo.producto_presentacion.factor) AS Cantidad, dbo.producto_estado.nombre AS NomEstado, 
                      dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.producto_estado.dañado, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, 
                      dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.stock_res.cantidad AS CantidadReservada, 
                      dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto.existencia_min AS existencia_min_umbas, dbo.producto.existencia_max AS existencia_max_umbas,
                       dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia AS existencia_min_pres, 
                      dbo.producto_presentacion.MaximoExistencia AS existencia_max_pres
FROM         dbo.producto_estado RIGHT OUTER JOIN
                      dbo.producto_bodega INNER JOIN
                      dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto LEFT OUTER JOIN
                      dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion RIGHT OUTER JOIN
                      dbo.unidad_medida INNER JOIN
                      dbo.propietarios INNER JOIN
                      dbo.stock INNER JOIN
                      dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON 
                      dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida ON 
                      dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega LEFT OUTER JOIN
                      dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock AND dbo.stock.IdPropietarioBodega = dbo.stock_res.IdPropietarioBodega AND 
                      dbo.stock.IdProductoBodega = dbo.stock_res.IdProductoBodega AND dbo.stock.IdUbicacion = dbo.stock_res.IdUbicacion ON 
                      dbo.producto_estado.IdEstado = dbo.stock.IdProductoEstado LEFT OUTER JOIN
                      dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto_bodega.IdProductoBodega, 
                      dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, 
                      dbo.producto.nombre, dbo.stock.lote, dbo.stock.serial, dbo.producto_bodega.IdBodega, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, 
                      dbo.producto_estado.utilizable, dbo.producto_estado.dañado, dbo.stock.IdPresentacion, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, 
                      dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.stock_res.cantidad, dbo.producto_presentacion.factor, 
                      dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, 
                      dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia
```
