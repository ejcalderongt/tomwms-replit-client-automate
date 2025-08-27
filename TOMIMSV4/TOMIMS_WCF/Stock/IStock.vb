Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IStock" in both code and config file together.
<ServiceContract()>
Public Interface IStock

    <OperationContract()>
    Function GuardarExistencias(ByVal pListObjS As List(Of clsBeStock), ByVal pListObjSP As List(Of clsBeStock_parametro), ByVal pListObjSE As List(Of clsBeStock_se))

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function MaxIDRec() As Integer

    <OperationContract()>
    Function MaxIDSe() As Integer

    <OperationContract()>
    Function MaxIDSeRec() As Integer

    <OperationContract()>
    Function MaxIDP() As Integer

    <OperationContract()>
    Function GetAllByPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_stock_res)

    <OperationContract()>
    Function GetExistenciaByProductoBodega(ByVal pIdProducto As Integer) As List(Of clsBeStock)

    <OperationContract()>
    Function GetSingleByCodigo(ByVal Codigo As String) As clsBeVW_stock_res

    <OperationContract()>
    Function GetSingleByIdStock(ByVal pIdStock As Integer) As clsBeVW_stock_res

    <OperationContract()>
    Sub GetExistenciaResByProducto(ByRef pProducto As clsBeStock, ByVal pIdBodega As Integer)

    <OperationContract()>
    Function GetAllStockRecByRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec)

    <OperationContract()>
    Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer) As List(Of clsBeStock_se_rec)

    <OperationContract()>
    Function ReservaStock(ByVal pStockRes As clsBeStock_res, ByVal DiasVencimiento As Integer) As Boolean

    <OperationContract()>
    Function GetProductsPendientesRequisicion(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)


    <OperationContract()>
    Function GetRptProductsMinMax(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

    <OperationContract()>
    Function getRptProductosProximosVencimiento(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

    <OperationContract()>
    Function GetStock(ByVal pTransito As Boolean, ByVal pIdOrdenCompraEnc As Integer) As DataTable

    <OperationContract()>
    Function TieneStockReservado(ByVal IdPedidoDet As Integer) As Boolean

    <OperationContract()>
    Function StockResEstaPickeado(ByVal IdPedidoDet As Integer) As Boolean

    '<OperationContract()>
    'Function GetSingle(ByVal pIdStock As Integer) As clsBeStock

    <OperationContract()>
    Function EliminaStockReservado(ByVal IdPedidoEnc As Integer, ByVal IdPedidoDet As Integer) As Boolean

    <OperationContract()>
    Function GetByIdProductoBodega(ByVal pIdProductoBodega As Integer, ByVal pIdPresentacion As Integer) As clsBeStock

    <OperationContract()>
    Function GetAllByBP(ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res)

    <OperationContract()>
    Function GetCantidadReservada(ByVal pIdStock As Integer) As Double

End Interface
