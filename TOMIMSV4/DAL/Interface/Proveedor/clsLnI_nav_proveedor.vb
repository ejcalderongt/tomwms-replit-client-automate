Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_proveedor

    Public Shared Sub Cargar(ByRef oBeI_nav_proveedor As clsBeI_nav_proveedor, ByRef dr As DataRow)
        Try
            With oBeI_nav_proveedor
                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Name = IIf(IsDBNull(dr.Item("Name")), "", dr.Item("Name"))
                .Adress = IIf(IsDBNull(dr.Item("Adress")), "", dr.Item("Adress"))
                .City = IIf(IsDBNull(dr.Item("City")), "", dr.Item("City"))
                .Country = IIf(IsDBNull(dr.Item("Country")), "", dr.Item("Country"))
                .Phone_No = IIf(IsDBNull(dr.Item("Phone_No")), "", dr.Item("Phone_No"))
                .Contact = IIf(IsDBNull(dr.Item("Contact")), "", dr.Item("Contact"))
                .Search_Name = IIf(IsDBNull(dr.Item("Search_Name")), "", dr.Item("Search_Name"))
                .VAT_Registratrion_No = IIf(IsDBNull(dr.Item("VAT_Registratrion_No")), "", dr.Item("VAT_Registratrion_No"))
                .Location_Code = IIf(IsDBNull(dr.Item("Location_Code")), "", dr.Item("Location_Code"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_proveedor As clsBeI_nav_proveedor,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_proveedor")
            If Not oBeI_nav_proveedor.No Is Nothing Then Ins.Add("no", "@no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Name Is Nothing Then Ins.Add("name", "@name", DataType.Parametro)
            If Not oBeI_nav_proveedor.Adress Is Nothing Then Ins.Add("adress", "@adress", DataType.Parametro)
            If Not oBeI_nav_proveedor.City Is Nothing Then Ins.Add("city", "@city", DataType.Parametro)
            If Not oBeI_nav_proveedor.Country Is Nothing Then Ins.Add("country", "@country", DataType.Parametro)
            If Not oBeI_nav_proveedor.Phone_No Is Nothing Then Ins.Add("phone_no", "@phone_no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Contact Is Nothing Then Ins.Add("contact", "@contact", DataType.Parametro)
            If Not oBeI_nav_proveedor.Search_Name Is Nothing Then Ins.Add("search_name", "@search_name", DataType.Parametro)
            If Not oBeI_nav_proveedor.VAT_Registratrion_No Is Nothing Then Ins.Add("vat_registratrion_no", "@vat_registratrion_no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Location_Code Is Nothing Then Ins.Add("location_code", "@location_code", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If Not oBeI_nav_proveedor.No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_proveedor.No))
            If Not oBeI_nav_proveedor.Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NAME", oBeI_nav_proveedor.Name))
            If Not oBeI_nav_proveedor.Adress Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ADRESS", oBeI_nav_proveedor.Adress))
            If Not oBeI_nav_proveedor.City Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CITY", oBeI_nav_proveedor.City))
            If Not oBeI_nav_proveedor.Country Is Nothing Then cmd.Parameters.Add(New SqlParameter("@COUNTRY", oBeI_nav_proveedor.Country))
            If Not oBeI_nav_proveedor.Phone_No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PHONE_NO", oBeI_nav_proveedor.Phone_No))
            If Not oBeI_nav_proveedor.Contact Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CONTACT", oBeI_nav_proveedor.Contact))
            If Not oBeI_nav_proveedor.Search_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SEARCH_NAME", oBeI_nav_proveedor.Search_Name))
            If Not oBeI_nav_proveedor.VAT_Registratrion_No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@VAT_REGISTRATRION_NO", oBeI_nav_proveedor.VAT_Registratrion_No))
            If Not oBeI_nav_proveedor.Location_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_proveedor.Location_Code))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_proveedor As clsBeI_nav_proveedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_proveedor")
            If Not oBeI_nav_proveedor.No Is Nothing Then Upd.Add("no", "@no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Name Is Nothing Then Upd.Add("name", "@name", DataType.Parametro)
            If Not oBeI_nav_proveedor.Adress Is Nothing Then Upd.Add("adress", "@adress", DataType.Parametro)
            If Not oBeI_nav_proveedor.City Is Nothing Then Upd.Add("city", "@city", DataType.Parametro)
            If Not oBeI_nav_proveedor.Country Is Nothing Then Upd.Add("country", "@country", DataType.Parametro)
            If Not oBeI_nav_proveedor.Phone_No Is Nothing Then Upd.Add("phone_no", "@phone_no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Contact Is Nothing Then Upd.Add("contact", "@contact", DataType.Parametro)
            If Not oBeI_nav_proveedor.Search_Name Is Nothing Then Upd.Add("search_name", "@search_name", DataType.Parametro)
            If Not oBeI_nav_proveedor.VAT_Registratrion_No Is Nothing Then Upd.Add("vat_registratrion_no", "@vat_registratrion_no", DataType.Parametro)
            If Not oBeI_nav_proveedor.Location_Code Is Nothing Then Upd.Add("location_code", "@location_code", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If Not oBeI_nav_proveedor.No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_proveedor.No))
            If Not oBeI_nav_proveedor.Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NAME", oBeI_nav_proveedor.Name))
            If Not oBeI_nav_proveedor.Adress Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ADRESS", oBeI_nav_proveedor.Adress))
            If Not oBeI_nav_proveedor.City Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CITY", oBeI_nav_proveedor.City))
            If Not oBeI_nav_proveedor.Country Is Nothing Then cmd.Parameters.Add(New SqlParameter("@COUNTRY", oBeI_nav_proveedor.Country))
            If Not oBeI_nav_proveedor.Phone_No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PHONE_NO", oBeI_nav_proveedor.Phone_No))
            If Not oBeI_nav_proveedor.Contact Is Nothing Then cmd.Parameters.Add(New SqlParameter("@CONTACT", oBeI_nav_proveedor.Contact))
            If Not oBeI_nav_proveedor.Search_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SEARCH_NAME", oBeI_nav_proveedor.Search_Name))
            If Not oBeI_nav_proveedor.VAT_Registratrion_No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@VAT_REGISTRATRION_NO", oBeI_nav_proveedor.VAT_Registratrion_No))
            If Not oBeI_nav_proveedor.Location_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_proveedor.Location_Code))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeI_nav_proveedor As clsBeI_nav_proveedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_proveedor" &
             "  Where(No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_proveedor.No))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_proveedor"
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

End Class
