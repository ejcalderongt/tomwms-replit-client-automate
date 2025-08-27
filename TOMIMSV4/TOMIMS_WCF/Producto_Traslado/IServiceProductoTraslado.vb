<ServiceContract()>
Public Interface IServiceProductoTraslado

    <OperationContract()>
    Function MaxIDTraslado() As Integer

    <OperationContract()>
    Function Insert(ByRef ObjPF As clsBeTrans_tras_enc) As Integer

    <OperationContract()>
    Function Update(ByVal ObjPF As clsBeTrans_tras_enc) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjPF As clsBeTrans_tras_enc) As Integer

    '<OperationContract()>
    'Sub GuardarTransaccion(ByVal pListObjPF As List(Of clsBeTrans_tras_enc))

    <OperationContract()>
    Function Get_All_Traslados(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

End Interface
