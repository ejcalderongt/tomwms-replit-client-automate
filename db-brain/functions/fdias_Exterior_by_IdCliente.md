---
id: db-brain-function-scalar-fdias-exterior-by-idcliente
type: db-function-scalar
title: dbo.fdias_Exterior_by_IdCliente
schema: dbo
name: fdias_Exterior_by_IdCliente
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.fdias_Exterior_by_IdCliente`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 3 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdCliente` | `int` |  |
| 2 | `@IdFamilia` | `int` |  |
| 3 | `@IdClasificacion` | `int` |  |

## Consume

- `cliente_tiempos`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[fdias_Exterior_by_IdCliente] 
(
	@IdCliente INT,
	@IdFamilia INT,
	@IdClasificacion INT
)
RETURNS NVARCHAR(250)
AS
BEGIN
	
	DECLARE @resultado int
	-- Declare the return variable here
	
	SELECT @resultado = (select Dias_Exterior from cliente_tiempos 
	WHERE (IdCliente=@IdCliente AND IdClasificacion = @IdClasificacion AND IdFamilia = @IdFamilia) )
	
	RETURN @resultado;

END
```
