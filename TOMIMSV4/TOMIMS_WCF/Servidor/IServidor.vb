Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServidor" in both code and config file together.
<ServiceContract()>
Public Interface IServidor

    <OperationContract()>
    Function FechaHoraServidor() As String

    <OperationContract()>
    Function FechaServidor() As String

    <OperationContract()>
    Function HoraServidor() As String

End Interface
