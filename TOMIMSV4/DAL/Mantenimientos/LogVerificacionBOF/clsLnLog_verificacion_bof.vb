Imports System.Data.SqlClient

Public Class clsLnLog_verificacion_bof

    Public Shared Function Insertar(ByRef oBe As clsBeLog_verificacion_bof,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("log_verificacion_bof")

            ' Campos mínimos
            Ins.Add("MensajeError", "@MensajeError", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            ' Opcionales
            If oBe.RutaError <> "" Then Ins.Add("RutaError", "@RutaError", DataType.Parametro)
            If oBe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBe.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            If oBe.IdPedidoDet <> 0 Then Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            If oBe.IdPickingUbic <> 0 Then Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            If oBe.IdPickingEnc <> 0 Then Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            If oBe.IdPickingDet <> 0 Then Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            If oBe.IdProductoBodega <> 0 Then Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            If oBe.Sku <> "" Then Ins.Add("sku", "@sku", DataType.Parametro)
            If oBe.Cantidad <> 0 Then Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            If oBe.IdMotivo <> 0 Then Ins.Add("idmotivo", "@idmotivo", DataType.Parametro)
            If oBe.IdEstado <> 0 Then Ins.Add("idestado", "@idestado", DataType.Parametro)
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


            ' Parámetros obligatorios
            cmd.Parameters.Add(New SqlParameter("@fec_agr", oBe.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@MensajeError", oBe.MensajeError))

            ' Parámetros opcionales
            If oBe.RutaError <> "" Then cmd.Parameters.Add(New SqlParameter("@RutaError", oBe.RutaError))
            If oBe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBe.IdBodega))
            If oBe.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidoenc", oBe.IdPedidoEnc))
            If oBe.IdPedidoDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidodet", oBe.IdPedidoDet))
            If oBe.IdPickingUbic <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingubic", oBe.IdPickingUbic))
            If oBe.IdPickingEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingenc", oBe.IdPickingEnc))
            If oBe.IdPickingDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpickingdet", oBe.IdPickingDet))
            If oBe.IdProductoBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idproductobodega", oBe.IdProductoBodega))
            If oBe.Sku <> "" Then cmd.Parameters.Add(New SqlParameter("@sku", oBe.Sku))
            If oBe.Cantidad <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad", oBe.Cantidad))
            If oBe.IdMotivo <> 0 Then cmd.Parameters.Add(New SqlParameter("@idmotivo", oBe.IdMotivo))
            If oBe.IdEstado <> 0 Then cmd.Parameters.Add(New SqlParameter("@idestado", oBe.IdEstado))
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
