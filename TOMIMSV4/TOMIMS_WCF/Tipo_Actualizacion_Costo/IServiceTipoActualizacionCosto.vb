Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceTipoActualizacionCosto" in both code and config file together.
<ServiceContract()>
Public Interface IServiceTipoActualizacionCosto

    <OperationContract()>
    Function GetAll() As List(Of clsBeTipo_actualizacion_costo)

    <OperationContract()>
    Function GetSingle(ByVal pIdTipoActualizacionCosto As Integer) As clsBeTipo_actualizacion_costo

    <OperationContract()>
    Function GetParametro(ByVal pIdPropietario As Integer, ByVal pIdProveedor As Integer) As clsBeTipo_actualizacion_costo


End Interface
