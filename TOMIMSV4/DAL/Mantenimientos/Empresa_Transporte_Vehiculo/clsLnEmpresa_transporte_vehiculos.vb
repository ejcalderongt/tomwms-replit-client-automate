Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnEmpresa_transporte_vehiculos

    Public Shared Sub Cargar(ByRef oBeEmpresa_transporte_vehiculos As clsBeEmpresa_transporte_vehiculos, ByRef dr As DataRow)
        Try
            With oBeEmpresa_transporte_vehiculos
                .IdVehiculo = IIf(IsDBNull(dr.Item("IdVehiculo")), 0, dr.Item("IdVehiculo"))
                .IdEmpresaTransporte = IIf(IsDBNull(dr.Item("IdEmpresaTransporte")), 0, dr.Item("IdEmpresaTransporte"))
                .IdTipoContenedor = IIf(IsDBNull(dr.Item("IdTipoContenedor")), 0, dr.Item("IdTipoContenedor"))
                .Placa = IIf(IsDBNull(dr.Item("placa")), "", dr.Item("placa"))
                .Marca = IIf(IsDBNull(dr.Item("marca")), "", dr.Item("marca"))
                .Modelo = IIf(IsDBNull(dr.Item("modelo")), "", dr.Item("modelo"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Volumen = IIf(IsDBNull(dr.Item("volumen")), 0.0, dr.Item("volumen"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Tipo = IIf(IsDBNull(dr.Item("tipo")), "", dr.Item("tipo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Placa_comercial = IIf(IsDBNull(dr.Item("placa_comercial")), "", dr.Item("placa_comercial"))
                .Es_contedor = IIf(IsDBNull(dr.Item("es_contedor")), 0, dr.Item("es_contedor"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeEmpresa_transporte_vehiculos As clsBeEmpresa_transporte_vehiculos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("empresa_transporte_vehiculos")
            Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Ins.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Ins.Add("idtipocontenedor", "@idtipocontenedor", DataType.Parametro)
            Ins.Add("placa", "@placa", DataType.Parametro)
            Ins.Add("marca", "@marca", DataType.Parametro)
            Ins.Add("modelo", "@modelo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("volumen", "@volumen", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("placa_comercial", "@placa_comercial", DataType.Parametro)
            Ins.Add("es_contedor", "@es_contedor", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeEmpresa_transporte_vehiculos.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeEmpresa_transporte_vehiculos.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeEmpresa_transporte_vehiculos.IdTipoContenedor))
            cmd.Parameters.Add(New SqlParameter("@PLACA", oBeEmpresa_transporte_vehiculos.Placa))
            cmd.Parameters.Add(New SqlParameter("@MARCA", oBeEmpresa_transporte_vehiculos.Marca))
            cmd.Parameters.Add(New SqlParameter("@MODELO", oBeEmpresa_transporte_vehiculos.Modelo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeEmpresa_transporte_vehiculos.Peso))
            cmd.Parameters.Add(New SqlParameter("@VOLUMEN", oBeEmpresa_transporte_vehiculos.Volumen))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa_transporte_vehiculos.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa_transporte_vehiculos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa_transporte_vehiculos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa_transporte_vehiculos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa_transporte_vehiculos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeEmpresa_transporte_vehiculos.Tipo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEmpresa_transporte_vehiculos.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEmpresa_transporte_vehiculos.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEmpresa_transporte_vehiculos.Ancho))
            cmd.Parameters.Add(New SqlParameter("@PLACA_COMERCIAL", oBeEmpresa_transporte_vehiculos.Placa_comercial))
            cmd.Parameters.Add(New SqlParameter("@ES_CONTEDOR", oBeEmpresa_transporte_vehiculos.Es_contedor))

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

    Public Shared Function Actualizar(ByRef oBeEmpresa_transporte_vehiculos As clsBeEmpresa_transporte_vehiculos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("empresa_transporte_vehiculos")
            Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Upd.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Upd.Add("idtipocontenedor", "@idtipocontenedor", DataType.Parametro)
            Upd.Add("placa", "@placa", DataType.Parametro)
            Upd.Add("marca", "@marca", DataType.Parametro)
            Upd.Add("modelo", "@modelo", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("volumen", "@volumen", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("placa_comercial", "@placa_comercial", DataType.Parametro)
            Upd.Add("es_contedor", "@es_contedor", DataType.Parametro)
            Upd.Where("IdVehiculo = @IdVehiculo")

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

            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeEmpresa_transporte_vehiculos.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeEmpresa_transporte_vehiculos.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONTENEDOR", oBeEmpresa_transporte_vehiculos.IdTipoContenedor))
            cmd.Parameters.Add(New SqlParameter("@PLACA", oBeEmpresa_transporte_vehiculos.Placa))
            cmd.Parameters.Add(New SqlParameter("@MARCA", oBeEmpresa_transporte_vehiculos.Marca))
            cmd.Parameters.Add(New SqlParameter("@MODELO", oBeEmpresa_transporte_vehiculos.Modelo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeEmpresa_transporte_vehiculos.Peso))
            cmd.Parameters.Add(New SqlParameter("@VOLUMEN", oBeEmpresa_transporte_vehiculos.Volumen))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa_transporte_vehiculos.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa_transporte_vehiculos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa_transporte_vehiculos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa_transporte_vehiculos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa_transporte_vehiculos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeEmpresa_transporte_vehiculos.Tipo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEmpresa_transporte_vehiculos.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEmpresa_transporte_vehiculos.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEmpresa_transporte_vehiculos.Ancho))
            cmd.Parameters.Add(New SqlParameter("@PLACA_COMERCIAL", oBeEmpresa_transporte_vehiculos.Placa_comercial))
            cmd.Parameters.Add(New SqlParameter("@ES_CONTEDOR", oBeEmpresa_transporte_vehiculos.Es_contedor))

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

    Public Shared Function Eliminar(ByRef oBeEmpresa_transporte_vehiculos As clsBeEmpresa_transporte_vehiculos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Empresa_transporte_vehiculos" &
             "  Where(IdVehiculo = @IdVehiculo)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeEmpresa_transporte_vehiculos.IdVehiculo))

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

            Const sp As String = "SELECT * FROM Empresa_transporte_vehiculos"
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

    Public Shared Function Obtener(ByRef oBeEmpresa_transporte_vehiculos As clsBeEmpresa_transporte_vehiculos) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_vehiculos" &
            " Where(IdVehiculo = @IdVehiculo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeEmpresa_transporte_vehiculos.IdVehiculo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_vehiculos, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdVehiculo(ByVal pIdVehiculo As Integer) As clsBeEmpresa_transporte_vehiculos

        Get_Single_By_IdVehiculo = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim oBeEmpresa_transporte_vehiculos As New clsBeEmpresa_transporte_vehiculos()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Empresa_transporte_vehiculos" &
            " Where(IdVehiculo = @IdVehiculo)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDVEHICULO", pIdVehiculo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_vehiculos, dt.Rows(0))
            End If

            lTransaction.Commit()

            Get_Single_By_IdVehiculo = oBeEmpresa_transporte_vehiculos

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_No_Placa(ByVal pIdVehiculo As Integer) As clsBeEmpresa_transporte_vehiculos

        Get_Single_By_No_Placa = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim oBeEmpresa_transporte_vehiculos As New clsBeEmpresa_transporte_vehiculos()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Empresa_transporte_vehiculos" &
            " Where(IdVehiculo = @IdVehiculo)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDVEHICULO", pIdVehiculo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_vehiculos, dt.Rows(0))
            End If

            lTransaction.Commit()

            Get_Single_By_No_Placa = oBeEmpresa_transporte_vehiculos

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_No_Placa(ByVal pPlaca As String,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeEmpresa_transporte_vehiculos

        Get_Single_By_No_Placa = Nothing

        Dim oBeEmpresa_transporte_vehiculos As New clsBeEmpresa_transporte_vehiculos()

        Try

            Dim sp As String = "SELECT * FROM Empresa_transporte_vehiculos " &
            " Where(Placa = @Placa)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@PLACA", pPlaca))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa_transporte_vehiculos, dt.Rows(0))
                Get_Single_By_No_Placa = oBeEmpresa_transporte_vehiculos
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
