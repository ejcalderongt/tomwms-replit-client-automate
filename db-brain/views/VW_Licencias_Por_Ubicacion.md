---
id: db-brain-view-vw-licencias-por-ubicacion
type: db-view
title: dbo.VW_Licencias_Por_Ubicacion
schema: dbo
name: VW_Licencias_Por_Ubicacion
kind: view
modify_date: 2023-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Licencias_Por_Ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-05-18 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Bodega` | `int` |  |  |
| 2 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 3 | `Cant_Lic` | `int` | ✓ |  |
| 4 | `Es_Rack` | `varchar(2)` |  |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Licencias_Por_Ubicacion]
AS
SELECT        s.IdBodega AS Bodega, dbo.Nombre_Completo_Ubicacion(s.IdUbicacion, s.IdBodega) AS Ubicacion, COUNT(DISTINCT s.lic_plate) AS Cant_Lic, CASE WHEN bt.es_rack = 1 THEN 'Si' ELSE 'No' END AS Es_Rack
FROM            dbo.stock AS s INNER JOIN
                         dbo.bodega_ubicacion AS bu ON s.IdUbicacion = bu.IdUbicacion AND s.IdBodega = bu.IdBodega INNER JOIN
                         dbo.bodega_tramo AS bt ON bt.IdSector = bu.IdSector AND bt.IdBodega = bu.IdBodega
GROUP BY s.IdUbicacion, s.IdBodega, bt.es_rack
```
