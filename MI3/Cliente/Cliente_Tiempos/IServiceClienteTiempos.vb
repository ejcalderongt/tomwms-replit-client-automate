' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServiceClienteTiempos" in both code and config file together.
Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceClienteTiempos

    <OperationContract()>
    Function Insert(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer

    <OperationContract()>
    Function Update(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer

    <OperationContract()>
    Function Delete(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer

    <OperationContract()>
    Function Get_Single_By_IdClienteTiempo(ByVal IdClienteTiempo As Integer) As clsBeCliente_tiempos

    <OperationContract()>
    Function Get_All() As List(Of clsBeCliente_tiempos)

    <OperationContract()>
    Function Max_IdTiempoCliente() As Integer

End Interface
