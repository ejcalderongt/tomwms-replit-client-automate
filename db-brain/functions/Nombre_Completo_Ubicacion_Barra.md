---
id: db-brain-function-scalar-nombre-completo-ubicacion-barra
type: db-function-scalar
title: dbo.Nombre_Completo_Ubicacion_Barra
schema: dbo
name: Nombre_Completo_Ubicacion_Barra
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Nombre_Completo_Ubicacion_Barra`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@Barra` | `nvarchar(50)` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Nombre_Completo_Ubicacion_Barra] 
(
	@Barra NVARCHAR(50),
	@IdBodega INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(200)
	-- Declare the return variable here
	
	SELECT @resultado = CASE WHEN T.es_rack = 1 THEN
			iif(CHARINDEX('-',t.descripcion,0)=0, t.descripcion, SUBSTRING(REPLACE(t.descripcion,'-',''),1, LEN(t.descripcion)-2)) + ' - ' +
			'C' + CONVERT(NVARCHAR(10),u.indice_x) + ' - ' +
			iif(CHARINDEX('-',t.descripcion,0)=0,'','T' + SUBSTRING(t.descripcion,iif(CHARINDEX('-',t.descripcion,0)<0,0,CHARINDEX('-',t.descripcion,0)+1),1)+ ' - ') +
			'N' + CONVERT(NVARCHAR(10),u.nivel)+ ' - ' +
			ISNULL(u.orientacion_pos,'0')+ ' - ' +
			'#' + CONVERT(NVARCHAR(10),u.IdUbicacion)
			ELSE U.descripcion + ' - #' + CONVERT(NVARCHAR(10),u.IdUbicacion) END
	FROM bodega_ubicacion u 
	inner join bodega_tramo t on u.idtramo = t.idtramo 
	and t.IdBodega = u.IdBodega
	WHERE (u.IdUbicacion=@Barra OR u.codigo_barra=@Barra OR u.codigo_barra2=@Barra) 
	And u.idbodega = @IdBodega

	RETURN @resultado;

END
```
