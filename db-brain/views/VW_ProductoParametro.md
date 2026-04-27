---
id: db-brain-view-vw-productoparametro
type: db-view
title: dbo.VW_ProductoParametro
schema: dbo
name: VW_ProductoParametro
kind: view
modify_date: 2016-04-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoParametro`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-04-22 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametro` | `int` |  |  |
| 2 | `IdProducto` | `int` |  |  |
| 3 | `valor_texto` | `nvarchar(50)` | ✓ |  |
| 4 | `valor_numerico` | `float` | ✓ |  |
| 5 | `valor_fecha` | `datetime` | ✓ |  |
| 6 | `valor_logico` | `bit` | ✓ |  |
| 7 | `capturar_siempre` | `bit` |  |  |
| 8 | `activo` | `bit` |  |  |
| 9 | `user_agr` | `nvarchar(50)` |  |  |
| 10 | `fec_agr` | `datetime` |  |  |
| 11 | `user_mod` | `nvarchar(50)` |  |  |
| 12 | `fec_mod` | `datetime` |  |  |
| 13 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 14 | `IdProductoParametro` | `int` |  |  |
| 15 | `tipo` | `nvarchar(50)` | ✓ |  |

## Consume

- `p_parametro`
- `producto`
- `producto_parametros`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_ProductoParametro
AS
SELECT pp.IdParametro,pr.IdProducto,pp.valor_texto, pp.valor_numerico,pp.valor_fecha,pp.valor_logico,pp.capturar_siempre,
pp.activo,pp.user_agr,pp.fec_agr,pp.user_mod,pp.fec_mod,p.descripcion,pp.IdProductoParametro,p.tipo 
FROM producto_parametros AS pp
INNER JOIN p_parametro AS p ON pp.IdParametro = p.IdParametro
INNER JOIN producto AS pr ON pp.IdProducto = pr.IdProducto
```
