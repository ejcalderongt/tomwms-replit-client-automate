---
id: db-brain-view-vw-stockpresentaciones
type: db-view
title: dbo.VW_StockPresentaciones
schema: dbo
name: VW_StockPresentaciones
kind: view
modify_date: 2021-09-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_StockPresentaciones`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-09-23 |
| Columnas | 22 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPresentacion` | `int` | ✓ |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `IdPropietario` | `int` | ✓ |  |
| 6 | `IdProducto` | `int` | ✓ |  |
| 7 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 8 | `imprime_barra` | `bit` | ✓ |  |
| 9 | `peso` | `float` | ✓ |  |
| 10 | `alto` | `float` | ✓ |  |
| 11 | `largo` | `float` | ✓ |  |
| 12 | `ancho` | `float` | ✓ |  |
| 13 | `factor` | `float` |  |  |
| 14 | `MinimoExistencia` | `float` | ✓ |  |
| 15 | `MaximoExistencia` | `float` | ✓ |  |
| 16 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 17 | `fec_agr` | `datetime` | ✓ |  |
| 18 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 19 | `fec_mod` | `datetime` | ✓ |  |
| 20 | `activo` | `bit` | ✓ |  |
| 21 | `IdStock` | `int` |  |  |
| 22 | `IdBodega` | `int` |  |  |

## Consume

- `producto_bodega`
- `producto_presentacion`
- `propietario_bodega`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_StockPresentaciones]
AS
SELECT        dbo.stock.IdPresentacion, dbo.producto_presentacion.nombre, dbo.stock.IdProductoBodega, dbo.propietario_bodega.IdPropietarioBodega, dbo.propietario_bodega.IdPropietario, dbo.producto_bodega.IdProducto, 
                         dbo.producto_presentacion.codigo_barra, dbo.producto_presentacion.imprime_barra, dbo.producto_presentacion.peso, dbo.producto_presentacion.alto, dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, 
                         dbo.producto_presentacion.factor, dbo.producto_presentacion.MinimoExistencia, dbo.producto_presentacion.MaximoExistencia, dbo.producto_presentacion.user_agr, dbo.producto_presentacion.fec_agr, 
                         dbo.producto_presentacion.user_mod, dbo.producto_presentacion.fec_mod, dbo.producto_presentacion.activo, dbo.stock.IdStock, dbo.stock.IdBodega
FROM            dbo.stock INNER JOIN
                         dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
```
