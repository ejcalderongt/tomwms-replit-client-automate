Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IServiceRecepcionInfraccion" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IServiceRecepcionInfraccion

    <OperationContract()>
    Function Guardar(ByVal pListObjRD As List(Of clsBeTrans_re_det_infraccion)) As Boolean

End Interface
