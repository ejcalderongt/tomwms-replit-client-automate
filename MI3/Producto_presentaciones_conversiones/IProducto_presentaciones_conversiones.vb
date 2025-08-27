Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IProducto_presentaciones_conversiones" in both code and config file together.
<ServiceContract()>
Public Interface IProducto_presentaciones_conversiones

    <OperationContract()>
    Function Max_Id() As Integer

    <OperationContract()>
    Function Get_All() As List(Of clsBeProducto_presentaciones_conversiones)

    <OperationContract()>
    Function Get_Single_By_IdPresentacionOrigen_And_IdPresentacionDestino(ByVal pIdPresentacionOrigen As Integer,
                                               ByVal pIdPresentacionDestino As Integer) As clsBeProducto_presentaciones_conversiones

    <OperationContract()>
    Function Get_Single_By_IdConversion(ByVal pIdConversion As Integer) As clsBeProducto_presentaciones_conversiones

    <OperationContract()>
    Function Insert(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer

    <OperationContract()>
    Function Update(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer

    <OperationContract()>
    Function Delete(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer

End Interface
