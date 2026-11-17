-- #CKFK20261117_REC_CM_RCA. Solo lectura.
SELECT IdRecepcionEnc, estado, activo, Habilitar_Stock, IdBodega, bloqueada, fec_agr, fec_mod
FROM dbo.trans_re_enc WHERE IdRecepcionEnc=28335;
SELECT IdRecepcionEnc, IdRecepcionDet, IdOrdenCompraEnc, IdOrdenCompraDet, No_Linea,
       codigo_producto, IdProductoBodega, IdProductoTallaColor, cantidad_recibida, lic_plate, fec_agr
FROM dbo.trans_re_det WHERE IdRecepcionEnc=28335 ORDER BY IdRecepcionDet;
SELECT IdOrdenCompraEnc, IdOrdenCompraDet, IdOrdenCompraDetLote, no_linea, codigo_producto,
       IdProductoBodega, IdProductoTallaColor, Talla, Color, cantidad, cantidad_recibida, lic_plate
FROM dbo.trans_oc_det_lote WHERE lic_plate='J100000635' ORDER BY IdOrdenCompraEnc,IdOrdenCompraDet,IdOrdenCompraDetLote;
SELECT IdOrdenCompraEnc, IdOrdenCompraDet, No_Linea, codigo_producto, IdProductoBodega,
       IdProductoTallaColor, cantidad, cantidad_recibida
FROM dbo.trans_oc_det WHERE IdOrdenCompraEnc IN
 (SELECT IdOrdenCompraEnc FROM dbo.trans_re_det WHERE IdRecepcionEnc=28335)
ORDER BY IdOrdenCompraDet;
SELECT IdStockRec, IdRecepcionEnc, IdRecepcionDet, IdProductoBodega, IdProductoTallaColor,
       Talla, Color, cantidad, lic_plate, IdUbicacion, regularizado, activo
FROM dbo.stock_rec WHERE IdRecepcionEnc=28335 OR lic_plate='J100000635';
SELECT IdStock, IdRecepcionEnc, IdRecepcionDet, IdProductoBodega, IdProductoTallaColor,
       cantidad, lic_plate, IdUbicacion, IdPickingEnc, IdPedidoEnc, IdDespachoEnc, activo
FROM dbo.stock WHERE IdRecepcionEnc=28335 OR lic_plate='J100000635';
SELECT IdMovimiento, IdTransaccion, IdRecepcion, IdRecepcionDet, IdProductoBodega,
       IdProductoTallaColor, cantidad, cantidad_hist, lic_plate, IdUbicacionOrigen, IdUbicacionDestino, fecha
FROM dbo.trans_movimientos WHERE IdRecepcion=28335 OR lic_plate='J100000635' ORDER BY IdMovimiento;
SELECT IdBodega, Control_Talla_Color, bloquear_lp_hh FROM dbo.bodega
WHERE IdBodega IN (SELECT IdBodega FROM dbo.trans_re_enc WHERE IdRecepcionEnc=28335);
