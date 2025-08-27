Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_transacciones_out_error

    Public Shared Sub Cargar(ByRef oBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error, ByRef dr As DataRow)

        Try

            With oBeI_nav_transacciones_out_error
                .IdMensaje = IIf(IsDBNull(dr.Item("IdMensaje")), 0, dr.Item("IdMensaje"))
                .IdTransaccionWMS = IIf(IsDBNull(dr.Item("IdTransaccionWMS")), 0, dr.Item("IdTransaccionWMS"))
                .IdTipoTransaccionWMS = IIf(IsDBNull(dr.Item("IdTipoTransaccionWMS")), "", dr.Item("IdTipoTransaccionWMS"))
                .ReferenciaERP = IIf(IsDBNull(dr.Item("ReferenciaERP")), "", dr.Item("ReferenciaERP"))
                .TransaccionERP = IIf(IsDBNull(dr.Item("TransaccionERP")), "", dr.Item("TransaccionERP"))
                .Mensaje = IIf(IsDBNull(dr.Item("Mensaje")), "", dr.Item("Mensaje"))
                .EsError = IIf(IsDBNull(dr.Item("EsError")), False, dr.Item("EsError"))
                .NumeroError = IIf(IsDBNull(dr.Item("NumeroError")), "", dr.Item("NumeroError"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                .UsuarioERP = IIf(IsDBNull(dr.Item("UsuarioERP")), "", dr.Item("UsuarioERP"))
                .UsuarioWMS = IIf(IsDBNull(dr.Item("UsuarioWMS")), "", dr.Item("UsuarioWMS"))
                .IdDirectorio = IIf(IsDBNull(dr.Item("IdDirectorio")), "", dr.Item("IdDirectorio"))
            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_transacciones_out_error")
            Ins.Add("idmensaje", "@idmensaje", DataType.Parametro)
            Ins.Add("idtransaccionwms", "@idtransaccionwms", DataType.Parametro)
            Ins.Add("idtipotransaccionwms", "@idtipotransaccionwms", DataType.Parametro)
            Ins.Add("referenciaerp", "@referenciaerp", DataType.Parametro)
            Ins.Add("transaccionerp", "@transaccionerp", DataType.Parametro)
            Ins.Add("mensaje", "@mensaje", DataType.Parametro)
            Ins.Add("eserror", "@eserror", DataType.Parametro)
            Ins.Add("numeroerror", "@numeroerror", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("usuarioerp", "@usuarioerp", DataType.Parametro)
            Ins.Add("usuariowms", "@usuariowms", DataType.Parametro)
            Ins.Add("iddirectorio", "@iddirectorio", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeI_nav_transacciones_out_error.IdMensaje))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONWMS", oBeI_nav_transacciones_out_error.IdTransaccionWMS))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCIONWMS", oBeI_nav_transacciones_out_error.IdTipoTransaccionWMS))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIAERP", oBeI_nav_transacciones_out_error.ReferenciaERP))
            cmd.Parameters.Add(New SqlParameter("@TRANSACCIONERP", oBeI_nav_transacciones_out_error.TransaccionERP))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeI_nav_transacciones_out_error.Mensaje))
            cmd.Parameters.Add(New SqlParameter("@ESERROR", oBeI_nav_transacciones_out_error.EsError))
            cmd.Parameters.Add(New SqlParameter("@NUMEROERROR", oBeI_nav_transacciones_out_error.NumeroError))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeI_nav_transacciones_out_error.Observacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_transacciones_out_error.Fecha))
            cmd.Parameters.Add(New SqlParameter("@USUARIOERP", oBeI_nav_transacciones_out_error.UsuarioERP))
            cmd.Parameters.Add(New SqlParameter("@USUARIOWMS", oBeI_nav_transacciones_out_error.UsuarioWMS))
            cmd.Parameters.Add(New SqlParameter("@IDDIRECTORIO", oBeI_nav_transacciones_out_error.IdDirectorio))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Upd.Init("i_nav_transacciones_out_error")
            Upd.Add("idmensaje", "@idmensaje", DataType.Parametro)
            Upd.Add("idtransaccionwms", "@idtransaccionwms", DataType.Parametro)
            Upd.Add("idtipotransaccionwms", "@idtipotransaccionwms", DataType.Parametro)
            Upd.Add("referenciaerp", "@referenciaerp", DataType.Parametro)
            Upd.Add("transaccionerp", "@transaccionerp", DataType.Parametro)
            Upd.Add("mensaje", "@mensaje", DataType.Parametro)
            Upd.Add("eserror", "@eserror", DataType.Parametro)
            Upd.Add("numeroerror", "@numeroerror", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("usuarioerp", "@usuarioerp", DataType.Parametro)
            Upd.Add("usuariowms", "@usuariowms", DataType.Parametro)
            Upd.Where("IdMensaje = @IdMensaje")

            Dim sp As String = Upd.SQL()

            Dim EsTransaccional As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}


            If EsTransaccional Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeI_nav_transacciones_out_error.IdMensaje))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONWMS", oBeI_nav_transacciones_out_error.IdTransaccionWMS))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCIONWMS", oBeI_nav_transacciones_out_error.IdTipoTransaccionWMS))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIAERP", oBeI_nav_transacciones_out_error.ReferenciaERP))
            cmd.Parameters.Add(New SqlParameter("@TRANSACCIONERP", oBeI_nav_transacciones_out_error.TransaccionERP))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeI_nav_transacciones_out_error.Mensaje))
            cmd.Parameters.Add(New SqlParameter("@ESERROR", oBeI_nav_transacciones_out_error.EsError))
            cmd.Parameters.Add(New SqlParameter("@NUMEROERROR", oBeI_nav_transacciones_out_error.NumeroError))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeI_nav_transacciones_out_error.Observacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_transacciones_out_error.Fecha))
            cmd.Parameters.Add(New SqlParameter("@USUARIOERP", oBeI_nav_transacciones_out_error.UsuarioERP))
            cmd.Parameters.Add(New SqlParameter("@USUARIOWMS", oBeI_nav_transacciones_out_error.UsuarioWMS))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Const sp As String = " Delete from I_nav_transacciones_out_error" &
             "  Where(IdMensaje = @IdMensaje)"

            Dim EsTransaccional As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            If EsTransaccional Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeI_nav_transacciones_out_error.IdMensaje))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected


        Catch ex As Exception
            Throw ex
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try
    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_out_error" &
            " Where(IdMensaje = @IdMensaje)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeI_nav_transacciones_out_error.IdMensaje))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_transacciones_out_error, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_transacciones_out_error)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out_error)
            Const sp As String = "SELECT * FROM I_nav_transacciones_out_error"
            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out_error As New clsBeI_nav_transacciones_out_error

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out_error = New clsBeI_nav_transacciones_out_error
                Cargar(vBeI_nav_transacciones_out_error, dr)
                lReturnList.Add(vBeI_nav_transacciones_out_error)
            Next

            Return lReturnList

            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_transacciones_out_error As clsBeI_nav_transacciones_out_error)

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_out_error" &
            " Where(IdMensaje = @IdMensaje)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENSAJE", pBeI_nav_transacciones_out_error.IdMensaje))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_transacciones_out_error, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMensaje),0) FROM I_nav_transacciones_out_error "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Filtros(ByVal pIdDirectorio As Integer,
                                              ByVal pFechaDesde As Date,
                                              ByVal pFechaHasta As Date) As List(Of clsBeI_nav_transacciones_out_error)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out_error)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out_error WHERE IdDirectorio = @IdDirectorio "

            sp += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDesde) &
                   " AND " & FormatoFechas.fFecha(pFechaHasta)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdDirectorio", pIdDirectorio)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out_error As New clsBeI_nav_transacciones_out_error

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out_error = New clsBeI_nav_transacciones_out_error
                Cargar(vBeI_nav_transacciones_out_error, dr)
                lReturnList.Add(vBeI_nav_transacciones_out_error)
            Next

            lTransaction.Commit()

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

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeI_nav_transacciones_out_error)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out_error)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out_error WHERE IdTransaccionWMS = @IdPedidoEnc"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out_error As New clsBeI_nav_transacciones_out_error

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out_error = New clsBeI_nav_transacciones_out_error
                Cargar(vBeI_nav_transacciones_out_error, dr)
                lReturnList.Add(vBeI_nav_transacciones_out_error)
            Next

            lTransaction.Commit()

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

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_transacciones_out_error)

        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out_error)

        Try

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out_error WHERE IdTransaccionWMS = @IdPedidoEnc"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out_error As New clsBeI_nav_transacciones_out_error

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out_error = New clsBeI_nav_transacciones_out_error
                Cargar(vBeI_nav_transacciones_out_error, dr)
                lReturnList.Add(vBeI_nav_transacciones_out_error)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
