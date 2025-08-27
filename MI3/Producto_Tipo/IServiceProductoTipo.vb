Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceProductoTipo

    <OperationContract()>
    Function Insert(ByRef ObjPT As clsBeProducto_tipo) As Integer

    <OperationContract()>
    Function Update(ByVal ObjPT As clsBeProducto_tipo) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjPT As clsBeProducto_tipo) As Integer

    <OperationContract()>
    Function Get_All_Filtro_By_IdPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_tipo)

    <OperationContract()>
    Function Get_Single_By_IdTipoProducto(ByVal pIdFamilia As Integer) As clsBeProducto_tipo

    <OperationContract()>
    Function Get_Single_By_IdTipoProducto_And_IdPropietario(ByVal pIdFamilia As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_tipo

    <OperationContract()>
    Sub Delete(ByVal pIdTipoProducto As Integer)

    <OperationContract()>
    Function Exist(ByVal pIdTipoProducto As Integer) As Boolean

    <OperationContract()>
    Function Existe_Producto_Asocidado(ByVal pIdTipoProducto As Integer) As Boolean

    <OperationContract()>
    Function MAXIdProductoTipo() As Integer

    <OperationContract()>
    Function Exist_By_Codigo(pCodigo As String) As Boolean

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
