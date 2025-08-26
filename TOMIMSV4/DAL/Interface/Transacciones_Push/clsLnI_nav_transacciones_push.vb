Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_transacciones_push

    Public Shared Sub Cargar(ByRef oBeI_nav_transacciones_push As clsBeI_nav_transacciones_push, ByRef dr As DataRow)
        Try
            With oBeI_nav_transacciones_push
                .IdTransaccionPush = IIf(IsDBNull(dr.Item("IdTransaccionPush")), 0, dr.Item("IdTransaccionPush"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietariobodega = IIf(IsDBNull(dr.Item("IdPropietariobodega")), 0, dr.Item("IdPropietariobodega"))
                .IdOrdenCompra = IIf(IsDBNull(dr.Item("IdOrdenCompra")), 0, dr.Item("IdOrdenCompra"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .Idproductobodega = IIf(IsDBNull(dr.Item("Idproductobodega")), 0, dr.Item("Idproductobodega"))
                .Idproducto = IIf(IsDBNull(dr.Item("Idproducto")), 0, dr.Item("Idproducto"))
                .Idunidadmedida = IIf(IsDBNull(dr.Item("Idunidadmedida")), 0, dr.Item("Idunidadmedida"))
                .Idpresentacion = IIf(IsDBNull(dr.Item("Idpresentacion")), 0, dr.Item("Idpresentacion"))
                .Idproductoestado = IIf(IsDBNull(dr.Item("Idproductoestado")), 0, dr.Item("Idproductoestado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                .No_linea = IIf(IsDBNull(dr.Item("no_linea")), "", dr.Item("no_linea"))
                .Codigo_variante = IIf(IsDBNull(dr.Item("codigo_variante")), "", dr.Item("codigo_variante"))
                .Nom_unidad_medida = IIf(IsDBNull(dr.Item("nom_unidad_medida")), "", dr.Item("nom_unidad_medida"))
                .Tipo_transaccion = IIf(IsDBNull(dr.Item("tipo_transaccion")), "", dr.Item("tipo_transaccion"))
                .IdTipoDocumento = IIf(IsDBNull(dr.Item("IdTipoDocumento")), 0, dr.Item("IdTipoDocumento"))
                .Tipo_push = IIf(IsDBNull(dr.Item("tipo_push")), "", dr.Item("tipo_push"))
                .No_recepcion_almacen = IIf(IsDBNull(dr.Item("no_recepcion_almacen")), "", dr.Item("no_recepcion_almacen"))
                .Documento_ubicacion = IIf(IsDBNull(dr.Item("documento_ubicacion")), "", dr.Item("documento_ubicacion"))
                .Documento_ingreso = IIf(IsDBNull(dr.Item("documento_ingreso")), "", dr.Item("documento_ingreso"))
                .Documento_recepcion = IIf(IsDBNull(dr.Item("documento_recepcion")), "", dr.Item("documento_recepcion"))
                .Location_code = IIf(IsDBNull(dr.Item("location_code")), "", dr.Item("location_code"))
                .Zone_code = IIf(IsDBNull(dr.Item("zone_code")), "", dr.Item("zone_code"))
                .Bin_code = IIf(IsDBNull(dr.Item("bin_code")), "", dr.Item("bin_code"))
                .Assigne_user_id = IIf(IsDBNull(dr.Item("assigne_user_id")), "", dr.Item("assigne_user_id"))
                .Item_no = IIf(IsDBNull(dr.Item("item_no")), "", dr.Item("item_no"))
                .No_orden_prod = IIf(IsDBNull(dr.Item("no_orden_prod")), "", dr.Item("no_orden_prod"))
                .Respuesta_push = IIf(IsDBNull(dr.Item("respuesta_push")), "", dr.Item("respuesta_push"))
                .Enviado_A_ERP = IIf(IsDBNull(dr.Item("Enviado_A_ERP")), False, dr.Item("Enviado_A_ERP"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_transacciones_push As clsBeI_nav_transacciones_push, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_transacciones_push")
            Ins.Add("idtransaccionpush", "@idtransaccionpush", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idordencompra", "@idordencompra", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_variante", "@codigo_variante", DataType.Parametro)
            Ins.Add("nom_unidad_medida", "@nom_unidad_medida", DataType.Parametro)
            Ins.Add("tipo_transaccion", "@tipo_transaccion", DataType.Parametro)
            Ins.Add("idtipodocumento", "@idtipodocumento", DataType.Parametro)
            Ins.Add("tipo_push", "@tipo_push", DataType.Parametro)
            Ins.Add("no_recepcion_almacen", "@no_recepcion_almacen", DataType.Parametro)
            Ins.Add("documento_ubicacion", "@documento_ubicacion", DataType.Parametro)
            Ins.Add("documento_ingreso", "@documento_ingreso", DataType.Parametro)
            Ins.Add("documento_recepcion", "@documento_recepcion", DataType.Parametro)
            Ins.Add("location_code", "@location_code", DataType.Parametro)
            Ins.Add("zone_code", "@zone_code", DataType.Parametro)
            Ins.Add("bin_code", "@bin_code", DataType.Parametro)
            Ins.Add("assigne_user_id", "@assigne_user_id", DataType.Parametro)
            Ins.Add("item_no", "@item_no", DataType.Parametro)
            Ins.Add("no_orden_prod", "@no_orden_prod", DataType.Parametro)
            Ins.Add("respuesta_push", "@respuesta_push", DataType.Parametro)
            Ins.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_push.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_transacciones_push.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_push.IdPropietariobodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_push.IdOrdenCompra))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_push.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeI_nav_transacciones_push.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_push.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_push.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_push.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_push.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_push.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_transacciones_push.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeI_nav_transacciones_push.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_transacciones_push.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeI_nav_transacciones_push.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_transacciones_push.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_push.Codigo_variante))
            cmd.Parameters.Add(New SqlParameter("@NOM_UNIDAD_MEDIDA", oBeI_nav_transacciones_push.Nom_unidad_medida))
            cmd.Parameters.Add(New SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_push.Tipo_transaccion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTO", oBeI_nav_transacciones_push.IdTipoDocumento))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PUSH", oBeI_nav_transacciones_push.Tipo_push))
            cmd.Parameters.Add(New SqlParameter("@NO_RECEPCION_ALMACEN", oBeI_nav_transacciones_push.No_recepcion_almacen))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_UBICACION", oBeI_nav_transacciones_push.Documento_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_INGRESO", oBeI_nav_transacciones_push.Documento_ingreso))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_RECEPCION", oBeI_nav_transacciones_push.Documento_recepcion))
            cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_transacciones_push.Location_code))
            cmd.Parameters.Add(New SqlParameter("@ZONE_CODE", oBeI_nav_transacciones_push.Zone_code))
            cmd.Parameters.Add(New SqlParameter("@BIN_CODE", oBeI_nav_transacciones_push.Bin_code))
            cmd.Parameters.Add(New SqlParameter("@ASSIGNE_USER_ID", oBeI_nav_transacciones_push.Assigne_user_id))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_transacciones_push.Item_no))
            cmd.Parameters.Add(New SqlParameter("@NO_ORDEN_PROD", oBeI_nav_transacciones_push.No_orden_prod))
            cmd.Parameters.Add(New SqlParameter("@RESPUESTA_PUSH", oBeI_nav_transacciones_push.Respuesta_push))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeI_nav_transacciones_push.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_transacciones_push.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_transacciones_push.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_push.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_transacciones_push.User_mod))

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

    Public Shared Function Actualizar(ByRef oBeI_nav_transacciones_push As clsBeI_nav_transacciones_push, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_push")
            Upd.Add("idtransaccionpush", "@idtransaccionpush", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idordencompra", "@idordencompra", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("codigo_variante", "@codigo_variante", DataType.Parametro)
            Upd.Add("nom_unidad_medida", "@nom_unidad_medida", DataType.Parametro)
            Upd.Add("tipo_transaccion", "@tipo_transaccion", DataType.Parametro)
            Upd.Add("idtipodocumento", "@idtipodocumento", DataType.Parametro)
            Upd.Add("tipo_push", "@tipo_push", DataType.Parametro)
            Upd.Add("no_recepcion_almacen", "@no_recepcion_almacen", DataType.Parametro)
            Upd.Add("documento_ubicacion", "@documento_ubicacion", DataType.Parametro)
            Upd.Add("documento_ingreso", "@documento_ingreso", DataType.Parametro)
            Upd.Add("documento_recepcion", "@documento_recepcion", DataType.Parametro)
            Upd.Add("location_code", "@location_code", DataType.Parametro)
            Upd.Add("zone_code", "@zone_code", DataType.Parametro)
            Upd.Add("bin_code", "@bin_code", DataType.Parametro)
            Upd.Add("assigne_user_id", "@assigne_user_id", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("no_orden_prod", "@no_orden_prod", DataType.Parametro)
            Upd.Add("respuesta_push", "@respuesta_push", DataType.Parametro)
            Upd.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("IdTransaccionPush = @IdTransaccionPush")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_transacciones_push.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_transacciones_push.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeI_nav_transacciones_push.IdPropietariobodega))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRA", oBeI_nav_transacciones_push.IdOrdenCompra))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_transacciones_push.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeI_nav_transacciones_push.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_transacciones_push.Idproductobodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeI_nav_transacciones_push.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeI_nav_transacciones_push.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeI_nav_transacciones_push.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeI_nav_transacciones_push.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_transacciones_push.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeI_nav_transacciones_push.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_transacciones_push.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeI_nav_transacciones_push.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeI_nav_transacciones_push.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_VARIANTE", oBeI_nav_transacciones_push.Codigo_variante))
            cmd.Parameters.Add(New SqlParameter("@NOM_UNIDAD_MEDIDA", oBeI_nav_transacciones_push.Nom_unidad_medida))
            cmd.Parameters.Add(New SqlParameter("@TIPO_TRANSACCION", oBeI_nav_transacciones_push.Tipo_transaccion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTO", oBeI_nav_transacciones_push.IdTipoDocumento))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PUSH", oBeI_nav_transacciones_push.Tipo_push))
            cmd.Parameters.Add(New SqlParameter("@NO_RECEPCION_ALMACEN", oBeI_nav_transacciones_push.No_recepcion_almacen))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_UBICACION", oBeI_nav_transacciones_push.Documento_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_INGRESO", oBeI_nav_transacciones_push.Documento_ingreso))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_RECEPCION", oBeI_nav_transacciones_push.Documento_recepcion))
            cmd.Parameters.Add(New SqlParameter("@LOCATION_CODE", oBeI_nav_transacciones_push.Location_code))
            cmd.Parameters.Add(New SqlParameter("@ZONE_CODE", oBeI_nav_transacciones_push.Zone_code))
            cmd.Parameters.Add(New SqlParameter("@BIN_CODE", oBeI_nav_transacciones_push.Bin_code))
            cmd.Parameters.Add(New SqlParameter("@ASSIGNE_USER_ID", oBeI_nav_transacciones_push.Assigne_user_id))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_transacciones_push.Item_no))
            cmd.Parameters.Add(New SqlParameter("@NO_ORDEN_PROD", oBeI_nav_transacciones_push.No_orden_prod))
            cmd.Parameters.Add(New SqlParameter("@RESPUESTA_PUSH", oBeI_nav_transacciones_push.Respuesta_push))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeI_nav_transacciones_push.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_transacciones_push.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_transacciones_push.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_push.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_transacciones_push.User_mod))

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


    Public Shared Function Eliminar(ByRef oBeI_nav_transacciones_push As clsBeI_nav_transacciones_push, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_transacciones_push" & _
             "  Where(IdTransaccionPush = @IdTransaccionPush)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_push"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeI_nav_transacciones_push)

        Dim lReturnList As New List(Of clsBeI_nav_transacciones_push)

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_push"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_transacciones_push = New clsBeI_nav_transacciones_push()
                            Cargar(vBeI_nav_transacciones_push, dr)
                            lReturnList.Add(vBeI_nav_transacciones_push)
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

    Public Shared Sub GetSingle(ByRef pBeI_nav_transacciones_push As clsBeI_nav_transacciones_push)

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_push" & _
            " Where(IdTransaccionPush = @IdTransaccionPush)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_transacciones_push, lDataTable.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransaccionPush),0) FROM I_nav_transacciones_push"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
