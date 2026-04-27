---
id: db-brain-view-vw-tareas-hh
type: db-view
title: dbo.VW_tareas_hh
schema: dbo
name: VW_tareas_hh
kind: view
modify_date: 2016-09-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_tareas_hh`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-09-16 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareahh` | `int` |  |  |
| 2 | `IdPropietario` | `int` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 4 | `IdBodega` | `int` | ✓ |  |
| 5 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 6 | `IdMuelle` | `int` | ✓ |  |
| 7 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 8 | `IdEstado` | `int` | ✓ |  |
| 9 | `Estado` | `nvarchar(50)` | ✓ |  |
| 10 | `IdPrioridad` | `int` | ✓ |  |
| 11 | `Prioridad` | `nvarchar(50)` | ✓ |  |
| 12 | `IdTipoTarea` | `int` | ✓ |  |
| 13 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 14 | `FechaInicio` | `datetime` | ✓ |  |
| 15 | `FechaFin` | `datetime` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
- `propietarios`
- `sis_estado_tarea_hh`
- `sis_prioridad_tarea_hh`
- `sis_tipo_tarea`
- `tarea_hh`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_tareas_hh
AS
SELECT     dbo.tarea_hh.IdTareahh, dbo.tarea_hh.IdPropietario, dbo.propietarios.nombre_comercial AS Propietario, dbo.tarea_hh.IdBodega, dbo.bodega.nombre AS Bodega, 
                      dbo.tarea_hh.IdMuelle, dbo.bodega_muelles.nombre AS Muelle, dbo.tarea_hh.IdEstado, dbo.sis_estado_tarea_hh.descripcion AS Estado, dbo.tarea_hh.IdPrioridad, 
                      dbo.sis_prioridad_tarea_hh.descripcion AS Prioridad, dbo.tarea_hh.IdTipoTarea, dbo.sis_tipo_tarea.Nombre AS TipoTarea, dbo.tarea_hh.FechaInicio, 
                      dbo.tarea_hh.FechaFin
FROM         dbo.tarea_hh LEFT OUTER JOIN
                      dbo.sis_tipo_tarea ON dbo.tarea_hh.IdTipoTarea = dbo.sis_tipo_tarea.IdTipoTarea LEFT OUTER JOIN
                      dbo.bodega ON dbo.tarea_hh.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                      dbo.sis_estado_tarea_hh ON dbo.tarea_hh.IdEstado = dbo.sis_estado_tarea_hh.IdEstado LEFT OUTER JOIN
                      dbo.sis_prioridad_tarea_hh ON dbo.tarea_hh.IdPrioridad = dbo.sis_prioridad_tarea_hh.IdPrioridad LEFT OUTER JOIN
                      dbo.bodega_muelles ON dbo.tarea_hh.IdMuelle = dbo.bodega_muelles.IdMuelle AND dbo.bodega.IdBodega = dbo.bodega_muelles.IdBodega LEFT OUTER JOIN
                      dbo.propietarios ON dbo.tarea_hh.IdPropietario = dbo.propietarios.IdPropietario
```
