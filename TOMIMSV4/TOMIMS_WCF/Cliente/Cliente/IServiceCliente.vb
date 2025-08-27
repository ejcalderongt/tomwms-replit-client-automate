<ServiceContract()>
Public Interface IServiceCliente

    <OperationContract()>
    Sub Insert(ByVal pObjC As clsBeCliente, ByVal pListObjCT As List(Of clsBeCliente_tiempos), ByVal pListObjCB As List(Of clsBeCliente_bodega), ByVal pUbicacionesEntregaCliente As List(Of clsBeCliente_direccion))

    <OperationContract()>
    Sub Disable(ByVal pObjC As clsBeCliente)

    <OperationContract()>
    Function Get_All_Filter(ByVal pActivo As Boolean) As List(Of clsBeCliente)

    <OperationContract()>
    Function Get_Single_By_IdCliente(ByVal pIdCliente As Integer) As clsBeCliente

    <OperationContract()>
    Function Get_Single_By_Propietario(ByVal pIdCliente As Integer, ByVal pIdPropietario As Integer) As clsBeCliente

    <OperationContract()>
    Function Get_All_Tiempos_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_tiempos)

    <OperationContract()>
    Function Get_All_Direcciones_Entrega_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_direccion)

    <OperationContract()>
    Function Get_All_Bodegas_By_Cliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_bodega)

    <OperationContract()>
    Function Max_Id_Dir_Entrega(ByVal pIdCliente As Integer) As Integer    

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
