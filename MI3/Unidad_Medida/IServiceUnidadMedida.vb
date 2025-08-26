Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceUnidadMedida

    <OperationContract()>
    Function Insert(ByRef ObjUM As clsBeUnidad_medida) As Integer

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjUM As List(Of clsBeUnidad_medida))

    <OperationContract()>
    Function Update(ByVal ObjUM As clsBeUnidad_medida) As Integer

    <OperationContract()>
    Function Disable(ByRef ObjUM As clsBeUnidad_medida) As Integer

    <OperationContract()>
    Sub Delete(ByVal pIdPropietario As Integer)

    <OperationContract()>
    Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer)

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeUnidad_medida)

    <OperationContract()>
    Function Get_Single_By_Nombre(ByVal pNombreUM As String) As clsBeUnidad_medida

    <OperationContract()>
    Function Get_Single_By_IdUnidadMedida_And_IdPropietario(ByVal pIdUnidadMedida As Integer, ByVal pIdPropietario As Integer) As clsBeUnidad_medida

    <OperationContract()>
    Function Exist(ByVal pIdUnidadMedida As Integer) As Boolean

    <OperationContract()>
    Function Existe_Producto_Asociado(ByVal pIdUnidadMedida As Integer) As Boolean

    <OperationContract()>
    Function Get_Single_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer) As clsBeUnidad_medida

    <OperationContract()>
    Function MAXIdUnidadMedida() As Integer

    <OperationContract()>
    Function Exist_By_Codigo(ByVal pCodigo As String) As Boolean

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
