Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_acuerdoscomerciales_enc

    Public Shared Sub Cargar(ByRef oBeTrans_acuerdoscomerciales_enc As clsBeTrans_acuerdoscomerciales_enc, ByRef dr As DataRow)
        Try
            With oBeTrans_acuerdoscomerciales_enc
                .IdAcuerdoEnc = IIf(IsDBNull(dr.Item("IdAcuerdoEnc")), 0, dr.Item("IdAcuerdoEnc"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Codigo_acuerdo = IIf(IsDBNull(dr.Item("codigo_acuerdo")), 0, dr.Item("codigo_acuerdo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Tipo_cobro = IIf(IsDBNull(dr.Item("tipo_cobro")), "", dr.Item("tipo_cobro"))
                .Cod_moneda = IIf(IsDBNull(dr.Item("cod_moneda")), 0, dr.Item("cod_moneda"))
                .Moneda = IIf(IsDBNull(dr.Item("moneda")), "", dr.Item("moneda"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), False, dr.Item("estado"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_acuerdoscomerciales_enc As clsBeTrans_acuerdoscomerciales_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_acuerdoscomerciales_enc")
            Ins.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("tipo_cobro", "@tipo_cobro", DataType.Parametro)
            Ins.Add("cod_moneda", "@cod_moneda", DataType.Parametro)
            Ins.Add("moneda", "@moneda", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)

            Ins.Add("user_agr", "@estado", DataType.Parametro)
            Ins.Add("fec_agr", "@estado", DataType.Parametro)
            Ins.Add("user_mod", "@estado", DataType.Parametro)
            Ins.Add("fec_mod", "@estado", DataType.Parametro)
            Ins.Add("fec_erp", "@estado", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_enc.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_acuerdoscomerciales_enc.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeTrans_acuerdoscomerciales_enc.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_acuerdoscomerciales_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COBRO", oBeTrans_acuerdoscomerciales_enc.Tipo_cobro))
            cmd.Parameters.Add(New SqlParameter("@COD_MONEDA", oBeTrans_acuerdoscomerciales_enc.Cod_moneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeTrans_acuerdoscomerciales_enc.Moneda))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_acuerdoscomerciales_enc.Estado))

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_acuerdoscomerciales_enc.user_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_acuerdoscomerciales_enc.fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_acuerdoscomerciales_enc.user_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_acuerdoscomerciales_enc.fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_ERP", oBeTrans_acuerdoscomerciales_enc.fec_erp))


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

    Public Shared Function Actualizar(ByRef oBeTrans_acuerdoscomerciales_enc As clsBeTrans_acuerdoscomerciales_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_acuerdoscomerciales_enc")
            Upd.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("tipo_cobro", "@tipo_cobro", DataType.Parametro)
            Upd.Add("cod_moneda", "@cod_moneda", DataType.Parametro)
            Upd.Add("moneda", "@moneda", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdAcuerdoEnc = @IdAcuerdoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_enc.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_acuerdoscomerciales_enc.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeTrans_acuerdoscomerciales_enc.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_acuerdoscomerciales_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_COBRO", oBeTrans_acuerdoscomerciales_enc.Tipo_cobro))
            cmd.Parameters.Add(New SqlParameter("@COD_MONEDA", oBeTrans_acuerdoscomerciales_enc.Cod_moneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeTrans_acuerdoscomerciales_enc.Moneda))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_acuerdoscomerciales_enc.Estado))

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

    Public Shared Function Eliminar(ByRef oBeTrans_acuerdoscomerciales_enc As clsBeTrans_acuerdoscomerciales_enc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_acuerdoscomerciales_enc" & _
             "  Where(IdAcuerdoEnc = @IdAcuerdoEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_enc.IdAcuerdoEnc))

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

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_enc"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_acuerdoscomerciales_enc)

        Dim lReturnList As New List(Of clsBeTrans_acuerdoscomerciales_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_acuerdoscomerciales_enc As New clsBeTrans_acuerdoscomerciales_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_acuerdoscomerciales_enc = New clsBeTrans_acuerdoscomerciales_enc()
                            Cargar(vBeTrans_acuerdoscomerciales_enc, dr)
                            lReturnList.Add(vBeTrans_acuerdoscomerciales_enc)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_acuerdoscomerciales_enc As clsBeTrans_acuerdoscomerciales_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_enc" & _
            " Where(IdAcuerdoEnc = @IdAcuerdoEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoEnc", pBeTrans_acuerdoscomerciales_enc.IdAcuerdoEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        'Dim vBeTrans_acuerdoscomerciales_enc As New clsBeTrans_acuerdoscomerciales_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeTrans_acuerdoscomerciales_enc, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoEnc),0) FROM Trans_acuerdoscomerciales_enc"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal pIdCliente As Integer) As DataTable


        Get_All_For_Combo = Nothing

        Try
            '#GT18072024: listamos acuerdos para iterar el proceso de prefactura, mostrar acuerdos con detalle
            'Const sp As String = "SELECT IdAcuerdoEnc,codigo_acuerdo,descripcion, moneda as Moneda
            '                               FROM trans_acuerdoscomerciales_Enc 
            '					  WHERE IdCliente=@IdCliente and estado=1 "

            Const sp As String = "SELECT Distinct enc.IdAcuerdoEnc,enc.codigo_acuerdo,enc.descripcion, moneda as Moneda
                                  FROM trans_acuerdoscomerciales_Enc enc left outer join trans_acuerdoscomerciales_det det on enc.IdAcuerdoEnc=det.IdAcuerdoEnc 
								  WHERE enc.IdCliente=@IdCliente and enc.estado=1 and det.estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_For_Combo = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Get_All_For_Combo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_AcuerdosEnc_And_Detalle_By_IdCliente(ByVal pIdCliente As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_acuerdoscomerciales_enc)

        Get_AcuerdosEnc_And_Detalle_By_IdCliente = Nothing

        Try


            Const sp As String = "select IdAcuerdoEnc,IdCliente,codigo_acuerdo,descripcion,tipo_cobro,cod_moneda,moneda,estado
                                  from trans_acuerdoscomerciales_enc where IdCliente=@IdCliente and estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBe_acuerdoscomerciales_Enc As New clsBeTrans_acuerdoscomerciales_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_AcuerdosEnc_And_Detalle_By_IdCliente = New List(Of clsBeTrans_acuerdoscomerciales_enc)

                            For Each dr As DataRow In lDataTable.Rows

                                vBe_acuerdoscomerciales_Enc = New clsBeTrans_acuerdoscomerciales_enc()
                                Cargar(vBe_acuerdoscomerciales_Enc, dr)

                                vBe_acuerdoscomerciales_Enc.lDetalle = clsLnTrans_acuerdoscomerciales_det.Get_All_By_Codigo_Acuerdo(vBe_acuerdoscomerciales_Enc.Codigo_acuerdo,
                                                                                                                                    pIdBodega,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)


                                Get_AcuerdosEnc_And_Detalle_By_IdCliente.Add(vBe_acuerdoscomerciales_Enc)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            'Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoEnc),0) FROM Trans_acuerdoscomerciales_enc"

            Using lCommand As New SqlCommand(sp, pConnection)

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

    Public Shared Function Existe(ByVal pCodigoAcuerdo As String,
                                  ByRef Cnn As SqlConnection,
                                  ByRef pTransaction As SqlTransaction) As clsBeTrans_acuerdoscomerciales_enc

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM trans_acuerdoscomerciales_enc WHERE codigo_acuerdo=@Codigo_Acuerdo "

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Acuerdo", pCodigoAcuerdo)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeTrans_acuerdoscomerciales_enc()

                    Cargar(ObjUM, lRow)

                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_by_AcuerdoEnc(ByVal pCodigoAcuerdo As String, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        Existe_by_AcuerdoEnc = False

        Try

            Dim vSQL As String = "SELECT * FROM trans_acuerdoscomerciales_enc WHERE codigo_acuerdo=@Codigo_Acuerdo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Acuerdo", pCodigoAcuerdo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Existe_by_AcuerdoEnc = (lDT.Rows.Count > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Acuerdos_Comerciales_By_IdCliente(ByVal pIdCliente As Integer, ByVal pActivos As Boolean) As DataTable


        Get_Acuerdos_Comerciales_By_IdCliente = Nothing

        Try

            Dim sp As String = "select det.estado,det.prioridad,enc.codigo_acuerdo codigo,enc.descripcion as acuerdo,
                                         enc.tipo_cobro,enc.moneda,
                                         enc.estado estado_enc,det.codigo_producto,
										 Bd.nombre bodega,
									     det.IdTipoCobro [tipo cobro],
									     det.servicio,det.correlativo_detalleacuerdo as correlativo,
                                         det.descripcion,det.monto,det.porcentaje
                                  from trans_acuerdoscomerciales_enc enc left outer join trans_acuerdoscomerciales_det det
                                         on enc.codigo_acuerdo=det.codigo_acuerdo and enc.IdAcuerdoEnc=det.IdAcuerdoEnc
										 left outer join Bodega bd on det.IdBodega=bd.IdBodega
                                  where enc.IdCliente=@codigo_cliente "

            If pActivos = True Then
                sp += " And det.estado=1"
            Else
                sp += " AND det.estado=0"
            End If


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_cliente", pIdCliente)
                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Acuerdos_Comerciales_By_IdCliente = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try


    End Function

    Public Shared Function Get_AcuerdoEnc_And_Detalle_By_IdCliente(ByVal pIdCliente As Integer, ByVal pIdBodega As Integer) As clsBeTrans_acuerdoscomerciales_enc

        Get_AcuerdoEnc_And_Detalle_By_IdCliente = Nothing

        Try


            Const sp As String = "select distinct enc.IdAcuerdoEnc,enc.IdCliente,enc.codigo_acuerdo,enc.descripcion,enc.tipo_cobro,enc.cod_moneda,enc.moneda,enc.estado
                                  from trans_acuerdoscomerciales_enc enc left outer join trans_acuerdoscomerciales_det det on enc.IdAcuerdoEnc=det.IdAcuerdoEnc 
							      where enc.IdCliente=@IdCliente and enc.estado=1 and det.estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBe_acuerdoscomerciales_Enc As New clsBeTrans_acuerdoscomerciales_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then


                            vBe_acuerdoscomerciales_Enc = New clsBeTrans_acuerdoscomerciales_enc()
                            Cargar(vBe_acuerdoscomerciales_Enc, lDataTable.Rows(0))

                            vBe_acuerdoscomerciales_Enc.lDetalle = clsLnTrans_acuerdoscomerciales_det.Get_All_By_Codigo_Acuerdo(vBe_acuerdoscomerciales_Enc.Codigo_acuerdo,
                                                                                                                                pIdBodega,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)


                            Get_AcuerdoEnc_And_Detalle_By_IdCliente = vBe_acuerdoscomerciales_Enc

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#GT10062024: si cliente tiene mas de un acuerdo habilitado, alertar en prefactura y en OC/PE

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdCliente As Integer) As Integer

        Get_All_By_IdPropietario = 0

        Try

            Const sp As String = "SELECT DISTINCT enc.* FROM Trans_acuerdoscomerciales_enc enc
													inner join trans_acuerdoscomerciales_det det
													on enc.codigo_acuerdo=det.codigo_acuerdo and enc.IdAcuerdoEnc=det.IdAcuerdoEnc
													where enc.estado=1 and det.estado=1 and IdCliente=@IdCliente "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_By_IdPropietario = lDataTable.Rows.Count
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


    '#GT10062024: mostrar solo encabezado del acuerdo comercial.
    Public Shared Function Get_AcuerdosEnc_By_IdCliente_And_IdBodega(ByVal pIdCliente As Integer, ByVal pIdBodega As Integer) As DataTable


        Get_AcuerdosEnc_By_IdCliente_And_IdBodega = Nothing

        Try

            Const sp As String = "select distinct enc.IdAcuerdoEnc,enc.codigo_acuerdo codigo,enc.descripcion as acuerdo,
										 enc.tipo_cobro,enc.moneda
									     from trans_acuerdoscomerciales_enc enc inner join trans_acuerdoscomerciales_det det
										 on enc.codigo_acuerdo=det.codigo_acuerdo and enc.IdAcuerdoEnc=det.IdAcuerdoEnc
										 inner join Bodega bd on det.IdBodega=bd.IdBodega
										 where IdCliente=@IdCliente and enc.estado=1 and det.estado=1
										 and det.IdBodega=@IdBodega "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_AcuerdosEnc_By_IdCliente_And_IdBodega = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_AcuerdoEnc_And_Detalle_By_IdAcuerdoEnc(ByVal pIdAcuerdoEnc As Integer, ByVal pIdBodega As Integer) As clsBeTrans_acuerdoscomerciales_enc

        Get_AcuerdoEnc_And_Detalle_By_IdAcuerdoEnc = Nothing

        Try


            Const sp As String = "select IdAcuerdoEnc,IdCliente,codigo_acuerdo,descripcion,tipo_cobro,cod_moneda,moneda,estado
                                  from trans_acuerdoscomerciales_enc where IdAcuerdoEnc=@pIdAcuerdoEnc and estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdAcuerdoEnc", pIdAcuerdoEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBe_acuerdoscomerciales_Enc As New clsBeTrans_acuerdoscomerciales_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then


                            vBe_acuerdoscomerciales_Enc = New clsBeTrans_acuerdoscomerciales_enc()
                            Cargar(vBe_acuerdoscomerciales_Enc, lDataTable.Rows(0))

                            vBe_acuerdoscomerciales_Enc.lDetalle = clsLnTrans_acuerdoscomerciales_det.Get_All_By_Codigo_Acuerdo(vBe_acuerdoscomerciales_Enc.Codigo_acuerdo,
                                                                                                                                pIdBodega,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)


                            Get_AcuerdoEnc_And_Detalle_By_IdAcuerdoEnc = vBe_acuerdoscomerciales_Enc

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
