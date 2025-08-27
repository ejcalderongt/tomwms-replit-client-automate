' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "ServicePedido" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicePedido.svc o ServicePedido.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Reflection


Public Class ServicePedido
    Implements IServicePedido

    Function MaxID() As Integer Implements IServicePedido.MaxID

        Try
            Return clsLnTrans_pe_enc.MaxID
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function ActualizarDatos(ByVal pObjE As clsBeTrans_pe_enc,
                                    ByVal pListObjTD As List(Of clsBeTrans_pe_det)) As Boolean Implements IServicePedido.ActualizaDatos

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction()

            If pObjE.IsNew Then
                pObjE.IdPedidoEnc = clsLnTrans_oc_enc.MaxID()
                If pObjE.No_documento.ToString = "" Then pObjE.No_documento = Strings.Right("PE000000" & pObjE.IdPedidoEnc, 8)
                clsLnTrans_pe_enc.Insertar(pObjE, lConnection, lTransaction)
            Else
                clsLnTrans_pe_enc.Actualizar(pObjE, lConnection, lTransaction)
            End If

            For Each Obj As clsBeTrans_pe_det In pListObjTD
                clsLnTrans_pe_det.Actualizar(Obj, lConnection, lTransaction)
            Next

            lTransaction.Commit()

            lConnection.Close()

            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New FaultException(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Function GetSingle(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc Implements IServicePedido.GetSingle

        Try
            Return clsLnTrans_pe_enc.GetSingle(pIdPedidoEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pAnulado As Boolean = False) As DataTable Implements IServicePedido.GetAll

        Try

            Return clsLnTrans_pe_enc.GetAll(pActivo, pFechaDel, pFechaAl)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllActivos(ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal ConPicking As Boolean) As DataTable Implements IServicePedido.GetAllActivos

        Try

            Return clsLnTrans_pe_enc.Get_All_Activos(pFechaDel, pFechaAl, ConPicking, "")

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetDetalle(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det) Implements IServicePedido.GetDetalle

        Try
            Return clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(CObj(pIdPedidoEnc)).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function InsertaEncabezado(ByRef pPedido As clsBeTrans_pe_enc) As Boolean Implements IServicePedido.InsertaEncabezado

        InsertaEncabezado = False

        Try
            
            Dim ResultadoInsert As Integer = 0

            pPedido.IdPedidoEnc = clsLnTrans_pe_enc.MaxID()
            pPedido.No_documento = Strings.Right("PE000000" & clsLnTrans_pe_enc.MaxID(), 7)

            ResultadoInsert = clsLnTrans_pe_enc.Insertar(pPedido)

            InsertaEncabezado = ResultadoInsert > 0

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function InsertaDetalle(ByRef pPedidoDet As clsBeTrans_pe_det) As Boolean Implements IServicePedido.InsertaDetalle

        InsertaDetalle = False

        Try

            Dim ResultadoInsert As Integer = 0

            If pPedidoDet.IsNew Then
                pPedidoDet.IdPedidoDet = clsLnTrans_pe_det.MaxID() + 1
                ResultadoInsert = clsLnTrans_pe_det.Insertar(pPedidoDet)
            Else
                ResultadoInsert = clsLnTrans_pe_det.Actualizar(pPedidoDet)
            End If

            InsertaDetalle = ResultadoInsert > 0

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function PedidoTieneDetalle(ByVal IdPedidoEnc As Integer) As Boolean Implements IServicePedido.PedidoTieneDetalle

        PedidoTieneDetalle = False

        Try

            'PedidoTieneDetalle = clsLnTrans_pe_enc.PedidoTieneDetalle(IdPedidoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function EliminaLineaDetallePedido(ByVal IdpedidoEnc As Integer, ByVal IdPedidoDet As Integer, ByVal IdPickingEnc As Integer) As Boolean Implements IServicePedido.EliminaLineaDetallePedido

        EliminaLineaDetallePedido = False

        Try

            EliminaLineaDetallePedido = clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(IdpedidoEnc, IdPedidoDet,IdPickingEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function EliminaPedidoSinDetalle(ByVal IdPedidoEnc As Integer) As Boolean Implements IServicePedido.EliminaPedidoSinDetalle

        EliminaPedidoSinDetalle = False

        Try

            'EliminaPedidoSinDetalle = clsLnTrans_pe_enc.EliminarEncabezadoPedido(IdPedidoEnc) > 0

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function EliminaPedidoConDetalle(ByVal IdPedidoEnc As Integer) As Boolean Implements IServicePedido.EliminaPedidoConDetalle

        EliminaPedidoConDetalle = False


        Try

            'EliminaPedidoConDetalle = clsLnTrans_pe_enc.EliminarPedido(IdPedidoEnc) > 0

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    'AnularPedido

    Public Function AnularPedidoConDetalle(ByVal IdPedidoEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer) As Boolean Implements IServicePedido.AnularPedidoConDetalle

        AnularPedidoConDetalle = False


        Try

            AnularPedidoConDetalle = clsLnTrans_pe_enc.AnularPedido(IdPedidoEnc, pIdMotivoAnulacionBodega) > 0

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetAllByPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det) Implements IServicePedido.GetAllByPedidoEnc

        Try

            Return clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(CObj(pIdPedidoEnc)).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetAllByPedidoDet(ByVal pIdPedidoDet As Integer) As List(Of clsBeStock_res) Implements IServicePedido.GetAllByPedidoDet

        Try

            Return clsLnTrans_pe_det.Get_All_By_IdPedidoDet(pIdPedidoDet)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    'Public Function GetAllStockResByPedido(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det) Implements IServicePedido.GetAllStockResByPedido

    '    Try

    '        Return clsLnTrans_pe_det.GetAllStockResByPedido(pIdPedidoEnc).ToList

    '    Catch ex As FaultException
    '        Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
    '    Catch ex1 As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
    '    End Try

    'End Function

End Class
