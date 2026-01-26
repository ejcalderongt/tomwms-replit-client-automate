Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_reab

    Public Shared Function Insertar(ByRef oBe As clsBeLog_error_wms_reab,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Ins.Init("log_error_wms_reab")

            ' Campos mínimos que siempre enviamos
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            ' Opcionales (solo si tienen valor)
            If oBe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBe.RutaError <> "" Then Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)
            If oBe.IdStock <> 0 Then Ins.Add("idstock", "@idstock", DataType.Parametro)
            If oBe.IdMovimiento <> 0 Then Ins.Add("idmovimiento", "@idmovimiento", DataType.Parametro)
            If oBe.Lic_Plate_Anterior <> "" Then Ins.Add("lic_plate_anterior", "@lic_plate_anterior", DataType.Parametro)
            If oBe.Lic_Plate <> "" Then Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            If oBe.IdResolucion <> 0 Then Ins.Add("idresolucion", "@idresolucion", DataType.Parametro)
            If oBe.IdProductoBodega <> 0 Then Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            If oBe.Cantidad <> 0 Then Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            If oBe.User_agr <> 0 Then Ins.Add("user_agr", "@user_agr", DataType.Parametro)

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

            ' Parámetros opcionales
            If oBe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBe.IdBodega))
            If oBe.RutaError <> "" Then cmd.Parameters.Add(New SqlParameter("@rutaerror", oBe.RutaError))
            If oBe.IdStock <> 0 Then cmd.Parameters.Add(New SqlParameter("@idstock", oBe.IdStock))
            If oBe.IdMovimiento <> 0 Then cmd.Parameters.Add(New SqlParameter("@idmovimiento", oBe.IdMovimiento))
            If oBe.Lic_Plate_Anterior <> "" Then cmd.Parameters.Add(New SqlParameter("@lic_plate_anterior", oBe.Lic_Plate_Anterior))
            If oBe.Lic_Plate <> "" Then cmd.Parameters.Add(New SqlParameter("@lic_plate", oBe.Lic_Plate))
            If oBe.IdResolucion <> 0 Then cmd.Parameters.Add(New SqlParameter("@idresolucion", oBe.IdResolucion))
            If oBe.IdProductoBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idproductobodega", oBe.IdProductoBodega))
            If oBe.Cantidad <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad", oBe.Cantidad))
            If oBe.User_agr <> 0 Then cmd.Parameters.Add(New SqlParameter("@user_agr", oBe.User_agr))

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
