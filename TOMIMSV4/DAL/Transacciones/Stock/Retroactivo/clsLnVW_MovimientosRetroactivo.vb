Public Class clsLnVW_MovimientosRetroactivo

    Public Shared Sub Cargar(ByRef oBeVW_movimientos As clsBeVW_MovimientosRetroactivo, ByRef dr As DataRow)

        Try

            With oBeVW_movimientos

                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), 0, dr.Item("Propietario"))
                If dr.Table.Columns.Contains("Producto") Then .Producto = IIf(IsDBNull(dr.Item("Producto")), 0, dr.Item("Producto"))
                If dr.Table.Columns.Contains("Presentacion") Then .Presentacion = IIf(IsDBNull(dr.Item("Presentación") Or dr.Item("Presentacion") = ""), 0, dr.Item("Presentación"))
                If dr.Table.Columns.Contains("EstadoOrigen") Then .EstadoOrigen = IIf(IsDBNull(dr.Item("EstadoOrigen")), 0, dr.Item("EstadoOrigen"))
                If dr.Table.Columns.Contains("EstadoDestino") Then .EstadoDestino = IIf(IsDBNull(dr.Item("EstadoDestino")), 0, dr.Item("EstadoDestino"))
                If dr.Table.Columns.Contains("UMBas") Then .UMBas = IIf(IsDBNull(dr.Item("UMBas")), 0, dr.Item("UMBas"))
                If dr.Table.Columns.Contains("Cantidad") Then .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0, dr.Item("Cantidad"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("Peso")), 0, dr.Item("Peso"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), 0, dr.Item("lote"))
                If dr.Table.Columns.Contains("Fecha_Vence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), New Date(1900, 1, 1), dr.Item("Fecha_Vence"))
                If dr.Table.Columns.Contains("UbicOrigen") Then .UbicOrigen = IIf(IsDBNull(dr.Item("UbicOrigen")), 0, dr.Item("UbicOrigen"))
                If dr.Table.Columns.Contains("UbicDestino") Then .UbicDestino = IIf(IsDBNull(dr.Item("UbicDestino")), 0, dr.Item("UbicDestino"))
                If dr.Table.Columns.Contains("TipoTarea") Then .TipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                If dr.Table.Columns.Contains("IdBodegaOrigen") Then .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                If dr.Table.Columns.Contains("Fecha") Then .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                If dr.Table.Columns.Contains("IdProducto") Then .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                If dr.Table.Columns.Contains("Codigo") Then .Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
                If dr.Table.Columns.Contains("CodigoBarra") Then .CodigoBarra = IIf(IsDBNull(dr.Item("CodigoBarra")), 0, dr.Item("CodigoBarra"))
                If dr.Table.Columns.Contains("IdTipoTarea") Then .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))

                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdEstadoOrigen") Then .IdEstadoOrigen = IIf(IsDBNull(dr.Item("IdEstadoOrigen")), 0, dr.Item("IdEstadoOrigen"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))

                If dr.Table.Columns.Contains("No_Doc_Ingreso") Then .No_Doc_Ingreso = IIf(IsDBNull(dr.Item("No_Doc_Ingreso")), "", dr.Item("No_Doc_Ingreso"))
                If dr.Table.Columns.Contains("No_Ref_Ingreso") Then .No_Ref_Ingreso = IIf(IsDBNull(dr.Item("No_Ref_Ingreso")), "", dr.Item("No_Ref_Ingreso"))
                If dr.Table.Columns.Contains("No_Doc_Salida") Then .No_Doc_Salida = IIf(IsDBNull(dr.Item("No_Doc_Salida")), "", dr.Item("No_Doc_Salida"))
                If dr.Table.Columns.Contains("No_Ref_Salida") Then .No_Ref_Salida = IIf(IsDBNull(dr.Item("No_Ref_Salida")), "", dr.Item("No_Ref_Salida"))

                If dr.Table.Columns.Contains("Ingresos") Then .Ingresos = IIf(IsDBNull(dr.Item("Ingresos")), 0, dr.Item("Ingresos"))
                If dr.Table.Columns.Contains("Salidas") Then .Salidas = IIf(IsDBNull(dr.Item("Salidas")), 0, dr.Item("Salidas"))
                If dr.Table.Columns.Contains("Ajustes_Positivos") Then .Ajustes_Positivos = IIf(IsDBNull(dr.Item("Ajustes_Positivos")), 0, dr.Item("Ajustes_Positivos"))
                If dr.Table.Columns.Contains("Ajustes_Negativos") Then .Ajustes_Negativos = IIf(IsDBNull(dr.Item("Ajustes_Negativos")), 0, dr.Item("Ajustes_Negativos"))
                If dr.Table.Columns.Contains("IdMovimiento") Then .IdMovimiento = IIf(IsDBNull(dr.Item("IdMovimiento")), 0, dr.Item("IdMovimiento"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
