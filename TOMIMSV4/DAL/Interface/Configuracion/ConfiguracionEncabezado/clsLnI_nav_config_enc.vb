Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_config_enc

    Public Shared Sub Cargar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc, ByRef dr As DataRow)

        Try

            With oBeI_nav_config_enc

                .Idnavconfigenc = IIf(IsDBNull(dr.Item("idnavconfigenc")), 0, dr.Item("idnavconfigenc"))
                .Idempresa = IIf(IsDBNull(dr.Item("idempresa")), 0, dr.Item("idempresa"))
                .Idbodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .IdPropietario = IIf(IsDBNull(dr.Item("idPropietario")), 0, dr.Item("idPropietario"))
                .IdUsuario = IIf(IsDBNull(dr.Item("idUsuario")), 0, dr.Item("idUsuario"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Rechazar_pedido_incompleto = IIf(IsDBNull(dr.Item("rechazar_pedido_incompleto")), 0, dr.Item("rechazar_pedido_incompleto"))
                .Despachar_existencia_parcial = IIf(IsDBNull(dr.Item("despachar_existencia_parcial")), 0, dr.Item("despachar_existencia_parcial"))
                .Convertir_decimales_a_umbas = IIf(IsDBNull(dr.Item("convertir_decimales_a_umbas")), 0, dr.Item("convertir_decimales_a_umbas"))
                .Generar_pedido_ingreso_bodega_destino = IIf(IsDBNull(dr.Item("generar_pedido_ingreso_bodega_destino")), False, dr.Item("generar_pedido_ingreso_bodega_destino"))
                .Generar_Recepcion_Auto_Bodega_Destino = IIf(IsDBNull(dr.Item("generar_recepcion_auto_bodega_destino")), False, dr.Item("generar_recepcion_auto_bodega_destino"))
                .Codigo_proveedor_produccion = IIf(IsDBNull(dr.Item("codigo_proveedor_produccion")), "", dr.Item("codigo_proveedor_produccion"))
                .IdFamilia = IIf(IsDBNull(dr.Item("idFamilia")), 0, dr.Item("idFamilia"))
                .Idclasificacion = IIf(IsDBNull(dr.Item("idclasificacion")), 0, dr.Item("idclasificacion"))
                .IdMarca = IIf(IsDBNull(dr.Item("idMarca")), 0, dr.Item("idMarca"))
                .IdTipoProducto = IIf(IsDBNull(dr.Item("idTipoProducto")), 0, dr.Item("idTipoProducto"))
                .Control_lote = IIf(IsDBNull(dr.Item("control_lote")), False, dr.Item("control_lote"))
                .Control_vencimiento = IIf(IsDBNull(dr.Item("control_vencimiento")), False, dr.Item("control_vencimiento"))
                .Control_peso = IIf(IsDBNull(dr.Item("control_peso")), False, dr.Item("control_peso"))
                .Genera_lp = IIf(IsDBNull(dr.Item("genera_lp")), False, dr.Item("genera_lp"))
                .Nombre_ejecutable = IIf(IsDBNull(dr.Item("nombre_ejecutable")), "", dr.Item("nombre_ejecutable"))
                .IdTipoDocumentoTransferenciasIngreso = IIf(IsDBNull(dr.Item("idtipodocumentotransferenciasingreso")), 3, dr.Item("idtipodocumentotransferenciasingreso"))
                .Crear_Recepcion_De_Transferencia_NAV = IIf(IsDBNull(dr.Item("crear_recepcion_de_transferencia_nav")), False, dr.Item("crear_recepcion_de_transferencia_nav"))
                .Crear_Recepcion_De_Compra_NAV = IIf(IsDBNull(dr.Item("crear_recepcion_de_compra_nav")), False, dr.Item("crear_recepcion_de_compra_nav"))
                .IdAcuerdoEnc = IIf(IsDBNull(dr.Item("IdAcuerdoEnc")), 0, dr.Item("IdAcuerdoEnc"))
                .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), 0, dr.Item("IdTipoEtiqueta"))
                .equiparar_cliente_con_propietario_en_doc_salida = IIf(IsDBNull(dr.Item("equiparar_cliente_con_propietario_en_doc_salida")), False, dr.Item("equiparar_cliente_con_propietario_en_doc_salida"))
                .Push_Ingreso_NAV_Desde_HH = IIf(IsDBNull(dr.Item("push_ingreso_nav_desde_hh")), False, dr.Item("push_ingreso_nav_desde_hh"))
                .Reservar_UMBas_Primero = IIf(IsDBNull(dr.Item("Reservar_UMBas_Primero")), False, dr.Item("Reservar_UMBas_Primero"))
                .Implosion_Automatica = IIf(IsDBNull(dr.Item("Implosion_Automatica")), False, dr.Item("Implosion_Automatica"))
                .Explosion_Automatica = IIf(IsDBNull(dr.Item("Explosion_Automatica")), False, dr.Item("Explosion_Automatica"))
                .Ejecutar_En_Despacho_Automaticamente = IIf(IsDBNull(dr.Item("Ejecutar_En_Despacho_Automaticamente")), False, dr.Item("Ejecutar_En_Despacho_Automaticamente"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), clsDataContractDI.tTipoRotacion.FIFO, dr.Item("IdTipoRotacion"))
                .Explosion_Automatica_Desde_Ubicacion_Picking = IIf(IsDBNull(dr.Item("Explosion_Automatica_Desde_Ubicacion_Picking")), False, dr.Item("Explosion_Automatica_Desde_Ubicacion_Picking"))
                .Explosion_Automatica_Nivel_Max = IIf(IsDBNull(dr.Item("explosion_automatica_nivel_max")), -1, dr.Item("explosion_automatica_nivel_max"))
                .Conservar_Zona_Picking_Clavaud = IIf(IsDBNull(dr.Item("Conservar_Zona_Picking_Clavaud")), False, dr.Item("Conservar_Zona_Picking_Clavaud"))
                .Excluir_Ubicaciones_Reabasto = IIf(IsDBNull(dr.Item("Excluir_Ubicaciones_Reabasto")), False, dr.Item("Excluir_Ubicaciones_Reabasto"))
                .considerar_paletizado_en_reabasto = IIf(IsDBNull(dr.Item("considerar_paletizado_en_reabasto")), False, dr.Item("considerar_paletizado_en_reabasto"))
                .Considerar_Disponibilidad_Ubicacion_Reabasto = IIf(IsDBNull(dr.Item("Considerar_Disponibilidad_Ubicacion_Reabasto")), False, dr.Item("Considerar_Disponibilidad_Ubicacion_Reabasto"))
                .Dias_Vida_Defecto_Perecederos = IIf(IsDBNull(dr.Item("Dias_Vida_Defecto_Perecederos")), 0, dr.Item("Dias_Vida_Defecto_Perecederos"))
                .Codigo_Bodega_ERP_NC = IIf(IsDBNull(dr.Item("Codigo_Bodega_ERP_NC")), "", dr.Item("Codigo_Bodega_ERP_NC"))
                .Lote_Defecto_Entrada_NC = IIf(IsDBNull(dr.Item("Lote_Defecto_Entrada_NC")), "", dr.Item("Lote_Defecto_Entrada_NC"))
                .Vence_Defecto_NC = IIf(IsDBNull(dr.Item("Vence_Defecto_NC")), New Date(1900, 1, 1), dr.Item("Vence_Defecto_NC"))
                .IdProductoEstado_NC = IIf(IsDBNull(dr.Item("IdProductoEstado_NC")), 0, dr.Item("IdProductoEstado_NC"))
                .Interface_SAP = IIf(IsDBNull(dr.Item("Interface_SAP")), False, dr.Item("Interface_SAP"))
                .SAP_Control_Draft_Ajustes = IIf(IsDBNull(dr.Item("sap_control_draft_ajustes")), False, dr.Item("sap_control_draft_ajustes"))
                .SAP_Control_Draft_Traslados = IIf(IsDBNull(dr.Item("sap_control_draft_traslados")), False, dr.Item("sap_control_draft_traslados"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .Rango_Dias_Importacion = IIf(IsDBNull(dr.Item("Rango_Dias_Importacion")), 0, dr.Item("Rango_Dias_Importacion"))
                .Inferir_Bonificacion_Pedido_SAP = IIf(IsDBNull(dr.Item("inferir_bonificacion_pedido_sap")), 0, dr.Item("inferir_bonificacion_pedido_sap"))
                .Rechazar_Bonificacion_Incompleta = IIf(IsDBNull(dr.Item("rechazar_bonificacion_incompleta")), 0, dr.Item("rechazar_bonificacion_incompleta"))
                .Equiparar_Productos = IIf(IsDBNull(dr.Item("Equiparar_Productos")), False, dr.Item("Equiparar_Productos"))
                .Bodega_Facturacion = IIf(IsDBNull(dr.Item("Bodega_Facturacion")), "", dr.Item("Bodega_Facturacion"))
                .Valida_Solo_Codigo = IIf(IsDBNull(dr.Item("Valida_Solo_Codigo")), False, dr.Item("Valida_Solo_Codigo"))
                .Excluir_Recepcion_Picking = IIf(IsDBNull(dr.Item("Excluir_Recepcion_Picking")), False, dr.Item("Excluir_Recepcion_Picking"))
                .Bodega_Prorrateo = IIf(IsDBNull(dr.Item("Bodega_Prorrateo")), "", dr.Item("Bodega_Prorrateo"))
                .Bodega_Prorrateo1 = IIf(IsDBNull(dr.Item("Bodega_Prorrateo1")), "", dr.Item("Bodega_Prorrateo1"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK 20210506 Modifqué esta función para que guarde el propietario se guarde nulo cuando sea igual a 0
    Public Shared Function Insertar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_config_enc")
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBeI_nav_config_enc.IdPropietario <> 0 Then Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("rechazar_pedido_incompleto", "@rechazar_pedido_incompleto", DataType.Parametro)
            Ins.Add("despachar_existencia_parcial", "@despachar_existencia_parcial", DataType.Parametro)
            Ins.Add("convertir_decimales_a_umbas", "@convertir_decimales_a_umbas", DataType.Parametro)
            Ins.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", DataType.Parametro)
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", DataType.Parametro)
            Ins.Add("codigo_proveedor_produccion", "@codigo_proveedor_produccion", DataType.Parametro)
            Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Ins.Add("idmarca", "@idmarca", DataType.Parametro)
            Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            Ins.Add("control_lote", "@control_lote", DataType.Parametro)
            Ins.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Ins.Add("genera_lp", "@genera_lp", DataType.Parametro)
            Ins.Add("nombre_ejecutable", "@nombre_ejecutable", DataType.Parametro)
            Ins.Add("IdTipoDocumentoTransferenciasIngreso", "@IdTipoDocumentoTransferenciasIngreso", DataType.Parametro)
            Ins.Add("crear_recepcion_de_transferencia_nav", "@crear_recepcion_de_transferencia_nav", DataType.Parametro)
            Ins.Add("crear_recepcion_de_compra_nav", "@crear_recepcion_de_compra_nav", DataType.Parametro)
            Ins.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Ins.Add("IdTipoEtiqueta", "@IdTipoEtiqueta", DataType.Parametro)
            Ins.Add("equiparar_cliente_con_propietario_en_doc_salida", "@equiparar_cliente_con_propietario_en_doc_salida", DataType.Parametro)
            Ins.Add("push_ingreso_nav_desde_hh", "@push_ingreso_nav_desde_hh", DataType.Parametro)
            Ins.Add("reservar_umbas_primero", "@reservar_umbas_primero", DataType.Parametro)
            Ins.Add("implosion_automatica", "@implosion_automatica", DataType.Parametro)
            Ins.Add("explosion_automatica", "@explosion_automatica", DataType.Parametro)
            Ins.Add("ejecutar_en_despacho_automaticamente", "@ejecutar_en_despacho_automaticamente", DataType.Parametro)
            Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Ins.Add("explosion_automatica_desde_ubicacion_picking", "@explosion_automatica_desde_ubicacion_picking", DataType.Parametro)
            Ins.Add("explosion_automatica_nivel_max", "@explosion_automatica_nivel_max", DataType.Parametro)
            Ins.Add("conservar_zona_picking_clavaud", "@conservar_zona_picking_clavaud", DataType.Parametro)
            Ins.Add("excluir_ubicaciones_reabasto", "@excluir_ubicaciones_reabasto", DataType.Parametro)
            Ins.Add("considerar_paletizado_en_reabasto", "@considerar_paletizado_en_reabasto", DataType.Parametro)
            Ins.Add("Considerar_Disponibilidad_Ubicacion_Reabasto", "@Considerar_Disponibilidad_Ubicacion_Reabasto", DataType.Parametro)
            Ins.Add("dias_vida_defecto_perecederos", "@dias_vida_defecto_perecederos", DataType.Parametro)
            Ins.Add("codigo_bodega_erp_nc", "@codigo_bodega_erp_nc", DataType.Parametro)
            Ins.Add("lote_defecto_entrada_nc", "@lote_defecto_entrada_nc", DataType.Parametro)
            Ins.Add("vence_defecto_nc", "@vence_defecto_nc", DataType.Parametro)
            Ins.Add("idproductoestado_nc", "@idproductoestado_nc", DataType.Parametro)
            Ins.Add("interface_sap", "@interface_sap", DataType.Parametro)
            Ins.Add("sap_control_draft_ajustes", "@sap_control_draft_ajustes", DataType.Parametro)
            Ins.Add("sap_control_draft_traslados", "@sap_control_draft_traslados", DataType.Parametro)
            Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Ins.Add("rango_dias_importacion", "@rango_dias_importacion", DataType.Parametro)
            Ins.Add("inferir_bonificacion_pedido_sap", "@inferir_bonificacion_pedido_sap", DataType.Parametro)
            Ins.Add("rechazar_bonificacion_incompleta", "@rechazar_bonificacion_incompleta", DataType.Parametro)
            Ins.Add("equiparar_productos", "@equiparar_productos", DataType.Parametro)
            Ins.Add("bodega_facturacion", "@bodega_facturacion", DataType.Parametro)
            Ins.Add("valida_solo_codigo", "@valida_solo_codigo", DataType.Parametro)
            Ins.Add("Excluir_Recepcion_Picking", "@Excluir_Recepcion_Picking", DataType.Parametro)
            Ins.Add("bodega_prorrateo", "@bodega_prorrateo", DataType.Parametro)
            Ins.Add("bodega_prorrateo1", "@bodega_prorrateo1", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_enc.Idnavconfigenc))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_config_enc.Idempresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_config_enc.Idbodega))
            If oBeI_nav_config_enc.IdPropietario <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeI_nav_config_enc.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeI_nav_config_enc.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeI_nav_config_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_config_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_config_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_config_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_config_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_config_enc.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_PEDIDO_INCOMPLETO", oBeI_nav_config_enc.Rechazar_pedido_incompleto))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_EXISTENCIA_PARCIAL", oBeI_nav_config_enc.Despachar_existencia_parcial))
            cmd.Parameters.Add(New SqlParameter("@CONVERTIR_DECIMALES_A_UMBAS", oBeI_nav_config_enc.Convertir_decimales_a_umbas))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PEDIDO_INGRESO_BODEGA_DESTINO", oBeI_nav_config_enc.Generar_pedido_ingreso_bodega_destino))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_RECEPCION_AUTO_BODEGA_DESTINO", oBeI_nav_config_enc.Generar_Recepcion_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR_PRODUCCION", oBeI_nav_config_enc.Codigo_proveedor_produccion))
            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeI_nav_config_enc.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeI_nav_config_enc.Idclasificacion))
            cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeI_nav_config_enc.IdMarca))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeI_nav_config_enc.IdTipoProducto))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeI_nav_config_enc.Control_lote))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeI_nav_config_enc.Control_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeI_nav_config_enc.Control_peso))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP", oBeI_nav_config_enc.Genera_lp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EJECUTABLE", oBeI_nav_config_enc.Nombre_ejecutable))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTOTRANSFERENCIASINGRESO", oBeI_nav_config_enc.IdTipoDocumentoTransferenciasIngreso))
            cmd.Parameters.Add(New SqlParameter("@CREAR_RECEPCION_DE_TRANSFERENCIA_NAV", oBeI_nav_config_enc.Crear_Recepcion_De_Transferencia_NAV))
            cmd.Parameters.Add(New SqlParameter("@CREAR_RECEPCION_DE_COMPRA_NAV", oBeI_nav_config_enc.Crear_Recepcion_De_Compra_NAV))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeI_nav_config_enc.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeI_nav_config_enc.IdTipoEtiqueta))
            'equiparar_cliente_con_propietario_en_doc_salida
            cmd.Parameters.Add(New SqlParameter("@EQUIPARAR_CLIENTE_CON_PROPIETARIO_EN_DOC_SALIDA", oBeI_nav_config_enc.equiparar_cliente_con_propietario_en_doc_salida))
            cmd.Parameters.Add(New SqlParameter("@PUSH_INGRESO_NAV_DESDE_HH", oBeI_nav_config_enc.Push_Ingreso_NAV_Desde_HH))
            cmd.Parameters.Add(New SqlParameter("@RESERVAR_UMBAS_PRIMERO", oBeI_nav_config_enc.Reservar_UMBas_Primero))
            cmd.Parameters.Add(New SqlParameter("@IMPLOSION_AUTOMATICA", oBeI_nav_config_enc.Implosion_Automatica))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA", oBeI_nav_config_enc.Explosion_Automatica))
            cmd.Parameters.Add(New SqlParameter("@EJECUTAR_EN_DESPACHO_AUTOMATICAMENTE", oBeI_nav_config_enc.Ejecutar_En_Despacho_Automaticamente))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeI_nav_config_enc.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA_DESDE_UBICACION_PICKING", oBeI_nav_config_enc.Explosion_Automatica_Desde_Ubicacion_Picking))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA_NIVEL_MAX", oBeI_nav_config_enc.Explosion_Automatica_Nivel_Max))
            cmd.Parameters.Add(New SqlParameter("@CONSERVAR_ZONA_PICKING_CLAVAUD", oBeI_nav_config_enc.Conservar_Zona_Picking_Clavaud))
            cmd.Parameters.Add(New SqlParameter("@EXCLUIR_UBICACIONES_REABASTO", oBeI_nav_config_enc.Excluir_Ubicaciones_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@CONSIDERAR_PALETIZADO_EN_REABASTO", oBeI_nav_config_enc.considerar_paletizado_en_reabasto))
            cmd.Parameters.Add(New SqlParameter("@CONSIDERAR_DISPONIBILIDAD_UBICACION_REABASTO", oBeI_nav_config_enc.Considerar_Disponibilidad_Ubicacion_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VIDA_DEFECTO_PERECEDEROS", oBeI_nav_config_enc.Dias_Vida_Defecto_Perecederos))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP_NC", oBeI_nav_config_enc.Codigo_Bodega_ERP_NC))
            cmd.Parameters.Add(New SqlParameter("@LOTE_DEFECTO_ENTRADA_NC", oBeI_nav_config_enc.Lote_Defecto_Entrada_NC))
            cmd.Parameters.Add(New SqlParameter("@VENCE_DEFECTO_NC", oBeI_nav_config_enc.Vence_Defecto_NC))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO_NC", oBeI_nav_config_enc.IdProductoEstado_NC))
            cmd.Parameters.Add(New SqlParameter("@INTERFACE_SAP", oBeI_nav_config_enc.Interface_SAP))
            cmd.Parameters.Add(New SqlParameter("@SAP_CONTROL_DRAFT_AJUSTES", oBeI_nav_config_enc.SAP_Control_Draft_Ajustes))
            cmd.Parameters.Add(New SqlParameter("@SAP_Control_Draft_Traslados", oBeI_nav_config_enc.SAP_Control_Draft_Traslados))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeI_nav_config_enc.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@RANGO_DIAS_IMPORTACION", oBeI_nav_config_enc.Rango_Dias_Importacion))
            cmd.Parameters.Add(New SqlParameter("@INFERIR_BONIFICACION_PEDIDO_SAP", oBeI_nav_config_enc.Inferir_Bonificacion_Pedido_SAP))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_BONIFICACION_INCOMPLETA", oBeI_nav_config_enc.Rechazar_Bonificacion_Incompleta))
            cmd.Parameters.Add(New SqlParameter("@Equiparar_Productos", oBeI_nav_config_enc.Equiparar_Productos))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Facturacion", oBeI_nav_config_enc.Bodega_Facturacion))
            cmd.Parameters.Add(New SqlParameter("@Valida_Solo_Codigo", oBeI_nav_config_enc.Valida_Solo_Codigo))
            cmd.Parameters.Add(New SqlParameter("@Excluir_Recepcion_Picking", oBeI_nav_config_enc.Excluir_Recepcion_Picking))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Prorrateo", oBeI_nav_config_enc.Bodega_Prorrateo))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Prorrateo1", oBeI_nav_config_enc.Bodega_Prorrateo1))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20210506 Modifqué esta función para que guarde el propietario se guarde nulo cuando sea igual a 0
    Public Shared Function Actualizar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_config_enc")
            Upd.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            If oBeI_nav_config_enc.IdPropietario <> 0 Then Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            If oBeI_nav_config_enc.IdUsuario <> 0 Then Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("rechazar_pedido_incompleto", "@rechazar_pedido_incompleto", DataType.Parametro)
            Upd.Add("despachar_existencia_parcial", "@despachar_existencia_parcial", DataType.Parametro)
            Upd.Add("convertir_decimales_a_umbas", "@convertir_decimales_a_umbas", DataType.Parametro)
            Upd.Add("generar_pedido_ingreso_bodega_destino", "@generar_pedido_ingreso_bodega_destino", DataType.Parametro)
            Upd.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", DataType.Parametro)
            Upd.Add("codigo_proveedor_produccion", "@codigo_proveedor_produccion", DataType.Parametro)
            Upd.Add("idfamilia", "@idfamilia", DataType.Parametro)
            Upd.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Upd.Add("idmarca", "@idmarca", DataType.Parametro)
            Upd.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            Upd.Add("control_lote", "@control_lote", DataType.Parametro)
            Upd.Add("control_peso", "@control_peso", DataType.Parametro)
            Upd.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Upd.Add("genera_lp", "@genera_lp", DataType.Parametro)
            Upd.Add("nombre_ejecutable", "@nombre_ejecutable", DataType.Parametro)
            Upd.Add("IdTipoDocumentoTransferenciasIngreso", "@IdTipoDocumentoTransferenciasIngreso", DataType.Parametro)
            Upd.Add("crear_recepcion_de_transferencia_nav", "@crear_recepcion_de_transferencia_nav", DataType.Parametro)
            Upd.Add("crear_recepcion_de_compra_nav", "@crear_recepcion_de_compra_nav", DataType.Parametro)
            Upd.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Upd.Add("idtipoetiqueta", "@idtipoetiqueta", DataType.Parametro)
            Upd.Add("equiparar_cliente_con_propietario_en_doc_salida", "@equiparar_cliente_con_propietario_en_doc_salida", DataType.Parametro)
            Upd.Add("push_ingreso_nav_desde_hh", "@push_ingreso_nav_desde_hh", DataType.Parametro)
            Upd.Add("reservar_umbas_primero", "@reservar_umbas_primero", DataType.Parametro)
            Upd.Add("implosion_automatica", "@implosion_automatica", DataType.Parametro)
            Upd.Add("explosion_automatica", "@explosion_automatica", DataType.Parametro)
            Upd.Add("ejecutar_en_despacho_automaticamente", "@ejecutar_en_despacho_automaticamente", DataType.Parametro)
            Upd.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Upd.Add("explosion_automatica_desde_ubicacion_picking", "@explosion_automatica_desde_ubicacion_picking", DataType.Parametro)
            Upd.Add("explosion_automatica_nivel_max", "@explosion_automatica_nivel_max", DataType.Parametro)
            Upd.Add("conservar_zona_picking_clavaud", "@conservar_zona_picking_clavaud", DataType.Parametro)
            Upd.Add("excluir_ubicaciones_reabasto", "@excluir_ubicaciones_reabasto", DataType.Parametro)
            Upd.Add("considerar_paletizado_en_reabasto", "@considerar_paletizado_en_reabasto", DataType.Parametro)
            Upd.Add("Considerar_Disponibilidad_Ubicacion_Reabasto", "@Considerar_Disponibilidad_Ubicacion_Reabasto", DataType.Parametro)
            Upd.Add("dias_vida_defecto_perecederos", "@dias_vida_defecto_perecederos", DataType.Parametro)
            Upd.Add("codigo_bodega_erp_nc", "@codigo_bodega_erp_nc", DataType.Parametro)
            Upd.Add("lote_defecto_entrada_nc", "@lote_defecto_entrada_nc", DataType.Parametro)
            Upd.Add("vence_defecto_nc", "@vence_defecto_nc", DataType.Parametro)
            Upd.Add("idproductoestado_nc", "@idproductoestado_nc", DataType.Parametro)
            Upd.Add("interface_sap", "@interface_sap", DataType.Parametro)
            Upd.Add("sap_control_draft_ajustes", "@sap_control_draft_ajustes", DataType.Parametro)
            Upd.Add("sap_control_draft_traslados", "@sap_control_draft_traslados", DataType.Parametro)
            Upd.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Upd.Add("rango_dias_importacion", "@rango_dias_importacion", DataType.Parametro)
            Upd.Add("inferir_bonificacion_pedido_sap", "@inferir_bonificacion_pedido_sap", DataType.Parametro)
            Upd.Add("rechazar_bonificacion_incompleta", "@rechazar_bonificacion_incompleta", DataType.Parametro)
            Upd.Add("equiparar_productos", "@equiparar_productos", DataType.Parametro)
            Upd.Add("bodega_facturacion", "@bodega_facturacion", DataType.Parametro)
            Upd.Add("Valida_Solo_Codigo", "@Valida_Solo_Codigo", DataType.Parametro)
            Upd.Add("Excluir_Recepcion_Picking", "@Excluir_Recepcion_Picking", DataType.Parametro)
            Ins.Add("bodega_prorrateo", "@bodega_prorrateo", DataType.Parametro)
            Ins.Add("bodega_prorrateo1", "@bodega_prorrateo1", DataType.Parametro)
            Upd.Where("idnavconfigenc = @idnavconfigenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_enc.Idnavconfigenc))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_config_enc.Idempresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_config_enc.Idbodega))
            If oBeI_nav_config_enc.IdPropietario <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeI_nav_config_enc.IdPropietario))
            If oBeI_nav_config_enc.IdUsuario <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeI_nav_config_enc.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeI_nav_config_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_config_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_config_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_config_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_config_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_config_enc.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_PEDIDO_INCOMPLETO", oBeI_nav_config_enc.Rechazar_pedido_incompleto))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_EXISTENCIA_PARCIAL", oBeI_nav_config_enc.Despachar_existencia_parcial))
            cmd.Parameters.Add(New SqlParameter("@CONVERTIR_DECIMALES_A_UMBAS", oBeI_nav_config_enc.Convertir_decimales_a_umbas))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PEDIDO_INGRESO_BODEGA_DESTINO", oBeI_nav_config_enc.Generar_pedido_ingreso_bodega_destino))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_RECEPCION_AUTO_BODEGA_DESTINO", oBeI_nav_config_enc.Generar_Recepcion_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR_PRODUCCION", oBeI_nav_config_enc.Codigo_proveedor_produccion))
            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeI_nav_config_enc.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeI_nav_config_enc.Idclasificacion))
            cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeI_nav_config_enc.IdMarca))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeI_nav_config_enc.IdTipoProducto))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeI_nav_config_enc.Control_lote))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeI_nav_config_enc.Control_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeI_nav_config_enc.Control_peso))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP", oBeI_nav_config_enc.Genera_lp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EJECUTABLE", oBeI_nav_config_enc.Nombre_ejecutable))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTOTRANSFERENCIASINGRESO", oBeI_nav_config_enc.IdTipoDocumentoTransferenciasIngreso))
            cmd.Parameters.Add(New SqlParameter("@CREAR_RECEPCION_DE_TRANSFERENCIA_NAV", oBeI_nav_config_enc.Crear_Recepcion_De_Transferencia_NAV))
            cmd.Parameters.Add(New SqlParameter("@CREAR_RECEPCION_DE_COMPRA_NAV", oBeI_nav_config_enc.Crear_Recepcion_De_Compra_NAV))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeI_nav_config_enc.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeI_nav_config_enc.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@equiparar_cliente_con_propietario_en_doc_salida", oBeI_nav_config_enc.equiparar_cliente_con_propietario_en_doc_salida))
            cmd.Parameters.Add(New SqlParameter("@PUSH_INGRESO_NAV_DESDE_HH", oBeI_nav_config_enc.Push_Ingreso_NAV_Desde_HH))
            cmd.Parameters.Add(New SqlParameter("@RESERVAR_UMBAS_PRIMERO", oBeI_nav_config_enc.Reservar_UMBas_Primero))
            cmd.Parameters.Add(New SqlParameter("@IMPLOSION_AUTOMATICA", oBeI_nav_config_enc.Implosion_Automatica))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA", oBeI_nav_config_enc.Explosion_Automatica))
            cmd.Parameters.Add(New SqlParameter("@EJECUTAR_EN_DESPACHO_AUTOMATICAMENTE", oBeI_nav_config_enc.Ejecutar_En_Despacho_Automaticamente))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeI_nav_config_enc.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA_DESDE_UBICACION_PICKING", oBeI_nav_config_enc.Explosion_Automatica_Desde_Ubicacion_Picking))
            cmd.Parameters.Add(New SqlParameter("@EXPLOSION_AUTOMATICA_NIVEL_MAX", oBeI_nav_config_enc.Explosion_Automatica_Nivel_Max))
            cmd.Parameters.Add(New SqlParameter("@CONSERVAR_ZONA_PICKING_CLAVAUD", oBeI_nav_config_enc.Conservar_Zona_Picking_Clavaud))
            cmd.Parameters.Add(New SqlParameter("EXCLUIR_UBICACIONES_REABASTO", oBeI_nav_config_enc.Excluir_Ubicaciones_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@CONSIDERAR_PALETIZADO_EN_REABASTO", oBeI_nav_config_enc.considerar_paletizado_en_reabasto))
            cmd.Parameters.Add(New SqlParameter("@CONSIDERAR_DISPONIBILIDAD_UBICACION_REABASTO", oBeI_nav_config_enc.Considerar_Disponibilidad_Ubicacion_Reabasto))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VIDA_DEFECTO_PERECEDEROS", oBeI_nav_config_enc.Dias_Vida_Defecto_Perecederos))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ERP_NC", oBeI_nav_config_enc.Codigo_Bodega_ERP_NC))
            cmd.Parameters.Add(New SqlParameter("@LOTE_DEFECTO_ENTRADA_NC", oBeI_nav_config_enc.Lote_Defecto_Entrada_NC))
            cmd.Parameters.Add(New SqlParameter("@VENCE_DEFECTO_NC", oBeI_nav_config_enc.Vence_Defecto_NC))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO_NC", oBeI_nav_config_enc.IdProductoEstado_NC))
            cmd.Parameters.Add(New SqlParameter("@INTERFACE_SAP", oBeI_nav_config_enc.Interface_SAP))
            cmd.Parameters.Add(New SqlParameter("@SAP_CONTROL_DRAFT_AJUSTES", oBeI_nav_config_enc.SAP_Control_Draft_Ajustes))
            cmd.Parameters.Add(New SqlParameter("@SAP_CONTROL_DRAFT_TRASLADOS", oBeI_nav_config_enc.SAP_Control_Draft_Traslados))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeI_nav_config_enc.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@RANGO_DIAS_IMPORTACION", oBeI_nav_config_enc.Rango_Dias_Importacion))
            cmd.Parameters.Add(New SqlParameter("@INFERIR_BONIFICACION_PEDIDO_SAP", oBeI_nav_config_enc.Inferir_Bonificacion_Pedido_SAP))
            cmd.Parameters.Add(New SqlParameter("@RECHAZAR_BONIFICACION_INCOMPLETA", oBeI_nav_config_enc.Rechazar_Bonificacion_Incompleta))
            cmd.Parameters.Add(New SqlParameter("@EQUIPARAR_PRODUCTOS", oBeI_nav_config_enc.Equiparar_Productos))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Facturacion", oBeI_nav_config_enc.Bodega_Facturacion))
            cmd.Parameters.Add(New SqlParameter("@Valida_Solo_Codigo", oBeI_nav_config_enc.Valida_Solo_Codigo))
            cmd.Parameters.Add(New SqlParameter("@Excluir_Recepcion_Picking", oBeI_nav_config_enc.Excluir_Recepcion_Picking))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Prorrateo", oBeI_nav_config_enc.Bodega_Prorrateo))
            cmd.Parameters.Add(New SqlParameter("@Bodega_Prorrateo1", oBeI_nav_config_enc.Bodega_Prorrateo1))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM I_nav_config_enc 
                                  Where(idnavconfigenc = @idnavconfigenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_enc.Idnavconfigenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_config_enc, dt.Rows(0))
                Obtener = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_config_enc As clsBeI_nav_config_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_config_enc 
                                   Where(idnavconfigenc = @idnavconfigenc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_enc.Idnavconfigenc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_config_enc "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

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
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_config_enc"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeI_nav_config_enc)

        Dim lReturnList As New List(Of clsBeI_nav_config_enc)

        Try

            Const sp As String = "SELECT * FROM I_nav_config_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_config_enc As New clsBeI_nav_config_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_config_enc = New clsBeI_nav_config_enc()
                            Cargar(vBeI_nav_config_enc, dr)
                            lReturnList.Add(vBeI_nav_config_enc)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_config_enc As clsBeI_nav_config_enc) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM I_nav_config_enc
                                  Where(idnavconfigenc = @idnavconfigenc)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@idnavconfigenc", pBeI_nav_config_enc.Idnavconfigenc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeI_nav_config_enc, lDataTable.Rows(0))
                            GetSingle = True
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdConfigEnc As Integer) As clsBeI_nav_config_enc

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_config_enc
                                  WHERE(idnavconfigenc = @idnavconfigenc)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction()

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", IdConfigEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeI_nav_config_enc As New clsBeI_nav_config_enc
                Cargar(pBeI_nav_config_enc, dt.Rows(0))
                Return pBeI_nav_config_enc
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idnavconfigenc),0) FROM I_nav_config_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_DMS() As DataTable

        Get_All_By_DMS = Nothing

        Try

            Const sp As String = "SELECT idnavconfigenc Id,nombre descripcion, nombre_ejecutable entidad FROM I_nav_config_enc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_All_By_DMS = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class

