Imports System.Reflection
Imports System.ServiceModel

Public Class srvRoadDespacho
    Implements IsrvRoadDespacho

    Public Function Enviar_Despacho_A_Road(ByRef BeDespachoEnc As clsBeTrans_despacho_enc) As Boolean Implements IsrvRoadDespacho.Enviar_Despacho_A_Road

        Enviar_Despacho_A_Road = False

        Try

            Return clsLnDS_PEDIDO.Enviar_Despacho_Desde_WMS(BeDespachoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insertar_Ruta(ByRef BeRuta As clsBeRoad_ruta)

        Try

            Return clsLnRoad_ruta.Insertar(BeRuta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insertar_Vendedor(ByRef BeVendedor As clsBeRoad_p_vendedor)

        Try

            Return clsLnRoad_p_vendedor.Insertar(BeVendedor)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class