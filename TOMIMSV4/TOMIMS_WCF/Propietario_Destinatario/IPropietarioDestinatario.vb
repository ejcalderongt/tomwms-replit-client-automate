Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IPropietarioDestinatario" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IPropietarioDestinatario

    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBePropietario_destinatario) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBePropietario_destinatario) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBePropietario_destinatario) As Integer

    <OperationContract()>
    Function GetAllMail(ByVal pListObjRegla As List(Of Integer)) As List(Of clsBePropietario_destinatario)

    <OperationContract()>
    Function MaxID() As Integer

End Interface
