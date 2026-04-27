---
id: db-brain-view-vw-propietario
type: db-view
title: dbo.VW_Propietario
schema: dbo
name: VW_Propietario
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Propietario`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 21 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdEmpresa` | `int` | ✓ |  |
| 4 | `IdTipoActualizacionCosto` | `int` | ✓ |  |
| 5 | `contacto` | `nvarchar(100)` |  |  |
| 6 | `nombre_comercial` | `nvarchar(100)` |  |  |
| 7 | `imagen` | `image` | ✓ |  |
| 8 | `telefono` | `nvarchar(50)` | ✓ |  |
| 9 | `direccion` | `nvarchar(50)` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `email` | `nvarchar(100)` | ✓ |  |
| 16 | `actualiza_costo_oc` | `bit` | ✓ |  |
| 17 | `color` | `int` | ✓ |  |
| 18 | `codigo` | `nvarchar(25)` | ✓ |  |
| 19 | `sistema` | `bit` | ✓ |  |
| 20 | `nit` | `nvarchar(50)` | ✓ |  |
| 21 | `es_consolidador` | `bit` | ✓ |  |

## Consume

- `empresa`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Propietario]
AS
SELECT        e.nombre AS Empresa, p.IdPropietario, p.IdEmpresa, p.IdTipoActualizacionCosto, p.contacto, p.nombre_comercial, p.imagen, p.telefono, p.direccion,
              p.activo, p.user_agr, p.fec_agr, p.user_mod, p.fec_mod, p.email, 
              p.actualiza_costo_oc, p.color, p.codigo, p.sistema, p.nit, p.es_consolidador
FROM            dbo.propietarios AS p LEFT OUTER JOIN
                         dbo.empresa AS e ON p.IdEmpresa = e.IdEmpresa
```
