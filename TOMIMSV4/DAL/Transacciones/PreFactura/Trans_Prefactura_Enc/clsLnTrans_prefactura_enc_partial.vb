Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_prefactura_enc

    Public Shared Function Guardar_Datos(ByVal BeTrans_prefactura_enc As clsBeTrans_prefactura_enc,
                                         ByVal pListPrefactura_det As List(Of clsBeTrans_prefactura_det),
                                         ByVal pListPrefactura_mov As List(Of clsBeTrans_prefactura_mov)) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Guarda_Trans_Enc(BeTrans_prefactura_enc, lConnection, lTransaction)

            Guarda_Trans_Det(BeTrans_prefactura_enc.IdTransPrefacturaEnc, pListPrefactura_det, lConnection, lTransaction)

            Guardar_Trans_Mov(BeTrans_prefactura_enc.IdTransPrefacturaEnc, pListPrefactura_mov, lConnection, lTransaction)

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()

        End Try
        '
    End Function

    Private Shared Sub Guardar_Trans_Mov(ByVal pIdPrefacturaEnc As Integer, ByRef pListPrefacturaMov As List(Of clsBeTrans_prefactura_mov),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)
        Try

            If pListPrefacturaMov IsNot Nothing Then
                Dim lMax As Integer = clsLnTrans_prefactura_mov.MaxID(pIdPrefacturaEnc, lConnection, lTransaction)
                For Each movimiento As clsBeTrans_prefactura_mov In pListPrefacturaMov

                    lMax += 1
                    movimiento.Idtransprefacturamov = lMax
                    movimiento.IdTransPrefacturaEnc = pIdPrefacturaEnc
                    clsLnTrans_prefactura_mov.Insertar(movimiento, lConnection, lTransaction)

                Next
            End If
        Catch ex As Exception
            'Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            Dim vMsgError As String = String.Format("Error_07012025_PrefacturaMov: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Function Guarda_Trans_Det(ByVal pIdPrefacturaEnc As Integer, ByRef pListPrefacturaDet As List(Of clsBeTrans_prefactura_det),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Boolean

        Try

            If pListPrefacturaDet IsNot Nothing Then

                Dim lMax As Integer = clsLnTrans_prefactura_det.MaxID(pIdPrefacturaEnc, lConnection, lTransaction)

                For Each Obj As clsBeTrans_prefactura_det In pListPrefacturaDet

                    lMax += 1
                    Obj.IdTransPrefacturaDet = lMax
                    Obj.IdTransPrefacturaEnc = pIdPrefacturaEnc
                    clsLnTrans_prefactura_det.Insertar(Obj, lConnection, lTransaction)

                Next

            End If

        Catch ex As Exception
            'Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            Dim vMsgError As String = String.Format("Error_07012025_PrefacturaDet: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try


    End Function

    Public Shared Function Guarda_Trans_Enc(ByRef pObjPrefacturaEnc As clsBeTrans_prefactura_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Boolean

        Guarda_Trans_Enc = False

        Try

            If pObjPrefacturaEnc.IsNew Then
                pObjPrefacturaEnc.IdTransPrefacturaEnc = MaxID(lConnection, lTransaction) + 1
                pObjPrefacturaEnc.IsNew = False
                pObjPrefacturaEnc.Observacion = "Ref_wms:" & pObjPrefacturaEnc.IdTransPrefacturaEnc & ".  " & pObjPrefacturaEnc.Observacion
                Insertar(pObjPrefacturaEnc,
                         lConnection,
                         lTransaction)

            Else
                Actualizar(pObjPrefacturaEnc,
                           lConnection,
                           lTransaction)
            End If

            Guarda_Trans_Enc = True

        Catch ex As Exception
            'Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            Dim vMsgError As String = String.Format("Error_07012025_PrefacturaEnc: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw ex
        End Try

    End Function


    Public Shared Function Listar_Movimientos(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "select *from VW_Get_Lista_PrefacturasEnc where 1=1 "

            sp += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) & " AND " & FormatoFechas.fFecha(pFechaAl)

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT28082024: si prefactura se envia correctamente al ERP, actualizar estado de procesado.
    Public Shared Function Actualizar_by_Proceso_ERP(ByRef oBeTrans_prefactura_erp As clsBeTrans_prefactura_erp, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_prefactura_enc")
            Upd.Add("procesado_erp", "@procesado_erp", DataType.Parametro)
            Upd.Add("IdTipoCuenta", "@IdTipoCuenta", DataType.Parametro)
            Upd.Where("IdTransPrefacturaEnc = @IdTransPrefacturaEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_erp.IdPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_ERP", 1))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUENTA", 1))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("Error_07012025_PrefacturaEnc_UpdateByERP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    '#GT28082024: cambiar estado a anulado en la prefactura.
    Public Shared Function Anular_Prefactura_By_IdTransPrefacturaEnc(ByVal IdPrefacturaEnc As Integer,
                                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Anular_Prefactura_By_IdTransPrefacturaEnc = False

        Try

            Upd.Init("trans_prefactura_enc")
            Upd.Add("anulada", "@anulada", DataType.Parametro)
            Upd.Where("IdTransPrefacturaEnc = @IdTransPrefacturaEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", IdPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@anulada", 1))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()


            Anular_Prefactura_By_IdTransPrefacturaEnc = IIf(rowsAffected > 0, 1, 0)

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            'Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    '#GT09082024: insertar prefactura en el ERP
    Public Shared Function Insertar_Prefactura_ERP(ByVal pPrefacturaERP As clsBeTrans_prefactura_erp, ByVal pTimeOut As Integer, ByVal pUsuario As String) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand
        Insertar_Prefactura_ERP = False
        Dim returnValue As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Dim pTimeOut = clsBD.Instancia.TimeOutConBD

            'lblPrg.AppendText(vbNewLine)
            'lblPrg.AppendText("Ejecutando SP para insertar desfase...")
            'lblPrg.AppendText(vbNewLine)
            'lblPrg.Refresh()
            'lblPrg.SelectionStart = lblPrg.TextLength
            'lblPrg.ScrollToCaret()

            '#GT09082024: variables para enviar prefactura a SP
            Dim PrefacturaDetalle = New DataTable
            PrefacturaDetalle.Columns.Add("correlativo_detalleacuerdo", GetType(Integer))
            PrefacturaDetalle.Columns.Add("codigoproducto", GetType(String))
            PrefacturaDetalle.Columns.Add("dias", GetType(Integer))
            PrefacturaDetalle.Columns.Add("monto", GetType(Double))

            For Each Detalle In pPrefacturaERP.Detalle
                PrefacturaDetalle.Rows.Add(Detalle.corre_cbdetacuerdosservicios, Detalle.codigoproducto, Detalle.dias, Detalle.monto)
            Next

            Dim vSQL As String = "sp_cortes_integrados"
            cmd = New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.StoredProcedure}
            cmd.CommandTimeout = pTimeOut

            '#GT Encabezado
            cmd.Parameters.AddWithValue("@corre_cbmaeacuerdosservicios", SqlDbType.Int).Value = pPrefacturaERP.Codigo_acuerdo
            cmd.Parameters.AddWithValue("@periodo", SqlDbType.NVarChar).Value = pPrefacturaERP.Periodo
            cmd.Parameters.AddWithValue("@mercaderia", SqlDbType.NVarChar).Value = pPrefacturaERP.Mercaderia
            cmd.Parameters.AddWithValue("@tipo_cambio", SqlDbType.Money).Value = pPrefacturaERP.TipoCambio
            cmd.Parameters.AddWithValue("@codigo_cliente_enviado", SqlDbType.Int).Value = pPrefacturaERP.IdCliente
            cmd.Parameters.AddWithValue("@codigo_cliente_facturacion_enviado", SqlDbType.NVarChar).Value = pPrefacturaERP.IdCliente_facturar
            cmd.Parameters.AddWithValue("@nit_facturacion_enviado", SqlDbType.NVarChar).Value = pPrefacturaERP.Nit
            '#GT Detalle
            cmd.Parameters.AddWithValue("@detalle", SqlDbType.Structured).Value = PrefacturaDetalle


            returnValue = cmd.ExecuteNonQuery()

            Dim vMsgError As String = String.Format("sp_cortes_integrados ejecutado por " & pUsuario & " Prefactura:" & pPrefacturaERP.IdPrefacturaEnc & " en " & Now & " con respuesta: " & returnValue)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Insertar_Prefactura_ERP = True

            If returnValue < 0 Then
                Throw New Exception("Aritec-sp_cortes_integrados retorna valor " & returnValue)
            End If
            '    Insertar_Prefactura_ERP = True

            'Else

            'End If

            'cmd.Parameters.Add("@PrefacturaDetalle", SqlDbType.Structured)
            'cmd.Parameters("@RegistrosARevisar").Direction = ParameterDirection.Output
            'If pTimeOut > 0 Then
            '    cmd.CommandTimeout = pTimeOut
            'End If

            'returnValue = cmd.ExecuteNonQuery()

            'If returnValue > 0 Then
            '    Insertar_Prefactura_ERP = True
            'Else

            'End If


            'If (returnValue > 0) Then

            '    lblPrg.AppendText(vbNewLine)
            '    lblPrg.AppendText("Aviso: El proceso histórico con retroactivo faltante agregó " & returnValue & " registros.")
            '    lblPrg.AppendText(vbNewLine)
            '    lblPrg.Refresh()
            '    lblPrg.SelectionStart = lblPrg.TextLength
            '    lblPrg.ScrollToCaret()
            'Else
            '    lblPrg.AppendText(vbNewLine)
            '    lblPrg.AppendText("Error: El proceso histórico con retroactivo no devolvió cuantos registros inserto!.")
            '    lblPrg.AppendText(vbNewLine)
            '    lblPrg.Refresh()
            '    lblPrg.SelectionStart = lblPrg.TextLength
            '    lblPrg.ScrollToCaret()

            'End If

            lTransaction.Commit()

        Catch ex As Exception
            Insertar_Prefactura_ERP = False
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Dim vMsgError As String = String.Format("Error_09082024_PreFacturaERP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Reporte_By_IdPrefactura(ByVal IdPrefactura As Integer) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Reporte_By_IdPrefactura = Nothing

        Try

            Dim sp As String = "select * from VW_RPT_PREFACTURA WHERE IdTransPrefacturaEnc=@IdTransPrefacturaEnc "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTransPrefacturaEnc", IdPrefactura)

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_Reporte_By_IdPrefactura = dt

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
