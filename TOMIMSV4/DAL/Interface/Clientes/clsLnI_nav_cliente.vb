Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_cliente

    Public Shared Sub Cargar(ByRef oBeI_nav_cliente As clsBeI_nav_cliente, ByRef dr As DataRow)
        Try
            With oBeI_nav_cliente
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Codigo_cliente = IIf(IsDBNull(dr.Item("codigo_cliente")), "", dr.Item("codigo_cliente"))
                .Nombre_cliente = IIf(IsDBNull(dr.Item("nombre_cliente")), "", dr.Item("nombre_cliente"))
                .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                .Razon_social = IIf(IsDBNull(dr.Item("razon_social")), "", dr.Item("razon_social"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), False, dr.Item("procesado_wms"))
                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Name = IIf(IsDBNull(dr.Item("Name")), "", dr.Item("Name"))
                .Adress = IIf(IsDBNull(dr.Item("Adress")), "", dr.Item("Adress"))
                .City = IIf(IsDBNull(dr.Item("City")), "", dr.Item("City"))
                .Country = IIf(IsDBNull(dr.Item("Country")), "", dr.Item("Country"))
                .Phone_No = IIf(IsDBNull(dr.Item("Phone_No")), "", dr.Item("Phone_No"))
                .ContactName = IIf(IsDBNull(dr.Item("ContactName")), "", dr.Item("ContactName"))
                .Search_Name = IIf(IsDBNull(dr.Item("Search_Name")), "", dr.Item("Search_Name"))
                .VAT_Registratrion_No = IIf(IsDBNull(dr.Item("VAT_Registratrion_No")), "", dr.Item("VAT_Registratrion_No"))
                .Location_Code = IIf(IsDBNull(dr.Item("Location_Code")), "", dr.Item("Location_Code"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_cliente As clsBeI_nav_cliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

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
            Ins.Add("no", "@no", DataType.Parametro)
            Ins.Add("name", "@name", DataType.Parametro)
            Ins.Add("adress", "@adress", DataType.Parametro)
            Ins.Add("city", "@city", DataType.Parametro)
            Ins.Add("country", "@country", DataType.Parametro)
            If Not oBeI_nav_cliente.Phone_No Is Nothing Then Ins.Add("phone_no", "@phone_no", DataType.Parametro)
            If Not oBeI_nav_cliente.ContactName Is Nothing Then Ins.Add("contactname", "@contactname", DataType.Parametro)
            Ins.Add("search_name", "@search_name", DataType.Parametro)
            Ins.Add("vat_registratrion_no", "@vat_registratrion_no", DataType.Parametro)
            Ins.Add("location_code", "@location_code", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_cliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeI_nav_cliente.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeI_nav_cliente.Nombre_cliente))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeI_nav_cliente.Nit))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeI_nav_cliente.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_cliente.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_cliente.No))
            cmd.Parameters.Add(New SqlParameter("@NAME", oBeI_nav_cliente.Name))
            If Not oBeI_nav_cliente.Adress Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ADRESS", oBeI_nav_cliente.Adress))
            If Not oBeI_nav_cliente.City Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CITY", oBeI_nav_cliente.City))
            cmd.Parameters.Add(New SqlParameter("@COUNTRY", oBeI_nav_cliente.Country))
            If Not oBeI_nav_cliente.Phone_No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PHONE_NO", oBeI_nav_cliente.Phone_No))
            If Not oBeI_nav_cliente.ContactName Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CONTACTNAME", oBeI_nav_cliente.ContactName))
            cmd.Parameters.Add(New SqlParameter("@SEARCH_NAME", oBeI_nav_cliente.Search_Name))
            cmd.Parameters.Add(New SqlParameter("@VAT_REGISTRATRION_NO", oBeI_nav_cliente.VAT_Registratrion_No))
            cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_cliente.Location_Code))

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

    Public Shared Function Actualizar(ByRef oBeI_nav_cliente As clsBeI_nav_cliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

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
            Upd.Add("no", "@no", DataType.Parametro)
            Upd.Add("name", "@name", DataType.Parametro)
            Upd.Add("adress", "@adress", DataType.Parametro)
            Upd.Add("city", "@city", DataType.Parametro)
            Upd.Add("country", "@country", DataType.Parametro)
            Upd.Add("phone_no", "@phone_no", DataType.Parametro)
            Upd.Add("contactname", "@contactname", DataType.Parametro)
            Upd.Add("search_name", "@search_name", DataType.Parametro)
            Upd.Add("vat_registratrion_no", "@vat_registratrion_no", DataType.Parametro)
            Upd.Add("location_code", "@location_code", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_cliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeI_nav_cliente.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeI_nav_cliente.Nombre_cliente))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeI_nav_cliente.Nit))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeI_nav_cliente.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_cliente.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_cliente.No))
            cmd.Parameters.Add(New SqlParameter("@NAME", oBeI_nav_cliente.Name))
            If Not oBeI_nav_cliente.Adress Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ADRESS", oBeI_nav_cliente.Adress))
            If Not oBeI_nav_cliente.City Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CITY", oBeI_nav_cliente.City))
            cmd.Parameters.Add(New SqlParameter("@COUNTRY", oBeI_nav_cliente.Country))
            cmd.Parameters.Add(New SqlParameter("@PHONE_NO", oBeI_nav_cliente.Phone_No))
            cmd.Parameters.Add(New SqlParameter("@CONTACTNAME", oBeI_nav_cliente.ContactName))
            cmd.Parameters.Add(New SqlParameter("@SEARCH_NAME", oBeI_nav_cliente.Search_Name))
            cmd.Parameters.Add(New SqlParameter("@VAT_REGISTRATRION_NO", oBeI_nav_cliente.VAT_Registratrion_No))
            cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_cliente.Location_Code))

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

    Public Shared Function Eliminar(ByRef oBeI_nav_cliente As clsBeI_nav_cliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_cliente" &
             "  Where(IdCliente = @IdCliente)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_cliente.IdCliente))

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
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeI_nav_cliente)

        Dim lReturnList As New List(Of clsBeI_nav_cliente)

        Try

            'WHERE procesado_wms = 0 
            Const sp As String = "SELECT * FROM I_nav_cliente "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_cliente As New clsBeI_nav_cliente

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_cliente = New clsBeI_nav_cliente()
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_cliente)

        Dim lReturnList As New List(Of clsBeI_nav_cliente)

        Try

            Const sp As String = "SELECT * FROM I_nav_cliente"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_cliente As New clsBeI_nav_cliente

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_cliente = New clsBeI_nav_cliente()
                    Cargar(vBeI_nav_cliente, dr)
                    lReturnList.Add(vBeI_nav_cliente)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeI_nav_cliente As clsBeI_nav_cliente)

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

                        Dim vBeI_nav_cliente As New clsBeI_nav_cliente

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_cliente, lDataTable.Rows(0))
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_cliente "

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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCliente),0) FROM I_nav_cliente"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
