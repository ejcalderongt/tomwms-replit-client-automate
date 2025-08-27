Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnDS_PEDIDOD

    Public Shared Sub Cargar(ByRef oBeDS_PEDIDOD As clsBeDS_PEDIDOD, ByRef dr As DataRow)
        Try
            With oBeDS_PEDIDOD
                .COREL = IIf(IsDBNull(dr.Item("COREL")), "", dr.Item("COREL"))
                .PRODUCTO = IIf(IsDBNull(dr.Item("PRODUCTO")), "", dr.Item("PRODUCTO"))
                .EMPRESA = IIf(IsDBNull(dr.Item("EMPRESA")), "", dr.Item("EMPRESA"))
                .ANULADO = IIf(IsDBNull(dr.Item("ANULADO")), "", dr.Item("ANULADO"))
                .CANT = IIf(IsDBNull(dr.Item("CANT")), 0.0, dr.Item("CANT"))
                .PRECIO = IIf(IsDBNull(dr.Item("PRECIO")), 0.0, dr.Item("PRECIO"))
                .IMP = IIf(IsDBNull(dr.Item("IMP")), 0.0, dr.Item("IMP"))
                .DES = IIf(IsDBNull(dr.Item("DES")), 0.0, dr.Item("DES"))
                .DESMON = IIf(IsDBNull(dr.Item("DESMON")), 0.0, dr.Item("DESMON"))
                .TOTAL = IIf(IsDBNull(dr.Item("TOTAL")), 0.0, dr.Item("TOTAL"))
                .PRECIODOC = IIf(IsDBNull(dr.Item("PRECIODOC")), 0.0, dr.Item("PRECIODOC"))
                .PESO = IIf(IsDBNull(dr.Item("PESO")), 0.0, dr.Item("PESO"))
                .VAL1 = IIf(IsDBNull(dr.Item("VAL1")), 0.0, dr.Item("VAL1"))
                .VAL2 = IIf(IsDBNull(dr.Item("VAL2")), "", dr.Item("VAL2"))
                .Ruta = IIf(IsDBNull(dr.Item("ruta")), "", dr.Item("ruta"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeDS_PEDIDOD As clsBeDS_PEDIDOD, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("ds_pedidod")
            Ins.Add("corel", "@corel", DataType.Parametro)
            Ins.Add("producto", "@producto", DataType.Parametro)
            Ins.Add("empresa", "@empresa", DataType.Parametro)
            Ins.Add("anulado", "@anulado", DataType.Parametro)
            Ins.Add("cant", "@cant", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("imp", "@imp", DataType.Parametro)
            Ins.Add("des", "@des", DataType.Parametro)
            Ins.Add("desmon", "@desmon", DataType.Parametro)
            Ins.Add("total", "@total", DataType.Parametro)
            Ins.Add("preciodoc", "@preciodoc", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("val1", "@val1", DataType.Parametro)
            Ins.Add("val2", "@val2", DataType.Parametro)
            Ins.Add("ruta", "@ruta", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDOD.COREL))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", oBeDS_PEDIDOD.PRODUCTO))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeDS_PEDIDOD.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeDS_PEDIDOD.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeDS_PEDIDOD.CANT))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeDS_PEDIDOD.PRECIO))
            cmd.Parameters.Add(New SqlParameter("@IMP", oBeDS_PEDIDOD.IMP))
            cmd.Parameters.Add(New SqlParameter("@DES", oBeDS_PEDIDOD.DES))
            cmd.Parameters.Add(New SqlParameter("@DESMON", oBeDS_PEDIDOD.DESMON))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeDS_PEDIDOD.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@PRECIODOC", oBeDS_PEDIDOD.PRECIODOC))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeDS_PEDIDOD.PESO))
            cmd.Parameters.Add(New SqlParameter("@VAL1", oBeDS_PEDIDOD.VAL1))
            cmd.Parameters.Add(New SqlParameter("@VAL2", oBeDS_PEDIDOD.VAL2))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeDS_PEDIDOD.Ruta))

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

    Public Shared Function Actualizar(ByRef oBeDS_PEDIDOD As clsBeDS_PEDIDOD, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("ds_pedidod")
            Upd.Add("corel", "@corel", DataType.Parametro)
            Upd.Add("producto", "@producto", DataType.Parametro)
            Upd.Add("empresa", "@empresa", DataType.Parametro)
            Upd.Add("anulado", "@anulado", DataType.Parametro)
            Upd.Add("cant", "@cant", DataType.Parametro)
            Upd.Add("precio", "@precio", DataType.Parametro)
            Upd.Add("imp", "@imp", DataType.Parametro)
            Upd.Add("des", "@des", DataType.Parametro)
            Upd.Add("desmon", "@desmon", DataType.Parametro)
            Upd.Add("total", "@total", DataType.Parametro)
            Upd.Add("preciodoc", "@preciodoc", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("val1", "@val1", DataType.Parametro)
            Upd.Add("val2", "@val2", DataType.Parametro)
            Upd.Add("ruta", "@ruta", DataType.Parametro)
            Upd.Where("COREL = @COREL" &
                " AND PRODUCTO = @PRODUCTO")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDOD.COREL))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", oBeDS_PEDIDOD.PRODUCTO))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeDS_PEDIDOD.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeDS_PEDIDOD.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeDS_PEDIDOD.CANT))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeDS_PEDIDOD.PRECIO))
            cmd.Parameters.Add(New SqlParameter("@IMP", oBeDS_PEDIDOD.IMP))
            cmd.Parameters.Add(New SqlParameter("@DES", oBeDS_PEDIDOD.DES))
            cmd.Parameters.Add(New SqlParameter("@DESMON", oBeDS_PEDIDOD.DESMON))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeDS_PEDIDOD.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@PRECIODOC", oBeDS_PEDIDOD.PRECIODOC))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeDS_PEDIDOD.PESO))
            cmd.Parameters.Add(New SqlParameter("@VAL1", oBeDS_PEDIDOD.VAL1))
            cmd.Parameters.Add(New SqlParameter("@VAL2", oBeDS_PEDIDOD.VAL2))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeDS_PEDIDOD.Ruta))

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


    Public Shared Function Eliminar(ByRef oBeDS_PEDIDOD As clsBeDS_PEDIDOD, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from DS_PEDIDOD" &
             "  Where(COREL = @COREL)" &
             "  AND (PRODUCTO = @PRODUCTO)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDOD.COREL))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", oBeDS_PEDIDOD.PRODUCTO))

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

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM DS_PEDIDOD"
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

    Public Shared Function GetAll() As List(Of clsBeDS_PEDIDOD)

        Dim lReturnList As New List(Of clsBeDS_PEDIDOD)

        Try

            Const sp As String = "SELECT * FROM DS_PEDIDOD"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDS_PEDIDOD As New clsBeDS_PEDIDOD

                        For Each dr As DataRow In lDataTable.Rows
                            vBeDS_PEDIDOD = New clsBeDS_PEDIDOD()
                            Cargar(vBeDS_PEDIDOD, dr)
                            lReturnList.Add(vBeDS_PEDIDOD)
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

    Public Shared Sub GetSingle(ByRef pBeDS_PEDIDOD As clsBeDS_PEDIDOD)

        Try

            Const sp As String = "SELECT * FROM DS_PEDIDOD" &
            " Where(COREL = @COREL)" &
            " AND (PRODUCTO = @PRODUCTO)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDS_PEDIDOD As New clsBeDS_PEDIDOD

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeDS_PEDIDOD, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(COREL),0) FROM DS_PEDIDOD"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
