Imports System.Data.SqlClient

Public Class clsLnTrans_packing_enc

    Public Shared Sub Cargar(ByRef oBeTrans_packing_enc As clsBeTrans_packing_enc, ByRef dr As DataRow, ByVal IsForAndr As Boolean)

        Try

            With oBeTrans_packing_enc

                .Idpackingenc = IIf(IsDBNull(dr.Item("idpackingenc")), 0, dr.Item("idpackingenc"))
                .Idbodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Idpickingenc = IIf(IsDBNull(dr.Item("idpickingenc")), 0, dr.Item("idpickingenc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("iddespachoenc")), 0, dr.Item("iddespachoenc"))
                .Idproductobodega = IIf(IsDBNull(dr.Item("idproductobodega")), 0, dr.Item("idproductobodega"))
                .Idproductoestado = IIf(IsDBNull(dr.Item("idproductoestado")), 0, dr.Item("idproductoestado"))
                .Idpresentacion = IIf(IsDBNull(dr.Item("idpresentacion")), 0, dr.Item("idpresentacion"))
                .Idunidadmedida = IIf(IsDBNull(dr.Item("idunidadmedida")), 0, dr.Item("idunidadmedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))

                If IsForAndr Then
                    Dim sf As String = Format(dr.Item("fecha_vence"), "yyyy-MM-dd") & "T00:00:00"
                    .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), sf)
                Else
                    .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                End If

                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .No_linea = IIf(IsDBNull(dr.Item("no_linea")), 0, dr.Item("no_linea"))
                .Cantidad_bultos_packing = IIf(IsDBNull(dr.Item("cantidad_bultos_packing")), 0.0, dr.Item("cantidad_bultos_packing"))
                .Cantidad_camas_packing = IIf(IsDBNull(dr.Item("cantidad_camas_packing")), 0.0, dr.Item("cantidad_camas_packing"))
                .Idoperadorbodega = IIf(IsDBNull(dr.Item("idoperadorbodega")), 0, dr.Item("idoperadorbodega"))
                .Idempresaservicio = IIf(IsDBNull(dr.Item("idempresaservicio")), 0, dr.Item("idempresaservicio"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Fecha_packing = IIf(IsDBNull(dr.Item("fecha_packing")), New Date(1900, 1, 1), dr.Item("fecha_packing"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Finalizado = IIf(IsDBNull(dr.Item("Finalizado")), 0, dr.Item("Finalizado"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), New Date(1900, 1, 1), dr.Item("fec_agr"))
                .Usr_agr = IIf(IsDBNull(dr.Item("usr_agr")), "", dr.Item("usr_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), New Date(1900, 1, 1), dr.Item("fec_mod"))
                .Usr_agr = IIf(IsDBNull(dr.Item("usr_mod")), "", dr.Item("usr_mod"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_packing_enc As clsBeTrans_packing_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_packing_enc")
            Ins.Add("idpackingenc", "@idpackingenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("cantidad_bultos_packing", "@cantidad_bultos_packing", DataType.Parametro)
            Ins.Add("cantidad_camas_packing", "@cantidad_camas_packing", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("idempresaservicio", "@idempresaservicio", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fecha_packing", "@fecha_packing", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("usr_mod", "@usr_mod", DataType.Parametro)
            Ins.Add("usr_agr", "@usr_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPACKINGENC", oBeTrans_packing_enc.Idpackingenc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_packing_enc.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_packing_enc.Idpickingenc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_packing_enc.Iddespachoenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_packing_enc.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_packing_enc.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_packing_enc.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_packing_enc.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_packing_enc.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_packing_enc.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_packing_enc.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_packing_enc.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_BULTOS_PACKING", oBeTrans_packing_enc.Cantidad_bultos_packing))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_CAMAS_PACKING", oBeTrans_packing_enc.Cantidad_camas_packing))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_packing_enc.Idoperadorbodega))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESASERVICIO", oBeTrans_packing_enc.Idempresaservicio))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_packing_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PACKING", oBeTrans_packing_enc.Fecha_packing))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_packing_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_packing_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USR_MOD", oBeTrans_packing_enc.Usr_mod))
            cmd.Parameters.Add(New SqlParameter("@USR_AGR", oBeTrans_packing_enc.Usr_agr))


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

    Public Shared Function Actualizar(ByRef oBeTrans_packing_enc As clsBeTrans_packing_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_packing_enc")
            Upd.Add("idpackingenc", "@idpackingenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("cantidad_bultos_packing", "@cantidad_bultos_packing", DataType.Parametro)
            Upd.Add("cantidad_camas_packing", "@cantidad_camas_packing", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("idempresaservicio", "@idempresaservicio", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("fecha_packing", "@fecha_packing", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Where("idpackingenc = @idpackingenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPACKINGENC", oBeTrans_packing_enc.Idpackingenc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_packing_enc.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_packing_enc.Idpickingenc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_packing_enc.Iddespachoenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_packing_enc.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_packing_enc.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_packing_enc.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_packing_enc.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_packing_enc.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_packing_enc.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_packing_enc.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_packing_enc.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_BULTOS_PACKING", oBeTrans_packing_enc.Cantidad_bultos_packing))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_CAMAS_PACKING", oBeTrans_packing_enc.Cantidad_camas_packing))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_packing_enc.Idoperadorbodega))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESASERVICIO", oBeTrans_packing_enc.Idempresaservicio))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_packing_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PACKING", oBeTrans_packing_enc.Fecha_packing))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_packing_enc.IdPedidoEnc))

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

    Public Shared Function Eliminar(ByRef oBeTrans_packing_enc As clsBeTrans_packing_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_packing_enc " &
             "  Where(idpackingenc = @idpackingenc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPACKINGENC", oBeTrans_packing_enc.Idpackingenc))

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

            Const sp As String = "SELECT * FROM Trans_packing_enc"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_packing_enc)

        Dim lReturnList As New List(Of clsBeTrans_packing_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_packing_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_packing_enc As New clsBeTrans_packing_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_packing_enc = New clsBeTrans_packing_enc()
                            Cargar(vBeTrans_packing_enc, dr, False)
                            lReturnList.Add(vBeTrans_packing_enc)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_packing_enc As clsBeTrans_packing_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_packing_enc" &
            " Where(idpackingenc = @idpackingenc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_packing_enc As New clsBeTrans_packing_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_packing_enc, lDataTable.Rows(0), False)
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

            Const sp As String = "SELECT ISNULL(Max(idpackingenc),0) FROM Trans_packing_enc"

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


    Public Shared Function Listar_PackingDespachado_By_RangoFechas(ByVal pFechaInicial As Date, ByVal pFechaFinal As Date, ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim dt As New DataTable

        Listar_PackingDespachado_By_RangoFechas = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_PackingDespachado
                                           WHERE fecha_packing between @FechaInicial and @FechaFinal and IdBodega=@IdBodega  "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@FechaInicial", pFechaInicial.Date))
            cmd.Parameters.Add(New SqlParameter("@FechaFinal", pFechaFinal.Date))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            Dim dad As New SqlDataAdapter(cmd)
            dad.Fill(dt)

            lTransaction.Commit()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Listar_PackingDespachado_By_RangoFechas = New DataTable
                Listar_PackingDespachado_By_RangoFechas = dt
            End If

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
