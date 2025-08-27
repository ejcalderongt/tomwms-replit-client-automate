Imports System.Data.SqlClient

Public Class clsLnTms_ticket

    Public Shared Sub Cargar(ByRef oBeTms_ticket As clsBeTms_ticket, ByRef dr As DataRow)

        Try

            With oBeTms_ticket

                .IdTicket = IIf(IsDBNull(dr.Item("IdTicket")), 0, dr.Item("IdTicket"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdUbicacionDestino = IIf(IsDBNull(dr.Item("IdUbicacionDestino")), 0, dr.Item("IdUbicacionDestino"))
                .IdPiloto = IIf(IsDBNull(dr.Item("IdPiloto")), 0, dr.Item("IdPiloto"))
                .IdVehiculo = IIf(IsDBNull(dr.Item("IdVehiculo")), 0, dr.Item("IdVehiculo"))
                .IdEmpresaTransporte = IIf(IsDBNull(dr.Item("IdEmpresaTransporte")), 0, dr.Item("IdEmpresaTransporte"))
                .Tipo_Operacion = IIf(IsDBNull(dr.Item("Tipo_Operacion")), "", dr.Item("Tipo_Operacion"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), New Date(1900, 1, 1), dr.Item("Fecha_Ingreso"))
                .Fecha_Salida = IIf(IsDBNull(dr.Item("Fecha_Salida")), New Date(1900, 1, 1), dr.Item("Fecha_Salida"))
                .Fecha_Finalizado = IIf(IsDBNull(dr.Item("Fecha_Finalizado")), New Date(1900, 1, 1), dr.Item("Fecha_Finalizado"))
                .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
                .No_Poliza = IIf(IsDBNull(dr.Item("No_Poliza")), "", dr.Item("No_Poliza"))
                .No_Placa = IIf(IsDBNull(dr.Item("No_Placa")), "", dr.Item("No_Placa"))
                .No_Documento_Piloto = IIf(IsDBNull(dr.Item("No_Documento_Piloto")), "", dr.Item("No_Documento_Piloto"))
                .Tipo_Documento_Piloto = IIf(IsDBNull(dr.Item("Tipo_Documento_Piloto")), "", dr.Item("Tipo_Documento_Piloto"))
                .Nombres_Piloto = IIf(IsDBNull(dr.Item("Nombres_Piloto")), "", dr.Item("Nombres_Piloto"))
                .Apellidos_Piloto = IIf(IsDBNull(dr.Item("Apellidos_Piloto")), "", dr.Item("Apellidos_Piloto"))
                .No_TC = IIf(IsDBNull(dr.Item("No_TC")), "", dr.Item("No_TC"))
                .Procesado_Stock_Jornada = IIf(IsDBNull(dr.Item("Procesado_Stock_Jornada")), False, dr.Item("Procesado_Stock_Jornada"))
                .Fecha_Procesado_Stock_Jornada = IIf(IsDBNull(dr.Item("fecha_procesado_stock_jornada")), New Date(1900, 1, 1), dr.Item("fecha_procesado_stock_jornada"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTms_ticket As clsBeTms_ticket, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tms_ticket")
            Ins.Add("idticket", "@idticket", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Ins.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Ins.Add("tipo_operacion", "@tipo_operacion", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_salida", "@fecha_salida", DataType.Parametro)
            Ins.Add("fecha_finalizado", "@fecha_finalizado", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("no_placa", "@no_placa", DataType.Parametro)
            Ins.Add("no_documento_piloto", "@no_documento_piloto", DataType.Parametro)
            Ins.Add("tipo_documento_piloto", "@tipo_documento_piloto", DataType.Parametro)
            Ins.Add("nombres_piloto", "@nombres_piloto", DataType.Parametro)
            Ins.Add("apellidos_piloto", "@apellidos_piloto", DataType.Parametro)
            Ins.Add("no_tc", "@no_tc", DataType.Parametro)
            Ins.Add("procesado_stock_jornada", "@procesado_stock_jornada", DataType.Parametro)
            Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTms_ticket.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTms_ticket.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTms_ticket.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeTms_ticket.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTms_ticket.Estado))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza))
            cmd.Parameters.Add(New SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NO_TC", oBeTms_ticket.No_TC))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.Fecha_Procesado_Stock_Jornada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT10052022_0922: guardar ticket sin valores de propietario,vehiculo, poliza, solamente el ID
    Public Shared Function Insertar_Directo(ByRef oBeTms_ticket As clsBeTms_ticket, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tms_ticket")
            Ins.Add("idticket", "@idticket", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            'Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            'Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            'Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            'Ins.Add("idempresatransporte","@idempresatransporte", DataType.Parametro)
            Ins.Add("tipo_operacion", "@tipo_operacion", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_salida", "@fecha_salida", DataType.Parametro)
            Ins.Add("fecha_finalizado", "@fecha_finalizado", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("no_placa", "@no_placa", DataType.Parametro)
            Ins.Add("no_documento_piloto", "@no_documento_piloto", DataType.Parametro)
            Ins.Add("tipo_documento_piloto", "@tipo_documento_piloto", DataType.Parametro)
            Ins.Add("nombres_piloto", "@nombres_piloto", DataType.Parametro)
            Ins.Add("apellidos_piloto", "@apellidos_piloto", DataType.Parametro)
            Ins.Add("no_tc", "@no_tc", DataType.Parametro)
            Ins.Add("procesado_stock_jornada", "@procesado_stock_jornada", DataType.Parametro)
            Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa))
            'cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTms_ticket.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino))
            'cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTms_ticket.IdPiloto))
            'cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTms_ticket.IdVehiculo))
            'cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeTms_ticket.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTms_ticket.Estado))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza))
            cmd.Parameters.Add(New SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NO_TC", oBeTms_ticket.No_TC))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.Fecha_Procesado_Stock_Jornada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTms_ticket As clsBeTms_ticket, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("idticket", "@idticket", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Upd.Add("idempresatransporte", "@idempresatransporte", DataType.Parametro)
            Upd.Add("tipo_operacion", "@tipo_operacion", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_salida", "@fecha_salida", DataType.Parametro)
            Upd.Add("fecha_finalizado", "@fecha_finalizado", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Upd.Add("no_placa", "@no_placa", DataType.Parametro)
            Upd.Add("no_documento_piloto", "@no_documento_piloto", DataType.Parametro)
            Upd.Add("tipo_documento_piloto", "@tipo_documento_piloto", DataType.Parametro)
            Upd.Add("nombres_piloto", "@nombres_piloto", DataType.Parametro)
            Upd.Add("apellidos_piloto", "@apellidos_piloto", DataType.Parametro)
            Upd.Add("no_tc", "@no_tc", DataType.Parametro)
            Upd.Add("procesado_stock_jornada", "@procesado_stock_jornada", DataType.Parametro)
            Upd.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTms_ticket.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTms_ticket.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", oBeTms_ticket.IdUbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTms_ticket.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTms_ticket.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESATRANSPORTE", oBeTms_ticket.IdEmpresaTransporte))
            cmd.Parameters.Add(New SqlParameter("@TIPO_OPERACION", oBeTms_ticket.Tipo_Operacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeTms_ticket.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SALIDA", oBeTms_ticket.Fecha_Salida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FINALIZADO", oBeTms_ticket.Fecha_Finalizado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTms_ticket.Estado))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeTms_ticket.No_Poliza))
            cmd.Parameters.Add(New SqlParameter("@NO_PLACA", oBeTms_ticket.No_Placa))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PILOTO", oBeTms_ticket.No_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@TIPO_DOCUMENTO_PILOTO", oBeTms_ticket.Tipo_Documento_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES_PILOTO", oBeTms_ticket.Nombres_Piloto))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS_PILOTO", oBeTms_ticket.Apellidos_Piloto))
            cmd.Parameters.Add(New SqlParameter("@NO_TC", oBeTms_ticket.No_TC))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_STOCK_JORNADA", oBeTms_ticket.Procesado_Stock_Jornada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeTms_ticket.fecha_procesado_stock_jornada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Tms_Ticket(ByRef BeTms_ticket As Integer, ByRef Estado As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@ESTADO", Estado))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Tms_Ticket_Procesado(ByRef BeTms_ticket As Integer,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fecha_procesado", "@fecha_procesado", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Procesado"))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Tms_Ticket_Procesado_Por_Stock_Jornada(ByVal IdTicketTMS As Integer,
                                                                             Optional ByVal pConection As SqlConnection = Nothing,
                                                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("procesado_stock_jornada", "@procesado_stock_jornada", DataType.Parametro)
            Upd.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@PROCESADO_STOCK_JORNADA", 1))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", IdTicketTMS))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Tms_Ticket_Asignado(ByRef BeTms_ticket As Integer,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fecha_asignado", "@fecha_asignado", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Asignado"))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ASIGNADO", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Eliminar(ByRef oBeTms_ticket As clsBeTms_ticket, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tms_ticket" &
             "  Where(IdTicket = @IdTicket)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Tms_ticket"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTms_ticket)

        Dim lReturnList As New List(Of clsBeTms_ticket)

        Try

            Const sp As String = "SELECT * FROM Tms_ticket"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTms_ticket As New clsBeTms_ticket

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTms_ticket = New clsBeTms_ticket()
                            Cargar(vBeTms_ticket, dr)
                            lReturnList.Add(vBeTms_ticket)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Ticket_Procesado_Stock_Jornada(ByVal IdTicket As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Boolean

        Ticket_Procesado_Stock_Jornada = False

        Try

            Const sp As String = "SELECT * FROM Tms_ticket " &
                                 " Where(IdTicket = @IdTicket AND procesado_stock_jornada =1) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", IdTicket)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Ticket_Procesado_Stock_Jornada = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Grid(ByVal pIdEmpresa As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Get_All_For_Grid = Nothing

        Try

            Dim vSQL As String = "SELECT IdTicket, 
										 Nombre_Piloto, 
									     Apellidos_Piloto, 
									     Placa_Vehiculo, 
									     Placa_TC, 
									     Empresa_Transporte, 
									     tipo_operacion, 
									     Fecha_Ingreso, 
									     Fecha_Salida,
										 Estado
									     FROM VW_TMS_Tikcet 
										 WHERE IdEmpresa = @IdEmpresa "

            vSQL += String.Format(" AND cast(Fecha_Ingreso AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel.Date), FormatoFechas.fFecha(pFechaAl.Date))

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_For_Grid = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function TMS_MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer


        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenTmsEnc),0) FROM Tms_ticket_pol"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Fecha_Ingreso_Tms_Ticket(ByRef BeTms_ticket As clsBeTms_ticket,
                                                               Optional ByVal pConection As SqlConnection = Nothing,
                                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", BeTms_ticket.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket.IdTicket))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Tms_Ticket_Finalizado(ByRef BeTms_ticket As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fecha_finalizado", "@fecha_finalizado", DataType.Parametro)
            Upd.Where("IdTicket = @IdTicket")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Finalizado"))
            cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FINALIZADO", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
