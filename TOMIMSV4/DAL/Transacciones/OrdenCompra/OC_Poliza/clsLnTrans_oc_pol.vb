Imports System.Data.SqlClient

Public Class clsLnTrans_oc_pol

    Public Shared Sub Cargar(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_pol
                .IdOrdenCompraPol = IIf(IsDBNull(dr.Item("IdOrdenCompraPol")), 0, dr.Item("IdOrdenCompraPol"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .Bl_No = IIf(IsDBNull(dr.Item("bl_no")), "", dr.Item("bl_no"))
                .NoPoliza = IIf(IsDBNull(dr.Item("NoPoliza")), "", dr.Item("NoPoliza"))
                .Pto_Descarga = IIf(IsDBNull(dr.Item("pto_descarga")), "", dr.Item("pto_descarga"))
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
                .Total_bultos_Peso_Bruto = IIf(IsDBNull(dr.Item("total_bultos_peso")), 0.0, dr.Item("total_bultos_peso"))
                .Total_bultos_Peso_Neto = IIf(IsDBNull(dr.Item("total_bultos_peso_neto")), 0.0, dr.Item("total_bultos_peso_neto"))
                .Total_usd = IIf(IsDBNull(dr.Item("total_usd")), 0.0, dr.Item("total_usd"))
                .Total_flete = IIf(IsDBNull(dr.Item("total_flete")), 0.0, dr.Item("total_flete"))
                .Total_seguro = IIf(IsDBNull(dr.Item("total_seguro")), 0.0, dr.Item("total_seguro"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdRegimen = IIf(IsDBNull(dr.Item("IdRegimen")), "0", dr.Item("IdRegimen"))
                .codigo_poliza = IIf(IsDBNull(dr.Item("codigo_poliza")), "", dr.Item("codigo_poliza"))
                .ticket = IIf(IsDBNull(dr.Item("ticket")), "", dr.Item("ticket"))
                .numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), 0, dr.Item("numero_orden"))
                .fecha_aceptacion = IIf(IsDBNull(dr.Item("fecha_aceptacion")), Date.Now, dr.Item("fecha_aceptacion"))
                .fecha_llegada = IIf(IsDBNull(dr.Item("fecha_llegada")), Date.Now, dr.Item("fecha_llegada"))
                .total_otros = IIf(IsDBNull(dr.Item("total_otros")), 0, dr.Item("total_otros"))
                'GT22012021
                .clave_aduana = IIf(IsDBNull(dr.Item("clave_aduana")), 0, dr.Item("clave_aduana"))
                .nit_imp_exp = IIf(IsDBNull(dr.Item("nit_imp_exp")), 0, dr.Item("nit_imp_exp"))
                .clase = IIf(IsDBNull(dr.Item("clase")), 0, dr.Item("clase"))
                .mod_transporte = IIf(IsDBNull(dr.Item("mod_transporte")), 0, dr.Item("mod_transporte"))
                .total_liquidar = IIf(IsDBNull(dr.Item("total_liquidar")), 0, dr.Item("total_liquidar"))
                .total_general = IIf(IsDBNull(dr.Item("total_general")), 0, dr.Item("total_general"))
                .Codigo_Barra = IIf(IsDBNull(dr.Item("Codigo_Barra")), "", dr.Item("Codigo_Barra"))
                .activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                '#GT16092024:
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), False, dr.Item("IdBodega"))

            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_oc_pol")
            Ins.Add("idordencomprapol", "@idordencomprapol", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
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
            Ins.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", DataType.Parametro)
            Ins.Add("total_usd", "@total_usd", DataType.Parametro)
            Ins.Add("total_flete", "@total_flete", DataType.Parametro)
            Ins.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            '#EJC20201228: Agregado para cealsa.
            Ins.Add("codigo_poliza", "@codigo_poliza", DataType.Parametro)
            Ins.Add("ticket", "@ticket", DataType.Parametro)
            Ins.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Ins.Add("fecha_aceptacion", "@fecha_aceptacion", DataType.Parametro)
            Ins.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Ins.Add("total_otros", "@total_otros", DataType.Parametro)
            Ins.Add("idregimen", "@idregimen", DataType.Parametro)
            'GT22012021: otros campos para cealsa.
            Ins.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Ins.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Ins.Add("clase", "@clase", DataType.Parametro)
            Ins.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Ins.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Ins.Add("total_general", "@total_general", DataType.Parametro)
            '#EJC20210222:
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            '#GT10102023:
            Ins.Add("activo", "@activo", DataType.Parametro)
            '#GT16092024: asociar bodega a la poliza
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)


            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_oc_pol.IdOrdenCompraPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_pol.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@BL_NO", IIf(oBeTrans_oc_pol.Bl_No Is Nothing, DBNull.Value, oBeTrans_oc_pol.Bl_No)))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", IIf(oBeTrans_oc_pol.NoPoliza Is Nothing, DBNull.Value, oBeTrans_oc_pol.NoPoliza)))
            cmd.Parameters.Add(New SqlParameter("@PTO_DESCARGA", IIf(oBeTrans_oc_pol.Pto_Descarga Is Nothing, DBNull.Value, oBeTrans_oc_pol.Pto_Descarga)))
            cmd.Parameters.Add(New SqlParameter("@VIAJE_NO", IIf(oBeTrans_oc_pol.Viaje_no Is Nothing, DBNull.Value, oBeTrans_oc_pol.Viaje_no)))
            cmd.Parameters.Add(New SqlParameter("@BUQUE_NO", IIf(oBeTrans_oc_pol.Buque_no Is Nothing, DBNull.Value, oBeTrans_oc_pol.Buque_no)))
            cmd.Parameters.Add(New SqlParameter("@REMITENTE", IIf(oBeTrans_oc_pol.Remitente Is Nothing, DBNull.Value, oBeTrans_oc_pol.Remitente)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ABORDAJE", IIf(oBeTrans_oc_pol.Fecha_abordaje = Nothing, DBNull.Value, oBeTrans_oc_pol.Fecha_abordaje)))
            cmd.Parameters.Add(New SqlParameter("@DESTINO", IIf(oBeTrans_oc_pol.Destino Is Nothing, DBNull.Value, oBeTrans_oc_pol.Destino)))
            cmd.Parameters.Add(New SqlParameter("@DIR_DESTINO", IIf(oBeTrans_oc_pol.Dir_destino Is Nothing, DBNull.Value, oBeTrans_oc_pol.Dir_destino)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", IIf(oBeTrans_oc_pol.Descripcion Is Nothing, DBNull.Value, oBeTrans_oc_pol.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@PO_NUMBER", IIf(oBeTrans_oc_pol.Po_number Is Nothing, DBNull.Value, oBeTrans_oc_pol.Po_number)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_pol.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PIEZAS", oBeTrans_oc_pol.Piezas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_KGS", oBeTrans_oc_pol.Total_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBM", oBeTrans_oc_pol.Cbm))
            cmd.Parameters.Add(New SqlParameter("@DUA", IIf(oBeTrans_oc_pol.Dua Is Nothing, DBNull.Value, oBeTrans_oc_pol.Dua)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTrans_oc_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", IIf(oBeTrans_oc_pol.Pais_procede Is Nothing, DBNull.Value, oBeTrans_oc_pol.Pais_procede)))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_oc_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTrans_oc_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEAS", oBeTrans_oc_pol.Total_lineas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS", oBeTrans_oc_pol.Total_bultos))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTrans_oc_pol.Total_bultos_Peso_Bruto))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO_NETO", oBeTrans_oc_pol.Total_bultos_Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTrans_oc_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTrans_oc_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTrans_oc_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_POLIZA", oBeTrans_oc_pol.codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@TICKET", oBeTrans_oc_pol.ticket))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTrans_oc_pol.numero_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACION", oBeTrans_oc_pol.fecha_aceptacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBeTrans_oc_pol.fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_OTROS", oBeTrans_oc_pol.total_otros))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTrans_oc_pol.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTrans_oc_pol.clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTrans_oc_pol.nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTrans_oc_pol.clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTrans_oc_pol.mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTrans_oc_pol.total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTrans_oc_pol.total_general))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_oc_pol.Codigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol.activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_pol.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_pol")
            Upd.Add("idordencomprapol", "@idordencomprapol", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
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
            Upd.Add("total_bultos_peso_neto", "@total_bultos_peso_neto", DataType.Parametro)
            Upd.Add("total_usd", "@total_usd", DataType.Parametro)
            Upd.Add("total_flete", "@total_flete", DataType.Parametro)
            Upd.Add("total_seguro", "@total_seguro", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("codigo_poliza", "@codigo_poliza", DataType.Parametro)
            Upd.Add("ticket", "@ticket", DataType.Parametro)
            Upd.Add("numero_orden", "@numero_orden", DataType.Parametro)
            Upd.Add("fecha_aceptacion", "@fecha_aceptacion", DataType.Parametro)
            Upd.Add("fecha_llegada", "@fecha_llegada", DataType.Parametro)
            Upd.Add("total_otros", "@total_otros", DataType.Parametro)
            Upd.Add("idregimen", "@idregimen", DataType.Parametro)

            'GT22012021: otros campos para cealsa.
            Upd.Add("clave_aduana", "@clave_aduana", DataType.Parametro)
            Upd.Add("nit_imp_exp", "@nit_imp_exp", DataType.Parametro)
            Upd.Add("clase", "@clase", DataType.Parametro)
            Upd.Add("mod_transporte", "@mod_transporte", DataType.Parametro)
            Upd.Add("total_liquidar", "@total_liquidar", DataType.Parametro)
            Upd.Add("total_general", "@total_general", DataType.Parametro)

            '#EJC20210222
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            '#GT10102023:
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)

            Upd.Where("IdOrdenCompraPol = @IdOrdenCompraPol " &
                "AND IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_oc_pol.IdOrdenCompraPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_pol.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@BL_NO", IIf(oBeTrans_oc_pol.Bl_No Is Nothing, DBNull.Value, oBeTrans_oc_pol.Bl_No)))
            cmd.Parameters.Add(New SqlParameter("@NOPOLIZA", IIf(oBeTrans_oc_pol.NoPoliza Is Nothing, DBNull.Value, oBeTrans_oc_pol.NoPoliza)))
            cmd.Parameters.Add(New SqlParameter("@PTO_DESCARGA", IIf(oBeTrans_oc_pol.Pto_Descarga Is Nothing, DBNull.Value, oBeTrans_oc_pol.Pto_Descarga)))
            cmd.Parameters.Add(New SqlParameter("@VIAJE_NO", IIf(oBeTrans_oc_pol.Viaje_no Is Nothing, DBNull.Value, oBeTrans_oc_pol.Viaje_no)))
            cmd.Parameters.Add(New SqlParameter("@BUQUE_NO", IIf(oBeTrans_oc_pol.Buque_no Is Nothing, DBNull.Value, oBeTrans_oc_pol.Buque_no)))
            cmd.Parameters.Add(New SqlParameter("@REMITENTE", IIf(oBeTrans_oc_pol.Remitente Is Nothing, DBNull.Value, oBeTrans_oc_pol.Remitente)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ABORDAJE", IIf(oBeTrans_oc_pol.Fecha_abordaje = Nothing, DBNull.Value, oBeTrans_oc_pol.Fecha_abordaje)))
            cmd.Parameters.Add(New SqlParameter("@DESTINO", IIf(oBeTrans_oc_pol.Destino Is Nothing, DBNull.Value, oBeTrans_oc_pol.Destino)))
            cmd.Parameters.Add(New SqlParameter("@DIR_DESTINO", IIf(oBeTrans_oc_pol.Dir_destino Is Nothing, DBNull.Value, oBeTrans_oc_pol.Dir_destino)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", IIf(oBeTrans_oc_pol.Descripcion Is Nothing, DBNull.Value, oBeTrans_oc_pol.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@PO_NUMBER", IIf(oBeTrans_oc_pol.Po_number Is Nothing, DBNull.Value, oBeTrans_oc_pol.Po_number)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_pol.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PIEZAS", oBeTrans_oc_pol.Piezas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_KGS", oBeTrans_oc_pol.Total_kgs))
            cmd.Parameters.Add(New SqlParameter("@CBM", oBeTrans_oc_pol.Cbm))
            cmd.Parameters.Add(New SqlParameter("@DUA", IIf(oBeTrans_oc_pol.Dua Is Nothing, DBNull.Value, oBeTrans_oc_pol.Dua)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_POLIZA", oBeTrans_oc_pol.Fecha_poliza))
            cmd.Parameters.Add(New SqlParameter("@PAIS_PROCEDE", IIf(oBeTrans_oc_pol.Pais_procede Is Nothing, DBNull.Value, oBeTrans_oc_pol.Pais_procede)))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CAMBIO", oBeTrans_oc_pol.Tipo_cambio))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_VALORADUANA", oBeTrans_oc_pol.Total_valoraduana))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEAS", oBeTrans_oc_pol.Total_lineas))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS", oBeTrans_oc_pol.Total_bultos))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO", oBeTrans_oc_pol.Total_bultos_Peso_Bruto))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_BULTOS_PESO_NETO", oBeTrans_oc_pol.Total_bultos_Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_USD", oBeTrans_oc_pol.Total_usd))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_FLETE", oBeTrans_oc_pol.Total_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_SEGURO", oBeTrans_oc_pol.Total_seguro))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_POLIZA", oBeTrans_oc_pol.codigo_poliza))
            cmd.Parameters.Add(New SqlParameter("@TICKET", oBeTrans_oc_pol.ticket))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTrans_oc_pol.numero_orden))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ACEPTACION", oBeTrans_oc_pol.fecha_aceptacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_LLEGADA", oBeTrans_oc_pol.fecha_llegada))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_OTROS", oBeTrans_oc_pol.total_otros))
            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeTrans_oc_pol.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@CLAVE_ADUANA", oBeTrans_oc_pol.clave_aduana))
            cmd.Parameters.Add(New SqlParameter("@NIT_IMP_EXP", oBeTrans_oc_pol.nit_imp_exp))
            cmd.Parameters.Add(New SqlParameter("@CLASE", oBeTrans_oc_pol.clase))
            cmd.Parameters.Add(New SqlParameter("@MOD_TRANSPORTE", oBeTrans_oc_pol.mod_transporte))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LIQUIDAR", oBeTrans_oc_pol.total_liquidar))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_GENERAL", oBeTrans_oc_pol.total_general))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_oc_pol.Codigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol.activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_pol.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Function Eliminar(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_oc_pol" &
             "  Where(IdOrdenCompraPol = @IdOrdenCompraPol) " &
             "  AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_oc_pol.IdOrdenCompraPol))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_pol.IdOrdenCompraEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_oc_pol" &
            " Where(IdOrdenCompraPol = @IdOrdenCompraPol) " &
            "AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAPOL", oBeTrans_oc_pol.IdOrdenCompraPol))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_pol.IdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_pol, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
