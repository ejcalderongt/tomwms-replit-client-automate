Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnDS_PEDIDO

    Public Shared Sub Cargar(ByRef oBeDS_PEDIDO As clsBeDS_PEDIDO, ByRef dr As DataRow)
        Try
            With oBeDS_PEDIDO
                .COREL = IIf(IsDBNull(dr.Item("COREL")), "", dr.Item("COREL"))
                .ANULADO = IIf(IsDBNull(dr.Item("ANULADO")), "", dr.Item("ANULADO"))
                .FECHA = IIf(IsDBNull(dr.Item("FECHA")), Date.Now, dr.Item("FECHA"))
                .EMPRESA = IIf(IsDBNull(dr.Item("EMPRESA")), "", dr.Item("EMPRESA"))
                .RUTA = IIf(IsDBNull(dr.Item("RUTA")), "", dr.Item("RUTA"))
                .VENDEDOR = IIf(IsDBNull(dr.Item("VENDEDOR")), "", dr.Item("VENDEDOR"))
                .CLIENTE = IIf(IsDBNull(dr.Item("CLIENTE")), "", dr.Item("CLIENTE"))
                .KILOMETRAJE = IIf(IsDBNull(dr.Item("KILOMETRAJE")), 0.0, dr.Item("KILOMETRAJE"))
                .FECHAENTR = IIf(IsDBNull(dr.Item("FECHAENTR")), Date.Now, dr.Item("FECHAENTR"))
                .DIRENTREGA = IIf(IsDBNull(dr.Item("DIRENTREGA")), "", dr.Item("DIRENTREGA"))
                .TOTAL = IIf(IsDBNull(dr.Item("TOTAL")), 0.0, dr.Item("TOTAL"))
                .DESMONTO = IIf(IsDBNull(dr.Item("DESMONTO")), 0.0, dr.Item("DESMONTO"))
                .IMPMONTO = IIf(IsDBNull(dr.Item("IMPMONTO")), 0.0, dr.Item("IMPMONTO"))
                .PESO = IIf(IsDBNull(dr.Item("PESO")), 0.0, dr.Item("PESO"))
                .BANDERA = IIf(IsDBNull(dr.Item("BANDERA")), "", dr.Item("BANDERA"))
                .STATCOM = IIf(IsDBNull(dr.Item("STATCOM")), "", dr.Item("STATCOM"))
                .CALCOBJ = IIf(IsDBNull(dr.Item("CALCOBJ")), "", dr.Item("CALCOBJ"))
                .IMPRES = IIf(IsDBNull(dr.Item("IMPRES")), 0, dr.Item("IMPRES"))
                .ADD1 = IIf(IsDBNull(dr.Item("ADD1")), "", dr.Item("ADD1"))
                .ADD2 = IIf(IsDBNull(dr.Item("ADD2")), "", dr.Item("ADD2"))
                .ADD3 = IIf(IsDBNull(dr.Item("ADD3")), "", dr.Item("ADD3"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeDS_PEDIDO As clsBeDS_PEDIDO, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("ds_pedido")
            Ins.Add("corel", "@corel", DataType.Parametro)
            Ins.Add("anulado", "@anulado", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("empresa", "@empresa", DataType.Parametro)
            Ins.Add("ruta", "@ruta", DataType.Parametro)
            Ins.Add("vendedor", "@vendedor", DataType.Parametro)
            Ins.Add("cliente", "@cliente", DataType.Parametro)
            Ins.Add("kilometraje", "@kilometraje", DataType.Parametro)
            Ins.Add("fechaentr", "@fechaentr", DataType.Parametro)
            Ins.Add("direntrega", "@direntrega", DataType.Parametro)
            Ins.Add("total", "@total", DataType.Parametro)
            Ins.Add("desmonto", "@desmonto", DataType.Parametro)
            Ins.Add("impmonto", "@impmonto", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("bandera", "@bandera", DataType.Parametro)
            Ins.Add("statcom", "@statcom", DataType.Parametro)
            Ins.Add("calcobj", "@calcobj", DataType.Parametro)
            Ins.Add("impres", "@impres", DataType.Parametro)
            Ins.Add("add1", "@add1", DataType.Parametro)
            Ins.Add("add2", "@add2", DataType.Parametro)
            Ins.Add("add3", "@add3", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDO.COREL))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeDS_PEDIDO.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeDS_PEDIDO.FECHA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeDS_PEDIDO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeDS_PEDIDO.RUTA))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeDS_PEDIDO.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE", oBeDS_PEDIDO.CLIENTE))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeDS_PEDIDO.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTR", oBeDS_PEDIDO.FECHAENTR))
            cmd.Parameters.Add(New SqlParameter("@DIRENTREGA", oBeDS_PEDIDO.DIRENTREGA))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeDS_PEDIDO.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@DESMONTO", oBeDS_PEDIDO.DESMONTO))
            cmd.Parameters.Add(New SqlParameter("@IMPMONTO", oBeDS_PEDIDO.IMPMONTO))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeDS_PEDIDO.PESO))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeDS_PEDIDO.BANDERA))
            cmd.Parameters.Add(New SqlParameter("@STATCOM", oBeDS_PEDIDO.STATCOM))
            cmd.Parameters.Add(New SqlParameter("@CALCOBJ", oBeDS_PEDIDO.CALCOBJ))
            cmd.Parameters.Add(New SqlParameter("@IMPRES", oBeDS_PEDIDO.IMPRES))
            cmd.Parameters.Add(New SqlParameter("@ADD1", oBeDS_PEDIDO.ADD1))
            cmd.Parameters.Add(New SqlParameter("@ADD2", oBeDS_PEDIDO.ADD2))
            cmd.Parameters.Add(New SqlParameter("@ADD3", oBeDS_PEDIDO.ADD3))

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

    Public Shared Function Actualizar(ByRef oBeDS_PEDIDO As clsBeDS_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("ds_pedido")
            Upd.Add("corel", "@corel", DataType.Parametro)
            Upd.Add("anulado", "@anulado", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("empresa", "@empresa", DataType.Parametro)
            Upd.Add("ruta", "@ruta", DataType.Parametro)
            Upd.Add("vendedor", "@vendedor", DataType.Parametro)
            Upd.Add("cliente", "@cliente", DataType.Parametro)
            Upd.Add("kilometraje", "@kilometraje", DataType.Parametro)
            Upd.Add("fechaentr", "@fechaentr", DataType.Parametro)
            Upd.Add("direntrega", "@direntrega", DataType.Parametro)
            Upd.Add("total", "@total", DataType.Parametro)
            Upd.Add("desmonto", "@desmonto", DataType.Parametro)
            Upd.Add("impmonto", "@impmonto", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("bandera", "@bandera", DataType.Parametro)
            Upd.Add("statcom", "@statcom", DataType.Parametro)
            Upd.Add("calcobj", "@calcobj", DataType.Parametro)
            Upd.Add("impres", "@impres", DataType.Parametro)
            Upd.Add("add1", "@add1", DataType.Parametro)
            Upd.Add("add2", "@add2", DataType.Parametro)
            Upd.Add("add3", "@add3", DataType.Parametro)
            Upd.Where("COREL = @COREL")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDO.COREL))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeDS_PEDIDO.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeDS_PEDIDO.FECHA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeDS_PEDIDO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeDS_PEDIDO.RUTA))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeDS_PEDIDO.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE", oBeDS_PEDIDO.CLIENTE))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeDS_PEDIDO.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTR", oBeDS_PEDIDO.FECHAENTR))
            cmd.Parameters.Add(New SqlParameter("@DIRENTREGA", oBeDS_PEDIDO.DIRENTREGA))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeDS_PEDIDO.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@DESMONTO", oBeDS_PEDIDO.DESMONTO))
            cmd.Parameters.Add(New SqlParameter("@IMPMONTO", oBeDS_PEDIDO.IMPMONTO))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeDS_PEDIDO.PESO))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeDS_PEDIDO.BANDERA))
            cmd.Parameters.Add(New SqlParameter("@STATCOM", oBeDS_PEDIDO.STATCOM))
            cmd.Parameters.Add(New SqlParameter("@CALCOBJ", oBeDS_PEDIDO.CALCOBJ))
            cmd.Parameters.Add(New SqlParameter("@IMPRES", oBeDS_PEDIDO.IMPRES))
            cmd.Parameters.Add(New SqlParameter("@ADD1", oBeDS_PEDIDO.ADD1))
            cmd.Parameters.Add(New SqlParameter("@ADD2", oBeDS_PEDIDO.ADD2))
            cmd.Parameters.Add(New SqlParameter("@ADD3", oBeDS_PEDIDO.ADD3))

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


    Public Shared Function Eliminar(ByRef oBeDS_PEDIDO As clsBeDS_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from DS_PEDIDO" &
             "  Where(COREL = @COREL)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeDS_PEDIDO.COREL))

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

            Const sp As String = "SELECT * FROM DS_PEDIDO"
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

    Public Shared Function GetAll() As List(Of clsBeDS_PEDIDO)

        Dim lReturnList As New List(Of clsBeDS_PEDIDO)

        Try

            Const sp As String = "SELECT * FROM DS_PEDIDO"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDS_PEDIDO As New clsBeDS_PEDIDO

                        For Each dr As DataRow In lDataTable.Rows
                            vBeDS_PEDIDO = New clsBeDS_PEDIDO()
                            Cargar(vBeDS_PEDIDO, dr)
                            lReturnList.Add(vBeDS_PEDIDO)
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

    Public Shared Sub GetSingle(ByRef pBeDS_PEDIDO As clsBeDS_PEDIDO)

        Try

            Const sp As String = "SELECT * FROM DS_PEDIDO" &
            " Where(COREL = @COREL)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDS_PEDIDO As New clsBeDS_PEDIDO

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeDS_PEDIDO, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(COREL),0) FROM DS_PEDIDO"

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
