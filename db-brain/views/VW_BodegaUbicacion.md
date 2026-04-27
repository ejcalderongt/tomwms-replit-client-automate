---
id: db-brain-view-vw-bodegaubicacion
type: db-view
title: dbo.VW_BodegaUbicacion
schema: dbo
name: VW_BodegaUbicacion
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_BodegaUbicacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdArea` | `int` |  |  |
| 3 | `Area` | `nvarchar(50)` | ✓ |  |
| 4 | `IdSector` | `int` |  |  |
| 5 | `Sector` | `nvarchar(50)` | ✓ |  |
| 6 | `IdTramo` | `int` |  |  |
| 7 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 8 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 9 | `UbicacionCompleta` | `nvarchar(200)` | ✓ |  |
| 10 | `IdUbicacion` | `int` |  |  |
| 11 | `sistema` | `bit` | ✓ |  |
| 12 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 13 | `codigo_barra2` | `nvarchar(50)` | ✓ |  |
| 14 | `dañado` | `bit` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `bloqueada` | `bit` | ✓ |  |
| 17 | `acepta_pallet` | `bit` | ✓ |  |
| 18 | `ubicacion_picking` | `bit` | ✓ |  |
| 19 | `ubicacion_recepcion` | `bit` | ✓ |  |
| 20 | `ubicacion_despacho` | `bit` | ✓ |  |
| 21 | `ubicacion_merma` | `bit` | ✓ |  |
| 22 | `ubicacion_virtual` | `bit` | ✓ |  |
| 23 | `ubicacion_ne` | `bit` | ✓ |  |
| 24 | `Columna` | `int` | ✓ |  |
| 25 | `Nivel` | `int` | ✓ |  |
| 26 | `ubicacion_muelle` | `bit` |  |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
/****** #CKFK20240609 Agregamos campo Ubicacion_Muelle ******/
CREATE VIEW [dbo].[VW_BodegaUbicacion]
AS
SELECT dbo.bodega_ubicacion.IdBodega, dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion AS Sector, dbo.bodega_tramo.IdTramo,
dbo.bodega_tramo.descripcion AS Tramo, dbo.bodega_ubicacion.descripcion, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS UbicacionCompleta,
dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.sistema, dbo.bodega_ubicacion.codigo_barra, dbo.bodega_ubicacion.codigo_barra2, dbo.bodega_ubicacion.dañado, dbo.bodega_ubicacion.activo,
dbo.bodega_ubicacion.bloqueada, dbo.bodega_ubicacion.acepta_pallet, dbo.bodega_ubicacion.ubicacion_picking, dbo.bodega_ubicacion.ubicacion_recepcion, dbo.bodega_ubicacion.ubicacion_despacho,
dbo.bodega_ubicacion.ubicacion_merma, dbo.bodega_ubicacion.ubicacion_virtual, dbo.bodega_ubicacion.ubicacion_ne, dbo.bodega_ubicacion.indice_x AS Columna, dbo.bodega_ubicacion.nivel AS Nivel, 
dbo.bodega_ubicacion.ubicacion_muelle
FROM dbo.bodega INNER JOIN
dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND
dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega
```
