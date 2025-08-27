' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Transacion_Ubicacion_Tarima" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Transacion_Ubicacion_Tarima.svc o Transacion_Ubicacion_Tarima.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class Transacion_Ubicacion_Tarima
    Implements ITransacion_Ubicacion_Tarima

    'Public Function MaxID(ByVal pConnection As System.Data.SqlClient.SqlConnection, ByVal pTransaction As System.Data.SqlClient.SqlTransaction) As Integer Implements ITransacion_Ubicacion_Tarima.MaxID

    '    Try
    '        Return clsLnTrans_ubic_tarima.MaxID(pConnection, pTransaction)
    '    Catch ex As FaultException
    '        Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
    '    Catch ex1 As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
    '    End Try

    'End Function

    Public Function MaxID() As Integer Implements ITransacion_Ubicacion_Tarima.MaxID

        Try
            Return clsLnTrans_ubic_tarima.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetAllByIdEnc(ByVal pIdTarimaTareaEnc As Integer) As List(Of clsBeTrans_ubic_tarima) Implements ITransacion_Ubicacion_Tarima.GetAllByIdEnc

        Try
            'get all by mi huevo
            Return clsLnTrans_ubic_tarima.GetAllByIdEnc(pIdTarimaTareaEnc).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
