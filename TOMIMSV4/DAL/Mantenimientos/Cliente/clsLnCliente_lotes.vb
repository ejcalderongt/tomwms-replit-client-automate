Imports System.Data.SqlClient
Imports System.Reflection
Imports System
Imports TOMWMS.WMSTipoDato

Public Class clsLnCliente_lotes

	Public Shared Sub Cargar(ByRef oBeCliente_lotes As clsBeCliente_lotes, ByRef dr As DataRow)
		Try
			With oBeCliente_lotes
				.IdClienteLote = IIf(IsDBNull(dr.Item("IdClienteLote")), 0, dr.Item("IdClienteLote"))
				.IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
				.Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
				.IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
				.IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
				.Bloquear = IIf(IsDBNull(dr.Item("bloquear")), False, dr.Item("bloquear"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeCliente_lotes As clsBeCliente_lotes, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("cliente_lotes")
			Ins.Add("idclientelote", "@idclientelote", Tipo.Parametro)
			Ins.Add("idcliente", "@idcliente", Tipo.Parametro)
			Ins.Add("lote", "@lote", Tipo.Parametro)
			Ins.Add("idproductoestado", "@idproductoestado", Tipo.Parametro)
			Ins.Add("user_agr", "@user_agr", Tipo.Parametro)
			Ins.Add("fec_agr", "@fec_agr", Tipo.Parametro)
			Ins.Add("user_mod", "@user_mod", Tipo.Parametro)
			Ins.Add("fec_mod", "@fec_mod", Tipo.Parametro)
			Ins.Add("activo", "@activo", Tipo.Parametro)
			Ins.Add("bloquear", "@bloquear", Tipo.Parametro)
			Ins.Add("idproducto", "@idproducto", Tipo.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDCLIENTELOTE", oBeCliente_lotes.IdClienteLote))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_lotes.IdCliente))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeCliente_lotes.Lote))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeCliente_lotes.IdProductoEstado))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeCliente_lotes.IdProducto))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_lotes.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_lotes.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_lotes.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_lotes.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_lotes.Activo))
			cmd.Parameters.Add(New SqlParameter("@BLOQUEAR", oBeCliente_lotes.Bloquear))

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

	Public Shared Function Actualizar(ByRef oBeCliente_lotes As clsBeCliente_lotes, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("cliente_lotes")
			Upd.Add("idclientelote", "@idclientelote", Tipo.Parametro)
			Upd.Add("idcliente", "@idcliente", Tipo.Parametro)
			Upd.Add("lote", "@lote", Tipo.Parametro)
			Upd.Add("idproductoestado", "@idproductoestado", Tipo.Parametro)
			Upd.Add("idproducto", "@idproducto", Tipo.Parametro)
			Upd.Add("user_agr", "@user_agr", Tipo.Parametro)
			Upd.Add("fec_agr", "@fec_agr", Tipo.Parametro)
			Upd.Add("user_mod", "@user_mod", Tipo.Parametro)
			Upd.Add("fec_mod", "@fec_mod", Tipo.Parametro)
			Upd.Add("activo", "@activo", Tipo.Parametro)
			Upd.Add("bloquear", "@bloquear", Tipo.Parametro)
			Upd.Where("IdClienteLote = @IdClienteLote")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDCLIENTELOTE", oBeCliente_lotes.IdClienteLote))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_lotes.IdCliente))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeCliente_lotes.Lote))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeCliente_lotes.IdProductoEstado))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeCliente_lotes.IdProducto))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_lotes.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_lotes.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_lotes.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_lotes.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_lotes.Activo))
			cmd.Parameters.Add(New SqlParameter("@BLOQUEAR", oBeCliente_lotes.Bloquear))

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


	Public Shared Function Eliminar(ByVal IdClienteLote As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Cliente_lotes" &
			 "  Where(IdClienteLote = @IdClienteLote)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDCLIENTELOTE", IdClienteLote))

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

	Public Shared Function Get_All_By_IdCliente(ByVal IdCliente As Integer) As List(Of clsBeCliente_lotes)

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "SELECT * FROM Cliente_lotes"
			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
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
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_All() As List(Of clsBeCliente_lotes)

		Dim lReturnList As New List(Of clsBeCliente_lotes)

		Try

			Const sp As String = "SELECT * FROM Cliente_lotes WHERE IdCliente = @IdCliente "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeCliente_lotes As New clsBeCliente_lotes

						For Each dr As DataRow In lDataTable.Rows
							vBeCliente_lotes = New clsBeCliente_lotes()
							Cargar(vBeCliente_lotes, dr)
							lReturnList.Add(vBeCliente_lotes)
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

	Public Shared Sub GetSingle(ByRef pBeCliente_lotes As clsBeCliente_lotes)

		Try

			Const sp As String = "SELECT * FROM Cliente_lotes" &
			" Where(IdClienteLote = @IdClienteLote)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeCliente_lotes As New clsBeCliente_lotes

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeCliente_lotes, lDataTable.Rows(0))
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

			Const sp As String = "SELECT ISNULL(Max(IdClienteLote),0) FROM Cliente_lotes"

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

	Public Shared Function Get_Lotes_By_IdCliente(IdCliente As Integer, bloqueados As Boolean) As DataTable

		Get_Lotes_By_IdCliente = Nothing

		Dim lDataTable As New DataTable

		Try
			Dim vSQL As String = "
            SELECT
				cl.IdClienteLote,	
				p.codigo,
				p.nombre,
				cl.Lote, 
				pe.nombre AS Estado, 
				cl.fec_agr AS Fecha, 
				usu.nombres AS Usuario
			FROM cliente_lotes cl
			JOIN producto_estado pe ON cl.IdProductoEstado = pe.IdEstado
			JOIN usuario usu ON cl.user_agr = usu.IdUsuario
			JOIN producto p on p.IdProducto = cl.idproducto			
            WHERE cl.bloquear = @Bloquear AND cl.IdCliente = @IdCliente 
			ORDER BY cl.fec_agr DESC"

			Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
					Using lDTA As New SqlDataAdapter(vSQL, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)
						lDTA.SelectCommand.Parameters.AddWithValue("@Bloquear", If(bloqueados, 1, 0))

						lDTA.Fill(lDataTable)

					End Using

					lTransaction.Commit()
				End Using

				lConnection.Close()
			End Using

			Return lDataTable

		Catch ex As Exception
			Throw ex
		End Try

	End Function
	Public Shared Function GetSingle_By_IdClienteLote(ByVal IdClienteLote As Integer) As clsBeCliente_lotes

		Dim vBeCliente_lotes As New clsBeCliente_lotes

		Try
			Const sp As String = "
            SELECT * 
            FROM Cliente_lotes 
            WHERE IdClienteLote = @IdClienteLote"

			Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdClienteLote", IdClienteLote)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeCliente_lotes, lDataTable.Rows(0))
						End If

					End Using

					lTransaction.Commit()
				End Using

				lConnection.Close()
			End Using

			Return vBeCliente_lotes

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name, ex.Message))
		End Try

	End Function

	Public Shared Function Get_All_By_IdCliente(ByVal IdCliente As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeCliente_lotes)

		Dim lReturnList As New List(Of clsBeCliente_lotes)

		Try

			Const sp As String = "SELECT * FROM Cliente_lotes WHERE IdCliente = @IdCliente "

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim vBeCliente_lotes As New clsBeCliente_lotes

				For Each dr As DataRow In lDataTable.Rows
					vBeCliente_lotes = New clsBeCliente_lotes()
					Cargar(vBeCliente_lotes, dr)
					lReturnList.Add(vBeCliente_lotes)
				Next

			End Using

			Return lReturnList

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

End Class