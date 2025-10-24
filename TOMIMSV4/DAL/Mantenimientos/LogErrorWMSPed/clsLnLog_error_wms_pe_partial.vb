Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_pe
    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pIdEmpresa As Integer = 0,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pUsrAgr As Integer = 0,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdPedidoEnc As Integer = 0,
                                    Optional ByVal pIdPedidoDet As Integer = 0,
                                    Optional ByVal pCodigoProducto As String = "",
                                    Optional ByVal pCantidad As Double = 0,
                                    Optional ByVal pIdUMBas As Integer = 0,
                                    Optional ByVal pIdEstado As Integer = 0,
                                    Optional ByVal pNoLinea As Integer = 0,
                                    Optional ByVal pIdPresentacion As Integer = 0,
                                    Optional ByVal pTalla As String = "",
                                    Optional ByVal pColor As String = "",
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBeLog_error_wms_pe As New clsBeLog_error_wms_pe()

            ' Obligatorio
            oBeLog_error_wms_pe.MensajeError = pMensajeExcepcion

            ' Opcionales
            oBeLog_error_wms_pe.IdEmpresa = pIdEmpresa
            oBeLog_error_wms_pe.IdBodega = pIdBodega
            oBeLog_error_wms_pe.RutaError = pStackTrace
            oBeLog_error_wms_pe.IdPedidoEnc = pIdPedidoEnc
            oBeLog_error_wms_pe.IdPedidoDet = pIdPedidoDet
            oBeLog_error_wms_pe.CodigoProducto = pCodigoProducto
            oBeLog_error_wms_pe.Cantidad = pCantidad
            oBeLog_error_wms_pe.IdUMBas = pIdUMBas
            oBeLog_error_wms_pe.IdEstado = pIdEstado
            oBeLog_error_wms_pe.NoLinea = pNoLinea
            oBeLog_error_wms_pe.IdPresentacion = pIdPresentacion
            oBeLog_error_wms_pe.Talla = pTalla
            oBeLog_error_wms_pe.Color = pColor
            oBeLog_error_wms_pe.UsrAgr = pUsrAgr
            oBeLog_error_wms_pe.FecAgr = Now

            Insertar(oBeLog_error_wms_pe, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw ex
        End Try

    End Sub
End Class
