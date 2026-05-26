/*
    HH read-model: stock por ubicacion.
    Mantiene el mismo contrato de columnas que VW_Stock_Res para no romper TOMHHWS.
*/
CREATE OR ALTER PROCEDURE dbo.usp_wms_hh_stock_by_ubicacion_v1
    @IdUbicacion INT,
    @IdBodega INT
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT *
    FROM dbo.VW_Stock_Res
    WHERE IdUbicacion = @IdUbicacion
      AND IdBodega = @IdBodega;
END;
GO
