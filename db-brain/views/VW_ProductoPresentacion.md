---
id: db-brain-view-vw-productopresentacion
type: db-view
title: dbo.VW_ProductoPresentacion
schema: dbo
name: VW_ProductoPresentacion
kind: view
modify_date: 2022-06-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoPresentacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-24 |
| Columnas | 32 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Producto` | `nvarchar(100)` | ✓ |  |
| 2 | `IdPresentacion` | `int` |  |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(50)` | ✓ |  |
| 6 | `imprime_barra` | `bit` | ✓ |  |
| 7 | `peso` | `float` | ✓ |  |
| 8 | `alto` | `float` | ✓ |  |
| 9 | `largo` | `float` | ✓ |  |
| 10 | `ancho` | `float` | ✓ |  |
| 11 | `factor` | `float` |  |  |
| 12 | `MinimoExistencia` | `float` | ✓ |  |
| 13 | `MaximoExistencia` | `float` | ✓ |  |
| 14 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `activo` | `bit` | ✓ |  |
| 19 | `EsPallet` | `bit` | ✓ |  |
| 20 | `Precio` | `float` | ✓ |  |
| 21 | `MinimoPeso` | `float` | ✓ |  |
| 22 | `MaximoPeso` | `float` | ✓ |  |
| 23 | `Costo` | `float` | ✓ |  |
| 24 | `CamasPorTarima` | `float` | ✓ |  |
| 25 | `CajasPorCama` | `float` | ✓ |  |
| 26 | `genera_lp_auto` | `bit` | ✓ |  |
| 27 | `permitir_paletizar` | `bit` | ✓ |  |
| 28 | `sistema` | `bit` | ✓ |  |
| 29 | `IdPresentacionPallet` | `int` | ✓ |  |
| 30 | `IdProductoBodega` | `int` |  |  |
| 31 | `IdBodega` | `int` | ✓ |  |
| 32 | `codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_presentacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoPresentacion]
AS
SELECT        p.nombre AS Producto, pp.IdPresentacion, pp.IdProducto, pp.codigo_barra, pp.nombre, pp.imprime_barra, pp.peso, pp.alto, pp.largo, pp.ancho, pp.factor, pp.MinimoExistencia, pp.MaximoExistencia, pp.user_agr, pp.fec_agr, 
                         pp.user_mod, pp.fec_mod, pp.activo, pp.EsPallet, pp.Precio, pp.MinimoPeso, pp.MaximoPeso, pp.Costo, pp.CamasPorTarima, pp.CajasPorCama, pp.genera_lp_auto, pp.permitir_paletizar, pp.sistema, pp.IdPresentacionPallet, 
                         dbo.producto_bodega.IdProductoBodega, dbo.producto_bodega.IdBodega, p.codigo
FROM            dbo.producto_presentacion AS pp INNER JOIN
                         dbo.producto AS p ON pp.IdProducto = p.IdProducto INNER JOIN
                         dbo.producto_bodega ON p.IdProducto = dbo.producto_bodega.IdProducto
GROUP BY p.nombre, pp.IdPresentacion, pp.IdProducto, pp.codigo_barra, pp.nombre, pp.peso, pp.alto, pp.largo, pp.ancho, pp.factor, pp.MinimoExistencia, pp.MaximoExistencia, pp.user_agr, pp.fec_agr, pp.user_mod, pp.fec_mod, 
                         pp.Precio, pp.MinimoPeso, pp.MaximoPeso, pp.Costo, pp.CamasPorTarima, pp.CajasPorCama, pp.IdPresentacionPallet, dbo.producto_bodega.IdProductoBodega, pp.activo, pp.EsPallet, pp.genera_lp_auto, pp.permitir_paletizar, 
                         pp.sistema, pp.imprime_barra, dbo.producto_bodega.IdBodega, p.codigo
```
