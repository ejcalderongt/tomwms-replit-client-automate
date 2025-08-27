Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "IServicePropietarioReglaRecepcion" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface IServicePropietarioReglaRecepcion

    <OperationContract()>
    Sub Guarda(ByVal pObjRE As clsBePropietario_reglas_enc, ByVal pListObjR As List(Of clsBePropietario_reglas_det))

    <OperationContract()>
    Function GetSingle(ByVal pIdReglaPropietarioEnc As Integer) As clsBePropietario_reglas_enc

    <OperationContract()>
    Function GetAllByEncabezado(ByVal pIdReglaPropietarioEnc As Integer) As List(Of clsBePropietario_reglas_det)

    <OperationContract()>
    Function GetAllDet() As List(Of clsBePropietario_reglas_det)

    <OperationContract()>
    Function GetAllByPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_reglas_enc)

    <OperationContract()>
    Sub DesactivarRegla(ByVal pIdReglaPropietarioEnc As Integer)

    <OperationContract()>
    Sub Desactivar(ByVal pIdReglaPropietarioDet As Integer)

    <OperationContract()>
    Function ExisteRegla(ByVal pIdReglaRecepcion As Integer, ByVal pIdPropietario As Integer) As Boolean

End Interface
