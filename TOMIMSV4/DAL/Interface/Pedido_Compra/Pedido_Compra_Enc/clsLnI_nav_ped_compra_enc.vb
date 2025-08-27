Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_compra_enc

    Public Shared Sub Cargar(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc, ByRef dr As DataRow)

        Try

            With oBeI_nav_ped_compra_enc

                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Buy_From_Vendor_No = IIf(IsDBNull(dr.Item("Buy_From_Vendor_No")), "", dr.Item("Buy_From_Vendor_No"))
                .Buy_From_Vendor_Name = IIf(IsDBNull(dr.Item("Buy_From_Vendor_Name")), "", dr.Item("Buy_From_Vendor_Name"))
                .Posting_Description = IIf(IsDBNull(dr.Item("Posting_Description")), "", dr.Item("Posting_Description"))
                .Posting_Date = IIf(IsDBNull(dr.Item("Posting_Date")), Date.Now, dr.Item("Posting_Date"))
                .Order_Date = IIf(IsDBNull(dr.Item("Order_Date")), Date.Now, dr.Item("Order_Date"))
                .Document_Date = IIf(IsDBNull(dr.Item("Document_Date")), Date.Now, dr.Item("Document_Date"))
                .Vendor_Invoice_No = IIf(IsDBNull(dr.Item("Vendor_Invoice_No")), "", dr.Item("Vendor_Invoice_No"))
                .Status = IIf(IsDBNull(dr.Item("Status")), "0", dr.Item("Status"))
                .Payment_Terms_Code = IIf(IsDBNull(dr.Item("Payment_Terms_Code")), "", dr.Item("Payment_Terms_Code"))
                .Ship_To_Name = IIf(IsDBNull(dr.Item("Ship_To_Name")), "", dr.Item("Ship_To_Name"))
                .Location_Code = IIf(IsDBNull(dr.Item("Location_Code")), "", dr.Item("Location_Code"))
                .Ship_To_Contact = IIf(IsDBNull(dr.Item("Ship_To_Contact")), "", dr.Item("Ship_To_Contact"))
                .Expected_Receipt_Date = IIf(IsDBNull(dr.Item("Expected_Receipt_Date")), Nothing, dr.Item("Expected_Receipt_Date"))
                .Is_Internal_Transfer = IIf(IsDBNull(dr.Item("Is_Internal_Transfer")), False, dr.Item("Is_Internal_Transfer"))
                .Product_Owner_Code = IIf(IsDBNull(dr.Item("Product_Owner_Code")), "", dr.Item("Product_Owner_Code"))
                .Internal_Transfer_Document_No = IIf(IsDBNull(dr.Item("Internal_Transfer_Document_No")), "", dr.Item("Internal_Transfer_Document_No"))
                .Document_Type = IIf(IsDBNull(dr.Item("Document_Type")), 1, dr.Item("Document_Type"))
                .IsImport = IIf(IsDBNull(dr.Item("IsImport")), 0, dr.Item("IsImport"))
                .Company_Code = IIf(IsDBNull(dr.Item("Company_Code")), 0, dr.Item("Company_Code"))

            End With

        Catch ex1 As SqlException
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ped_compra_enc")
            If oBeI_nav_ped_compra_enc.No IsNot Nothing Then Ins.Add("no", "@no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_No IsNot Nothing Then Ins.Add("buy_from_vendor_no", "@buy_from_vendor_no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name IsNot Nothing Then Ins.Add("buy_from_vendor_name", "@buy_from_vendor_name", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Posting_Description IsNot Nothing Then Ins.Add("posting_description", "@posting_description", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Posting_Date IsNot Nothing Then Ins.Add("posting_date", "@posting_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Order_Date IsNot Nothing Then Ins.Add("order_date", "@order_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Document_Date IsNot Nothing Then Ins.Add("document_date", "@document_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Vendor_Invoice_No IsNot Nothing Then Ins.Add("vendor_invoice_no", "@vendor_invoice_no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then Ins.Add("status", "@status", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Payment_Terms_Code Is Nothing Then Ins.Add("payment_terms_code", "@payment_terms_code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Ship_To_Name IsNot Nothing Then Ins.Add("ship_to_name", "@ship_to_name", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Location_Code IsNot Nothing Then Ins.Add("location_code", "@location_code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Ship_To_Contact IsNot Nothing Then Ins.Add("ship_to_contact", "@ship_to_contact", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Expected_Receipt_Date Is Nothing Then Ins.Add("expected_receipt_date", "@expected_receipt_date", DataType.Parametro)
            Ins.Add("Is_Internal_Transfer", "@Is_Internal_Transfer", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Product_Owner_Code IsNot Nothing Then Ins.Add("Product_Owner_Code", "@Product_Owner_Code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No IsNot Nothing Then Ins.Add("Internal_Transfer_Document_No", "@Internal_Transfer_Document_No", DataType.Parametro)
            Ins.Add("document_type", "@document_type", DataType.Parametro)
            Ins.Add("IsImport", "@IsImport", DataType.Parametro)
            Ins.Add("Company_Code", "@Company_Code", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If oBeI_nav_ped_compra_enc.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_enc.No))
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@BUY_FROM_VENDOR_NO", oBeI_nav_ped_compra_enc.Buy_From_Vendor_No))
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@BUY_FROM_VENDOR_NAME", oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name))
            If oBeI_nav_ped_compra_enc.Posting_Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@POSTING_DESCRIPTION", oBeI_nav_ped_compra_enc.Posting_Description))
            If oBeI_nav_ped_compra_enc.Posting_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@POSTING_DATE", oBeI_nav_ped_compra_enc.Posting_Date))
            If oBeI_nav_ped_compra_enc.Order_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ORDER_DATE", oBeI_nav_ped_compra_enc.Order_Date))
            If oBeI_nav_ped_compra_enc.Document_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DOCUMENT_DATE", oBeI_nav_ped_compra_enc.Document_Date))
            If oBeI_nav_ped_compra_enc.Vendor_Invoice_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@VENDOR_INVOICE_NO", oBeI_nav_ped_compra_enc.Vendor_Invoice_No))
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_compra_enc.Status))
            If oBeI_nav_ped_compra_enc.Payment_Terms_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@PAYMENT_TERMS_CODE", oBeI_nav_ped_compra_enc.Payment_Terms_Code))
            If oBeI_nav_ped_compra_enc.Ship_To_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SHIP_TO_NAME", oBeI_nav_ped_compra_enc.Ship_To_Name))
            If oBeI_nav_ped_compra_enc.Location_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_ped_compra_enc.Location_Code))
            If oBeI_nav_ped_compra_enc.Ship_To_Contact IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SHIP_TO_CONTACT", oBeI_nav_ped_compra_enc.Ship_To_Contact))
            If oBeI_nav_ped_compra_enc.Expected_Receipt_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@EXPECTED_RECEIPT_DATE", oBeI_nav_ped_compra_enc.Expected_Receipt_Date))
            cmd.Parameters.Add(New SqlParameter("@IS_INTERNAL_TRANSFER", oBeI_nav_ped_compra_enc.Is_Internal_Transfer))
            If oBeI_nav_ped_compra_enc.Product_Owner_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@Product_Owner_Code", oBeI_nav_ped_compra_enc.Product_Owner_Code))
            If oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@Internal_Transfer_Document_No", oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No))
            cmd.Parameters.Add(New SqlParameter("@document_type", oBeI_nav_ped_compra_enc.Document_Type))
            cmd.Parameters.Add(New SqlParameter("@IsImport", oBeI_nav_ped_compra_enc.IsImport))
            cmd.Parameters.Add(New SqlParameter("@Company_Code", oBeI_nav_ped_compra_enc.Company_Code))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_compra_enc")
            If oBeI_nav_ped_compra_enc.No IsNot Nothing Then Upd.Add("no", "@no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_No IsNot Nothing Then Upd.Add("buy_from_vendor_no", "@buy_from_vendor_no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name IsNot Nothing Then Upd.Add("buy_from_vendor_name", "@buy_from_vendor_name", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Posting_Description IsNot Nothing Then Upd.Add("posting_description", "@posting_description", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Posting_Date IsNot Nothing Then Upd.Add("posting_date", "@posting_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Order_Date IsNot Nothing Then Upd.Add("order_date", "@order_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Document_Date IsNot Nothing Then Upd.Add("document_date", "@document_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Vendor_Invoice_No IsNot Nothing Then Upd.Add("vendor_invoice_no", "@vendor_invoice_no", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then Upd.Add("status", "@status", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Payment_Terms_Code IsNot Nothing Then Upd.Add("payment_terms_code", "@payment_terms_code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Ship_To_Name IsNot Nothing Then Upd.Add("ship_to_name", "@ship_to_name", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Location_Code IsNot Nothing Then Upd.Add("location_code", "@location_code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Ship_To_Contact IsNot Nothing Then Upd.Add("ship_to_contact", "@ship_to_contact", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Expected_Receipt_Date IsNot Nothing Then Upd.Add("expected_receipt_date", "@expected_receipt_date", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Product_Owner_Code IsNot Nothing Then Upd.Add("Product_Owner_Code", "@Product_Owner_Code", DataType.Parametro)
            If oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No IsNot Nothing Then Upd.Add("Internal_Transfer_Document_No", "@Internal_Transfer_Document_No", DataType.Parametro)
            Upd.Add("document_type", "@document_type", DataType.Parametro)
            Upd.Add("IsImport", "@IsImport", DataType.Parametro)
            Upd.Add("Is_Internal_Transfer", "@Is_Internal_Transfer", DataType.Parametro)
            Upd.Add("Company_Code", "@Company_Code", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If oBeI_nav_ped_compra_enc.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_enc.No))
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@BUY_FROM_VENDOR_NO", oBeI_nav_ped_compra_enc.Buy_From_Vendor_No))
            If oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@BUY_FROM_VENDOR_NAME", oBeI_nav_ped_compra_enc.Buy_From_Vendor_Name))
            If oBeI_nav_ped_compra_enc.Posting_Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@POSTING_DESCRIPTION", oBeI_nav_ped_compra_enc.Posting_Description))
            If oBeI_nav_ped_compra_enc.Posting_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@POSTING_DATE", oBeI_nav_ped_compra_enc.Posting_Date))
            If oBeI_nav_ped_compra_enc.Order_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ORDER_DATE", oBeI_nav_ped_compra_enc.Order_Date))
            If oBeI_nav_ped_compra_enc.Document_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DOCUMENT_DATE", oBeI_nav_ped_compra_enc.Document_Date))
            If oBeI_nav_ped_compra_enc.Vendor_Invoice_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@VENDOR_INVOICE_NO", oBeI_nav_ped_compra_enc.Vendor_Invoice_No))
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_compra_enc.Status))
            If oBeI_nav_ped_compra_enc.Payment_Terms_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@PAYMENT_TERMS_CODE", oBeI_nav_ped_compra_enc.Payment_Terms_Code))
            If oBeI_nav_ped_compra_enc.Ship_To_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SHIP_TO_NAME", oBeI_nav_ped_compra_enc.Ship_To_Name))
            If oBeI_nav_ped_compra_enc.Location_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_ped_compra_enc.Location_Code))
            If oBeI_nav_ped_compra_enc.Ship_To_Contact IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SHIP_TO_CONTACT", oBeI_nav_ped_compra_enc.Ship_To_Contact))
            If oBeI_nav_ped_compra_enc.Expected_Receipt_Date IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@EXPECTED_RECEIPT_DATE", oBeI_nav_ped_compra_enc.Expected_Receipt_Date))
            cmd.Parameters.Add(New SqlParameter("@Is_Internal_Transfer", oBeI_nav_ped_compra_enc.Is_Internal_Transfer))
            If oBeI_nav_ped_compra_enc.Product_Owner_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@Product_Owner_Code", oBeI_nav_ped_compra_enc.Product_Owner_Code))
            If oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@Internal_Transfer_Document_No", oBeI_nav_ped_compra_enc.Internal_Transfer_Document_No))
            cmd.Parameters.Add(New SqlParameter("@document_type", oBeI_nav_ped_compra_enc.Document_Type))
            cmd.Parameters.Add(New SqlParameter("@IsImport", oBeI_nav_ped_compra_enc.IsImport))
            cmd.Parameters.Add(New SqlParameter("@Company_Code", oBeI_nav_ped_compra_enc.Company_Code))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_compra_enc" &
             "  Where(No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_enc.No))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()

        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_compra_enc"
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            If rowsAffected >= 0 Then
                Return True
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_enc"
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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc) As Boolean

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_enc" &
            " Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_enc.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ped_compra_enc, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_ped_compra_enc)

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_enc)
            Const sp As String = "SELECT * FROM I_nav_ped_compra_enc "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_compra_enc As New clsBeI_nav_ped_compra_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_compra_enc = New clsBeI_nav_ped_compra_enc
                Cargar(vBeI_nav_ped_compra_enc, dr)
                lReturnList.Add(vBeI_nav_ped_compra_enc)

            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc)

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_enc 
             Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_compra_enc.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ped_compra_enc, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(No),0) FROM I_nav_ped_compra_enc"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_compra_enc")
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then Upd.Add("status", "@status", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If oBeI_nav_ped_compra_enc.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_enc.No))
            If oBeI_nav_ped_compra_enc.Status IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_compra_enc.Status))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

End Class
