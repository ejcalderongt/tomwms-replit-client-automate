/* Diagnostico de integridad MI3 reserva por documento.
   Cambiar @No por el documento probado.

   Objetivo:
   - Ver cantidades solicitadas en trans_pe_det vs reservas reales en stock_res.
   - Mostrar motivo de interface para lineas sin reserva o parciales.
   - Evitar falsos positivos por productos repetidos: stock_res se une por IdPedidoDet.
*/

DECLARE @No NVARCHAR(50) = N'EA153305O203916';

IF OBJECT_ID('tempdb..#lineas_mi3_reserva_diag') IS NOT NULL
    DROP TABLE #lineas_mi3_reserva_diag;

SELECT *
INTO #lineas_mi3_reserva_diag
FROM (
    SELECT
        e.IdPedidoEnc,
        e.Referencia,
        d.IdPedidoDet,
        d.No_linea,
        d.Codigo_Producto,
        d.Cantidad AS PedidoDetCantidad,
        d.IdPresentacion,
        d.Nom_presentacion,
        ISNULL(pp.factor, 1) AS Factor,
        intf.Quantity AS InterfaceQuantity,
        intf.Variant_Code,
        intf.Quantity_Reserved_WMS,
        intf.Status AS InterfaceStatus,
        ISNULL(intf.Process_Result, '') AS Process_Result,
        COUNT(sr.IdStockRes) AS Reservas,
        SUM(ISNULL(sr.Cantidad, 0)) AS StockResCantidad
    FROM trans_pe_enc e
    INNER JOIN trans_pe_det d ON d.IdPedidoEnc = e.IdPedidoEnc
    LEFT JOIN producto_presentacion pp ON pp.IdPresentacion = d.IdPresentacion
    LEFT JOIN stock_res sr
        ON sr.IdPedidoDet = d.IdPedidoDet
       AND sr.IdTransaccion = e.IdPedidoEnc
    LEFT JOIN i_nav_ped_traslado_det intf
        ON intf.NoEnc = e.Referencia
       AND intf.Line_No = d.No_linea
    WHERE e.Referencia = @No
    GROUP BY
        e.IdPedidoEnc,
        e.Referencia,
        d.IdPedidoDet,
        d.No_linea,
        d.Codigo_Producto,
        d.Cantidad,
        d.IdPresentacion,
        d.Nom_presentacion,
        pp.factor,
        intf.Quantity,
        intf.Variant_Code,
        intf.Quantity_Reserved_WMS,
        intf.Status,
        intf.Process_Result
) lineas;

SELECT
    No_linea,
    Codigo_Producto,
    PedidoDetCantidad,
    IdPresentacion,
    Nom_presentacion,
    Factor,
    InterfaceQuantity,
    Variant_Code,
    Quantity_Reserved_WMS,
    Reservas,
    StockResCantidad,
    CASE
        WHEN Reservas = 0 THEN 'SIN_RESERVA'
        WHEN IdPresentacion > 0 AND (StockResCantidad / NULLIF(Factor, 0)) + 0.001 < InterfaceQuantity THEN 'PARCIAL'
        WHEN IdPresentacion = 0 AND StockResCantidad + 0.001 < PedidoDetCantidad THEN 'PARCIAL'
        ELSE 'OK'
    END AS EstadoAuditoria,
    Process_Result
FROM #lineas_mi3_reserva_diag
ORDER BY No_linea;

SELECT
    No_linea,
    Codigo_Producto,
    InterfaceQuantity,
    Quantity_Reserved_WMS,
    Reservas,
    StockResCantidad,
    CASE
        WHEN Process_Result LIKE '%TIPO_NO_RESERVA=%'
            THEN SUBSTRING(Process_Result, CHARINDEX('TIPO_NO_RESERVA=', Process_Result), 2000)
        WHEN Reservas > 0
            THEN CONCAT('PARCIAL_CON_STOCK_REAL: reservado=', StockResCantidad, ', solicitado=', InterfaceQuantity, ' ', Variant_Code)
        WHEN Process_Result <> ''
            THEN Process_Result
        ELSE '(sin motivo registrado)'
    END AS Motivo
FROM #lineas_mi3_reserva_diag
WHERE Reservas = 0
   OR (IdPresentacion > 0 AND (StockResCantidad / NULLIF(Factor, 0)) + 0.001 < InterfaceQuantity)
   OR (IdPresentacion = 0 AND StockResCantidad + 0.001 < PedidoDetCantidad)
ORDER BY No_linea;

DROP TABLE #lineas_mi3_reserva_diag;
