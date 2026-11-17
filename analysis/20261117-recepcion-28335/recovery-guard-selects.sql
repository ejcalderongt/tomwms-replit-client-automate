SELECT N'precondicion_1' AS prueba, CASE WHEN (NOT EXISTS (SELECT 1 FROM dbo.trans_re_enc WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND estado=N'Pendiente' AND activo=1 AND IdBodega=31
          AND Habilitar_Stock=1 AND ISNULL(bloqueada,0)=0)) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_2' AS prueba, CASE WHEN (NOT EXISTS (SELECT 1 FROM dbo.trans_oc_enc WITH(UPDLOCK,HOLDLOCK)
        WHERE IdOrdenCompraEnc=28359 AND IdBodega=31 AND activo=1 AND IdEstadoOC=3
          AND ISNULL(Enviado_A_ERP,0)=0 AND ISNULL(push_to_nav,0)=0)) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_3' AS prueba, CASE WHEN (NOT EXISTS (SELECT 1 FROM dbo.trans_oc_det WITH(UPDLOCK,HOLDLOCK)
        WHERE IdOrdenCompraEnc=28359 AND IdOrdenCompraDet=2 AND codigo_producto=N'AG00034526041365'
          AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159 AND cantidad=7 AND cantidad_recibida=6)) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_4' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.trans_re_det WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND IdOrdenCompraDet=2)<>2 OR
       (SELECT COUNT(*) FROM dbo.trans_re_det WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND IdRecepcionDet IN (46491,46492) AND IdOrdenCompraEnc=28359
          AND IdOrdenCompraDet=2 AND codigo_producto=N'AG00034526041365' AND No_Linea=1
          AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159 AND cantidad_recibida=3
          AND ISNULL(lic_plate,N'')=N'')<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_5' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.trans_oc_det_lote WITH(UPDLOCK,HOLDLOCK)
        WHERE IdOrdenCompraEnc=28359 AND IdOrdenCompraDet=2)<>2 OR
       (SELECT COUNT(*) FROM dbo.trans_oc_det_lote WITH(UPDLOCK,HOLDLOCK)
        WHERE IdOrdenCompraEnc=28359 AND IdOrdenCompraDet=2 AND IdProductoBodega=1431878
          AND IdProductoTallaColor=40159 AND no_linea=1 AND lic_plate=N'J100000635' AND activo=1
          AND ISNULL(cantidad_recibida,0)=0 AND
          ((IdOrdenCompraDetLote=1348 AND cantidad=4) OR (IdOrdenCompraDetLote=1349 AND cantidad=3)))<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_6' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.stock_rec WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159)<>2 OR
       (SELECT COUNT(*) FROM dbo.stock_rec WITH(UPDLOCK,HOLDLOCK)
        WHERE IdStockRec IN (48381,48382) AND IdRecepcionEnc=28335 AND IdRecepcionDet=46491
          AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159 AND cantidad=3
          AND IdUbicacion=747 AND activo=1 AND ISNULL(lic_plate,N'')=N'')<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_7' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.stock WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159)<>2 OR
       (SELECT COUNT(*) FROM dbo.stock WITH(UPDLOCK,HOLDLOCK)
        WHERE IdStock IN (649920,649921) AND IdRecepcionEnc=28335 AND IdRecepcionDet=46491
          AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159 AND cantidad=3
          AND IdUbicacion=747 AND activo=1 AND ISNULL(lic_plate,N'')=N''
          AND ISNULL(IdPedidoEnc,0)=0 AND ISNULL(IdPickingEnc,0)=0 AND ISNULL(IdDespachoEnc,0)=0)<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_8' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.trans_movimientos WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcion=28335 AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159)<>2 OR
       (SELECT COUNT(*) FROM dbo.trans_movimientos WITH(UPDLOCK,HOLDLOCK)
        WHERE IdMovimiento IN (856572,856573) AND IdRecepcion=28335 AND IdRecepcionDet=46491
          AND IdTransaccion=28335 AND IdProductoBodega=1431878 AND IdProductoTallaColor=40159
          AND cantidad=3 AND IdUbicacionOrigen=747 AND IdUbicacionDestino=747
          AND ISNULL(lic_plate,N'')=N'')<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_9' AS prueba, CASE WHEN ((SELECT COUNT(*) FROM dbo.i_nav_transacciones_out WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND (IdRecepcionDet IN (46491,46492) OR
             (IdProductoBodega=1431878 AND IdProductoTallaColor=40159)))<>2 OR
       (SELECT COUNT(*) FROM dbo.i_nav_transacciones_out WITH(UPDLOCK,HOLDLOCK)
        WHERE IdRecepcionEnc=28335 AND IdOrdenCompra=28359 AND IdProductoBodega=1431878
          AND IdProductoTallaColor=40159 AND cantidad=3 AND ISNULL(enviado,0)=0
          AND ISNULL(cantidad_enviada,0)=0 AND tipo_transaccion=N'INGRESO'
          AND ((idtransaccion=152013 AND IdRecepcionDet=46491) OR
               (idtransaccion=152014 AND IdRecepcionDet=46492)))<>2) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_10' AS prueba, CASE WHEN (EXISTS(SELECT 1 FROM dbo.stock_res WITH(UPDLOCK,HOLDLOCK) WHERE IdStock IN (649920,649921)) OR
       EXISTS(SELECT 1 FROM dbo.trans_picking_ubic WITH(UPDLOCK,HOLDLOCK) WHERE IdStock IN (649920,649921) OR IdStock_reemplazo IN (649920,649921)) OR
       EXISTS(SELECT 1 FROM dbo.trans_pe_det WITH(UPDLOCK,HOLDLOCK) WHERE IdStockEspecifico IN (649920,649921)) OR
       EXISTS(SELECT 1 FROM dbo.stock_hist WITH(UPDLOCK,HOLDLOCK) WHERE IdRecepcionEnc=28335 AND IdRecepcionDet IN (46491,46492)) OR
       EXISTS(SELECT 1 FROM dbo.producto_pallet WITH(UPDLOCK,HOLDLOCK) WHERE IdRecepcionEnc=28335 AND IdRecepcionDet IN (46491,46492)) OR
       EXISTS(SELECT 1 FROM dbo.trans_ubic_hh_stock WITH(UPDLOCK,HOLDLOCK) WHERE IdRecepcionEnc=28335 AND IdRecepcionDet IN (46491,46492))) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;
SELECT N'precondicion_11' AS prueba, CASE WHEN (EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN
        (OBJECT_ID('dbo.stock'),OBJECT_ID('dbo.stock_rec'),OBJECT_ID('dbo.trans_movimientos'),OBJECT_ID('dbo.trans_oc_det_lote')))) THEN N'BLOQUEAR' ELSE N'OK' END AS resultado;