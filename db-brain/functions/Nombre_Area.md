---
id: db-brain-function-scalar-nombre-area
type: db-function-scalar
title: dbo.Nombre_Area
schema: dbo
name: Nombre_Area
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Nombre_Area`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdArea` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_area`

## Quién la referencia

**19** objetos:

- `VW_Fiscal_historico` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Nombre_Area] 
(
	@IdArea INT,
	@IdBodega INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(200)
	-- Declare the return variable here
	
	SELECT @resultado = a.Descripcion
	FROM bodega_area a
	WHERE (a.IdArea=@IdArea) 
	And a.idbodega = @IdBodega

	RETURN @resultado;

END
```
