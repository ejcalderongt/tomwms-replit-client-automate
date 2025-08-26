' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

Public Class ServiceMotivoDevolucionBodega
    Implements IServiceMotivoDevolucionBodega

    Public Function ActualizarDatos(ByVal pObjMD As clsBeMotivo_devolucion, ByVal pListObjMDB As List(Of clsBeMotivo_devolucion_bodega)) As Boolean Implements IServiceMotivoDevolucionBodega.ActualizaDatos

        ActualizarDatos = False

        Try

            clsLnMotivo_devolucion_bodega.ActualizarDatos(pObjMD, pListObjMDB)
        
            ActualizarDatos = True
               
        Catch ex As Exception          
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetAllByMotivoDevolucion(ByVal pIdMotivoDevolucion As Integer) As List(Of clsBeMotivo_devolucion_bodega) Implements IServiceMotivoDevolucionBodega.GetAllByMotivoDevolucion

        Try
            Dim List As New List(Of clsBeMotivo_devolucion_bodega)
            List = clsLnMotivo_devolucion_bodega.GetAllByMotivoDevolucion(pIdMotivoDevolucion)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
