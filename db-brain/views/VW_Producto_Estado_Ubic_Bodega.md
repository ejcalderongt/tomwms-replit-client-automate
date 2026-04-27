---
id: db-brain-view-vw-producto-estado-ubic-bodega
type: db-view
title: dbo.VW_Producto_Estado_Ubic_Bodega
schema: dbo
name: VW_Producto_Estado_Ubic_Bodega
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Producto_Estado_Ubic_Bodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 18 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEstado` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 5 | `utilizable` | `bit` | ✓ |  |
| 6 | `activo` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `dañado` | `bit` | ✓ |  |
| 12 | `codigo_bodega_erp` | `nvarchar(50)` | ✓ |  |
| 13 | `sistema` | `bit` | ✓ |  |
| 14 | `IdUbicacionBodegaDefecto` | `int` | ✓ |  |
| 15 | `IdBodega` | `int` | ✓ |  |
| 16 | `NombreUbic` | `nvarchar(200)` | ✓ |  |
| 17 | `dias_vencimiento_clasificacion` | `int` |  |  |
| 18 | `tolerancia_dias_vencimiento` | `int` |  |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto_estado`
- `producto_estado_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [VW_Producto_Estado_Ubic_Bodega]
AS
SELECT producto_estado.IdEstado, producto_estado.IdPropietario, producto_estado.nombre, producto_estado.IdUbicacionDefecto, producto_estado.utilizable, producto_estado.activo,
       producto_estado.user_agr, producto_estado.fec_agr, producto_estado.user_mod, producto_estado.fec_mod, producto_estado.dañado, producto_estado.codigo_bodega_erp,
       producto_estado.sistema, producto_estado_ubic.IdUbicacionDefecto AS IdUbicacionBodegaDefecto, bodega.IdBodega, dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion,
       bodega_ubicacion.IdBodega) AS NombreUbic, producto_estado.dias_vencimiento_clasificacion, producto_estado.tolerancia_dias_vencimiento
FROM bodega_ubicacion
INNER JOIN producto_estado_ubic ON bodega_ubicacion.IdUbicacion = producto_estado_ubic.IdUbicacionDefecto AND bodega_ubicacion.IdBodega = producto_estado_ubic.IdBodega
INNER JOIN bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
INNER JOIN bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector AND bodega_tramo.IdBodega = bodega_sector.IdBodega
INNER JOIN bodega_area ON bodega_sector.IdArea = bodega_area.IdArea AND bodega_sector.IdBodega = bodega_area.IdBodega
INNER JOIN bodega ON bodega_area.IdBodega = bodega.IdBodega
RIGHT OUTER JOIN producto_estado ON producto_estado_ubic.IdEstado = producto_estado.IdEstado
GROUP BY producto_estado.IdEstado, producto_estado.IdPropietario, producto_estado.nombre, producto_estado.IdUbicacionDefecto, producto_estado.user_agr, producto_estado.fec_agr,
         producto_estado.user_mod, producto_estado.fec_mod, producto_estado.codigo_bodega_erp, producto_estado_ubic.IdUbicacionDefecto, bodega.IdBodega,
         bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega, producto_estado.utilizable, producto_estado.activo, 
         producto_estado.dañado, producto_estado.sistema, producto_estado.dias_vencimiento_clasificacion, producto_estado.tolerancia_dias_vencimiento
```
