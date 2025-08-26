Imports System.Reflection

Public Class ServiceOrdenCompraTI
    Implements IServiceOrdenCompraTI

    Public Function Insertar(ByRef pObj As clsBeTrans_oc_ti) As Integer Implements IServiceOrdenCompraTI.Insertar

        Try
            Dim ObjLnPE As New clsLnTrans_oc_ti
            Return ObjLnPE.Insertar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar(ByVal pObj As clsBeTrans_oc_ti) As Integer Implements IServiceOrdenCompraTI.Actualizar

        Try
            Dim ObjLnPE As New clsLnTrans_oc_ti
            Return ObjLnPE.Actualizar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Eliminar(ByRef pObj As clsBeTrans_oc_ti) As Integer Implements IServiceOrdenCompraTI.Eliminar

        Try
            Dim ObjLnPE As New clsLnTrans_oc_ti()
            pObj.Activo = False
            Return ObjLnPE.Actualizar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxID() As Integer Implements IServiceOrdenCompraTI.MaxID

        Try
            Return clsLnTrans_oc_ti.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeTrans_oc_ti) Implements IServiceOrdenCompraTI.GetAllFiltro

        Try
            Return clsLnTrans_oc_ti.Get_All_Filtro(pActivo).ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetSingle(ByVal pIdTipoIngresoOC As Integer) As clsBeTrans_oc_ti Implements IServiceOrdenCompraTI.GetSingle

        Try
            Return clsLnTrans_oc_ti.GetSingle(pIdTipoIngresoOC)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
