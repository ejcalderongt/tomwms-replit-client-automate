/*
  #EJC20260522_RECEPCION_DET_READMODEL
  Read-model para carga de frmRecepcion en modo editar.

  Objetivo:
    Reducir roundtrips por linea al abrir una recepcion. El SP devuelve el
    detalle recibido ya enriquecido con producto, unidad, presentacion, estado,
    motivo, OC relacionada y talla/color.

  Contrato:
    - No escribe datos.
    - Mantiene columnas base de VW_Get_Detalle_By_IdRecepcionEnc requeridas por
      clsLnTrans_re_det.Cargar.
    - Agrega aliases con prefijo para hidratar objetos sin llamadas por linea.
*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_wms_recepcion_detalle_readmodel_v1
    @IdRecepcionEnc INT,
    @IdBodega INT
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        det.*,

        p.IdProducto AS Producto_IdProducto,
        p.IdPropietario AS Producto_IdPropietario,
        p.IdClasificacion AS Producto_IdClasificacion,
        p.IdUnidadMedidaBasica AS Producto_IdUnidadMedidaBasica,
        p.codigo AS Producto_Codigo,
        p.nombre AS Producto_Nombre,
        p.control_vencimiento AS Producto_ControlVencimiento,
        p.control_peso AS Producto_ControlPeso,
        p.control_lote AS Producto_ControlLote,
        p.peso_referencia AS Producto_PesoReferencia,
        p.IdTipoManufactura AS Producto_IdTipoManufactura,

        pp.IdPresentacion AS Presentacion_IdPresentacion,
        pp.IdProducto AS Presentacion_IdProducto,
        pp.codigo_barra AS Presentacion_CodigoBarra,
        pp.nombre AS Presentacion_Nombre,
        pp.peso AS Presentacion_Peso,
        pp.factor AS Presentacion_Factor,
        pp.activo AS Presentacion_Activo,
        pp.EsPallet AS Presentacion_EsPallet,
        pp.Costo AS Presentacion_Costo,
        pp.CamasPorTarima AS Presentacion_CamasPorTarima,
        pp.CajasPorCama AS Presentacion_CajasPorCama,
        pp.IdPresentacionPallet AS Presentacion_IdPresentacionPallet,
        pp.codigo AS Presentacion_Codigo,

        um.IdUnidadMedida AS Unidad_IdUnidadMedida,
        um.codigo AS Unidad_Codigo,
        um.Nombre AS Unidad_Nombre,
        um.factor AS Unidad_Factor,

        pe.IdEstado AS Estado_IdEstado,
        pe.Nombre AS Estado_Nombre,

        md.IdMotivoDevolucion AS Motivo_IdMotivoDevolucion,
        md.Nombre AS Motivo_Nombre,

        oc.Cantidad AS OC_CantidadSolicitada,
        oc.Costo AS OC_Costo,

        ta.IdTalla,
        ta.Codigo AS codigo_talla,
        co.IdColor,
        co.Codigo AS codigo_color,
        ptc.CodigoSKU AS ProductoTallaColor_CodigoSKU
    FROM dbo.VW_Get_Detalle_By_IdRecepcionEnc AS det
    INNER JOIN dbo.producto_bodega AS pb
        ON pb.IdProductoBodega = det.IdProductoBodega
    INNER JOIN dbo.producto AS p
        ON p.IdProducto = pb.IdProducto
    LEFT JOIN dbo.producto_presentacion AS pp
        ON pp.IdPresentacion = det.IdPresentacion
    LEFT JOIN dbo.unidad_medida AS um
        ON um.IdUnidadMedida = det.IdUnidadMedida
    LEFT JOIN dbo.producto_estado AS pe
        ON pe.IdEstado = det.IdProductoEstado
    LEFT JOIN dbo.motivo_devolucion AS md
        ON md.IdMotivoDevolucion = det.IdMotivoDevolucion
    LEFT JOIN dbo.trans_oc_det AS oc
        ON oc.IdOrdenCompraEnc = det.IdOrdenCompraEnc
       AND oc.IdOrdenCompraDet = det.IdOrdenCompraDet
    LEFT JOIN dbo.producto_talla_color AS ptc
        ON ptc.IdProductoTallaColor = det.IdProductoTallaColor
    LEFT JOIN dbo.talla AS ta
        ON ta.IdTalla = ptc.IdTalla
    LEFT JOIN dbo.color AS co
        ON co.IdColor = ptc.IdColor
    WHERE det.IdRecepcionEnc = @IdRecepcionEnc
      AND det.IdBodega = @IdBodega
    ORDER BY det.IdRecepcionDet;
END;
GO
