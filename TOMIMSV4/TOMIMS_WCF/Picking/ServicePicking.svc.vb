Imports System.Reflection

' NOTE: You can use the "Rename" command on the context menu to change the class name "ServicePicking" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServicePicking.svc or ServicePicking.svc.vb at the Solution Explorer and start debugging.
Public Class ServicePicking
    Implements IServicePicking

    ''' <summary>
    '''  Creada por Ricardo García - Guarda y actualiza el proceso de Picking
    ''' </summary>
    ''' <param name="pObjE"></param>
    ''' <param name="pObjTA"></param>
    ''' <param name="pListObjD"></param>
    ''' <param name="pListObjP"></param>
    ''' <param name="pListObjO"></param>
    ''' <param name="pListObjU"></param>
    ''' <returns></returns>
    Public Function Guardar(ByVal pObjE As clsBeTrans_picking_enc,
                            ByVal pObjTA As clsBeTarea_hh,
                            ByVal pListObjD As List(Of clsBeTrans_picking_det),
                            ByVal pListObjP As List(Of clsBeTrans_picking_det_parametros),
                            ByVal pListObjO As List(Of clsBeTrans_picking_op),
                            ByVal pListObjU As List(Of clsBeTrans_picking_ubic)) As Boolean Implements IServicePicking.Guardar

        Try

            Return clsLnTrans_picking_enc.Guardar(pObjE, pObjTA, pListObjD, pListObjP, pListObjO, pListObjU)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function Eliminar(ByVal pObjE As clsBeTrans_picking_enc,
                            ByVal pListObjD As List(Of clsBeTrans_picking_det),
                            ByVal pListObjP As List(Of clsBeTrans_picking_det_parametros),
                            ByVal pListObjO As List(Of clsBeTrans_picking_op),
                            ByVal pListObjU As List(Of clsBeTrans_picking_ubic)) As Boolean Implements IServicePicking.Eliminar

        Eliminar = False

        Try

            '#EJC20171022_1025PM:
            'Return clsLnTrans_picking_enc.Eliminar(pObjE, pListObjD, pListObjP, pListObjO, pListObjU)

        Catch ex As Exception
           Throw New FaultException(ex.Message)
        End Try

    End Function


    Public Function GetAllOperadoresByPicking(ByVal pIdPicking As Integer) As List(Of clsBeTrans_picking_op) Implements IServicePicking.GetAllOperadoresByPicking

        Try
            Return clsLnTrans_picking_op.Get_All_By_IdPickingEnc(pIdPicking).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Sub DeleteOp(ByVal pIdOperadorPicking As Integer) Implements IServicePicking.DeleteOp

        Try
            clsLnTrans_picking_op.Delete(pIdOperadorPicking)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub


    Public Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable Implements IServicePicking.GetAll

        Try
            Return clsLnTrans_picking_enc.GetAll(pActivo, pFechaDel, pFechaAl)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetSingle(ByVal pIdPickingEnc As Integer) As clsBeTrans_picking_enc Implements IServicePicking.GetSingle

        Try
            Return clsLnTrans_picking_enc.GetSingle(pIdPickingEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function MaxID() As Integer Implements IServicePicking.MaxID

        Try

            Return clsLnTrans_picking_enc.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Anula(ByVal pIdPickingEnc As Integer) As Boolean Implements IServicePicking.Anula

        Anula = False

        Try

            '#EJC20171022_1025PM:
            'Return clsLnTrans_picking_enc.Anula(pIdPickingEnc)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García - Reporte de Ubicacion de Picking
    ''' </summary>
    ''' <param name="pIdPicking"></param>
    ''' <returns></returns>
    Public Function GetUbicacionPicking(ByVal pIdPicking As Integer, ByVal pIdPedidoEnc As Integer) As DataTable Implements IServicePicking.GetUbicacionPicking

        Try
            Return clsLnTrans_picking_ubic.Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(pIdPicking, pIdPedidoEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pFechaDel"></param>
    ''' <param name="pFechaAl"></param>
    ''' <returns></returns>
    Public Function GetPickingUbicacion(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdPropietarioBodega As Integer) As DataTable Implements IServicePicking.GetPickingUbicacion

        Try
            Return clsLnTrans_picking_ubic.Get_Picking_Ubicacion(pActivo, pFechaDel, pFechaAl, pIdPropietarioBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
