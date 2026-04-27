---
id: db-brain-view-vw-productoestado
type: db-view
title: dbo.VW_ProductoEstado
schema: dbo
name: VW_ProductoEstado
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoEstado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `IdEstado` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `IdUbicacionDefecto` | `int` | ✓ |  |
| 6 | `utilizable` | `bit` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `dañado` | `bit` | ✓ |  |
| 13 | `codigo_bodega_erp` | `nvarchar(50)` | ✓ |  |
| 14 | `sistema` | `bit` | ✓ |  |
| 15 | `dias_vencimiento_clasificacion` | `int` |  |  |
| 16 | `tolerancia_dias_vencimiento` | `int` |  |  |

## Consume

- `producto_estado`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoEstado]
AS
SELECT p.nombre_comercial AS Propietario, e.IdEstado, e.IdPropietario, e.nombre, e.IdUbicacionDefecto, e.utilizable, e.activo, e.user_agr, e.fec_agr, e.user_mod, e.fec_mod, 
       e.dañado, e.codigo_bodega_erp, e.sistema, e.dias_vencimiento_clasificacion,e.tolerancia_dias_vencimiento
FROM         dbo.producto_estado AS e INNER JOIN
                      dbo.propietarios AS p ON e.IdPropietario = p.IdPropietario
```
