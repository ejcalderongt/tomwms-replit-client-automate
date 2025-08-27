Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMenu_sistema

    Public Shared Sub Cargar(ByRef oBeMenu_sistema As clsBeMenu_sistema, ByRef dr As DataRow)
        Try
            With oBeMenu_sistema
                .IdMenu = IIf(IsDBNull(dr.Item("IdMenu")), "", dr.Item("IdMenu"))
                .Titulo = IIf(IsDBNull(dr.Item("titulo")), "", dr.Item("titulo"))
                .Nombre_lgco = IIf(IsDBNull(dr.Item("nombre_lgco")), "", dr.Item("nombre_lgco"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Padre = IIf(IsDBNull(dr.Item("padre")), "", dr.Item("padre"))
                .Solicitar_clave_autorizacion = IIf(IsDBNull(dr.Item("solicitar_clave_autorizacion")), False, dr.Item("solicitar_clave_autorizacion"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeMenu_sistema As clsBeMenu_sistema, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("menu_sistema")
            Ins.Add("idmenu", "@idmenu", DataType.Parametro)
            Ins.Add("titulo", "@titulo", DataType.Parametro)
            Ins.Add("nombre_lgco", "@nombre_lgco", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("padre", "@padre", DataType.Parametro)
            Ins.Add("solicitar_clave_autorizacion", "@solicitar_clave_autorizacion", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_sistema.IdMenu))
            cmd.Parameters.Add(New SqlParameter("@TITULO", oBeMenu_sistema.Titulo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_LGCO", oBeMenu_sistema.Nombre_lgco))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeMenu_sistema.Nivel))
            cmd.Parameters.Add(New SqlParameter("@PADRE", oBeMenu_sistema.Padre))
            cmd.Parameters.Add(New SqlParameter("@SOLICITAR_CLAVE_AUTORIZACION", oBeMenu_sistema.Solicitar_clave_autorizacion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMenu_sistema.IdMenu = CStr(cmd.Parameters("@IDMENU").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeMenu_sistema As clsBeMenu_sistema, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("menu_sistema")
            Upd.Add("idmenu", "@idmenu", DataType.Parametro)
            Upd.Add("titulo", "@titulo", DataType.Parametro)
            Upd.Add("nombre_lgco", "@nombre_lgco", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("padre", "@padre", DataType.Parametro)
            Upd.Add("solicitar_clave_autorizacion", "@solicitar_clave_autorizacion", DataType.Parametro)
            Upd.Where("IdMenu = @IdMenu")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_sistema.IdMenu))
            cmd.Parameters.Add(New SqlParameter("@TITULO", oBeMenu_sistema.Titulo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_LGCO", oBeMenu_sistema.Nombre_lgco))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeMenu_sistema.Nivel))
            cmd.Parameters.Add(New SqlParameter("@PADRE", oBeMenu_sistema.Padre))
            cmd.Parameters.Add(New SqlParameter("@SOLICITAR_CLAVE_AUTORIZACION", oBeMenu_sistema.Solicitar_clave_autorizacion))


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

    Public Shared Function Eliminar(ByRef oBeMenu_sistema As clsBeMenu_sistema, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Menu_sistema" &
             "  Where(IdMenu = @IdMenu)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_sistema.IdMenu))


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

            Const sp As String = " Delete from Menu_sistema"
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

            Const sp As String = "SELECT * FROM Menu_sistema"
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

    Public Shared Function Obtener(ByRef oBeMenu_sistema As clsBeMenu_sistema) As Boolean

        Try

            Const sp As String = "SELECT * FROM Menu_sistema" &
            " Where(IdMenu = @IdMenu)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_sistema.IdMenu))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMenu_sistema, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMenu_sistema)

        Try

            Dim lReturnList As New List(Of clsBeMenu_sistema)
            Const sp As String = "SELECT * FROM Menu_sistema"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenu_sistema As New clsBeMenu_sistema

            For Each dr As DataRow In dt.Rows
                vBeMenu_sistema = New clsBeMenu_sistema
                Cargar(vBeMenu_sistema, dr)
                lReturnList.Add(vBeMenu_sistema)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMenu_sistema As clsBeMenu_sistema)

        Try

            Const sp As String = "SELECT * FROM Menu_sistema" &
            " Where(IdMenu = @IdMenu)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENU", pBeMenu_sistema.IdMenu))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMenu_sistema, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdMenu),0) FROM Menu_sistema"

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
