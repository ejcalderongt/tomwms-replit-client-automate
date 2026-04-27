---
id: db-brain-view-vw-ubicaciones-por-regla
type: db-view
title: dbo.vw_ubicaciones_por_regla
schema: dbo
name: vw_ubicaciones_por_regla
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.vw_ubicaciones_por_regla`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 27 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicacionEnc` | `int` |  |  |
| 2 | `IdUbicacion` | `int` |  |  |
| 3 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `ancho` | `float` | ✓ |  |
| 5 | `largo` | `float` | ✓ |  |
| 6 | `alto` | `float` | ✓ |  |
| 7 | `IdTramo` | `int` |  |  |
| 8 | `indice_x` | `int` | ✓ |  |
| 9 | `nivel` | `int` | ✓ |  |
| 10 | `IdIndiceRotacion` | `int` | ✓ |  |
| 11 | `IdTipoRotacion` | `int` | ✓ |  |
| 12 | `dañado` | `bit` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `bloqueada` | `bit` | ✓ |  |
| 15 | `acepta_pallet` | `bit` | ✓ |  |
| 16 | `IdBodega` | `int` |  |  |
| 17 | `IdPropietarioBodega` | `int` | ✓ |  |
| 18 | `regla_ubic_det_prop_Activo` | `bit` | ✓ |  |
| 19 | `IdPropietario` | `int` | ✓ |  |
| 20 | `IdIndiceRotacionRegla` | `int` | ✓ |  |
| 21 | `IdTipoRotacionRegla` | `int` | ✓ |  |
| 22 | `IdTipoProducto` | `int` | ✓ |  |
| 23 | `regla_ubic_det_tp_Activo` | `bit` | ✓ |  |
| 24 | `IdEstado` | `int` | ✓ |  |
| 25 | `regla_ubic_det_pe_Activo` | `bit` | ✓ |  |
| 26 | `IdPresentacion` | `int` | ✓ |  |
| 27 | `Nombre_Completo` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `propietario_bodega`
- `regla_ubic_det_ir`
- `regla_ubic_det_pe`
- `regla_ubic_det_pp`
- `regla_ubic_det_prop`
- `regla_ubic_det_tp`
- `regla_ubic_det_tr`
- `regla_ubic_enc`
- `regla_ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[vw_ubicaciones_por_regla]
AS
SELECT regla_ubic_enc.IdReglaUbicacionEnc, bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion, bodega_ubicacion.ancho, bodega_ubicacion.largo, bodega_ubicacion.alto, 
bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.IdIndiceRotacion, bodega_ubicacion.IdTipoRotacion, bodega_ubicacion.dañado, 
bodega_ubicacion.activo, bodega_ubicacion.bloqueada, bodega_ubicacion.acepta_pallet, bodega_area.IdBodega, regla_ubic_det_prop.IdPropietarioBodega, 
regla_ubic_det_prop.Activo AS regla_ubic_det_prop_Activo, propietario_bodega.IdPropietario, regla_ubic_det_ir.IdIndiceRotacion AS IdIndiceRotacionRegla, 
regla_ubic_det_tr.IdTipoRotacion AS IdTipoRotacionRegla, regla_ubic_det_tp.IdTipoProducto, regla_ubic_det_tp.Activo AS regla_ubic_det_tp_Activo, regla_ubic_det_pe.IdEstado, 
regla_ubic_det_pe.Activo AS regla_ubic_det_pe_Activo, regla_ubic_det_pp.IdPresentacion, dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion,bodega_ubicacion.IdBodega) as Nombre_Completo
FROM regla_ubic_det_tp INNER JOIN
bodega_ubicacion INNER JOIN
bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND 
bodega_ubicacion.IdBodega = bodega_tramo.IdBodega INNER JOIN
bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector AND bodega_tramo.IdArea = bodega_sector.IdArea AND bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN
bodega_area ON bodega_sector.IdArea = bodega_area.IdArea AND bodega_sector.IdBodega = bodega_area.IdBodega INNER JOIN
regla_ubicacion ON bodega_ubicacion.IdUbicacion = regla_ubicacion.IdUbicacion AND 
                   bodega_ubicacion.IdBodega = regla_ubicacion.IdBodega INNER JOIN
regla_ubic_enc ON regla_ubicacion.IdReglaUbicacionEnc = regla_ubic_enc.IdReglaUbicacionEnc AND
                  regla_ubic_enc.Activo = 1
               ON regla_ubic_det_tp.IdReglaUbicacionEnc = regla_ubic_enc.IdReglaUbicacionEnc  AND
                  regla_ubic_det_tp.Activo = 1  LEFT OUTER JOIN
regla_ubic_det_pp ON regla_ubic_enc.IdReglaUbicacionEnc = regla_ubic_det_pp.IdReglaUbicacionEnc AND
                     regla_ubic_det_pp.Activo = 1  LEFT OUTER JOIN
regla_ubic_det_pe ON regla_ubic_enc.IdReglaUbicacionEnc = regla_ubic_det_pe.IdReglaUbicacionEnc AND
                     regla_ubic_det_pe.Activo = 1   LEFT OUTER JOIN
propietario_bodega INNER JOIN
regla_ubic_det_prop ON propietario_bodega.IdPropietarioBodega = regla_ubic_det_prop.IdPropietarioBodega  AND
                     regla_ubic_det_prop.Activo = 1   ON 
regla_ubic_enc.IdReglaUbicacionEnc = regla_ubic_det_prop.IdReglaUbicacionEnc LEFT OUTER JOIN
regla_ubic_det_tr ON regla_ubic_enc.IdReglaUbicacionEnc = regla_ubic_det_tr.IdReglaUbicacionEnc AND 
                     bodega_ubicacion.IdTipoRotacion = regla_ubic_det_tr.IdTipoRotacion AND
                     regla_ubic_det_tr.Activo = 1   LEFT OUTER JOIN
regla_ubic_det_ir ON regla_ubic_enc.IdReglaUbicacionEnc = regla_ubic_det_ir.IdReglaUbicacionEnc AND 
                     bodega_ubicacion.IdIndiceRotacion = regla_ubic_det_ir.IdIndiceRotacion AND
                     regla_ubic_det_ir.Activo = 1
```
