Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_oc
    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                     ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pIdUsuarioAgr As Integer,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdOCEnc As Integer = 0,
                                    Optional ByVal pIdOCDet As Integer = 0)

        Try

            Dim oBeLog_error_wms_oc As New clsBeLog_error_wms_oc()

            oBeLog_error_wms_oc.MensajeError = pMensajeExcepcion
            oBeLog_error_wms_oc.Fecha = Now
            oBeLog_error_wms_oc.IdEmpresa = pIdEmpresa
            oBeLog_error_wms_oc.IdBodega = pIdBodega
            oBeLog_error_wms_oc.IdUsuarioAgr = pIdUsuarioAgr
            oBeLog_error_wms_oc.RutaError = pStackTrace
            oBeLog_error_wms_oc.IdOrdenCompraEnc = pIdOCEnc
            oBeLog_error_wms_oc.IdOrdenCompraDet = pIdOCDet

            Insertar(oBeLog_error_wms_oc)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class
