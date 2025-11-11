Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_pick

    Public Shared Function Insertar(ByRef oBe As clsBeLog_error_wms_pick,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Ins.Init("log_error_wms_pick")

            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            If oBe.IdEmpresa <> 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If oBe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBe.RutaError <> "" Then Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)

            If oBe.IdPickingEnc <> 0 Then Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            If oBe.IdPickingDet <> 0 Then Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            If oBe.IdPickingUbic <> 0 Then Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)

            If oBe.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            If oBe.IdPedidoDet <> 0 Then Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)

            If oBe.CodigoProducto <> "" Then Ins.Add("codigoproducto", "@codigoproducto", DataType.Parametro)
            If oBe.NombreProducto <> "" Then Ins.Add("nombreproducto", "@nombreproducto", DataType.Parametro)

            If oBe.Cantidad_Recibida <> 0 Then Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            If oBe.User_Agr <> 0 Then Ins.Add("user_agr", "@user_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim esRemota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            If esRemota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            ' Parámetros requeridos
            cmd.Parameters.Add(New SqlParameter("@mensajeerror", oBe.MensajeError))
            cmd.Parameters.Add(New SqlParameter("@fec_agr", oBe.Fec_agr))

            If oBe.IdEmpresa <> 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBe.IdEmpresa))
            If oBe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBe.IdBodega))
            If oBe.RutaError <> "" Then cmd.Parameters.Add(New SqlParameter("@rutaerror", oBe.RutaError))

            If oBe.IdPickingEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingenc", oBe.IdPickingEnc))
            If oBe.IdPickingDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingdet", oBe.IdPickingDet))
            If oBe.IdPickingUbic <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingubic", oBe.IdPickingUbic))

            If oBe.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidoenc", oBe.IdPedidoEnc))
            If oBe.IdPedidoDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidodet", oBe.IdPedidoDet))

            If oBe.CodigoProducto <> "" Then cmd.Parameters.Add(New SqlParameter("@codigoproducto", oBe.CodigoProducto))
            If oBe.NombreProducto <> "" Then cmd.Parameters.Add(New SqlParameter("@nombreproducto", oBe.NombreProducto))

            If oBe.Cantidad_Recibida <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad_recibida", oBe.Cantidad_Recibida))
            If oBe.User_Agr <> 0 Then cmd.Parameters.Add(New SqlParameter("@user_agr", oBe.User_Agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            If Not esRemota Then lTransaction.Commit()
            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction?.Dispose()
            lConnection?.Dispose()
        End Try
    End Function

End Class
