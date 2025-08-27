Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTms_ticket_pol

    Public Shared Sub Cargar(ByRef oBeTms_ticket_pol As clsBeTms_ticket_pol, ByRef dr As DataRow)

        Try

            With oBeTms_ticket_pol

                .IdTicket = IIf(IsDBNull(dr.Item("IdTicket")), 0, dr.Item("IdTicket"))
                .IdOrdenTmsEnc = IIf(IsDBNull(dr.Item("IdOrdenTmsEnc")), 0, dr.Item("IdOrdenTmsEnc"))
                .NoPoliza = IIf(IsDBNull(dr.Item("NoPoliza")), "", dr.Item("NoPoliza"))
                .Dua = IIf(IsDBNull(dr.Item("dua")), "", dr.Item("dua"))
                .Fecha_poliza = IIf(IsDBNull(dr.Item("fecha_poliza")), Date.Now, dr.Item("fecha_poliza"))
                .Pais_procede = IIf(IsDBNull(dr.Item("pais_procede")), "", dr.Item("pais_procede"))
                .Tipo_cambio = IIf(IsDBNull(dr.Item("tipo_cambio")), 0.0, dr.Item("tipo_cambio"))
                .Total_valoraduana = IIf(IsDBNull(dr.Item("total_valoraduana")), 0.0, dr.Item("total_valoraduana"))
                .Total_bultos_peso = IIf(IsDBNull(dr.Item("total_bultos_peso")), 0.0, dr.Item("total_bultos_peso"))
                .Total_usd = IIf(IsDBNull(dr.Item("total_usd")), 0.0, dr.Item("total_usd"))
                .Total_flete = IIf(IsDBNull(dr.Item("total_flete")), 0.0, dr.Item("total_flete"))
                .Total_seguro = IIf(IsDBNull(dr.Item("total_seguro")), 0.0, dr.Item("total_seguro"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Clave_aduana = IIf(IsDBNull(dr.Item("clave_aduana")), "", dr.Item("clave_aduana"))
                .Nit_imp_exp = IIf(IsDBNull(dr.Item("nit_imp_exp")), "", dr.Item("nit_imp_exp"))
                .Clase = IIf(IsDBNull(dr.Item("clase")), "", dr.Item("clase"))
                .Mod_transporte = IIf(IsDBNull(dr.Item("mod_transporte")), "", dr.Item("mod_transporte"))
                .Total_liquidar = IIf(IsDBNull(dr.Item("total_liquidar")), 0.0, dr.Item("total_liquidar"))
                .Total_general = IIf(IsDBNull(dr.Item("total_general")), 0.0, dr.Item("total_general"))
                .IdRegimen = IIf(IsDBNull(dr.Item("IdRegimen")), 0.0, dr.Item("IdRegimen"))
                .Codigo_Barra = IIf(IsDBNull(dr.Item("Codigo_Barra")), "", dr.Item("Codigo_Barra"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTms_ticket_pol As clsBeTms_ticket_pol, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tms_ticket_pol")
            Ins.Add("IdTicket", "@IdTicket", DataType.Parametro)
            Ins.Add("idordentmsenc", "@idordentmsenc", DataType.Parametro)
            Ins.Add("nopoliza", "@nopoliza", DataType.Parametro)
            Ins.Add("dua", "@dua", DataType.Parametro)
            Ins.Add("fecha_poliza", "@fecha_poliza", DataType.Parametro)
            Ins.Add("pais_procede", "@pais_procede", DataType.Parametro)
            Ins.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Ins.Add("total_valoraduana", "@total_valoraduana", DataType.Parametro)
            Ins.Add("total_bultos_peso", "@total_bultos_peso", DataType.Parametro)
            Ins.Add("total_usd", "@total_usd", DataType.Parametro)
            Ins.Add("total_flete", "@total_flete", DataType.Parametro)
            Ins.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Ins.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Ins.Add("clase", "@clase", DataType.Parametro)
            Ins.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Ins.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Ins.Add("total_general", "@total_general", DataType.Parametro)
            Ins.Add("IdRegimen", "@IdRegimen", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket_pol.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDORDENTMSENC", oBeTms_ticket_pol.IdOrdenTmsEnc))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", oBeTms_ticket_pol.NoPoliza))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBeTms_ticket_pol.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTms_ticket_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", oBeTms_ticket_pol.Pais_procede))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTms_ticket_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTms_ticket_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTms_ticket_pol.Total_bultos_peso))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTms_ticket_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTms_ticket_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTms_ticket_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTms_ticket_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTms_ticket_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTms_ticket_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTms_ticket_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTms_ticket_pol.Clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTms_ticket_pol.Nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTms_ticket_pol.Clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTms_ticket_pol.Mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTms_ticket_pol.Total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTms_ticket_pol.Total_general))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTms_ticket_pol.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTms_ticket_pol.Codigo_Barra))

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

    Public Shared Function Actualizar(ByRef oBeTms_ticket_pol As clsBeTms_ticket_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tms_ticket_pol")
            Upd.Add("IdTicket", "@IdTicket", DataType.Parametro)
            Upd.Add("idordentmsenc", "@idordentmsenc", DataType.Parametro)
            Upd.Add("IdRegimen", "@IdRegimen", DataType.Parametro)
            Upd.Add("nopoliza", "@nopoliza", DataType.Parametro)
            Upd.Add("dua", "@dua", DataType.Parametro)
            Upd.Add("fecha_poliza", "@fecha_poliza", DataType.Parametro)
            Upd.Add("pais_procede", "@pais_procede", DataType.Parametro)
            Upd.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Upd.Add("total_valoraduana", "@total_valoraduana", DataType.Parametro)
            Upd.Add("total_bultos_peso", "@total_bultos_peso", DataType.Parametro)
            Upd.Add("total_usd", "@total_usd", DataType.Parametro)
            Upd.Add("total_flete", "@total_flete", DataType.Parametro)
            Upd.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Upd.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Upd.Add("clase", "@clase", DataType.Parametro)
            Upd.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Upd.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Upd.Add("total_general", "@total_general", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Where("IdOrdenTmsPol = @IdOrdenTmsPol" &
                " AND IdOrdenTmsEnc = @IdOrdenTmsEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket_pol.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDORDENTMSENC", oBeTms_ticket_pol.IdOrdenTmsEnc))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTms_ticket_pol.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", oBeTms_ticket_pol.NoPoliza))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBeTms_ticket_pol.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTms_ticket_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", oBeTms_ticket_pol.Pais_procede))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTms_ticket_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTms_ticket_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTms_ticket_pol.Total_bultos_peso))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTms_ticket_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTms_ticket_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTms_ticket_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTms_ticket_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTms_ticket_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTms_ticket_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTms_ticket_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTms_ticket_pol.Clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTms_ticket_pol.Nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTms_ticket_pol.Clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTms_ticket_pol.Mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTms_ticket_pol.Total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTms_ticket_pol.Total_general))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTms_ticket_pol.Codigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTms_ticket_pol.Codigo_Barra))

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

    Public Shared Function Eliminar(ByRef oBeTms_ticket_pol As clsBeTms_ticket_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tms_ticket_pol" &
             "  Where(IdTicket = @IdTicket)" &
             "  AND (IdOrdenTmsEnc = @IdOrdenTmsEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket_pol.IdTicket))
            cmd.Parameters.Add(New SqlParameter("@IDORDENTMSENC", oBeTms_ticket_pol.IdOrdenTmsEnc))

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

            Const sp As String = "SELECT * FROM Tms_ticket_pol"
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

    Public Shared Function Get_All() As List(Of clsBeTms_ticket_pol)

        Dim lReturnList As New List(Of clsBeTms_ticket_pol)

        Try

            Const sp As String = "SELECT * FROM Tms_ticket_pol"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTms_ticket_pol As New clsBeTms_ticket_pol

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTms_ticket_pol = New clsBeTms_ticket_pol()
                            Cargar(vBeTms_ticket_pol, dr)
                            lReturnList.Add(vBeTms_ticket_pol)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTicket As Integer) As clsBeTms_ticket_pol


        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM tms_ticket_pol WHERE IdTicket=@IdTicket"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTms_ticket_pol()

                        Cargar(Obj, lRow)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTicket As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeTms_ticket_pol

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM tms_ticket_pol WHERE IdTicket=@IdTicket"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", pIdTicket)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTms_ticket_pol()
                    Cargar(Obj, lRow)
                    GetSingle = Obj
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenTmsEnc),0) FROM Tms_ticket_pol"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
