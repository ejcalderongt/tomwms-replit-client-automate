Imports System.Data.SqlClient

Public Class clsLnTrans_log_pedido_liberacion

    Public Shared Sub Cargar(ByRef oBeTrans_log_pedido_liberacion As clsBeTrans_log_pedido_liberacion, ByRef dr As DataRow)
        Try
            With oBeTrans_log_pedido_liberacion
                .IdLogLiberacionStock = IIf(IsDBNull(dr.Item("IdLogLiberacionStock")), 0, dr.Item("IdLogLiberacionStock"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdUsuario = IIf(IsDBNull(dr.Item("IdUsuario")), 0, dr.Item("IdUsuario"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), Date.Now, dr.Item("Fecha"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo_Producto")), "", dr.Item("Codigo_Producto"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Lic_Plate = IIf(IsDBNull(dr.Item("Lic_Plate")), "", dr.Item("Lic_Plate"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), Date.Now, dr.Item("Fecha_Vence"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_log_pedido_liberacion As clsBeTrans_log_pedido_liberacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_log_pedido_liberacion")
            Ins.Add("idlogliberacionstock", "@idlogliberacionstock", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("IdPickingUbic", "@IdPickingUbic", DataType.Parametro)
            Ins.Add("IdPickingDet", "@IdPickingDet", DataType.Parametro)
            Ins.Add("IdStock", "@IdStock", DataType.Parametro)
            Ins.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            Ins.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Ins.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Ins.Add("IdUnidadMedida", "@IdUnidadMedida", DataType.Parametro)
            Ins.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Ins.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGLIBERACIONSTOCK", oBeTrans_log_pedido_liberacion.IdLogLiberacionStock))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_log_pedido_liberacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_log_pedido_liberacion.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_log_pedido_liberacion.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_log_pedido_liberacion.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_log_pedido_liberacion.Fecha))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_log_pedido_liberacion.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_log_pedido_liberacion.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_log_pedido_liberacion.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_log_pedido_liberacion.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_log_pedido_liberacion.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_log_pedido_liberacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_log_pedido_liberacion.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_log_pedido_liberacion.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_log_pedido_liberacion.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_log_pedido_liberacion.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_log_pedido_liberacion.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_log_pedido_liberacion.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_log_pedido_liberacion.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_log_pedido_liberacion.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_log_pedido_liberacion.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_log_pedido_liberacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_log_pedido_liberacion.IdPresentacion))

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

    Public Shared Function Actualizar(ByRef oBeTrans_log_pedido_liberacion As clsBeTrans_log_pedido_liberacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_log_pedido_liberacion")
            Upd.Add("idlogliberacionstock", "@idlogliberacionstock", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("IdPickingUbic", "@IdPickingUbic", DataType.Parametro)
            Upd.Add("IdPickingDet", "@IdPickingDet", DataType.Parametro)
            Upd.Add("IdStock", "@IdStock", DataType.Parametro)
            Upd.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            Upd.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Upd.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Upd.Add("IdUnidadMedida", "@IdUnidadMedida", DataType.Parametro)
            Upd.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Upd.Where("IdLogLiberacionStock = @IdLogLiberacionStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGLIBERACIONSTOCK", oBeTrans_log_pedido_liberacion.IdLogLiberacionStock))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_log_pedido_liberacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_log_pedido_liberacion.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_log_pedido_liberacion.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_log_pedido_liberacion.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_log_pedido_liberacion.Fecha))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_log_pedido_liberacion.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_log_pedido_liberacion.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_log_pedido_liberacion.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_log_pedido_liberacion.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_log_pedido_liberacion.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_log_pedido_liberacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_log_pedido_liberacion.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_log_pedido_liberacion.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_log_pedido_liberacion.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_log_pedido_liberacion.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_log_pedido_liberacion.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_log_pedido_liberacion.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_log_pedido_liberacion.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_log_pedido_liberacion.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_log_pedido_liberacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_log_pedido_liberacion.IdProductoEstado))

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


    Public Shared Function Eliminar(ByRef oBeTrans_log_pedido_liberacion As clsBeTrans_log_pedido_liberacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_log_pedido_liberacion" &
             "  Where(IdLogLiberacionStock = @IdLogLiberacionStock)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGLIBERACIONSTOCK", oBeTrans_log_pedido_liberacion.IdLogLiberacionStock))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_log_pedido_liberacion"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_log_pedido_liberacion)

        Dim lReturnList As New List(Of clsBeTrans_log_pedido_liberacion)

        Try

            Const sp As String = "SELECT * FROM Trans_log_pedido_liberacion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_log_pedido_liberacion = New clsBeTrans_log_pedido_liberacion()
                            Cargar(vBeTrans_log_pedido_liberacion, dr)
                            lReturnList.Add(vBeTrans_log_pedido_liberacion)
                        Next

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

    Public Shared Sub GetSingle(ByRef pBeTrans_log_pedido_liberacion As clsBeTrans_log_pedido_liberacion)

        Try

            Const sp As String = "SELECT * FROM Trans_log_pedido_liberacion" &
            " Where(IdLogLiberacionStock = @IdLogLiberacionStock)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_log_pedido_liberacion, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLogLiberacionStock),0) FROM Trans_log_pedido_liberacion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc_And_IdBodega(ByVal pIdPedidoEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_log_pedido_liberacion)

        Dim lReturnList As New List(Of clsBeTrans_log_pedido_liberacion)

        Try

            Const sp As String = "SELECT * FROM Trans_log_pedido_liberacion WHERE IdPedidoEnc = @IdPedidoEnc AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_log_pedido_liberacion = New clsBeTrans_log_pedido_liberacion()
                            Cargar(vBeTrans_log_pedido_liberacion, dr)
                            lReturnList.Add(vBeTrans_log_pedido_liberacion)
                        Next

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

    Public Shared Function Get_All_By_IdPedidoEnc_And_IdBodega(ByVal pIdPedidoEnc As Integer,
                                                               ByVal pIdBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_log_pedido_liberacion)

        Dim lReturnList As New List(Of clsBeTrans_log_pedido_liberacion)

        Try

            Const sp As String = "SELECT * FROM Trans_log_pedido_liberacion WHERE IdPedidoEnc = @IdPedidoEnc AND IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_log_pedido_liberacion = New clsBeTrans_log_pedido_liberacion()
                    Cargar(vBeTrans_log_pedido_liberacion, dr)
                    lReturnList.Add(vBeTrans_log_pedido_liberacion)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
