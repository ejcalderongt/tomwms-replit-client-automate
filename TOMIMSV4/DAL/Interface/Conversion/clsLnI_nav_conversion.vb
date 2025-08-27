Imports System.Data.SqlClient

Public Class clsLnI_nav_conversion

    Public Shared Sub Cargar(ByRef oBeI_nav_conversion As clsBeI_nav_conversion, ByRef dr As DataRow)
        Try
            With oBeI_nav_conversion
                .IdConversion = IIf(IsDBNull(dr.Item("IdConversion")), 0, dr.Item("IdConversion"))
                .Item_No = IIf(IsDBNull(dr.Item("Item_No")), "", dr.Item("Item_No"))
                .Code = IIf(IsDBNull(dr.Item("Code")), "", dr.Item("Code"))
                .Qty_per_Unit_of_Measure = IIf(IsDBNull(dr.Item("Qty_per_Unit_of_Measure")), 0.0, dr.Item("Qty_per_Unit_of_Measure"))
                .Height = IIf(IsDBNull(dr.Item("Height")), 0.0, dr.Item("Height"))
                .Width = IIf(IsDBNull(dr.Item("Width")), 0.0, dr.Item("Width"))
                .Length = IIf(IsDBNull(dr.Item("Length")), 0.0, dr.Item("Length"))
                .Cubage = IIf(IsDBNull(dr.Item("Cubage")), 0.0, dr.Item("Cubage"))
                .Weight = IIf(IsDBNull(dr.Item("Weight")), 0.0, dr.Item("Weight"))
                .Package_Type = IIf(IsDBNull(dr.Item("Package_Type")), "", dr.Item("Package_Type"))
                .ItemUnitOfMeasure = IIf(IsDBNull(dr.Item("ItemUnitOfMeasure")), "", dr.Item("ItemUnitOfMeasure"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_conversion As clsBeI_nav_conversion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_conversion")
            Ins.Add("idconversion", "@idconversion", DataType.Parametro)
            Ins.Add("item_no", "@item_no", DataType.Parametro)
            Ins.Add("code", "@code", DataType.Parametro)
            Ins.Add("qty_per_unit_of_measure", "@qty_per_unit_of_measure", DataType.Parametro)
            Ins.Add("height", "@height", DataType.Parametro)
            Ins.Add("width", "@width", DataType.Parametro)
            Ins.Add("length", "@length", DataType.Parametro)
            Ins.Add("cubage", "@cubage", DataType.Parametro)
            Ins.Add("weight", "@weight", DataType.Parametro)
            If oBeI_nav_conversion.Package_Type IsNot Nothing Then Ins.Add("package_type", "@package_type", DataType.Parametro)
            If oBeI_nav_conversion.ItemUnitOfMeasure IsNot Nothing Then Ins.Add("itemunitofmeasure", "@itemunitofmeasure", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeI_nav_conversion.IdConversion))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_conversion.Item_No))
            cmd.Parameters.Add(New SqlParameter("@CODE", oBeI_nav_conversion.Code))
            cmd.Parameters.Add(New SqlParameter("@QTY_PER_UNIT_OF_MEASURE", oBeI_nav_conversion.Qty_per_Unit_of_Measure))
            cmd.Parameters.Add(New SqlParameter("@HEIGHT", oBeI_nav_conversion.Height))
            cmd.Parameters.Add(New SqlParameter("@WIDTH", oBeI_nav_conversion.Width))
            cmd.Parameters.Add(New SqlParameter("@LENGTH", oBeI_nav_conversion.Length))
            cmd.Parameters.Add(New SqlParameter("@CUBAGE", oBeI_nav_conversion.Cubage))
            cmd.Parameters.Add(New SqlParameter("@WEIGHT", oBeI_nav_conversion.Weight))
            If oBeI_nav_conversion.Package_Type IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@PACKAGE_TYPE", oBeI_nav_conversion.Package_Type))
            If oBeI_nav_conversion.ItemUnitOfMeasure IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEMUNITOFMEASURE", oBeI_nav_conversion.ItemUnitOfMeasure))

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

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from i_nav_conversion"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function Actualizar(ByRef oBeI_nav_conversion As clsBeI_nav_conversion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_conversion")
            Upd.Add("idconversion", "@idconversion", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("code", "@code", DataType.Parametro)
            Upd.Add("qty_per_unit_of_measure", "@qty_per_unit_of_measure", DataType.Parametro)
            Upd.Add("height", "@height", DataType.Parametro)
            Upd.Add("width", "@width", DataType.Parametro)
            Upd.Add("length", "@length", DataType.Parametro)
            Upd.Add("cubage", "@cubage", DataType.Parametro)
            Upd.Add("weight", "@weight", DataType.Parametro)
            Upd.Add("package_type", "@package_type", DataType.Parametro)
            Upd.Add("itemunitofmeasure", "@itemunitofmeasure", DataType.Parametro)
            Upd.Where("IdConversion = @IdConversion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeI_nav_conversion.IdConversion))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_conversion.Item_No))
            cmd.Parameters.Add(New SqlParameter("@CODE", oBeI_nav_conversion.Code))
            cmd.Parameters.Add(New SqlParameter("@QTY_PER_UNIT_OF_MEASURE", oBeI_nav_conversion.Qty_per_Unit_of_Measure))
            cmd.Parameters.Add(New SqlParameter("@HEIGHT", oBeI_nav_conversion.Height))
            cmd.Parameters.Add(New SqlParameter("@WIDTH", oBeI_nav_conversion.Width))
            cmd.Parameters.Add(New SqlParameter("@LENGTH", oBeI_nav_conversion.Length))
            cmd.Parameters.Add(New SqlParameter("@CUBAGE", oBeI_nav_conversion.Cubage))
            cmd.Parameters.Add(New SqlParameter("@WEIGHT", oBeI_nav_conversion.Weight))
            cmd.Parameters.Add(New SqlParameter("@PACKAGE_TYPE", oBeI_nav_conversion.Package_Type))
            cmd.Parameters.Add(New SqlParameter("@ITEMUNITOFMEASURE", oBeI_nav_conversion.ItemUnitOfMeasure))

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


    Public Shared Function Eliminar(ByRef oBeI_nav_conversion As clsBeI_nav_conversion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_conversion" &
             "  Where(IdConversion = @IdConversion)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeI_nav_conversion.IdConversion))

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

            Const sp As String = "SELECT * FROM I_nav_conversion"
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

    Public Shared Function Get_All() As List(Of clsBeI_nav_conversion)

        Dim lReturnList As New List(Of clsBeI_nav_conversion)

        Try

            Const sp As String = "SELECT * FROM I_nav_conversion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_conversion As New clsBeI_nav_conversion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_conversion = New clsBeI_nav_conversion()
                            Cargar(vBeI_nav_conversion, dr)
                            lReturnList.Add(vBeI_nav_conversion)
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

    Public Shared Sub GetSingle(ByRef pBeI_nav_conversion As clsBeI_nav_conversion)

        Try

            Const sp As String = "SELECT * FROM I_nav_conversion" &
            " Where(IdConversion = @IdConversion)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_conversion As New clsBeI_nav_conversion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_conversion, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdConversion),0) FROM I_nav_conversion"

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

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdConversion),0) FROM I_nav_conversion"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Codigo_Producto(ByVal pCodigoProducto As String,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_conversion)

        Dim lReturnList As New List(Of clsBeI_nav_conversion)

        Try

            Const sp As String = "SELECT * FROM I_nav_conversion WHERE Item_No = @Item_No "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Item_No", pCodigoProducto)
                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Dim vBeI_nav_conversion As New clsBeI_nav_conversion

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_conversion = New clsBeI_nav_conversion()
                    Cargar(vBeI_nav_conversion, dr)
                    lReturnList.Add(vBeI_nav_conversion)
                    Application.DoEvents()
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Exist(ByVal pItemNo As String, ByVal pCode As String)

        Try

            Const sp As String = "SELECT * FROM I_nav_conversion" &
            " Where(Item_No = @Item_No AND Code = @Code)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Item_No", pItemNo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Code", pCode)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_conversion As New clsBeI_nav_conversion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_conversion, lDataTable.Rows(0))
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

    Public Shared Function Exist(ByVal pItemNo As String, ByVal pCode As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Exist = False

        Try

            Const sp As String = "SELECT * FROM I_nav_conversion" &
            " Where(Item_No = @Item_No AND Code = @Code)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Item_No", pItemNo)
                lDTA.SelectCommand.Parameters.AddWithValue("@Code", pCode)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_conversion As New clsBeI_nav_conversion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    'Cargar(vBeI_nav_conversion, lDataTable.Rows(0))
                    Exist = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
