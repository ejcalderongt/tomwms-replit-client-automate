Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_traslado_det

    Public Shared Sub Cargar(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det, ByRef dr As DataRow)

        Try

            With oBeI_nav_ped_traslado_det

                .NoEnc = IIf(IsDBNull(dr.Item("NoEnc")), "", dr.Item("NoEnc"))
                .Line_No = IIf(IsDBNull(dr.Item("Line_No")), "", dr.Item("Line_No"))
                .Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), "", dr.Item("Variant_Code"))
                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Description = IIf(IsDBNull(dr.Item("Description")), "", dr.Item("Description"))
                .Item_No = IIf(IsDBNull(dr.Item("Item_No")), "", dr.Item("Item_No"))
                .Qty_to_Receive = IIf(IsDBNull(dr.Item("Qty_to_Receive")), 0.0, dr.Item("Qty_to_Receive"))
                .Qty_to_Ship = IIf(IsDBNull(dr.Item("Qty_to_Ship")), 0.0, dr.Item("Qty_to_Ship"))
                .Quantity = IIf(IsDBNull(dr.Item("Quantity")), 0.0, dr.Item("Quantity"))
                .Transfer_to_CodeField = IIf(IsDBNull(dr.Item("transfer_to_CodeField")), "", dr.Item("transfer_to_CodeField"))
                .Transfer_From_CodeField = IIf(IsDBNull(dr.Item("Transfer_From_CodeField")), "", dr.Item("Transfer_From_CodeField"))
                .Shipment_Date = IIf(IsDBNull(dr.Item("Shipment_Date")), Nothing, dr.Item("Shipment_Date"))
                .Unit_of_Measure_Code = IIf(IsDBNull(dr.Item("Unit_of_Measure_Code")), "", dr.Item("Unit_of_Measure_Code"))
                .Status = IIf(IsDBNull(dr.Item("Status")), "0", dr.Item("Status"))
                .Price = IIf(IsDBNull(dr.Item("Price")), "0", dr.Item("Price"))
                .Source_ID = IIf(IsDBNull(dr.Item("Source_ID")), "", dr.Item("Source_ID"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .Is_Partially_Processed = IIf(IsDBNull(dr.Item("Is_Partially_Processed")), False, dr.Item("Is_Partially_Processed"))
                .Scan_Type = IIf(IsDBNull(dr.Item("Scan_Type")), "", dr.Item("Scan_Type"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                .Size = IIf(IsDBNull(dr.Item("Size")), "", dr.Item("Size"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ped_traslado_det")
            Ins.Add("noenc", "@noenc", DataType.Parametro)
            Ins.Add("Line_No", "@Line_No", DataType.Parametro)
            If Not oBeI_nav_ped_traslado_det.Variant_Code Is Nothing Then Ins.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            Ins.Add("no", "@no", DataType.Parametro)
            Ins.Add("description", "@description", DataType.Parametro)
            Ins.Add("item_no", "@item_no", DataType.Parametro)
            Ins.Add("qty_to_receive", "@qty_to_receive", DataType.Parametro)
            Ins.Add("qty_to_ship", "@qty_to_ship", DataType.Parametro)
            Ins.Add("quantity", "@quantity", DataType.Parametro)
            Ins.Add("transfer_to_codefield", "@transfer_to_codefield", DataType.Parametro)
            Ins.Add("shipment_date", "@shipment_date", DataType.Parametro)
            Ins.Add("status", "@status", DataType.Parametro)
            Ins.Add("price", "@price", DataType.Parametro)
            Ins.Add("unit_of_measure_code", "@unit_of_measure_code", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("is_partially_processed", "@is_partially_processed", DataType.Parametro)
            Ins.Add("transfer_from_codefield", "@transfer_from_codefield", DataType.Parametro)
            Ins.Add("scan_type", "@scan_type", DataType.Parametro)
            Ins.Add("Color", "@Color", DataType.Parametro)
            Ins.Add("Size", "@Size", DataType.Parametro)

            If Not oBeI_nav_ped_traslado_det.Source_ID Is Nothing Then Ins.Add("source_id", "@source_id", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))
            If Not oBeI_nav_ped_traslado_det.Variant_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@Variant_Code", oBeI_nav_ped_traslado_det.Variant_Code))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description)))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField))
            cmd.Parameters.Add(New SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date))
            cmd.Parameters.Add(New SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status))
            cmd.Parameters.Add(New SqlParameter("@PRICE", oBeI_nav_ped_traslado_det.Price))
            If Not oBeI_nav_ped_traslado_det.Source_ID Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SOURCE_ID", oBeI_nav_ped_traslado_det.Source_ID))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_From_CodeField))
            cmd.Parameters.Add(New SqlParameter("@SCAN_TYPE", oBeI_nav_ped_traslado_det.Scan_Type))
            cmd.Parameters.Add(New SqlParameter("@Color", oBeI_nav_ped_traslado_det.Color))
            cmd.Parameters.Add(New SqlParameter("@Size", oBeI_nav_ped_traslado_det.Size))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeI_nav_ped_traslado_det.NoEnc = CStr(cmd.Parameters("@NOENC").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("noenc", "@noenc", DataType.Parametro)
            Upd.Add("Line_No", "@Line_No", DataType.Parametro)
            Upd.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            Upd.Add("no", "@no", DataType.Parametro)
            Upd.Add("description", "@description", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("qty_to_receive", "@qty_to_receive", DataType.Parametro)
            Upd.Add("qty_to_ship", "@qty_to_ship", DataType.Parametro)
            Upd.Add("quantity", "@quantity", DataType.Parametro)
            If Not oBeI_nav_ped_traslado_det.Transfer_to_CodeField Is Nothing Then Upd.Add("transfer_to_codefield", "@transfer_to_codefield", DataType.Parametro)
            Upd.Add("shipment_date", "@shipment_date", DataType.Parametro)
            Upd.Add("unit_of_measure_code", "@unit_of_measure_code", DataType.Parametro)
            Upd.Add("status", "@status", DataType.Parametro)
            Upd.Add("price", "@price", DataType.Parametro)
            Upd.Add("source_id", "@source_id", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("is_partially_processed", "@is_partially_processed", DataType.Parametro)
            Ins.Add("transfer_from_codefield", "@transfer_from_codefield", DataType.Parametro)
            Ins.Add("scan_type", "@scan_type", DataType.Parametro)
            Upd.Add("Color", "@Color", DataType.Parametro)
            Upd.Add("Size", "@Size", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc" &
                " AND No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@Line_No", oBeI_nav_ped_traslado_det.Line_No))
            cmd.Parameters.Add(New SqlParameter("@Variant_Code", oBeI_nav_ped_traslado_det.Variant_Code))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            '#CKFK20221024 Agregué la condición para que quite el &
            cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description)))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField))
            cmd.Parameters.Add(New SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date))
            cmd.Parameters.Add(New SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status))
            cmd.Parameters.Add(New SqlParameter("@PRICE", oBeI_nav_ped_traslado_det.Price))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_ID", oBeI_nav_ped_traslado_det.Source_ID))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_FROM_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_From_CodeField))
            cmd.Parameters.Add(New SqlParameter("@SCAN_TYPE", oBeI_nav_ped_traslado_det.Scan_Type))
            cmd.Parameters.Add(New SqlParameter("@Color", oBeI_nav_ped_traslado_det.Color))
            cmd.Parameters.Add(New SqlParameter("@Size", oBeI_nav_ped_traslado_det.Size))

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

    Public Shared Function Actualizar_Status_Det(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("status", "@status", DataType.Parametro)
            Upd.Add("process_result", "@process_result", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc" &
                " AND No = @No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            cmd.Parameters.Add(New SqlParameter("@STATUS", oBeI_nav_ped_traslado_det.Status))
            cmd.Parameters.Add(New SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result))


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

    Public Shared Function Actualizar_Process_Result(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("Process_Result", "@Process_Result", DataType.Parametro)
            Upd.Add("Qty_to_Receive", "@Qty_to_Receive", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No AND Item_No = @Item_No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_RemotaPorParametro As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_RemotaPorParametro Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))

            cmd.Parameters.Add(New SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive))
            cmd.Parameters.Add(New SqlParameter("@PROCESS_RESULT", oBeI_nav_ped_traslado_det.Process_Result))

            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_RemotaPorParametro Then
                lTransaction.Commit()
            End If

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_traslado_det" &
             "  Where(NoEnc = @NoEnc)" &
             "  AND (No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))


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

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing,
                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_traslado_det"
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

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det"
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

    Public Shared Function Obtener(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det" &
            " Where(NoEnc = @NoEnc)" &
            " AND (No = @No)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ped_traslado_det, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_ped_traslado_det)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_det)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_det As New clsBeI_nav_ped_traslado_det

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_det = New clsBeI_nav_ped_traslado_det
                Cargar(vBeI_nav_ped_traslado_det, dr)
                lReturnList.Add(vBeI_nav_ped_traslado_det)

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

    Public Shared Function GetSingle(ByRef pBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det)

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det" &
            " Where(NoEnc = @NoEnc)" &
            " AND (No = @No)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_traslado_det.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_traslado_det.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ped_traslado_det, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(NoEnc),0) FROM I_nav_ped_traslado_det"

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

    Public Shared Function Actualizar_IdPedidoDet(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("IdPedidoDet", "@IdPedidoDet", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No AND Item_No = @Item_No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No))


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

    Public Shared Function Tiene_Clientes_Diferentes(ByVal pNoEnc As String) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Tiene_Clientes_Diferentes = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT DISTINCT transfer_to_codefield FROM I_nav_ped_traslado_det 
            Where(NoEnc = @NoEnc) "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pNoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 1 Then
                Tiene_Clientes_Diferentes = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    'Public Shared Function Tiene_Clientes_Diferentes(ByVal pNoEnc As String,
    '                                                 ByVal lConnection As SqlConnection,
    '                                                 ByVal lTransaction As SqlTransaction) As Boolean

    '    Tiene_Clientes_Diferentes = False

    '    Try

    '        Const sp As String = "Select DISTINCT transfer_to_codefield FROM I_nav_ped_traslado_det 
    '                              Where(NoEnc = @NoEnc) "


    '        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pNoEnc))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count > 1 Then
    '            Tiene_Clientes_Diferentes = True
    '        End If

    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function

    'Public Shared Function Get_All_Consolidado(ByVal pNoEnc As String,
    '                                           ByVal lConnection As SqlConnection,
    '                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_ped_traslado_det)

    '    Try

    '        Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_det)
    '        Const sp As String = "SELECT * FROM I_nav_ped_traslado_det WHERE NoEnc = @NoEnc "
    '        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.AddWithValue("@NoEnc", pNoEnc)
    '        Dim dt As New DataTable

    '        dad.Fill(dt)

    '        Dim vBeI_nav_ped_traslado_det As New clsBeI_nav_ped_traslado_det

    '        For Each dr As DataRow In dt.Rows

    '            vBeI_nav_ped_traslado_det = New clsBeI_nav_ped_traslado_det
    '            Cargar(vBeI_nav_ped_traslado_det, dr)
    '            lReturnList.Add(vBeI_nav_ped_traslado_det)

    '        Next

    '        cmd.Dispose()

    '        Return lReturnList

    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Actualizar_Partially_Processed(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("is_partially_processed", "@is_partially_processed", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No AND IdPedidoDet = @IdPedidoDet ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            cmd.Parameters.Add(New SqlParameter("@IS_PARTIALLY_PROCESSED", oBeI_nav_ped_traslado_det.Is_Partially_Processed))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeI_nav_ped_traslado_det.IdPedidoDet))


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

    '#EJC202312112048_ANT:
    'Public Shared Function Actualizar_Quantity_Reserved_WMS(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
    '                                                        ByVal oBeProducto As clsBeProducto,
    '                                                        Optional ByVal pConection As SqlConnection = Nothing,
    '                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing
    '    Dim tmpBeProductoPresentacion As New clsBeProducto_Presentacion

    '    Try

    '        tmpBeProductoPresentacion = Nothing

    '        Upd.Init("i_nav_ped_traslado_det")
    '        Upd.Add("Quantity_Reserved_WMS", "ISNULL(Quantity_Reserved_WMS,0) + @Quantity_Reserved_WMS", DataType.Parametro)
    '        Upd.Where("NoEnc = @NoEnc 
    '                   AND No = @No 
    '                   AND Line_No = @Line_No")

    '        Dim sp As String = Upd.SQL()

    '        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
    '        Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

    '        If Es_Transaccion_Remota Then
    '            cmd = New SqlCommand(sp, pConection, pTransaction)
    '            If oBeI_nav_ped_traslado_det.Unit_of_Measure_Code <> oBeProducto.UnidadMedida.Nombre Then
    '                tmpBeProductoPresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(oBeProducto.IdProducto,
    '                                                                                                                   oBeI_nav_ped_traslado_det.Unit_of_Measure_Code,
    '                                                                                                                   pConection,
    '                                                                                                                   pTransaction)
    '            End If
    '        Else
    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
    '            cmd = New SqlCommand(sp, lConnection, lTransaction)
    '            If oBeI_nav_ped_traslado_det.Unit_of_Measure_Code <> oBeProducto.UnidadMedida.Nombre Then
    '                tmpBeProductoPresentacion =
    '                    clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(oBeProducto.IdProducto,
    '                                                                                           oBeI_nav_ped_traslado_det.Unit_of_Measure_Code,
    '                                                                                           pConection,
    '                                                                                           pTransaction)
    '            End If
    '        End If

    '        If tmpBeProductoPresentacion IsNot Nothing Then
    '            If tmpBeProductoPresentacion.Factor > 0 Then
    '                oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS = oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS / tmpBeProductoPresentacion.Factor
    '            End If
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
    '        cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
    '        cmd.Parameters.Add(New SqlParameter("@QUANTITY_RESERVED_WMS", oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS))
    '        cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))

    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        cmd.Dispose()

    '        If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '        Return rowsAffected

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '    End Try

    'End Function

    Public Shared Function Actualizar_Quantity_Reserved_WMS(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                                            ByVal oBeProducto As clsBeProducto,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim tmpBeProductoPresentacion As New clsBeProducto_Presentacion

        Try

            tmpBeProductoPresentacion = Nothing

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("Quantity_Reserved_WMS", "@Quantity_Reserved_WMS", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc 
                       AND No = @No 
                       AND Line_No = @Line_No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY_RESERVED_WMS", oBeI_nav_ped_traslado_det.Quantity_Reserved_WMS))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det.Line_No))

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

End Class