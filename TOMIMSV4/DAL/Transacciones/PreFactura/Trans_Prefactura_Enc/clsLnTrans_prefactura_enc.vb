Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_prefactura_enc

    Public Shared Sub Cargar(ByRef oBeTrans_prefactura_enc As clsBeTrans_prefactura_enc, ByRef dr As DataRow)
        Try
            With oBeTrans_prefactura_enc
                .IdTransPrefacturaEnc = IIf(IsDBNull(dr.Item("IdTransPrefacturaEnc")), 0, dr.Item("IdTransPrefacturaEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdClienteBodega = IIf(IsDBNull(dr.Item("IdClienteBodega")), 0, dr.Item("IdClienteBodega"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdTipoCuenta = IIf(IsDBNull(dr.Item("IdTipoCuenta")), 0, dr.Item("IdTipoCuenta"))
                .Tipo_Cambio = IIf(IsDBNull(dr.Item("Tipo_Cambio")), 0.0, dr.Item("Tipo_Cambio"))
                .IdOrdenCompraPol = IIf(IsDBNull(dr.Item("IdOrdenCompraPol")), 0, dr.Item("IdOrdenCompraPol"))
                .Poliza_oc_numero_orden = IIf(IsDBNull(dr.Item("poliza_oc_numero_orden")), "", dr.Item("poliza_oc_numero_orden"))
                .IdOrdenPedidoEnc = IIf(IsDBNull(dr.Item("IdOrdenPedidoEnc")), 0, dr.Item("IdOrdenPedidoEnc"))
                .IdOrdenPedidoPol = IIf(IsDBNull(dr.Item("IdOrdenPedidoPol")), 0, dr.Item("IdOrdenPedidoPol"))
                .Poliza_pe_numero_orden = IIf(IsDBNull(dr.Item("poliza_pe_numero_orden")), "", dr.Item("poliza_pe_numero_orden"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Anulada = IIf(IsDBNull(dr.Item("anulada")), False, dr.Item("anulada"))
                .Fecha_desde = IIf(IsDBNull(dr.Item("fecha_desde")), Date.Now, dr.Item("fecha_desde"))
                .Fecha_hasta = IIf(IsDBNull(dr.Item("fecha_hasta")), Date.Now, dr.Item("fecha_hasta"))
                .Es_consolidador = IIf(IsDBNull(dr.Item("es_consolidador")), False, dr.Item("es_consolidador"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Procesado_erp = IIf(IsDBNull(dr.Item("procesado_erp")), False, dr.Item("procesado_erp"))
                .Autorizacion_erp = IIf(IsDBNull(dr.Item("autorizacion_erp")), "", dr.Item("autorizacion_erp"))
                .Cobro_peso_bruto = IIf(IsDBNull(dr.Item("cobro_peso_bruto")), False, dr.Item("cobro_peso_bruto"))
                .Variante_cobro = IIf(IsDBNull(dr.Item("variante_cobro")), False, dr.Item("variante_cobro"))
                .Agrupar_producto = IIf(IsDBNull(dr.Item("agrupar_producto")), False, dr.Item("agrupar_producto"))
                .Valor_Aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                .Valor_General = IIf(IsDBNull(dr.Item("valor_general")), 0.0, dr.Item("valor_general"))
                .Valor_Peso = IIf(IsDBNull(dr.Item("valor_peso")), 0.0, dr.Item("valor_peso"))

            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_prefactura_enc As clsBeTrans_prefactura_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_prefactura_enc")
            Ins.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)

            If oBeTrans_prefactura_enc.IdClienteBodega > 0 Then
                Ins.Add("idclientebodega", "@idclientebodega", DataType.Parametro)
            End If

            If oBeTrans_prefactura_enc.IdOrdenCompraEnc > 0 Then
                Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            End If

            Ins.Add("idtipocuenta", "@idtipocuenta", DataType.Parametro)
            Ins.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)

            If oBeTrans_prefactura_enc.IdOrdenPedidoEnc > 0 Then
                Ins.Add("IdOrdenPedidoEnc", "@IdOrdenPedidoEnc", DataType.Parametro)
            End If

            If oBeTrans_prefactura_enc.IdOrdenCompraPol > 0 Then
                Ins.Add("idordencomprapol", "@idordencomprapol", DataType.Parametro)
            End If

            If oBeTrans_prefactura_enc.Poliza_oc_numero_orden <> "" Then
                Ins.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", DataType.Parametro)
            End If

            If oBeTrans_prefactura_enc.IdOrdenPedidoPol > 0 Then
                Ins.Add("idordenpedidopol", "@idordenpedidopol", DataType.Parametro)
            End If

            If oBeTrans_prefactura_enc.Poliza_pe_numero_orden <> "" Then
                Ins.Add("poliza_pe_numero_orden", "@poliza_pe_numero_orden", DataType.Parametro)
            End If

            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("anulada", "@anulada", DataType.Parametro)
            Ins.Add("fecha_desde", "@fecha_desde", DataType.Parametro)
            Ins.Add("fecha_hasta", "@fecha_hasta", DataType.Parametro)
            Ins.Add("es_consolidador", "@es_consolidador", DataType.Parametro)
            Ins.Add("Observacion", "@Observacion", DataType.Parametro)

            Ins.Add("procesado_erp", "@procesado_erp", DataType.Parametro)
            Ins.Add("autorizacion_erp", "@autorizacion_erp", DataType.Parametro)
            Ins.Add("cobro_peso_bruto", "@cobro_peso_bruto", DataType.Parametro)

            Ins.Add("variante_cobro", "@variante_cobro", DataType.Parametro)
            Ins.Add("agrupar_producto", "@agrupar_producto", DataType.Parametro)

            Ins.Add("valor_aduana", "@agrupar_producto", DataType.Parametro)
            Ins.Add("valor_general", "@agrupar_producto", DataType.Parametro)
            Ins.Add("valor_peso", "@agrupar_producto", DataType.Parametro)



            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_enc.IdTransPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_prefactura_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_prefactura_enc.IdPropietarioBodega))

            If oBeTrans_prefactura_enc.IdClienteBodega > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDCLIENTEBODEGA", oBeTrans_prefactura_enc.IdClienteBodega))
            End If

            If oBeTrans_prefactura_enc.IdOrdenCompraEnc > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_prefactura_enc.IdOrdenCompraEnc))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUENTA", oBeTrans_prefactura_enc.IdTipoCuenta))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_prefactura_enc.Tipo_Cambio))

            If oBeTrans_prefactura_enc.IdOrdenCompraPol > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_prefactura_enc.IdOrdenCompraPol))
            End If

            If oBeTrans_prefactura_enc.Poliza_oc_numero_orden <> "" Then
                cmd.Parameters.Add(New SqlParameter("@POLIZA_OC_NUMERO_ORDEN", oBeTrans_prefactura_enc.Poliza_oc_numero_orden))
            End If

            If oBeTrans_prefactura_enc.IdOrdenPedidoEnc > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_prefactura_enc.IdOrdenPedidoEnc))
            End If

            If oBeTrans_prefactura_enc.IdOrdenPedidoPol > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOPOL", oBeTrans_prefactura_enc.IdOrdenPedidoPol))
            End If

            If oBeTrans_prefactura_enc.Poliza_pe_numero_orden <> "" Then
                cmd.Parameters.Add(New SqlParameter("@POLIZA_PE_NUMERO_ORDEN", oBeTrans_prefactura_enc.Poliza_pe_numero_orden))
            End If

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ANULADA", oBeTrans_prefactura_enc.Anulada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESDE", oBeTrans_prefactura_enc.Fecha_desde))
            cmd.Parameters.Add(New SqlParameter("@FECHA_HASTA", oBeTrans_prefactura_enc.Fecha_hasta))
            cmd.Parameters.Add(New SqlParameter("@ES_CONSOLIDADOR", oBeTrans_prefactura_enc.Es_consolidador))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_prefactura_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_ERP", oBeTrans_prefactura_enc.Procesado_erp))
            cmd.Parameters.Add(New SqlParameter("@AUTORIZACION_ERP", oBeTrans_prefactura_enc.Autorizacion_erp))
            cmd.Parameters.Add(New SqlParameter("@COBRO_PESO_BRUTO", oBeTrans_prefactura_enc.Cobro_peso_bruto))

            cmd.Parameters.Add(New SqlParameter("@VARIANTE_COBRO", oBeTrans_prefactura_enc.Variante_cobro))
            cmd.Parameters.Add(New SqlParameter("@AGRUPAR_PRODUCTO", oBeTrans_prefactura_enc.Agrupar_producto))

            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_prefactura_enc.Valor_Aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_GENERAL", oBeTrans_prefactura_enc.Valor_General))
            cmd.Parameters.Add(New SqlParameter("@VALOR_PESO", oBeTrans_prefactura_enc.Valor_Peso))


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

    Public Shared Function Actualizar(ByRef oBeTrans_prefactura_enc As clsBeTrans_prefactura_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_prefactura_enc")
            Upd.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            If oBeTrans_prefactura_enc.IdClienteBodega > 0 Then
                Upd.Add("idclientebodega", "@idclientebodega", DataType.Parametro)
            End If
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idtipocuenta", "@idtipocuenta", DataType.Parametro)
            Upd.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Upd.Add("idordencomprapol", "@idordencomprapol", DataType.Parametro)
            Upd.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", DataType.Parametro)
            Upd.Add("IdOrdenPedidoEnc", "@IdOrdenPedidoEnc", DataType.Parametro)

            Upd.Add("idordenpedidopol", "@idordenpedidopol", DataType.Parametro)
            Upd.Add("poliza_pe_numero_orden", "@poliza_pe_numero_orden", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("anulada", "@anulada", DataType.Parametro)
            Upd.Add("fecha_desde", "@fecha_desde", DataType.Parametro)
            Upd.Add("fecha_hasta", "@fecha_hasta", DataType.Parametro)
            Upd.Add("es_consolidador", "@es_consolidador", DataType.Parametro)
            Upd.Add("Observacion", "@Observacion", DataType.Parametro)
            Upd.Where("IdTransPrefacturaEnc = @IdTransPrefacturaEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_enc.IdTransPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_prefactura_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_prefactura_enc.IdPropietarioBodega))
            If oBeTrans_prefactura_enc.IdClienteBodega > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDCLIENTEBODEGA", oBeTrans_prefactura_enc.IdClienteBodega))
            End If
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_prefactura_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUENTA", oBeTrans_prefactura_enc.IdTipoCuenta))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_prefactura_enc.Tipo_Cambio))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_prefactura_enc.IdOrdenCompraPol))
            cmd.Parameters.Add(New SqlParameter("@POLIZA_OC_NUMERO_ORDEN", oBeTrans_prefactura_enc.Poliza_oc_numero_orden))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_prefactura_enc.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOPOL", oBeTrans_prefactura_enc.IdOrdenPedidoPol))
            cmd.Parameters.Add(New SqlParameter("@POLIZA_PE_NUMERO_ORDEN", oBeTrans_prefactura_enc.Poliza_pe_numero_orden))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ANULADA", oBeTrans_prefactura_enc.Anulada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESDE", oBeTrans_prefactura_enc.Fecha_desde))
            cmd.Parameters.Add(New SqlParameter("@FECHA_HASTA", oBeTrans_prefactura_enc.Fecha_hasta))
            cmd.Parameters.Add(New SqlParameter("@ES_CONSOLIDADOR", oBeTrans_prefactura_enc.Es_consolidador))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_prefactura_enc.Observacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_prefactura_enc As clsBeTrans_prefactura_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_prefactura_enc" & _
             "  Where(IdTransPrefacturaEnc = @IdTransPrefacturaEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_enc.IdTransPrefacturaEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_enc"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_prefactura_enc)

        Dim lReturnList As New List(Of clsBeTrans_prefactura_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_prefactura_enc As New clsBeTrans_prefactura_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_prefactura_enc = New clsBeTrans_prefactura_enc()
                            Cargar(vBeTrans_prefactura_enc, dr)
                            lReturnList.Add(vBeTrans_prefactura_enc)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_prefactura_enc As clsBeTrans_prefactura_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_enc" & _
            " Where(IdTransPrefacturaEnc = @IdTransPrefacturaEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTransPrefacturaEnc", pBeTrans_prefactura_enc.IdTransPrefacturaEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        'Dim vBeTrans_prefactura_enc As New clsBeTrans_prefactura_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeTrans_prefactura_enc, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub GetSingle_And_Details_By_IdPrefacturaEnc(ByRef pBeTrans_prefactura_enc As clsBeTrans_prefactura_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_enc Where(IdTransPrefacturaEnc = @IdTransPrefacturaEnc)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTransPrefacturaEnc", pBeTrans_prefactura_enc.IdTransPrefacturaEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeTrans_prefactura_enc, lDataTable.Rows(0))

                            pBeTrans_prefactura_enc.lDetalle_Det = clsLnTrans_prefactura_det.Get_All_By_IdPreFacturaEnc(pBeTrans_prefactura_enc.IdTransPrefacturaEnc,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                            If Not pBeTrans_prefactura_enc.Es_consolidador Then

                                pBeTrans_prefactura_enc.lDetalle_Mov = clsLnTrans_prefactura_mov.Get_All_By_IdPreFacturaEnc(pBeTrans_prefactura_enc.IdTransPrefacturaEnc,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransPrefacturaEnc),0) FROM Trans_prefactura_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransPrefacturaEnc),0) FROM Trans_prefactura_enc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

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

    '#GT14112024: buscar servicio asociado a una prefactura para evitar que lo eliminen
    Public Shared Function Exist_By_IdOrdenCompraEnc_and_ServicioDet(ByRef IdOrdenCompraenc As Integer, ByVal Servicio As clsBeTrans_acuerdoscomerciales_det) As Boolean

        Exist_By_IdOrdenCompraEnc_and_ServicioDet = False

        Try

            Const sp As String = " select p_enc.IdOrdenCompraEnc,p_enc.IdTransPrefacturaEnc,p_det.IdAcuerdoEnc,p_det.IdAcuerdoDet 
									      from trans_prefactura_enc p_enc inner join trans_prefactura_det p_det on p_enc.IdTransPrefacturaEnc=p_det.IdTransPrefacturaEnc 
								   where p_enc.IdOrdenCompraEnc=@IdOrdenCompraEnc and p_det.IdAcuerdoEnc=@IdAcuerdo and p_det.IdAcuerdoDet=@IdAcuerdoDet "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraenc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", Servicio.IdAcuerdoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", Servicio.IdAcuerdoDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Exist_By_IdOrdenCompraEnc_and_ServicioDet = True
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

    '#GT14112024: buscar servicio asociado en el movimiento si fuera seleccion multiple
    Public Shared Function Exist_By_IdOrdenCompraEnc_and_Movimiento(ByRef IdOrdenCompraenc As Integer, ByVal Servicio As clsBeTrans_acuerdoscomerciales_det) As Boolean

        Exist_By_IdOrdenCompraEnc_and_Movimiento = False

        Try

            Const sp As String = " select p_det.IdAcuerdoEnc,p_det.IdAcuerdoDet,p_mov.IdOrdenCompraEnc from trans_prefactura_enc p_enc 
										  inner join trans_prefactura_det p_det on p_enc.IdTransPrefacturaEnc=p_det.IdTransPrefacturaEnc
										  inner join trans_prefactura_mov p_mov on p_enc.IdTransPrefacturaEnc=p_mov.IdTransPrefacturaEnc 
								   where p_mov.IdOrdenCompraEnc=@IdOrdenCompraEnc and p_det.IdAcuerdoEnc=@IdAcuerdo and p_det.IdAcuerdoDet=@IdAcuerdoDet "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraenc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", Servicio.IdAcuerdoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", Servicio.IdAcuerdoDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Exist_By_IdOrdenCompraEnc_and_Movimiento = True
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

    Public Shared Function Exist_By_IdOrdenCompraEnc_and_ServicioDet(ByRef IdOrdenCompraenc As Integer, ByVal Servicio As clsBeTrans_acuerdoscomerciales_det,
                                                                                                        ByVal pConnection As SqlConnection,
                                                                                                        ByVal pTransaction As SqlTransaction) As Boolean

        Exist_By_IdOrdenCompraEnc_and_ServicioDet = False

        Try

            Const sp As String = " select p_enc.IdOrdenCompraEnc,p_enc.IdTransPrefacturaEnc,p_det.IdAcuerdoEnc,p_det.IdAcuerdoDet 
									      from trans_prefactura_enc p_enc inner join trans_prefactura_det p_det on p_enc.IdTransPrefacturaEnc=p_det.IdTransPrefacturaEnc 
								   where p_enc.IdOrdenCompraEnc=@IdOrdenCompraEnc and p_det.IdAcuerdoEnc=@IdAcuerdo and p_det.IdAcuerdoDet=@IdAcuerdoDet "


            Using lDTA As New SqlDataAdapter(sp, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraenc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", Servicio.IdAcuerdoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", Servicio.IdAcuerdoDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Exist_By_IdOrdenCompraEnc_and_ServicioDet = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exist_By_IdOrdenCompraEnc_and_Movimiento(ByRef IdOrdenCompraenc As Integer, ByVal Servicio As clsBeTrans_acuerdoscomerciales_det,
                                                                                                       ByVal pConnection As SqlConnection,
                                                                                                       ByVal pTransaction As SqlTransaction) As Boolean

        Exist_By_IdOrdenCompraEnc_and_Movimiento = False

        Try

            Const sp As String = " select p_det.IdAcuerdoEnc,p_det.IdAcuerdoDet,p_mov.IdOrdenCompraEnc from trans_prefactura_enc p_enc 
										  inner join trans_prefactura_det p_det on p_enc.IdTransPrefacturaEnc=p_det.IdTransPrefacturaEnc
										  inner join trans_prefactura_mov p_mov on p_enc.IdTransPrefacturaEnc=p_mov.IdTransPrefacturaEnc 
								   where p_mov.IdOrdenCompraEnc=@IdOrdenCompraEnc and p_det.IdAcuerdoEnc=@IdAcuerdo and p_det.IdAcuerdoDet=@IdAcuerdoDet "


            Using lDTA As New SqlDataAdapter(sp, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraenc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", Servicio.IdAcuerdoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", Servicio.IdAcuerdoDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Exist_By_IdOrdenCompraEnc_and_Movimiento = True
                End If
            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
