Imports System.Data.SqlClient
Partial Public Class clsLnTrans_manufactura_enc

    ''' <summary>
    ''' #EJC202404020635: Inserta la transacción de manufactura después de que se inserta el pedido de SAP.
    ''' </summary>
    ''' <param name="INavPedidoCliente"></param>
    ''' <param name="BePedidoEncResult"></param>
    ''' <returns></returns>
    Public Shared Function Insertar_Manufactura_Por_Defecto(ByVal INavPedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                            ByVal BePedidoEncResult As clsBeTrans_pe_enc,
                                                            ByVal BeConfigEnc As clsBeI_nav_config_enc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vCantidadLineas As Integer = 0
        Dim lStockResByPedido As New List(Of clsBeVW_stock_res)

        Insertar_Manufactura_Por_Defecto = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not BeConfigEnc Is Nothing Then

                lStockResByPedido = clsLnStock_res.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc, lConnection, lTransaction)

                Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc
                BeManufacturaEnc.IdManufacturaEnc = MaxID(lConnection, lTransaction) + 1
                BeManufacturaEnc.IdBodega = BePedidoEncResult.IdBodega
                BeManufacturaEnc.IdPropietarioBodega = BePedidoEncResult.IdPropietarioBodega
                BeManufacturaEnc.IdPedidoEnc = BePedidoEncResult.IdPedidoEnc
                BeManufacturaEnc.IdTipoManufactura = BePedidoEncResult.IdTipoManufactura
                BeManufacturaEnc.Fecha_manufactura = New Date(1900, 1, 1)
                BeManufacturaEnc.Fec_agr = Now
                BeManufacturaEnc.Fec_mod = Now
                BeManufacturaEnc.Estado = "Sin Asignar"
                BeManufacturaEnc.User_agr = BeConfigEnc.IdUsuario
                BeManufacturaEnc.User_mod = BeConfigEnc.User_agr
                BeManufacturaEnc.Escaneo = True
                BeManufacturaEnc.Activo = True

                Insertar(BeManufacturaEnc, lConnection, lTransaction)

                Dim BeManufacturaDet As New clsBeTrans_manufactura_det

                If BeManufacturaEnc.IdTipoManufactura = clsDataContractDI.Manufacturing_Process.Pegar_Stickers Then

                    Dim lBeProductoBodega As List(Of clsBeProducto_bodega) = clsLnProducto_bodega.Get_All_By_IdTipoManufactura(BeManufacturaEnc.IdTipoManufactura,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                    Dim lProductoConBono As List(Of Integer) = lBeProductoBodega.Select(Function(a) a.IdProductoBodega).Distinct.ToList()

                    Dim lProductoConBonoEnPedido = BePedidoEncResult.Detalle.FindAll(Function(x) lProductoConBono.Contains(x.IdProductoBodega))

                    If lProductoConBonoEnPedido.Count = 0 Then
                        Throw New Exception("MSG_240506: La tarea de manufactura es pegar_stickers pero ningún producto tiene bono en el pedido.")
                    End If

                    If Not lStockResByPedido Is Nothing Then

                        For Each ProductoEnPedido In BePedidoEncResult.Detalle.FindAll(Function(x) lProductoConBono.Contains(x.IdProductoBodega))

                            If lStockResByPedido.Any(Function(x) x.Codigo_Producto = ProductoEnPedido.Codigo_Producto) Then

                                BeManufacturaDet = New clsBeTrans_manufactura_det
                                BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(lConnection, lTransaction) + 1
                                BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                                BeManufacturaDet.IdPedidoDet = ProductoEnPedido.IdPedidoDet
                                BeManufacturaDet.IdPropietarioBodega = BePedidoEncResult.IdPropietarioBodega
                                BeManufacturaDet.IdProductoBodega = ProductoEnPedido.IdProductoBodega
                                BeManufacturaDet.Codigo_producto = ProductoEnPedido.Codigo_Producto
                                BeManufacturaDet.Nombre_producto = ProductoEnPedido.Nombre_producto
                                BeManufacturaDet.Cantidad_esperada = ProductoEnPedido.Cantidad
                                BeManufacturaDet.Fec_agr = Now
                                BeManufacturaDet.Fec_mod = Now
                                BeManufacturaDet.User_agr = BeConfigEnc.IdUsuario
                                BeManufacturaDet.User_mod = BeConfigEnc.User_agr

                                clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, lConnection, lTransaction)

                                vCantidadLineas += 1

                            End If

                        Next

                    Else
                        Throw New Exception("El pedido no tiene reservas para procesos de manufactura ligera.")
                    End If

                Else

                    For Each ProductoEnPedido In BePedidoEncResult.Detalle

                        If lStockResByPedido.Any(Function(x) x.Codigo_Producto = ProductoEnPedido.Codigo_Producto) Then

                            BeManufacturaDet = New clsBeTrans_manufactura_det
                            BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(lConnection, lTransaction) + 1
                            BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                            BeManufacturaDet.IdPedidoDet = ProductoEnPedido.IdPedidoDet
                            BeManufacturaDet.IdPropietarioBodega = BePedidoEncResult.IdPropietarioBodega
                            BeManufacturaDet.IdProductoBodega = ProductoEnPedido.IdProductoBodega
                            BeManufacturaDet.Codigo_producto = ProductoEnPedido.Codigo_Producto
                            BeManufacturaDet.Nombre_producto = ProductoEnPedido.Nombre_producto
                            BeManufacturaDet.Cantidad_esperada = ProductoEnPedido.Cantidad
                            BeManufacturaDet.Fec_agr = Now
                            BeManufacturaDet.Fec_mod = Now
                            BeManufacturaDet.User_agr = BeConfigEnc.IdUsuario
                            BeManufacturaDet.User_mod = BeConfigEnc.User_agr

                            clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, lConnection, lTransaction)

                            vCantidadLineas += 1

                        End If

                    Next

                End If

            End If

            If vCantidadLineas = 0 Then
                Throw New Exception("La tarea de manufactura no se creó, revise si las líneas de detalle aplican a tarea de manufactura ligera.")
            End If

            lTransaction.Commit()

            Insertar_Manufactura_Por_Defecto = True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_TipoManufactura_By_IdPedidoDet(ByVal pIdPedidoDet As Integer) As Integer

        Try

            Dim lTipoManufactura As Integer = 0

            Dim vSQL As String = "SELECT e.IdTipoManufactura
                                  FROM trans_manufactura_enc e INNER JOIN trans_manufactura_det d on e.IdManufacturaEnc = d.IdManufacturaEnc
                                  WHERE d.IdPedidoDet = @IdPedidoDet "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
                        lCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lTipoManufactura = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTipoManufactura

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Pendientes_By_IdManufacturaEnc(ByVal pIdManufacturaEnc As Integer) As Integer

        Try

            Dim lPendientes As Integer = 0

            Dim vSQL As String = "SELECT SUM(d.cantidad_esperada)-SUM(d.cantidad_recibida) Diferencia
                                  FROM trans_manufactura_enc e INNER JOIN 
                                       trans_manufactura_det d on e.IdManufacturaEnc = d.IdManufacturaEnc
                                  WHERE e.IdManufacturaEnc = @IdManufacturaEnc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
                        lCommand.Parameters.AddWithValue("@IdManufacturaEnc", pIdManufacturaEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lPendientes = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lPendientes

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Tarea_Manufactura_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE from trans_manufactura_det 
                                   WHERE IdManufacturaEnc IN (SELECT IdManufacturaEnc FROM trans_manufactura_enc WHERE IdPedidoEnc =  @IdPedidoEnc);
                                   DELETE FROM trans_manufactura_enc WHERE IdPedidoEnc =  @IdPedidoEnc;"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", pIdPedidoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Manufactura_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lIdManufactura As Integer = 0

            Dim vSQL As String = "SELECT e.IdManufacturaEnc
                                  FROM trans_manufactura_enc e 
                                  WHERE e.IdPedidoEnc = @IdPedidoEnc AND e.estado <> 'Anulado' "

            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = lTransaction
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lIdManufactura = CInt(lReturnValue)
                End If

            End Using

            Return lIdManufactura

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle_By_IdPedidoEnc(ByRef pBeTrans_manufactura_enc As clsBeTrans_manufactura_enc,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_enc  
                                  Where(IdPedidoEnc = @IdPedidoEnc)"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pBeTrans_manufactura_enc.IdPedidoEnc)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_manufactura_enc As New clsBeTrans_manufactura_enc

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTrans_manufactura_enc, lDataTable.Rows(0))
                    pBeTrans_manufactura_enc = vBeTrans_manufactura_enc
                Else
                    vBeTrans_manufactura_enc = Nothing
                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
