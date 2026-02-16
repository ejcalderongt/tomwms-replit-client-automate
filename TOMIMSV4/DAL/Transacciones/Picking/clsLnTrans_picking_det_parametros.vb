Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_picking_det_parametros

    Public Shared Sub Cargar(ByRef oBeTrans_picking_det_parametros As clsBeTrans_picking_det_parametros, ByRef dr As DataRow)
        Try
            With oBeTrans_picking_det_parametros
                .IdParametroPicking = IIf(IsDBNull(dr.Item("IdParametroPicking")), 0, dr.Item("IdParametroPicking"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), Date.Now, dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_det_parametros As clsBeTrans_picking_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_picking_det_parametros")
            Ins.Add("idparametropicking", "@idparametropicking", DataType.Parametro)
            Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
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


            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det_parametros.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_picking_det_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeTrans_picking_det_parametros.Valor_texto Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeTrans_picking_det_parametros.Valor_numerico Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeTrans_picking_det_parametros.Valor_fecha Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeTrans_picking_det_parametros.Valor_logico Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_logico)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_det_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_det_parametros.Fec_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

            oBeTrans_picking_det_parametros.IdParametroPicking = CInt(cmd.Parameters("@IDPARAMETROPICKING").Value)

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_picking_det_parametros As clsBeTrans_picking_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_picking_det_parametros")
            Upd.Add("idparametropicking", "@idparametropicking", DataType.Parametro)
            Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Upd.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Upd.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Upd.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Upd.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Upd.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Where("IdParametroPicking = @IdParametroPicking")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det_parametros.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeTrans_picking_det_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeTrans_picking_det_parametros.Valor_texto Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeTrans_picking_det_parametros.Valor_numerico Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeTrans_picking_det_parametros.Valor_fecha Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeTrans_picking_det_parametros.Valor_logico Is Nothing, DBNull.Value, oBeTrans_picking_det_parametros.Valor_logico)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_det_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_det_parametros.Fec_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_picking_det_parametros As clsBeTrans_picking_det_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_picking_det_parametros" &
             "  Where(IdParametroPicking = @IdParametroPicking)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_picking_det_parametros"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_picking_det_parametros As clsBeTrans_picking_det_parametros) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Trans_picking_det_parametros" &
            " Where(IdParametroPicking = @IdParametroPicking)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPARAMETROPICKING", oBeTrans_picking_det_parametros.IdParametroPicking))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_picking_det_parametros, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
