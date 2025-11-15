Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_pick

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pIdEmpresa As Integer = 0,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pUserAgr As Integer = 0,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdPickingEnc As Integer = 0,
                                    Optional ByVal pIdPickingDet As Integer = 0,
                                    Optional ByVal pIdPickingUbic As Integer = 0,
                                    Optional ByVal pIdPedidoEnc As Integer = 0,
                                    Optional ByVal pIdPedidoDet As Integer = 0,
                                    Optional ByVal pCodigoProducto As String = "",
                                    Optional ByVal pNombreProducto As String = "",
                                    Optional ByVal pCantidadRecibida As Double = 0,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBe As New clsBeLog_error_wms_pick()

            ' Obligatorio
            oBe.MensajeError = pMensajeExcepcion

            ' Opcionales
            oBe.IdEmpresa = pIdEmpresa
            oBe.IdBodega = pIdBodega
            oBe.RutaError = pStackTrace

            oBe.IdPickingEnc = pIdPickingEnc
            oBe.IdPickingDet = pIdPickingDet
            oBe.IdPickingUbic = pIdPickingUbic

            oBe.IdPedidoEnc = pIdPedidoEnc
            oBe.IdPedidoDet = pIdPedidoDet

            oBe.CodigoProducto = pCodigoProducto
            oBe.NombreProducto = pNombreProducto
            oBe.Cantidad_Recibida = pCantidadRecibida

            oBe.User_Agr = pUserAgr
            oBe.Fec_agr = Now ' datetime en la tabla

            Insertar(oBe, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw
        End Try

    End Sub

End Class
