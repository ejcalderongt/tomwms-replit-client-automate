---
id: db-brain-view-vw-productoclasificacion
type: db-view
title: dbo.VW_ProductoClasificacion
schema: dbo
name: VW_ProductoClasificacion
kind: view
modify_date: 2021-11-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoClasificacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-16 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Expr1` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `IdClasificacion` | `int` |  |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `nombre` | `nvarchar(50)` | ✓ |  |
| 6 | `activo` | `bit` | ✓ |  |
| 7 | `sistema` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` |  |  |
| 12 | `codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto_clasificacion`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoClasificacion]
AS
SELECT     p.IdPropietario AS Expr1,  p.nombre_comercial AS Propietario, pc.*
FROM         dbo.producto_clasificacion AS pc INNER JOIN
                      dbo.propietarios AS p ON pc.IdPropietario = p.IdPropietario
```
