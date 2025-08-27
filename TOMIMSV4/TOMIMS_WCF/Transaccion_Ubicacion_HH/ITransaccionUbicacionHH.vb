Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "ITransaccionUbicacionHH" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface ITransaccionUbicacionHH
    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBeTrans_ubic_hh_enc) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBeTrans_ubic_hh_enc) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBeTrans_ubic_hh_enc) As Integer

    <OperationContract()>
    Function GetAllFiltro(ByVal pActivo As Boolean, ByVal pFechaInicio As DateTime, ByVal pFechaFin As DateTime) As List(Of clsBeTrans_ubic_hh_enc)

    <OperationContract()>
    Function GetAll() As List(Of clsBeTrans_ubic_hh_enc)

    <OperationContract()>
    Function GetSingle(ByVal pIdTransUbicHhEnc As Integer) As clsBeTrans_ubic_hh_enc

    <OperationContract()>
    Function Exists(ByVal pIdTransUbicHhEnc As Integer) As Boolean

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Sub GuardarTransaccion(ByVal pObjEnc As clsBeTrans_ubic_hh_enc, ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det), ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op), ByVal pListObjMov As List(Of clsBeTrans_movimientos), ByVal conHh As Boolean, ByVal pIdPropietario As Integer, ByVal pListObjStock As List(Of clsBeStock), ByVal pListObjTransUbicTarima As List(Of clsBeTrans_ubic_tarima), ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                ByVal IdTareaHH As Integer)

    <OperationContract()>
    Function GetAllByTransUbicEnc(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_det)


    <OperationContract()>
    Function GetAllByTransUbicOp(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_op)

    <OperationContract()>
    Function GetSingleStock(ByVal IdStock As Integer) As clsBeStock

    <OperationContract()>
    Function ActualizarStock(ByVal Obj As clsBeStock) As Integer

    <OperationContract()>
    Function GetDimensionesProductos(ByRef pIdUbicacion As Integer) As List(Of clsBeVW_stock_res)


End Interface
