' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServicePropietario

    <OperationContract()>
    Function Insert(ByRef ObjP As clsBePropietarios) As Integer

    <OperationContract()>
    Function Update(ByVal ObjP As clsBePropietarios) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjP As clsBePropietarios) As Integer

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBePropietarios)

    <OperationContract()>
    Function Get_Single_By_IdPropietario(ByVal pIdPropietario As Integer) As clsBePropietarios

    <OperationContract()>
    Function Exist(ByVal pIdPropietario As Integer) As Boolean

    <OperationContract()>
    Function MAXIdPropietario() As Integer

    ' TODO: agregue aquí sus operaciones de servicio




End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
