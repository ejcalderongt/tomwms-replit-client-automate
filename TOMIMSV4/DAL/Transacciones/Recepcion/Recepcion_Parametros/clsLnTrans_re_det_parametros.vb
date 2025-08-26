Imports System.Data.SqlClient

Public Class clsLnTrans_re_det_parametros

    Public Overridable Sub Cargar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros, ByRef dr As DataRow)

        Try

            With oBeTrans_re_det_parametros
                .IdParametroDet = IIf(IsDBNull(dr.Item("IdParametroDet")), 0, dr.Item("IdParametroDet"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), New Date(1900, 1, 1), dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), New Date(1900, 1, 1), dr.Item("fec_agr"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_det_parametros")
            Ins.Add("idparametrodet", "@idparametrodet", DataType.Parametro)
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Ins.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Ins.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Ins.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Ins.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRODET", oBeTrans_re_det_parametros.IdParametroDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det_parametros.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det_parametros.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_re_det_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeTrans_re_det_parametros.Valor_texto.Trim = "", DBNull.Value, oBeTrans_re_det_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeTrans_re_det_parametros.Valor_numerico = 0, DBNull.Value, oBeTrans_re_det_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeTrans_re_det_parametros.Valor_fecha = New Date(1900, 1, 1), DBNull.Value, oBeTrans_re_det_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeTrans_re_det_parametros.Valor_logico, True, oBeTrans_re_det_parametros.Valor_logico)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det_parametros.Fec_agr))

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

    Public Shared Function Actualizar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_det_parametros")
            Upd.Add("idparametrodet", "@idparametrodet", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Upd.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Upd.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Upd.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Upd.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Where("IdParametroDet = @IdParametroDet " &
                "AND IdRecepcionDet = @IdRecepcionDet " &
                "AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRODET", oBeTrans_re_det_parametros.IdParametroDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det_parametros.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det_parametros.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_re_det_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeTrans_re_det_parametros.Valor_texto.Trim = "", DBNull.Value, oBeTrans_re_det_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeTrans_re_det_parametros.Valor_numerico = 0, DBNull.Value, oBeTrans_re_det_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeTrans_re_det_parametros.Valor_fecha = New Date(1900, 1, 1), DBNull.Value, oBeTrans_re_det_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeTrans_re_det_parametros.Valor_logico, True, oBeTrans_re_det_parametros.Valor_logico)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det_parametros.Fec_agr))

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


    Public Shared Function Eliminar(ByRef oBeTrans_re_det_parametros As clsBeTrans_re_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_re_det_parametros" &
             "  Where(IdParametroDet = @IdParametroDet) " &
             "  AND (IdRecepcionDet = @IdRecepcionDet) " &
             "  AND (IdRecepcionEnc = @IdRecepcionEnc) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRODET", oBeTrans_re_det_parametros.IdParametroDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det_parametros.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det_parametros.IdRecepcionEnc))

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

End Class