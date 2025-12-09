Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_traslado_enc

    Public Shared Sub Cargar(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, ByRef dr As DataRow)

        Try

            With oBeI_nav_ped_traslado_enc

                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Posting_Date = IIf(IsDBNull(dr.Item("Posting_Date")), Nothing, dr.Item("Posting_Date"))
                .Receipt_Date = IIf(IsDBNull(dr.Item("Receipt_Date")), Nothing, dr.Item("Receipt_Date"))
                .Shipment_Date = IIf(IsDBNull(dr.Item("Shipment_Date")), Nothing, dr.Item("Shipment_Date"))
                .Status = IIf(IsDBNull(dr.Item("Status")), False, dr.Item("Status"))
                .Transfer_from_Code = IIf(IsDBNull(dr.Item("Transfer_from_Code")), "", dr.Item("Transfer_from_Code"))
                .Transfer_from_Contact = IIf(IsDBNull(dr.Item("Transfer_from_Contact")), "", dr.Item("Transfer_from_Contact"))
                .Transfer_from_Name = IIf(IsDBNull(dr.Item("Transfer_from_Name")), "", dr.Item("Transfer_from_Name"))
                .Transfer_to_Code = IIf(IsDBNull(dr.Item("Transfer_to_Code")), "", dr.Item("Transfer_to_Code"))
                .Transfer_to_Contact = IIf(IsDBNull(dr.Item("Transfer_to_Contact")), "", dr.Item("Transfer_to_Contact"))
                .Transfer_to_Name = IIf(IsDBNull(dr.Item("Transfer_to_Name")), "", dr.Item("Transfer_to_Name"))
                .Transfer_to_CodeField = IIf(IsDBNull(dr.Item("transfer_to_CodeField")), "", dr.Item("transfer_to_CodeField"))
                .Product_Owner_Code = IIf(IsDBNull(dr.Item("Product_Owner_Code")), "", dr.Item("Product_Owner_Code"))
                .Receipt_Document_Reference = IIf(IsDBNull(dr.Item("Receipt_Document_Reference")), "", dr.Item("Receipt_Document_Reference"))
                .Document_Type = IIf(IsDBNull(dr.Item("document_type")), 0, dr.Item("document_type"))
                .External_Document_No = IIf(IsDBNull(dr.Item("external_document_no")), 0, dr.Item("external_document_no"))
                .RoadCodigoRuta = IIf(IsDBNull(dr.Item("external_document_no")), "0", dr.Item("external_document_no"))
                .RoadCodigoVendedor = IIf(IsDBNull(dr.Item("external_document_no")), "0", dr.Item("external_document_no"))
                .Manufacturing_Process = IIf(IsDBNull(dr.Item("Manufacturing_Process")), "0", dr.Item("Manufacturing_Process"))
                .Address = IIf(IsDBNull(dr.Item("Address")), "", dr.Item("Address"))
                .Comments = IIf(IsDBNull(dr.Item("Comments")), "", dr.Item("Comments"))
                .Company_Code = IIf(IsDBNull(dr.Item("Comments")), "", dr.Item("Comments"))
                .IsExport = IIf(IsDBNull(dr.Item("IsExport")), "", dr.Item("IsExport"))
                .Transportation_Guide = IIf(IsDBNull(dr.Item("Transportation_Guide")), "", dr.Item("Transportation_Guide"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub
    Public Shared Function Insertar(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ped_traslado_enc")
            Ins.Add("no", "@no", DataType.Parametro)
            Ins.Add("posting_date", "@posting_date", DataType.Parametro)
            Ins.Add("receipt_date", "@receipt_date", DataType.Parametro)
            Ins.Add("shipment_date", "@shipment_date", DataType.Parametro)
            Ins.Add("status", "@status", DataType.Parametro)
            Ins.Add("transfer_from_code", "@transfer_from_code", DataType.Parametro)
            Ins.Add("transfer_from_name", "@transfer_from_name", DataType.Parametro)
            Ins.Add("transfer_to_code", "@transfer_to_code", DataType.Parametro)
            Ins.Add("transfer_to_name", "@transfer_to_name", DataType.Parametro)
            Ins.Add("transfer_to_codefield", "@transfer_to_codefield", DataType.Parametro)
            Ins.Add("product_owner_code", "@product_owner_code", DataType.Parametro)
            Ins.Add("receipt_document_reference", "@receipt_document_reference", DataType.Parametro)
            Ins.Add("document_type", "@document_type", DataType.Parametro)
            Ins.Add("Manufacturing_Process", "@Manufacturing_Process", DataType.Parametro)
            Ins.Add("Address", "@Address", DataType.Parametro)
            Ins.Add("Comments", "@Comments", DataType.Parametro)
            Ins.Add("Company_Code", "@Company_Code", DataType.Parametro)
            Ins.Add("IsExport", "@IsExport", DataType.Parametro)
            If Not oBeI_nav_ped_traslado_enc.Transportation_Guide Is Nothing Then Ins.Add("Transportation_Guide", "@Transportation_Guide", DataType.Parametro)

            If Not oBeI_nav_ped_traslado_enc.External_Document_No Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.External_Document_No.Trim = "" Then Ins.Add("external_document_no", "@external_document_no", DataType.Parametro)
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim = "" Then Ins.Add("RoadCodigoRuta", "@RoadCodigoRuta", DataType.Parametro)
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim = "" Then Ins.Add("RoadCodigoVendedor", "@RoadCodigoVendedor", DataType.Parametro)
            End If

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))
            cmd.Parameters.Add(New SqlParameter("@POSTING_DATE", oBeI_nav_ped_traslado_enc.Posting_Date))
            cmd.Parameters.Add(New SqlParameter("@RECEIPT_DATE", oBeI_nav_ped_traslado_enc.Receipt_Date))
            cmd.Parameters.Add(New SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_enc.Shipment_Date))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_enc.Status))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_CODE", oBeI_nav_ped_traslado_enc.Transfer_from_Code))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_NAME", oBeI_nav_ped_traslado_enc.Transfer_from_Name))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODE", oBeI_nav_ped_traslado_enc.Transfer_to_Code))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_NAME", oBeI_nav_ped_traslado_enc.Transfer_to_Name))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_enc.Transfer_to_CodeField))
            cmd.Parameters.Add(New SqlParameter("@PRODUCT_OWNER_CODE", oBeI_nav_ped_traslado_enc.Product_Owner_Code))
            cmd.Parameters.Add(New SqlParameter("@RECEIPT_DOCUMENT_REFERENCE", oBeI_nav_ped_traslado_enc.Receipt_Document_Reference))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENT_TYPE", oBeI_nav_ped_traslado_enc.Document_Type))
            cmd.Parameters.Add(New SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_ped_traslado_enc.Manufacturing_Process))
            cmd.Parameters.Add(New SqlParameter("@ADDRESS", oBeI_nav_ped_traslado_enc.Address))
            cmd.Parameters.Add(New SqlParameter("@COMMENTS", oBeI_nav_ped_traslado_enc.Comments))
            cmd.Parameters.Add(New SqlParameter("@COMPANY_CODE", oBeI_nav_ped_traslado_enc.Company_Code))
            If Not oBeI_nav_ped_traslado_enc.Transportation_Guide Is Nothing Then cmd.Parameters.Add(New SqlParameter("@TRANSPORTATION_GUIDE", oBeI_nav_ped_traslado_enc.Transportation_Guide))

            If Not oBeI_nav_ped_traslado_enc.External_Document_No Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.External_Document_No.Trim = "" Then cmd.Parameters.Add(New SqlParameter("@EXTERNAL_DOCUMENT_NO", oBeI_nav_ped_traslado_enc.External_Document_No))
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim = "" Then cmd.Parameters.Add(New SqlParameter("@RoadCodigoRuta", oBeI_nav_ped_traslado_enc.RoadCodigoRuta))
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim = "" Then cmd.Parameters.Add(New SqlParameter("@RoadCodigoVendedor", oBeI_nav_ped_traslado_enc.RoadCodigoVendedor))
            End If

            cmd.Parameters.Add(New SqlParameter("@ISEXPORT", oBeI_nav_ped_traslado_enc.IsExport))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeI_nav_ped_traslado_enc.No = CStr(cmd.Parameters("@NO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_enc")
            Upd.Add("no", "@no", DataType.Parametro)
            Upd.Add("posting_date", "@posting_date", DataType.Parametro)
            Upd.Add("receipt_date", "@receipt_date", DataType.Parametro)
            Upd.Add("shipment_date", "@shipment_date", DataType.Parametro)
            Upd.Add("status", "@status", DataType.Parametro)
            Upd.Add("transfer_from_code", "@transfer_from_code", DataType.Parametro)
            Upd.Add("transfer_from_contact", "@transfer_from_contact", DataType.Parametro)
            Upd.Add("transfer_from_name", "@transfer_from_name", DataType.Parametro)
            Upd.Add("transfer_to_code", "@transfer_to_code", DataType.Parametro)
            Upd.Add("transfer_to_contact", "@transfer_to_contact", DataType.Parametro)
            Upd.Add("transfer_to_name", "@transfer_to_name", DataType.Parametro)
            Upd.Add("transfer_to_codefield", "@transfer_to_codefield", DataType.Parametro)
            Upd.Add("product_owner_code", "@product_owner_code", DataType.Parametro)
            Upd.Add("receipt_document_reference", "@receipt_document_reference", DataType.Parametro)
            Upd.Add("document_type", "@document_type", DataType.Parametro)
            Upd.Add("external_document_no", "@external_document_no", DataType.Parametro)
            Upd.Add("Manufacturing_Process", "@Manufacturing_Process", DataType.Parametro)
            Upd.Add("Address", "@Address", DataType.Parametro)
            Upd.Add("Comments", "@Comments", DataType.Parametro)
            Upd.Add("Company_Code", "@Company_Code", DataType.Parametro)
            Upd.Add("IsExport", "@IsExport", DataType.Parametro)
            Upd.Add("Transportation_Guide", "@Transportation_Guide", DataType.Parametro)

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim = "" Then Upd.Add("RoadCodigoRuta", "@RoadCodigoRuta", DataType.Parametro)
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim = "" Then Upd.Add("RoadCodigoVendedor", "@RoadCodigoVendedor", DataType.Parametro)
            End If

            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))
            cmd.Parameters.Add(New SqlParameter("@POSTING_DATE", oBeI_nav_ped_traslado_enc.Posting_Date))
            cmd.Parameters.Add(New SqlParameter("@RECEIPT_DATE", oBeI_nav_ped_traslado_enc.Receipt_Date))
            cmd.Parameters.Add(New SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_enc.Shipment_Date))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_enc.Status))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_CODE", oBeI_nav_ped_traslado_enc.Transfer_from_Code))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_CONTACT", oBeI_nav_ped_traslado_enc.Transfer_from_Contact))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_NAME", oBeI_nav_ped_traslado_enc.Transfer_from_Name))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODE", oBeI_nav_ped_traslado_enc.Transfer_to_Code))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CONTACT", oBeI_nav_ped_traslado_enc.Transfer_to_Contact))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_NAME", oBeI_nav_ped_traslado_enc.Transfer_to_Name))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_enc.Transfer_to_CodeField))
            cmd.Parameters.Add(New SqlParameter("@PRODUCT_OWNER_CODE", oBeI_nav_ped_traslado_enc.Product_Owner_Code))
            cmd.Parameters.Add(New SqlParameter("@RECEIPT_DOCUMENT_REFERENCE", oBeI_nav_ped_traslado_enc.Receipt_Document_Reference))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENT_TYPE", oBeI_nav_ped_traslado_enc.Document_Type))
            cmd.Parameters.Add(New SqlParameter("@EXTERNAL_DOCUMENT_NO", oBeI_nav_ped_traslado_enc.External_Document_No))
            cmd.Parameters.Add(New SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_ped_traslado_enc.Manufacturing_Process))
            cmd.Parameters.Add(New SqlParameter("@ADDRESS", oBeI_nav_ped_traslado_enc.Address))
            cmd.Parameters.Add(New SqlParameter("@COMMENTS", oBeI_nav_ped_traslado_enc.Comments))
            cmd.Parameters.Add(New SqlParameter("@COMPANY_CODE", oBeI_nav_ped_traslado_enc.Company_Code))
            cmd.Parameters.Add(New SqlParameter("@TRANSPORTATION_GUIDE", oBeI_nav_ped_traslado_enc.Transportation_Guide))

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoRuta.Trim = "" Then cmd.Parameters.Add(New SqlParameter("@RoadCodigoRuta", oBeI_nav_ped_traslado_enc.RoadCodigoRuta))
            End If

            If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor Is Nothing Then
                If Not oBeI_nav_ped_traslado_enc.RoadCodigoVendedor.Trim = "" Then cmd.Parameters.Add(New SqlParameter("@RoadCodigoVendedor", oBeI_nav_ped_traslado_enc.RoadCodigoVendedor))
            End If

            cmd.Parameters.Add(New SqlParameter("@ISEXPORT", oBeI_nav_ped_traslado_enc.IsExport))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Documento_De_Ingreso_En_Bodega_Destino(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_enc")
            Upd.Add("receipt_document_reference", "@receipt_document_reference", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))
            cmd.Parameters.Add(New SqlParameter("@RECEIPT_DOCUMENT_REFERENCE", oBeI_nav_ped_traslado_enc.Receipt_Document_Reference))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_traslado_enc" &
             "  Where(No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_traslado_enc"
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

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc" &
            " Where(No = @No)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ped_traslado_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_ped_traslado_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_enc)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc
                Cargar(vBeI_nav_ped_traslado_enc, dr)
                lReturnList.Add(vBeI_nav_ped_traslado_enc)

            Next

            lConnection.Dispose()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc)

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc" &
            " Where(No = @No)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_traslado_enc.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ped_traslado_enc, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pNo As String,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction)


        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc" &
            " Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc
                Cargar(pBeI_nav_ped_traslado_enc, dt.Rows(0))
                Return pBeI_nav_ped_traslado_enc
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(No),0) FROM I_nav_ped_traslado_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_Pedido_Traslado_By_Referencia(ByVal Ref As String) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT enc.No,enc.Transfer_from_Code,enc.Transfer_to_Code,det.no as Codigo,det.Description as Producto,det.Quantity as Cantidad
                    FROM i_nav_ped_traslado_det det inner join 
                    i_nav_ped_traslado_enc enc on enc.No = det.NoEnc
                    WHERE enc.no = @Referencia 
                    Order By det.no"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@Referencia", Ref)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)

                End Using

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_Pedido_Traslado_By_Referencia(ByVal Ref As String,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            '#CKFK20240829 Agregué el nombre de la presentación 
            '#CKFK20250516 Corregí la relación con la presentación,
            'porque el producto tiene dos presentaciones se duplica la información
            Dim vSQL As String = "SELECT enc.No,enc.Transfer_from_Code,enc.Transfer_to_Code,det.no as 
                                         Codigo,det.Description as Producto,det.Quantity as Cantidad, det.Unit_Of_Measure_Code as UM, 
                                         CASE WHEN det.Variant_Code IS NOT NULL THEN PP.nombre ELSE '' END  as Presentacion, 
                                         IsNull(det.Quantity_Reserved_WMS,0) Cantidad_Reservada,
                                         det.Status, det.Process_Result as Resultado, det.Size as Talla, det.Color 
                                  FROM i_nav_ped_traslado_det det INNER JOIN 
                                       i_nav_ped_traslado_enc enc on enc.No = det.NoEnc LEFT OUTER JOIN
	                                   producto_presentacion pp ON pp.codigo = det.No AND pp.nombre = det.Unit_of_Measure_Code
                                  WHERE enc.no = @Referencia 
                                  Order By det.no "

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Parameters.AddWithValue("@Referencia", Ref)
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.Fill(lTable)

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC202301242213:Actualizar estado a importado.
    ''' </summary>
    ''' <param name="oBeI_nav_ped_traslado_enc"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Estado(ByRef oBeI_nav_ped_traslado_enc As clsBeI_nav_ped_traslado_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_enc")
            Upd.Add("status", "@status", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_enc.No))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_enc.Status))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Pedidos_Incompletos() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_Pedidos_Incompletos = Nothing

        Try

            Dim vSQL As String = "SELECT distinct enc.[No] 
                                  FROM i_nav_ped_traslado_det det inner join 
                                  i_nav_ped_traslado_enc enc on enc.No = det.NoEnc 
                                  WHERE det.Process_Result <> 'Ok' "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_Pedidos_Incompletos = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
