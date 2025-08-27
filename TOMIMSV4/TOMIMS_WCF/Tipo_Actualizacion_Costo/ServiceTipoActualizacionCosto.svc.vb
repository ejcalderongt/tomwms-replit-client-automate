' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceTipoActualizacionCosto" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceTipoActualizacionCosto.svc or ServiceTipoActualizacionCosto.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class ServiceTipoActualizacionCosto
    Implements IServiceTipoActualizacionCosto

    Public Function GetAll() As List(Of clsBeTipo_actualizacion_costo) Implements IServiceTipoActualizacionCosto.GetAll

        Try
            Return clsLnTipo_actualizacion_costo.GetAll()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetSingle(ByVal pIdTipoActualizacionCosto As Integer) As clsBeTipo_actualizacion_costo Implements IServiceTipoActualizacionCosto.GetSingle

        Try
            Return clsLnTipo_actualizacion_costo.GetSingle(pIdTipoActualizacionCosto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetParametro(ByVal pIdPropietario As Integer, ByVal pIdProveedor As Integer) As clsBeTipo_actualizacion_costo Implements IServiceTipoActualizacionCosto.GetParametro

        Try
            Return clsLnTipo_actualizacion_costo.GetParametro(pIdPropietario, pIdProveedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
