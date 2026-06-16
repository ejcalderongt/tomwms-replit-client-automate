SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    IF COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'human_error') IS NOT NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_det_error
            DROP COLUMN human_error;
    END

    IF COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'level_log') IS NOT NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_det_error
            DROP COLUMN level_log;
    END

    IF COL_LENGTH('dbo.i_nav_ejecucion_res', 'tipo_documento_sap') IS NOT NULL
    BEGIN
        ALTER TABLE dbo.i_nav_ejecucion_res
            DROP COLUMN tipo_documento_sap;
    END

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;

    THROW;
END CATCH;