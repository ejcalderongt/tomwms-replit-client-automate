' NOTE: You can use the "Rename" command on the context menu to change the class name "TransaccionLog" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select TransaccionLog.svc or TransaccionLog.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class TransaccionLog
    Implements ITransaccion_Log

    ''' <summary>
    ''' Creado por Ricardo Garía
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll() As List(Of clsBeTransacciones_log) Implements ITransaccion_Log.GetAll

        Try
            Return clsLnTransacciones_log.GetAll()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjListT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Posponer(ByVal pObjListT As List(Of clsBeTransacciones_log)) As Boolean Implements ITransaccion_Log.Posponer

        Try

            Return clsLnTransacciones_log.Posponer(pObjListT)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjListT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EnviarTarea(ByVal pObjListT As List(Of clsBeTransacciones_log), ByVal pHost As String) As Boolean Implements ITransaccion_Log.EnviarTarea

        Try

            Return clsLnTransacciones_log.EnviarTarea(pObjListT, pHost)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class
