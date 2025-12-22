Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles

' #MA20251219
Partial Public Class clsLnTrans_verificacion_etiqueta
    Public Shared Function Guardar_Etiqueta_Verificacion(ByVal vBePickingUbic As clsBeTrans_picking_ubic,
                                                         ByVal pUsuario As String,
                                                         ByVal pIdTipoEtiquetaVerificacion As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As clsBeTrans_verificacion_etiqueta

        Dim etiqueta As New clsBeTrans_verificacion_etiqueta()

        Dim BePedido As New clsBeTrans_pe_enc
        Dim BeOperadorPickeo As New clsBeOperador
        Dim BeOperadorVerifico As New clsBeOperador

        Try
            etiqueta.IdPickingUbic = vBePickingUbic.IdPickingUbic
            etiqueta.IdPickingEnc = vBePickingUbic.IdPickingEnc
            etiqueta.IdPickingDet = vBePickingUbic.IdPickingDet
            etiqueta.IdProductoEstado = vBePickingUbic.IdProductoEstado
            etiqueta.IdStockRes = vBePickingUbic.IdStockRes
            etiqueta.IdStock = vBePickingUbic.IdStock
            etiqueta.IdProductoBodega = vBePickingUbic.IdProductoBodega
            etiqueta.IdPedidoEnc = vBePickingUbic.IdPedidoEnc
            etiqueta.IdPedidoDet = vBePickingUbic.IdPedidoDet
            etiqueta.IdBodega = vBePickingUbic.IdBodega
            etiqueta.IdPropietarioBodega = vBePickingUbic.IdPropietarioBodega
            etiqueta.IdPresentacion = vBePickingUbic.IdPresentacion
            etiqueta.IdUnidadMedida = vBePickingUbic.IdUnidadMedida
            etiqueta.IdOperadorBodega_Pickeo = vBePickingUbic.IdOperadorBodega_Pickeo
            etiqueta.IdOperadorBodega_Verifico = vBePickingUbic.IdOperadorBodega_Verifico
            etiqueta.IdOperadorBodega_Asignado = vBePickingUbic.IdOperadorBodega_Asignado
            etiqueta.Lote = vBePickingUbic.Lote
            etiqueta.Fecha_vence = vBePickingUbic.Fecha_Vence
            etiqueta.Lic_plate = vBePickingUbic.Lic_plate
            etiqueta.Peso_verificado = vBePickingUbic.Peso_verificado
            etiqueta.Cantidad_verificada = vBePickingUbic.Cantidad_Verificada
            etiqueta.Fecha_verificado = vBePickingUbic.Fecha_verificado

            BePedido = clsLnTrans_pe_enc.Get_Single_Sin_Picking(vBePickingUbic.IdPedidoEnc, lConnection, lTransaction)
            etiqueta.Nombre_cliente = BePedido.Cliente.Nombre_comercial
            etiqueta.Referencia_pedido = BePedido.Referencia_Documento_Ingreso_Bodega_Destino

            etiqueta.Nombre_producto = vBePickingUbic.NombreProducto
            etiqueta.Nombre_operador_pickeo = clsLnOperador_bodega.Get_Nombre_By_IdOperadorBodega(vBePickingUbic.IdOperadorBodega_Pickeo, lConnection, lTransaction)
            etiqueta.Nombre_operador_verifico = clsLnOperador_bodega.Get_Nombre_By_IdOperadorBodega(vBePickingUbic.IdOperadorBodega_Verifico, lConnection, lTransaction)
            If vBePickingUbic.IdPresentacion = 0 Then
                etiqueta.Nombre_Presentacion = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(vBePickingUbic.IdUnidadMedida, lConnection, lTransaction)
            Else
                etiqueta.Nombre_Presentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(vBePickingUbic.IdPresentacion, lConnection, lTransaction)
            End If

            etiqueta.Codigo_producto = vBePickingUbic.CodigoProducto
            etiqueta.Codigo_talla = String.Empty
            etiqueta.Codigo_color = String.Empty

            etiqueta.Codigo_barra_etiqueta = GenerarCodigoBarra(etiqueta.IdOperadorBodega_Verifico)

            etiqueta.User_agr = pUsuario
            etiqueta.User_mod = pUsuario
            etiqueta.Fec_agr = Now
            etiqueta.Activo = True
            etiqueta.ZPL_Etiqueta = GenerarZPL(pIdTipoEtiquetaVerificacion, etiqueta)

            Insertar(etiqueta, lConnection, lTransaction)

            Return etiqueta

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name} {ex.Message}")
        End Try

    End Function
    Private Shared Function GenerarCodigoBarra(ByVal idOperador As Integer) As String
        Return $"{Now:yyyyMMddHHmmss}{idOperador}"
    End Function

    Private Shared Function GenerarZPL(ByVal pIdTipoEtiquetaVerificacion As Integer,
                                       ByVal pEtiqueta As clsBeTrans_verificacion_etiqueta) As String

        Dim beTipoEtiquetaVerificacion As New clsBeTipo_etiqueta()
        Dim zpl As String = ""

        Try
            beTipoEtiquetaVerificacion = clsLnTipo_etiqueta.GetSingle_By_IdTipoEtiqueta(pIdTipoEtiquetaVerificacion)

            zpl = String.Format(
                   beTipoEtiquetaVerificacion.codigo_zpl(),
                   pEtiqueta.Nombre_operador_verifico,
                   Now,
                   pEtiqueta.Nombre_operador_pickeo,
                   pEtiqueta.Codigo_producto & " - " & pEtiqueta.Nombre_producto,
                   pEtiqueta.Cantidad_verificada & " " & pEtiqueta.Nombre_Presentacion,
                   pEtiqueta.Lote,
                   pEtiqueta.Nombre_cliente,
                   pEtiqueta.IdPedidoEnc & " " & pEtiqueta.Referencia_pedido,
                   pEtiqueta.IdPickingEnc,
                   pEtiqueta.Codigo_barra_etiqueta
           )
        Catch ex As Exception
        End Try

        Return zpl
    End Function
End Class
