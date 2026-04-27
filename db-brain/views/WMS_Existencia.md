---
id: db-brain-view-wms-existencia
type: db-view
title: dbo.WMS_Existencia
schema: dbo
name: WMS_Existencia
kind: view
modify_date: 2025-08-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.WMS_Existencia`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-08-01 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Producto` | `nvarchar(100)` | ✓ |  |
| 4 | `Presentacion` | `nvarchar(50)` |  |  |
| 5 | `Factor` | `float` |  |  |
| 6 | `Ubicacion` | `nvarchar(4000)` | ✓ |  |
| 7 | `Cantidad Unidades` | `float` | ✓ |  |
| 8 | `Cantidad Presentacion` | `float` | ✓ |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[WMS_Existencia]
AS

select
	t1.nombre as Bodega
	,t3.codigo as Codigo
	,t3.nombre as Producto
	,isnull(t4.nombre,'Unidad') as Presentacion
	,isnull(t4.factor,1) as Factor
	,CASE WHEN t6.es_rack = 1 THEN
				iif(CHARINDEX('-',t6.descripcion,0)=0, t6.descripcion, SUBSTRING(REPLACE(t6.descripcion,'-',''),1, LEN(t6.descripcion)-2)) + ' - ' +
				'C' + CONVERT(NVARCHAR(10),t5.indice_x) + ' - ' +
				iif(CHARINDEX('-',t6.descripcion,0)=0,'','T' + SUBSTRING(t6.descripcion,iif(CHARINDEX('-',t6.descripcion,0)<0,0,CHARINDEX('-',t6.descripcion,0)+1),1)+ ' - ') +
				'N' + CONVERT(NVARCHAR(10),t5.nivel)+ ' - ' +
				ISNULL(t5.orientacion_pos,'0')+ ' - ' +
				'#' + CONVERT(NVARCHAR(10),t5.IdUbicacion)
				ELSE t5.descripcion + ' - #' + CONVERT(NVARCHAR(10),t5.IdUbicacion) END as "Ubicacion"
	,Sum(t0.Cantidad) as "Cantidad Unidades"
	,Sum(t0.Cantidad)/isnull(t4.factor,1) as "Cantidad Presentacion"
from
	stock as t0
	join bodega as t1 on t0.IdBodega = t1.IdBodega
	join producto_bodega as t2 on t0.IdProductoBodega = t2.IdProductoBodega
	join producto as t3 on t2.IdProducto = t3.IdProducto
	left join producto_presentacion as t4 on t2.IdProducto = t4.IdProducto and t0.IdPresentacion = t4.IdPresentacion
	left join bodega_ubicacion as t5 on t0.IdUbicacion = t5.IdUbicacion
	left join bodega_tramo as t6 on t5.idtramo = t6.idtramo
group by
	t1.nombre
	,t3.codigo
	,t3.nombre
	,t4.nombre
	,t4.factor
	,t6.es_rack
	,t6.descripcion
	,t5.orientacion_pos
	,t5.descripcion
	,t5.IdUbicacion
	,t5.indice_x
	,t5.nivel

--select * from ubicaciones_por_regla
```
