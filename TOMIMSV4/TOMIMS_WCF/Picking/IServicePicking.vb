Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IServicePicking" in both code and config file together.
<ServiceContract()>
Public Interface IServicePicking


    ''' <summary>
    ''' Servicio Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjE"></param>
    ''' <param name="pObjTA"></param>
    ''' <param name="pListObjD"></param>
    ''' <param name="pObjListP"></param>
    ''' <param name="pListObjO"></param>
    ''' <param name="pListObjU"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function Guardar(ByVal pObjE As clsBeTrans_picking_enc,
                     ByVal pObjTA As clsBeTarea_hh,
                     ByVal pListObjD As List(Of clsBeTrans_picking_det),
                     ByVal pObjListP As List(Of clsBeTrans_picking_det_parametros),
                     ByVal pListObjO As List(Of clsBeTrans_picking_op),
                     ByVal pListObjU As List(Of clsBeTrans_picking_ubic)) As Boolean

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjE"></param>
    ''' <param name="pListObjD"></param>
    ''' <param name="pObjListP"></param>
    ''' <param name="pListObjO"></param>
    ''' <param name="pListObjU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function Eliminar(ByVal pObjE As clsBeTrans_picking_enc, _
                     ByVal pListObjD As List(Of clsBeTrans_picking_det), _
                     ByVal pObjListP As List(Of clsBeTrans_picking_det_parametros), _
                     ByVal pListObjO As List(Of clsBeTrans_picking_op), _
                     ByVal pListObjU As List(Of clsBeTrans_picking_ubic)) As Boolean


    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pIdPicking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAllOperadoresByPicking(ByVal pIdPicking As Integer) As List(Of clsBeTrans_picking_op)


    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pIdOperadorPicking"></param>
    ''' <remarks></remarks>
    <OperationContract()>
    Sub DeleteOp(ByVal pIdOperadorPicking As Integer)


    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable


    <OperationContract()>
    Function GetSingle(ByVal pIdPickingEnc As Integer) As clsBeTrans_picking_enc


    <OperationContract>
    Function MaxID() As Integer


    <OperationContract>
    Function Anula(ByVal pIdPickingEnc As Integer) As Boolean


    <OperationContract>
    Function GetUbicacionPicking(ByVal pIdPicking As Integer, ByVal pIdPedidoEnc As Integer) As DataTable


    <OperationContract>
    Function GetPickingUbicacion(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdPropietarioBodega As Integer) As DataTable

End Interface
