Imports System
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Data.Linq.Helpers
Imports DevExpress.Xpo.Helpers

Public Class clsLnI_nav_barras_rfid_enc

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_enc
				.IdRFIDEnc = IIf(IsDBNull(dr.Item("IdRFIDEnc")), 0, dr.Item("IdRFIDEnc"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
				.Tipo = IIf(IsDBNull(dr.Item("Tipo")), "", dr.Item("Tipo"))
				.IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
				.IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_enc")
			'Ins.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
			Ins.Add("idbodega", "@idbodega", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Ins.Add("estado", "@estado", DataType.Parametro)
			Ins.Add("tipo", "@tipo", DataType.Parametro)
			Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
			Ins.Add("idcliente", "@idcliente", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			sp &= "; SELECT CAST(SCOPE_IDENTITY() AS INT);"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			'cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_enc.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_barras_rfid_enc.IdRecepcionEnc))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_enc.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_enc.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_enc.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_barras_rfid_enc.Estado))
			cmd.Parameters.Add(New SqlParameter("@TIPO", oBeI_nav_barras_rfid_enc.Tipo))
			cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeI_nav_barras_rfid_enc.IdProveedor))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_barras_rfid_enc.IdCliente))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_enc.IdPedidoEnc))

			Dim idGeneradoObj As Object = cmd.ExecuteScalar()
			Dim rowsAffected As Integer = 0

			If Not idGeneradoObj Is Nothing AndAlso Not IsDBNull(idGeneradoObj) Then
				oBeI_nav_barras_rfid_enc.IdRFIDEnc = Convert.ToInt32(idGeneradoObj)
				rowsAffected = oBeI_nav_barras_rfid_enc.IdRFIDEnc
			End If

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


	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_enc")
			'Upd.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
			Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("tipo", "@tipo", DataType.Parametro)
			Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
			Upd.Add("idcliente", "@idcliente", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Where("IdRFIDEnc = @IdRFIDEnc")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_enc.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_barras_rfid_enc.IdRecepcionEnc))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_enc.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_enc.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_enc.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_barras_rfid_enc.Estado))
			cmd.Parameters.Add(New SqlParameter("@TIPO", oBeI_nav_barras_rfid_enc.Tipo))
			cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeI_nav_barras_rfid_enc.IdProveedor))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_barras_rfid_enc.IdCliente))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_enc.IdPedidoEnc))

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

	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_enc" &
			 "  Where(IdRFIDEnc = @IdRFIDEnc)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_enc"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_enc"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						For Each dr As DataRow In lDataTable.Rows
							vBeI_nav_barras_rfid_enc = New clsBeI_nav_barras_rfid_enc()
							Cargar(vBeI_nav_barras_rfid_enc, dr)
							lReturnList.Add(vBeI_nav_barras_rfid_enc)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc)
		Try

			Dim sp As String = "SELECT * FROM I_nav_barras_rfid_enc WHERE (IdRFIDEnc = @IdRFIDEnc)"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter()

						lDTA.SelectCommand = New SqlCommand()
						lDTA.SelectCommand.Connection = lConnection
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.CommandType = CommandType.Text

						lDTA.SelectCommand.Parameters.AddWithValue("@IdRFIDEnc", pBeI_nav_barras_rfid_enc.IdRFIDEnc)

						If pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdPedidoEnc <= 0 Then
							sp &= " AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"
							lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc)

						ElseIf pBeI_nav_barras_rfid_enc.IdPedidoEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc <= 0 Then
							sp &= " AND (IdPedidoEnc = @IdPedidoEnc)"
							lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pBeI_nav_barras_rfid_enc.IdPedidoEnc)

						ElseIf pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdPedidoEnc > 0 Then
							Throw New Exception("IdOrdenCompraEnc e IdPedidoEnc no pueden enviarse al mismo tiempo.")
						End If

						lDTA.SelectCommand.CommandText = sp

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeI_nav_barras_rfid_enc, lDataTable.Rows(0))

							vBeI_nav_barras_rfid_enc.Cliente = New clsBeCliente
							vBeI_nav_barras_rfid_enc.Proveedor = New clsBeProveedor
							vBeI_nav_barras_rfid_enc.Cliente = clsLnCliente.GetSingle(vBeI_nav_barras_rfid_enc.IdCliente, lConnection, lTransaction)
							vBeI_nav_barras_rfid_enc.Proveedor = clsLnProveedor.GetSingle(vBeI_nav_barras_rfid_enc.IdProveedor, lConnection, lTransaction)

							pBeI_nav_barras_rfid_enc = vBeI_nav_barras_rfid_enc
							pBeI_nav_barras_rfid_enc.Detalle = New List(Of clsBeI_nav_barras_rfid_det)
							pBeI_nav_barras_rfid_enc.Detalle = clsLnI_nav_barras_rfid_det.Get_All_By_IdRFIDEnc(pBeI_nav_barras_rfid_enc.IdRFIDEnc, lConnection, lTransaction)

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

			Const sp As String = "SELECT ISNULL(Max(IdRFIDEnc),0) FROM I_nav_barras_rfid_enc"

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


	Public Shared Function Get_All_Ingresos() As DataTable

		Get_All_Ingresos = Nothing

		Try

			Const sp As String = "SELECT IdRFIDEnc,IdOrdenCompraEnc,IdRecepcionEnc,isnull(bd.nombre,'ND') Bodega,enc.fec_agr,enc.fec_mod,Estado,Tipo,
										 isnull(prov.nombre,'ND') proveedor,ISNULL(cl.nombre_comercial,'ND') cliente
										 FROM I_nav_barras_rfid_enc enc 
										 left join proveedor prov on enc.IdProveedor=prov.Idproveedor
										 left join cliente cl on enc.IdCliente = cl.IdCliente
										 left join Bodega bd on enc.IdBodega=bd.Idbodega
										 where ISNULL(Tipo, '') ='ING' "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						'lDTA.SelectCommand.Parameters.AddWithValue("@NombreParametro", valor)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						'Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Get_All_Ingresos = New DataTable
							Get_All_Ingresos = lDataTable

						End If

					End Using

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function Get_All_Salidas() As DataTable

		Get_All_Salidas = Nothing

		Try

			Const sp As String = "SELECT IdRFIDEnc,IdOrdenCompraEnc,IdRecepcionEnc,isnull(bd.nombre,'ND') Bodega,enc.fec_agr,enc.fec_mod,Estado,Tipo,
										 isnull(prov.nombre,'ND') proveedor,ISNULL(cl.nombre_comercial,'ND') cliente
										 FROM I_nav_barras_rfid_enc enc 
										 left join proveedor prov on enc.IdProveedor=prov.Idproveedor
										 left join cliente cl on enc.IdCliente = cl.IdCliente
									     left join Bodega bd on enc.IdBodega=bd.IdBodega
										 where ISNULL(Tipo, '') ='SAL' "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						'lDTA.SelectCommand.Parameters.AddWithValue("@NombreParametro", valor)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Get_All_Salidas = lDataTable
						End If

					End Using

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function MaxID(ByRef lConnection As SqlConnection,
								 ByRef lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdRFIDEnc),0) FROM i_nav_barras_rfid_enc"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue)
				End If
			End Using

			Return lMax

		Catch ex1 As SqlException
			Throw ex1
		End Try

	End Function

	Public Shared Function MaxID_CompraEnc(ByRef lConnection As SqlConnection,
										   ByRef lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraEnc),0) FROM i_nav_barras_rfid_enc"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue)
				End If
			End Using

			Return lMax

		Catch ex1 As SqlException
			Throw ex1
		End Try

	End Function

	Public Shared Function MaxID_PedidoEnc(ByRef lConnection As SqlConnection,
										   ByRef lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdPedidoEnc),0) FROM i_nav_barras_rfid_enc"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue)
				End If
			End Using

			Return lMax

		Catch ex1 As SqlException
			Throw ex1
		End Try

	End Function


	'#GT19032026: Guardar encabezado y un detalle para retornar Id a la HH-RFID.
	Public Shared Function Guardar_Encabezado_Con_Primer_Detalle(ByVal pEncabezado As clsBeI_nav_barras_rfid_enc) As clsBeI_nav_barras_rfid_enc

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Guardar_Encabezado_Con_Primer_Detalle = Nothing

		Try
			lConnection.Open()
			lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

			'#GT10042026: a futuro se podria asociar el doc de ingreso o salida
			'If pEncabezado.Tipo = "ING" Then
			'	pEncabezado.IdOrdenCompraEnc = 0
			'Else
			'	pEncabezado.IdPedidoEnc = 0
			'End If

			pEncabezado.IdRFIDEnc = Insertar(pEncabezado, lConnection, lTransaction)
			pEncabezado.Detalle(0).IdRFIDEnc = pEncabezado.IdRFIDEnc

			For Each detalle As clsBeI_nav_barras_rfid_det In pEncabezado.Detalle

				If Not clsLnI_nav_barras_rfid_det.Exist_By_EPC(detalle.Barra_epc, pEncabezado.Tipo, lConnection, lTransaction) Then
					'clsLnI_nav_barras_rfid_det.Insertar(detalle, lConnection, lTransaction)


					If clsLnI_nav_barras_rfid_det.Insertar_Detalle_RFID(detalle, lConnection, lTransaction) Then

						Dim BeBarra_Pallet As New clsBeI_nav_barras_pallet
						BeBarra_Pallet.Codigo_barra = detalle.Barra_epc
						BeBarra_Pallet.Recibido = 1

						If Not clsLnI_nav_barras_pallet.Actualiza_Recibido_BarraPallet_RFID(BeBarra_Pallet, lConnection, lTransaction) Then
							Throw New Exception("No se pudo actualizar el estado recibido de la barra pallet.")
						End If

					Else
						Throw New Exception("No se pudo insertar el detalle RFID.")
					End If

				Else
					Throw New Exception("La barra EPC ya está registrada para este tipo de operación, no puede agregarse nuevamente.")
				End If

			Next

			Guardar_Encabezado_Con_Primer_Detalle = pEncabezado

			lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

	'#GT23032026: no guarda encabezado, solo valida que exista y procede a insertar el siguiente detalle.
	Public Shared Function Agregar_Detalle_A_Encabezado_RFID(ByVal pEncabezado As clsBeI_nav_barras_rfid_enc) As Boolean

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Agregar_Detalle_A_Encabezado_RFID = False

		Try
			lConnection.Open()
			lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

			If Existe_By_IDRFIDEnc(pEncabezado.IdRFIDEnc, lConnection, lTransaction) Then

				For Each detalle As clsBeI_nav_barras_rfid_det In pEncabezado.Detalle

					detalle.IdRFIDEnc = pEncabezado.IdRFIDEnc

					If Not clsLnI_nav_barras_rfid_det.Exist_By_EPC(detalle.Barra_epc, pEncabezado.Tipo, lConnection, lTransaction) Then

						If clsLnI_nav_barras_rfid_det.Insertar_Detalle_RFID(detalle, lConnection, lTransaction) Then

							Dim BeBarra_Pallet As New clsBeI_nav_barras_pallet
							BeBarra_Pallet.Codigo_barra = detalle.Barra_epc
							BeBarra_Pallet.Recibido = 1

							If Not clsLnI_nav_barras_pallet.Actualiza_Recibido_BarraPallet_RFID(BeBarra_Pallet, lConnection, lTransaction) Then
								Throw New Exception("No se pudo actualizar el estado recibido de la barra pallet.")
							End If

						Else
							Throw New Exception("No se pudo insertar el detalle RFID.")
						End If

					Else
						Throw New Exception("La barra EPC en el lote ya está registrada para este tipo de operación, no puede agregarse nuevamente.")
					End If

				Next

				Agregar_Detalle_A_Encabezado_RFID = True

			Else
				Throw New Exception("No se encontró el identificador del encabezado para insertar el detalle.")
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

	Public Shared Function Existe_By_IDRFIDEnc(ByVal pIdRFIDEnc As Integer,
										   ByRef lConnection As SqlConnection,
										   ByRef lTransaction As SqlTransaction) As Boolean

		Try

			Dim lExiste As Boolean = False

			Const sp As String = "SELECT count(*) FROM i_nav_barras_rfid_enc where IdRFIDEnc=@IdRFIDEnc"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				lCommand.Parameters.Add(New SqlParameter("@IdRFIDEnc", pIdRFIDEnc))

				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lExiste = (CInt(lReturnValue) > 0)
				End If
			End Using

			Return lExiste

		Catch ex1 As SqlException
			Throw ex1
		End Try

	End Function

	'#GT07042026: stock para BOF.
	Public Shared Function Get_Stock(fechaInicial, fechaFinal) As DataTable

		Dim lDataTable As New DataTable

		Try

			Const sp As String =
				"SELECT 
						encIng.IdRFIDEnc Documento,
						encIng.IdOrdenCompraEnc Ingreso,
						encIng.Estado,
						encIng.Tipo,
						--encIng.IdProveedor,
						isnull(Prov.nombre,'ND') Proveedor,
						--encIng.IdRecepcionEnc Recepcion,
						encIng.IdPedidoEnc Salida,
						--encIng.IdCliente,
						isnull(CL.nombre_comercial,'ND') Cliente,
						--encIng.IdBodega,
						BD.nombre Bodega,
						--encIng.fec_mod,
						detIng.barra_epc,
						barra_pallet.Codigo Codigo,
						barra_pallet.Nombre Descripcion,
						detIng.IdDispositivo,
						--detIng.tagid,
						OP.nombres +' '+OP.apellidos Operador,
						encIng.fec_agr Fecha
						FROM I_nav_barras_rfid_det detIng
						INNER JOIN I_nav_barras_rfid_enc encIng ON encIng.IdRFIDEnc = detIng.IdRFIDEnc
						LEFT JOIN proveedor Prov ON encIng.IdProveedor=Prov.IdProveedor
						LEFT JOIN operador OP ON detIng.IdOperador = OP.IdOperador
						LEFT JOIN cliente CL ON encIng.IdCliente = CL.IdCliente
						LEFT JOIN bodega BD ON encIng.IdBodega = BD.IdBodega
						LEFT JOIN i_nav_barras_pallet barra_pallet ON detIng.barra_epc = barra_pallet.Codigo_Barra
						WHERE ISNULL(encIng.Tipo, '') = 'ING'
						AND NOT EXISTS (
						SELECT 1
						FROM I_nav_barras_rfid_det detSal
						INNER JOIN I_nav_barras_rfid_enc encSal
						ON encSal.IdRFIDEnc = detSal.IdRFIDEnc
						WHERE ISNULL(encSal.Tipo, '') = 'SAL'
						AND ISNULL(detSal.barra_epc, '') = ISNULL(detIng.barra_epc, '')
						) and encIng.fec_agr >= @pFechaInicial AND encIng.fec_agr < DATEADD(DAY, 1, @pFechaFinal) "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@pFechaInicial", fechaInicial)
						lDTA.SelectCommand.Parameters.AddWithValue("@pFechaFinal", fechaFinal)
						lDTA.Fill(lDataTable)

					End Using

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

			Return lDataTable

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	'#GT07042026: stock para HH
	Public Shared Function Get_Stock_WS() As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			Const sp As String =
				"SELECT 
					encIng.IdRFIDEnc Documento,
					encIng.IdRFIDEnc,
					encIng.IdOrdenCompraEnc Ingreso,
					encIng.IdOrdenCompraEnc,
					encIng.Estado,
					encIng.Tipo,
					encIng.IdProveedor,
					encIng.IdRecepcionEnc,
					encIng.IdPedidoEnc,
					encIng.IdBodega,
					encIng.fec_mod,
					encIng.fec_agr,
					detIng.IdRFIDDet,
					detIng.barra_epc,
					detIng.IdDispositivo,
					detIng.tagid,
					detIng.IdOperador,
					barra_pallet.Codigo,
					barra_pallet.Nombre,
					encIng.fec_agr Fecha
				FROM I_nav_barras_rfid_det detIng
				INNER JOIN I_nav_barras_rfid_enc encIng ON encIng.IdRFIDEnc = detIng.IdRFIDEnc
				LEFT JOIN proveedor Prov ON encIng.IdProveedor = Prov.IdProveedor
				LEFT JOIN operador OP ON detIng.IdOperador = OP.IdOperador
				LEFT JOIN cliente CL ON encIng.IdCliente = CL.IdCliente
				LEFT JOIN bodega BD ON encIng.IdBodega = BD.IdBodega
				LEFT JOIN i_nav_barras_pallet barra_pallet ON detIng.barra_epc = barra_pallet.Codigo_Barra
				WHERE ISNULL(encIng.Tipo, '') = 'ING'
				  AND NOT EXISTS (
						SELECT 1
						FROM I_nav_barras_rfid_det detSal
						INNER JOIN I_nav_barras_rfid_enc encSal ON encSal.IdRFIDEnc = detSal.IdRFIDEnc
						WHERE ISNULL(encSal.Tipo, '') = 'SAL'
						  AND ISNULL(detSal.barra_epc, '') = ISNULL(detIng.barra_epc, '')
				  )
				  --AND encIng.fec_agr >= @pFechaInicial AND encIng.fec_agr < DATEADD(DAY, 1, @pFechaFinal)
				ORDER BY encIng.IdRFIDEnc"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						'lDTA.SelectCommand.Parameters.AddWithValue("@pFechaInicial", fechaInicial.Date)
						'lDTA.SelectCommand.Parameters.AddWithValue("@pFechaFinal", fechaFinal.Date)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim lDic As New Dictionary(Of Integer, clsBeI_nav_barras_rfid_enc)

						For Each dr As DataRow In lDataTable.Rows

							Dim lIdRFIDEnc As Integer = CInt(dr("IdRFIDEnc"))
							Dim vEnc As clsBeI_nav_barras_rfid_enc = Nothing

							If Not lDic.ContainsKey(lIdRFIDEnc) Then
								vEnc = New clsBeI_nav_barras_rfid_enc()
								Cargar(vEnc, dr)

								' Ajustar al nombre real de la propiedad de detalle en clsBeI_nav_barras_rfid_enc
								vEnc.Detalle = New List(Of clsBeI_nav_barras_rfid_det)

								lDic.Add(lIdRFIDEnc, vEnc)
								lReturnList.Add(vEnc)
							Else
								vEnc = lDic(lIdRFIDEnc)
							End If

							Dim vDet As New clsBeI_nav_barras_rfid_det()
							clsLnI_nav_barras_rfid_det.Cargar(vDet, dr)

							' Solo se agregan los detalles que ya vienen en la tabla,
							' porque el query ya excluyó los que tienen salida
							vEnc.Detalle.Add(vDet)

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

	Public Shared Function Get_Stock_WS_Paginado(pPagina As Integer, pTamanoPagina As Integer) As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			If pPagina <= 0 Then pPagina = 1
			If pTamanoPagina <= 0 Then pTamanoPagina = 50

			Dim lOffset As Integer = (pPagina - 1) * pTamanoPagina

			Const spEnc As String =
				"WITH EncabezadosStock AS
			(
				SELECT DISTINCT
					   ing.IdRFIDEnc,
					   ing.IdOrdenCompraEnc,
					   ing.IdRecepcionEnc,
					   ing.IdBodega,
					   ing.fec_agr,
					   ing.fec_mod,
					   ing.Estado,
					   ing.Tipo,
					   ing.IdProveedor,
					   ing.IdCliente,
					   ing.IdPedidoEnc
				FROM I_nav_barras_rfid_enc ing
				INNER JOIN I_nav_barras_rfid_det detIng
					ON detIng.IdRFIDEnc = ing.IdRFIDEnc
				WHERE ISNULL(ing.Tipo, '') = 'ING'
				  AND NOT EXISTS (
						SELECT 1
						FROM I_nav_barras_rfid_enc sal
						INNER JOIN I_nav_barras_rfid_det detSal
							ON detSal.IdRFIDEnc = sal.IdRFIDEnc
						WHERE ISNULL(sal.Tipo, '') = 'SAL'
						  AND ISNULL(detSal.barra_epc, '') = ISNULL(detIng.barra_epc, '')
				  )
			)
			SELECT *
			FROM EncabezadosStock
			ORDER BY IdRFIDEnc
			OFFSET @Offset ROWS FETCH NEXT @TamanoPagina ROWS ONLY"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Dim lIds As New List(Of Integer)
					Dim lDic As New Dictionary(Of Integer, clsBeI_nav_barras_rfid_enc)

					Using lDTAEnc As New SqlDataAdapter(spEnc, lConnection)

						lDTAEnc.SelectCommand.CommandType = CommandType.Text
						lDTAEnc.SelectCommand.Transaction = lTransaction
						lDTAEnc.SelectCommand.Parameters.AddWithValue("@Offset", lOffset)
						lDTAEnc.SelectCommand.Parameters.AddWithValue("@TamanoPagina", pTamanoPagina)

						Dim lDataTableEnc As New DataTable
						lDTAEnc.Fill(lDataTableEnc)

						For Each drFila As DataRow In lDataTableEnc.Rows
							Dim vRFIDEncabezado As New clsBeI_nav_barras_rfid_enc()
							Cargar(vRFIDEncabezado, drFila)

							vRFIDEncabezado.Cliente = New clsBeCliente
							vRFIDEncabezado.Proveedor = New clsBeProveedor
							vRFIDEncabezado.Detalle = New List(Of clsBeI_nav_barras_rfid_det)
							vRFIDEncabezado.Cliente = clsLnCliente.GetSingle(vRFIDEncabezado.IdCliente, lConnection, lTransaction)
							vRFIDEncabezado.Proveedor = clsLnProveedor.GetSingle(vRFIDEncabezado.IdProveedor, lConnection, lTransaction)
							lReturnList.Add(vRFIDEncabezado)
							lDic.Add(vRFIDEncabezado.IdRFIDEnc, vRFIDEncabezado)
							lIds.Add(vRFIDEncabezado.IdRFIDEnc)
						Next

					End Using

					If lIds.Count > 0 Then

						Dim lIdsSql As String = String.Join(",", lIds)

						Dim spDet As String =
							"SELECT
							 detIng.*,
							 encIng.IdRFIDEnc
						 FROM I_nav_barras_rfid_det detIng
						 INNER JOIN I_nav_barras_rfid_enc encIng
							ON encIng.IdRFIDEnc = detIng.IdRFIDEnc
						 WHERE encIng.IdRFIDEnc IN (" & lIdsSql & ")
						   AND ISNULL(encIng.Tipo, '') = 'ING'
						   AND NOT EXISTS (
								SELECT 1
								FROM I_nav_barras_rfid_det detSal
								INNER JOIN I_nav_barras_rfid_enc encSal
									ON encSal.IdRFIDEnc = detSal.IdRFIDEnc
								WHERE ISNULL(encSal.Tipo, '') = 'SAL'
								  AND ISNULL(detSal.barra_epc, '') = ISNULL(detIng.barra_epc, '')
						   )
						 ORDER BY encIng.IdRFIDEnc"

						Using lDTADet As New SqlDataAdapter(spDet, lConnection)

							lDTADet.SelectCommand.CommandType = CommandType.Text
							lDTADet.SelectCommand.Transaction = lTransaction

							Dim lDataTableDet As New DataTable
							lDTADet.Fill(lDataTableDet)

							For Each drFilaDetalle As DataRow In lDataTableDet.Rows

								Dim lIdRFIDEnc As Integer = CInt(drFilaDetalle("IdRFIDEnc"))

								If lDic.ContainsKey(lIdRFIDEnc) Then
									Dim vDet As New clsBeI_nav_barras_rfid_det()
									clsLnI_nav_barras_rfid_det.Cargar(vDet, drFilaDetalle)

									vDet.Producto = New clsBeProducto

									Dim vCodigoProducto = clsLnI_nav_barras_pallet.Get_CodigoProducto_By_Barra_EPC(vDet.Barra_epc, lConnection, lTransaction)

									If String.IsNullOrWhiteSpace(vCodigoProducto) Then
										Throw New ApplicationException(String.Format("GT08042026: No existe código de producto para la barra EPC: {0}", vDet.Barra_epc))
									End If

									vDet.Producto = clsLnProducto.Get_Single_By_Codigo(vCodigoProducto, lConnection, lTransaction)
									lDic(lIdRFIDEnc).Detalle.Add(vDet)

								End If

							Next

						End Using

					End If

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

			Return lReturnList

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), ex)
		End Try

	End Function

	Public Shared Function Actualizar_Encabezado(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_enc")
			'Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			'Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
			'Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			'Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			'Upd.Add("estado", "@estado", DataType.Parametro)
			'Upd.Add("tipo", "@tipo", DataType.Parametro)
			Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
			Upd.Add("idcliente", "@idcliente", DataType.Parametro)
			'Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Where("IdRFIDEnc = @IdRFIDEnc")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))
			'cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_enc.IdOrdenCompraEnc))
			'cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_barras_rfid_enc.IdRecepcionEnc))
			'cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_enc.IdBodega))
			'cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_enc.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_enc.Fec_mod))
			'cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_barras_rfid_enc.Estado))
			'cmd.Parameters.Add(New SqlParameter("@TIPO", oBeI_nav_barras_rfid_enc.Tipo))
			cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeI_nav_barras_rfid_enc.IdProveedor))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_barras_rfid_enc.IdCliente))
			'cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_enc.IdPedidoEnc))

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

End Class