' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceMotivoAnulacion

    <OperationContract()>
    Function Insertar(ByRef ObjMA As clsBeMotivo_anulacion) As Integer

    <OperationContract()>
    Sub GuardarTransaccion(ByVal pListObjMA As List(Of clsBeMotivo_anulacion))

    <OperationContract()>
    Function Actualizar(ByVal ObjMA As clsBeMotivo_anulacion) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjMA As clsBeMotivo_anulacion) As Integer

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_anulacion)

    <OperationContract()>
    Function GetSingle(ByVal pIdMotivoAnulacion As Integer) As clsBeMotivo_anulacion

    <OperationContract()>
    Sub Delete(ByVal pIdEmpresa As Integer)

    <OperationContract()>
    Function Exists(ByVal pIdMotivoAnulacion As Integer) As Boolean

    <OperationContract()>
    Function MAXIdMotivoAnulacion() As Integer

    <OperationContract()>
    Function GetAllByBodega(ByVal pIdBodega As Integer) As List(Of clsBeMotivo_anulacion)
    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
