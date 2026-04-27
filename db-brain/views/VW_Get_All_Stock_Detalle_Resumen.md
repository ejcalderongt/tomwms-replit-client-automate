---
id: db-brain-view-vw-get-all-stock-detalle-resumen
type: db-view
title: dbo.VW_Get_All_Stock_Detalle_Resumen
schema: dbo
name: VW_Get_All_Stock_Detalle_Resumen
kind: view
modify_date: 2020-02-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_All_Stock_Detalle_Resumen`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2020-02-05 |
| Columnas | 21 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `Producto` | `nvarchar(100)` | ✓ |  |
| 7 | `Barra` | `nvarchar(35)` | ✓ |  |
| 8 | `Estado` | `nvarchar(50)` | ✓ |  |
| 9 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 10 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 11 | `serial` | `nvarchar(50)` | ✓ |  |
| 12 | `Cant_Presentacion` | `float` | ✓ |  |
| 13 | `Cant_UMBas` | `float` | ✓ |  |
| 14 | `Fecha_Ingreso` | `datetime` | ✓ |  |
| 15 | `Fecha_Vence` | `datetime` | ✓ |  |
| 16 | `lote` | `nvarchar(50)` |  |  |
| 17 | `NoRecepcion` | `int` | ✓ |  |
| 18 | `IdUbicacion` | `int` |  |  |
| 19 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 20 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 21 | `largo` | `float` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
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
CREATE VIEW dbo.VW_Get_All_Stock_Detalle_Resumen
AS
SELECT        s.IdStock, pr.IdProducto, prb.IdProductoBodega, pr.codigo AS Codigo, p.nombre_comercial AS Propietario, pr.nombre AS Producto, pr.codigo_barra AS Barra, pe.nombre AS Estado, pp.nombre AS Presentación, 
                         u.Nombre AS UMBas, s.serial, s.cantidad / pp.factor AS Cant_Presentacion, s.cantidad AS Cant_UMBas, s.fecha_ingreso AS Fecha_Ingreso, s.fecha_vence AS Fecha_Vence, s.lote, s.IdRecepcionEnc AS NoRecepcion, 
                         s.IdUbicacion, dbo.bodega_tramo.descripcion AS Tramo, u1.descripcion AS Ubicacion, u1.largo
FROM            dbo.producto_estado AS pe RIGHT OUTER JOIN
                         dbo.stock AS s INNER JOIN
                         dbo.propietario_bodega AS pb ON s.IdPropietarioBodega = pb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS p ON pb.IdPropietario = p.IdPropietario INNER JOIN
                         dbo.producto_bodega AS prb ON s.IdProductoBodega = prb.IdProductoBodega INNER JOIN
                         dbo.producto AS pr ON prb.IdProducto = pr.IdProducto INNER JOIN
                         dbo.unidad_medida AS u ON s.IdUnidadMedida = u.IdUnidadMedida LEFT OUTER JOIN
                         dbo.bodega_tramo INNER JOIN
                         dbo.bodega_ubicacion AS u1 ON dbo.bodega_tramo.IdTramo = u1.IdTramo ON s.IdBodega = u1.IdBodega AND s.IdUbicacion = u1.IdUbicacion LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u2 ON s.IdBodega = u2.IdBodega AND s.IdUbicacion_anterior = u2.IdUbicacion ON pe.IdEstado = s.IdProductoEstado LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON s.IdPresentacion = pp.IdPresentacion
```
