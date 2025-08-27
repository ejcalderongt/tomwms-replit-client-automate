' NOTE: You can use the "Rename" command on the context menu to change the class name "ProductoRellenado" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ProductoRellenado.svc or ProductoRellenado.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class PresentacionTarima
    Implements IPresentacion_Tarima

    Public Function Insertar(ByVal pObj As clsBeProducto_presentacion_tarima) As Boolean Implements IPresentacion_Tarima.Insertar

        Try
            
            pObj.IdPresentacionTarima = clsLnProducto_presentacion_tarima.MaxID()
            Return clsLnProducto_presentacion_tarima.Insertar(pObj)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar(ByVal pObj As clsBeProducto_presentacion_tarima) As Boolean Implements IPresentacion_Tarima.Actualizar

        Try
            
            Return clsLnProducto_presentacion_tarima.Actualizar(pObj)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetSingle(ByVal pIdPresentacionTarima As Integer) As clsBeProducto_presentacion_tarima Implements IPresentacion_Tarima.GetSingle

        Try

            Return clsLnProducto_presentacion_tarima.GetSingle(pIdPresentacionTarima)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByPresentacion(ByVal pActivo As Boolean) As List(Of clsBeProducto_presentacion_tarima) Implements IPresentacion_Tarima.GetAllByPresentacion

        Try

            Return clsLnProducto_presentacion_tarima.GetAllByPresentacion(pActivo).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    'Public Function GetAll() As List(Of clsBeProducto_presentacion_tarima) Implements IPresentacion_Tarima.GetAll

    '    Try

    '        'Return clsLnProducto_presentacion_tarima.GetAll().ToList
    '        Return Nothing
    '    Catch ex As FaultException
    '        Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
    '    Catch ex1 As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
    '    End Try

    'End Function


    Public Sub Desactivar(ByVal pIdPresentacionTarima As Integer) Implements IPresentacion_Tarima.Desactivar

        Try
            clsLnProducto_presentacion_tarima.Desactivar(pIdPresentacionTarima)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

End Class
