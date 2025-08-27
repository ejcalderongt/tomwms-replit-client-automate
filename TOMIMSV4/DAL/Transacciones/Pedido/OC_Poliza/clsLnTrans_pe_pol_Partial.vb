Imports System.Data.SqlClient

Partial Public Class clsLnTrans_pe_pol

    Public Shared Function MaxID(ByVal pIdOrdenPedidoEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdOrdenPedidoPol),0) FROM trans_pe_pol WHERE IdOrdenPedidoEnc={0}", pIdOrdenPedidoEnc), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Inserta_Polizas(ByVal lConnection As SqlConnection,
                                           ByVal lTransaction As SqlTransaction,
                                           ByRef prg As ProgressBar,
                                           ByRef lblprg As RichTextBox) As Boolean

        Dim lPedidoConNumOrdenWMS As New List(Of clsBeTrans_pe_enc)
        Dim vRegistrosActualizados As Integer = 0
        Dim BePoliza As New clsBeTrans_pe_pol
        Dim BePolizaExistente As New clsBeTrans_pe_pol
        Dim BePolizaCEALSA As New clsBeTablas_relacionadas
        Dim vContadorProgress As Integer = 0

        Try

            lPedidoConNumOrdenWMS = clsLnTrans_pe_enc.Get_All_Pedidos_Con_NumOrden_SinPoliza(lConnection, lTransaction)

            vContadorProgress = 0
            prg.Value = 0
            prg.Minimum = 0
            prg.Maximum = lPedidoConNumOrdenWMS.Count

            For Each PedidoConNumOrden In lPedidoConNumOrdenWMS

                'If PedidoConNumOrden.IdPedidoEnc = 10126 Then
                '    Debug.Print(PedidoConNumOrden.IdPedidoEnc)
                'End If

                vRegistrosActualizados = 0

                BePoliza = New clsBeTrans_pe_pol

                BePolizaCEALSA = clsLnTablas_relacionadas.Get_By_IdPedidoEnc(PedidoConNumOrden.IdPedidoEnc, lConnection, lTransaction)

                If Not BePolizaCEALSA Is Nothing Then

                    BePoliza.IdOrdenPedidoPol = 1
                    BePoliza.IsNew = True
                    BePoliza.User_agr = "dts"
                    BePoliza.Fec_agr = Now
                    BePoliza.NoPoliza = BePolizaCEALSA.Copoliza
                    BePoliza.Pais_procede = ""
                    BePoliza.Total_valoraduana = 0
                    BePoliza.Total_bultos_Peso = 0
                    BePoliza.Total_flete = 0
                    BePoliza.Total_usd = 0
                    BePoliza.Dua = PedidoConNumOrden.No_Documento_Externo
                    BePoliza.Fecha_poliza = BePolizaCEALSA.Fecha_orden_entrega
                    BePoliza.Tipo_cambio = 0
                    BePoliza.Total_lineas = 1
                    BePoliza.Total_bultos = 0
                    BePoliza.Total_seguro = 0
                    BePoliza.User_mod = "dts"
                    BePoliza.Fec_mod = BePolizaCEALSA.Fecha_orden_entrega
                    BePoliza.IdRegimen = 17
                    BePoliza.codigo_poliza = ""
                    BePoliza.ticket = 0
                    BePoliza.numero_orden = PedidoConNumOrden.No_Documento_Externo
                    BePoliza.fecha_aceptacion = BePolizaCEALSA.Fecha_orden_entrega
                    BePoliza.fecha_llegada = BePolizaCEALSA.Fecha_orden_entrega
                    BePoliza.total_otros = 0
                    BePoliza.clave_aduana = ""
                    BePoliza.nit_imp_exp = ""
                    BePoliza.clase = "10"
                    BePoliza.mod_transporte = "3"
                    BePoliza.total_liquidar = 0
                    BePoliza.total_general = 0
                    BePoliza.IdOrdenPedidoEnc = PedidoConNumOrden.IdPedidoEnc

                    If PedidoConNumOrden.IdPedidoEnc = 7013 Then
                        Debug.WriteLine("aqui va el pedido")
                    End If

                    BePolizaExistente = GetSingle_By_IdPedido_IdPoliza(BePoliza.IdOrdenPedidoEnc, BePoliza.IdOrdenPedidoPol, lConnection, lTransaction)

                    If BePolizaExistente Is Nothing Then

                        Insertar(BePoliza, lConnection, lTransaction)

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Se insertó póliza con nùmero de orden: " & PedidoConNumOrden.No_Documento_Externo & " IdPedidoEnc: " & PedidoConNumOrden.IdPedidoEnc)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                End If

                vContadorProgress += 1
                prg.Value = vContadorProgress

                Application.DoEvents()

            Next

        Catch ex As Exception

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Ocurrió un error al insertar pólizas: " & ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw ex

        End Try
    End Function

    Public Shared Function Actualiza_Polizas(ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction,
                                             ByRef prg As ProgressBar,
                                             ByRef lblprg As RichTextBox) As Boolean

        Dim lTempPePolConPedido As New List(Of clsBeTmp_trans_pe_pol)
        Dim vRegistrosActualizados As Integer = 0
        Dim BePolizaPeEnc As New clsBeTrans_pe_pol
        Dim vContadorProgress As Integer = 0

        Try

            lTempPePolConPedido = clsLnTmp_trans_pe_pol.Get_All_Con_Pedido(lConnection, lTransaction)

            vContadorProgress = 0
            prg.Value = 0
            prg.Minimum = 0
            prg.Maximum = lTempPePolConPedido.Count

            For Each PolizaConPedido In lTempPePolConPedido

                vRegistrosActualizados = 0

                BePolizaPeEnc = New clsBeTrans_pe_pol

                BePolizaPeEnc = GetSingleId(PolizaConPedido.IdOrdenPedidoEnc,
                                            lConnection,
                                            lTransaction)

                If Not BePolizaPeEnc Is Nothing Then

                    BePolizaPeEnc.IsNew = False
                    BePolizaPeEnc.User_agr = "MI3"
                    BePolizaPeEnc.Fec_agr = Now
                    BePolizaPeEnc.NoPoliza = PolizaConPedido.NoPoliza
                    BePolizaPeEnc.Pais_procede = PolizaConPedido.Pais_procede
                    BePolizaPeEnc.Total_valoraduana = PolizaConPedido.Total_valoraduana
                    BePolizaPeEnc.Total_bultos_Peso = PolizaConPedido.Total_bultos_peso
                    BePolizaPeEnc.Total_flete = PolizaConPedido.Total_flete
                    BePolizaPeEnc.Total_usd = PolizaConPedido.Total_usd
                    BePolizaPeEnc.Dua = PolizaConPedido.Dua
                    BePolizaPeEnc.Fecha_poliza = PolizaConPedido.Fecha_poliza
                    BePolizaPeEnc.Tipo_cambio = PolizaConPedido.Tipo_cambio
                    BePolizaPeEnc.Total_lineas = PolizaConPedido.Total_lineas
                    BePolizaPeEnc.Total_bultos = PolizaConPedido.Total_bultos_peso
                    BePolizaPeEnc.Total_seguro = PolizaConPedido.Total_seguro
                    BePolizaPeEnc.User_mod = PolizaConPedido.User_mod
                    BePolizaPeEnc.Fec_mod = PolizaConPedido.Fec_mod
                    BePolizaPeEnc.IdRegimen = PolizaConPedido.IdRegimen
                    BePolizaPeEnc.codigo_poliza = PolizaConPedido.Codigo_poliza
                    BePolizaPeEnc.ticket = PolizaConPedido.Ticket
                    BePolizaPeEnc.numero_orden = PolizaConPedido.Numero_orden
                    BePolizaPeEnc.fecha_aceptacion = PolizaConPedido.Fecha_aceptacion
                    BePolizaPeEnc.fecha_llegada = PolizaConPedido.Fecha_llegada
                    BePolizaPeEnc.total_otros = PolizaConPedido.Total_otros
                    BePolizaPeEnc.clave_aduana = PolizaConPedido.Clave_aduana
                    BePolizaPeEnc.nit_imp_exp = PolizaConPedido.Nit_imp_exp
                    BePolizaPeEnc.clase = PolizaConPedido.Clase
                    BePolizaPeEnc.mod_transporte = PolizaConPedido.Mod_transporte
                    BePolizaPeEnc.total_liquidar = PolizaConPedido.Total_liquidar
                    BePolizaPeEnc.total_general = PolizaConPedido.Total_general
                    BePolizaPeEnc.IdOrdenPedidoEnc = PolizaConPedido.IdOrdenPedidoEnc

                    Actualizar(BePolizaPeEnc, lConnection, lTransaction)

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("Póliza: " & BePolizaPeEnc.numero_orden & " actualizada para el IdPedidoEnc: " & PolizaConPedido.IdOrdenPedidoEnc)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

                vContadorProgress += 1
                prg.Value = vContadorProgress

                Application.DoEvents()

            Next

        Catch ex As Exception

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Ocurrió un error al insertar pólizas: " & ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw ex

        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdOrdenPedidoEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = String.Format("SELECT ISNULL(Max(IdOrdenPedidoPol),0) FROM trans_pe_pol WHERE IdOrdenPedidoEnc={0}", pIdOrdenPedidoEnc)

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_No_Orden(ByVal pNoOrden As String,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_pol

        Get_Single_By_No_Orden = Nothing

        Try

            Const sp As String = "SELECT * FROM trans_pe_pol WHERE numero_orden = @numero_orden "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTmp_trans_pe_pol As New clsBeTrans_pe_pol

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
                    Get_Single_By_No_Orden = vBeTmp_trans_pe_pol
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_No_Orden(ByVal pNoOrden As String) As clsBeTrans_pe_pol

        Get_Single_By_No_Orden = Nothing

        Try

            Const sp As String = "SELECT * FROM trans_pe_pol WHERE numero_orden = @numero_orden "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(sp, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeTmp_trans_pe_pol As New clsBeTrans_pe_pol

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
                        Get_Single_By_No_Orden = vBeTmp_trans_pe_pol
                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Polizas_Cero(ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As Boolean

        Dim vRegistrosEliminados As Integer = 0
        Dim vSQL As String

        Try
            vSQL = "DELETE FROM trans_pe_pol WHERE NoPoliza = '0'"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosEliminados = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception

            Throw ex

        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietarioBodega_And_IdBodega(ByVal IdPropietarioBodega As Integer,
                                                                       ByVal IdBodega As Integer) As DataTable

        Get_All_By_IdPropietarioBodega_And_IdBodega = Nothing

        Try
            Dim listPolizas As New clsBeTrans_oc_pol

            Dim vSQL As String = " select pe_pol.IdOrdenPedidoEnc,pe_pol.numero_orden,pe_pol.codigo_poliza 
                                          from trans_pe_pol pe_pol inner join trans_pe_enc pe_enc on 
                                               pe_pol.IdOrdenPedidoEnc=pe_enc.IdPedidoEnc	
                                          where IdPropietarioBodega=@IdPropietarioBodega 
                                                and pe_enc.IdBodega = @IdBodega "

            'vSQL += " ORDER BY IdOrdenCompraPol "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdPropietarioBodega_And_IdBodega = lDataTable

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


    '#GT17062024: listar todos los pedidos con el mismo numero_orden
    Public Shared Function Get_All_By_Numero_Orden(ByVal pNumero_Orden As String) As List(Of clsBeTrans_pe_pol)

        Get_All_By_Numero_Orden = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_pe_pol WHERE Numero_Orden=@Numero_Orden  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNumero_Orden)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Get_All_By_Numero_Orden = New List(Of clsBeTrans_pe_pol)

                            For i As Integer = 0 To lDT.Rows.Count - 1

                                Dim lRow As DataRow = lDT.Rows(i)
                                Dim Obj As New clsBeTrans_pe_pol()
                                Cargar(Obj, lRow)
                                Obj.IsNew = False
                                Get_All_By_Numero_Orden.Add(Obj)
                            Next

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


    Public Shared Function Anular_poliza(ByRef oBeTrans_pe_pol As clsBeTrans_pe_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_pol")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdOrdenPedidoEnc = @IdOrdenPedidoEnc and numero_orden=@numero_orden ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_pe_pol.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTrans_pe_pol.numero_orden))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_pol.activo))

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

End Class
