/* #EJC20260520_RESERVA_BYB_FIX:
   Validacion de paridad para POST /api/sync/salidas/mi3/insertar.
   Cambiar @No si se genero otro documento con build-mi3-salida-ea153305-payload.ps1.
*/

DECLARE @No NVARCHAR(50) = N'EA-153305-WAPI01';

DECLARE @Pedido TABLE (
    IdPedidoEnc INT NOT NULL,
    Referencia NVARCHAR(50) NOT NULL
);

INSERT INTO @Pedido (IdPedidoEnc, Referencia)
SELECT IdPedidoEnc, Referencia
FROM trans_pe_enc
WHERE Referencia = @No;

SELECT
    p.IdPedidoEnc,
    p.Referencia,
    COUNT(d.IdPedidoDet) AS LineasPedido,
    SUM(d.Cantidad) AS TotalPedidoDet,
    SUM(ISNULL(x.CantidadReservadaStockRes, 0)) AS TotalStockRes
FROM @Pedido p
LEFT JOIN trans_pe_det d ON d.IdPedidoEnc = p.IdPedidoEnc
OUTER APPLY (
    SELECT SUM(sr.Cantidad) AS CantidadReservadaStockRes
    FROM stock_res sr
    WHERE sr.IdPedidoDet = d.IdPedidoDet
) x
GROUP BY p.IdPedidoEnc, p.Referencia;

SELECT
    d.No_linea,
    d.Codigo_Producto,
    d.Nombre_producto,
    d.Cantidad AS CantidadPedidoDet,
    d.Nom_unid_med,
    d.Nom_presentacion,
    d.IdPresentacion,
    COUNT(sr.IdStockRes) AS Reservas,
    SUM(ISNULL(sr.Cantidad, 0)) AS CantidadStockRes,
    SUM(CASE WHEN sr.IdStock IS NULL THEN 0 ELSE 1 END) AS ReservasConStock,
    SUM(CASE WHEN ISNULL(sr.IdStock, 0) = 0 THEN 1 ELSE 0 END) AS ReservasSinStock,
    MIN(sr.Fec_agr) AS PrimerReserva,
    MAX(sr.Fec_agr) AS UltimaReserva
FROM @Pedido p
INNER JOIN trans_pe_det d ON d.IdPedidoEnc = p.IdPedidoEnc
LEFT JOIN stock_res sr ON sr.IdPedidoDet = d.IdPedidoDet
GROUP BY
    d.No_linea,
    d.Codigo_Producto,
    d.Nombre_producto,
    d.Cantidad,
    d.Nom_unid_med,
    d.Nom_presentacion,
    d.IdPresentacion
ORDER BY d.No_linea;

SELECT
    det.Line_No,
    det.Item_No,
    det.Quantity AS CantidadInterface,
    det.Unit_of_Measure_Code,
    det.Variant_Code,
    det.Quantity_Reserved_WMS,
    det.Process_Result
FROM i_nav_ped_traslado_det det
WHERE det.NoEnc = @No
ORDER BY det.Line_No;
