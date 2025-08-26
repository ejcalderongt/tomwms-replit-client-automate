Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IProductoEstadoUbicacion" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IProductoEstadoUbicacion
    <OperationContract()>
    Function Insert(ByRef ObjA As clsBeProducto_estado_ubic) As Integer

    <OperationContract()>
    Function Update(ByVal ObjA As clsBeProducto_estado_ubic) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjA As clsBeProducto_estado_ubic) As Integer

    <OperationContract()>    
    Function Get_All_Filtro(pIdEstado As Integer) As List(Of clsBeProducto_estado_ubic)
End Interface
