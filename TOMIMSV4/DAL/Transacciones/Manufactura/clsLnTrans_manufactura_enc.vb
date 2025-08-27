Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_manufactura_enc

    Public Shared Sub Cargar(ByRef oBeTrans_manufactura_enc As clsBeTrans_manufactura_enc, ByRef dr As DataRow)
        Try
            With oBeTrans_manufactura_enc
                .IdManufacturaEnc = IIf(IsDBNull(dr.Item("IdManufacturaEnc")), 0, dr.Item("IdManufacturaEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdTipoManufactura = IIf(IsDBNull(dr.Item("IdTipoManufactura")), 0, dr.Item("IdTipoManufactura"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Fecha_manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Escaneo = IIf(IsDBNull(dr.Item("escaneo")), False, dr.Item("escaneo"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_manufactura_enc As clsBeTrans_manufactura_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_manufactura_enc")
            Ins.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idtipomanufactura", "@idtipomanufactura", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("escaneo", "@escaneo", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_enc.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_manufactura_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_manufactura_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeTrans_manufactura_enc.IdTipoManufactura))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_manufactura_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeTrans_manufactura_enc.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_manufactura_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_manufactura_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_manufactura_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESCANEO", oBeTrans_manufactura_enc.Escaneo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_manufactura_enc.Activo))

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

    Public Shared Function Actualizar(ByRef oBeTrans_manufactura_enc As clsBeTrans_manufactura_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_manufactura_enc")
            Upd.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idtipomanufactura", "@idtipomanufactura", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("escaneo", "@escaneo", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdManufacturaEnc = @IdManufacturaEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_enc.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_manufactura_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_manufactura_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeTrans_manufactura_enc.IdTipoManufactura))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_manufactura_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeTrans_manufactura_enc.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_manufactura_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_manufactura_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_manufactura_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESCANEO", oBeTrans_manufactura_enc.Escaneo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_manufactura_enc.Activo))

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

    Public Shared Function Eliminar(ByRef oBeTrans_manufactura_enc As clsBeTrans_manufactura_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_manufactura_enc" & _
             "  Where(IdManufacturaEnc = @IdManufacturaEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_enc.IdManufacturaEnc))

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

            Const sp As String = "SELECT * FROM Trans_manufactura_enc"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_manufactura_enc)

        Dim lReturnList As New List(Of clsBeTrans_manufactura_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_enc As New clsBeTrans_manufactura_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_manufactura_enc = New clsBeTrans_manufactura_enc()
                            Cargar(vBeTrans_manufactura_enc, dr)
                            lReturnList.Add(vBeTrans_manufactura_enc)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_manufactura_enc As clsBeTrans_manufactura_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_enc " &
            " Where(IdManufacturaEnc = @IdManufacturaEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdManufacturaEnc", pBeTrans_manufactura_enc.IdManufacturaEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_enc As New clsBeTrans_manufactura_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_manufactura_enc, lDataTable.Rows(0))
                            pBeTrans_manufactura_enc = vBeTrans_manufactura_enc
                        Else
                            vBeTrans_manufactura_enc = Nothing
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

            Const sp As String = "SELECT ISNULL(Max(IdManufacturaEnc),0) FROM Trans_manufactura_enc"

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

            Dim vSQL As String = "SELECT ISNULL(Max(IdManufacturaEnc),0) FROM Trans_manufactura_enc"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean,
                                               ByVal pFechaDel As Date,
                                               ByVal pFechaAl As Date,
                                               ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "	SELECT  manufactura_enc.IdManufacturaEnc as Correlativo,tipo.nombre as Manufactura,
											pe_enc.Fecha_Pedido,
											manufactura_enc.fecha_manufactura,
											manufactura_enc.IdPedidoEnc as Pedido,pe_enc.referencia as Referencia,
											pe_enc.IdCliente,cl.nombre_comercial as Cliente,
											manufactura_enc.IdTipoManufactura,Convert(Time(0), manufactura_enc.hora_ini) Hora_Ini,
											Convert(Time(0), manufactura_enc.hora_fin) Hora_Fin,
											manufactura_enc.estado as Estado,
											usuario.nombres +' '+ usuario.apellidos as Operador
									FROM trans_manufactura_enc manufactura_enc INNER JOIN 
                                         trans_pe_enc pe_enc ON manufactura_enc.IdPedidoEnc = pe_enc.IdPedidoEnc INNER JOIN 
                                         trans_manufactura_tipo tipo ON manufactura_enc.idtipomanufactura=tipo.idtipomanufactura INNER JOIN 
                                         cliente cl ON pe_enc.IdCliente=cl.IdCliente INNER JOIN 
                                         usuario ON manufactura_enc.user_agr= usuario.idusuario
									WHERE manufactura_enc.IdBodega = @IdBodega AND manufactura_enc.activo = @Activo "

            vSQL += String.Format(" AND cast(manufactura_enc.fec_agr AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
                        lDataAdapter.Fill(lTable)

                        If lTable IsNot Nothing Then
                            Get_All_By_IdBodega = lTable
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

    Public Shared Function Guardar(ByVal pManufacturaEnc As clsBeTrans_manufactura_enc,
                                   ByVal pListManufacturaDet As List(Of clsBeTrans_manufactura_det)) As Boolean

        Guardar = False
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Guarda_Trans_Manufactura_Enc(pManufacturaEnc, lConnection, lTransaction)

            clsLnTrans_manufactura_det.Guardar_Detalle(pManufacturaEnc.IdManufacturaEnc,
                                                       pListManufacturaDet,
                                                       lConnection,
                                                       lTransaction)

            Guardar = True

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try
    End Function

    Public Shared Sub Guarda_Trans_Manufactura_Enc(ByVal pBeManufacturaEnc As clsBeTrans_manufactura_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)
        Try

            If pBeManufacturaEnc.IsNew Then
                pBeManufacturaEnc.IdManufacturaEnc = MaxID(lConnection, lTransaction) + 1
                Insertar(pBeManufacturaEnc, lConnection, lTransaction)
            Else
                Actualizar(pBeManufacturaEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Obtener(ByRef pBeTrans_manufactura_enc As clsBeTrans_manufactura_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM trans_manufactura_enc Where(IdManufacturaEnc = @IdManufacturaEnc)"


            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdManufacturaEnc", pBeTrans_manufactura_enc.IdManufacturaEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_manufactura_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Encabezado(ByVal IdManufacturaEnc As Integer, ByVal Estado As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_manufactura_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Where("IdManufacturaEnc = @IdManufacturaEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", Estado))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", Now))

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
