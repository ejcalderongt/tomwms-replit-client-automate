Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceBodega" in both code and config file together.
<ServiceContract()>
Public Interface IServiceBodega

    <OperationContract()>
    Function Get_All() As List(Of clsBeBodega)

    <OperationContract()>
    Function Get_Single_By_IdClienteBodega(pIdBodega As Integer) As clsBeBodega

    <OperationContract>
    Function Get_IdBodegaWMS_By_Codigo_Bodega_ERP(ByVal pCodBodegaERP As String) As Integer

    <OperationContract>
    Function Get_IdBodegaWMS_By_Codigo(ByVal pCodigoBodega As String) As Integer

    <OperationContract>
    Function Inserta_I_NAV_Bodega(ByVal pBeBodegaERP As clsBeI_nav_bodega) As Integer

    <OperationContract>
    Function Get_All_I_Nav_Bodega() As List(Of clsBeI_nav_bodega)

    <OperationContract>
    Function Eliminar_I_Nav_Bodega(pCodigoBodega As String) As Integer

    <OperationContract>
    Function Get_All_By_IdEmpresa_And_IdUsuario(pIdEmpresa As Integer, pIdUsuario As Integer) As List(Of clsBodegasUsuarioRes)


End Interface
