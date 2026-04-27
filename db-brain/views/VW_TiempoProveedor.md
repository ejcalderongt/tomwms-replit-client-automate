---
id: db-brain-view-vw-tiempoproveedor
type: db-view
title: dbo.VW_TiempoProveedor
schema: dbo
name: VW_TiempoProveedor
kind: view
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TiempoProveedor`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-02-01 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Familia` | `nvarchar(50)` | ✓ |  |
| 2 | `Clasificación` | `nvarchar(50)` | ✓ |  |
| 3 | `IdTiempoproveedor` | `int` |  |  |
| 4 | `Idproveedor` | `int` |  |  |
| 5 | `IdFamilia` | `int` | ✓ |  |
| 6 | `IdClasificacion` | `int` | ✓ |  |
| 7 | `Dias_Local` | `int` | ✓ |  |
| 8 | `Dias_Exterior` | `int` | ✓ |  |
| 9 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |

## Consume

- `producto_clasificacion`
- `producto_familia`
- `proveedor_tiempos`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TiempoProveedor]
AS
SELECT     pf.nombre AS Familia, pc.nombre AS Clasificación, ct.*
FROM         dbo.proveedor_tiempos AS ct LEFT OUTER JOIN
                      dbo.producto_familia AS pf ON ct.IdFamilia = pf.IdFamilia LEFT OUTER JOIN
                      dbo.producto_clasificacion AS pc ON ct.IdClasificacion = pc.IdClasificacion
```
