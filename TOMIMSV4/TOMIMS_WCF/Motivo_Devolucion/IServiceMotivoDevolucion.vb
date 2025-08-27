' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceMotivoDevolucion

    <OperationContract()>
    Function Insertar(ByRef ObjMD As clsBeMotivo_devolucion) As Integer

    <OperationContract()>
    Sub GuardarTransaccion(ByVal pListObjMD As List(Of clsBeMotivo_devolucion))

    <OperationContract()>
    Function Actualizar(ByVal ObjMD As clsBeMotivo_devolucion) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjMD As clsBeMotivo_devolucion) As Integer

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_devolucion)

    <OperationContract()>
    Function GetAllByPropietarioBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion)

    <OperationContract()>
    Function GetAllByPropietarioBodegaDetalle(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeMotivo_devolucion)

    <OperationContract()>
    Function GetSingle(ByVal pIdMotivoDevolucion As Integer) As clsBeMotivo_devolucion

    <OperationContract()>
    Sub Delete(ByVal pIdEmpresa As Integer, ByVal pIdPropietario As Integer)

    <OperationContract()>
    Function Exists(ByVal pIdMotivoDevolucion As Integer) As Boolean

    <OperationContract()>
    Function MAXID() As Integer

    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
