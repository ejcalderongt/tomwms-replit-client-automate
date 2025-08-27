Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "ITransacion_Ubicacion_Tarima" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface ITransacion_Ubicacion_Tarima

    '<OperationContract()>
    'Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function GetAllByIdEnc(ByVal pIdTarimaTareaEnc As Integer) As List(Of clsBeTrans_ubic_tarima)




End Interface
