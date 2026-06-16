SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    IF COL_LENGTH('dbo.i_nav_ejecucion_res', 'tipo_documento_sap') IS NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_res
            ADD tipo_documento_sap NVARCHAR(50) NULL;
    END

    IF COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'level_log') IS NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_det_error
            ADD level_log NVARCHAR(25) NULL;
    END

    IF COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'human_error') IS NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_det_error
            ADD human_error NVARCHAR(1000) NULL;
    END

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;

    THROW;
END CATCH;