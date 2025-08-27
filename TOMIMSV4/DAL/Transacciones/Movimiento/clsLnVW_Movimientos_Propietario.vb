Public Class clsLnVW_Movimientos_propietario

    Public Shared Sub Cargar(ByRef oBeMovimientos_propietario As clsBeVW_Movimientos_Propietario, ByRef dr As DataRow)
        Try
            With oBeMovimientos_propietario
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .TipoTarea = IIf(IsDBNull(dr.Item("TipoTarea")), "", dr.Item("TipoTarea"))
                .idpedidoenc = IIf(IsDBNull(dr.Item("idpedidoenc")), 0, dr.Item("idpedidoenc"))
                .iddespachoenc = IIf(IsDBNull(dr.Item("iddespachoenc")), 0, dr.Item("iddespachoenc"))
                .IdDespachoDet = IIf(IsDBNull(dr.Item("IdDespachoDet")), 0, dr.Item("IdDespachoDet"))
                .observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .no_ticket_tms = IIf(IsDBNull(dr.Item("no_ticket_tms")), "", dr.Item("no_ticket_tms"))
                .poliza = IIf(IsDBNull(dr.Item("poliza")), "", dr.Item("poliza"))
                .numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), "", dr.Item("numero_orden"))
                .referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                .valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0.0, dr.Item("valor_dai"))
                .valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0.0, dr.Item("valor_iva"))
                .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                .Producto = IIf(IsDBNull(dr.Item("Producto")), "", dr.Item("Producto"))
                .presentacion = IIf(IsDBNull(dr.Item("presentacion")), "", dr.Item("presentacion"))
                .UMBas = IIf(IsDBNull(dr.Item("UMBas")), "", dr.Item("UMBas"))
                .cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .Contabilizar = IIf(IsDBNull(dr.Item("Contabilizar")), False, dr.Item("Contabilizar"))
                .fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .licencia = IIf(IsDBNull(dr.Item("licencia")), "", dr.Item("licencia"))
                .Clasificacion = IIf(IsDBNull(dr.Item("Clasificacion")), "", dr.Item("Clasificacion"))
                .Familia = IIf(IsDBNull(dr.Item("Familia")), "", dr.Item("Familia"))
                .IdMovimiento = IIf(IsDBNull(dr.Item("IdMovimiento")), 0, dr.Item("IdMovimiento"))
                .Codigo_Bodega_Origen = IIf(IsDBNull(dr.Item("Codigo_Bodega_Origen")), "", dr.Item("Codigo_Bodega_Origen"))
                .Nombre_Bodega_Origen = IIf(IsDBNull(dr.Item("Nombre_Bodega_Origen")), "", dr.Item("Nombre_Bodega_Origen"))
                .NombreArea = IIf(IsDBNull(dr.Item("NombreArea")), "", dr.Item("NombreArea"))
                .factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .fecha_ingreso_rec = IIf(IsDBNull(dr.Item("fecha_ingreso_rec")), Nothing, dr.Item("fecha_ingreso_rec"))
                .fecha_ingreso_ticket = IIf(IsDBNull(dr.Item("fecha_ingreso_ticket")), Nothing, dr.Item("fecha_ingreso_ticket"))
                .numero_orden_salida = IIf(IsDBNull(dr.Item("numero_orden_salida")), "", dr.Item("numero_orden_salida"))
                .codigo_poliza_salida = IIf(IsDBNull(dr.Item("codigo_poliza_salida")), "", dr.Item("codigo_poliza_salida"))
                .regimen_ingreso = IIf(IsDBNull(dr.Item("regimen_ingreso")), "", dr.Item("regimen_ingreso"))
                .regimen_salida = IIf(IsDBNull(dr.Item("regimen_salida")), "", dr.Item("regimen_salida"))

            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





End Class
