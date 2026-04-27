---
id: db-brain-view-vw-estacionalidadproducto
type: db-view
title: dbo.VW_EstacionalidadProducto
schema: dbo
name: VW_EstacionalidadProducto
kind: view
modify_date: 2022-01-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_EstacionalidadProducto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-01-13 |
| Columnas | 20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Barra` | `nvarchar(35)` | ✓ |  |
| 4 | `nombre` | `nvarchar(100)` | ✓ |  |
| 5 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 6 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 7 | `lote` | `nvarchar(50)` |  |  |
| 8 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_ingreso` | `datetime` | ✓ |  |
| 10 | `fecha_vence` | `datetime` | ✓ |  |
| 11 | `FechaProyectada` | `datetime` | ✓ |  |
| 12 | `Tolerancia_Dias` | `int` | ✓ |  |
| 13 | `CantidadSF` | `float` |  |  |
| 14 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 15 | `IdUbicacion` | `int` |  |  |
| 16 | `Cantidad` | `float` | ✓ |  |
| 17 | `IdProducto` | `int` | ✓ |  |
| 18 | `IdPropietarioBodega` | `int` |  |  |
| 19 | `IdBodega` | `int` | ✓ |  |
| 20 | `UbicacionCompleta` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_EstacionalidadProducto]
AS
SELECT  dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo, dbo.producto.codigo_barra AS Barra, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, 
        dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.lic_plate, dbo.stock.fecha_ingreso, 
		dbo.stock.fecha_vence, DATEADD(day, 0, GETDATE()) AS FechaProyectada, 
        DATEDIFF(DAY,dbo.stock.fecha_ingreso,GETDATE()) as Tolerancia_Dias, dbo.stock.cantidad AS CantidadSF, dbo.producto_estado.nombre AS NomEstado, 
        dbo.stock.IdUbicacion, ROUND(dbo.stock.cantidad / dbo.producto_presentacion.factor, 2) AS Cantidad, dbo.producto.IdProducto, dbo.propietario_bodega.IdPropietarioBodega, dbo.propietario_bodega.IdBodega, 
        dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, dbo.propietario_bodega.IdBodega) AS UbicacionCompleta
FROM            dbo.producto_estado INNER JOIN
        dbo.unidad_medida INNER JOIN
        dbo.propietarios INNER JOIN
        dbo.propietario_bodega INNER JOIN
        dbo.producto_bodega 
		INNER JOIN dbo.stock 
		ON dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega 		
		ON dbo.propietario_bodega.IdPropietarioBodega = dbo.stock.IdPropietarioBodega 
		ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario 
		ON dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida 
		ON dbo.producto_estado.IdEstado = dbo.stock.IdProductoEstado 		
		INNER JOIN dbo.bodega_ubicacion 
		ON dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion 
		AND dbo.producto_bodega.IdBodega = dbo.bodega_ubicacion.IdBodega
		AND dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion 
		LEFT OUTER JOIN
        dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion LEFT OUTER JOIN
        dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
WHERE        (dbo.producto.control_vencimiento = 1)
GROUP BY dbo.propietarios.nombre_comercial, dbo.stock.IdStock, dbo.propietario_bodega.IdPropietarioBodega, dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto.IdProducto, dbo.producto.codigo, 
        dbo.producto.nombre, dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.cantidad, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.producto.existencia_min, dbo.producto.existencia_max, 
        dbo.producto.codigo_barra, dbo.producto.tolerancia, dbo.producto.control_vencimiento, dbo.producto_presentacion.nombre, dbo.producto_estado.IdEstado, dbo.producto_estado.nombre, dbo.stock.serial, dbo.stock.IdUbicacion, 
        dbo.stock.cantidad, dbo.producto_presentacion.factor, dbo.propietario_bodega.IdBodega, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.descripcion, 
        dbo.bodega_ubicacion.orientacion_pos, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.lic_plate
```
