---
id: db-brain-view-vw-operador-zona-picking
type: db-view
title: dbo.VW_Operador_Zona_Picking
schema: dbo
name: VW_Operador_Zona_Picking
kind: view
modify_date: 2022-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Operador_Zona_Picking`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-07-02 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` | ✓ |  |
| 2 | `IdZonaPickingTramoOperador` | `int` |  |  |
| 3 | `IdZonaPickingTramo` | `int` |  |  |
| 4 | `IdZonaPicking` | `int` |  |  |
| 5 | `IdOperador` | `int` |  |  |
| 6 | `Zona_Picking` | `nvarchar(50)` | ✓ |  |
| 7 | `dia_semana` | `int` |  |  |
| 8 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 9 | `min_x` | `int` | ✓ |  |
| 10 | `max_x` | `int` | ✓ |  |
| 11 | `min_y` | `int` | ✓ |  |
| 12 | `max_y` | `int` | ✓ |  |
| 13 | `Dia` | `varchar(9)` | ✓ |  |

## Consume

- `bodega_tramo`
- `operador_zona_picking_tramo`
- `zona_picking`
- `zona_picking_tramo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Operador_Zona_Picking]
AS
SELECT        dbo.zona_picking.IdEmpresa, dbo.operador_zona_picking_tramo.IdZonaPickingTramoOperador, dbo.operador_zona_picking_tramo.IdZonaPickingTramo, dbo.operador_zona_picking_tramo.IdZonaPicking, 
                         dbo.operador_zona_picking_tramo.IdOperador, dbo.zona_picking.nombre AS Zona_Picking, 						 
						 dbo.operador_zona_picking_tramo.dia_semana, dbo.bodega_tramo.descripcion AS Tramo, dbo.zona_picking_tramo.min_x, 
                         dbo.zona_picking_tramo.max_x, dbo.zona_picking_tramo.min_y, dbo.zona_picking_tramo.max_y,
						 case dbo.operador_zona_picking_tramo.dia_semana
							WHEN 1 THEN 'Lunes'
							WHEN 2 THEN 'Martes'
							WHEN 3 THEN 'Miércoles'
							WHEN 4 THEN 'Jueves'
							WHEN 5 THEN 'Viernes'
							WHEN 6 THEN 'Sábado'
							WHEN 7 THEN 'Domingo'
						  END AS Dia

FROM            dbo.operador_zona_picking_tramo INNER JOIN
                         dbo.zona_picking_tramo ON dbo.operador_zona_picking_tramo.IdZonaPickingTramo = dbo.zona_picking_tramo.IdZonaPickingTramo INNER JOIN
                         dbo.zona_picking ON dbo.zona_picking_tramo.IdZonaPicking = dbo.zona_picking.IdZonaPicking INNER JOIN
                         dbo.bodega_tramo ON dbo.zona_picking_tramo.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.zona_picking_tramo.IdSector = dbo.bodega_tramo.IdSector AND dbo.zona_picking_tramo.IdArea = dbo.bodega_tramo.IdArea AND 
                         dbo.zona_picking_tramo.IdBodega = dbo.bodega_tramo.IdBodega
```
