Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnEmpresa_transporte_pilotos

    Public Shared Sub Cargar(ByRef oBeEmpresa_transporte_pilotos As clsBeEmpresa_transporte_pilotos, ByRef dr As DataRow)
        Try
            With oBeEmpresa_transporte_pilotos
                .IdPiloto = IIf(IsDBNull(dr.Item("IdPiloto")), 0, dr.Item("IdPiloto"))
                .IdEmpresaTransporte = IIf(IsDBNull(dr.Item("IdEmpresaTransporte")), 0, dr.Item("IdEmpresaTransporte"))
                .Nombres = IIf(IsDBNull(dr.Item("nombres")), "", dr.Item("nombres"))
                .Apellidos = IIf(IsDBNull(dr.Item("apellidos")), "", dr.Item("apellidos"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Correo_electronico = IIf(IsDBNull(dr.Item("correo_electronico")), "", dr.Item("correo_electronico"))
                .No_carnet = IIf(IsDBNull(dr.Item("no_carnet")), "", dr.Item("no_carnet"))
                .Fecha_expiracion_carnet = IIf(IsDBNull(dr.Item("fecha_expiracion_carnet")), Date.Now, dr.Item("fecha_expiracion_carnet"))
                .No_dpi = IIf(IsDBNull(dr.Item("no_dpi")), "", dr.Item("no_dpi"))
                .No_Licencia = IIf(IsDBNull(dr.Item("no_licencia")), "", dr.Item("no_licencia"))
                .Fecha_expiracion_licencia = IIf(IsDBNull(dr.Item("fecha_expiracion_licencia")), Date.Now, dr.Item("fecha_expiracion_licencia"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Foto = IIf(IsDBNull(dr.Item("foto")), Nothing, dr.Item("foto"))
                .Fecha_nacimiento = IIf(IsDBNull(dr.Item("fecha_nacimiento")), Date.Now, dr.Item("fecha_nacimiento"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_salida = IIf(IsDBNull(dr.Item("fecha_salida")), Date.Now, dr.Item("fecha_salida"))
                .IdTipoLicencia = IIf(IsDBNull(dr.Item("IdTipoLicencia")), "", dr.Item("IdTipoLicencia"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeEmpresa_transporte_pilotos As clsBeEmpresa_transporte_pilotos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("empresa_transporte_pilotos")
            Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Ins.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Ins.Add("nombres", "@nombres", DataType.Parametro)
            Ins.Add("apellidos", "@apellidos", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Ins.Add("no_carnet", "@no_carnet", DataType.Parametro)
            Ins.Add("fecha_expiracion_carnet", "@fecha_expiracion_carnet", DataType.Parametro)
            Ins.Add("no_dpi", "@no_dpi", DataType.Parametro)
            Ins.Add("no_licencia", "@no_licencia", DataType.Parametro)
            Ins.Add("fecha_expiracion_licencia", "@fecha_expiracion_licencia", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("foto", "@foto", DataType.Parametro)
            Ins.Add("fecha_nacimiento", "@fecha_nacimiento", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_salida", "@fecha_salida", DataType.Parametro)
            Ins.Add("idtipolicencia", "@idtipolicencia", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeEmpresa_transporte_pilotos.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeEmpresa_transporte_pilotos.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeEmpresa_transporte_pilotos.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeEmpresa_transporte_pilotos.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeEmpresa_transporte_pilotos.Telefono))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBeEmpresa_transporte_pilotos.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@NO_CARNET", oBeEmpresa_transporte_pilotos.No_carnet))
            cmd.Parameters.Add(New SqlParameter("@FECHA_EXPIRACION_CARNET", oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet))
            cmd.Parameters.Add(New SqlParameter("@NO_DPI", oBeEmpresa_transporte_pilotos.No_dpi))
            cmd.Parameters.Add(New SqlParameter("@NO_LICENCIA", oBeEmpresa_transporte_pilotos.No_Licencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_EXPIRACION_LICENCIA", oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeEmpresa_transporte_pilotos.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeEmpresa_transporte_pilotos.Direccion))

            If oBeEmpresa_transporte_pilotos.Foto IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@FOTO", oBeEmpresa_transporte_pilotos.Foto))
            Else
                cmd.Parameters.Add(New SqlParameter("@FOTO", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_NACIMIENTO", oBeEmpresa_transporte_pilotos.Fecha_nacimiento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeEmpresa_transporte_pilotos.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SALIDA", oBeEmpresa_transporte_pilotos.Fecha_salida))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOLICENCIA", oBeEmpresa_transporte_pilotos.IdTipoLicencia))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa_transporte_pilotos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa_transporte_pilotos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa_transporte_pilotos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa_transporte_pilotos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa_transporte_pilotos.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeEmpresa_transporte_pilotos As clsBeEmpresa_transporte_pilotos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("empresa_transporte_pilotos")
            Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Upd.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Upd.Add("nombres", "@nombres", DataType.Parametro)
            Upd.Add("apellidos", "@apellidos", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Upd.Add("no_carnet", "@no_carnet", DataType.Parametro)
            Upd.Add("fecha_expiracion_carnet", "@fecha_expiracion_carnet", DataType.Parametro)
            Upd.Add("no_dpi", "@no_dpi", DataType.Parametro)
            Upd.Add("no_licencia", "@no_licencia", DataType.Parametro)
            Upd.Add("fecha_expiracion_licencia", "@fecha_expiracion_licencia", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("foto", "@foto", DataType.Parametro)
            Upd.Add("fecha_nacimiento", "@fecha_nacimiento", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_salida", "@fecha_salida", DataType.Parametro)
            Upd.Add("idtipolicencia", "@idtipolicencia", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdPiloto = @IdPiloto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeEmpresa_transporte_pilotos.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeEmpresa_transporte_pilotos.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeEmpresa_transporte_pilotos.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeEmpresa_transporte_pilotos.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeEmpresa_transporte_pilotos.Telefono))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBeEmpresa_transporte_pilotos.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@NO_CARNET", oBeEmpresa_transporte_pilotos.No_carnet))
            cmd.Parameters.Add(New SqlParameter("@FECHA_EXPIRACION_CARNET", oBeEmpresa_transporte_pilotos.Fecha_expiracion_carnet))
            cmd.Parameters.Add(New SqlParameter("@NO_DPI", oBeEmpresa_transporte_pilotos.No_dpi))
            cmd.Parameters.Add(New SqlParameter("@NO_LICENCIA", oBeEmpresa_transporte_pilotos.No_Licencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_EXPIRACION_LICENCIA", oBeEmpresa_transporte_pilotos.Fecha_expiracion_licencia))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeEmpresa_transporte_pilotos.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeEmpresa_transporte_pilotos.Direccion))

            If oBeEmpresa_transporte_pilotos.Foto IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@FOTO", oBeEmpresa_transporte_pilotos.Foto))
            Else
                cmd.Parameters.Add(New SqlParameter("@FOTO", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_NACIMIENTO", oBeEmpresa_transporte_pilotos.Fecha_nacimiento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeEmpresa_transporte_pilotos.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SALIDA", oBeEmpresa_transporte_pilotos.Fecha_salida))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOLICENCIA", oBeEmpresa_transporte_pilotos.IdTipoLicencia))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa_transporte_pilotos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa_transporte_pilotos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa_transporte_pilotos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa_transporte_pilotos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa_transporte_pilotos.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeEmpresa_transporte_pilotos As clsBeEmpresa_transporte_pilotos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Empresa_transporte_pilotos" &
             "  Where(IdPiloto = @IdPiloto)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeEmpresa_transporte_pilotos.IdPiloto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Empresa_transporte_pilotos"
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

    Public Shared Function Obtener(ByRef oBeEmpresa_transporte_pilotos As clsBeEmpresa_transporte_pilotos) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_pilotos" &
            " Where(IdPiloto = @IdPiloto)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPILOTO", oBeEmpresa_transporte_pilotos.IdPiloto))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_pilotos, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_No_Documento(ByVal pNoDocumento As String) As clsBeEmpresa_transporte_pilotos

        Get_By_No_Documento = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim oBeEmpresa_transporte_pilotos As New clsBeEmpresa_transporte_pilotos()

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_pilotos" &
            " Where(no_dpi = @no_documento OR no_licencia = @no_documento)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@no_documento", pNoDocumento))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_pilotos, dt.Rows(0))
                oBeEmpresa_transporte_pilotos.IsNew = False
            End If

            lTransaction.Commit()

            Get_By_No_Documento = oBeEmpresa_transporte_pilotos

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_By_No_Documento(ByVal pNoDocumento As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeEmpresa_transporte_pilotos

        Get_By_No_Documento = Nothing

        Dim oBeEmpresa_transporte_pilotos As New clsBeEmpresa_transporte_pilotos()

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_pilotos " &
            " Where(no_dpi = @no_documento OR no_licencia = @no_documento)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@no_documento", pNoDocumento))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_pilotos, dt.Rows(0))
                oBeEmpresa_transporte_pilotos.IsNew = False
            End If

            Get_By_No_Documento = oBeEmpresa_transporte_pilotos

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_IdPiloto(ByVal pIdPiloto As Integer) As clsBeEmpresa_transporte_pilotos

        Get_By_IdPiloto = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim oBeEmpresa_transporte_pilotos As New clsBeEmpresa_transporte_pilotos()

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_pilotos" &
            " Where(IdPiloto= @IdPiloto)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPiloto", pIdPiloto))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_pilotos, dt.Rows(0))
                oBeEmpresa_transporte_pilotos.IsNew = False
            End If

            lTransaction.Commit()

            Get_By_IdPiloto = oBeEmpresa_transporte_pilotos

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

End Class
