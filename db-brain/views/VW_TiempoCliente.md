---
id: db-brain-view-vw-tiempocliente
type: db-view
title: dbo.VW_TiempoCliente
schema: dbo
name: VW_TiempoCliente
kind: view
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TiempoCliente`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-03-18 |
| Columnas | 14 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Familia` | `nvarchar(50)` | ✓ |  |
| 2 | `Clasificación` | `nvarchar(50)` | ✓ |  |
| 3 | `Cliente` | `nvarchar(150)` | ✓ |  |
| 4 | `IdTiempoCliente` | `int` |  |  |
| 5 | `IdCliente` | `int` |  |  |
| 6 | `IdFamilia` | `int` | ✓ |  |
| 7 | `IdClasificacion` | `int` | ✓ |  |
| 8 | `Dias_Local` | `int` | ✓ |  |
| 9 | `Dias_Exterior` | `int` | ✓ |  |
| 10 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |

## Consume

- `cliente`
- `cliente_tiempos`
- `producto_clasificacion`
- `producto_familia`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TiempoCliente]
AS
SELECT     pf.nombre AS Familia, pc.nombre AS Clasificación, cl.nombre_comercial Cliente, ct.*
FROM         dbo.cliente_tiempos AS ct INNER JOIN cliente cl ON ct.IdCliente = cl.IdCliente LEFT OUTER JOIN
                      dbo.producto_familia AS pf ON ct.IdFamilia = pf.IdFamilia LEFT OUTER JOIN
                      dbo.producto_clasificacion AS pc ON ct.IdClasificacion = pc.IdClasificacion
```
