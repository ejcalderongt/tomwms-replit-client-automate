Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnTrans_verificacion_etiqueta

	Public Shared Sub Cargar(ByRef oBeTrans_verificacion_etiqueta As clsBeTrans_verificacion_etiqueta, ByRef dr As DataRow)
		Try
			With oBeTrans_verificacion_etiqueta
				.IdVerificacionEtiqueta = IIf(IsDBNull(dr.Item("IdVerificacionEtiqueta")), 0, dr.Item("IdVerificacionEtiqueta"))
				.IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
				.IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
				.IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
				.IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
				.IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
				.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
				.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
				.IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.IdOperadorBodega_Pickeo = IIf(IsDBNull(dr.Item("IdOperadorBodega_Pickeo")), 0, dr.Item("IdOperadorBodega_Pickeo"))
				.IdOperadorBodega_Verifico = IIf(IsDBNull(dr.Item("IdOperadorBodega_Verifico")), 0, dr.Item("IdOperadorBodega_Verifico"))
				.IdUbicacionTemporal = IIf(IsDBNull(dr.Item("IdUbicacionTemporal")), 0, dr.Item("IdUbicacionTemporal"))
				.IdOperadorBodega_Asignado = IIf(IsDBNull(dr.Item("IdOperadorBodega_Asignado")), 0, dr.Item("IdOperadorBodega_Asignado"))
				.IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
				.Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
				.Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
				.Nombre_operador_pickeo = IIf(IsDBNull(dr.Item("nombre_operador_pickeo")), "", dr.Item("nombre_operador_pickeo"))
				.Nombre_operador_verifico = IIf(IsDBNull(dr.Item("nombre_operador_verifico")), "", dr.Item("nombre_operador_verifico"))
				.Nombre_cliente = IIf(IsDBNull(dr.Item("nombre_cliente")), "", dr.Item("nombre_cliente"))
				.Codigo_talla = IIf(IsDBNull(dr.Item("codigo_talla")), "", dr.Item("codigo_talla"))
				.Codigo_color = IIf(IsDBNull(dr.Item("codigo_color")), "", dr.Item("codigo_color"))
				.Codigo_barra_etiqueta = IIf(IsDBNull(dr.Item("codigo_barra_etiqueta")), "", dr.Item("codigo_barra_etiqueta"))
				.Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
				.Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
				.Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
				.Peso_verificado = IIf(IsDBNull(dr.Item("peso_verificado")), 0.0, dr.Item("peso_verificado"))
				.Cantidad_verificada = IIf(IsDBNull(dr.Item("cantidad_verificada")), 0.0, dr.Item("cantidad_verificada"))
				.Fecha_verificado = IIf(IsDBNull(dr.Item("fecha_verificado")), Date.Now, dr.Item("fecha_verificado"))
				.Referencia_pedido = IIf(IsDBNull(dr.Item("referencia_pedido")), "", dr.Item("referencia_pedido"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
				.ZPL_Etiqueta = IIf(IsDBNull(dr.Item("zpl_etiqueta")), "", dr.Item("zpl_etiqueta"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeTrans_verificacion_etiqueta As clsBeTrans_verificacion_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("trans_verificacion_etiqueta")
			Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
			Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
			Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
			Ins.Add("idstock", "@idstock", DataType.Parametro)
			Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
			Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
			Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
			Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
			Ins.Add("idstockres", "@idstockres", DataType.Parametro)
			Ins.Add("idbodega", "@idbodega", DataType.Parametro)
			Ins.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", DataType.Parametro)
			Ins.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", DataType.Parametro)
			Ins.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
			Ins.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", DataType.Parametro)
			Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
			Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
			Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
			Ins.Add("nombre_operador_pickeo", "@nombre_operador_pickeo", DataType.Parametro)
			Ins.Add("nombre_operador_verifico", "@nombre_operador_verifico", DataType.Parametro)
			Ins.Add("nombre_cliente", "@nombre_cliente", DataType.Parametro)
			Ins.Add("codigo_talla", "@codigo_talla", DataType.Parametro)
			Ins.Add("codigo_color", "@codigo_color", DataType.Parametro)
			Ins.Add("codigo_barra_etiqueta", "@codigo_barra_etiqueta", DataType.Parametro)
			Ins.Add("lote", "@lote", DataType.Parametro)
			Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
			Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
			Ins.Add("peso_verificado", "@peso_verificado", DataType.Parametro)
			Ins.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
			Ins.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
			Ins.Add("referencia_pedido", "@referencia_pedido", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("user_mod", "@user_mod", DataType.Parametro)
			Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Ins.Add("activo", "@activo", DataType.Parametro)
			Ins.Add("zpl_etiqueta", "@zpl_etiqueta", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_verificacion_etiqueta.IdPickingUbic))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_verificacion_etiqueta.IdPickingEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_verificacion_etiqueta.IdPickingDet))
			cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_verificacion_etiqueta.IdStock))
			cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_verificacion_etiqueta.IdPropietarioBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_verificacion_etiqueta.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_verificacion_etiqueta.IdProductoEstado))
			cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_verificacion_etiqueta.IdPresentacion))
			cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_verificacion_etiqueta.IdUnidadMedida))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_verificacion_etiqueta.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_verificacion_etiqueta.IdPedidoDet))
			cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_verificacion_etiqueta.IdStockRes))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_verificacion_etiqueta.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Pickeo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Verifico))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_verificacion_etiqueta.IdUbicacionTemporal))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Asignado))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_verificacion_etiqueta.IdProductoTallaColor))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_verificacion_etiqueta.Codigo_producto))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_verificacion_etiqueta.Nombre_producto))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR_PICKEO", oBeTrans_verificacion_etiqueta.Nombre_operador_pickeo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR_VERIFICO", oBeTrans_verificacion_etiqueta.Nombre_operador_verifico))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeTrans_verificacion_etiqueta.Nombre_cliente))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_TALLA", oBeTrans_verificacion_etiqueta.Codigo_talla))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_COLOR", oBeTrans_verificacion_etiqueta.Codigo_color))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA_ETIQUETA", oBeTrans_verificacion_etiqueta.Codigo_barra_etiqueta))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_verificacion_etiqueta.Lote))
			cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_verificacion_etiqueta.Fecha_vence))
			cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_verificacion_etiqueta.Lic_plate))
			cmd.Parameters.Add(New SqlParameter("@PESO_VERIFICADO", oBeTrans_verificacion_etiqueta.Peso_verificado))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_verificacion_etiqueta.Cantidad_verificada))
			cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", oBeTrans_verificacion_etiqueta.Fecha_verificado))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_PEDIDO", oBeTrans_verificacion_etiqueta.Referencia_pedido))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_verificacion_etiqueta.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_verificacion_etiqueta.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_verificacion_etiqueta.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_verificacion_etiqueta.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_verificacion_etiqueta.Activo))
			cmd.Parameters.Add(New SqlParameter("@ZPL_ETIQUETA", oBeTrans_verificacion_etiqueta.ZPL_Etiqueta))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Actualizar(ByRef oBeTrans_verificacion_etiqueta As clsBeTrans_verificacion_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_verificacion_etiqueta")
			Upd.Add("idverificacionetiqueta", "@idverificacionetiqueta", DataType.Parametro)
			Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
			Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
			Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
			Upd.Add("idstock", "@idstock", DataType.Parametro)
			Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
			Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
			Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
			Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
			Upd.Add("idstockres", "@idstockres", DataType.Parametro)
			Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			Upd.Add("idoperadorbodega_pickeo", "@idoperadorbodega_pickeo", DataType.Parametro)
			Upd.Add("idoperadorbodega_verifico", "@idoperadorbodega_verifico", DataType.Parametro)
			Upd.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
			Upd.Add("idoperadorbodega_asignado", "@idoperadorbodega_asignado", DataType.Parametro)
			Upd.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
			Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
			Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
			Upd.Add("nombre_operador_pickeo", "@nombre_operador_pickeo", DataType.Parametro)
			Upd.Add("nombre_operador_verifico", "@nombre_operador_verifico", DataType.Parametro)
			Upd.Add("nombre_cliente", "@nombre_cliente", DataType.Parametro)
			Upd.Add("codigo_talla", "@codigo_talla", DataType.Parametro)
			Upd.Add("codigo_color", "@codigo_color", DataType.Parametro)
			Upd.Add("codigo_barra_etiqueta", "@codigo_barra_etiqueta", DataType.Parametro)
			Upd.Add("lote", "@lote", DataType.Parametro)
			Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
			Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
			Upd.Add("peso_verificado", "@peso_verificado", DataType.Parametro)
			Upd.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
			Upd.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
			Upd.Add("referencia_pedido", "@referencia_pedido", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Add("activo", "@activo", DataType.Parametro)
			Upd.Add("zpl_etiqueta", "@zpl_etiqueta", DataType.Parametro)
			Upd.Where("IdVerificacionEtiqueta = @IdVerificacionEtiqueta")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDVERIFICACIONETIQUETA", oBeTrans_verificacion_etiqueta.IdVerificacionEtiqueta))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_verificacion_etiqueta.IdPickingUbic))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_verificacion_etiqueta.IdPickingEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_verificacion_etiqueta.IdPickingDet))
			cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_verificacion_etiqueta.IdStock))
			cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_verificacion_etiqueta.IdPropietarioBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_verificacion_etiqueta.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_verificacion_etiqueta.IdProductoEstado))
			cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_verificacion_etiqueta.IdPresentacion))
			cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_verificacion_etiqueta.IdUnidadMedida))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_verificacion_etiqueta.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_verificacion_etiqueta.IdPedidoDet))
			cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_verificacion_etiqueta.IdStockRes))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_verificacion_etiqueta.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Pickeo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Verifico))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_verificacion_etiqueta.IdUbicacionTemporal))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_verificacion_etiqueta.IdOperadorBodega_Asignado))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_verificacion_etiqueta.IdProductoTallaColor))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_verificacion_etiqueta.Codigo_producto))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_verificacion_etiqueta.Nombre_producto))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR_PICKEO", oBeTrans_verificacion_etiqueta.Nombre_operador_pickeo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_OPERADOR_VERIFICO", oBeTrans_verificacion_etiqueta.Nombre_operador_verifico))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE_CLIENTE", oBeTrans_verificacion_etiqueta.Nombre_cliente))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_TALLA", oBeTrans_verificacion_etiqueta.Codigo_talla))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_COLOR", oBeTrans_verificacion_etiqueta.Codigo_color))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA_ETIQUETA", oBeTrans_verificacion_etiqueta.Codigo_barra_etiqueta))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_verificacion_etiqueta.Lote))
			cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_verificacion_etiqueta.Fecha_vence))
			cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_verificacion_etiqueta.Lic_plate))
			cmd.Parameters.Add(New SqlParameter("@PESO_VERIFICADO", oBeTrans_verificacion_etiqueta.Peso_verificado))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_verificacion_etiqueta.Cantidad_verificada))
			cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", oBeTrans_verificacion_etiqueta.Fecha_verificado))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_PEDIDO", oBeTrans_verificacion_etiqueta.Referencia_pedido))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_verificacion_etiqueta.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_verificacion_etiqueta.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_verificacion_etiqueta.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_verificacion_etiqueta.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_verificacion_etiqueta.Activo))
			cmd.Parameters.Add(New SqlParameter("@ZPL_ETIQUETA", oBeTrans_verificacion_etiqueta.ZPL_Etiqueta))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function


	Public Shared Function Eliminar(ByRef oBeTrans_verificacion_etiqueta As clsBeTrans_verificacion_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_verificacion_etiqueta" &
			 "  Where(IdVerificacionEtiqueta = @IdVerificacionEtiqueta)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDVERIFICACIONETIQUETA", oBeTrans_verificacion_etiqueta.IdVerificacionEtiqueta))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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

			Const sp As String = "SELECT * FROM Trans_verificacion_etiqueta"
			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
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
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_All() As List(Of clsBeTrans_verificacion_etiqueta)

		Dim lReturnList As New List(Of clsBeTrans_verificacion_etiqueta)

		Try

			Const sp As String = "SELECT * FROM Trans_verificacion_etiqueta"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeTrans_verificacion_etiqueta As New clsBeTrans_verificacion_etiqueta

						For Each dr As DataRow In lDataTable.Rows
							vBeTrans_verificacion_etiqueta = New clsBeTrans_verificacion_etiqueta()
							Cargar(vBeTrans_verificacion_etiqueta, dr)
							lReturnList.Add(vBeTrans_verificacion_etiqueta)
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

	Public Shared Sub GetSingle(ByRef pBeTrans_verificacion_etiqueta As clsBeTrans_verificacion_etiqueta)

		Try

			Const sp As String = "SELECT * FROM Trans_verificacion_etiqueta" &
			" Where(IdVerificacionEtiqueta = @IdVerificacionEtiqueta)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeTrans_verificacion_etiqueta As New clsBeTrans_verificacion_etiqueta

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeTrans_verificacion_etiqueta, lDataTable.Rows(0))
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

	Public Shared Function MaxID() As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdVerificacionEtiqueta),0) FROM Trans_verificacion_etiqueta"

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
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

End Class
