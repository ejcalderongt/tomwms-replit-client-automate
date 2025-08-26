Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_despacho_enc

    Public Shared Sub Cargar(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc, ByRef dr As DataRow)
        Try
            With oBeTrans_despacho_enc
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdVehiculo = IIf(IsDBNull(dr.Item("IdVehiculo")), 0, dr.Item("IdVehiculo"))
                .IdPiloto = IIf(IsDBNull(dr.Item("IdPiloto")), 0, dr.Item("IdPiloto"))
                .IdRuta = IIf(IsDBNull(dr.Item("IdRuta")), 0, dr.Item("IdRuta"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
                .No_pase = IIf(IsDBNull(dr.Item("no_pase")), 0, dr.Item("no_pase"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Numero = IIf(IsDBNull(dr.Item("numero")), 0, dr.Item("numero"))
                .Marchamo = IIf(IsDBNull(dr.Item("marchamo")), "", dr.Item("marchamo"))
                .Cant_bultos = IIf(IsDBNull(dr.Item("cant_bultos")), 0.0, dr.Item("cant_bultos"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .No_Documento_Externo = IIf(IsDBNull(dr.Item("no_documento_externo")), "", dr.Item("no_documento_externo"))
            End With
        Catch ex As Exception
            Throw New Exception("Trans_despacho_enc_Cargar: " & ex.message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_despacho_enc")
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            If oBeTrans_despacho_enc.IdVehiculo <> 0 Then Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            If oBeTrans_despacho_enc.IdPiloto <> 0 Then Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            If oBeTrans_despacho_enc.IdRuta <> 0 Then Ins.Add("idruta", "@idruta", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("no_pase", "@no_pase", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("numero", "@numero", DataType.Parametro)
            Ins.Add("marchamo", "@marchamo", DataType.Parametro)
            Ins.Add("cant_bultos", "@cant_bultos", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_enc.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_despacho_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_despacho_enc.IdPropietarioBodega))
            If oBeTrans_despacho_enc.IdVehiculo <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_despacho_enc.IdVehiculo))
            If oBeTrans_despacho_enc.IdPiloto <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_despacho_enc.IdPiloto))
            If oBeTrans_despacho_enc.IdRuta <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeTrans_despacho_enc.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_despacho_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@NO_PASE", oBeTrans_despacho_enc.No_pase))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_despacho_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_despacho_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_despacho_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_despacho_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeTrans_despacho_enc.Numero))
            cmd.Parameters.Add(New SqlParameter("@MARCHAMO", oBeTrans_despacho_enc.Marchamo))
            cmd.Parameters.Add(New SqlParameter("@CANT_BULTOS", oBeTrans_despacho_enc.Cant_bultos))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_despacho_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_despacho_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_despacho_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_despacho_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_despacho_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_EXTERNO", oBeTrans_despacho_enc.No_Documento_Externo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Trans_despacho_enc_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Upd.Init("trans_despacho_enc")
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Upd.Add("idruta", "@idruta", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("no_pase", "@no_pase", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("numero", "@numero", DataType.Parametro)
            Upd.Add("marchamo", "@marchamo", DataType.Parametro)
            Upd.Add("cant_bultos", "@cant_bultos", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Upd.Where("IdDespachoEnc = @IdDespachoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_enc.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_despacho_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_despacho_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_despacho_enc.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_despacho_enc.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeTrans_despacho_enc.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_despacho_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@NO_PASE", oBeTrans_despacho_enc.No_pase))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_despacho_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_despacho_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_despacho_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_despacho_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeTrans_despacho_enc.Numero))
            cmd.Parameters.Add(New SqlParameter("@MARCHAMO", oBeTrans_despacho_enc.Marchamo))
            cmd.Parameters.Add(New SqlParameter("@CANT_BULTOS", oBeTrans_despacho_enc.Cant_bultos))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_despacho_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_despacho_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_despacho_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_despacho_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_despacho_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_EXTERNO", oBeTrans_despacho_enc.No_Documento_Externo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception("Trans_despacho_enc_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Function Eliminar(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc,
                             Optional ByVal pConection As SqlConnection = Nothing,
                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_despacho_enc" &
             "  Where(IdDespachoEnc = @IdDespachoEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_enc.IdDespachoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception("Trans_despacho_enc_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Dim sp As String = "SELECT * FROM Trans_despacho_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw New Exception("Trans_despacho_enc_Listar: " & ex.Message)
        End Try
    End Function

    Public Function Obtener(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc) As Boolean


        Try


            Dim sp As String = "SELECT * FROM Trans_despacho_enc" &
            " Where(IdDespachoEnc = @IdDespachoEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_enc.IdDespachoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_despacho_enc, dt.Rows(0))
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

    Public Shared Function Actualizar_Encabezado(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_despacho_enc")
            If oBeTrans_despacho_enc.IdVehiculo <> 0 Then Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            If oBeTrans_despacho_enc.IdPiloto <> 0 Then Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            If oBeTrans_despacho_enc.IdRuta <> 0 Then Upd.Add("idruta", "@idruta", DataType.Parametro)
            Upd.Add("no_pase", "@no_pase", DataType.Parametro)
            Upd.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("numero", "@numero", DataType.Parametro)
            Upd.Add("marchamo", "@marchamo", DataType.Parametro)
            Upd.Add("cant_bultos", "@cant_bultos", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdDespachoEnc = @IdDespachoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_enc.IdDespachoEnc))
            If oBeTrans_despacho_enc.IdVehiculo <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_despacho_enc.IdVehiculo))
            If oBeTrans_despacho_enc.IdPiloto <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_despacho_enc.IdPiloto))
            If oBeTrans_despacho_enc.IdRuta <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeTrans_despacho_enc.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@NO_PASE", oBeTrans_despacho_enc.No_pase))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_EXTERNO", oBeTrans_despacho_enc.No_Documento_Externo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_despacho_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeTrans_despacho_enc.Numero))
            cmd.Parameters.Add(New SqlParameter("@MARCHAMO", oBeTrans_despacho_enc.Marchamo))
            cmd.Parameters.Add(New SqlParameter("@CANT_BULTOS", oBeTrans_despacho_enc.Cant_bultos))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_despacho_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_despacho_enc.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception("Trans_despacho_enc_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

End Class
