Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "ITransacionUbicacionHhDet" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface ITransacionUbicacionHhDet

    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBeTrans_ubic_hh_det) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBeTrans_ubic_hh_det) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBeTrans_ubic_hh_det) As Integer


    <OperationContract()>
    Function MaxID() As Integer

    <OperationContract()>
    Function GetAllByTransUbicEnc(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_det)

    <OperationContract()>
    Function GetSingle(ByVal IdTransUbicHhDet As Integer) As clsBeTrans_ubic_hh_det





End Interface
