' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "RoadVendedor" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RoadVendedor.svc o RoadVendedor.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class RoadVendedor
    Implements IRoadVendedor

    Public Function Insertar(ByRef ObjP As clsBeRoad_p_vendedor) As Integer Implements IRoadVendedor.Insertar
        Try            
            Return clsLnRoad_p_vendedor.Insertar(ObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Actualizar(ObjP As clsBeRoad_p_vendedor) As Integer Implements IRoadVendedor.Actualizar
        Try            
            Return clsLnRoad_p_vendedor.Actualizar(ObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Eliminar(ByRef ObjP As clsBeRoad_p_vendedor) As Integer Implements IRoadVendedor.Eliminar
        Try            
            ObjP.Bloqueado = False
            Return clsLnRoad_p_vendedor.Actualizar(ObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function



    Public Function listar(ByRef ObjP As clsBeRoad_p_vendedor) As DataTable Implements IRoadVendedor.listar
        Try
            'Dim List As New List(Of clsBeRoad_p_vendedor)
            Dim list As New DataTable
            Dim ObjLnPE As New clsLnRoad_p_vendedor()
            'list = ObjLnPE.Listar()
            Return list
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_p_vendedor) Implements IRoadVendedor.GetAllFiltro
        Try
            Dim List As New List(Of clsBeRoad_p_vendedor)
            List = clsLnRoad_p_vendedor.GetAllFiltro(pActivo)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetSingle(pIdVendedor As Integer) As clsBeRoad_p_vendedor Implements IRoadVendedor.GetSingle
        Try
            Return clsLnRoad_p_vendedor.GetSingle(pIdVendedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function MaxIdVendedor() As Integer Implements IRoadVendedor.MaxIdVendedor
        Try
            Return clsLnRoad_p_vendedor.MaxID
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
