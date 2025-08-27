Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServicePaisMunicipio" in both code and config file together.
<ServiceContract()>
Public Interface IServicePaisMunicipio

    <OperationContract()>
    Function Exist(pIdMuni As Integer) As Boolean

    <OperationContract()>
    Function Max_IdPaisMunicipio() As Integer

    <OperationContract()>
    Function Get_AllMunicipio() As List(Of clsBePais_municipio)

    <OperationContract()>
    Function Delete(ByRef BePaisMuni As clsBePais_municipio) As Integer

    <OperationContract()>
    Function Update(ByRef BePaisMuni As clsBePais_municipio) As Integer

    <OperationContract()>
    Function Insert(ByRef BePaisMuni As clsBePais_municipio) As Integer

End Interface
