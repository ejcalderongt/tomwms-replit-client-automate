Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.CodeParser

Partial Public Class clsLnLog_error_wms_rec
    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pIdEmpresa As Integer = 0,
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pIdUsuarioAgr As Integer = 0,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdRecEnc As Integer = 0,
                                    Optional ByVal pIdRecDet As Integer = 0,
                                    Optional ByVal pLineNo As Integer = 0,
                                    Optional ByVal pItemNo As String = "",
                                    Optional ByVal pUMBas As String = "",
                                    Optional ByVal pVariantCode As String = "",
                                    Optional ByVal pCantidad As Integer = 0,
                                    Optional ByVal pReferenciaDocumento As String = "",
                                    Optional ByVal pNumeroLinea As Integer = 0,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try

            Dim oBeLog_error_wms_rec As New clsBeLog_error_wms_rec()
            oBeLog_error_wms_rec.MensajeError = pMensajeExcepcion
            oBeLog_error_wms_rec.Fecha = Now
            oBeLog_error_wms_rec.IdEmpresa = pIdEmpresa
            oBeLog_error_wms_rec.IdBodega = pIdBodega
            oBeLog_error_wms_rec.IdUsuarioAgr = pIdUsuarioAgr
            oBeLog_error_wms_rec.RutaError = pStackTrace
            oBeLog_error_wms_rec.IdRecepcionEnc = pIdRecEnc
            oBeLog_error_wms_rec.IdRecepcionDet = pIdRecDet
            oBeLog_error_wms_rec.Item_No = pItemNo
            oBeLog_error_wms_rec.UmBas = pUMBas
            oBeLog_error_wms_rec.Variant_Code = pVariantCode
            oBeLog_error_wms_rec.Cantidad = pCantidad
            oBeLog_error_wms_rec.Referencia_Documento = pReferenciaDocumento
            oBeLog_error_wms_rec.Line_No = pNumeroLinea

            Insertar(oBeLog_error_wms_rec, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw ex
        End Try

    End Sub
End Class
