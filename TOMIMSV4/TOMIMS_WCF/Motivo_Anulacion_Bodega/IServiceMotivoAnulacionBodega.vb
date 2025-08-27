' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceMotivoAnulacionBodega

    <OperationContract()>
    Function ActualizarDatos(ByVal pObjMD As clsBeMotivo_anulacion, ByVal pListObjMDB As List(Of clsBeMotivo_anulacion_bodega)) As Boolean

    <OperationContract()>
    Function GetAllByMotivoAnulacion(ByVal pIdMotivoAnulacion As Integer) As List(Of clsBeMotivo_anulacion_bodega)

    <OperationContract()>
    Sub GetIdMotivoAnulacionBodega(ByRef BeMotivoAnulacionBodega As clsBeMotivo_anulacion_bodega)
    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
