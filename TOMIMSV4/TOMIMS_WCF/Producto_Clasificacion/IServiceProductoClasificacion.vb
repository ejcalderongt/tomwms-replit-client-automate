<ServiceContract()>
Public Interface IServiceProductoClasificacion

    <OperationContract()>
    Function Insert(ByRef BeProductoClasificacion As clsBeProducto_clasificacion) As Integer

    <OperationContract()>
    Sub GuardarTransaccion(ByVal pListBeProductoClas As List(Of clsBeProducto_clasificacion))

    <OperationContract()>
    Function Update(ByVal BeProductoClasificacion As clsBeProducto_clasificacion) As Integer

    <OperationContract()>
    Function Disable(ByRef BeProductoClasificacion As clsBeProducto_clasificacion) As Integer

    <OperationContract()>
    Sub Delete(ByVal pIdClasificacion As Integer)

    <OperationContract()>
    Sub DeleteByPropietario(ByVal pIdPropietario As Integer)

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_clasificacion)

    <OperationContract()>
    Function Get_Single_By_IdProductoClasificacion(ByVal pIdProductoClasificacion As Integer) As clsBeProducto_clasificacion

    <OperationContract()>
    Function Get_Single_By_IdProductoClas_And_IdPropietario(ByVal pIdProductoClasificacion As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_clasificacion

    <OperationContract()>
    Function Exist(ByVal pIdClasificacion As Integer) As Boolean

    <OperationContract()>
    Function Exist_Producto_Asociado(ByVal pIdClasificacion As Integer) As Boolean

    <OperationContract()>
    Function Max_IdProducto_Clasificacion() As Integer    

End Interface
