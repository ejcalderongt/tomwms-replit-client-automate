Imports System.Data.SqlClient

Public Class clsLnTrans_servicio_enc

    Public Shared Sub Cargar(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_servicio_enc

                .IdServicioEnc = IIf(IsDBNull(dr.Item("IdServicioEnc")), 0, dr.Item("IdServicioEnc"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .No_poliza = IIf(IsDBNull(dr.Item("no_poliza")), "", dr.Item("no_poliza"))
                .No_orden = IIf(IsDBNull(dr.Item("no_orden")), "", dr.Item("no_orden"))
                .Fecha_doc_ingreso = IIf(IsDBNull(dr.Item("fecha_doc_ingreso")), Date.Now, dr.Item("fecha_doc_ingreso"))
                .Fecha_servicio = IIf(IsDBNull(dr.Item("fecha_servicio")), Date.Now, dr.Item("fecha_servicio"))
                .Enviado_a_erp = IIf(IsDBNull(dr.Item("enviado_a_erp")), False, dr.Item("enviado_a_erp"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "Nuevo", dr.Item("estado"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Es_Ingreso = IIf(IsDBNull(dr.Item("Es_Ingreso")), False, dr.Item("Es_Ingreso"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_servicio_enc")
            Ins.Add("idservicioenc", "@idservicioenc", DataType.Parametro)
            If Not oBeTrans_servicio_enc.IdOrdenCompraEnc = 0 Then Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("no_orden", "@no_orden", DataType.Parametro)
            Ins.Add("fecha_doc_ingreso", "@fecha_doc_ingreso", DataType.Parametro)
            Ins.Add("fecha_servicio", "@fecha_servicio", DataType.Parametro)
            Ins.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            If Not oBeTrans_servicio_enc.IdPedidoEnc = 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("es_ingreso", "@es_ingreso", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_enc.IdServicioEnc))
            If Not oBeTrans_servicio_enc.IdOrdenCompraEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_servicio_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_servicio_enc.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_servicio_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeTrans_servicio_enc.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@NO_ORDEN", oBeTrans_servicio_enc.No_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DOC_INGRESO", oBeTrans_servicio_enc.Fecha_doc_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SERVICIO", oBeTrans_servicio_enc.Fecha_servicio))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_servicio_enc.Enviado_a_erp))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_servicio_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_servicio_enc.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_servicio_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_servicio_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_servicio_enc.Estado))
            If Not oBeTrans_servicio_enc.IdPedidoEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_servicio_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ES_INGRESO", oBeTrans_servicio_enc.Es_Ingreso))

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

    Public Shared Function Actualizar(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_servicio_enc")
            Upd.Add("idservicioenc", "@idservicioenc", DataType.Parametro)
            If Not oBeTrans_servicio_enc.IdOrdenCompraEnc = 0 Then Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Upd.Add("no_orden", "@no_orden", DataType.Parametro)
            Upd.Add("fecha_doc_ingreso", "@fecha_doc_ingreso", DataType.Parametro)
            Upd.Add("fecha_servicio", "@fecha_servicio", DataType.Parametro)
            Upd.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("es_ingreso", "@es_ingreso", DataType.Parametro)
            Upd.Where("IdServicioEnc = @IdServicioEnc")

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
            If Not oBeTrans_servicio_enc.IdOrdenCompraEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_servicio_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_servicio_enc.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_servicio_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeTrans_servicio_enc.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@NO_ORDEN", oBeTrans_servicio_enc.No_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DOC_INGRESO", oBeTrans_servicio_enc.Fecha_doc_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SERVICIO", oBeTrans_servicio_enc.Fecha_servicio))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_servicio_enc.Enviado_a_erp))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_servicio_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_servicio_enc.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_servicio_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_servicio_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_servicio_enc.Estado))
            'GT 30082021 2203: el valor puede ser 0, porque si es entrada no hay valor de pedido y seguro pasara con IdcompraEnc
            'If Not oBeTrans_servicio_enc.IdPedidoEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_servicio_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_servicio_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ES_INGRESO", oBeTrans_servicio_enc.Es_Ingreso))

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


    Public Shared Function Eliminar(ByRef oBeTrans_servicio_enc As clsBeTrans_servicio_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_servicio_enc" &
             "  Where(IdServicioEnc = @IdServicioEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_enc.IdServicioEnc))

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

            Const sp As String = "SELECT * FROM Trans_servicio_enc"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_servicio_enc)

        Dim lReturnList As New List(Of clsBeTrans_servicio_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_enc As New clsBeTrans_servicio_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_servicio_enc = New clsBeTrans_servicio_enc()
                            vBeTrans_servicio_enc.IsNew = False
                            Cargar(vBeTrans_servicio_enc, dr)
                            lReturnList.Add(vBeTrans_servicio_enc)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_servicio_enc As clsBeTrans_servicio_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_enc" &
            " Where(IdServicioEnc = @IdServicioEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdServicioEnc", pBeTrans_servicio_enc.IdServicioEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_enc As New clsBeTrans_servicio_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_servicio_enc, lDataTable.Rows(0))
                            pBeTrans_servicio_enc = New clsBeTrans_servicio_enc()
                            pBeTrans_servicio_enc.IsNew = False
                            pBeTrans_servicio_enc = vBeTrans_servicio_enc
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

    Public Shared Function Get_Servicios_By_NoOrden_And_Rango_Fechas(pNoOrden As String, fechaDesde As Date, fechaHasta As Date) As List(Of clsBeStock_jornada_logistico)
        Throw New NotImplementedException()
    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdServicioEnc),0) FROM Trans_servicio_enc"

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

    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date,
                                  ByVal pFechaAl As Date,
                                  Optional ByVal pIdBodega As Integer = 0,
                                  Optional ByVal pIdPropietario As Integer = 0) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Trans_Servicios WHERE 1 > 0 "

            If pActivo = True Then
                vSQL += " And Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += " AND cast(Fecha AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega=@IdBodega"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND IdPropietario=@IdPropietario"
            End If

            If pIdBodega <> 0 AndAlso pIdPropietario <> 0 Then
                vSQL += " AND Estado IN ('NUEVA','BACK ORDER')"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        If pIdBodega <> 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        If pIdPropietario <> 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
