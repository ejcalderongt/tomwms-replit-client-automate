Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceProductoEstado

    <OperationContract()>
    Function Insert(ByRef ObjPE As clsBeProducto_estado) As Integer

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPE As List(Of clsBeProducto_estado))

    <OperationContract()>
    Sub Insert_Producto_Estado_With_Ubic(ByVal pObjPE As clsBeProducto_estado, ByVal pListObjDet As List(Of clsBeProducto_estado_ubic))


    <OperationContract()>
    Function Update(ByVal ObjPE As clsBeProducto_estado) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjPE As clsBeProducto_estado) As Integer

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_estado)

    <OperationContract()>
    Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBeProducto_estado)

    <OperationContract()>
    Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBeProducto_estado)

    <OperationContract()>
    Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer)

    <OperationContract()>
    Sub Delete(ByVal BeProductoEstado As clsBeProducto_estado)

    <OperationContract()>
    Function Exist_By_IdEstado(ByVal pIdEstado As Integer) As Boolean

    <OperationContract()>
    Function Max_IdProducto_Estado() As Integer

    <OperationContract()>
    Function Get_All_Estados_Stock_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_estado)

    <OperationContract()>
    Function Get_All_By_IdEstado_And_Estatus(ByVal pIdEstado As Integer, ByVal Activo As Boolean) As List(Of clsBeProducto_estado_ubic)

    <OperationContract()>
    Function Get_Single_By_IdEstado(ByVal pIdEstado As Integer) As clsBeProducto_estado

    <OperationContract()>
    Sub Delete_By_IdEstado_Ubic(ByVal pIdEstadoUbic As Integer)

End Interface
