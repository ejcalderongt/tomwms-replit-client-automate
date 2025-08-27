Imports System.ServiceModel

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de interfaz "ITransaccionUbicacionHHOperador" en el código y en el archivo de configuración a la vez.
<ServiceContract()>
Public Interface ITransaccionUbicacionHHOperador

    <OperationContract()>
    Function GetAllByTransUbicOp(ByVal pIdTransUbicHhEnc As Integer) As List(Of clsBeTrans_ubic_hh_op)

    <OperationContract()>
    Function Insertar(ByRef ObjA As clsBeTrans_ubic_hh_op) As Integer

    <OperationContract()>
    Function Actualizar(ByVal ObjA As clsBeTrans_ubic_hh_op) As Integer

    <OperationContract()>
    Function Eliminar(ByRef ObjA As clsBeTrans_ubic_hh_op) As Integer

    <OperationContract()>
    Sub DeleteOp(ByVal pIdTransUbicOp As Integer, ByVal pIdTransUbicHhEnc As Integer)

End Interface
