Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ejecucion_enc

    Public Shared Sub Cargar(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, ByRef dr As DataRow)
        Try
            With oBeI_nav_ejecucion_enc
                .IdEjecucionEnc = IIf(IsDBNull(dr.Item("idejecucionenc")), 0, dr.Item("idejecucionenc"))
                .IdNavConfigEnc = IIf(IsDBNull(dr.Item("idnavconfigenc")), 0, dr.Item("idnavconfigenc"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
                .Exitosa = IIf(IsDBNull(dr.Item("exitosa")), False, dr.Item("exitosa"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, _
                                    Optional ByVal pConection as SqlConnection = Nothing, _
                                    Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ejecucion_enc")
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim sp As String = Ins.SQLIdentity("idejecucionenc")
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeI_nav_ejecucion_enc.IdEjecucionEnc = newId

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return newId

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ejecucion_enc")
            Upd.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Upd.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("exitosa", "@exitosa", DataType.Parametro)
            Upd.Where("idejecucionenc = @idejecucionenc")

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

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_enc.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_ejecucion_enc.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_ejecucion_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_enc.Exitosa))

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


    Public Shared Function Eliminar(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ejecucion_enc" &
             "  Where(idejecucionenc = @idejecucionenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_enc.IdEjecucionEnc))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ejecucion_enc"
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

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                dad.Fill(dt)
                Return dt
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_enc" & _
            " Where(idejecucionenc = @idejecucionenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_enc.IdEjecucionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ejecucion_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_ejecucion_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ejecucion_enc)
            Const sp As String = "SELECT * FROM I_nav_ejecucion_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ejecucion_enc As New clsBeI_nav_ejecucion_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ejecucion_enc = New clsBeI_nav_ejecucion_enc
                Cargar(vBeI_nav_ejecucion_enc, dr)
                lReturnList.Add(vBeI_nav_ejecucion_enc)

            Next

            Return lReturnList



        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_ejecucion_enc As clsBeI_nav_ejecucion_enc)

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_enc" & _
            " Where(idejecucionenc = @idejecucionenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", pBeI_nav_ejecucion_enc.IdEjecucionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ejecucion_enc, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
