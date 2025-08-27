Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnImpresora_mensaje

    Public Shared Sub Cargar(ByRef oBeImpresora_mensaje As clsBeImpresora_mensaje, ByRef dr As DataRow)
        Try
            With oBeImpresora_mensaje
                .IdImpresoraMensaje = IIf(IsDBNull(dr.Item("IdImpresoraMensaje")), 0, dr.Item("IdImpresoraMensaje"))
                .IdImpresora = IIf(IsDBNull(dr.Item("IdImpresora")), 0, dr.Item("IdImpresora"))
                .Mensaje = IIf(IsDBNull(dr.Item("mensaje")), "", dr.Item("mensaje"))
                .IdMensaje = IIf(IsDBNull(dr.Item("IdMensaje")), 0, dr.Item("IdMensaje"))
                .Host = IIf(IsDBNull(dr.Item("host")), "", dr.Item("host"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), 0, dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeImpresora_mensaje As clsBeImpresora_mensaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresora_mensaje")
            Ins.Add("idimpresoramensaje", "@idimpresoramensaje", DataType.Parametro)
            Ins.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Ins.Add("mensaje", "@mensaje", DataType.Parametro)
            Ins.Add("idmensaje", "@idmensaje", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORAMENSAJE", oBeImpresora_mensaje.IdImpresoraMensaje))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora_mensaje.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeImpresora_mensaje.Mensaje))
            cmd.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeImpresora_mensaje.IdMensaje))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeImpresora_mensaje.Host))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_mensaje.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_mensaje.Fec_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeImpresora_mensaje As clsBeImpresora_mensaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora_mensaje")
            Upd.Add("idimpresoramensaje", "@idimpresoramensaje", "F")
            Upd.Add("idimpresora", "@idimpresora", "F")
            Upd.Add("mensaje", "@mensaje", "F")
            Upd.Add("idmensaje", "@idmensaje", "F")
            Upd.Add("host", "@host", "F")
            Upd.Add("user_agr", "@user_agr", "F")
            Upd.Add("fec_agr", "@fec_agr", "F")
            Upd.Where("IdImpresoraMensaje = @IdImpresoraMensaje")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORAMENSAJE", oBeImpresora_mensaje.IdImpresoraMensaje))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora_mensaje.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeImpresora_mensaje.Mensaje))
            cmd.Parameters.Add(New SqlParameter("@IDMENSAJE", oBeImpresora_mensaje.IdMensaje))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeImpresora_mensaje.Host))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_mensaje.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_mensaje.Fec_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeImpresora_mensaje As clsBeImpresora_mensaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresora_mensaje" & _
             "  Where(IdImpresoraMensaje = @IdImpresoraMensaje)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORAMENSAJE", oBeImpresora_mensaje.IdImpresoraMensaje))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Impresora_mensaje"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeImpresora_mensaje)

        Dim lReturnList As New List(Of clsBeImpresora_mensaje)

        Try

            Const sp As String = "SELECT * FROM Impresora_mensaje"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_mensaje As New clsBeImpresora_mensaje

                        For Each dr As DataRow In lDataTable.Rows
                            vBeImpresora_mensaje = New clsBeImpresora_mensaje()
                            Cargar(vBeImpresora_mensaje, dr)
                            lReturnList.Add(vBeImpresora_mensaje)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeImpresora_mensaje As clsBeImpresora_mensaje)

        Try

            Const sp As String = "SELECT * FROM Impresora_mensaje" & _
            " Where(IdImpresoraMensaje = @IdImpresoraMensaje)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_mensaje As New clsBeImpresora_mensaje

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresora_mensaje, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdImpresoraMensaje),0) FROM Impresora_mensaje"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingleByNombre(ByVal pMensaje As String) As clsBeImpresora_mensaje
        Dim vBeImpresora_mensaje As clsBeImpresora_mensaje = Nothing
        Try
            Const sp As String = "SELECT * FROM Impresora_mensaje Where (mensaje = @Mensaje)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Mensaje", pMensaje)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            vBeImpresora_mensaje = New clsBeImpresora_mensaje()
                            Cargar(vBeImpresora_mensaje, lDataTable.Rows(0))
                        End If

                    End Using
                    lTransaction.Commit()
                End Using
                lConnection.Close()
            End Using

            Return vBeImpresora_mensaje

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
