---
id: db-brain-sp-getlistastockbyproductobodega
type: db-sp
title: dbo.GetListaStockByProductoBodega
schema: dbo
name: GetListaStockByProductoBodega
kind: sp
modify_date: 2016-06-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.GetListaStockByProductoBodega`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2016-06-17 |
| Parámetros | 9 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdProductoBodega` | `int` |  |
| 2 | `@IdPresentacion` | `int` |  |
| 3 | `@IdUnidadMedida` | `int` |  |
| 4 | `@IdProductoEstado` | `int` |  |
| 5 | `@Lote` | `nvarchar(50)` |  |
| 6 | `@Aniada` | `int` |  |
| 7 | `@fecha_vence` | `datetime` |  |
| 8 | `@fecha_manufactura` | `datetime` |  |
| 9 | `@DiasVencimientoCliente` | `int` |  |

## Consume

- `PRODUCTO_BODEGA`
- `STOCK`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[GetListaStockByProductoBodega]
@IdProductoBodega int, 
@IdPresentacion int, 
@IdUnidadMedida int,
@IdProductoEstado int,
@Lote nvarchar(50) = null,
@Aniada int = null,
@fecha_vence datetime = null,
@fecha_manufactura datetime = null,
@DiasVencimientoCliente int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

IF @DiasVencimientoCliente = 0 

BEGIN

	SELECT * FROM STOCK INNER JOIN 
	PRODUCTO_BODEGA ON STOCK.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
	WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProductoBodega
	AND STOCK.IdPresentacion=@IdPresentacion
	AND STOCK.IdUnidadMedida =@IdUnidadMedida
	AND STOCK.IdProductoEstado=@IdProductoEstado
	AND STOCK.lote=ISNULL(CAST(@Lote AS NVARCHAR(50)),STOCK.Lote)
	AND STOCK.añada=ISNULL(@Aniada,STOCK.añada)
	AND STOCK.fecha_vence=@fecha_vence
	AND STOCK.fecha_vence=@fecha_manufactura

END

ELSE

	SELECT * FROM STOCK INNER JOIN 
	PRODUCTO_BODEGA ON STOCK.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
	WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProductoBodega
	AND STOCK.IdPresentacion=@IdPresentacion
	AND STOCK.IdUnidadMedida =@IdUnidadMedida
	AND STOCK.IdProductoEstado=@IdProductoEstado
	AND STOCK.lote=ISNULL(CAST(@Lote AS NVARCHAR(50)),STOCK.Lote)
	AND STOCK.añada=ISNULL(@Aniada,STOCK.añada)
	AND STOCK.fecha_vence=@fecha_vence
	AND STOCK.fecha_vence=@fecha_manufactura
	AND DATEDIFF (DAY,GETDATE(),STOCK.fecha_vence) >=@DiasVencimientoCliente

END
```
