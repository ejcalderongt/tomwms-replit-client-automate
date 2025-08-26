Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IProductoRellenado" in both code and config file together.
<ServiceContract()>
Public Interface IPresentacion_Tarima

    <OperationContract()>
    Function Insertar(ByVal pObj As clsBeProducto_presentacion_tarima) As Boolean

    <OperationContract()>
    Function Actualizar(ByVal pObj As clsBeProducto_presentacion_tarima) As Boolean

    <OperationContract()>
    Function GetSingle(ByVal pIdPresentacionTarima As Integer) As clsBeProducto_presentacion_tarima

    <OperationContract()>
    Function GetAllByPresentacion(ByVal pActivo As Boolean) As List(Of clsBeProducto_presentacion_tarima)

    '<OperationContract()>
    'Function MaxID() As Integer

    '<OperationContract()>
    'Function ExisteConfiguracion(ByVal pIdPresentacion As Integer, ByVal pIdUbicacion As Integer)
    '<OperationContract()>
    'Function GetAll() As List(Of clsBeProducto_presentacion_tarima)

    <OperationContract()>
    Sub Desactivar(ByVal pIdPresentacionTarima As Integer)

End Interface
