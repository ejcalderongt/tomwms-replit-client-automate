Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceBodegaTramo" in both code and config file together.
<ServiceContract()>
Public Interface IServiceBodegaTramo

    <OperationContract()>
    Function Insert(ByRef oBeBodega_tramo As clsBeBodega_tramo) As Integer
    <OperationContract()>
    Function GetSingle(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As clsBeBodega_tramo
    <OperationContract()>
    Function Get_All() As List(Of clsBeBodega_tramo)
    <OperationContract()>
    Function Update_Single(oBeBodega_tramo As clsBeBodega_tramo) As Integer

End Interface
