Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_transacciones_out

    Public Shared Sub Cargar(ByRef oBeI_nav_transacciones_out As clsBeI_nav_transacciones_out, ByRef dr As DataRow)

        Try

            With oBeI_nav_transacciones_out

                .Idtransaccion = IIf(IsDBNull(dr.Item("idtransaccion")), 0, dr.Item("idtransaccion"))
                .Idempresa = IIf(IsDBNull(dr.Item("idempresa")), 0, dr.Item("idempresa"))
                .Idbodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Idpropietario = IIf(IsDBNull(dr.Item("idpropietario")), 0, dr.Item("idpropietario"))
                .Idpropietariobodega = IIf(IsDBNull(dr.Item("idpropietariobodega")), 0, dr.Item("idpropietariobodega"))
                .Idordencompra = IIf(IsDBNull(dr.Item("idordencompra")), 0, dr.Item("idordencompra"))
                .Idrecepcionenc = IIf(IsDBNull(dr.Item("idrecepcionenc")), 0, dr.Item("idrecepcionenc"))
                .Idpedidoenc = IIf(IsDBNull(dr.Item("idpedidoenc")), 0, dr.Item("idpedidoenc"))
                .Iddespachoenc = IIf(IsDBNull(dr.Item("iddespachoenc")), 0, dr.Item("iddespachoenc"))
                .Idproductobodega = IIf(IsDBNull(dr.Item("idproductobodega")), 0, dr.Item("idproductobodega"))
                .Idproducto = IIf(IsDBNull(dr.Item("idproducto")), 0, dr.Item("idproducto"))
                .Idunidadmedida = IIf(IsDBNull(dr.Item("idunidadmedida")), 0, dr.Item("idunidadmedida"))
                .Idpresentacion = IIf(IsDBNull(dr.Item("idpresentacion")), 0, dr.Item("idpresentacion"))
                .Idproductoestado = IIf(IsDBNull(dr.Item("idproductoestado")), 0, dr.Item("idproductoestado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Nothing, dr.Item("fecha_vence"))
                .Fecha_recepcion = IIf(IsDBNull(dr.Item("fecha_recepcion")), Nothing, dr.Item("fecha_recepcion"))
                .No_pedido = IIf(IsDBNull(dr.Item("no_pedido")), "", dr.Item("no_pedido"))
                .No_linea = IIf(IsDBNull(dr.Item("no_linea")), "", dr.Item("no_linea"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Codigo_variante = IIf(IsDBNull(dr.Item("codigo_variante")), "", dr.Item("codigo_variante"))
                .Unidad_medida = IIf(IsDBNull(dr.Item("unidad_medida")), "", dr.Item("unidad_medida"))
                .Tipo_transaccion = IIf(IsDBNull(dr.Item("tipo_transaccion")), "", dr.Item("tipo_transaccion"))
                .Enviado = IIf(IsDBNull(dr.Item("enviado")), False, dr.Item("enviado"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Cantidad_Esperada = IIf(IsDBNull(dr.Item("Cantidad_Esperada")), 0.0, dr.Item("Cantidad_Esperada"))
                .Lic_Plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Uds_Lic_Plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0.0, dr.Item("uds_lic_plate"))
                .Cantidad_Presentacion = IIf(IsDBNull(dr.Item("cantidad_presentacion")), 0.0, dr.Item("cantidad_presentacion"))
                .IdTipoDocumento = IIf(IsDBNull(dr.Item("IdTipoDocumento")), 0, dr.Item("IdTipoDocumento"))
                .codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), 0, dr.Item("codigo_barra"))
                .valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0, dr.Item("valor_aduana"))
                .valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0, dr.Item("valor_fob"))
                .valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0, dr.Item("valor_iva"))
                .valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0, dr.Item("valor_dai"))
                .valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0, dr.Item("valor_seguro"))
                .valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0, dr.Item("valor_flete"))
                .peso_neto = IIf(IsDBNull(dr.Item("peso_neto")), 0, dr.Item("peso_neto"))
                .peso_bruto = IIf(IsDBNull(dr.Item("peso_bruto")), 0, dr.Item("peso_bruto"))
                .fecha_despacho = IIf(IsDBNull(dr.Item("fecha_despacho")), Nothing, dr.Item("fecha_despacho"))
                .no_documento_salida_ref_devol = IIf(IsDBNull(dr.Item("no_documento_salida_ref_devol")), "", dr.Item("no_documento_salida_ref_devol"))
                .IdPedidoEncDevol = IIf(IsDBNull(dr.Item("IdPedidoEncDevol")), 0, dr.Item("IdPedidoEncDevol"))
                .IdDespachoDet = IIf(IsDBNull(dr.Item("IdDespachoDet")), 0, dr.Item("IdDespachoDet"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .Cantidad_Enviada = IIf(IsDBNull(dr.Item("Cantidad_Enviada")), 0, dr.Item("Cantidad_Enviada"))
                .Cantidad_Pendiente = IIf(IsDBNull(dr.Item("Cantidad_Pendiente")), 0, dr.Item("Cantidad_Pendiente"))
                .Auditar = IIf(IsDBNull(dr.Item("Auditar")), False, dr.Item("Auditar"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_transacciones_out As clsBeI_nav_transacciones_out, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_transacciones_out")
            Ins.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idordencompra", "@idordencompra", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Ins.Add("no_pedido", "@no_pedido", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("codigo_variante", "@codigo_variante", DataType.Parametro)
            Ins.Add("unidad_medida", "@unidad_medida", DataType.Parametro)
            Ins.Add("tipo_transaccion", "@tipo_transaccion", DataType.Parametro)
            Ins.Add("enviado", "@enviado", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("cantidad_esperada", "@cantidad_esperada", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Ins.Add("IdTipoDocumento", "@IdTipoDocumento", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Ins.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Ins.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Ins.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Ins.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Ins.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Ins.Add("peso_bruto", "@peso_bruto", DataType.Parametro)
            Ins.Add("fecha_despacho", "@fecha_despacho", DataType.Parametro)
            '#EJC20210617:Devolución Idealsa con referencia.
            Ins.Add("no_documento_salida_ref_devol", "@no_documento_salida_ref_devol", DataType.Parametro)
            Ins.Add("IdPedidoEncDevol", "@IdPedidoEncDevol", DataType.Parametro)
            Ins.Add("IdDespachoDet", "@IdDespachoDet", DataType.Parametro)
            Ins.Add("IdRecepcionDet", "@IdRecepcionDet", DataType.Parametro)
            Ins.Add("cantidad_enviada", "@cantidad_enviada", DataType.Parametro)
            Ins.Add("cantidad_pendiente", "@cantidad_pendiente", DataType.Parametro)
            Ins.Add("auditar", "@auditar", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_out.Idempresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_transacciones_out.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeI_nav_transacciones_out.Idpropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_out.Idpropietariobodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_out.Idordencompra))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_out.Idrecepcionenc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_transacciones_out.Idpedidoenc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeI_nav_transacciones_out.Iddespachoenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_out.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_out.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_out.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_out.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_out.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_transacciones_out.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeI_nav_transacciones_out.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_transacciones_out.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeI_nav_transacciones_out.Fecha_vence = Nothing, DBNull.Value, oBeI_nav_transacciones_out.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeI_nav_transacciones_out.Fecha_recepcion))
            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO", oBeI_nav_transacciones_out.No_pedido))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_transacciones_out.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_transacciones_out.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeI_nav_transacciones_out.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_out.Codigo_variante))
            cmd.Parameters.Add(New SqlParameter("@UNIDAD_MEDIDA", oBeI_nav_transacciones_out.Unidad_medida))
            cmd.Parameters.Add(New SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_out.Tipo_transaccion))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_transacciones_out.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_transacciones_out.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_out.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_transacciones_out.User_mod))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ESPERADA", oBeI_nav_transacciones_out.Cantidad_Esperada))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeI_nav_transacciones_out.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeI_nav_transacciones_out.Uds_Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_transacciones_out.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTO", oBeI_nav_transacciones_out.IdTipoDocumento))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_transacciones_out.codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeI_nav_transacciones_out.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeI_nav_transacciones_out.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeI_nav_transacciones_out.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeI_nav_transacciones_out.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeI_nav_transacciones_out.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeI_nav_transacciones_out.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeI_nav_transacciones_out.peso_neto))
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeI_nav_transacciones_out.peso_bruto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHO", IIf(oBeI_nav_transacciones_out.fecha_despacho = Nothing, DBNull.Value, oBeI_nav_transacciones_out.fecha_despacho)))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_SALIDA_REF_DEVOL", IIf(oBeI_nav_transacciones_out.fecha_despacho = Nothing, DBNull.Value, oBeI_nav_transacciones_out.no_documento_salida_ref_devol)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCDEVOL", IIf(oBeI_nav_transacciones_out.no_documento_salida_ref_devol = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdPedidoEncDevol)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", IIf(oBeI_nav_transacciones_out.IdDespachoDet = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdDespachoDet)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeI_nav_transacciones_out.IdRecepcionDet = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ENVIADA", oBeI_nav_transacciones_out.Cantidad_Enviada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PENDIENTE", oBeI_nav_transacciones_out.Cantidad_Pendiente))
            cmd.Parameters.Add(New SqlParameter("@AUDITAR", oBeI_nav_transacciones_out.Auditar))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_transacciones_out As clsBeI_nav_transacciones_out, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idordencompra", "@idordencompra", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Upd.Add("no_pedido", "@no_pedido", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("codigo_variante", "@codigo_variante", DataType.Parametro)
            Upd.Add("unidad_medida", "@unidad_medida", DataType.Parametro)
            Upd.Add("tipo_transaccion", "@tipo_transaccion", DataType.Parametro)
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("cantidad_esperada", "@cantidad_esperada", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Upd.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Upd.Add("IdTipoDocumento", "@IdTipoDocumento", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Upd.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Upd.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Upd.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Upd.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Upd.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Upd.Add("peso_bruto", "@peso_bruto", DataType.Parametro)
            Upd.Add("no_documento_salida_ref_devol", "@no_documento_salida_ref_devol", DataType.Parametro)
            Upd.Add("IdPedidoEncDevol", "@IdPedidoEncDevol", DataType.Parametro)
            Upd.Add("IdDespachoDet", "@IdDespachoDet", DataType.Parametro)
            Upd.Add("IdRecepcionDet", "@IdRecepcionDet", DataType.Parametro)
            Upd.Add("cantidad_enviada", "@cantidad_enviada", DataType.Parametro)
            Upd.Add("cantidad_pendiente", "@cantidad_pendiente", DataType.Parametro)
            Upd.Add("auditar", "@auditar", DataType.Parametro)
            Upd.Where("idtransaccion = @idtransaccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_out.Idempresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_transacciones_out.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeI_nav_transacciones_out.Idpropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_out.Idpropietariobodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_out.Idordencompra))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_out.Idrecepcionenc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_transacciones_out.Idpedidoenc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeI_nav_transacciones_out.Iddespachoenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_out.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_out.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_out.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_out.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_out.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_transacciones_out.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeI_nav_transacciones_out.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_transacciones_out.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeI_nav_transacciones_out.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeI_nav_transacciones_out.Fecha_recepcion))
            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO", oBeI_nav_transacciones_out.No_pedido))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_transacciones_out.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_transacciones_out.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeI_nav_transacciones_out.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_out.Codigo_variante))
            cmd.Parameters.Add(New SqlParameter("@UNIDAD_MEDIDA", oBeI_nav_transacciones_out.Unidad_medida))
            cmd.Parameters.Add(New SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_out.Tipo_transaccion))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_transacciones_out.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_transacciones_out.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_out.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_transacciones_out.User_mod))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ESPERADA", oBeI_nav_transacciones_out.Cantidad_Esperada))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeI_nav_transacciones_out.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeI_nav_transacciones_out.Uds_Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_transacciones_out.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@IdTipoDocumento", oBeI_nav_transacciones_out.IdTipoDocumento))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_transacciones_out.codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeI_nav_transacciones_out.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeI_nav_transacciones_out.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeI_nav_transacciones_out.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeI_nav_transacciones_out.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeI_nav_transacciones_out.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeI_nav_transacciones_out.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeI_nav_transacciones_out.peso_neto))
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeI_nav_transacciones_out.peso_bruto))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_SALIDA_REF_DEVOL", IIf(oBeI_nav_transacciones_out.fecha_despacho = Nothing, DBNull.Value, oBeI_nav_transacciones_out.no_documento_salida_ref_devol)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCDEVOL", IIf(oBeI_nav_transacciones_out.no_documento_salida_ref_devol = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdPedidoEncDevol)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", IIf(oBeI_nav_transacciones_out.IdDespachoDet = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdDespachoDet)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeI_nav_transacciones_out.IdRecepcionDet = Nothing, DBNull.Value, oBeI_nav_transacciones_out.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ENVIADA", oBeI_nav_transacciones_out.Cantidad_Enviada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PENDIENTE", oBeI_nav_transacciones_out.Cantidad_Pendiente))
            cmd.Parameters.Add(New SqlParameter("@AUDITAR", oBeI_nav_transacciones_out.Auditar))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_transacciones_out As clsBeI_nav_transacciones_out, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_transacciones_out" &
             "  Where(idtransaccion = @idtransaccion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_transacciones_out"
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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeI_nav_transacciones_out As clsBeI_nav_transacciones_out) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = True

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM I_nav_transacciones_out" &
                                 " Where(idtransaccion = @idtransaccion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRANSACCION", pBeI_nav_transacciones_out.Idtransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_transacciones_out, dt.Rows(0))
                GetSingle = True
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

    Public Shared Function GetSingle(ByVal pIdTransaccion As Integer) As clsBeI_nav_transacciones_out

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = " SELECT * FROM I_nav_transacciones_out " &
                                 " Where(idtransaccion = @idtransaccion) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRANSACCION", pIdTransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out
                Cargar(pBeI_nav_transacciones_out, dt.Rows(0))
                GetSingle = pBeI_nav_transacciones_out
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idtransaccion),0) FROM I_nav_transacciones_out"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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
            Throw ex
        End Try

    End Function

End Class