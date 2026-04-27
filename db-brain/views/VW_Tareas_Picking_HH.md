---
id: db-brain-view-vw-tareas-picking-hh
type: db-view
title: dbo.VW_Tareas_Picking_HH
schema: dbo
name: VW_Tareas_Picking_HH
kind: view
modify_date: 2025-05-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tareas_Picking_HH`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-06 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `IdUbicacionPicking` | `int` | ✓ |  |
| 5 | `fecha_picking` | `datetime` | ✓ |  |
| 6 | `hora_ini` | `datetime` | ✓ |  |
| 7 | `hora_fin` | `datetime` | ✓ |  |
| 8 | `estado` | `nvarchar(20)` | ✓ |  |
| 9 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `detalle_operador` | `bit` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `NombreBodega` | `nvarchar(50)` | ✓ |  |
| 16 | `nombre_comercial` | `nvarchar(100)` |  |  |
| 17 | `NombreUbicacion` | `nvarchar(200)` | ✓ |  |
| 18 | `IdOperadorBodega` | `int` |  |  |
| 19 | `referencia` | `varchar(50)` | ✓ |  |
| 20 | `IdBodegaMuelle` | `int` | ✓ |  |
| 21 | `Tiene_Manufactura` | `int` |  |  |
| 22 | `IdPrioridadPicking` | `int` | ✓ |  |
| 23 | `NombrePrioridad` | `nvarchar(50)` | ✓ |  |
| 24 | `NombreMuelle` | `nvarchar(50)` | ✓ |  |
| 25 | `IdUbicacionDefecto` | `int` |  |  |
| 26 | `codigo_barra_muelle` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_muelles`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `propietario_bodega`
- `propietarios`
- `trans_pe_enc`
- `trans_picking_enc`
- `trans_picking_op`
- `trans_picking_prioridad`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Tareas_Picking_HH] as
SELECT p.IdPickingEnc, p.IdBodega, p.IdPropietarioBodega, p.IdUbicacionPicking, p.fecha_picking, p.hora_ini, p.hora_fin, p.estado, 
p.user_agr, p.fec_agr, p.user_mod, p.fec_mod, p.detalle_operador, p.activo, b.nombre AS NombreBodega,
pp.nombre_comercial, CASE WHEN bodega_tramo.es_rack = 1 THEN dbo.Nombre_Completo_Ubicacion(p.IdUbicacionPicking,p.IdBodega)
ELSE bodega_tramo.descripcion END AS NombreUbicacion, o.IdOperadorBodega,p.referencia, p.IdBodegaMuelle,
CASE WHEN TieneManufactura>0 THEN 1 ELSE 0 END Tiene_Manufactura, p.IdPrioridadPicking, dbo.trans_picking_prioridad.Nombre AS NombrePrioridad, bm.nombre as NombreMuelle,
isnull(bm.IdUbicacionDefecto,0)IdUbicacionDefecto, bm.codigo_barra as codigo_barra_muelle
FROM dbo.trans_picking_enc AS p INNER JOIN
(select DISTINCT IdPickingEnc, IdOperadorBodega FROM dbo.trans_picking_op) AS o ON p.IdPickingEnc = o.IdPickingEnc INNER JOIN
dbo.bodega AS b ON p.IdBodega = b.IdBodega INNER JOIN
dbo.propietario_bodega AS pb ON pb.IdPropietarioBodega = p.IdPropietarioBodega AND b.IdBodega = pb.IdBodega INNER JOIN
dbo.propietarios AS pp ON pp.IdPropietario = pb.IdPropietario INNER JOIN
dbo.bodega_area ON b.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector INNER JOIN
dbo.bodega_ubicacion ON p.IdUbicacionPicking = dbo.bodega_ubicacion.IdUbicacion AND dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND
p.IdBodega = dbo.bodega_ubicacion.IdBodega	LEFT OUTER JOIN
(select IdPickingEnc, SUM(IdTipoManufactura) TieneManufactura from trans_pe_enc where IdTipoManufactura <>0 GROUP BY IdPickingEnc ) AS T ON p.IdPickingEnc = t.IdPickingEnc LEFT JOIN
dbo.trans_picking_prioridad ON p.IdPrioridadPicking = dbo.trans_picking_prioridad.IdPrioridadPicking LEFT JOIN
dbo.bodega_muelles AS bm ON bm.IdMuelle = p.IdBodegaMuelle
```
