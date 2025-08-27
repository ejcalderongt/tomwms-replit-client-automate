Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceProductoSustituto

    <OperationContract()>
    Function Insert(ByVal pObj As clsBeProducto_sustituto) As Boolean

    <OperationContract()>
    Function Update(ByVal pObj As clsBeProducto_sustituto) As Boolean

    <OperationContract()>
    Sub Delete(ByVal pIdProductoSustituto As Integer)

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPS As List(Of clsBeProducto_sustituto))

    <OperationContract()>
    Function Exist(ByVal pIdProductoOriginal As Integer, ByVal pIdProductoPresentacionOriginal As Integer) As Boolean

    <OperationContract()>
    Function Get_All_By_IdProducto(ByVal pIdProductoOriginal As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_sustituto)

    <OperationContract()>
    Function Get_Single_By_IdProductoSustituto(ByVal pIdProductoSustituto As Integer) As clsBeProducto_sustituto

    <OperationContract()>
    Function MaxID() As Integer


    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
