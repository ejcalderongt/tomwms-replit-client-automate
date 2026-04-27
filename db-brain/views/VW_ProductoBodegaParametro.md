---
id: db-brain-view-vw-productobodegaparametro
type: db-view
title: dbo.VW_ProductoBodegaParametro
schema: dbo
name: VW_ProductoBodegaParametro
kind: view
modify_date: 2016-04-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoBodegaParametro`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-04-27 |
| Columnas | 18 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoParametro` | `int` |  |  |
| 2 | `IdParametro` | `int` |  |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 5 | `valor_numerico` | `float` | ✓ |  |
| 6 | `valor_fecha` | `datetime` | ✓ |  |
| 7 | `valor_logico` | `bit` | ✓ |  |
| 8 | `activo` | `bit` |  |  |
| 9 | `user_agr` | `nvarchar(50)` |  |  |
| 10 | `fec_agr` | `datetime` |  |  |
| 11 | `user_mod` | `nvarchar(50)` |  |  |
| 12 | `fec_mod` | `datetime` |  |  |
| 13 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 14 | `tipo` | `nvarchar(50)` | ✓ |  |
| 15 | `Producto` | `int` |  |  |
| 16 | `ProductoParametro` | `int` |  |  |
| 17 | `IdProductoBodega` | `int` |  |  |
| 18 | `capturar_siempre` | `bit` |  |  |

## Consume

- `p_parametro`
- `producto`
- `producto_bodega`
- `producto_parametros`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_ProductoBodegaParametro
AS
SELECT        pp.IdProductoParametro, pp.IdParametro, pp.IdProducto, pp.valor_texto, pp.valor_numerico, pp.valor_fecha, pp.valor_logico, pp.activo, pp.user_agr, pp.fec_agr, 
                         pp.user_mod, pp.fec_mod, p.descripcion, p.tipo, pp.IdProducto AS Producto, pp.IdProductoParametro AS ProductoParametro, ppb.IdProductoBodega, 
                         pp.capturar_siempre
FROM            dbo.producto_parametros AS pp INNER JOIN
                         dbo.p_parametro AS p ON pp.IdParametro = p.IdParametro INNER JOIN
                         dbo.producto AS pr ON pp.IdProducto = pr.IdProducto INNER JOIN
                         dbo.producto_bodega AS ppb ON pr.IdProducto = ppb.IdProducto
```
