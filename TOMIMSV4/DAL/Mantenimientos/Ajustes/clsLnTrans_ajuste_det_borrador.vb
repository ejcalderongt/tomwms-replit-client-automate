Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ajuste_det_borrador

    Public Shared Sub Cargar(ByRef oBeTrans_ajuste_det_borrador As clsBeTrans_ajuste_det_borrador, ByRef dr As DataRow)
        Try
            With oBeTrans_ajuste_det_borrador
                .IdAjusteDetBorrador = IIf(IsDBNull(dr.Item("idajustedetborrador")), 0, dr.Item("idajustedetborrador"))
                .idajusteenc = IIf(IsDBNull(dr.Item("idajusteenc")), 0, dr.Item("idajusteenc"))
                .idajustedet = IIf(IsDBNull(dr.Item("idajustedet")), 0, dr.Item("idajustedet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .lote_original = IIf(IsDBNull(dr.Item("lote_original")), "", dr.Item("lote_original"))
                .lote_nuevo = IIf(IsDBNull(dr.Item("lote_nuevo")), "", dr.Item("lote_nuevo"))
                .fecha_vence_original = IIf(IsDBNull(dr.Item("fecha_vence_original")), Date.Now, dr.Item("fecha_vence_original"))
                .fecha_vence_nueva = IIf(IsDBNull(dr.Item("fecha_vence_nueva")), Date.Now, dr.Item("fecha_vence_nueva"))
                .peso_original = IIf(IsDBNull(dr.Item("peso_original")), 0.0, dr.Item("peso_original"))
                .peso_nuevo = IIf(IsDBNull(dr.Item("peso_nuevo")), 0.0, dr.Item("peso_nuevo"))
                .cantidad_original = IIf(IsDBNull(dr.Item("cantidad_original")), 0.0, dr.Item("cantidad_original"))
                .cantidad_nueva = IIf(IsDBNull(dr.Item("cantidad_nueva")), 0.0, dr.Item("cantidad_nueva"))
                .codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .idtipoajuste = IIf(IsDBNull(dr.Item("idtipoajuste")), 0, dr.Item("idtipoajuste"))
                .idmotivoajuste = IIf(IsDBNull(dr.Item("idmotivoajuste")), 0, dr.Item("idmotivoajuste"))
                .observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .codigo_ajuste = IIf(IsDBNull(dr.Item("codigo_ajuste")), "", dr.Item("codigo_ajuste"))
                .enviado = IIf(IsDBNull(dr.Item("enviado")), False, dr.Item("enviado"))
                .IdBodegaERP = IIf(IsDBNull(dr.Item("IdBodegaERP")), 0, dr.Item("IdBodegaERP"))
                .lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .referencia_ajuste_erp = IIf(IsDBNull(dr.Item("referencia_ajuste_erp")), "", dr.Item("referencia_ajuste_erp"))
                .estado_ajuste_erp = IIf(IsDBNull(dr.Item("estado_ajuste_erp")), False, dr.Item("estado_ajuste_erp"))
                .estado_borrador = IIf(IsDBNull(dr.Item("estado_borrador")), "BORRADOR", dr.Item("estado_borrador"))
                .confirmado = IIf(IsDBNull(dr.Item("confirmado")), False, dr.Item("confirmado"))
                .procesado = IIf(IsDBNull(dr.Item("procesado")), False, dr.Item("procesado"))
                .fecha_creacion = IIf(IsDBNull(dr.Item("fecha_creacion")), Date.Now, dr.Item("fecha_creacion"))
                .usuario_creacion = IIf(IsDBNull(dr.Item("usuario_creacion")), "", dr.Item("usuario_creacion"))
                .fecha_modificacion = IIf(IsDBNull(dr.Item("fecha_modificacion")), Date.Now, dr.Item("fecha_modificacion"))
                .usuario_modificacion = IIf(IsDBNull(dr.Item("usuario_modificacion")), "", dr.Item("usuario_modificacion"))

                '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
                .IdProveedor = IIf(IsDBNull(dr.Item("idproveedor")), 0, dr.Item("idproveedor"))
                .Codigo_Proveedor = IIf(IsDBNull(dr.Item("codigo_proveedor")), "", dr.Item("codigo_proveedor"))
                .Nombre_Proveedor = IIf(IsDBNull(dr.Item("nombre_proveedor")), "", dr.Item("nombre_proveedor"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("idrecepcionenc")), 0, dr.Item("idrecepcionenc"))

            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ajuste_det_borrador As clsBeTrans_ajuste_det_borrador,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ajuste_det_borrador")
            Ins.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Ins.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("lote_original", "@lote_original", DataType.Parametro)
            Ins.Add("lote_nuevo", "@lote_nuevo", DataType.Parametro)
            Ins.Add("fecha_vence_original", "@fecha_vence_original", DataType.Parametro)
            Ins.Add("fecha_vence_nueva", "@fecha_vence_nueva", DataType.Parametro)
            Ins.Add("peso_original", "@peso_original", DataType.Parametro)
            Ins.Add("peso_nuevo", "@peso_nuevo", DataType.Parametro)
            Ins.Add("cantidad_original", "@cantidad_original", DataType.Parametro)
            Ins.Add("cantidad_nueva", "@cantidad_nueva", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("idtipoajuste", "@idtipoajuste", DataType.Parametro)
            Ins.Add("idmotivoajuste", "@idmotivoajuste", DataType.Parametro)
            If oBeTrans_ajuste_det_borrador.observacion IsNot Nothing Then Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("codigo_ajuste", "@codigo_ajuste", DataType.Parametro)
            Ins.Add("enviado", "@enviado", DataType.Parametro)
            Ins.Add("idbodegaerp", "@idbodegaerp", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("referencia_ajuste_erp", "@referencia_ajuste_erp", DataType.Parametro)
            Ins.Add("estado_ajuste_erp", "@estado_ajuste_erp", DataType.Parametro)
            Ins.Add("estado_borrador", "@estado_borrador", DataType.Parametro)
            Ins.Add("confirmado", "@confirmado", DataType.Parametro)
            Ins.Add("procesado", "@procesado", DataType.Parametro)
            Ins.Add("fecha_creacion", "@fecha_creacion", DataType.Parametro)
            If oBeTrans_ajuste_det_borrador.usuario_creacion IsNot Nothing Then Ins.Add("usuario_creacion", "@usuario_creacion", DataType.Parametro)
            Ins.Add("fecha_modificacion", "@fecha_modificacion", DataType.Parametro)
            If oBeTrans_ajuste_det_borrador.usuario_modificacion IsNot Nothing Then Ins.Add("usuario_modificacion", "@usuario_modificacion", DataType.Parametro)
            If oBeTrans_ajuste_det_borrador.IdRecepcionEnc > 0 Then Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("codigo_proveedor", "@codigo_proveedor", DataType.Parametro)
            Ins.Add("nombre_proveedor", "@nombre_proveedor", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det_borrador.idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeTrans_ajuste_det_borrador.idajustedet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ajuste_det_borrador.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_det_borrador.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_ajuste_det_borrador.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_ajuste_det_borrador.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_ajuste_det_borrador.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_ajuste_det_borrador.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_ajuste_det_borrador.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE_ORIGINAL", oBeTrans_ajuste_det_borrador.lote_original))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUEVO", IIf(oBeTrans_ajuste_det_borrador.lote_nuevo Is Nothing, "", oBeTrans_ajuste_det_borrador.lote_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_ORIGINAL", oBeTrans_ajuste_det_borrador.fecha_vence_original))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_NUEVA", oBeTrans_ajuste_det_borrador.fecha_vence_nueva))
            cmd.Parameters.Add(New SqlParameter("@PESO_ORIGINAL", oBeTrans_ajuste_det_borrador.peso_original))
            cmd.Parameters.Add(New SqlParameter("@PESO_NUEVO", oBeTrans_ajuste_det_borrador.peso_nuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ORIGINAL", oBeTrans_ajuste_det_borrador.cantidad_original))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_NUEVA", oBeTrans_ajuste_det_borrador.cantidad_nueva))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_ajuste_det_borrador.codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_ajuste_det_borrador.nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeTrans_ajuste_det_borrador.idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeTrans_ajuste_det_borrador.idmotivoajuste))
            If oBeTrans_ajuste_det_borrador.observacion IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ajuste_det_borrador.observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AJUSTE", oBeTrans_ajuste_det_borrador.codigo_ajuste))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_ajuste_det_borrador.enviado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAERP", oBeTrans_ajuste_det_borrador.IdBodegaERP))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_ajuste_det_borrador.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_AJUSTE_ERP", oBeTrans_ajuste_det_borrador.referencia_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_AJUSTE_ERP", oBeTrans_ajuste_det_borrador.estado_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_BORRADOR", oBeTrans_ajuste_det_borrador.estado_borrador))
            cmd.Parameters.Add(New SqlParameter("@CONFIRMADO", oBeTrans_ajuste_det_borrador.confirmado))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO", oBeTrans_ajuste_det_borrador.procesado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CREACION", oBeTrans_ajuste_det_borrador.fecha_creacion))
            If oBeTrans_ajuste_det_borrador.usuario_creacion IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@USUARIO_CREACION", oBeTrans_ajuste_det_borrador.usuario_creacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MODIFICACION", oBeTrans_ajuste_det_borrador.fecha_modificacion))
            If oBeTrans_ajuste_det_borrador.usuario_modificacion IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@USUARIO_MODIFICACION", oBeTrans_ajuste_det_borrador.usuario_modificacion))

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeTrans_ajuste_det_borrador.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR", IIf(oBeTrans_ajuste_det_borrador.Codigo_Proveedor Is Nothing, "", oBeTrans_ajuste_det_borrador.Codigo_Proveedor)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROVEEDOR", IIf(oBeTrans_ajuste_det_borrador.Nombre_Proveedor Is Nothing, "", oBeTrans_ajuste_det_borrador.Nombre_Proveedor)))

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_ajuste_det_borrador.IdRecepcionEnc))

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

    Public Shared Function Actualizar(ByRef oBeTrans_ajuste_det_borrador As clsBeTrans_ajuste_det_borrador,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ajuste_det_borrador")
            Upd.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Upd.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("lote_original", "@lote_original", DataType.Parametro)
            Upd.Add("lote_nuevo", "@lote_nuevo", DataType.Parametro)
            Upd.Add("fecha_vence_original", "@fecha_vence_original", DataType.Parametro)
            Upd.Add("fecha_vence_nueva", "@fecha_vence_nueva", DataType.Parametro)
            Upd.Add("peso_original", "@peso_original", DataType.Parametro)
            Upd.Add("peso_nuevo", "@peso_nuevo", DataType.Parametro)
            Upd.Add("cantidad_original", "@cantidad_original", DataType.Parametro)
            Upd.Add("cantidad_nueva", "@cantidad_nueva", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("idtipoajuste", "@idtipoajuste", DataType.Parametro)
            Upd.Add("idmotivoajuste", "@idmotivoajuste", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("codigo_ajuste", "@codigo_ajuste", DataType.Parametro)
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("idbodegaerp", "@idbodegaerp", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("referencia_ajuste_erp", "@referencia_ajuste_erp", DataType.Parametro)
            Upd.Add("estado_ajuste_erp", "@estado_ajuste_erp", DataType.Parametro)
            Upd.Add("estado_borrador", "@estado_borrador", DataType.Parametro)
            Upd.Add("confirmado", "@confirmado", DataType.Parametro)
            Upd.Add("procesado", "@procesado", DataType.Parametro)
            Upd.Add("fecha_creacion", "@fecha_creacion", DataType.Parametro)
            Upd.Add("usuario_creacion", "@usuario_creacion", DataType.Parametro)
            Upd.Add("fecha_modificacion", "@fecha_modificacion", DataType.Parametro)
            Upd.Add("usuario_modificacion", "@usuario_modificacion", DataType.Parametro)

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("codigo_proveedor", "@codigo_proveedor", DataType.Parametro)
            Upd.Add("nombre_proveedor", "@nombre_proveedor", DataType.Parametro)

            Upd.Where("idajustedetborrador = @idajustedetborrador")

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

            cmd.Parameters.Add(New SqlParameter("@idajustedetborrador", oBeTrans_ajuste_det_borrador.IdAjusteDetBorrador))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det_borrador.idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeTrans_ajuste_det_borrador.idajustedet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ajuste_det_borrador.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_det_borrador.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_ajuste_det_borrador.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_ajuste_det_borrador.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_ajuste_det_borrador.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_ajuste_det_borrador.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_ajuste_det_borrador.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE_ORIGINAL", oBeTrans_ajuste_det_borrador.lote_original))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUEVO", oBeTrans_ajuste_det_borrador.lote_nuevo))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_ORIGINAL", oBeTrans_ajuste_det_borrador.fecha_vence_original))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_NUEVA", oBeTrans_ajuste_det_borrador.fecha_vence_nueva))
            cmd.Parameters.Add(New SqlParameter("@PESO_ORIGINAL", oBeTrans_ajuste_det_borrador.peso_original))
            cmd.Parameters.Add(New SqlParameter("@PESO_NUEVO", oBeTrans_ajuste_det_borrador.peso_nuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ORIGINAL", oBeTrans_ajuste_det_borrador.cantidad_original))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_NUEVA", oBeTrans_ajuste_det_borrador.cantidad_nueva))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_ajuste_det_borrador.codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_ajuste_det_borrador.nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeTrans_ajuste_det_borrador.idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeTrans_ajuste_det_borrador.idmotivoajuste))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ajuste_det_borrador.observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AJUSTE", oBeTrans_ajuste_det_borrador.codigo_ajuste))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_ajuste_det_borrador.enviado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAERP", oBeTrans_ajuste_det_borrador.IdBodegaERP))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_ajuste_det_borrador.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_AJUSTE_ERP", oBeTrans_ajuste_det_borrador.referencia_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_AJUSTE_ERP", oBeTrans_ajuste_det_borrador.estado_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_BORRADOR", oBeTrans_ajuste_det_borrador.estado_borrador))
            cmd.Parameters.Add(New SqlParameter("@CONFIRMADO", oBeTrans_ajuste_det_borrador.confirmado))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO", oBeTrans_ajuste_det_borrador.procesado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CREACION", oBeTrans_ajuste_det_borrador.fecha_creacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_CREACION", oBeTrans_ajuste_det_borrador.usuario_creacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MODIFICACION", oBeTrans_ajuste_det_borrador.fecha_modificacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_MODIFICACION", oBeTrans_ajuste_det_borrador.usuario_modificacion))

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeTrans_ajuste_det_borrador.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR", IIf(oBeTrans_ajuste_det_borrador.Codigo_Proveedor Is Nothing, "", oBeTrans_ajuste_det_borrador.Codigo_Proveedor)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROVEEDOR", IIf(oBeTrans_ajuste_det_borrador.Nombre_Proveedor Is Nothing, "", oBeTrans_ajuste_det_borrador.Nombre_Proveedor)))

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

    Public Shared Function GetSingle(ByRef pBeTrans_ajuste_det_borrador As clsBeTrans_ajuste_det_borrador)

        Try

            Const sp As String = "SELECT * FROM trans_ajuste_det_borrador WHERE (idajustedetborrador = @idajustedetborrador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            lConnection.Open()
            Dim lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Transaction = lTransaction
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idajustedetborrador", pBeTrans_ajuste_det_borrador.IdAjusteDetBorrador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ajuste_det_borrador, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_ajuste_det_borrador As clsBeTrans_ajuste_det_borrador,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM trans_ajuste_det_borrador WHERE (idajustedetborrador = @idajustedetborrador)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idajustedetborrador", pBeTrans_ajuste_det_borrador.IdAjusteDetBorrador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ajuste_det_borrador, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(MAX(idajustedetborrador),0) FROM trans_ajuste_det_borrador"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Por_IdAjusteEnc(ByVal pIdAjusteEnc As Integer,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "DELETE FROM trans_ajuste_det_borrador WHERE idajusteenc = @idajusteenc"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", pIdAjusteEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_By_IdAjusteEnc(ByVal pIdAjusteEnc As Integer) As List(Of clsBeTrans_ajuste_det_borrador)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_det_borrador)
            Const sp As String = "SELECT * FROM Trans_ajuste_det_borrador WHERE IdAjusteEnc = @IdAjusteEnc"

            If lConnection.State = ConnectionState.Closed Then lConnection.Open()

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)

            lTransaction = lConnection.BeginTransaction()
            cmd.Transaction = lTransaction

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_det_borrador As New clsBeTrans_ajuste_det_borrador

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_det_borrador = New clsBeTrans_ajuste_det_borrador
                Cargar(vBeTrans_ajuste_det_borrador, dr)

                vBeTrans_ajuste_det_borrador.UmBas = clsLnUnidad_medida.Get_Codigo_By_IdUnidadMedida(vBeTrans_ajuste_det_borrador.IdUnidadMedida,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                If vBeTrans_ajuste_det_borrador.IdPresentacion <> 0 Then
                    vBeTrans_ajuste_det_borrador.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBeTrans_ajuste_det_borrador.IdProductoBodega,
                                                                                                                 vBeTrans_ajuste_det_borrador.IdPresentacion,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)

                    vBeTrans_ajuste_det_borrador.Nombre_Presentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(vBeTrans_ajuste_det_borrador.IdPresentacion,
                                                                                                                                         lConnection,
                                                                                                                                         lTransaction)
                Else
                    vBeTrans_ajuste_det_borrador.Factor = 0
                    vBeTrans_ajuste_det_borrador.Nombre_Presentacion = ""
                End If

                lReturnList.Add(vBeTrans_ajuste_det_borrador)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Eliminar_Por_IdAjusteEnc_And_IdAjusteDet(ByVal pIdAjusteEnc As Integer,
                                                                    ByVal pIdAjusteDetBorrador As Integer,
                                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "DELETE FROM trans_ajuste_det_borrador WHERE idajusteenc = @idajusteenc and idajustedetborrador = @idajustedetborrador "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", pIdAjusteEnc))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDETBORRADOR", pIdAjusteDetBorrador))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class