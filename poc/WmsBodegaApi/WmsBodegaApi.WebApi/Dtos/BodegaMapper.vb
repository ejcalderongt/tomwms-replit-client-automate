Imports TOMWMS  ' namespace de Entity.dll y DAL.dll del WMS

Namespace Dtos

    ''' <summary>
    ''' Mapeo bidireccional clsBeBodega &lt;-&gt; BodegaDto.
    ''' Asignación 1:1 explícita por propiedad. NO usar AutoMapper en POC para mantener
    ''' el código auditable y sin dependencias mágicas.
    ''' </summary>
    Public Module BodegaMapper

        Public Function ToDto(be As clsBeBodega) As BodegaDto
            If be Is Nothing Then Return Nothing
            Return New BodegaDto With {
                .IdBodega = be.IdBodega,
                .IdPais = be.IdPais,
                .IdEmpresa = be.IdEmpresa,
                .Codigo = be.Codigo,
                .Nombre = be.Nombre,
                .Codigo_barra = be.Codigo_barra,
                .Nombre_comercial = be.Nombre_comercial,
                .Direccion = be.Direccion,
                .Telefono = be.Telefono,
                .Email = be.Email,
                .Encargado = be.Encargado,
                .Ubic_recepcion = be.Ubic_recepcion,
                .Ubic_picking = be.Ubic_picking,
                .Ubic_despacho = be.Ubic_despacho,
                .Ubic_merma = be.Ubic_merma,
                .User_agr = be.User_agr,
                .Fec_agr = be.Fec_agr,
                .User_mod = be.User_mod,
                .Fec_mod = be.Fec_mod,
                .Activo = be.Activo,
                .Coordenada_x = be.Coordenada_x,
                .Coordenada_y = be.Coordenada_y,
                .Largo = be.Largo,
                .Ancho = be.Ancho,
                .Alto = be.Alto,
                .Reservar_stocks_por_linea = be.Reservar_stocks_por_linea,
                .Rechazar_pedido_por_stock = be.Rechazar_pedido_por_stock,
                .IdTipoTransaccion = be.IdTipoTransaccion,
                .Zoom = be.Zoom,
                .IdMotivoUbicacionDanadoPicking = be.IdMotivoUbicacionDañadoPicking,
                .Cambio_ubicacion_auto = be.cambio_ubicacion_auto,
                .Codigo_bodega_erp = be.codigo_bodega_erp,
                .Cuenta_Ingreso_Mercancias = be.Cuenta_Ingreso_Mercancias,
                .Cuenta_Egreso_Mercancias = be.Cuenta_Egreso_Mercancias,
                .Notificacion_Voz = be.Notificacion_Voz,
                .Control_Tarifa_Servicios = be.Control_Tarifa_Servicios,
                .Id_Motivo_Ubic_Reabasto = be.Id_Motivo_Ubic_Reabasto,
                .Es_Bodega_Fiscal = be.Es_Bodega_Fiscal,
                .Habilitar_ingreso_consolidado = be.habilitar_ingreso_consolidado,
                .Captura_estiba_ingreso = be.captura_estiba_ingreso,
                .Captura_pallet_no_estandar = be.captura_pallet_no_estandar,
                .Valor_porcentaje_iva = be.valor_porcentaje_iva,
                .Control_banderas_cliente = be.control_banderas_cliente,
                .Permitir_Verificacion_Consolidada = be.Permitir_Verificacion_Consolidada,
                .IdTamanoEtiquetaUbicacionDefecto = be.IdTamañoEtiquetaUbicacionDefecto,
                .Ubicar_Tarimas_Completas_Reabasto = be.Ubicar_Tarimas_Completas_Reabasto,
                .IdTipoTransaccionSalida = be.IdTipoTransaccionSalida,
                .Permitir_Eliminar_Documento_Salida = be.Permitir_Eliminar_Documento_Salida,
                .Eliminar_Documento_Salida = be.Eliminar_Documento_Salida,
                .Operador_Picking_Realiza_Verificacion = be.Operador_Picking_Realiza_Verificacion,
                .Permitir_Cambio_Ubic_Producto_Picking = be.Permitir_Cambio_Ubic_Producto_Picking,
                .Permitir_Buen_Estado_En_Reemplazo = be.Permitir_Buen_Estado_En_Reemplazo,
                .Cambio_ubicacion_restrictivo = be.cambio_ubicacion_restrictivo,
                .Permitir_cambio_ubic_indice_menor = be.permitir_cambio_ubic_indice_menor,
                .Requerir_mismo_producto_posiciones = be.requerir_mismo_producto_posiciones
            }
        End Function

        Public Function ToEntity(dto As BodegaDto) As clsBeBodega
            If dto Is Nothing Then Return Nothing
            Dim be As New clsBeBodega()
            be.IdBodega = dto.IdBodega
            be.IdPais = dto.IdPais
            be.IdEmpresa = dto.IdEmpresa
            be.Codigo = dto.Codigo
            be.Nombre = dto.Nombre
            be.Codigo_barra = dto.Codigo_barra
            be.Nombre_comercial = dto.Nombre_comercial
            be.Direccion = dto.Direccion
            be.Telefono = dto.Telefono
            be.Email = dto.Email
            be.Encargado = dto.Encargado
            be.Ubic_recepcion = dto.Ubic_recepcion
            be.Ubic_picking = dto.Ubic_picking
            be.Ubic_despacho = dto.Ubic_despacho
            be.Ubic_merma = dto.Ubic_merma
            be.User_agr = dto.User_agr
            be.Fec_agr = dto.Fec_agr
            be.User_mod = dto.User_mod
            be.Fec_mod = dto.Fec_mod
            be.Activo = dto.Activo
            be.Coordenada_x = dto.Coordenada_x
            be.Coordenada_y = dto.Coordenada_y
            be.Largo = dto.Largo
            be.Ancho = dto.Ancho
            be.Alto = dto.Alto
            be.Reservar_stocks_por_linea = dto.Reservar_stocks_por_linea
            be.Rechazar_pedido_por_stock = dto.Rechazar_pedido_por_stock
            be.IdTipoTransaccion = dto.IdTipoTransaccion
            be.Zoom = dto.Zoom
            be.IdMotivoUbicacionDañadoPicking = dto.IdMotivoUbicacionDanadoPicking
            be.cambio_ubicacion_auto = dto.Cambio_ubicacion_auto
            be.codigo_bodega_erp = dto.Codigo_bodega_erp
            be.Cuenta_Ingreso_Mercancias = dto.Cuenta_Ingreso_Mercancias
            be.Cuenta_Egreso_Mercancias = dto.Cuenta_Egreso_Mercancias
            be.Notificacion_Voz = dto.Notificacion_Voz
            be.Control_Tarifa_Servicios = dto.Control_Tarifa_Servicios
            be.Id_Motivo_Ubic_Reabasto = dto.Id_Motivo_Ubic_Reabasto
            be.Es_Bodega_Fiscal = dto.Es_Bodega_Fiscal
            be.habilitar_ingreso_consolidado = dto.Habilitar_ingreso_consolidado
            be.captura_estiba_ingreso = dto.Captura_estiba_ingreso
            be.captura_pallet_no_estandar = dto.Captura_pallet_no_estandar
            be.valor_porcentaje_iva = dto.Valor_porcentaje_iva
            be.control_banderas_cliente = dto.Control_banderas_cliente
            be.Permitir_Verificacion_Consolidada = dto.Permitir_Verificacion_Consolidada
            be.IdTamañoEtiquetaUbicacionDefecto = dto.IdTamanoEtiquetaUbicacionDefecto
            be.Ubicar_Tarimas_Completas_Reabasto = dto.Ubicar_Tarimas_Completas_Reabasto
            be.IdTipoTransaccionSalida = dto.IdTipoTransaccionSalida
            be.Permitir_Eliminar_Documento_Salida = dto.Permitir_Eliminar_Documento_Salida
            be.Eliminar_Documento_Salida = dto.Eliminar_Documento_Salida
            be.Operador_Picking_Realiza_Verificacion = dto.Operador_Picking_Realiza_Verificacion
            be.Permitir_Cambio_Ubic_Producto_Picking = dto.Permitir_Cambio_Ubic_Producto_Picking
            be.Permitir_Buen_Estado_En_Reemplazo = dto.Permitir_Buen_Estado_En_Reemplazo
            be.cambio_ubicacion_restrictivo = dto.Cambio_ubicacion_restrictivo
            be.permitir_cambio_ubic_indice_menor = dto.Permitir_cambio_ubic_indice_menor
            be.requerir_mismo_producto_posiciones = dto.Requerir_mismo_producto_posiciones
            Return be
        End Function

    End Module

End Namespace
