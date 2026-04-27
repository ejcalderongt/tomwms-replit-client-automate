---
id: db-brain-view-vw-ubicaciones-inventario-ciclico
type: db-view
title: dbo.VW_Ubicaciones_Inventario_Ciclico
schema: dbo
name: VW_Ubicaciones_Inventario_Ciclico
kind: view
modify_date: 2021-12-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ubicaciones_Inventario_Ciclico`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-12-01 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `IdArea` | `int` |  |  |
| 4 | `Area` | `nvarchar(50)` | ✓ |  |
| 5 | `IdSector` | `int` |  |  |
| 6 | `Sector` | `nvarchar(50)` | ✓ |  |
| 7 | `IdTramo` | `int` |  |  |
| 8 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 9 | `IdUbicacion` | `int` |  |  |
| 10 | `idoperador` | `int` | ✓ |  |
| 11 | `Operador` | `nvarchar(100)` | ✓ |  |
| 12 | `Ubicacion` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `trans_inv_ciclico`
- `trans_inv_ciclico_ubic`
- `trans_inv_operador`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ubicaciones_Inventario_Ciclico]
AS
SELECT  dbo.bodega_area.IdBodega, dbo.trans_inv_ciclico_ubic.idinventarioenc, dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion AS Sector, 
                         dbo.bodega_tramo.IdTramo, dbo.bodega_tramo.descripcion AS Tramo, dbo.bodega_ubicacion.IdUbicacion, dbo.trans_inv_operador.idoperador, dbo.operador.nombres AS Operador, 
                         dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Ubicacion
FROM            dbo.operador INNER JOIN
                         dbo.trans_inv_operador ON dbo.operador.IdOperador = dbo.trans_inv_operador.idoperador RIGHT OUTER JOIN
                         dbo.bodega_area INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdArea = dbo.bodega_tramo.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                         dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                         dbo.trans_inv_ciclico_ubic ON dbo.bodega_ubicacion.IdUbicacion = dbo.trans_inv_ciclico_ubic.IdUbicacion AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_ciclico_ubic.IdBodega ON 
                         dbo.trans_inv_operador.idinventarioenc = dbo.trans_inv_ciclico_ubic.idinventarioenc AND dbo.trans_inv_operador.idubic = dbo.trans_inv_ciclico_ubic.IdUbicacion
GROUP BY dbo.bodega_area.IdBodega, dbo.trans_inv_ciclico_ubic.idinventarioenc, dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion, dbo.bodega_tramo.IdTramo, 
                         dbo.bodega_tramo.descripcion, dbo.bodega_ubicacion.IdUbicacion, dbo.trans_inv_operador.idoperador, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos,
						 dbo.operador.nombres, dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.IdBodega
UNION
SELECT dbo.bodega_area.IdBodega, dbo.trans_inv_ciclico.idinventarioenc, dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion AS Sector, 
                         dbo.bodega_tramo.IdTramo, dbo.bodega_tramo.descripcion AS Tramo, dbo.bodega_ubicacion.IdUbicacion, dbo.trans_inv_operador.idoperador, dbo.operador.nombres AS Operador, 
                         dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Ubicacion
FROM            dbo.operador INNER JOIN
                         dbo.trans_inv_operador ON dbo.operador.IdOperador = dbo.trans_inv_operador.idoperador RIGHT OUTER JOIN
                         dbo.bodega_area INNER JOIN
                         dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdArea = dbo.bodega_tramo.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                         dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                         dbo.trans_inv_ciclico ON dbo.bodega_ubicacion.IdUbicacion = dbo.trans_inv_ciclico.IdUbicacion AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_ciclico.IdBodega ON 
                         dbo.trans_inv_operador.idinventarioenc = dbo.trans_inv_ciclico.idinventarioenc AND dbo.trans_inv_operador.idubic = dbo.trans_inv_ciclico.IdUbicacion
GROUP BY dbo.bodega_area.IdBodega, dbo.trans_inv_ciclico.idinventarioenc, dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion, dbo.bodega_tramo.IdTramo, 
                         dbo.bodega_tramo.descripcion, dbo.bodega_ubicacion.IdUbicacion, dbo.trans_inv_operador.idoperador, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, 
                         dbo.operador.nombres, dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.IdBodega
```
