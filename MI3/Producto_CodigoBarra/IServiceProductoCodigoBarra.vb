Imports System.ServiceModel

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

    <OperationContract()>
    Function MaxID() As Integer

    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
