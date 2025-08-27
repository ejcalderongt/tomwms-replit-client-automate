Imports System.Data.SqlClient

Public Class clsLnTrans_picking_ubic_stock

    Public Shared Sub Cargar(ByRef oBeTrans_picking_ubic_stock As clsBeTrans_picking_ubic_stock, ByRef dr As DataRow)
        Try
            With oBeTrans_picking_ubic_stock
                .IdPickingUbicStock = IIf(IsDBNull(dr.Item("IdPickingUbicStock")), 0, dr.Item("IdPickingUbicStock"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacionAnterior = IIf(IsDBNull(dr.Item("IdUbicacionAnterior")), 0, dr.Item("IdUbicacionAnterior"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .Idpickingenc = IIf(IsDBNull(dr.Item("idpickingenc")), 0, dr.Item("idpickingenc"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .IdOperadorBodega_Pickeo = IIf(IsDBNull(dr.Item("IdOperadorBodega_Pickeo")), 0, dr.Item("IdOperadorBodega_Pickeo"))
                .IdOperadorBodega_Verifico = IIf(IsDBNull(dr.Item("IdOperadorBodega_Verifico")), 0, dr.Item("IdOperadorBodega_Verifico"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Fecha_minima = IIf(IsDBNull(dr.Item("fecha_minima")), Date.Now, dr.Item("fecha_minima"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Licencia = IIf(IsDBNull(dr.Item("licencia")), "", dr.Item("licencia"))
                .Cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Cantidad_verificada = IIf(IsDBNull(dr.Item("cantidad_verificada")), 0.0, dr.Item("cantidad_verificada"))
                .Fecha_picking = IIf(IsDBNull(dr.Item("fecha_picking")), Date.Now, dr.Item("fecha_picking"))
                .Fecha_verificado = IIf(IsDBNull(dr.Item("fecha_verificado")), Date.Now, dr.Item("fecha_verificado"))
                .Fecha_despachado = IIf(IsDBNull(dr.Item("fecha_despachado")), Date.Now, dr.Item("fecha_despachado"))
                .Cantidad_despachada = IIf(IsDBNull(dr.Item("cantidad_despachada")), 0.0, dr.Item("cantidad_despachada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdUbicacionTemporal = IIf(IsDBNull(dr.Item("IdUbicacionTemporal")), 0, dr.Item("IdUbicacionTemporal"))
                .IdOperadorBodega_Asignado = IIf(IsDBNull(dr.Item("IdOperadorBodega_Asignado")), 0, dr.Item("IdOperadorBodega_Asignado"))
                .IdMovimiento = IIf(IsDBNull(dr.Item("IdMovimiento")), 0, dr.Item("IdMovimiento"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_ubic_stock As clsBeTrans_picking_ubic_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_picking_ubic_stock")
            Ins.Add("idpickingubicstock", "@idpickingubicstock", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idstockres", "@idstockres", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacionanterior", "@idubicacionanterior", DataType.Parametro)
            Ins.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", DataType.Parametro)
            Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_minima", "@fecha_minima", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("licencia", "@licencia", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("cantidad_pickeada", "@cantidad_pickeada", DataType.Parametro)
            Ins.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
            Ins.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Ins.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
            Ins.Add("fecha_despachado", "@fecha_despachado", DataType.Parametro)
            Ins.Add("cantidad_despachada", "@cantidad_despachada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
            Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("IdMovimiento", "@IdMovimiento", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBICSTOCK", oBeTrans_picking_ubic_stock.IdPickingUbicStock))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic_stock.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic_stock.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_ubic_stock.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_picking_ubic_stock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic_stock.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_ubic_stock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_picking_ubic_stock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_picking_ubic_stock.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_picking_ubic_stock.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_picking_ubic_stock.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_picking_ubic_stock.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_picking_ubic_stock.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_ubic_stock.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_ubic_stock.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic_stock.Idpickingenc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_picking_ubic_stock.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Verifico))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_picking_ubic_stock.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_picking_ubic_stock.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MINIMA", oBeTrans_picking_ubic_stock.Fecha_minima))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeTrans_picking_ubic_stock.Serial))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_picking_ubic_stock.Licencia))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PICKEADA", oBeTrans_picking_ubic_stock.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic_stock.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_picking_ubic_stock.Cantidad_verificada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", oBeTrans_picking_ubic_stock.Fecha_picking))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", oBeTrans_picking_ubic_stock.Fecha_verificado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHADO", oBeTrans_picking_ubic_stock.Fecha_despachado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_DESPACHADA", oBeTrans_picking_ubic_stock.Cantidad_despachada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_ubic_stock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_ubic_stock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_ubic_stock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_ubic_stock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_ubic_stock.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic_stock.IdUbicacionTemporal))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Asignado))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_picking_ubic_stock.Host))
            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_picking_ubic_stock.IdMovimiento))

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

    Public Shared Function Actualizar(ByRef oBeTrans_picking_ubic_stock As clsBeTrans_picking_ubic_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic_stock")
            Upd.Add("idpickingubicstock", "@idpickingubicstock", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idstockres", "@idstockres", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacionanterior", "@idubicacionanterior", DataType.Parametro)
            Upd.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", DataType.Parametro)
            Upd.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_minima", "@fecha_minima", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("licencia", "@licencia", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
            Upd.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Upd.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
            Upd.Add("fecha_despachado", "@fecha_despachado", DataType.Parametro)
            Upd.Add("cantidad_despachada", "@cantidad_despachada", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
            Upd.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", DataType.Parametro)
            Upd.Add("idmovimiento", "@idmovimiento", DataType.Parametro)
            Upd.Where("IdPickingUbicStock = @IdPickingUbicStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBICSTOCK", oBeTrans_picking_ubic_stock.IdPickingUbicStock))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic_stock.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic_stock.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_ubic_stock.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_picking_ubic_stock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic_stock.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_ubic_stock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_picking_ubic_stock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_picking_ubic_stock.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_picking_ubic_stock.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_picking_ubic_stock.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_picking_ubic_stock.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_picking_ubic_stock.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_ubic_stock.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_ubic_stock.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic_stock.Idpickingenc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_picking_ubic_stock.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Verifico))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_picking_ubic_stock.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_picking_ubic_stock.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MINIMA", oBeTrans_picking_ubic_stock.Fecha_minima))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeTrans_picking_ubic_stock.Serial))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_picking_ubic_stock.Licencia))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic_stock.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_picking_ubic_stock.Cantidad_verificada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", oBeTrans_picking_ubic_stock.Fecha_picking))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", oBeTrans_picking_ubic_stock.Fecha_verificado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHADO", oBeTrans_picking_ubic_stock.Fecha_despachado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_DESPACHADA", oBeTrans_picking_ubic_stock.Cantidad_despachada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_ubic_stock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_ubic_stock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_ubic_stock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_ubic_stock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_ubic_stock.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic_stock.IdUbicacionTemporal))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_picking_ubic_stock.IdOperadorBodega_Asignado))
            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_picking_ubic_stock.IdMovimiento))

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


    Public Shared Function Eliminar(ByRef oBeTrans_picking_ubic_stock As clsBeTrans_picking_ubic_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_picking_ubic_stock " &
             "  Where(IdPickingUbicStock = @IdPickingUbicStock)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBICSTOCK", oBeTrans_picking_ubic_stock.IdPickingUbicStock))

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

            Const sp As String = "SELECT * FROM Trans_picking_ubic_stock"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
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

    Public Shared Function Get_All() As List(Of clsBeTrans_picking_ubic_stock)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic_stock)

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic_stock"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_picking_ubic_stock As New clsBeTrans_picking_ubic_stock

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_picking_ubic_stock = New clsBeTrans_picking_ubic_stock()
                            Cargar(vBeTrans_picking_ubic_stock, dr)
                            lReturnList.Add(vBeTrans_picking_ubic_stock)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_picking_ubic_stock As clsBeTrans_picking_ubic_stock)

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic_stock" &
            " Where(IdPickingUbicStock = @IdPickingUbicStock)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_picking_ubic_stock As New clsBeTrans_picking_ubic_stock

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_picking_ubic_stock, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdPickingUbicStock),0) FROM Trans_picking_ubic_stock"

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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPickingUbic_And_IdStock(ByVal IdPickingEnc As Integer,
                                                                   ByVal IdPickingUbic As Integer,
                                                                   ByVal IdStock As Integer) As clsBeTrans_picking_ubic_stock

        Get_Single_By_IdPickingUbic_And_IdStock = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic_stock " &
                                " WHERE(IdPickingUbic = @IdPickingUbic AND IdStock = @IdStock AND IdPickingEnc = @IdPickingEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingUbic", IdPickingUbic)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_picking_ubic_stock As New clsBeTrans_picking_ubic_stock

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_picking_ubic_stock, lDataTable.Rows(0))
                            Get_Single_By_IdPickingUbic_And_IdStock = vBeTrans_picking_ubic_stock
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

    Public Shared Function Get_Single_By_IdPickingUbic_And_IdStock(ByVal IdPickingEnc As Integer,
                                                                  ByVal IdPickingUbic As Integer,
                                                                  ByVal IdStock As Integer,
                                                                  ByVal lConnection As SqlConnection,
                                                                  ByVal lTransaction As SqlTransaction) As clsBeTrans_picking_ubic_stock

        Get_Single_By_IdPickingUbic_And_IdStock = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic_stock " &
                                " Where(IdPickingUbic = @IdPickingUbic AND IdStock = @IdStock AND IdPickingEnc = @IdPickingEnc) "


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingUbic", IdPickingUbic)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_picking_ubic_stock As New clsBeTrans_picking_ubic_stock

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTrans_picking_ubic_stock, lDataTable.Rows(0))
                    Get_Single_By_IdPickingUbic_And_IdStock = vBeTrans_picking_ubic_stock
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class