Imports System.Data.SqlClient

Partial Public Class clsLnI_nav_cliente

    Public Shared Function Get_IdCliente_By_Codigo(ByVal pCodigo As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_IdCliente_By_Codigo = -1

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente" &
            " Where(codigo_cliente = @codigo_cliente OR [No]=@codigo_cliente)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo_cliente", pCodigo)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_cliente As New clsBeI_nav_cliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeI_nav_cliente, lDataTable.Rows(0))
                    Return vBeI_nav_cliente.IdCliente
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_De_Procesar(ByRef lblprg As RichTextBox,
                                                          ByRef prg As ProgressBar,
                                                          ByVal lConnectionERP As SqlConnection,
                                                          ByVal lTransactionERP As SqlTransaction) As List(Of clsBeI_nav_cliente)

        Dim lReturnList As New List(Of clsBeI_nav_cliente)

        Get_All_Pendientes_De_Procesar = Nothing

        Dim vContador As Integer = 0
        Dim lEncAllNoProcesados As New List(Of clsBeI_nav_acuerdo_enc)
        Dim lDetAllNoProcesados As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente WHERE procesado_wms = 0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_cliente As New clsBeI_nav_cliente

                        lblprg.Visible = True
                        prg.Maximum = lDataTable.Rows.Count

                        '#EJC20210306: Optimización Alfa, obtener todos separar despues por memoria.
                        'lEncAllNoProcesados = clsLnI_nav_detacuerdoscomerciales.Get_All_Encabezados_Acuerdos_Comerciales_No_Procesados(lConnectionERP, lTransactionERP)
                        'lDetAllNoProcesados = clsLnI_nav_detacuerdoscomerciales.Get_All_Detalles_No_Procesados(lConnectionERP, lTransactionERP)

                        'lEncAllNoProcesados = clsLnI_nav_acuerdo_det.Get_All_Encabezados_Acuerdos_Comerciales_No_Procesados(lConnectionERP, lTransactionERP)
                        'lDetAllNoProcesados = clsLnI_nav_acuerdo_det.Get_All_Detalles_No_Procesados(lConnectionERP, lTransactionERP)

                        For Each dr As DataRow In lDataTable.Rows

                            vBeI_nav_cliente = New clsBeI_nav_cliente()
                            Cargar(vBeI_nav_cliente, dr)
                            '#EJC20210305: Obtener los acuerdos comerciales desde el ERP por cada cliente.
                            'No se pasa transacción porque esto va a otra BD.

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Consultando acuerdos comerciales para: " & vBeI_nav_cliente.Codigo_cliente & " " & vBeI_nav_cliente.Nombre_cliente)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            '#EJC20210306: Optimizado para búsqueda en memoria.
                            'vBeI_nav_cliente.lAcuerdosEncERP = clsLnI_nav_detacuerdoscomerciales.Get_All_Encabezados_Acuerdos_Comerciales_By_CodCliente(vBeI_nav_cliente.IdCliente, lConnectionERP, lTransactionERP)
                            'vBeI_nav_cliente.lAcuerdosDetERP = clsLnI_nav_detacuerdoscomerciales.Get_All_By_CodCliente(vBeI_nav_cliente.IdCliente, lConnectionERP, lTransactionERP)

                            '#GT02052024: falta validar cuando se importan clientes, que vengan con acuerdos encabezado/detalle
                            'vBeI_nav_cliente.lAcuerdosEncERP = lEncAllNoProcesados.FindAll(Function(x) x.Idcliente = vBeI_nav_cliente.Codigo_cliente)
                            'vBeI_nav_cliente.lAcuerdosDetERP = lDetAllNoProcesados.FindAll(Function(x) x.Codcliente = vBeI_nav_cliente.Codigo_cliente)

                            lReturnList.Add(vBeI_nav_cliente)
                            prg.Value = vContador
                            vContador += 1

                            lblprg.AppendText("     Detalle de acuerdos --->  " & vBeI_nav_cliente.lAcuerdosDetERP.Count)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            Application.DoEvents()

                        Next

                        Get_All_Pendientes_De_Procesar = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        Finally
            prg.Value = 0
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_WMS(ByRef oBeI_nav_cliente As clsBeI_nav_cliente,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            '#CKFK 20210312 19:58 Modifiqué el update para que se haga por el código del cliente y no por el IdCliente
            Upd.Init("i_nav_cliente")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("Codigo_cliente = @codigo_cliente")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            '#CKFK 20210312 Reemplacé el IdCliente por el Codigo cliente
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeI_nav_cliente.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_cliente.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_cliente"
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
