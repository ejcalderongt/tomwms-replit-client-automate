---
id: db-brain-view-vw-productooc
type: db-view
title: dbo.VW_ProductoOC
schema: dbo
name: VW_ProductoOC
kind: view
modify_date: 2018-06-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoOC`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-06-16 |
| Columnas | 62 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBodega` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `IdClasificacion` | `int` | ✓ |  |
| 5 | `IdFamilia` | `int` | ✓ |  |
| 6 | `IdMarca` | `int` | ✓ |  |
| 7 | `IdTipoProducto` | `int` | ✓ |  |
| 8 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 9 | `IdBodega` | `int` | ✓ |  |
| 10 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 11 | `Clasificación` | `nvarchar(50)` | ✓ |  |
| 12 | `Familia` | `nvarchar(50)` | ✓ |  |
| 13 | `Marca` | `nvarchar(50)` | ✓ |  |
| 14 | `Tipo Producto` | `nvarchar(50)` | ✓ |  |
| 15 | `Unidad Medida` | `nvarchar(50)` | ✓ |  |
| 16 | `codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 18 | `nombre` | `nvarchar(100)` | ✓ |  |
| 19 | `existencia_min` | `float` | ✓ |  |
| 20 | `existencia_max` | `float` | ✓ |  |
| 21 | `costo` | `float` | ✓ |  |
| 22 | `precio` | `float` | ✓ |  |
| 23 | `activo` | `bit` | ✓ |  |
| 24 | `IdPropietarioBodega` | `int` | ✓ |  |
| 25 | `activoproductobodega` | `bit` | ✓ |  |
| 26 | `IdCamara` | `int` | ✓ |  |
| 27 | `IdTipoRotacion` | `int` | ✓ |  |
| 28 | `IdPerfilSerializado` | `int` | ✓ |  |
| 29 | `IdIndiceRotacion` | `int` | ✓ |  |
| 30 | `IdSimbologia` | `int` | ✓ |  |
| 31 | `IdArancel` | `int` | ✓ |  |
| 32 | `peso_referencia` | `float` | ✓ |  |
| 33 | `peso_tolerancia` | `float` | ✓ |  |
| 34 | `temperatura_referencia` | `float` | ✓ |  |
| 35 | `temperatura_tolerancia` | `float` | ✓ |  |
| 36 | `serializado` | `bit` | ✓ |  |
| 37 | `genera_lote` | `bit` | ✓ |  |
| 38 | `genera_lp_old` | `bit` | ✓ |  |
| 39 | `control_vencimiento` | `bit` | ✓ |  |
| 40 | `control_lote` | `bit` | ✓ |  |
| 41 | `peso_recepcion` | `bit` | ✓ |  |
| 42 | `peso_despacho` | `bit` | ✓ |  |
| 43 | `temperatura_recepcion` | `bit` | ✓ |  |
| 44 | `temperatura_despacho` | `bit` | ✓ |  |
| 45 | `materia_prima` | `bit` | ✓ |  |
| 46 | `kit` | `bit` | ✓ |  |
| 47 | `tolerancia` | `int` | ✓ |  |
| 48 | `ciclo_vida` | `int` | ✓ |  |
| 49 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 50 | `fec_agr` | `datetime` | ✓ |  |
| 51 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 52 | `fec_mod` | `datetime` | ✓ |  |
| 53 | `noserie` | `nvarchar(50)` | ✓ |  |
| 54 | `noparte` | `nvarchar(50)` | ✓ |  |
| 55 | `fechamanufactura` | `bit` | ✓ |  |
| 56 | `captura_arancel` | `bit` | ✓ |  |
| 57 | `largo` | `float` | ✓ |  |
| 58 | `es_hardware` | `bit` | ✓ |  |
| 59 | `alto` | `float` | ✓ |  |
| 60 | `ancho` | `float` | ✓ |  |
| 61 | `control_peso` | `bit` | ✓ |  |
| 62 | `capturar_aniada` | `bit` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_familia`
- `producto_marca`
- `producto_tipo`
- `propietario_bodega`
- `propietarios`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoOC]
AS
SELECT     prb.IdProductoBodega, pd.IdProducto, pd.IdPropietario, pd.IdClasificacion, pd.IdFamilia, pd.IdMarca, pd.IdTipoProducto, pd.IdUnidadMedidaBasica, prb.IdBodega, 
                      pp.nombre_comercial AS Propietario, pc.nombre AS Clasificación, pf.nombre AS Familia, pm.nombre AS Marca, pt.NombreTipoProducto AS [Tipo Producto], 
                      u.Nombre AS [Unidad Medida], pd.codigo, pd.codigo_barra, pd.nombre, pd.existencia_min, 
                      pd.existencia_max, pd.costo, pd.precio, pd.activo, ppb.IdPropietarioBodega, prb.activo AS activoproductobodega, pd.IdCamara, 
                      pd.IdTipoRotacion, pd.IdPerfilSerializado, pd.IdIndiceRotacion, pd.IdSimbologia, pd.IdArancel, pd.peso_referencia, pd.peso_tolerancia, pd.temperatura_referencia, 
                      pd.temperatura_tolerancia, pd.serializado, pd.genera_lote, pd.genera_lp_old, pd.control_vencimiento, pd.control_lote, pd.peso_recepcion, pd.peso_despacho, 
                      pd.temperatura_recepcion, pd.temperatura_despacho, pd.materia_prima, pd.kit, pd.tolerancia, pd.ciclo_vida, pd.user_agr, pd.fec_agr, pd.user_mod, pd.fec_mod, 
                      pd.noserie, pd.noparte, pd.fechamanufactura, pd.captura_arancel, pd.largo, pd.es_hardware, pd.alto, pd.ancho, pd.control_peso, pd.capturar_aniada
FROM         dbo.producto AS pd LEFT OUTER JOIN
                      dbo.propietarios AS pp ON pd.IdPropietario = pp.IdPropietario LEFT OUTER JOIN
                      dbo.propietario_bodega AS ppb ON pd.IdPropietario = ppb.IdPropietario LEFT OUTER JOIN
                      dbo.producto_clasificacion AS pc ON pd.IdClasificacion = pc.IdClasificacion LEFT OUTER JOIN
                      dbo.producto_familia AS pf ON pd.IdFamilia = pf.IdFamilia LEFT OUTER JOIN
                      dbo.producto_marca AS pm ON pd.IdMarca = pm.IdMarca LEFT OUTER JOIN
                      dbo.producto_tipo AS pt ON pd.IdTipoProducto = pt.IdTipoProducto LEFT OUTER JOIN
                      dbo.unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida INNER JOIN
                      dbo.producto_bodega AS prb ON pd.IdProducto = prb.IdProducto AND ppb.IdBodega = prb.IdBodega
```
