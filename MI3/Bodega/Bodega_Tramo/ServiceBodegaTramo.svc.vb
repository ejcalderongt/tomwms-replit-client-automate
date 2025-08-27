' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceBodegaTramo" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceBodegaTramo.svc or ServiceBodegaTramo.svc.vb at the Solution Explorer and start debugging.

Imports System.Reflection
Imports System.ServiceModel

Public Class ServiceBodegaTramo
    Implements IServiceBodegaTramo

    Public Function GetSingle(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As clsBeBodega_tramo Implements IServiceBodegaTramo.GetSingle

        GetSingle = Nothing

        Try

            Return clsLnBodega_tramo.GetSingle(pIdTramo, pIdBodega)

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetSingle: " & ex.Message)
        End Try

    End Function

    Public Function Insert(ByRef oBeBodega_tramo As clsBeBodega_tramo) As Integer Implements IServiceBodegaTramo.Insert

        Try

            Return clsLnBodega_tramo.Insertar(oBeBodega_tramo)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function Update_Single(ByVal oBeBodega_tramo As clsBeBodega_tramo) As Integer _
                                  Implements IServiceBodegaTramo.Update_Single

        Update_Single = 0

        Try

            Return clsLnBodega_tramo.Actualizar(oBeBodega_tramo)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function Get_All() As List(Of clsBeBodega_tramo) Implements IServiceBodegaTramo.Get_All

        Try

            Dim List As New List(Of clsBeBodega_tramo)
            List = clsLnBodega_tramo.GetAll()

            Return List.ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
