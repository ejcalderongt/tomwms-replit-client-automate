Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IBarras_Pallet" in both code and config file together.
<ServiceContract()>
Public Interface IBarras_Pallet

    <OperationContract()>
    Sub Insert(ByVal pListBarras_Pallet As List(Of clsBeI_nav_barras_pallet))
    <OperationContract()>
    Sub Insert_Single(ByVal pBarra_Pallet As clsBeI_nav_barras_pallet)
    <OperationContract()>
    Sub Update_Single(pBarra_Pallet As clsBeI_nav_barras_pallet)
    <OperationContract()>
    Function Max_IdPallet() As Integer
    <OperationContract()>
    Function Existe(Codigo_Barra_Pallet As String) As Boolean
    <OperationContract()>
    Function Get_All_Activos(pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet)
    <OperationContract()>
    Function Get_All_Pendientes_Recepcion(pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet)

End Interface
