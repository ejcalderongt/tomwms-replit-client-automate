---
id: db-brain-view-vw-motivodevolucion
type: db-view
title: dbo.VW_MotivoDevolucion
schema: dbo
name: VW_MotivoDevolucion
kind: view
modify_date: 2017-06-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_MotivoDevolucion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-06-01 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 2 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 3 | `IdMotivoDevolucion` | `int` |  |  |
| 4 | `IdEmpresa` | `int` |  |  |
| 5 | `IdPropietario` | `int` | ✓ |  |
| 6 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` |  |  |
| 12 | `es_detalle` | `bit` |  |  |

## Consume

- `empresa`
- `motivo_devolucion`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_MotivoDevolucion
AS
SELECT     e.nombre AS Empresa, p.nombre_comercial AS Propietario, d.*
FROM         dbo.motivo_devolucion AS d LEFT OUTER JOIN
                      dbo.empresa AS e ON d.IdEmpresa = e.IdEmpresa LEFT OUTER JOIN
                      dbo.propietarios AS p ON d.IdPropietario = p.IdPropietario
```
