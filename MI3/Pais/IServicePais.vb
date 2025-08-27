Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServicePais" in both code and config file together.
<ServiceContract()>
Public Interface IServicePais

    <OperationContract()>
    Function Update(ByVal BePais As clsBePaises) As Integer

    <OperationContract()>
    Function Delete(ByVal pIdPais As Integer) As Integer

    <OperationContract()>
    Function Insert(ByRef BePais As clsBePaises) As Integer

    <OperationContract()>
    Function Get_All_Filtro() As List(Of clsBePaises)

    <OperationContract()>
    Function Max_IdPais() As Integer

    <OperationContract()>
    Function Exist(pIdPais As Integer) As Boolean

End Interface
