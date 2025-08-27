' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceOrdenCompraTI

    <OperationContract()>
    Function Insertar(ByRef Obj As clsBeTrans_oc_ti) As Integer

    <OperationContract()>
    Function Actualizar(ByVal Obj As clsBeTrans_oc_ti) As Integer

    <OperationContract()>
    Function Eliminar(ByRef Obj As clsBeTrans_oc_ti) As Integer

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeTrans_oc_ti)

    <OperationContract()>
    Function GetSingle(ByVal pIdTipoIngresoOC As Integer) As clsBeTrans_oc_ti

    <OperationContract()>
    Function MaxID() As Integer

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
