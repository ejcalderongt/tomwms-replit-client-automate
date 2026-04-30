Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ajuste_det

    Public Shared Sub Cargar(ByRef oBeTrans_ajuste_det As clsBeTrans_ajuste_det, ByRef dr As DataRow)
        Try
            With oBeTrans_ajuste_det
                .IdAjusteDet = IIf(IsDBNull(dr.Item("idajustedet")), 0, dr.Item("idajustedet"))
                .IdAjusteEnc = IIf(IsDBNull(dr.Item("idajusteenc")), 0, dr.Item("idajusteenc"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .Lote_original = IIf(IsDBNull(dr.Item("lote_original")), "", dr.Item("lote_original"))
                .Lote_nuevo = IIf(IsDBNull(dr.Item("lote_nuevo")), "", dr.Item("lote_nuevo"))
                .Fecha_vence_original = IIf(IsDBNull(dr.Item("fecha_vence_original")), Date.Now, dr.Item("fecha_vence_original"))
                .Fecha_vence_nueva = IIf(IsDBNull(dr.Item("fecha_vence_nueva")), Date.Now, dr.Item("fecha_vence_nueva"))
                .Peso_original = IIf(IsDBNull(dr.Item("peso_original")), 0.0, dr.Item("peso_original"))
                .Peso_nuevo = IIf(IsDBNull(dr.Item("peso_nuevo")), 0.0, dr.Item("peso_nuevo"))
                .Cantidad_original = IIf(IsDBNull(dr.Item("cantidad_original")), 0.0, dr.Item("cantidad_original"))
                .Cantidad_nueva = IIf(IsDBNull(dr.Item("cantidad_nueva")), 0.0, dr.Item("cantidad_nueva"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Idtipoajuste = IIf(IsDBNull(dr.Item("idtipoajuste")), 0, dr.Item("idtipoajuste"))
                .IdMotivoAjuste = IIf(IsDBNull(dr.Item("idmotivoajuste")), 0, dr.Item("idmotivoajuste"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Codigo_ajuste = IIf(IsDBNull(dr.Item("codigo_ajuste")), "", dr.Item("codigo_ajuste"))
                .Enviado = IIf(IsDBNull(dr.Item("enviado")), False, dr.Item("enviado"))
                .IdBodegaERP = IIf(IsDBNull(dr.Item("IdBodegaERP")), 0, dr.Item("IdBodegaERP"))
                .lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .referencia_ajuste_erp = IIf(IsDBNull(dr.Item("referencia_ajuste_erp")), "", dr.Item("referencia_ajuste_erp"))
                .estado_ajuste_erp = IIf(IsDBNull(dr.Item("estado_ajuste_erp")), False, dr.Item("estado_ajuste_erp"))
                .IdProductoTallaColor_origen = IIf(IsDBNull(dr.Item("IdProductoTallaColor_origen")), 0, dr.Item("IdProductoTallaColor_origen"))
                .Talla_origen = IIf(IsDBNull(dr.Item("Talla_origen")), "", dr.Item("Talla_origen"))
                .Color_origen = IIf(IsDBNull(dr.Item("Color_origen")), "", dr.Item("Color_origen"))

                .IdProductoTallaColor_destino = IIf(IsDBNull(dr.Item("IdProductoTallaColor_destino")), 0, dr.Item("IdProductoTallaColor_destino"))
                .Talla_destino = IIf(IsDBNull(dr.Item("Talla_destino")), "", dr.Item("Talla_destino"))
                .Color_destino = IIf(IsDBNull(dr.Item("Color_destino")), "", dr.Item("Color_destino"))

                '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
                .IdProveedor = IIf(IsDBNull(dr.Item("idproveedor")), 0, dr.Item("idproveedor"))
                .Codigo_Proveedor = IIf(IsDBNull(dr.Item("codigo_proveedor")), "", dr.Item("codigo_proveedor"))
                .Nombre_Proveedor = IIf(IsDBNull(dr.Item("nombre_proveedor")), "", dr.Item("nombre_proveedor"))

            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub
    Public Shared Function Insertar(ByRef oBeTrans_ajuste_det As clsBeTrans_ajuste_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ajuste_det")
            Ins.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Ins.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
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
            If oBeTrans_ajuste_det.Observacion IsNot Nothing Then Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("codigo_ajuste", "@codigo_ajuste", DataType.Parametro)
            Ins.Add("enviado", "@enviado", DataType.Parametro)
            Ins.Add("idbodegaerp", "@idbodegaerp", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("referencia_ajuste_erp", "@referencia_ajuste_erp", DataType.Parametro)
            Ins.Add("estado_ajuste_erp", "@estado_ajuste_erp", DataType.Parametro)

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("codigo_proveedor", "@codigo_proveedor", DataType.Parametro)
            Ins.Add("nombre_proveedor", "@nombre_proveedor", DataType.Parametro)

            If oBeTrans_ajuste_det.IdProductoTallaColor_origen > 0 Then
                Ins.Add("IdProductoTallaColor_origen", "@IdProductoTallaColor_origen", DataType.Parametro)
                Ins.Add("talla_origen", "@talla_origen", DataType.Parametro)
                Ins.Add("color_origen", "@color_origen", DataType.Parametro)
            End If

            If oBeTrans_ajuste_det.IdProductoTallaColor_destino > 0 Then
                Ins.Add("IdProductoTallaColor_destino", "@IdProductoTallaColor_destino", DataType.Parametro)
                Ins.Add("talla_destino", "@talla_destino", DataType.Parametro)
                Ins.Add("color_destino", "@color_destino", DataType.Parametro)
            End If

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

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeTrans_ajuste_det.IdAjusteDet))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det.IdAjusteEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ajuste_det.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_det.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_ajuste_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_ajuste_det.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_ajuste_det.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_ajuste_det.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_ajuste_det.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE_ORIGINAL", oBeTrans_ajuste_det.Lote_original))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUEVO", IIf(oBeTrans_ajuste_det.Lote_nuevo = Nothing, "", oBeTrans_ajuste_det.Lote_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_ORIGINAL", oBeTrans_ajuste_det.Fecha_vence_original))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_NUEVA", oBeTrans_ajuste_det.Fecha_vence_nueva))
            cmd.Parameters.Add(New SqlParameter("@PESO_ORIGINAL", oBeTrans_ajuste_det.Peso_original))
            cmd.Parameters.Add(New SqlParameter("@PESO_NUEVO", oBeTrans_ajuste_det.Peso_nuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ORIGINAL", oBeTrans_ajuste_det.Cantidad_original + oBeTrans_ajuste_det.CantReservada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_NUEVA", oBeTrans_ajuste_det.Cantidad_nueva + oBeTrans_ajuste_det.CantReservada))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_ajuste_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_ajuste_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeTrans_ajuste_det.Idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeTrans_ajuste_det.IdMotivoAjuste))
            If oBeTrans_ajuste_det.Observacion IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ajuste_det.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AJUSTE", oBeTrans_ajuste_det.Codigo_ajuste))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_ajuste_det.Enviado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAERP", oBeTrans_ajuste_det.IdBodegaERP))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_ajuste_det.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_AJUSTE_ERP", oBeTrans_ajuste_det.referencia_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_AJUSTE_ERP", oBeTrans_ajuste_det.estado_ajuste_erp))

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeTrans_ajuste_det.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR", IIf(oBeTrans_ajuste_det.Codigo_Proveedor Is Nothing, "", oBeTrans_ajuste_det.Codigo_Proveedor)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROVEEDOR", IIf(oBeTrans_ajuste_det.Nombre_Proveedor Is Nothing, "", oBeTrans_ajuste_det.Nombre_Proveedor)))

            If oBeTrans_ajuste_det.IdProductoTallaColor_origen > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor_origen", oBeTrans_ajuste_det.IdProductoTallaColor_origen))
                cmd.Parameters.Add(New SqlParameter("@Talla_origen", oBeTrans_ajuste_det.Talla_origen))
                cmd.Parameters.Add(New SqlParameter("@Color_origen", oBeTrans_ajuste_det.Color_origen))
            End If

            If oBeTrans_ajuste_det.IdProductoTallaColor_destino > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor_destino", oBeTrans_ajuste_det.IdProductoTallaColor_destino))
                cmd.Parameters.Add(New SqlParameter("@Talla_destino", oBeTrans_ajuste_det.Talla_destino))
                cmd.Parameters.Add(New SqlParameter("@Color_destino", oBeTrans_ajuste_det.Color_destino))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_ajuste_det.IdAjusteDet = CInt(cmd.Parameters("@IDAJUSTEDET").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeTrans_ajuste_det As clsBeTrans_ajuste_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ajuste_det")
            Upd.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Upd.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
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

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("codigo_proveedor", "@codigo_proveedor", DataType.Parametro)
            Upd.Add("nombre_proveedor", "@nombre_proveedor", DataType.Parametro)

            If oBeTrans_ajuste_det.IdProductoTallaColor_origen > 0 Then
                Upd.Add("IdProductoTallaColor_origen", "@IdProductoTallaColor_origen", DataType.Parametro)
                Upd.Add("talla_origen", "@talla_origen", DataType.Parametro)
                Upd.Add("color_origen", "@color_origen", DataType.Parametro)
            End If

            If oBeTrans_ajuste_det.IdProductoTallaColor_destino > 0 Then
                Upd.Add("IdProductoTallaColor_destino", "@IdProductoTallaColor_destino", DataType.Parametro)
                Upd.Add("talla_destino", "@talla_destino", DataType.Parametro)
                Upd.Add("color_destion", "@color_destion", DataType.Parametro)
            End If

            Upd.Where("idajustedet = @idajustedet")

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

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeTrans_ajuste_det.IdAjusteDet))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det.IdAjusteEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ajuste_det.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_det.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_ajuste_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_ajuste_det.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_ajuste_det.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_ajuste_det.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_ajuste_det.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE_ORIGINAL", oBeTrans_ajuste_det.Lote_original))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUEVO", oBeTrans_ajuste_det.Lote_nuevo))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_ORIGINAL", oBeTrans_ajuste_det.Fecha_vence_original))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_NUEVA", oBeTrans_ajuste_det.Fecha_vence_nueva))
            cmd.Parameters.Add(New SqlParameter("@PESO_ORIGINAL", oBeTrans_ajuste_det.Peso_original))
            cmd.Parameters.Add(New SqlParameter("@PESO_NUEVO", oBeTrans_ajuste_det.Peso_nuevo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ORIGINAL", oBeTrans_ajuste_det.Cantidad_original))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_NUEVA", oBeTrans_ajuste_det.Cantidad_nueva))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_ajuste_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_ajuste_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeTrans_ajuste_det.Idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTE", oBeTrans_ajuste_det.IdMotivoAjuste))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ajuste_det.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AJUSTE", oBeTrans_ajuste_det.Codigo_ajuste))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_ajuste_det.Enviado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAERP", oBeTrans_ajuste_det.IdBodegaERP))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_ajuste_det.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_AJUSTE_ERP", oBeTrans_ajuste_det.referencia_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_AJUSTE_ERP", oBeTrans_ajuste_det.estado_ajuste_erp))

            '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeTrans_ajuste_det.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PROVEEDOR", IIf(oBeTrans_ajuste_det.Codigo_Proveedor Is Nothing, "", oBeTrans_ajuste_det.Codigo_Proveedor)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROVEEDOR", IIf(oBeTrans_ajuste_det.Nombre_Proveedor Is Nothing, "", oBeTrans_ajuste_det.Nombre_Proveedor)))

            If oBeTrans_ajuste_det.IdProductoTallaColor_origen > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor_origen", oBeTrans_ajuste_det.IdProductoTallaColor_origen))
                cmd.Parameters.Add(New SqlParameter("@Talla_origen", oBeTrans_ajuste_det.Talla_origen))
                cmd.Parameters.Add(New SqlParameter("@Color_origen", oBeTrans_ajuste_det.Color_origen))
            End If

            If oBeTrans_ajuste_det.IdProductoTallaColor_destino > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor_destino", oBeTrans_ajuste_det.IdProductoTallaColor_destino))
                cmd.Parameters.Add(New SqlParameter("@Talla_destino", oBeTrans_ajuste_det.Talla_destino))
                cmd.Parameters.Add(New SqlParameter("@Color_destino", oBeTrans_ajuste_det.Color_destino))
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
    Public Shared Function GetSingle(ByRef pBeTrans_ajuste_det As clsBeTrans_ajuste_det)

        Try

            Const sp As String = "SELECT * FROM Trans_ajuste_det" &
            " Where(idajustedet = @idajustedet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            lConnection.Open()
            Dim lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Transaction = lTransaction
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDAJUSTEDET", pBeTrans_ajuste_det.IdAjusteDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ajuste_det, dt.Rows(0))
                pBeTrans_ajuste_det.UmBas = clsLnUnidad_medida.Get_Codigo_By_IdUnidadMedida(pBeTrans_ajuste_det.IdUnidadMedida,
                                                                                            lConnection,
                                                                                            lTransaction)
                If pBeTrans_ajuste_det.IdPresentacion <> 0 Then
                    pBeTrans_ajuste_det.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBeTrans_ajuste_det.IdProductoBodega,
                                                                                                           pBeTrans_ajuste_det.IdBodegaERP,
                                                                                                           lConnection,
                                                                                                           lTransaction)
                Else
                    pBeTrans_ajuste_det.Factor = 0
                End If

            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetSingle(ByRef pBeTrans_ajuste_det As clsBeTrans_ajuste_det,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_ajuste_det" &
            " Where(idajustedet = @idajustedet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDAJUSTEDET", pBeTrans_ajuste_det.IdAjusteDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ajuste_det, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idajustedet),0) FROM Trans_ajuste_det"

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

End Class
