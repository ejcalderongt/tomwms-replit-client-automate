' NOTE: You can use the "Rename" command on the context menu to change the class name "ServicePaisMunicipio" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServicePaisMunicipio.svc or ServicePaisMunicipio.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServicePaisMunicipio
    Implements IServicePaisMunicipio

    Public Function Insert(ByRef BePaisMuni As clsBePais_municipio) As Integer Implements IServicePaisMunicipio.Insert

        Try
            Return clsLnPais_municipio.Insertar(BePaisMuni)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BePaisMuni As clsBePais_municipio) As Integer Implements IServicePaisMunicipio.Update

        Try

            Return clsLnPais_municipio.Actualizar(BePaisMuni)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef BePaisMuni As clsBePais_municipio) As Integer Implements IServicePaisMunicipio.Delete

        Delete = 0

        Try

            Return clsLnPais_municipio.Eliminar(BePaisMuni)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllMunicipio() As List(Of clsBePais_municipio) Implements IServicePaisMunicipio.Get_AllMunicipio

        Try

            Return clsLnPais_municipio.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdPaisMunicipio() As Integer Implements IServicePaisMunicipio.Max_IdPaisMunicipio

        Try

            Return clsLnPais_municipio.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exist(ByVal pIdMuni As Integer) As Boolean Implements IServicePaisMunicipio.Exist

        Try

            Return clsLnPais_municipio.Exists(pIdMuni)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
