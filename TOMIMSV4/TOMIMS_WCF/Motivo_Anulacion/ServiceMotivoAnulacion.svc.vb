Imports System.Reflection

Public Class ServiceMotivoAnulacion
    Implements IServiceMotivoAnulacion

    Public Function Insertar(ByRef pObjMA As clsBeMotivo_anulacion) As Integer Implements IServiceMotivoAnulacion.Insertar

        Try            
            Return clsLnMotivo_anulacion.Insertar(pObjMA)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub GuardarTransaccion(ByVal pListObjMA As List(Of clsBeMotivo_anulacion)) Implements IServiceMotivoAnulacion.GuardarTransaccion

        Dim ObjMA As New clsLnMotivo_anulacion()

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            For Each Obj As clsBeMotivo_anulacion In pListObjMA
                If Obj.IsNew Then
                    clsLnMotivo_anulacion.Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjMA.Actualizar(Obj, lConnection, lTransaction)
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

    Public Function Actualizar(ByVal pObjMA As clsBeMotivo_anulacion) As Integer Implements IServiceMotivoAnulacion.Actualizar

        Try
            Dim ObjLnPE As New clsLnMotivo_anulacion()
            Return ObjLnPE.Actualizar(pObjMA)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub Delete(ByVal pIdEmpresa As Integer) Implements IServiceMotivoAnulacion.Delete

        Try
            clsLnMotivo_anulacion.Delete(pIdEmpresa)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Function Exists(ByVal pIdMotivoAnulacion As Integer) As Boolean Implements IServiceMotivoAnulacion.Exists

        Try
            Return clsLnMotivo_anulacion.Exists(pIdMotivoAnulacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Eliminar(ByRef pObjMA As clsBeMotivo_anulacion) As Integer Implements IServiceMotivoAnulacion.Eliminar

        Try
            Dim ObjLnPE As New clsLnMotivo_anulacion()
            pObjMA.Activo = False
            Return ObjLnPE.Actualizar(pObjMA)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIdMotivoDevolucion() As Integer Implements IServiceMotivoAnulacion.MAXIdMotivoAnulacion

        Try
            Return clsLnMotivo_anulacion.MaxIdMotivoAnulacion()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_anulacion) Implements IServiceMotivoAnulacion.GetAllFiltro

        Try
            Return clsLnMotivo_anulacion.GetAllFiltro(pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetAllByBodega(ByVal pIdBodega As Integer) As List(Of clsBeMotivo_anulacion) Implements IServiceMotivoAnulacion.GetAllByBodega

        Try
            Return clsLnMotivo_anulacion.Get_All_By_IdBodega(pIdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetSingle(ByVal pIdMotivoAnulacion As Integer) As clsBeMotivo_anulacion Implements IServiceMotivoAnulacion.GetSingle

        Try
            Return clsLnMotivo_anulacion.GetSingle(pIdMotivoAnulacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
