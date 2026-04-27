---
id: db-brain-view-vw-bodegamuelle
type: db-view
title: dbo.VW_BodegaMuelle
schema: dbo
name: VW_BodegaMuelle
kind: view
modify_date: 2025-06-15
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_BodegaMuelle`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-15 |
| Columnas | 5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 4 | `Activo` | `bit` | ✓ |  |
| 5 | `Ubicacion` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_BodegaMuelle]
AS
SELECT bm.IdMuelle AS Código, b.nombre AS Bodega, bm.nombre AS Muelle, bm.Activo,
dbo.Nombre_Completo_Ubicacion(bm.IdUbicacionDefecto,bm.IdBodega) as Ubicacion
FROM     dbo.bodega_muelles AS bm INNER JOIN
dbo.bodega AS b ON bm.IdBodega = b.IdBodega LEFT OUTER JOIN
dbo.bodega_ubicacion ON bm.IdBodega = dbo.bodega_ubicacion.IdBodega AND bm.IdUbicacionDefecto = dbo.bodega_ubicacion.IdUbicacion
```
