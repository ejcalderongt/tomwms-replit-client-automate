Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_pack

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdEmpresa As Integer = 0,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pIdPedidoEnc As Integer = 0,
                                    Optional ByVal pIdPickingEnc As Integer = 0,
                                    Optional ByVal pIdPickingUbic As Integer = 0,
                                    Optional ByVal pIdDespachoEnc As Integer = 0,
                                    Optional ByVal pIdStock As Integer = 0,
                                    Optional ByVal pIdProductoBodega As Integer = 0,
                                    Optional ByVal pIdProductoEstado As Integer = 0,
                                    Optional ByVal pIdPresentacion As Integer = 0,
                                    Optional ByVal pIdUnidadMedida As Integer = 0,
                                    Optional ByVal pLic_Plate As String = "",
                                    Optional ByVal pCantidad_Bultos_Packing As Integer = 0,
                                    Optional ByVal pIdOperador As Integer = 0,
                                    Optional ByVal pUsuario_agr As String = "",
                                    Optional ByVal pEsImplosion As Boolean = False,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBe As New clsBeLog_error_wms_pack()

            ' Obligatorio
            oBe.MensajeError = pMensajeExcepcion

            ' Opcionales
            oBe.IdEmpresa = pIdEmpresa
            oBe.IdBodega = pIdBodega
            oBe.RutaError = pStackTrace
            oBe.IdPedidoEnc = pIdPedidoEnc
            oBe.IdPickingEnc = pIdPickingEnc
            oBe.IdPickingUbic = pIdPickingUbic
            oBe.IdDespachoEnc = pIdDespachoEnc
            oBe.IdStock = pIdStock
            oBe.IdProductoBodega = pIdProductoBodega
            oBe.IdProductoEstado = pIdProductoEstado
            oBe.IdPresentacion = pIdPresentacion
            oBe.IdUnidadMedida = pIdUnidadMedida
            oBe.Lic_Plate = pLic_Plate
            oBe.Cantidad_Bultos_Packing = pCantidad_Bultos_Packing
            oBe.IdOperador = pIdOperador
            oBe.User_agr = pUsuario_agr
            oBe.Fec_agr = Now
            oBe.EsImplosion = pEsImplosion

            Insertar(oBe, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw
        End Try

    End Sub

End Class
