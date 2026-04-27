---
id: db-brain-view-vw-productosi
type: db-view
title: dbo.VW_ProductoSI
schema: dbo
name: VW_ProductoSI
kind: view
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoSI`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-05 |
| Columnas | 69 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `IdClasificacion` | `int` | ✓ |  |
| 6 | `IdFamilia` | `int` | ✓ |  |
| 7 | `IdMarca` | `int` | ✓ |  |
| 8 | `IdTipoProducto` | `int` | ✓ |  |
| 9 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 10 | `IdCamara` | `int` | ✓ |  |
| 11 | `IdTipoRotacion` | `int` | ✓ |  |
| 12 | `IdPerfilSerializado` | `int` | ✓ |  |
| 13 | `IdIndiceRotacion` | `int` | ✓ |  |
| 14 | `IdSimbologia` | `int` | ✓ |  |
| 15 | `IdArancel` | `int` | ✓ |  |
| 16 | `codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `nombre` | `nvarchar(100)` | ✓ |  |
| 18 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 19 | `precio` | `float` | ✓ |  |
| 20 | `existencia_min` | `float` | ✓ |  |
| 21 | `existencia_max` | `float` | ✓ |  |
| 22 | `costo` | `float` | ✓ |  |
| 23 | `peso_referencia` | `float` | ✓ |  |
| 24 | `peso_tolerancia` | `float` | ✓ |  |
| 25 | `noparte` | `nvarchar(50)` | ✓ |  |
| 26 | `noserie` | `nvarchar(50)` | ✓ |  |
| 27 | `control_peso` | `bit` | ✓ |  |
| 28 | `ciclo_vida` | `int` | ✓ |  |
| 29 | `tolerancia` | `int` | ✓ |  |
| 30 | `kit` | `bit` | ✓ |  |
| 31 | `materia_prima` | `bit` | ✓ |  |
| 32 | `control_lote` | `bit` | ✓ |  |
| 33 | `control_vencimiento` | `bit` | ✓ |  |
| 34 | `genera_lote` | `bit` | ✓ |  |
| 35 | `serializado` | `bit` | ✓ |  |
| 36 | `codigo_barra_presentacion` | `nvarchar(50)` | ✓ |  |
| 37 | `codigo_barra_pcb` | `nvarchar(35)` | ✓ |  |
| 38 | `NomPresentacion` | `nvarchar(50)` | ✓ |  |
| 39 | `activopp` | `bit` | ✓ |  |
| 40 | `IdPresentacion` | `int` | ✓ |  |
| 41 | `factor` | `float` | ✓ |  |
| 42 | `peso_recepcion` | `bit` | ✓ |  |
| 43 | `peso_despacho` | `bit` | ✓ |  |
| 44 | `temperatura_referencia` | `float` | ✓ |  |
| 45 | `temperatura_tolerancia` | `float` | ✓ |  |
| 46 | `temperatura_recepcion` | `bit` | ✓ |  |
| 47 | `temperatura_despacho` | `bit` | ✓ |  |
| 48 | `fechamanufactura` | `bit` | ✓ |  |
| 49 | `capturar_aniada` | `bit` | ✓ |  |
| 50 | `Arancel` | `nvarchar(150)` | ✓ |  |
| 51 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 52 | `fec_agr` | `datetime` | ✓ |  |
| 53 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 54 | `fec_mod` | `datetime` | ✓ |  |
| 55 | `captura_arancel` | `bit` | ✓ |  |
| 56 | `es_hardware` | `bit` | ✓ |  |
| 57 | `activo` | `bit` | ✓ |  |
| 58 | `imagen` | `int` | ✓ |  |
| 59 | `largo` | `float` | ✓ |  |
| 60 | `ancho` | `float` | ✓ |  |
| 61 | `alto` | `float` | ✓ |  |
| 62 | `genera_lp_old` | `bit` | ✓ |  |
| 63 | `IdUnidadMedidaCobro` | `int` | ✓ |  |
| 64 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 65 | `IdTipoManufactura` | `int` | ✓ |  |
| 66 | `IdProductoParametroA` | `int` | ✓ |  |
| 67 | `IdProductoParametroB` | `int` | ✓ |  |
| 68 | `Dias_Inventario_Promedio` | `int` | ✓ |  |
| 69 | `margen_impresion` | `float` |  |  |

## Consume

- `arancel`
- `producto`
- `producto_bodega`
- `producto_codigos_barra`
- `producto_presentacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoSI]
AS
SELECT DISTINCT 
                         pb.IdBodega, pb.IdProductoBodega, p.IdProducto, p.IdPropietario, p.IdClasificacion, p.IdFamilia, p.IdMarca, p.IdTipoProducto, p.IdUnidadMedidaBasica, p.IdCamara, p.IdTipoRotacion, p.IdPerfilSerializado, p.IdIndiceRotacion, 
                         p.IdSimbologia, p.IdArancel, p.codigo, p.nombre, p.codigo_barra, p.precio, p.existencia_min, p.existencia_max, p.costo, p.peso_referencia, p.peso_tolerancia, p.noparte, p.noserie, p.control_peso, p.ciclo_vida, p.tolerancia, p.kit, 
                         p.materia_prima, p.control_lote, p.control_vencimiento, p.genera_lote, p.serializado, pp.codigo_barra AS codigo_barra_presentacion, pcb.codigo_barra AS codigo_barra_pcb, pp.nombre AS NomPresentacion, 
                         pp.activo AS activopp, pp.IdPresentacion, pp.factor, p.peso_recepcion, p.peso_despacho, p.temperatura_referencia, p.temperatura_tolerancia, p.temperatura_recepcion, p.temperatura_despacho, p.fechamanufactura, 
                         p.capturar_aniada, a.nombre AS Arancel, p.user_agr, p.fec_agr, p.user_mod, p.fec_mod, p.captura_arancel, p.es_hardware, p.activo, NULL AS imagen, p.largo, 
                         p.ancho, p.alto, p.genera_lp_old, p.IdUnidadMedidaCobro, p.IdTipoEtiqueta, p.IdTipoManufactura, 
                         p.IdProductoParametroA, p.IdProductoParametroB, p.Dias_Inventario_Promedio, margen_impresion
FROM            dbo.producto_bodega AS pb RIGHT OUTER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.producto_codigos_barra AS pcb ON p.IdProducto = pcb.IdProducto LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                         dbo.arancel AS a ON p.IdArancel = a.IdArancel
WHERE        (pb.activo = 1) AND (p.activo = 1)
```
