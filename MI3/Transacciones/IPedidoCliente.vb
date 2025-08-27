Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IPedidoCliente" in both code and config file together.
<ServiceContract()>
Public Interface IPedidoCliente

    <OperationContract()>
    Function Insert(ByRef BeINavPedClienteEnc As clsBeI_nav_ped_traslado_enc, ByRef Resultado As String) As Integer

    <OperationContract()>
    Function Update(ByRef BeINavPedClienteEnc As clsBeI_nav_ped_traslado_enc) As Integer

    <OperationContract()>
    Function Delete(NoEnc As String) As Boolean

    <OperationContract()>
    Function Get_Single_By_NoEnc(NoEnc As String) As clsBeI_nav_ped_traslado_enc

    <OperationContract()>
    Function Insert_Multiple(ByVal lPedidosCompra As List(Of clsBeI_nav_ped_traslado_enc), ByRef Resultado As String) As Boolean

    <OperationContract()>
    Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(pReferencia As String, pEnviado As Boolean) As Integer

    <OperationContract()>
    Function Update_Receipt_Document_Reference(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_traslado_enc) As Integer

    <OperationContract()>
    Function Actualizar_Referencia_By_IdPedidoEnc(pReferencia As String, pIdPedidoEnc As Integer) As Integer

    <OperationContract()>
    Function Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(pIdPedidoEnc As Integer, pEnviado As Boolean) As Integer
    <OperationContract()>
    Function Generar_Ingreso_Por_Anulacion_NC_SAP(IdPedidoEnc As Integer, DocEntrySolicitudDevolucion As Integer) As Boolean

End Interface
