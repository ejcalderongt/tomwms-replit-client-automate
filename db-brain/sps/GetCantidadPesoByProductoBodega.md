---
id: db-brain-sp-getcantidadpesobyproductobodega
type: db-sp
title: dbo.GetCantidadPesoByProductoBodega
schema: dbo
name: GetCantidadPesoByProductoBodega
kind: sp
modify_date: 2016-06-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.GetCantidadPesoByProductoBodega`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2016-06-16 |
| Parámetros | 10 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdProductoBodega` | `int` |  |
| 2 | `@IdPresentacion` | `int` |  |
| 3 | `@IdUnidadMedida` | `int` |  |
| 4 | `@IdProductoEstado` | `int` |  |
| 5 | `@Lote` | `nvarchar(50)` |  |
| 6 | `@Aniada` | `int` |  |
| 7 | `@fecha_vence` | `nvarchar(50)` |  |
| 8 | `@fecha_manufactura` | `nvarchar(50)` |  |
| 9 | `@CantidadEnStock` | `float` | ✓ |
| 10 | `@PesoEnStock` | `float` | ✓ |

## Consume

- `PRODUCTO_BODEGA`
- `STOCK_REC`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[GetCantidadPesoByProductoBodega]
@IdProductoBodega int, 
@IdPresentacion int, 
@IdUnidadMedida int,
@IdProductoEstado int,
@Lote nvarchar(50) = null,
@Aniada int = null,
@fecha_vence nvarchar(50) = null,
@fecha_manufactura nvarchar(50) = null,
@CantidadEnStock float output, 
@PesoEnStock float output
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT @CantidadEnStock = 
(SELECT SUM(STOCK_REC.CANTIDAD) AS CANTIDAD
FROM STOCK_REC INNER JOIN 
PRODUCTO_BODEGA ON STOCK_REC.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProductoBodega
AND STOCK_REC.IdPresentacion=@IdPresentacion
AND STOCK_REC.IdUnidadMedida =@IdUnidadMedida
AND STOCK_REC.IdProductoEstado=@IdProductoEstado
AND STOCK_REC.lote=ISNULL(CAST(@Lote AS NVARCHAR(50)),STOCK_REC.Lote)
AND STOCK_REC.añada=ISNULL(@Aniada,STOCK_REC.añada)
AND STOCK_REC.fecha_vence=ISNULL(CONVERT(NVARCHAR,@fecha_vence,112),STOCK_REC.fecha_vence)
AND STOCK_REC.fecha_vence=ISNULL(CONVERT(NVARCHAR,@fecha_manufactura,112),STOCK_REC.fecha_manufactura))

SELECT @PesoEnStock = 
(SELECT SUM(STOCK_REC.PESO) AS PESO
FROM STOCK_REC INNER JOIN 
PRODUCTO_BODEGA ON STOCK_REC.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProductoBodega
AND STOCK_REC.IdPresentacion=@IdPresentacion
AND STOCK_REC.IdUnidadMedida =@IdUnidadMedida
AND STOCK_REC.IdProductoEstado=@IdProductoEstado
AND STOCK_REC.lote=ISNULL(CAST(@Lote AS NVARCHAR(50)),STOCK_REC.Lote)
AND STOCK_REC.añada=ISNULL(@Aniada,STOCK_REC.añada)
AND STOCK_REC.fecha_vence=ISNULL(CONVERT(NVARCHAR,@fecha_vence,112),STOCK_REC.fecha_vence)
AND STOCK_REC.fecha_vence=ISNULL(CONVERT(NVARCHAR,@fecha_manufactura,112),STOCK_REC.fecha_manufactura))

SELECT ISNULL(@CantidadEnStock,0)as CantidadEnStock,ISNULL(@PesoEnStock,0)as PesoEnStock

END
```
