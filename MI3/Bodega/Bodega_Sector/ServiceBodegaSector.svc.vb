' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceBodegaSector" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceBodegaSector.svc or ServiceBodegaSector.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServiceBodegaSector
    Implements IServiceBodegaSector
    Public Function GetSingle(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As clsBeBodega_sector Implements IServiceBodegaSector.GetSingle

        GetSingle = Nothing

        Try

            Return clsLnBodega_sector.GetSingle(pIdTramo, pIdBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insert(ByRef oBeBodega_tramo As clsBeBodega_sector) As Integer Implements IServiceBodegaSector.Insert

        Try

            Return clsLnBodega_sector.Insertar(oBeBodega_tramo)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update_Single(ByVal oBeBodega_tramo As clsBeBodega_sector) As Integer _
                                  Implements IServiceBodegaSector.Update_Single

        Update_Single = 0

        Try

            Return clsLnBodega_sector.Actualizar(oBeBodega_tramo)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All() As List(Of clsBeBodega_sector) Implements IServiceBodegaSector.Get_All

        Try

            Dim List As New List(Of clsBeBodega_sector)
            List = clsLnBodega_sector.GetAll()

            Return List.ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
