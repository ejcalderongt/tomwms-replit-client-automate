/*
#EJC20260603_FIX_VERIFICACION_VIEWS_ASCII
Recrea vistas de verificacion usando nombres de columnas escapados con [].
Se aplica cuando sqlcmd falla por caracteres especiales en script fuente.
*/

SET NOCOUNT ON;
GO

CREATE OR ALTER VIEW dbo.VW_Verificacion
AS
SELECT
    ped.IdPedidoEnc,
    ped.IdPedidoDet,
    pubic.IdProductoBodega,
    pubic.lote,
    pubic.fecha_vence,
    pubic.lic_plate,
    ped.nom_unid_med,
    ped.nombre_producto,
    ped.nom_estado,
    SUM(pubic.cantidad_solicitada) AS cantidad_solicitada,
    SUM(pubic.cantidad_recibida) AS cantidad_recibida,
    SUM(pubic.cantidad_verificada) AS cantidad_verificada,
    ped.IdPresentacion,
    ped.IdUnidadMedidaBasica,
    P.codigo,
    ped.ndias,
    SUM(pubic.cantidad_recibida) - SUM(pubic.cantidad_verificada) AS diferencia,
    pubic.IdPresentacion AS IdPresentacionPicking,
    dbo.producto_presentacion.nombre AS nom_presentacion,
    pubic.IdProductoEstado,
    dbo.Nombre_Area(bodega_ubicacion.IdArea, pubic.IdBodega) AS NombreArea,
    pc.nombre AS NombreClasificacion,
    CASE WHEN P.IdTipoManufactura = 1 THEN 'Bono' ELSE ISNULL(mt.nombre, '') END AS Bono,
    pubic.IdProductoTallaColor,
    c.Codigo AS Codigo_Color,
    t.Codigo AS Codigo_Talla,
    c.Nombre AS Nombre_Color,
    t.Nombre AS Nombre_Talla,
    ptc.CodigoSKU
FROM dbo.bodega_ubicacion
INNER JOIN dbo.trans_pe_det ped
    INNER JOIN dbo.trans_picking_det pkdet ON ped.IdPedidoDet = pkdet.IdPedidoDet
    INNER JOIN dbo.trans_picking_ubic pubic ON pkdet.IdPickingDet = pubic.IdPickingDet
ON dbo.bodega_ubicacion.IdUbicacion = pubic.IdUbicacion
AND dbo.bodega_ubicacion.IdBodega = pubic.IdBodega
LEFT OUTER JOIN dbo.producto P
    INNER JOIN dbo.producto_bodega pb ON P.IdProducto = pb.IdProducto
ON pubic.IdProductoBodega = pb.IdProductoBodega
LEFT OUTER JOIN dbo.producto_presentacion ON pubic.IdPresentacion = dbo.producto_presentacion.IdPresentacion
LEFT OUTER JOIN dbo.producto_clasificacion pc ON pc.IdClasificacion = P.IdClasificacion
LEFT OUTER JOIN dbo.trans_manufactura_enc me ON me.IdPedidoEnc = ped.IdPedidoEnc
LEFT OUTER JOIN dbo.trans_manufactura_tipo mt ON mt.idtipomanufactura = me.IdTipoManufactura
LEFT OUTER JOIN dbo.producto_talla_color ptc ON pubic.IdProductoTallaColor = ptc.IdProductoTallaColor
LEFT OUTER JOIN dbo.talla t ON ptc.IdTalla = t.IdTalla
LEFT OUTER JOIN dbo.color c ON ptc.IdColor = c.IdColor
WHERE pubic.[dañado_verificacion] = 0
  AND pubic.[dañado_picking] = 0
  AND pubic.no_encontrado = 0
  AND pkdet.IdPickingEnc NOT IN (
      SELECT IdPickingEnc
      FROM dbo.trans_picking_enc
      WHERE estado = 'Anulado'
  )
GROUP BY
    ped.IdPedidoEnc, ped.IdPedidoDet, ped.IdProductoBodega, pubic.lote, pubic.fecha_vence,
    ped.nom_unid_med, ped.nombre_producto, ped.nom_estado, ped.IdPresentacion, ped.IdUnidadMedidaBasica,
    P.codigo, ped.ndias, pubic.[dañado_verificacion], pubic.lic_plate, pubic.IdPresentacion,
    dbo.producto_presentacion.nombre, pubic.IdProductoBodega, pubic.IdProductoEstado, pubic.IdBodega,
    pc.nombre, dbo.bodega_ubicacion.IdArea, P.IdTipoManufactura, mt.nombre, pubic.IdProductoTallaColor,
    c.Codigo, t.Codigo, c.Nombre, t.Nombre, ptc.CodigoSKU;
GO

CREATE OR ALTER VIEW dbo.VW_Verificacion_Detallado_Sin_Licencia
AS
SELECT
    ped.IdPedidoEnc,
    0 AS IdPedidoDet,
    pubic.IdProductoBodega,
    pubic.lote,
    pubic.fecha_vence,
    '' AS lic_plate,
    ped.nom_unid_med,
    ped.nombre_producto,
    ped.nom_estado,
    SUM(pubic.cantidad_solicitada) AS cantidad_solicitada,
    SUM(pubic.cantidad_recibida) AS cantidad_recibida,
    SUM(pubic.cantidad_verificada) AS cantidad_verificada,
    ped.IdPresentacion,
    ped.IdUnidadMedidaBasica,
    P.codigo,
    ped.ndias,
    SUM(pubic.cantidad_recibida) - SUM(pubic.cantidad_verificada) AS diferencia,
    pubic.IdPresentacion AS IdPresentacionPicking,
    dbo.producto_presentacion.nombre AS nom_presentacion,
    pubic.IdProductoEstado,
    dbo.Nombre_Area(bodega_ubicacion.IdArea, pubic.IdBodega) AS NombreArea,
    pc.nombre AS NombreClasificacion,
    CASE WHEN P.IdTipoManufactura = 1 THEN 'Bono' ELSE ISNULL(mt.nombre, '') END AS Bono,
    pubic.IdProductoTallaColor,
    c.Codigo AS Codigo_Color,
    t.Codigo AS Codigo_Talla,
    c.Nombre AS Nombre_Color,
    t.Nombre AS Nombre_Talla,
    ptc.CodigoSKU
FROM dbo.bodega_ubicacion
INNER JOIN dbo.trans_pe_det ped
    INNER JOIN dbo.trans_picking_det pkdet ON ped.IdPedidoDet = pkdet.IdPedidoDet
    INNER JOIN dbo.trans_picking_ubic pubic ON pkdet.IdPickingDet = pubic.IdPickingDet
ON dbo.bodega_ubicacion.IdUbicacion = pubic.IdUbicacion
AND dbo.bodega_ubicacion.IdBodega = pubic.IdBodega
LEFT OUTER JOIN dbo.producto P
    INNER JOIN dbo.producto_bodega pb ON P.IdProducto = pb.IdProducto
ON pubic.IdProductoBodega = pb.IdProductoBodega
LEFT OUTER JOIN dbo.producto_presentacion ON pubic.IdPresentacion = dbo.producto_presentacion.IdPresentacion
LEFT OUTER JOIN dbo.producto_clasificacion pc ON pc.IdClasificacion = P.IdClasificacion
LEFT OUTER JOIN dbo.trans_manufactura_enc me ON me.IdPedidoEnc = ped.IdPedidoEnc
LEFT OUTER JOIN dbo.trans_manufactura_tipo mt ON mt.idtipomanufactura = me.IdTipoManufactura
LEFT OUTER JOIN dbo.producto_talla_color ptc ON ptc.IdProductoTallaColor = pubic.IdProductoTallaColor
LEFT OUTER JOIN dbo.color c ON c.IdColor = ptc.IdColor
LEFT OUTER JOIN dbo.talla t ON t.IdTalla = ptc.IdTalla
WHERE pubic.[dañado_verificacion] = 0
  AND pubic.[dañado_picking] = 0
  AND pubic.no_encontrado = 0
  AND pkdet.IdPickingEnc NOT IN (
      SELECT IdPickingEnc
      FROM dbo.trans_picking_enc
      WHERE estado = 'Anulado'
  )
GROUP BY
    ped.IdPedidoEnc, ped.IdProductoBodega, pubic.lote, pubic.fecha_vence, ped.nom_unid_med,
    ped.nombre_producto, ped.nom_estado, ped.IdPresentacion, ped.IdUnidadMedidaBasica, P.codigo, ped.ndias,
    pubic.[dañado_verificacion], pubic.IdPresentacion, dbo.producto_presentacion.nombre, pubic.IdProductoBodega,
    pubic.IdProductoEstado, pubic.IdBodega, pc.nombre, dbo.bodega_ubicacion.IdArea, P.IdTipoManufactura, mt.nombre,
    pubic.IdProductoTallaColor, c.Codigo, t.Codigo, c.Nombre, t.Nombre, ptc.CodigoSKU;
GO
