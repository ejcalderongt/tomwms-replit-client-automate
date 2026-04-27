---
id: db-brain-view-vw-productofamilia
type: db-view
title: dbo.VW_ProductoFamilia
schema: dbo
name: VW_ProductoFamilia
kind: view
modify_date: 2021-11-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoFamilia`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-16 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `IdFamilia` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto_familia`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_ProductoFamilia
AS
SELECT        p.nombre_comercial AS Propietario, f.IdFamilia, f.IdPropietario, f.nombre, f.activo, f.user_agr, f.fec_agr, f.user_mod, f.fec_mod, f.codigo
FROM            dbo.producto_familia AS f INNER JOIN
                         dbo.propietarios AS p ON f.IdPropietario = p.IdPropietario
```
