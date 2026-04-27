---
id: db-brain-function-scalar-convertsecondsformatofecha
type: db-function-scalar
title: dbo.ConvertSecondsFormatoFecha
schema: dbo
name: ConvertSecondsFormatoFecha
kind: function-scalar
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.ConvertSecondsFormatoFecha`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2024-09-12 |
| Parámetros | 1 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@Seconds` | `decimal` |  |

## Quién la referencia

**3** objetos:

- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- =============================================
CREATE FUNCTION [dbo].[ConvertSecondsFormatoFecha]
(
@Seconds DECIMAL
)
RETURNS NVARCHAR(50)
AS
BEGIN

DECLARE @resultado NVARCHAR(50)
DECLARE @dias DECIMAL
DECLARE @segundos_sobrantes DECIMAL
DECLARE @horas AS INT
DECLARE @minutos AS INT
DECLARE @segundos AS INT

SELECT @dias = @Seconds / (24 * 60 * 60)
SELECT @segundos_sobrantes =ABS(@Seconds -(@dias* (24 * 60 * 60)))
SELECT @horas = @segundos_sobrantes  / (60.0 * 60.0)
SELECT @minutos = ((@segundos_sobrantes % (60.0 * 60.0)) / 60.0)
SELECT @segundos = (((@segundos_sobrantes) % (60.0 * 60.0)) % 60.0)

-- Declare the return variable here
SELECT @resultado = CONVERT(NVARCHAR(50),@dias) + ' ' + 
RIGHT('00'+ CONVERT(NVARCHAR(2),@horas),2) + ':' +
RIGHT('00'+ CONVERT(NVARCHAR(2),@minutos),2) + ':' +
RIGHT('00'+ CONVERT(NVARCHAR(2),@segundos),2);

RETURN @resultado;

END ;
```
