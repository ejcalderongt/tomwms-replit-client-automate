' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "RoadRuta" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RoadRuta.svc o RoadRuta.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection


Public Class RoadRuta

    Implements IRoadRuta


    Public Function Insertar(ByVal ObjP As clsBeRoad_ruta) As Integer Implements IRoadRuta.Insertar

        Try            
            Return clsLnRoad_ruta.Insertar(ObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function Actualizar(ObjP As clsBeRoad_ruta) As Integer Implements IRoadRuta.Actualizar

        Try            
            Return clsLnRoad_ruta.Actualizar(ObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function Eliminar(ByRef ObjP As clsBeRoad_ruta) As Integer Implements IRoadRuta.Eliminar

        Try
            
            ObjP.ACTIVO = False
            Return clsLnRoad_ruta.Actualizar(ObjP)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_ruta) Implements IRoadRuta.GetAllFiltro

        Try

            Dim List As New List(Of clsBeRoad_ruta)
            List = clsLnRoad_ruta.GetAllFiltro(pActivo)
            Return List.ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetSingle(pIdRuta As Integer) As clsBeRoad_ruta Implements IRoadRuta.GetSingle

        Try

            Return clsLnRoad_ruta.GetSingle(pIdRuta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function Listar(ByRef ObjP As clsBeRoad_ruta) As DataTable Implements IRoadRuta.listar

        Try
            
            Dim list As New DataTable
            Dim ObjLnPE As New clsLnRoad_ruta()
            'list = ObjLnPE.Listar()
            Return list

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function MaxID() As Integer Implements IRoadRuta.MaxID

        Try

            Return clsLnRoad_ruta.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
