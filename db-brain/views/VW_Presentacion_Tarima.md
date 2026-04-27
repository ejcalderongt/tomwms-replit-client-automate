---
id: db-brain-view-vw-presentacion-tarima
type: db-view
title: dbo.VW_Presentacion_Tarima
schema: dbo
name: VW_Presentacion_Tarima
kind: view
modify_date: 2017-05-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Presentacion_Tarima`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-05-30 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProducto` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `TipoTarima` | `nvarchar(50)` | ✓ |  |
| 4 | `IdPresentacionTarima` | `int` |  |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdTipoTarima` | `int` | ✓ |  |
| 7 | `Cantidad` | `float` | ✓ |  |
| 8 | `CantidadPorCama` | `float` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |

## Consume

- `producto_presentacion`
- `producto_presentacion_tarima`
- `tipo_tarima`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Presentacion_Tarima
AS
SELECT     pp.IdProducto, pp.nombre, tp.Nombre AS TipoTarima, ppt.*
FROM         dbo.producto_presentacion_tarima AS ppt INNER JOIN
                      dbo.producto_presentacion AS pp ON ppt.IdPresentacion = pp.IdPresentacion INNER JOIN
                      dbo.tipo_tarima AS tp ON ppt.IdTipoTarima = tp.IdTipoTarima
```
