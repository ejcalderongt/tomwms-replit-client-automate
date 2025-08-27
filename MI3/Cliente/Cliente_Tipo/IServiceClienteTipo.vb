Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceClienteTipo

    <OperationContract()>
    Function Insert(ByRef BeClienteTipo As clsBeCliente_tipo) As Integer

    <OperationContract()>
    Function Update(ByVal BeClienteTipo As clsBeCliente_tipo) As Integer

    <OperationContract()>
    Function Disable(ByRef BeClienteTipo As clsBeCliente_tipo) As Integer

    <OperationContract()>
    Function Delete_All() As Integer

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeCliente_tipo)

    <OperationContract()>
    Function Get_Single_By_IdClienteTipo(ByVal pIdClienteTipo As Integer) As clsBeCliente_tipo

    <OperationContract()>
    Function Max_IdClienteTipo() As Integer

    <OperationContract()>
    Function Exist_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As Boolean

End Interface
