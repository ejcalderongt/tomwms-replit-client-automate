' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection

Public Class ServiceRecepcion
    Implements IServiceRecepcion

    Public Function Guardar(ByVal pObjTareaHH As clsBeTarea_hh,
                            ByVal pObjR As clsBeTrans_re_enc, ByVal ObjROC As clsBeTrans_re_oc,
                            ByVal pListObjRD As List(Of clsBeTrans_re_det),
                            ByVal pListObjRDP As List(Of clsBeTrans_re_det_parametros),
                            ByVal pListObjROP As List(Of clsBeTrans_re_op),
                            ByVal pListObjF As List(Of clsBeTrans_re_fact),
                            ByVal pListObjRI As List(Of clsBeTrans_re_img),
                            ByVal pListObjSE As List(Of clsBeStock_se_rec),
                            ByVal pListObjS As List(Of List(Of clsBeStock_rec)),
                            ByVal pListObjPP As List(Of clsBeProducto_pallet),
                            ByVal pIdBodega As Integer) As Integer Implements IServiceRecepcion.Guardar

        Try


            Return clsLnTrans_re_enc.Guardar(pObjTareaHH,
                                             pObjR,
                                             ObjROC,
                                             pListObjRD,
                                             pListObjRDP,
                                             pListObjROP,
                                             pListObjF,
                                             pListObjRI,
                                             pListObjSE,
                                             pListObjS,
                                             pListObjPP,
                                             pIdBodega)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    '''  Creada por Ricardo García
    ''' </summary>
    ''' <param name="backOrder"></param>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="pIdEmpresa"></param>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pIdUsuario"></param>
    ''' <param name="pListObjDetR"></param>
    Public Sub CerrarRecepcion(ByVal pRecEnc As clsBeTrans_re_enc,ByVal backOrder As Boolean, ByVal pIdOrdenCompraEnc As Integer, ByVal pIdRecepcionEnc As Integer,
                               ByVal pIdEmpresa As Integer, ByVal pIdBodega As Integer, ByVal pIdUsuario As String,
                               ByVal pListObjDetR As List(Of clsBeTrans_re_det)) Implements IServiceRecepcion.CerrarRecepcion

        Try

            clsLnTrans_re_enc.Finalizar_Recepcion(pRecEnc,backOrder, 
                                                  pIdOrdenCompraEnc, 
                                                  pIdRecepcionEnc, 
                                                  pIdEmpresa, 
                                                  pIdBodega, 
                                                  pIdUsuario, 
                                                  pListObjDetR,
                                                  True)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImpresionByRecepcion(ByVal pIdRecepcionEnc As Integer) As DataTable Implements IServiceRecepcion.GetImpresionByRecepcion

        Try
            Return clsLnTrans_re_enc.Get_Impresion_By_IdRecepcionEnc(pIdRecepcionEnc)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo Garcia funcion que obtiene todas las existencias segun el IdRecepcion
    ''' </summary>
    ''' <param name="pIdRecepcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllByRecepcion(ByVal pIdRecepcion As Integer) As List(Of clsBeStock_rec) Implements IServiceRecepcion.GetAllByRecepcion

        Try
            Return clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcion).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García obtengo todos los parametros Segun IdStock
    ''' </summary>
    ''' <param name="pIdStock"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllByIdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_parametro) Implements IServiceRecepcion.GetAllByIdStock

        Try
            Return clsLnStock_parametro.Get_All_By_IdStock(pIdStock).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Function GetAllSeriesByIdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_se) Implements IServiceRecepcion.GetAllSeriesByIdStock

        Try
            Return clsLnStock_se.Get_All_Serie_By_IdStock(pIdStock).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetSingle(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc Implements IServiceRecepcion.GetSingle

        Try
            Return clsLnTrans_re_enc.GetSingle(pIdRecepcionEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable Implements IServiceRecepcion.GetAll

        Try
            Return clsLnTrans_re_enc.GetAll(pActivo, pFechaDel, pFechaAl)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllOperadoresByRecepcion(ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_op) Implements IServiceRecepcion.GetAllOperadoresByRecepcion

        Try
            Return clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(pIdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllStockRecByRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec) Implements IServiceRecepcion.GetAllStockRecByRecepcion

        Try
            Return clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer) As List(Of clsBeStock_se_rec) Implements IServiceRecepcion.GetAllSerieByIdStockRec

        Try
            Return clsLnStock_se_rec.GetAllSerieByIdStockRec(pIdStockRec).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub DeleteOp(ByVal pIdOperadorRec As Integer, ByVal pIdRecepcionEnc As Integer) Implements IServiceRecepcion.DeleteOp

        Try
            clsLnTrans_re_op.Delete(pIdOperadorRec, pIdRecepcionEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Sub DeleteDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) Implements IServiceRecepcion.DeleteDet

        Try
            clsLnTrans_re_det.Delete(0, pIdRecepcionEnc, pIdRecepcionDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Sub DeleteImageByIndex(ByVal pIdRecepcionEnc As Integer, ByVal pIdImagen As Integer) Implements IServiceRecepcion.DeleteImageByIndex

        Try
            clsLnTrans_re_img.Delete(pIdRecepcionEnc, pIdImagen)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Function MaxIdEnc() As Integer Implements IServiceRecepcion.MaxIdEnc

        Try
            Return clsLnTrans_re_enc.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIdDet(ByVal pIdRecepcionEnc As Integer) As Integer Implements IServiceRecepcion.MaxIdDet

        Try
            Return clsLnTrans_re_det.MaxID(pIdRecepcionEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIdDetP(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As Integer Implements IServiceRecepcion.MaxIdDetP

        Try
            Return clsLnTrans_re_det_parametros.MaxID(pIdRecepcionEnc, pIdRecepcionDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIdImg(ByVal pIdRecepcionEnc As Integer) As Integer Implements IServiceRecepcion.MaxIdImg

        Try
            Return clsLnTrans_re_img.MaxID(pIdRecepcionEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function MaxIdOC(ByVal pIdRecepcionEnc As Integer) As Integer Implements IServiceRecepcion.MaxIdOC

        Try
            Return clsLnTrans_re_oc.MaxID(pIdRecepcionEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub Delete(ByVal pIdFacturaRecepcion As Integer) Implements IServiceRecepcion.Delete

        Try
            clsLnTrans_re_fact.Delete(pIdFacturaRecepcion)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Function GetAllByBodega(ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_enc) Implements IServiceRecepcion.GetAllByBodega

        Try
            Return clsLnTrans_re_enc.Get_All_By_IdBodega(pIdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByOrdenCompra(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_re_det) Implements IServiceRecepcion.GetAllByOrdenCompra

        Try

            Return clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class