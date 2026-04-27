---
id: db-brain-view-vw-productosustituto
type: db-view
title: dbo.VW_ProductoSustituto
schema: dbo
name: VW_ProductoSustituto
kind: view
modify_date: 2016-03-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProductoSustituto`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-03-21 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `IdProductoOriginal` | `int` |  |  |
| 3 | `IdProductoPresentacionOriginal` | `int` | ✓ |  |
| 4 | `IdProductoReemplazo` | `int` |  |  |
| 5 | `IdProductoPresentacionReemplazo` | `int` | ✓ |  |
| 6 | `Presentación Original` | `nvarchar(50)` | ✓ |  |
| 7 | `Producto Reemplazo` | `nvarchar(50)` | ✓ |  |
| 8 | `Presentación Reemplazo` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Consume

- `producto`
- `producto_presentacion`
- `producto_sustituto`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_ProductoSustituto
AS
SELECT ps.IdProductoSustituto AS Código,ps.IdProductoOriginal,ps.IdProductoPresentacionOriginal,ps.IdProductoReemplazo,
ps.IdProductoPresentacionReemplazo, 
pp.nombre AS 'Presentación Original', p1.nombre AS 'Producto Reemplazo',
pp1.nombre AS 'Presentación Reemplazo',ps.activo
FROM producto_sustituto AS ps
INNER JOIN producto AS p ON ps.IdProductoOriginal = p.IdProducto
INNER JOIN producto_presentacion AS pp ON ps.IdProductoPresentacionOriginal = pp.IdPresentacion
INNER JOIN producto AS p1 ON ps.IdProductoReemplazo = p1.IdProducto
INNER JOIN producto_presentacion AS pp1 ON ps.IdProductoPresentacionReemplazo = pp1.IdPresentacion
```
