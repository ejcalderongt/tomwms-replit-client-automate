Imports System.Data.SqlClient

Public Class clsLnCEALSA_clientes

    Public Shared Sub Cargar(ByRef oBeCEALSA_clientes As clsBeCEALSA_clientes, ByRef dr As DataRow)
        Try
            With oBeCEALSA_clientes
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Codigo_cliente = IIf(IsDBNull(dr.Item("codigo_cliente")), "", dr.Item("codigo_cliente"))
                .Nombre_cliente = IIf(IsDBNull(dr.Item("nombre_cliente")), "", dr.Item("nombre_cliente"))
                .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                .Razon_social = IIf(IsDBNull(dr.Item("razon_social")), "", dr.Item("razon_social"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), False, dr.Item("procesado_wms"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCEALSA_clientes As clsBeCEALSA_clientes, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_cliente")
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)
            Ins.Add("nombre_cliente", "@nombre_cliente", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("razon_social", "@razon_social", DataType.Parametro)
            Ins.Add("procesado_wms", "@procesado_wms", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCEALSA_clientes.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeCEALSA_clientes.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeCEALSA_clientes.Nombre_cliente))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeCEALSA_clientes.Nit))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeCEALSA_clientes.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeCEALSA_clientes.Procesado_wms))

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

    Public Shared Function Actualizar(ByRef oBeCEALSA_clientes As clsBeCEALSA_clientes, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_cliente")
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)
            Upd.Add("nombre_cliente", "@nombre_cliente", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("razon_social", "@razon_social", DataType.Parametro)
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCEALSA_clientes.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeCEALSA_clientes.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeCEALSA_clientes.Nombre_cliente))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeCEALSA_clientes.Nit))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeCEALSA_clientes.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeCEALSA_clientes.Procesado_wms))

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

    Public Shared Function Eliminar(ByRef oBeCEALSA_clientes As clsBeCEALSA_clientes, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_cliente" &
             "  Where(IdCliente = @IdCliente)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCEALSA_clientes.IdCliente))

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

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_cliente "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

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

            Const sp As String = "SELECT * FROM I_nav_cliente"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeCEALSA_clientes)

        Dim lReturnList As New List(Of clsBeCEALSA_clientes)

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_cliente As New clsBeCEALSA_clientes

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_cliente = New clsBeCEALSA_clientes()
                            Cargar(vBeI_nav_cliente, dr)
                            lReturnList.Add(vBeI_nav_cliente)
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

    Public Shared Sub GetSingle(ByRef pBeI_nav_cliente As clsBeCEALSA_clientes)

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente" &
            " Where(IdCliente = @IdCliente)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_cliente As New clsBeCEALSA_clientes

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_cliente, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdCliente),0) FROM I_nav_cliente"

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
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Bandera(ByRef pCliente As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_cliente")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codigo_cliente = @codigo_cliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@procesado_wms", True))
            cmd.Parameters.Add(New SqlParameter("@codigo_cliente", pCliente))

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

    Public Shared Function Get_All_Pendientes_De_Sincronizacion() As List(Of clsBeCEALSA_clientes)

        Get_All_Pendientes_De_Sincronizacion = Nothing

        Dim lReturnList As New List(Of clsBeCEALSA_clientes)

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente WHERE procesado_wms = 0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_clientes As New clsBeCEALSA_clientes

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCEALSA_clientes = New clsBeCEALSA_clientes()
                            Cargar(vBeCEALSA_clientes, dr)
                            '#EJC20210305: No me hace mucho sentido a mi mismo leer el detalle en este momento...
                            'vBeCEALSA_clientes.lAcuerdos = clsLnI_nav_detacuerdoscomerciales.Get_All_By_CodCliente(vBeCEALSA_clientes.Codigo_cliente, lConnection, lTransaction)
                            lReturnList.Add(vBeCEALSA_clientes)
                        Next

                        Get_All_Pendientes_De_Sincronizacion = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_WMS(ByRef oBeI_nav_cliente As clsBeI_nav_cliente) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Upd.Init("i_nav_cliente")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            cmd = New SqlCommand(sp, lConnection, lTransaction)

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_cliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_cliente.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

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

    Public Shared Function Actualizar_Procesado_WMS(ByRef oBeI_nav_cliente As clsBeI_nav_cliente,
                                                    ByVal lConnectionERP As SqlConnection,
                                                    ByVal lTransactionERP As SqlTransaction) As Integer

        Try


            Upd.Init("i_nav_cliente")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codigo_cliente = @codigo_cliente")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            cmd = New SqlCommand(sp, lConnectionERP, lTransactionERP)

            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeI_nav_cliente.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_cliente.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
