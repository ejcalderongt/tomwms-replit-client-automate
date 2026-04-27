---
id: db-brain-view-vw-stockestadosproducto
type: db-view
title: dbo.VW_StockEstadosProducto
schema: dbo
name: VW_StockEstadosProducto
kind: view
modify_date: 2016-06-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_StockEstadosProducto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-06-06 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoEstado` | `int` | ✓ |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `IdPropietario` | `int` | ✓ |  |
| 6 | `IdProducto` | `int` | ✓ |  |
| 7 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 8 | `utilizable` | `bit` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `dañado` | `bit` | ✓ |  |

## Consume

- `producto_bodega`
- `producto_estado`
- `propietario_bodega`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_StockEstadosProducto
AS
SELECT        dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.stock.IdProductoBodega, dbo.propietario_bodega.IdPropietarioBodega, 
                         dbo.propietario_bodega.IdPropietario, dbo.producto_bodega.IdProducto, dbo.producto_estado.IdUbicacionDefecto, dbo.producto_estado.utilizable, 
                         dbo.producto_estado.activo, dbo.producto_estado.dañado
FROM            dbo.producto_estado INNER JOIN
                         dbo.stock ON dbo.producto_estado.IdEstado = dbo.stock.IdProductoEstado INNER JOIN
                         dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.producto_bodega ON dbo.stock.IdProductoBodega = dbo.producto_bodega.IdProductoBodega
```
