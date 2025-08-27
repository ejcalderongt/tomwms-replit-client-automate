' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "MotivoUbicacion" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MotivoUbicacion.svc o MotivoUbicacion.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class MotivoUbicacion
    Implements IMotivoUbicacion
    Dim objLn As New clsLnMotivo_ubicacion

    Public Function Insertar(ByRef pObjMotivoUbicacion As clsBeMotivo_ubicacion) As Integer Implements IMotivoUbicacion.Insertar
        Try            
            Return clsLnMotivo_ubicacion.Insertar(pObjMotivoUbicacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Actualizar(ByVal pObjMotivoUbicacion As clsBeMotivo_ubicacion) As Integer Implements IMotivoUbicacion.Actualizar
        Try            
            Return clsLnMotivo_ubicacion.Actualizar(pObjMotivoUbicacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function Eliminar(ByRef pObjMotivoUbicacion As clsBeMotivo_ubicacion) As Integer Implements IMotivoUbicacion.Eliminar
        Try            
            Return clsLnMotivo_ubicacion.Actualizar(pObjMotivoUbicacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Function MaxID() As Integer Implements IMotivoUbicacion.MaxID
        Try
            Return clsLnMotivo_ubicacion.MaxID
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetSingle(ByVal pIdMotivoUbicacion As Integer) As clsBeMotivo_ubicacion Implements IMotivoUbicacion.GetSingle
        Try
            Return clsLnMotivo_ubicacion.GetSingle(pIdMotivoUbicacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeMotivo_ubicacion) Implements IMotivoUbicacion.GetAll
        Try
            Return clsLnMotivo_ubicacion.GetAll(pActivo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
