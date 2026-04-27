---
id: db-brain-view-vw-fiscal-valorizaci-n
type: db-view
title: dbo.VW_Fiscal_Valorización
schema: dbo
name: VW_Fiscal_Valorización
kind: view
modify_date: 2022-06-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Fiscal_Valorización`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-16 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Fecha` | `date` | ✓ |  |
| 2 | `Regimen` | `nvarchar(20)` | ✓ |  |
| 3 | `bodega` | `nvarchar(50)` | ✓ |  |
| 4 | `IdStock` | `int` | ✓ |  |
| 5 | `no_poliza` | `nvarchar(50)` | ✓ |  |
| 6 | `IdArea` | `int` |  |  |
| 7 | `IdBodega` | `int` | ✓ |  |
| 8 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 9 | `IdProductoBodega` | `int` | ✓ |  |
| 10 | `valor_aduana` | `float` | ✓ |  |
| 11 | `valor_dai` | `float` | ✓ |  |
| 12 | `valor_flete` | `float` | ✓ |  |
| 13 | `valor_fob` | `float` | ✓ |  |
| 14 | `valor_iva` | `float` | ✓ |  |
| 15 | `valor_seguro` | `float` | ✓ |  |
| 16 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `Grupo` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_area`
- `bodega_ubicacion`
- `stock_jornada`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create view VW_Fiscal_Valorización as
SELECT        sj.Fecha, sj.Regimen, ba.Descripcion AS bodega, sj.IdStock, sj.no_poliza, ba.IdArea, sj.IdBodega, sj.codigo_producto, sj.IdProductoBodega, sj.valor_aduana, sj.valor_dai, sj.valor_flete, sj.valor_fob, sj.valor_iva, 
                         sj.valor_seguro, ba.Codigo,ba.Grupo
FROM            dbo.stock_jornada AS sj INNER JOIN
                         dbo.bodega_ubicacion AS bu ON sj.IdUbicacion = bu.IdUbicacion AND sj.IdBodega = bu.IdBodega INNER JOIN
                         dbo.bodega_area AS ba ON bu.IdArea = ba.IdArea AND bu.IdBodega = ba.IdBodega
```
