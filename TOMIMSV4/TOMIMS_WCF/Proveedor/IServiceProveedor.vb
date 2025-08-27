Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceProveedor" in both code and config file together.
<ServiceContract()>
Public Interface IServiceProveedor

    <OperationContract()>
    Function Insert(ByRef ObjP As clsBeProveedor) As Integer

    <OperationContract()>
    Function Update(ByVal ObjP As clsBeProveedor) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjP As clsBeProveedor) As Integer

    <OperationContract()>
    Function Get_All_By_IdBodega(ByVal pActivo As Boolean, ByVal pIdBodega As Integer) As List(Of clsBeProveedor)

    Function Get_All_By_CodBodega(ByVal pActivo As Boolean, ByVal pCodBodega As String) As List(Of clsBeProveedor)

    <OperationContract()>
    Function Get_All_By_IdPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProveedor)

    <OperationContract()>
    Function Get_Single_By_IdProveedor(ByVal pIdProveedor As Integer) As clsBeProveedor

    <OperationContract()>
    Function Get_By_IdProveedor_And_IdPropietarioBodega(ByVal pIdProveedor As Integer, ByVal pIdPropietario As Integer) As clsBeProveedor

    <OperationContract()>
    Function MaxIdProveedor() As Integer

    <OperationContract()>
    Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeProveedor_bodega)

End Interface
