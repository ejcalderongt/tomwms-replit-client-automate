SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @RegStockRecHuerfanosAntes INT;
    DECLARE @RegSinDetalle17140Antes INT;
    DECLARE @RegFixConDetalle INT;
    DECLARE @RegFix17140 INT;

    SELECT @RegStockRecHuerfanosAntes = COUNT(*)
    FROM dbo.stock_rec sr
    LEFT JOIN dbo.trans_re_det d
      ON d.IdRecepcionEnc = sr.IdRecepcionEnc
     AND d.IdRecepcionDet = sr.IdRecepcionDet
    WHERE ISNULL(sr.IdRecepcionEnc, 0) <> 0
      AND (ISNULL(sr.IdRecepcionDet, 0) = 0 OR d.IdRecepcionDet IS NULL);

    SELECT @RegSinDetalle17140Antes = COUNT(*)
    FROM dbo.stock_rec sr
    WHERE sr.IdRecepcionEnc = 17140
      AND NOT EXISTS (
          SELECT 1
          FROM dbo.trans_re_det d
          WHERE d.IdRecepcionEnc = sr.IdRecepcionEnc
      );

    IF OBJECT_ID('tempdb..#FixConDetalle') IS NOT NULL DROP TABLE #FixConDetalle;

    ;WITH Orphan AS (
        SELECT sr.IdStockRec,
               sr.IdRecepcionEnc,
               sr.IdProductoBodega,
               sr.No_linea,
               sr.Cantidad,
               sr.Lic_plate,
               sr.Fec_agr
        FROM dbo.stock_rec sr
        LEFT JOIN dbo.trans_re_det d
          ON d.IdRecepcionEnc = sr.IdRecepcionEnc
         AND d.IdRecepcionDet = sr.IdRecepcionDet
        WHERE ISNULL(sr.IdRecepcionEnc, 0) <> 0
          AND ISNULL(sr.IdRecepcionDet, 0) = 0
          AND d.IdRecepcionDet IS NULL
          AND EXISTS (
              SELECT 1
              FROM dbo.trans_re_det dx
              WHERE dx.IdRecepcionEnc = sr.IdRecepcionEnc
          )
    ),
    MatchDet AS (
        SELECT o.IdStockRec,
               d.IdRecepcionDet,
               ROW_NUMBER() OVER (
                   PARTITION BY o.IdStockRec
                   ORDER BY
                       CASE WHEN ISNULL(o.Lic_plate, '') <> ''
                                  AND ISNULL(o.Lic_plate, '') = ISNULL(d.Lic_plate, '')
                            THEN 0 ELSE 1 END,
                       ABS(ISNULL(o.Cantidad, 0) - ISNULL(d.cantidad_recibida, 0)),
                       d.IdRecepcionDet
               ) AS rn
        FROM Orphan o
        INNER JOIN dbo.trans_re_det d
          ON d.IdRecepcionEnc = o.IdRecepcionEnc
         AND d.IdProductoBodega = o.IdProductoBodega
         AND ISNULL(d.No_Linea, 0) = ISNULL(o.No_linea, 0)
         AND (
              (ISNULL(o.Lic_plate, '') <> ''
               AND ISNULL(o.Lic_plate, '') = ISNULL(d.Lic_plate, ''))
              OR ABS(ISNULL(o.Cantidad, 0) - ISNULL(d.cantidad_recibida, 0)) < 0.0001
         )
    )
    SELECT o.IdStockRec,
           o.IdRecepcionEnc,
           o.IdProductoBodega,
           o.No_linea,
           o.Cantidad,
           o.Lic_plate,
           o.Fec_agr,
           m.IdRecepcionDet AS NuevoIdRecepcionDet
    INTO #FixConDetalle
    FROM Orphan o
    INNER JOIN MatchDet m
      ON m.IdStockRec = o.IdStockRec
     AND m.rn = 1;

    SELECT @RegFixConDetalle = COUNT(*) FROM #FixConDetalle;

    IF @RegFixConDetalle <> 161
    BEGIN
        THROW 51000, 'Validacion fallida: se esperaban 161 stock_rec reamarrables a trans_re_det existente.', 1;
    END;

    DECLARE @Map17140 TABLE (
        IdStockRec INT NOT NULL PRIMARY KEY,
        IdRecepcionEnc INT NOT NULL,
        IdProductoBodega INT NOT NULL,
        No_linea INT NULL,
        Cantidad FLOAT NULL,
        Lic_plate NVARCHAR(100) NULL,
        NuevoIdRecepcionDet INT NOT NULL
    );

    ;WITH Src17140 AS (
        SELECT sr.IdStockRec,
               sr.IdRecepcionEnc,
               sr.IdProductoBodega,
               sr.No_linea,
               sr.Cantidad,
               sr.Lic_plate,
               sr.IdPresentacion,
               sr.IdUnidadMedida,
               sr.IdProductoEstado,
               sr.Lote,
               sr.Fecha_vence,
               sr.Fecha_Ingreso,
               sr.Peso,
               sr.User_agr,
               sr.Fec_agr,
               sr.Pallet_No_Estandar,
               sr.IdProductoTallaColor,
               od.IdOrdenCompraEnc,
               od.IdOrdenCompraDet,
               od.Costo,
               od.codigo_producto,
               COALESCE(od.nombre_producto, p.Nombre) AS nombre_producto,
               COALESCE(od.nombre_presentacion, pp.Nombre, '') AS nombre_presentacion,
               COALESCE(od.nombre_unidad_medida_basica, um.Nombre, '') AS nombre_unidad_medida,
               COALESCE(pe.Nombre, '') AS nombre_producto_estado
        FROM dbo.stock_rec sr
        INNER JOIN dbo.trans_re_oc ro
          ON ro.IdRecepcionEnc = sr.IdRecepcionEnc
        INNER JOIN dbo.trans_oc_det od
          ON od.IdOrdenCompraEnc = ro.IdOrdenCompraEnc
         AND od.IdProductoBodega = sr.IdProductoBodega
         AND od.No_Linea = sr.No_linea
        LEFT JOIN dbo.producto_bodega pb
          ON pb.IdProductoBodega = sr.IdProductoBodega
        LEFT JOIN dbo.producto p
          ON p.IdProducto = pb.IdProducto
        LEFT JOIN dbo.producto_presentacion pp
          ON pp.IdPresentacion = sr.IdPresentacion
        LEFT JOIN dbo.unidad_medida um
          ON um.IdUnidadMedida = sr.IdUnidadMedida
        LEFT JOIN dbo.producto_estado pe
          ON pe.IdEstado = sr.IdProductoEstado
        WHERE sr.IdRecepcionEnc = 17140
          AND ISNULL(sr.IdRecepcionDet, 0) = 0
          AND NOT EXISTS (
              SELECT 1
              FROM dbo.trans_re_det d
              WHERE d.IdRecepcionEnc = sr.IdRecepcionEnc
                AND d.IdProductoBodega = sr.IdProductoBodega
                AND ISNULL(d.No_Linea, 0) = ISNULL(sr.No_linea, 0)
                AND ISNULL(d.Lic_plate, '') = ISNULL(sr.Lic_plate, '')
          )
    )
    MERGE dbo.trans_re_det AS tgt
    USING Src17140 AS src
       ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (
            IdRecepcionEnc,
            IdProductoBodega,
            IdPresentacion,
            IdUnidadMedida,
            IdProductoEstado,
            IdOperadorBodega,
            IdMotivoDevolucion,
            No_Linea,
            cantidad_recibida,
            nombre_producto,
            nombre_presentacion,
            nombre_unidad_medida,
            nombre_producto_estado,
            lote,
            fecha_vence,
            fecha_ingreso,
            peso,
            peso_estadistico,
            peso_minimo,
            peso_maximo,
            peso_unitario,
            user_agr,
            fec_agr,
            observacion,
            añada,
            costo,
            costo_oc,
            costo_estadistico,
            atributo_variante_1,
            codigo_producto,
            lic_plate,
            pallet_no_estandar,
            IdOrdenCompraEnc,
            IdOrdenCompraDet,
            IdJornadaSistema,
            host,
            IdProductoTallaColor
        )
        VALUES (
            src.IdRecepcionEnc,
            src.IdProductoBodega,
            NULLIF(src.IdPresentacion, 0),
            src.IdUnidadMedida,
            src.IdProductoEstado,
            NULL,
            NULL,
            src.No_linea,
            src.Cantidad,
            src.nombre_producto,
            src.nombre_presentacion,
            src.nombre_unidad_medida,
            src.nombre_producto_estado,
            ISNULL(src.Lote, ''),
            src.Fecha_vence,
            src.Fecha_Ingreso,
            src.Peso,
            NULL,
            NULL,
            NULL,
            NULL,
            src.User_agr,
            src.Fec_agr,
            '',
            NULL,
            src.Costo,
            NULL,
            NULL,
            NULL,
            src.codigo_producto,
            src.Lic_plate,
            src.Pallet_No_Estandar,
            src.IdOrdenCompraEnc,
            src.IdOrdenCompraDet,
            0,
            'DATAFIX_20260520',
            src.IdProductoTallaColor
        )
    OUTPUT src.IdStockRec,
           src.IdRecepcionEnc,
           src.IdProductoBodega,
           src.No_linea,
           src.Cantidad,
           src.Lic_plate,
           inserted.IdRecepcionDet
      INTO @Map17140 (
           IdStockRec,
           IdRecepcionEnc,
           IdProductoBodega,
           No_linea,
           Cantidad,
           Lic_plate,
           NuevoIdRecepcionDet
      );

    INSERT INTO @Map17140 (
        IdStockRec,
        IdRecepcionEnc,
        IdProductoBodega,
        No_linea,
        Cantidad,
        Lic_plate,
        NuevoIdRecepcionDet
    )
    SELECT sr.IdStockRec,
           sr.IdRecepcionEnc,
           sr.IdProductoBodega,
           sr.No_linea,
           sr.Cantidad,
           sr.Lic_plate,
           d.IdRecepcionDet
    FROM dbo.stock_rec sr
    INNER JOIN dbo.trans_re_det d
      ON d.IdRecepcionEnc = sr.IdRecepcionEnc
     AND d.IdProductoBodega = sr.IdProductoBodega
     AND ISNULL(d.No_Linea, 0) = ISNULL(sr.No_linea, 0)
     AND ISNULL(d.Lic_plate, '') = ISNULL(sr.Lic_plate, '')
    WHERE sr.IdRecepcionEnc = 17140
      AND ISNULL(sr.IdRecepcionDet, 0) = 0
      AND NOT EXISTS (
          SELECT 1
          FROM @Map17140 m
          WHERE m.IdStockRec = sr.IdStockRec
      );

    SELECT @RegFix17140 = COUNT(*) FROM @Map17140;

    IF @RegFix17140 <> 2
    BEGIN
        THROW 51001, 'Validacion fallida: se esperaban 2 trans_re_det reconstruidos/mapeados para recepcion 17140.', 1;
    END;

    IF OBJECT_ID('tempdb..#FixAll') IS NOT NULL DROP TABLE #FixAll;

    SELECT IdStockRec,
           IdRecepcionEnc,
           IdProductoBodega,
           No_linea,
           Cantidad,
           Lic_plate,
           Fec_agr,
           NuevoIdRecepcionDet
    INTO #FixAll
    FROM #FixConDetalle
    UNION ALL
    SELECT IdStockRec,
           IdRecepcionEnc,
           IdProductoBodega,
           No_linea,
           Cantidad,
           Lic_plate,
           NULL AS Fec_agr,
           NuevoIdRecepcionDet
    FROM @Map17140;

    UPDATE sr
       SET sr.IdRecepcionDet = f.NuevoIdRecepcionDet
    FROM dbo.stock_rec sr
    INNER JOIN #FixAll f
      ON f.IdStockRec = sr.IdStockRec
    WHERE ISNULL(sr.IdRecepcionDet, 0) = 0;

    UPDATE s
       SET s.IdRecepcionDet = f.NuevoIdRecepcionDet
    FROM dbo.stock s
    INNER JOIN #FixAll f
      ON f.IdRecepcionEnc = s.IdRecepcionEnc
     AND f.IdProductoBodega = s.IdProductoBodega
     AND ISNULL(f.Lic_plate, '') = ISNULL(s.Lic_plate, '')
     AND ABS(ISNULL(f.Cantidad, 0) - ISNULL(s.Cantidad, 0)) < 0.0001
    WHERE ISNULL(s.IdRecepcionDet, 0) = 0;

    IF OBJECT_ID('tempdb..#MovMap') IS NOT NULL DROP TABLE #MovMap;

    ;WITH Mov AS (
        SELECT m.IdMovimiento,
               m.IdRecepcion,
               m.IdProductoBodega,
               m.Cantidad,
               m.Lic_plate,
               m.Fecha,
               ROW_NUMBER() OVER (
                   PARTITION BY m.IdRecepcion, m.IdProductoBodega, m.Cantidad
                   ORDER BY m.Fecha, m.IdMovimiento
               ) AS rn
        FROM dbo.trans_movimientos m
        WHERE ISNULL(m.IdRecepcion, 0) <> 0
          AND ISNULL(m.IdRecepcionDet, 0) = 0
          AND EXISTS (
              SELECT 1
              FROM #FixAll f
              WHERE f.IdRecepcionEnc = m.IdRecepcion
                AND f.IdProductoBodega = m.IdProductoBodega
                AND ABS(ISNULL(f.Cantidad, 0) - ISNULL(m.Cantidad, 0)) < 0.0001
          )
    ),
    Fix AS (
        SELECT f.*,
               ROW_NUMBER() OVER (
                   PARTITION BY f.IdRecepcionEnc, f.IdProductoBodega, f.Cantidad
                   ORDER BY COALESCE(f.Fec_agr, '19000101'), f.IdStockRec
               ) AS rn
        FROM #FixAll f
    )
    SELECT m.IdMovimiento,
           f.NuevoIdRecepcionDet
    INTO #MovMap
    FROM Mov m
    INNER JOIN Fix f
      ON f.IdRecepcionEnc = m.IdRecepcion
     AND f.IdProductoBodega = m.IdProductoBodega
     AND ABS(ISNULL(f.Cantidad, 0) - ISNULL(m.Cantidad, 0)) < 0.0001
     AND f.rn = m.rn;

    UPDATE m
       SET m.IdRecepcionDet = mm.NuevoIdRecepcionDet
    FROM dbo.trans_movimientos m
    INNER JOIN #MovMap mm
      ON mm.IdMovimiento = m.IdMovimiento
    WHERE ISNULL(m.IdRecepcionDet, 0) = 0;

    DECLARE @HuerfanosDespues INT;
    DECLARE @StockDespues INT;
    DECLARE @MovDespues INT;

    SELECT @HuerfanosDespues = COUNT(*)
    FROM dbo.stock_rec sr
    LEFT JOIN dbo.trans_re_det d
      ON d.IdRecepcionEnc = sr.IdRecepcionEnc
     AND d.IdRecepcionDet = sr.IdRecepcionDet
    WHERE ISNULL(sr.IdRecepcionEnc, 0) <> 0
      AND (ISNULL(sr.IdRecepcionDet, 0) = 0 OR d.IdRecepcionDet IS NULL);

    SELECT @StockDespues = COUNT(*)
    FROM dbo.stock s
    INNER JOIN #FixAll f
      ON f.IdRecepcionEnc = s.IdRecepcionEnc
     AND f.IdProductoBodega = s.IdProductoBodega
     AND ISNULL(f.Lic_plate, '') = ISNULL(s.Lic_plate, '')
     AND ABS(ISNULL(f.Cantidad, 0) - ISNULL(s.Cantidad, 0)) < 0.0001
    WHERE 1 = 1
      AND ISNULL(s.IdRecepcionDet, 0) = 0;

    SELECT @MovDespues = COUNT(*)
    FROM dbo.trans_movimientos m
    INNER JOIN #MovMap mm
      ON mm.IdMovimiento = m.IdMovimiento
    WHERE 1 = 1
      AND ISNULL(m.IdRecepcionDet, 0) = 0;

    IF @HuerfanosDespues <> 0
    BEGIN
        THROW 51002, 'Validacion fallida: aun quedan stock_rec huerfanos.', 1;
    END;

    IF @StockDespues <> 0
    BEGIN
        THROW 51003, 'Validacion fallida: aun quedan stock con IdRecepcionDet cero/nulo en recepciones corregidas.', 1;
    END;

    IF @MovDespues <> 0
    BEGIN
        THROW 51004, 'Validacion fallida: aun quedan movimientos con IdRecepcionDet cero/nulo en recepciones corregidas.', 1;
    END;

    SELECT 'ANTES' AS Etapa,
           @RegStockRecHuerfanosAntes AS StockRecHuerfanos,
           @RegSinDetalle17140Antes AS StockRec17140SinDetalle,
           NULL AS StockRecHuerfanosDespues,
           NULL AS StockCeroDespues,
           NULL AS MovimientosCeroDespues;

    SELECT 'DESPUES' AS Etapa,
           NULL AS StockRecHuerfanos,
           NULL AS StockRec17140SinDetalle,
           @HuerfanosDespues AS StockRecHuerfanosDespues,
           @StockDespues AS StockCeroDespues,
           @MovDespues AS MovimientosCeroDespues;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
