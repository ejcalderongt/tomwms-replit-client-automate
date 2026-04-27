---
id: db-brain-function-scalar-get-porcentaje-avance-picking
type: db-function-scalar
title: dbo.Get_Porcentaje_Avance_Picking
schema: dbo
name: Get_Porcentaje_Avance_Picking
kind: function-scalar
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Get_Porcentaje_Avance_Picking`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2024-02-01 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdPickingEnc` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- =============================================
-- Author:		Carolina Fuentes
-- Create date: 14-08-2023
-- Description:	Función para obtener porcentaje de avance del Picking
-- =============================================
CREATE FUNCTION Get_Porcentaje_Avance_Picking 
(
	@IdPickingEnc int, @IdBodega int
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Porcentaje float = 0

	SELECT @Porcentaje = ROUND(SUM(cantidad_recibida)*100/SUM(cantidad_solicitada),2)
	FROM trans_picking_ubic
	WHERE dañado_picking = 0 AND dañado_verificacion = 0 AND no_encontrado = 0 
	      AND IdPickingEnc = @IdPickingEnc AND IdBodega = @IdBodega
	GROUP BY IdPickingEnc
	HAVING SUM(cantidad_solicitada)>0

	-- Return the result of the function
	RETURN @Porcentaje

END
```
