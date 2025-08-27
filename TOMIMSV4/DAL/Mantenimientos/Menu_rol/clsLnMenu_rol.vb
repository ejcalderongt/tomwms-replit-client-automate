Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMenu_rol

    Public Shared Sub Cargar(ByRef oBeMenu_rol As clsBeMenu_rol, ByRef dr As DataRow)
        Try
            With oBeMenu_rol
                .IdMenuRol = IIf(IsDBNull(dr.Item("IdMenuRol")), 0, dr.Item("IdMenuRol"))
                .IdMenu = IIf(IsDBNull(dr.Item("IdMenu")), "", dr.Item("IdMenu"))
                .IdRol = IIf(IsDBNull(dr.Item("IdRol")), 0, dr.Item("IdRol"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Visible = IIf(IsDBNull(dr.Item("visible")), False, dr.Item("visible"))
                .Leer = IIf(IsDBNull(dr.Item("Leer")), False, dr.Item("Leer"))
                .Modificar = IIf(IsDBNull(dr.Item("Modificar")), False, dr.Item("Modificar"))
                .Eliminar = IIf(IsDBNull(dr.Item("Eliminar")), False, dr.Item("Eliminar"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeMenu_rol As clsBeMenu_rol, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("menu_rol")
            Ins.Add("idmenurol", "@idmenurol", DataType.Parametro)
            Ins.Add("idmenu", "@idmenu", DataType.Parametro)
            Ins.Add("idrol", "@idrol", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("visible", "@visible", DataType.Parametro)
            Ins.Add("Leer", "@Leer", DataType.Parametro)
            Ins.Add("Modificar", "@Modificar", DataType.Parametro)
            Ins.Add("Eliminar", "@Eliminar", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUROL", oBeMenu_rol.IdMenuRol))
            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_rol.IdMenu))
            cmd.Parameters.Add(New SqlParameter("@IDROL", oBeMenu_rol.IdRol))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMenu_rol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMenu_rol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMenu_rol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMenu_rol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMenu_rol.Activo))
            cmd.Parameters.Add(New SqlParameter("@VISIBLE", oBeMenu_rol.Visible))
            cmd.Parameters.Add(New SqlParameter("@LEER", oBeMenu_rol.Leer))
            cmd.Parameters.Add(New SqlParameter("@MODIFICAR", oBeMenu_rol.Modificar))
            cmd.Parameters.Add(New SqlParameter("@ELIMINAR", oBeMenu_rol.Eliminar))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMenu_rol.IdMenuRol = CInt(cmd.Parameters("@IDMENUROL").Value)

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

    Public Shared Function Actualizar(ByRef oBeMenu_rol As clsBeMenu_rol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("menu_rol")
            Upd.Add("idmenu", "@idmenu", DataType.Parametro)
            Upd.Add("idrol", "@idrol", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("visible", "@visible", DataType.Parametro)
            Upd.Add("Leer", "@Leer", DataType.Parametro)
            Upd.Add("Modificar", "@Modificar", DataType.Parametro)
            Upd.Add("Eliminar", "@Eliminar", DataType.Parametro)
            Upd.Where("idmenu = @idmenu and idRol = @IdRol ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUROL", oBeMenu_rol.IdMenuRol))
            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_rol.IdMenu))
            cmd.Parameters.Add(New SqlParameter("@IDROL", oBeMenu_rol.IdRol))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMenu_rol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMenu_rol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMenu_rol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMenu_rol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMenu_rol.Activo))
            cmd.Parameters.Add(New SqlParameter("@VISIBLE", oBeMenu_rol.Visible))
            cmd.Parameters.Add(New SqlParameter("@LEER", oBeMenu_rol.Leer))
            cmd.Parameters.Add(New SqlParameter("@MODIFICAR", oBeMenu_rol.Modificar))
            cmd.Parameters.Add(New SqlParameter("@ELIMINAR", oBeMenu_rol.Eliminar))

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

    Public Shared Function Eliminar(ByRef oBeMenu_rol As clsBeMenu_rol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Menu_rol" &
             "  Where(IdMenuRol = @IdMenuRol)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUROL", oBeMenu_rol.IdMenuRol))

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

            Const sp As String = " Delete from Menu_rol"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

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

            Const sp As String = "SELECT * FROM Menu_rol"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeMenu_rol As clsBeMenu_rol) As Boolean

        Try

            Const sp As String = "SELECT * FROM Menu_rol" & _
            " Where(IdMenuRol = @IdMenuRol)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUROL", oBeMenu_rol.IDMENUROL))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMenu_rol, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMenu_rol)

        Try

            Dim lReturnList As New List(Of clsBeMenu_rol)
            Const sp As String = "SELECT * FROM Menu_rol"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenu_rol As New clsBeMenu_rol

            For Each dr As DataRow In dt.Rows

                vBeMenu_rol = New clsBeMenu_rol
                Cargar(vBeMenu_rol, dr)
                lReturnList.Add(vBeMenu_rol)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMenu_rol As clsBeMenu_rol)

        Try

            Const sp As String = "SELECT * FROM Menu_rol" & _
            " Where(IdMenuRol = @IdMenuRol)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUROL", pBeMenu_rol.IDMENUROL))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMenu_rol, dt.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMenuRol),0) FROM Menu_rol"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Opciones(ByRef oBeMenu_rol As clsBeMenu_rol,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("menu_rol")
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("Leer", "@Leer", DataType.Parametro)
            Upd.Add("Modificar", "@Modificar", DataType.Parametro)
            Upd.Add("Eliminar", "@Eliminar", DataType.Parametro)
            Upd.Where("idmenu = @idmenu and idRol = @IdRol ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENU", oBeMenu_rol.IdMenu))
            cmd.Parameters.Add(New SqlParameter("@IDROL", oBeMenu_rol.IdRol))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMenu_rol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMenu_rol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@LEER", oBeMenu_rol.Leer))
            cmd.Parameters.Add(New SqlParameter("@MODIFICAR", oBeMenu_rol.Modificar))
            cmd.Parameters.Add(New SqlParameter("@ELIMINAR", oBeMenu_rol.Eliminar))

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

End Class
