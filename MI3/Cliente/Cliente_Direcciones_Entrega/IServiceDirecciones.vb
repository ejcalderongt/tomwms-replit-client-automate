Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceDirecciones

    <OperationContract()>
    Function Insert(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer

    <OperationContract()>
    Function Update(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer

    <OperationContract()>
    Function Delete(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer

    <OperationContract()>
    Function Get_Single_By_IdClienteDireccion(ByVal IdCliente As Integer, IdDireccion As Integer) As clsBeCliente_direccion

    <OperationContract()>
    Function Get_All_By_IdCliente(ByVal IdCliente As Integer) As List(Of clsBeCliente_direccion)

End Interface
