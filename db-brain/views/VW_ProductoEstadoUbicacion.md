---
id: db-brain-view-vw-productoestadoubicacion
type: db-view
title: dbo.VW_ProductoEstadoUbicacion
schema: dbo
name: VW_ProductoEstadoUbicacion
kind: view
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoEstadoUbicacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2020-10-07 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `fec_agr` | `datetime` | ✓ |  |
| 2 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 3 | `fec_mod` | `datetime` | ✓ |  |
| 4 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `Ubicacion` | `nvarchar(156)` | ✓ |  |
| 8 | `IdEstado` | `int` |  |  |
| 9 | `IdProductoEstadUbic` | `int` |  |  |
| 10 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 11 | `IdBodega` | `int` | ✓ |  |
| 12 | `Bodega` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `producto_estado`
- `producto_estado_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoEstadoUbicacion]
AS
SELECT        dbo.producto_estado_ubic.fec_agr, dbo.producto_estado_ubic.user_agr, dbo.producto_estado_ubic.fec_mod, dbo.producto_estado_ubic.user_mod, dbo.producto_estado_ubic.activo, dbo.producto_estado.nombre AS Estado, 
                         'T[' + dbo.bodega_tramo.descripcion + '] - C[' + RIGHT('00' + ISNULL(CONVERT(NVARCHAR(10), dbo.bodega_ubicacion.indice_x), ''), 2) + '] - N[' + RIGHT('00' + ISNULL(CONVERT(NVARCHAR(10), dbo.bodega_ubicacion.nivel), ''), 
                         2) + ']' + ' - P[' + dbo.bodega_ubicacion.orientacion_pos + ']' + ' - #[' + CONVERT(NVARCHAR(25), dbo.bodega_ubicacion.IdUbicacion) + ']' AS Ubicacion, dbo.producto_estado_ubic.IdEstado, 
                         dbo.producto_estado_ubic.IdProductoEstadUbic, dbo.producto_estado_ubic.IdUbicacionDefecto, dbo.bodega.IdBodega, dbo.bodega.nombre AS Bodega
FROM            dbo.bodega_area INNER JOIN
                         dbo.bodega_tramo INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                         dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND 
                         dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega ON dbo.bodega_area.IdBodega = dbo.bodega.IdBodega RIGHT OUTER JOIN
                         dbo.producto_estado_ubic ON dbo.bodega_ubicacion.IdBodega = dbo.producto_estado_ubic.IdBodega AND dbo.bodega_ubicacion.IdUbicacion = dbo.producto_estado_ubic.IdUbicacionDefecto LEFT OUTER JOIN
                         dbo.producto_estado ON dbo.producto_estado_ubic.IdEstado = dbo.producto_estado.IdEstado
```
