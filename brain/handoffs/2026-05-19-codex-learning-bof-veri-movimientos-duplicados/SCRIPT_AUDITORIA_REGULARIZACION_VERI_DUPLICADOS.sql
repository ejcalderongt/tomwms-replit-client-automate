/*
    Auditoria y regularizacion controlada de duplicados exactos VERI
    ----------------------------------------------------------------
    Caso base: IdPickingEnc = 1465

    Objetivo:
      1. Detectar movimientos VERI duplicados exactos.
      2. Comparar movimientos actuales vs. movimientos depurados contra trans_picking_ubic.
      3. Permitir simulacion con ROLLBACK antes de cualquier aplicacion real.
      4. Devolver bitacora de lo que se eliminaria o elimino.

    Seguridad:
      - Por defecto NO borra nada.
      - Si @EjecutarDelete = 1 y @ConfirmarCommit = 0, borra dentro de transaccion
        y luego ejecuta ROLLBACK.
      - Para aplicar a todos los pickings, @IdPickingEnc debe quedar NULL y
        @ProcesarTodos debe ser 1.

    Nota:
      Este script corrige solo duplicados exactos por llave logica. No corrige
      huerfanos ni mismatches de cantidad/llave.
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @IdPickingEnc INT = 1465;      -- Caso puntual. Para todos, dejar NULL.
DECLARE @ProcesarTodos BIT = 0;        -- Cambiar a 1 solo si @IdPickingEnc es NULL y se quiere auditar/aplicar global.
DECLARE @EjecutarDelete BIT = 0;       -- 0 = solo auditoria. 1 = ejecuta DELETE controlado.
DECLARE @ConfirmarCommit BIT = 0;      -- 0 = ROLLBACK. 1 = COMMIT. Usar 1 solo con autorizacion explicita.
DECLARE @RunId UNIQUEIDENTIFIER = NEWID();
DECLARE @FechaEjecucion DATETIME = GETDATE();
DECLARE @RunIdTexto VARCHAR(36) = CONVERT(VARCHAR(36), @RunId);

IF @IdPickingEnc IS NULL AND @ProcesarTodos = 0
BEGIN
    THROW 51001, 'Configuracion invalida: para procesar todos los pickings debe usar @ProcesarTodos = 1.', 1;
END;

IF @IdPickingEnc IS NOT NULL AND @ProcesarTodos = 1
BEGIN
    RAISERROR('Aviso: @ProcesarTodos = 1 se ignora porque @IdPickingEnc tiene valor especifico.', 10, 1) WITH NOWAIT;
END;

IF @EjecutarDelete = 0 AND @ConfirmarCommit = 1
BEGIN
    THROW 51002, 'Configuracion invalida: no tiene sentido confirmar commit si @EjecutarDelete = 0.', 1;
END;

RAISERROR('RunId: %s', 10, 1, @RunIdTexto) WITH NOWAIT;
RAISERROR('Inicio auditoria duplicados VERI.', 10, 1) WITH NOWAIT;

IF OBJECT_ID('tempdb..#Candidatos') IS NOT NULL DROP TABLE #Candidatos;
IF OBJECT_ID('tempdb..#BitacoraEliminados') IS NOT NULL DROP TABLE #BitacoraEliminados;

CREATE TABLE #BitacoraEliminados (
    RunId UNIQUEIDENTIFIER NOT NULL,
    FechaEjecucion DATETIME NOT NULL,
    Accion NVARCHAR(30) NOT NULL,
    IdMovimiento INT NOT NULL,
    IdTipoTarea INT NOT NULL,
    IdTransaccion INT NOT NULL,
    IdPedidoEnc INT NULL,
    IdPedidoDet INT NULL,
    IdRecepcion INT NULL,
    IdRecepcionDet INT NULL,
    IdProductoBodega INT NULL,
    IdUbicacionOrigen INT NULL,
    IdUbicacionDestino INT NULL,
    IdPresentacion INT NULL,
    IdEstadoOrigen INT NULL,
    IdEstadoDestino INT NULL,
    IdUnidadMedida INT NULL,
    barra_pallet NVARCHAR(100) NULL,
    lote NVARCHAR(100) NULL,
    fecha_vence DATETIME NULL,
    cantidad FLOAT NULL,
    fecha_movimiento DATETIME NULL,
    fecha_agr DATETIME NULL,
    usuario_agr NVARCHAR(50) NULL,
    IdMovimientoConservado INT NULL,
    NumeroRepeticion INT NULL,
    TotalRepeticiones INT NULL
);

;WITH VeriRank AS (
    SELECT
        m.IdMovimiento,
        m.IdTipoTarea,
        m.IdTransaccion,
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
        m.fecha,
        m.fecha_agr,
        m.usuario_agr,
        ROW_NUMBER() OVER (
            PARTITION BY
                m.IdTipoTarea,
                m.IdTransaccion,
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
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                m.cantidad
            ORDER BY m.IdMovimiento
        ) AS NumeroRepeticion,
        COUNT(*) OVER (
            PARTITION BY
                m.IdTipoTarea,
                m.IdTransaccion,
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
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                m.cantidad
        ) AS TotalRepeticiones,
        MIN(m.IdMovimiento) OVER (
            PARTITION BY
                m.IdTipoTarea,
                m.IdTransaccion,
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
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.lote COLLATE DATABASE_DEFAULT, N''),
                ISNULL(m.fecha_vence, CONVERT(DATETIME, '19000101', 112)),
                m.cantidad
        ) AS IdMovimientoConservado
    FROM dbo.trans_movimientos m
    WHERE m.IdTipoTarea = 11
      AND (@IdPickingEnc IS NULL OR m.IdTransaccion = @IdPickingEnc)
)
SELECT
    *
INTO #Candidatos
FROM VeriRank
WHERE TotalRepeticiones > 1;

RAISERROR('Candidatos calculados.', 10, 1) WITH NOWAIT;

/* 1) Resumen de configuracion */
SELECT
    @RunId AS RunId,
    @FechaEjecucion AS FechaEjecucion,
    @IdPickingEnc AS IdPickingEnc,
    @ProcesarTodos AS ProcesarTodos,
    @EjecutarDelete AS EjecutarDelete,
    @ConfirmarCommit AS ConfirmarCommit,
    CASE
        WHEN @EjecutarDelete = 0 THEN 'SOLO_AUDITORIA'
        WHEN @EjecutarDelete = 1 AND @ConfirmarCommit = 0 THEN 'SIMULACION_DELETE_CON_ROLLBACK'
        WHEN @EjecutarDelete = 1 AND @ConfirmarCommit = 1 THEN 'DELETE_CON_COMMIT'
    END AS ModoEjecucion;

/* 2) Resumen ejecutivo de duplicados */
SELECT
    COUNT(*) AS MovimientosEnGruposDuplicados,
    SUM(CASE WHEN NumeroRepeticion = 1 THEN 1 ELSE 0 END) AS MovimientosAConservar,
    SUM(CASE WHEN NumeroRepeticion > 1 THEN 1 ELSE 0 END) AS MovimientosCandidatosEliminar,
    SUM(CASE WHEN NumeroRepeticion = 1 THEN ISNULL(cantidad, 0) ELSE 0 END) AS CantidadConservada,
    SUM(CASE WHEN NumeroRepeticion > 1 THEN ISNULL(cantidad, 0) ELSE 0 END) AS CantidadCandidataEliminar,
    COUNT(DISTINCT IdTransaccion) AS PickingsConDuplicados
FROM #Candidatos;

/* 3) Grupos duplicados: una fila por llave logica */
SELECT
    IdTransaccion AS IdPickingEnc,
    IdPedidoEnc,
    IdPedidoDet,
    IdRecepcion,
    IdRecepcionDet,
    IdProductoBodega,
    IdUbicacionOrigen,
    IdUbicacionDestino,
    IdPresentacion,
    IdEstadoOrigen,
    IdEstadoDestino,
    IdUnidadMedida,
    barra_pallet,
    lote,
    fecha_vence,
    cantidad,
    MIN(IdMovimiento) AS IdMovimientoConservado,
    COUNT(*) AS Repeticiones,
    COUNT(*) - 1 AS CandidatosEliminar,
    SUM(cantidad) AS CantidadActualGrupo,
    MIN(cantidad) AS CantidadDepuradaGrupo,
    SUM(cantidad) - MIN(cantidad) AS CantidadExcesoGrupo,
    MIN(fecha_agr) AS PrimeraFechaAgr,
    MAX(fecha_agr) AS UltimaFechaAgr
FROM #Candidatos
GROUP BY
    IdTransaccion,
    IdPedidoEnc,
    IdPedidoDet,
    IdRecepcion,
    IdRecepcionDet,
    IdProductoBodega,
    IdUbicacionOrigen,
    IdUbicacionDestino,
    IdPresentacion,
    IdEstadoOrigen,
    IdEstadoDestino,
    IdUnidadMedida,
    barra_pallet,
    lote,
    fecha_vence,
    cantidad
ORDER BY
    IdTransaccion,
    IdPedidoDet,
    barra_pallet,
    lote,
    cantidad DESC;

/* 4) Detalle de candidatos: incluye movimiento conservado y movimientos a eliminar */
SELECT
    CASE WHEN NumeroRepeticion = 1 THEN 'CONSERVAR' ELSE 'CANDIDATO_ELIMINAR' END AS AccionPropuesta,
    IdMovimiento,
    IdMovimientoConservado,
    NumeroRepeticion,
    TotalRepeticiones,
    IdTransaccion AS IdPickingEnc,
    IdPedidoEnc,
    IdPedidoDet,
    IdRecepcion,
    IdRecepcionDet,
    IdProductoBodega,
    IdUbicacionOrigen,
    IdUbicacionDestino,
    IdPresentacion,
    IdEstadoOrigen,
    IdEstadoDestino,
    IdUnidadMedida,
    barra_pallet,
    lote,
    fecha_vence,
    cantidad,
    fecha,
    fecha_agr,
    usuario_agr
FROM #Candidatos
ORDER BY
    IdTransaccion,
    IdPedidoDet,
    barra_pallet,
    lote,
    cantidad DESC,
    NumeroRepeticion,
    IdMovimiento;

/* 5) Comparativo contra trans_picking_ubic usando Cantidad_Presentacion de vw_movimientos */
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
        COUNT(*) AS LineasPickingUbic,
        SUM(ISNULL(pu.Cantidad_Solicitada, 0)) AS CantidadSolicitadaPU,
        SUM(ISNULL(pu.Cantidad_Recibida, 0)) AS CantidadRecibidaPU,
        SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPU
    FROM dbo.trans_picking_ubic pu
    WHERE (@IdPickingEnc IS NULL OR pu.IdPickingEnc = @IdPickingEnc)
    GROUP BY
        pu.IdPickingEnc,
        pu.IdPedidoEnc,
        pu.IdPedidoDet,
        CONVERT(INT, pu.IdRecepcion),
        pu.IdProductoBodega,
        pu.Lic_plate,
        pu.Lote,
        pu.Fecha_Vence
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
        COUNT(*) AS MovVERIActual,
        SUM(CASE WHEN c.IdMovimiento IS NOT NULL AND c.NumeroRepeticion > 1 THEN 1 ELSE 0 END) AS MovVERICandidatosEliminar,
        SUM(ISNULL(v.Cantidad_Presentacion, 0)) AS CantidadPresentacionVERIActual,
        SUM(CASE
                WHEN c.IdMovimiento IS NOT NULL AND c.NumeroRepeticion > 1 THEN 0
                ELSE ISNULL(v.Cantidad_Presentacion, 0)
            END) AS CantidadPresentacionVERIDepurada,
        SUM(ISNULL(m.cantidad, 0)) AS CantidadUMBASVERIActual,
        SUM(CASE
                WHEN c.IdMovimiento IS NOT NULL AND c.NumeroRepeticion > 1 THEN 0
                ELSE ISNULL(m.cantidad, 0)
            END) AS CantidadUMBASVERIDepurada
    FROM dbo.trans_movimientos m
    INNER JOIN dbo.vw_movimientos v
        ON v.IdMovimiento = m.IdMovimiento
    LEFT JOIN #Candidatos c
        ON c.IdMovimiento = m.IdMovimiento
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
)
SELECT
    COALESCE(pg.IdPickingEnc, mg.IdPickingEnc) AS IdPickingEnc,
    COALESCE(pg.IdPedidoEnc, mg.IdPedidoEnc) AS IdPedidoEnc,
    COALESCE(pg.IdPedidoDet, mg.IdPedidoDet) AS IdPedidoDet,
    COALESCE(pg.IdRecepcion, mg.IdRecepcion) AS IdRecepcion,
    COALESCE(pg.IdProductoBodega, mg.IdProductoBodega) AS IdProductoBodega,
    COALESCE(pg.Lic_plate COLLATE DATABASE_DEFAULT, mg.Lic_plate COLLATE DATABASE_DEFAULT) AS Lic_plate,
    COALESCE(pg.Lote COLLATE DATABASE_DEFAULT, mg.Lote COLLATE DATABASE_DEFAULT) AS Lote,
    COALESCE(pg.Fecha_Vence, mg.Fecha_Vence) AS Fecha_Vence,
    ISNULL(pg.LineasPickingUbic, 0) AS LineasPickingUbic,
    ISNULL(pg.CantidadSolicitadaPU, 0) AS CantidadSolicitadaPU,
    ISNULL(pg.CantidadRecibidaPU, 0) AS CantidadRecibidaPU,
    ISNULL(pg.CantidadVerificadaPU, 0) AS CantidadVerificadaPU,
    ISNULL(mg.MovVERIActual, 0) AS MovVERIActual,
    ISNULL(mg.MovVERICandidatosEliminar, 0) AS MovVERICandidatosEliminar,
    ISNULL(mg.CantidadPresentacionVERIActual, 0) AS CantidadPresentacionVERIActual,
    ISNULL(mg.CantidadPresentacionVERIDepurada, 0) AS CantidadPresentacionVERIDepurada,
    ISNULL(mg.CantidadPresentacionVERIActual, 0) - ISNULL(pg.CantidadVerificadaPU, 0) AS DiferenciaActualVsPU,
    ISNULL(mg.CantidadPresentacionVERIDepurada, 0) - ISNULL(pg.CantidadVerificadaPU, 0) AS DiferenciaDepuradaVsPU,
    ISNULL(mg.CantidadUMBASVERIActual, 0) AS CantidadUMBASVERIActual,
    ISNULL(mg.CantidadUMBASVERIDepurada, 0) AS CantidadUMBASVERIDepurada,
    CASE
        WHEN pg.IdPickingEnc IS NULL THEN 'MOVIMIENTO_VERI_SIN_PICKING_UBIC'
        WHEN mg.IdPickingEnc IS NULL THEN 'PICKING_UBIC_SIN_MOVIMIENTO_VERI'
        WHEN ISNULL(mg.CantidadPresentacionVERIDepurada, 0) = ISNULL(pg.CantidadVerificadaPU, 0) THEN 'OK_DEPURADO_COINCIDE'
        ELSE 'REVISAR_MISMATCH'
    END AS EstadoDepuracion
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
WHERE
    ISNULL(mg.MovVERICandidatosEliminar, 0) > 0
    OR ISNULL(mg.CantidadPresentacionVERIActual, 0) <> ISNULL(pg.CantidadVerificadaPU, 0)
    OR ISNULL(mg.CantidadPresentacionVERIDepurada, 0) <> ISNULL(pg.CantidadVerificadaPU, 0)
ORDER BY
    COALESCE(pg.IdPickingEnc, mg.IdPickingEnc),
    COALESCE(pg.IdPedidoDet, mg.IdPedidoDet),
    COALESCE(pg.Lic_plate COLLATE DATABASE_DEFAULT, mg.Lic_plate COLLATE DATABASE_DEFAULT);

/* 6) Ejecucion controlada */
BEGIN TRAN;

IF @EjecutarDelete = 1
BEGIN
    RAISERROR('Ejecutando DELETE controlado de duplicados exactos VERI.', 10, 1) WITH NOWAIT;

    DELETE m
        OUTPUT
            @RunId,
            @FechaEjecucion,
            N'DELETE_VERI_DUPLICADO',
            DELETED.IdMovimiento,
            DELETED.IdTipoTarea,
            DELETED.IdTransaccion,
            DELETED.IdPedidoEnc,
            DELETED.IdPedidoDet,
            DELETED.IdRecepcion,
            DELETED.IdRecepcionDet,
            DELETED.IdProductoBodega,
            DELETED.IdUbicacionOrigen,
            DELETED.IdUbicacionDestino,
            DELETED.IdPresentacion,
            DELETED.IdEstadoOrigen,
            DELETED.IdEstadoDestino,
            DELETED.IdUnidadMedida,
            DELETED.barra_pallet,
            DELETED.lote,
            DELETED.fecha_vence,
            DELETED.cantidad,
            DELETED.fecha,
            DELETED.fecha_agr,
            DELETED.usuario_agr,
            c.IdMovimientoConservado,
            c.NumeroRepeticion,
            c.TotalRepeticiones
        INTO #BitacoraEliminados (
            RunId,
            FechaEjecucion,
            Accion,
            IdMovimiento,
            IdTipoTarea,
            IdTransaccion,
            IdPedidoEnc,
            IdPedidoDet,
            IdRecepcion,
            IdRecepcionDet,
            IdProductoBodega,
            IdUbicacionOrigen,
            IdUbicacionDestino,
            IdPresentacion,
            IdEstadoOrigen,
            IdEstadoDestino,
            IdUnidadMedida,
            barra_pallet,
            lote,
            fecha_vence,
            cantidad,
            fecha_movimiento,
            fecha_agr,
            usuario_agr,
            IdMovimientoConservado,
            NumeroRepeticion,
            TotalRepeticiones
        )
    FROM dbo.trans_movimientos m
    INNER JOIN #Candidatos c
        ON c.IdMovimiento = m.IdMovimiento
    WHERE c.NumeroRepeticion > 1;
END
ELSE
BEGIN
    RAISERROR('Modo solo auditoria: no se ejecuta DELETE.', 10, 1) WITH NOWAIT;
END;

/* 7) Bitacora de lo actualizado o de lo que se actualizaria */
IF @EjecutarDelete = 1
BEGIN
    SELECT
        *
    FROM #BitacoraEliminados
    ORDER BY
        IdTransaccion,
        IdPedidoDet,
        barra_pallet,
        lote,
        cantidad DESC,
        NumeroRepeticion,
        IdMovimiento;
END
ELSE
BEGIN
    SELECT
        @RunId AS RunId,
        @FechaEjecucion AS FechaEjecucion,
        N'DELETE_VERI_DUPLICADO_PROPUESTO' AS Accion,
        IdMovimiento,
        IdTipoTarea,
        IdTransaccion,
        IdPedidoEnc,
        IdPedidoDet,
        IdRecepcion,
        IdRecepcionDet,
        IdProductoBodega,
        IdUbicacionOrigen,
        IdUbicacionDestino,
        IdPresentacion,
        IdEstadoOrigen,
        IdEstadoDestino,
        IdUnidadMedida,
        barra_pallet,
        lote,
        fecha_vence,
        cantidad,
        fecha AS fecha_movimiento,
        fecha_agr,
        usuario_agr,
        IdMovimientoConservado,
        NumeroRepeticion,
        TotalRepeticiones
    FROM #Candidatos
    WHERE NumeroRepeticion > 1
    ORDER BY
        IdTransaccion,
        IdPedidoDet,
        barra_pallet,
        lote,
        cantidad DESC,
        NumeroRepeticion,
        IdMovimiento;
END;

IF @EjecutarDelete = 1 AND @ConfirmarCommit = 1
BEGIN
    COMMIT TRAN;
    RAISERROR('COMMIT ejecutado. Los duplicados exactos fueron eliminados.', 10, 1) WITH NOWAIT;
END
ELSE
BEGIN
    ROLLBACK TRAN;
    RAISERROR('ROLLBACK ejecutado. No se aplicaron cambios permanentes.', 10, 1) WITH NOWAIT;
END;

RAISERROR('Fin auditoria duplicados VERI.', 10, 1) WITH NOWAIT;
