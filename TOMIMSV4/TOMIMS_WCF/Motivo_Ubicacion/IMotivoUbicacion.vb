Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IMotivoUbicacion" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IMotivoUbicacion

  
    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBeMotivo_ubicacion) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBeMotivo_ubicacion) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBeMotivo_ubicacion) As Integer

  
    <OperationContract()>
    Function MaxID() As Integer


    <OperationContract()>
    Function GetSingle(ByVal pIdMotivoUbicacion As Integer) As clsBeMotivo_ubicacion


    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeMotivo_ubicacion)

End Interface
