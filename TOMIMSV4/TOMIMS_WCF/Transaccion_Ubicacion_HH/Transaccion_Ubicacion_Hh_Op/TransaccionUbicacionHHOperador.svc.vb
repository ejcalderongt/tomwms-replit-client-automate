' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "TransaccionUbicacionHHOperador" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TransaccionUbicacionHHOperador.svc o TransaccionUbicacionHHOperador.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class TransaccionUbicacionHHOperador
    Implements ITransaccionUbicacionHHOperador

    Public Function GetAllByTransUbicOp(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_op) Implements ITransaccionUbicacionHHOperador.GetAllByTransUbicOp
        Try
            Return clsLnTrans_ubic_hh_op.Get_All_By_IdTareaUbicacion(pIdTransUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function Insertar(ByRef pObjTranUbicHhOp As clsBeTrans_ubic_hh_op) As Integer Implements ITransaccionUbicacionHHOperador.Insertar
        Try           
            Return clsLnTrans_ubic_hh_op.Insertar(pObjTranUbicHhOp)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Actualizar(ByVal pObjTranUbicHhOp As clsBeTrans_ubic_hh_op) As Integer Implements ITransaccionUbicacionHHOperador.Actualizar
        Try
            Dim ObjLnTransUbicHhOp As New clsLnTrans_ubic_hh_op
            Return ObjLnTransUbicHhOp.Actualizar(pObjTranUbicHhOp)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function Eliminar(ByRef pObjTranUbicHhOp As clsBeTrans_ubic_hh_op) As Integer Implements ITransaccionUbicacionHHOperador.Eliminar
        Try
            Dim ObjLnTransUbicHhOp As New clsLnTrans_ubic_hh_op
            Return ObjLnTransUbicHhOp.Actualizar(pObjTranUbicHhOp)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Sub DeleteOp(ByVal pIdTransUbicOp As Integer, ByVal pIdTransUbicHhEnc As Integer) Implements ITransaccionUbicacionHHOperador.DeleteOp

        Try
            clsLnTrans_re_op.Delete(pIdTransUbicOp, pIdTransUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

End Class
