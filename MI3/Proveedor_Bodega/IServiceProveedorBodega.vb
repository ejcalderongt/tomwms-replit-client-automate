Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceProveedorBodega

    <OperationContract()>
    Function ActualizaDatos(ByVal pObjMD As clsBeProveedor, ByVal pListObjMDB As List(Of clsBeProveedor_bodega)) As Boolean

    <OperationContract()>
    Function Get_All_By_IdProveedor(ByVal pIdProveedor As Integer) As List(Of clsBeProveedor_bodega)

    <OperationContract()>
    Function Get_Single_By_IdProveedorBodega(ByVal pIdProveedor As Integer) As clsBeProveedor_bodega

    <OperationContract()>
    Function Insert(ByRef BeProveedorBodega As clsBeProveedor_bodega) As Integer

    <OperationContract()>
    Function MaxIdProveedorBodega() As Integer

End Interface
