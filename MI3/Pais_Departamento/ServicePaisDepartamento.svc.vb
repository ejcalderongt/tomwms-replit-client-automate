' NOTE: You can use the "Rename" command on the context menu to change the class name "ServicePaisDepartamento" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServicePaisDepartamento.svc or ServicePaisDepartamento.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServicePaisDepartamento
    Implements IServicePaisDepartamento

    Public Function Insert(ByRef BePaisDep As clsBePais_departamento) As Integer Implements IServicePaisDepartamento.Insert

        Try
            Return clsLnPais_departamento.Insertar(BePaisDep)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BePaisRegion As clsBePais_departamento) As Integer Implements IServicePaisDepartamento.Update

        Try

            Return clsLnPais_departamento.Actualizar(BePaisRegion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef BePaisRegion As clsBePais_departamento) As Integer Implements IServicePaisDepartamento.Delete

        Delete = 0

        Try

            Return clsLnPais_departamento.Eliminar(BePaisRegion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllDepartamento() As List(Of clsBePais_departamento) Implements IServicePaisDepartamento.Get_AllDepartamento

        Try

            Return clsLnPais_departamento.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdPaisDepartamento() As Integer Implements IServicePaisDepartamento.Max_IdPaisDepartamento

        Try

            Return clsLnPais_departamento.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exist(ByVal pIdDept As Integer) As Boolean Implements IServicePaisDepartamento.Exist

        Try

            Return clsLnPais_departamento.Exists(pIdDept)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
