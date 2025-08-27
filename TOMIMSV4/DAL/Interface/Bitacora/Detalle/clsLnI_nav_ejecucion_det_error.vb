Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ejecucion_det_error

    Public Shared Sub Cargar(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error, ByRef dr As DataRow)
        Try
            With oBeI_nav_ejecucion_det_error
                .Idejecuciondet = IIf(IsDBNull(dr.Item("idejecuciondet")), 0, dr.Item("idejecuciondet"))
                .Idejecucionenc = IIf(IsDBNull(dr.Item("idejecucionenc")), 0, dr.Item("idejecucionenc"))
                .Idnavconfigdet = IIf(IsDBNull(dr.Item("idnavconfigdet")), 0, dr.Item("idnavconfigdet"))
                .vError = IIf(IsDBNull(dr.Item("error")), "", dr.Item("error"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ejecucion_det_error")
            Ins.Add("idejecuciondet", "@idejecuciondet", DataType.Parametro)
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("error", "@error", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_det_error.Idejecucionenc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_det_error.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@ERROR", oBeI_nav_ejecucion_det_error.vError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeI_nav_ejecucion_det_error.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_det_error.Fecha))

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

    Public Shared Function Actualizar(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ejecucion_det_error")
            Upd.Add("idejecuciondet", "@idejecuciondet", DataType.Parametro)
            Upd.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Upd.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Upd.Add("error", "@error", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Where("idejecuciondet = @idejecuciondet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_det_error.Idejecucionenc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_det_error.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@ERROR", oBeI_nav_ejecucion_det_error.vError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeI_nav_ejecucion_det_error.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_det_error.Fecha))

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

    Public Shared Function Eliminar(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ejecucion_det_error" &
             "  Where(idejecuciondet = @idejecuciondet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ejecucion_det_error"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_det_error"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_det_error" &
            " Where(idejecuciondet = @idejecuciondet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", oBeI_nav_ejecucion_det_error.Idejecuciondet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ejecucion_det_error, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_ejecucion_det_error)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ejecucion_det_error)
            Const sp As String = "SELECT * FROM I_nav_ejecucion_det_error"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ejecucion_det_error As New clsBeI_nav_ejecucion_det_error

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ejecucion_det_error = New clsBeI_nav_ejecucion_det_error
                Cargar(vBeI_nav_ejecucion_det_error, dr)
                lReturnList.Add(vBeI_nav_ejecucion_det_error)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_ejecucion_det_error As clsBeI_nav_ejecucion_det_error)

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_det_error" &
            " Where(idejecuciondet = @idejecuciondet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONDET", pBeI_nav_ejecucion_det_error.Idejecuciondet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ejecucion_det_error, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idejecuciondet),0) FROM I_nav_ejecucion_det_error"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class