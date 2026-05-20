Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnTrans_inv_ciclico_rfid

	Public Shared Sub Cargar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, ByRef dr As DataRow)
		Try
			With oBeTrans_inv_ciclico_rfid
				.Idinvciclico = IIf(IsDBNull(dr.Item("idinvciclico")), 0, dr.Item("idinvciclico"))
				.Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
				.IdPallet = IIf(IsDBNull(dr.Item("IdPallet")), 0, dr.Item("IdPallet"))
				.Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
				.Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
				.Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
				.Codigo_Barra = IIf(IsDBNull(dr.Item("Codigo_Barra")), "", dr.Item("Codigo_Barra"))
				.SSCC = IIf(IsDBNull(dr.Item("SSCC")), "", dr.Item("SSCC"))
				.GTIN = IIf(IsDBNull(dr.Item("GTIN")), "", dr.Item("GTIN"))
				.Fecha_Produccion = IIf(IsDBNull(dr.Item("Fecha_Produccion")), Date.Now, dr.Item("Fecha_Produccion"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
				.Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
				.EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
				.EsReconteo = IIf(IsDBNull(dr.Item("EsReconteo")), False, dr.Item("EsReconteo"))
				.Cantidad_reconteo = IIf(IsDBNull(dr.Item("cantidad_reconteo")), 0, dr.Item("cantidad_reconteo"))
				.Iddispositivo = IIf(IsDBNull(dr.Item("Iddispositivo")), "", dr.Item("Iddispositivo"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("trans_inv_ciclico_rfid")
			Ins.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
			Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
			Ins.Add("idpallet", "@idpallet", DataType.Parametro)
			Ins.Add("codigo", "@codigo", DataType.Parametro)
			Ins.Add("nombre", "@nombre", DataType.Parametro)
			Ins.Add("lote", "@lote", DataType.Parametro)
			Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
			Ins.Add("sscc", "@sscc", DataType.Parametro)
			Ins.Add("gtin", "@gtin", DataType.Parametro)
			Ins.Add("fecha_produccion", "@fecha_produccion", DataType.Parametro)
			Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("user_mod", "@user_mod", DataType.Parametro)
			Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Ins.Add("idoperador", "@idoperador", DataType.Parametro)
			Ins.Add("cantidad", "@cantidad", DataType.Parametro)
			Ins.Add("espallet", "@espallet", DataType.Parametro)
			Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))
			cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_rfid.Idinventarioenc))
			cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeTrans_inv_ciclico_rfid.IdPallet))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_ciclico_rfid.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_inv_ciclico_rfid.Nombre))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico_rfid.Lote))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_inv_ciclico_rfid.Codigo_Barra))
			cmd.Parameters.Add(New SqlParameter("@SSCC", oBeTrans_inv_ciclico_rfid.SSCC))
			cmd.Parameters.Add(New SqlParameter("@GTIN", oBeTrans_inv_ciclico_rfid.GTIN))
			cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeTrans_inv_ciclico_rfid.Fecha_Produccion))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico_rfid.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico_rfid.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico_rfid.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_ciclico_rfid.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico_rfid.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico_rfid.IdOperador))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico_rfid.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico_rfid.EsPallet))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_ciclico_rfid.IdBodega))

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

	Public Shared Function Actualizar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_inv_ciclico_rfid")
			Upd.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
			Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
			Upd.Add("idpallet", "@idpallet", DataType.Parametro)
			Upd.Add("codigo", "@codigo", DataType.Parametro)
			Upd.Add("nombre", "@nombre", DataType.Parametro)
			Upd.Add("lote", "@lote", DataType.Parametro)
			Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
			Upd.Add("sscc", "@sscc", DataType.Parametro)
			Upd.Add("gtin", "@gtin", DataType.Parametro)
			Upd.Add("fecha_produccion", "@fecha_produccion", DataType.Parametro)
			Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Add("idoperador", "@idoperador", DataType.Parametro)
			Upd.Add("cantidad", "@cantidad", DataType.Parametro)
			Upd.Add("espallet", "@espallet", DataType.Parametro)
			Upd.Where("idinvciclico = @idinvciclico")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))
			cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_rfid.Idinventarioenc))
			cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeTrans_inv_ciclico_rfid.IdPallet))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_ciclico_rfid.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_inv_ciclico_rfid.Nombre))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico_rfid.Lote))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_inv_ciclico_rfid.Codigo_Barra))
			cmd.Parameters.Add(New SqlParameter("@SSCC", oBeTrans_inv_ciclico_rfid.SSCC))
			cmd.Parameters.Add(New SqlParameter("@GTIN", oBeTrans_inv_ciclico_rfid.GTIN))
			cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeTrans_inv_ciclico_rfid.Fecha_Produccion))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico_rfid.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico_rfid.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico_rfid.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_ciclico_rfid.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico_rfid.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico_rfid.IdOperador))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico_rfid.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico_rfid.EsPallet))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State =ConnectionState.Open Then lConnection.Close
			If Not lConnection is Nothing Then lConnection.Dispose()
			If Not lTransaction is Nothing Then lTransaction.Dispose()
		End Try

	End Function


	Public Shared Function Eliminar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_inv_ciclico_rfid" & _ 
			 "  Where(idinvciclico = @idinvciclico)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State =ConnectionState.Open Then lConnection.Close
			If Not lConnection is Nothing Then lConnection.Dispose()
			If Not lTransaction is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Listar() As DataTable

			Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid"
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType=CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			Dim dt As New DataTable
			dad.Fill(dt)

			lTransaction.Commit()

			Return dt

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State =ConnectionState.Open Then lConnection.Close
			If Not lConnection is Nothing Then lConnection.Dispose()
			If Not lTransaction is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_All() As List(Of clsBeTrans_inv_ciclico_rfid)
		
		Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_rfid)
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_inv_ciclico_rfid As New clsBeTrans_inv_ciclico_rfid

						For Each dr As DataRow In lDataTable.Rows
						vBeTrans_inv_ciclico_rfid = New clsBeTrans_inv_ciclico_rfid()
						Cargar(vBeTrans_inv_ciclico_rfid, dr)
						lReturnList.Add(vBeTrans_inv_ciclico_rfid)
						Next
		
					End Using
		
					lTransaction.Commit()
		
				End Using
		
				lConnection.Close()
		
			End Using

			Return lReturnList

		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Sub GetSingle(ByRef pBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid) 
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid" & _ 
			" Where(idinvciclico = @idinvciclico)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_inv_ciclico_rfid As New clsBeTrans_inv_ciclico_rfid

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeTrans_inv_ciclico_rfid, lDataTable.Rows(0))
					End If
		
					End Using
		
					lTransaction.Commit()
		
				End Using
		
				lConnection.Close()
		
			End Using

		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Sub

	Public Shared Function MaxID() As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico_rfid"

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
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function MaxID(ByRef lConnection As SqlConnection,
								 ByRef lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico_rfid"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = Convert.ToInt32(lReturnValue) + 1
				End If
			End Using

			Return lMax

		Catch ex As Exception
			Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
			clsLnLog_error_wms.Agregar_Error(vMsgError)
			Throw ex
		End Try

	End Function

	Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_RFID(ByVal pIdInventarioEnc As Integer, ByVal IdBodega As Integer) As List(Of clsBeTrans_inv_ciclico_rfid)

		Get_All_BeTransInvCiclico_By_IdInventarioEnc_RFID = Nothing

		Try


			Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
                                         dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
                                         dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
                                         dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
                                         dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
                                         dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
                                         dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
                                         trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  FROM dbo.trans_inv_ciclico INNER JOIN
                                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                                         dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                                         dbo.bodega_tramo INNER JOIN
                                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                                         dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                                         dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                                         dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
                                         dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                                         dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                                  WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
                                  GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
                                         dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
                                         dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
                                         dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
                                         dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
                                         dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


			Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(vSQL, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
						lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico_rfid

						For Each lRow As DataRow In lDataTable.Rows

							BeTransInvCiclico = New clsBeTrans_inv_ciclico_rfid

							BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")



							If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
								BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
							End If

							If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
								BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
							End If

							If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
								BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
							End If

							If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
								BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
							End If

							If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
								BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
							End If

							If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
								BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
							End If

							'If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
							'	BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
							'End If

							If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
								BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
							End If



							If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
								BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
							End If


							If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
								BeTransInvCiclico.Cantidad = CType(lRow("Cantidad"), Double)
							End If


							'If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
							'	BeTransInvCiclico.Idp = CType(lRow("IdPropietario"), Integer)
							'End If


							If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
								BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
							End If

							Get_All_BeTransInvCiclico_By_IdInventarioEnc_RFID.Add(BeTransInvCiclico)

						Next


					End Using

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

		Catch ex As Exception
			Throw ex
		End Try

	End Function

	Public Shared Function Actualizar_DetalleCiclico_Con_Lectura_RFID(ByVal pListaCiclicoRFID As List(Of clsBeTrans_inv_ciclico_rfid)) As Boolean

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Actualizar_DetalleCiclico_Con_Lectura_RFID = False

		Try
			lConnection.Open()
			lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


			If pListaCiclicoRFID IsNot Nothing AndAlso pListaCiclicoRFID.Count > 0 Then

				For Each Registro As clsBeTrans_inv_ciclico_rfid In pListaCiclicoRFID

					If Registro_sin_conteo(Registro, lConnection, lTransaction) Then
						Actualizar_Registro(Registro, lConnection, lTransaction)
					End If
				Next

				Actualizar_DetalleCiclico_Con_Lectura_RFID = True

			End If

			lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then
				Try
					lTransaction.Rollback()
				Catch
				End Try
			End If

			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), ex)
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

	Public Shared Function Registro_sin_conteo(ByVal pRegistro As clsBeTrans_inv_ciclico_rfid,
											   ByRef lConnection As SqlConnection,
											   ByRef lTransaction As SqlTransaction) As Boolean

		Registro_sin_conteo = False

		Try

			Dim vSQL As String = "SELECT TOP 1 SSCC FROM trans_inv_ciclico_rfid
								        WHERE (idinventarioenc=@idinventarioenc and SSCC=@sscc)"


			Using lDTA As New SqlDataAdapter(vSQL, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pRegistro.Idinventarioenc)
				lDTA.SelectCommand.Parameters.AddWithValue("@sscc", pRegistro.SSCC)

				Dim lDT As New DataTable
				lDTA.Fill(lDT)

				If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
					Registro_sin_conteo = True
				End If

			End Using


		Catch ex As Exception
			Throw ex
		End Try

	End Function


	Public Shared Function Actualizar_Registro(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid,
											   Optional ByVal pConection As SqlConnection = Nothing,
											   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_inv_ciclico_rfid")
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Add("idoperador", "@idoperador", DataType.Parametro)
			Upd.Add("cantidad", "@cantidad", DataType.Parametro)
			Upd.Add("iddispositivo", "@iddispositivo", DataType.Parametro)

			Upd.Where("idinventarioenc = @idinventarioenc and sscc=@sscc ")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_rfid.Idinventarioenc))
			cmd.Parameters.Add(New SqlParameter("@SSCC", oBeTrans_inv_ciclico_rfid.SSCC))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico_rfid.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico_rfid.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_ciclico_rfid.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico_rfid.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico_rfid.IdOperador))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico_rfid.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeTrans_inv_ciclico_rfid.Iddispositivo))


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

	Public Shared Function Get_All(ByVal pIdInventarioEnc As Integer,
							   ByVal pIdBodega As Integer,
							   Optional ByVal pConection As SqlConnection = Nothing,
							   Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_inv_ciclico_rfid)

		Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_rfid)

		Try

			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid " &
								 "WHERE idInventarioEnc = @idInventarioEnc " &
								 "AND idBodega = @idBodega"

			Dim lConnection As SqlConnection = pConection
			Dim lTransaction As SqlTransaction = pTransaction
			Dim lCloseConnection As Boolean = False
			Dim lCommitTransaction As Boolean = False

			If lConnection Is Nothing Then
				lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()
				lCloseConnection = True
			End If

			If lTransaction Is Nothing Then
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lCommitTransaction = True
			End If

			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)

			dad.SelectCommand.Parameters.Add(New SqlParameter("@idInventarioEnc", pIdInventarioEnc))
			dad.SelectCommand.Parameters.Add(New SqlParameter("@idBodega", pIdBodega))

			Dim lDataTable As New DataTable
			dad.Fill(lDataTable)

			For Each dr As DataRow In lDataTable.Rows
				Dim vBeTrans_inv_ciclico_rfid As New clsBeTrans_inv_ciclico_rfid()
				Cargar(vBeTrans_inv_ciclico_rfid, dr)
				lReturnList.Add(vBeTrans_inv_ciclico_rfid)
			Next

			If lCommitTransaction Then
				lTransaction.Commit()
			End If

			If lCloseConnection Then
				lConnection.Close()
				lConnection.Dispose()
			End If

			Return lReturnList

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function Eliminar_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
														ByVal IdInventario As Integer,
														Optional ByVal pConection As SqlConnection = Nothing,
														Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_inv_ciclico_rfid" &
			 "  Where(IdProductoBodega = @IdProductoBodega and idinventarioenc=@idinventarioenc)"

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection)
				cmd.Transaction = pTransaction
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))


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

	Public Shared Function Get_Conteo_Productos(ByVal pIdInventarioEnc As Integer,
												ByVal pIdBodega As Integer,
												Optional ByVal pConection As SqlConnection = Nothing,
												Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer


		Get_Conteo_Productos = 0
		Try

			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid " &
								 "WHERE idInventarioEnc = @idInventarioEnc " &
								 "AND idBodega = @idBodega"

			Dim lConnection As SqlConnection = pConection
			Dim lTransaction As SqlTransaction = pTransaction
			Dim lCloseConnection As Boolean = False
			Dim lCommitTransaction As Boolean = False

			If lConnection Is Nothing Then
				lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()
				lCloseConnection = True
			End If

			If lTransaction Is Nothing Then
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lCommitTransaction = True
			End If

			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)

			dad.SelectCommand.Parameters.Add(New SqlParameter("@idInventarioEnc", pIdInventarioEnc))
			dad.SelectCommand.Parameters.Add(New SqlParameter("@idBodega", pIdBodega))

			Dim lDataTable As New DataTable
			dad.Fill(lDataTable)

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Get_Conteo_Productos = lDataTable.Rows.Count
			End If

			If lCommitTransaction Then
				lTransaction.Commit()
			End If

			If lCloseConnection Then
				lConnection.Close()
				lConnection.Dispose()
			End If

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

End Class
