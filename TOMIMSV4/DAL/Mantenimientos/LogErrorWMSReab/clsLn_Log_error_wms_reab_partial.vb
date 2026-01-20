Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms_reab

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    Optional ByVal pStackTrace As String = "",
                                    Optional ByVal pIdBodega As Integer = 0,
                                    Optional ByVal pIdStock As Integer = 0,
                                    Optional ByVal pIdMovimiento As Integer = 0,
                                    Optional ByVal pLic_Plate_Anterior As String = "",
                                    Optional ByVal pLic_Plate As String = "",
                                    Optional ByVal pIdResolucion As Integer = 0,
                                    Optional ByVal pIdProductoBodega As Integer = 0,
                                    Optional ByVal pCantidad As Double = 0,
                                    Optional ByVal pUser_agr As Integer = 0,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing)

        Try
            Dim oBe As New clsBeLog_error_wms_reab()

            ' Obligatorio
            oBe.MensajeError = pMensajeExcepcion

            ' Opcionales
            oBe.RutaError = pStackTrace
            oBe.IdBodega = pIdBodega
            oBe.IdStock = pIdStock
            oBe.IdMovimiento = pIdMovimiento
            oBe.Lic_Plate_Anterior = pLic_Plate_Anterior
            oBe.Lic_Plate = pLic_Plate
            oBe.IdResolucion = pIdResolucion
            oBe.IdProductoBodega = pIdProductoBodega
            oBe.Cantidad = pCantidad
            oBe.User_agr = pUser_agr
            oBe.Fec_agr = Now ' datetime en la tabla

            Insertar(oBe, pConection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Throw
        End Try

    End Sub

End Class
