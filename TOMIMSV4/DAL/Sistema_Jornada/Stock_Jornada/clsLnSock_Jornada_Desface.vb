Public Class clsLnSock_Jornada_Desface

    Public Shared Sub Cargar(ByRef oBeSTOCK_JORNADA_DESFACE As clsBeStock_Jornada_Desface, ByRef dr As DataRow)
        Try
            With oBeSTOCK_JORNADA_DESFACE
                .CONSECUTIVO = IIf(IsDBNull(dr.Item("CONSECUTIVO")), 0, dr.Item("CONSECUTIVO"))
                .LIC_PLATE = IIf(IsDBNull(dr.Item("LIC_PLATE")), "", dr.Item("LIC_PLATE"))
                .FECHA = IIf(IsDBNull(dr.Item("FECHA")), New Date(1900, 1, 1), dr.Item("FECHA"))
                .IDSTOCK = IIf(IsDBNull(dr.Item("IDSTOCK")), 0, dr.Item("IDSTOCK"))
                .IDJORNADASISTEMA = IIf(IsDBNull(dr.Item("IDJORNADASISTEMA")), 0, dr.Item("IDJORNADASISTEMA"))
                .IDBODEGA = IIf(IsDBNull(dr.Item("IDBODEGA")), 0, dr.Item("IDBODEGA"))
                .IDTICKETTMS = IIf(IsDBNull(dr.Item("IDTICKETTMS")), 0, dr.Item("IDTICKETTMS"))
                .FECHA_CONSECUTIVA = IIf(IsDBNull(dr.Item("FECHA_CONSECUTIVA")), New Date(1900, 1, 1), dr.Item("FECHA_CONSECUTIVA"))
                .MIN_FECHA = IIf(IsDBNull(dr.Item("MIN_FECHA")), New Date(1900, 1, 1), dr.Item("MIN_FECHA"))
                .MAX_FECHA = IIf(IsDBNull(dr.Item("MAX_FECHA")), New Date(1900, 1, 1), dr.Item("MAX_FECHA"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
