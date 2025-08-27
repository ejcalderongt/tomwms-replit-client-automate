' NOTE: You can use the "Rename" command on the context menu to change the class name "ServicePais" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServicePais.svc or ServicePais.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServicePais
    Implements IServicePais

    Public Function Insert(ByRef BePais As clsBePaises) As Integer Implements IServicePais.Insert

        Try
            Return clsLnPaises.Insertar(BePais)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByVal BePais As clsBePaises) As Integer Implements IServicePais.Update

        Try

            Return clsLnPaises.Actualizar(BePais)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByVal pIdPais As Integer) As Integer Implements IServicePais.Delete

        Delete = 0

        Try

            Return clsLnPaises.Eliminar(pIdPais)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All_Filtro() As List(Of clsBePaises) Implements IServicePais.Get_All_Filtro

        Try

            Return clsLnPaises.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdPais() As Integer Implements IServicePais.Max_IdPais

        Try

            Return clsLnPaises.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exist(ByVal pIdPais As Integer) As Boolean Implements IServicePais.Exist

        Try

            Return clsLnPaises.Exists(pIdPais)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
