Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IMovimiento" in both code and config file together.
<ServiceContract()>
Public Interface IMovimiento

    <OperationContract()>
    Function GetMovimiento(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

End Interface
