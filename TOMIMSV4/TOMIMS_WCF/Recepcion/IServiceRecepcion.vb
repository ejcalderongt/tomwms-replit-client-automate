' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceRecepcion

    <OperationContract()>
    Function Guardar(ByVal pObjTareaHH As clsBeTarea_hh,
                     ByVal pObjR As clsBeTrans_re_enc, ByVal ObjROC As clsBeTrans_re_oc,
                     ByVal pListObjRD As List(Of clsBeTrans_re_det),
                     ByVal pListObjRDP As List(Of clsBeTrans_re_det_parametros),
                     ByVal pListObjROP As List(Of clsBeTrans_re_op),
                     ByVal pListObjF As List(Of clsBeTrans_re_fact),
                     ByVal pListObjRI As List(Of clsBeTrans_re_img),
                     ByVal pListObjSE As List(Of clsBeStock_se_rec),
                     ByVal pListObjS As List(Of List(Of clsBeStock_rec)),
                     ByVal pListObjPP As List(Of clsBeProducto_pallet),
                     ByVal pIdBodega As Integer) As Integer


    <OperationContract()>
    Function GetImpresionByRecepcion(ByVal pIdRecepcionEnc As Integer) As DataTable


    <OperationContract()>
    Sub CerrarRecepcion(ByVal pRecEnc As clsBeTrans_re_enc,ByVal backoreder As Boolean, ByVal pIdOrdenCompraEnc As Integer, ByVal pIdRecepcionEnc As Integer,
                        ByVal pIdEmpresa As Integer, ByVal pIdBodega As Integer, ByVal pIdUsuario As String,
                        ByVal pListObjDetR As List(Of clsBeTrans_re_det))


    <OperationContract()>
    Function GetAllByRecepcion(ByVal pIdRecepcion As Integer) As List(Of clsBeStock_rec)


    <OperationContract()>
    Function GetAllByIdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_parametro)


    <OperationContract()>
    Function GetAllSeriesByIdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_se)


    <OperationContract()>
    Function GetSingle(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc


    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable


    <OperationContract()>
    Function GetAllSerieByIdStockRec(ByVal pIdStockRec As Integer) As List(Of clsBeStock_se_rec)


    <OperationContract()>
    Function GetAllOperadoresByRecepcion(ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_op)


    <OperationContract()>
    Function GetAllStockRecByRecepcion(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec)


    <OperationContract()>
    Sub DeleteOp(ByVal pIdOperadorRec As Integer, ByVal pIdRecepcionEnc As Integer)


    <OperationContract()>
    Sub DeleteDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer)


    <OperationContract()>
    Sub DeleteImageByIndex(ByVal pIdRecepcionEnc As Integer, ByVal pIdImagen As Integer)


    <OperationContract()>
    Function MaxIdEnc() As Integer


    <OperationContract()>
    Function MaxIdDet(ByVal pIdRecepcionEnc As Integer) As Integer


    <OperationContract()>
    Function MaxIdDetP(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As Integer


    <OperationContract()>
    Function MaxIdImg(ByVal pIdRecepcionEnc As Integer) As Integer


    <OperationContract()>
    Function MaxIdOC(ByVal pIdRecepcionEnc As Integer) As Integer


    <OperationContract()>
    Sub Delete(ByVal pIdFacturaRecepcion As Integer)


    <OperationContract()>
    Function GetAllByBodega(ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_enc)


    <OperationContract()>
    Function GetAllByOrdenCompra(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_re_det)

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
