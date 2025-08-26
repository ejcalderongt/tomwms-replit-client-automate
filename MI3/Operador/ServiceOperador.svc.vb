' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceOperador" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceOperador.svc or ServiceOperador.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServiceOperador
    Implements IServiceOperador

    Public Function Insert(ByRef BeOperador As clsBeOperador) As Integer Implements IServiceOperador.Insert

        Try
            Return clsLnOperador.Insertar(BeOperador)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BeOperador As clsBeOperador) As Integer Implements IServiceOperador.Update

        Try

            Return clsLnOperador.Actualizar(BeOperador)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef BeOperador As clsBeOperador) As Integer Implements IServiceOperador.Delete

        Delete = 0

        Try

            Return clsLnOperador.Eliminar(BeOperador)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllOperador() As List(Of clsBeOperador) Implements IServiceOperador.Get_AllOperador

        Try

            Return clsLnOperador.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdOperador() As Integer Implements IServiceOperador.Max_IdOperador

        Try

            Return clsLnOperador.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exist(ByVal pIdOperador As Integer) As Boolean Implements IServiceOperador.Exist

        Try

            Return clsLnOperador.Exists(pIdOperador)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
