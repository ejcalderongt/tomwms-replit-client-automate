Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsLnProducto_talla_color

	Public Shared Sub Cargar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, ByRef dr As DataRow)
		Try
			With oBeProducto_talla_color
				.IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
				.IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
				.IdTalla = IIf(IsDBNull(dr.Item("IdTalla")), 0, dr.Item("IdTalla"))
				.IdColor = IIf(IsDBNull(dr.Item("IdColor")), 0, dr.Item("IdColor"))
				.CodigoSKU = IIf(IsDBNull(dr.Item("CodigoSKU")), "", dr.Item("CodigoSKU"))
				.IdCampaña = IIf(IsDBNull(dr.Item("IdCampaña")), 0, dr.Item("IdCampaña"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), New Date(1900, 1, 1), dr.Item("fec_agr"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), New Date(1900, 1, 1), dr.Item("fec_mod"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("producto_talla_color")
			Ins.Add("idproductotallacolor", "@idproductotallacolor", WMSTipoDato.Tipo.Parametro)
			Ins.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
			Ins.Add("idtalla", "@idtalla", WMSTipoDato.Tipo.Parametro)
			Ins.Add("idcolor", "@idcolor", WMSTipoDato.Tipo.Parametro)
			Ins.Add("codigosku", "@codigosku", WMSTipoDato.Tipo.Parametro)
			If oBeProducto_talla_color.IdCampaña > 0 Then Ins.Add("idcampaña", "@idcampaña", WMSTipoDato.Tipo.Parametro)
			Ins.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
			Ins.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
			Ins.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
			Ins.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)
			Ins.Add("activo", "@activo", WMSTipoDato.Tipo.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_talla_color.IdProducto))
			cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_talla_color.IdTalla))
			cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_talla_color.IdColor))
			cmd.Parameters.Add(New SqlParameter("@CODIGOSKU", oBeProducto_talla_color.CodigoSKU))
			If oBeProducto_talla_color.IdCampaña > 0 Then cmd.Parameters.Add(New SqlParameter("@IDCAMPAÑA", oBeProducto_talla_color.IdCampaña))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_talla_color.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_talla_color.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_talla_color.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_talla_color.User_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_talla_color.Activo))

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
	Public Shared Function Actualizar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("producto_talla_color")
			Upd.Add("idproductotallacolor", "@idproductotallacolor", WMSTipoDato.Tipo.Parametro)
			Upd.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
			Upd.Add("idtalla", "@idtalla", WMSTipoDato.Tipo.Parametro)
			Upd.Add("idcolor", "@idcolor", WMSTipoDato.Tipo.Parametro)
			Upd.Add("codigosku", "@codigosku", WMSTipoDato.Tipo.Parametro)
			Upd.Add("idcampaña", "@idcampaña", WMSTipoDato.Tipo.Parametro)
			Upd.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
			Upd.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
			Upd.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
			Upd.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)
			Upd.Add("activo", "@activo", WMSTipoDato.Tipo.Parametro)
			Upd.Where("IdProductoTallaColor = @IdProductoTallaColor")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_talla_color.IdProducto))
			cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_talla_color.IdTalla))
			cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_talla_color.IdColor))
			cmd.Parameters.Add(New SqlParameter("@CODIGOSKU", oBeProducto_talla_color.CodigoSKU))
			cmd.Parameters.Add(New SqlParameter("@IDCAMPAÑA", oBeProducto_talla_color.IdCampaña))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_talla_color.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_talla_color.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_talla_color.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_talla_color.User_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_talla_color.Activo))

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


	Public Shared Function Eliminar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Producto_talla_color" &
			 "  Where(IdProductoTallaColor = @IdProductoTallaColor)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))

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

			Const sp As String = "SELECT * FROM Producto_talla_color"
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

	Public Shared Function Get_All() As List(Of clsBeProducto_talla_color)

		Dim lReturnList As New List(Of clsBeProducto_talla_color)

		Try

			Const sp As String = "SELECT * FROM Producto_talla_color"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeProducto_talla_color As New clsBeProducto_talla_color

						For Each dr As DataRow In lDataTable.Rows
							vBeProducto_talla_color = New clsBeProducto_talla_color()
							Cargar(vBeProducto_talla_color, dr)
							lReturnList.Add(vBeProducto_talla_color)
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
	Public Shared Function GetSingle(IdProductoTallaColor As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeProducto_talla_color

		GetSingle = Nothing

		Try

			Const sp As String = " SELECT * FROM Producto_talla_color " &
								 " Where(IdProductoTallaColor = @IdProductoTallaColor)"


			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("IdProductoTallaColor", IdProductoTallaColor)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeProducto_talla_color As New clsBeProducto_talla_color

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
					GetSingle = vBeProducto_talla_color
				End If

			End Using

		Catch ex As Exception
			Throw ex
		End Try

	End Function
	Public Shared Function MaxID() As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color"

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

	Public Shared Function Existe(ByVal idProductoTallaColor As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Dim sql As String = "SELECT COUNT(idproductotallacolor) FROM producto_talla_color WHERE idproductotallacolor = @idproductotallacolor"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@idproductotallacolor", idProductoTallaColor))

			Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return (count > 0)

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try
	End Function

	Public Shared Sub InsertOrUpdate(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing)
		Try
			If ExisteBySKU(oBeProducto_talla_color.CodigoSKU, pConection, pTransaction) Then
				Actualizar(oBeProducto_talla_color, pConection, pTransaction)
			Else
				oBeProducto_talla_color.IdProductoTallaColor = MaxID(pConection, pTransaction) + 1
				Insertar(oBeProducto_talla_color, pConection, pTransaction)
			End If
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function ExisteBySKU(ByVal CodigoSku As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Dim sql As String = "SELECT COUNT(idproductotallacolor) FROM producto_talla_color WHERE CodigoSKU = @CodigoSKU"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@CodigoSKU", CodigoSku))

			Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return (count > 0)

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try
	End Function

	Public Shared Function Get_All_Dt_By_IdCampaña(IdCampaña As Integer) As DataTable

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "select ptc.IdProductoTallaColor as Codigo, 
									t.Descripcion as Talla,
									CONCAT(c.codigo,'-', c.Nombre) as Color,
									ca.Nombre as Campaña,
									ptc.CodigoSKU
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									left join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdCampaña= @IdCampaña "

			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			cmd.Parameters.AddWithValue("@IdCampaña", IdCampaña)
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

	Public Shared Function Get_All_Dt_By_IdProducto(IdProducto As Integer) As DataTable

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "select IdProductoTallaColor as Codigo, 
									t.Descripcion as Color,
									c.Nombre as Talla,
									ca.Nombre as Campaña,
									ptc.CodigoSKU 
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									left join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdProducto = @IdProducto "

			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			dad.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
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

	Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue)
				End If

			End Using

			Return lMax

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function Get_Single_By_IdProducto(IdProducto As Integer, Talla As String, Color As String) As clsBeProducto_talla_color

		Get_Single_By_IdProducto = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.Codigo = @CodigoTalla AND c.Codigo = @CodigoColor)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
						lDTA.SelectCommand.Parameters.AddWithValue("@CodigoTalla", Talla)
						lDTA.SelectCommand.Parameters.AddWithValue("@CodigoColor", Color)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeProducto_talla_color As New clsBeProducto_talla_color

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
							Get_Single_By_IdProducto = vBeProducto_talla_color
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

	Public Shared Function Get_Single_By_Params(IdProducto As Integer, Talla As String, Color As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeProducto_talla_color

		Get_Single_By_Params = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.Codigo = @CodigoTalla AND c.Codigo = @CodigoColor)"


			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
				lDTA.SelectCommand.Parameters.AddWithValue("@CodigoTalla", Talla)
				lDTA.SelectCommand.Parameters.AddWithValue("@CodigoColor", Color)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeProducto_talla_color As New clsBeProducto_talla_color

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
					Get_Single_By_Params = vBeProducto_talla_color
				End If

			End Using

		Catch ex As Exception
			Throw ex
		End Try

	End Function

	Public Shared Function GetSingle(ByVal IdProductoTallaColor As Integer) As clsBeProducto_talla_color

		GetSingle = Nothing

		Try

			Const sp As String = "SELECT * FROM Producto_talla_color " &
								 " Where(IdProductoTallaColor = @IdProductoTallaColor)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoTallaColor", IdProductoTallaColor)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeProducto_talla_color As New clsBeProducto_talla_color

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
							GetSingle = vBeProducto_talla_color
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

	Public Shared Function Get_All_Dt_By_IdProductoTallaColor(IdProductoTallaColor As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As DataTable

		Try

			Const sp As String = "select IdProductoTallaColor as Codigo, 
									t.Codigo as Talla,
									c.Codigo as Color,									
									ptc.CodigoSKU as SKU
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									left join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdProductoTallaColor = @IdProductoTallaColor "

			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			dad.SelectCommand.Parameters.AddWithValue("@IdProductoTallaColor", IdProductoTallaColor)
			Dim dt As New DataTable
			dad.Fill(dt)

			Return dt

		Catch ex As Exception
			Throw ex
		End Try

	End Function

	Public Shared Function Get_All_Dt_By_IdCampaña_And_IdOrdenCompraEnc(IdCampaña As Integer,
																		pIdOrdenCompraEnc As Integer) As DataTable

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "select ptc.IdProductoTallaColor as Codigo, 
									t.Descripcion as Talla,
									CONCAT(c.codigo,'-', c.Nombre) as Color,
									ca.Nombre as Campaña,
									ptc.CodigoSKU
									from producto_talla_color ptc 
									inner join trans_oc_det oc ON oc.IdProductoTallaColor = ptc.IdProductoTallaColor
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									inner join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdCampaña= @IdCampaña AND oc.IdOrdenCompraEnc = @IdIdOrdenCompraEnc"

			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			cmd.Parameters.AddWithValue("@IdCampaña", IdCampaña)
			cmd.Parameters.AddWithValue("@IdIdOrdenCompraEnc", pIdOrdenCompraEnc)
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

	Public Shared Function Get_Single_By_IdProductoBodega(IdProductoBodega As Integer, Talla As String, Color As String) As clsBeProducto_talla_color

		Get_Single_By_IdProductoBodega = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  join producto_bodega pb on pb.IdProducto = p.IdProducto
								  Where(pb.IdProductoBodega = @IdProductoBodega AND t.Codigo = @CodigoTalla AND c.Codigo = @CodigoColor)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
						lDTA.SelectCommand.Parameters.AddWithValue("@CodigoTalla", Talla)
						lDTA.SelectCommand.Parameters.AddWithValue("@CodigoColor", Color)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeProducto_talla_color As New clsBeProducto_talla_color

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
							Get_Single_By_IdProductoBodega = vBeProducto_talla_color
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

	Public Shared Function Get_Single_By_IdProducto(IdProducto As Integer, Talla As String, Color As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeProducto_talla_color

		Get_Single_By_IdProducto = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.Codigo = @CodigoTalla AND c.Codigo = @CodigoColor)"


			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
				lDTA.SelectCommand.Parameters.AddWithValue("@CodigoTalla", Talla)
				lDTA.SelectCommand.Parameters.AddWithValue("@CodigoColor", Color)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeProducto_talla_color As New clsBeProducto_talla_color

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
					Get_Single_By_IdProducto = vBeProducto_talla_color
				End If

			End Using

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function Get_Single_Dt_By_IdProductoTallaColor(IdProductoTallaColor As Integer) As DataTable
		Try

			'#GT12122025: left join para la campaña, puede ser null

			Const sql As String = "
						SELECT 
							ptc.IdProductoTallaColor AS Codigo, 
							t.Codigo AS Talla,
							c.Codigo AS Color,									
							ptc.CodigoSKU AS SKU
						FROM producto_talla_color AS ptc
						INNER JOIN talla  AS t ON ptc.IdTalla  = t.IdTalla
						INNER JOIN color  AS c ON ptc.IdColor  = c.IdColor
						LEFT JOIN [campaña] AS ca ON ptc.IdCampaña = ca.IdCampaña
						WHERE ptc.IdProductoTallaColor = @IdProductoTallaColor;"

			Dim dt As New DataTable()

			Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
					Using cmd As New SqlCommand(sql, lConnection, lTransaction)
						cmd.CommandType = CommandType.Text
						cmd.Parameters.Add("@IdProductoTallaColor", SqlDbType.Int).Value = IdProductoTallaColor

						Using dad As New SqlDataAdapter(cmd)
							dad.Fill(dt)
						End Using
					End Using
				End Using
			End Using

			Return dt

		Catch ex As Exception
			Throw New Exception($"Error en Get_All_Dt_By_IdProductoTallaColor: {ex.Message}", ex)
		End Try
	End Function

	Public Shared Function Existe_Producto_By_Talla_and_Color(ByVal IdProducto As Integer, ByVal Idtalla As Integer, ByVal IdColor As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Dim sql As String = "SELECT COUNT(*) FROM producto_talla_color WHERE (Idproducto=@IdProducto and IdTalla=@IdTalla and IdColor=@IdColor)"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IdProducto", IdProducto))
			cmd.Parameters.Add(New SqlParameter("@IdTalla", Idtalla))
			cmd.Parameters.Add(New SqlParameter("@IdColor", IdColor))

			Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return (count > 0)

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try
	End Function

	'#GT13082025: cargar producto_talla_color por Id, no por códigos
	Public Shared Function Get_Single_By_IdParameters(IdProducto As Integer, IdTalla As Integer, IdColor As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeProducto_talla_color

		Get_Single_By_IdParameters = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.IdTalla = @IdTalla AND c.IdColor = @IdColor)"

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
				lDTA.SelectCommand.Parameters.AddWithValue("@IdTalla", IdTalla)
				lDTA.SelectCommand.Parameters.AddWithValue("@IdColor", IdColor)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeProducto_talla_color As New clsBeProducto_talla_color

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
					Get_Single_By_IdParameters = vBeProducto_talla_color
				End If

			End Using

		Catch ex As Exception
			Throw ex
		End Try

	End Function

	'#GT25082025: devolver una lista de objetos
	Public Shared Function Get_All_By_IdProducto(IdProducto As Integer, Optional IdCampania As Integer = 0) As List(Of clsBeProducto_talla_color)

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Get_All_By_IdProducto = Nothing

		Try

			Dim sp As String = "select ptc.*
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor "

			If IdCampania > 0 Then
				sp += "inner join campaña ca on ptc.IdCampaña = ca.IdCampaña "
			End If

			sp += "WHERE ptc.IdProducto = @IdProducto "

			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			dad.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
			Dim dt As New DataTable
			dad.Fill(dt)


			If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
				Get_All_By_IdProducto = New List(Of clsBeProducto_talla_color)()

				For Each row In dt.Rows
					Dim vBeProducto_talla_color = New clsBeProducto_talla_color()
					Cargar(vBeProducto_talla_color, row)
					Get_All_By_IdProducto.Add(vBeProducto_talla_color)
				Next

			End If

			lTransaction.Commit()

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	'#GT26082025: obtener producto_talla_color segun la talla y el color seleccionado en un pedido manual
	Public Shared Function Get_ProductoTallaColor_By_Talla_and_Color(ByVal Idtalla As Integer,
																	 ByVal IdColor As Integer,
																	 ByVal IdProducto As Integer,
																	 Optional ByVal pConection As SqlConnection = Nothing,
																	 Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeProducto_talla_color

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing
		Get_ProductoTallaColor_By_Talla_and_Color = Nothing
		Try

			Dim sql As String = "SELECT * 
                                 FROM producto_talla_color
                                 WHERE IdTalla=@IdTalla and IdColor=@IdColor and IdProducto = @IdProducto "

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IdTalla", Idtalla))
			cmd.Parameters.Add(New SqlParameter("@IdColor", IdColor))
			cmd.Parameters.Add(New SqlParameter("@IdProducto", IdProducto))

			Dim lDataTable As New DataTable


			Using da As New SqlDataAdapter(cmd)
				da.Fill(lDataTable)
			End Using

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Dim vBeProducto_talla_color = New clsBeProducto_talla_color()
				Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
				Get_ProductoTallaColor_By_Talla_and_Color = vBeProducto_talla_color
			End If


		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_All_By_IdProducto_FromStock(IdProducto As Integer, Optional IdCampania As Integer = 0) As List(Of clsBeProducto_talla_color)

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Get_All_By_IdProducto_FromStock = Nothing

		Try

			Dim sp As String = "select ptc.*
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor "

			If IdCampania > 0 Then
				sp += "inner join campaña ca on ptc.IdCampaña = ca.IdCampaña "
			End If

			sp += "WHERE ptc.IdProducto = @IdProducto AND ptc.IdProductoTallaColor IN (Select IdProductoTallaColor from stock Where IdProducto = @IdProducto) "

			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			dad.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
			Dim dt As New DataTable
			dad.Fill(dt)


			Dim v As New List(Of clsBeProducto_talla_color)()

			If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

				For Each row In dt.Rows
					Dim vBeProducto_talla_color = New clsBeProducto_talla_color()
					Cargar(vBeProducto_talla_color, row)
					v.Add(vBeProducto_talla_color)
				Next

			End If

			Get_All_By_IdProducto_FromStock = v

			lTransaction.Commit()

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_Single_Dt_By_IdProductoTallaColor(IdProductoTallaColor As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As DataTable
		Try
			Const sql As String = "
						SELECT 
							ptc.IdProductoTallaColor AS Codigo, 
							t.Codigo AS Talla,
							c.Codigo AS Color,									
							ptc.CodigoSKU AS SKU
						FROM producto_talla_color AS ptc
						INNER JOIN talla  AS t ON ptc.IdTalla  = t.IdTalla
						INNER JOIN color  AS c ON ptc.IdColor  = c.IdColor
						LEFT JOIN [campaña] AS ca ON ptc.IdCampaña = ca.IdCampaña
						WHERE ptc.IdProductoTallaColor = @IdProductoTallaColor;"

			Dim dt As New DataTable()

			Using cmd As New SqlCommand(sql, lConnection, lTransaction)
				cmd.CommandType = CommandType.Text
				cmd.Parameters.Add("@IdProductoTallaColor", SqlDbType.Int).Value = IdProductoTallaColor

				Using dad As New SqlDataAdapter(cmd)
					dad.Fill(dt)
				End Using
			End Using

			Return dt

		Catch ex As Exception
			Throw New Exception($"Error en Get_All_Dt_By_IdProductoTallaColor: {ex.Message}", ex)
		End Try
	End Function

	Public Shared Function Get_Single_By_IdColor_IdTalla(IdProducto As Integer, Talla As String, Color As String) As clsBeProducto_talla_color

		Get_Single_By_IdColor_IdTalla = Nothing

		Try

			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.IdTalla = @IdTalla AND c.IdColor = @IdColor)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
						lDTA.SelectCommand.Parameters.AddWithValue("@IdTalla", Talla)
						lDTA.SelectCommand.Parameters.AddWithValue("@IdColor", Color)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeProducto_talla_color As New clsBeProducto_talla_color

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
							Get_Single_By_IdColor_IdTalla = vBeProducto_talla_color
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

	Public Shared Function Get_Single_By_IdColor_IdTalla(IdProducto As Integer, Talla As String, Color As String,
														 lConnection As SqlConnection,
														 lTransaction As SqlTransaction) As clsBeProducto_talla_color

		Get_Single_By_IdColor_IdTalla = Nothing

		Try
			Const sp As String = "select * from producto_talla_color p
								  join talla t on p.IdTalla = t.IdTalla
								  join color c on p.IdColor = c.IdColor
								  Where(p.IdProducto = @IdProducto AND t.Codigo = @Talla AND c.Codigo = @Color)"

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
				lDTA.SelectCommand.Parameters.AddWithValue("@Talla", Talla)
				lDTA.SelectCommand.Parameters.AddWithValue("@Color", Color)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeProducto_talla_color As New clsBeProducto_talla_color

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
					Get_Single_By_IdColor_IdTalla = vBeProducto_talla_color
				End If

			End Using

		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Public Shared Function Get_IdProductoTallaColor_By_CodTalla_and_CodColor(ByVal pCodigoTalla As String,
																			 ByVal pCodigoColor As String,
																			 ByVal pIdProducto As Integer,
																			 Optional ByVal pConection As SqlConnection = Nothing,
																			 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing
		Get_IdProductoTallaColor_By_CodTalla_and_CodColor = 0

		Try

			Dim sql As String = "SELECT IdProductoTallaColor
								 FROM producto_talla_color ptc LEFT OUTER JOIN 
									  talla t ON t.IdTalla = ptc.IdTalla LEFT OUTER JOIN
									  color c ON c.IdColor = ptc.IdColor 
								 WHERE t.Codigo=@CodTalla and c.Codigo=@CodColor and ptc.IdProducto = @IdProducto"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@CodTalla", pCodigoTalla))
			cmd.Parameters.Add(New SqlParameter("@CodColor", pCodigoColor))
			cmd.Parameters.Add(New SqlParameter("@IdProducto", pIdProducto))

			Dim lDataTable As New DataTable

			Using da As New SqlDataAdapter(cmd)
				da.Fill(lDataTable)
			End Using

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Get_IdProductoTallaColor_By_CodTalla_and_CodColor = lDataTable.Rows(0).Item("IdProductoTallaColor")
			End If

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_IdProductoTallaColor_By_IdTalla_and_IdColor(ByVal pIdtalla As Integer, ByVal pIdColor As Integer,
																		   ByVal pIdProducto As Integer,
																		   Optional ByVal pConection As SqlConnection = Nothing,
																		   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
		Dim cmd As New SqlCommand()
		Dim lTransaction As SqlTransaction = Nothing
		Get_IdProductoTallaColor_By_IdTalla_and_IdColor = Nothing
		Try

			Dim sql As String = "SELECT ptc.IdProductoTallaColor
								 FROM producto_talla_color ptc LEFT OUTER JOIN 
									  talla t ON t.IdTalla = ptc.IdTalla LEFT OUTER JOIN
									  color c ON c.IdColor = ptc.IdColor  
								 WHERE (t.IdTalla=@pIdTalla and c.IdColor=@pIdColor and ptc.IdProducto=@pIdProducto )"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sql, pConection, pTransaction)
			Else
				lConnection.Open()
				lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sql, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@pIdTalla", pIdtalla))
			cmd.Parameters.Add(New SqlParameter("@pIdColor", pIdColor))
			cmd.Parameters.Add(New SqlParameter("@pIdProducto", pIdProducto))

			Dim lDataTable As New DataTable


			Using da As New SqlDataAdapter(cmd)
				da.Fill(lDataTable)
			End Using

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Get_IdProductoTallaColor_By_IdTalla_and_IdColor = lDataTable.Rows(0).Item("IdProductoTallaColor")
			End If


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