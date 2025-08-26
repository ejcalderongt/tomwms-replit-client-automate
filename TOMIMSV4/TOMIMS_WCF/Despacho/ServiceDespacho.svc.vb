' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "ServiceDespacho" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceDespacho.svc o ServiceDespacho.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class ServiceDespacho
    Implements IServiceDespacho


    'Public Function Guardar(ByVal pObjE As clsBeTrans_despacho_enc, ByVal pListObj As List(Of clsBeTrans_despacho_det),
    '                        ByVal pIdEmpresa As Integer, ByVal pIdBodega As Integer, ByVal pIdUsuario As Integer) As Boolean Implements IServiceDespacho.Guardar

    '    Try

    '        Return clsLnTrans_despacho_enc.Guardar(pObjE, pListObj, pIdEmpresa, pIdBodega, pIdUsuario)

    '    Catch ex As Exception
    '       Throw New FaultException(ex.Message)
    '    End Try

    'End Function


    Public Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable Implements IServiceDespacho.GetAll

        Try
            Return clsLnTrans_despacho_enc.GetAll(pActivo, pFechaDel, pFechaAl)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetSingle(ByVal pIdDespachoEnc As Integer) As clsBeTrans_despacho_enc Implements IServiceDespacho.GetSingle

        Try
            Return clsLnTrans_despacho_enc.GetSingle(pIdDespachoEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Anula(ByVal pIdDespachoEnc As Integer) Implements IServiceDespacho.Anula

        Try

            Return clsLnTrans_despacho_enc.Anular_Despacho(pIdDespachoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
