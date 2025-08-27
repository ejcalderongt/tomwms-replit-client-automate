Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IsrvUsuario" in both code and config file together.
<ServiceContract()>
Public Interface IsrvUsuario

    <OperationContract()>
    Function Usuario_Valido_By_Obj(ByRef Usuario As clsBeUsuario) As Boolean

    <OperationContract()>
    Function Usuario_Valido_By_Params(ByVal pCodigo As String, pClave As String, pIdEmpresa As Integer) As Boolean

End Interface
