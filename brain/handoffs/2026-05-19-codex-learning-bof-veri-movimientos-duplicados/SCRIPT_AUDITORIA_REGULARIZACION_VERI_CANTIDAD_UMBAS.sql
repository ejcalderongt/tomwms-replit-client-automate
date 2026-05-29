    /*
        Auditoria y regularizacion controlada de VERI grabada sin factor de presentacion
        --------------------------------------------------------------------------------
        Caso base: IdPickingEnc = 1465

        Problema:
        Algunas VERI quedaron con trans_movimientos.cantidad en presentacion
        (ej. 72 cajas) cuando la tabla espera UMBAS (ej. 72 * factor 24 = 1728).

        Criterio conservador:
        Solo se propone actualizar cuando:
            - existe una unica VERI para la llave operativa;
            - trans_picking_ubic tiene Cantidad_Verificada > 0;
            - la presentacion de picking tiene factor > 1;
            - VERI.cantidad = SUM(trans_picking_ubic.Cantidad_Verificada);
            - PIK del mismo grupo confirma la cantidad esperada en UMBAS.

        Seguridad:
        - Por defecto NO actualiza nada.
        - Si @EjecutarUpdate = 1 y @ConfirmarCommit = 0, actualiza dentro de
            transaccion y luego ejecuta ROLLBACK.
        - Para aplicar a todos los pickings, @IdPickingEnc debe quedar NULL y
            @ProcesarTodos debe ser 1.

        Nota:
        Este script no borra movimientos y no corrige duplicados exactos. Debe
        ejecutarse despues de depurar duplicados VERI.
    */

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @IdPickingEnc INT = 1465;      -- Caso puntual. Para todos, dejar NULL.
    DECLARE @ProcesarTodos BIT = 0;        -- Cambiar a 1 solo si @IdPickingEnc es NULL.
    DECLARE @EjecutarUpdate BIT = 0;       -- 0 = solo auditoria. 1 = ejecuta UPDATE controlado.
    DECLARE @ConfirmarCommit BIT = 0;      -- 0 = ROLLBACK. 1 = COMMIT. Usar 1 solo con autorizacion explicita.
    DECLARE @RunId UNIQUEIDENTIFIER = NEWID();
    DECLARE @FechaEjecucion DATETIME = GETDATE();
    DECLARE @RunIdTexto VARCHAR(36) = CONVERT(VARCHAR(36), @RunId);

    IF @IdPickingEnc IS NULL AND @ProcesarTodos = 0
    BEGIN
        THROW 52001, 'Configuracion invalida: para procesar todos los pickings debe usar @ProcesarTodos = 1.', 1;
    END;

    IF @IdPickingEnc IS NOT NULL AND @ProcesarTodos = 1
    BEGIN
        RAISERROR('Aviso: @ProcesarTodos = 1 se ignora porque @IdPickingEnc tiene valor especifico.', 10, 1) WITH NOWAIT;
    END;

    IF @EjecutarUpdate = 0 AND @ConfirmarCommit = 1
    BEGIN
        THROW 52002, 'Configuracion invalida: no tiene sentido confirmar commit si @EjecutarUpdate = 0.', 1;
    END;

    RAISERROR('RunId: %s', 10, 1, @RunIdTexto) WITH NOWAIT;
    RAISERROR('Inicio auditoria VERI cantidad UMBAS.', 10, 1) WITH NOWAIT;

    IF OBJECT_ID('tempdb..#CandidatosVeriUmbas') IS NOT NULL DROP TABLE #CandidatosVeriUmbas;
    IF OBJECT_ID('tempdb..#BitacoraActualizados') IS NOT NULL DROP TABLE #BitacoraActualizados;

    CREATE TABLE #BitacoraActualizados (
        RunId UNIQUEIDENTIFIER NOT NULL,
        FechaEjecucion DATETIME NOT NULL,
        Accion NVARCHAR(40) NOT NULL,
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
        DiferenciaCorregida FLOAT NULL,
        fecha_agr DATETIME NULL,
        usuario_agr NVARCHAR(50) NULL
    );

    CREATE TABLE #CandidatosVeriUmbas (
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
        MovVERI INT NOT NULL,
        IdMovimientoVERI INT NOT NULL,
        IdPresentacionVERI INT NULL,
        CantidadActualVERI FLOAT NULL,
        CantidadNuevaVERI FLOAT NULL,
        MovPIK INT NOT NULL,
        CantidadUMBASPIK FLOAT NULL,
        DiferenciaCorregida FLOAT NULL,
        fecha_agr DATETIME NULL,
        usuario_agr NVARCHAR(50) NULL
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
            pu.IdPresentacion AS IdPresentacionPU,
            pp.nombre AS PresentacionPU,
            pp.factor AS FactorPresentacion,
            COUNT(*) AS LineasPU,
            SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPU
        FROM dbo.trans_picking_ubic pu
        INNER JOIN dbo.producto_presentacion pp
            ON pp.IdPresentacion = pu.IdPresentacion
        WHERE pu.Cantidad_Verificada > 0
        AND (@IdPickingEnc IS NULL OR pu.IdPickingEnc = @IdPickingEnc)
        GROUP BY
            pu.IdPickingEnc,
            pu.IdPedidoEnc,
            pu.IdPedidoDet,
            CONVERT(INT, pu.IdRecepcion),
            pu.IdProductoBodega,
            pu.Lic_plate,
            pu.Lote,
            pu.Fecha_Vence,
            pu.IdPresentacion,
            pp.nombre,
            pp.factor
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
            MIN(m.IdPresentacion) AS IdPresentacionVERI,
            SUM(ISNULL(m.cantidad, 0)) AS CantidadUMBASVERI,
            MIN(m.fecha_agr) AS fecha_agr,
            MIN(m.usuario_agr) AS usuario_agr
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 11
        AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTransaccion,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdProductoBodega,
            m.barra_pallet,
            m.lote,
            m.fecha_vence
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
            COUNT(*) AS MovPIK,
            SUM(ISNULL(m.cantidad, 0)) AS CantidadUMBASPIK
        FROM dbo.trans_movimientos m
        WHERE m.IdTipoTarea = 8
        AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
        GROUP BY
            m.IdTransaccion,
            m.IdPedidoEnc,
            m.IdPedidoDet,
            m.IdRecepcion,
            m.IdProductoBodega,
            m.barra_pallet,
            m.lote,
            m.fecha_vence
    )
    INSERT INTO #CandidatosVeriUmbas (
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
        LineasPU,
        CantidadVerificadaPU,
        MovVERI,
        IdMovimientoVERI,
        IdPresentacionVERI,
        CantidadActualVERI,
        CantidadNuevaVERI,
        MovPIK,
        CantidadUMBASPIK,
        DiferenciaCorregida,
        fecha_agr,
        usuario_agr
    )
    SELECT
        pg.IdPickingEnc,
        pg.IdPedidoEnc,
        pg.IdPedidoDet,
        pg.IdRecepcion,
        vg.IdRecepcionDet,
        pg.IdProductoBodega,
        pg.Lic_plate,
        pg.Lote,
        pg.Fecha_Vence,
        pg.IdPresentacionPU AS IdPresentacion,
        pg.PresentacionPU AS Presentacion,
        pg.FactorPresentacion,
        pg.LineasPU,
        pg.CantidadVerificadaPU,
        vg.MovVERI,
        vg.IdMovimientoVERI,
        vg.IdPresentacionVERI,
        vg.CantidadUMBASVERI AS CantidadActualVERI,
        pg.CantidadVerificadaPU * pg.FactorPresentacion AS CantidadNuevaVERI,
        pik.MovPIK,
        pik.CantidadUMBASPIK,
        (pg.CantidadVerificadaPU * pg.FactorPresentacion) - vg.CantidadUMBASVERI AS DiferenciaCorregida,
        vg.fecha_agr,
        vg.usuario_agr
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
    AND vg.CantidadUMBASVERI = pg.CantidadVerificadaPU
    AND pik.CantidadUMBASPIK = pg.CantidadVerificadaPU * pg.FactorPresentacion;

    RAISERROR('Candidatos calculados.', 10, 1) WITH NOWAIT;

    /* 1) Resumen de configuracion */
    SELECT
        @RunId AS RunId,
        @FechaEjecucion AS FechaEjecucion,
        @IdPickingEnc AS IdPickingEnc,
        @ProcesarTodos AS ProcesarTodos,
        @EjecutarUpdate AS EjecutarUpdate,
        @ConfirmarCommit AS ConfirmarCommit,
        CASE
            WHEN @EjecutarUpdate = 0 THEN 'SOLO_AUDITORIA'
            WHEN @EjecutarUpdate = 1 AND @ConfirmarCommit = 0 THEN 'SIMULACION_UPDATE_CON_ROLLBACK'
            WHEN @EjecutarUpdate = 1 AND @ConfirmarCommit = 1 THEN 'UPDATE_CON_COMMIT'
        END AS ModoEjecucion;

    /* 2) Resumen ejecutivo */
    SELECT
        COUNT(*) AS MovimientosCandidatosActualizar,
        COUNT(DISTINCT IdPickingEnc) AS PickingsAfectados,
        SUM(CantidadActualVERI) AS CantidadActualTotal,
        SUM(CantidadNuevaVERI) AS CantidadNuevaTotal,
        SUM(DiferenciaCorregida) AS DiferenciaCorregidaTotal,
        MIN(IdPickingEnc) AS PrimerPicking,
        MAX(IdPickingEnc) AS UltimoPicking
    FROM #CandidatosVeriUmbas;

    /* 3) Detalle de candidatos */
    SELECT
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
        LineasPU,
        CantidadVerificadaPU,
        MovVERI,
        CantidadActualVERI,
        CantidadNuevaVERI,
        MovPIK,
        CantidadUMBASPIK,
        DiferenciaCorregida,
        fecha_agr,
        usuario_agr
    FROM #CandidatosVeriUmbas
    ORDER BY
        IdPickingEnc,
        IdPedidoDet,
        Lic_plate;

    BEGIN TRAN;

    IF @EjecutarUpdate = 1
    BEGIN
        RAISERROR('Ejecutando UPDATE controlado de VERI cantidad UMBAS.', 10, 1) WITH NOWAIT;

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
                c.DiferenciaCorregida,
                INSERTED.fecha_agr,
                INSERTED.usuario_agr
            INTO #BitacoraActualizados (
                RunId,
                FechaEjecucion,
                Accion,
                IdMovimiento,
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
                CantidadAnterior,
                CantidadNueva,
                CantidadVerificadaPU,
                CantidadPIK,
                DiferenciaCorregida,
                fecha_agr,
                usuario_agr
            )
        FROM dbo.trans_movimientos m
        INNER JOIN #CandidatosVeriUmbas c
            ON c.IdMovimientoVERI = m.IdMovimiento;
    END
    ELSE
    BEGIN
        RAISERROR('Modo solo auditoria: no se ejecuta UPDATE.', 10, 1) WITH NOWAIT;
    END;

    /* 4) Bitacora aplicada o propuesta */
    IF @EjecutarUpdate = 1
    BEGIN
        SELECT *
        FROM #BitacoraActualizados
        ORDER BY IdPickingEnc, IdPedidoDet, Lic_plate;
    END
    ELSE
    BEGIN
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
            DiferenciaCorregida,
            fecha_agr,
            usuario_agr
        FROM #CandidatosVeriUmbas
        ORDER BY IdPickingEnc, IdPedidoDet, Lic_plate;
    END;

    IF @EjecutarUpdate = 1 AND @ConfirmarCommit = 1
    BEGIN
        COMMIT TRAN;
        RAISERROR('COMMIT ejecutado. Las cantidades VERI fueron actualizadas a UMBAS.', 10, 1) WITH NOWAIT;
    END
    ELSE
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('ROLLBACK ejecutado. No se aplicaron cambios permanentes.', 10, 1) WITH NOWAIT;
    END;

    RAISERROR('Fin auditoria VERI cantidad UMBAS.', 10, 1) WITH NOWAIT;
