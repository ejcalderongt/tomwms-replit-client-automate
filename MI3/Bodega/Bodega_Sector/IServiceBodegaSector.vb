Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceBodegaSector" in both code and config file together.
<ServiceContract()>
Public Interface IServiceBodegaSector
    <OperationContract()>
    Function GetSingle(pIdTramo As Integer, pIdBodega As Integer) As clsBeBodega_sector
    <OperationContract()>
    Function Insert(ByRef oBeBodega_tramo As clsBeBodega_sector) As Integer
    <OperationContract()>
    Function Update_Single(oBeBodega_tramo As clsBeBodega_sector) As Integer
    <OperationContract()>
    Function Get_All() As List(Of clsBeBodega_sector)
End Interface
