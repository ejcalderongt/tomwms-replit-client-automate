SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @IdRecepcionEnc INT = 17140;
    DECLARE @RegAntes INT;
    DECLARE @RegInsertados INT;

    SELECT @RegAntes = COUNT(*)
    FROM dbo.trans_re_det rd
    INNER JOIN dbo.stock_rec sr
      ON sr.IdRecepcionEnc = rd.IdRecepcionEnc
     AND sr.IdRecepcionDet = rd.IdRecepcionDet
    LEFT JOIN dbo.i_nav_transacciones_out io
      ON io.IdRecepcionEnc = rd.IdRecepcionEnc
     AND io.IdRecepcionDet = rd.IdRecepcionDet
    WHERE rd.IdRecepcionEnc = @IdRecepcionEnc
      AND io.IdRecepcionDet IS NULL;

    ;WITH Pendientes AS (
        SELECT
            b.IdEmpresa,
            re.IdBodega,
            pb.IdPropietario,
            re.IdPropietarioBodega,
            ro.IdOrdenCompraEnc,
            rd.IdRecepcionEnc,
            rd.IdProductoBodega,
            p.IdProducto,
            rd.IdUnidadMedida,
            NULLIF(rd.IdPresentacion, 0) AS IdPresentacion,
            rd.IdProductoEstado,
            rd.cantidad_recibida,
            rd.peso,
            rd.lote,
            CAST(rd.fecha_vence AS DATE) AS fecha_vence,
            CAST(rd.fecha_ingreso AS DATE) AS fecha_recepcion,
            oc.No_Documento,
            rd.No_Linea,
            p.codigo,
            p.nombre,
            od.atributo_variante_1,
            rd.nombre_unidad_medida,
            oc.IdTipoIngresoOC,
            rd.fec_agr,
            rd.user_agr,
            rd.lic_plate,
            sr.uds_lic_plate,
            ISNULL(od.cantidad, rd.cantidad_recibida) AS Cantidad_Esperada,
            p.codigo_barra,
            od.valor_aduana,
            od.valor_fob,
            od.valor_iva,
            od.valor_dai,
            od.valor_seguro,
            od.valor_flete,
            od.peso_neto,
            od.peso_bruto,
            rd.IdRecepcionDet,
            rd.IdProductoTallaColor
        FROM dbo.trans_re_det rd
        INNER JOIN dbo.trans_re_enc re
          ON re.IdRecepcionEnc = rd.IdRecepcionEnc
        INNER JOIN dbo.trans_re_oc ro
          ON ro.IdRecepcionEnc = rd.IdRecepcionEnc
        INNER JOIN dbo.trans_oc_enc oc
          ON oc.IdOrdenCompraEnc = ro.IdOrdenCompraEnc
        LEFT JOIN dbo.trans_oc_det od
          ON od.IdOrdenCompraEnc = ro.IdOrdenCompraEnc
         AND od.No_Linea = rd.No_Linea
         AND od.IdProductoBodega = rd.IdProductoBodega
        LEFT JOIN dbo.producto_bodega pbod
          ON pbod.IdProductoBodega = rd.IdProductoBodega
        LEFT JOIN dbo.producto p
          ON p.IdProducto = pbod.IdProducto
        LEFT JOIN dbo.propietario_bodega pb
          ON pb.IdPropietarioBodega = re.IdPropietarioBodega
        LEFT JOIN dbo.bodega b
          ON b.IdBodega = re.IdBodega
        INNER JOIN dbo.stock_rec sr
          ON sr.IdRecepcionEnc = rd.IdRecepcionEnc
         AND sr.IdRecepcionDet = rd.IdRecepcionDet
        LEFT JOIN dbo.i_nav_transacciones_out io
          ON io.IdRecepcionEnc = rd.IdRecepcionEnc
         AND io.IdRecepcionDet = rd.IdRecepcionDet
        WHERE rd.IdRecepcionEnc = @IdRecepcionEnc
          AND io.IdRecepcionDet IS NULL
    )
    INSERT INTO dbo.i_nav_transacciones_out (
        idempresa,
        idbodega,
        idpropietario,
        idpropietariobodega,
        idordencompra,
        idrecepcionenc,
        idpedidoenc,
        iddespachoenc,
        idproductobodega,
        idproducto,
        idunidadmedida,
        idpresentacion,
        idproductoestado,
        cantidad,
        peso,
        lote,
        fecha_vence,
        fecha_recepcion,
        no_pedido,
        no_linea,
        codigo_producto,
        nombre_producto,
        codigo_variante,
        unidad_medida,
        tipo_transaccion,
        enviado,
        fec_agr,
        user_agr,
        fec_mod,
        user_mod,
        Cantidad_Esperada,
        lic_plate,
        uds_lic_plate,
        cantidad_presentacion,
        IdTipoDocumento,
        es_servicio,
        codigo_barra,
        valor_aduana,
        valor_fob,
        valor_iva,
        valor_dai,
        valor_seguro,
        valor_flete,
        peso_neto,
        peso_bruto,
        fecha_despacho,
        no_documento_salida_ref_devol,
        IdPedidoEncDevol,
        IdDespachoDet,
        IdRecepcionDet,
        cantidad_enviada,
        cantidad_pendiente,
        auditar,
        Talla,
        Color,
        IdProductoTallaColor
    )
    SELECT
        IdEmpresa,
        IdBodega,
        IdPropietario,
        IdPropietarioBodega,
        IdOrdenCompraEnc,
        IdRecepcionEnc,
        0,
        0,
        IdProductoBodega,
        IdProducto,
        IdUnidadMedida,
        ISNULL(IdPresentacion, 0),
        IdProductoEstado,
        cantidad_recibida,
        peso,
        lote,
        fecha_vence,
        fecha_recepcion,
        No_Documento,
        CONVERT(NVARCHAR(100), No_Linea),
        codigo,
        nombre,
        atributo_variante_1,
        nombre_unidad_medida,
        N'INGRESO',
        0,
        GETDATE(),
        user_agr,
        GETDATE(),
        user_agr,
        Cantidad_Esperada,
        lic_plate,
        uds_lic_plate,
        CASE WHEN ISNULL(IdPresentacion, 0) > 0 THEN cantidad_recibida ELSE NULL END,
        IdTipoIngresoOC,
        0,
        codigo_barra,
        valor_aduana,
        valor_fob,
        valor_iva,
        valor_dai,
        valor_seguro,
        valor_flete,
        peso_neto,
        peso_bruto,
        CONVERT(DATE, '19000101', 112),
        NULL,
        0,
        0,
        IdRecepcionDet,
        0,
        0,
        0,
        NULL,
        NULL,
        IdProductoTallaColor
    FROM Pendientes;

    SET @RegInsertados = @@ROWCOUNT;

    IF @RegAntes <> @RegInsertados
    BEGIN
        THROW 51001, 'La cantidad de registros faltantes no coincide con los insertados en i_nav_transacciones_out.', 1;
    END;

    SELECT
        @RegAntes AS RegistrosFaltantesAntes,
        @RegInsertados AS RegistrosInsertados;

    SELECT
        idtransaccion,
        idrecepcionenc,
        IdRecepcionDet,
        no_linea,
        codigo_producto,
        cantidad,
        lic_plate,
        tipo_transaccion,
        enviado,
        fec_agr
    FROM dbo.i_nav_transacciones_out
    WHERE idrecepcionenc = @IdRecepcionEnc
    ORDER BY IdRecepcionDet;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
