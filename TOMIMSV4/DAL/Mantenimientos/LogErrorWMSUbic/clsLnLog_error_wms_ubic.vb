Imports System.Data.SqlClient

Public Class clsLnLog_error_wms_ubic

    Public Shared Function Insertar(ByRef oBe As clsBeLog_error_wms_ubic,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Ins.Init("log_error_wms_ubic")

            ' Requeridos (mínimos)
            Ins.Add("mensajeerror", "@mensajeerror", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

            ' Opcionales (solo si tienen valor)
            If oBe.IdEmpresa <> 0 Then Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            If oBe.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBe.RutaError <> "" Then Ins.Add("rutaerror", "@rutaerror", DataType.Parametro)

            If oBe.IdTareaUbicacionEnc <> 0 Then Ins.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            If oBe.IdMotivoUbicacion <> 0 Then Ins.Add("idmotivoubicacion", "@idmotivoubicacion", DataType.Parametro)
            If oBe.IdTareaUbicacionDet <> 0 Then Ins.Add("idtareaubicaciondet", "@idtareaubicaciondet", DataType.Parametro)

            If oBe.IdUbicacionOrigen <> 0 Then Ins.Add("idubicacionorigen", "@idubicacionorigen", DataType.Parametro)
            If oBe.IdUbicacionDestino <> 0 Then Ins.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)

            If oBe.IdEstadoOrigen <> 0 Then Ins.Add("idestadoorigen", "@idestadoorigen", DataType.Parametro)
            If oBe.IdEstadoDestino <> 0 Then Ins.Add("idestadodestino", "@idestadodestino", DataType.Parametro)

            If oBe.IdStock <> 0 Then Ins.Add("idstock", "@idstock", DataType.Parametro)
            If oBe.IdUMBAs <> 0 Then Ins.Add("idumbas", "@idumbas", DataType.Parametro)
            If oBe.IdPresentacion <> 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            If oBe.Cantidad <> 0 Then Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            If oBe.Licencia <> "" Then Ins.Add("licencia", "@licencia", DataType.Parametro)
            If oBe.IdOperador <> 0 Then Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            If oBe.user_agr <> 0 Then Ins.Add("user_agr", "@user_agr", DataType.Parametro)

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
            cmd.Parameters.Add(New SqlParameter("@fec_agr", oBe.fec_agr))

            ' Parámetros opcionales
            If oBe.IdEmpresa <> 0 Then cmd.Parameters.Add(New SqlParameter("@idempresa", oBe.IdEmpresa))
            If oBe.IdBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@idbodega", oBe.IdBodega))
            If oBe.RutaError <> "" Then cmd.Parameters.Add(New SqlParameter("@rutaerror", oBe.RutaError))

            If oBe.IdTareaUbicacionEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@idtareaubicacionenc", oBe.IdTareaUbicacionEnc))
            If oBe.IdMotivoUbicacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@idmotivoubicacion", oBe.IdMotivoUbicacion))
            If oBe.IdTareaUbicacionDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@idtareaubicaciondet", oBe.IdTareaUbicacionDet))

            If oBe.IdUbicacionOrigen <> 0 Then cmd.Parameters.Add(New SqlParameter("@idubicacionorigen", oBe.IdUbicacionOrigen))
            If oBe.IdUbicacionDestino <> 0 Then cmd.Parameters.Add(New SqlParameter("@idubicaciondestino", oBe.IdUbicacionDestino))

            If oBe.IdEstadoOrigen <> 0 Then cmd.Parameters.Add(New SqlParameter("@idestadoorigen", oBe.IdEstadoOrigen))
            If oBe.IdEstadoDestino <> 0 Then cmd.Parameters.Add(New SqlParameter("@idestadodestino", oBe.IdEstadoDestino))

            If oBe.IdStock <> 0 Then cmd.Parameters.Add(New SqlParameter("@idstock", oBe.IdStock))
            If oBe.IdUMBAs <> 0 Then cmd.Parameters.Add(New SqlParameter("@idumbas", oBe.IdUMBAs))
            If oBe.IdPresentacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@idpresentacion", oBe.IdPresentacion))
            If oBe.Cantidad <> 0 Then cmd.Parameters.Add(New SqlParameter("@cantidad", oBe.Cantidad))
            If oBe.Licencia <> "" Then cmd.Parameters.Add(New SqlParameter("@licencia", oBe.Licencia))
            If oBe.IdOperador <> 0 Then cmd.Parameters.Add(New SqlParameter("@idoperador", oBe.IdOperador))
            If oBe.user_agr <> 0 Then cmd.Parameters.Add(New SqlParameter("@user_agr", oBe.user_agr))

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
