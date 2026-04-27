---
id: db-brain-view-vw-producto
type: db-view
title: dbo.VW_Producto
schema: dbo
name: VW_Producto
kind: view
modify_date: 2025-05-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Producto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-06 |
| Columnas | 70 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProducto` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdClasificacion` | `int` | ✓ |  |
| 4 | `IdFamilia` | `int` | ✓ |  |
| 5 | `IdMarca` | `int` | ✓ |  |
| 6 | `IdTipoProducto` | `int` | ✓ |  |
| 7 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 8 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 9 | `Clasificación` | `nvarchar(50)` | ✓ |  |
| 10 | `Familia` | `nvarchar(50)` | ✓ |  |
| 11 | `Marca` | `nvarchar(50)` | ✓ |  |
| 12 | `Tipo Producto` | `nvarchar(50)` | ✓ |  |
| 13 | `Unidad Medida` | `nvarchar(50)` | ✓ |  |
| 14 | `Código` | `nvarchar(50)` | ✓ |  |
| 15 | `Código de Barra` | `nvarchar(35)` | ✓ |  |
| 16 | `Producto` | `nvarchar(100)` | ✓ |  |
| 17 | `Existencia Mínima` | `float` | ✓ |  |
| 18 | `Existencia Máxima` | `float` | ✓ |  |
| 19 | `costo` | `float` | ✓ |  |
| 20 | `precio` | `float` | ✓ |  |
| 21 | `activo` | `bit` | ✓ |  |
| 22 | `IdCamara` | `int` | ✓ |  |
| 23 | `IdTipoRotacion` | `int` | ✓ |  |
| 24 | `IdPerfilSerializado` | `int` | ✓ |  |
| 25 | `IdIndiceRotacion` | `int` | ✓ |  |
| 26 | `IdSimbologia` | `int` | ✓ |  |
| 27 | `IdArancel` | `int` | ✓ |  |
| 28 | `codigo` | `nvarchar(50)` | ✓ |  |
| 29 | `nombre` | `nvarchar(100)` | ✓ |  |
| 30 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 31 | `Expr8` | `float` | ✓ |  |
| 32 | `existencia_min` | `float` | ✓ |  |
| 33 | `existencia_max` | `float` | ✓ |  |
| 34 | `Expr9` | `float` | ✓ |  |
| 35 | `peso_referencia` | `float` | ✓ |  |
| 36 | `peso_tolerancia` | `float` | ✓ |  |
| 37 | `temperatura_referencia` | `float` | ✓ |  |
| 38 | `temperatura_tolerancia` | `float` | ✓ |  |
| 39 | `Expr10` | `bit` | ✓ |  |
| 40 | `serializado` | `bit` | ✓ |  |
| 41 | `genera_lote` | `bit` | ✓ |  |
| 42 | `control_vencimiento` | `bit` | ✓ |  |
| 43 | `control_lote` | `bit` | ✓ |  |
| 44 | `peso_recepcion` | `bit` | ✓ |  |
| 45 | `peso_despacho` | `bit` | ✓ |  |
| 46 | `temperatura_recepcion` | `bit` | ✓ |  |
| 47 | `temperatura_despacho` | `bit` | ✓ |  |
| 48 | `materia_prima` | `bit` | ✓ |  |
| 49 | `kit` | `bit` | ✓ |  |
| 50 | `tolerancia` | `int` | ✓ |  |
| 51 | `ciclo_vida` | `int` | ✓ |  |
| 52 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 53 | `fec_agr` | `datetime` | ✓ |  |
| 54 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 55 | `fec_mod` | `datetime` | ✓ |  |
| 56 | `imagen` | `image` | ✓ |  |
| 57 | `noserie` | `nvarchar(50)` | ✓ |  |
| 58 | `noparte` | `nvarchar(50)` | ✓ |  |
| 59 | `fechamanufactura` | `bit` | ✓ |  |
| 60 | `capturar_aniada` | `bit` | ✓ |  |
| 61 | `control_peso` | `bit` | ✓ |  |
| 62 | `captura_arancel` | `bit` | ✓ |  |
| 63 | `es_hardware` | `bit` | ✓ |  |
| 64 | `IDPRODUCTOPARAMETROA` | `int` | ✓ |  |
| 65 | `IDPRODUCTOPARAMETROB` | `int` | ✓ |  |
| 66 | `producto_parametro_codigoA` | `nvarchar(50)` | ✓ |  |
| 67 | `producto_parametro_nombreA` | `nvarchar(50)` | ✓ |  |
| 68 | `producto_parametro_codigoB` | `nvarchar(50)` | ✓ |  |
| 69 | `producto_parametro_nombreB` | `nvarchar(50)` | ✓ |  |
| 70 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |

## Consume

- `indice_rotacion`
- `producto`
- `producto_clasificacion`
- `producto_familia`
- `producto_marca`
- `producto_parametro_a`
- `producto_parametro_b`
- `producto_tipo`
- `propietarios`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Producto]
AS
SELECT pd.IdProducto, pd.IdPropietario, pd.IdClasificacion, pd.IdFamilia, pd.IdMarca, pd.IdTipoProducto, pd.IdUnidadMedidaBasica, pp.nombre_comercial AS Propietario, pc.nombre AS Clasificación, pf.nombre AS Familia, pm.nombre AS Marca, 
                  pt.NombreTipoProducto AS [Tipo Producto], u.Nombre AS [Unidad Medida], pd.codigo AS Código, pd.codigo_barra AS [Código de Barra], pd.nombre AS Producto, pd.existencia_min AS [Existencia Mínima], 
                  pd.existencia_max AS [Existencia Máxima], pd.costo, pd.precio, pd.activo, pd.IdCamara, pd.IdTipoRotacion, pd.IdPerfilSerializado, pd.IdIndiceRotacion, pd.IdSimbologia, pd.IdArancel, pd.codigo, pd.nombre, pd.codigo_barra, 
                  pd.precio AS Expr8, pd.existencia_min, pd.existencia_max, pd.costo AS Expr9, pd.peso_referencia, pd.peso_tolerancia, pd.temperatura_referencia, pd.temperatura_tolerancia, pd.activo AS Expr10, pd.serializado, pd.genera_lote, 
                  pd.control_vencimiento, pd.control_lote, pd.peso_recepcion, pd.peso_despacho, pd.temperatura_recepcion, pd.temperatura_despacho, pd.materia_prima, pd.kit, pd.tolerancia, pd.ciclo_vida, pd.user_agr, pd.fec_agr, pd.user_mod, 
                  pd.fec_mod, pd.imagen, pd.noserie, pd.noparte, pd.fechamanufactura, pd.capturar_aniada, pd.control_peso, pd.captura_arancel, pd.es_hardware, pd.IDPRODUCTOPARAMETROA, pd.IDPRODUCTOPARAMETROB, 
                  dbo.producto_parametro_a.Codigo AS producto_parametro_codigoA, dbo.producto_parametro_a.Nombre AS producto_parametro_nombreA, dbo.producto_parametro_b.Codigo AS producto_parametro_codigoB, 
                  dbo.producto_parametro_b.Nombre AS producto_parametro_nombreB, dbo.indice_rotacion.Descripcion AS IndiceRotacion
FROM     dbo.producto AS pd LEFT OUTER JOIN
                  dbo.indice_rotacion ON pd.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion LEFT OUTER JOIN
                  dbo.producto_parametro_a ON pd.IDPRODUCTOPARAMETROA = dbo.producto_parametro_a.IdProductoParametroA LEFT OUTER JOIN
                  dbo.producto_parametro_b ON pd.IDPRODUCTOPARAMETROB = dbo.producto_parametro_b.IdProductoParametroB LEFT OUTER JOIN
                  dbo.propietarios AS pp ON pd.IdPropietario = pp.IdPropietario LEFT OUTER JOIN
                  dbo.producto_clasificacion AS pc ON pd.IdClasificacion = pc.IdClasificacion LEFT OUTER JOIN
                  dbo.producto_familia AS pf ON pd.IdFamilia = pf.IdFamilia LEFT OUTER JOIN
                  dbo.producto_marca AS pm ON pd.IdMarca = pm.IdMarca LEFT OUTER JOIN
                  dbo.producto_tipo AS pt ON pd.IdTipoProducto = pt.IdTipoProducto LEFT OUTER JOIN
                  dbo.unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida and u.IdPropietario=pd.IdPropietario
```
