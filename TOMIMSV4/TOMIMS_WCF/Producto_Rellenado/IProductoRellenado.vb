Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IProductoRellenado" in both code and config file together.
<ServiceContract()>
Public Interface IProductoRellenado

    <OperationContract()>
    Function Insert(ByVal pObj As clsBeProducto_rellenado) As Boolean

    <OperationContract()>
    Function Update(ByVal pObj As clsBeProducto_rellenado) As Boolean

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPR As List(Of clsBeProducto_rellenado))

    <OperationContract()>
    Function Get_Single_By_IdRellenado(ByVal pIdRellenado As Integer) As clsBeProducto_rellenado

    <OperationContract()>
    Function Get_All_By_IdPresentacion(ByVal pIdPresentacion As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_rellenado)

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function Existe_Configuracion(ByVal pIdPresentacion As Integer, ByVal pIdUbicacion As Integer)

    <OperationContract()>
    Sub Disable(ByVal pIdRellenado As Integer)

End Interface
