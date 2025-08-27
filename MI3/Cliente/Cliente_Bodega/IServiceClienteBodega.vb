' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceClienteBodega" in both code and config file together.
Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceClienteBodega

    <OperationContract()>
    Function Insert(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer

    <OperationContract()>
    Function Update(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer

    <OperationContract()>
    Function Delete(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer

    <OperationContract()>
    Function Get_Single_By_IdClienteBodega(ByVal IdClienteBodega As Integer) As clsBeCliente_bodega
    <OperationContract()>
    Function Get_All() As List(Of clsBeCliente_bodega)
    <OperationContract()>
    Function Max_Id() As Integer

End Interface
