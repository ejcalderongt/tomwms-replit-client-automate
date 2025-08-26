Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_prefactura_det

    Public Shared Sub Cargar(ByRef oBeTrans_prefactura_det As clsBeTrans_prefactura_det, ByRef dr As DataRow)
        Try
            With oBeTrans_prefactura_det
                .IdTransPrefacturaDet = IIf(IsDBNull(dr.Item("IdTransPrefacturaDet")), 0, dr.Item("IdTransPrefacturaDet"))
                .IdTransPrefacturaEnc = IIf(IsDBNull(dr.Item("IdTransPrefacturaEnc")), 0, dr.Item("IdTransPrefacturaEnc"))
                .IdAcuerdoEnc = IIf(IsDBNull(dr.Item("IdAcuerdoEnc")), 0, dr.Item("IdAcuerdoEnc"))
                .Codigo_acuerdo_enc = IIf(IsDBNull(dr.Item("codigo_acuerdo_enc")), 0, dr.Item("codigo_acuerdo_enc"))
                .Codigo_producto_acuerdo_det = IIf(IsDBNull(dr.Item("codigo_producto_acuerdo_det")), "", dr.Item("codigo_producto_acuerdo_det"))
                .IdAcuerdoDet = IIf(IsDBNull(dr.Item("IdAcuerdoDet")), 0, dr.Item("IdAcuerdoDet"))
                .Correlativo_detalle_acuerdo = IIf(IsDBNull(dr.Item("correlativo_detalle_acuerdo")), 0, dr.Item("correlativo_detalle_acuerdo"))
                .Numero_unidades_acuerdo_det = IIf(IsDBNull(dr.Item("numero_unidades_acuerdo_det")), 0, dr.Item("numero_unidades_acuerdo_det"))
                .Servicio = IIf(IsDBNull(dr.Item("servicio")), 0.0, dr.Item("servicio"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), 0.0, dr.Item("descripcion"))
                .Monto = IIf(IsDBNull(dr.Item("monto")), 0.0, dr.Item("monto"))
                .Porcentaje = IIf(IsDBNull(dr.Item("porcentaje")), 0.0, dr.Item("porcentaje"))
                .Dias_eventos = IIf(IsDBNull(dr.Item("dias_eventos")), 0, dr.Item("dias_eventos"))
                .Valor = IIf(IsDBNull(dr.Item("valor")), 0.0, dr.Item("valor"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Monto_Erp = IIf(IsDBNull(dr.Item("monto_erp")), 0.0, dr.Item("monto_erp"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_prefactura_det As clsBeTrans_prefactura_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_prefactura_det")
            Ins.Add("idtransprefacturadet", "@idtransprefacturadet", DataType.Parametro)
            Ins.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)
            Ins.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Ins.Add("codigo_acuerdo_enc", "@codigo_acuerdo_enc", DataType.Parametro)
            Ins.Add("codigo_producto_acuerdo_det", "@codigo_producto_acuerdo_det", DataType.Parametro)
            Ins.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Ins.Add("correlativo_detalle_acuerdo", "@correlativo_detalle_acuerdo", DataType.Parametro)
            Ins.Add("numero_unidades_acuerdo_det", "@numero_unidades_acuerdo_det", DataType.Parametro)
            Ins.Add("servicio", "@servicio", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("monto", "@monto", DataType.Parametro)
            Ins.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Ins.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Ins.Add("valor", "@valor", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("monto_erp", "@monto_erp", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURADET", oBeTrans_prefactura_det.IdTransPrefacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_det.IdTransPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_prefactura_det.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO_ENC", oBeTrans_prefactura_det.Codigo_acuerdo_enc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO_ACUERDO_DET", oBeTrans_prefactura_det.Codigo_producto_acuerdo_det))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_prefactura_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_DETALLE_ACUERDO", oBeTrans_prefactura_det.Correlativo_detalle_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES_ACUERDO_DET", oBeTrans_prefactura_det.Numero_unidades_acuerdo_det))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeTrans_prefactura_det.Servicio))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_prefactura_det.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeTrans_prefactura_det.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeTrans_prefactura_det.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeTrans_prefactura_det.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@VALOR", oBeTrans_prefactura_det.Valor))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_prefactura_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@MONTO_ERP", oBeTrans_prefactura_det.Monto_Erp))


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

    Public Shared Function Actualizar(ByRef oBeTrans_prefactura_det As clsBeTrans_prefactura_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_prefactura_det")
            Upd.Add("idtransprefacturadet", "@idtransprefacturadet", DataType.Parametro)
            Upd.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)
            Upd.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Upd.Add("codigo_acuerdo_enc", "@codigo_acuerdo_enc", DataType.Parametro)
            Upd.Add("codigo_producto_acuerdo_det", "@codigo_producto_acuerdo_det", DataType.Parametro)
            Upd.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Upd.Add("correlativo_detalle_acuerdo", "@correlativo_detalle_acuerdo", DataType.Parametro)
            Upd.Add("numero_unidades_acuerdo_det", "@numero_unidades_acuerdo_det", DataType.Parametro)
            Upd.Add("servicio", "@servicio", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("monto", "@monto", DataType.Parametro)
            Upd.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Upd.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Upd.Add("valor", "@valor", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdTransPrefacturaDet = @IdTransPrefacturaDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURADET", oBeTrans_prefactura_det.IdTransPrefacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_det.IdTransPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_prefactura_det.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO_ENC", oBeTrans_prefactura_det.Codigo_acuerdo_enc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO_ACUERDO_DET", oBeTrans_prefactura_det.Codigo_producto_acuerdo_det))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_prefactura_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_DETALLE_ACUERDO", oBeTrans_prefactura_det.Correlativo_detalle_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES_ACUERDO_DET", oBeTrans_prefactura_det.Numero_unidades_acuerdo_det))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeTrans_prefactura_det.Servicio))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_prefactura_det.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeTrans_prefactura_det.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeTrans_prefactura_det.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeTrans_prefactura_det.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@VALOR", oBeTrans_prefactura_det.Valor))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_prefactura_det.Activo))

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

    Public Shared Function Eliminar(ByRef oBeTrans_prefactura_det As clsBeTrans_prefactura_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_prefactura_det" & _
             "  Where(IdTransPrefacturaDet = @IdTransPrefacturaDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURADET", oBeTrans_prefactura_det.IdTransPrefacturaDet))

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

            Const sp As String = "SELECT * FROM Trans_prefactura_det"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_prefactura_det)

        Dim lReturnList As New List(Of clsBeTrans_prefactura_det)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_prefactura_det As New clsBeTrans_prefactura_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_prefactura_det = New clsBeTrans_prefactura_det()
                            Cargar(vBeTrans_prefactura_det, dr)
                            lReturnList.Add(vBeTrans_prefactura_det)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_prefactura_det As clsBeTrans_prefactura_det)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_det" & _
            " Where(IdTransPrefacturaDet = @IdTransPrefacturaDet)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_prefactura_det As New clsBeTrans_prefactura_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_prefactura_det, lDataTable.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransPrefacturaDet),0) FROM Trans_prefactura_det"

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

    Public Shared Function MaxID(ByVal pIdTransPrefacturaEnc As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdTransPrefacturaDet),0) FROM Trans_prefactura_det where IdTransPrefacturaEnc={0}", pIdTransPrefacturaEnc)

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

    Public Shared Function Get_All_By_IdPreFacturaEnc(ByVal pIdTransPrefacturaEnc As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_prefactura_det)

        Get_All_By_IdPreFacturaEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_det Where(IdTransPrefacturaEnc = @IdTransPrefacturaEnc) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IDTRANSPREFACTURAENC", pIdTransPrefacturaEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    Get_All_By_IdPreFacturaEnc = New List(Of clsBeTrans_prefactura_det)
                    Dim vBePreFacturaDet As New clsBeTrans_prefactura_det

                    For Each dr As DataRow In lDataTable.Rows
                        vBePreFacturaDet = New clsBeTrans_prefactura_det()
                        Cargar(vBePreFacturaDet, dr)
                        Get_All_By_IdPreFacturaEnc.Add(vBePreFacturaDet)
                    Next

                    Return Get_All_By_IdPreFacturaEnc

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT06062024: validar si un acuerdo ya esta en uso con una prefactura, para no modificar sus parametros.
    Public Shared Function Exist_By_IdCorrelativo_And_Codigo_Acuerdo(ByRef pBeTrans_acuerdocomercial_det As clsBeTrans_acuerdoscomerciales_det) As Boolean

        Exist_By_IdCorrelativo_And_Codigo_Acuerdo = False

        Try

            Const sp As String = "select TOP 1 idbodega,det.codigo_acuerdo_enc,correlativo_detalle_acuerdo from trans_prefactura_enc fac 
									     left outer join trans_prefactura_det det on fac.IdTransPrefacturaEnc=det.IdTransPrefacturaEnc
										 where correlativo_detalle_acuerdo=@correlativo_detalleacuerdo and codigo_acuerdo_enc=@codigo_acuerdo and anulada=0 "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CORRELATIVO_DETALLEACUERDO", pBeTrans_acuerdocomercial_det.Correlativo_detalleacuerdo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pBeTrans_acuerdocomercial_det.Codigo_acuerdo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Exist_By_IdCorrelativo_And_Codigo_Acuerdo = (lDT.Rows.Count > 0)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
