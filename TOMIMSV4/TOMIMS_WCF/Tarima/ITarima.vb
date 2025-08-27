Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "ITarima" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface ITarima

    <OperationContract()>
    Function ActualizarEstado(ByVal ObjT As clsBeTarimas) As Integer

End Interface
