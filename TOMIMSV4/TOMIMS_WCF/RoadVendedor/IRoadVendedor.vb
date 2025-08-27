Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IRoadVendedor" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IRoadVendedor
    <OperationContract()>
    Function Insertar(ByRef ObjP As clsBeRoad_p_vendedor) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjP As clsBeRoad_p_vendedor) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjP As clsBeRoad_p_vendedor) As Integer

    <OperationContract()>
    Function listar(ByRef ObjP As clsBeRoad_p_vendedor) As DataTable

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_p_vendedor)

    <OperationContract()>
    Function GetSingle(ByVal pIdVendedor As Integer) As clsBeRoad_p_vendedor

    <OperationContract()>
    Function MaxIdVendedor() As Integer

End Interface
