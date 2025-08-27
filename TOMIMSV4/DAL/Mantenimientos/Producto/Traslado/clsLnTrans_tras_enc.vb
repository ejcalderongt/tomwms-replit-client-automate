
Imports System.Data.SqlClient

Public Class clsLnTrans_tras_enc

    Public Shared Sub Cargar(ByRef oBeTrans_tras_enc As clsBeTrans_tras_enc, ByRef dr As DataRow)
        Try
            With oBeTrans_tras_enc
                .IdTrasladoEnc = IIf(IsDBNull(dr.Item("IdTrasladoEnc")), 0, dr.Item("IdTrasladoEnc"))
                .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                .IdBodegaDestino = IIf(IsDBNull(dr.Item("IdBodegaDestino")), 0, dr.Item("IdBodegaDestino"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdMuelleOrigen = IIf(IsDBNull(dr.Item("IdMuelleOrigen")), 0, dr.Item("IdMuelleOrigen"))
                .IdPiloto = IIf(IsDBNull(dr.Item("IdPiloto")), 0, dr.Item("IdPiloto"))
                .IdVehiculo = IIf(IsDBNull(dr.Item("IdVehiculo")), 0, dr.Item("IdVehiculo"))
                .IdRuta = IIf(IsDBNull(dr.Item("IdRuta")), 0, dr.Item("IdRuta"))
                .FechaTraslado = IIf(IsDBNull(dr.Item("FechaTraslado")), Date.Now, dr.Item("FechaTraslado"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Ubicacion = IIf(IsDBNull(dr.Item("ubicacion")), "", dr.Item("ubicacion"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .No_documento = IIf(IsDBNull(dr.Item("no_documento")), 0, dr.Item("no_documento"))
                .Local = IIf(IsDBNull(dr.Item("local")), False, dr.Item("local"))
                .Pallet_primero = IIf(IsDBNull(dr.Item("pallet_primero")), False, dr.Item("pallet_primero"))
                .Anulado = IIf(IsDBNull(dr.Item("anulado")), False, dr.Item("anulado"))
                .FechaEntrega = IIf(IsDBNull(dr.Item("FechaEntrega")), Date.Now, dr.Item("FechaEntrega"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .HoraEntregaDesde = IIf(IsDBNull(dr.Item("HoraEntregaDesde")), Date.Now, dr.Item("HoraEntregaDesde"))
                .HoraEntregaHasta = IIf(IsDBNull(dr.Item("HoraEntregaHasta")), Date.Now, dr.Item("HoraEntregaHasta"))
                .NoGuia = IIf(IsDBNull(dr.Item("NoGuia")), "", dr.Item("NoGuia"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_tras_enc As clsBeTrans_tras_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_tras_enc")
            Ins.Add("idtrasladoenc", "@idtrasladoenc", DataType.Parametro)
            Ins.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Ins.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idmuelleorigen", "@idmuelleorigen", DataType.Parametro)
            Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Ins.Add("idruta", "@idruta", DataType.Parametro)
            Ins.Add("fechatraslado", "@fechatraslado", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("no_documento", "@no_documento", DataType.Parametro)
            Ins.Add("local", "@local", DataType.Parametro)
            Ins.Add("pallet_primero", "@pallet_primero", DataType.Parametro)
            Ins.Add("anulado", "@anulado", DataType.Parametro)
            Ins.Add("fechaentrega", "@fechaentrega", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("horaentregadesde", "@horaentregadesde", DataType.Parametro)
            Ins.Add("horaentregahasta", "@horaentregahasta", DataType.Parametro)
            Ins.Add("noguia", "@noguia", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRASLADOENC", oBeTrans_tras_enc.IdTrasladoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_tras_enc.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", oBeTrans_tras_enc.IdBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_tras_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLEORIGEN", oBeTrans_tras_enc.IdMuelleOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_tras_enc.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_tras_enc.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeTrans_tras_enc.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@FECHATRASLADO", oBeTrans_tras_enc.FechaTraslado))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_tras_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_tras_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_tras_enc.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_tras_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_tras_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_tras_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_tras_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_tras_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_tras_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_tras_enc.No_documento))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeTrans_tras_enc.Local))
            cmd.Parameters.Add(New SqlParameter("@PALLET_PRIMERO", oBeTrans_tras_enc.Pallet_primero))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeTrans_tras_enc.Anulado))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTREGA", oBeTrans_tras_enc.FechaEntrega))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_tras_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGADESDE", oBeTrans_tras_enc.HoraEntregaDesde))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGAHASTA", oBeTrans_tras_enc.HoraEntregaHasta))
            cmd.Parameters.Add(New SqlParameter("@NOGUIA", oBeTrans_tras_enc.NoGuia))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_tras_enc As clsBeTrans_tras_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()


        Try

            Upd.Init("trans_tras_enc")
            Upd.Add("idtrasladoenc", "@idtrasladoenc", DataType.Parametro)
            Upd.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Upd.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idmuelleorigen", "@idmuelleorigen", DataType.Parametro)
            Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Upd.Add("idruta", "@idruta", DataType.Parametro)
            Upd.Add("fechatraslado", "@fechatraslado", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("no_documento", "@no_documento", DataType.Parametro)
            Upd.Add("local", "@local", DataType.Parametro)
            Upd.Add("pallet_primero", "@pallet_primero", DataType.Parametro)
            Upd.Add("anulado", "@anulado", DataType.Parametro)
            Upd.Add("fechaentrega", "@fechaentrega", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("horaentregadesde", "@horaentregadesde", DataType.Parametro)
            Upd.Add("horaentregahasta", "@horaentregahasta", DataType.Parametro)
            Upd.Add("noguia", "@noguia", DataType.Parametro)
            Upd.Where("IdTrasladoEnc = @IdTrasladoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRASLADOENC", oBeTrans_tras_enc.IdTrasladoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_tras_enc.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", oBeTrans_tras_enc.IdBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_tras_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLEORIGEN", oBeTrans_tras_enc.IdMuelleOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_tras_enc.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_tras_enc.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeTrans_tras_enc.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@FECHATRASLADO", oBeTrans_tras_enc.FechaTraslado))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_tras_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_tras_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_tras_enc.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_tras_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_tras_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_tras_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_tras_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_tras_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_tras_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_tras_enc.No_documento))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeTrans_tras_enc.Local))
            cmd.Parameters.Add(New SqlParameter("@PALLET_PRIMERO", oBeTrans_tras_enc.Pallet_primero))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeTrans_tras_enc.Anulado))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTREGA", oBeTrans_tras_enc.FechaEntrega))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_tras_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGADESDE", oBeTrans_tras_enc.HoraEntregaDesde))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGAHASTA", oBeTrans_tras_enc.HoraEntregaHasta))
            cmd.Parameters.Add(New SqlParameter("@NOGUIA", oBeTrans_tras_enc.NoGuia))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeTrans_tras_enc As clsBeTrans_tras_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_tras_enc" &
             "  Where(IdTrasladoEnc = @IdTrasladoEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRASLADOENC", oBeTrans_tras_enc.IdTrasladoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_tras_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_tras_enc As clsBeTrans_tras_enc) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Trans_tras_enc" &
            " Where(IdTrasladoEnc = @IdTrasladoEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRASLADOENC", oBeTrans_tras_enc.IdTrasladoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_tras_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
