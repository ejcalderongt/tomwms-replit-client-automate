Imports System.Reflection
Imports System.ServiceModel

Public Class srvReportes
    Implements IsrvReportes

    Public Function Get_Movimientos_Kardex_By_IdEmpresa_MI3(ByVal pIdEmpresa As Integer,
                                                            ByVal pFechaDel As Date,
                                                            ByVal pFechaAl As Date) As List(Of clsBeVW_Movimientos) Implements IsrvReportes.Get_Movimientos_Kardex_By_IdEmpresa

        Try

            Return clsLnTrans_movimientos.Get_Movimientos_Kardex_By_IdEmpresa_MI3(pIdEmpresa, pFechaDel, pFechaAl)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Function Get_Stock_By_IdEmpresa(ByVal pIdEmpresa As Integer) As List(Of clsBeVW_stock_res) Implements IsrvReportes.Get_Stock_By_IdEmpresa

        Get_Stock_By_IdEmpresa = Nothing

        Try

            Return clsLnStock.Get_Stock_By_IdEmpresa_MI3(pIdEmpresa)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Function Get_Indicadores_Ocupacion_By_IdBodega(ByVal pIdBodega As Integer,
                                                          ByRef UbicacionesVacias As Integer,
                                                          ByRef UbicacionesOcupadas As Integer) As Boolean Implements IsrvReportes.Get_Indicadores_Ocupacion_By_IdBodega

        Get_Indicadores_Ocupacion_By_IdBodega = False

        Try

            Return clsLnBodega.Get_Indicadores_Ocupacion_By_IdBodega(pIdBodega, UbicacionesVacias, UbicacionesOcupadas)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function


End Class