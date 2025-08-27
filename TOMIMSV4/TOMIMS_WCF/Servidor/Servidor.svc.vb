' NOTE: You can use the "Rename" command on the context menu to change the class name "Servidor" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Servidor.svc or Servidor.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class Servidor
    Implements IServidor

    Public Function FechaHoraServidor() As String Implements IServidor.FechaHoraServidor

        Try
            Return clsServidor.FechaHoraServidor
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function FechaServidor() As String Implements IServidor.FechaServidor

        Try
            Return clsServidor.Get_Fecha_Servidor
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function HoraServidor() As String Implements IServidor.HoraServidor

        Try
            Return clsServidor.HoraServivdor
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
