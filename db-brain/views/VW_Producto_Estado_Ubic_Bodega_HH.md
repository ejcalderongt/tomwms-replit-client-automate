---
id: db-brain-view-vw-producto-estado-ubic-bodega-hh
type: db-view
title: dbo.VW_Producto_Estado_Ubic_Bodega_HH
schema: dbo
name: VW_Producto_Estado_Ubic_Bodega_HH
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Producto_Estado_Ubic_Bodega_HH`

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
| 13 | `Sistema` | `bit` |  |  |
| 14 | `IdUbicacionBodegaDefecto` | `int` |  |  |
| 15 | `IdBodega` | `int` |  |  |
| 16 | `NombreUbic` | `nvarchar(200)` | ✓ |  |
| 17 | `dias_vencimiento_clasificacion` | `int` |  |  |
| 18 | `tolerancia_dias_vencimiento` | `int` |  |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto_estado`
- `producto_estado_ubic`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Producto_Estado_Ubic_Bodega_HH]
AS
SELECT e.IdEstado, e.IdPropietario, e.nombre, e.IdUbicacionDefecto, e.utilizable, e.activo, e.user_agr, e.fec_agr, e.user_mod, e.fec_mod,
       e.dañado, e.codigo_bodega_erp, ISNULL(e.sistema,0) Sistema, 
	   ISNULL(pe.IdUbicacionDefecto, 0) AS IdUbicacionBodegaDefecto, 
       ISNULL(pe.IdBodega, 0) AS IdBodega, 
	   CASE WHEN ISNULL(pe.IdUbicacionDefecto, 0) = 0 THEN 'N/D' ELSE dbo.Nombre_Completo_Ubicacion(pe.IdUbicacionDefecto, pe.IdBodega) 
       END AS NombreUbic, e.dias_vencimiento_clasificacion,e.tolerancia_dias_vencimiento
FROM            dbo.producto_estado AS e INNER JOIN
                         dbo.propietarios AS p ON e.IdPropietario = p.IdPropietario LEFT OUTER JOIN
                         (SELECT * 
						  FROM dbo.producto_estado_ubic 
						  WHERE EXISTS (SELECT bu.IdUbicacion 
						                FROM bodega_ubicacion bu 
										WHERE bu.IdUbicacion = producto_estado_ubic.IdUbicacionDefecto AND
						                      bu.IdBodega = producto_estado_ubic.IdBodega )) AS pe  ON pe.IdEstado = e.IdEstado
```
