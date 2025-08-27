' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceProveedorBodega

    <OperationContract()>
    Function ActualizaDatos(ByVal pObjMD As clsBeProveedor, ByVal pListObjMDB As List(Of clsBeProveedor_bodega)) As Boolean

    <OperationContract()>
    Function Get_All_By_IdProveedor(ByVal pIdProveedor As Integer) As List(Of clsBeProveedor_bodega)

    <OperationContract()>
    Function Get_Single_By_IdProveedorBodega(ByVal pIdProveedor As Integer) As clsBeProveedor_bodega
    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
