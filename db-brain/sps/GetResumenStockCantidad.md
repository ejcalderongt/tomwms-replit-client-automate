---
id: db-brain-sp-getresumenstockcantidad
type: db-sp
title: dbo.GetResumenStockCantidad
schema: dbo
name: GetResumenStockCantidad
kind: sp
modify_date: 2017-10-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.GetResumenStockCantidad`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2017-10-02 |
| Parámetros | 6 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdProducto` | `int` |  |
| 2 | `@IdPresentacion` | `int` |  |
| 3 | `@IdUnidadMedida` | `int` |  |
| 4 | `@IdProductoEstado` | `int` |  |
| 5 | `@CantidadEnStock` | `float` | ✓ |
| 6 | `@PesoEnStock` | `float` | ✓ |

## Consume

- `PRODUCTO_BODEGA`
- `STOCK`
- `stock_res`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[GetResumenStockCantidad]
@IdProducto int, 
@IdPresentacion int, 
@IdUnidadMedida int,
@IdProductoEstado int,
@CantidadEnStock float output, 
@PesoEnStock float output
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

declare @CantidadEnStockReservado float
declare @PesoEnStockReservado float

--Busca cuanto hay en reserva

SELECT @CantidadEnStockReservado = 
(SELECT SUM(stock_res.CANTIDAD) AS CANTIDAD
FROM stock_res INNER JOIN 
PRODUCTO_BODEGA ON stock_res.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProducto
AND (stock_res.IdPresentacion IS NULL OR stock_res.IdPresentacion =@IdPresentacion)
AND stock_res.IdUnidadMedida =@IdUnidadMedida
AND stock_res.IdEstado=@IdProductoEstado)

SELECT @PesoEnStockReservado = 
(SELECT SUM(stock_res.PESO) AS PESO
FROM stock_res INNER JOIN 
PRODUCTO_BODEGA ON stock_res.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProducto
AND (stock_res.IdPresentacion IS NULL OR stock_res.IdPresentacion=@IdPresentacion)
AND stock_res.IdUnidadMedida =@IdUnidadMedida
AND stock_res.IdEstado=@IdProductoEstado)

--Busca cuando hay real

SELECT @CantidadEnStock = 
(SELECT SUM(STOCK.CANTIDAD) AS CANTIDAD
FROM STOCK INNER JOIN 
PRODUCTO_BODEGA ON STOCK.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProducto
AND (STOCK.IdPresentacion IS NULL OR STOCK.IdPresentacion =@IdPresentacion)
AND STOCK.IdUnidadMedida =@IdUnidadMedida
AND STOCK.IdProductoEstado=@IdProductoEstado)

SELECT @CantidadEnStock-= ISNULL(@CantidadEnStockReservado,0)

SELECT @PesoEnStock = 
(SELECT SUM(STOCK.PESO) AS PESO
FROM STOCK INNER JOIN 
PRODUCTO_BODEGA ON STOCK.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA 
WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProducto
AND (STOCK.IdPresentacion IS NULL OR STOCK.IdPresentacion=@IdPresentacion)
AND STOCK.IdUnidadMedida =@IdUnidadMedida
AND STOCK.IdProductoEstado=@IdProductoEstado)

SELECT @PesoEnStock -=ISNULL(@PesoEnStockReservado,0)

SELECT isnull(@CantidadEnStock,0)as CantidadEnStock,isnull(@PesoEnStock,0)as PesoEnStock

END
```
