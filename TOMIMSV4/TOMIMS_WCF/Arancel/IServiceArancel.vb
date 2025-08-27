<ServiceContract()>
Public Interface IServiceArancel

    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBeArancel) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBeArancel) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBeArancel) As Integer

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeArancel)

    <OperationContract()>
    Function GetAll() As List(Of clsBeArancel)

    <OperationContract()>
    Function GetSingle(ByVal pIdArancel As Integer) As clsBeArancel

    <OperationContract()>
    Function Exists(ByVal pIdArancel As Integer) As Boolean

    <OperationContract()>
    Function MaxID() As Integer

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
