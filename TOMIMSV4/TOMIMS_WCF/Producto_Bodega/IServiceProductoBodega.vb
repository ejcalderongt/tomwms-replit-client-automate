<ServiceContract()>
Public Interface IServiceProductoBodega

    <OperationContract()>
    Function Insert(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer

    <OperationContract()>
    Function Update(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer

    <OperationContract()>
    Function Delete(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer

    <OperationContract()>
    Function Get_Single_By_IdProductoBodega(ByVal IdProductoBodega As Integer) As clsBeProducto_bodega

    <OperationContract()>
    Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_bodega)

End Interface
