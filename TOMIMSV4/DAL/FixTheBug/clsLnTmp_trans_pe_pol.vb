Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTmp_trans_pe_pol

    Public Shared Sub Cargar(ByRef oBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol, ByRef dr As DataRow)
        Try
            With oBeTmp_trans_pe_pol
                .IdOrdenPedidoPol = IIf(IsDBNull(dr.Item("IdOrdenPedidoPol")), 0, dr.Item("IdOrdenPedidoPol"))
                .IdOrdenPedidoEnc = IIf(IsDBNull(dr.Item("IdOrdenPedidoEnc")), 0, dr.Item("IdOrdenPedidoEnc"))
                .Bl_no = IIf(IsDBNull(dr.Item("bl_no")), "", dr.Item("bl_no"))
                .NoPoliza = IIf(IsDBNull(dr.Item("NoPoliza")), "", dr.Item("NoPoliza"))
                .Pto_descarga = IIf(IsDBNull(dr.Item("pto_descarga")), "", dr.Item("pto_descarga"))
                .Viaje_no = IIf(IsDBNull(dr.Item("viaje_no")), "", dr.Item("viaje_no"))
                .Buque_no = IIf(IsDBNull(dr.Item("buque_no")), "", dr.Item("buque_no"))
                .Remitente = IIf(IsDBNull(dr.Item("remitente")), "", dr.Item("remitente"))
                .Fecha_abordaje = IIf(IsDBNull(dr.Item("fecha_abordaje")), Date.Now, dr.Item("fecha_abordaje"))
                .Destino = IIf(IsDBNull(dr.Item("destino")), "", dr.Item("destino"))
                .Dir_destino = IIf(IsDBNull(dr.Item("dir_destino")), "", dr.Item("dir_destino"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Po_number = IIf(IsDBNull(dr.Item("po_number")), "", dr.Item("po_number"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
                .Piezas = IIf(IsDBNull(dr.Item("piezas")), 0, dr.Item("piezas"))
                .Total_kgs = IIf(IsDBNull(dr.Item("total_kgs")), 0.0, dr.Item("total_kgs"))
                .Cbm = IIf(IsDBNull(dr.Item("cbm")), 0.0, dr.Item("cbm"))
                .Dua = IIf(IsDBNull(dr.Item("dua")), "", dr.Item("dua"))
                .Fecha_poliza = IIf(IsDBNull(dr.Item("fecha_poliza")), Date.Now, dr.Item("fecha_poliza"))
                .Pais_procede = IIf(IsDBNull(dr.Item("pais_procede")), "", dr.Item("pais_procede"))
                .Tipo_cambio = IIf(IsDBNull(dr.Item("tipo_cambio")), 0.0, dr.Item("tipo_cambio"))
                .Total_valoraduana = IIf(IsDBNull(dr.Item("total_valoraduana")), 0.0, dr.Item("total_valoraduana"))
                .Total_lineas = IIf(IsDBNull(dr.Item("total_lineas")), 0, dr.Item("total_lineas"))
                .Total_bultos = IIf(IsDBNull(dr.Item("total_bultos")), 0, dr.Item("total_bultos"))
                .Total_bultos_peso = IIf(IsDBNull(dr.Item("total_bultos_peso")), 0.0, dr.Item("total_bultos_peso"))
                .Total_usd = IIf(IsDBNull(dr.Item("total_usd")), 0.0, dr.Item("total_usd"))
                .Total_flete = IIf(IsDBNull(dr.Item("total_flete")), 0.0, dr.Item("total_flete"))
                .Total_seguro = IIf(IsDBNull(dr.Item("total_seguro")), 0.0, dr.Item("total_seguro"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Clave_aduana = IIf(IsDBNull(dr.Item("clave_aduana")), "", dr.Item("clave_aduana"))
                .Nit_imp_exp = IIf(IsDBNull(dr.Item("nit_imp_exp")), "", dr.Item("nit_imp_exp"))
                .Clase = IIf(IsDBNull(dr.Item("clase")), "", dr.Item("clase"))
                .Mod_transporte = IIf(IsDBNull(dr.Item("mod_transporte")), "", dr.Item("mod_transporte"))
                .Total_liquidar = IIf(IsDBNull(dr.Item("total_liquidar")), 0.0, dr.Item("total_liquidar"))
                .Total_general = IIf(IsDBNull(dr.Item("total_general")), 0.0, dr.Item("total_general"))
                .Codigo_poliza = IIf(IsDBNull(dr.Item("codigo_poliza")), "", dr.Item("codigo_poliza"))
                .Ticket = IIf(IsDBNull(dr.Item("ticket")), "", dr.Item("ticket"))
                .Numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), "", dr.Item("numero_orden"))
                .Fecha_aceptacion = IIf(IsDBNull(dr.Item("fecha_aceptacion")), Date.Now, dr.Item("fecha_aceptacion"))
                .Fecha_llegada = IIf(IsDBNull(dr.Item("fecha_llegada")), Date.Now, dr.Item("fecha_llegada"))
                .Total_otros = IIf(IsDBNull(dr.Item("total_otros")), 0.0, dr.Item("total_otros"))
                .IdRegimen = IIf(IsDBNull(dr.Item("IdRegimen")), 0, dr.Item("IdRegimen"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tmp_trans_pe_pol")
            Ins.Add("idordenpedidopol", "@idordenpedidopol", DataType.Parametro)
            Ins.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Ins.Add("bl_no", "@bl_no", DataType.Parametro)
            Ins.Add("nopoliza", "@nopoliza", DataType.Parametro)
            Ins.Add("pto_descarga", "@pto_descarga", DataType.Parametro)
            Ins.Add("viaje_no", "@viaje_no", DataType.Parametro)
            Ins.Add("buque_no", "@buque_no", DataType.Parametro)
            Ins.Add("remitente", "@remitente", DataType.Parametro)
            Ins.Add("fecha_abordaje", "@fecha_abordaje", DataType.Parametro)
            Ins.Add("destino", "@destino", DataType.Parametro)
            Ins.Add("dir_destino", "@dir_destino", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("po_number", "@po_number", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("piezas", "@piezas", DataType.Parametro)
            Ins.Add("total_kgs", "@total_kgs", DataType.Parametro)
            Ins.Add("cbm", "@cbm", DataType.Parametro)
            Ins.Add("dua", "@dua", DataType.Parametro)
            Ins.Add("fecha_poliza", "@fecha_poliza", DataType.Parametro)
            Ins.Add("pais_procede", "@pais_procede", DataType.Parametro)
            Ins.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Ins.Add("total_valoraduana", "@total_valoraduana", DataType.Parametro)
            Ins.Add("total_lineas", "@total_lineas", DataType.Parametro)
            Ins.Add("total_bultos", "@total_bultos", DataType.Parametro)
            Ins.Add("total_bultos_peso", "@total_bultos_peso", DataType.Parametro)
            Ins.Add("total_usd", "@total_usd", DataType.Parametro)
            Ins.Add("total_flete", "@total_flete", DataType.Parametro)
            Ins.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Ins.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Ins.Add("clase", "@clase", DataType.Parametro)
            Ins.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Ins.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Ins.Add("total_general", "@total_general", DataType.Parametro)
            Ins.Add("codigo_poliza", "@codigo_poliza", DataType.Parametro)
            Ins.Add("ticket", "@ticket", DataType.Parametro)
            Ins.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Ins.Add("fecha_aceptacion", "@fecha_aceptacion", DataType.Parametro)
            Ins.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Ins.Add("total_otros", "@total_otros", DataType.Parametro)
            Ins.Add("idregimen", "@idregimen", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOPOL", oBeTmp_trans_pe_pol.IdOrdenPedidoPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTmp_trans_pe_pol.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@BL_NO", oBeTmp_trans_pe_pol.Bl_no))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", oBeTmp_trans_pe_pol.NoPoliza))
            cmd.Parameters.Add(New SqlParameter("@PTO_DESCARGA", oBeTmp_trans_pe_pol.Pto_descarga))
            cmd.Parameters.Add(New SqlParameter("@VIAJE_NO", oBeTmp_trans_pe_pol.Viaje_no))
            cmd.Parameters.Add(New SqlParameter("@BUQUE_NO", oBeTmp_trans_pe_pol.Buque_no))
            cmd.Parameters.Add(New SqlParameter("@REMITENTE", oBeTmp_trans_pe_pol.Remitente))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ABORDAJE", oBeTmp_trans_pe_pol.Fecha_abordaje))
            cmd.Parameters.Add(New SqlParameter("@DESTINO", oBeTmp_trans_pe_pol.Destino))
            cmd.Parameters.Add(New SqlParameter("@DIR_DESTINO", oBeTmp_trans_pe_pol.Dir_destino))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTmp_trans_pe_pol.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PO_NUMBER", oBeTmp_trans_pe_pol.Po_number))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTmp_trans_pe_pol.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PIEZAS", oBeTmp_trans_pe_pol.Piezas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_KGS", oBeTmp_trans_pe_pol.Total_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBM", oBeTmp_trans_pe_pol.Cbm))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBeTmp_trans_pe_pol.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTmp_trans_pe_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", oBeTmp_trans_pe_pol.Pais_procede))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTmp_trans_pe_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTmp_trans_pe_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEAS", oBeTmp_trans_pe_pol.Total_lineas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS", oBeTmp_trans_pe_pol.Total_bultos))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTmp_trans_pe_pol.Total_bultos_peso))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTmp_trans_pe_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTmp_trans_pe_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTmp_trans_pe_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTmp_trans_pe_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTmp_trans_pe_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTmp_trans_pe_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTmp_trans_pe_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTmp_trans_pe_pol.Clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTmp_trans_pe_pol.Nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTmp_trans_pe_pol.Clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTmp_trans_pe_pol.Mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTmp_trans_pe_pol.Total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTmp_trans_pe_pol.Total_general))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_POLIZA", oBeTmp_trans_pe_pol.Codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@TICKET", oBeTmp_trans_pe_pol.Ticket))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTmp_trans_pe_pol.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACION", oBeTmp_trans_pe_pol.Fecha_aceptacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBeTmp_trans_pe_pol.Fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_OTROS", oBeTmp_trans_pe_pol.Total_otros))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTmp_trans_pe_pol.IdRegimen))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tmp_trans_pe_pol")
            Upd.Add("idordenpedidopol", "@idordenpedidopol", DataType.Parametro)
            Upd.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Upd.Add("bl_no", "@bl_no", DataType.Parametro)
            Upd.Add("nopoliza", "@nopoliza", DataType.Parametro)
            Upd.Add("pto_descarga", "@pto_descarga", DataType.Parametro)
            Upd.Add("viaje_no", "@viaje_no", DataType.Parametro)
            Upd.Add("buque_no", "@buque_no", DataType.Parametro)
            Upd.Add("remitente", "@remitente", DataType.Parametro)
            Upd.Add("fecha_abordaje", "@fecha_abordaje", DataType.Parametro)
            Upd.Add("destino", "@destino", DataType.Parametro)
            Upd.Add("dir_destino", "@dir_destino", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("po_number", "@po_number", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("piezas", "@piezas", DataType.Parametro)
            Upd.Add("total_kgs", "@total_kgs", DataType.Parametro)
            Upd.Add("cbm", "@cbm", DataType.Parametro)
            Upd.Add("dua", "@dua", DataType.Parametro)
            Upd.Add("fecha_poliza", "@fecha_poliza", DataType.Parametro)
            Upd.Add("pais_procede", "@pais_procede", DataType.Parametro)
            Upd.Add("tipo_cambio", "@tipo_cambio", DataType.Parametro)
            Upd.Add("total_valoraduana", "@total_valoraduana", DataType.Parametro)
            Upd.Add("total_lineas", "@total_lineas", DataType.Parametro)
            Upd.Add("total_bultos", "@total_bultos", DataType.Parametro)
            Upd.Add("total_bultos_peso", "@total_bultos_peso", DataType.Parametro)
            Upd.Add("total_usd", "@total_usd", DataType.Parametro)
            Upd.Add("total_flete", "@total_flete", DataType.Parametro)
            Upd.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Upd.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Upd.Add("clase", "@clase", DataType.Parametro)
            Upd.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Upd.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Upd.Add("total_general", "@total_general", DataType.Parametro)
            Upd.Add("codigo_poliza", "@codigo_poliza", DataType.Parametro)
            Upd.Add("ticket", "@ticket", DataType.Parametro)
            Upd.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Upd.Add("fecha_aceptacion", "@fecha_aceptacion", DataType.Parametro)
            Upd.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Upd.Add("total_otros", "@total_otros", DataType.Parametro)
            Upd.Add("idregimen", "@idregimen", DataType.Parametro)
            Upd.Add("idregimen", "@idregimen", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOPOL", oBeTmp_trans_pe_pol.IdOrdenPedidoPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTmp_trans_pe_pol.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@BL_NO", oBeTmp_trans_pe_pol.Bl_no))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", oBeTmp_trans_pe_pol.NoPoliza))
            cmd.Parameters.Add(New SqlParameter("@PTO_DESCARGA", oBeTmp_trans_pe_pol.Pto_descarga))
            cmd.Parameters.Add(New SqlParameter("@VIAJE_NO", oBeTmp_trans_pe_pol.Viaje_no))
            cmd.Parameters.Add(New SqlParameter("@BUQUE_NO", oBeTmp_trans_pe_pol.Buque_no))
            cmd.Parameters.Add(New SqlParameter("@REMITENTE", oBeTmp_trans_pe_pol.Remitente))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ABORDAJE", oBeTmp_trans_pe_pol.Fecha_abordaje))
            cmd.Parameters.Add(New SqlParameter("@DESTINO", oBeTmp_trans_pe_pol.Destino))
            cmd.Parameters.Add(New SqlParameter("@DIR_DESTINO", oBeTmp_trans_pe_pol.Dir_destino))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTmp_trans_pe_pol.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PO_NUMBER", oBeTmp_trans_pe_pol.Po_number))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTmp_trans_pe_pol.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PIEZAS", oBeTmp_trans_pe_pol.Piezas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_KGS", oBeTmp_trans_pe_pol.Total_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBM", oBeTmp_trans_pe_pol.Cbm))
            cmd.Parameters.Add(New SqlParameter("@DUA", oBeTmp_trans_pe_pol.Dua))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTmp_trans_pe_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", oBeTmp_trans_pe_pol.Pais_procede))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTmp_trans_pe_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTmp_trans_pe_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEAS", oBeTmp_trans_pe_pol.Total_lineas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS", oBeTmp_trans_pe_pol.Total_bultos))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTmp_trans_pe_pol.Total_bultos_peso))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTmp_trans_pe_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTmp_trans_pe_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTmp_trans_pe_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTmp_trans_pe_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTmp_trans_pe_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTmp_trans_pe_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTmp_trans_pe_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTmp_trans_pe_pol.Clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTmp_trans_pe_pol.Nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTmp_trans_pe_pol.Clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTmp_trans_pe_pol.Mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTmp_trans_pe_pol.Total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTmp_trans_pe_pol.Total_general))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_POLIZA", oBeTmp_trans_pe_pol.Codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@TICKET", oBeTmp_trans_pe_pol.Ticket))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTmp_trans_pe_pol.Numero_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACION", oBeTmp_trans_pe_pol.Fecha_aceptacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBeTmp_trans_pe_pol.Fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_OTROS", oBeTmp_trans_pe_pol.Total_otros))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTmp_trans_pe_pol.IdRegimen))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tmp_trans_pe_pol"

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

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTmp_trans_pe_pol)

        Dim lReturnList As New List(Of clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTmp_trans_pe_pol = New clsBeTmp_trans_pe_pol()
                            Cargar(vBeTmp_trans_pe_pol, dr)
                            lReturnList.Add(vBeTmp_trans_pe_pol)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Con_Pedido(ByVal lConnection As SqlConnection,
                                              ByVal lTransaction As SqlTransaction) As List(Of clsBeTmp_trans_pe_pol)

        Dim lReturnList As New List(Of clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol 
                                  WHERE IdOrdenPedidoEnc <> 0 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

                For Each dr As DataRow In lDataTable.Rows
                    vBeTmp_trans_pe_pol = New clsBeTmp_trans_pe_pol()
                    Cargar(vBeTmp_trans_pe_pol, dr)
                    lReturnList.Add(vBeTmp_trans_pe_pol)
                Next

            End Using


            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '
    Public Shared Sub GetSingle(ByRef pBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Sub GetSingle(ByRef pBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol,
                                ByVal lConnection As SqlConnection,
                                ByVal lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol 
                                  WHERE (numero_orden = @numero_orden)  "
            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pBeTmp_trans_pe_pol.Numero_orden)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                pBeTmp_trans_pe_pol = Nothing
                Dim vBeTmp_trans_pe_pol_new As New clsBeTmp_trans_pe_pol

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTmp_trans_pe_pol_new, lDataTable.Rows(0))
                    pBeTmp_trans_pe_pol = vBeTmp_trans_pe_pol_new
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenPedidoPol),0) FROM Tmp_trans_pe_pol "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Actualizar_IdPedidoEnc(ByRef oBeTmp_trans_pe_pol As clsBeTmp_trans_pe_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tmp_trans_pe_pol")
            Upd.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Upd.Where("idordenpedidopol = @idordenpedidopol")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOPOL", oBeTmp_trans_pe_pol.IdOrdenPedidoPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTmp_trans_pe_pol.IdOrdenPedidoEnc))

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

    Public Shared Function GetSingleId(ByVal IdPedidoEnc As Integer) As clsBeTmp_trans_pe_pol

        Try

            Dim vSQL = "SELECT * FROM tmp_trans_pe_pol 
                        WHERE(numero_orden = @numero_orden) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", IdPedidoEnc)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTmp_trans_pe_pol()
                        Cargar(Obj, lRow)
                        Return Obj
                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Single_By_No_Orden(ByVal pNoOrden As String) As clsBeTmp_trans_pe_pol

    '	Get_Single_By_No_Orden = Nothing

    '	Try

    '		Const sp As String = "SELECT * FROM Tmp_trans_pe_pol WHERE numero_orden = @numero_orden"

    '		Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

    '			lConnection.Open()

    '			Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '				Using lDTA As New SqlDataAdapter(sp, lConnection)

    '					lDTA.SelectCommand.CommandType = CommandType.Text
    '					lDTA.SelectCommand.Transaction = lTransaction
    '					lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

    '					Dim lDataTable As New DataTable
    '					lDTA.Fill(lDataTable)

    '					Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

    '					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
    '						Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
    '					End If

    '				End Using

    '				lTransaction.Commit()

    '			End Using

    '			lConnection.Close()

    '		End Using

    '	Catch ex As Exception
    '		Throw ex
    '	End Try

    'End Function

    Public Shared Function Get_Single_By_No_Orden(ByVal pNoOrden As String,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeTmp_trans_pe_pol

        Get_Single_By_No_Orden = Nothing

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol WHERE numero_orden = @numero_orden "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
                    Get_Single_By_No_Orden = vBeTmp_trans_pe_pol
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Barras_Escaneadas() As Integer

        Get_Count_Barras_Escaneadas = 0

        Try

            Const sp As String = "SELECT count(IdOrdenPedidoPol) as cantidad FROM Tmp_trans_pe_pol "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Count_Barras_Escaneadas = CInt(lReturnValue)
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

    Public Shared Function Get_Single_By_No_Orden(ByVal pNoOrden As String) As clsBeTmp_trans_pe_pol

        Get_Single_By_No_Orden = Nothing

        Try

            Const sp As String = "SELECT * FROM Tmp_trans_pe_pol WHERE numero_orden = @numero_orden"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@numero_orden", pNoOrden)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTmp_trans_pe_pol As New clsBeTmp_trans_pe_pol

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTmp_trans_pe_pol, lDataTable.Rows(0))
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

    Public Shared Function Get_All_Pendientes_Insert(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTmp_trans_pe_pol)

        Dim lReturnList As New List(Of clsBeTmp_trans_pe_pol)

        Try

            Const sp As String = "SELECT * from tmp_trans_pe_pol where numero_orden in (select copoliza from tablas_relacionadas)
								  AND numero_orden not in (select numero_orden from trans_pe_pol)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTablas_relacionadas As New clsBeTmp_trans_pe_pol

                For Each dr As DataRow In lDataTable.Rows
                    vBeTablas_relacionadas = New clsBeTmp_trans_pe_pol()
                    Cargar(vBeTablas_relacionadas, dr)
                    lReturnList.Add(vBeTablas_relacionadas)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class