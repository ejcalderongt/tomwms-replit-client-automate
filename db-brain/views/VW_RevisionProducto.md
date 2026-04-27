---
id: db-brain-view-vw-revisionproducto
type: db-view
title: dbo.VW_RevisionProducto
schema: dbo
name: VW_RevisionProducto
kind: view
modify_date: 2017-08-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RevisionProducto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-08-13 |
| Columnas | 16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Producto` | `nvarchar(50)` | ✓ |  |
| 2 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 3 | `Estado` | `nvarchar(50)` | ✓ |  |
| 4 | `Ubicación` | `nvarchar(50)` | ✓ |  |
| 5 | `Mínimo` | `float` | ✓ |  |
| 6 | `Máximo` | `float` | ✓ |  |
| 7 | `Disponible` | `float` | ✓ |  |
| 8 | `IdPropietarioBodega` | `int` |  |  |
| 9 | `IdPropietario` | `int` |  |  |
| 10 | `IdBodega` | `int` | ✓ |  |
| 11 | `IdProductoBodega` | `int` |  |  |
| 12 | `IdPresentacion` | `int` |  |  |
| 13 | `IdUbicacion` | `int` | ✓ |  |
| 14 | `IdProductoEstado` | `int` | ✓ |  |
| 15 | `IdUnidadMedida` | `int` | ✓ |  |
| 16 | `factor` | `float` |  |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `producto_rellenado`
- `propietarios`
- `stock`
- `transacciones_log`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_RevisionProducto
AS
SELECT DISTINCT 
                      p.nombre AS Producto, pp.nombre AS Presentación, est.nombre AS Estado, u.descripcion AS Ubicación, pr.Minimo AS Mínimo, pr.Maximo AS Máximo, SUM(s.cantidad) 
                      AS Disponible, s.IdPropietarioBodega, pro.IdPropietario, pb.IdBodega, pb.IdProductoBodega, pp.IdPresentacion, u.IdUbicacion, s.IdProductoEstado, 
                      um.IdUnidadMedida, pp.factor
FROM         dbo.producto_presentacion AS pp INNER JOIN
                      dbo.producto AS p ON pp.IdProducto = p.IdProducto INNER JOIN
                      dbo.producto_bodega AS pb ON p.IdProducto = pb.IdProducto INNER JOIN
                      dbo.stock AS s ON pb.IdProductoBodega = s.IdProductoBodega AND pp.IdPresentacion = s.IdPresentacion INNER JOIN
                      dbo.producto_rellenado AS pr ON pp.IdPresentacion = pr.IdPresentacion LEFT OUTER JOIN
                      dbo.bodega_ubicacion AS u ON u.IdUbicacion = pr.IdUbicacion INNER JOIN
                      dbo.propietarios AS pro ON p.IdPropietario = pro.IdPropietario LEFT OUTER JOIN
                      dbo.producto_estado AS est ON pro.IdPropietario = est.IdPropietario AND s.IdProductoEstado = est.IdEstado LEFT OUTER JOIN
                      dbo.unidad_medida AS um ON pro.IdPropietario = um.IdPropietario AND s.IdUnidadMedida = um.IdUnidadMedida
WHERE     (u.IdUbicacion NOT IN
                          (SELECT     IdUbicacion
                            FROM          dbo.transacciones_log
                            WHERE      (IdProductoBodega = pb.IdProductoBodega) AND (IdPresentacion = pp.IdPresentacion) AND (IdProductoEstado = s.IdProductoEstado) AND 
                                                   (IdUnidadMedida = s.IdUnidadMedida) AND (CONVERT(VARCHAR(8), fec_agr, 112) = CONVERT(VARCHAR(8), GETDATE(), 112))))
GROUP BY p.nombre, pp.nombre, est.nombre, u.descripcion, pr.Minimo, pr.Maximo, s.IdPropietarioBodega, pb.IdProductoBodega, pp.IdPresentacion, u.IdUbicacion, 
                      s.IdProductoEstado, um.IdUnidadMedida, pro.IdPropietario, pb.IdBodega, pp.factor
HAVING      (SUM(s.cantidad) <= pr.Minimo) AND (pr.Minimo > 0)
```
