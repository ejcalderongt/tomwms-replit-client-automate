Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_re_det

    Public Shared Property lProductosInMemory As New List(Of clsBeProducto)

    '#EJC20180113: Agregue transaccionalidad en GetByRecepcion en clase clsLnTrans_re_det
    Public Shared Function Get_Detalle_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                         ByVal pIdBodegaEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)
        Dim vlProductosInMemory As New List(Of clsBeProducto)
        Dim vSublProductosInMemory As New List(Of clsBeProducto)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Get_Detalle_By_IdRecepcionEnc 
                                      WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdBodega = @IdBodega "

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodegaEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransReDet As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            vlProductosInMemory = clsLnProducto.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                                                          pIdBodegaEnc,
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

                                BeTransReDet = New clsBeTrans_re_det

                                Cargar(BeTransReDet, lRow)

                                BeTransReDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                BeTransReDet.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)

                                BeTransReDet.Producto = lProductosInMemory.Find(Function(x) x.IdProducto = BeTransReDet.Producto.IdProducto)
                                'clsLnProducto.Obtener(Obj.Producto, lConnection, lTransaction)

                                ' Se agregó el campo de IdPropietarioBodega ya que al guardar el stock necesito tener un IdPropietarioBodega de la recepción

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransReDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    If BeTransReDet.Presentacion.IdPresentacion <> 0 Then
                                        clsLnProducto_presentacion.Obtener(BeTransReDet.Presentacion, lConnection, lTransaction)
                                        If BeTransReDet.Presentacion.EsPallet Then
                                            'Obj.cantidad_recibida = 1
                                        End If
                                    End If
                                End If

                                If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                                    BeTransReDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                                    clsLnUnidad_medida.Obtener(BeTransReDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    BeTransReDet.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                                    clsLnProducto_estado.Obtener(BeTransReDet.ProductoEstado, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    BeTransReDet.MotivoDevolucion.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                    clsLnMotivo_devolucion.Obtener(BeTransReDet.MotivoDevolucion, lConnection, lTransaction)
                                End If

                                If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                                    BeTransReDet.IdUbicacion = CType(lRow("IdUbicacionRecepcion"), Integer)
                                    BeTransReDet.IdUbicacionAnterior = CType(lRow("IdUbicacionRecepcion"), Integer)
                                End If

                                BeTransReDet.IsNew = False

                                lReturnList.Add(BeTransReDet)

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

    Public Shared Function Get_Detalle_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                         ByVal pIdBodega As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_det)

        Get_Detalle_By_IdRecepcionEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Get_Detalle_By_IdRecepcionEnc 
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransReDet As clsBeTrans_re_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransReDet = New clsBeTrans_re_det

                        Cargar(BeTransReDet, lRow)

                        BeTransReDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                        BeTransReDet.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        clsLnProducto.Obtener(BeTransReDet.Producto, lConnection, lTransaction)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransReDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            clsLnProducto_presentacion.Obtener(BeTransReDet.Presentacion, lConnection, lTransaction)
                        End If

                        If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                            BeTransReDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                            clsLnUnidad_medida.Obtener(BeTransReDet.UnidadMedida, lConnection, lTransaction)
                        End If

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            BeTransReDet.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                            clsLnProducto_estado.Obtener(BeTransReDet.ProductoEstado, lConnection, lTransaction)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            BeTransReDet.MotivoDevolucion.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                            If BeTransReDet.MotivoDevolucion.IdMotivoDevolucion <> 0 Then
                                clsLnMotivo_devolucion.Obtener(BeTransReDet.MotivoDevolucion, lConnection, lTransaction)
                            End If
                        End If

                        If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                            BeTransReDet.IdUbicacion = CType(lRow("IdUbicacionRecepcion"), Integer)
                            BeTransReDet.IdUbicacionAnterior = CType(lRow("IdUbicacionRecepcion"), Integer)
                        End If

                        BeTransReDet.IsNew = False

                        lReturnList.Add(BeTransReDet)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function DI_To_Rec(ByVal pIdRecepcionEnc As Integer,
                                     ByVal pIdOrdenCompraEnc As Integer,
                                     ByVal pIdUsuario As Integer) As List(Of clsBeTrans_re_det)

        DI_To_Rec = Nothing

        Dim lReturnList As New List(Of clsBeTrans_re_det)
        Dim lTransOcDet As New List(Of clsBeTrans_oc_det)
        Dim BeRec As New clsBeTrans_re_det()
        Dim vMaxIdRecDet As Integer = 0
        Dim BeBuenEstadoProducto As New clsBeProducto_estado()
        Dim vIdPropietario As Integer = 0
        Dim vIdBodega As Integer = 0
        Dim BeOperador As New clsBeOperador()

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    lTransOcDet = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)

                    vMaxIdRecDet = MaxID(pIdRecepcionEnc, lConnection, lTransaction) + 1

                    For Each DetOC As clsBeTrans_oc_det In lTransOcDet

                        vIdBodega = clsLnTrans_oc_enc.Get_IdBodega_By_IdOrdenCompraEnc(pIdOrdenCompraEnc, lConnection, lTransaction)
                        vIdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(vIdBodega, DetOC.IdPropietarioBodega, lConnection, lTransaction)
                        BeBuenEstadoProducto = clsLnProducto_estado.Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH(vIdPropietario, vIdBodega, lConnection, lTransaction)
                        BeOperador = clsLnOperador.Get_IdOperadorBodega_Defecto(vIdBodega, lConnection, lTransaction)

                        BeRec = New clsBeTrans_re_det()
                        BeRec.IdRecepcionDet = vMaxIdRecDet
                        BeRec.IdRecepcionEnc = pIdRecepcionEnc
                        BeRec.IdPropietarioBodega = DetOC.IdPropietarioBodega
                        BeRec.IdProductoBodega = DetOC.IdProductoBodega
                        BeRec.IdPresentacion = DetOC.IdPresentacion
                        BeRec.Presentacion = clsLnProducto_presentacion.GetSingle(DetOC.IdPresentacion, lConnection, lTransaction)
                        BeRec.UnidadMedida = clsLnUnidad_medida.GetSingle(DetOC.IdUnidadMedidaBasica, lConnection, lTransaction)
                        BeRec.IdUnidadMedida = DetOC.IdUnidadMedidaBasica
                        BeRec.IdProductoEstado = BeBuenEstadoProducto.IdEstado
                        BeRec.Producto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(DetOC.IdProductoBodega, lConnection, lTransaction)
                        BeRec.IdOperadorBodega = BeOperador.IdOperador
                        BeRec.IdMotivoDevolucion = 0
                        BeRec.No_Linea = DetOC.No_Linea
                        BeRec.cantidad_recibida = DetOC.Cantidad
                        BeRec.Nombre_producto = DetOC.Nombre_producto
                        BeRec.Nombre_presentacion = DetOC.Nombre_presentacion
                        BeRec.Nombre_unidad_medida = DetOC.Nombre_unidad_medida_basica
                        BeRec.Nombre_producto_estado = BeBuenEstadoProducto.Nombre

                        If BeRec.Producto.Control_lote Then
                            BeRec.Lote = FormatoFechas.tFecha(Now)
                        Else
                            BeRec.Lote = ""
                        End If

                        If BeRec.Producto.Control_vencimiento Then
                            BeRec.Fecha_vence = Now.AddMonths(3)
                        Else
                            BeRec.Fecha_vence = New Date(1900, 1, 1)
                        End If

                        BeRec.Fecha_ingreso = Now
                        BeRec.Peso = DetOC.Peso
                        BeRec.Peso_Estadistico = DetOC.Peso
                        BeRec.Peso_Minimo = 0
                        BeRec.Peso_Maximo = 0
                        BeRec.peso_unitario = (DetOC.Peso / DetOC.Cantidad)
                        BeRec.User_agr = pIdUsuario
                        BeRec.Fec_agr = Now
                        BeRec.Observacion = "Ingreso auto at: " & Now
                        BeRec.Aniada = 0
                        BeRec.Costo = DetOC.Costo
                        BeRec.Costo_Estadistico = DetOC.Costo
                        BeRec.Atributo_Variante_1 = ""
                        BeRec.Codigo_Producto = DetOC.Codigo_Producto
                        BeRec.Lic_plate = "L" & FormatoFechas.tFecha(Now)
                        BeRec.Pallet_No_Estandar = False
                        BeRec.IsNew = True
                        BeRec.IdOrdenCompraEnc = DetOC.IdOrdenCompraEnc
                        BeRec.IdOrdenCompraDet = DetOC.IdOrdenCompraDet
                        lReturnList.Add(BeRec)

                        vMaxIdRecDet += 1

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT p.IdProducto,p.Control_Peso,det.* 
                                 FROM trans_re_det AS det INNER JOIN producto_bodega AS pb 
                                 ON det.IdProductoBodega = pb.IdProductoBodega 
                                 INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                                 WHERE IdRecepcionEnc=@IdRecepcionEnc "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_re_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_re_det

                        Cargar(Obj, lRow)

                        Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                            Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                        End If

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            Obj.MotivoDevolucion.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                        End If

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

    '#CKFK 20180115 11:15PM: Agregué filtro por número de línea
    ''' <summary>
    ''' Creada por Carolina Fuentes para obtener todos los registros de detalle de la recepción de un producto en una recepción
    ''' Modificada por Carolina Fuentes para obtener todos los registros de detalle de la recepción de un producto, de una línea,
    ''' de un IdOrdenCompraDet en una recepción
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="pIdProductoBodega"></param>
    ''' <param name="pNoLinea"></param>
    ''' <param name="pIdOrdenCompraDet"></param>
    ''' <returns></returns>
    Public Shared Function Get_Detalle_By_IdRecepcionDet_HH(ByVal pIdRecepcionEnc As Integer,
                                                            ByVal pIdProductoBodega As Integer,
                                                            ByVal pNoLinea As Integer,
                                                            ByVal pIdOrdenCompraDet As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            '#EJC20180113: Agregué transaccionalidad a toda la función GetByRecepcionDetHH en clsLnTrans_re_det
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT p.IdProducto,p.Control_Peso,det.* 
                             FROM trans_re_det AS det INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega 
                             INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto
                             WHERE IdRecepcionEnc=@IdRecepcionEnc AND det.IdProductoBodega = @IdProductoBodega AND No_Linea = @NoLinea 
                                   AND det.IdOrdenCompraDet  = @IdOrdenCompraDet"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", pNoLinea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                Cargar(Obj, lRow)

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                                    'Obj.Producto = clsLnProducto.GetSingle(Obj.Producto.IdProducto, pCampos, lConnection, lTransaction)
                                    Dim pCampos(5) As clsBeProducto.ProdPropiedades
                                    pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                                    pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                                    pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                                    pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                                    pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                                    pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                                    clsLnProducto.Obtener(Obj.Producto, lConnection, lTransaction)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.Obtener(Obj.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                                    Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                                    clsLnUnidad_medida.Obtener(Obj.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    Obj.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                                    Obj.ProductoEstado = clsLnProducto_estado.GetSingle(Obj.ProductoEstado.IdEstado, lConnection, lTransaction)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    Obj.MotivoDevolucion.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                    clsLnMotivo_devolucion.GetSingle(Obj.MotivoDevolucion, lConnection, lTransaction)
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

    Private Shared Function Delete_Producto_Pallet(ByVal pIdRecepcionEnc As Integer,
                                           ByVal pIdRecepcionDet As Integer,
                                           Optional ByRef pConnection As SqlConnection = Nothing,
                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Delete_Producto_Pallet = 0

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As SqlCommand = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTrans As SqlTransaction = Nothing

        Const vDeleteProductoPallet As String = "DELETE FROM producto_pallet WHERE IdRecepcionEnc = @IdRecepcionEnc 
                                                 AND IdRecepcionDet = @IdRecepcionDet "

        If Not Es_Transaccion_Remota Then lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vDeleteProductoPallet, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lCommand = New SqlCommand(vDeleteProductoPallet, lConnection, lTrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pIdRecepcionDet))

            Return lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If
        End Try

    End Function

    Private Shared Function Delete_Stock_Se(ByVal pIdRecepcionEnc As Integer,
                                           ByVal pIdRecepcionDet As Integer,
                                           Optional ByRef pConnection As SqlConnection = Nothing,
                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Delete_Stock_Se = 0

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lCommand As SqlCommand = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTrans As SqlTransaction = Nothing
        Dim pIdStockRec As Integer = 0

        Const vDeleteStockSeRecFK As String = "DELETE FROM stock_se_rec WHERE IdStockRec=@pIdStockRec"

        If Not Es_Transaccion_Remota Then lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            If Es_Transaccion_Remota Then
                pIdStockRec = clsLnStock_rec.GetIdStockRecByIdRecEncDet(pIdRecepcionEnc, pIdRecepcionDet, pConnection, pTransaction)
                lCommand = New SqlCommand(vDeleteStockSeRecFK, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                pIdStockRec = clsLnStock_rec.GetIdStockRecByIdRecEncDet(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                lCommand = New SqlCommand(vDeleteStockSeRecFK, lConnection, lTrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@pIdStockRec", pIdStockRec))

            Return lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                lConnection.Close()
                lConnection.Dispose()
            End If
        End Try

    End Function

    Private Shared Function Delete_Stock_Rec(ByVal pIdRecepcionEnc As Integer,
                                           ByVal pIdRecepcionDet As Integer,
                                           ByRef pConnection As SqlConnection,
                                           ByRef pTransaction As SqlTransaction) As Integer

        Delete_Stock_Rec = 0

        Const vDeleteStockRecFK As String = "DELETE FROM stock_rec  
                                             WHERE IdRecepcionEnc=@IdRecepcionEnc
                                             And IdRecepcionDet=@IdRecepcionDet"

        Try


            Dim lCommand As New SqlCommand(vDeleteStockRecFK, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pIdRecepcionDet))

            Return lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Delete_Rec_Det_Parametros(ByVal pIdRecepcionEnc As Integer,
                                                       ByVal pIdRecepcionDet As Integer,
                                                       ByRef pConnection As SqlConnection,
                                                       ByRef pTransaction As SqlTransaction) As Integer

        Delete_Rec_Det_Parametros = 0

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Const vDeleteTransReDetParametros As String = "DELETE FROM trans_re_det_parametros " &
                                                        " WHERE IdRecepcionEnc=@IdRecepcionEnc " &
                                                        " And IdRecepcionDet=@IdRecepcionDet"

        Try

            Dim lCommand As New SqlCommand(vDeleteTransReDetParametros, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pIdRecepcionDet))

            Return lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Delete_Rec_Det(ByVal pIdRecepcionEnc As Integer,
                                           ByVal pIdRecepcionDet As Integer,
                                           ByRef pConnection As SqlConnection,
                                           ByRef pTransaction As SqlTransaction) As Integer

        Delete_Rec_Det = 0



        Const vDeleteTransReDet As String = "DELETE FROM trans_re_det " &
                                            " WHERE IdRecepcionEnc=@IdRecepcionEnc " &
                                            " And IdRecepcionDet=@IdRecepcionDet"

        Try


            Dim lCommand As New SqlCommand(vDeleteTransReDet, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pIdRecepcionDet))

            Return lCommand.ExecuteNonQuery()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Actualiza_Detalle_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                 ByVal pIdRecepcionEnc As Integer,
                                                 ByVal pIdRecepcionDet As Integer,
                                                 ByRef pConnection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction) As Integer

        Actualiza_Detalle_OC = 0


        Dim PresentacionRecibida As Integer
        Dim beTransReDet As New clsBeTrans_re_det
        Dim CantidadRecibida As Double
        Dim vUpdateTransOC As String = ""
        Dim ListOrdenCompraDet As New List(Of clsBeTrans_oc_det)

        Try

            If pIdOrdenCompraEnc > 0 Then

                beTransReDet = Get_Recepcion_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                               pIdRecepcionDet,
                                                               pConnection,
                                                               pTransaction)

                ListOrdenCompraDet = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc_And_IdProductoBodega(pIdOrdenCompraEnc,
                                                                                                               beTransReDet.IdProductoBodega,
                                                                                                               beTransReDet.No_Linea,
                                                                                                               beTransReDet.IdOrdenCompraDet,
                                                                                                               pConnection,
                                                                                                               pTransaction)

                PresentacionRecibida = beTransReDet.IdPresentacion

                CantidadRecibida = beTransReDet.cantidad_recibida

                If ListOrdenCompraDet.Count > 0 Then

                    For Each det As clsBeTrans_oc_det In ListOrdenCompraDet

                        If det.IdPresentacion = PresentacionRecibida Then

                            If det.Cantidad_recibida >= CantidadRecibida Then

                                vUpdateTransOC = String.Format(" UPDATE trans_oc_det set cantidad_recibida = cantidad_recibida - {0}" &
                                                               " WHERE IdOrdenCompraDet = {1} And IdOrdenCompraEnc = {2} ",
                                                               CantidadRecibida, det.IdOrdenCompraDet, det.IdOrdenCompraEnc)
                                Exit For

                            Else

                                vUpdateTransOC = String.Format(" UPDATE trans_oc_det set cantidad_recibida = cantidad_recibida - {0}" &
                                                              " WHERE IdOrdenCompraDet = {1} And IdOrdenCompraEnc = {2} ",
                                                               0, det.IdOrdenCompraDet, det.IdOrdenCompraEnc)
                                Exit For

                            End If
                            '#AT20220518 Si las presentaciones no son iguales valida lo siguiente: 
                            'Si la presentación es = 0 hace la conversión antes de actualizar de la cantidad_recibida
                        Else

                            If beTransReDet.IdPresentacion = 0 Then

                                Dim BePresentacion As New clsBeProducto_Presentacion()
                                BePresentacion = clsLnProducto_presentacion.GetSingle(det.IdPresentacion, pConnection, pTransaction)

                                Dim Factor = BePresentacion.Factor

                                CantidadRecibida = beTransReDet.cantidad_recibida / Factor

                                If det.Cantidad_recibida >= CantidadRecibida Then

                                    vUpdateTransOC = String.Format(" UPDATE trans_oc_det set cantidad_recibida = cantidad_recibida - {0}" &
                                                                   " WHERE IdOrdenCompraDet = {1} And IdOrdenCompraEnc = {2} ",
                                                                   CantidadRecibida, det.IdOrdenCompraDet, det.IdOrdenCompraEnc)
                                    Exit For

                                Else

                                    vUpdateTransOC = String.Format(" UPDATE trans_oc_det set cantidad_recibida = cantidad_recibida - {0}" &
                                                                  " WHERE IdOrdenCompraDet = {1} And IdOrdenCompraEnc = {2} ",
                                                                   0, det.IdOrdenCompraDet, det.IdOrdenCompraEnc)
                                    Exit For

                                End If


                            End If
                        End If
                    Next

                End If

                If vUpdateTransOC <> "" Then

                    Dim lCommand As New SqlCommand(vUpdateTransOC, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                    Actualiza_Detalle_OC = lCommand.ExecuteNonQuery()

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Actualiza_Detalle_Lotes_OC(ByVal pIdOrdenCompraEnc As Integer,
                                                       ByVal pIdRecepcionEnc As Integer,
                                                       ByVal pIdRecepcionDet As Integer,
                                                       ByRef pConnection As SqlConnection,
                                                       ByRef pTransaction As SqlTransaction) As Integer

        Actualiza_Detalle_Lotes_OC = 0


        Dim PresentacionRecibida As Integer
        Dim beTransReDet As New clsBeTrans_re_det
        Dim CantidadRecibida As Double
        Dim vUpdateTransOC As String = ""
        Dim ListOrdenCompraDetLote As New List(Of clsBeTrans_oc_det_lote)
        Dim BeTransOCDetLote As New clsBeTrans_oc_det_lote

        Try

            If pIdOrdenCompraEnc > 0 Then

                beTransReDet = Get_Recepcion_By_IdRecepcionEnc(pIdRecepcionEnc,
                                                               pIdRecepcionDet,
                                                               pConnection,
                                                               pTransaction)

                ListOrdenCompraDetLote = clsLnTrans_oc_det_lote.Get_Detalle_Lotes_OC_By_RecepcionDet(beTransReDet,
                                                                                                     pConnection,
                                                                                                     pTransaction)

                PresentacionRecibida = beTransReDet.IdPresentacion

                If ListOrdenCompraDetLote.Count > 0 Then

                    If beTransReDet.IdPresentacion <> 0 Then
                        If beTransReDet.Presentacion.Factor <> 0 Then
                            CantidadRecibida = beTransReDet.cantidad_recibida * beTransReDet.Presentacion.Factor
                        Else
                            CantidadRecibida = beTransReDet.cantidad_recibida
                        End If
                    Else
                        CantidadRecibida = beTransReDet.cantidad_recibida
                    End If

                    For Each det As clsBeTrans_oc_det_lote In ListOrdenCompraDetLote

                        BeTransOCDetLote.IdOrdenCompraDet = det.IdOrdenCompraDet
                        BeTransOCDetLote.IdOrdenCompraEnc = det.IdOrdenCompraEnc
                        BeTransOCDetLote.Lote = det.Lote
                        BeTransOCDetLote.Fecha_vence = det.Fecha_vence
                        BeTransOCDetLote.Lic_Plate = det.Lic_Plate

                        If det.IdPresentacion = PresentacionRecibida Then

                            If det.Cantidad_recibida >= CantidadRecibida Then

                                BeTransOCDetLote.Cantidad_recibida = det.Cantidad_recibida - CantidadRecibida

                                Exit For

                            Else

                                BeTransOCDetLote.Cantidad_recibida = 0

                                Exit For

                            End If
                            '#AT20220518 Si las presentaciones no son iguales valida lo siguiente: 
                            'Si la presentación es = 0 hace la conversión antes de actualizar de la cantidad_recibida
                        Else

                            If beTransReDet.IdPresentacion = 0 Then

                                Dim BePresentacion As New clsBeProducto_Presentacion()
                                BePresentacion = clsLnProducto_presentacion.GetSingle(det.IdPresentacion, pConnection, pTransaction)

                                Dim Factor = BePresentacion.Factor

                                CantidadRecibida = beTransReDet.cantidad_recibida / Factor

                                If det.Cantidad_recibida >= CantidadRecibida Then

                                    BeTransOCDetLote.Cantidad_recibida = det.Cantidad_recibida - CantidadRecibida

                                    Exit For

                                Else

                                    BeTransOCDetLote.Cantidad_recibida = 0

                                    Exit For

                                End If


                            End If

                        End If

                    Next

                    Actualiza_Detalle_Lotes_OC = clsLnTrans_oc_det_lote.Actualizar_Cantidad_Recibida(BeTransOCDetLote, pConnection, pTransaction)

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Delete(ByVal IdOrdenCompraEnc As Integer,
                                  ByVal pIdRecepcionEnc As Integer,
                                  ByVal pIdRecepcionDet As Integer,
                                  Optional ByRef pConnection As SqlConnection = Nothing,
                                  Optional ByRef pTransaction As SqlTransaction = Nothing) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lTrans As SqlTransaction = Nothing
        Dim Resultado As String = ""

        If Not Es_Transaccion_Remota Then
            lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
        End If

        Try

            Dim FilasAfectadas As Integer = Delete_Stock_Se(pIdRecepcionEnc,
                                                            pIdRecepcionDet,
                                                            IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                            IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format("Eliminé {0} series ", FilasAfectadas)

            FilasAfectadas = Delete_Producto_Pallet(pIdRecepcionEnc,
                                                    pIdRecepcionDet,
                                                    IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                    IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format("Eliminé {0} producto pallet ", FilasAfectadas)

            FilasAfectadas = Delete_Stock_Rec(pIdRecepcionEnc,
                                              pIdRecepcionDet,
                                              IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                              IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format("Eliminé {0} stockrec ", FilasAfectadas)

            '#EJC20200317: Me encontré que no eliminar de stock generaba error de llave foránea al eliminar el recepcion_det
            'Previo a volver a insertar...
            FilasAfectadas = clsLnStock.Eliminar_By_IdRecepcionEnc_And_IdRecepcionDet(pIdRecepcionEnc,
                                                                                     pIdRecepcionDet,
                                                                                     IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                     IIf(Es_Transaccion_Remota, pTransaction, lTrans))

            Resultado += String.Format("Eliminé {0} stockrec ", FilasAfectadas)

            FilasAfectadas = Delete_Rec_Det_Parametros(pIdRecepcionEnc,
                                                       pIdRecepcionDet,
                                                       IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                       IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format("Eliminé {0} parámetros ", FilasAfectadas)

            If IdOrdenCompraEnc > 0 Then
                Actualiza_Detalle_OC(IdOrdenCompraEnc,
                                     pIdRecepcionEnc,
                                     pIdRecepcionDet,
                                     IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                     IIf(Es_Transaccion_Remota, pTransaction, lTrans))
                Resultado += String.Format("Actualicé {0} orden de compra detalle ", FilasAfectadas)
                Actualiza_Detalle_Lotes_OC(IdOrdenCompraEnc,
                                     pIdRecepcionEnc,
                                     pIdRecepcionDet,
                                     IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                     IIf(Es_Transaccion_Remota, pTransaction, lTrans))
                Resultado += String.Format("Actualicé {0} orden de compra detalle lotes ", FilasAfectadas)
            End If

            FilasAfectadas = Delete_Rec_Det(pIdRecepcionEnc,
                                            pIdRecepcionDet,
                                            IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                            IIf(Es_Transaccion_Remota, pTransaction, lTrans))

            Resultado += String.Format("Eliminé {0} detalles de la recepción ", FilasAfectadas)

            If Not Es_Transaccion_Remota Then

                Try

                    lTrans.Commit()

                Catch ex As Exception
                    If Not lTrans Is Nothing Then lTrans.Rollback()
                    Throw ex
                End Try

            End If

            Return Resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso Not lTrans Is Nothing Then lTrans.Rollback()
            Throw New Exception(String.Format("{0} {1} {2}", MethodBase.GetCurrentMethod().Name, ex.Message, Resultado))
        Finally
            If Not Es_Transaccion_Remota Then If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet(ByVal IdOrdenCompraEnc As Integer,
                                                                           ByVal pIdRecepcionEnc As Integer,
                                                                           ByVal pIdRecepcionDet As Integer,
                                                                           ByVal pIdHost As String,
                                                                           Optional ByRef pConnection As SqlConnection = Nothing,
                                                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
        Dim lTrans As SqlTransaction = Nothing
        Dim Resultado As String = ""
        Dim pRecEnc As New clsBeTrans_re_enc
        Dim pBeStockAnt As New clsBeStock
        Dim pRecDet As New clsBeTrans_re_det
        Dim IdPedido As Integer = 0

        If Not Es_Transaccion_Remota Then lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

        Try

            pRecEnc = clsLnTrans_re_enc.GetSingle(pIdRecepcionEnc,
                                                      IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                      IIf(Es_Transaccion_Remota, pTransaction, lTrans))

            If pRecEnc.Habilitar_Stock Then

                pRecDet.IdRecepcionDet = pIdRecepcionDet
                pRecDet.IdRecepcionEnc = pIdRecepcionEnc

                Obtener(pRecDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))

                If pRecDet.IdPresentacion <> 0 Then

                    Dim BePresentacion As New clsBeProducto_Presentacion
                    BePresentacion.IdPresentacion = pRecDet.IdPresentacion
                    clsLnProducto_presentacion.GetSingle(BePresentacion, lConnection, lTrans)

                    '#EJC20190325: Carol: No consideraste la presentación, por lo tanto la cantidad no encaja.
                    'No se como me afectan los decimales aún, att yo del pasado.
                    If Not BePresentacion Is Nothing Then
                        If Not BePresentacion.EsPallet Then
                            pRecDet.cantidad_recibida = (pRecDet.cantidad_recibida * BePresentacion.Factor)
                        Else
                            pRecDet.cantidad_recibida = (pRecDet.cantidad_recibida * BePresentacion.Factor * BePresentacion.CajasPorCama * BePresentacion.CamasPorTarima)
                        End If
                    End If

                End If

                '#AT20230126 Se valida que la licencia tenga stock Res
                If clsLnStock.Existe_StockRes(pRecDet, IdPedido, lConnection, lTrans) Then
                    Throw New Exception("#AT20230127A No se puede eliminar la licencia de la recepción, porque el stock está reservado en el pedido: " & IdPedido)
                Else
                    '#EJC20190325: Carol: No consideraste la presentación, por lo tanto la cantidad no encaja.
                    clsLnStock.Get_Stock(pRecDet,
                                         pBeStockAnt,
                                         IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                         IIf(Es_Transaccion_Remota, pTransaction, lTrans))

                    '#CKFK 20181107 0653PM Aquí voy a llamar a la función que me va a eliminar el Stock antes de insertarlo nuevamente
                    If Not clsLnStock.Elimina_Stock_Anterior(pBeStockAnt, Resultado, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans)) Then
                        Resultado += "No eliminé el stock anterior "
                        Throw New Exception("No se puede realizar la modificación de la recepción, el stock fue modificado")
                    Else
                        Resultado += "Eliminé el stock anterior "
                    End If

                End If

            End If

            Dim FilasAfectadas As Integer = Delete_Stock_Se(pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format(" Eliminé {0} series ", FilasAfectadas)

            FilasAfectadas = Delete_Producto_Pallet(pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format(" Eliminé {0} producto pallet ", FilasAfectadas)

            FilasAfectadas = Delete_Stock_Rec(pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format(" Eliminé {0} stockrec ", FilasAfectadas)

            FilasAfectadas = Delete_Rec_Det_Parametros(pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format(" Eliminé {0} parámetros ", FilasAfectadas)

            '#GT16102024: Cuando se elimina una linea de recepcion, debe exisitir la OC, porque no hay recepción ciega para HH.
            If IdOrdenCompraEnc > 0 Then
                Actualiza_Detalle_Lotes_OC(IdOrdenCompraEnc, pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
                Resultado += String.Format(" Actualicé {0} orden de compra detalle lote ", FilasAfectadas)
                Actualiza_Detalle_OC(IdOrdenCompraEnc, pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
                Resultado += String.Format(" Actualicé {0} orden de compra detalle ", FilasAfectadas)
            Else
                Resultado += " No actualicé el detalle de la OC."
                Throw New Exception(" No se puede actualizar el detalle en la OC")
            End If

            FilasAfectadas = Delete_Rec_Det(pIdRecepcionEnc, pIdRecepcionDet, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))
            Resultado += String.Format(" Eliminé {0} detalles de la recepción ", FilasAfectadas)

            clsLnI_nav_transacciones_out.Eliminar_By_IdRecepcionEnc_And_IdRecepcionDet(pIdRecepcionEnc,
                                                                                       pIdRecepcionDet,
                                                                                       IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                       IIf(Es_Transaccion_Remota, pTransaction, lTrans))

            '#EJC202404270040: Log Error WMS. al eliminar línea de recepción BOF.
            '#GT16102024: Se agrega al log del error, el host desde donde se elimina la linea.
            Dim BeMensajeError As New clsBeLog_error_wms
            BeMensajeError.IdError = clsLnLog_error_wms.MaxID(IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans)) + 1
            BeMensajeError.Item_No = pRecDet.Codigo_Producto
            BeMensajeError.Fecha = Now
            BeMensajeError.IdRecepcionEnc = pRecDet.IdRecepcionEnc
            BeMensajeError.IdBodega = pRecEnc.IdBodega
            BeMensajeError.Line_No = pRecDet.No_Linea
            BeMensajeError.Cantidad = pRecDet.cantidad_recibida
            BeMensajeError.IdUsuarioAgr = pRecEnc.User_agr
            BeMensajeError.MensajeError = " #EJC240427: Se eliminó el producto " & pRecDet.Codigo_Producto & " Licencia: " & pRecDet.Lic_plate & " Cantidad: " & pRecDet.cantidad_recibida & " Usuario: " & pRecEnc.User_agr & " host: " & pIdHost
            BeMensajeError.Referencia_Documento = pRecEnc.NoOrdencompra
            clsLnLog_error_wms.Insertar(BeMensajeError, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTrans))

            If Not Es_Transaccion_Remota Then

                Try

                    lTrans.Commit()

                Catch ex As Exception
                    If Not lTrans Is Nothing Then lTrans.Rollback()
                    Throw ex
                End Try

            End If


            Return Resultado


        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso Not lTrans Is Nothing Then lTrans.Rollback()
            Dim vMsgError As String = String.Format("{0} {1} {3}", MethodBase.GetCurrentMethod.Name(), ex.Message, pIdHost)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            'Throw New Exception(String.Format("{0} {1} {2}", MethodBase.GetCurrentMethod().Name, ex.Message, Resultado))
            Throw New Exception(String.Format("{0} {1}", ex.Message, Resultado))
        Finally
            If Not Es_Transaccion_Remota Then If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer) As Integer

        MaxID = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = String.Format("SELECT ISNULL(Max(IdRecepcionDet),0) FROM trans_re_det WHERE IdRecepcionEnc={0}", pIdRecepcionEnc)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            MaxID = CInt(lReturnValue)
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

    Public Shared Function MaxID(ByVal pIdRecepcionEnc As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdRecepcionDet),0) 
                                              FROM trans_re_det 
                                              WHERE IdRecepcionEnc={0}", pIdRecepcionEnc)

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text

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

    Public Shared Function Existe_Producto_En_Recepcion_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                            ByRef lConnection As SqlConnection,
                                                                            ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Producto_En_Recepcion_By_IdProductoBodega = False

        Try

            Dim lExists As Boolean = True

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_re_det WHERE IdProductoBodega=@IdProductoBodega AND IdREcepcionEnc = @IdRecepcionEnc"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) <= 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Orden_Compra_Filtro(ByVal pIdOrdenCompraEnc As Integer,
                                                     ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT det.No_Linea, det.IdProductoBodega, SUM(det.cantidad_recibida) AS CantidadRecibida, det.IdPresentacion
                       From trans_re_enc As re INNER Join trans_re_det As det On re.IdRecepcionEnc = det.IdRecepcionEnc
                       INNER JOIN trans_re_oc AS oc ON re.IdRecepcionEnc = oc.IdRecepcionEnc
                       Where oc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND det.IdRecepcionEnc <> @IdRecepcionEnc
                       Group By det.IdProductoBodega, det.IdPresentacion, det.No_Linea"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                '#EJC20180113: Agregué No_Linea en GetAllByOrdenCompraFiltro
                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
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
    ''' Creada por Carolina Fuentes
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdRecepcionEnc_Sin_OC(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT det.No_Linea, det.IdProductoBodega, SUM(det.cantidad_recibida) As CantidadRecibida, det.IdPresentacion 
                        FROM trans_re_enc As re INNER JOIN trans_re_det As det On 
                        re.IdRecepcionEnc = det.IdRecepcionEnc 
                        WHERE re.IdRecepcionEnc=@IdRecepcionEnc  GROUP BY det.IdProductoBodega, det.IdPresentacion, det.No_Linea"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
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

    Public Shared Function Get_All_By_IdRecEnc(ByVal pIdRecEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT det.No_Linea, det.IdProductoBodega, SUM(det.cantidad_recibida) As CantidadRecibida 
                        FROM trans_re_enc As re INNER JOIN trans_re_det As det On re.IdRecepcionEnc = det.IdRecepcionEnc 
                        WHERE det.IdRecepcionEnc=@IdRecepcionEnc  GROUP BY det.IdProductoBodega, det.No_Linea"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
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

    '#CKFK 20210411 Función creada para obtener las recepciones donde exista un producto
    Public Shared Function Get_All_By_Codigo_Producto(ByVal pCodigo As String, ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT distinct N
                                  FROM VW_Recepcion_For_HH_By_IdBodega_By_Producto
                                  WHERE p.codigo = @pCodigo AND re.IdBodega = @pIdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
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

    Public Shared Function Get_Recepcion_By_IdRecepcionEnc(ByVal pIdRecEnc As Integer, ByVal pIdRecDet As Integer, Optional ByRef pConnection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As clsBeTrans_re_det

        Dim beTransReDet As New clsBeTrans_re_det
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_det det WHERE det.IdRecepcionEnc=@IdRecepcionEnc And det.IdRecepcionDet=@IdRecepcionDet"

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeTrans_re_det

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows

                    Obj = New clsBeTrans_re_det

                    Cargar(Obj, lRow)

                    If Obj.IdPresentacion <> 0 Then

                        If Es_Transaccion_Remota Then
                            Obj.Presentacion = clsLnProducto_presentacion.GetSinglew(Obj.IdPresentacion, pConnection, pTransaction)
                        Else
                            Obj.Presentacion = clsLnProducto_presentacion.GetSinglew(Obj.IdPresentacion, lConnection, lTransaction)
                        End If

                    End If

                    beTransReDet = Obj

                Next

            End If

            If Not Es_Transaccion_Remota Then

                lConnection.Close()
                lConnection.Dispose()

            End If

            Return beTransReDet

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creada por Carlos Manuel
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns></returns>
    Public Shared Function GetAllByOrdenCompraRec(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT det.IdProductoBodega,det.IdPresentacion,det.No_Linea, det.nombre_producto, det.nombre_presentacion,det.nombre_unidad_medida,
                                       det.nombre_producto_estado, det.lote,det.fecha_vence,det.peso,det.observacion,det.costo,det.costo_oc,det.costo_estadistico,re.IdRecepcionEnc,
                                       SUM(det.cantidad_recibida) AS CantidadRecibida
                                       FROM trans_re_enc AS re INNER JOIN trans_re_det AS det ON
                                       re.IdRecepcionEnc = det.IdRecepcionEnc
                                       INNER JOIN trans_re_oc AS oc ON re.IdRecepcionEnc = oc.IdRecepcionEnc
                                       WHERE oc.IdOrdenCompraEnc=@IdOrdenCompraEnc GROUP BY det.IdProductoBodega,det.IdPresentacion,det.No_Linea,det.nombre_producto,
                                       det.nombre_presentacion,det.nombre_unidad_medida,det.nombre_producto_estado,det.lote,det.fecha_vence,det.peso,
                                       det.observacion,det.costo,det.costo_oc,det.costo_estadistico,re.IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("no_linea") IsNot DBNull.Value AndAlso lRow("no_linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("no_linea"), Integer)
                                End If

                                '#EJC20180113: Agruegué atributo_variante_1 en GetAllByOrdenCompraRec
                                If lRow("atributo_variante_1") IsNot DBNull.Value AndAlso lRow("atributo_variante_1") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("atributo_variante_1"), String)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
                                End If

                                If lRow("nombre_producto") IsNot DBNull.Value AndAlso lRow("nombre_producto") IsNot Nothing Then
                                    Obj.Nombre_producto = CType(lRow("nombre_producto"), String)
                                End If

                                If lRow("nombre_presentacion") IsNot DBNull.Value AndAlso lRow("nombre_presentacion") IsNot Nothing Then
                                    Obj.Nombre_presentacion = CType(lRow("nombre_presentacion"), String)
                                End If

                                If lRow("nombre_unidad_medida") IsNot DBNull.Value AndAlso lRow("nombre_unidad_medida") IsNot Nothing Then
                                    Obj.Nombre_unidad_medida = CType(lRow("nombre_unidad_medida"), String)
                                End If

                                If lRow("nombre_producto_estado") IsNot DBNull.Value AndAlso lRow("nombre_producto_estado") IsNot Nothing Then
                                    Obj.Nombre_producto_estado = CType(lRow("nombre_producto_estado"), String)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_vence = CType(lRow("fecha_vence"), String)
                                End If

                                If lRow("peso") IsNot DBNull.Value AndAlso lRow("peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("peso"), Double)
                                End If

                                If lRow("observacion") IsNot DBNull.Value AndAlso lRow("observacion") IsNot Nothing Then
                                    Obj.Observacion = CType(lRow("observacion"), String)
                                End If

                                If lRow("costo") IsNot DBNull.Value AndAlso lRow("costo") IsNot Nothing Then
                                    Obj.Costo = CType(lRow("costo"), Double)
                                End If

                                If lRow("costo_oc") IsNot DBNull.Value AndAlso lRow("costo_oc") IsNot Nothing Then
                                    Obj.Costo_Oc = CType(lRow("costo_oc"), Double)
                                End If

                                If lRow("costo_estadistico") IsNot DBNull.Value AndAlso lRow("costo_estadistico") IsNot Nothing Then
                                    Obj.Costo_Estadistico = CType(lRow("costo_estadistico"), Double)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
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

    Public Shared Sub Guarda_Trans_re_det(ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            For Each BeTransReDet As clsBeTrans_re_det In pListRecDet

                If BeTransReDet.IsNew Then
                    BeTransReDet.IdRecepcionDet = MaxID(BeTransReDet.IdRecepcionEnc, lConnection, lTransaction) + 1
                    BeTransReDet.Fecha_ingreso = Now
                    BeTransReDet.Fec_agr = Now
                    Insertar(BeTransReDet, lConnection, lTransaction)
                Else
                    Actualizar(BeTransReDet, lConnection, lTransaction)
                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guarda_Trans_re_det(ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                          ByVal Comparar_Detalle_Actual As Boolean,
                                          ByVal pRecEnc As clsBeTrans_re_enc,
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            If Not pListRecDet Is Nothing Then

                Dim pListRecDetActual As New List(Of clsBeTrans_re_det)
                Dim BeRecDetActual As New clsBeTrans_re_det

                If Comparar_Detalle_Actual Then

                    pListRecDetActual = Get_All_By_IdRecepcionEnc(pRecEnc.IdRecepcionEnc,
                                                                  lConnection,
                                                                  lTransaction)

                    For Each BeRecDet As clsBeTrans_re_det In pListRecDet

                        If BeRecDet.IsNew Then
                            Insertar(BeRecDet,
                                     lConnection,
                                     lTransaction)
                        Else

                            If pListRecDetActual.Count > 0 Then

                                BeRecDetActual = pListRecDetActual.Find(Function(x) x.IdRecepcionDet = BeRecDet.IdRecepcionDet _
                                                                        AndAlso BeRecDet.IdRecepcionEnc = BeRecDet.IdRecepcionEnc)

                                If Not BeRecDetActual Is Nothing Then

                                    If BeRecDet.IdProductoEstado <> BeRecDetActual.IdProductoEstado Then
                                        Throw New Exception("No se puede modificar el estado del producto recibido: " & BeRecDet.Nombre_producto_estado & " <> " & BeRecDetActual.Nombre_producto_estado)
                                    ElseIf BeRecDet.Lote <> BeRecDetActual.Lote Then
                                        Throw New Exception("No se puede modificar el lote del producto recibido: " & BeRecDet.Lote & " <> " & BeRecDetActual.Lote)
                                    ElseIf (BeRecDet.Producto.Control_vencimiento AndAlso BeRecDet.Fecha_vence.Date <> BeRecDetActual.Fecha_vence.Date) Then
                                        Throw New Exception("No se puede modificar la fecha-vence del producto recibido: " & BeRecDet.Fecha_vence & " <> " & BeRecDetActual.Fecha_vence)
                                    Else
                                        Actualizar(BeRecDet, lConnection, lTransaction)
                                    End If

                                End If

                            End If

                        End If

                    Next

                Else
                    'No envió el flag para validar contra recepción existente, no se modificará el stock ni el detalle de la recepción.
                End If


            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Guarda_Trans_re_det(ByVal pBeTransReDet As clsBeTrans_re_det,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Trans_re_det = 0

        Try

            If pBeTransReDet.IsNew Then
                pBeTransReDet.IdRecepcionDet = MaxID(pBeTransReDet.IdRecepcionEnc, lConnection, lTransaction) + 1
                Guarda_Trans_re_det = Insertar(pBeTransReDet, lConnection, lTransaction)
            Else
                Guarda_Trans_re_det = Actualizar(pBeTransReDet, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Detalle(ByVal pIdOrdenCompra As Integer,
                                            ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As String

        Dim vFilas As String = ""

        Try


            'Si existe detalle de recepcion elimínelo
            For Each det In pListRecDet
                If Existe(det, lConnection, lTransaction) Then
                    vFilas = Delete(pIdOrdenCompra,
                                    det.IdRecepcionEnc,
                                    det.IdRecepcionDet,
                                    lConnection,
                                    lTransaction)
                End If
            Next

            Eliminar_Detalle = vFilas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT det.IdRecepcionDet, det.IdProductoBodega, ISNULL(det.IdPresentacion, '') AS IdPresentacion, det.No_Linea, det.nombre_producto, det.nombre_presentacion, 
                      det.nombre_unidad_medida, det.nombre_producto_estado, det.lote, det.fecha_vence, ISNULL(det.peso, 0) AS peso, det.observacion, det.costo, ISNULL(det.costo_oc, 
                      0) AS costo_oc, ISNULL(det.costo_estadistico, 0) AS costo_estadistico, re.IdRecepcionEnc, SUM(det.cantidad_recibida) AS CantidadRecibida, det.codigo_producto,
                    det.lic_plate
                    FROM trans_re_enc AS re INNER JOIN
                      trans_re_det AS det ON re.IdRecepcionEnc = det.IdRecepcionEnc INNER JOIN
                      trans_re_oc AS oc ON re.IdRecepcionEnc = oc.IdRecepcionEnc
                    WHERE (oc.IdOrdenCompraEnc = @IdOrdenCompraEnc)
                    GROUP BY det.IdProductoBodega, det.IdPresentacion, det.No_Linea, det.nombre_producto, det.nombre_presentacion, det.nombre_unidad_medida, 
                      det.nombre_producto_estado, det.lote, det.fecha_vence, det.peso, det.observacion, det.costo, det.costo_oc, det.costo_estadistico, re.IdRecepcionEnc, 
                      det.IdRecepcionDet,det.codigo_producto, det.lic_plate "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("IdRecepcionDet") IsNot DBNull.Value AndAlso lRow("IdRecepcionDet") IsNot Nothing Then
                                    Obj.IdRecepcionDet = CType(lRow("IdRecepcionDet"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("nombre_producto") IsNot DBNull.Value AndAlso lRow("nombre_producto") IsNot Nothing Then
                                    Obj.Nombre_producto = CType(lRow("nombre_producto"), String)
                                End If

                                If lRow("nombre_presentacion") IsNot DBNull.Value AndAlso lRow("nombre_presentacion") IsNot Nothing Then
                                    Obj.Nombre_presentacion = CType(lRow("nombre_presentacion"), String)
                                End If

                                If lRow("nombre_unidad_medida") IsNot DBNull.Value AndAlso lRow("nombre_unidad_medida") IsNot Nothing Then
                                    Obj.Nombre_unidad_medida = CType(lRow("nombre_unidad_medida"), String)
                                End If

                                If lRow("nombre_producto_estado") IsNot DBNull.Value AndAlso lRow("nombre_producto_estado") IsNot Nothing Then
                                    Obj.Nombre_producto_estado = CType(lRow("nombre_producto_estado"), String)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("peso") IsNot DBNull.Value AndAlso lRow("peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("peso"), Double)
                                End If

                                If lRow("observacion") IsNot DBNull.Value AndAlso lRow("observacion") IsNot Nothing Then
                                    Obj.Observacion = CType(lRow("observacion"), String)
                                End If

                                If lRow("costo") IsNot DBNull.Value AndAlso lRow("costo") IsNot Nothing Then
                                    Obj.Costo = CType(lRow("costo"), Double)
                                End If

                                If lRow("costo_oc") IsNot DBNull.Value AndAlso lRow("costo_oc") IsNot Nothing Then
                                    Obj.Costo_Oc = CType(lRow("costo_oc"), Double)
                                End If

                                If lRow("costo_estadistico") IsNot DBNull.Value AndAlso lRow("costo_estadistico") IsNot Nothing Then
                                    Obj.Costo_Estadistico = CType(lRow("costo_estadistico"), Double)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
                                End If

                                If lRow("CantidadRecibida") IsNot DBNull.Value AndAlso lRow("CantidadRecibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("CantidadRecibida"), Double)
                                End If

                                If lRow("codigo_producto") IsNot DBNull.Value AndAlso lRow("codigo_producto") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("codigo_producto"), String)
                                End If

                                '#EJC20190624: Idealsa, lic_plate
                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.Lic_plate = CType(lRow("lic_plate"), String)
                                End If

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

    Public Shared Function Get_Recepciones_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_det)

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT trans_re_det.IdProductoBodega, trans_re_det.IdPresentacion, trans_re_det.IdUnidadMedida, trans_re_det.IdProductoEstado, 
                         trans_re_det.IdOperadorBodega, trans_re_det.IdMotivoDevolucion, trans_re_det.No_Linea, trans_re_det.cantidad_recibida, 
                         trans_re_det.nombre_producto, trans_re_det.nombre_presentacion, trans_re_det.nombre_unidad_medida, trans_re_det.nombre_producto_estado, 
                         trans_re_det.lote, trans_re_det.fecha_vence, trans_re_det.fecha_ingreso, trans_re_det.peso, trans_oc_det.IdOrdenCompraEnc, 
                         trans_re_det.IdRecepcionEnc, trans_re_det.codigo_producto, trans_re_enc.fecha_recepcion, trans_re_enc.hora_ini_pc, 
                         trans_re_enc.hora_fin_pc, trans_re_enc.estado, trans_re_enc.fecha_tarea, trans_re_enc.IdUbicacionRecepcion, trans_re_enc.IdMuelle,trans_re_det.lic_plate
                        FROM trans_re_det INNER JOIN
                         trans_re_oc ON trans_re_det.IdRecepcionEnc = trans_re_oc.IdRecepcionEnc INNER JOIN
                         trans_oc_det ON trans_re_oc.IdOrdenCompraEnc = trans_oc_det.IdOrdenCompraEnc INNER JOIN
                         trans_re_enc ON trans_re_det.IdRecepcionEnc = trans_re_enc.IdRecepcionEnc
                        WHERE trans_oc_det.IdOrdenCompraEnc = @IdOrdenCompraEnc
                        GROUP BY trans_re_det.IdProductoBodega, trans_re_det.IdPresentacion, trans_re_det.IdUnidadMedida, trans_re_det.IdProductoEstado, 
                         trans_re_det.IdOperadorBodega, trans_re_det.IdMotivoDevolucion, trans_re_det.No_Linea, trans_re_det.cantidad_recibida, 
                         trans_re_det.nombre_producto, trans_re_det.nombre_presentacion, trans_re_det.nombre_unidad_medida, trans_re_det.nombre_producto_estado, 
                         trans_re_det.lote, trans_re_det.fecha_vence, trans_re_det.fecha_ingreso, trans_re_det.peso, trans_oc_det.IdOrdenCompraEnc, 
                         trans_re_det.IdRecepcionEnc, trans_re_det.codigo_producto, trans_re_enc.fecha_recepcion, trans_re_enc.hora_ini_pc, 
                         trans_re_enc.hora_fin_pc, trans_re_enc.estado, trans_re_enc.fecha_tarea, trans_re_enc.IdUbicacionRecepcion, trans_re_enc.IdMuelle,trans_re_det.lic_plate"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_re_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_re_det

                                If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                                    Obj.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                                End If

                                If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                                    Obj.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                                End If

                                If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                    Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                                End If

                                If lRow("No_Linea") IsNot DBNull.Value AndAlso lRow("No_Linea") IsNot Nothing Then
                                    Obj.No_Linea = CType(lRow("No_Linea"), Integer)
                                End If

                                If lRow("nombre_producto") IsNot DBNull.Value AndAlso lRow("nombre_producto") IsNot Nothing Then
                                    Obj.Nombre_producto = CType(lRow("nombre_producto"), String)
                                End If

                                If lRow("nombre_presentacion") IsNot DBNull.Value AndAlso lRow("nombre_presentacion") IsNot Nothing Then
                                    Obj.Nombre_presentacion = CType(lRow("nombre_presentacion"), String)
                                End If

                                If lRow("nombre_unidad_medida") IsNot DBNull.Value AndAlso lRow("nombre_unidad_medida") IsNot Nothing Then
                                    Obj.Nombre_unidad_medida = CType(lRow("nombre_unidad_medida"), String)
                                End If

                                If lRow("nombre_producto_estado") IsNot DBNull.Value AndAlso lRow("nombre_producto_estado") IsNot Nothing Then
                                    Obj.Nombre_producto_estado = CType(lRow("nombre_producto_estado"), String)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("peso") IsNot DBNull.Value AndAlso lRow("peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("peso"), Double)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
                                End If

                                If lRow("IdOrdenCompraEnc") IsNot DBNull.Value AndAlso lRow("IdOrdenCompraEnc") IsNot Nothing Then
                                    Obj.IdOrdenCompraEnc = CType(lRow("IdOrdenCompraEnc"), Integer)
                                End If

                                If lRow("fecha_ingreso") IsNot DBNull.Value AndAlso lRow("fecha_ingreso") IsNot Nothing Then
                                    Obj.Fecha_ingreso = CType(lRow("fecha_ingreso"), Date)
                                End If

                                If lRow("fecha_recepcion") IsNot DBNull.Value AndAlso lRow("fecha_recepcion") IsNot Nothing Then
                                    Obj.Fecha_Rec = CType(lRow("fecha_recepcion"), Date)
                                End If

                                If lRow("hora_ini_pc") IsNot DBNull.Value AndAlso lRow("hora_ini_pc") IsNot Nothing Then
                                    Obj.Hora_ini = CType(lRow("hora_ini_pc"), Date)
                                End If

                                If lRow("fecha_tarea") IsNot DBNull.Value AndAlso lRow("fecha_tarea") IsNot Nothing Then
                                    Obj.Fecha_tarea = CType(lRow("fecha_tarea"), Date)
                                End If

                                If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacionRecepcion"), Integer)
                                    Obj.UbicacionCompleta = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(Obj.IdUbicacion, pIdBodega, lConnection, lTransaction)
                                End If

                                If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                                    Obj.Estado_Rec = CType(lRow("estado"), String)
                                End If

                                If lRow("cantidad_recibida") IsNot DBNull.Value AndAlso lRow("cantidad_recibida") IsNot Nothing Then
                                    Obj.cantidad_recibida = CType(lRow("cantidad_recibida"), Double)
                                End If

                                If lRow("codigo_producto") IsNot DBNull.Value AndAlso lRow("codigo_producto") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("codigo_producto"), String)
                                End If

                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.Lic_plate = CType(lRow("lic_plate"), String)
                                End If

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

    Public Shared Function Get_All_Sin_IdOrdenCompra(Optional ByRef pConnection As SqlConnection = Nothing,
                                                     Optional ByRef pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_re_det)

        Dim beTransReDet As New clsBeTrans_re_det
        Dim lReturnlist As New List(Of clsBeTrans_re_det)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT * FROM trans_re_det det WHERE det.IdOrdenCompraEnc IS NULL And det.IdOrdenCompraDet IS NULL"

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeTrans_re_det

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows
                    Obj = New clsBeTrans_re_det()
                    Cargar(Obj, lRow)
                    beTransReDet = Obj
                    lReturnlist.Add(beTransReDet)
                Next

            End If

            If Not Es_Transaccion_Remota Then

                lTransaction.Commit()
                lConnection.Close()
                lConnection.Dispose()

            End If

            Get_All_Sin_IdOrdenCompra = lReturnlist

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then
                If Not lTransaction Is Nothing Then lTransaction.Rollback()
            End If
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' #EJC202209211314: Concurrencia y copias de stock.
    ''' </summary>
    ''' <param name="pListRecDet"></param>
    ''' <param name="pListaStockRec"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Public Shared Function Guarda_Trans_re_det(ByVal pListRecDet As List(Of clsBeTrans_re_det),
                                              ByRef pListaStockRec As List(Of clsBeStock_rec),
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Dim vFilas As Integer = 0

        Try

            Dim MaxIdRecepcionDet As Integer = 0

            For Each BeTransReDet As clsBeTrans_re_det In pListRecDet

                If BeTransReDet.IsNew Then
                    MaxIdRecepcionDet = MaxID(BeTransReDet.IdRecepcionEnc, lConnection, lTransaction) + 1
                    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = BeTransReDet.IdRecepcionDet).ForEach(Sub(s) s.IdRecepcionDet = MaxIdRecepcionDet)
                    BeTransReDet.IdRecepcionDet = MaxIdRecepcionDet
                    BeTransReDet.Fecha_ingreso = Now
                    BeTransReDet.Fec_agr = Now
                    If BeTransReDet.IdPresentacion = -1 Then
                        BeTransReDet.IdPresentacion = 0
                    End If
                    vFilas += Insertar(BeTransReDet, lConnection, lTransaction)
                Else
                    vFilas += Actualizar(BeTransReDet, lConnection, lTransaction)
                End If

            Next

            Guarda_Trans_re_det = vFilas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Detalle_By_IdOrdenCompraEnc_And_BeRecepcionDet(ByVal pIdOrdenCompra As Integer,
                                                                                   ByVal pBeRecepcionDet As clsBeTrans_re_det,
                                                                                   ByRef lConnection As SqlConnection,
                                                                                   ByRef lTransaction As SqlTransaction) As Integer

        Dim vFilas As Integer = 0

        Try


            If Existe_By_BeRecepcionDet(pBeRecepcionDet,
                                       lConnection,
                                       lTransaction) Then

                vFilas = Delete(pIdOrdenCompra,
                                pBeRecepcionDet.IdRecepcionEnc,
                                pBeRecepcionDet.IdRecepcionDet,
                                lConnection,
                                lTransaction)
            End If

            Eliminar_Detalle_By_IdOrdenCompraEnc_And_BeRecepcionDet = vFilas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Guarda_Trans_re_det(ByVal BeTransReDet As clsBeTrans_re_det,
                                               ByRef pListaStockRec As List(Of clsBeStock_rec),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Integer

        Dim vFilas As Integer = 0

        Try

            Dim MaxIdRecepcionDet As Integer = 0

            If BeTransReDet.IsNew Then

                MaxIdRecepcionDet = MaxID(BeTransReDet.IdRecepcionEnc, lConnection, lTransaction) + 1

                pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = BeTransReDet.IdRecepcionDet).ForEach(Sub(s) s.IdRecepcionDet = MaxIdRecepcionDet)

                BeTransReDet.IdRecepcionDet = MaxIdRecepcionDet
                BeTransReDet.Fecha_ingreso = Now
                BeTransReDet.Fec_agr = Now

                vFilas += Insertar(BeTransReDet, lConnection, lTransaction)

                If BeTransReDet.Lic_plate.Equals("") Then
                    Dim vMsgError As String = "AVISO_02122024: insert rec_det lp_vacia IdRecepcionEnc:" & BeTransReDet.IdRecepcionEnc & " y IdRecepcionDet:" & BeTransReDet.IdRecepcionDet
                    clsLnLog_error_wms.Agregar_Error(vMsgError,
                                                     lConnection,
                                                     lTransaction)
                End If

            Else

                vFilas += Actualizar(BeTransReDet, lConnection, lTransaction)

                If BeTransReDet.Lic_plate.Equals("") Then
                    Dim vMsgError As String = "AVISO_02122024: update rec_det lp_vacia IdRecepcionEnc:" & BeTransReDet.IdRecepcionEnc & " y IdRecepcionDet:" & BeTransReDet.IdRecepcionDet
                    clsLnLog_error_wms.Agregar_Error(vMsgError,
                                                     lConnection,
                                                     lTransaction)
                End If

            End If

            Guarda_Trans_re_det = BeTransReDet.IdRecepcionDet

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT10032023: este metodo debe retornar un string no un integer
    Public Shared Function Eliminar_Detalle(ByVal pIdOrdenCompra As Integer,
                                            ByVal BeRecDet As clsBeTrans_re_det,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As String

        Eliminar_Detalle = ""

        Try


            If Existe(BeRecDet, lConnection, lTransaction) Then

                Eliminar_Detalle = Delete(pIdOrdenCompra,
                                          BeRecDet.IdRecepcionEnc,
                                          BeRecDet.IdRecepcionDet,
                                          lConnection,
                                          lTransaction)

            End If



        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_BeRecepcionDet(ByVal oBeTrans_re_det As clsBeTrans_re_det,
                                                    ByVal pConection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction) As Boolean


        Existe_By_BeRecepcionDet = False

        Try

            Dim sp As String = "SELECT * FROM Trans_re_det 
                                WHERE(IdRecepcionDet = @IdRecepcionDet) 
                                AND (IdRecepcionEnc = @IdRecepcionEnc) AND (IdOperadorBodega=@IdOperadorBodega ) "

            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                sp += " AND LIC_PLATE = @LIC_PLATE "
            End If

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_re_det.IdOperadorBodega))

            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_re_det.Lic_plate))
            End If

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Existe_By_BeRecepcionDet = (dt.Rows.Count = 1)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal oBeTrans_re_det As clsBeTrans_re_det,
                                                                       ByVal pConection As SqlConnection,
                                                                       ByVal pTransaction As SqlTransaction) As Boolean


        Existe_By_IdRecepcionEnc_And_IdRecepcionDet = False

        Try

            Dim sp As String = "SELECT * FROM Trans_re_det 
                                WHERE(IdRecepcionDet = @IdRecepcionDet) 
                                AND (IdRecepcionEnc = @IdRecepcionEnc) "

            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                sp += " AND LIC_PLATE = @LIC_PLATE "
            End If

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_re_det.IdOperadorBodega))

            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_re_det.Lic_plate))
            End If

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function getCantidadRecibida(ByVal No_Linea As Integer,
                                               ByVal pIdOrdenCompraEnc As Integer,
                                               ByVal pIdRecepcionEnc As Integer) As Double


        Dim lCantidad As Double = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT SUM(cantidad_recibida) cant 
                                      FROM Trans_re_det 
                                      WHERE (no_linea = @no_linea) AND 
                                            (IdRecepcionEnc <> @IdRecepcionEnc) AND 
                                            (IdOrdenCompraEnc = @IdOrdenCompraEnc ) "

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
                        lCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))
                        lCommand.Parameters.Add(New SqlParameter("@no_linea", No_Linea))

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lCantidad = CInt(lReturnValue)
                        End If

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lCantidad

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Productos_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As DataTable

        Get_Productos_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = "select distinct IdProductoBodega,codigo_producto,nombre_producto 
                                          from trans_re_det where IdOrdenCompraEnc=@pIdOrdenCompraEnc "



            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)


                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Productos_By_IdOrdenCompraEnc = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception

        End Try
    End Function

    Public Shared Function Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet_hh(ByVal IdOrdenCompraEnc As Integer,
                                                                              ByVal pIdRecepcionEnc As Integer,
                                                                              ByVal pIdRecepcionDet As Integer,
                                                                              ByVal pIdHost As String) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTrans As SqlTransaction = Nothing
        Dim Resultado As String = ""
        Dim pRecEnc As New clsBeTrans_re_enc
        Dim pBeStockAnt As New clsBeStock
        Dim pRecDet As New clsBeTrans_re_det
        Dim IdPedido As Integer = 0
        Try


            lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#GT05122024: Validar que no haya sido Finalizada previamente.
            If Not clsLnTrans_re_enc.Finalizada(pIdRecepcionEnc, lConnection, lTrans) Then

                pRecEnc = clsLnTrans_re_enc.GetSingle(pIdRecepcionEnc, lConnection, lTrans)

                If pRecEnc.Habilitar_Stock Then

                    pRecDet.IdRecepcionDet = pIdRecepcionDet
                    pRecDet.IdRecepcionEnc = pIdRecepcionEnc
                    Obtener(pRecDet, lConnection, lTrans)

                    If pRecDet.IdPresentacion <> 0 Then

                        Dim BePresentacion As New clsBeProducto_Presentacion
                        BePresentacion.IdPresentacion = pRecDet.IdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePresentacion, lConnection, lTrans)

                        '#EJC20190325: Carol: No consideraste la presentación, por lo tanto la cantidad no encaja.
                        'No se como me afectan los decimales aún, att yo del pasado.
                        If Not BePresentacion Is Nothing Then
                            If Not BePresentacion.EsPallet Then
                                pRecDet.cantidad_recibida = (pRecDet.cantidad_recibida * BePresentacion.Factor)
                            Else
                                pRecDet.cantidad_recibida = (pRecDet.cantidad_recibida * BePresentacion.Factor * BePresentacion.CajasPorCama * BePresentacion.CamasPorTarima)
                            End If
                        End If

                    End If

                    '#AT20230126 Se valida que la licencia tenga stock Res
                    If clsLnStock.Existe_StockRes(pRecDet, IdPedido, lConnection, lTrans) Then
                        Throw New Exception("#AT20230127A No se puede eliminar la licencia de la recepción, porque el stock está reservado en el pedido: " & IdPedido)
                    Else
                        '#EJC20190325: Carol: No consideraste la presentación, por lo tanto la cantidad no encaja.
                        clsLnStock.Get_Stock(pRecDet,
                                             pBeStockAnt,
                                             lConnection,
                                             lTrans)

                        '#CKFK 20181107 0653PM Aquí voy a llamar a la función que me va a eliminar el Stock antes de insertarlo nuevamente
                        If Not clsLnStock.Elimina_Stock_Anterior(pBeStockAnt, Resultado, lConnection, lTrans) Then
                            Resultado += "No eliminé el stock anterior "
                            Throw New Exception("No se puede realizar la modificación de la recepción, el stock fue modificado")
                        Else
                            Resultado += "Eliminé el stock anterior "
                        End If

                    End If

                End If

                Dim FilasAfectadas As Integer = Delete_Stock_Se(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                Resultado += String.Format(" Eliminé {0} series ", FilasAfectadas)

                FilasAfectadas = Delete_Producto_Pallet(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                Resultado += String.Format(" Eliminé {0} producto pallet ", FilasAfectadas)

                FilasAfectadas = Delete_Stock_Rec(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                Resultado += String.Format(" Eliminé {0} stockrec ", FilasAfectadas)

                FilasAfectadas = Delete_Rec_Det_Parametros(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                Resultado += String.Format(" Eliminé {0} parámetros ", FilasAfectadas)

                '#GT16102024: Cuando se elimina una linea de recepcion, debe exisitir la OC, porque no hay recepción ciega para HH.
                If IdOrdenCompraEnc > 0 Then
                    Actualiza_Detalle_Lotes_OC(IdOrdenCompraEnc, pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                    Resultado += String.Format(" Actualicé {0} orden de compra detalle lote ", FilasAfectadas)
                    Actualiza_Detalle_OC(IdOrdenCompraEnc, pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)
                    '#GT19122024: actualizo la OC por eliminar un detalle.
                    Dim BeMensajeErrorOC As New clsBeLog_error_wms
                    BeMensajeErrorOC.IdError = clsLnLog_error_wms.MaxID(lConnection, lTrans) + 1
                    BeMensajeErrorOC.IdRecepcionEnc = pIdRecepcionEnc
                    BeMensajeErrorOC.Line_No = pIdRecepcionDet
                    BeMensajeErrorOC.Fecha = Now
                    BeMensajeErrorOC.IdBodega = pRecEnc.IdBodega
                    BeMensajeErrorOC.Cantidad = pRecDet.cantidad_recibida
                    BeMensajeErrorOC.MensajeError = "AVISO19122024A_HH_EliminarRecepcion: Se actualiza OC " & IdOrdenCompraEnc & " con recepcion det " & pIdRecepcionDet & " cantidad " & pRecDet.cantidad_recibida
                    clsLnLog_error_wms.Insertar(BeMensajeErrorOC, lConnection, lTrans)
                    Resultado += String.Format(" Actualicé {0} orden de compra detalle ", FilasAfectadas)
                Else
                    Resultado += " No actualicé el detalle de la OC."
                    Throw New Exception(" No se puede actualizar el detalle en la OC")
                End If

                FilasAfectadas = Delete_Rec_Det(pIdRecepcionEnc, pIdRecepcionDet, lConnection, lTrans)

                If FilasAfectadas Then
                    '#GT19122024: eliminar la linea de recepcion
                    Dim vMsgError As String = "AVISO19122024B_HH_EliminarRecepcion: Se elimina recepcion " & pIdRecepcionEnc & " linea detalle " & pIdRecepcionDet & " cantidad " & pRecDet.cantidad_recibida & " y licencia " & pRecDet.Lic_plate
                    clsLnLog_error_wms.Agregar_Error(vMsgError, lConnection, lTrans)
                Else
                    Throw New Exception("ERROR19122024B_HH_EliminarRecepcion: No se puede eliminar la linea seleccionada de la recepciòn.")
                End If

                Resultado += String.Format(" Eliminé {0} detalles de la recepción ", FilasAfectadas)

                If pRecEnc.Habilitar_Stock Then

                    Dim pFilasAfectadas = clsLnI_nav_transacciones_out.Eliminar_By_IdRecepcionEnc_And_IdRecepcionDet(pIdRecepcionEnc,
                                                                                                                     pIdRecepcionDet,
                                                                                                                     lConnection,
                                                                                                                     lTrans)

                    If pFilasAfectadas = 0 Then
                        Dim vMsgError As String = "AVISO19122024C_HH_EliminarRecepcion: No se pudo eliminar registro de i_nav_transacciones_out, recepcion " & pIdRecepcionEnc & " recepcion detalle " & pIdRecepcionDet & " producto " & pRecDet.IdProductoBodega & " y licencia " & pRecDet.Lic_plate
                        clsLnLog_error_wms.Agregar_Error(vMsgError, lConnection, lTrans)
                        Throw New Exception("ERROR19122024B_HH_EliminarRecepcion: No se pudo eliminar el registro de i_nav_transacciones_out")
                    Else
                        Resultado += String.Format(" Eliminé {0} registros de la i_nav_transacciones_out ", FilasAfectadas)
                    End If

                End If

                '#EJC202404270040: Log Error WMS. al eliminar línea de recepción BOF.
                '#GT16102024: Se agrega al log del error, el host desde donde se elimina la linea.
                Dim BeMensajeError As New clsBeLog_error_wms
                BeMensajeError.IdError = clsLnLog_error_wms.MaxID(lConnection, lTrans) + 1
                BeMensajeError.Item_No = pRecDet.Codigo_Producto
                BeMensajeError.Fecha = Now
                BeMensajeError.IdRecepcionEnc = pRecDet.IdRecepcionEnc
                BeMensajeError.IdBodega = pRecEnc.IdBodega
                BeMensajeError.Line_No = pRecDet.No_Linea
                BeMensajeError.Cantidad = pRecDet.cantidad_recibida
                BeMensajeError.IdUsuarioAgr = pRecEnc.User_agr
                BeMensajeError.MensajeError = "EJC240427_HH_EliminarRecepcion: Se eliminó el producto " & pRecDet.Codigo_Producto & " Licencia: " & pRecDet.Lic_plate & " Cantidad: " & pRecDet.cantidad_recibida & " Usuario: " & pRecEnc.User_agr & " host: " & pIdHost
                BeMensajeError.Referencia_Documento = pRecEnc.NoOrdencompra
                clsLnLog_error_wms.Insertar(BeMensajeError, lConnection, lTrans)

            Else
                Throw New Exception("ERROR_DE_PROCESO_20241205_HH: La recepción " & pIdRecepcionEnc & " fue previamente finalizada.")
            End If

            lTrans.Commit()

            Return Resultado

        Catch ex As Exception
            If lTrans IsNot Nothing Then lTrans.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function Get_Detalle_By_IdRecepcionEnc_And_Codigo(ByVal pIdRecepcionEnc As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal CodigoProducto As String,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_re_det)

        Get_Detalle_By_IdRecepcionEnc_And_Codigo = Nothing

        Dim lReturnList As New List(Of clsBeTrans_re_det)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Get_Detalle_By_IdRecepcionEnc 
                                  WHERE IdRecepcionEnc=@IdRecepcionEnc AND IdBodega = @IdBodega AND codigo_producto = @CodigoProducto"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@CodigoProducto", CodigoProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransReDet As clsBeTrans_re_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransReDet = New clsBeTrans_re_det

                        Cargar(BeTransReDet, lRow)

                        BeTransReDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                        BeTransReDet.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                        'clsLnProducto.Obtener(BeTransReDet.Producto, lConnection, lTransaction)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransReDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            'clsLnProducto_presentacion.Obtener(BeTransReDet.Presentacion, lConnection, lTransaction)
                        End If

                        If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                            BeTransReDet.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                            'clsLnUnidad_medida.Obtener(BeTransReDet.UnidadMedida, lConnection, lTransaction)
                        End If

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            BeTransReDet.ProductoEstado.IdEstado = CType(lRow("IdProductoEstado"), Integer)
                            'clsLnProducto_estado.Obtener(BeTransReDet.ProductoEstado, lConnection, lTransaction)
                        End If

                        If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                            BeTransReDet.MotivoDevolucion.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                            If BeTransReDet.MotivoDevolucion.IdMotivoDevolucion <> 0 Then
                                'clsLnMotivo_devolucion.Obtener(BeTransReDet.MotivoDevolucion, lConnection, lTransaction)
                            End If
                        End If

                        If lRow("IdUbicacionRecepcion") IsNot DBNull.Value AndAlso lRow("IdUbicacionRecepcion") IsNot Nothing Then
                            BeTransReDet.IdUbicacion = CType(lRow("IdUbicacionRecepcion"), Integer)
                            BeTransReDet.IdUbicacionAnterior = CType(lRow("IdUbicacionRecepcion"), Integer)
                        End If

                        BeTransReDet.IsNew = False

                        lReturnList.Add(BeTransReDet)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
