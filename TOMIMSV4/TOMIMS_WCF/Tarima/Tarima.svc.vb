' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Tarima" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Tarima.svc o Tarima.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class Tarima
    Implements ITarima

    Public Function ActualizarEstado(ByVal pObtT As clsBeTarimas) As Integer Implements ITarima.ActualizarEstado

        ActualizarEstado = 0

        Try            

            Return clsLnTarimas.Actualizar(pObtT)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
