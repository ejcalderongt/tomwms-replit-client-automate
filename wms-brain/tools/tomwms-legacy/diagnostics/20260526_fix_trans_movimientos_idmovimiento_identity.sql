/*
 #EJC20260526
 Objetivo:
   Migrar dbo.trans_movimientos.IdMovimiento a IDENTITY(1,1) preservando
   todos los valores históricos de IdMovimiento.

 Ambiente validado:
   Server   = LAPTOP-5GDJFUCN\PROGRAX
   Database = TOMWMS_MAMPA_QA

 Notas:
   - Script orientado a ventana de mantenimiento.
   - Conserva tabla backup: dbo.trans_movimientos__pre_identity_20260526
   - No elimina automáticamente el backup.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    IF OBJECT_ID('dbo.trans_movimientos', 'U') IS NULL
        THROW 51000, 'No existe dbo.trans_movimientos.', 1;

    IF EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID('dbo.trans_movimientos')
          AND name = 'IdMovimiento'
          AND is_identity = 1
    )
        THROW 51000, 'IdMovimiento ya es IDENTITY. Script no requerido.', 1;

    DECLARE @rows_before BIGINT = (SELECT COUNT_BIG(*) FROM dbo.trans_movimientos);
    DECLARE @max_before INT = (SELECT ISNULL(MAX(IdMovimiento), 0) FROM dbo.trans_movimientos);

    IF OBJECT_ID('dbo.trans_movimientos_new_20260526', 'U') IS NOT NULL
        DROP TABLE dbo.trans_movimientos_new_20260526;

    CREATE TABLE dbo.trans_movimientos_new_20260526 (
        IdMovimiento         INT IDENTITY(1,1) NOT NULL,
        IdEmpresa            INT NOT NULL,
        IdBodegaOrigen       INT NOT NULL,
        IdTransaccion        INT NOT NULL,
        IdPropietarioBodega  INT NULL,
        IdProductoBodega     INT NULL,
        IdUbicacionOrigen    INT NULL,
        IdUbicacionDestino   INT NULL,
        IdPresentacion       INT NULL,
        IdEstadoOrigen       INT NULL,
        IdEstadoDestino      INT NULL,
        IdUnidadMedida       INT NULL,
        IdTipoTarea          INT NULL,
        IdBodegaDestino      INT NULL,
        IdRecepcion          INT NULL,
        cantidad             FLOAT NULL,
        serie                NVARCHAR(50) NULL,
        peso                 FLOAT NULL,
        lote                 NVARCHAR(50) NULL,
        fecha_vence          DATETIME NULL,
        fecha                DATETIME NULL,
        barra_pallet         NVARCHAR(50) NULL,
        hora_ini             DATETIME NULL,
        hora_fin             DATETIME NULL,
        fecha_agr            DATETIME NULL,
        usuario_agr          NVARCHAR(25) NULL,
        cantidad_hist        FLOAT NULL,
        peso_hist            FLOAT NULL,
        lic_plate            NVARCHAR(100) NULL,
        IdOperadorBodega     INT NULL CONSTRAINT DF_trans_movimientos_new_IdOperadorBodega DEFAULT ((0)),
        IdRecepcionDet       INT NULL CONSTRAINT DF_trans_movimientos_new_IdRecepcionDet DEFAULT ((0)),
        IdPedidoEnc          INT NULL CONSTRAINT DF_trans_movimientos_new_IdPedidoEnc DEFAULT ((0)),
        IdPedidoDet          INT NULL CONSTRAINT DF_trans_movimientos_new_IdPedidoDet DEFAULT ((0)),
        IdDespachoEnc        INT NULL CONSTRAINT DF_trans_movimientos_new_IdDespachoEnc DEFAULT ((0)),
        IdDespachoDet        INT NULL CONSTRAINT DF_trans_movimientos_new_IdDespachoDet DEFAULT ((0)),
        IdProductoTallaColor INT NULL CONSTRAINT DF_trans_movimientos_new_IdProductoTallaColor DEFAULT ((0)),
        Talla                NVARCHAR(100) NULL,
        Color                NVARCHAR(100) NULL
    );

    SET IDENTITY_INSERT dbo.trans_movimientos_new_20260526 ON;

    INSERT INTO dbo.trans_movimientos_new_20260526 (
        IdMovimiento, IdEmpresa, IdBodegaOrigen, IdTransaccion, IdPropietarioBodega, IdProductoBodega,
        IdUbicacionOrigen, IdUbicacionDestino, IdPresentacion, IdEstadoOrigen, IdEstadoDestino,
        IdUnidadMedida, IdTipoTarea, IdBodegaDestino, IdRecepcion, cantidad, serie, peso, lote,
        fecha_vence, fecha, barra_pallet, hora_ini, hora_fin, fecha_agr, usuario_agr, cantidad_hist,
        peso_hist, lic_plate, IdOperadorBodega, IdRecepcionDet, IdPedidoEnc, IdPedidoDet, IdDespachoEnc,
        IdDespachoDet, IdProductoTallaColor, Talla, Color
    )
    SELECT
        IdMovimiento, IdEmpresa, IdBodegaOrigen, IdTransaccion, IdPropietarioBodega, IdProductoBodega,
        IdUbicacionOrigen, IdUbicacionDestino, IdPresentacion, IdEstadoOrigen, IdEstadoDestino,
        IdUnidadMedida, IdTipoTarea, IdBodegaDestino, IdRecepcion, cantidad, serie, peso, lote,
        fecha_vence, fecha, barra_pallet, hora_ini, hora_fin, fecha_agr, usuario_agr, cantidad_hist,
        peso_hist, lic_plate, IdOperadorBodega, IdRecepcionDet, IdPedidoEnc, IdPedidoDet, IdDespachoEnc,
        IdDespachoDet, IdProductoTallaColor, Talla, Color
    FROM dbo.trans_movimientos WITH (HOLDLOCK, TABLOCKX);

    SET IDENTITY_INSERT dbo.trans_movimientos_new_20260526 OFF;

    /*
      Para reutilizar nombres de constraints del objeto final, se elimina PK/FK del original
      antes del swap.
    */
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT PK_movimientos;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_bodega;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_producto_bodega;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_producto_estado;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_producto_estado1;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_producto_presentacion;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_propietario_bodega;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_sis_tipo_tarea_hh;
    ALTER TABLE dbo.trans_movimientos DROP CONSTRAINT FK_trans_movimientos_unidad_medida;

    EXEC sp_rename 'dbo.trans_movimientos', 'trans_movimientos__pre_identity_20260526';
    EXEC sp_rename 'dbo.trans_movimientos_new_20260526', 'trans_movimientos';

    /*
      Re-crear PK/FK en tabla nueva con nombres originales.
    */
    ALTER TABLE dbo.trans_movimientos
    ADD CONSTRAINT PK_movimientos PRIMARY KEY CLUSTERED (IdEmpresa, IdBodegaOrigen, IdTransaccion, IdMovimiento);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_bodega
        FOREIGN KEY (IdBodegaDestino) REFERENCES dbo.bodega(IdBodega);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_producto_bodega
        FOREIGN KEY (IdProductoBodega) REFERENCES dbo.producto_bodega(IdProductoBodega);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_producto_estado
        FOREIGN KEY (IdEstadoOrigen) REFERENCES dbo.producto_estado(IdEstado);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_producto_estado1
        FOREIGN KEY (IdEstadoDestino) REFERENCES dbo.producto_estado(IdEstado);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_producto_presentacion
        FOREIGN KEY (IdPresentacion) REFERENCES dbo.producto_presentacion(IdPresentacion);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_propietario_bodega
        FOREIGN KEY (IdPropietarioBodega) REFERENCES dbo.propietario_bodega(IdPropietarioBodega);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_sis_tipo_tarea_hh
        FOREIGN KEY (IdTipoTarea) REFERENCES dbo.sis_tipo_tarea(IdTipoTarea);

    ALTER TABLE dbo.trans_movimientos WITH CHECK
    ADD CONSTRAINT FK_trans_movimientos_unidad_medida
        FOREIGN KEY (IdUnidadMedida) REFERENCES dbo.unidad_medida(IdUnidadMedida);

    /*
      Re-crear índices no clusterizados del objeto anterior.
    */
    DECLARE @sql NVARCHAR(MAX) = N'';

    ;WITH idx AS (
        SELECT i.index_id, i.name, i.is_unique, i.has_filter, i.filter_definition
        FROM sys.indexes i
        WHERE i.object_id = OBJECT_ID('dbo.trans_movimientos__pre_identity_20260526')
          AND i.index_id > 1
          AND i.is_hypothetical = 0
    ),
    keys AS (
        SELECT
            i.index_id,
            STRING_AGG(
                QUOTENAME(c.name) + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END,
                ', '
            ) WITHIN GROUP (ORDER BY ic.key_ordinal) AS key_cols
        FROM sys.indexes i
        JOIN sys.index_columns ic
            ON ic.object_id = i.object_id
           AND ic.index_id = i.index_id
           AND ic.key_ordinal > 0
        JOIN sys.columns c
            ON c.object_id = ic.object_id
           AND c.column_id = ic.column_id
        WHERE i.object_id = OBJECT_ID('dbo.trans_movimientos__pre_identity_20260526')
          AND i.index_id > 1
        GROUP BY i.index_id
    ),
    includes AS (
        SELECT
            i.index_id,
            STRING_AGG(QUOTENAME(c.name), ', ') WITHIN GROUP (ORDER BY ic.index_column_id) AS include_cols
        FROM sys.indexes i
        JOIN sys.index_columns ic
            ON ic.object_id = i.object_id
           AND ic.index_id = i.index_id
           AND ic.is_included_column = 1
        JOIN sys.columns c
            ON c.object_id = ic.object_id
           AND c.column_id = ic.column_id
        WHERE i.object_id = OBJECT_ID('dbo.trans_movimientos__pre_identity_20260526')
          AND i.index_id > 1
        GROUP BY i.index_id
    )
    SELECT @sql = STRING_AGG(CAST(
        'CREATE ' + CASE WHEN i.is_unique = 1 THEN 'UNIQUE ' ELSE '' END + 'NONCLUSTERED INDEX ' + QUOTENAME(i.name) +
        ' ON dbo.trans_movimientos (' + k.key_cols + ')' +
        CASE WHEN inc.include_cols IS NOT NULL THEN ' INCLUDE (' + inc.include_cols + ')' ELSE '' END +
        CASE WHEN i.has_filter = 1 THEN ' WHERE ' + i.filter_definition ELSE '' END + ';'
    AS NVARCHAR(MAX)), CHAR(10))
    FROM idx i
    JOIN keys k ON k.index_id = i.index_id
    LEFT JOIN includes inc ON inc.index_id = i.index_id;

    IF @sql IS NOT NULL AND LEN(@sql) > 0
        EXEC sp_executesql @sql;

    DECLARE @max_after INT = (SELECT ISNULL(MAX(IdMovimiento), 0) FROM dbo.trans_movimientos);
    DBCC CHECKIDENT ('dbo.trans_movimientos', RESEED, @max_after) WITH NO_INFOMSGS;

    DECLARE @rows_after BIGINT = (SELECT COUNT_BIG(*) FROM dbo.trans_movimientos);

    IF @rows_before <> @rows_after
        THROW 51000, 'Validación fallida: conteo de filas no coincide.', 1;

    IF EXISTS (
        SELECT 1
        FROM dbo.trans_movimientos
        GROUP BY IdEmpresa, IdBodegaOrigen, IdTransaccion, IdMovimiento
        HAVING COUNT(*) > 1
    )
        THROW 51000, 'Validación fallida: llave PK duplicada en tabla nueva.', 1;

    COMMIT TRAN;

    SELECT
        '#EJC20260526 OK' AS Resultado,
        @rows_before AS FilasAntes,
        @rows_after AS FilasDespues,
        @max_before AS MaxIdAntes,
        @max_after AS MaxIdDespues;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;

/*
Validación rápida post-migración:
SELECT COLUMNPROPERTY(OBJECT_ID('dbo.trans_movimientos'), 'IdMovimiento', 'IsIdentity') AS IsIdentity;
SELECT COUNT(*) AS Filas, MIN(IdMovimiento) AS MinId, MAX(IdMovimiento) AS MaxId FROM dbo.trans_movimientos;
SELECT IDENT_CURRENT('dbo.trans_movimientos') AS IdentCurrent;
*/
