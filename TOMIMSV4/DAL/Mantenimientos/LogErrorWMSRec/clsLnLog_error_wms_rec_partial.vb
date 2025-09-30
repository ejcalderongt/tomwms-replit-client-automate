Imports System.Reflection
Imports DevExpress.CodeParser

Partial Public Class clsLnLog_error_wms_rec
    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                     ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pIdUsuarioAgr As Integer,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdRecEnc As Integer = 0)

        Try

            Dim oBeLog_error_wms_rec As New clsBeLog_error_wms_rec()
            oBeLog_error_wms_rec.MensajeError = pMensajeExcepcion
            oBeLog_error_wms_rec.Fecha = Now
            oBeLog_error_wms_rec.IdEmpresa = pIdEmpresa
            oBeLog_error_wms_rec.IdBodega = pIdBodega
            oBeLog_error_wms_rec.IdUsuarioAgr = pIdUsuarioAgr
            If Not pStackTrace = "" Then oBeLog_error_wms_rec.RutaError = pStackTrace
            If Not pIdRecEnc = 0 Then oBeLog_error_wms_rec.IdRecepcionEnc = pIdRecEnc

            Insertar(oBeLog_error_wms_rec)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw ex
        End Try

    End Sub
End Class
