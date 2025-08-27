Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_compra_det

    Public Shared Sub Cargar(ByRef oBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det, ByRef dr As DataRow)
        Try
            With oBeI_nav_ped_compra_det
                .NoEnc = IIf(IsDBNull(dr.Item("NoEnc")), "", dr.Item("NoEnc"))
                .Line_No = IIf(IsDBNull(dr.Item("Line_No")), "0", dr.Item("Line_No"))
                .Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), "", dr.Item("Variant_Code"))
                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Type = IIf(IsDBNull(dr.Item("Type")), "", dr.Item("Type"))
                .Description = IIf(IsDBNull(dr.Item("Description")), "", dr.Item("Description"))
                .Description2 = IIf(IsDBNull(dr.Item("Description2")), "", dr.Item("Description2"))
                .Location_Code = IIf(IsDBNull(dr.Item("Location_Code")), "", dr.Item("Location_Code"))
                .Quantity = IIf(IsDBNull(dr.Item("Quantity")), 0.0, dr.Item("Quantity"))
                .Unit_of_Measure_Code = IIf(IsDBNull(dr.Item("Unit_Of_Measure_Code")), "", dr.Item("Unit_Of_Measure_Code"))
                .Direct_Unit_Cost = IIf(IsDBNull(dr.Item("Direct_Unit_Cost")), 0.0, dr.Item("Direct_Unit_Cost"))
                .Line_Amount = IIf(IsDBNull(dr.Item("Line_Amount")), 0.0, dr.Item("Line_Amount"))
                .Quantity_Received = IIf(IsDBNull(dr.Item("Quantity_Received")), 0.0, dr.Item("Quantity_Received"))
                .Planed_Receipt_Date = IIf(IsDBNull(dr.Item("Planed_Receipt_Date")), Date.Now, dr.Item("Planed_Receipt_Date"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ped_compra_det")
            If oBeI_nav_ped_compra_det.NoEnc IsNot Nothing Then Ins.Add("noenc", "@noenc", DataType.Parametro)
            Ins.Add("Line_No", "@Line_No", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Variant_Code IsNot Nothing Then Ins.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            If oBeI_nav_ped_compra_det.No IsNot Nothing Then Ins.Add("no", "@no", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Type IsNot Nothing Then Ins.Add("type", "@type", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Description IsNot Nothing Then Ins.Add("description", "@description", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Description2 IsNot Nothing Then Ins.Add("description2", "@description2", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Location_Code IsNot Nothing Then Ins.Add("location_code", "@location_code", DataType.Parametro)
            Ins.Add("quantity", "@quantity", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Unit_of_Measure_Code IsNot Nothing Then Ins.Add("unit_of_measure_code", "@unit_of_measure_code", DataType.Parametro)
            Ins.Add("direct_unit_cost", "@direct_unit_cost", DataType.Parametro)
            Ins.Add("line_amount", "@line_amount", DataType.Parametro)
            Ins.Add("quantity_received", "@quantity_received", DataType.Parametro)
            Ins.Add("planed_receipt_date", "@planed_receipt_date", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            If oBeI_nav_ped_compra_det.NoEnc IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_compra_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_compra_det.Line_No))
            If oBeI_nav_ped_compra_det.Variant_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@Variant_Code", oBeI_nav_ped_compra_det.Variant_Code))
            If oBeI_nav_ped_compra_det.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_det.No))

            If oBeI_nav_ped_compra_det.Type IsNot Nothing Then
                If oBeI_nav_ped_compra_det.Type.GetType() Is GetType(String) Then
                    cmd.Parameters.Add(New SqlParameter("@TYPE", oBeI_nav_ped_compra_det.Type.ToString()))
                Else
                    cmd.Parameters.Add(New SqlParameter("@TYPE", oBeI_nav_ped_compra_det.Type))
                End If
            End If

            If oBeI_nav_ped_compra_det.Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", oBeI_nav_ped_compra_det.Description))
            If oBeI_nav_ped_compra_det.Description2 IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION2", oBeI_nav_ped_compra_det.Description2))
            If oBeI_nav_ped_compra_det.Location_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_ped_compra_det.Location_Code))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY", oBeI_nav_ped_compra_det.Quantity))
            If oBeI_nav_ped_compra_det.Unit_of_Measure_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_compra_det.Unit_of_Measure_Code))
            cmd.Parameters.Add(New SqlParameter("@DIRECT_UNIT_COST", oBeI_nav_ped_compra_det.Direct_Unit_Cost))
            cmd.Parameters.Add(New SqlParameter("@LINE_AMOUNT", oBeI_nav_ped_compra_det.Line_Amount))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY_RECEIVED", oBeI_nav_ped_compra_det.Quantity_Received))
            cmd.Parameters.Add(New SqlParameter("@PLANED_RECEIPT_DATE", oBeI_nav_ped_compra_det.Planed_Receipt_Date))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_Det {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ped_compra_det")
            If oBeI_nav_ped_compra_det.NoEnc IsNot Nothing Then Upd.Add("noenc", "@noenc", DataType.Parametro)
            Upd.Add("Line_No", "@Line_No", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Variant_Code IsNot Nothing Then Upd.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            If oBeI_nav_ped_compra_det.No IsNot Nothing Then Upd.Add("no", "@no", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Type IsNot Nothing Then Upd.Add("type", "@type", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Description IsNot Nothing Then Upd.Add("description", "@description", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Description2 IsNot Nothing Then Upd.Add("description2", "@description2", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Location_Code IsNot Nothing Then Upd.Add("location_code", "@location_code", DataType.Parametro)
            Upd.Add("quantity", "@quantity", DataType.Parametro)
            If oBeI_nav_ped_compra_det.Unit_of_Measure_Code IsNot Nothing Then Upd.Add("unit_of_measure_code", "@unit_of_measure_code", DataType.Parametro)
            Upd.Add("direct_unit_cost", "@direct_unit_cost", DataType.Parametro)
            Upd.Add("line_amount", "@line_amount", DataType.Parametro)
            Upd.Add("quantity_received", "@quantity_received", DataType.Parametro)
            Upd.Add("planed_receipt_date", "@planed_receipt_date", DataType.Parametro)
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

            If oBeI_nav_ped_compra_det.NoEnc IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_compra_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_compra_det.Line_No))
            If oBeI_nav_ped_compra_det.Variant_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeI_nav_ped_compra_det.Variant_Code))
            If oBeI_nav_ped_compra_det.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_det.No))
            If oBeI_nav_ped_compra_det.Type IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@TYPE", oBeI_nav_ped_compra_det.Type))
            If oBeI_nav_ped_compra_det.Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", oBeI_nav_ped_compra_det.Description))
            If oBeI_nav_ped_compra_det.Description2 IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION2", oBeI_nav_ped_compra_det.Description2))
            If oBeI_nav_ped_compra_det.Location_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_ped_compra_det.Location_Code))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY", oBeI_nav_ped_compra_det.Quantity))
            If oBeI_nav_ped_compra_det.Unit_of_Measure_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_compra_det.Unit_of_Measure_Code))
            cmd.Parameters.Add(New SqlParameter("@DIRECT_UNIT_COST", oBeI_nav_ped_compra_det.Direct_Unit_Cost))
            cmd.Parameters.Add(New SqlParameter("@LINE_AMOUNT", oBeI_nav_ped_compra_det.Line_Amount))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY_RECEIVED", oBeI_nav_ped_compra_det.Quantity_Received))
            cmd.Parameters.Add(New SqlParameter("@PLANED_RECEIPT_DATE", oBeI_nav_ped_compra_det.Planed_Receipt_Date))


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

    Public Shared Function Eliminar(ByRef oBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_compra_det" &
             "  Where(NoEnc = @NoEnc)" &
             "  AND (No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_compra_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_det.No))


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

        Try

            Const sp As String = " Delete from I_nav_ped_compra_det"
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

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_det"
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

    Public Shared Function Obtener(ByRef oBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_det 
             Where(NoEnc = @NoEnc)
             AND (No = @No) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_compra_det.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_compra_det.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ped_compra_det, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_ped_compra_det)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_det)
            Const sp As String = "SELECT * FROM I_nav_ped_compra_det "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_compra_det As New clsBeI_nav_ped_compra_det

            For Each dr As DataRow In dt.Rows
                vBeI_nav_ped_compra_det = New clsBeI_nav_ped_compra_det
                Cargar(vBeI_nav_ped_compra_det, dr)
                lReturnList.Add(vBeI_nav_ped_compra_det)
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

    Public Shared Function GetSingle(ByRef pBeI_nav_ped_compra_det As clsBeI_nav_ped_compra_det) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_det" &
            " Where(NoEnc = @NoEnc)" &
            " AND (No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_compra_det.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_compra_det.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ped_compra_det, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(NoEnc),0) FROM I_nav_ped_compra_det"

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
