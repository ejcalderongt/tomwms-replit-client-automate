Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_rec
    Public Shared Function Insertar(ByRef oBeLog_error_wms_rec As clsBeLog_error_wms_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("log_error_wms_rec")
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)

            If Not oBeLog_error_wms_rec.Line_No = 0 Then Ins.Add("Line_No", "@Line_No", DataType.Parametro)
            If Not oBeLog_error_wms_rec.UmBas = "" Then Ins.Add("UmBas", "@UmBas", DataType.Parametro)
            If Not oBeLog_error_wms_rec.Variant_Code = "" Then Ins.Add("Variant_Code", "@Variant_Code", DataType.Parametro)
            If Not oBeLog_error_wms_rec.Item_No = "" Then Ins.Add("Item_No", "@Item_No", DataType.Parametro)
            If Not oBeLog_error_wms_rec.Cantidad = 0 Then Ins.Add("Cantidad", "@Cantidad", DataType.Parametro)
            If Not oBeLog_error_wms_rec.Referencia_Documento = "" Then Ins.Add("Referencia_Documento", "@Referencia_Documento", DataType.Parametro)
            If Not oBeLog_error_wms_rec.IdEmpresa = 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If Not oBeLog_error_wms_rec.IdBodega = 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If Not oBeLog_error_wms_rec.IdRecepcionEnc = 0 Then Ins.Add("IdRecepcionEnc", "@IdRecepcionEnc", DataType.Parametro)
            If Not oBeLog_error_wms_rec.IdRecepcionDet = 0 Then Ins.Add("IdRecepcionDet", "@IdRecepcionDet", DataType.Parametro)
            If Not oBeLog_error_wms_rec.IdUsuarioAgr = 0 Then Ins.Add("IdUsuarioAgr", "@IdUsuarioAgr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@fecha", oBeLog_error_wms_rec.Fecha))
            cmd.Parameters.Add(New SqlParameter("@mensajeerror", oBeLog_error_wms_rec.MensajeError))

            If Not oBeLog_error_wms_rec.Line_No = 0 Then cmd.Parameters.Add(New SqlParameter("@Line_No", oBeLog_error_wms_rec.Line_No))
            If Not oBeLog_error_wms_rec.UmBas = "" Then cmd.Parameters.Add(New SqlParameter("@UmBas", oBeLog_error_wms_rec.UmBas))
            If Not oBeLog_error_wms_rec.Variant_Code = "" Then cmd.Parameters.Add(New SqlParameter("@Variant_Code", oBeLog_error_wms_rec.Variant_Code))
            If Not oBeLog_error_wms_rec.Item_No = "" Then cmd.Parameters.Add(New SqlParameter("@Item_No", oBeLog_error_wms_rec.Item_No))
            If Not oBeLog_error_wms_rec.Cantidad = 0 Then cmd.Parameters.Add(New SqlParameter("@Cantidad", oBeLog_error_wms_rec.Cantidad))
            If Not oBeLog_error_wms_rec.Referencia_Documento = "" Then cmd.Parameters.Add(New SqlParameter("@Referencia_Documento", oBeLog_error_wms_rec.Referencia_Documento))
            If Not oBeLog_error_wms_rec.IdEmpresa = 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBeLog_error_wms_rec.IdEmpresa))
            If Not oBeLog_error_wms_rec.IdBodega = 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBeLog_error_wms_rec.IdBodega))
            If Not oBeLog_error_wms_rec.IdRecepcionEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IdRecepcionEnc", oBeLog_error_wms_rec.IdRecepcionEnc))
            If Not oBeLog_error_wms_rec.IdRecepcionDet = 0 Then cmd.Parameters.Add(New SqlParameter("@IdRecepcionDet", oBeLog_error_wms_rec.IdRecepcionDet))
            If Not oBeLog_error_wms_rec.IdUsuarioAgr = 0 Then cmd.Parameters.Add(New SqlParameter("@IdUsuarioAgr", oBeLog_error_wms_rec.IdUsuarioAgr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
End Class
