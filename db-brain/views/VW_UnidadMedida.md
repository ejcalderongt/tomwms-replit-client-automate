---
id: db-brain-view-vw-unidadmedida
type: db-view
title: dbo.VW_UnidadMedida
schema: dbo
name: VW_UnidadMedida
kind: view
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_UnidadMedida`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-08-25 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `IdUnidadMedida` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `codigo` | `nvarchar(25)` | ✓ |  |
| 11 | `es_um_cobro` | `bit` | ✓ |  |
| 12 | `factor` | `float` | ✓ |  |

## Consume

- `propietarios`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_UnidadMedida]
AS
SELECT        p.nombre_comercial AS Propietario, e.IdUnidadMedida, e.IdPropietario, e.Nombre, e.activo, e.fec_agr, e.user_mod, e.fec_mod, e.user_agr, e.codigo, e.es_um_cobro, e.factor
FROM            dbo.unidad_medida AS e INNER JOIN
                         dbo.propietarios AS p ON e.IdPropietario = p.IdPropietario
```
