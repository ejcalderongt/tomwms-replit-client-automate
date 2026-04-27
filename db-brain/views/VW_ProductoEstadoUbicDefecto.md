---
id: db-brain-view-vw-productoestadoubicdefecto
type: db-view
title: dbo.VW_ProductoEstadoUbicDefecto
schema: dbo
name: VW_ProductoEstadoUbicDefecto
kind: view
modify_date: 2019-07-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoEstadoUbicDefecto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2019-07-29 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdEstado` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `NombreUbic` | `nvarchar(90)` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `producto_estado`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoEstadoUbicDefecto]
AS
SELECT u.IdUbicacion,  p.IdEstado, a.IdBodega, CASE WHEN t.es_rack = 1 THEN 'R' + RIGHT('00' + SUBSTRING(t.descripcion, 2, 
iif(CHARINDEX('-', t.descripcion, 0) < 0, 1, CHARINDEX('-', t.descripcion, 0) - 2)), 2) + ' - ' + 'C' + RIGHT('00' + CONVERT(NVARCHAR(10), 
u.indice_x), 2) + ' - ' + 'T' + SUBSTRING(t.descripcion, iif(CHARINDEX('-', t.descripcion, 0) < 0, 0, CHARINDEX('-', 
t.descripcion, 0) + 1), 1) + ' - ' + 'N' + RIGHT('00' + CONVERT(NVARCHAR(10), u.nivel), 2) 
+ ' - ' + 'Pos' + u.orientacion_pos + ' - ' + '#' + CONVERT(NVARCHAR(10), u.IdUbicacion) 
ELSE t.descripcion END AS NombreUbic
FROM producto_estado AS p 
INNER JOIN bodega_ubicacion AS u ON p.IdUbicacionDefecto = u.IdUbicacion 
INNER JOIN bodega_tramo t ON t.IdTramo = u.IdTramo
INNER JOIN bodega_sector s ON s.IdSector = t.IdSector
INNER JOIN bodega_area a ON s.IdArea = a.IdArea
```
