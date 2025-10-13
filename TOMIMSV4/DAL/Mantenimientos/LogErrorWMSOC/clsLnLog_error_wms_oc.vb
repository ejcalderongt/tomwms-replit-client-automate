Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_oc
    Public Shared Function Insertar(ByRef oBeLog_error_wms_oc As clsBeLog_error_wms_oc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("log_error_wms_oc")
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)

            If Not oBeLog_error_wms_oc.IdEmpresa = 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If Not oBeLog_error_wms_oc.IdBodega = 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If Not oBeLog_error_wms_oc.RutaError = "" Then Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)
            If Not oBeLog_error_wms_oc.IdOrdenCompraEnc = 0 Then Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            If oBeLog_error_wms_oc.IdOrdenCompraEnc <> 0 AndAlso oBeLog_error_wms_oc.IdOrdenCompraDet <> 0 Then Ins.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            If Not oBeLog_error_wms_oc.IdUsuarioAgr = 0 Then Ins.Add("idusuarioagr", "@idusuarioagr", DataType.Parametro)
            If Not oBeLog_error_wms_oc.Codigo_producto = "" Then Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            If Not oBeLog_error_wms_oc.UmBas = "" Then Ins.Add("umbas", "@umbas", DataType.Parametro)
            If Not oBeLog_error_wms_oc.Variant_Code = "" Then Ins.Add("variant_code", "@variant_code", DataType.Parametro)
            If Not oBeLog_error_wms_oc.Cantidad = 0 Then Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            If Not oBeLog_error_wms_oc.Referencia_Documento = "" Then Ins.Add("referencia_documento", "@referencia_documento", DataType.Parametro)
            If Not oBeLog_error_wms_oc.IdUsuario = 0 Then Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            If Not oBeLog_error_wms_oc.IdOperador = 0 Then Ins.Add("idoperador", "@idoperador", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@fecha", oBeLog_error_wms_oc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@mensajeerror", oBeLog_error_wms_oc.MensajeError))

            If Not oBeLog_error_wms_oc.IdEmpresa = 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBeLog_error_wms_oc.IdEmpresa))
            If Not oBeLog_error_wms_oc.IdBodega = 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBeLog_error_wms_oc.IdBodega))
            If Not oBeLog_error_wms_oc.RutaError = "" Then cmd.Parameters.Add(New SqlParameter("@rutaerror", oBeLog_error_wms_oc.RutaError))
            If Not oBeLog_error_wms_oc.IdOrdenCompraEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@idordencompraenc", oBeLog_error_wms_oc.IdOrdenCompraEnc))
            If oBeLog_error_wms_oc.IdOrdenCompraEnc <> 0 AndAlso oBeLog_error_wms_oc.IdOrdenCompraDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idordencompradet", oBeLog_error_wms_oc.IdOrdenCompraDet))
            If Not oBeLog_error_wms_oc.IdUsuarioAgr = 0 Then cmd.Parameters.Add(New SqlParameter("@idusuarioagr", oBeLog_error_wms_oc.IdUsuarioAgr))
            If Not oBeLog_error_wms_oc.Codigo_producto = "" Then cmd.Parameters.Add(New SqlParameter("@codigo_producto", oBeLog_error_wms_oc.Codigo_producto))
            If Not oBeLog_error_wms_oc.UmBas = "" Then cmd.Parameters.Add(New SqlParameter("@umbas", oBeLog_error_wms_oc.UmBas))
            If Not oBeLog_error_wms_oc.Variant_Code = "" Then cmd.Parameters.Add(New SqlParameter("@variant_code", oBeLog_error_wms_oc.Variant_Code))
            If Not oBeLog_error_wms_oc.Cantidad = 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad", oBeLog_error_wms_oc.Cantidad))
            If Not oBeLog_error_wms_oc.Referencia_Documento = "" Then cmd.Parameters.Add(New SqlParameter("@referencia_documento", oBeLog_error_wms_oc.Referencia_Documento))
            If Not oBeLog_error_wms_oc.IdUsuario = 0 Then cmd.Parameters.Add(New SqlParameter("@idusuario", oBeLog_error_wms_oc.IdUsuario))
            If Not oBeLog_error_wms_oc.IdOperador = 0 Then cmd.Parameters.Add(New SqlParameter("@idoperador", oBeLog_error_wms_oc.IdOperador))

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
