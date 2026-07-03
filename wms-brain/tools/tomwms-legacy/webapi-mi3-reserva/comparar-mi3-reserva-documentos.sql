/* Comparacion de paridad para optimizaciones MI3 reserva.
   Usar despues de liberar stock y correr el mismo payload EA-153305 contra
   documento baseline y documento optimizado.
*/

DECLARE @BaselineNo NVARCHAR(50) = N'EA-153305-WAPI01';
DECLARE @OptimizedNo NVARCHAR(50) = N'EA153305OPT';

WITH resumen AS (
    SELECT
        e.Referencia,
        d.No_linea,
        d.Codigo_Producto,
        d.Cantidad AS CantidadPedidoDet,
        d.IdPresentacion,
        d.Nom_presentacion,
        COUNT(sr.IdStockRes) AS Reservas,
        SUM(ISNULL(sr.Cantidad, 0)) AS CantidadStockRes
    FROM trans_pe_enc e
    INNER JOIN trans_pe_det d ON d.IdPedidoEnc = e.IdPedidoEnc
    LEFT JOIN stock_res sr ON sr.IdPedidoDet = d.IdPedidoDet
    WHERE e.Referencia IN (@BaselineNo, @OptimizedNo)
    GROUP BY
        e.Referencia,
        d.No_linea,
        d.Codigo_Producto,
        d.Cantidad,
        d.IdPresentacion,
        d.Nom_presentacion
)
SELECT
    COALESCE(b.No_linea, o.No_linea) AS No_linea,
    COALESCE(b.Codigo_Producto, o.Codigo_Producto) AS Codigo_Producto,
    b.CantidadPedidoDet AS BaselinePedidoDet,
    o.CantidadPedidoDet AS OptimizedPedidoDet,
    b.CantidadStockRes AS BaselineStockRes,
    o.CantidadStockRes AS OptimizedStockRes,
    b.Reservas AS BaselineReservas,
    o.Reservas AS OptimizedReservas,
    CASE
        WHEN ISNULL(b.CantidadPedidoDet, -999999) <> ISNULL(o.CantidadPedidoDet, -999999) THEN 'DIF_PEDIDO_DET'
        WHEN ISNULL(b.CantidadStockRes, -999999) <> ISNULL(o.CantidadStockRes, -999999) THEN 'DIF_STOCK_RES'
        WHEN ISNULL(b.Reservas, -1) <> ISNULL(o.Reservas, -1) THEN 'DIF_RESERVAS'
        ELSE 'OK'
    END AS Comparacion
FROM resumen b
FULL OUTER JOIN resumen o
    ON o.No_linea = b.No_linea
   AND o.Referencia = @OptimizedNo
   AND b.Referencia = @BaselineNo
WHERE (b.Referencia = @BaselineNo OR b.Referencia IS NULL)
  AND (o.Referencia = @OptimizedNo OR o.Referencia IS NULL)
ORDER BY COALESCE(b.No_linea, o.No_linea);

SELECT
    e.Referencia,
    COUNT(DISTINCT d.IdPedidoDet) AS Lineas,
    COUNT(sr.IdStockRes) AS Reservas,
    SUM(d.Cantidad) AS TotalPedidoDet,
    SUM(ISNULL(sr.Cantidad, 0)) AS TotalStockRes
FROM trans_pe_enc e
LEFT JOIN trans_pe_det d ON d.IdPedidoEnc = e.IdPedidoEnc
LEFT JOIN stock_res sr ON sr.IdPedidoDet = d.IdPedidoDet
WHERE e.Referencia IN (@BaselineNo, @OptimizedNo)
GROUP BY e.Referencia
ORDER BY e.Referencia;
