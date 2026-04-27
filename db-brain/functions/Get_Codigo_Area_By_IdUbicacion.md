---
id: db-brain-function-scalar-get-codigo-area-by-idubicacion
type: db-function-scalar
title: dbo.Get_Codigo_Area_By_IdUbicacion
schema: dbo
name: Get_Codigo_Area_By_IdUbicacion
kind: function-scalar
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Get_Codigo_Area_By_IdUbicacion`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2024-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdUbicacion` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_area`
- `bodega_ubicacion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Get_Codigo_Area_By_IdUbicacion] 
(
	@IdUbicacion INT,
	@IdBodega INT
)
RETURNS NVARCHAR(10)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(10)
	-- Declare the return variable here
	
	SELECT @resultado = a.codigo
	FROM bodega_ubicacion u 
	inner join bodega_area a on u.IdArea = a.IdArea
	and u.IdBodega = a.IdBodega
	WHERE (u.IdUbicacion=@IdUbicacion) 
	And u.idbodega = @IdBodega

	RETURN @resultado;

END
```
