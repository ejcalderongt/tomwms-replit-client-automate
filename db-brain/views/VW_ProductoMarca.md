---
id: db-brain-view-vw-productomarca
type: db-view
title: dbo.VW_ProductoMarca
schema: dbo
name: VW_ProductoMarca
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoMarca`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMarca` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `codigo` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto_marca`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoMarca]
AS
SELECT     m.IdMarca, p.IdPropietario, p.nombre_comercial AS Propietario, m.nombre, m.activo, m.user_agr, m.fec_agr, m.user_mod, m.fec_mod, m.codigo
FROM         dbo.producto_marca AS m INNER JOIN
                      dbo.propietarios AS p ON m.IdPropietario = p.IdPropietario
```
