
SELECT COUNT(1) FROM propietario_reglas_enc WHERE IdReglaRecepcion=4 AND IdPropietario=1

select * from producto_presentacion_tarima

delete from producto_presentacion_tarima where idpresentaciontarima=3

SELECT IdImpresora AS Código, empresa.nombre AS Empresa,
impresora.nombre AS Impresora, direccion_Ip AS 'Dirección IP' FROM Impresora 
INNER JOIN empresa ON impresora.IdEmpresa = empresa.IdEmpresa

delete from impresora

SELECT ISNULL(Max(Idimpresora),0) FROM impresora

SELECT IdImpresora AS Código, empresa.nombre AS Empresa, impresora.nombre AS Impresora, 
direccion_Ip AS 'Dirección IP' FROM Impresora 
INNER JOIN empresa ON impresora.IdEmpresa = empresa.IdEmpresa WHERE 1 > 0 

select * from trans_re_det_Infraccion

--delete from trans_re_det_infraccion

select * from trans_oc_enc
select * from trans_oc_det
select * from trans_re_det where idrecepcionenc=3
select * from trans_re_det where idpresentacion is null
select * from trans_re_enc where idrecepcionenc in (1,6,7,19)
select * from propietario_bodega where idpropietariobodega =6
select * from propietarios where idpropietario=4
select * from propietario_reglas_enc

SELECT DISTINCT p.nombre_comercial AS Propietario,b.nombre AS Bodega,r.nombre AS ReglaInfraccionada, 
oc.fecha_creacion AS FechaOC, renc.fecha_recepcion,pp.nombre AS Presentacion,pr.codigo AS CodigoProducto,pr.nombre AS Producto,
renc.IdRecepcionEnc, oc.IdOrdenCompraEnc,ocdet.cantidad AS CantidadSolicitada,ocdet.costo AS CostoOrdenCompra,
rencdet.cantidad_recibida AS CantidadRecibida,rencdet.costo AS CostoRecepcion,
r.IdReglaRecepcion
FROM trans_re_det_Infraccion AS i
INNER JOIN trans_re_enc AS renc ON i.IdRecepcionEnc = renc.IdRecepcionEnc
INNER Join trans_re_det AS rencdet ON renc.IdRecepcionEnc = rencdet.IdRecepcionEnc
INNER JOIN propietario_bodega AS pb ON renc.IdPropietarioBodega = pb.IdPropietarioBodega
INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario
INNER JOIN bodega AS b ON pb.IdBodega = b.IdBodega
INNER JOIN propietario_reglas_enc AS pre ON i.IdReglaPropietarioEnc = pre.IdReglaPropietarioEnc 
AND pre.IdPropietario = p.IdPropietario
INNER JOIN reglas_recepcion AS r ON pre.IdReglaRecepcion = r.IdReglaRecepcion
INNER JOIN trans_oc_enc AS oc ON i.IdOrdenCompraEnc = oc.IdOrdenCompraEnc
INNER JOIN trans_oc_det AS ocdet ON oc.IdOrdenCompraEnc = ocdet.IdOrdenCompraEnc
INNER JOIN producto_bodega AS pb2 ON i.IdProductoBodega = pb2.IdProductoBodega
INNER JOIN producto AS pr ON pb2.IdProducto = pr.IdProducto
INNER JOIN producto_presentacion AS pp ON i.IdPresentacion = pp.IdPresentacion
WHERE i.IdRecepcionEnc=7

select * from producto_presentacion
SELECT nombre FROM reglas_recepcion WHERE IdReglaRecepcion=1

select * from VW_Propietario_Regla_Recepcion

select * from producto

SELECT pb.IdProducto,p.codigo AS CodigoProducto,p.nombre AS Producto,det.* FROM trans_oc_det AS det INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto WHERE det.IdOrdenCompraEnc=1

