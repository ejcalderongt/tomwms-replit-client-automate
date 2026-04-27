---
id: db-brain-view-vw-ocupacionbodega
type: db-view
title: dbo.VW_OcupacionBodega
schema: dbo
name: VW_OcupacionBodega
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_OcupacionBodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdStock` | `int` |  |  |
| 4 | `Ubicacion` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto_bodega`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_OcupacionBodega]
AS
SELECT dbo.bodega_ubicacion.IdUbicacion, 
       dbo.bodega_ubicacion.IdBodega, ISNULL(dbo.stock.IdStock, 0) AS IdStock,
       dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.idbodega) Ubicacion
FROM   dbo.stock INNER JOIN dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND
	   dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega RIGHT OUTER JOIN 
	   dbo.bodega INNER JOIN
	   dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
	   dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND 
       dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
	   dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND 
	   dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
	   dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND 
	   dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
	   dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega ON dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega AND 
	   dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega
```
