Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServicePais_Region" in both code and config file together.
<ServiceContract()>
Public Interface IServicePaisRegion

    <OperationContract()>
    Function Insert(ByRef BePaisRegion As clsBePais_region) As Integer

    <OperationContract()>
    Function Update(ByRef BePaisRegion As clsBePais_region) As Integer

    <OperationContract()>
    Function Delete(ByRef BePaisRegion As clsBePais_region) As Integer

    <OperationContract()>
    Function Get_AllRegion() As List(Of clsBePais_region)

    <OperationContract()>
    Function Max_IdPaisRegion() As Integer

    <OperationContract()>
    Function Exist(pIdRegion As Integer) As Boolean

End Interface
