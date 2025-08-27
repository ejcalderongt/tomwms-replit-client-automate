Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnUsuario

    Public Shared Sub Cargar(ByRef oBeUsuario As clsBeUsuario, ByRef dr As DataRow)
        Try
            With oBeUsuario
                .IdUsuario = IIf(IsDBNull(dr.Item("IdUsuario")), 0, dr.Item("IdUsuario"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombres = IIf(IsDBNull(dr.Item("nombres")), "", dr.Item("nombres"))
                .Apellidos = IIf(IsDBNull(dr.Item("apellidos")), "", dr.Item("apellidos"))
                .Cedula = IIf(IsDBNull(dr.Item("cedula")), "", dr.Item("cedula"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Clave = IIf(IsDBNull(dr.Item("clave")), "", dr.Item("clave"))
                .Ultimo_login = IIf(IsDBNull(dr.Item("ultimo_login")), Date.Now, dr.Item("ultimo_login"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Foto = IIf(IsDBNull(dr.Item("foto")), Nothing, dr.Item("foto"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Clave_autorizacion = IIf(IsDBNull(dr.Item("clave_autorizacion")), "", dr.Item("clave_autorizacion"))
                .Usuario_sap_b1 = IIf(IsDBNull(dr.Item("usuario_sap_b1")), "", dr.Item("usuario_sap_b1"))
                .Clave_sap_b1 = IIf(IsDBNull(dr.Item("clave_sap_b1")), "", dr.Item("clave_sap_b1"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeUsuario As clsBeUsuario, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("usuario")
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombres", "@nombres", DataType.Parametro)
            Ins.Add("apellidos", "@apellidos", DataType.Parametro)
            Ins.Add("cedula", "@cedula", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("clave", "@clave", DataType.Parametro)
            Ins.Add("ultimo_login", "@ultimo_login", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If Not oBeUsuario.Foto Is Nothing Then Ins.Add("foto", "@foto", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("clave_autorizacion", "@clave_autorizacion", DataType.Parametro)
            '#GT10072025: campos para usuario clave SAP
            Ins.Add("usuario_sap_b1", "@usuario_sap_b1", DataType.Parametro)
            Ins.Add("clave_sap_b1", "@clave_sap_b1", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeUsuario.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeUsuario.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@CEDULA", oBeUsuario.Cedula))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeUsuario.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeUsuario.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeUsuario.Email))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeUsuario.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeUsuario.Clave))
            cmd.Parameters.Add(New SqlParameter("@ULTIMO_LOGIN", oBeUsuario.Ultimo_login))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeUsuario.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeUsuario.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeUsuario.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeUsuario.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeUsuario.Fec_mod))
            If Not oBeUsuario.Foto Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FOTO", oBeUsuario.Foto))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeUsuario.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_AUTORIZACION", oBeUsuario.Clave_autorizacion))
            '#GT10072025: campos para usuario clave SAP
            cmd.Parameters.Add(New SqlParameter("@USUARIO_SAP_B1", oBeUsuario.Usuario_sap_b1))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_SAP_B1", oBeUsuario.Clave_sap_b1))


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

    Public Shared Function Actualizar(ByRef oBeUsuario As clsBeUsuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("usuario")
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombres", "@nombres", DataType.Parametro)
            Upd.Add("apellidos", "@apellidos", DataType.Parametro)
            Upd.Add("cedula", "@cedula", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("clave", "@clave", DataType.Parametro)
            Upd.Add("ultimo_login", "@ultimo_login", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If Not oBeUsuario.Foto Is Nothing Then Upd.Add("foto", "@foto", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("clave_autorizacion", "@clave_autorizacion", DataType.Parametro)
            '#GT10072025: campos para usuario clave SAP
            Upd.Add("usuario_sap_b1", "@usuario_sap_b1", DataType.Parametro)
            Upd.Add("clave_sap_b1", "@clave_sap_b1", DataType.Parametro)


            Upd.Where("IdUsuario = @IdUsuario")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeUsuario.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeUsuario.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@CEDULA", oBeUsuario.Cedula))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeUsuario.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeUsuario.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeUsuario.Email))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeUsuario.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeUsuario.Clave))
            cmd.Parameters.Add(New SqlParameter("@ULTIMO_LOGIN", oBeUsuario.Ultimo_login))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeUsuario.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeUsuario.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeUsuario.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeUsuario.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeUsuario.Fec_mod))
            If Not oBeUsuario.Foto Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FOTO", oBeUsuario.Foto))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeUsuario.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_AUTORIZACION", oBeUsuario.Clave_autorizacion))
            '#GT10072025: campos para usuario clave SAP
            cmd.Parameters.Add(New SqlParameter("@USUARIO_SAP_B1", oBeUsuario.Usuario_sap_b1))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_SAP_B1", oBeUsuario.Clave_sap_b1))

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

    Public Shared Function Eliminar(ByRef oBeUsuario As clsBeUsuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Usuario" &
             "  Where(IdUsuario = @IdUsuario)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario.IdUsuario))

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

            Const sp As String = " Delete from Usuario"
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

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Usuario"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function Obtener(ByRef oBeUsuario As clsBeUsuario) As Boolean

        Try

            Const sp As String = "SELECT * FROM Usuario" &
            " Where(IdUsuario = @IdUsuario)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario.IdUsuario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeUsuario, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
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

    Public Shared Function GetAll() As List(Of clsBeUsuario)

        Try

            Dim lReturnList As New List(Of clsBeUsuario)
            Const sp As String = "SELECT * FROM Usuario"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUsuario As New clsBeUsuario

            For Each dr As DataRow In dt.Rows

                vBeUsuario = New clsBeUsuario
                Cargar(vBeUsuario, dr)
                lReturnList.Add(vBeUsuario)

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

    Public Shared Function GetSingle(ByRef pBeUsuario As clsBeUsuario)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Usuario" &
            " Where(IdUsuario = @IdUsuario)"

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUSUARIO", pBeUsuario.IdUsuario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeUsuario, dt.Rows(0))
            End If

            lTransaction.Commit()

            Return True

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            clsLnLog_error_wms.Agregar_Error(vMsgError)

            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()

            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeUsuario As clsBeUsuario,
                                     ByRef pConnection As SqlConnection,
                                     ByRef pTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Usuario 
                                  Where(IdUsuario = @IdUsuario)"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUSUARIO", pBeUsuario.IdUsuario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeUsuario, dt.Rows(0))
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

    Public Shared Function GetSingle(ByVal pIdUsuario As Integer) As clsBeUsuario

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Usuario 
                                  Where(IdUsuario = @IdUsuario) "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUSUARIO", pIdUsuario))

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            If dt.Rows.Count = 1 Then
                Dim pBeUsuario As New clsBeUsuario
                Cargar(pBeUsuario, dt.Rows(0))
                Return pBeUsuario
            End If

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdUsuario As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeUsuario
        Try
            If lConnection Is Nothing OrElse lConnection.State <> ConnectionState.Open Then
                Throw New Exception("La conexión a la base de datos no está abierta.")
            End If

            Const sp As String = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdUsuario", pIdUsuario))

            Dim dt As New DataTable
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeUsuario As New clsBeUsuario
                Cargar(pBeUsuario, dt.Rows(0))
                Return pBeUsuario
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdUsuario),0) FROM Usuario"

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

End Class
