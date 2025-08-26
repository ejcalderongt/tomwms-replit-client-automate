Imports System.Data.SqlClient

Public Class clsLnStock_se

    Public Shared Sub Cargar(ByRef oBeStock_se As clsBeStock_se, ByRef dr As DataRow)
        Try
            With oBeStock_se
                .IdStockSe = IIf(IsDBNull(dr.Item("IdStockSe")), 0, dr.Item("IdStockSe"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .NoSerie = IIf(IsDBNull(dr.Item("NoSerie")), "", dr.Item("NoSerie"))
                .NoSerieInicial = IIf(IsDBNull(dr.Item("NoSerieInicial")), "", dr.Item("NoSerieInicial"))
                .NoSerieFinal = IIf(IsDBNull(dr.Item("NoSerieFinal")), "", dr.Item("NoSerieFinal"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeStock_se As clsBeStock_se, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("stock_se")
            Ins.Add("idstockse", "@idstockse", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("noserie", "@noserie", DataType.Parametro)
            Ins.Add("noserieinicial", "@noserieinicial", DataType.Parametro)
            Ins.Add("noseriefinal", "@noseriefinal", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_se.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", IIf(oBeStock_se.NoSerie = String.Empty, DBNull.Value, oBeStock_se.NoSerie)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEINICIAL", IIf(oBeStock_se.NoSerieInicial = String.Empty, DBNull.Value, oBeStock_se.NoSerieInicial)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEFINAL", IIf(oBeStock_se.NoSerieFinal = String.Empty, DBNull.Value, oBeStock_se.NoSerieFinal)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_se.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_se.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_se.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_se.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_se.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeStock_se As clsBeStock_se, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("stock_se")
            Upd.Add("idstockse", "@idstockse", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("noserie", "@noserie", DataType.Parametro)
            Upd.Add("noserieinicial", "@noserieinicial", DataType.Parametro)
            Upd.Add("noseriefinal", "@noseriefinal", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdStockSe = @IdStockSe")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_se.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", IIf(oBeStock_se.NoSerie = String.Empty, DBNull.Value, oBeStock_se.NoSerie)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEINICIAL", IIf(oBeStock_se.NoSerieInicial = String.Empty, DBNull.Value, oBeStock_se.NoSerieInicial)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEFINAL", IIf(oBeStock_se.NoSerieFinal = String.Empty, DBNull.Value, oBeStock_se.NoSerieFinal)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_se.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_se.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_se.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_se.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_se.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeStock_se As clsBeStock_se, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Stock_se" &
             "  Where(IdStockSe = @IdStockSe)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_By_IdStock(ByVal pIdStock As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Stock_se Where(IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdStock", pIdStock))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeStock_se As clsBeStock_se) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Stock_se" &
            " Where(IdStockSe = @IdStockSe)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKSE", oBeStock_se.IdStockSe))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeStock_se, dt.Rows(0))
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

End Class
