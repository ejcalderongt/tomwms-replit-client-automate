Imports System.Data.SqlClient
Public Class clsLnTrans_pe_det_log_reserva

    Public Shared Sub Cargar(ByRef oBeTrans_pe_det_log_reserva As clsBeTrans_pe_det_log_reserva, ByRef dr As DataRow)
        Try
            With oBeTrans_pe_det_log_reserva
                .IdLogReserva = IIf(IsDBNull(dr.Item("IdLogReserva")), 0, dr.Item("IdLogReserva"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), Date.Now, dr.Item("Fecha"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Line_No = IIf(IsDBNull(dr.Item("Line_No")), 0, dr.Item("Line_No"))
                .Item_No = IIf(IsDBNull(dr.Item("Item_No")), "", dr.Item("Item_No"))
                .UmBas = IIf(IsDBNull(dr.Item("UmBas")), "", dr.Item("UmBas"))
                .Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), "", dr.Item("Variant_Code"))
                .MensajeLog = IIf(IsDBNull(dr.Item("MensajeLog")), "", dr.Item("MensajeLog"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Caso_Reserva = IIf(IsDBNull(dr.Item("Caso_Reserva")), "", dr.Item("Caso_Reserva"))
                .EsError = IIf(IsDBNull(dr.Item("EsError")), False, dr.Item("EsError"))
                .Referencia_Documento = IIf(IsDBNull(dr.Item("Referencia_Documento")), "", dr.Item("Referencia_Documento"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), New Date(1900, 1, 1), dr.Item("Fecha_Vence"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_det_log_reserva As clsBeTrans_pe_det_log_reserva, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_det_log_reserva")
            Ins.Add("idlogreserva", "@idlogreserva", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("line_no", "@line_no", DataType.Parametro)
            Ins.Add("item_no", "@item_no", DataType.Parametro)
            Ins.Add("umbas", "@umbas", DataType.Parametro)
            Ins.Add("variant_code", "@variant_code", DataType.Parametro)
            Ins.Add("mensajelog", "@mensajelog", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("caso_reserva", "@caso_reserva", DataType.Parametro)
            Ins.Add("eserror", "@eserror", DataType.Parametro)
            Ins.Add("referencia_documento", "@referencia_documento", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idstockres", "@idstockres", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGRESERVA", oBeTrans_pe_det_log_reserva.IdLogReserva))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_pe_det_log_reserva.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_pe_det_log_reserva.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_det_log_reserva.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeTrans_pe_det_log_reserva.Line_No))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeTrans_pe_det_log_reserva.Item_No))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_pe_det_log_reserva.UmBas))
            cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeTrans_pe_det_log_reserva.Variant_Code))
            cmd.Parameters.Add(New SqlParameter("@MENSAJELOG", oBeTrans_pe_det_log_reserva.MensajeLog))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_det_log_reserva.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CASO_RESERVA", oBeTrans_pe_det_log_reserva.Caso_Reserva))
            cmd.Parameters.Add(New SqlParameter("@ESERROR", oBeTrans_pe_det_log_reserva.EsError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO", oBeTrans_pe_det_log_reserva.Referencia_Documento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_pe_det_log_reserva.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det_log_reserva.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_pe_det_log_reserva.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_pe_det_log_reserva.IdStockRes))

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

    Public Shared Function Actualizar(ByRef oBeTrans_pe_det_log_reserva As clsBeTrans_pe_det_log_reserva, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_det_log_reserva")
            Upd.Add("idlogreserva", "@idlogreserva", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("line_no", "@line_no", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("umbas", "@umbas", DataType.Parametro)
            Upd.Add("variant_code", "@variant_code", DataType.Parametro)
            Upd.Add("mensajelog", "@mensajelog", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("caso_reserva", "@caso_reserva", DataType.Parametro)
            Upd.Add("eserror", "@eserror", DataType.Parametro)
            Upd.Add("referencia_documento", "@referencia_documento", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idstockres", "@idstockres", DataType.Parametro)
            Upd.Where("IdLogReserva = @IdLogReserva")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGRESERVA", oBeTrans_pe_det_log_reserva.IdLogReserva))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_pe_det_log_reserva.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_pe_det_log_reserva.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_det_log_reserva.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeTrans_pe_det_log_reserva.Line_No))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeTrans_pe_det_log_reserva.Item_No))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_pe_det_log_reserva.UmBas))
            cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeTrans_pe_det_log_reserva.Variant_Code))
            cmd.Parameters.Add(New SqlParameter("@MENSAJELOG", oBeTrans_pe_det_log_reserva.MensajeLog))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_det_log_reserva.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CASO_RESERVA", oBeTrans_pe_det_log_reserva.Caso_Reserva))
            cmd.Parameters.Add(New SqlParameter("@ESERROR", oBeTrans_pe_det_log_reserva.EsError))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO", oBeTrans_pe_det_log_reserva.Referencia_Documento))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_pe_det_log_reserva.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det_log_reserva.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_pe_det_log_reserva.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_pe_det_log_reserva.IdStockRes))

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


    Public Shared Function Eliminar(ByRef oBeTrans_pe_det_log_reserva As clsBeTrans_pe_det_log_reserva, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_det_log_reserva" &
             "  Where(IdLogReserva = @IdLogReserva)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGRESERVA", oBeTrans_pe_det_log_reserva.IdLogReserva))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_pe_det_log_reserva)

        Dim lReturnList As New List(Of clsBeTrans_pe_det_log_reserva)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_det_log_reserva As New clsBeTrans_pe_det_log_reserva

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_pe_det_log_reserva = New clsBeTrans_pe_det_log_reserva()
                            Cargar(vBeTrans_pe_det_log_reserva, dr)
                            lReturnList.Add(vBeTrans_pe_det_log_reserva)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_pe_det_log_reserva As clsBeTrans_pe_det_log_reserva)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva" &
            " Where(IdLogReserva = @IdLogReserva)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_det_log_reserva As New clsBeTrans_pe_det_log_reserva

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_det_log_reserva, lDataTable.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLogReserva),0) FROM Trans_pe_det_log_reserva"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function Get_All_By_IdPedidoEnc_And_Idbodedga(ByVal pIdPedidoEnc As Integer, ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdPedidoEnc_And_Idbodedga = Nothing

        Try

            Const sp As String = "  SELECT trans_pe_det_log_reserva.IdLogReserva, trans_pe_det.IdPedidoEnc, trans_pe_det.IdPedidoDet,
                                    trans_pe_det.no_linea, trans_pe_det.codigo_producto, trans_pe_det.nombre_producto,
                                    trans_pe_det_log_reserva.Fecha, trans_pe_det_log_reserva.IdBodega,
                                    trans_pe_det_log_reserva.Line_No, trans_pe_det_log_reserva.Item_No,
                                    trans_pe_det_log_reserva.UmBas, trans_pe_det_log_reserva.Variant_Code,
                                    trans_pe_det_log_reserva.Cantidad, trans_pe_det_log_reserva.Caso_Reserva,
                                    trans_pe_det_log_reserva.EsError, trans_pe_det_log_reserva.Referencia_Documento,
                                    trans_pe_det_log_reserva.Fecha_Vence, trans_pe_det_log_reserva.IdStock,
                                    trans_pe_det_log_reserva.IdStockRes, trans_pe_det_log_reserva.MensajeLog
                                    FROM trans_pe_det_log_reserva RIGHT OUTER JOIN
                                    trans_pe_det ON trans_pe_det_log_reserva.IdPedidoDet = trans_pe_det.IdPedidoDet
                                    AND trans_pe_det_log_reserva.IdPedidoEnc = trans_pe_det.IdPedidoEnc
                                    WHERE trans_pe_det.IdPedidoEnc = @IdPedidoEnc AND trans_pe_det_log_reserva.IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_IdPedidoEnc_And_Idbodedga = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc_And_Idbodedga(ByVal pIdPedidoEnc As Integer,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdPedidoEnc_And_Idbodedga = Nothing

        Try

            Const sp As String = "  SELECT trans_pe_det_log_reserva.IdLogReserva, trans_pe_det.IdPedidoEnc, trans_pe_det.IdPedidoDet,
                                    trans_pe_det.no_linea, trans_pe_det.codigo_producto, trans_pe_det.nombre_producto,
                                    trans_pe_det_log_reserva.Fecha, trans_pe_det_log_reserva.IdBodega,
                                    trans_pe_det_log_reserva.Line_No, trans_pe_det_log_reserva.Item_No,
                                    trans_pe_det_log_reserva.UmBas, trans_pe_det_log_reserva.Variant_Code,
                                    trans_pe_det_log_reserva.Cantidad, trans_pe_det_log_reserva.Caso_Reserva,
                                    trans_pe_det_log_reserva.EsError, trans_pe_det_log_reserva.Referencia_Documento,
                                    trans_pe_det_log_reserva.Fecha_Vence, trans_pe_det_log_reserva.IdStock,
                                    trans_pe_det_log_reserva.IdStockRes, trans_pe_det_log_reserva.MensajeLog
                                    FROM trans_pe_det_log_reserva RIGHT OUTER JOIN
                                    trans_pe_det ON trans_pe_det_log_reserva.IdPedidoDet = trans_pe_det.IdPedidoDet
                                    AND trans_pe_det_log_reserva.IdPedidoEnc = trans_pe_det.IdPedidoEnc
                                    WHERE trans_pe_det.IdPedidoEnc = @IdPedidoEnc AND trans_pe_det_log_reserva.IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Get_All_By_IdPedidoEnc_And_Idbodedga = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
