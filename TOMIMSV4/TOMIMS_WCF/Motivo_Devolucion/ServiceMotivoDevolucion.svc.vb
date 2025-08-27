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

Public Class ServiceMotivoDevolucion
    Implements IServiceMotivoDevolucion

    Public Function Insertar(ByRef pObjMD As clsBeMotivo_devolucion) As Integer Implements IServiceMotivoDevolucion.Insertar

        Try

            Return clsLnMotivo_devolucion.Insertar(pObjMD)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub GuardarTransaccion(ByVal pListObjMD As List(Of clsBeMotivo_devolucion)) Implements IServiceMotivoDevolucion.GuardarTransaccion
       

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            For Each Obj As clsBeMotivo_devolucion In pListObjMD
                If Obj.IsNew Then
                    clsLnMotivo_devolucion.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnMotivo_devolucion.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw New FaultException(ex.Message)
        Finally
            lConnection.Close()
        End Try

    End Sub

    Public Function Actualizar(ByVal pObjMD As clsBeMotivo_devolucion) As Integer Implements IServiceMotivoDevolucion.Actualizar

        Try            
            Return clsLnMotivo_devolucion.Actualizar(pObjMD)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Eliminar(ByRef pObjMD As clsBeMotivo_devolucion) As Integer Implements IServiceMotivoDevolucion.Eliminar

        Try            
            pObjMD.Activo = False
            Return clsLnMotivo_devolucion.Actualizar(pObjMD)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Sub Delete(ByVal pIdEmpresa As Integer, ByVal pIdPropietario As Integer) Implements IServiceMotivoDevolucion.Delete

        Try
            clsLnMotivo_devolucion.Delete(pIdEmpresa, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Private Function Exists(ByVal pIdMotivoDevolucion As Integer) As Boolean Implements IServiceMotivoDevolucion.Exists

        Try
            Return clsLnMotivo_devolucion.Exists(pIdMotivoDevolucion)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function MAXID() As Integer Implements IServiceMotivoDevolucion.MAXID

        Try
            Return clsLnMotivo_devolucion.MAXID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_devolucion) Implements IServiceMotivoDevolucion.GetAllFiltro

        Try
            Return clsLnMotivo_devolucion.GetAllFiltro(pActivo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByPropietarioBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion) Implements IServiceMotivoDevolucion.GetAllByPropietarioBodega

        Try
            Return clsLnMotivo_devolucion.GetAllByPropietarioBodega(pIdPropietario, pIdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Function GetAllByPropietarioBodegaDetalle(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion) Implements IServiceMotivoDevolucion.GetAllByPropietarioBodegaDetalle

        Try
            Return clsLnMotivo_devolucion.GetAllByPropietarioBodegaDetalle(pIdPropietario, pIdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetSingle(ByVal pIdMotivoDevolucion As Integer) As clsBeMotivo_devolucion Implements IServiceMotivoDevolucion.GetSingle

        Try
            Return clsLnMotivo_devolucion.GetSingle(pIdMotivoDevolucion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
