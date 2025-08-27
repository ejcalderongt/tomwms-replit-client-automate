' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceProductoFamilia

    <OperationContract()>
    Function Insert(ByRef ObjPF As clsBeProducto_familia) As Integer

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPF As List(Of clsBeProducto_familia))

    <OperationContract()>
    Function Update(ByVal ObjPF As clsBeProducto_familia) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjPF As clsBeProducto_familia) As Integer

    <OperationContract()>
    Sub Delete(ByVal pIdFamilia As Integer)

    <OperationContract()>
    Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer)

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pFiltro As String, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_familia)

    <OperationContract()>
    Function Get_Single_By_IdProducto_Familia(ByVal pIdFamilia As Integer) As clsBeProducto_familia

    <OperationContract()>
    Function Get_Single_By_IdProductoFamilia_And_IdPropietario(ByVal pIdFamilia As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_familia

    <OperationContract()>
    Function Exist(ByVal pIdFamilia As Integer) As Boolean

    <OperationContract()>
    Function Exist_Producto_Asociado(ByVal pIdFamilia As Integer) As Boolean

    <OperationContract()>
    Function Max_IdProducto_Familia() As Integer

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
