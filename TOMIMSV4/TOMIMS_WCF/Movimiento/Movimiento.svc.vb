' NOTE: You can use the "Rename" command on the context menu to change the class name "Movimiento" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Movimiento.svc or Movimiento.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class Movimiento
    Implements IMovimiento

    Public Function GetMovimiento(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable Implements IMovimiento.GetMovimiento

        Try
            Return clsLnTrans_movimientos.GetMovimientos(pIdBodegaOrigen, pFechaDel, pFechaAl)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
