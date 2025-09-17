Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnLog_error_wms

    Public Shared Sub Cargar(ByRef oBeLog_error_wms As clsBeLog_error_wms, ByRef dr As DataRow)

        Try

            With oBeLog_error_wms

                .IdError = IIf(IsDBNull(dr.Item("IdError")), 0, dr.Item("IdError"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                .MensajeError = IIf(IsDBNull(dr.Item("MensajeError")), "", dr.Item("MensajeError"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdUsuarioAgr = IIf(IsDBNull(dr.Item("IdUsuarioAgr")), 0, dr.Item("IdUsuarioAgr"))
                .Line_No = IIf(IsDBNull(dr.Item("Line_No")), 0, dr.Item("Line_No"))
                .Item_No = IIf(IsDBNull(dr.Item("Item_No")), 0, dr.Item("Item_No"))
                .UmBas = IIf(IsDBNull(dr.Item("UmBas")), 0, dr.Item("UmBas"))
                .Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), 0, dr.Item("Variant_Code"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0, dr.Item("Cantidad"))
                .Referencia_Documento = IIf(IsDBNull(dr.Item("Referencia_Documento")), 0, dr.Item("Referencia_Documento"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeLog_error_wms As clsBeLog_error_wms, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("log_error_wms")
            Ins.Add("iderror", "@iderror", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro)
            Ins.Add("IdPickingEnc", "@IdPickingEnc", DataType.Parametro)
            Ins.Add("IdRecepcionEnc", "@IdRecepcionEnc", DataType.Parametro)
            Ins.Add("IdUsuarioAgr", "@IdUsuarioAgr", DataType.Parametro)

            If Not oBeLog_error_wms.Line_No = 0 Then Ins.Add("Line_No", "@Line_No", DataType.Parametro)
            If Not oBeLog_error_wms.UmBas = "" Then Ins.Add("UmBas", "@UmBas", DataType.Parametro)
            If Not oBeLog_error_wms.Variant_Code = "" Then Ins.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            If Not oBeLog_error_wms.Item_No = "" Then Ins.Add("Item_No", "@Item_No", DataType.Parametro)
            If Not oBeLog_error_wms.Cantidad = 0 Then Ins.Add("Cantidad", "@Cantidad", DataType.Parametro)
            If Not oBeLog_error_wms.Referencia_Documento = "" Then Ins.Add("Referencia_Documento", "@Referencia_Documento", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDERROR", oBeLog_error_wms.IdError))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeLog_error_wms.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeLog_error_wms.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeLog_error_wms.Fecha))
            cmd.Parameters.Add(New SqlParameter("@MENSAJEERROR", oBeLog_error_wms.MensajeError))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_error_wms.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeLog_error_wms.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeLog_error_wms.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIOAGR", oBeLog_error_wms.IdUsuarioAgr))

            If Not oBeLog_error_wms.Line_No = 0 Then cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeLog_error_wms.Line_No))
            If Not oBeLog_error_wms.UmBas = "" Then cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeLog_error_wms.UmBas))
            If Not oBeLog_error_wms.Variant_Code = "" Then cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeLog_error_wms.Variant_Code))
            If Not oBeLog_error_wms.Item_No = "" Then cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeLog_error_wms.Item_No))
            If Not oBeLog_error_wms.Cantidad = 0 Then cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeLog_error_wms.Cantidad))
            If Not oBeLog_error_wms.Referencia_Documento = "" Then cmd.Parameters.Add(New SqlParameter("@Referencia_Documento", oBeLog_error_wms.Referencia_Documento))

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

    Public Shared Function Actualizar(ByRef oBeLog_error_wms As clsBeLog_error_wms, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("log_error_wms")
            Upd.Add("iderror", "@iderror", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Upd.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro)
            Upd.Add("IdPickingEnc", "@IdPickingEnc", DataType.Parametro)
            Upd.Add("IdRecepcionEnc", "@IdRecepcionEnc", DataType.Parametro)
            Upd.Add("IdUsuarioAgr", "@IdUsuarioAgr", DataType.Parametro)
            Upd.Add("Line_No", "@Line_No", DataType.Parametro)
            Upd.Add("Item_No", "@Item_No", DataType.Parametro)
            Upd.Add("UmBas", "@UmBas", DataType.Parametro)
            Upd.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            Upd.Add("Referencia_Documento", "@Referencia_Documento", DataType.Parametro)
            Upd.Where("IdError = @IdError")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDERROR", oBeLog_error_wms.IdError))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeLog_error_wms.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeLog_error_wms.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeLog_error_wms.Fecha))
            cmd.Parameters.Add(New SqlParameter("@MENSAJEERROR", oBeLog_error_wms.MensajeError))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_error_wms.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeLog_error_wms.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeLog_error_wms.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIOAGR", oBeLog_error_wms.IdUsuarioAgr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeLog_error_wms As clsBeLog_error_wms, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Log_error_wms" &
             "  Where(IdError = @IdError)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDERROR", oBeLog_error_wms.IdError))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
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

            Const sp As String = "SELECT * FROM Log_error_wms"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeLog_error_wms)

        Dim lReturnList As New List(Of clsBeLog_error_wms)

        Try

            Const sp As String = "SELECT * FROM Log_error_wms"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeLog_error_wms As New clsBeLog_error_wms

                        For Each dr As DataRow In lDataTable.Rows
                            vBeLog_error_wms = New clsBeLog_error_wms()
                            Cargar(vBeLog_error_wms, dr)
                            lReturnList.Add(vBeLog_error_wms)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeLog_error_wms As clsBeLog_error_wms)

        Try

            Const sp As String = "SELECT * FROM Log_error_wms" &
            " Where(IdError = @IdError)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeLog_error_wms As New clsBeLog_error_wms

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeLog_error_wms, lDataTable.Rows(0))
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

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdError),0) FROM Log_error_wms "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Log_By_Filtros(ByVal pFechaDel As Date,
                                                  ByVal pFechaAl As Date,
                                                  ByVal pIdProductoBodega As Integer,
                                                  ByVal pIdBodega As Integer) As DataTable


        Get_All_Log_By_Filtros = Nothing

        Dim vCodigoProducto As String = ""

        Try

            Dim vSQL As String = ""

            vSQL = "select * from log_error_wms
                    WHERE IdBodega=@IdBodega "

            If pIdProductoBodega <> 0 Then
                vCodigoProducto = clsLnProducto.Get_Codigo_By_IdProductoBodega(pIdProductoBodega)
                vSQL += " AND Item_No = @Item_No"
            End If

            vSQL += String.Format(" And cast(Fecha AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY Fecha "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdProductoBodega <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@Item_No", vCodigoProducto)
                        End If

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_All_Log_By_Filtros = lTable

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

    Public Shared Function Get_All_Log_By_Rango_Fechas(ByVal pFechaDel As Date,
                                                       ByVal pFechaAl As Date) As DataTable


        Get_All_Log_By_Rango_Fechas = Nothing

        Dim vCodigoProducto As String = ""

        Try

            Dim vSQL As String = ""

            vSQL = "select l.*, u.nombres + ' ' + u.apellidos AS Usuario
                    from log_error_wms l left join usuario u ON l.IdUsuarioAgr = u.IdUsuario
                    WHERE 1 > 0 "

            vSQL += String.Format(" And cast(l.Fecha AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY l.Fecha "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_All_Log_By_Rango_Fechas = lTable

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

    ''' <summary>
    ''' #EJC202303011651
    ''' </summary>
    ''' <param name="pReferenciaDocumento"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_Referencia_Documento(ByVal pReferenciaDocumento As String) As List(Of clsBeLog_error_wms)

        Dim lReturnList As New List(Of clsBeLog_error_wms)

        Try

            Const sp As String = "SELECT * FROM LOG_ERROR_WMS WHERE REFERENCIA_DOCUMENTO = @REFERENCIA_DOCUMENTO"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@REFERENCIA_DOCUMENTO", pReferenciaDocumento)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeLog_error_wms As New clsBeLog_error_wms

                        For Each dr As DataRow In lDataTable.Rows
                            vBeLog_error_wms = New clsBeLog_error_wms()
                            Cargar(vBeLog_error_wms, dr)
                            lReturnList.Add(vBeLog_error_wms)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_Referencia_Documento(ByVal pReferenciaDocumento As String,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Log_error_wms" &
             "  Where(Referencia_Documento = @Referencia_Documento)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO", pReferenciaDocumento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
