' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class ServiceArancel
    Implements IServiceArancel


    Public Function Insertar(ByRef pObjUM As clsBeArancel) As Integer Implements IServiceArancel.Insertar

        Try

            Return clsLnArancel.Insertar(pObjUM)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Actualizar(ByVal pObjUM As clsBeArancel) As Integer Implements IServiceArancel.Actualizar

        Try
            Return clsLnArancel.Actualizar(pObjUM)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Eliminar(ByRef pObjUM As clsBeArancel) As Integer Implements IServiceArancel.Eliminar

        Try
            Return clsLnArancel.Actualizar(pObjUM)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function MaxID() As Integer Implements IServiceArancel.MaxID

        Try
            Return clsLnArancel.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeArancel) Implements IServiceArancel.GetAllFiltro

        Try            
            Return clsLnArancel.Get_All_Filtro(pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetAll() As List(Of clsBeArancel) Implements IServiceArancel.GetAll

        Try
            Return clsLnArancel.GetAll()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Exists(ByVal pIdArancel As Integer) As Boolean Implements IServiceArancel.Exists

        Try

            Return clsLnArancel.Exists(pIdArancel)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetSingle(ByVal pIdArancel As Integer) As clsBeArancel Implements IServiceArancel.GetSingle

        Try
            Return clsLnArancel.GetSingle(pIdArancel)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


End Class
