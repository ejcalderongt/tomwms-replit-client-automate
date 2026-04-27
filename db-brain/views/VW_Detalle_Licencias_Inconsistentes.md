---
id: db-brain-view-vw-detalle-licencias-inconsistentes
type: db-view
title: dbo.VW_Detalle_Licencias_Inconsistentes
schema: dbo
name: VW_Detalle_Licencias_Inconsistentes
kind: view
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Detalle_Licencias_Inconsistentes`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-02-01 |
| Columnas | 5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 5 | `IdBodega` | `int` |  |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `stock`
- `stock_res`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Detalle_Licencias_Inconsistentes]
AS
SELECT S4.lic_plate Licencia, pr.Codigo, pr.Nombre, dbo.Nombre_Completo_Ubicacion(s4.IdUbicacion, s4.IdBodega) Ubicacion, s4.IdBodega
FROM   stock S4 inner join 
       producto_bodega pb on s4.IdProductoBodega = pb.IdProductoBodega inner join
	   producto pr on pr.IdProducto = pb.IdProducto
WHERE  S4.IdUbicacion IN (
							SELECT s3.IdUbicacion
							FROM   dbo.stock AS s3 INNER JOIN
								   dbo.bodega_ubicacion AS bu ON s3.IdUbicacion = bu.IdUbicacion AND s3.IdBodega = bu.IdBodega INNER JOIN
								   dbo.bodega_tramo AS bt ON bt.IdSector = bu.IdSector AND bt.IdBodega = bu.IdBodega
							WHERE  bu.nivel>1 
							GROUP BY s3.IdUbicacion, s3.IdBodega, bt.es_rack
							HAVING COUNT(DISTINCT s3.lic_plate)>1) 
       AND S4.IDSTOCK NOT IN (SELECT MAX(IdStock)
                              FROM stock s2 WHERE Idubicacion in  (SELECT s.IdUbicacion
																   FROM    dbo.stock AS s INNER JOIN
																		   dbo.bodega_ubicacion AS bu ON s.IdUbicacion = bu.IdUbicacion AND s.IdBodega = bu.IdBodega INNER JOIN
																		   dbo.bodega_tramo AS bt ON bt.IdSector = bu.IdSector AND bt.IdBodega = bu.IdBodega
																	WHERE  bu.nivel>1 
																	GROUP BY s.IdUbicacion, s.IdBodega, bt.es_rack
																	HAVING COUNT(DISTINCT s.lic_plate)>1)

                               GROUP BY IdUbicacion)
	   AND S4.IdStock NOT IN (SELECT IdStock FROM stock_res)
```
