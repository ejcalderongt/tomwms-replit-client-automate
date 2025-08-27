Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMenu_rol_op

    Public Shared Sub Cargar(ByRef oBeMenu_rol_op As clsBeMenu_rol_op, ByRef dr As DataRow)
        Try
            With oBeMenu_rol_op
                .IdMenuSistemaOP = IIf(IsDBNull(dr.Item("IdMenuSistemaOP")), "", dr.Item("IdMenuSistemaOP"))
                .IdRolOperador = IIf(IsDBNull(dr.Item("IdRolOperador")), 0, dr.Item("IdRolOperador"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Visible = IIf(IsDBNull(dr.Item("visible")), False, dr.Item("visible"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw New Exception("Menu_rol_op_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeMenu_rol_op As clsBeMenu_rol_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("menu_rol_op")
            Ins.Add("idmenusistemaop", "@idmenusistemaop", DataType.Parametro)
            Ins.Add("idroloperador", "@idroloperador", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("visible", "@visible", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_rol_op.IdMenuSistemaOP))
            cmd.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeMenu_rol_op.IdRolOperador))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMenu_rol_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMenu_rol_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMenu_rol_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMenu_rol_op.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@VISIBLE", oBeMenu_rol_op.Visible))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMenu_rol_op.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Menu_rol_op_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeMenu_rol_op As clsBeMenu_rol_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("menu_rol_op")
            Upd.Add("idmenusistemaop", "@idmenusistemaop", DataType.Parametro)
            Upd.Add("idroloperador", "@idroloperador", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("visible", "@visible", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdMenuSistemaOP = @IdMenuSistemaOP" &
                " AND IdRolOperador = @IdRolOperador")

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

            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_rol_op.IdMenuSistemaOP))
            cmd.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeMenu_rol_op.IdRolOperador))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMenu_rol_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMenu_rol_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMenu_rol_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMenu_rol_op.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@VISIBLE", oBeMenu_rol_op.Visible))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMenu_rol_op.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Menu_rol_op_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeMenu_rol_op As clsBeMenu_rol_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Menu_rol_op" &
             "  Where(IdMenuSistemaOP = @IdMenuSistemaOP)" &
             "  AND (IdRolOperador = @IdRolOperador)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_rol_op.IdMenuSistemaOP))
            cmd.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeMenu_rol_op.IdRolOperador))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Menu_rol_op_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Menu_rol_op"
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
            Throw New Exception("Menu_rol_op_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Menu_rol_op"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Menu_rol_op_Listar: " & ex.Message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeMenu_rol_op As clsBeMenu_rol_op) As Boolean

        Try

            Const sp As String = "SELECT * FROM Menu_rol_op" &
            " Where(IdMenuSistemaOP = @IdMenuSistemaOP)" &
            " AND (IdRolOperador = @IdRolOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_rol_op.IdMenuSistemaOP))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeMenu_rol_op.IdRolOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMenu_rol_op, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMenu_rol_op)

        Try

            Dim lReturnList As New List(Of clsBeMenu_rol_op)
            Const sp As String = "SELECT * FROM Menu_rol_op"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenu_rol_op As New clsBeMenu_rol_op

            For Each dr As DataRow In dt.Rows

                vBeMenu_rol_op = New clsBeMenu_rol_op
                Cargar(vBeMenu_rol_op, dr)
                lReturnList.Add(vBeMenu_rol_op)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Menu_rol_op_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMenu_rol_op As clsBeMenu_rol_op)

        Try

            Const sp As String = "SELECT * FROM Menu_rol_op" &
            " Where(IdMenuSistemaOP = @IdMenuSistemaOP)" &
            " AND (IdRolOperador = @IdRolOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", pBeMenu_rol_op.IdMenuSistemaOP))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDROLOPERADOR", pBeMenu_rol_op.IdRolOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMenu_rol_op, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
