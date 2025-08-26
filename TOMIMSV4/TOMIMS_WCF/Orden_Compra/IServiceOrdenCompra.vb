' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text

<ServiceContract()>
Public Interface IServiceOrdenCompra

    <OperationContract()>
    Function ActualizaDatos(ByVal pObjE As clsBeTrans_oc_enc, _
                            ByVal pListObjTD As List(Of clsBeTrans_oc_det), _
                            ByVal pObjP As clsBeTrans_oc_pol, _
                            ByVal pListObjI As List(Of clsBeTrans_oc_imagen), _
                            ByVal pListObjP As List(Of clsBeProducto)) As Integer

    ' TODO: agregue aquí sus operaciones de servicio

    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function GetSingle(ByVal pIdOrdenCompraEnc As Integer) As clsBeTrans_oc_enc

    <OperationContract()>
    Function GetByImpresion(ByVal pIdOrdenCompra As Integer) As DataTable

    <OperationContract()>
    Function GetOrdenCompraByPropietario(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdPropietarioBodega As Integer) As clsBeTrans_oc_enc

    <OperationContract()>
    Function GetOrdenCompra(ByVal pIdOrdenCompraEnc As Integer) As clsBeTrans_oc_enc

    <OperationContract()>
    Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pIdBodega As Integer = 0, Optional ByVal pIdPropietario As Integer = 0) As DataTable

    <OperationContract()>
    Function GetDetalle(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_det)

    <OperationContract()>
    Function GetImpresionByOC(ByVal pIdOrdenCompra As Integer) As DataTable

    <OperationContract()>
    Function GetCodigos(ByVal pIdBobdega As Integer, ByVal pIdPropietario As Int16) As DataTable

    <OperationContract()>
    Function GetPoliza(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_pol

    <OperationContract()>
    Function GetImagenes(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_imagen)

    <OperationContract()>
    Sub DeleteImageByIndex(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraImg As Integer)

    <OperationContract()>
    Sub Delete(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraDet As Integer)

    <OperationContract()>
    Function Anula(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer) As Boolean

    <OperationContract>
    Function GetAllByOrdenCompra(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_det)

    <OperationContract>
    Function CantidadTransito(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Double

    <OperationContract()>
    Function Tiene_Recepciones_Activas(ByVal pIdOrdenCompraEnc As Integer) As Boolean

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
