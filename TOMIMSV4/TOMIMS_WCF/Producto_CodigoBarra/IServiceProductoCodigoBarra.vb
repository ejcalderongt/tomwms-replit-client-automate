' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceProductoCodigoBarra

    <OperationContract()>
    Function Insert(ByRef ObjPC As clsBeProducto_codigos_barra) As Boolean

    <OperationContract()>
    Function Update(ByVal ObjPC As clsBeProducto_codigos_barra) As Integer

    <OperationContract()>
    Function Update_Existing(ByVal pObjPC As clsBeProducto_codigos_barra, ByVal pCodigoBarra As String) As Integer

    <OperationContract()>
    Function Exist_Codigo_Barra(ByVal pIdProducto As Integer, ByVal pIdProveedor As Integer, ByVal pCodigoBarra As String) As Boolean

    <OperationContract()>
    Function Delete(ByRef ObjPC As clsBeProducto_codigos_barra) As Integer

    <OperationContract()>
    Sub Disable(ByVal pIdProducto As Integer, ByVal pIdProveedor As Integer, ByVal pCodigoBarra As String)

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra)

    <OperationContract()>
    Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra)

    <OperationContract()>
    Function Get_Single_By_IdProducto(ByVal pIdProducto As Integer, ByVal pIdProveedor As Integer, ByVal pCodigoBarra As String) As clsBeProducto_codigos_barra

    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
