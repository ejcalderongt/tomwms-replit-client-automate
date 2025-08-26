Public Class clsLnStockResByReabastecimientoLog

    Public Shared Sub Cargar(ByRef oBeStockResByReabastecimientoLog As clsBeStockResByReabastecimientoLog, ByRef dr As DataRow)
        Try
            With oBeStockResByReabastecimientoLog
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdReabastecimientoLog = IIf(IsDBNull(dr.Item("IdReabastecimientoLog")), 0, dr.Item("IdReabastecimientoLog"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
