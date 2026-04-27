---
id: db-brain-view-vw-zona-picking-tramo-operador
type: db-view
title: dbo.VW_Zona_Picking_Tramo_Operador
schema: dbo
name: VW_Zona_Picking_Tramo_Operador
kind: view
modify_date: 2022-06-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Zona_Picking_Tramo_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-10 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` | ✓ |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdZonaPickingTramoOperador` | `int` |  |  |
| 4 | `IdZonaPickingTramo` | `int` |  |  |
| 5 | `IdZonaPicking` | `int` |  |  |
| 6 | `IdOperador` | `int` |  |  |
| 7 | `dia_semana` | `int` |  |  |
| 8 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `activo` | `bit` | ✓ |  |

## Consume

- `operador_zona_picking_tramo`
- `zona_picking`
- `zona_picking_tramo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Zona_Picking_Tramo_Operador]
AS
SELECT dbo.zona_picking.IdEmpresa, dbo.zona_picking_tramo.IdBodega, dbo.operador_zona_picking_tramo.IdZonaPickingTramoOperador, dbo.operador_zona_picking_tramo.IdZonaPickingTramo,
dbo.operador_zona_picking_tramo.IdZonaPicking, dbo.operador_zona_picking_tramo.IdOperador, dbo.operador_zona_picking_tramo.dia_semana, dbo.operador_zona_picking_tramo.user_agr,
dbo.operador_zona_picking_tramo.fec_agr, dbo.operador_zona_picking_tramo.user_mod, dbo.operador_zona_picking_tramo.fec_mod, dbo.operador_zona_picking_tramo.activo
FROM dbo.zona_picking INNER JOIN
dbo.zona_picking_tramo ON dbo.zona_picking.IdZonaPicking = dbo.zona_picking_tramo.IdZonaPicking INNER JOIN
dbo.operador_zona_picking_tramo ON dbo.zona_picking.IdZonaPicking = dbo.operador_zona_picking_tramo.IdZonaPicking
GROUP BY dbo.zona_picking.IdEmpresa, dbo.zona_picking_tramo.IdBodega, dbo.operador_zona_picking_tramo.IdZonaPickingTramoOperador, dbo.operador_zona_picking_tramo.IdZonaPickingTramo,
dbo.operador_zona_picking_tramo.IdZonaPicking, dbo.operador_zona_picking_tramo.IdOperador, dbo.operador_zona_picking_tramo.dia_semana, dbo.operador_zona_picking_tramo.user_agr,
dbo.operador_zona_picking_tramo.fec_agr, dbo.operador_zona_picking_tramo.user_mod, dbo.operador_zona_picking_tramo.fec_mod, dbo.operador_zona_picking_tramo.activo
```
