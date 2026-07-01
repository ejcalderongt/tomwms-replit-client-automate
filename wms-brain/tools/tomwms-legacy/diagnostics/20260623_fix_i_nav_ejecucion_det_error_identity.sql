SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID('dbo.i_nav_ejecucion_det_error','U') IS NULL
BEGIN
    THROW 51000, 'La tabla dbo.i_nav_ejecucion_det_error no existe.', 1;
END
GO

IF EXISTS (
    SELECT 1
    FROM sys.identity_columns
    WHERE object_id = OBJECT_ID('dbo.i_nav_ejecucion_det_error')
      AND name = 'idejecuciondet'
)
BEGIN
    PRINT 'idejecuciondet ya es IDENTITY. No se requiere cambio.';
    RETURN;
END
GO

BEGIN TRY
    BEGIN TRAN;

    IF OBJECT_ID('dbo.i_nav_ejecucion_det_error_new', 'U') IS NOT NULL
        DROP TABLE dbo.i_nav_ejecucion_det_error_new;

    CREATE TABLE dbo.i_nav_ejecucion_det_error_new (
        idejecuciondet INT IDENTITY(1,1) NOT NULL,
        idejecucionenc INT NOT NULL,
        idnavconfigdet INT NOT NULL,
        [error] NVARCHAR(MAX) NULL,
        referencia NVARCHAR(255) NULL,
        fecha DATETIME NOT NULL,
        no_linea INT NOT NULL CONSTRAINT DF_i_nav_ejecucion_det_error_new_no_linea DEFAULT(0),
        codigo_producto NVARCHAR(100) NULL,
        umbas NVARCHAR(100) NULL,
        codigo_presentacion NVARCHAR(100) NULL,
        CONSTRAINT PK_i_nav_ejecucion_det_error_new PRIMARY KEY CLUSTERED (idejecuciondet)
    );

    SET IDENTITY_INSERT dbo.i_nav_ejecucion_det_error_new ON;

    DECLARE @sql NVARCHAR(MAX) = N'
        INSERT INTO dbo.i_nav_ejecucion_det_error_new (
            idejecuciondet,
            idejecucionenc,
            idnavconfigdet,
            [error],
            referencia,
            fecha,
            no_linea,
            codigo_producto,
            umbas,
            codigo_presentacion' +
            CASE WHEN COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'level_log') IS NOT NULL THEN N', level_log' ELSE N'' END +
            CASE WHEN COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'human_error') IS NOT NULL THEN N', human_error' ELSE N'' END + N'
        )
        SELECT
            idejecuciondet,
            idejecucionenc,
            idnavconfigdet,
            [error],
            referencia,
            fecha,
            no_linea,
            codigo_producto,
            umbas,
            codigo_presentacion' +
            CASE WHEN COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'level_log') IS NOT NULL THEN N', level_log' ELSE N'' END +
            CASE WHEN COL_LENGTH('dbo.i_nav_ejecucion_det_error', 'human_error') IS NOT NULL THEN N', human_error' ELSE N'' END + N'
        FROM dbo.i_nav_ejecucion_det_error WITH (HOLDLOCK, TABLOCKX);';

    EXEC sp_executesql @sql;

    SET IDENTITY_INSERT dbo.i_nav_ejecucion_det_error_new OFF;

    DECLARE @maxId INT = ISNULL((SELECT MAX(idejecuciondet) FROM dbo.i_nav_ejecucion_det_error_new), 0);
    DBCC CHECKIDENT ('dbo.i_nav_ejecucion_det_error_new', RESEED, @maxId) WITH NO_INFOMSGS;

    DROP TABLE dbo.i_nav_ejecucion_det_error;
    EXEC sp_rename 'dbo.i_nav_ejecucion_det_error_new', 'i_nav_ejecucion_det_error';

    COMMIT TRAN;
    PRINT 'Tabla i_nav_ejecucion_det_error convertida a IDENTITY correctamente.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
