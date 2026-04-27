---
id: db-brain-view-vw-navdetalleconfiguracion
type: db-view
title: dbo.VW_navdetalleconfiguracion
schema: dbo
name: VW_navdetalleconfiguracion
kind: view
modify_date: 2017-09-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_navdetalleconfiguracion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-09-25 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `dia` | `int` | ✓ |  |
| 3 | `horainicio` | `datetime` | ✓ |  |
| 4 | `frecuencia` | `int` | ✓ |  |
| 5 | `Entidad` | `varchar(256)` | ✓ |  |
| 6 | `idnavconfigenc` | `int` |  |  |
| 7 | `nombreDia` | `varchar(9)` |  |  |
| 8 | `horafin` | `datetime` | ✓ |  |

## Consume

- `i_nav_config_det`
- `i_nav_ent`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_navdetalleconfiguracion
AS
SELECT        dbo.i_nav_config_det.idnavconfigdet AS Correlativo, dbo.i_nav_config_det.dia, dbo.i_nav_config_det.horainicio, dbo.i_nav_config_det.frecuencia, dbo.i_nav_ent.nombre AS Entidad, 
                         dbo.i_nav_config_det.idnavconfigenc, 
                         CASE dia WHEN 1 THEN 'Lunes' WHEN 2 THEN 'Martes' WHEN 3 THEN 'Miercoles' WHEN 4 THEN 'Jueves' WHEN 5 THEN 'Viernes' WHEN 6 THEN 'Sabado' ELSE 'Domingo' END AS nombreDia, 
                         dbo.i_nav_config_det.horafin
FROM            dbo.i_nav_config_det INNER JOIN
                         dbo.i_nav_ent ON dbo.i_nav_config_det.idnavent = dbo.i_nav_ent.idnavent
```
