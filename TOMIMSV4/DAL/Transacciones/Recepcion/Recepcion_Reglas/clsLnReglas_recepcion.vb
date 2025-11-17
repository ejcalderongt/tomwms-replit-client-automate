Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnReglas_recepcion

    Public Shared Sub Cargar(ByRef oBeReglas_recepcion As clsBeReglas_recepcion, ByRef dr As DataRow)
        Try
            With oBeReglas_recepcion
                .IdReglaRecepcion = IIf(IsDBNull(dr.Item("IdReglaRecepcion")), 0, dr.Item("IdReglaRecepcion"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Rechazar = IIf(IsDBNull(dr.Item("Rechazar")), False, dr.Item("Rechazar"))
                .StockNoDisponible = IIf(IsDBNull(dr.Item("StockNoDisponible")), False, dr.Item("StockNoDisponible"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Es_Proceso = IIf(IsDBNull(dr.Item("es_proceso")), False, dr.Item("es_proceso"))
                .TipoRegla = IIf(IsDBNull(dr.Item("TipoRegla")), "", dr.Item("TipoRegla"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeReglas_recepcion As clsBeReglas_recepcion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("reglas_recepcion")
            Ins.Add("idreglarecepcion", "@idreglarecepcion", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("rechazar", "@rechazar", DataType.Parametro)
            Ins.Add("stocknodisponible", "@stocknodisponible", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBeReglas_recepcion.IdReglaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeReglas_recepcion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeReglas_recepcion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeReglas_recepcion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR", oBeReglas_recepcion.Rechazar))
            cmd.Parameters.Add(New SqlParameter("@STOCKNODISPONIBLE", oBeReglas_recepcion.StockNoDisponible))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeReglas_recepcion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeReglas_recepcion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeReglas_recepcion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeReglas_recepcion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeReglas_recepcion.Activo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeReglas_recepcion As clsBeReglas_recepcion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("reglas_recepcion")
            Upd.Add("idreglarecepcion", "@idreglarecepcion", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("rechazar", "@rechazar", DataType.Parametro)
            Upd.Add("stocknodisponible", "@stocknodisponible", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdReglaRecepcion = @IdReglaRecepcion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBeReglas_recepcion.IdReglaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeReglas_recepcion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeReglas_recepcion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeReglas_recepcion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR", oBeReglas_recepcion.Rechazar))
            cmd.Parameters.Add(New SqlParameter("@STOCKNODISPONIBLE", oBeReglas_recepcion.StockNoDisponible))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeReglas_recepcion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeReglas_recepcion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeReglas_recepcion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeReglas_recepcion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeReglas_recepcion.Activo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeReglas_recepcion As clsBeReglas_recepcion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Reglas_recepcion" &
             "  Where(IdReglaRecepcion = @IdReglaRecepcion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBeReglas_recepcion.IdReglaRecepcion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Reglas_recepcion"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeReglas_recepcion As clsBeReglas_recepcion) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Reglas_recepcion" &
            " Where(IdReglaRecepcion = @IdReglaRecepcion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBeReglas_recepcion.IdReglaRecepcion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeReglas_recepcion, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeReglas_recepcion As clsBeReglas_recepcion) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Reglas_recepcion" &
            " Where(IdReglaRecepcion = @IdReglaRecepcion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", pBeReglas_recepcion.IdReglaRecepcion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeReglas_recepcion, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeReglas_recepcion As clsBeReglas_recepcion,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Reglas_recepcion " &
            " Where(IdReglaRecepcion = @IdReglaRecepcion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", pBeReglas_recepcion.IdReglaRecepcion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeReglas_recepcion, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReglaRecepcion),0) FROM Reglas_recepcion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Reglas_recepcion"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
