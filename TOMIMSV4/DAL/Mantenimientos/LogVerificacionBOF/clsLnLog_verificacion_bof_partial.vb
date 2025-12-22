Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_verificacion_bof

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pIdPedidoEnc As Integer = 0,
                                    Optional ByVal pIdPedidoDet As Integer = 0,
                                    Optional ByVal pIdPickingUbic As Integer = 0,
                                    Optional ByVal pIdPickingEnc As Integer = 0,
                                    Optional ByVal pIdPickingDet As Integer = 0,
                                    Optional ByVal pIdProductoBodega As Integer = 0,
                                    Optional ByVal pSku As String = "",
                                    Optional ByVal pCantidad As Double = 0,
                                    Optional ByVal pIdMotivo As Integer = 0,
                                    Optional ByVal pIdEstado As Integer = 0,
                                    Optional ByVal pUser_agr As String = "",
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBe As New clsBeLog_verificacion_bof()

            ' Obligatorios
            oBe.MensajeError = pMensajeExcepcion
            oBe.Fec_agr = Now

            ' Optional
            '       oBe.RutaError = pStackTrace
            ' Optional
            oBe.RutaError = pStackTrace
            oBe.IdBodega = pIdBodega
            oBe.IdPedidoEnc = pIdPedidoEnc
            oBe.IdPedidoDet = pIdPedidoDet
            oBe.IdPickingUbic = pIdPickingUbic
            oBe.IdPickingEnc = pIdPickingEnc
            oBe.IdPickingDet = pIdPickingDet
            oBe.IdProductoBodega = pIdProductoBodega
            oBe.Sku = pSku
            oBe.Cantidad = pCantidad
            oBe.IdMotivo = pIdMotivo
            oBe.IdEstado = pIdEstado
            oBe.User_agr = pUser_agr

            Insertar(oBe, pConection, pTransaction)
            'Dim vMsgError As String =
        Catch ex As Exception
            Dim vMsgError As String =
                String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw
        End Try

    End Sub
