---
id: db-brain-view-vw-ubicaciones-picking
type: db-view
title: dbo.VW_Ubicaciones_Picking
schema: dbo
name: VW_Ubicaciones_Picking
kind: view
modify_date: 2021-11-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ubicaciones_Picking`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-19 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `Descripcion` | `nvarchar(200)` | ✓ |  |
| 4 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 5 | `Sector` | `nvarchar(50)` | ✓ |  |
| 6 | `Area` | `nvarchar(50)` | ✓ |  |
| 7 | `Nivel` | `int` | ✓ |  |
| 8 | `Indice_X` | `int` | ✓ |  |
| 9 | `Codigo_Barra` | `nvarchar(50)` | ✓ |  |
| 10 | `Activo` | `bit` | ✓ |  |
| 11 | `Bloqueada` | `bit` | ✓ |  |
| 12 | `Acepta_Pallet` | `bit` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ubicaciones_Picking]
AS
SELECT DISTINCT 
                         u.IdBodega, u.IdUbicacion, dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) as Descripcion , t.descripcion AS Tramo, s.descripcion AS Sector, a.Descripcion AS Area, u.nivel AS Nivel, u.indice_x AS Indice_X, u.codigo_barra AS Codigo_Barra, u.activo AS Activo, 
                         u.bloqueada AS Bloqueada, u.acepta_pallet AS Acepta_Pallet
FROM            bodega_sector AS s INNER JOIN
                         bodega_tramo AS t ON s.IdSector = t.IdSector AND s.IdBodega = t.IdBodega AND s.IdArea = t.IdArea INNER JOIN
                         bodega_area AS a ON s.IdArea = a.IdArea AND s.IdBodega = a.IdBodega INNER JOIN
                         bodega_ubicacion AS u ON t.IdBodega = u.IdBodega AND t.IdArea = u.IdArea AND t.IdSector = u.IdSector AND t.IdTramo = u.IdTramo
WHERE        (u.ubicacion_picking = 1) AND (u.activo = 1)
```
