Imports System.Data.SqlClient

Partial Public Class clsLnTrans_servicio_enc

    Public Shared Function Guardar_Registro(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc,
                                            ByVal lTransServiciosDet As List(Of clsBeTrans_servicio_det)) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If oBeTrans_servicio_enc.IsNew Then
                Insertar(oBeTrans_servicio_enc, lConnection, lTransaction)
            Else
                Actualizar(oBeTrans_servicio_enc, lConnection, lTransaction)
            End If

            Dim vMaxId As Integer = clsLnTrans_servicio_det.MaxID(lConnection, lTransaction) + 1

            'es correcto, efren.
            For Each Serv In lTransServiciosDet

                If Serv.IsNew Then
                    Serv.IdServicioEnc = oBeTrans_servicio_enc.IdServicioEnc
                    Serv.IdServicioDet = vMaxId
                    clsLnTrans_servicio_det.Insertar(Serv, lConnection, lTransaction)
                    vMaxId += 1
                Else
                    clsLnTrans_servicio_det.Actualizar(Serv, lConnection, lTransaction)
                End If

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Anular(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_servicio_enc")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdServicioEnc = @IdServicioEnc AND IdOrdenCompraEnc=@IdOrdenCompraEnc")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_enc.IdServicioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_servicio_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_servicio_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_servicio_enc.Estado))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle_By_OC(ByRef pIdDocumento As Integer, ByVal pTipoDocIngreso As Boolean) As clsBeTrans_servicio_enc


        GetSingle_By_OC = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vBeTrans_servicio_enc As New clsBeTrans_servicio_enc
        Dim sp As String = ""


        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pTipoDocIngreso Then

                sp = "SELECT * FROM Trans_servicio_enc" &
                                    " Where(IdOrdenCompraEnc = @pIdDocumento)"
            Else

                sp = "SELECT * FROM Trans_servicio_enc" &
                                    " Where(IdPedidoEnc = @pIdDocumento)"

            End If


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdDocumento", pIdDocumento)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)

                    With vBeTrans_servicio_enc
                        .IdServicioEnc = lRow("IdServicioEnc")
                        .IdOrdenCompraEnc = IIf(IsDBNull(lRow("IdOrdenCompraEnc")), 0, lRow("IdOrdenCompraEnc"))
                        .IdEmpresa = lRow("IdEmpresa")
                        .IdBodega = lRow("IdBodega")
                        .No_poliza = lRow("No_poliza")
                        .No_orden = lRow("No_orden")
                        .Fecha_doc_ingreso = lRow("Fecha_doc_ingreso")
                        .Enviado_a_erp = lRow("Enviado_a_erp")
                        .Activo = lRow("Activo")
                        .Estado = lRow("Estado")
                        .IdPedidoEnc = IIf(IsDBNull(lRow("IdPedidoEnc")), 0, lRow("IdPedidoEnc"))
                    End With

                End If

            End Using

            lTransaction.Commit()

            Return vBeTrans_servicio_enc

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Cerrar_Documento_Ingreso(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_servicio_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdServicioEnc = @IdServicioEnc AND IdOrdenCompraEnc=@IdOrdenCompraEnc")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_enc.IdServicioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_servicio_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_servicio_enc.Estado))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


End Class
