---
id: db-brain-view-vw-productotipo
type: db-view
title: dbo.VW_ProductoTipo
schema: dbo
name: VW_ProductoTipo
kind: view
modify_date: 2021-11-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoTipo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-11-16 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoProducto` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `NombreTipoProducto` | `nvarchar(50)` | ✓ |  |
| 6 | `Activo` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |

## Consume

- `producto_tipo`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoTipo]
AS
SELECT        e.IdTipoProducto, p.IdPropietario, p.nombre_comercial AS Propietario, e.Codigo, e.NombreTipoProducto, e.Activo, e.user_agr, e.fec_agr, e.user_mod, e.fec_mod
FROM            dbo.producto_tipo AS e INNER JOIN
                         dbo.propietarios AS p ON e.IdPropietario = p.IdPropietario
```
