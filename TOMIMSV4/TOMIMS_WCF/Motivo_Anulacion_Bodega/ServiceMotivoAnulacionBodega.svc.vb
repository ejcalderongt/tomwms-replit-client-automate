Imports System.Reflection

Public Class ServiceMotivoAnulacionBodega
    Implements IServiceMotivoAnulacionBodega

    Public Function ActualizarDatos(ByVal pObjMA As clsBeMotivo_anulacion, ByVal pListObjMAB As List(Of clsBeMotivo_anulacion_bodega)) As Boolean Implements IServiceMotivoAnulacionBodega.ActualizarDatos

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Dim ObjMA As New clsLnMotivo_anulacion()
        Dim ObjMAB As New clsLnMotivo_anulacion_bodega()

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            ObjMA.Actualizar(pObjMA, lConnection, lTransaction)

            Dim lMax As Integer = clsLnMotivo_anulacion_bodega.MaxID()

            For Each Obj As clsBeMotivo_anulacion_bodega In pListObjMAB

                If Obj.IdMotivoAnulacionBodega = 0 Then
                    lMax += 1
                    Obj.IdMotivoAnulacionBodega = lMax
                    ObjMAB.Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjMAB.Actualizar(Obj, lConnection, lTransaction)
                End If

            Next

            lTransaction.Commit()
            lConnection.Close()

            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetAllByMotivoAnulacion(ByVal pIdMotivoAnulacion As Integer) As List(Of clsBeMotivo_anulacion_bodega) Implements IServiceMotivoAnulacionBodega.GetAllByMotivoAnulacion

        Try
            Dim List As New List(Of clsBeMotivo_anulacion_bodega)
            List = clsLnMotivo_anulacion_bodega.GetAllByMotivoAnulacion(pIdMotivoAnulacion)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub GetIdMotivoAnulacionBodega(ByRef BeMotivoAnulacionBodega As clsBeMotivo_anulacion_bodega) Implements IServiceMotivoAnulacionBodega.GetIdMotivoAnulacionBodega

        Try

            clsLnMotivo_anulacion_bodega.GetIdMotivoAnulacionBodega(BeMotivoAnulacionBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

End Class