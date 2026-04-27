---
id: db-brain-view-vw-bodegasector
type: db-view
title: dbo.VW_BodegaSector
schema: dbo
name: VW_BodegaSector
kind: view
modify_date: 2016-04-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_BodegaSector`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-04-25 |
| Columnas | 34 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Expr1` | `int` |  |  |
| 2 | `IdSector` | `int` |  |  |
| 3 | `IdArea` | `int` |  |  |
| 4 | `sistema` | `bit` | ✓ |  |
| 5 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `alto` | `float` | ✓ |  |
| 12 | `largo` | `float` | ✓ |  |
| 13 | `ancho` | `float` | ✓ |  |
| 14 | `margen_izquierdo` | `float` | ✓ |  |
| 15 | `margen_derecho` | `float` | ✓ |  |
| 16 | `margen_superior` | `float` | ✓ |  |
| 17 | `margen_inferior` | `float` | ✓ |  |
| 18 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 19 | `Expr2` | `int` |  |  |
| 20 | `Expr3` | `bit` | ✓ |  |
| 21 | `Expr4` | `nvarchar(50)` | ✓ |  |
| 22 | `Expr5` | `nvarchar(25)` | ✓ |  |
| 23 | `Expr6` | `datetime` | ✓ |  |
| 24 | `Expr7` | `nvarchar(25)` | ✓ |  |
| 25 | `Expr8` | `datetime` | ✓ |  |
| 26 | `Expr9` | `bit` | ✓ |  |
| 27 | `Expr10` | `float` | ✓ |  |
| 28 | `Expr11` | `float` | ✓ |  |
| 29 | `Expr12` | `float` | ✓ |  |
| 30 | `Expr13` | `float` | ✓ |  |
| 31 | `Expr14` | `float` | ✓ |  |
| 32 | `Expr15` | `float` | ✓ |  |
| 33 | `Expr16` | `float` | ✓ |  |
| 34 | `Expr17` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_BodegaSector
AS
SELECT        dbo.bodega_sector.IdSector AS Expr1, dbo.bodega_sector.*, dbo.bodega_sector.IdArea AS Expr2, dbo.bodega_sector.sistema AS Expr3, dbo.bodega_sector.descripcion AS Expr4, 
                         dbo.bodega_sector.user_agr AS Expr5, dbo.bodega_sector.fec_agr AS Expr6, dbo.bodega_sector.user_mod AS Expr7, dbo.bodega_sector.fec_mod AS Expr8, dbo.bodega_sector.activo AS Expr9, 
                         dbo.bodega_sector.alto AS Expr10, dbo.bodega_sector.largo AS Expr11, dbo.bodega_sector.ancho AS Expr12, dbo.bodega_sector.margen_izquierdo AS Expr13, dbo.bodega_sector.margen_derecho AS Expr14, 
                         dbo.bodega_sector.margen_superior AS Expr15, dbo.bodega_sector.margen_inferior AS Expr16, dbo.bodega_sector.Codigo AS Expr17
FROM            dbo.bodega INNER JOIN
                         dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega RIGHT OUTER JOIN
                         dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea
```
