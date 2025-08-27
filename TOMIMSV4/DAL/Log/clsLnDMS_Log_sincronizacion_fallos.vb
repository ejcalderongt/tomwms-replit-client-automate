Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnDMS_Log_sincronizacion_fallos

	Public Shared Sub Cargar(ByRef oBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos, ByRef dr As DataRow)
		Try
			With oBeLog_sincronizacion_fallos
				.IdLogFallo = IIf(IsDBNull(dr.Item("IdLogFallo")), 0, dr.Item("IdLogFallo"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
				.Mensaje_error = IIf(IsDBNull(dr.Item("Mensaje_error")), "", dr.Item("Mensaje_error"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("log_dms_sincronizacion_fallos")
			Ins.Add("idlogfallo", "@idlogfallo", DataType.Parametro)
			Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("estado", "@estado", DataType.Parametro)
			Ins.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGFALLO", oBeLog_sincronizacion_fallos.IdLogFallo))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeLog_sincronizacion_fallos.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_sincronizacion_fallos.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_fallos.Estado))
			cmd.Parameters.Add(New SqlParameter("@MENSAJE_ERROR", oBeLog_sincronizacion_fallos.Mensaje_error))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_sincronizacion_fallos.Fec_agr))

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

	Public Shared Function Actualizar(ByRef oBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("log_dms_sincronizacion_fallos")
			Upd.Add("idlogfallo", "@idlogfallo", DataType.Parametro)
			Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Where("IdLogFallo = @IdLogFallo")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGFALLO", oBeLog_sincronizacion_fallos.IdLogFallo))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeLog_sincronizacion_fallos.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_sincronizacion_fallos.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_fallos.Estado))
			cmd.Parameters.Add(New SqlParameter("@MENSAJE_ERROR", oBeLog_sincronizacion_fallos.Mensaje_error))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_sincronizacion_fallos.Fec_agr))

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


	Public Shared Function Eliminar(ByRef oBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from log_dms_sincronizacion_fallos" &
			 "  Where(IdLogFallo = @IdLogFallo)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGFALLO", oBeLog_sincronizacion_fallos.IdLogFallo))

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


	Public Shared Function Eliminar_Todo(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from log_dms_sincronizacion_fallos "


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

			Const sp As String = "SELECT * FROM log_dms_sincronizacion_fallos"
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


	Public Shared Function Listar_By_Error() As DataTable

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "SELECT IdLogFallo as IdLog, IdOrdenCompraEnc Ingreso, IdPedidoEnc Salida, IdProducto Producto, Mensaje_error, Fec_agr Fecha 
							      FROM log_dms_sincronizacion_fallos"

			'where Estado='Error'"

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
	Public Shared Function Get_All() As List(Of clsBeDMS_Log_sincronizacion_fallos)

		Dim lReturnList As New List(Of clsBeDMS_Log_sincronizacion_fallos)

		Try

			Const sp As String = "SELECT * FROM log_dms_sincronizacion_fallos"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeLog_sincronizacion_fallos As New clsBeDMS_Log_sincronizacion_fallos

						For Each dr As DataRow In lDataTable.Rows
							vBeLog_sincronizacion_fallos = New clsBeDMS_Log_sincronizacion_fallos()
							Cargar(vBeLog_sincronizacion_fallos, dr)
							lReturnList.Add(vBeLog_sincronizacion_fallos)
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

	Public Shared Sub GetSingle(ByRef pBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos)

		Try

			Const sp As String = "SELECT * FROM log_dms_sincronizacion_fallos" &
			" Where(IdLogFallo = @IdLogFallo)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeLog_sincronizacion_fallos As New clsBeDMS_Log_sincronizacion_fallos

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeLog_sincronizacion_fallos, lDataTable.Rows(0))
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

	Public Shared Function MaxID(Optional ByRef lConnection As SqlConnection = Nothing, Optional ByRef lTransaction As SqlTransaction = Nothing) As Integer

		Dim localConnection As Boolean = False
		Dim localTransaction As Boolean = False
		Dim lMax As Integer = 0

		Try
			' Crear conexión local si no viene externa
			If lConnection Is Nothing Then
				lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()
				localConnection = True
			End If

			' Crear transacción local si no viene externa
			If lTransaction Is Nothing Then
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				localTransaction = True
			End If

			Const sp As String = "SELECT ISNULL(Max(IdLogFallo),0) FROM log_dms_sincronizacion_fallos"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue)
				End If
			End Using

			' Hacer commit si la transacción fue creada localmente
			If localTransaction Then
				lTransaction.Commit()
			End If

			Return lMax

		Catch ex As Exception
			' Hacer rollback si la transacción fue creada localmente
			If localTransaction AndAlso lTransaction IsNot Nothing Then
				Try
					lTransaction.Rollback()
				Catch
					' Ignorar errores en rollback
				End Try
			End If
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))

		Finally
			' Cerrar conexión solo si fue creada localmente
			If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
				lConnection.Close()
			End If
		End Try

	End Function


	'Public Shared Function MaxID() As Integer

	'	Try

	'		Dim lMax As Integer = 0

	'		Const sp As String = "SELECT ISNULL(Max(IdLogFallo),0) FROM log_dms_sincronizacion_fallos"

	'		Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

	'			lConnection.Open()

	'			Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

	'				Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

	'					Dim lReturnValue As Object = lCommand.ExecuteScalar()
	'					If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
	'						lMax = CInt(lReturnValue)
	'					End If

	'				End Using

	'				lTransaction.Commit()

	'			End Using

	'			lConnection.Close()

	'		End Using

	'		Return lMax

	'	Catch ex As Exception
	'		Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
	'	End Try

	'End Function

	'#GT01072025: obtener producto no sincronizado.
	'#GT15072025: validar que el producto esta asociado a un propietario parametrizado para sincronizar.
	'Public Shared Function ObtenerRegistrosFallidos_by_Producto(ByVal listaPropietarios As List(Of Integer), ByVal pFechaSincro As DateTime, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of Integer)
	'	Dim fallidos As New List(Of Integer)()

	'	Try
	'		Dim query As String = "SELECT distinct IdProducto FROM log_dms_sincronizacion_fallos WHERE Estado = 'Error' AND Idproducto<>0 and fec_agr < @pFechaSincro"
	'		Using cmd As New SqlCommand(query, pConection, pTransaction)
	'			cmd.Parameters.AddWithValue("@pFechaSincro", pFechaSincro)
	'			Using reader As SqlDataReader = cmd.ExecuteReader()
	'				While reader.Read()
	'					fallidos.Add(reader.GetInt32(0)) ' El ID de registro es el primer campo
	'				End While
	'			End Using
	'		End Using
	'	Catch ex As Exception
	'		Console.WriteLine("Error al obtener registros fallidos: " & ex.Message)
	'	End Try

	'	Return fallidos
	'End Function

	Public Shared Function ObtenerRegistrosFallidos_by_Producto(ByVal listaPropietarios As List(Of Integer),
																ByVal pFechaSincro As DateTime,
																Optional ByVal pConection As SqlConnection = Nothing,
																Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of Integer)

		Dim fallidos As New List(Of Integer)()

		Try
			' Construir parámetros dinámicos para el IN
			Dim parametrosIn As New List(Of String)
			For i As Integer = 0 To listaPropietarios.Count - 1
				parametrosIn.Add("@prop" & i)
			Next

			Dim inClause As String = String.Join(",", parametrosIn)

			Dim query As String =
			"SELECT DISTINCT IdProducto " &
			"FROM log_dms_sincronizacion_fallos " &
			"WHERE Estado = 'Error' " &
			"AND IdProducto <> 0 " &
			"AND fec_agr < @pFechaSincro " &
			"AND IdPropietario IN (" & inClause & ")"

			Using cmd As New SqlCommand(query, pConection, pTransaction)
				' Parámetro fecha
				cmd.Parameters.AddWithValue("@pFechaSincro", pFechaSincro)

				' Parámetros de propietarios
				For i As Integer = 0 To listaPropietarios.Count - 1
					cmd.Parameters.AddWithValue(parametrosIn(i), listaPropietarios(i))
				Next

				Using reader As SqlDataReader = cmd.ExecuteReader()
					While reader.Read()
						fallidos.Add(reader.GetInt32(0))
					End While
				End Using
			End Using

		Catch ex As Exception
			Console.WriteLine("Error al obtener registros fallidos: " & ex.Message)
		End Try

		Return fallidos
	End Function


	Public Shared Function ObtenerRegistrosFallidos_by_Pedido(ByVal listaPropietarios As List(Of Integer), ByVal pFechaSincro As DateTime, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of Integer)
		Dim fallidos As New List(Of Integer)()

		Try

			' Construir parámetros dinámicos para el IN
			Dim parametrosIn As New List(Of String)
			For i As Integer = 0 To listaPropietarios.Count - 1
				parametrosIn.Add("@prop" & i)
			Next

			Dim inClause As String = String.Join(",", parametrosIn)

			Dim query As String = "SELECT distinct IdPedidoEnc FROM log_dms_sincronizacion_fallos WHERE Estado = 'Error' AND IdPedidoEnc<>0 and fec_agr < @pFechaSincro AND IdPropietario IN (" & inClause & ") "


			Using cmd As New SqlCommand(query, pConection, pTransaction)
				cmd.Parameters.AddWithValue("@pFechaSincro", pFechaSincro)


				' Parámetros de propietarios
				For i As Integer = 0 To listaPropietarios.Count - 1
					cmd.Parameters.AddWithValue(parametrosIn(i), listaPropietarios(i))
				Next

				Using reader As SqlDataReader = cmd.ExecuteReader()
					While reader.Read()
						fallidos.Add(reader.GetInt32(0)) ' El ID de registro es el primer campo
					End While
				End Using
			End Using
		Catch ex As Exception
			Console.WriteLine("Error al obtener registros fallidos: " & ex.Message)
		End Try

		Return fallidos
	End Function


	Public Shared Function ObtenerRegistrosFallidos_by_Ingreso(ByVal listaPropietarios As List(Of Integer), ByVal pFechaSincro As DateTime, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of Integer)
		Dim fallidos As New List(Of Integer)()

		Try


			' Construir parámetros dinámicos para el IN
			Dim parametrosIn As New List(Of String)
			For i As Integer = 0 To listaPropietarios.Count - 1
				parametrosIn.Add("@prop" & i)
			Next

			Dim inClause As String = String.Join(",", parametrosIn)

			Dim query As String = "SELECT distinct IdOrdenCompraEnc FROM log_dms_sincronizacion_fallos WHERE Estado = 'Error' AND IdOrdenCompraEnc<>0 and fec_agr < @pFechaSincro AND IdPropietario IN (" & inClause & ") "
			Using cmd As New SqlCommand(query, pConection, pTransaction)
				cmd.Parameters.AddWithValue("@pFechaSincro", pFechaSincro)

				' Parámetros de propietarios
				For i As Integer = 0 To listaPropietarios.Count - 1
					cmd.Parameters.AddWithValue(parametrosIn(i), listaPropietarios(i))
				Next


				Using reader As SqlDataReader = cmd.ExecuteReader()
					While reader.Read()
						fallidos.Add(reader.GetInt32(0)) ' El ID de registro es el primer campo
					End While
				End Using
			End Using
		Catch ex As Exception
			Console.WriteLine("Error al obtener registros fallidos: " & ex.Message)
		End Try

		Return fallidos
	End Function

	Public Shared Function Actualizar_Registro(ByRef oBeLog_sincronizacion_fallos As clsBeDMS_Log_sincronizacion_fallos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("log_dms_sincronizacion_fallos")
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

			If oBeLog_sincronizacion_fallos.IdOrdenCompraEnc > 0 Then
				Upd.Where("idordencompraenc = @idordencompraenc")
			End If

			If oBeLog_sincronizacion_fallos.IdPedidoEnc > 0 Then
				Upd.Where("idpedidoenc = @idpedidoenc")
			End If

			If oBeLog_sincronizacion_fallos.IdProducto > 0 Then
				Upd.Where("idproducto = @idproducto")
			End If



			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_fallos.Estado))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeLog_sincronizacion_fallos.Fec_mod))

			If oBeLog_sincronizacion_fallos.IdOrdenCompraEnc > 0 Then
				cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeLog_sincronizacion_fallos.IdOrdenCompraEnc))
			End If

			If oBeLog_sincronizacion_fallos.IdPedidoEnc > 0 Then
				cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_sincronizacion_fallos.IdPedidoEnc))
			End If

			If oBeLog_sincronizacion_fallos.IdProducto > 0 Then
				cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeLog_sincronizacion_fallos.IdProducto))
			End If

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

	Public Shared Function Existe_by_Ingreso(ByVal pIdOrdenCompraEnc As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean
		Existe_by_Ingreso = False

		Try
			Dim query As String = "SELECT 1 FROM log_dms_sincronizacion_fallos WHERE Estado = 'Error' AND IdOrdenCompraEnc = @pIdOrdenCompraEnc"
			Using cmd As New SqlCommand(query, pConection, pTransaction)
				cmd.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
				Using reader As SqlDataReader = cmd.ExecuteReader()
					If reader.HasRows Then
						Existe_by_Ingreso = True
					End If
				End Using
			End Using
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Function

	Public Shared Function Existe_by_Pedido(ByVal pIdPedidoEnc As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean
		Existe_by_Pedido = False

		Try
			Dim query As String = "SELECT 1 FROM log_dms_sincronizacion_fallos WHERE Estado = 'Error' AND IdPedidoEnc = @IdPedidoEnc"
			Using cmd As New SqlCommand(query, pConection, pTransaction)
				cmd.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
				Using reader As SqlDataReader = cmd.ExecuteReader()
					If reader.HasRows Then
						Existe_by_Pedido = True
					End If
				End Using
			End Using
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Function

	Public Shared Function Existe_by_Producto(ByVal pProducto As clsBeProducto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean
		Existe_by_Producto = False

		Try
			Dim query As String = "SELECT 1 FROM log_dms_sincronizacion_fallos WHERE (Estado = 'Error' AND IdProducto = @IdProducto and IdPropietario=@IdPropietario)"
			Using cmd As New SqlCommand(query, pConection, pTransaction)
				cmd.Parameters.AddWithValue("@IdProducto", pProducto.IdProducto)
				cmd.Parameters.AddWithValue("@IdPropietario", pProducto.IdPropietario)
				Using reader As SqlDataReader = cmd.ExecuteReader()
					If reader.HasRows Then
						Existe_by_Producto = True
					End If
				End Using
			End Using
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Function

End Class
