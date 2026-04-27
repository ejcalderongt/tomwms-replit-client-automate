---
id: db-brain-function-scalar-nombre-completo-ubicacion-1
type: db-function-scalar
title: dbo.Nombre_Completo_Ubicacion_1
schema: dbo
name: Nombre_Completo_Ubicacion_1
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Nombre_Completo_Ubicacion_1`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdUbicacion` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Nombre_Completo_Ubicacion_1] 
(
	@IdUbicacion INT,
	@IdBodega INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(200)
	-- Declare the return variable here
	
	SELECT @resultado = CASE WHEN T.es_rack = 1 THEN
			'R' + RIGHT('00'+ SUBSTRING(t.descripcion,2,iif(CHARINDEX('-',t.descripcion,0)<0,1,CHARINDEX('-',t.descripcion,0)-2)),2) + ' - ' +
			'C' + RIGHT('00'+ CONVERT(NVARCHAR(10),u.indice_x),2 )+ ' - ' +
			'T' + SUBSTRING(t.descripcion,iif(CHARINDEX('-',t.descripcion,0)<0,0,CHARINDEX('-',t.descripcion,0)+1),1)+ ' - ' +
			'N' + RIGHT('00' + CONVERT(NVARCHAR(10),u.nivel),2 )+ ' - ' +
			'Pos' + u.orientacion_pos+ ' - ' +
			'#' + CONVERT(NVARCHAR(10),u.IdUbicacion)
			ELSE U.descripcion + ' - #' + CONVERT(NVARCHAR(10),u.IdUbicacion) END
	FROM bodega_ubicacion u 
	inner join bodega_tramo t on u.idtramo = t.idtramo 
	and t.IdBodega = u.IdBodega
	WHERE (u.IdUbicacion=@IdUbicacion) 
	And u.idbodega = @IdBodega

	RETURN @resultado;

END
```
