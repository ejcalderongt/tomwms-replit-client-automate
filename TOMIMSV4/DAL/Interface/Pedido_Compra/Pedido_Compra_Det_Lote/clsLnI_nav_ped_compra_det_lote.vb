Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_compra_det_lote

    Public Shared Sub Cargar(ByRef oBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote, ByRef dr As DataRow)
        Try
            With oBei_nav_ped_compra_det_lote
                .NoEnc = IIf(IsDBNull(dr.Item("NoEnc")), "", dr.Item("NoEnc"))
                .source_ID = IIf(IsDBNull(dr.Item("source_ID")), "", dr.Item("source_ID"))
                .Source_Prod_Order_Line = IIf(IsDBNull(dr.Item("Source_Prod_Order_Line")), 0, dr.Item("Source_Prod_Order_Line"))
                .Item_No = IIf(IsDBNull(dr.Item("Item_No")), 0, dr.Item("Item_No"))
                .Lot_No = IIf(IsDBNull(dr.Item("Lot_No")), "", dr.Item("Lot_No"))
                .Expiration_Date = IIf(IsDBNull(dr.Item("Expiration_Date")), Date.Now, dr.Item("Expiration_Date"))
                .Entry_No = IIf(IsDBNull(dr.Item("Entry_No")), "", dr.Item("Entry_No"))
                .Source_Type = IIf(IsDBNull(dr.Item("Source_Type")), 0, dr.Item("Source_Type"))
                .Quantity_Base = IIf(IsDBNull(dr.Item("Quantity_Base")), 0.0, dr.Item("Quantity_Base"))
                .Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), "", dr.Item("Variant_Code"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ped_compra_det_lote")
            Ins.Add("noenc", "@noenc", DataType.Parametro)
            Ins.Add("source_ID", "@source_ID", DataType.Parametro)
            Ins.Add("source_prod_order_line", "@source_prod_order_line", DataType.Parametro)
            Ins.Add("item_no", "@item_no", DataType.Parametro)
            Ins.Add("lot_no", "@lot_no", DataType.Parametro)
            Ins.Add("expiration_date", "@expiration_date", DataType.Parametro)
            If oBei_nav_ped_compra_det_lote.Entry_No IsNot Nothing Then Ins.Add("entry_no", "@entry_no", DataType.Parametro)
            Ins.Add("source_type", "@source_type", DataType.Parametro)
            Ins.Add("quantity_base", "@quantity_base", DataType.Parametro)
            If oBei_nav_ped_compra_det_lote.Variant_Code IsNot Nothing Then Ins.Add("variant_code", "@variant_code", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBei_nav_ped_compra_det_lote.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@source_ID", oBei_nav_ped_compra_det_lote.source_ID))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_PROD_ORDER_LINE", oBei_nav_ped_compra_det_lote.Source_Prod_Order_Line))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBei_nav_ped_compra_det_lote.Item_No))
            cmd.Parameters.Add(New SqlParameter("@LOT_NO", oBei_nav_ped_compra_det_lote.Lot_No))
            cmd.Parameters.Add(New SqlParameter("@EXPIRATION_DATE", oBei_nav_ped_compra_det_lote.Expiration_Date))
            If oBei_nav_ped_compra_det_lote.Entry_No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ENTRY_NO", oBei_nav_ped_compra_det_lote.Entry_No))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_TYPE", oBei_nav_ped_compra_det_lote.Source_Type))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY_BASE", oBei_nav_ped_compra_det_lote.Quantity_Base))
            If oBei_nav_ped_compra_det_lote.Variant_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBei_nav_ped_compra_det_lote.Variant_Code))

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

    Public Shared Function Actualizar(ByRef oBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_compra_det_lote")
            Upd.Add("noenc", "@noenc", DataType.Parametro)
            Upd.Add("source_ID", "@source_ID", DataType.Parametro)
            Upd.Add("source_prod_order_line", "@source_prod_order_line", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("lot_no", "@lot_no", DataType.Parametro)
            Upd.Add("expiration_date", "@expiration_date", DataType.Parametro)
            Upd.Add("entry_no", "@entry_no", DataType.Parametro)
            Upd.Add("source_type", "@source_type", DataType.Parametro)
            Upd.Add("quantity_base", "@quantity_base", DataType.Parametro)
            If oBei_nav_ped_compra_det_lote.Variant_Code IsNot Nothing Then Upd.Add("variant_code", "@variant_code", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc" &
                " AND Source_Prod_Order_Line = @Source_Prod_Order_Line" &
                " AND Item_No = @Item_No")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBei_nav_ped_compra_det_lote.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@source_ID", oBei_nav_ped_compra_det_lote.source_ID))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_PROD_ORDER_LINE", oBei_nav_ped_compra_det_lote.Source_Prod_Order_Line))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBei_nav_ped_compra_det_lote.Item_No))
            cmd.Parameters.Add(New SqlParameter("@LOT_NO", oBei_nav_ped_compra_det_lote.Lot_No))
            cmd.Parameters.Add(New SqlParameter("@EXPIRATION_DATE", oBei_nav_ped_compra_det_lote.Expiration_Date))
            cmd.Parameters.Add(New SqlParameter("@ENTRY_NO", oBei_nav_ped_compra_det_lote.Entry_No))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_TYPE", oBei_nav_ped_compra_det_lote.Source_Type))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY_BASE", oBei_nav_ped_compra_det_lote.Quantity_Base))
            If oBei_nav_ped_compra_det_lote.Variant_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBei_nav_ped_compra_det_lote.Variant_Code))

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

    Public Shared Function Eliminar(ByRef oBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from i_nav_ped_compra_det_lote" &
             "  Where(NoEnc = @NoEnc)" &
             "  AND (Source_Prod_Order_Line = @Source_Prod_Order_Line)" &
             "  AND (Item_No = @Item_No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBei_nav_ped_compra_det_lote.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@SOURCE_PROD_ORDER_LINE", oBei_nav_ped_compra_det_lote.Source_Prod_Order_Line))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBei_nav_ped_compra_det_lote.Item_No))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        EliminarTodos = False

        Try

            Const sp As String = " Delete from i_nav_ped_compra_det_lote"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                '#CKFK 20211108 Agregué la transacción
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM i_nav_ped_compra_det_lote"
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

    Public Shared Function Obtener(ByRef oBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM i_nav_ped_compra_det_lote" &
            " Where(NoEnc = @NoEnc)" &
            " AND (Source_Prod_Order_Line = @Source_Prod_Order_Line)" &
            " AND (Item_No = @Item_No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", oBei_nav_ped_compra_det_lote.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@SOURCE_PROD_ORDER_LINE", oBei_nav_ped_compra_det_lote.Source_Prod_Order_Line))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@ITEM_NO", oBei_nav_ped_compra_det_lote.Item_No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBei_nav_ped_compra_det_lote, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_ped_compra_det_lote)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_det_lote)
            Const sp As String = "SELECT * FROM i_nav_ped_compra_det_lote "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBei_nav_ped_compra_det_lote As New clsBeI_nav_ped_compra_det_lote

            For Each dr As DataRow In dt.Rows
                vBei_nav_ped_compra_det_lote = New clsBeI_nav_ped_compra_det_lote
                Cargar(vBei_nav_ped_compra_det_lote, dr)
                lReturnList.Add(vBei_nav_ped_compra_det_lote)
            Next

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

    Public Shared Function GetSingle(ByRef pBei_nav_ped_compra_det_lote As clsBeI_nav_ped_compra_det_lote)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)


            Const sp As String = "SELECT * FROM i_nav_ped_compra_det_lote" &
            " Where(NoEnc = @NoEnc)" &
            " AND (Source_Prod_Order_Line = @Source_Prod_Order_Line)" &
            " AND (Item_No = @Item_No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBei_nav_ped_compra_det_lote.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@SOURCE_PROD_ORDER_LINE", pBei_nav_ped_compra_det_lote.Source_Prod_Order_Line))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@ITEM_NO", pBei_nav_ped_compra_det_lote.Item_No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBei_nav_ped_compra_det_lote, dt.Rows(0))
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

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(NoEnc),0) FROM i_nav_ped_compra_det_lote"

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

End Class
