' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "TransaccionUbicacionHH" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TransaccionUbicacionHH.svc o TransaccionUbicacionHH.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class TransaccionUbicacionHH
    Implements ITransaccionUbicacionHH

    Public Function Insertar(ByRef pObjTranUbicHhEnc As clsBeTrans_ubic_hh_enc) As Integer Implements ITransaccionUbicacionHH.Insertar
        Try            
            Return clsLnTrans_ubic_hh_enc.Insertar(pObjTranUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Actualizar(ByVal pObjTranUbicHhEnc As clsBeTrans_ubic_hh_enc) As Integer Implements ITransaccionUbicacionHH.Actualizar
        Try            
            Return clsLnTrans_ubic_hh_enc.Actualizar(pObjTranUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function Eliminar(ByRef pObjTranUbicHhEnc As clsBeTrans_ubic_hh_enc) As Integer Implements ITransaccionUbicacionHH.Eliminar
        Try            
            pObjTranUbicHhEnc.Activo = False
            Return clsLnTrans_ubic_hh_enc.Actualizar(pObjTranUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Function MaxID() As Integer Implements ITransaccionUbicacionHH.MaxID
        Try
            Return clsLnTrans_ubic_hh_enc.MaxID
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllFiltro(ByVal pActivo As Boolean, ByVal pFechaInicio As DateTime, ByVal pFechaFin As DateTime) As List(Of clsBeTrans_ubic_hh_enc) Implements ITransaccionUbicacionHH.GetAllFiltro
        Try
            Return clsLnTrans_ubic_hh_enc.Get_All_Filtro(pActivo, pFechaInicio, pFechaFin).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetAll() As List(Of clsBeTrans_ubic_hh_enc) Implements ITransaccionUbicacionHH.GetAll
        Try
            Return clsLnTrans_ubic_hh_enc.GetAll()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Exists(ByVal pIdTranUbicHhEnc As Integer) As Boolean Implements ITransaccionUbicacionHH.Exists
        Try
            'get all by mi ass
            Return clsLnTrans_ubic_hh_enc.Exists(pIdTranUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetSingle(ByVal pIdTranUbicHhEnc As Integer) As clsBeTrans_ubic_hh_enc Implements ITransaccionUbicacionHH.GetSingle
        Try
            Return clsLnTrans_ubic_hh_enc.GetSingle(pIdTranUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Sub GuardarTransaccion(ByVal pObjEnc As clsBeTrans_ubic_hh_enc,
                ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                ByVal pListObjMov As List(Of clsBeTrans_movimientos),
                ByVal conHh As Boolean,
                ByVal pIdPropietario As Integer,
                ByVal pListObjStock As List(Of clsBeStock),
                ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                ByVal IdTareaHH As Integer) Implements ITransaccionUbicacionHH.GuardarTransaccion

        Try

            clsLnTrans_ubic_hh_enc.Guardar_Transaccion(pObjEnc, pListObjDet, pListObjOp, pListObjMov, conHh, pIdPropietario, pListObjStock, pListObjTransUbicTarimaDisponibles, pListObjTransUbicTarimasUsadas, IdTareaHH)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Function GetAllByTransUbicEnc(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_det) Implements ITransaccionUbicacionHH.GetAllByTransUbicEnc
        Try
            Return clsLnTrans_ubic_hh_det.Get_All_By_Id_Trans_Ubic_Enc(pIdTransUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByTransUbicOp(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_op) Implements ITransaccionUbicacionHH.GetAllByTransUbicOp
        Try
            Return clsLnTrans_ubic_hh_op.Get_All_By_IdTareaUbicacion(pIdTransUbicHhEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetSingleStock(ByVal IdStock As Integer) As clsBeStock Implements ITransaccionUbicacionHH.GetSingleStock
        Try
            Return clsLnStock.GetSingle(IdStock)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function ActualizarStock(ByVal ObjS As clsBeStock) As Integer Implements ITransaccionUbicacionHH.ActualizarStock
        Try            
            Return clsLnStock.Actualizar(ObjS)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    Public Function GetDimensionesProductos(ByRef pIdUbicacion As Integer) As List(Of clsBeVW_stock_res) Implements ITransaccionUbicacionHH.GetDimensionesProductos
        Try
            Return clsLnTrans_ubic_hh_enc.Get_Dimensiones_Productos(pIdUbicacion).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
