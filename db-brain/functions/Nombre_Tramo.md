---
id: db-brain-function-scalar-nombre-tramo
type: db-function-scalar
title: dbo.Nombre_Tramo
schema: dbo
name: Nombre_Tramo
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Nombre_Tramo`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdTramo` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_tramo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Nombre_Tramo] 
(
	@IdTramo INT,
	@IdBodega INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(200)
	-- Declare the return variable here
	
	SELECT @resultado = a.Descripcion
	FROM bodega_tramo a
	WHERE (a.IdTramo=@IdTramo) 
	And a.idbodega = @IdBodega

	RETURN @resultado;

END
```
