Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_pe
    Public Shared Function Insertar(ByRef oBeLog_error_wms_pe As clsBeLog_error_wms_pe,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            ' Inicializa el helper de inserción
            Ins.Init("log_error_wms_pe")
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            ' Agrega los campos si tienen valor
            If oBeLog_error_wms_pe.IdEmpresa <> 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If oBeLog_error_wms_pe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBeLog_error_wms_pe.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            If oBeLog_error_wms_pe.IdPedidoDet <> 0 Then Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            If oBeLog_error_wms_pe.CodigoProducto <> "" Then Ins.Add("codigoproducto", "@codigoproducto", DataType.Parametro)
            If oBeLog_error_wms_pe.Cantidad <> 0 Then Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            If oBeLog_error_wms_pe.IdUMBas <> 0 Then Ins.Add("idumbas", "@idumbas", DataType.Parametro)
            If oBeLog_error_wms_pe.IdEstado <> 0 Then Ins.Add("idestado", "@idestado", DataType.Parametro)
            If oBeLog_error_wms_pe.NoLinea <> 0 Then Ins.Add("nolinea", "@nolinea", DataType.Parametro)
            If oBeLog_error_wms_pe.IdPresentacion <> 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            If oBeLog_error_wms_pe.Talla <> "" Then Ins.Add("talla", "@talla", DataType.Parametro)
            If oBeLog_error_wms_pe.Color <> "" Then Ins.Add("color", "@color", DataType.Parametro)
            If oBeLog_error_wms_pe.UsrAgr <> 0 Then Ins.Add("usr_agr", "@usr_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            ' Parámetros obligatorios
            cmd.Parameters.Add(New SqlParameter("@mensajeerror", oBeLog_error_wms_pe.MensajeError))
            cmd.Parameters.Add(New SqlParameter("@rutaerror", oBeLog_error_wms_pe.RutaError))
            cmd.Parameters.Add(New SqlParameter("@fec_agr", oBeLog_error_wms_pe.FecAgr))

            ' Parámetros opcionales
            If oBeLog_error_wms_pe.IdEmpresa <> 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBeLog_error_wms_pe.IdEmpresa))
            If oBeLog_error_wms_pe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBeLog_error_wms_pe.IdBodega))
            If oBeLog_error_wms_pe.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidoenc", oBeLog_error_wms_pe.IdPedidoEnc))
            If oBeLog_error_wms_pe.IdPedidoDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpedidodet", oBeLog_error_wms_pe.IdPedidoDet))
            If oBeLog_error_wms_pe.CodigoProducto <> "" Then cmd.Parameters.Add(New SqlParameter("@codigoproducto", oBeLog_error_wms_pe.CodigoProducto))
            If oBeLog_error_wms_pe.Cantidad <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad", oBeLog_error_wms_pe.Cantidad))
            If oBeLog_error_wms_pe.IdUMBas <> 0 Then cmd.Parameters.Add(New SqlParameter("@idumbas", oBeLog_error_wms_pe.IdUMBas))
            If oBeLog_error_wms_pe.IdEstado <> 0 Then cmd.Parameters.Add(New SqlParameter("@idestado", oBeLog_error_wms_pe.IdEstado))
            If oBeLog_error_wms_pe.NoLinea <> 0 Then cmd.Parameters.Add(New SqlParameter("@nolinea", oBeLog_error_wms_pe.NoLinea))
            If oBeLog_error_wms_pe.IdPresentacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpresentacion", oBeLog_error_wms_pe.IdPresentacion))
            If oBeLog_error_wms_pe.Talla <> "" Then cmd.Parameters.Add(New SqlParameter("@talla", oBeLog_error_wms_pe.Talla))
            If oBeLog_error_wms_pe.Color <> "" Then cmd.Parameters.Add(New SqlParameter("@color", oBeLog_error_wms_pe.Color))
            If oBeLog_error_wms_pe.UsrAgr <> 0 Then cmd.Parameters.Add(New SqlParameter("@usr_agr", oBeLog_error_wms_pe.UsrAgr))

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
