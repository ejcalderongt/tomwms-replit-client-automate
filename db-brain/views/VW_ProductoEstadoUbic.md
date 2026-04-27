---
id: db-brain-view-vw-productoestadoubic
type: db-view
title: dbo.VW_ProductoEstadoUbic
schema: dbo
name: VW_ProductoEstadoUbic
kind: view
modify_date: 2022-05-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoEstadoUbic`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-24 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdEstado` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `NombreUbic` | `nvarchar(200)` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto_estado_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_ProductoEstadoUbic]
AS
SELECT u.IdUbicacion, p.IdEstado, dbo.bodega.IdBodega,
dbo.Nombre_Completo_Ubicacion(u.idubicacion,u.idbodega) AS NombreUbic
FROM dbo.producto_estado_ubic AS p INNER JOIN
dbo.bodega_ubicacion AS u ON p.IdUbicacionDefecto = u.IdUbicacion INNER JOIN
dbo.bodega_tramo AS t ON t .IdTramo = u.IdTramo INNER JOIN
dbo.bodega ON p.IdBodega = dbo.bodega.IdBodega AND t .IdBodega = dbo.bodega.IdBodega
```
