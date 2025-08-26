Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IsrvRoadDespacho" in both code and config file together.
<ServiceContract()>
Public Interface IsrvRoadDespacho

    <OperationContract()>
    Function Enviar_Despacho_A_Road(ByRef BeDespachoEnc As clsBeTrans_despacho_enc) As Boolean

End Interface
