---
id: db-brain-sp-sp-stock-jornada-desfase
type: db-sp
title: dbo.SP_STOCK_JORNADA_DESFASE
schema: dbo
name: SP_STOCK_JORNADA_DESFASE
kind: sp
modify_date: 2022-10-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SP_STOCK_JORNADA_DESFASE`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2022-10-25 |
| Parámetros | 3 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@Fecha_Inicial` | `date` |  |
| 2 | `@Fecha_Final` | `date` |  |
| 3 | `@RegistrosARevisar` | `int` | ✓ |

## Consume

- `stock_jornada`
- `stock_jornada_consecutivo`
- `stock_jornada_desfase`
- `stock_jornada_fecha_consecutiva`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- =============================================
-- Author:        Carolina Fuentes
-- Create date:   17-10-2022
-- Description:   Obtener los registros que no existen en Stock Jornada, se agregó rango de fecha
-- =============================================
CREATE PROCEDURE [dbo].[sp_stock_jornada_desfase]
-- Add the parameters for the stored procedure here
   @Fecha_Inicial AS DATE,
   @Fecha_Final AS DATE,
   @RegistrosARevisar  AS INT OUTPUT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

	IF EXISTS (SELECT * FROM sys.objects WHERE name = 'stock_jornada_consecutivo' AND type = 'U') DROP TABLE stock_jornada_consecutivo
	IF EXISTS (SELECT * FROM sys.objects WHERE name = 'stock_jornada_desfase' AND type = 'U') DROP TABLE stock_jornada_desfase
	IF EXISTS (SELECT * FROM sys.objects WHERE name = 'stock_jornada_fecha_consecutiva' AND type = 'U') DROP TABLE stock_jornada_fecha_consecutiva
	IF EXISTS (SELECT * FROM sys.objects WHERE name = 'stock_jornada_desface' AND type = 'U') DROP TABLE stock_jornada_desface

	  SELECT ROW_NUMBER() OVER(PARTITION BY lic_plate ORDER BY fecha ASC) AS consecutivo, 
	  lic_plate, 
	  fecha, 
	  idstock, 
	  idjornadasistema, 
	  idbodega, 
	  idtickettms
	  INTO  stock_jornada_consecutivo
	  FROM stock_jornada 
	  WHERE Fecha between  @Fecha_Inicial and @Fecha_Final

	  SELECT *, DATEADD(DAY, consecutivo-1,
			 (SELECT MIN(fecha) FROM stock_jornada_consecutivo S WHERE  S.lic_plate = stock_jornada_consecutivo.lic_plate)) AS fecha_consecutiva,
			 (SELECT MIN(fecha) FROM stock_jornada_consecutivo S WHERE  S.lic_plate = stock_jornada_consecutivo.lic_plate) AS min_fecha,
			 (SELECT MAX(fecha) FROM stock_jornada_consecutivo S WHERE  S.lic_plate = stock_jornada_consecutivo.lic_plate) AS max_fecha,
			 (SELECT MAX(consecutivo) FROM stock_jornada_consecutivo S WHERE  S.lic_plate = stock_jornada_consecutivo.lic_plate) AS max_consecutivo
		INTO  stock_jornada_fecha_consecutiva
		FROM stock_jornada_consecutivo
		WHERE DATEDIFF(DAY,DATEADD(DAY, consecutivo-1, fecha), fecha)<>0

	   INSERT INTO stock_jornada_fecha_consecutiva
		SELECT consecutivo+1, lic_plate, fecha, IdStock, IdJornadaSistema, IdBodega, IdTicketTMS, DATEADD(DAY, max_consecutivo,min_fecha) fecha_consecutiva, min_fecha, max_fecha, max_consecutivo
		FROM stock_jornada_fecha_consecutiva
		WHERE  consecutivo = max_consecutivo

	   SELECT DF.*
		INTO stock_jornada_desfase
		FROM stock_jornada_fecha_consecutiva DF
		WHERE NOT EXISTS (SELECT * FROM stock_jornada D WHERE D.lic_plate = DF.lic_plate AND D.fecha = DF.fecha_consecutiva)
		AND DF.max_fecha >=DF.fecha_consecutiva
		ORDER BY lic_plate, fecha_consecutiva

	  SELECT @RegistrosARevisar  =(SELECT COUNT(*) FROM stock_jornada_desfase)
	  
	  RETURN @RegistrosARevisar
 
END
```
