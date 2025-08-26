Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceOperadorBodega" in both code and config file together.
<ServiceContract()>
Public Interface IServiceOperadorBodega

    <OperationContract()>
    Function Insert(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer
    <OperationContract()>
    Function Update(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer
    <OperationContract()>
    Function Delete(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer
    <OperationContract()>
    Function Get_AllOperadorBodega() As List(Of clsBeOperador_bodega)
    <OperationContract()>
    Function Max_IdOperadorBodega() As Integer

End Interface
