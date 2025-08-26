Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnD_PEDIDO

    Public Shared Sub Cargar(ByRef oBeD_PEDIDO As clsBeD_PEDIDO, ByRef dr As DataRow)
        Try
            With oBeD_PEDIDO
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
                .STATPROC = IIf(IsDBNull(dr.Item("STATPROC")), "", dr.Item("STATPROC"))
                .RECHAZADO = IIf(IsDBNull(dr.Item("RECHAZADO")), False, dr.Item("RECHAZADO"))
                .RAZON_RECHAZADO = IIf(IsDBNull(dr.Item("RAZON_RECHAZADO")), "", dr.Item("RAZON_RECHAZADO"))
                .INFORMADO = IIf(IsDBNull(dr.Item("INFORMADO")), False, dr.Item("INFORMADO"))
                .SUCURSAL = IIf(IsDBNull(dr.Item("SUCURSAL")), "", dr.Item("SUCURSAL"))
                .ID_DESPACHO = IIf(IsDBNull(dr.Item("ID_DESPACHO")), 0, dr.Item("ID_DESPACHO"))
                .ID_FACTURACION = IIf(IsDBNull(dr.Item("ID_FACTURACION")), 0, dr.Item("ID_FACTURACION"))
                .RUTASUPER = IIf(IsDBNull(dr.Item("RUTASUPER")), "", dr.Item("RUTASUPER"))
                .NO_PEDIDO_ERP = IIf(IsDBNull(dr.Item("NO_PEDIDO_ERP")), "", dr.Item("NO_PEDIDO_ERP"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeD_PEDIDO As clsBeD_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("d_pedido")
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
            Ins.Add("statproc", "@statproc", DataType.Parametro)
            Ins.Add("rechazado", "@rechazado", DataType.Parametro)
            Ins.Add("razon_rechazado", "@razon_rechazado", DataType.Parametro)
            Ins.Add("informado", "@informado", DataType.Parametro)
            Ins.Add("sucursal", "@sucursal", DataType.Parametro)
            Ins.Add("id_despacho", "@id_despacho", DataType.Parametro)
            Ins.Add("id_facturacion", "@id_facturacion", DataType.Parametro)
            Ins.Add("rutasuper", "@rutasuper", DataType.Parametro)
            Ins.Add("no_pedido_erp", "@no_pedido_erp", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeD_PEDIDO.COREL))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeD_PEDIDO.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeD_PEDIDO.FECHA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeD_PEDIDO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeD_PEDIDO.RUTA))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeD_PEDIDO.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE", oBeD_PEDIDO.CLIENTE))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeD_PEDIDO.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTR", oBeD_PEDIDO.FECHAENTR))
            cmd.Parameters.Add(New SqlParameter("@DIRENTREGA", oBeD_PEDIDO.DIRENTREGA))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeD_PEDIDO.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@DESMONTO", oBeD_PEDIDO.DESMONTO))
            cmd.Parameters.Add(New SqlParameter("@IMPMONTO", oBeD_PEDIDO.IMPMONTO))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeD_PEDIDO.PESO))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeD_PEDIDO.BANDERA))
            cmd.Parameters.Add(New SqlParameter("@STATCOM", oBeD_PEDIDO.STATCOM))
            cmd.Parameters.Add(New SqlParameter("@CALCOBJ", oBeD_PEDIDO.CALCOBJ))
            cmd.Parameters.Add(New SqlParameter("@IMPRES", oBeD_PEDIDO.IMPRES))
            cmd.Parameters.Add(New SqlParameter("@ADD1", oBeD_PEDIDO.ADD1))
            cmd.Parameters.Add(New SqlParameter("@ADD2", oBeD_PEDIDO.ADD2))
            cmd.Parameters.Add(New SqlParameter("@ADD3", oBeD_PEDIDO.ADD3))
            cmd.Parameters.Add(New SqlParameter("@STATPROC", oBeD_PEDIDO.STATPROC))
            cmd.Parameters.Add(New SqlParameter("@RECHAZADO", oBeD_PEDIDO.RECHAZADO))
            cmd.Parameters.Add(New SqlParameter("@RAZON_RECHAZADO", oBeD_PEDIDO.RAZON_RECHAZADO))
            cmd.Parameters.Add(New SqlParameter("@INFORMADO", oBeD_PEDIDO.INFORMADO))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeD_PEDIDO.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@ID_DESPACHO", oBeD_PEDIDO.ID_DESPACHO))
            cmd.Parameters.Add(New SqlParameter("@ID_FACTURACION", oBeD_PEDIDO.ID_FACTURACION))
            cmd.Parameters.Add(New SqlParameter("@RUTASUPER", oBeD_PEDIDO.RUTASUPER))
            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO_ERP", oBeD_PEDIDO.NO_PEDIDO_ERP))

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

    Public Shared Function Actualizar(ByRef oBeD_PEDIDO As clsBeD_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("d_pedido")
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
            Upd.Add("statproc", "@statproc", DataType.Parametro)
            Upd.Add("rechazado", "@rechazado", DataType.Parametro)
            Upd.Add("razon_rechazado", "@razon_rechazado", DataType.Parametro)
            Upd.Add("informado", "@informado", DataType.Parametro)
            Upd.Add("sucursal", "@sucursal", DataType.Parametro)
            Upd.Add("id_despacho", "@id_despacho", DataType.Parametro)
            Upd.Add("id_facturacion", "@id_facturacion", DataType.Parametro)
            Upd.Add("rutasuper", "@rutasuper", DataType.Parametro)
            Upd.Add("no_pedido_erp", "@no_pedido_erp", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeD_PEDIDO.COREL))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeD_PEDIDO.ANULADO))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeD_PEDIDO.FECHA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeD_PEDIDO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeD_PEDIDO.RUTA))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeD_PEDIDO.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE", oBeD_PEDIDO.CLIENTE))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeD_PEDIDO.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@FECHAENTR", oBeD_PEDIDO.FECHAENTR))
            cmd.Parameters.Add(New SqlParameter("@DIRENTREGA", oBeD_PEDIDO.DIRENTREGA))
            cmd.Parameters.Add(New SqlParameter("@TOTAL", oBeD_PEDIDO.TOTAL))
            cmd.Parameters.Add(New SqlParameter("@DESMONTO", oBeD_PEDIDO.DESMONTO))
            cmd.Parameters.Add(New SqlParameter("@IMPMONTO", oBeD_PEDIDO.IMPMONTO))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeD_PEDIDO.PESO))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeD_PEDIDO.BANDERA))
            cmd.Parameters.Add(New SqlParameter("@STATCOM", oBeD_PEDIDO.STATCOM))
            cmd.Parameters.Add(New SqlParameter("@CALCOBJ", oBeD_PEDIDO.CALCOBJ))
            cmd.Parameters.Add(New SqlParameter("@IMPRES", oBeD_PEDIDO.IMPRES))
            cmd.Parameters.Add(New SqlParameter("@ADD1", oBeD_PEDIDO.ADD1))
            cmd.Parameters.Add(New SqlParameter("@ADD2", oBeD_PEDIDO.ADD2))
            cmd.Parameters.Add(New SqlParameter("@ADD3", oBeD_PEDIDO.ADD3))
            cmd.Parameters.Add(New SqlParameter("@STATPROC", oBeD_PEDIDO.STATPROC))
            cmd.Parameters.Add(New SqlParameter("@RECHAZADO", oBeD_PEDIDO.RECHAZADO))
            cmd.Parameters.Add(New SqlParameter("@RAZON_RECHAZADO", oBeD_PEDIDO.RAZON_RECHAZADO))
            cmd.Parameters.Add(New SqlParameter("@INFORMADO", oBeD_PEDIDO.INFORMADO))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeD_PEDIDO.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@ID_DESPACHO", oBeD_PEDIDO.ID_DESPACHO))
            cmd.Parameters.Add(New SqlParameter("@ID_FACTURACION", oBeD_PEDIDO.ID_FACTURACION))
            cmd.Parameters.Add(New SqlParameter("@RUTASUPER", oBeD_PEDIDO.RUTASUPER))
            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO_ERP", oBeD_PEDIDO.NO_PEDIDO_ERP))

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

    Public Shared Function Actualizar_Estado(ByRef oBeD_PEDIDO As clsBeD_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("d_pedido")
            Upd.Add("statcom", "@statcom", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeD_PEDIDO.COREL))
            cmd.Parameters.Add(New SqlParameter("@STATCOM", oBeD_PEDIDO.STATCOM))

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

    Public Shared Function Eliminar(ByRef oBeD_PEDIDO As clsBeD_PEDIDO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from D_PEDIDO" &
             "  Where(COREL = @COREL)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COREL", oBeD_PEDIDO.COREL))

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

            Const sp As String = "SELECT * FROM D_PEDIDO"
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

    Public Shared Function GetAll() As List(Of clsBeD_PEDIDO)

        Dim lReturnList As New List(Of clsBeD_PEDIDO)

        Try

            Const sp As String = "SELECT * FROM D_PEDIDO"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeD_PEDIDO As New clsBeD_PEDIDO

                        For Each dr As DataRow In lDataTable.Rows
                            vBeD_PEDIDO = New clsBeD_PEDIDO()
                            Cargar(vBeD_PEDIDO, dr)
                            lReturnList.Add(vBeD_PEDIDO)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeD_PEDIDO As clsBeD_PEDIDO)

        Try

            Const sp As String = "SELECT * FROM D_PEDIDO" &
            " Where(COREL = @COREL)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@COREL", pBeD_PEDIDO.COREL)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeD_PEDIDO As New clsBeD_PEDIDO

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeD_PEDIDO, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(COREL),0) FROM D_PEDIDO"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
