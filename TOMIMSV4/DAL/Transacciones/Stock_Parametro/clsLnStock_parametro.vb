Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_parametro

    Public Shared Sub Cargar(ByRef oBeStock_parametro As clsBeStock_parametro, ByRef dr As DataRow)
        Try
            With oBeStock_parametro
                .IdStockParametro = IIf(IsDBNull(dr.Item("IdStockParametro")), 0, dr.Item("IdStockParametro"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), Date.Now, dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeStock_parametro As clsBeStock_parametro, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("stock_parametro")
            Ins.Add("idstockparametro", "@idstockparametro", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Ins.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Ins.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Ins.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Ins.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_parametro.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeStock_parametro.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", oBeStock_parametro.Valor_texto))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", oBeStock_parametro.Valor_numerico))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", oBeStock_parametro.Valor_fecha))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", oBeStock_parametro.Valor_logico))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_parametro.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_parametro.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_parametro.Activo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeStock_parametro As clsBeStock_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_parametro")
            Upd.Add("idstockparametro", "@idstockparametro", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Upd.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Upd.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Upd.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Upd.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdStockParametro = @IdStockParametro")

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_parametro.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeStock_parametro.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", oBeStock_parametro.Valor_texto))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", oBeStock_parametro.Valor_numerico))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", oBeStock_parametro.Valor_fecha))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", oBeStock_parametro.Valor_logico))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_parametro.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_parametro.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_parametro.Activo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeStock_parametro As clsBeStock_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_parametro" &
             "  Where(IdStockParametro = @IdStockParametro)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKPARAMETRO", oBeStock_parametro.IdStockParametro))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_parametro"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockParametro),0) FROM Stock_parametro"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
