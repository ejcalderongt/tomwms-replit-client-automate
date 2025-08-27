Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IsrvDocumentoIngresoRef" in both code and config file together.
<ServiceContract()>
Public Interface IsrvDocumentoIngresoRef

    <OperationContract()>
    Function Insert(pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer

    <OperationContract()>
    Function Update(pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer

    <OperationContract()>
    Function Delete(pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer

    <OperationContract()>
    Function Get_Single_By_Codigo_Documento(pCodigoDocumento As String) As clsBeTrans_oc_docu_ref

End Interface
