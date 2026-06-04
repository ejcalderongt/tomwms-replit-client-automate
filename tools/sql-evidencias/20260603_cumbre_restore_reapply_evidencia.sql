/*
#EJC20260603_CUMBRE_RESTORE_REAPPLY
Evidencia de reaplicacion de objetos requeridos despues de restore (2026-05-28)
Ambiente objetivo: 52.41.114.122,1437 / TOMWMS_LA_CUMBRE_QA

Scripts aplicados fuera de este archivo:
1) tools/sql-deliverables/20260602_verificacion_views.sql
2) tools/sql-deliverables/20260602_verificacion_detallado.sql
3) tools/stock-en-fecha/001_optimize_stock_en_fecha.sql

En este archivo:
- Reaplica objetos de configuracion/sync si no existen.
- Deja validaciones de existencia para evidencia.
*/

SET NOCOUNT ON;
GO

/* =========================================================
   1) i_nav_config_enc.enviar_ingreso_sap_via_ws
   ========================================================= */
IF COL_LENGTH('dbo.i_nav_config_enc', 'enviar_ingreso_sap_via_ws') IS NULL
BEGIN
    ALTER TABLE dbo.i_nav_config_enc
        ADD enviar_ingreso_sap_via_ws bit NOT NULL
            CONSTRAINT DF_i_nav_config_enc_enviar_ingreso_sap_via_ws DEFAULT (0);
END
GO

/* =========================================================
   2) i_nav_sync_request (cola de ejecucion worker WMS)
   ========================================================= */
IF OBJECT_ID('dbo.i_nav_sync_request', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.i_nav_sync_request (
        idsyncrequest bigint IDENTITY(1,1) NOT NULL,
        idnavconfigenc int NOT NULL,
        idempresa int NOT NULL,
        idbodega int NOT NULL,
        idusuario int NOT NULL,
        tipo_interface int NOT NULL,
        origen varchar(30) NOT NULL,
        estado varchar(20) NOT NULL,
        parametros nvarchar(max) NULL,
        fecha_solicitud datetime NOT NULL
            CONSTRAINT DF_i_nav_sync_request_fecha_solicitud DEFAULT (GETDATE()),
        fecha_inicio datetime NULL,
        fecha_fin datetime NULL,
        intento int NOT NULL
            CONSTRAINT DF_i_nav_sync_request_intento DEFAULT (0),
        mensaje nvarchar(1000) NULL,
        host_solicita varchar(100) NULL,
        host_procesa varchar(100) NULL,
        CONSTRAINT PK_i_nav_sync_request PRIMARY KEY CLUSTERED (idsyncrequest)
    );
END
GO

/* =========================================================
   3) Indices de trabajo para cola (lectura por estado/activa)
   ========================================================= */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('dbo.i_nav_sync_request')
      AND name = 'IX_i_nav_sync_request_estado_fecha'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_i_nav_sync_request_estado_fecha
        ON dbo.i_nav_sync_request (estado, fecha_solicitud, idsyncrequest);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('dbo.i_nav_sync_request')
      AND name = 'IX_i_nav_sync_request_activa'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_i_nav_sync_request_activa
        ON dbo.i_nav_sync_request (idnavconfigenc, tipo_interface, estado, idsyncrequest);
END
GO

/* =========================================================
   4) Validaciones de evidencia
   ========================================================= */
SELECT
    @@SERVERNAME AS servidor,
    DB_NAME() AS base_datos,
    GETDATE() AS fecha_ejecucion;
GO

SELECT
    CASE WHEN COL_LENGTH('dbo.i_nav_config_enc', 'enviar_ingreso_sap_via_ws') IS NULL THEN 0 ELSE 1 END AS ok_columna_enviar_ingreso_sap_via_ws,
    CASE WHEN OBJECT_ID('dbo.i_nav_sync_request', 'U') IS NULL THEN 0 ELSE 1 END AS ok_tabla_i_nav_sync_request,
    CASE WHEN OBJECT_ID('dbo.VW_Verificacion', 'V') IS NULL THEN 0 ELSE 1 END AS ok_vw_verificacion,
    CASE WHEN OBJECT_ID('dbo.VW_Verificacion_Detallado_Sin_Licencia', 'V') IS NULL THEN 0 ELSE 1 END AS ok_vw_verificacion_detallado,
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Movimientos', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_stock_en_fecha_movimientos,
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Snapshot', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_stock_en_fecha_snapshot,
    CASE WHEN OBJECT_ID('dbo.usp_Reporte_StockEnFecha_Consolidado', 'P') IS NULL THEN 0 ELSE 1 END AS ok_sp_stock_en_fecha_consolidado;
GO

SELECT
    i.name AS indice,
    OBJECT_NAME(i.object_id) AS tabla
FROM sys.indexes i
WHERE i.object_id = OBJECT_ID('dbo.i_nav_sync_request')
  AND i.name IN ('IX_i_nav_sync_request_estado_fecha', 'IX_i_nav_sync_request_activa')
ORDER BY i.name;
GO
