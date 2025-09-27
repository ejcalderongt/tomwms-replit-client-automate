Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_jornada

    Public Shared Sub Cargar(ByRef oBeStock_jornada As clsBeStock_jornada, ByRef dr As DataRow)

        Try

            With oBeStock_jornada

                If dr.Table.Columns.Contains("IdStockJornada") Then .IdStockJornada = IIf(IsDBNull(dr.Item("IdStockJornada")), 0, dr.Item("IdStockJornada"))
                If dr.Table.Columns.Contains("IdJornadaSistema") Then .IdJornadaSistema = IIf(IsDBNull(dr.Item("IdJornadaSistema")), 0, dr.Item("IdJornadaSistema"))
                If dr.Table.Columns.Contains("Fecha") Then .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                If dr.Table.Columns.Contains("IdBodega") Then .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                If dr.Table.Columns.Contains("IdPropietarioBodega") Then .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                If dr.Table.Columns.Contains("IdProductoEstado") Then .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdUbicacion") Then .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                If dr.Table.Columns.Contains("IdUbicacion_anterior") Then .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                If dr.Table.Columns.Contains("IdRecepcionEnc") Then .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                If dr.Table.Columns.Contains("IdRecepcionDet") Then .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                If dr.Table.Columns.Contains("IdPedidoEnc") Then .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                If dr.Table.Columns.Contains("IdPickingEnc") Then .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                If dr.Table.Columns.Contains("IdDespachoEnc") Then .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                If dr.Table.Columns.Contains("Lic_plate") Then .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                If dr.Table.Columns.Contains("Serial") Then .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                If dr.Table.Columns.Contains("Cantidad") Then .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                If dr.Table.Columns.Contains("Fecha_ingreso") Then .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                If dr.Table.Columns.Contains("Fecha_vence") Then .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                If dr.Table.Columns.Contains("Uds_lic_plate") Then .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0.0, dr.Item("uds_lic_plate"))
                If dr.Table.Columns.Contains("No_bulto") Then .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                If dr.Table.Columns.Contains("Fecha_manufactura") Then .Fecha_manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                If dr.Table.Columns.Contains("Añada") Then .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                If dr.Table.Columns.Contains("User_agr") Then .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                If dr.Table.Columns.Contains("Fec_agr") Then .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                If dr.Table.Columns.Contains("User_mod") Then .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                If dr.Table.Columns.Contains("Fec_mod") Then .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                If dr.Table.Columns.Contains("Activo") Then .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                If dr.Table.Columns.Contains("Temperatura") Then .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                If dr.Table.Columns.Contains("Atributo_variante_1") Then .Atributo_variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                If dr.Table.Columns.Contains("Pallet_no_estandar") Then .Pallet_no_estandar = IIf(IsDBNull(dr.Item("pallet_no_estandar")), False, dr.Item("pallet_no_estandar"))
                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                If dr.Table.Columns.Contains("Proveedor") Then .Proveedor = IIf(IsDBNull(dr.Item("Proveedor")), "", dr.Item("Proveedor"))
                If dr.Table.Columns.Contains("Bodega") Then .Bodega = IIf(IsDBNull(dr.Item("Bodega")), "", dr.Item("Bodega"))
                If dr.Table.Columns.Contains("IdOrdenCompraEnc") Then .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                If dr.Table.Columns.Contains("No_DocumentoOC") Then .No_DocumentoOC = IIf(IsDBNull(dr.Item("No_DocumentoOC")), "", dr.Item("No_DocumentoOC"))
                If dr.Table.Columns.Contains("No_DocumentoRec") Then .No_DocumentoRec = IIf(IsDBNull(dr.Item("No_DocumentoRec")), "", dr.Item("No_DocumentoRec"))
                If dr.Table.Columns.Contains("ReferenciaOC") Then .ReferenciaOC = IIf(IsDBNull(dr.Item("ReferenciaOC")), "", dr.Item("ReferenciaOC"))
                If dr.Table.Columns.Contains("Fecha_Recepcion") Then .Fecha_Recepcion = IIf(IsDBNull(dr.Item("Fecha_Recepcion")), Date.Now, dr.Item("Fecha_Recepcion"))
                If dr.Table.Columns.Contains("TipoTrans") Then .TipoTrans = IIf(IsDBNull(dr.Item("TipoTrans")), "", dr.Item("TipoTrans"))
                If dr.Table.Columns.Contains("Fecha_Agrego") Then .Fecha_Agrego = IIf(IsDBNull(dr.Item("Fecha_Agrego")), Date.Now, dr.Item("Fecha_Agrego"))
                If dr.Table.Columns.Contains("Codigo_producto") Then .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                If dr.Table.Columns.Contains("Codigo_barra_producto") Then .Codigo_barra_producto = IIf(IsDBNull(dr.Item("codigo_barra_producto")), "", dr.Item("codigo_barra_producto"))
                If dr.Table.Columns.Contains("Nombre_producto") Then .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                If dr.Table.Columns.Contains("Existencia") Then .Existencia = IIf(IsDBNull(dr.Item("existencia")), 0.0, dr.Item("existencia"))
                If dr.Table.Columns.Contains("Nom_umBas") Then .Nom_umBas = IIf(IsDBNull(dr.Item("nom_umBas")), "", dr.Item("nom_umBas"))
                If dr.Table.Columns.Contains("Nom_estado_producto") Then .Nom_estado_producto = IIf(IsDBNull(dr.Item("nom_estado_producto")), "", dr.Item("nom_estado_producto"))
                If dr.Table.Columns.Contains("Nom_presentacion_producto") Then .Nom_presentacion_producto = IIf(IsDBNull(dr.Item("nom_presentacion_producto")), "", dr.Item("nom_presentacion_producto"))
                If dr.Table.Columns.Contains("Ubicacion_origen") Then .Ubicacion_origen = IIf(IsDBNull(dr.Item("ubicacion_origen")), "", dr.Item("ubicacion_origen"))
                If dr.Table.Columns.Contains("No_poliza") Then .No_poliza = IIf(IsDBNull(dr.Item("no_poliza")), "", dr.Item("no_poliza"))
                If dr.Table.Columns.Contains("Valor_aduana") Then .Valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                If dr.Table.Columns.Contains("Valor_fob") Then .Valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0.0, dr.Item("valor_fob"))
                If dr.Table.Columns.Contains("Valor_iva") Then .Valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0.0, dr.Item("valor_iva"))
                If dr.Table.Columns.Contains("Valor_dai") Then .Valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0.0, dr.Item("valor_dai"))
                If dr.Table.Columns.Contains("Valor_seguro") Then .Valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0.0, dr.Item("valor_seguro"))
                If dr.Table.Columns.Contains("Valor_flete") Then .Valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0.0, dr.Item("valor_flete"))
                If dr.Table.Columns.Contains("Peso_neto") Then .Peso_neto = IIf(IsDBNull(dr.Item("peso_neto")), 0.0, dr.Item("peso_neto"))
                If dr.Table.Columns.Contains("Numero_orden") Then .Numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), "", dr.Item("numero_orden"))
                If dr.Table.Columns.Contains("Codigo_regimen") Then .Codigo_regimen = IIf(IsDBNull(dr.Item("codigo_regimen")), "", dr.Item("codigo_regimen"))
                If dr.Table.Columns.Contains("Nombre_regimen") Then .Nombre_regimen = IIf(IsDBNull(dr.Item("nombre_regimen")), "", dr.Item("nombre_regimen"))
                If dr.Table.Columns.Contains("Dias_vencimiento_regimen") Then .Dias_vencimiento_regimen = IIf(IsDBNull(dr.Item("dias_vencimiento_regimen")), 0, dr.Item("dias_vencimiento_regimen"))
                If dr.Table.Columns.Contains("Fecha_Ingreso_Ticket_TMS") Then .Fecha_Ingreso_Ticket_TMS = IIf(IsDBNull(dr.Item("fecha_ingreso_ticket_tms")), Now, dr.Item("fecha_ingreso_ticket_tms"))
                If dr.Table.Columns.Contains("Es_Retroactivo") Then .Es_Retroactivo = IIf(IsDBNull(dr.Item("es_retroactivo")), False, dr.Item("es_retroactivo"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0, dr.Item("factor"))
                If dr.Table.Columns.Contains("CamasPorTarima") Then .CamasPorTarima = IIf(IsDBNull(dr.Item("camasportarima")), 0, dr.Item("camasportarima"))
                If dr.Table.Columns.Contains("CajasPorCama") Then .CajasPorCama = IIf(IsDBNull(dr.Item("cajasporcama")), 0, dr.Item("cajasporcama"))
                If dr.Table.Columns.Contains("Cantidad_Ingreso_Afecta_A_salida") Then .Cantidad_Ingreso_Afecta_A_salida = IIf(IsDBNull(dr.Item("cantidad_ingreso_afecta_a_salida")), 0, dr.Item("cantidad_ingreso_afecta_a_salida"))
                If dr.Table.Columns.Contains("Stock_Jornada_Hash") Then .Stock_Jornada_Hash = IIf(IsDBNull(dr.Item("Stock_Jornada_Hash")), "", dr.Item("Stock_Jornada_Hash"))
                If dr.Table.Columns.Contains("IdTicketTMS") Then .IdTicketTMS = IIf(IsDBNull(dr.Item("IdTicketTMS")), "0", dr.Item("IdTicketTMS"))
                If dr.Table.Columns.Contains("IdPropietario") Then .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                If dr.Table.Columns.Contains("IdClasificacion") Then .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                If dr.Table.Columns.Contains("Clasificacion") Then .Clasificacion = IIf(IsDBNull(dr.Item("Clasificacion")), "", dr.Item("Clasificacion"))
                If dr.Table.Columns.Contains("Regimen") Then .Regimen = IIf(IsDBNull(dr.Item("Regimen")), "", dr.Item("Regimen"))
                If dr.Table.Columns.Contains("Posiciones") Then .Posiciones = IIf(IsDBNull(dr.Item("Posiciones")), 0, dr.Item("Posiciones"))
                If dr.Table.Columns.Contains("no_documento_procesado_erp") Then .No_Documento_Procesado_ERP = IIf(IsDBNull(dr.Item("no_documento_procesado_erp")), 0, dr.Item("no_documento_procesado_erp"))
                If dr.Table.Columns.Contains("procesado_erp") Then .Procesado_ERP = IIf(IsDBNull(dr.Item("procesado_erp")), 0, dr.Item("procesado_erp"))
                If dr.Table.Columns.Contains("TipoRubro") Then .TipoRubro = IIf(IsDBNull(dr.Item("TipoRubro")), 0, dr.Item("TipoRubro"))
                If dr.Table.Columns.Contains("Bultos_Por_Tarima") Then .Bultos_Por_Tarima = IIf(IsDBNull(dr.Item("Bultos_Por_Tarima")), 0, dr.Item("Bultos_Por_Tarima"))
                If dr.Table.Columns.Contains("UMBultos") Then .Nom_presentacion_producto = IIf(IsDBNull(dr.Item("UMBultos")), 0, dr.Item("UMBultos"))
                '#GT10092025: talla/color
                If dr.Table.Columns.Contains("IdProductoTallaColor") Then .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                If dr.Table.Columns.Contains("Talla") Then .Talla = IIf(IsDBNull(dr.Item("Talla")), 0, dr.Item("Talla"))
                If dr.Table.Columns.Contains("Color") Then .Color = IIf(IsDBNull(dr.Item("Color")), 0, dr.Item("Color"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeStock_jornada As clsBeStock_jornada,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean

        Try

            Ins.Init("stock_jornada")
            Ins.Add("idstockjornada", "@idstockjornada", DataType.Parametro)
            Ins.Add("idjornadasistema", "@idjornadasistema", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            If oBeStock_jornada.IdPresentacion <> 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idubicacion_anterior", "@idubicacion_anterior", DataType.Parametro)
            If oBeStock_jornada.IdRecepcionEnc <> 0 Then Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            If oBeStock_jornada.IdRecepcionDet <> 0 Then Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            If oBeStock_jornada.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("temperatura", "@temperatura", DataType.Parametro)
            If Not oBeStock_jornada.Atributo_variante_1 Is Nothing Then Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Ins.Add("propietario", "@propietario", DataType.Parametro)
            Ins.Add("proveedor", "@proveedor", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("no_documentooc", "@no_documentooc", DataType.Parametro)
            Ins.Add("no_documentorec", "@no_documentorec", DataType.Parametro)
            Ins.Add("referenciaoc", "@referenciaoc", DataType.Parametro)
            Ins.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Ins.Add("tipotrans", "@tipotrans", DataType.Parametro)
            Ins.Add("fecha_agrego", "@fecha_agrego", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("codigo_barra_producto", "@codigo_barra_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("existencia", "@existencia", DataType.Parametro)
            Ins.Add("nom_umbas", "@nom_umbas", DataType.Parametro)
            Ins.Add("nom_estado_producto", "@nom_estado_producto", DataType.Parametro)
            Ins.Add("nom_presentacion_producto", "@nom_presentacion_producto", DataType.Parametro)
            Ins.Add("ubicacion_origen", "@ubicacion_origen", DataType.Parametro)
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Ins.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Ins.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Ins.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Ins.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Ins.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Ins.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Ins.Add("codigo_regimen", "@codigo_regimen", DataType.Parametro)
            Ins.Add("nombre_regimen", "@nombre_regimen", DataType.Parametro)
            Ins.Add("dias_vencimiento_regimen", "@dias_vencimiento_regimen", DataType.Parametro)
            Ins.Add("fecha_ingreso_ticket_tms", "@fecha_ingreso_ticket_tms", DataType.Parametro)
            Ins.Add("es_retroactivo", "@es_retroactivo", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("camasportarima", "@camasportarima", DataType.Parametro)
            Ins.Add("cajasporcama", "@cajasporcama", DataType.Parametro)
            Ins.Add("cantidad_ingreso_afecta_a_salida", "@cantidad_ingreso_afecta_a_salida", DataType.Parametro)
            Ins.Add("stock_jornada_hash", "@stock_jornada_hash", DataType.Parametro)
            Ins.Add("idtickettms", "@idtickettms", DataType.Parametro)
            Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Ins.Add("IdClasificacion", "@IdClasificacion", DataType.Parametro)
            Ins.Add("Clasificacion", "@Clasificacion", DataType.Parametro)
            Ins.Add("Regimen", "@Regimen", DataType.Parametro)
            Ins.Add("Posiciones", "@Posiciones", DataType.Parametro)
            Ins.Add("costo_unitario", "@costo_unitario", DataType.Parametro)
            Ins.Add("no_documento_procesado_erp", "@no_documento_procesado_erp", DataType.Parametro)
            Ins.Add("procesado_erp", "@procesado_erp", DataType.Parametro)
            Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)
            '#GT10092025: talla/color
            If oBeStock_jornada.IdProductoTallaColor > 0 Then
                Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
                Ins.Add("Talla", "@Talla", DataType.Parametro)
                Ins.Add("Color", "@Color", DataType.Parametro)
            End If



            Dim sp As String = Ins.SQL()

            If sp.Trim = "" Then
                Throw New Exception("ERROR_202211022330: No se pudo generar la cadena de insert, por algún extraño valor supongo.")
            End If

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = sp}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKJORNADA", oBeStock_jornada.IdStockJornada))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADASISTEMA", oBeStock_jornada.IdJornadaSistema))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeStock_jornada.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_jornada.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_jornada.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_jornada.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_jornada.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_jornada.IdProductoEstado))
            If oBeStock_jornada.IdPresentacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_jornada.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_jornada.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_jornada.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeStock_jornada.IdUbicacion_anterior))

            If oBeStock_jornada.IdRecepcionEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_jornada.IdRecepcionEnc))
            If oBeStock_jornada.IdRecepcionDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_jornada.IdRecepcionDet))

            If oBeStock_jornada.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_jornada.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_jornada.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_jornada.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_jornada.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_jornada.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_jornada.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_jornada.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_jornada.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_jornada.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_jornada.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_jornada.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_jornada.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_jornada.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_jornada.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_jornada.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_jornada.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_jornada.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_jornada.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_jornada.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_jornada.Temperatura))
            If Not oBeStock_jornada.Atributo_variante_1 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock_jornada.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_jornada.Pallet_no_estandar))
            cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeStock_jornada.Propietario))
            cmd.Parameters.Add(New SqlParameter("@PROVEEDOR", oBeStock_jornada.Proveedor))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeStock_jornada.Bodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeStock_jornada.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOOC", oBeStock_jornada.No_DocumentoOC))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOREC", oBeStock_jornada.No_DocumentoRec))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIAOC", oBeStock_jornada.ReferenciaOC))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeStock_jornada.Fecha_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeStock_jornada.TipoTrans))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGO", oBeStock_jornada.Fecha_Agrego))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeStock_jornada.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA_PRODUCTO", oBeStock_jornada.Codigo_barra_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeStock_jornada.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA", oBeStock_jornada.Existencia))
            cmd.Parameters.Add(New SqlParameter("@NOM_UMBAS", oBeStock_jornada.Nom_umBas))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO_PRODUCTO", oBeStock_jornada.Nom_estado_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION_PRODUCTO", oBeStock_jornada.Nom_presentacion_producto))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_ORIGEN", oBeStock_jornada.Ubicacion_origen))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeStock_jornada.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeStock_jornada.Valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeStock_jornada.Valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeStock_jornada.Valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeStock_jornada.Valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeStock_jornada.Valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeStock_jornada.Valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeStock_jornada.Peso_neto))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeStock_jornada.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_REGIMEN", oBeStock_jornada.Codigo_regimen))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_REGIMEN", oBeStock_jornada.Nombre_regimen))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO_REGIMEN", oBeStock_jornada.Dias_vencimiento_regimen))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO_TICKET_TMS", oBeStock_jornada.Fecha_Ingreso_Ticket_TMS))
            cmd.Parameters.Add(New SqlParameter("@ES_RETROACTIVO", oBeStock_jornada.Es_Retroactivo))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeStock_jornada.Factor))
            cmd.Parameters.Add(New SqlParameter("@CAMASPORTARIMA", oBeStock_jornada.CamasPorTarima))
            cmd.Parameters.Add(New SqlParameter("@CAJASPORCAMA", oBeStock_jornada.CajasPorCama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_INGRESO_AFECTA_A_SALIDA", oBeStock_jornada.Cantidad_Ingreso_Afecta_A_salida))
            cmd.Parameters.Add(New SqlParameter("@STOCK_JORNADA_HASH", oBeStock_jornada.Stock_Jornada_Hash))
            cmd.Parameters.Add(New SqlParameter("@IDTICKETTMS", oBeStock_jornada.IdTicketTMS))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeStock_jornada.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeStock_jornada.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@CLASIFICACION", oBeStock_jornada.Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@REGIMEN", oBeStock_jornada.Regimen))
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeStock_jornada.Posiciones))
            cmd.Parameters.Add(New SqlParameter("@COSTO_UNITARIO", oBeStock_jornada.Costo_Unitario))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PROCESADO_ERP", oBeStock_jornada.No_Documento_Procesado_ERP))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_ERP", oBeStock_jornada.Procesado_ERP))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeStock_jornada.Fecha_Procesado_Stock_Jornada))

            If oBeStock_jornada.IdProductoTallaColor > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_jornada.IdProductoTallaColor))
                cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_jornada.Talla))
                cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_jornada.Color))
            End If

            Dim rowsAfected As Integer = cmd.ExecuteNonQuery()

            'cmd.ExecuteNonQueryAsync()
            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAfected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("Error_202211022328: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If Not lConnection Is Nothing Then lConnection.Dispose()
                If Not lTransaction Is Nothing Then lTransaction.Dispose()
            End If

        End Try

    End Function

    Public Shared Function Insertar_desde_HH(ByRef oBeStock_jornada As clsBeStock_jornada,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean

        Try

            Ins.Init("stock_jornada")
            Ins.Add("idstockjornada", "@idstockjornada", DataType.Parametro)
            Ins.Add("idjornadasistema", "@idjornadasistema", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            If oBeStock_jornada.IdPresentacion <> 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idubicacion_anterior", "@idubicacion_anterior", DataType.Parametro)
            If oBeStock_jornada.IdRecepcionEnc <> 0 Then Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            If oBeStock_jornada.IdRecepcionDet <> 0 Then Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            If oBeStock_jornada.IdPedidoEnc <> 0 Then Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("temperatura", "@temperatura", DataType.Parametro)
            If Not oBeStock_jornada.Atributo_variante_1 Is Nothing Then Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Ins.Add("propietario", "@propietario", DataType.Parametro)
            Ins.Add("proveedor", "@proveedor", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("no_documentooc", "@no_documentooc", DataType.Parametro)
            Ins.Add("no_documentorec", "@no_documentorec", DataType.Parametro)
            Ins.Add("referenciaoc", "@referenciaoc", DataType.Parametro)
            Ins.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Ins.Add("tipotrans", "@tipotrans", DataType.Parametro)
            Ins.Add("fecha_agrego", "@fecha_agrego", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("codigo_barra_producto", "@codigo_barra_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("existencia", "@existencia", DataType.Parametro)
            Ins.Add("nom_umbas", "@nom_umbas", DataType.Parametro)
            Ins.Add("nom_estado_producto", "@nom_estado_producto", DataType.Parametro)
            Ins.Add("nom_presentacion_producto", "@nom_presentacion_producto", DataType.Parametro)
            Ins.Add("ubicacion_origen", "@ubicacion_origen", DataType.Parametro)
            Ins.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Ins.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Ins.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Ins.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Ins.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Ins.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Ins.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Ins.Add("codigo_regimen", "@codigo_regimen", DataType.Parametro)
            Ins.Add("nombre_regimen", "@nombre_regimen", DataType.Parametro)
            Ins.Add("dias_vencimiento_regimen", "@dias_vencimiento_regimen", DataType.Parametro)
            Ins.Add("fecha_ingreso_ticket_tms", "@fecha_ingreso_ticket_tms", DataType.Parametro)
            Ins.Add("es_retroactivo", "@es_retroactivo", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("camasportarima", "@camasportarima", DataType.Parametro)
            Ins.Add("cajasporcama", "@cajasporcama", DataType.Parametro)
            Ins.Add("cantidad_ingreso_afecta_a_salida", "@cantidad_ingreso_afecta_a_salida", DataType.Parametro)
            Ins.Add("stock_jornada_hash", "@stock_jornada_hash", DataType.Parametro)
            Ins.Add("idtickettms", "@idtickettms", DataType.Parametro)
            Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Ins.Add("IdClasificacion", "@IdClasificacion", DataType.Parametro)
            Ins.Add("Clasificacion", "@Clasificacion", DataType.Parametro)
            Ins.Add("Regimen", "@Regimen", DataType.Parametro)
            Ins.Add("Posiciones", "@Posiciones", DataType.Parametro)
            Ins.Add("costo_unitario", "@costo_unitario", DataType.Parametro)
            Ins.Add("no_documento_procesado_erp", "@no_documento_procesado_erp", DataType.Parametro)
            Ins.Add("procesado_erp", "@procesado_erp", DataType.Parametro)
            Ins.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            If sp.Trim = "" Then
                Throw New Exception("ERROR_202211022330: No se pudo generar la cadena de insert, por algún extraño valor supongo.")
            End If

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = sp}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKJORNADA", oBeStock_jornada.IdStockJornada))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADASISTEMA", oBeStock_jornada.IdJornadaSistema))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeStock_jornada.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_jornada.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_jornada.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_jornada.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_jornada.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_jornada.IdProductoEstado))
            If oBeStock_jornada.IdPresentacion <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_jornada.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_jornada.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_jornada.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeStock_jornada.IdUbicacion_anterior))

            If oBeStock_jornada.IdRecepcionEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_jornada.IdRecepcionEnc))
            If oBeStock_jornada.IdRecepcionDet <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_jornada.IdRecepcionDet))

            If oBeStock_jornada.IdPedidoEnc <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_jornada.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_jornada.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_jornada.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_jornada.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_jornada.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_jornada.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_jornada.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_jornada.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_jornada.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_jornada.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_jornada.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_jornada.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_jornada.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_jornada.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_jornada.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_jornada.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_jornada.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_jornada.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_jornada.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_jornada.Temperatura))
            If Not oBeStock_jornada.Atributo_variante_1 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock_jornada.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_jornada.Pallet_no_estandar))
            cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeStock_jornada.Propietario))
            cmd.Parameters.Add(New SqlParameter("@PROVEEDOR", oBeStock_jornada.Proveedor))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeStock_jornada.Bodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeStock_jornada.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOOC", oBeStock_jornada.No_DocumentoOC))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOREC", oBeStock_jornada.No_DocumentoRec))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIAOC", oBeStock_jornada.ReferenciaOC))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeStock_jornada.Fecha_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeStock_jornada.TipoTrans))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGO", oBeStock_jornada.Fecha_Agrego))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeStock_jornada.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA_PRODUCTO", oBeStock_jornada.Codigo_barra_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeStock_jornada.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA", oBeStock_jornada.Existencia))
            cmd.Parameters.Add(New SqlParameter("@NOM_UMBAS", oBeStock_jornada.Nom_umBas))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO_PRODUCTO", oBeStock_jornada.Nom_estado_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION_PRODUCTO", oBeStock_jornada.Nom_presentacion_producto))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_ORIGEN", oBeStock_jornada.Ubicacion_origen))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeStock_jornada.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeStock_jornada.Valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeStock_jornada.Valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeStock_jornada.Valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeStock_jornada.Valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeStock_jornada.Valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeStock_jornada.Valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeStock_jornada.Peso_neto))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeStock_jornada.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_REGIMEN", oBeStock_jornada.Codigo_regimen))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_REGIMEN", oBeStock_jornada.Nombre_regimen))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO_REGIMEN", oBeStock_jornada.Dias_vencimiento_regimen))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO_TICKET_TMS", oBeStock_jornada.Fecha_Ingreso_Ticket_TMS))
            cmd.Parameters.Add(New SqlParameter("@ES_RETROACTIVO", oBeStock_jornada.Es_Retroactivo))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeStock_jornada.Factor))
            cmd.Parameters.Add(New SqlParameter("@CAMASPORTARIMA", oBeStock_jornada.CamasPorTarima))
            cmd.Parameters.Add(New SqlParameter("@CAJASPORCAMA", oBeStock_jornada.CajasPorCama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_INGRESO_AFECTA_A_SALIDA", oBeStock_jornada.Cantidad_Ingreso_Afecta_A_salida))
            cmd.Parameters.Add(New SqlParameter("@STOCK_JORNADA_HASH", oBeStock_jornada.Stock_Jornada_Hash))
            cmd.Parameters.Add(New SqlParameter("@IDTICKETTMS", oBeStock_jornada.IdTicketTMS))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeStock_jornada.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeStock_jornada.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@CLASIFICACION", oBeStock_jornada.Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@REGIMEN", oBeStock_jornada.Regimen))
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeStock_jornada.Posiciones))
            cmd.Parameters.Add(New SqlParameter("@COSTO_UNITARIO", oBeStock_jornada.Costo_Unitario))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PROCESADO_ERP", oBeStock_jornada.No_Documento_Procesado_ERP))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_ERP", oBeStock_jornada.Procesado_ERP))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", oBeStock_jornada.Fecha_Procesado_Stock_Jornada))

            Dim rowsAfected As Integer = cmd.ExecuteNonQuery()

            'cmd.ExecuteNonQueryAsync()
            'cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAfected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("Error_202211022328: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If Not lConnection Is Nothing Then lConnection.Dispose()
                If Not lTransaction Is Nothing Then lTransaction.Dispose()
            End If

        End Try



    End Function

    Public Shared Function Actualizar(ByRef oBeStock_jornada As clsBeStock_jornada, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_jornada")
            Upd.Add("idstockjornada", "@idstockjornada", DataType.Parametro)
            Upd.Add("idjornadasistema", "@idjornadasistema", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idubicacion_anterior", "@idubicacion_anterior", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Upd.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("añada", "@añada", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("temperatura", "@temperatura", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Upd.Add("propietario", "@propietario", DataType.Parametro)
            Upd.Add("proveedor", "@proveedor", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("no_documentooc", "@no_documentooc", DataType.Parametro)
            Upd.Add("no_documentorec", "@no_documentorec", DataType.Parametro)
            Upd.Add("referenciaoc", "@referenciaoc", DataType.Parametro)
            Upd.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Upd.Add("tipotrans", "@tipotrans", DataType.Parametro)
            Upd.Add("fecha_agrego", "@fecha_agrego", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("codigo_barra_producto", "@codigo_barra_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("existencia", "@existencia", DataType.Parametro)
            Upd.Add("nom_umbas", "@nom_umbas", DataType.Parametro)
            Upd.Add("nom_estado_producto", "@nom_estado_producto", DataType.Parametro)
            Upd.Add("nom_presentacion_producto", "@nom_presentacion_producto", DataType.Parametro)
            Upd.Add("ubicacion_origen", "@ubicacion_origen", DataType.Parametro)
            Upd.Add("no_poliza", "@no_poliza", DataType.Parametro)
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Upd.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Upd.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Upd.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Upd.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Upd.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Upd.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Upd.Add("codigo_regimen", "@codigo_regimen", DataType.Parametro)
            Upd.Add("nombre_regimen", "@nombre_regimen", DataType.Parametro)
            Upd.Add("dias_vencimiento_regimen", "@dias_vencimiento_regimen", DataType.Parametro)
            Upd.Add("fecha_ingreso_ticket_tms", "@fecha_ingreso_ticket_tms", DataType.Parametro)
            Upd.Add("es_retroactivo", "@es_retroactivo", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("camasportarima", "@camasportarima", DataType.Parametro)
            Upd.Add("cajasporcama", "@cajasporcama", DataType.Parametro)
            Upd.Add("cantidad_ingreso_afecta_a_salida", "@cantidad_ingreso_afecta_a_salida", DataType.Parametro)
            '#EJC20210519: En general nada se debería actualizar en stock jornada, pero este campo menos!
            'Así que lo omitiremos explícitamente, osea, lo pongo aquí solo para que sepan que aderede no lo puse jaja.
            'Upd.Add("stock_jornada_hash", "@stock_jornada_hash", DataType.Parametro)
            Upd.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Upd.Add("IdClasificacion", "@IdClasificacion", DataType.Parametro)
            Upd.Add("Clasificacion", "@Clasificacion", DataType.Parametro)
            Upd.Add("Regimen", "@Regimen", DataType.Parametro)
            Upd.Add("Posiciones", "@Posiciones", DataType.Parametro)
            Upd.Add("costo_unitario", "@costo_unitario", DataType.Parametro)
            Upd.Add("no_documento_procesado_erp", "@no_documento_procesado_erp", DataType.Parametro)
            Upd.Add("procesado_erp", "@procesado_erp", DataType.Parametro)

            If oBeStock_jornada.IdProductoTallaColor > 0 Then
                Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
                Upd.Add("Talla", "@Talla", DataType.Parametro)
                Upd.Add("Color", "@Color", DataType.Parametro)
            End If

            Upd.Where("IdStockJornada = @IdStockJornada")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKJORNADA", oBeStock_jornada.IdStockJornada))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADASISTEMA", oBeStock_jornada.IdJornadaSistema))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeStock_jornada.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_jornada.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_jornada.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_jornada.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_jornada.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_jornada.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_jornada.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_jornada.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_jornada.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeStock_jornada.IdUbicacion_anterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_jornada.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_jornada.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_jornada.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_jornada.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_jornada.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_jornada.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_jornada.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_jornada.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_jornada.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_jornada.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_jornada.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_jornada.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_jornada.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_jornada.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_jornada.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_jornada.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_jornada.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_jornada.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_jornada.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_jornada.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_jornada.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_jornada.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock_jornada.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_jornada.Pallet_no_estandar))
            cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeStock_jornada.Propietario))
            cmd.Parameters.Add(New SqlParameter("@PROVEEDOR", oBeStock_jornada.Proveedor))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeStock_jornada.Bodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeStock_jornada.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOOC", oBeStock_jornada.No_DocumentoOC))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTOREC", oBeStock_jornada.No_DocumentoRec))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIAOC", oBeStock_jornada.ReferenciaOC))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeStock_jornada.Fecha_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeStock_jornada.TipoTrans))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGO", oBeStock_jornada.Fecha_Agrego))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeStock_jornada.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA_PRODUCTO", oBeStock_jornada.Codigo_barra_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeStock_jornada.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA", oBeStock_jornada.Existencia))
            cmd.Parameters.Add(New SqlParameter("@NOM_UMBAS", oBeStock_jornada.Nom_umBas))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO_PRODUCTO", oBeStock_jornada.Nom_estado_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION_PRODUCTO", oBeStock_jornada.Nom_presentacion_producto))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_ORIGEN", oBeStock_jornada.Ubicacion_origen))
            cmd.Parameters.Add(New SqlParameter("@NO_POLIZA", oBeStock_jornada.No_poliza))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeStock_jornada.Valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeStock_jornada.Valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeStock_jornada.Valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeStock_jornada.Valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeStock_jornada.Valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeStock_jornada.Valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeStock_jornada.Peso_neto))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeStock_jornada.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_REGIMEN", oBeStock_jornada.Codigo_regimen))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_REGIMEN", oBeStock_jornada.Nombre_regimen))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO_REGIMEN", oBeStock_jornada.Dias_vencimiento_regimen))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO_TICKET_TMS", oBeStock_jornada.Fecha_Ingreso_Ticket_TMS))
            cmd.Parameters.Add(New SqlParameter("@ES_RETROACTIVO", oBeStock_jornada.Es_Retroactivo))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeStock_jornada.Factor))
            cmd.Parameters.Add(New SqlParameter("@CAMASPORTARIMA", oBeStock_jornada.CamasPorTarima))
            cmd.Parameters.Add(New SqlParameter("@CAJASPORCAMA", oBeStock_jornada.CajasPorCama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_INGRESO_AFECTA_A_SALIDA", oBeStock_jornada.Cantidad_Ingreso_Afecta_A_salida))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeStock_jornada.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeStock_jornada.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@CLASIFICACION", oBeStock_jornada.Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@REGIMEN", oBeStock_jornada.Regimen))
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeStock_jornada.Posiciones))
            cmd.Parameters.Add(New SqlParameter("@COSTO_UNITARIO", oBeStock_jornada.Costo_Unitario))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_PROCESADO_ERP", oBeStock_jornada.No_Documento_Procesado_ERP))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_ERP", oBeStock_jornada.Procesado_ERP))

            If oBeStock_jornada.IdProductoTallaColor > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_jornada.IdProductoTallaColor))
                cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_jornada.Talla))
                cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_jornada.Color))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeStock_jornada As clsBeStock_jornada, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_jornada" &
             "  Where(IdStockJornada = @IdStockJornada)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKJORNADA", oBeStock_jornada.IdStockJornada))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar_Historico_desde_HH(ByRef oBeStock_jornada As clsBeStock_jornada, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Eliminar_Historico_desde_HH = False
        Try

            Const sp As String = " Delete from Stock_jornada" &
             "  Where IdStock = @IdStock and IdProductoBodega=@IdProductoBodega and lic_plate=@lic_plate and IdPropietarioBodega=@IdPropietarioBodega "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_jornada.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_jornada.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_jornada.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_jornada.IdPropietarioBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Eliminar_Historico_desde_HH = IIf(rowsAffected > 0, True, False)

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Eliminar_Historico_desde_HH

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_jornada"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        For Each dr As DataRow In lDataTable.Rows
                            vBeStock_jornada = New clsBeStock_jornada()
                            Cargar(vBeStock_jornada, dr)
                            lReturnList.Add(vBeStock_jornada)
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

    Public Shared Sub GetSingle(ByRef pBeStock_jornada As clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada" &
            " Where(IdStockJornada = @IdStockJornada)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeStock_jornada, lDataTable.Rows(0))
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

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockJornada),0) FROM Stock_jornada"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function Insertar_Temporal_Licencias_Pendientes_Retroactivo(Lista_Licencias_pendiente As List(Of clsBeStock_jornada),
                                                                              Optional ByVal pConection As SqlConnection = Nothing,
                                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean


        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Insertar_Temporal_Licencias_Pendientes_Retroactivo = False

        Try

            For Each Lic_Sin_Registro In Lista_Licencias_pendiente
                Insertar_LIcencia_Pendiente(Lic_Sin_Registro, lConnection, lTransaction)
                Insertar_Temporal_Licencias_Pendientes_Retroactivo = True
            Next

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Insertar_LIcencia_Pendiente(ByRef Lic_Sin_Registro As clsBeStock_jornada,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean

        Try

            Ins.Init("licencias_pendientes_retroactivo")
            Ins.Add("licencia", "@lic_plate", DataType.Parametro)
            Ins.Add("fecha_ticket", "@fecha_ticket", DataType.Parametro)
            Ins.Add("fecha_inicial", "@fecha_inicial", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            If sp.Trim = "" Then
                Throw New Exception("ERROR_202211022330: No se pudo generar la cadena de insert, por algún extraño valor supongo.")
            End If

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = sp}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", Lic_Sin_Registro.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_TICKET", Lic_Sin_Registro.Fecha_Ingreso_Ticket_TMS))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIAL", Lic_Sin_Registro.Fecha))
            Dim rowsAfected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_14042023: Se agregó licencia " & Lic_Sin_Registro.Lic_plate & " a proceso_licencias_pendientes_retroactivo")

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAfected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("Error_202211022328: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If Not lConnection Is Nothing Then lConnection.Dispose()
                If Not lTransaction Is Nothing Then lTransaction.Dispose()
            End If

        End Try

    End Function

    Public Shared Function Limpiar_Temporal_Licencias(Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " truncate table licencias_pendientes_retroactivo"


            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Limpiar_Temporal_Stock_Jornada(Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " truncate table stock_jornada_temporal"


            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class