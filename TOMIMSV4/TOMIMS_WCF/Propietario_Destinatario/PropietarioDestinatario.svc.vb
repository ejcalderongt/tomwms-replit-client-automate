' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "PropietarioDestinatario" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione PropietarioDestinatario.svc o PropietarioDestinatario.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class PropietarioDestinatario
    Implements IPropietarioDestinatario

    Public Function Insertar(ByRef pObjDestinatario As clsBePropietario_destinatario) As Integer Implements IPropietarioDestinatario.Insertar

        Try

            Dim ObjLnDestinatario As New clsLnPropietario_destinatario
            Return ObjLnDestinatario.Insertar(pObjDestinatario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar(ByVal pObjDestinatario As clsBePropietario_destinatario) As Integer Implements IPropietarioDestinatario.Actualizar

        Try
            Dim ObjLnDestinatario As New clsLnPropietario_destinatario
            Return ObjLnDestinatario.Actualizar(pObjDestinatario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Eliminar(ByRef pObjDestinatario As clsBePropietario_destinatario) As Integer Implements IPropietarioDestinatario.Eliminar

        Try
            Dim ObjLnDestinatario As New clsLnPropietario_destinatario
            pObjDestinatario.Activo = False
            Return ObjLnDestinatario.Actualizar(pObjDestinatario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllMail(ByVal pListObjRegla As List(Of Integer)) As List(Of clsBePropietario_destinatario) Implements IPropietarioDestinatario.GetAllMail

        Try

            Return clsLnPropietario_destinatario.GetAllMail(pListObjRegla).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function MaxID() As Integer Implements IPropietarioDestinatario.MaxID

        Try
            Return clsLnPropietario_destinatario.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
