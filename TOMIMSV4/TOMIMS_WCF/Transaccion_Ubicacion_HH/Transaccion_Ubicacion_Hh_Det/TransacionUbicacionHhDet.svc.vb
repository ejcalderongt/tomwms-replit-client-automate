' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "TransacionUbicacionHhDet" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TransacionUbicacionHhDet.svc o TransacionUbicacionHhDet.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class TransacionUbicacionHhDet
    Implements ITransacionUbicacionHhDet

    Public Function Insertar(ByRef pObjTranUbicHhDet As clsBeTrans_ubic_hh_det) As Integer Implements ITransacionUbicacionHhDet.Insertar
        Try            
            Return clsLnTrans_ubic_hh_det.Insertar(pObjTranUbicHhDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Actualizar(ByVal pObjTranUbicHhDet As clsBeTrans_ubic_hh_det) As Integer Implements ITransacionUbicacionHhDet.Actualizar
        Try
            Return clsLnTrans_ubic_hh_det.Actualizar(pObjTranUbicHhDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Eliminar(ByRef pObjTranUbicHhDet As clsBeTrans_ubic_hh_det) As Integer Implements ITransacionUbicacionHhDet.Eliminar
        Try            
            pObjTranUbicHhDet.Activo = False
            Return clsLnTrans_ubic_hh_det.Actualizar(pObjTranUbicHhDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Function MaxID() As Integer Implements ITransacionUbicacionHhDet.MaxID
        Try
            Return clsLnTrans_ubic_hh_det.MaxID
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function




    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pIdTranUbicHhDet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSingle(ByVal pIdTranUbicHhDet As Integer) As clsBeTrans_ubic_hh_det Implements ITransacionUbicacionHhDet.GetSingle
        Try
            Return clsLnTrans_ubic_hh_det.GetSingle(pIdTranUbicHhDet)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function


    Public Function GetAllByTransUbicEnc(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_det) Implements ITransacionUbicacionHhDet.GetAllByTransUbicEnc
        Try
            Return clsLnTrans_ubic_hh_det.Get_All_By_Id_Trans_Ubic_Enc(pIdTransUbicHhEnc)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function


End Class
