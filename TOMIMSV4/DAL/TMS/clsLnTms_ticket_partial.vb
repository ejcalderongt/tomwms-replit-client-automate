Imports System.Data.SqlClient

Partial Public Class clsLnTms_ticket

    Public Shared Function Get_Ultima_Visita_By_IdPiloto(ByVal pIdPiloto As Integer) As clsBeTms_ticket

        Get_Ultima_Visita_By_IdPiloto = Nothing

        Try

            Const sp As String = "SELECT top(1) * FROM Tms_ticket " &
            " Where(IdPiloto = @IdPiloto) order by Fecha_Ingreso desc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPiloto", pIdPiloto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTms_ticket As New clsBeTms_ticket

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTms_ticket, lDataTable.Rows(0))
                            Get_Ultima_Visita_By_IdPiloto = vBeTms_ticket
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Ticket(Bepiloto As clsBeEmpresa_transporte_pilotos,
                                          BeVehiculo As clsBeEmpresa_transporte_vehiculos,
                                          BeTicket As clsBeTms_ticket,
                                          BeTmsTicket As clsBeTms_ticket_pol) As Boolean

        Guardar_Ticket = False

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not clsLnEmpresa_transporte_pilotos.Existe_No_Licencia(Bepiloto.No_Licencia) Then
                Bepiloto.IdPiloto = clsLnEmpresa_transporte_pilotos.MaxID(lConnection, lTransaction) + 1
                BeTicket.IdPiloto = Bepiloto.IdPiloto
                'Bepiloto.IdTipoLicencia = 1
                clsLnEmpresa_transporte_pilotos.Insertar(Bepiloto)
            Else
                Bepiloto = clsLnEmpresa_transporte_pilotos.Get_By_No_Documento(Bepiloto.No_Licencia, lConnection, lTransaction)
            End If

            If Not clsLnEmpresa_transporte_vehiculos.Existe_Placa(BeVehiculo.Placa) Then
                BeVehiculo.IdVehiculo = clsLnEmpresa_transporte_vehiculos.MaxID(lConnection, lTransaction) + 1
                BeTicket.IdVehiculo = BeVehiculo.IdVehiculo
                clsLnEmpresa_transporte_vehiculos.Insertar(BeVehiculo)
            Else
                BeVehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_No_Placa(BeVehiculo.Placa, lConnection, lTransaction)
                If Not BeVehiculo Is Nothing Then
                    BeTicket.IdVehiculo = BeVehiculo.IdVehiculo
                End If
            End If

            BeTicket.IdTicket = MaxID(lConnection, lTransaction) + 1

            'GT 10022021 si el ticket viene con poliza se almacena.
            If BeTmsTicket IsNot Nothing Then

                'GT 26012021 se usa el Id Ticket para la DUCA
                BeTmsTicket.IdTicket = BeTicket.IdTicket
                'GT 26012021 se obtiene el IdOrdenTmsEnc
                BeTmsTicket.IdOrdenTmsEnc = TMS_MaxID(lConnection, lTransaction) + 1

                '#EJC20210222
                clsLnTms_ticket_pol.Insertar(BeTmsTicket, lConnection, lTransaction)

            End If

            Insertar(BeTicket, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar_Ticket = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    '#GT09052022: Guardar Ticket CEALSA sin valores, solamente el id del Ticket
    Public Shared Function Guardar_Ticket(BeTicket As clsBeTms_ticket) As Boolean

        Guardar_Ticket = False

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeTicket.IdTicket = MaxID(lConnection, lTransaction) + 1
            Insertar_Directo(BeTicket, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar_Ticket = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Ticket_By_Id(ByVal pIdTicket As Integer) As clsBeTms_ticket

        Get_Ticket_By_Id = Nothing

        Try

            Const sp As String = "SELECT top(1) * FROM Tms_ticket 
							      Where(IdTicket = @IdTicket) order by Fecha_Ingreso desc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTms_ticket As New clsBeTms_ticket

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTms_ticket, lDataTable.Rows(0))
                            '#EJC20210222
                            vBeTms_ticket.ObjPoliza = clsLnTms_ticket_pol.GetSingle(vBeTms_ticket.IdTicket, lConnection, lTransaction)
                            Get_Ticket_By_Id = vBeTms_ticket
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeTicket_By_IdTicket(ByVal pIdTicket As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As clsBeTms_ticket

        Get_BeTicket_By_IdTicket = Nothing

        Try

            Const sp As String = "SELECT top(1) * FROM Tms_ticket 
							      Where(IdTicket = @IdTicket) order by Fecha_Ingreso desc "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTms_ticket As New clsBeTms_ticket

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Cargar(vBeTms_ticket, lDataTable.Rows(0))
                    '#EJC20210222
                    vBeTms_ticket.ObjPoliza = clsLnTms_ticket_pol.GetSingle(vBeTms_ticket.IdTicket,
                                                                            lConnection,
                                                                            lTransaction)

                    Get_BeTicket_By_IdTicket = vBeTms_ticket

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Info_Ingreso(ByVal pIdTicket As Integer,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As clsBeVW_Fecha_Recepcion_TMS_Ticket

        Get_Info_Ingreso = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Fecha_Recepcion_TMS_Ticket
							      Where(IdTicket = @IdTicket)  "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTms_ticket As New clsBeVW_Fecha_Recepcion_TMS_Ticket

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    clsLnVW_Fecha_Recepcion_TMS_Ticket.Cargar(vBeTms_ticket, lDataTable.Rows(0))
                    Get_Info_Ingreso = vBeTms_ticket
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class