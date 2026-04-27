---
id: db-brain-view-vw-ubicaciones-tramo-disponibles
type: db-view
title: dbo.VW_Ubicaciones_Tramo_Disponibles
schema: dbo
name: VW_Ubicaciones_Tramo_Disponibles
kind: view
modify_date: 2021-11-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ubicaciones_Tramo_Disponibles`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-18 |
| Columnas | 36 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTramo` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `IdStock` | `int` |  |  |
| 4 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 5 | `ancho` | `float` | ✓ |  |
| 6 | `largo` | `float` | ✓ |  |
| 7 | `alto` | `float` | ✓ |  |
| 8 | `nivel` | `int` | ✓ |  |
| 9 | `indice_x` | `int` | ✓ |  |
| 10 | `IdIndiceRotacion` | `int` | ✓ |  |
| 11 | `IdTipoRotacion` | `int` | ✓ |  |
| 12 | `sistema` | `bit` | ✓ |  |
| 13 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 14 | `codigo_barra2` | `nvarchar(50)` | ✓ |  |
| 15 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `dañado` | `bit` | ✓ |  |
| 20 | `activo` | `bit` | ✓ |  |
| 21 | `bloqueada` | `bit` | ✓ |  |
| 22 | `acepta_pallet` | `bit` | ✓ |  |
| 23 | `ubicacion_picking` | `bit` | ✓ |  |
| 24 | `ubicacion_recepcion` | `bit` | ✓ |  |
| 25 | `ubicacion_despacho` | `bit` | ✓ |  |
| 26 | `ubicacion_merma` | `bit` | ✓ |  |
| 27 | `margen_izquierdo` | `float` | ✓ |  |
| 28 | `margen_derecho` | `float` | ✓ |  |
| 29 | `margen_superior` | `float` | ✓ |  |
| 30 | `margen_inferior` | `float` | ✓ |  |
| 31 | `orientacion_pos` | `nvarchar(50)` | ✓ |  |
| 32 | `ubicacion_virtual` | `bit` | ✓ |  |
| 33 | `ubicacion_ne` | `bit` | ✓ |  |
| 34 | `IdBodega` | `int` |  |  |
| 35 | `IdArea` | `int` |  |  |
| 36 | `IdSector` | `int` |  |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `producto_bodega`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ubicaciones_Tramo_Disponibles]
AS
SELECT        dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.IdUbicacion, ISNULL(dbo.stock.IdStock, 0) AS IdStock, dbo.bodega_ubicacion.descripcion, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, 
                         dbo.bodega_ubicacion.alto, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.IdIndiceRotacion, dbo.bodega_ubicacion.IdTipoRotacion, dbo.bodega_ubicacion.sistema, 
                         dbo.bodega_ubicacion.codigo_barra, dbo.bodega_ubicacion.codigo_barra2, dbo.bodega_ubicacion.user_agr, dbo.bodega_ubicacion.fec_agr, dbo.bodega_ubicacion.user_mod, dbo.bodega_ubicacion.fec_mod, 
                         dbo.bodega_ubicacion.dañado, dbo.bodega_ubicacion.activo, dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.acepta_pallet, dbo.bodega_ubicacion.ubicacion_picking, dbo.bodega_ubicacion.ubicacion_recepcion, 
                         dbo.bodega_ubicacion.ubicacion_despacho, dbo.bodega_ubicacion.ubicacion_merma, dbo.bodega_ubicacion.margen_izquierdo, dbo.bodega_ubicacion.margen_derecho, dbo.bodega_ubicacion.margen_superior, 
                         dbo.bodega_ubicacion.margen_inferior, dbo.bodega_ubicacion.orientacion_pos, dbo.bodega_ubicacion.ubicacion_virtual, dbo.bodega_ubicacion.ubicacion_ne, dbo.bodega_ubicacion.IdBodega, dbo.bodega_ubicacion.IdArea, 
                         dbo.bodega_ubicacion.IdSector
FROM            dbo.stock INNER JOIN
                         dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega RIGHT OUTER JOIN
                         dbo.bodega INNER JOIN
                         dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                         dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega ON dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND 
                         dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega
```
