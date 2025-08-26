Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IPedidoCompra" in both code and config file together.
<ServiceContract()>
Public Interface IPedidoCompra

    <OperationContract()>
    Function Insert(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc, ByRef Resultado As String) As Integer

    <OperationContract()>
    Function Update(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc) As Integer

    <OperationContract()>
    Function Delete(NoEnc As String) As Boolean

    <OperationContract()>
    Function Get_Single_By_NoEnc(NoEnc As String) As clsBeI_nav_ped_compra_enc

    <OperationContract()>
    Function Insert_Multiple(lPedidosCompra As List(Of clsBeI_nav_ped_compra_enc)) As Boolean

    <OperationContract()>
    Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(ByVal pReferencia As String, ByVal pEnviado As Boolean) As Integer

    <OperationContract()>
    Function Registrar_Lote_Documento_Ingreso(pNoDocumentoIngreso As String,
                                              pNoLinea As Integer,
                                              pCodigoProducto As String,
                                              pCantidad As Double,
                                              pFechaVence As Date,
                                              pLote As String,
                                              pLicencia As String,
                                              pUbicacionNAV As String) As Boolean

    <OperationContract()>
    Function Actualizar_Estado_Enviado_A_ERP_By_No_Documento_Devolucion(pNo_Documento_Devolucion As String, pEnviado As Boolean) As Integer

    <OperationContract()>
    Function Existe_Documento_Ingreso(pNoDocumentoIngreso As String) As Boolean

    <OperationContract()>
    Function Desactivar_Ubicacion_Documento_Ingreso(pNoDocumentoIngreso As String, pUbicacion As String) As Boolean

    <OperationContract>
    Function Actualizar_Licencia_OP(ByVal pNoDocumentoIngreso As String,
                                    ByVal pNoLinea As Integer,
                                    ByVal pCodigoProducto As String,
                                    ByVal pFechaVence As Date,
                                    ByVal pLote As String,
                                    ByVal pLicencia As String,
                                    ByVal pUbicacionNAV As String) As Boolean

    '<OperationContract()>
    'Function Actualizar_Estado_Documento_Ingreso_By_Referencia(ByVal pReferencia As String, ByVal pEstado As clsDataContractDI.tEstadoOC) As Integer

    '<OperationContract()>
    'Function Actualizar_Estado_Documento_Ingreso_By_Referencia(ByVal pReferencia As String, ByVal pEnviadoAERP As Boolean, ByVal pEstado As clsDataContractDI.tEstadoOC) As Integer

End Interface
