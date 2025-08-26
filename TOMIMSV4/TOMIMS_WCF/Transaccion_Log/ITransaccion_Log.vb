Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITransaccionLog" in both code and config file together.
<ServiceContract()>
Public Interface ITransaccion_Log

    <OperationContract()>
    Function GetAll() As List(Of clsBeTransacciones_log)

    <OperationContract()>
    Function Posponer(ByVal pObjListT As List(Of clsBeTransacciones_log)) As Boolean

    <OperationContract()>
    Function EnviarTarea(ByVal pObjListT As List(Of clsBeTransacciones_log), ByVal pHost As String) As Boolean

End Interface
