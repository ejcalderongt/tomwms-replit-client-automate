---
id: db-brain-view-vw-bodegatramo
type: db-view
title: dbo.VW_BodegaTramo
schema: dbo
name: VW_BodegaTramo
kind: view
modify_date: 2022-05-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_BodegaTramo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-26 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdArea` | `int` |  |  |
| 3 | `IdSector` | `int` |  |  |
| 4 | `IdTramo` | `int` |  |  |
| 5 | `Area` | `nvarchar(50)` | ✓ |  |
| 6 | `Sector` | `nvarchar(50)` | ✓ |  |
| 7 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 8 | `EsRack` | `bit` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_BodegaTramo]
AS
SELECT        dbo.bodega.IdBodega, dbo.bodega_sector.IdArea, dbo.bodega_sector.IdSector, dbo.bodega_tramo.IdTramo, dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.descripcion AS Sector, 
                         dbo.bodega_tramo.descripcion AS Tramo, dbo.bodega_tramo.es_rack AS EsRack
FROM            dbo.bodega_tramo INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega LEFT OUTER JOIN
                         dbo.bodega INNER JOIN
                         dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega ON dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega AND dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea
```
