---
id: db-brain-view-vw-zona-picking-tramo
type: db-view
title: dbo.VW_Zona_Picking_Tramo
schema: dbo
name: VW_Zona_Picking_Tramo
kind: view
modify_date: 2022-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Zona_Picking_Tramo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-27 |
| Columnas | 18 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Area` | `nvarchar(50)` | ✓ |  |
| 2 | `Sector` | `nvarchar(50)` | ✓ |  |
| 3 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 4 | `IdZonaPickingTramo` | `int` |  |  |
| 5 | `IdZonaPicking` | `int` |  |  |
| 6 | `IdBodega` | `int` | ✓ |  |
| 7 | `IdArea` | `int` | ✓ |  |
| 8 | `IdSector` | `int` | ✓ |  |
| 9 | `IdTramo` | `int` | ✓ |  |
| 10 | `min_x` | `int` | ✓ |  |
| 11 | `max_x` | `int` | ✓ |  |
| 12 | `min_y` | `int` | ✓ |  |
| 13 | `max_y` | `int` | ✓ |  |
| 14 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `activo` | `bit` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `zona_picking_tramo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Zona_Picking_Tramo]
AS
SELECT        dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.descripcion AS Sector, dbo.bodega_tramo.descripcion AS Tramo, dbo.zona_picking_tramo.*
FROM            dbo.zona_picking_tramo INNER JOIN
                         dbo.bodega_tramo ON dbo.zona_picking_tramo.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.zona_picking_tramo.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.zona_picking_tramo.IdArea = dbo.bodega_tramo.IdArea AND
                          dbo.zona_picking_tramo.IdSector = dbo.bodega_tramo.IdSector INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega
```
