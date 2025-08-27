' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServicePropietarioBodega

    <OperationContract()>
    Function Insert_Multiple(ByVal pListObj As List(Of clsBePropietario_destinatario)) As Boolean

    <OperationContract()>
    Function EliminarDestinatario(ByVal pListObjE As List(Of clsBePropietario_destinatario)) As Boolean

    <OperationContract()>
    Function ActualizarDatos(ByVal pObjP As clsBePropietarios,
                             ByVal pListObjP As List(Of clsBePropietario_bodega),
                                    ByVal pListDestinatarios As List(Of clsBePropietario_destinatario)) As Boolean

    <OperationContract()>
    Function GetAllByPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_bodega)

    ' TODO: agregue aquí sus operaciones de servicio


    <OperationContract()>
    Function GetAllByIdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_destinatario)

    <OperationContract()>
    Function DesactivarDestinatario(ByRef ObjA As clsBePropietario_destinatario) As Integer

    <OperationContract()>
    Sub DeleteDestinatario(ByVal pIdDestinatario As Integer)

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
