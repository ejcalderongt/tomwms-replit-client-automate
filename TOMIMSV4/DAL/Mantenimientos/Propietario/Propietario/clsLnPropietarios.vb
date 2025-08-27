Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnPropietarios

    Public Shared lPropietariosInMemory As New List(Of clsBePropietarios)

    Public Shared Sub Cargar(ByRef oBePropietarios As clsBePropietarios, ByRef dr As DataRow)

        Try

            With oBePropietarios

                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdTipoActualizacionCosto = IIf(IsDBNull(dr.Item("IdTipoActualizacionCosto")), 0, dr.Item("IdTipoActualizacionCosto"))
                .Contacto = IIf(IsDBNull(dr.Item("contacto")), "", dr.Item("contacto"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .NIT = IIf(IsDBNull(dr.Item("NIT")), "", dr.Item("NIT"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Actualiza_costo_oc = IIf(IsDBNull(dr.Item("actualiza_costo_oc")), False, dr.Item("actualiza_costo_oc"))
                .Color = IIf(IsDBNull(dr.Item("color")), 0, dr.Item("color"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Sistema = IIf(IsDBNull(dr.Item("Sistema")), False, dr.Item("Sistema"))
                .Es_Consolidador = IIf(IsDBNull(dr.Item("es_consolidador")), False, dr.Item("es_consolidador"))
                .ControlUx = IIf(IsDBNull(dr.Item("controlux")), False, dr.Item("controlux"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub CargarSinImagen(ByRef oBePropietarios As clsBePropietarios, ByRef dr As DataRow)

        Try

            With oBePropietarios

                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdTipoActualizacionCosto = IIf(IsDBNull(dr.Item("IdTipoActualizacionCosto")), 0, dr.Item("IdTipoActualizacionCosto"))
                .Contacto = IIf(IsDBNull(dr.Item("contacto")), "", dr.Item("contacto"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .NIT = IIf(IsDBNull(dr.Item("NIT")), "", dr.Item("NIT"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Actualiza_costo_oc = IIf(IsDBNull(dr.Item("actualiza_costo_oc")), False, dr.Item("actualiza_costo_oc"))
                .Color = IIf(IsDBNull(dr.Item("color")), 0, dr.Item("color"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Sistema = IIf(IsDBNull(dr.Item("Sistema")), False, dr.Item("Sistema"))
                .Es_Consolidador = IIf(IsDBNull(dr.Item("es_consolidador")), False, dr.Item("es_consolidador"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBePropietarios As clsBePropietarios, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            Ins.Init("propietarios")
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If Not oBePropietarios.IdTipoActualizacionCosto = 0 Then Ins.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", DataType.Parametro)
            Ins.Add("contacto", "@contacto", DataType.Parametro)
            Ins.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            If Not oBePropietarios.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", DataType.Parametro)
            Ins.Add("color", "@color", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("es_consolidador", "@es_consolidador", DataType.Parametro)
            Ins.Add("controlux", "@controlux", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBePropietarios.IdEmpresa))
            If Not oBePropietarios.IdTipoActualizacionCosto = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBePropietarios.IdTipoActualizacionCosto))
            cmd.Parameters.Add(New SqlParameter("@CONTACTO", oBePropietarios.Contacto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", clsPublic.Quitar_Caracteres_No_Permitidos(oBePropietarios.Nombre_comercial)))
            If Not oBePropietarios.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBePropietarios.Imagen))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBePropietarios.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBePropietarios.NIT))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBePropietarios.Direccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietarios.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietarios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietarios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietarios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietarios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBePropietarios.Email))
            cmd.Parameters.Add(New SqlParameter("@ACTUALIZA_COSTO_OC", oBePropietarios.Actualiza_costo_oc))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBePropietarios.Color))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBePropietarios.Codigo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBePropietarios.Sistema))
            cmd.Parameters.Add(New SqlParameter("@ES_CONSOLIDADOR", oBePropietarios.Es_Consolidador))
            cmd.Parameters.Add(New SqlParameter("@CONTROLUX", oBePropietarios.ControlUx))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            '#CKFK 20210312 Aquí agregué la condición de que solo haga rollback y cierre la conexion si la transacción no es remota
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

    Public Shared Function Actualizar(ByRef oBePropietarios As clsBePropietarios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("propietarios")
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            If oBePropietarios.IdTipoActualizacionCosto <> 0 Then
                Upd.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", DataType.Parametro)
            End If

            Upd.Add("contacto", "@contacto", DataType.Parametro)
            Upd.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            If Not oBePropietarios.Imagen Is Nothing Then Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("actualiza_costo_oc", "@actualiza_costo_oc", DataType.Parametro)
            Upd.Add("color", "@color", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("es_consolidador", "@es_consolidador", DataType.Parametro)
            Upd.Add("controlux", "@controlux", DataType.Parametro)
            Upd.Where("IdPropietario = @IdPropietario")


            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBePropietarios.IdEmpresa))
            If oBePropietarios.IdTipoActualizacionCosto <> 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBePropietarios.IdTipoActualizacionCosto))
            End If
            cmd.Parameters.Add(New SqlParameter("@CONTACTO", oBePropietarios.Contacto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", clsPublic.Quitar_Caracteres_No_Permitidos(oBePropietarios.Nombre_comercial)))
            If Not oBePropietarios.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBePropietarios.Imagen))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBePropietarios.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBePropietarios.NIT))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBePropietarios.Direccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietarios.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietarios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietarios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietarios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietarios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBePropietarios.Email))
            cmd.Parameters.Add(New SqlParameter("@ACTUALIZA_COSTO_OC", oBePropietarios.Actualiza_costo_oc))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBePropietarios.Color))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBePropietarios.Codigo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBePropietarios.Sistema))
            cmd.Parameters.Add(New SqlParameter("@ES_CONSOLIDADOR", oBePropietarios.Es_Consolidador))
            cmd.Parameters.Add(New SqlParameter("@CONTROLUX", oBePropietarios.ControlUx))

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


    Public Shared Function Eliminar(ByRef oBePropietarios As clsBePropietarios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Propietarios" &
             "  Where(IdPropietario = @IdPropietario)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))

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

            Const sp As String = " Delete from Propietarios"
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

            Const sp As String = "SELECT * FROM Propietarios"
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

    Public Shared Function Obtener(ByRef oBePropietarios As clsBePropietarios) As Boolean

        Try

            Const sp As String = "SELECT * FROM Propietarios" &
            " Where(IdPropietario = @IdPropietario)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBePropietarios, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBePropietarios)

        Try

            Dim lReturnList As New List(Of clsBePropietarios)
            Const sp As String = "SELECT * FROM Propietarios"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePropietarios As New clsBePropietarios

            For Each dr As DataRow In dt.Rows

                vBePropietarios = New clsBePropietarios
                Cargar(vBePropietarios, dr)
                lReturnList.Add(vBePropietarios)

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

    Public Shared Function GetSingle(ByRef pBePropietarios As clsBePropietarios)

        Try

            Const sp As String = "SELECT * FROM Propietarios" &
            " Where(IdPropietario = @IdPropietario)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", pBePropietarios.IdPropietario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBePropietarios, dt.Rows(0))
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

    Public Shared Function Get_Single_By_NIT(ByVal pNIT As String) As clsBePropietarios

        Get_Single_By_NIT = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Propietarios" &
            " Where(NIT = @NIT)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NIT", pNIT))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBePropietarios As New clsBePropietarios()
                Cargar(pBePropietarios, dt.Rows(0))
                Get_Single_By_NIT = pBePropietarios
            End If

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String) As clsBePropietarios

        Get_Single_By_Nombre = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Propietarios " &
            " Where(nombre_comercial like '%" & pNombre & "%')"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBePropietarios As New clsBePropietarios()
                Cargar(pBePropietarios, dt.Rows(0))
                Get_Single_By_Nombre = pBePropietarios
            End If

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBePropietarios

        Get_Single_By_Nombre = Nothing

        Try

            Dim sp As String = "SELECT * FROM Propietarios " &
            " Where(nombre_comercial like '%" & pNombre & "%')"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBePropietarios As New clsBePropietarios()
                Cargar(pBePropietarios, dt.Rows(0))
                Get_Single_By_Nombre = pBePropietarios
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_NIT(ByVal pNIT As String,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As clsBePropietarios

        Get_Single_By_NIT = Nothing

        Try

            Const sp As String = "SELECT * FROM Propietarios" &
            " Where(NIT = @NIT)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NIT", pNIT))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBePropietarios As New clsBePropietarios()
                Cargar(pBePropietarios, dt.Rows(0))
                Get_Single_By_NIT = pBePropietarios
            End If

        Catch ex1 As SqlException
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

            Const sp As String = "SELECT ISNULL(Max(IdPropietario),0) FROM Propietarios"

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

    Public Shared Function MaxID(lConnection As SqlConnection,
                                 lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPropietario),0) FROM Propietarios"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If
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

End Class
