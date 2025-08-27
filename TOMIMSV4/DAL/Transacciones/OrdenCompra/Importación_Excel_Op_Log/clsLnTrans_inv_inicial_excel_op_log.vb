Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_inicial_excel_op_log

    Public Shared Sub Cargar(ByRef oBeTrans_inv_inicial_excel_op_log As clsBeTrans_inv_inicial_excel_op_log, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_inicial_excel_op_log
                .IdInvIniExcel = IIf(IsDBNull(dr.Item("IdInvIniExcel")), 0, dr.Item("IdInvIniExcel"))
                .No_linea = IIf(IsDBNull(dr.Item("no_linea")), 0, dr.Item("no_linea"))
                .Barra = IIf(IsDBNull(dr.Item("barra")), "", dr.Item("barra"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Nit_facturar_a = IIf(IsDBNull(dr.Item("nit_facturar_a")), "", dr.Item("nit_facturar_a"))
                .Nit_propietario = IIf(IsDBNull(dr.Item("nit_propietario")), "", dr.Item("nit_propietario"))
                .Propietario = IIf(IsDBNull(dr.Item("propietario")), "", dr.Item("propietario"))
                .Shipper = IIf(IsDBNull(dr.Item("shipper")), "", dr.Item("shipper"))
                .Pieces = IIf(IsDBNull(dr.Item("pieces")), 0.0, dr.Item("pieces"))
                .Peso_kgs = IIf(IsDBNull(dr.Item("peso_kgs")), 0.0, dr.Item("peso_kgs"))
                .Cbms = IIf(IsDBNull(dr.Item("cbms")), "", dr.Item("cbms"))
                .Valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                .Fob = IIf(IsDBNull(dr.Item("fob")), 0.0, dr.Item("fob"))
                .Flete = IIf(IsDBNull(dr.Item("flete")), 0.0, dr.Item("flete"))
                .Seguro = IIf(IsDBNull(dr.Item("seguro")), 0.0, dr.Item("seguro"))
                .Dai = IIf(IsDBNull(dr.Item("dai")), 0.0, dr.Item("dai"))
                .Iva = IIf(IsDBNull(dr.Item("iva")), 0.0, dr.Item("iva"))
                .Tipo_cambio = IIf(IsDBNull(dr.Item("tipo_cambio")), 0.0, dr.Item("tipo_cambio"))
                .Umbas = IIf(IsDBNull(dr.Item("umbas")), "", dr.Item("umbas"))
                .Presentacion = IIf(IsDBNull(dr.Item("presentacion")), "", dr.Item("presentacion"))
                .Factor_presentacion = IIf(IsDBNull(dr.Item("factor_presentacion")), 0.0, dr.Item("factor_presentacion"))
                .Bultos_por_pallet = IIf(IsDBNull(dr.Item("bultos_por_pallet")), 0.0, dr.Item("bultos_por_pallet"))
                .Clasificacion = IIf(IsDBNull(dr.Item("clasificacion")), "", dr.Item("clasificacion"))
                .Pol_scan_poliza = IIf(IsDBNull(dr.Item("pol_scan_poliza")), "", dr.Item("pol_scan_poliza"))
                .Scan_ticket = IIf(IsDBNull(dr.Item("scan_ticket")), "", dr.Item("scan_ticket"))
                .Nombre_operador = IIf(IsDBNull(dr.Item("nombre_operador")), "", dr.Item("nombre_operador"))
                .Nit_consolidador = IIf(IsDBNull(dr.Item("nit_consolidador")), "", dr.Item("nit_consolidador"))
                .Pol_numero_orden = IIf(IsDBNull(dr.Item("pol_numero_orden")), "", dr.Item("pol_numero_orden"))
                .Pol_numero_duca = IIf(IsDBNull(dr.Item("pol_numero_duca")), "", dr.Item("pol_numero_duca"))
                .Pol_clave_aduana = IIf(IsDBNull(dr.Item("pol_clave_aduana")), "", dr.Item("pol_clave_aduana"))
                .Pol_nit_importador = IIf(IsDBNull(dr.Item("pol_nit_importador")), "", dr.Item("pol_nit_importador"))
                .Pol_regimen = IIf(IsDBNull(dr.Item("pol_regimen")), "", dr.Item("pol_regimen"))
                .Pol_clase = IIf(IsDBNull(dr.Item("pol_clase")), "", dr.Item("pol_clase"))
                .Pol_pais_procedencia = IIf(IsDBNull(dr.Item("pol_pais_procedencia")), "", dr.Item("pol_pais_procedencia"))
                .Pol_modo_transporte = IIf(IsDBNull(dr.Item("pol_modo_transporte")), "", dr.Item("pol_modo_transporte"))
                .Pol_tipo_cambio = IIf(IsDBNull(dr.Item("pol_tipo_cambio")), 0.0, dr.Item("pol_tipo_cambio"))
                .Pol_total_valor_aduana = IIf(IsDBNull(dr.Item("pol_total_valor_aduana")), 0.0, dr.Item("pol_total_valor_aduana"))
                .Pol_total_peso_bruto = IIf(IsDBNull(dr.Item("pol_total_peso_bruto")), 0.0, dr.Item("pol_total_peso_bruto"))
                .Pol_totalfobusd = IIf(IsDBNull(dr.Item("pol_totalfobusd")), 0.0, dr.Item("pol_totalfobusd"))
                .Pol_total_flete_usd = IIf(IsDBNull(dr.Item("pol_total_flete_usd")), 0.0, dr.Item("pol_total_flete_usd"))
                .Pol_total_seguro_usd = IIf(IsDBNull(dr.Item("pol_total_seguro_usd")), 0.0, dr.Item("pol_total_seguro_usd"))
                .Pol_totalotrosgastosusd = IIf(IsDBNull(dr.Item("pol_totalotrosgastosusd")), 0.0, dr.Item("pol_totalotrosgastosusd"))
                .Pol_total_liquidar = IIf(IsDBNull(dr.Item("pol_total_liquidar")), 0.0, dr.Item("pol_total_liquidar"))
                .Pol_total_general = IIf(IsDBNull(dr.Item("pol_total_general")), 0.0, dr.Item("pol_total_general"))
                .Pol_codigo_poliza = IIf(IsDBNull(dr.Item("pol_codigo_poliza")), "", dr.Item("pol_codigo_poliza"))
                .Pol_fecha_llegada = IIf(IsDBNull(dr.Item("pol_fecha_llegada")), New Date(1900, 1, 1), dr.Item("pol_fecha_llegada"))
                .Codigo_ubicacion = IIf(IsDBNull(dr.Item("codigo_ubicacion")), "", dr.Item("codigo_ubicacion"))
                .Codigo_bodega = IIf(IsDBNull(dr.Item("codigo_bodega")), "", dr.Item("codigo_bodega"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Regularizado = IIf(IsDBNull(dr.Item("regularizado")), False, dr.Item("regularizado"))
                .Fecha_procesado = IIf(IsDBNull(dr.Item("fecha_procesado")), New Date(1900, 1, 1), dr.Item("fecha_procesado"))
                .Id_documento_ingreso = IIf(IsDBNull(dr.Item("id_documento_ingreso")), 0, dr.Item("id_documento_ingreso"))
                .Licencia = IIf(IsDBNull(dr.Item("licencia")), "", dr.Item("licencia"))
                .Consolidado = IIf(IsDBNull(dr.Item("consolidado")), 0, dr.Item("consolidado"))
                .Posiciones = IIf(IsDBNull(dr.Item("posiciones")), 0, dr.Item("posiciones"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_inicial_excel_op_log As clsBeTrans_inv_inicial_excel_op_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_inicial_excel_op_log")
            Ins.Add("idinviniexcel", "@idinviniexcel", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("barra", "@barra", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("nit_facturar_a", "@nit_facturar_a", DataType.Parametro)
            Ins.Add("nit_propietario", "@nit_propietario", DataType.Parametro)
            Ins.Add("propietario", "@propietario", DataType.Parametro)
            Ins.Add("shipper", "@shipper", DataType.Parametro)
            Ins.Add("pieces", "@pieces", DataType.Parametro)
            Ins.Add("peso_kgs", "@peso_kgs", DataType.Parametro)
            Ins.Add("cbms", "@cbms", DataType.Parametro)
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("fob", "@fob", DataType.Parametro)
            Ins.Add("flete", "@flete", DataType.Parametro)
            Ins.Add("seguro", "@seguro", DataType.Parametro)
            Ins.Add("dai", "@dai", DataType.Parametro)
            Ins.Add("iva", "@iva", DataType.Parametro)
            Ins.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Ins.Add("umbas", "@umbas", DataType.Parametro)
            Ins.Add("presentacion", "@presentacion", DataType.Parametro)
            Ins.Add("factor_presentacion", "@factor_presentacion", DataType.Parametro)
            Ins.Add("bultos_por_pallet", "@bultos_por_pallet", DataType.Parametro)
            Ins.Add("clasificacion", "@clasificacion", DataType.Parametro)
            Ins.Add("pol_scan_poliza", "@pol_scan_poliza", DataType.Parametro)
            Ins.Add("scan_ticket", "@scan_ticket", DataType.Parametro)
            Ins.Add("nombre_operador", "@nombre_operador", DataType.Parametro)
            Ins.Add("nit_consolidador", "@nit_consolidador", DataType.Parametro)
            Ins.Add("pol_numero_orden", "@pol_numero_orden", DataType.Parametro)
            Ins.Add("pol_numero_duca", "@pol_numero_duca", DataType.Parametro)
            Ins.Add("pol_clave_aduana", "@pol_clave_aduana", DataType.Parametro)
            Ins.Add("pol_nit_importador", "@pol_nit_importador", DataType.Parametro)
            Ins.Add("pol_regimen", "@pol_regimen", DataType.Parametro)
            Ins.Add("pol_clase", "@pol_clase", DataType.Parametro)
            Ins.Add("pol_pais_procedencia", "@pol_pais_procedencia", DataType.Parametro)
            Ins.Add("pol_modo_transporte", "@pol_modo_transporte", DataType.Parametro)
            Ins.Add("pol_tipo_cambio", "@pol_tipo_cambio", DataType.Parametro)
            Ins.Add("pol_total_valor_aduana", "@pol_total_valor_aduana", DataType.Parametro)
            Ins.Add("pol_total_peso_bruto", "@pol_total_peso_bruto", DataType.Parametro)
            Ins.Add("pol_totalfobusd", "@pol_totalfobusd", DataType.Parametro)
            Ins.Add("pol_total_flete_usd", "@pol_total_flete_usd", DataType.Parametro)
            Ins.Add("pol_total_seguro_usd", "@pol_total_seguro_usd", DataType.Parametro)
            Ins.Add("pol_totalotrosgastosusd", "@pol_totalotrosgastosusd", DataType.Parametro)
            Ins.Add("pol_total_liquidar ", "@pol_total_liquidar ", DataType.Parametro)
            Ins.Add("pol_total_general", "@pol_total_general", DataType.Parametro)
            Ins.Add("pol_codigo_poliza", "@pol_codigo_poliza", DataType.Parametro)
            Ins.Add("pol_fecha_llegada", "@pol_fecha_llegada", DataType.Parametro)
            Ins.Add("codigo_ubicacion", "@codigo_ubicacion", DataType.Parametro)
            Ins.Add("codigo_bodega", "@codigo_bodega", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("regularizado", "@regularizado", DataType.Parametro)
            Ins.Add("fecha_procesado", "@fecha_procesado", DataType.Parametro)
            Ins.Add("id_documento_ingreso", "@id_documento_ingreso", DataType.Parametro)
            Ins.Add("licencia", "@licencia", DataType.Parametro)
            Ins.Add("consolidado", "@consolidado", DataType.Parametro)
            Ins.Add("posiciones", "@posiciones", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVINIEXCEL", oBeTrans_inv_inicial_excel_op_log.IdInvIniExcel))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_inv_inicial_excel_op_log.No_linea))
            cmd.Parameters.Add(New SqlParameter("@BARRA", oBeTrans_inv_inicial_excel_op_log.Barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_inv_inicial_excel_op_log.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_inv_inicial_excel_op_log.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NIT_FACTURAR_A", oBeTrans_inv_inicial_excel_op_log.Nit_facturar_a))
            cmd.Parameters.Add(New SqlParameter("@NIT_PROPIETARIO", oBeTrans_inv_inicial_excel_op_log.Nit_propietario))
            cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeTrans_inv_inicial_excel_op_log.Propietario))
            cmd.Parameters.Add(New SqlParameter("@SHIPPER", oBeTrans_inv_inicial_excel_op_log.Shipper))
            cmd.Parameters.Add(New SqlParameter("@PIECES", oBeTrans_inv_inicial_excel_op_log.Pieces))
            cmd.Parameters.Add(New SqlParameter("@PESO_KGS", oBeTrans_inv_inicial_excel_op_log.Peso_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBMS", oBeTrans_inv_inicial_excel_op_log.Cbms))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_inv_inicial_excel_op_log.Valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@FOB", oBeTrans_inv_inicial_excel_op_log.Fob))
            cmd.Parameters.Add(New SqlParameter("@FLETE", oBeTrans_inv_inicial_excel_op_log.Flete))
            cmd.Parameters.Add(New SqlParameter("@SEGURO", oBeTrans_inv_inicial_excel_op_log.Seguro))
            cmd.Parameters.Add(New SqlParameter("@DAI", oBeTrans_inv_inicial_excel_op_log.Dai))
            cmd.Parameters.Add(New SqlParameter("@IVA", oBeTrans_inv_inicial_excel_op_log.Iva))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_inv_inicial_excel_op_log.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_inv_inicial_excel_op_log.Umbas))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeTrans_inv_inicial_excel_op_log.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@FACTOR_PRESENTACION", oBeTrans_inv_inicial_excel_op_log.Factor_presentacion))
            cmd.Parameters.Add(New SqlParameter("@BULTOS_POR_PALLET", oBeTrans_inv_inicial_excel_op_log.Bultos_por_pallet))
            cmd.Parameters.Add(New SqlParameter("@CLASIFICACION", oBeTrans_inv_inicial_excel_op_log.Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@POL_SCAN_POLIZA", oBeTrans_inv_inicial_excel_op_log.Pol_scan_poliza))
            cmd.Parameters.Add(New SqlParameter("@SCAN_TICKET", oBeTrans_inv_inicial_excel_op_log.Scan_ticket))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR", oBeTrans_inv_inicial_excel_op_log.Nombre_operador))
            cmd.Parameters.Add(New SqlParameter("@NIT_CONSOLIDADOR", oBeTrans_inv_inicial_excel_op_log.Nit_consolidador))
            cmd.Parameters.Add(New SqlParameter("@POL_NUMERO_ORDEN", oBeTrans_inv_inicial_excel_op_log.Pol_numero_orden))
            cmd.Parameters.Add(New SqlParameter("@POL_NUMERO_DUCA", oBeTrans_inv_inicial_excel_op_log.Pol_numero_duca))
            cmd.Parameters.Add(New SqlParameter("@POL_CLAVE_ADUANA", oBeTrans_inv_inicial_excel_op_log.Pol_clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@POL_NIT_IMPORTADOR", oBeTrans_inv_inicial_excel_op_log.Pol_nit_importador))
            cmd.Parameters.Add(New SqlParameter("@POL_REGIMEN", oBeTrans_inv_inicial_excel_op_log.Pol_regimen))
            cmd.Parameters.Add(New SqlParameter("@POL_CLASE", oBeTrans_inv_inicial_excel_op_log.Pol_clase))
            cmd.Parameters.Add(New SqlParameter("@POL_PAIS_PROCEDENCIA", oBeTrans_inv_inicial_excel_op_log.Pol_pais_procedencia))
            cmd.Parameters.Add(New SqlParameter("@POL_MODO_TRANSPORTE", oBeTrans_inv_inicial_excel_op_log.Pol_modo_transporte))
            cmd.Parameters.Add(New SqlParameter("@POL_TIPO_CAMBIO", oBeTrans_inv_inicial_excel_op_log.Pol_tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_VALOR_ADUANA", oBeTrans_inv_inicial_excel_op_log.Pol_total_valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_PESO_BRUTO", oBeTrans_inv_inicial_excel_op_log.Pol_total_peso_bruto))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTALFOBUSD", oBeTrans_inv_inicial_excel_op_log.Pol_totalfobusd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_FLETE_USD", oBeTrans_inv_inicial_excel_op_log.Pol_total_flete_usd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_SEGURO_USD", oBeTrans_inv_inicial_excel_op_log.Pol_total_seguro_usd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTALOTROSGASTOSUSD", oBeTrans_inv_inicial_excel_op_log.Pol_totalotrosgastosusd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_LIQUIDAR ", oBeTrans_inv_inicial_excel_op_log.Pol_total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_GENERAL", oBeTrans_inv_inicial_excel_op_log.Pol_total_general))
            cmd.Parameters.Add(New SqlParameter("@POL_CODIGO_POLIZA", oBeTrans_inv_inicial_excel_op_log.Pol_codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@POL_FECHA_LLEGADA", oBeTrans_inv_inicial_excel_op_log.Pol_fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_UBICACION", oBeTrans_inv_inicial_excel_op_log.Codigo_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA", oBeTrans_inv_inicial_excel_op_log.Codigo_bodega))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_inv_inicial_excel_op_log.Referencia))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", oBeTrans_inv_inicial_excel_op_log.Regularizado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO", oBeTrans_inv_inicial_excel_op_log.Fecha_procesado))
            cmd.Parameters.Add(New SqlParameter("@ID_DOCUMENTO_INGRESO", oBeTrans_inv_inicial_excel_op_log.Id_documento_ingreso))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_inv_inicial_excel_op_log.Licencia))
            cmd.Parameters.Add(New SqlParameter("@CONSOLIDADO", oBeTrans_inv_inicial_excel_op_log.Consolidado))
            '#GT04052022_0919: se guardan las posicones ocupadas por la merca
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeTrans_inv_inicial_excel_op_log.Posiciones))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
            If Not lTransaction Is Nothing Then lTransaction.Rollback()

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Sub Guardar_Transaccion(ByVal pListObjMD As List(Of clsBeTrans_inv_inicial_excel_op_log),
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)
        Try

            Dim vIdMaxInvIniExcel As Integer = MaxID(lConnection, lTransaction) + 1

            For Each Obj As clsBeTrans_inv_inicial_excel_op_log In pListObjMD
                Obj.IdInvIniExcel = vIdMaxInvIniExcel
                Insertar(Obj, lConnection, lTransaction)
                vIdMaxInvIniExcel += 1
            Next

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw New Exception(vMsgError)

        End Try

    End Sub



    Public Shared Function Actualizar(ByRef oBeTrans_inv_inicial_excel_op_log As clsBeTrans_inv_inicial_excel_op_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_inicial_excel_op_log")
            Upd.Add("idinviniexcel", "@idinviniexcel", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("barra", "@barra", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("nit_facturar_a", "@nit_facturar_a", DataType.Parametro)
            Upd.Add("nit_propietario", "@nit_propietario", DataType.Parametro)
            Upd.Add("propietario", "@propietario", DataType.Parametro)
            Upd.Add("shipper", "@shipper", DataType.Parametro)
            Upd.Add("pieces", "@pieces", DataType.Parametro)
            Upd.Add("peso_kgs", "@peso_kgs", DataType.Parametro)
            Upd.Add("cbms", "@cbms", DataType.Parametro)
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("fob", "@fob", DataType.Parametro)
            Upd.Add("flete", "@flete", DataType.Parametro)
            Upd.Add("seguro", "@seguro", DataType.Parametro)
            Upd.Add("dai", "@dai", DataType.Parametro)
            Upd.Add("iva", "@iva", DataType.Parametro)
            Upd.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Upd.Add("umbas", "@umbas", DataType.Parametro)
            Upd.Add("presentacion", "@presentacion", DataType.Parametro)
            Upd.Add("factor_presentacion", "@factor_presentacion", DataType.Parametro)
            Upd.Add("bultos_por_pallet", "@bultos_por_pallet", DataType.Parametro)
            Upd.Add("clasificacion", "@clasificacion", DataType.Parametro)
            Upd.Add("pol_scan_poliza", "@pol_scan_poliza", DataType.Parametro)
            Upd.Add("scan_ticket", "@scan_ticket", DataType.Parametro)
            Upd.Add("nombre_operador", "@nombre_operador", DataType.Parametro)
            Upd.Add("nit_consolidador", "@nit_consolidador", DataType.Parametro)
            Upd.Add("pol_numero_orden", "@pol_numero_orden", DataType.Parametro)
            Upd.Add("pol_numero_duca", "@pol_numero_duca", DataType.Parametro)
            Upd.Add("pol_clave_aduana", "@pol_clave_aduana", DataType.Parametro)
            Upd.Add("pol_nit_importador", "@pol_nit_importador", DataType.Parametro)
            Upd.Add("pol_regimen", "@pol_regimen", DataType.Parametro)
            Upd.Add("pol_clase", "@pol_clase", DataType.Parametro)
            Upd.Add("pol_pais_procedencia", "@pol_pais_procedencia", DataType.Parametro)
            Upd.Add("pol_modo_transporte", "@pol_modo_transporte", DataType.Parametro)
            Upd.Add("pol_tipo_cambio", "@pol_tipo_cambio", DataType.Parametro)
            Upd.Add("pol_total_valor_aduana", "@pol_total_valor_aduana", DataType.Parametro)
            Upd.Add("pol_total_peso_bruto", "@pol_total_peso_bruto", DataType.Parametro)
            Upd.Add("pol_totalfobusd", "@pol_totalfobusd", DataType.Parametro)
            Upd.Add("pol_total_flete_usd", "@pol_total_flete_usd", DataType.Parametro)
            Upd.Add("pol_total_seguro_usd", "@pol_total_seguro_usd", DataType.Parametro)
            Upd.Add("pol_totalotrosgastosusd", "@pol_totalotrosgastosusd", DataType.Parametro)
            Upd.Add("pol_total_liquidar ", "@pol_total_liquidar ", DataType.Parametro)
            Upd.Add("pol_total_general", "@pol_total_general", DataType.Parametro)
            Upd.Add("pol_codigo_poliza", "@pol_codigo_poliza", DataType.Parametro)
            Upd.Add("pol_fecha_llegada", "@pol_fecha_llegada", DataType.Parametro)
            Upd.Add("codigo_ubicacion", "@codigo_ubicacion", DataType.Parametro)
            Upd.Add("codigo_bodega", "@codigo_bodega", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("regularizado", "@regularizado", DataType.Parametro)
            Upd.Add("fecha_procesado", "@fecha_procesado", DataType.Parametro)
            Upd.Add("id_documento_ingreso", "@id_documento_ingreso", DataType.Parametro)
            Upd.Where("IdInvIniExcel = @IdInvIniExcel")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVINIEXCEL", oBeTrans_inv_inicial_excel_op_log.IdInvIniExcel))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_inv_inicial_excel_op_log.No_linea))
            cmd.Parameters.Add(New SqlParameter("@BARRA", oBeTrans_inv_inicial_excel_op_log.Barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_inv_inicial_excel_op_log.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_inv_inicial_excel_op_log.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NIT_FACTURAR_A", oBeTrans_inv_inicial_excel_op_log.Nit_facturar_a))
            cmd.Parameters.Add(New SqlParameter("@NIT_PROPIETARIO", oBeTrans_inv_inicial_excel_op_log.Nit_propietario))
            cmd.Parameters.Add(New SqlParameter("@PROPIETARIO", oBeTrans_inv_inicial_excel_op_log.Propietario))
            cmd.Parameters.Add(New SqlParameter("@SHIPPER", oBeTrans_inv_inicial_excel_op_log.Shipper))
            cmd.Parameters.Add(New SqlParameter("@PIECES", oBeTrans_inv_inicial_excel_op_log.Pieces))
            cmd.Parameters.Add(New SqlParameter("@PESO_KGS", oBeTrans_inv_inicial_excel_op_log.Peso_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBMS", oBeTrans_inv_inicial_excel_op_log.Cbms))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_inv_inicial_excel_op_log.Valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@FOB", oBeTrans_inv_inicial_excel_op_log.Fob))
            cmd.Parameters.Add(New SqlParameter("@FLETE", oBeTrans_inv_inicial_excel_op_log.Flete))
            cmd.Parameters.Add(New SqlParameter("@SEGURO", oBeTrans_inv_inicial_excel_op_log.Seguro))
            cmd.Parameters.Add(New SqlParameter("@DAI", oBeTrans_inv_inicial_excel_op_log.Dai))
            cmd.Parameters.Add(New SqlParameter("@IVA", oBeTrans_inv_inicial_excel_op_log.Iva))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_inv_inicial_excel_op_log.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_inv_inicial_excel_op_log.Umbas))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeTrans_inv_inicial_excel_op_log.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@FACTOR_PRESENTACION", oBeTrans_inv_inicial_excel_op_log.Factor_presentacion))
            cmd.Parameters.Add(New SqlParameter("@BULTOS_POR_PALLET", oBeTrans_inv_inicial_excel_op_log.Bultos_por_pallet))
            cmd.Parameters.Add(New SqlParameter("@CLASIFICACION", oBeTrans_inv_inicial_excel_op_log.Clasificacion))
            cmd.Parameters.Add(New SqlParameter("@POL_SCAN_POLIZA", oBeTrans_inv_inicial_excel_op_log.Pol_scan_poliza))
            cmd.Parameters.Add(New SqlParameter("@SCAN_TICKET", oBeTrans_inv_inicial_excel_op_log.Scan_ticket))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR", oBeTrans_inv_inicial_excel_op_log.Nombre_operador))
            cmd.Parameters.Add(New SqlParameter("@NIT_CONSOLIDADOR", oBeTrans_inv_inicial_excel_op_log.Nit_consolidador))
            cmd.Parameters.Add(New SqlParameter("@POL_NUMERO_ORDEN", oBeTrans_inv_inicial_excel_op_log.Pol_numero_orden))
            cmd.Parameters.Add(New SqlParameter("@POL_NUMERO_DUCA", oBeTrans_inv_inicial_excel_op_log.Pol_numero_duca))
            cmd.Parameters.Add(New SqlParameter("@POL_CLAVE_ADUANA", oBeTrans_inv_inicial_excel_op_log.Pol_clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@POL_NIT_IMPORTADOR", oBeTrans_inv_inicial_excel_op_log.Pol_nit_importador))
            cmd.Parameters.Add(New SqlParameter("@POL_REGIMEN", oBeTrans_inv_inicial_excel_op_log.Pol_regimen))
            cmd.Parameters.Add(New SqlParameter("@POL_CLASE", oBeTrans_inv_inicial_excel_op_log.Pol_clase))
            cmd.Parameters.Add(New SqlParameter("@POL_PAIS_PROCEDENCIA", oBeTrans_inv_inicial_excel_op_log.Pol_pais_procedencia))
            cmd.Parameters.Add(New SqlParameter("@POL_MODO_TRANSPORTE", oBeTrans_inv_inicial_excel_op_log.Pol_modo_transporte))
            cmd.Parameters.Add(New SqlParameter("@POL_TIPO_CAMBIO", oBeTrans_inv_inicial_excel_op_log.Pol_tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_VALOR_ADUANA", oBeTrans_inv_inicial_excel_op_log.Pol_total_valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_PESO_BRUTO", oBeTrans_inv_inicial_excel_op_log.Pol_total_peso_bruto))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTALFOBUSD", oBeTrans_inv_inicial_excel_op_log.Pol_totalfobusd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_FLETE_USD", oBeTrans_inv_inicial_excel_op_log.Pol_total_flete_usd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_SEGURO_USD", oBeTrans_inv_inicial_excel_op_log.Pol_total_seguro_usd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTALOTROSGASTOSUSD", oBeTrans_inv_inicial_excel_op_log.Pol_totalotrosgastosusd))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_LIQUIDAR ", oBeTrans_inv_inicial_excel_op_log.Pol_total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@POL_TOTAL_GENERAL", oBeTrans_inv_inicial_excel_op_log.Pol_total_general))
            cmd.Parameters.Add(New SqlParameter("@POL_CODIGO_POLIZA", oBeTrans_inv_inicial_excel_op_log.Pol_codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@POL_FECHA_LLEGADA", oBeTrans_inv_inicial_excel_op_log.Pol_fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_UBICACION", oBeTrans_inv_inicial_excel_op_log.Codigo_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA", oBeTrans_inv_inicial_excel_op_log.Codigo_bodega))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_inv_inicial_excel_op_log.Referencia))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", oBeTrans_inv_inicial_excel_op_log.Regularizado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO", oBeTrans_inv_inicial_excel_op_log.Fecha_procesado))
            cmd.Parameters.Add(New SqlParameter("@ID_DOCUMENTO_INGRESO", oBeTrans_inv_inicial_excel_op_log.Id_documento_ingreso))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_inv_inicial_excel_op_log As clsBeTrans_inv_inicial_excel_op_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_inicial_excel_op_log" &
             "  Where(IdInvIniExcel = @IdInvIniExcel)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVINIEXCEL", oBeTrans_inv_inicial_excel_op_log.IdInvIniExcel))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
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

            Const sp As String = "SELECT * FROM Trans_inv_inicial_excel_op_log"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_inv_inicial_excel_op_log)

        Dim lReturnList As New List(Of clsBeTrans_inv_inicial_excel_op_log)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_inicial_excel_op_log"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_inicial_excel_op_log = New clsBeTrans_inv_inicial_excel_op_log()
                            Cargar(vBeTrans_inv_inicial_excel_op_log, dr)
                            lReturnList.Add(vBeTrans_inv_inicial_excel_op_log)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_inv_inicial_excel_op_log As clsBeTrans_inv_inicial_excel_op_log)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_inicial_excel_op_log" &
            " Where(IdInvIniExcel = @IdInvIniExcel)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_inv_inicial_excel_op_log, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdInvIniExcel),0) FROM Trans_inv_inicial_excel_op_log"

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
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdInvIniExcel),0) FROM Trans_inv_inicial_excel_op_log"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Filter_by_DocIngreso(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "select distinct codigo_bodega,consolidado,
									pol_codigo_poliza,
									id_documento_ingreso,
									referencia,
								    pol_fecha_llegada
									from trans_inv_inicial_excel_op_log "

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_DocIngreso(ByVal pCodigoBodega As String, ByVal pDocIngreso As String) As clsBeTrans_inv_inicial_excel_op_log

        Get_Single_By_DocIngreso = Nothing

        Try


            Dim vSQL As String = "SELECT top 1 * FROM Trans_inv_inicial_excel_op_log where 1=1  "

            If pCodigoBodega = 11 Then

                vSQL += " and id_documento_ingreso = @pDocIngreso"
            Else

                vSQL += " and pol_codigo_poliza = @pDocIngreso"
            End If


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        'lDTA.SelectCommand.Parameters.AddWithValue("@pCodigoBodega", pCodigoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pDocIngreso", pDocIngreso)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_inv_inicial_excel_op_log, lDataTable.Rows(0))
                            Get_Single_By_DocIngreso = vBeTrans_inv_inicial_excel_op_log
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_DocIngreso(ByVal pEsBodegaFiscal As Boolean,
                                                    ByVal pDocIngreso As String,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As clsBeTrans_inv_inicial_excel_op_log

        Get_Single_By_DocIngreso = Nothing

        Try


            Dim vSQL As String = "SELECT top 1 * FROM Trans_inv_inicial_excel_op_log where 1=1 "

            If Not pEsBodegaFiscal Then
                vSQL += " and id_documento_ingreso = @pDocIngreso "
            Else
                vSQL += " and pol_codigo_poliza = @pDocIngreso"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pDocIngreso", pDocIngreso)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log
                    Cargar(vBeTrans_inv_inicial_excel_op_log, lDataTable.Rows(0))
                    Get_Single_By_DocIngreso = vBeTrans_inv_inicial_excel_op_log
                End If

                Return Get_Single_By_DocIngreso

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_DocIngreso(ByVal pEsBodegaFiscal As Boolean,
                                                 ByVal pDocIngreso As String) As List(Of clsBeTrans_inv_inicial_excel_op_log)

        Dim lReturnList As New List(Of clsBeTrans_inv_inicial_excel_op_log)

        Try


            Dim vSQL As String = "SELECT * FROM Trans_inv_inicial_excel_op_log where 1=1  "

            If Not pEsBodegaFiscal Then
                vSQL += " and id_documento_ingreso = @pDocIngreso"
            Else
                vSQL += " and pol_codigo_poliza = @pDocIngreso"
            End If

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        'lDTA.SelectCommand.Parameters.AddWithValue("@pCodigoBodega", pCodigoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pDocIngreso", pDocIngreso)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_inicial_excel_op_log = New clsBeTrans_inv_inicial_excel_op_log()
                            Cargar(vBeTrans_inv_inicial_excel_op_log, dr)
                            lReturnList.Add(vBeTrans_inv_inicial_excel_op_log)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_DocIngreso(ByVal pEsBodegaFiscal As Boolean,
                                                 ByVal pDocIngreso As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_inicial_excel_op_log)

        Dim lReturnList As New List(Of clsBeTrans_inv_inicial_excel_op_log)

        Try


            Dim vSQL As String = "SELECT * FROM Trans_inv_inicial_excel_op_log where 1=1  "

            If Not pEsBodegaFiscal Then
                vSQL += " and id_documento_ingreso = @pDocIngreso"
            Else
                vSQL += " and pol_codigo_poliza = @pDocIngreso"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pDocIngreso", pDocIngreso)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_inv_inicial_excel_op_log As New clsBeTrans_inv_inicial_excel_op_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_inicial_excel_op_log = New clsBeTrans_inv_inicial_excel_op_log()
                    Cargar(vBeTrans_inv_inicial_excel_op_log, dr)
                    lReturnList.Add(vBeTrans_inv_inicial_excel_op_log)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Eliminar_Temporal(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_inicial_excel_op_log"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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
