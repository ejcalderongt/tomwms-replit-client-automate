/*
    Stored procedures de auditoria y regularizacion VERI
    ----------------------------------------------------

    Procedimientos:
      1. dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
      2. dbo.usp_WMS_VERI_RegularizarCantidadUmbas
      3. dbo.usp_WMS_VERI_PostCheck

    Reglas de seguridad:
      - Por defecto solo auditan.
      - Para todos los pickings, usar @IdPickingEnc = NULL y @ProcesarTodos = 1.
      - Para escribir datos, activar @EjecutarDelete/@EjecutarUpdate = 1.
      - Para dejar cambios permanentes, activar @ConfirmarCommit = 1.
      - Si se ejecuta escritura con @ConfirmarCommit = 0, se hace ROLLBACK.

    Orden recomendado:
      EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos @IdPickingEnc = 1465;
      EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos @IdPickingEnc = 1465, @EjecutarDelete = 1, @ConfirmarCommit = 1;
      EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas @IdPickingEnc = 1465;
      EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas @IdPickingEnc = 1465, @EjecutarUpdate = 1, @ConfirmarCommit = 1;
      EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465;
*/

CREATE OR ALTER PROCEDURE dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc INT = NULL,
    @ProcesarTodos BIT = 0,
    @EjecutarDelete BIT = 0,
    @ConfirmarCommit BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @RunId UNIQUEIDENTIFIER = NEWID();
    DECLARE @FechaEjecucion DATETIME = GETDATE();

    IF @IdPickingEnc IS NULL AND @ProcesarTodos = 0
        THROW 53001, 'Configuracion invalida: para procesar todos los pickings debe usar @ProcesarTodos = 1.', 1;

    IF @EjecutarDelete = 0 AND @ConfirmarCommit = 1
        THROW 53002, 'Configuracion invalida: @ConfirmarCommit = 1 requiere @EjecutarDelete = 1.', 1;

    IF OBJECT_ID('tempdb..#VeriDup') IS NOT NULL DROP TABLE #VeriDup;
    IF OBJECT_ID('tempdb..#DeletedVeriDup') IS NOT NULL DROP TABLE #DeletedVeriDup;

    CREATE TABLE #DeletedVeriDup (
        RunId UNIQUEIDENTIFIER NOT NULL,
        FechaEjecucion DATETIME NOT NULL,
        Accion NVARCHAR(40) NOT NULL,
        IdMovimiento INT NOT NULL,
        IdMovimientoConservado INT NOT NULL,
        IdPickingEnc INT NOT NULL,
        IdPedidoEnc INT NULL,
        IdPedidoDet INT NULL,
        IdRecepcion INT NULL,
        IdRecepcionDet INT NULL,
        IdProductoBodega INT NULL,
        barra_pallet NVARCHAR(100) NULL,
        lote NVARCHAR(100) NULL,
        fecha_vence DATETIME NULL,
        cantidad FLOAT NULL,
        NumeroRepeticion INT NOT NULL,
        TotalRepeticiones INT NOT NULL
    );

    ;WITH Ranked AS (
        SELECT
            m.IdMovimiento,
            m.IdTipoTarea,
            m.IdTransaccion AS IdPickingEnc,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdRecepcionDet,
            m.IdProductoBodega,
            m.IdUbicacionOrigen,
            m.IdUbicacionDestino,
            m.IdPresentacion,
            m.IdEstadoOrigen,
            m.IdEstadoDestino,
            m.IdUnidadMedida,
            m.barra_pallet,
            m.lote,
            m.fecha_vence,
            m.cantidad,
            ROW_NUMBER() OVER (
                PARTITION BY
                    m.IdTipoTarea, m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
                    m.IdRecepcion, m.IdRecepcionDet, m.IdProductoBodega,
                    m.IdUbicacionOrigen, m.IdUbicacionDestino, m.IdPresentacion,
                    m.IdEstadoOrigen, m.IdEstadoDestino, m.IdUnidadMedida,
                    ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                    m.cantidad
                ORDER BY m.IdMovimiento
            ) AS NumeroRepeticion,
            COUNT(*) OVER (
                PARTITION BY
                    m.IdTipoTarea, m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
                    m.IdRecepcion, m.IdRecepcionDet, m.IdProductoBodega,
                    m.IdUbicacionOrigen, m.IdUbicacionDestino, m.IdPresentacion,
                    m.IdEstadoOrigen, m.IdEstadoDestino, m.IdUnidadMedida,
                    ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                    m.cantidad
            ) AS TotalRepeticiones,
            MIN(m.IdMovimiento) OVER (
                PARTITION BY
                    m.IdTipoTarea, m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
                    m.IdRecepcion, m.IdRecepcionDet, m.IdProductoBodega,
                    m.IdUbicacionOrigen, m.IdUbicacionDestino, m.IdPresentacion,
                    m.IdEstadoOrigen, m.IdEstadoDestino, m.IdUnidadMedida,
                    ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                    ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                    m.cantidad
            ) AS IdMovimientoConservado
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 11
          AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
    )
    SELECT *
    INTO #VeriDup
    FROM Ranked
    WHERE TotalRepeticiones > 1;

    SELECT
        @RunId AS RunId,
        @FechaEjecucion AS FechaEjecucion,
        @IdPickingEnc AS IdPickingEnc,
        @ProcesarTodos AS ProcesarTodos,
        @EjecutarDelete AS EjecutarDelete,
        @ConfirmarCommit AS ConfirmarCommit,
        CASE
            WHEN @EjecutarDelete = 0 THEN 'SOLO_AUDITORIA'
            WHEN @ConfirmarCommit = 0 THEN 'SIMULACION_DELETE_CON_ROLLBACK'
            ELSE 'DELETE_CON_COMMIT'
        END AS ModoEjecucion;

    SELECT
        COUNT(*) AS MovimientosEnGruposDuplicados,
        SUM(CASE WHEN NumeroRepeticion = 1 THEN 1 ELSE 0 END) AS MovimientosAConservar,
        SUM(CASE WHEN NumeroRepeticion > 1 THEN 1 ELSE 0 END) AS MovimientosCandidatosEliminar,
        SUM(CASE WHEN NumeroRepeticion > 1 THEN ISNULL(cantidad, 0) ELSE 0 END) AS CantidadCandidataEliminar,
        COUNT(DISTINCT IdPickingEnc) AS PickingsConDuplicados
    FROM #VeriDup;

    SELECT
        CASE WHEN NumeroRepeticion = 1 THEN 'CONSERVAR' ELSE 'CANDIDATO_ELIMINAR' END AS AccionPropuesta,
        IdMovimiento,
        IdMovimientoConservado,
        NumeroRepeticion,
        TotalRepeticiones,
        IdPickingEnc,
        IdPedidoEnc,
        IdPedidoDet,
        IdRecepcion,
        IdRecepcionDet,
        IdProductoBodega,
        barra_pallet,
        lote,
        fecha_vence,
        cantidad
    FROM #VeriDup
    ORDER BY IdPickingEnc, IdPedidoDet, barra_pallet, lote, cantidad DESC, NumeroRepeticion, IdMovimiento;

    BEGIN TRAN;

    IF @EjecutarDelete = 1
    BEGIN
        DELETE m
            OUTPUT
                @RunId,
                @FechaEjecucion,
                N'DELETE_VERI_DUPLICADO',
                DELETED.IdMovimiento,
                c.IdMovimientoConservado,
                DELETED.IdTransaccion,
                DELETED.IdPedidoEnc,
                DELETED.IdPedidoDet,
                DELETED.IdRecepcion,
                DELETED.IdRecepcionDet,
                DELETED.IdProductoBodega,
                DELETED.barra_pallet,
                DELETED.lote,
                DELETED.fecha_vence,
                DELETED.cantidad,
                c.NumeroRepeticion,
                c.TotalRepeticiones
            INTO #DeletedVeriDup
        FROM dbo.trans_movimientos m
        INNER JOIN #VeriDup c
            ON c.IdMovimiento = m.IdMovimiento
        WHERE c.NumeroRepeticion > 1;
    END;

    IF @EjecutarDelete = 1
        SELECT * FROM #DeletedVeriDup ORDER BY IdPickingEnc, IdPedidoDet, barra_pallet, lote, cantidad DESC;
    ELSE
        SELECT
            @RunId AS RunId,
            @FechaEjecucion AS FechaEjecucion,
            N'DELETE_VERI_DUPLICADO_PROPUESTO' AS Accion,
            IdMovimiento,
            IdMovimientoConservado,
            IdPickingEnc,
            IdPedidoEnc,
            IdPedidoDet,
            IdRecepcion,
            IdRecepcionDet,
            IdProductoBodega,
            barra_pallet,
            lote,
            fecha_vence,
            cantidad,
            NumeroRepeticion,
            TotalRepeticiones
        FROM #VeriDup
        WHERE NumeroRepeticion > 1
        ORDER BY IdPickingEnc, IdPedidoDet, barra_pallet, lote, cantidad DESC;

    IF @EjecutarDelete = 1 AND @ConfirmarCommit = 1
    BEGIN
        COMMIT TRAN;
        RAISERROR('COMMIT ejecutado. Duplicados exactos VERI eliminados.', 10, 1) WITH NOWAIT;
    END
    ELSE
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('ROLLBACK ejecutado. No se aplicaron cambios permanentes.', 10, 1) WITH NOWAIT;
    END
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc INT = NULL,
    @ProcesarTodos BIT = 0,
    @EjecutarUpdate BIT = 0,
    @ConfirmarCommit BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @RunId UNIQUEIDENTIFIER = NEWID();
    DECLARE @FechaEjecucion DATETIME = GETDATE();

    IF @IdPickingEnc IS NULL AND @ProcesarTodos = 0
        THROW 54001, 'Configuracion invalida: para procesar todos los pickings debe usar @ProcesarTodos = 1.', 1;

    IF @EjecutarUpdate = 0 AND @ConfirmarCommit = 1
        THROW 54002, 'Configuracion invalida: @ConfirmarCommit = 1 requiere @EjecutarUpdate = 1.', 1;

    IF OBJECT_ID('tempdb..#VeriUmbas') IS NOT NULL DROP TABLE #VeriUmbas;
    IF OBJECT_ID('tempdb..#UpdatedVeriUmbas') IS NOT NULL DROP TABLE #UpdatedVeriUmbas;

    CREATE TABLE #VeriUmbas (
        IdMovimientoVERI INT NOT NULL,
        IdPickingEnc INT NOT NULL,
        IdPedidoEnc INT NULL,
        IdPedidoDet INT NULL,
        IdRecepcion INT NULL,
        IdRecepcionDet INT NULL,
        IdProductoBodega INT NULL,
        Lic_plate NVARCHAR(100) NULL,
        Lote NVARCHAR(100) NULL,
        Fecha_Vence DATETIME NULL,
        IdPresentacion INT NULL,
        Presentacion NVARCHAR(200) NULL,
        FactorPresentacion FLOAT NULL,
        LineasPU INT NOT NULL,
        CantidadVerificadaPU FLOAT NULL,
        CantidadActualVERI FLOAT NULL,
        CantidadNuevaVERI FLOAT NULL,
        CantidadUMBASPIK FLOAT NULL,
        DiferenciaCorregida FLOAT NULL
    );

    CREATE TABLE #UpdatedVeriUmbas (
        RunId UNIQUEIDENTIFIER NOT NULL,
        FechaEjecucion DATETIME NOT NULL,
        Accion NVARCHAR(50) NOT NULL,
        IdMovimiento INT NOT NULL,
        IdPickingEnc INT NOT NULL,
        IdPedidoEnc INT NULL,
        IdPedidoDet INT NULL,
        IdRecepcion INT NULL,
        IdRecepcionDet INT NULL,
        IdProductoBodega INT NULL,
        Lic_plate NVARCHAR(100) NULL,
        Lote NVARCHAR(100) NULL,
        Fecha_Vence DATETIME NULL,
        IdPresentacion INT NULL,
        Presentacion NVARCHAR(200) NULL,
        FactorPresentacion FLOAT NULL,
        CantidadAnterior FLOAT NULL,
        CantidadNueva FLOAT NULL,
        CantidadVerificadaPU FLOAT NULL,
        CantidadPIK FLOAT NULL,
        DiferenciaCorregida FLOAT NULL
    );

    ;WITH PickingGrupo AS (
        SELECT
            pu.IdPickingEnc,
            pu.IdPedidoEnc,
            pu.IdPedidoDet,
            CONVERT(INT, pu.IdRecepcion) AS IdRecepcion,
            pu.IdProductoBodega,
            pu.Lic_plate,
            pu.Lote,
            pu.Fecha_Vence,
            pu.IdPresentacion,
            pp.nombre AS Presentacion,
            pp.factor AS FactorPresentacion,
            COUNT(*) AS LineasPU,
            SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPU
        FROM dbo.trans_picking_ubic pu
        INNER JOIN dbo.producto_presentacion pp
            ON pp.IdPresentacion = pu.IdPresentacion
        WHERE pu.Cantidad_Verificada > 0
          AND (@IdPickingEnc IS NULL OR pu.IdPickingEnc = @IdPickingEnc)
        GROUP BY
            pu.IdPickingEnc, pu.IdPedidoEnc, pu.IdPedidoDet, CONVERT(INT, pu.IdRecepcion),
            pu.IdProductoBodega, pu.Lic_plate, pu.Lote, pu.Fecha_Vence,
            pu.IdPresentacion, pp.nombre, pp.factor
    ),
    VeriGrupo AS (
        SELECT
            m.IdTransaccion AS IdPickingEnc,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdProductoBodega,
            m.barra_pallet AS Lic_plate,
            m.lote AS Lote,
            m.fecha_vence AS Fecha_Vence,
            COUNT(*) AS MovVERI,
            MIN(m.IdMovimiento) AS IdMovimientoVERI,
            MIN(m.IdRecepcionDet) AS IdRecepcionDet,
            SUM(ISNULL(m.cantidad, 0)) AS CantidadActualVERI
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 11
          AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet, m.IdRecepcion,
            m.IdProductoBodega, m.barra_pallet, m.lote, m.fecha_vence
    ),
    PikGrupo AS (
        SELECT
            m.IdTransaccion AS IdPickingEnc,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdProductoBodega,
            m.barra_pallet AS Lic_plate,
            m.lote AS Lote,
            m.fecha_vence AS Fecha_Vence,
            SUM(ISNULL(m.cantidad, 0)) AS CantidadUMBASPIK
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 8
          AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet, m.IdRecepcion,
            m.IdProductoBodega, m.barra_pallet, m.lote, m.fecha_vence
    )
    INSERT INTO #VeriUmbas (
        IdMovimientoVERI, IdPickingEnc, IdPedidoEnc, IdPedidoDet, IdRecepcion,
        IdRecepcionDet, IdProductoBodega, Lic_plate, Lote, Fecha_Vence,
        IdPresentacion, Presentacion, FactorPresentacion, LineasPU,
        CantidadVerificadaPU, CantidadActualVERI, CantidadNuevaVERI,
        CantidadUMBASPIK, DiferenciaCorregida
    )
    SELECT
        vg.IdMovimientoVERI,
        pg.IdPickingEnc,
        pg.IdPedidoEnc,
        pg.IdPedidoDet,
        pg.IdRecepcion,
        vg.IdRecepcionDet,
        pg.IdProductoBodega,
        pg.Lic_plate,
        pg.Lote,
        pg.Fecha_Vence,
        pg.IdPresentacion,
        pg.Presentacion,
        pg.FactorPresentacion,
        pg.LineasPU,
        pg.CantidadVerificadaPU,
        vg.CantidadActualVERI,
        pg.CantidadVerificadaPU * pg.FactorPresentacion AS CantidadNuevaVERI,
        pik.CantidadUMBASPIK,
        (pg.CantidadVerificadaPU * pg.FactorPresentacion) - vg.CantidadActualVERI
    FROM PickingGrupo pg
    INNER JOIN VeriGrupo vg
        ON vg.IdPickingEnc = pg.IdPickingEnc
       AND ISNULL(vg.IdPedidoEnc, 0) = ISNULL(pg.IdPedidoEnc, 0)
       AND ISNULL(vg.IdPedidoDet, 0) = ISNULL(pg.IdPedidoDet, 0)
       AND ISNULL(vg.IdRecepcion, 0) = ISNULL(pg.IdRecepcion, 0)
       AND ISNULL(vg.IdProductoBodega, 0) = ISNULL(pg.IdProductoBodega, 0)
       AND ISNULL(vg.Lic_plate COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lic_plate COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(vg.Lote COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lote COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(vg.Fecha_Vence, CONVERT(DATETIME, '19000101', 112)) = ISNULL(pg.Fecha_Vence, CONVERT(DATETIME, '19000101', 112))
    INNER JOIN PikGrupo pik
        ON pik.IdPickingEnc = pg.IdPickingEnc
       AND ISNULL(pik.IdPedidoEnc, 0) = ISNULL(pg.IdPedidoEnc, 0)
       AND ISNULL(pik.IdPedidoDet, 0) = ISNULL(pg.IdPedidoDet, 0)
       AND ISNULL(pik.IdRecepcion, 0) = ISNULL(pg.IdRecepcion, 0)
       AND ISNULL(pik.IdProductoBodega, 0) = ISNULL(pg.IdProductoBodega, 0)
       AND ISNULL(pik.Lic_plate COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lic_plate COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(pik.Lote COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lote COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(pik.Fecha_Vence, CONVERT(DATETIME, '19000101', 112)) = ISNULL(pg.Fecha_Vence, CONVERT(DATETIME, '19000101', 112))
    WHERE vg.MovVERI = 1
      AND pg.FactorPresentacion > 1
      AND vg.CantidadActualVERI = pg.CantidadVerificadaPU
      AND pik.CantidadUMBASPIK = pg.CantidadVerificadaPU * pg.FactorPresentacion;

    SELECT
        @RunId AS RunId,
        @FechaEjecucion AS FechaEjecucion,
        @IdPickingEnc AS IdPickingEnc,
        @ProcesarTodos AS ProcesarTodos,
        @EjecutarUpdate AS EjecutarUpdate,
        @ConfirmarCommit AS ConfirmarCommit,
        CASE
            WHEN @EjecutarUpdate = 0 THEN 'SOLO_AUDITORIA'
            WHEN @ConfirmarCommit = 0 THEN 'SIMULACION_UPDATE_CON_ROLLBACK'
            ELSE 'UPDATE_CON_COMMIT'
        END AS ModoEjecucion;

    SELECT
        COUNT(*) AS MovimientosCandidatosActualizar,
        COUNT(DISTINCT IdPickingEnc) AS PickingsAfectados,
        SUM(CantidadActualVERI) AS CantidadActualTotal,
        SUM(CantidadNuevaVERI) AS CantidadNuevaTotal,
        SUM(DiferenciaCorregida) AS DiferenciaCorregidaTotal
    FROM #VeriUmbas;

    SELECT *
    FROM #VeriUmbas
    ORDER BY IdPickingEnc, IdPedidoDet, Lic_plate;

    BEGIN TRAN;

    IF @EjecutarUpdate = 1
    BEGIN
        UPDATE m
            SET m.cantidad = c.CantidadNuevaVERI
            OUTPUT
                @RunId,
                @FechaEjecucion,
                N'UPDATE_VERI_CANTIDAD_UMBAS',
                INSERTED.IdMovimiento,
                INSERTED.IdTransaccion,
                INSERTED.IdPedidoEnc,
                INSERTED.IdPedidoDet,
                INSERTED.IdRecepcion,
                INSERTED.IdRecepcionDet,
                INSERTED.IdProductoBodega,
                INSERTED.barra_pallet,
                INSERTED.lote,
                INSERTED.fecha_vence,
                c.IdPresentacion,
                c.Presentacion,
                c.FactorPresentacion,
                DELETED.cantidad,
                INSERTED.cantidad,
                c.CantidadVerificadaPU,
                c.CantidadUMBASPIK,
                c.DiferenciaCorregida
            INTO #UpdatedVeriUmbas
        FROM dbo.trans_movimientos m
        INNER JOIN #VeriUmbas c
            ON c.IdMovimientoVERI = m.IdMovimiento;
    END;

    IF @EjecutarUpdate = 1
        SELECT * FROM #UpdatedVeriUmbas ORDER BY IdPickingEnc, IdPedidoDet, Lic_plate;
    ELSE
        SELECT
            @RunId AS RunId,
            @FechaEjecucion AS FechaEjecucion,
            N'UPDATE_VERI_CANTIDAD_UMBAS_PROPUESTO' AS Accion,
            IdMovimientoVERI AS IdMovimiento,
            IdPickingEnc,
            IdPedidoEnc,
            IdPedidoDet,
            IdRecepcion,
            IdRecepcionDet,
            IdProductoBodega,
            Lic_plate,
            Lote,
            Fecha_Vence,
            IdPresentacion,
            Presentacion,
            FactorPresentacion,
            CantidadActualVERI AS CantidadAnterior,
            CantidadNuevaVERI AS CantidadNueva,
            CantidadVerificadaPU,
            CantidadUMBASPIK AS CantidadPIK,
            DiferenciaCorregida
        FROM #VeriUmbas
        ORDER BY IdPickingEnc, IdPedidoDet, Lic_plate;

    IF @EjecutarUpdate = 1 AND @ConfirmarCommit = 1
    BEGIN
        COMMIT TRAN;
        RAISERROR('COMMIT ejecutado. Cantidades VERI actualizadas a UMBAS.', 10, 1) WITH NOWAIT;
    END
    ELSE
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('ROLLBACK ejecutado. No se aplicaron cambios permanentes.', 10, 1) WITH NOWAIT;
    END
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_WMS_VERI_PostCheck
    @IdPickingEnc INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Dup AS (
        SELECT
            m.IdTransaccion AS IdPickingEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdRecepcionDet,
            m.IdProductoBodega,
            m.barra_pallet,
            m.lote,
            m.fecha_vence,
            m.cantidad,
            COUNT(*) AS Repeticiones
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 11
          AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTipoTarea, m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
            m.IdRecepcion, m.IdRecepcionDet, m.IdProductoBodega,
            m.IdUbicacionOrigen, m.IdUbicacionDestino, m.IdPresentacion,
            m.IdEstadoOrigen, m.IdEstadoDestino, m.IdUnidadMedida,
            ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
            ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
            ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
            m.cantidad,
            m.barra_pallet, m.lote, m.fecha_vence
        HAVING COUNT(*) > 1
    ),
    PickingGrupo AS (
        SELECT
            pu.IdPickingEnc,
            pu.IdPedidoEnc,
            pu.IdPedidoDet,
            CONVERT(INT, pu.IdRecepcion) AS IdRecepcion,
            pu.IdProductoBodega,
            pu.Lic_plate,
            pu.Lote,
            pu.Fecha_Vence,
            SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPU
        FROM dbo.trans_picking_ubic pu
        WHERE (@IdPickingEnc IS NULL OR pu.IdPickingEnc = @IdPickingEnc)
        GROUP BY
            pu.IdPickingEnc, pu.IdPedidoEnc, pu.IdPedidoDet, CONVERT(INT, pu.IdRecepcion),
            pu.IdProductoBodega, pu.Lic_plate, pu.Lote, pu.Fecha_Vence
    ),
    MovGrupo AS (
        SELECT
            m.IdTransaccion AS IdPickingEnc,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdProductoBodega,
            m.barra_pallet AS Lic_plate,
            m.lote AS Lote,
            m.fecha_vence AS Fecha_Vence,
            COUNT(*) AS MovVERI,
            SUM(ISNULL(v.Cantidad_Presentacion, 0)) AS CantidadPresentacionVERI,
            SUM(ISNULL(m.cantidad, 0)) AS CantidadUMBASVERI
        FROM dbo.trans_movimientos m
        INNER JOIN dbo.vw_movimientos v
            ON v.IdMovimiento = m.IdMovimiento
        WHERE m.IdTipoTarea = 11
          AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet, m.IdRecepcion,
            m.IdProductoBodega, m.barra_pallet, m.lote, m.fecha_vence
    )
    SELECT
        'DUPLICADOS_EXACTOS_RESTANTES' AS Seccion,
        COUNT(*) AS Conteo
    FROM Dup
    UNION ALL
    SELECT
        'MISMATCH_PRESENTACION_RESTANTES' AS Seccion,
        COUNT(*) AS Conteo
    FROM PickingGrupo pg
    FULL OUTER JOIN MovGrupo mg
        ON mg.IdPickingEnc = pg.IdPickingEnc
       AND ISNULL(mg.IdPedidoEnc, 0) = ISNULL(pg.IdPedidoEnc, 0)
       AND ISNULL(mg.IdPedidoDet, 0) = ISNULL(pg.IdPedidoDet, 0)
       AND ISNULL(mg.IdRecepcion, 0) = ISNULL(pg.IdRecepcion, 0)
       AND ISNULL(mg.IdProductoBodega, 0) = ISNULL(pg.IdProductoBodega, 0)
       AND ISNULL(mg.Lic_plate COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lic_plate COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(mg.Lote COLLATE DATABASE_DEFAULT, N'') = ISNULL(pg.Lote COLLATE DATABASE_DEFAULT, N'')
       AND ISNULL(mg.Fecha_Vence, CONVERT(DATETIME, '19000101', 112)) = ISNULL(pg.Fecha_Vence, CONVERT(DATETIME, '19000101', 112))
    WHERE ISNULL(mg.CantidadPresentacionVERI, 0) <> ISNULL(pg.CantidadVerificadaPU, 0);
END;
GO
