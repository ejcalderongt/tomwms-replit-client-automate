---
id: db-brain-view-vw-rptstock
type: db-view
title: dbo.VW_RptStock
schema: dbo
name: VW_RptStock
kind: view
modify_date: 2017-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RptStock`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-05-05 |
| Columnas | 16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Producto` | `nvarchar(50)` | ✓ |  |
| 4 | `Estado` | `nvarchar(50)` | ✓ |  |
| 5 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 6 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 7 | `lote` | `nvarchar(50)` | ✓ |  |
| 8 | `serial` | `nvarchar(50)` | ✓ |  |
| 9 | `Cant Presentación` | `float` | ✓ |  |
| 10 | `Cant U.M Bas` | `float` | ✓ |  |
| 11 | `peso` | `float` | ✓ |  |
| 12 | `fecha_manufactura` | `datetime` | ✓ |  |
| 13 | `fecha_vence` | `datetime` | ✓ |  |
| 14 | `NoSerie` | `nvarchar(50)` | ✓ |  |
| 15 | `NoSerieInicial` | `nvarchar(50)` | ✓ |  |
| 16 | `NoSerieFinal` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_se`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_RptStock
AS
SELECT     s.IdStock, p.nombre_comercial AS Propietario, pr.nombre AS Producto, pe.nombre AS Estado, pp.nombre AS Presentacion, u.Nombre AS UnidadMedida, s.lote, s.serial,
                       s.cantidad / pp.factor AS 'Cant Presentación', s.cantidad AS 'Cant U.M Bas', s.peso, s.fecha_manufactura, s.fecha_vence, se.NoSerie, se.NoSerieInicial, 
                      se.NoSerieFinal
FROM         dbo.stock AS s INNER JOIN
                      dbo.propietario_bodega AS pb ON s.IdPropietarioBodega = pb.IdPropietarioBodega INNER JOIN
                      dbo.propietarios AS p ON pb.IdPropietario = p.IdPropietario INNER JOIN
                      dbo.producto_bodega AS prb ON s.IdProductoBodega = prb.IdProductoBodega INNER JOIN
                      dbo.producto AS pr ON prb.IdProducto = pr.IdProducto LEFT OUTER JOIN
                      dbo.producto_estado AS pe ON s.IdProductoEstado = pe.IdEstado AND p.IdPropietario = pe.IdPropietario LEFT OUTER JOIN
                      dbo.producto_presentacion AS pp ON s.IdPresentacion = pp.IdPresentacion AND pr.IdProducto = pp.IdProducto LEFT OUTER JOIN
                      dbo.unidad_medida AS u ON s.IdUnidadMedida = u.IdUnidadMedida AND p.IdPropietario = u.IdPropietario LEFT OUTER JOIN
                      dbo.stock_se AS se ON s.IdStock = se.IdStock
```
