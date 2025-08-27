' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceProductoPresentacion

    <OperationContract()>
    Function Insert(ByRef ObjPP As clsBeProducto_Presentacion) As Boolean

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPP As List(Of clsBeProducto_Presentacion))

    <OperationContract()>
    Function Update(ByVal ObjPP As clsBeProducto_Presentacion) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjPP As clsBeProducto_Presentacion) As Integer

    <OperationContract()>
    Sub Delete(ByVal pIdProducto As Integer)

    <OperationContract()>
    Sub Desactivar(ByVal pIdPresentacion As Integer)

    <OperationContract()>
    Sub Update_Minimo_And_Maximo(ByVal pObjP As clsBeProducto_Presentacion)

    <OperationContract()>
    Function Exist_Stock_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Boolean

    <OperationContract()>
    Function Exist(ByVal pIdPresentacion As Integer) As Boolean

    <OperationContract()>
    Function Existe_Presentacion_By_Codigo_Barra(ByVal pCodigoBarra As String) As Boolean

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Get_Single_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion

    <OperationContract()>
    Function Get_Factor_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion

    <OperationContract()>
    Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Save_Presentacion(ByVal pInsert As Boolean,
                                ByVal oBeProducto_presentacion As clsBeProducto_Presentacion, _
                                ByVal pIdProducto As Integer, ByVal pIdProveedor As Integer, _
                                ByVal pCodigoBarraAnterior As String) As Boolean

    <OperationContract()>
    Function Get_Peso_By_IdPresentacion(ByVal pIdPresentacion As Integer) As Double

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function Get_All_Stock_Con_Presentacion_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion)

    <OperationContract()>
    Function Tiene_Peso_By_IdPresentacion(ByVal pIdPresentacion As Integer) As Double

    <OperationContract()>
    Function Get_Factor_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Double

    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
