Imports System.Data.SqlClient

Public Class clsLnStock_se_rec

    Public Shared Sub Cargar(ByRef oBeStock_se_rec As clsBeStock_se_rec, ByRef dr As DataRow)
        Try
            With oBeStock_se_rec
                .IdStockSeRec = IIf(IsDBNull(dr.Item("IdStockSeRec")), 0, dr.Item("IdStockSeRec"))
                .IdStockRec = IIf(IsDBNull(dr.Item("IdStockRec")), 0, dr.Item("IdStockRec"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .NoSerie = IIf(IsDBNull(dr.Item("NoSerie")), "", dr.Item("NoSerie"))
                .NoSerieInicial = IIf(IsDBNull(dr.Item("NoSerieInicial")), "", dr.Item("NoSerieInicial"))
                .NoSerieFinal = IIf(IsDBNull(dr.Item("NoSerieFinal")), "", dr.Item("NoSerieFinal"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Regularizado = IIf(IsDBNull(dr.Item("regularizado")), False, dr.Item("regularizado"))
                .Fecha_regularizacion = IIf(IsDBNull(dr.Item("fecha_regularizacion")), Date.Now, dr.Item("fecha_regularizacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public shared Function Insertar(ByRef oBeStock_se_rec As clsBeStock_se_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("stock_se_rec")
            Ins.Add("idstockserec", "@idstockserec", DataType.Parametro)
            Ins.Add("idstockrec", "@idstockrec", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("noserie", "@noserie", DataType.Parametro)
            Ins.Add("noserieinicial", "@noserieinicial", DataType.Parametro)
            Ins.Add("noseriefinal", "@noseriefinal", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("regularizado", "@regularizado", DataType.Parametro)
            Ins.Add("fecha_regularizacion", "@fecha_regularizacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSEREC", oBeStock_se_rec.IdStockSeRec))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_se_rec.IdStockRec))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se_rec.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", IIf(oBeStock_se_rec.NoSerie = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerie)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEINICIAL", IIf(oBeStock_se_rec.NoSerieInicial = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerieInicial)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEFINAL", IIf(oBeStock_se_rec.NoSerieFinal = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerieFinal)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_se_rec.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_se_rec.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_se_rec.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_se_rec.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_se_rec.Activo))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", IIf(oBeStock_se_rec.Regularizado = Nothing, DBNull.Value, oBeStock_se_rec.Regularizado)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REGULARIZACION", IIf(oBeStock_se_rec.Fecha_regularizacion = Nothing, DBNull.Value, oBeStock_se_rec.Fecha_regularizacion)))

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

    Public Shared Function Actualizar(ByRef oBeStock_se_rec As clsBeStock_se_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("stock_se_rec")
            Upd.Add("idstockserec", "@idstockserec", DataType.Parametro)
            Upd.Add("idstockrec", "@idstockrec", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("noserie", "@noserie", DataType.Parametro)
            Upd.Add("noserieinicial", "@noserieinicial", DataType.Parametro)
            Upd.Add("noseriefinal", "@noseriefinal", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("regularizado", "@regularizado", DataType.Parametro)
            Upd.Add("fecha_regularizacion", "@fecha_regularizacion", DataType.Parametro)
            Upd.Where("IdStockSeRec = @IdStockSeRec")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSEREC", oBeStock_se_rec.IdStockSeRec))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_se_rec.IdStockRec))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_se_rec.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", IIf(oBeStock_se_rec.NoSerie = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerie)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEINICIAL", IIf(oBeStock_se_rec.NoSerieInicial = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerieInicial)))
            cmd.Parameters.Add(New SqlParameter("@NOSERIEFINAL", IIf(oBeStock_se_rec.NoSerieFinal = String.Empty, DBNull.Value, oBeStock_se_rec.NoSerieFinal)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_se_rec.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_se_rec.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_se_rec.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_se_rec.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_se_rec.Activo))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", IIf(oBeStock_se_rec.Regularizado = Nothing, DBNull.Value, oBeStock_se_rec.Regularizado)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REGULARIZACION", IIf(oBeStock_se_rec.Fecha_regularizacion = Nothing, DBNull.Value, oBeStock_se_rec.Fecha_regularizacion)))

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

    Public Shared Function Eliminar(ByRef oBeStock_se_rec As clsBeStock_se_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Stock_se_rec" &
             "  Where(IdStockSeRec = @IdStockSeRec)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKSEREC", oBeStock_se_rec.IdStockSeRec))

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

    Public Shared Function Obtener(ByRef oBeStock_se_rec As clsBeStock_se_rec) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Stock_se_rec" &
            " Where(IdStockSeRec = @IdStockSeRec)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKSEREC", oBeStock_se_rec.IdStockSeRec))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeStock_se_rec, dt.Rows(0))
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
