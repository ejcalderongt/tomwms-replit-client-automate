Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_prefactura_mov

    Public Shared Sub Cargar(ByRef oBeTrans_prefactura_mov As clsBeTrans_prefactura_mov, ByRef dr As DataRow)
        Try
            With oBeTrans_prefactura_mov
                .Idtransprefacturamov = IIf(IsDBNull(dr.Item("Idtransprefacturamov")), 0, dr.Item("Idtransprefacturamov"))
                .IdTransPrefacturaEnc = IIf(IsDBNull(dr.Item("IdTransPrefacturaEnc")), 0, dr.Item("IdTransPrefacturaEnc"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .Poliza_oc_numero_orden = IIf(IsDBNull(dr.Item("poliza_oc_numero_orden")), "", dr.Item("poliza_oc_numero_orden"))
                .Cantidad_tarimas = IIf(IsDBNull(dr.Item("cantidad_tarimas")), 0, dr.Item("cantidad_tarimas"))
                .Cantidad_cajas = IIf(IsDBNull(dr.Item("cantidad_cajas")), 0.0, dr.Item("cantidad_cajas"))
                .Costo_x_caja = IIf(IsDBNull(dr.Item("costo_x_caja")), 0.0, dr.Item("costo_x_caja"))
                .Almacenaje = IIf(IsDBNull(dr.Item("almacenaje")), 0.0, dr.Item("almacenaje"))
                .Manejo = IIf(IsDBNull(dr.Item("manejo")), 0.0, dr.Item("manejo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Unidades = IIf(IsDBNull(dr.Item("unidades")), 0, dr.Item("unidades"))
                .Fecha_cobro = IIf(IsDBNull(dr.Item("fecha_cobro")), Now, dr.Item("fecha_cobro"))
                .Valor_total = IIf(IsDBNull(dr.Item("valor_total")), Now, dr.Item("valor_total"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))

            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_prefactura_mov As clsBeTrans_prefactura_mov, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_prefactura_mov")
            Ins.Add("idtransprefacturamov", "@idtransprefacturamov", DataType.Parametro)
            Ins.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)

            If oBeTrans_prefactura_mov.IdOrdenCompraEnc > 0 Then
                Ins.Add("IdOrdenCompraEnc", "@IdOrdenCompraEnc", DataType.Parametro)
            End If

            If Not String.IsNullOrEmpty(oBeTrans_prefactura_mov.Poliza_oc_numero_orden) Then
                Ins.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", DataType.Parametro)
            End If

            Ins.Add("cantidad_tarimas", "@cantidad_tarimas", DataType.Parametro)
            Ins.Add("cantidad_cajas", "@cantidad_cajas", DataType.Parametro)
            Ins.Add("costo_x_caja", "@costo_x_caja", DataType.Parametro)
            Ins.Add("almacenaje", "@almacenaje", DataType.Parametro)
            Ins.Add("manejo", "@manejo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("unidades", "@unidades", DataType.Parametro)
            Ins.Add("fecha_cobro", "@fecha_cobro", DataType.Parametro)
            Ins.Add("valor_total", "@valor_total", DataType.Parametro)

            If Not String.IsNullOrEmpty(oBeTrans_prefactura_mov.Codigo_producto) Then
                Ins.Add("Codigo_producto", "@Codigo_producto", DataType.Parametro)
            End If


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAMOV", oBeTrans_prefactura_mov.Idtransprefacturamov))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_mov.IdTransPrefacturaEnc))

            If oBeTrans_prefactura_mov.IdOrdenCompraEnc > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_prefactura_mov.IdOrdenCompraEnc))
            End If

            If Not String.IsNullOrEmpty(oBeTrans_prefactura_mov.Poliza_oc_numero_orden) Then
                cmd.Parameters.Add(New SqlParameter("@POLIZA_OC_NUMERO_ORDEN", oBeTrans_prefactura_mov.Poliza_oc_numero_orden))
            End If

            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_TARIMAS", oBeTrans_prefactura_mov.Cantidad_tarimas))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_CAJAS", oBeTrans_prefactura_mov.Cantidad_cajas))
            cmd.Parameters.Add(New SqlParameter("@COSTO_X_CAJA", oBeTrans_prefactura_mov.Costo_x_caja))
            cmd.Parameters.Add(New SqlParameter("@ALMACENAJE", oBeTrans_prefactura_mov.Almacenaje))
            cmd.Parameters.Add(New SqlParameter("@MANEJO", oBeTrans_prefactura_mov.Manejo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_mov.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_mov.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_mov.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_mov.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_prefactura_mov.Activo))
            cmd.Parameters.Add(New SqlParameter("@UNIDADES", oBeTrans_prefactura_mov.Unidades))
            cmd.Parameters.Add(New SqlParameter("@FECHA_COBRO", oBeTrans_prefactura_mov.Fecha_cobro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TOTAL", oBeTrans_prefactura_mov.Valor_total))

            If Not String.IsNullOrEmpty(oBeTrans_prefactura_mov.Codigo_producto) Then
                cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_prefactura_mov.Codigo_producto))
            End If

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

    Public Shared Function Actualizar(ByRef oBeTrans_prefactura_mov As clsBeTrans_prefactura_mov, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_prefactura_mov")
            Upd.Add("idtransprefacturamov", "@idtransprefacturamov", DataType.Parametro)
            Upd.Add("idtransprefacturaenc", "@idtransprefacturaenc", DataType.Parametro)
            Upd.Add("IdOrdenCompraEnc", "@IdOrdenCompraEnc", DataType.Parametro)
            Upd.Add("poliza_oc_numero_orden", "@poliza_oc_numero_orden", DataType.Parametro)
            Upd.Add("cantidad_tarimas", "@cantidad_tarimas", DataType.Parametro)
            Upd.Add("cantidad_cajas", "@cantidad_cajas", DataType.Parametro)
            Upd.Add("costo_x_caja", "@costo_x_caja", DataType.Parametro)
            Upd.Add("almacenaje", "@almacenaje", DataType.Parametro)
            Upd.Add("manejo", "@manejo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("Idtransprefacturamov = @Idtransprefacturamov")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAMOV", oBeTrans_prefactura_mov.Idtransprefacturamov))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAENC", oBeTrans_prefactura_mov.IdTransPrefacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_prefactura_mov.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@POLIZA_OC_NUMERO_ORDEN", oBeTrans_prefactura_mov.Poliza_oc_numero_orden))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_TARIMAS", oBeTrans_prefactura_mov.Cantidad_tarimas))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_CAJAS", oBeTrans_prefactura_mov.Cantidad_cajas))
            cmd.Parameters.Add(New SqlParameter("@COSTO_X_CAJA", oBeTrans_prefactura_mov.Costo_x_caja))
            cmd.Parameters.Add(New SqlParameter("@ALMACENAJE", oBeTrans_prefactura_mov.Almacenaje))
            cmd.Parameters.Add(New SqlParameter("@MANEJO", oBeTrans_prefactura_mov.Manejo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_prefactura_mov.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_prefactura_mov.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_prefactura_mov.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_prefactura_mov.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_prefactura_mov.Activo))

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


    Public Shared Function Eliminar(ByRef oBeTrans_prefactura_mov As clsBeTrans_prefactura_mov, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_prefactura_mov" & _
             "  Where(Idtransprefacturamov = @Idtransprefacturamov)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSPREFACTURAMOV", oBeTrans_prefactura_mov.Idtransprefacturamov))

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

            Const sp As String = "SELECT * FROM Trans_prefactura_mov"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_prefactura_mov)

        Dim lReturnList As New List(Of clsBeTrans_prefactura_mov)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_mov"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_prefactura_mov As New clsBeTrans_prefactura_mov

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_prefactura_mov = New clsBeTrans_prefactura_mov()
                            Cargar(vBeTrans_prefactura_mov, dr)
                            lReturnList.Add(vBeTrans_prefactura_mov)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_prefactura_mov As clsBeTrans_prefactura_mov)

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_mov" & _
            " Where(Idtransprefacturamov = @Idtransprefacturamov)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_prefactura_mov As New clsBeTrans_prefactura_mov

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_prefactura_mov, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(Idtransprefacturamov),0) FROM Trans_prefactura_mov"

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

            Dim sp As String = String.Format("SELECT ISNULL(Max(Idtransprefacturamov),0) FROM Trans_prefactura_mov where IdTransPrefacturaEnc={0}", pIdTransPrefacturaEnc)

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
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_prefactura_mov)

        Get_All_By_IdPreFacturaEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_prefactura_mov Where(IdTransPrefacturaEnc = @IdTransPrefacturaEnc) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IDTRANSPREFACTURAENC", pIdTransPrefacturaEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    Get_All_By_IdPreFacturaEnc = New List(Of clsBeTrans_prefactura_mov)
                    Dim vBePreFacturaMov As New clsBeTrans_prefactura_mov

                    For Each dr As DataRow In lDataTable.Rows
                        vBePreFacturaMov = New clsBeTrans_prefactura_mov()
                        Cargar(vBePreFacturaMov, dr)
                        Get_All_By_IdPreFacturaEnc.Add(vBePreFacturaMov)
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

End Class
