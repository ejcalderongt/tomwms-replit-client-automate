---
id: db-brain-view-vw-productorellenado
type: db-view
title: dbo.VW_ProductoRellenado
schema: dbo
name: VW_ProductoRellenado
kind: view
modify_date: 2021-11-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoRellenado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-19 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `IdProducto` | `int` | ✓ |  |
| 4 | `Ubicación` | `nvarchar(200)` | ✓ |  |
| 5 | `Estado` | `nvarchar(50)` | ✓ |  |
| 6 | `IdRellenado` | `int` |  |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |
| 10 | `IdUbicacion` | `int` | ✓ |  |
| 11 | `IdTipoAccion` | `int` | ✓ |  |
| 12 | `Minimo` | `float` | ✓ |  |
| 13 | `Maximo` | `float` | ✓ |  |
| 14 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `Activo` | `bit` | ✓ |  |
| 19 | `IdUmBasAbastercerCon` | `int` | ✓ |  |
| 20 | `IdPresentacionAbastercerCon` | `int` | ✓ |  |
| 21 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 22 | `NomPresentacionRellenarCon` | `nvarchar(50)` | ✓ |  |
| 23 | `NomUmBasAbastecerCon` | `nvarchar(50)` | ✓ |  |
| 24 | `IdPropietario` | `int` | ✓ |  |
| 25 | `IdOperadorDefecto` | `int` |  |  |
| 26 | `NomOperador` | `nvarchar(201)` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `producto_rellenado`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoRellenado]
AS
SELECT        pr.IdBodega, pr.IdProductoBodega, dbo.producto_bodega.IdProducto, dbo.Nombre_Completo_Ubicacion(bu.IdUbicacion, bu.IdBodega) AS Ubicación, pe.nombre AS Estado, pr.IdRellenado, pr.IdPresentacion, 
                         pp.nombre AS Presentación, pr.IdProductoEstado, pr.IdUbicacion, pr.IdTipoAccion, pr.Minimo, pr.Maximo, pr.user_agr, pr.fec_agr, pr.user_mod, pr.fec_mod, pr.Activo, pr.IdUmBasAbastercerCon, pr.IdPresentacionAbastercerCon, 
                         pr.IdUnidadMedidaBasica, pp1.nombre AS NomPresentacionRellenarCon, um1.Nombre AS NomUmBasAbastecerCon, pr.IdPropietario, pr.IdOperadorDefecto, op.nombres + ' ' + op.apellidos as NomOperador 
FROM            dbo.producto_rellenado AS pr INNER JOIN
                         dbo.bodega_ubicacion AS bu ON pr.IdUbicacion = bu.IdUbicacion AND pr.IdBodega = bu.IdBodega INNER JOIN
                         dbo.producto_estado AS pe ON pr.IdProductoEstado = pe.IdEstado  INNER JOIN
                         dbo.producto_bodega ON pr.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND pr.IdBodega = dbo.producto_bodega.IdBodega LEFT OUTER JOIN
						 dbo.operador AS op ON op.IdOperador = pr.IdOperadorDefecto LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp1 ON pr.IdPresentacionAbastercerCon = pp1.IdPresentacion LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON pr.IdPresentacion = pp.IdPresentacion LEFT OUTER JOIN
                         dbo.unidad_medida AS um1 ON pr.IdUmBasAbastercerCon = um1.IdUnidadMedida
```
