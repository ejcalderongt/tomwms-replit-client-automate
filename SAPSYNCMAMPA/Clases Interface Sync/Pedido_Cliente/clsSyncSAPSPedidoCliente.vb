Imports System.Reflection
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPSPedidoCliente : Inherits clsInterfaceBase

    Implements IDisposable

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Shared lRetCode, lErrCode As Long
    Shared sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Shared BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Shared Async Function Inserta_Cliente_SAP(ByVal pCodigo As String) As Task(Of Boolean)

        Dim clsTransaccion As New clsTransaccion

        Try
            Dim client = New SapServiceLayerClient()
            Dim bp As BusinessPartnerDto = Await client.GetBusinessPartnerAsync(pCodigo)

            If bp Is Nothing Then
                Throw New Exception("No se encontró el cliente en SAP HANA con CardCode: " & pCodigo)
            End If

            clsTransaccion.Open_Connection()

            Dim BeCliente As New clsBeCliente With {
                .IdCliente = clsLnCliente.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdPropietario = BeConfigEnc.IdPropietario,
                .Codigo = bp.CardCode,
                .Nombre_comercial = bp.CardName,
                .Sistema = True,
                .Activo = True,
                .IdEmpresa = BeConfigEnc.Idempresa,
                .Nit = bp.CardCode,
                .IdTipoCliente = 1,
                .Es_bodega_recepcion = False,
                .Es_Bodega_Traslado = False
            }

            clsLnCliente.Insertar(BeCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            Dim BeClienteBodega As New clsBeCliente_bodega With {
                .IdClienteBodega = clsLnCliente_bodega.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdCliente = BeCliente.IdCliente,
                .IdBodega = BeConfigEnc.Idbodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Cliente = BeCliente
            }

            clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                        clsTransaccion.lConnection,
                                                        clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

            Return True

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Function

End Class