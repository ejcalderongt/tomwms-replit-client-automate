Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_pack

    Public Shared Function Insertar(ByRef oBe As clsBeLog_error_wms_pack,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Ins.Init("log_error_wms_pack")

            ' Requeridos (mínimos que siempre enviamos)
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("esimplosion", "@esimplosion", DataType.Parametro)

            ' Opcionales (solo si tienen valor)
            If oBe.RutaError <> "" Then Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)
            If oBe.IdEmpresa <> 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If oBe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBe.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            If oBe.IdPickingEnc <> 0 Then Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            If oBe.IdPickingUbic <> 0 Then Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            If oBe.IdDespachoEnc <> 0 Then Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            If oBe.IdStock <> 0 Then Ins.Add("idstock", "@idstock", DataType.Parametro)
            If oBe.IdProductoBodega <> 0 Then Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            If oBe.IdProductoEstado <> 0 Then Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            If oBe.IdPresentacion <> 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            If oBe.IdUnidadMedida <> 0 Then Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            If oBe.Lic_Plate <> "" Then Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            If oBe.Cantidad_Bultos_Packing <> 0 Then Ins.Add("cantidad_bultos_packing", "@cantidad_bultos_packing", DataType.Parametro)
            If oBe.IdOperador <> 0 Then Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            If oBe.User_agr <> "" Then Ins.Add("user_agr", "@user_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As SqlCommand

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
            cmd.Parameters.Add(New SqlParameter("@esimplosion", oBe.EsImplosion))

            ' Parámetros opcionales
            If oBe.RutaError <> "" Then cmd.Parameters.Add(New SqlParameter("@rutaerror", oBe.RutaError))
            If oBe.IdEmpresa <> 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBe.IdEmpresa))
            If oBe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBe.IdBodega))
            If oBe.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidoenc", oBe.IdPedidoEnc))
            If oBe.IdPickingEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingenc", oBe.IdPickingEnc))
            If oBe.IdPickingUbic <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingubic", oBe.IdPickingUbic))
            If oBe.IdDespachoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@iddespachoenc", oBe.IdDespachoEnc))
            If oBe.IdStock <> 0 Then cmd.Parameters.Add(New SqlParameter("@idstock", oBe.IdStock))
            If oBe.IdProductoBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idproductobodega", oBe.IdProductoBodega))
            If oBe.IdProductoEstado <> 0 Then cmd.Parameters.Add(New SqlParameter("@idproductoestado", oBe.IdProductoEstado))
            If oBe.IdPresentacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpresentacion", oBe.IdPresentacion))
            If oBe.IdUnidadMedida <> 0 Then cmd.Parameters.Add(New SqlParameter("@idunidadmedida", oBe.IdUnidadMedida))
            If oBe.Lic_Plate <> "" Then cmd.Parameters.Add(New SqlParameter("@lic_plate", oBe.Lic_Plate))
            If oBe.Cantidad_Bultos_Packing <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad_bultos_packing", oBe.Cantidad_Bultos_Packing))
            If oBe.IdOperador <> 0 Then cmd.Parameters.Add(New SqlParameter("@idoperador", oBe.IdOperador))
            If oBe.User_agr <> "" Then cmd.Parameters.Add(New SqlParameter("@user_agr", oBe.User_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            If Not esRemota Then lTransaction.Commit()
            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction?.Dispose()
            lConnection?.Dispose()
        End Try
    End Function

End Class
