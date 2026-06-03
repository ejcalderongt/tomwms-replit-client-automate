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

    Public Shared Function BuscarProductoBodega(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                    IdBodegaDestino As Integer,
                                                    BeConfigEnc As clsBeI_nav_config_enc,
                                                    lConnection As SqlConnection,
                                                    lTransInterface As SqlTransaction) As clsBeProducto_bodega
        Try
            Dim productoBodega As clsBeProducto_bodega = Nothing

            If BeConfigEnc.Equiparar_Productos Then
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(navPedidoCompraDet.No,
                                                                                IdBodegaDestino,
                                                                                lConnection,
                                                                                lTransInterface)

                If productoBodega Is Nothing Then
                    productoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(navPedidoCompraDet.No,
                                                                                   IdBodegaDestino,
                                                                                   lConnection,
                                                                                   lTransInterface)

                    If productoBodega Is Nothing Then
                        productoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(navPedidoCompraDet.No,
                                                                                          IdBodegaDestino,
                                                                                          lConnection,
                                                                                          lTransInterface)
                    End If
                End If
            Else
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(navPedidoCompraDet.No,
                                                                                IdBodegaDestino,
                                                                                lConnection,
                                                                                lTransInterface)
            End If

            Return productoBodega

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function ValidarYCalcularUMBas(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                 ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                 ByRef BePresentacion As clsBeProducto_Presentacion,
                                                 BeProductoBodega As clsBeProducto_bodega,
                                                 BeConfigEnc As clsBeI_nav_config_enc,
                                                 ByRef vCantidadSolicitadaPedido As Double,
                                                 ByRef vCantidadEnteraPres As Double,
                                                 ByRef vCantidadDecimalUMBas As Double,
                                                 ByRef lblprg As String,
                                                 lConnection As SqlConnection,
                                                 lTransInterface As SqlTransaction) As Boolean

        Try
            ' Buscar UM básica por código y propietario
            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                            BeConfigEnc.IdPropietario,
                                                                                            lConnection,
                                                                                            lTransInterface)

            If BeUnidadMedidaPedCompra IsNot Nothing Then
                ' Confirmar que el producto existe con la unidad medida
                If Not clsLnProducto.Existe(navPedidoCompraDet.No, BeUnidadMedidaPedCompra.IdUnidadMedida, lConnection, lTransInterface) Then
                    ' Buscar presentación por código variante si existe
                    If navPedidoCompraDet.Variant_Code <> "" Then
                        BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                                  navPedidoCompraDet.Variant_Code,
                                                                                                  lConnection,
                                                                                                  lTransInterface)
                        If BePresentacion IsNot Nothing Then
                            BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida
                        Else
                            Throw New Exception("ERROR_20220727_1228A: No se encontró la presentación asociada al código: " & navPedidoCompraDet.No &
                                            " Con código de variante: " & navPedidoCompraDet.Variant_Code & " para el IdProducto: " &
                                            BeProductoBodega.IdProducto & " en la línea " & navPedidoCompraDet.Line_No)
                        End If
                    End If
                End If
            Else
                If BeProductoBodega.Producto.UnidadMedida Is Nothing Then
                    Throw New Exception($"Producto: {navPedidoCompraDet.No} UnidMedBas No definida")
                End If

                ' Buscar presentación por nombre
                BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                       navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                       lConnection,
                                                                                       lTransInterface)

                If BePresentacion IsNot Nothing Then
                    BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida
                Else
                    Throw New Exception($"La unidad de medida: {navPedidoCompraDet.Unit_of_Measure_Code} no está definida para el código de producto:{navPedidoCompraDet.No} en la tabla unidad_medida.")
                End If
            End If

            ' Evaluar si se requiere conversión a UM básica
            If BeConfigEnc.Convertir_decimales_a_umbas = 1 AndAlso BeConfigEnc.Interface_SAP Then
                BePresentacion = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(BeProductoBodega.IdProducto,
                                                                                                   lConnection,
                                                                                                   lTransInterface)

                If BePresentacion IsNot Nothing Then
                    If BePresentacion.Factor <= 0 Then
                        Throw New Exception("ERROR_202210251745: El factor es 0 para la presentación NO se puede inferir la conversión.")
                    End If

                    clsPublic.Split_Decimal(navPedidoCompraDet.Quantity / BePresentacion.Factor,
                                            vCantidadEnteraPres,
                                            vCantidadDecimalUMBas)

                    vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * BePresentacion.Factor)
                    vCantidadEnteraPres = vCantidadEnteraPres * BePresentacion.Factor

                    vCantidadSolicitadaPedido = If(vCantidadEnteraPres > 0, vCantidadEnteraPres, vCantidadDecimalUMBas)
                Else
                    vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity
                End If
            Else
                vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity
            End If

            Return True
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function InsertarDetalleOrdenCompra(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                      navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                      BeProductoBodega As clsBeProducto_bodega,
                                                      BePresentacion As clsBeProducto_Presentacion,
                                                      BeConfigEnc As clsBeI_nav_config_enc,
                                                      BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                      vCantidadEnteraPres As Double,
                                                      vCantidadDecimalUMBas As Double,
                                                      ByRef vContadorLineasDetInsertadas As Integer,
                                                      ByRef lblprg As String,
                                                      ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                                      ByRef LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                                      ByVal gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                      ByVal pDetallePickingUbic As List(Of clsBeTrans_picking_ubic),
                                                      ByVal pControlTallaColor As Boolean,
                                                      lConnection As SqlConnection,
                                                      lTransInterface As SqlTransaction) As Boolean
        Try

            Dim BePedidoCompraDet As New clsBeTrans_oc_det() With {
                .IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc,
                .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
            }

            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
            BePedidoCompraDet.Codigo_Producto = IIf(navPedidoCompraDet.Barcode = "", BeProductoBodega.Producto.Codigo, navPedidoCompraDet.Barcode)
            BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description

            If Not (BeConfigEnc.Convertir_decimales_a_umbas = 1 Or BeConfigEnc.Interface_SAP) AndAlso vCantidadEnteraPres > 0 Then
                BePedidoCompraDet.Cantidad = Math.Round(vCantidadEnteraPres / BePresentacion.Factor, 6)
                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
            Else
                BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
            End If

            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
            BePedidoCompraDet.Activo = True
            BePedidoCompraDet.Porcentaje_arancel = 0
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code
            BePedidoCompraDet.IdPresentacion = If(BePresentacion IsNot Nothing, BePresentacion.IdPresentacion, 0)
            BePedidoCompraDet.Presentacion.IdPresentacion = BePedidoCompraDet.IdPresentacion

            If navPedidoCompraDet.Barcode <> "" Then
                Dim BeProductoTallaColor As New clsBeProducto_talla_color
                BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_Params(BeProductoBodega.IdProducto, navPedidoCompraDet.Size, navPedidoCompraDet.Color, lConnection, lTransInterface)

                If BeProductoTallaColor IsNot Nothing Then
                    BePedidoCompraDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor
                Else
                    lblprg += "No existe la Talla/Color definidas para el código " & navPedidoCompraDet.No & vbNewLine
                    Return False
                End If
            End If

            If Asigna_Unidad_De_Medida(BePedidoCompraDet, navPedidoCompraDet, BeUnidadMedidaPedCompra, BeProductoBodega, lConnection, lTransInterface) Then

                If Not (BeConfigEnc.Convertir_decimales_a_umbas = 1 Or BeConfigEnc.Interface_SAP) AndAlso vCantidadDecimalUMBas > 0 Then
                    Dim BePedidoCompraDetUmBas As New clsBeTrans_oc_det()
                    clsPublic.CopyObject(BePedidoCompraDet, BePedidoCompraDetUmBas)
                    BePedidoCompraDetUmBas.IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc
                    BePedidoCompraDetUmBas.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
                    BePedidoCompraDetUmBas.Cantidad = vCantidadDecimalUMBas
                    BePedidoCompraDetUmBas.IdPresentacion = 0
                    BePedidoCompraDetUmBas.Presentacion.IdPresentacion = 0
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDetUmBas, lConnection, lTransInterface)
                Else
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransInterface)
                End If

                If Not navPedidoCompraEnc.Internal_Transfer_Document_No = "" Then

                    If Not pDetallePickingUbic Is Nothing AndAlso pDetallePickingUbic.Count > 0 Then

                        Dim lMaxIdLoteDet As Integer = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransInterface) + 1
                        Dim lMaxIdPallet As Integer = clsLnI_nav_barras_pallet.MaxID(lConnection, lTransInterface) + 1
                        Dim BeTransReDet As New clsBeTrans_re_det
                        Dim BeINavBarraPallet As New clsBeI_nav_barras_pallet
                        Dim BeINavBarraPalletOriginal As New clsBeI_nav_barras_pallet
                        Dim BeStock As New clsBeStock
                        Dim loteDouble As Double = 0
                        Dim loteEntero As Integer = 0
                        Dim lFiltroPickingUbic As List(Of clsBeTrans_picking_ubic) = Nothing

                        'talla_color
                        If BeConfigEnc.Control_lote Then

                            Dim vCodigoProducto As String = If(navPedidoCompraDet.No, "").Trim().ToUpperInvariant()
                            Dim vCodigoTalla As String = If(navPedidoCompraDet.Size, "").Trim().ToUpperInvariant()
                            Dim vCodigoColor As String = If(navPedidoCompraDet.Color, "").Trim().ToUpperInvariant()

                            Dim lFiltroPickingUbic As List(Of clsBeTrans_picking_ubic)

                            If pControlTallaColor Then
                                '#EJC20260603_FIX_REC_TRASLADO_TC: filtro por bodega con control talla/color.
                                'Incluye fallback para historial con No_Linea=0.
                                lFiltroPickingUbic = pDetallePickingUbic.Where(Function(x) If(x Is Nothing, False,
                                                                                If(x.CodigoProducto, "").Trim().ToUpperInvariant() = vCodigoProducto AndAlso
                                                                                If(x.Codigo_Talla, "").Trim().ToUpperInvariant() = vCodigoTalla AndAlso
                                                                                If(x.Codigo_Color, "").Trim().ToUpperInvariant() = vCodigoColor AndAlso
                                                                                (x.No_Linea = navPedidoCompraDet.Line_No OrElse x.No_Linea = 0))).ToList()
                            Else
                                lFiltroPickingUbic = pDetallePickingUbic.Where(Function(x) If(x Is Nothing, False,
                                                                                If(x.CodigoProducto, "").Trim().ToUpperInvariant() = vCodigoProducto AndAlso
                                                                                (x.No_Linea = navPedidoCompraDet.Line_No OrElse x.No_Linea = 0))).ToList()
                            End If

                            For Each BePickingUbic As clsBeTrans_picking_ubic In lFiltroPickingUbic

                                BeOcDetLote = New clsBeTrans_oc_det_lote
                                BeOcDetLote.IdOrdenCompraDetLote = lMaxIdLoteDet
                                BeOcDetLote.IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc
                                BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                BeOcDetLote.Cantidad = BePickingUbic.Cantidad_despachada
                                BeOcDetLote.No_linea = BePedidoCompraDet.No_Linea
                                BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                BeOcDetLote.Lote = BePickingUbic.Lote
                                BeOcDetLote.Lic_Plate = BePickingUbic.Lic_plate
                                BeOcDetLote.Cantidad_recibida = 0
                                BeOcDetLote.Codigo_producto = BePedidoCompraDet.Codigo_Producto
                                BeOcDetLote.Fecha_vence = BePickingUbic.Fecha_Vence
                                BeOcDetLote.IdPresentacion = BePickingUbic.IdPresentacion
                                BeOcDetLote.Presentacion.IdPresentacion = BePickingUbic.IdPresentacion
                                BeOcDetLote.IdUnidadMedidaBasica = BePickingUbic.IdUnidadMedida
                                BeOcDetLote.UnidadMedida.IdUnidadMedida = BePickingUbic.IdUnidadMedida
                                BeOcDetLote.IdProductoTallaColor = BePickingUbic.IdProductoTallaColor
                                BeOcDetLote.Talla = BePickingUbic.Codigo_Talla
                                BeOcDetLote.Color = BePickingUbic.Codigo_Color
                                BeOcDetLote.Activo = True
                                BeOcDetLote.User_agr = BePedidoCompraDet.User_agr
                                BeOcDetLote.User_mod = BePedidoCompraDet.User_mod
                                clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransInterface)

                                lMaxIdLoteDet += 1

                            Next

                        End If

                    End If

                    vContadorLineasDetInsertadas += 1

                    Return True

                End If

                Return False

        Catch ex As Exception
            Dim vMsgEx3 As String = String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", ex.Message, ex.Source, vbNewLine)
            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgEx3, navPedidoCompraDet.Description, 0, 0)
            lblprg += vMsgEx3 & vbNewLine
            Throw New Exception(vMsgEx3)
        End Try

    End Function
    Public Shared Sub ValidarPresentaciones(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                            BeProductoBodega As clsBeProducto_bodega,
                                            ByRef BePresentacion As clsBeProducto_Presentacion,
                                            lConnection As SqlConnection,
                                            lTransInterface As SqlTransaction)
        Try
            ' Validación por código de unidad de medida (nombre)
            BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                       navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                       lConnection,
                                                                                       lTransInterface)

            ' Validación por variant_code si aplica
            If Not String.IsNullOrWhiteSpace(navPedidoCompraDet.Variant_Code) Then
                BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                           navPedidoCompraDet.Variant_Code,
                                                                                           lConnection,
                                                                                           lTransInterface)

                If BePresentacion Is Nothing Then
                    Throw New Exception("ERROR_20220727_1228E: No se encontró la presentación asociada al código: " &
                                    navPedidoCompraDet.No & " Con código variante: " & navPedidoCompraDet.Variant_Code)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function ActualizarDetalleOrdenCompra(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                        navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                        ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                                        ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                                        ByRef LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                                        BeProductoBodega As clsBeProducto_bodega,
                                                        ByRef VContadorBitacoraTOMWMS As Integer,
                                                        ByRef vContadorLineasDetInsertadas As Integer,
                                                        ByRef lblprg As String,
                                                        BeConfigEnc As clsBeI_nav_config_enc,
                                                        IdNavConfigDet As Integer,
                                                        PedidoCompraExistente As clsBeTrans_oc_enc,
                                                        lConnection As SqlConnection,
                                                        lTransInterface As SqlTransaction) As Boolean
        Try
            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
            BePedidoCompraDet.Codigo_Producto = BeProductoBodega.Producto.Codigo
            BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
            BePedidoCompraDet.Nombre_unidad_medida_basica = If(String.IsNullOrEmpty(navPedidoCompraDet.Variant_Code),
                                                               navPedidoCompraDet.Unit_of_Measure_Code,
                                                               BeProductoBodega.Producto.UnidadMedida.Nombre)

            Dim DifCant As Double = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad

            If BePedidoCompraDet.Cantidad <> 0 Then
                lblprg += vbNewLine
                lblprg += String.Format(If(DifCant = 0,
                                           "La cantidad no se modificó para pedido {0} producto {1} ",
                                           If(DifCant > 0,
                                              "La cantidad incrementó respecto a TOM para pedido {0} producto {1} ",
                                              "La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} ")),
                                        navPedidoCompraEnc.No,
                                        navPedidoCompraDet.No)
            End If

            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
            BePedidoCompraDet.Activo = True
            BePedidoCompraDet.Porcentaje_arancel = 0
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

            clsLnTrans_oc_det.Actualizar_Desde_Interface(BePedidoCompraDet, lConnection, lTransInterface)

            LotesExistentes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                             lConnection,
                                                                             lTransInterface)

            ProcesarLotes(navPedidoCompraEnc,
                          navPedidoCompraDet,
                          BeOcDetLote,
                          BePedidoCompraDet,
                          LotesExistentes,
                          lConnection,
                          lTransInterface)

            VContadorBitacoraTOMWMS += 1
            vContadorLineasDetInsertadas += 1

            Return True
        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(ex.Message)
            lblprg += String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine)
            Return False
        End Try
    End Function

End Class