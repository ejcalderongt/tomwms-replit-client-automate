Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_ubic

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pIdEmpresa As Integer = 0,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdTareaUbicacionEnc As Integer = 0,
                                    Optional ByVal pIdMotivoUbicacion As Integer = 0,
                                    Optional ByVal pIdTareaUbicacionDet As Integer = 0,
                                    Optional ByVal pIdUbicacionOrigen As Integer = 0,
                                    Optional ByVal pIdUbicacionDestino As Integer = 0,
                                    Optional ByVal pIdEstadoOrigen As Integer = 0,
                                    Optional ByVal pIdEstadoDestino As Integer = 0,
                                    Optional ByVal pIdStock As Integer = 0,
                                    Optional ByVal pIdUMBAs As Integer = 0,
                                    Optional ByVal pIdPresentacion As Integer = 0,
                                    Optional ByVal pCantidad As Double = 0,
                                    Optional ByVal pLicencia As String = "",
                                    Optional ByVal pIdOperador As Integer = 0,
                                    Optional ByVal pUsrAgr As Integer = 0,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBe As New clsBeLog_error_wms_ubic()

            ' Obligatorio
            oBe.MensajeError = pMensajeExcepcion

            ' Opcionales
            oBe.IdEmpresa = pIdEmpresa
            oBe.IdBodega = pIdBodega
            oBe.RutaError = pStackTrace

            oBe.IdTareaUbicacionEnc = pIdTareaUbicacionEnc
            oBe.IdMotivoUbicacion = pIdMotivoUbicacion
            oBe.IdTareaUbicacionDet = pIdTareaUbicacionDet

            oBe.IdUbicacionOrigen = pIdUbicacionOrigen
            oBe.IdUbicacionDestino = pIdUbicacionDestino

            oBe.IdEstadoOrigen = pIdEstadoOrigen
            oBe.IdEstadoDestino = pIdEstadoDestino

            oBe.IdStock = pIdStock
            oBe.IdUMBAs = pIdUMBAs
            oBe.IdPresentacion = pIdPresentacion
            oBe.Cantidad = pCantidad
            oBe.Licencia = pLicencia
            oBe.IdOperador = pIdOperador

            oBe.UsrAgr = pUsrAgr
            oBe.FechaAgr = Now

            Insertar(oBe, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw
        End Try

    End Sub

End Class
