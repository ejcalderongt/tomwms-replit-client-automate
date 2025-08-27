Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnBodega
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeBodega As clsBeBodega, ByRef dr As DataRow)

        Try

            With oBeBodega

                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPais = IIf(IsDBNull(dr.Item("IdPais")), 0, dr.Item("IdPais"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Encargado = IIf(IsDBNull(dr.Item("encargado")), "", dr.Item("encargado"))
                .Ubic_recepcion = IIf(IsDBNull(dr.Item("ubic_recepcion")), "", dr.Item("ubic_recepcion"))
                .Ubic_picking = IIf(IsDBNull(dr.Item("ubic_picking")), "", dr.Item("ubic_picking"))
                .Ubic_despacho = IIf(IsDBNull(dr.Item("ubic_despacho")), "", dr.Item("ubic_despacho"))
                .Ubic_merma = IIf(IsDBNull(dr.Item("ubic_merma")), "", dr.Item("ubic_merma"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Coordenada_x = IIf(IsDBNull(dr.Item("coordenada_x")), "", dr.Item("coordenada_x"))
                .Coordenada_y = IIf(IsDBNull(dr.Item("coordenada_y")), "", dr.Item("coordenada_y"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Reservar_stocks_por_linea = IIf(IsDBNull(dr.Item("reservar_stocks_por_linea")), False, dr.Item("reservar_stocks_por_linea"))
                .Rechazar_pedido_por_stock = IIf(IsDBNull(dr.Item("rechazar_pedido_por_stock")), False, dr.Item("rechazar_pedido_por_stock"))
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), "", dr.Item("IdTipoTransaccion"))
                .Zoom = IIf(IsDBNull(dr.Item("zoom")), 0.0, dr.Item("zoom"))
                .IdMotivoUbicacionDañadoPicking = IIf(IsDBNull(dr.Item("IdMotivoUbicacionDañadoPicking")), 0, dr.Item("IdMotivoUbicacionDañadoPicking"))
                .cambio_ubicacion_auto = IIf(IsDBNull(dr.Item("cambio_ubicacion_auto")), False, dr.Item("cambio_ubicacion_auto"))
                .codigo_bodega_erp = IIf(IsDBNull(dr.Item("codigo_bodega_erp")), "", dr.Item("codigo_bodega_erp"))
                .ubic_producto_ne = IIf(IsDBNull(dr.Item("ubic_producto_ne")), 0, dr.Item("ubic_producto_ne"))
                .IdProductoEstadoNE = IIf(IsDBNull(dr.Item("IdProductoEstadoNE")), 0, dr.Item("IdProductoEstadoNE"))
                .Cuenta_Ingreso_Mercancias = IIf(IsDBNull(dr.Item("Cuenta_Ingreso_Mercancias")), 0, dr.Item("Cuenta_Ingreso_Mercancias"))
                .Cuenta_Egreso_Mercancias = IIf(IsDBNull(dr.Item("Cuenta_Egreso_Mercancias")), 0, dr.Item("Cuenta_Egreso_Mercancias"))
                .Notificacion_Voz = IIf(IsDBNull(dr.Item("Notificacion_Voz")), False, dr.Item("Notificacion_Voz"))
                .Control_Tarifa_Servicios = IIf(IsDBNull(dr.Item("Control_Tarifa_Servicios")), False, dr.Item("Control_Tarifa_Servicios"))
                .Id_Motivo_Ubic_Reabasto = IIf(IsDBNull(dr.Item("Id_Motivo_Ubic_Reabasto")), 0, dr.Item("Id_Motivo_Ubic_Reabasto"))
                .Es_Bodega_Fiscal = IIf(IsDBNull(dr.Item("Es_Bodega_Fiscal")), False, dr.Item("Es_Bodega_Fiscal"))
                .habilitar_ingreso_consolidado = IIf(IsDBNull(dr.Item("habilitar_ingreso_consolidado")), False, dr.Item("habilitar_ingreso_consolidado"))
                .bloquear_lp_hh = IIf(IsDBNull(dr.Item("bloquear_lp_hh")), False, dr.Item("bloquear_lp_hh"))
                .captura_estiba_ingreso = IIf(IsDBNull(dr.Item("captura_estiba_ingreso")), False, dr.Item("captura_estiba_ingreso"))
                .captura_pallet_no_estandar = IIf(IsDBNull(dr.Item("captura_pallet_no_estandar")), False, dr.Item("captura_pallet_no_estandar"))
                .valor_porcentaje_iva = IIf(IsDBNull(dr.Item("valor_porcentaje_iva")), 0, dr.Item("valor_porcentaje_iva"))
                .Permitir_Verificacion_Consolidada = IIf(IsDBNull(dr.Item("Permitir_Verificacion_Consolidada")), False, dr.Item("Permitir_Verificacion_Consolidada"))
                .control_banderas_cliente = IIf(IsDBNull(dr.Item("control_banderas_cliente")), False, dr.Item("control_banderas_cliente"))
                .IdTamañoEtiquetaUbicacionDefecto = IIf(IsDBNull(dr.Item("IdTamañoEtiquetaUbicacionDefecto")), False, dr.Item("IdTamañoEtiquetaUbicacionDefecto"))
                .priorizar_ubicrec_sobre_ubicest = IIf(IsDBNull(dr.Item("priorizar_ubicrec_sobre_ubicest")), False, dr.Item("priorizar_ubicrec_sobre_ubicest"))
                .Ubicar_Tarimas_Completas_Reabasto = IIf(IsDBNull(dr.Item("Ubicar_Tarimas_Completas_Reabasto")), False, dr.Item("Ubicar_Tarimas_Completas_Reabasto"))
                .IdTipoTransaccionSalida = IIf(IsDBNull(dr.Item("IdTipoTransaccionSalida")), 0, dr.Item("IdTipoTransaccionSalida"))
                .Permitir_Eliminar_Documento_Salida = IIf(IsDBNull(dr.Item("Permitir_Eliminar_Documento_Salida")), False, dr.Item("Permitir_Eliminar_Documento_Salida"))
                '#GT03032022_0939:falta este parametro, sino, siempre mostrara falso visualmente, igual que en mostrar area
                .Mostrar_Area_En_HH = IIf(IsDBNull(dr.Item("mostrar_area_en_hh")), False, dr.Item("mostrar_area_en_hh"))
                .control_operador_ubicacion = IIf(IsDBNull(dr.Item("control_operador_ubicacion")), False, dr.Item("control_operador_ubicacion"))
                .confirmar_codigo_en_picking = IIf(IsDBNull(dr.Item("confirmar_codigo_en_picking")), False, dr.Item("confirmar_codigo_en_picking"))
                .inferir_origen_en_cambio_ubic = IIf(IsDBNull(dr.Item("inferir_origen_en_cambio_ubic")), False, dr.Item("inferir_origen_en_cambio_ubic"))
                .Eliminar_Documento_Salida = IIf(IsDBNull(dr.Item("Eliminar_Documento_Salida")), False, dr.Item("Eliminar_Documento_Salida"))
                .Operador_Picking_Realiza_Verificacion = IIf(IsDBNull(dr.Item("Operador_Picking_Realiza_Verificacion")), False, dr.Item("Operador_Picking_Realiza_Verificacion"))
                .Permitir_Cambio_Ubic_Producto_Picking = IIf(IsDBNull(dr.Item("Permitir_Cambio_Ubic_Producto_Picking")), False, dr.Item("Permitir_Cambio_Ubic_Producto_Picking"))
                .despachar_producto_vencido = IIf(IsDBNull(dr.Item("despachar_producto_vencido")), False, dr.Item("despachar_producto_vencido"))
                .tipo_pantalla_picking = IIf(IsDBNull(dr.Item("tipo_pantalla_picking")), 1, dr.Item("tipo_pantalla_picking"))
                .Verificacion_Consolidada = IIf(IsDBNull(dr.Item("Verificacion_Consolidada")), False, dr.Item("Verificacion_Consolidada"))
                .tipo_pantalla_recepcion = IIf(IsDBNull(dr.Item("tipo_pantalla_recepcion")), 1, dr.Item("tipo_pantalla_recepcion"))
                .tipo_pantalla_verificacion = IIf(IsDBNull(dr.Item("tipo_pantalla_verificacion")), 1, dr.Item("tipo_pantalla_verificacion"))
                .Permitir_Buen_Estado_En_Reemplazo = IIf(IsDBNull(dr.Item("permitir_buen_estado_en_reemplazo")), False, dr.Item("permitir_buen_estado_en_reemplazo"))
                .industria_motriz = IIf(IsDBNull(dr.Item("industria_motriz")), False, dr.Item("industria_motriz"))
                .Restringir_Vencimiento_En_Reemplazo = IIf(IsDBNull(dr.Item("Restringir_Vencimiento_En_Reemplazo")), False, dr.Item("Restringir_Vencimiento_En_Reemplazo"))
                .Restringir_Lote_En_Reemplazo = IIf(IsDBNull(dr.Item("Restringir_Lote_En_Reemplazo")), False, dr.Item("Restringir_Lote_En_Reemplazo"))
                .Top_Reabastecimiento_Manual = IIf(IsDBNull(dr.Item("Top_Reabastecimiento_Manual")), 1, dr.Item("Top_Reabastecimiento_Manual"))
                .Permitir_Decimales = IIf(IsDBNull(dr.Item("Permitir_Decimales")), False, dr.Item("Permitir_Decimales"))
                .Dias_Maximo_Vencimiento_Reemplazo = IIf(IsDBNull(dr.Item("Dias_Maximo_Vencimiento_Reemplazo")), 0, dr.Item("Dias_Maximo_Vencimiento_Reemplazo"))
                .Permitir_Repeticiones_En_Ingreso = IIf(IsDBNull(dr.Item("Permitir_Repeticiones_En_Ingreso")), False, dr.Item("Permitir_Repeticiones_En_Ingreso"))
                .Validar_Existencias_Inv_Ini = IIf(IsDBNull(dr.Item("Validar_Existencias_Inv_Ini")), False, dr.Item("Validar_Existencias_Inv_Ini"))
                .Calcular_Ubicacion_Sugerida_ML = IIf(IsDBNull(dr.Item("Calcular_Ubicacion_Sugerida_ML")), False, dr.Item("Calcular_Ubicacion_Sugerida_ML"))
                .validar_disponibilidad_ubicaicon_destino = IIf(IsDBNull(dr.Item("validar_disponibilidad_ubicaicon_destino")), False, dr.Item("validar_disponibilidad_ubicaicon_destino"))
                .Ordenar_Picking_Descendente = IIf(IsDBNull(dr.Item("Ordenar_Picking_Descendente")), False, dr.Item("Ordenar_Picking_Descendente"))
                .Ordenar_Por_Nombre_Completo = IIf(IsDBNull(dr.Item("Ordenar_Por_Nombre_Completo")), False, dr.Item("Ordenar_Por_Nombre_Completo"))
                .Permitir_Reemplazo_Picking = IIf(IsDBNull(dr.Item("permitir_reemplazo_picking")), False, dr.Item("permitir_reemplazo_picking"))
                .Permitir_No_Encontrado_Picking = IIf(IsDBNull(dr.Item("permitir_no_encontrado_picking")), False, dr.Item("permitir_no_encontrado_picking"))
                .Permitir_Reemplazo_Verificacion = IIf(IsDBNull(dr.Item("permitir_reemplazo_verificacion")), False, dr.Item("permitir_reemplazo_verificacion"))
                .Permitir_Reemplazo_Picking_Misma_Licencia = IIf(IsDBNull(dr.Item("Permitir_Reemplazo_Picking_Misma_Licencia")), False, dr.Item("Permitir_Reemplazo_Picking_Misma_Licencia"))
                '#GT02032023: dias antiguedad de un ticket para validar retroactivo
                .Dias_Limite_Retroactivo = IIf(IsDBNull(dr.Item("dias_limite_retroactivo")), 0, dr.Item("dias_limite_retroactivo"))
                .Horario_Ejecucion_Historico = IIf(IsDBNull(dr.Item("horario_ejecucion_historico")), New TimeSpan, dr.Item("horario_ejecucion_historico"))
                .Filtrar_Pedidos_Usuario = IIf(IsDBNull(dr.Item("filtrar_pedidos_usuario")), False, dr.Item("filtrar_pedidos_usuario"))
                .Liberar_Stock_Despachos_Parciales = IIf(IsDBNull(dr.Item("liberar_stock_despachos_parciales")), False, dr.Item("liberar_stock_despachos_parciales"))
                .Homologar_Lote_Vencimiento = IIf(IsDBNull(dr.Item("homologar_lote_vencimiento")), False, dr.Item("homologar_lote_vencimiento"))
                .Escanear_Licencia_Picking = IIf(IsDBNull(dr.Item("escanear_licencia_picking")), False, dr.Item("escanear_licencia_picking"))
                .IdTipoEtiquetaLicencia = IIf(IsDBNull(dr.Item("idtipoetiquetalicencia")), 0, dr.Item("idtipoetiquetalicencia"))
                .IdSimbologiaLicencia = IIf(IsDBNull(dr.Item("idsimbologialicencia")), 0, dr.Item("idsimbologialicencia"))
                .Interface_SAP = IIf(IsDBNull(dr.Item("interface_sap")), 0, dr.Item("interface_sap"))
                .Restringir_Areas_SAP = IIf(IsDBNull(dr.Item("Restringir_Areas_SAP")), False, dr.Item("Restringir_Areas_SAP"))
                .Control_Pallet_Mixto = IIf(IsDBNull(dr.Item("Control_Pallet_Mixto")), False, dr.Item("Control_Pallet_Mixto"))
                .Despacho_Automatico_HH = IIf(IsDBNull(dr.Item("despacho_automatico_hh")), False, dr.Item("despacho_automatico_hh"))
                .Limpiar_Campos = IIf(IsDBNull(dr.Item("limpiar_campos")), False, dr.Item("limpiar_campos"))
                .Permitir_Cambio_Ubic_Recepcion = IIf(IsDBNull(dr.Item("Permitir_Cambio_Ubic_Recepcion")), False, dr.Item("Permitir_Cambio_Ubic_Recepcion"))
                .Ruta_CDN = IIf(IsDBNull(dr.Item("Ruta_CDN")), "", dr.Item("Ruta_CDN"))
                .Rango_Dias_Documentos = IIf(IsDBNull(dr.Item("Rango_Dias_Documentos")), 0, dr.Item("Rango_Dias_Documentos"))
                .Agrupar_Sin_Lic_Veri_No_Cons = IIf(IsDBNull(dr.Item("agrupar_sin_lic_veri_no_cons")), False, dr.Item("agrupar_sin_lic_veri_no_cons"))
                .Advertir_Mpq_Umbas = IIf(IsDBNull(dr.Item("advertir_mpq_umbas")), False, dr.Item("advertir_mpq_umbas"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeBodega As clsBeBodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega")
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpais", "@idpais", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("encargado", "@encargado", DataType.Parametro)
            Ins.Add("ubic_recepcion", "@ubic_recepcion", DataType.Parametro)
            Ins.Add("ubic_picking", "@ubic_picking", DataType.Parametro)
            Ins.Add("ubic_despacho", "@ubic_despacho", DataType.Parametro)
            Ins.Add("ubic_merma", "@ubic_merma", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("coordenada_x", "@coordenada_x", DataType.Parametro)
            Ins.Add("coordenada_y", "@coordenada_y", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("reservar_stocks_por_linea", "@reservar_stocks_por_linea", DataType.Parametro)
            Ins.Add("rechazar_pedido_por_stock", "@rechazar_pedido_por_stock", DataType.Parametro)
            Ins.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Ins.Add("zoom", "@zoom", DataType.Parametro)
            Ins.Add("IdMotivoUbicacionDañadoPicking", "@IdMotivoUbicacionDañadoPicking", DataType.Parametro)
            Ins.Add("codigo_bodega_erp", "@codigo_bodega_erp", DataType.Parametro)
            Ins.Add("cambio_ubicacion_auto", "@cambio_ubicacion_auto", DataType.Parametro)
            Ins.Add("ubic_producto_ne", "@ubic_producto_ne", DataType.Parametro)
            Ins.Add("cuenta_ingreso_mercancias", "@cuenta_ingreso_mercancias", DataType.Parametro)
            Ins.Add("cuenta_egreso_mercancias", "@cuenta_egreso_mercancias", DataType.Parametro)
            Ins.Add("idproductoestadone", "@idproductoestadone", DataType.Parametro)
            Ins.Add("Notificacion_Voz", "@notificacion_voz", DataType.Parametro)
            Ins.Add("Control_Tarifa_Servicios", "@Control_Tarifa_Servicios", DataType.Parametro)
            Ins.Add("Id_Motivo_Ubic_Reabasto", "@Id_Motivo_Ubic_Reabasto", DataType.Parametro)
            Ins.Add("es_bodega_fiscal", "@es_bodega_fiscal", DataType.Parametro)
            Ins.Add("habilitar_ingreso_consolidado", "@habilitar_ingreso_consolidado", DataType.Parametro)
            Ins.Add("bloquear_lp_hh", "@bloquear_lp_hh", DataType.Parametro)
            Ins.Add("captura_estiba_ingreso", "@captura_estiba_ingreso", DataType.Parametro)
            Ins.Add("captura_pallet_no_estandar", "@captura_pallet_no_estandar", DataType.Parametro)
            Ins.Add("valor_porcentaje_iva", "@valor_porcentaje_iva", DataType.Parametro)
            Ins.Add("permitir_verificacion_consolidada", "@permitir_verificacion_consolidada", DataType.Parametro)
            Ins.Add("control_banderas_cliente", "@control_banderas_cliente", DataType.Parametro)
            Ins.Add("IdTamañoEtiquetaUbicacionDefecto", "@IdTamañoEtiquetaUbicacionDefecto", DataType.Parametro)
            Ins.Add("priorizar_ubicrec_sobre_ubicest", "@priorizar_ubicrec_sobre_ubicest", DataType.Parametro)
            Ins.Add("validar_disponibilidad_ubicaicon_destino", "@validar_disponibilidad_ubicaicon_destino", DataType.Parametro)
            Ins.Add("ubicar_tarimas_completas_reabasto", "@ubicar_tarimas_completas_reabasto", DataType.Parametro)
            Ins.Add("IdTipoTransaccionSalida", "@IdTipoTransaccionSalida", DataType.Parametro)
            Ins.Add("Permitir_Eliminar_Documento_Salida", "@Permitir_Eliminar_Documento_Salida", DataType.Parametro)
            Ins.Add("mostrar_area_en_hh", "@mostrar_area_en_hh", DataType.Parametro)
            Ins.Add("confirmar_codigo_en_picking", "@confirmar_codigo_en_picking", DataType.Parametro)
            Ins.Add("control_operador_ubicacion", "@control_operador_ubicacion", DataType.Parametro)
            Ins.Add("inferir_origen_en_cambio_ubic", "@inferir_origen_en_cambio_ubic", DataType.Parametro)
            Ins.Add("eliminar_documento_salida", "@eliminar_documento_salida", DataType.Parametro)
            Ins.Add("operador_picking_realiza_verificacion", "@operador_picking_realiza_verificacion", DataType.Parametro)
            Ins.Add("permitir_cambio_ubic_producto_picking", "@permitir_cambio_ubic_producto_picking", DataType.Parametro)
            Ins.Add("despachar_producto_vencido", "@despachar_producto_vencido", DataType.Parametro)
            Ins.Add("tipo_pantalla_picking", "@tipo_pantalla_picking", DataType.Parametro)
            Ins.Add("verificacion_consolidada", "@verificacion_consolidada", DataType.Parametro)
            Ins.Add("tipo_pantalla_recepcion", "@tipo_pantalla_recepcion", DataType.Parametro)
            Ins.Add("tipo_pantalla_verificacion", "@tipo_pantalla_verificacion", DataType.Parametro)
            Ins.Add("permitir_buen_estado_en_reemplazo", "@permitir_buen_estado_en_reemplazo", DataType.Parametro)
            Ins.Add("industria_motriz", "@industria_motriz", DataType.Parametro)
            Ins.Add("restringir_vencimiento_en_reemplazo", "@restringir_vencimiento_en_reemplazo", DataType.Parametro)
            Ins.Add("restringir_lote_en_reemplazo", "@restringir_lote_en_reemplazo", DataType.Parametro)
            Ins.Add("top_reabastecimiento_manual", "@top_reabastecimiento_manual", DataType.Parametro)
            Ins.Add("permitir_decimales", "@permitir_decimales", DataType.Parametro)
            Ins.Add("dias_maximo_vencimiento_reemplazo", "@dias_maximo_vencimiento_reemplazo", DataType.Parametro)
            Ins.Add("permitir_repeticiones_en_ingreso", "@Permitir_Repeticiones_En_Ingreso", DataType.Parametro)
            Ins.Add("validar_existencias_inv_ini", "@Validar_Existencias_Inv_Ini", DataType.Parametro)
            Ins.Add("calcular_ubicacion_sugerida_ml", "@calcular_ubicacion_sugerida_ml", DataType.Parametro)
            Ins.Add("ordenar_picking_descendente", "@ordenar_picking_descendente", DataType.Parametro)
            Ins.Add("ordenar_por_nombre_completo", "@ordenar_por_nombre_completo", DataType.Parametro)
            Ins.Add("permitir_reemplazo_picking", "@permitir_reemplazo_picking", DataType.Parametro)
            Ins.Add("permitir_no_encontrado_picking", "@permitir_no_encontrado_picking", DataType.Parametro)
            Ins.Add("permitir_reemplazo_verificacion", "@permitir_reemplazo_verificacion", DataType.Parametro)
            Ins.Add("permitir_reemplazo_picking_misma_licencia", "@permitir_reemplazo_picking_misma_licencia", DataType.Parametro)
            Ins.Add("dias_limite_retroactivo", "@dias_limite_retroactivo", DataType.Parametro)
            Ins.Add("horario_ejecucion_historico", "@horario_ejecucion_historico", DataType.Parametro)
            Ins.Add("filtrar_pedidos_usuario", "@filtrar_pedidos_usuario", DataType.Parametro)
            Ins.Add("liberar_stock_despachos_parciales", "@liberar_stock_despachos_parciales", DataType.Parametro)
            Ins.Add("homologar_lote_vencimiento", "@homologar_lote_vencimiento", DataType.Parametro)
            Ins.Add("escanear_licencia_picking", "@escanear_licencia_picking", DataType.Parametro)
            Ins.Add("idtipoetiquetalicencia", "@idtipoetiquetalicencia", DataType.Parametro)
            Ins.Add("idsimbologialicencia", "@idsimbologialicencia", DataType.Parametro)
            Ins.Add("interface_sap", "@interface_sap", DataType.Parametro)
            Ins.Add("restringir_areas_sap", "@restringir_areas_sap", DataType.Parametro)
            Ins.Add("control_pallet_mixto", "@control_pallet_mixto", DataType.Parametro)
            Ins.Add("despacho_automatico_hh", "@despacho_automatico_hh", DataType.Parametro)
            Ins.Add("limpiar_campos", "@limpiar_campos", DataType.Parametro)
            Ins.Add("permitir_cambio_ubic_recepcion", "@permitir_cambio_ubic_recepcion", DataType.Parametro)
            Ins.Add("ruta_cdn", "@ruta_cdn", DataType.Parametro)
            Ins.Add("rango_dias_documentos", "@rango_dias_documentos", DataType.Parametro)
            Ins.Add("agrupar_sin_lic_veri_no_cons", "@agrupar_sin_lic_veri_no_cons", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPAIS", oBeBodega.IdPais))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeBodega.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega.Nombre))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", oBeBodega.Nombre_comercial))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeBodega.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeBodega.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeBodega.Email))
            cmd.Parameters.Add(New SqlParameter("@ENCARGADO", oBeBodega.Encargado))
            cmd.Parameters.Add(New SqlParameter("@UBIC_RECEPCION", oBeBodega.Ubic_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBIC_PICKING", oBeBodega.Ubic_picking))
            cmd.Parameters.Add(New SqlParameter("@UBIC_DESPACHO", oBeBodega.Ubic_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBIC_MERMA", oBeBodega.Ubic_merma))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_X", oBeBodega.Coordenada_x))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_Y", oBeBodega.Coordenada_y))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega.Alto))
            cmd.Parameters.Add(New SqlParameter("@RESERVAR_STOCKS_POR_LINEA", oBeBodega.Reservar_stocks_por_linea))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_PEDIDO_POR_STOCK", oBeBodega.Rechazar_pedido_por_stock))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeBodega.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@ZOOM", oBeBodega.Zoom))
            cmd.Parameters.Add(New SqlParameter("@IdMotivoUbicacionDañadoPicking", oBeBodega.IdMotivoUbicacionDañadoPicking))
            cmd.Parameters.Add(New SqlParameter("@CAMBIO_UBICACION_AUTO", oBeBodega.cambio_ubicacion_auto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP", oBeBodega.codigo_bodega_erp))
            cmd.Parameters.Add(New SqlParameter("@UBIC_PRODUCTO_NE", oBeBodega.ubic_producto_ne))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADONE", oBeBodega.IdProductoEstadoNE))
            cmd.Parameters.Add(New SqlParameter("@CUENTA_INGRESO_MERCANCIAS", oBeBodega.Cuenta_Ingreso_Mercancias))
            cmd.Parameters.Add(New SqlParameter("@CUENTA_EGRESO_MERCANCIAS", oBeBodega.Cuenta_Egreso_Mercancias))
            cmd.Parameters.Add(New SqlParameter("@NOTIFICACION_VOZ", oBeBodega.Notificacion_Voz))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_TARIFA_SERVICIOS", oBeBodega.Control_Tarifa_Servicios))
            cmd.Parameters.Add(New SqlParameter("@ID_MOTIVO_UBIC_REABASTO", oBeBodega.Id_Motivo_Ubic_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@HABILITAR_INGRESO_CONSOLIDADO", oBeBodega.habilitar_ingreso_consolidado))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_FISCAL", oBeBodega.Es_Bodega_Fiscal))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEAR_LP_HH", oBeBodega.bloquear_lp_hh))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_ESTIBA_INGRESO", oBeBodega.captura_estiba_ingreso))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_PALLET_NO_ESTANDAR", oBeBodega.captura_pallet_no_estandar))
            cmd.Parameters.Add(New SqlParameter("@VALOR_PORCENTAJE_IVA", oBeBodega.valor_porcentaje_iva))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_VERIFICACION_CONSOLIDADA", oBeBodega.Permitir_Verificacion_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_BANDERAS_CLIENTE", oBeBodega.control_banderas_cliente))
            cmd.Parameters.Add(New SqlParameter("@IdTamañoEtiquetaUbicacionDefecto", oBeBodega.IdTamañoEtiquetaUbicacionDefecto))
            cmd.Parameters.Add(New SqlParameter("@PRIORIZAR_UBICREC_SOBRE_UBICEST", oBeBodega.priorizar_ubicrec_sobre_ubicest))
            cmd.Parameters.Add(New SqlParameter("@VALIDAR_DISPONIBILIDAD_UBICAICON_DESTINO", oBeBodega.validar_disponibilidad_ubicaicon_destino))
            cmd.Parameters.Add(New SqlParameter("@UBICAR_TARIMAS_COMPLETAS_REABASTO", oBeBodega.Ubicar_Tarimas_Completas_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCIONSALIDA", oBeBodega.IdTipoTransaccionSalida))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_ELIMINAR_DOCUMENTO_SALIDA", oBeBodega.Permitir_Eliminar_Documento_Salida))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_AREA_EN_HH", oBeBodega.Mostrar_Area_En_HH))
            cmd.Parameters.Add(New SqlParameter("@CONFIRMAR_CODIGO_EN_PICKING", oBeBodega.confirmar_codigo_en_picking))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_OPERADOR_UBICACION", oBeBodega.control_operador_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@INFERIR_ORIGEN_EN_CAMBIO_UBIC", oBeBodega.inferir_origen_en_cambio_ubic))
            cmd.Parameters.Add(New SqlParameter("@ELIMINAR_DOCUMENTO_SALIDA", oBeBodega.Eliminar_Documento_Salida))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_PICKING_REALIZA_VERIFICACION", oBeBodega.Operador_Picking_Realiza_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_CAMBIO_UBIC_PRODUCTO_PICKING", oBeBodega.Operador_Picking_Realiza_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_PRODUCTO_VENCIDO", oBeBodega.despachar_producto_vencido))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_PICKING", oBeBodega.tipo_pantalla_picking))
            cmd.Parameters.Add(New SqlParameter("@Verificacion_Consolidada", oBeBodega.Verificacion_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_RECEPCION", oBeBodega.tipo_pantalla_recepcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_VERIFICACION", oBeBodega.tipo_pantalla_verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_BUEN_ESTADO_EN_REEMPLAZO", oBeBodega.Permitir_Buen_Estado_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@INDUSTRIA_MOTRIZ", oBeBodega.industria_motriz))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_VENCIMIENTO_EN_REEMPLAZO", oBeBodega.Restringir_Vencimiento_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_LOTE_EN_REEMPLAZO", oBeBodega.Restringir_Lote_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@TOP_REABASTECIMIENTO_MANUAL", oBeBodega.Top_Reabastecimiento_Manual))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DECIMALES", oBeBodega.Permitir_Decimales))
            cmd.Parameters.Add(New SqlParameter("@DIAS_MAXIMO_VENCIMIENTO_REEMPLAZO", oBeBodega.Dias_Maximo_Vencimiento_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REPETICIONES_EN_INGRESO", oBeBodega.Permitir_Repeticiones_En_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@VALIDAR_EXISTENCIAS_INV_INI", oBeBodega.Validar_Existencias_Inv_Ini))
            cmd.Parameters.Add(New SqlParameter("@CALCULAR_UBICACION_SUGERIDA_ML", oBeBodega.Calcular_Ubicacion_Sugerida_ML))
            cmd.Parameters.Add(New SqlParameter("@ORDENAR_PICKING_DESCENDENTE", oBeBodega.Ordenar_Picking_Descendente))
            cmd.Parameters.Add(New SqlParameter("@ORDENAR_POR_NOMBRE_COMPLETO", oBeBodega.Ordenar_Por_Nombre_Completo))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_PICKING", oBeBodega.Permitir_Reemplazo_Picking))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_NO_ENCONTRADO_PICKING", oBeBodega.Permitir_No_Encontrado_Picking))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_VERIFICACION", oBeBodega.Permitir_Reemplazo_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_PICKING_MISMA_LICENCIA", oBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia))
            '#GT02032023: indica cuantos dias de antigüedad se permiten validar para historico 
            cmd.Parameters.Add(New SqlParameter("@DIAS_LIMITE_RETROACTIVO", oBeBodega.Dias_Limite_Retroactivo))
            cmd.Parameters.Add(New SqlParameter("@HORARIO_EJECUCION_HISTORICO", oBeBodega.Horario_Ejecucion_Historico))
            cmd.Parameters.Add(New SqlParameter("@FILTRAR_PEDIDOS_USUARIO", oBeBodega.Filtrar_Pedidos_Usuario))
            cmd.Parameters.Add(New SqlParameter("@LIBERAR_STOCK_DESPACHOS_PARCIALES", oBeBodega.Liberar_Stock_Despachos_Parciales))
            cmd.Parameters.Add(New SqlParameter("@HOMOLOGAR_LOTE_VENCIMIENTO", oBeBodega.Homologar_Lote_Vencimiento))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_LICENCIA_PICKING", oBeBodega.Escanear_Licencia_Picking))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETALICENCIA", oBeBodega.IdTipoEtiquetaLicencia))
            cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIALICENCIA", oBeBodega.IdSimbologiaLicencia))
            cmd.Parameters.Add(New SqlParameter("@INTERFACE_SAP", oBeBodega.Interface_SAP))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_AREAS_SAP", oBeBodega.Restringir_Areas_SAP))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PALLET_MIXTO", oBeBodega.Control_Pallet_Mixto))
            cmd.Parameters.Add(New SqlParameter("@DESPACHO_AUTOMATICO_HH", oBeBodega.Despacho_Automatico_HH))
            cmd.Parameters.Add(New SqlParameter("@LIMPIAR_CAMPOS", oBeBodega.Limpiar_Campos))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_CAMBIO_UBIC_RECEPCION", oBeBodega.Permitir_Cambio_Ubic_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@RUTA_CDN", oBeBodega.Ruta_CDN))
            cmd.Parameters.Add(New SqlParameter("@RANGO_DIAS_DOCUMENTOS", oBeBodega.Rango_Dias_Documentos))
            cmd.Parameters.Add(New SqlParameter("@AGRUPAR_SIN_LIC_VERI_NO_CONS", oBeBodega.Agrupar_Sin_Lic_Veri_No_Cons))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeBodega As clsBeBodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpais", "@idpais", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("encargado", "@encargado", DataType.Parametro)
            Upd.Add("ubic_recepcion", "@ubic_recepcion", DataType.Parametro)
            Upd.Add("ubic_picking", "@ubic_picking", DataType.Parametro)
            Upd.Add("ubic_despacho", "@ubic_despacho", DataType.Parametro)
            Upd.Add("ubic_merma", "@ubic_merma", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("coordenada_x", "@coordenada_x", DataType.Parametro)
            Upd.Add("coordenada_y", "@coordenada_y", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("reservar_stocks_por_linea", "@reservar_stocks_por_linea", DataType.Parametro)
            Upd.Add("rechazar_pedido_por_stock", "@rechazar_pedido_por_stock", DataType.Parametro)
            Upd.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Upd.Add("zoom", "@zoom", DataType.Parametro)
            Upd.Add("IdMotivoUbicacionDañadoPicking", "@IdMotivoUbicacionDañadoPicking", DataType.Parametro)
            Upd.Add("cambio_ubicacion_auto", "@cambio_ubicacion_auto", DataType.Parametro)
            Upd.Add("codigo_bodega_erp", "@codigo_bodega_erp", DataType.Parametro)
            Upd.Add("ubic_producto_ne", "@ubic_producto_ne", DataType.Parametro)
            Upd.Add("idproductoestadone", "@idproductoestadone", DataType.Parametro)
            Upd.Add("cuenta_ingreso_mercancias", "@cuenta_ingreso_mercancias", DataType.Parametro)
            Upd.Add("cuenta_egreso_mercancias", "@cuenta_egreso_mercancias", DataType.Parametro)
            Upd.Add("Notificacion_Voz", "@notificacion_voz", DataType.Parametro)
            Upd.Add("Control_Tarifa_Servicios", "@Control_Tarifa_Servicios", DataType.Parametro)
            Upd.Add("es_bodega_fiscal", "@es_bodega_fiscal", DataType.Parametro)
            Upd.Add("habilitar_ingreso_consolidado", "@habilitar_ingreso_consolidado", DataType.Parametro)
            Upd.Add("Id_Motivo_Ubic_Reabasto", "@Id_Motivo_Ubic_Reabasto", DataType.Parametro)
            Upd.Add("bloquear_lp_hh", "@bloquear_lp_hh", DataType.Parametro)
            Upd.Add("captura_estiba_ingreso", "@captura_estiba_ingreso", DataType.Parametro)
            Upd.Add("captura_pallet_no_estandar", "@captura_pallet_no_estandar", DataType.Parametro)
            Upd.Add("valor_porcentaje_iva", "@valor_porcentaje_iva", DataType.Parametro)
            Upd.Add("permitir_verificacion_consolidada", "@permitir_verificacion_consolidada", DataType.Parametro)
            Upd.Add("control_banderas_cliente", "@control_banderas_cliente", DataType.Parametro)
            Upd.Add("IdTamañoEtiquetaUbicacionDefecto", "@IdTamañoEtiquetaUbicacionDefecto", DataType.Parametro)
            Upd.Add("priorizar_ubicrec_sobre_ubicest", "@priorizar_ubicrec_sobre_ubicest", DataType.Parametro)
            Upd.Add("validar_disponibilidad_ubicaicon_destino", "@validar_disponibilidad_ubicaicon_destino", DataType.Parametro)
            Upd.Add("ubicar_tarimas_completas_reabasto", "@ubicar_tarimas_completas_reabasto", DataType.Parametro)
            Upd.Add("IdTipoTransaccionSalida", "@IdTipoTransaccionSalida", DataType.Parametro)
            Upd.Add("permitir_eliminar_documento_salida", "@permitir_eliminar_documento_salida", DataType.Parametro)
            Upd.Add("mostrar_area_en_hh", "@mostrar_area_en_hh", DataType.Parametro)
            Upd.Add("confirmar_codigo_en_picking", "@confirmar_codigo_en_picking", DataType.Parametro)
            Upd.Add("control_operador_ubicacion", "@control_operador_ubicacion", DataType.Parametro)
            Upd.Add("inferir_origen_en_cambio_ubic", "@inferir_origen_en_cambio_ubic", DataType.Parametro)
            Upd.Add("eliminar_documento_salida", "@eliminar_documento_salida", DataType.Parametro)
            Upd.Add("operador_picking_realiza_verificacion", "@operador_picking_realiza_verificacion", DataType.Parametro)
            Upd.Add("permitir_cambio_ubic_producto_picking", "@permitir_cambio_ubic_producto_picking", DataType.Parametro)
            Upd.Add("despachar_producto_vencido", "@despachar_producto_vencido", DataType.Parametro)
            Upd.Add("tipo_pantalla_picking", "@tipo_pantalla_picking", DataType.Parametro)
            Upd.Add("verificacion_consolidada", "@verificacion_consolidada", DataType.Parametro)
            Upd.Add("tipo_pantalla_recepcion", "@tipo_pantalla_recepcion", DataType.Parametro)
            Upd.Add("tipo_pantalla_verificacion", "@tipo_pantalla_verificacion", DataType.Parametro)
            Upd.Add("permitir_buen_estado_en_reemplazo", "@permitir_buen_estado_en_reemplazo", DataType.Parametro)
            Upd.Add("industria_motriz", "@industria_motriz", DataType.Parametro)
            Upd.Add("restringir_vencimiento_en_reemplazo", "@restringir_vencimiento_en_reemplazo", DataType.Parametro)
            Upd.Add("restringir_lote_en_reemplazo", "@restringir_lote_en_reemplazo", DataType.Parametro)
            Upd.Add("top_reabastecimiento_manual", "@top_reabastecimiento_manual", DataType.Parametro)
            Upd.Add("permitir_decimales", "@permitir_decimales", DataType.Parametro)
            Upd.Add("dias_maximo_vencimiento_reemplazo", "@dias_maximo_vencimiento_reemplazo", DataType.Parametro)
            Upd.Add("permitir_repeticiones_en_ingreso", "@permitir_repeticiones_en_ingreso", DataType.Parametro)
            Upd.Add("validar_existencias_inv_ini", "@Validar_Existencias_Inv_Ini", DataType.Parametro)
            Upd.Add("calcular_ubicacion_sugerida_ml", "@calcular_ubicacion_sugerida_ml", DataType.Parametro)
            Upd.Add("permitir_reemplazo_picking", "@permitir_reemplazo_picking", DataType.Parametro)
            Upd.Add("permitir_no_encontrado_picking", "@permitir_no_encontrado_picking", DataType.Parametro)
            Upd.Add("permitir_reemplazo_verificacion", "@permitir_reemplazo_verificacion", DataType.Parametro)
            Upd.Add("permitir_reemplazo_picking_misma_licencia", "@permitir_reemplazo_picking_misma_licencia", DataType.Parametro)
            Upd.Add("dias_limite_retroactivo", "@dias_limite_retroactivo", DataType.Parametro)
            Upd.Add("horario_ejecucion_historico", "@horario_ejecucion_historico", DataType.Parametro)
            Upd.Add("filtrar_pedidos_usuario", "@filtrar_pedidos_usuario", DataType.Parametro)
            Upd.Add("liberar_stock_despachos_parciales", "@liberar_stock_despachos_parciales", DataType.Parametro)
            Upd.Add("homologar_lote_vencimiento", "@homologar_lote_vencimiento", DataType.Parametro)
            Upd.Add("escanear_licencia_picking", "@escanear_licencia_picking", DataType.Parametro)
            Upd.Add("idtipoetiquetalicencia", "@idtipoetiquetalicencia", DataType.Parametro)
            Upd.Add("idsimbologialicencia", "@idsimbologialicencia", DataType.Parametro)
            Upd.Add("interface_sap", "@interface_sap", DataType.Parametro)
            Upd.Add("restringir_areas_sap", "@restringir_areas_sap", DataType.Parametro)
            Upd.Add("control_pallet_mixto", "@control_pallet_mixto", DataType.Parametro)
            Upd.Add("despacho_automatico_hh", "@despacho_automatico_hh", DataType.Parametro)
            Upd.Add("limpiar_campos", "@limpiar_campos", DataType.Parametro)
            Upd.Add("permitir_cambio_ubic_recepcion", "@permitir_cambio_ubic_recepcion", DataType.Parametro)
            Upd.Add("ruta_cdn", "@ruta_cdn", DataType.Parametro)
            Upd.Add("rango_dias_documentos", "@rango_dias_documentos", DataType.Parametro)
            Upd.Add("agrupar_sin_lic_veri_no_cons", "@agrupar_sin_lic_veri_no_cons", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPAIS", oBeBodega.IdPais))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeBodega.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega.Nombre))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", oBeBodega.Nombre_comercial))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeBodega.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeBodega.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeBodega.Email))
            cmd.Parameters.Add(New SqlParameter("@ENCARGADO", oBeBodega.Encargado))
            cmd.Parameters.Add(New SqlParameter("@UBIC_RECEPCION", oBeBodega.Ubic_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBIC_PICKING", oBeBodega.Ubic_picking))
            cmd.Parameters.Add(New SqlParameter("@UBIC_DESPACHO", oBeBodega.Ubic_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBIC_MERMA", oBeBodega.Ubic_merma))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_X", oBeBodega.Coordenada_x))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_Y", oBeBodega.Coordenada_y))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega.Alto))
            cmd.Parameters.Add(New SqlParameter("@RESERVAR_STOCKS_POR_LINEA", oBeBodega.Reservar_stocks_por_linea))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_PEDIDO_POR_STOCK", oBeBodega.Rechazar_pedido_por_stock))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeBodega.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@ZOOM", oBeBodega.Zoom))
            cmd.Parameters.Add(New SqlParameter("@IdMotivoUbicacionDañadoPicking", oBeBodega.IdMotivoUbicacionDañadoPicking))
            cmd.Parameters.Add(New SqlParameter("@CAMBIO_UBICACION_AUTO", oBeBodega.cambio_ubicacion_auto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP", oBeBodega.codigo_bodega_erp))
            cmd.Parameters.Add(New SqlParameter("@UBIC_PRODUCTO_NE", oBeBodega.ubic_producto_ne))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADONE", oBeBodega.IdProductoEstadoNE))
            cmd.Parameters.Add(New SqlParameter("@CUENTA_INGRESO_MERCANCIAS", oBeBodega.Cuenta_Ingreso_Mercancias))
            cmd.Parameters.Add(New SqlParameter("@CUENTA_EGRESO_MERCANCIAS", oBeBodega.Cuenta_Egreso_Mercancias))
            cmd.Parameters.Add(New SqlParameter("@NOTIFICACION_VOZ", oBeBodega.Notificacion_Voz))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_TARIFA_SERVICIOS", oBeBodega.Control_Tarifa_Servicios))
            cmd.Parameters.Add(New SqlParameter("@ID_MOTIVO_UBIC_REABASTO", oBeBodega.Id_Motivo_Ubic_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_FISCAL", oBeBodega.Es_Bodega_Fiscal))
            cmd.Parameters.Add(New SqlParameter("@HABILITAR_INGRESO_CONSOLIDADO", oBeBodega.habilitar_ingreso_consolidado))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEAR_LP_HH", oBeBodega.bloquear_lp_hh))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_ESTIBA_INGRESO", oBeBodega.captura_estiba_ingreso))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_PALLET_NO_ESTANDAR", oBeBodega.captura_pallet_no_estandar))
            cmd.Parameters.Add(New SqlParameter("@VALOR_PORCENTAJE_IVA", oBeBodega.valor_porcentaje_iva))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_VERIFICACION_CONSOLIDADA", oBeBodega.Permitir_Verificacion_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_BANDERAS_CLIENTE", oBeBodega.control_banderas_cliente))
            cmd.Parameters.Add(New SqlParameter("@IdTamañoEtiquetaUbicacionDefecto", oBeBodega.IdTamañoEtiquetaUbicacionDefecto))
            cmd.Parameters.Add(New SqlParameter("@PRIORIZAR_UBICREC_SOBRE_UBICEST", oBeBodega.priorizar_ubicrec_sobre_ubicest))
            cmd.Parameters.Add(New SqlParameter("@VALIDAR_DISPONIBILIDAD_UBICAICON_DESTINO", oBeBodega.validar_disponibilidad_ubicaicon_destino))
            cmd.Parameters.Add(New SqlParameter("@UBICAR_TARIMAS_COMPLETAS_REABASTO", oBeBodega.Ubicar_Tarimas_Completas_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCIONSALIDA", oBeBodega.IdTipoTransaccionSalida))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_ELIMINAR_DOCUMENTO_SALIDA", oBeBodega.Permitir_Eliminar_Documento_Salida))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_AREA_EN_HH", oBeBodega.Mostrar_Area_En_HH))
            cmd.Parameters.Add(New SqlParameter("@CONFIRMAR_CODIGO_EN_PICKING", oBeBodega.confirmar_codigo_en_picking))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_OPERADOR_UBICACION", oBeBodega.control_operador_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@INFERIR_ORIGEN_EN_CAMBIO_UBIC", oBeBodega.inferir_origen_en_cambio_ubic))
            cmd.Parameters.Add(New SqlParameter("@ELIMINAR_DOCUMENTO_SALIDA", oBeBodega.Eliminar_Documento_Salida))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_PICKING_REALIZA_VERIFICACION", oBeBodega.Operador_Picking_Realiza_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_CAMBIO_UBIC_PRODUCTO_PICKING", oBeBodega.Permitir_Cambio_Ubic_Producto_Picking))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_PRODUCTO_VENCIDO", oBeBodega.despachar_producto_vencido))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_PICKING", oBeBodega.tipo_pantalla_picking))
            cmd.Parameters.Add(New SqlParameter("@Verificacion_Consolidada", oBeBodega.Verificacion_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_RECEPCION", oBeBodega.tipo_pantalla_recepcion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PANTALLA_VERIFICACION", oBeBodega.tipo_pantalla_verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_BUEN_ESTADO_EN_REEMPLAZO", oBeBodega.Permitir_Buen_Estado_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@INDUSTRIA_MOTRIZ", oBeBodega.industria_motriz))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_VENCIMIENTO_EN_REEMPLAZO", oBeBodega.Restringir_Vencimiento_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_LOTE_EN_REEMPLAZO", oBeBodega.Restringir_Lote_En_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@TOP_REABASTECIMIENTO_MANUAL", oBeBodega.Top_Reabastecimiento_Manual))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DECIMALES", oBeBodega.Permitir_Decimales))
            cmd.Parameters.Add(New SqlParameter("@DIAS_MAXIMO_VENCIMIENTO_REEMPLAZO", oBeBodega.Dias_Maximo_Vencimiento_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REPETICIONES_EN_INGRESO", oBeBodega.Permitir_Repeticiones_En_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@VALIDAR_EXISTENCIAS_INV_INI", oBeBodega.Validar_Existencias_Inv_Ini))
            cmd.Parameters.Add(New SqlParameter("@CALCULAR_UBICACION_SUGERIDA_ML", oBeBodega.Calcular_Ubicacion_Sugerida_ML))
            cmd.Parameters.Add(New SqlParameter("@ORDENAR_PICKING_DESCENDENTE", oBeBodega.Ordenar_Picking_Descendente))
            cmd.Parameters.Add(New SqlParameter("@ORDENAR_POR_NOMBRE_COMPLETO", oBeBodega.Ordenar_Por_Nombre_Completo))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_PICKING", oBeBodega.Permitir_Reemplazo_Picking))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_NO_ENCONTRADO_PICKING", oBeBodega.Permitir_No_Encontrado_Picking))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_VERIFICACION", oBeBodega.Permitir_Reemplazo_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_REEMPLAZO_PICKING_MISMA_LICENCIA", oBeBodega.Permitir_Reemplazo_Picking_Misma_Licencia))
            cmd.Parameters.Add(New SqlParameter("@DIAS_LIMITE_RETROACTIVO", oBeBodega.Dias_Limite_Retroactivo))
            cmd.Parameters.Add(New SqlParameter("@HORARIO_EJECUCION_HISTORICO", oBeBodega.Horario_Ejecucion_Historico))
            cmd.Parameters.Add(New SqlParameter("@FILTRAR_PEDIDOS_USUARIO", oBeBodega.Filtrar_Pedidos_Usuario))
            cmd.Parameters.Add(New SqlParameter("@LIBERAR_STOCK_DESPACHOS_PARCIALES", oBeBodega.Liberar_Stock_Despachos_Parciales))
            cmd.Parameters.Add(New SqlParameter("@HOMOLOGAR_LOTE_VENCIMIENTO", oBeBodega.Homologar_Lote_Vencimiento))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_LICENCIA_PICKING", oBeBodega.Escanear_Licencia_Picking))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETALICENCIA", oBeBodega.IdTipoEtiquetaLicencia))
            cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIALICENCIA", oBeBodega.IdSimbologiaLicencia))
            cmd.Parameters.Add(New SqlParameter("@INTERFACE_SAP", oBeBodega.Interface_SAP))
            cmd.Parameters.Add(New SqlParameter("@RESTRINGIR_AREAS_SAP", oBeBodega.Restringir_Areas_SAP))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PALLET_MIXTO", oBeBodega.Control_Pallet_Mixto))
            cmd.Parameters.Add(New SqlParameter("@DESPACHO_AUTOMATICO_HH", oBeBodega.Despacho_Automatico_HH))
            cmd.Parameters.Add(New SqlParameter("@LIMPIAR_CAMPOS", oBeBodega.Limpiar_Campos))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_CAMBIO_UBIC_RECEPCION", oBeBodega.Permitir_Cambio_Ubic_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@RUTA_CDN", oBeBodega.Ruta_CDN))
            cmd.Parameters.Add(New SqlParameter("@RANGO_DIAS_DOCUMENTOS", oBeBodega.Rango_Dias_Documentos))
            cmd.Parameters.Add(New SqlParameter("@AGRUPAR_SIN_LIC_VERI_NO_CONS", oBeBodega.Agrupar_Sin_Lic_Veri_No_Cons))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_IdUbicacion_Recepcion(ByVal IdUbicacionRecepcion As Integer,
                                                            ByVal IdBodega As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("ubic_recepcion", "@ubic_recepcion", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@UBIC_RECEPCION", IdUbicacionRecepcion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_IdUbicacion_Despacho(ByVal IdUbicacionDespacho As Integer,
                                                           ByVal IdBodega As Integer,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("ubic_despacho", "@ubic_despacho", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@UBIC_DESPACHO", IdUbicacionDespacho))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_IdUbicacion_Picking(ByVal pIdUbicacionPicking As Integer, ByVal IdBodega As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("ubic_picking", "@ubic_picking", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ubic_picking", pIdUbicacionPicking))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_IdUbicacion_Merma(ByVal pIdUbicacionMerma As Integer, ByVal IdBodega As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("ubic_merma", "@ubic_merma", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ubic_merma", pIdUbicacionMerma))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeBodega As clsBeBodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega" &
             "  Where(IdBodega = @IdBodega)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar(ByVal IdEmpresa As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT idbodega, nombre from bodega where IdEmpresa = @IdEmpresa and activo=1"
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try


    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_Bodegas_Activas() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega WHERE Activo = 1"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try


    End Function

    Public Shared Function Obtener(ByRef oBeBodega As clsBeBodega) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega " &
            " Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeBodega)
            Const sp As String = "SELECT * FROM Bodega WHERE Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega

            For Each lRow As DataRow In dt.Rows
                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, lRow)
                lReturnList.Add(vBeBodega)
            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeBodega As clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega " &
                                 " Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega, dt.Rows(0))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Function GetSingle_By_Idbodega(ByVal pIdBodega As Integer) As clsBeBodega

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle_By_Idbodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                GetSingle_By_Idbodega = pBeBodega
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function
    Public Shared Function GetSingle_By_Idbodega(ByVal pIdBodega As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeBodega

        GetSingle_By_Idbodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(IdBodega = @IdBodega) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                Return pBeBodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_Codigo(ByVal pCodigo As String,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As clsBeBodega

        GetSingle_By_Codigo = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(Codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo", pCodigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                Return pBeBodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_CodigoArea(ByVal pCodigoArea As String,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As clsBeBodega

        GetSingle_By_CodigoArea = Nothing

        Try

            Const sp As String = "SELECT b.* 
                                  FROM bodega b INNER JOIN bodega_area ba ON b.IdBodega = ba.IdBodega
                                  Where(ba.Codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo", pCodigoArea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                Return pBeBodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As clsBeBodega

        GetSingle_By_IdPickingEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(IdBodega in (Select IdBodega From trans_picking_enc Where IdPickingEnc = @IdPickingEnc )) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                Return pBeBodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function GetSingle_By_Codigo_Or_IdBodega(ByVal pCodigo As String,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As clsBeBodega

        GetSingle_By_Codigo_Or_IdBodega = Nothing

        Try
            Dim sp As String = ""

            If IsNumeric(pCodigo) Then
                sp = "SELECT * FROM Bodega Where (IdBodega = @Codigo) "
            Else
                sp = "SELECT * FROM Bodega Where (codigo = @Codigo) "
            End If



            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo", pCodigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                Return pBeBodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Control_Banderas_Cliente(ByVal pIdBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Boolean

        Get_Control_Banderas_Cliente = False

        Try

            Const sp As String = "SELECT control_banderas_cliente FROM Bodega 
                                  Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Control_Banderas_Cliente = IIf(IsDBNull(dt.Rows(0).Item("control_banderas_cliente")), False, dt.Rows(0).Item("control_banderas_cliente"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeBodega As clsBeBodega, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Bodega Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega, dt.Rows(0))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    '#GT20062022: BODEGA por el código sin tran
    Public Shared Function GetSingle_By_Codigo(ByVal pCodigoBodega As String) As clsBeBodega

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle_By_Codigo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(CODIGO = @CODIGO)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", pCodigoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                GetSingle_By_Codigo = pBeBodega
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    '#GT20062022: BODEGA por el código sin tran
    Public Shared Function GetSingle_By_Codigobodega(ByVal pCodigoBodega As String) As clsBeBodega

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle_By_Codigobodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega 
                                  Where(CODIGO = @CODIGO)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", pCodigoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega As New clsBeBodega
                Cargar(pBeBodega, dt.Rows(0))
                GetSingle_By_Codigobodega = pBeBodega
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetRutaCDN_By_Idbodega(ByVal pIdBodega As Integer) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetRutaCDN_By_Idbodega = ""

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT RUTA_CDN FROM Bodega 
                                  Where(IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetRutaCDN_By_Idbodega = IIf(IsDBNull(dt.Rows(0).Item("RUTA_CDN")), "", dt.Rows(0).Item("RUTA_CDN"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

End Class
