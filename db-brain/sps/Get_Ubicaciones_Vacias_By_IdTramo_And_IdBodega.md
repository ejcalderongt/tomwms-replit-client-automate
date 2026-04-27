---
id: db-brain-sp-get-ubicaciones-vacias-by-idtramo-and-idbodega
type: db-sp
title: dbo.Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega
schema: dbo
name: Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega
kind: sp
modify_date: 2021-11-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2021-11-18 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdBodega` | `int` |  |
| 2 | `@IdTramo` | `int` |  |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega
	@IdBodega int,
	@IdTramo int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT COUNT(dbo.bodega_ubicacion.IdUbicacion) AS Ubicaciones_Vacias
	FROM dbo.bodega INNER JOIN
                dbo.bodega_area ON dbo.bodega.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
                dbo.bodega_sector ON dbo.bodega_area.IdArea = dbo.bodega_sector.IdArea 
				AND dbo.bodega_area.IdBodega = dbo.bodega_sector.IdBodega 
				INNER JOIN dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector 
				AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega 
				INNER JOIN dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo 
				AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector 
				AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea 
				AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega
				WHERE dbo.bodega_tramo.IdBodega  = @IdBodega
				AND dbo.bodega_tramo.IdTramo  = @IdTramo


END
```
