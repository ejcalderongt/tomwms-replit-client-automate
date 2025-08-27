' NOTE: You can use the "Rename" command on the context menu to change the class name "ServicePais_Region" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServicePais_Region.svc or ServicePais_Region.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServicePaisRegion
    Implements IServicePaisRegion

    Public Function Insert(ByRef BePaisRegion As clsBePais_region) As Integer Implements IServicePaisRegion.Insert

        Try
            Return clsLnPais_region.Insertar(BePaisRegion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BePaisRegion As clsBePais_region) As Integer Implements IServicePaisRegion.Update

        Try

            Return clsLnPais_region.Actualizar(BePaisRegion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef BePaisRegion As clsBePais_region) As Integer Implements IServicePaisRegion.Delete

        Delete = 0

        Try

            Return clsLnPais_region.Eliminar(BePaisRegion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllRegion() As List(Of clsBePais_region) Implements IServicePaisRegion.Get_AllRegion

        Try

            Return clsLnPais_region.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdPaisRegion() As Integer Implements IServicePaisRegion.Max_IdPaisRegion

        Try

            Return clsLnPais_region.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exist(ByVal pIdRegion As Integer) As Boolean Implements IServicePaisRegion.Exist

        Try

            Return clsLnPais_region.Exists(pIdRegion)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
