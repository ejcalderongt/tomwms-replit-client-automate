Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServicePaisDepartamento" in both code and config file together.
<ServiceContract()>
Public Interface IServicePaisDepartamento

    <OperationContract()>
    Function Insert(ByRef BePaisDep As clsBePais_departamento) As Integer

    <OperationContract()>
    Function Update(ByRef BePaisRegion As clsBePais_departamento) As Integer

    <OperationContract()>
    Function Delete(ByRef BePaisRegion As clsBePais_departamento) As Integer

    <OperationContract()>
    Function Get_AllDepartamento() As List(Of clsBePais_departamento)

    <OperationContract()>
    Function Max_IdPaisDepartamento() As Integer

    <OperationContract()>
    Function Exist(pIdRegion As Integer) As Boolean

End Interface
