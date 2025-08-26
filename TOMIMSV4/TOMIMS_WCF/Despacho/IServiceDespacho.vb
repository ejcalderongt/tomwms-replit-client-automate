
' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IServiceDespacho" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IServiceDespacho


    '<OperationContract()>
    'Function Guardar(ByVal pObjE As clsBeTrans_despacho_enc, ByVal pListObj As List(Of clsBeTrans_despacho_det),
    '                 ByVal pIdEmpresa As Integer, ByVal pIdBodega As Integer, ByVal pIdUsuario As Integer) As Boolean


    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable


    <OperationContract()>
    Function GetSingle(ByVal pIdDespachoEnc As Integer) As clsBeTrans_despacho_enc


    <OperationContract()>
    Function Anula(ByVal pIdDespachoEnc As Integer)

End Interface
