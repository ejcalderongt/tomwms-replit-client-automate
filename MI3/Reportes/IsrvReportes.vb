Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IsrvReportes" in both code and config file together.
<ServiceContract()>
Public Interface IsrvReportes


    <OperationContract()>
    Function Get_Movimientos_Kardex_By_IdEmpresa(pIdEmpresa As Integer, pFechaDel As Date, pFechaAl As Date) As List(Of clsBeVW_Movimientos)

    <OperationContract()>
    Function Get_Stock_By_IdEmpresa(pIdEmpresa As Integer) As List(Of clsBeVW_stock_res)

    <OperationContract()>
    Function Get_Indicadores_Ocupacion_By_IdBodega(pIdBodega As Integer, ByRef UbicacionesVacias As Integer, ByRef UbicacionesOcupadas As Integer) As Boolean

End Interface
