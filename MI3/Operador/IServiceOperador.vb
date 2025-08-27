Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceOperador" in both code and config file together.
<ServiceContract()>
Public Interface IServiceOperador

    <OperationContract()>
    Function Insert(ByRef BeOperador As clsBeOperador) As Integer

    <OperationContract()>
    Function Update(ByRef BeOperador As clsBeOperador) As Integer

    <OperationContract()>
    Function Delete(ByRef BeOperador As clsBeOperador) As Integer

    <OperationContract()>
    Function Get_AllOperador() As List(Of clsBeOperador)

    <OperationContract()>
    Function Max_IdOperador() As Integer

    <OperationContract()>
    Function Exist(pIdOperador As Integer) As Boolean

End Interface
