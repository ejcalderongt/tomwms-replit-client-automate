Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceConfiguracion" in both code and config file together.
<ServiceContract()>
Public Interface IServiceConfiguracion


    <OperationContract()>
    Function GetCST() As List(Of String)


    <OperationContract()>
    Function ModificarCST(ByVal pObjC As Configuracion) As Boolean


    <OperationContract()>
    Function ModificarCSTParcial(ByVal pObjC As Configuracion) As Boolean


    <OperationContract()>
    Function AbreConexion() As Boolean


    <OperationContract()>
    Function ExisteBaseDatos(ByVal pNombreBaseDatos As String) As Boolean

End Interface
