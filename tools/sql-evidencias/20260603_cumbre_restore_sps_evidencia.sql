/*
#EJC20260603_CUMBRE_RESTORE_SPS_EVIDENCIA
Evidencia puntual de SPs requeridos para Stock en una fecha.
Ambiente: 52.41.114.122,1437 / TOMWMS_LA_CUMBRE_QA
*/

SET NOCOUNT ON;
GO

SELECT
    @@SERVERNAME AS servidor,
    DB_NAME() AS base_datos,
    GETDATE() AS fecha_ejecucion;
GO

SELECT
    p.name AS sp_nombre,
    p.create_date,
    p.modify_date
FROM sys.procedures p
WHERE p.name IN (
    'usp_Reporte_StockEnFecha_Snapshot',
    'usp_Reporte_StockEnFecha_Movimientos',
    'usp_Reporte_StockEnFecha_Consolidado'
)
ORDER BY p.name;
GO

SELECT
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Snapshot', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_snapshot,
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Movimientos', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_movimientos,
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Consolidado', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_consolidado;
GO
