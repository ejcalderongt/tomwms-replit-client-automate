Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_oc_det

    Public Shared Property lProductosInMemory As New List(Of clsBeProducto)
    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            '#CKFK20250526 Quité lo de tomar el codigo del producto del campo noparte cuando la empresa fuera Killios
            '#AT20250610 Espero que no ocurra nada malo... p.codigo codigo_producto a det.codigo_producto
            Dim vSQL As String = "SELECT p.IdProducto,det.IdOrdenCompraEnc, det.IdOrdenCompraDet, det.IdProductoBodega, det.IdArancel, det.IdPresentacion, 
                                         det.IdUnidadMedidaBasica, det.IdMotivoDevolucion, det.No_Linea, det.nombre_producto, det.nombre_presentacion, 
	                                     det.nombre_arancel, det.porcentaje_arancel, det.nombre_unidad_medida_basica, det.cantidad, det.cantidad_recibida, 
	                                     det.costo, det.total_linea, det.user_agr, det.fec_agr, det.user_mod, det.fec_mod, det.activo, det.peso, 
	                                     det.peso_recibido, det.atributo_variante_1,	   
	                                     det.codigo_producto,	   
	                                     det.valor_aduana, det.valor_fob, det.valor_iva, 
	                                     det.valor_dai, det.valor_seguro, det.valor_flete, det.peso_neto, det.peso_bruto, det.IdPropietarioBodega, 
	                                     det.nombre_propietario, det.IdOrdenCompraDetPadre, det.IdEmbarcador, det.IdProductoTallaColor
                                  FROM trans_oc_enc as enc  inner join trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN 
                                       producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN 
                                       producto AS p ON pb.IdProducto = p.IdProducto  
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransOcDet As clsBeTrans_oc_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransOcDet = New clsBeTrans_oc_det
                        Cargar(BeTransOcDet, lRow, lConnection, lTransaction)

                        BeTransOcDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                        clsLnProducto.Obtener_SO(BeTransOcDet.Producto, lConnection, lTransaction)
                        '#CKFK20241031 Aquí se envía el código del producto del objeto del producto
                        '#AT20250610 Espero que no ocurra nada malo...
                        'BeTransOcDet.Codigo_Producto = BeTransOcDet.Producto.Codigo

                        If (BeTransOcDet.Producto.IdClasificacion <> 0) Then
                            BeTransOcDet.Producto.Clasificacion = clsLnProducto_clasificacion.GetSingle(BeTransOcDet.Producto.IdClasificacion,
                                                                                               lConnection,
                                                                                               lTransaction)
                        End If

                        If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                            BeTransOcDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransOcDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            clsLnProducto_presentacion.Obtener(BeTransOcDet.Presentacion, lConnection, lTransaction)
                        End If

                        If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                            clsLnUnidad_medida.Obtener(BeTransOcDet.UnidadMedida, lConnection, lTransaction)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                        End If

                        BeTransOcDet.IsNew = False

                        '#EJC20220224:Cargar embarcador para CEALSA.
                        If BeTransOcDet.IdEmbarcador <> 0 Then

                            '#GT02032022: el nombre embarcador se obtiene de su ID porque esta normalizado
                            Dim pBeTrans_oc_embarcador As New clsBeTrans_oc_embarcador
                            pBeTrans_oc_embarcador = clsLnTrans_oc_embarcador.Get_Single_By_IdEmbarcador(BeTransOcDet.IdEmbarcador,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                            If Not pBeTrans_oc_embarcador Is Nothing Then
                                BeTransOcDet.Nombre_Embarcador = pBeTrans_oc_embarcador.Nombre
                            End If

                        End If

                        If BeTransOcDet.IdProductoTallaColor <> 0 Then
                            Dim BeProductoTallaColor = clsLnProducto_talla_color.GetSingle(BeTransOcDet.IdProductoTallaColor,
                                                                                           lConnection,
                                                                                           lTransaction)
                            If Not BeProductoTallaColor Is Nothing Then
                                BeTransOcDet.Talla = clsLnTalla.GetSingle(BeProductoTallaColor.IdTalla,
                                                                          lConnection,
                                                                          lTransaction)

                                BeTransOcDet.Color = clsLnColor.GetSingle(BeProductoTallaColor.IdColor,
                                                                          lConnection,
                                                                          lTransaction)
                            End If
                        End If

                        If Not BeTransOcDet.IdOrdenCompraDetPadre = 0 Then
                            Dim ObjPadre As New clsBeTrans_oc_det
                            ObjPadre = lReturnList.Find(Function(x) x.IdProductoBodega = BeTransOcDet.IdOrdenCompraDetPadre)
                            If Not ObjPadre Is Nothing Then
                                ObjPadre.lProductosHijosKit.Add(BeTransOcDet)
                            End If
                        Else
                            lReturnList.Add(BeTransOcDet)
                        End If

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
                                      INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                      INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                      WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransOCDet As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTransOCDet = New clsBeTrans_oc_det

                                Cargar(BeTransOCDet, lRow, lConnection, lTransaction)

                                BeTransOCDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                clsLnProducto.Obtener(BeTransOCDet.Producto, lConnection, lTransaction)
                                BeTransOCDet.Codigo_Producto = BeTransOCDet.Producto.Codigo

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    BeTransOCDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                                    BeTransOCDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                                    clsLnArancel.Obtener(BeTransOCDet.Arancel, lConnection, lTransaction)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransOCDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.Obtener(BeTransOCDet.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    BeTransOCDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                    clsLnUnidad_medida.Obtener(BeTransOCDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    BeTransOCDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                '#EJC20220224:Cargar embarcador para CEALSA.
                                If BeTransOCDet.IdEmbarcador <> 0 Then

                                    '#GT02032022: el nombre embarcador se obtiene de su ID porque esta normalizado
                                    Dim pBeTrans_OC_Embarcador As New clsBeTrans_oc_embarcador
                                    pBeTrans_OC_Embarcador = clsLnTrans_oc_embarcador.Get_Single_By_IdEmbarcador(BeTransOCDet.IdEmbarcador,
                                                                                                                      lConnection,
                                                                                                                      lTransaction)

                                    If Not pBeTrans_OC_Embarcador Is Nothing Then
                                        BeTransOCDet.Nombre_Embarcador = pBeTrans_OC_Embarcador.Nombre
                                    End If

                                End If

                                BeTransOCDet.IsNew = False

                                lReturnList.Add(BeTransOCDet)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc_Sin_Refs(ByVal pIdOrdenCompraEnc As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,det.* 
                                      FROM trans_oc_det AS det 
                                      INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                      INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                      WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransOcDet As clsBeTrans_oc_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransOcDet = New clsBeTrans_oc_det

                        Cargar(BeTransOcDet, lRow, lConnection, lTransaction)

                        BeTransOcDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                        BeTransOcDet.Producto = clsLnProducto.GetSingle_For_HH(BeTransOcDet.Producto.IdProducto, lConnection, lTransaction)
                        'clsLnProducto.Obtener(BeTransOcDet.Producto, lConnection, lTransaction)

                        BeTransOcDet.Codigo_Producto = BeTransOcDet.Producto.Codigo


                        If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                            BeTransOcDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                        End If

                        If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                            BeTransOcDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransOcDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                        End If

                        '#EJC20220224:Cargar embarcador para CEALSA.
                        If BeTransOcDet.IdEmbarcador <> 0 Then

                            '#GT02032022: el nombre embarcador se obtiene de su ID porque esta normalizado
                            Dim pBeTrans_OC_Embarcador As New clsBeTrans_oc_embarcador
                            pBeTrans_OC_Embarcador = clsLnTrans_oc_embarcador.Get_Single_By_IdEmbarcador(BeTransOcDet.IdEmbarcador,
                                                                                                                      lConnection,
                                                                                                                      lTransaction)

                            If Not pBeTrans_OC_Embarcador Is Nothing Then
                                BeTransOcDet.Nombre_Embarcador = pBeTrans_OC_Embarcador.Nombre
                            End If

                        End If

                        '#EJC20220224:Cargar Clasificacion para CEALSA.
                        'If lRow("Nombre_clasificacion") IsNot DBNull.Value AndAlso lRow("Nombre_clasificacion") IsNot Nothing Then
                        '    Obj.Producto.Clasificacion.Nombre = CType(lRow("Nombre_clasificacion"), String)
                        'End If

                        BeTransOcDet.IsNew = False

                        lReturnList.Add(BeTransOcDet)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc) As List(Of clsBeTrans_oc_det)

        Get_By_IdOrdenCompraEnc_HH = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det)

            Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
                                    INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                    INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                    WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc  
                                    UNION SELECT p.IdProducto,oc.IdOrdenCompraEnc, 0, det.IdProductoBodega, NULL AS IdArancel, 
                                    det.IdPresentacion, det.IdUnidadMedida, det.IdMotivoDevolucion, 
                                    0 as No_Linea, det.nombre_producto, det.nombre_presentacion, '', 0, det.nombre_unidad_medida, 
                                    det.cantidad_recibida, det.cantidad_recibida, 
                                    det.costo, det.cantidad_recibida* det.costo, det.user_agr, det.fec_agr, det.user_agr, det.fec_agr, 
                                    1, det.cantidad_recibida* det.peso_estadistico, det.cantidad_recibida* det.peso, det.atributo_variante_1, codigo_producto
                                    FROM trans_re_det AS det 
                                    INNER JOIN trans_re_enc AS enc ON det.IdRecepcionEnc = enc.IdRecepcionEnc
                                    INNER JOIN trans_re_oc AS oc ON enc.IdRecepcionEnc = oc.IdRecepcionEnc
                                    INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                    INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                    WHERE oc.IdOrdenCompraEnc = @IdOrdenCompraEnc
                                    AND det.IdProductoBodega NOT IN (SELECT IdProductoBodega FROM trans_oc_det WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransOcDet As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTransOcDet = New clsBeTrans_oc_det

                                Cargar(BeTransOcDet, lRow, lConnection, lTransaction)

                                BeTransOcDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                clsLnProducto.Obtener(BeTransOcDet.Producto, lConnection, lTransaction)
                                BeTransOcDet.Codigo_Producto = BeTransOcDet.Producto.Codigo

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    BeTransOcDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                                    BeTransOcDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                                    BeTransOcDet.Arancel = BeTransOcDet.Producto.Arancel
                                    'clsLnArancel.Obtener(BeTransOcDet.Arancel)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransOcDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    If BeTransOcDet.Producto.Presentaciones IsNot Nothing Then
                                        If BeTransOcDet.Producto.Presentaciones.Count > 0 Then
                                            Dim vIdPresentacionODCdet As Integer = lRow("IdPresentacion")
                                            BeTransOcDet.Presentacion = BeTransOcDet.Producto.Presentaciones.Find(Function(x) x.IdPresentacion = vIdPresentacionODCdet)
                                        End If
                                    End If
                                    'clsLnProducto_presentacion.Obtener(BeTransOcDet.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    BeTransOcDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                    '    clsLnUnidad_medida.Obtener(BeTransOcDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                BeTransOcDet.IsNew = False

                                lReturnList.Add(BeTransOcDet)

                            Next

                            Get_By_IdOrdenCompraEnc_HH = lReturnList

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
    '                                                         ByRef lConnection As SqlConnection,
    '                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_det)

    '    Dim lReturnList As New List(Of clsBeTrans_oc_det)

    '    Try

    '        'Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
    '        '                      INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
    '        '                      INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
    '        '                      WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc  
    '        '                      UNION SELECT p.IdProducto,oc.IdOrdenCompraEnc, 0, det.IdProductoBodega, NULL AS IdArancel, 
    '        '                      det.IdPresentacion, det.IdUnidadMedida, det.IdMotivoDevolucion, 
    '        '                      0 as No_Linea, det.nombre_producto, det.nombre_presentacion, '', 0, det.nombre_unidad_medida, 
    '        '                      det.cantidad_recibida, det.cantidad_recibida, 
    '        '                      det.costo, det.cantidad_recibida* det.costo, det.user_agr, det.fec_agr, det.user_agr, det.fec_agr, 
    '        '                      1, det.cantidad_recibida* det.peso_estadistico, det.cantidad_recibida* det.peso, det.atributo_variante_1, codigo_producto
    '        '                      FROM trans_re_det AS det 
    '        '                      INNER JOIN trans_re_enc AS enc ON det.IdRecepcionEnc = enc.IdRecepcionEnc
    '        '                      INNER JOIN trans_re_oc AS oc ON enc.IdRecepcionEnc = oc.IdRecepcionEnc
    '        '                      INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
    '        '                      INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
    '        '                      WHERE oc.IdOrdenCompraEnc = @IdOrdenCompraEnc
    '        '                      AND det.IdProductoBodega NOT IN (SELECT IdProductoBodega FROM trans_oc_det WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc)"

    '        Dim vSQL As String = "SELECT pb.IdProducto, det.* FROM trans_oc_det AS det 
    '                              JOIN producto_bodega pb ON det.IdProductoBodega = pb.IdProductoBodega 
    '                              WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc "

    '        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '            lDTA.SelectCommand.Transaction = lTransaction
    '            lDTA.SelectCommand.CommandType = CommandType.Text
    '            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

    '            Dim lDataTable As New DataTable
    '            lDTA.Fill(lDataTable)

    '            Dim BeTransOcDet As clsBeTrans_oc_det

    '            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

    '                For Each lRow As DataRow In lDataTable.Rows

    '                    BeTransOcDet = New clsBeTrans_oc_det

    '                    Cargar(BeTransOcDet, lRow, lConnection, lTransaction)

    '                    BeTransOcDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
    '                    clsLnProducto.Obtener(BeTransOcDet.Producto, lConnection, lTransaction)

    '                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
    '                        BeTransOcDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
    '                    End If

    '                    If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
    '                        BeTransOcDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
    '                        BeTransOcDet.Arancel = BeTransOcDet.Producto.Arancel
    '                    End If

    '                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
    '                        BeTransOcDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
    '                        BeTransOcDet.Presentacion = BeTransOcDet.Producto.Presentaciones.Find(Function(x) x.IdPresentacion = BeTransOcDet.Presentacion.IdPresentacion)
    '                    End If

    '                    If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
    '                        BeTransOcDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
    '                        BeTransOcDet.UnidadMedida = BeTransOcDet.Producto.UnidadMedida
    '                    End If

    '                    If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
    '                        BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
    '                    End If

    '                    BeTransOcDet.IsNew = False

    '                    lReturnList.Add(BeTransOcDet)

    '                Next

    '            End If

    '        End Using

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    '#CKFK20220524 Agregué esta funcion para obtener el detalle de la OC
    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
                                  INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                  INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransoCDet As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTransoCDet = New clsBeTrans_oc_det

                                Cargar(BeTransoCDet, lRow, lConnection, lTransaction)

                                BeTransoCDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                clsLnProducto.Obtener(BeTransoCDet.Producto, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    BeTransoCDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                                    BeTransoCDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                                    BeTransoCDet.Arancel = BeTransoCDet.Producto.Arancel
                                    'clsLnArancel.Obtener(BeTransoCDet.Arancel, lConnection, lTransaction)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransoCDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    BeTransoCDet.Presentacion = BeTransoCDet.Producto.Presentaciones.Find(Function(x) x.IdPresentacion = BeTransoCDet.Presentacion.IdPresentacion)
                                    'clsLnProducto_presentacion.Obtener(BeTransoCDet.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    BeTransoCDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                    BeTransoCDet.UnidadMedida = BeTransoCDet.Producto.UnidadMedida
                                    'clsLnUnidad_medida.Obtener(BeTransoCDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    BeTransoCDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                BeTransoCDet.IsNew = False

                                lReturnList.Add(BeTransoCDet)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180114 11:08PM Agregué el filtro de número de línea para hacer el update de la cantidad recibida sobre la línea correspondiente
    Public Shared Function Get_Detalle_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                           ByVal pIdProductoBodega As Integer,
                                                           ByVal pIdPresentacion As Integer,
                                                           ByVal pNoLinea As Integer,
                                                           ByVal pIdOrdenCompraDet As Integer,
                                                           ByVal pConnection As SqlConnection,
                                                           ByVal pTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Detalle_By_IdOrdenCompraEnc = Nothing

        Dim BeTransOCDet As New clsBeTrans_oc_det

        Try

            Dim vSQL As String = ""

            '#EJC20171027_0344AM: Agregué filtro por IdPresentacion en GetByOrdenCompra.
            '#CK20171113_0521PM: Agregué este filtro: "And IdPresentacion IS NULL" cuando la IdPresentacion es 0 porque de lo contrario actualiza el registro que no es
            '#CK20180115_0621PM: Agregué este filtro: " And No_Linea = @NoLinea" porque de lo contrario actualiza el registro que no es
            If pIdPresentacion = 0 Then

                vSQL = "SELECT * FROM trans_oc_det 
                        WHERE IdOrdenCompraenc = @IdOrdenCompraenc 
                        And IdProductoBodega = @IdProductoBodega
                        And IdPresentacion IS NULL
                        And No_Linea = @NoLinea
                        And IdOrdenCompraDet = @IdOrdenCompraDet"

            ElseIf pIdPresentacion = -1 Then
                '#EJC20190325: Si la OC fue creada en una presentación y se está recibiendo en otra.
                'la comparación es a posteriori.

                vSQL = "SELECT *
                        FROM trans_oc_det 
                        WHERE IdOrdenCompraenc = @IdOrdenCompraenc 
                        And IdProductoBodega = @IdProductoBodega                        
                        And No_Linea = @NoLinea 
                        And IdOrdenCompraDet = @IdOrdenCompraDet"

            ElseIf pIdPresentacion <> 0 Then

                vSQL = "SELECT *
                        FROM trans_oc_det 
                        WHERE (IdOrdenCompraenc=@IdOrdenCompraenc 
                        And IdProductoBodega=@IdProductoBodega 
                        And IdPresentacion = @IdPresentacion)
                        And No_Linea = @NoLinea
                        And IdOrdenCompraDet = @IdOrdenCompraDet"

            End If

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraenc", pIdOrdenCompraEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", pNoLinea)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

                If (pIdPresentacion <> 0 AndAlso pIdPresentacion <> -1) Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)

                    BeTransOCDet = New clsBeTrans_oc_det

                    Cargar(BeTransOCDet, lRow, pConnection, pTransaction)

                    If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                        BeTransOCDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                    End If

                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                        BeTransOCDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                    End If

                    If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                        BeTransOCDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                    End If

                    BeTransOCDet.IsNew = False

                    Return BeTransOCDet

                Else
                    '#DILEMA: Si la O.C. se creo pej. en cajas, pero se recepcionaron pallets,
                    'Debería actualizar la cantidad recibida en la O.C. (en cajas)? o debería
                    'mostrarse la cantidad recibida en 0 porque no se recibió la presentación
                    'esperada?
                    If pIdPresentacion <> 0 AndAlso pIdPresentacion <> -1 Then

                        BeTransOCDet = Get_Detalle_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                     pIdProductoBodega,
                                                                     0,
                                                                     pNoLinea,
                                                                     pIdOrdenCompraDet,
                                                                     pConnection,
                                                                     pTransaction)

                        If Not BeTransOCDet Is Nothing Then
                            Get_Detalle_By_IdOrdenCompraEnc = BeTransOCDet
                        Else
                            BeTransOCDet = Get_Detalle_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                  pIdProductoBodega,
                                                                  -1,
                                                                  pNoLinea,
                                                                  pIdOrdenCompraDet,
                                                                  pConnection,
                                                                  pTransaction)

                            If Not BeTransOCDet Is Nothing Then
                                Get_Detalle_By_IdOrdenCompraEnc = BeTransOCDet
                            End If

                        End If

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer,
                                                      ByVal pIdProductoBodega As Integer,
                                                      ByVal pIdPresentacion As Integer) As clsBeTrans_oc_det

        Get_By_IdOrdenCompraEnc_HH = Nothing

        Dim Obj As New clsBeTrans_oc_det

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = ""

                    If pIdPresentacion = 0 Then

                        vSQL = "SELECT TOP 1 * FROM trans_oc_det " &
                                     " WHERE IdOrdenCompraenc=@IdOrdenCompraenc And IdProductoBodega=@IdProductoBodega" &
                                     " And IdPresentacion IS NULL "

                    Else

                        vSQL = "SELECT TOP 1 * FROM trans_oc_det " &
                                     " WHERE IdOrdenCompraenc=@IdOrdenCompraenc And IdProductoBodega=@IdProductoBodega" &
                                     " And IdPresentacion=@IdPresentacion"

                    End If

                    Dim lDTA As New SqlDataAdapter
                    lDTA.SelectCommand = New SqlCommand(vSQL, lConnection, lTransaction)
                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraenc", pIdOrdenCompraEnc)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                    If pIdPresentacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDataTable.Rows(0)

                        Obj = New clsBeTrans_oc_det

                        Cargar(Obj, lRow, lConnection, lTransaction)

                        If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                            Obj.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                            Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                        End If

                        Obj.IsNew = False

                        Return Obj

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Delete(ByVal pIdOrdenCompraEnc As Integer,
                             ByVal pIdOrdenCompraDet As Integer) As Integer

        Delete = 0

        Try

            Dim vSQL As String = "DELETE FROM trans_oc_det WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc And IdOrdenCompraDet=@IdOrdenCompraDet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)
                        Delete = lCommand.ExecuteNonQuery()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdOrdenCompraDet),0) FROM trans_oc_det WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Count_By_Producto_En_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                       ByVal pIdProductoBodega As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lCount As Integer = 0

            Dim sp As String = "SELECT ISNULL(COUNT(IdProductoBodega),0) 
                                FROM trans_oc_det 
                                WHERE IdOrdenCompraEnc=@pIdOrdenCompraEnc AND
                                      IdProductoBodega = @IdProductoBodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCount = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lCount

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Creada por Erik Calderón
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det)

        Get_All_By_IdOrdenCompraEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT No_Linea, IdProductoBodega, Cantidad FROM trans_oc_det 
                        WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_oc_det

                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad"), Double)
                                End If

                                Obj.IsNew = False

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20210613: Get_Det_OC With transaction
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_det)

        Get_All_By_IdOrdenCompraEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det)

            Dim vSQL As String = "SELECT * FROM trans_oc_det 
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_oc_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_oc_det
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        Obj.IsNew = False
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc_And_IdProductoBodega(ByVal pIdOrdenCompraEnc As Integer,
                                                        ByVal pIdProductoBodega As Integer,
                                                        ByVal pNoLinea As Integer,
                                                        ByVal pIdOrdenCompraDet As Integer,
                                                        Optional ByVal pConnection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT * FROM trans_oc_det 
                    WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc  
                    And IdProductoBodega=@IdProductoBodega 
                    And no_linea =@NoLinea AND IdOrdenCompraDet = @IdOrdenCompraDet "

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
            lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", pNoLinea)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeTrans_oc_det

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows

                    Obj = New clsBeTrans_oc_det
                    Cargar(Obj, lRow, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))
                    Obj.IsNew = False
                    lReturnList.Add(Obj)

                Next

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Exist(ByVal pOCEncabezado As Integer,
                                 ByVal pNoLinea As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction)

        Exist = Nothing

        Try

            Const sp As String = "Select * from trans_oc_det 
                                  Where IdOrdenCompraEnc = @pOCEncabezado  
                                  And No_Linea = @No_Linea"
            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pOCEncabezado", pOCEncabezado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Linea", pNoLinea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjUM As New clsBeTrans_oc_det()

                Cargar(ObjUM, lRow, pConnection, pTransaction)

                Exist = ObjUM

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdOrdenCompraDet),0) FROM trans_oc_det WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Max_No_Linea(ByVal pIdOrdenCompraEnc As Integer,
                                        ByVal pConnection As SqlConnection,
                                        ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(No_Linea),0) FROM trans_oc_det WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Desde_Interface(ByRef oBeTrans_oc_det As clsBeTrans_oc_det, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("trans_oc_det")
            Upd.Add("idordencompradet", "@idordencompradet", DataType.Parametro)

            'EJC_18092016
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            End If

            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("nombre_presentacion", "@nombre_presentacion", DataType.Parametro)
            Upd.Add("nombre_arancel", "@nombre_arancel", DataType.Parametro)
            Upd.Add("porcentaje_arancel", "@porcentaje_arancel", DataType.Parametro)
            Upd.Add("nombre_unidad_medida_basica", "@nombre_unidad_medida_basica", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("total_linea", "@total_linea", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)

            If Not oBeTrans_oc_det.Atributo_variante_1 Is Nothing Then
                Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            End If

            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc " &
                "AND IdOrdenCompraDet = @IdOrdenCompraDet")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det.IdProductoBodega))

            'ejc_18092016: object_reference throws when nothing value is in arancel
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IDARANCEL", IIf(oBeTrans_oc_det.Arancel.IdArancel = 0, DBNull.Value, oBeTrans_oc_det.Arancel.IdArancel)))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_oc_det.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_oc_det.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeTrans_oc_det.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_oc_det.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_oc_det.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_oc_det.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", IIf(oBeTrans_oc_det.No_Linea = 0, oBeTrans_oc_det.No_Linea, oBeTrans_oc_det.No_Linea)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", IIf(oBeTrans_oc_det.Nombre_producto Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRESENTACION", IIf(oBeTrans_oc_det.Nombre_presentacion Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_presentacion)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_ARANCEL", IIf(oBeTrans_oc_det.Nombre_arancel Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_arancel)))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE_ARANCEL", oBeTrans_oc_det.Porcentaje_arancel))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD_MEDIDA_BASICA", IIf(oBeTrans_oc_det.Nombre_unidad_medida_basica Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_unidad_medida_basica)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_oc_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_oc_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_oc_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_oc_det.Peso_Recibido))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_det.Activo))

            If Not oBeTrans_oc_det.Atributo_variante_1 Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_oc_det.Atributo_variante_1))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180504 Modifiqué la función Actualiza_Cantidad_Recibida_OC para que actualice la cantidad recibida
    'en base al total por el número de línea
    Public Shared Function Actualiza_Cantidad_Recibida_OC(ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                                          ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Actualiza_Cantidad_Recibida_OC = 0

        Dim BePresentacion As New clsBeProducto_Presentacion()
        Dim BeTransOcDet As New clsBeTrans_oc_det()

        Try

            ''#EJC20171025_1220PM:Validar primero que tenga orden de compra antes de correr el proceso en Actualiza_Cantidad_Recibida_OC
            If pRecOrdenCompra IsNot Nothing Then

                Dim CantidadRecibidaActual As Double = 0

                If Not pListRecDet Is Nothing Then

                    For Each BeTransReDet As clsBeTrans_re_det In pListRecDet

                        If pRecOrdenCompra.IdOrdenCompraEnc > 0 Then

                            CantidadRecibidaActual = clsLnTrans_re_enc.Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet.IdRecepcionEnc,
                                                                                                                                         BeTransReDet.IdRecepcionDet,
                                                                                                                                         lConnection,
                                                                                                                                         lTransaction)

                            If BeTransReDet.IdPresentacion = 0 Then
                                BeTransReDet.IdPresentacion = -1
                            End If

                            BeTransOcDet = Get_Detalle_By_IdOrdenCompraEnc(pRecOrdenCompra.IdOrdenCompraEnc,
                                                                           BeTransReDet.IdProductoBodega,
                                                                           BeTransReDet.IdPresentacion,
                                                                           BeTransReDet.No_Linea,
                                                                           BeTransReDet.IdOrdenCompraDet,
                                                                           lConnection,
                                                                           lTransaction)

                            If BeTransOcDet IsNot Nothing Then

                                If BeTransOcDet.IdOrdenCompraDet > 0 Then

                                    Dim Factor As Double = 0

                                    'resultado += " bo.Cantidad_recibida = " & BeTransOcDet.Cantidad_recibida.ToString
                                    'resultado += " CantidadRecibidaActual = " & CantidadRecibidaActual.ToString
                                    'resultado += " Obj1.cantidad_recibida = " & BeTransReDet.cantidad_recibida.ToString

                                    If BeTransReDet.IsNew Then

                                        '#EJC20220329: Validar la presentación por documento de liquidación de ruta MERCOPAN.
                                        If (BeTransReDet.IdPresentacion = 0 OrElse BeTransReDet.IdPresentacion = -1) Then

                                            If BeTransOcDet.IdPresentacion = 0 Then
                                                BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida
                                                'resultado += " IsNew bo.Cantidad recibida actualizada = " & BeTransOcDet.Cantidad_recibida.ToString
                                            Else
                                                '#EJC20220329: La orden de compra, tiene presentación, pero la línea de recepción NO.
                                                'Entonces dividir la cantidad que viene en la recepción en UMBAS por el factor de la presentación de la D.I.

                                                BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                                                If Not BePresentacion Is Nothing Then
                                                    If BePresentacion.Factor > 0 Then
                                                        'resultado += " IsNew bo.Cantidad recibida SIN ACTUALIZAR = " & BeTransOcDet.Cantidad_recibida.ToString()
                                                        BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.cantidad_recibida / BePresentacion.Factor, 6)
                                                        'resultado += " IsNew bo.Cantidad recibida actualizada = " & BeTransOcDet.Cantidad_recibida.ToString
                                                    Else
                                                        Throw New Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.")
                                                    End If
                                                Else
                                                    Throw New Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.")
                                                End If

                                            End If

                                        Else
                                            'la oc, dice 100 UN.
                                            'la rec, dice 1 CJ
                                            '#EJC20220329: La recepción está en presentación, pero el D.I. NO tiene presentación.
                                            'Multiplicar lo que viene en la rec por el factor.
                                            If Not BeTransOcDet.IdPresentacion = 0 Then

                                                BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                                                If Not BePresentacion Is Nothing Then
                                                    If BePresentacion.Factor > 0 Then
                                                        BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida
                                                        'resultado += " IsNew bo.Cantidad recibida actualizada = " & BeTransOcDet.Cantidad_recibida.ToString
                                                    Else
                                                        Throw New Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.")
                                                    End If
                                                Else
                                                    Throw New Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.")
                                                End If

                                            Else
                                                BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.cantidad_recibida * BePresentacion.Factor, 6)
                                                'resultado += " IsNew bo.Cantidad recibida actualizada = " & BeTransOcDet.Cantidad_recibida.ToString
                                            End If

                                        End If

                                    Else

                                        BeTransOcDet.Cantidad_recibida = BeTransOcDet.Cantidad_recibida - CantidadRecibidaActual
                                        BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida
                                        'resultado += " Not IsNew bo.Cantidad recibida actualizada = " & BeTransOcDet.Cantidad_recibida.ToString

                                    End If

                                    Actualiza_Cantidad_Recibida_OC = Actualizar_Cantidad_Recibida(BeTransOcDet,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                                End If

                            Else
                                Throw New Exception("ERROR_202210051048: No se obtuvo el objeto de detalle del documento de ingreso, no se podrá actualizar la cantidad recibida.")
                            End If

                        End If

                    Next

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Cantidad_Recibida_OC_Pallet(ByVal pIdOrdenCompraEnc As Integer,
                                                                 ByVal pRecDet As clsBeTrans_re_det,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As Boolean

        Actualiza_Cantidad_Recibida_OC_Pallet = False

        Try

            ''#EJC20171025_1220PM:Validar primero que tenga orden de compra antes de correr el proceso en Actualiza_Cantidad_Recibida_OC
            If pIdOrdenCompraEnc <> 0 Then

                Dim CantidadRecibidaActual As Double = 0

                CantidadRecibidaActual = clsLnTrans_re_enc.Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(pRecDet.IdRecepcionEnc,
                                                                                        pRecDet.IdRecepcionDet,
                                                                                        lConnection,
                                                                                        lTransaction)

                Dim BeOcDet As clsBeTrans_oc_det = Get_Detalle_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                   pRecDet.IdProductoBodega,
                                                                                   pRecDet.IdPresentacion,
                                                                                   pRecDet.No_Linea,
                                                                                   pRecDet.IdOrdenCompraDet,
                                                                                   lConnection,
                                                                                   lTransaction)

                If BeOcDet IsNot Nothing Then

                    Dim Factor As Double = 0

                    If pRecDet.IsNew Then

                        If (BeOcDet.IdPresentacion = pRecDet.IdPresentacion) Then
                            BeOcDet.Cantidad_recibida += pRecDet.cantidad_recibida
                        ElseIf (BeOcDet.IdPresentacion = 0 AndAlso pRecDet.IdPresentacion <> 0) Then
                            pRecDet.Presentacion = clsLnProducto_presentacion.GetSingle(pRecDet.IdPresentacion, lConnection, lTransaction)
                            Factor = pRecDet.Presentacion.Factor
                            If pRecDet.Presentacion.EsPallet Then
                                BeOcDet.Cantidad_recibida = (pRecDet.cantidad_recibida * pRecDet.Presentacion.CajasPorCama * pRecDet.Presentacion.CamasPorTarima * pRecDet.Presentacion.Factor)
                            Else
                                BeOcDet.Cantidad_recibida = (pRecDet.cantidad_recibida * IIf(Factor = 0, 1, Factor))
                            End If
                        Else
                            Throw New Exception("Condición no conocida para actualización de valores en O.C. Reporte código de error: #EJC -> 1")
                        End If

                    Else

                        If (BeOcDet.IdPresentacion = pRecDet.IdPresentacion) Then
                            BeOcDet.Cantidad_recibida = BeOcDet.Cantidad_recibida - CantidadRecibidaActual
                            BeOcDet.Cantidad_recibida += pRecDet.cantidad_recibida
                        ElseIf (BeOcDet.IdPresentacion = 0 AndAlso pRecDet.IdPresentacion <> 0) Then

                            pRecDet.Presentacion = clsLnProducto_presentacion.GetSingle(pRecDet.IdPresentacion, lConnection, lTransaction)

                            Factor = pRecDet.Presentacion.Factor

                            If pRecDet.Presentacion.EsPallet Then
                                BeOcDet.Cantidad_recibida = BeOcDet.Cantidad_recibida - (CantidadRecibidaActual * IIf(Factor = 0, 1, pRecDet.Presentacion.CajasPorCama * pRecDet.Presentacion.CamasPorTarima * pRecDet.Presentacion.Factor))
                                BeOcDet.Cantidad_recibida += (pRecDet.cantidad_recibida * IIf(Factor = 0, 1, pRecDet.Presentacion.CajasPorCama * pRecDet.Presentacion.CamasPorTarima * pRecDet.Presentacion.Factor))
                            Else
                                BeOcDet.Cantidad_recibida += (pRecDet.cantidad_recibida * IIf(Factor = 0, 1, Factor))
                            End If
                        ElseIf (BeOcDet.IdPresentacion <> 0 AndAlso pRecDet.IdPresentacion <> 0) Then

                            pRecDet.Presentacion = clsLnProducto_presentacion.GetSingle(pRecDet.IdPresentacion, lConnection, lTransaction)

                            Factor = pRecDet.Presentacion.Factor

                            If pRecDet.Presentacion.EsPallet Then
                                BeOcDet.Cantidad_recibida = BeOcDet.Cantidad_recibida - (CantidadRecibidaActual * IIf(Factor = 0, 1, pRecDet.Presentacion.CajasPorCama * pRecDet.Presentacion.CamasPorTarima * pRecDet.Presentacion.Factor))
                                BeOcDet.Cantidad_recibida += (pRecDet.cantidad_recibida * IIf(Factor = 0, 1, pRecDet.Presentacion.CajasPorCama * pRecDet.Presentacion.CamasPorTarima * pRecDet.Presentacion.Factor))
                            Else
                                BeOcDet.Cantidad_recibida += (pRecDet.cantidad_recibida * IIf(Factor = 0, 1, Factor))
                            End If
                        Else
                            Throw New Exception("Condición no conocida para actualización de valores en O.C. Reporte código de error: #EJC -> 3")
                        End If

                    End If

                    Actualizar(BeOcDet, lConnection, lTransaction)

                    Actualiza_Cantidad_Recibida_OC_Pallet = True

                Else
                    Throw New Exception(String.Format("No se encontró el registro de detalle en el documento de ingreso con valores IdOC: {0}, IdProdBod: {1}, IdPres: {2}, NoLinea{3}: ", pIdOrdenCompraEnc,
                                                    pRecDet.IdProductoBodega,
                                                    pRecDet.IdPresentacion,
                                                    pRecDet.No_Linea))
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cod_Variante_Nav(ByVal pIdOrdenCompraEnc As Integer,
                                                ByVal pNo_Linea As Integer,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As String

        Get_Cod_Variante_Nav = ""

        Try

            Dim vSQL As String = "SELECT atributo_variante_1 FROM trans_oc_det                         
                        WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc 
                        AND No_Linea = @No_Linea"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@No_Linea", pNo_Linea)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_Cod_Variante_Nav = IIf(IsDBNull(lDataTable.Rows(0).Item("atributo_variante_1")), "", lDataTable.Rows(0).Item("atributo_variante_1"))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_Linea(ByVal IdOrdenCompraEnc As Integer,
                                                                    ByVal No_Linea As Integer,
                                                                    ByVal IdProductoBodega As Integer,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_IdOrdenCompraEnc_And_Linea = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
                                 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc
                                 AND No_Linea = @NO_LINEA 
                                 AND IdProductoBodega = @IdProductoBodega)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO_LINEA", No_Linea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Return pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByVal IdOrdenCompraEnc As Integer,
                                                                               ByVal IdOrdenCompraDet As Integer,
                                                                               ByVal IdProductoBodega As Integer,
                                                                               ByRef lConnection As SqlConnection,
                                                                               ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
             Where(IdOrdenCompraEnc = @IdOrdenCompraEnc
             AND IdOrdenCompraDet = @IdOrdenCompraDet 
             AND IdProductoBodega = @IdProductoBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", IdOrdenCompraDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Return pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Transaccion(ByVal pListObjOCDet As List(Of clsBeTrans_oc_det),
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Boolean

        Guardar_Transaccion = False

        Try

            For Each Obj As clsBeTrans_oc_det In pListObjOCDet
                Insertar(Obj, lConnection, lTransaction)
            Next

            Guardar_Transaccion = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc_And_IdRecepcionDet(ByVal pIdOrdenCompraEnc As Integer,
                                                                          ByVal pIdRecepcionDet As Integer) As List(Of clsBeTrans_oc_det)

        Get_All_By_IdOrdenCompraEnc_And_IdRecepcionDet = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_Costo_Linea_Documento_Ingreso 
                                          WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_oc_det
                                Cargar(Obj, lRow, lConnection, lTransaction)
                                Obj.IsNew = False
                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Costo_Unitario_By_IdOrdenCompraEnc_And_IdRecepcionDet(ByVal pIdOrdenCompraEnc As Integer,
                                                                                     ByVal pIdRecepcionDet As Integer,
                                                                                     ByVal lConnection As SqlConnection,
                                                                                     ByVal lTransaction As SqlTransaction) As Double

        Get_Costo_Unitario_By_IdOrdenCompraEnc_And_IdRecepcionDet = 0

        Try

            Dim lResult As Double = 0

            Dim vSQL As String = "SELECT  
                                  d.costo AS costo_unitario
                                  FROM dbo.trans_oc_enc AS e INNER JOIN
                                  dbo.trans_oc_det AS d ON e.IdOrdenCompraEnc = d.IdOrdenCompraEnc 
                                  WHERE d.IdOrdenCompraEnc=@IdOrdenCompraEnc 
                                  GROUP BY d.IdProductoBodega, e.IdOrdenCompraEnc, d.IdOrdenCompraDet, d.No_Linea, d.IdPresentacion, d.costo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    lResult = IIf(IsDBNull(lDataTable.Rows(0).Item("costo_unitario")), 0, lDataTable.Rows(0).Item("costo_unitario"))
                End If

            End Using

            Return lResult

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' #EJC20220314: Obtiene el detalle del documento de ingreso (sin presentación), se utiliza en BYB, órden de producción.
    ''' Para saber si se tiene que insertar una línea adicional en UMBAS o NO.
    ''' </summary>
    ''' <param name="IdOrdenCompraEnc"></param>
    ''' <param name="IdProductoBodega"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdProductoBodega(ByVal IdOrdenCompraEnc As Integer,
                                                                               ByVal IdProductoBodega As Integer,
                                                                               ByRef lConnection As SqlConnection,
                                                                               ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_IdOrdenCompraEnc_And_IdProductoBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
                                 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc                                 
                                 AND IdProductoBodega = @IdProductoBodega
                                 AND IdPresentacion IS NULL)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Return pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Recepcion_Det(ByVal IdOrdenCompraEnc As Integer,
                                                       ByVal IdOrdenCompraDet As Integer,
                                                       ByVal IdProductoBodega As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_Recepcion_Det = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
             Where(No_Linea = @No_Linea
             AND IdUnidadMedida = @IdUnidadMedida 
             AND IdProductoBodega = @IdProductoBodega
             AND IdPresentacion = @IdPresEntacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", IdOrdenCompraDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Return pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Crear_Linea_Unidades(ByVal pBeTransOcDet As clsBeTrans_oc_det,
                                                ByVal pCantidadUnidades As Integer,
                                                ByVal pCantidadRecibida As Integer,
                                                ByVal pIdBodega As Integer,
                                                ByRef pListRecDet As List(Of clsBeTrans_re_det),
                                                ByRef pConnection As SqlConnection,
                                                ByRef pTransaction As SqlTransaction) As clsBeTrans_oc_det

        Dim vReturn As clsBeTrans_oc_det
        Crear_Linea_Unidades = Nothing

        Try

            '#EJC20220314: Validar si ya existe la línea en umbas prior to insert.
            Dim BeTransOcDetUMBas As New clsBeTrans_oc_det()
            BeTransOcDetUMBas = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdProductoBodega(pBeTransOcDet.IdOrdenCompraEnc,
                                                                                                      pBeTransOcDet.IdProductoBodega,
                                                                                                      pConnection,
                                                                                                      pTransaction)

            Dim vMaxIdOrdenCompraDet As Integer = clsLnTrans_oc_det.MaxID(pBeTransOcDet.IdOrdenCompraEnc,
                                                                          pConnection,
                                                                          pTransaction) + 1

            Dim vMaxIdLinea As Integer

            Dim BeBodega As New clsBeBodega

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, pConnection, pTransaction)

            If BeBodega.Interface_SAP Then
                vMaxIdLinea = pBeTransOcDet.No_Linea
            Else
                vMaxIdLinea = clsLnTrans_oc_det.Max_No_Linea(pBeTransOcDet.IdOrdenCompraEnc,
                                                                        pConnection,
                                                                        pTransaction) + 10000
            End If

            If BeTransOcDetUMBas Is Nothing Then

                pListRecDet.FindAll(Function(x) x.No_Linea = pBeTransOcDet.No_Linea).ForEach(Sub(s)
                                                                                                 s.No_Linea = vMaxIdLinea
                                                                                                 s.IdOrdenCompraDet = vMaxIdOrdenCompraDet
                                                                                             End Sub)

                '#CKFK20220131 Insertar el nuevo registro en la trans_oc_det sin presentación
                pBeTransOcDet.Cantidad = pCantidadUnidades
                pBeTransOcDet.Cantidad_recibida = 0
                pBeTransOcDet.IdPresentacion = 0
                pBeTransOcDet.Presentacion.IdPresentacion = 0
                pBeTransOcDet.No_Linea = vMaxIdLinea
                pBeTransOcDet.Total_linea = pBeTransOcDet.Costo * pBeTransOcDet.Cantidad
                pBeTransOcDet.IdOrdenCompraDet = vMaxIdOrdenCompraDet
                clsLnTrans_oc_det.Insertar(pBeTransOcDet, pConnection, pTransaction)

                vReturn = pBeTransOcDet

            Else

                vMaxIdOrdenCompraDet = BeTransOcDetUMBas.IdOrdenCompraDet
                vMaxIdLinea = BeTransOcDetUMBas.No_Linea

                BeTransOcDetUMBas.Cantidad += pCantidadUnidades
                pBeTransOcDet.Total_linea += pBeTransOcDet.Costo * pBeTransOcDet.Cantidad
                'BeTransOcDetUMBas.Cantidad_recibida += pCantidadRecibida
                clsLnTrans_oc_det.Actualizar_Cantidad(BeTransOcDetUMBas,
                                                      pConnection,
                                                      pTransaction)

                vReturn = BeTransOcDetUMBas

                pListRecDet.FindAll(Function(x) x.No_Linea = pBeTransOcDet.No_Linea And
                                        x.IdOrdenCompraDet = vMaxIdOrdenCompraDet).ForEach(Sub(s)
                                                                                               s.No_Linea = vMaxIdLinea
                                                                                               s.IdOrdenCompraDet = vMaxIdOrdenCompraDet
                                                                                           End Sub)

            End If

            Return vReturn

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC202209211051: Utilizar en el futuro para recargar una línea del D.I. 
    ''' en la HH pero se debe modificar la HH para que actualice la lista en el índice de la línea de la ocdet.
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pIdOrdenCompraDet"></param>
    ''' <returns></returns>
    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer,
                                                                 ByVal pIdOrdenCompraDet As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
                                  INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                  INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc AND det.IdOrdenCompraDet = @IdOrdenCompraDet"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_oc_det

                                Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                clsLnProducto.Obtener(Obj.Producto, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.Obtener(Obj.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                Obj.IsNew = False

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_OC_By_IdOrdenCompraEnc_HH2(ByVal pIdOrdenCompraEnc As Integer,
                                                                  ByVal pIdBodega As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)
        Dim vlProductosInMemory As New List(Of clsBeProducto)
        Dim vSublProductosInMemory As New List(Of clsBeProducto)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,det.* FROM trans_oc_det AS det 
                                  INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                  INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransOcDet As clsBeTrans_oc_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            vlProductosInMemory = clsLnProducto.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                            pIdBodega,
                                                                                            lConnection,
                                                                                            lTransaction)

                            If Not vlProductosInMemory Is Nothing Then

                                If vlProductosInMemory.Count > 0 Then

                                    If lProductosInMemory.Count > 0 Then

                                        vSublProductosInMemory = vlProductosInMemory.Except(lProductosInMemory).ToList()

                                        If Not vSublProductosInMemory Is Nothing Then

                                            If vSublProductosInMemory.Count > 0 Then
                                                lProductosInMemory.AddRange(vSublProductosInMemory)
                                            End If

                                        End If

                                    Else
                                        lProductosInMemory.AddRange(vlProductosInMemory)
                                    End If

                                End If

                            End If

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTransOcDet = New clsBeTrans_oc_det

                                Cargar(BeTransOcDet, lRow, lConnection, lTransaction)

                                BeTransOcDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                BeTransOcDet.Producto = lProductosInMemory.Find(Function(x) x.IdProducto = BeTransOcDet.Producto.IdProducto)
                                'clsLnProducto.Obtener(BeTransOcDet.Producto, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    BeTransOcDet.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                                    BeTransOcDet.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                                    clsLnArancel.Obtener(BeTransOcDet.Arancel, lConnection, lTransaction)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransOcDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    If BeTransOcDet.Presentacion.IdPresentacion <> 0 Then
                                        clsLnProducto_presentacion.Obtener(BeTransOcDet.Presentacion, lConnection, lTransaction)
                                    End If
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    BeTransOcDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                    clsLnUnidad_medida.Obtener(BeTransOcDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    BeTransOcDet.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                If BeTransOcDet.IdProductoTallaColor <> 0 Then
                                    Dim BeProductoTallaColor = clsLnProducto_talla_color.GetSingle(BeTransOcDet.IdProductoTallaColor,
                                                                                           lConnection,
                                                                                           lTransaction)
                                    If Not BeProductoTallaColor Is Nothing Then
                                        BeTransOcDet.Talla = clsLnTalla.GetSingle(BeProductoTallaColor.IdTalla,
                                                                                  lConnection,
                                                                                  lTransaction)

                                        BeTransOcDet.Color = clsLnColor.GetSingle(BeProductoTallaColor.IdColor,
                                                                                  lConnection,
                                                                                  lTransaction)
                                    End If
                                End If

                                BeTransOcDet.IsNew = False

                                lReturnList.Add(BeTransOcDet)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Cantidad_Recibida_OC(ByVal IdOrdenCompraEnc As Integer,
                                                          ByVal BeTransReDet As clsBeTrans_re_det,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Actualiza_Cantidad_Recibida_OC = 0

        Dim BePresentacion As New clsBeProducto_Presentacion()
        Dim BeTransOcDet As New clsBeTrans_oc_det()

        Try


            Dim CantidadRecibidaActual As Double = 0

            If Not BeTransReDet Is Nothing Then

                If IdOrdenCompraEnc > 0 Then

                    CantidadRecibidaActual = clsLnTrans_re_enc.Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet.IdRecepcionEnc,
                                                                                                                                 BeTransReDet.IdRecepcionDet,
                                                                                                                                 lConnection,
                                                                                                                                 lTransaction)

                    If BeTransReDet.IdPresentacion = 0 Then
                        BeTransReDet.IdPresentacion = -1
                    End If

                    BeTransOcDet = Get_Detalle_By_IdOrdenCompraEnc(IdOrdenCompraEnc,
                                                                   BeTransReDet.IdProductoBodega,
                                                                   BeTransReDet.IdPresentacion,
                                                                   BeTransReDet.No_Linea,
                                                                   BeTransReDet.IdOrdenCompraDet,
                                                                   lConnection,
                                                                   lTransaction)

                    If BeTransOcDet IsNot Nothing Then

                        If BeTransOcDet.IdOrdenCompraDet > 0 Then

                            Dim Factor As Double = 0

                            If BeTransReDet.IsNew Then

                                If (BeTransReDet.IdPresentacion = 0 OrElse BeTransReDet.IdPresentacion = -1) Then

                                    If BeTransOcDet.IdPresentacion = 0 Then
                                        BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida
                                    Else
                                        BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                              lConnection,
                                                                                              lTransaction)

                                        If Not BePresentacion Is Nothing Then
                                            If BePresentacion.Factor > 0 Then
                                                BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.cantidad_recibida / BePresentacion.Factor, 6)
                                            Else
                                                Throw New Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.")
                                            End If
                                        Else
                                            Throw New Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.")
                                        End If

                                    End If

                                Else
                                    'la oc, dice 100 UN.
                                    'la rec, dice 1 CJ
                                    '#EJC20220329: La recepción está en presentación, pero el D.I. NO tiene presentación.
                                    'Multiplicar lo que viene en la rec por el factor.
                                    If Not BeTransOcDet.IdPresentacion = 0 Then

                                        BePresentacion = clsLnProducto_presentacion.GetSingle(BeTransOcDet.IdPresentacion,
                                                                                              lConnection,
                                                                                              lTransaction)

                                        If Not BePresentacion Is Nothing Then
                                            If BePresentacion.Factor > 0 Then
                                                BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida
                                            Else
                                                Throw New Exception("Error: #20220329_FACT: El factor de la presentación es 0, no se puede actualizar la cantidad recibida del D.I.")
                                            End If
                                        Else
                                            Throw New Exception("Error: #20220329_MISS_PRES: No se pudo obtener la presentación del documento de ingreso.")
                                        End If

                                    Else
                                        BeTransOcDet.Cantidad_recibida += Math.Round(BeTransReDet.cantidad_recibida * BePresentacion.Factor, 6)
                                    End If

                                End If

                            Else

                                BeTransOcDet.Cantidad_recibida = BeTransOcDet.Cantidad_recibida - CantidadRecibidaActual
                                BeTransOcDet.Cantidad_recibida += BeTransReDet.cantidad_recibida

                            End If

                            Actualiza_Cantidad_Recibida_OC = Actualizar_Cantidad_Recibida(BeTransOcDet,
                                                                                          lConnection,
                                                                                          lTransaction)

                        End If

                    Else
                        Throw New Exception("ERROR_202210051048: No se obtuvo el objeto de detalle del documento de ingreso, no se podrá actualizar la cantidad recibida.")
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByVal IdOrdenCompraEnc As Integer,
                                                                               ByVal IdOrdenCompraDet As Integer,
                                                                               ByVal IdProductoBodega As Integer,
                                                                               ByVal NoLinea As Integer) As clsBeTrans_oc_det

        Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
                                 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc
                                 AND IdOrdenCompraDet = @IdOrdenCompraDet 
                                 AND IdProductoBodega = @IdProductoBodega 
                                 AND No_Linea = @NoLinea)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", IdOrdenCompraDet))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@NoLinea", NoLinea))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                        Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                        Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = pBeTrans_oc_det
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByVal IdOrdenCompraEnc As Integer,
                                                                               ByVal IdOrdenCompraDet As Integer,
                                                                               ByVal IdProductoBodega As Integer,
                                                                               ByVal NoLinea As Integer,
                                                                               ByVal lConnection As SqlConnection,
                                                                               ByVal lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
                                 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc
                                 AND IdOrdenCompraDet = @IdOrdenCompraDet 
                                 AND IdProductoBodega = @IdProductoBodega 
                                 AND No_Linea = @NoLinea)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", IdOrdenCompraDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NoLinea", NoLinea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Get_Single_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_ListOrdenCompraEnc_By_Codigo_Producto(ByVal pCodigo As String,
                                                                     ByVal pIdOperadorBodega As String) As DataTable

        Dim ListaOC As New DataTable()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT distinct d.IdOrdenCompraEnc
                                  FROM trans_oc_det d INNER JOIN 
                                       trans_re_oc r ON r.IdOrdenCompraEnc = d.IdOrdenCompraEnc INNER JOIN 
                                       trans_re_op o ON o.IdRecepcionEnc = r.IdRecepcionEnc INNER JOIN
									   trans_oc_enc e ON e.IdOrdenCompraEnc = d.IdOrdenCompraEnc
                                  WHERE (o.IdOperadorBodega = @IdOperadorBodega) 
                                        AND e.IdEstadoOC NOT IN (4,5,6) AND 
                                        ((d.codigo_producto = @Codigo_Producto) OR
                                         (d.IdProductoBodega IN (SELECT IdProductoBodega FROM VW_ProductoSI WHERE codigo_barra_pcb = @Codigo_Producto)))"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo_Producto", pCodigo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            Dim dt As New DataTable("ListaOC")
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                ListaOC = dt
            Else
                ListaOC = Nothing
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

        Return ListaOC

    End Function

    '#GT26082024: traer el peso bruto/neto para efectos de cobro en prefactura cuando no se maneja poliza
    Public Shared Function Get_Peso_OC_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer, ByVal pPesoBruto As Boolean) As Integer

        Get_Peso_OC_By_IdOrdenCompraEnc_HH = 0

        Try

            Dim vSQL As String = ""

            If pPesoBruto Then

                vSQL = "SELECT sum(peso_bruto) peso_bruto FROM trans_oc_det AS det 
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc"

            Else
                vSQL = "SELECT sum(peso_neto) peso_neto  FROM trans_oc_det AS det            
                                  WHERE det.IdOrdenCompraEnc = @IdOrdenCompraEnc"
            End If






            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)


                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For i As Integer = 0 To lDataTable.Rows.Count - 1
                                Get_Peso_OC_By_IdOrdenCompraEnc_HH = lDataTable.Rows(i)(0)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using



        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal pOCEncabezado As Integer, ByVal pNoLinea As Integer) As clsBeTrans_oc_det
        Exist = Nothing

        Using conn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            conn.Open()
            Using tran As SqlTransaction = conn.BeginTransaction(IsolationLevel.ReadCommitted)
                Try
                    Const sp As String = "SELECT * FROM trans_oc_det 
                                      WHERE IdOrdenCompraEnc = @pOCEncabezado  
                                      AND No_Linea = @No_Linea"

                    Dim cmd As New SqlCommand(sp, conn, tran) With {.CommandType = CommandType.Text}
                    cmd.Parameters.Add(New SqlParameter("@pOCEncabezado", pOCEncabezado))
                    cmd.Parameters.Add(New SqlParameter("@No_Linea", pNoLinea))

                    Dim dt As New DataTable
                    Using dad As New SqlDataAdapter(cmd)
                        dad.Fill(dt)
                    End Using

                    If dt.Rows.Count >= 1 Then
                        Dim lRow As DataRow = dt.Rows(0)
                        Dim ObjUM As New clsBeTrans_oc_det()
                        Cargar(ObjUM, lRow, conn, tran)
                        Exist = ObjUM
                    End If

                    tran.Commit()
                Catch ex As Exception
                    Try
                        tran.Rollback()
                    Catch
                    End Try
                    clsLnLog_error_wms.Agregar_Error(ex.Message)
                    Throw
                End Try
            End Using
        End Using
    End Function

    Public Shared Function Get_Single_By_Recepcion_Det_For_Inav(ByVal IdOrdenCompraEnc As Integer,
                                                                ByVal IdProductoBodega As Integer,
                                                                ByVal No_Linea As Integer,
                                                                ByVal Codigo_Producto As String,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_det

        Get_Single_By_Recepcion_Det_For_Inav = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det 
             Where(No_Linea = @No_Linea             
             AND IdProductoBodega = @IdProductoBodega
             AND Codigo_Producto = @Codigo_Producto
             AND IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Linea", No_Linea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo_Producto", Codigo_Producto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_oc_det As New clsBeTrans_oc_det
                Cargar(pBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
                Return pBeTrans_oc_det
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class