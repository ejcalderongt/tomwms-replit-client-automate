' NOTE: You can use the "Rename" command on the context menu to change the class name "Stock" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Stock.svc or Stock.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class Stock
    Implements IStock

    Public Function GuardarExistencias(ByVal pListObjS As List(Of clsBeStock), ByVal pListObjSP As List(Of clsBeStock_parametro), ByVal pListObjSE As List(Of clsBeStock_se)) Implements IStock.GuardarExistencias
        
        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction()

            For Each ObjS As clsBeStock In pListObjS
                If ObjS.IsNew Then
                    clsLnStock.Insertar(ObjS, lConnection, lTransaction)
                Else
                    clsLnStock.Actualizar(ObjS, lConnection, lTransaction)
                End If
            Next

            For Each ObjSP As clsBeStock_parametro In pListObjSP
                If ObjSP.IsNew Then
                    clsLnStock_parametro.Insertar(ObjSP, lConnection, lTransaction)
                Else
                    clsLnStock_parametro.Actualizar(ObjSP, lConnection, lTransaction)
                End If
            Next

            For Each ObjSE As clsBeStock_se In pListObjSE
                If ObjSE.IsNew Then
                    clsLnStock_se.Insertar(ObjSE, lConnection, lTransaction)
                Else
                    clsLnStock_se.Actualizar(ObjSE, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()            

            Return True

        Catch ex As Exception
            lTransaction.Rollback()            
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Function MaxID() As Integer Implements IStock.MaxID

        Try
            Return clsLnStock.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIDRec() As Integer Implements IStock.MaxIDRec

        Try
            Return clsLnStock_rec.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function MaxIDSe() As Integer Implements IStock.MaxIDSe

        Try
            Return clsLnStock_se.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIDSeRec() As Integer Implements IStock.MaxIDSeRec

        Try
            Return clsLnStock_se_rec.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function MaxIDP() As Integer Implements IStock.MaxIDP

        Try
            Return clsLnStock_parametro.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_stock_res) Implements IStock.GetAllByPropietarioBodega

        Try
            Return clsLnStock.Get_All_By_IdPropietarioBodega(pIdPropietarioBodega).ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetSingleByIdStock(ByVal pIdStock As Integer) As clsBeVW_stock_res Implements IStock.GetSingleByIdStock
        Try
            Return clsLnStock.Get_Single_By_IdStock(pIdStock)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function


    Public Function GetSingleByCodigo(ByVal Codigo As String) As clsBeVW_stock_res Implements IStock.GetSingleByCodigo
        Try
            Return clsLnStock.Get_Single_By_Codigo(Codigo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function
    Public Function GetExistenciaDetByIdProducto(ByVal pIdProducto As Integer) As List(Of clsBeStock) Implements IStock.GetExistenciaByProductoBodega

        Try
            'Devolver aquí la suma de la cantidad por producto, presentación, estado y/o unidad de medida
            Return clsLnStock.Get_Lista_Existencias_By_IdProductoBodega(pIdProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub GetExistenciaResByProducto(ByRef pProducto As clsBeStock, ByVal pIdBodega As Integer) Implements IStock.GetExistenciaResByProducto

        Try

            'Devolver aquí la suma de la cantidad por producto, presentación, estado y/o unidad de medida
            clsLnStock.Get_Existencia_Disp_By_IdProducto(pProducto, pIdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Sub

    Public Function GetAllStockRecByRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec) Implements IStock.GetAllStockRecByRecepcion

        Try
            Return clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer) As List(Of clsBeStock_se_rec) Implements IStock.GetAllSerieByIdStockRec

        Try
            Return clsLnStock_se_rec.GetAllSerieByIdStockRec(pIdStockRec).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function ReservaStock(ByVal pStockRes As clsBeStock_res, ByVal DiasVencimiento As Integer) As Boolean Implements IStock.ReservaStock

        ReservaStock = False

        Try

            'Return clsLnStock_res.ReservaStock(pStockRes, DiasVencimiento)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetRptProductsMinMax(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res) Implements IStock.GetRptProductsMinMax
        Try
            Return clsLnStock.GetRptProductsMinMax(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetProductsPendientesRequisicion(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res) Implements IStock.GetProductsPendientesRequisicion
        Try
            Return clsLnStock.GetProductsPendientesRequisicion(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function getRptProductosProximosVencimiento(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res) Implements IStock.getRptProductosProximosVencimiento
        Try
            Return clsLnStock.getRptProductosProximosVencimiento(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function GetStock(ByVal pTransito As Boolean, ByVal pIdOrdenCompraEnc As Integer) As DataTable Implements IStock.GetStock

        Try
            Return clsLnStock.GetStock(pTransito, pIdOrdenCompraEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function TieneStockReservado(ByVal IdPedidoDet As Integer) As Boolean Implements IStock.TieneStockReservado

        TieneStockReservado = False

        Try

            'Return clsLnStock_res.TieneStockRes(IdPedidoDet)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function StockResEstaPickeado(ByVal IdPedidoDet As Integer) As Boolean Implements IStock.StockResEstaPickeado
        StockResEstaPickeado = False
        Try
            Return clsLnStock_res.Stock_Res_Esta_Pickeado_By_IdPedidoDet(IdPedidoDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    Public Function EliminaStockReservado(ByVal IdPedidoEnc As Integer, ByVal IdPedidoDet As Integer) As Boolean Implements IStock.EliminaStockReservado

        EliminaStockReservado = False

        Try

            'Return clsLnStock_res.EliminarStockReservado(IdPedidoEnc, IdPedidoDet)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function GetByIdProductoBodega(ByVal pIdProductoBodega As Integer, ByVal pIdPresentacion As Integer) As clsBeStock Implements IStock.GetByIdProductoBodega

        Try
            Return clsLnStock.Get_BeStock_By_IdProductoBodega(pIdProductoBodega, pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    'Tab stock 
    Public Function GetAllByBP(ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res) Implements IStock.GetAllByBP

        Try
            Return clsLnStock.Get_All_By_IdProductoBodega(pIdProductoBodega).ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetCantidadReservada(ByVal pIdStock As Integer) As Double Implements IStock.GetCantidadReservada

        Try
            Return clsLnStock.Get_Cantidad_Reservada(pIdStock)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
