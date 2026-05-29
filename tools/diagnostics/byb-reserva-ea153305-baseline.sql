/*
    #EJC20260520_RESERVA_BYB_FIX:
    Baseline reproducible para pedido BYB EA-153305 / IdPedidoEnc 37320.

    Objetivo:
    - Comparar el resultado real de stock_res contra las lineas del payload MI3.
    - Mantener como punto de retorno las lineas que ya reservaron bien.
    - Enfocar nuevas correcciones solo en lineas pendientes/fallidas.

    Resultado de la corrida analizada en:
    C:\Users\yejc2\Downloads\Erores_WMS_140526_media

    Resumen trazas:
    - 63 payloads.
    - 59 OK.
    - 4 FALSE.
    - 00444100 lineas 300000 y 600000 fallan por CHECK stock_res.cantidad > 0,
      aunque las trazas reportan stock en picking suficiente.
*/

SET NOCOUNT ON;

DECLARE @IdPedidoEnc INT = 37320;
DECLARE @NoDocumento NVARCHAR(50) = N'EA-153305';

IF OBJECT_ID('tempdb..#PayloadMi3') IS NOT NULL DROP TABLE #PayloadMi3;

CREATE TABLE #PayloadMi3(
    NoLinea INT NOT NULL,
    Codigo NVARCHAR(50) NOT NULL,
    CantidadPayload FLOAT NOT NULL,
    VariantCode NVARCHAR(50) NULL,
    CantidadSolicitudWms FLOAT NULL,
    IdPresentacionSolicitud INT NULL,
    ResultadoTrace NVARCHAR(20) NOT NULL,
    MotivoEsperado NVARCHAR(300) NULL
);

INSERT INTO #PayloadMi3(NoLinea, Codigo, CantidadPayload, VariantCode, CantidadSolicitudWms, IdPresentacionSolicitud, ResultadoTrace, MotivoEsperado)
VALUES
    (10000,  N'00020502', 10,   N'CJ', 10,  7,   N'FALSE', N'Pendiente previo a este corte; revisar aparte para no mezclar con 00444100.'),
    (20000,  N'00140702', 125,  N'CJ', 125, 291, N'FALSE', N'Pendiente previo a este corte; revisar aparte para no mezclar con 00444100.'),
    (300000, N'00444100', 7.75, N'CJ', 31,  0,   N'FALSE', N'Falla por CHECK stock_res.cantidad > 0 durante insert; no es falta de stock.'),
    (600000, N'00444100', 7.25, N'CJ', 29,  0,   N'FALSE', N'Falla por CHECK stock_res.cantidad > 0 durante insert; no es falta de stock.');

PRINT '1) Lineas pendientes segun payload trace vs detalle actual';

SELECT
    p.NoLinea,
    p.Codigo,
    p.CantidadPayload,
    p.VariantCode,
    p.CantidadSolicitudWms,
    p.IdPresentacionSolicitud,
    p.ResultadoTrace,
    d.IdPedidoDet,
    d.Cantidad AS CantidadPedidoActual,
    d.IdPresentacion AS IdPresentacionPedidoActual,
    ISNULL(SUM(sr.cantidad), 0) AS CantidadReservadaActual,
    COUNT(sr.IdStockRes) AS RegistrosStockRes,
    CASE
        WHEN d.IdPedidoDet IS NULL THEN 'NO_EXISTE_DETALLE_ACTUAL'
        WHEN ISNULL(SUM(sr.cantidad), 0) = p.CantidadSolicitudWms THEN 'RESERVA_MATCH'
        WHEN ISNULL(SUM(sr.cantidad), 0) > 0 THEN 'RESERVA_PARCIAL'
        ELSE 'SIN_RESERVA'
    END AS EstadoActual,
    p.MotivoEsperado
FROM #PayloadMi3 p
LEFT JOIN trans_pe_det d
    ON d.IdPedidoEnc = @IdPedidoEnc
   AND d.no_linea = p.NoLinea
   AND d.codigo_producto = p.Codigo
LEFT JOIN stock_res sr
    ON sr.IdPedidoDet = d.IdPedidoDet
GROUP BY
    p.NoLinea,
    p.Codigo,
    p.CantidadPayload,
    p.VariantCode,
    p.CantidadSolicitudWms,
    p.IdPresentacionSolicitud,
    p.ResultadoTrace,
    d.IdPedidoDet,
    d.Cantidad,
    d.IdPresentacion,
    p.MotivoEsperado
ORDER BY p.NoLinea;

PRINT '2) Baseline de lineas que si quedaron reservadas en stock_res para el pedido';

SELECT
    d.no_linea,
    d.codigo_producto,
    d.nombre_producto,
    d.Cantidad AS CantidadPedido,
    d.IdPresentacion AS IdPresentacionPedido,
    COUNT(sr.IdStockRes) AS RegistrosStockRes,
    ISNULL(SUM(sr.cantidad), 0) AS CantidadReservada,
    CASE
        WHEN ISNULL(SUM(sr.cantidad), 0) = d.Cantidad THEN 'OK'
        WHEN ISNULL(SUM(sr.cantidad), 0) > 0 THEN 'PARCIAL'
        ELSE 'SIN_RESERVA'
    END AS EstadoReserva
FROM trans_pe_det d
LEFT JOIN stock_res sr
    ON sr.IdPedidoDet = d.IdPedidoDet
WHERE d.IdPedidoEnc = @IdPedidoEnc
GROUP BY
    d.no_linea,
    d.codigo_producto,
    d.nombre_producto,
    d.Cantidad,
    d.IdPresentacion
ORDER BY d.no_linea;

PRINT '3) Detalle de stock_res por linea actual';

SELECT
    d.no_linea,
    d.codigo_producto,
    sr.IdStockRes,
    sr.IdStock,
    sr.IdUbicacion,
    (u.descripcion + ' - #' + CAST(u.IdUbicacion AS varchar(20))) AS Ubicacion,
    sr.lote,
    sr.lic_plate,
    sr.cantidad,
    sr.IdPresentacion,
    sr.IdUnidadMedida,
    sr.fecha_vence,
    sr.fec_agr
FROM stock_res sr
INNER JOIN trans_pe_det d
    ON d.IdPedidoDet = sr.IdPedidoDet
LEFT JOIN bodega_ubicacion u
    ON u.IdUbicacion = sr.IdUbicacion
WHERE sr.IdPedido = @IdPedidoEnc
ORDER BY d.no_linea, sr.IdStockRes;

PRINT '4) Existencia disponible para 00444100; confirma que el pendiente no es por falta de stock';

SELECT
    s.IdStock,
    s.IdUbicacion,
    (u.descripcion + ' - #' + CAST(u.IdUbicacion AS varchar(20))) AS Ubicacion,
    u.ubicacion_picking,
    u.nivel,
    s.lote,
    s.lic_plate,
    s.cantidad,
    s.IdPresentacion,
    s.IdUnidadMedida,
    s.fecha_vence,
    ISNULL(r.Reservado, 0) AS Reservado,
    s.cantidad - ISNULL(r.Reservado, 0) AS DisponibleCalculado
FROM stock s
INNER JOIN Producto_bodega pb
    ON pb.IdProductoBodega = s.IdProductoBodega
INNER JOIN Producto pr
    ON pr.IdProducto = pb.IdProducto
LEFT JOIN bodega_ubicacion u
    ON u.IdUbicacion = s.IdUbicacion
OUTER APPLY (
    SELECT SUM(sr.cantidad) AS Reservado
    FROM stock_res sr
    WHERE sr.IdStock = s.IdStock
) r
WHERE pr.codigo = N'00444100'
  AND s.activo = 1
  AND s.IdBodega = 2
  AND s.IdProductoEstado = 1
ORDER BY s.fecha_vence, u.ubicacion_picking DESC, s.IdStock;

PRINT '5) Definicion del constraint que dispara el fallo 00444100';

SELECT
    cc.name AS ConstraintName,
    cc.definition AS Definition
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID('dbo.stock_res')
  AND cc.name = 'Cons_restriccion';
