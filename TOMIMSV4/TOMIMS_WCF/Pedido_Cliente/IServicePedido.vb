Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IServicePedido" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IServicePedido

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function ActualizaDatos(ByVal pObjE As clsBeTrans_pe_enc, _
                            ByVal pListObjTD As List(Of clsBeTrans_pe_det)) As Boolean

    <OperationContract()>
    Function GetSingle(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc


    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pAnulado As Boolean = False) As DataTable


    <OperationContract()>
    Function GetAllActivos(ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal ConPicking As Boolean) As DataTable


    <OperationContract()>
    Function GetDetalle(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det)


    <OperationContract()>
    Function InsertaEncabezado(ByRef pPedido As clsBeTrans_pe_enc) As Boolean


    <OperationContract()>
    Function InsertaDetalle(ByRef pPedidoDet As clsBeTrans_pe_det) As Boolean


    <OperationContract()>
    Function PedidoTieneDetalle(ByVal IdPedidoEnc As Integer) As Boolean


    <OperationContract()>
    Function EliminaPedidoSinDetalle(ByVal IdPedidoEnc As Integer) As Boolean


    <OperationContract()>
    Function EliminaPedidoConDetalle(ByVal IdPedidoEnc As Integer) As Boolean


    <OperationContract()>
    Function EliminaLineaDetallePedido(ByVal IdpedidoEnc As Integer, ByVal IdPedidoDet As Integer, ByVal IdPickingEnc As Integer) As Boolean


    <OperationContract()>
    Function AnularPedidoConDetalle(ByVal IdPedidoEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer) As Boolean


    <OperationContract()>
    Function GetAllByPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det)


    <OperationContract()>
    Function GetAllByPedidoDet(ByVal pIdPedidoDet As Integer) As List(Of clsBeStock_res)


    '<OperationContract()>
    'Function GetAllStockResByPedido(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det)

End Interface
