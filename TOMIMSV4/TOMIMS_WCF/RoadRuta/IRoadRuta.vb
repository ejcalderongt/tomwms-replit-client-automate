Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IRoadRuta" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IRoadRuta


    <OperationContract()>
    Function Insertar(ByVal ObjP As clsBeRoad_ruta) As Integer


    <OperationContract()>
    Function Actualizar(ByVal ObjP As clsBeRoad_ruta) As Integer


    <OperationContract()>
    Function Eliminar(ByRef ObjP As clsBeRoad_ruta) As Integer


    <OperationContract()>
    Function Listar(ByRef ObjP As clsBeRoad_ruta) As DataTable


    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeRoad_ruta)


    <OperationContract()>
    Function GetSingle(ByVal pIdRuta As Integer) As clsBeRoad_ruta


    <OperationContract()>
    Function MaxID() As Integer


End Interface
