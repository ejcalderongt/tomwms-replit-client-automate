Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnI_nav_barras_rfid_tipo_mov

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_tipo_mov As clsBeI_nav_barras_rfid_tipo_mov, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_tipo_mov
				.IdRfidTipoMov = IIf(IsDBNull(dr.Item("IdRfidTipoMov")), 0, dr.Item("IdRfidTipoMov"))
				.Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
				.Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
				.Signo = IIf(IsDBNull(dr.Item("signo")), 0, dr.Item("signo"))
				.Afecta_stock = IIf(IsDBNull(dr.Item("afecta_stock")), False, dr.Item("afecta_stock"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), 0, dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), 0, dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_tipo_mov As clsBeI_nav_barras_rfid_tipo_mov, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_tipo_mov")
			Ins.Add("idrfidtipomov", "@idrfidtipomov", DataType.Parametro)
			Ins.Add("codigo", "@codigo", DataType.Parametro)
			Ins.Add("nombre", "@nombre", DataType.Parametro)
			Ins.Add("signo", "@signo", DataType.Parametro)
			Ins.Add("afecta_stock", "@afecta_stock", DataType.Parametro)
			Ins.Add("activo", "@activo", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("user_mod", "@user_mod", DataType.Parametro)
			Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDTIPOMOV", oBeI_nav_barras_rfid_tipo_mov.IdRfidTipoMov))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeI_nav_barras_rfid_tipo_mov.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeI_nav_barras_rfid_tipo_mov.Nombre))
			cmd.Parameters.Add(New SqlParameter("@SIGNO", oBeI_nav_barras_rfid_tipo_mov.Signo))
			cmd.Parameters.Add(New SqlParameter("@AFECTA_STOCK", oBeI_nav_barras_rfid_tipo_mov.Afecta_stock))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_rfid_tipo_mov.Activo))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_tipo_mov.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_tipo_mov.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_barras_rfid_tipo_mov.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_tipo_mov.Fec_mod))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_tipo_mov As clsBeI_nav_barras_rfid_tipo_mov, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_tipo_mov")
			Upd.Add("idrfidtipomov", "@idrfidtipomov", DataType.Parametro)
			Upd.Add("codigo", "@codigo", DataType.Parametro)
			Upd.Add("nombre", "@nombre", DataType.Parametro)
			Upd.Add("signo", "@signo", DataType.Parametro)
			Upd.Add("afecta_stock", "@afecta_stock", DataType.Parametro)
			Upd.Add("activo", "@activo", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Where("IdRfidTipoMov = @IdRfidTipoMov")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDTIPOMOV", oBeI_nav_barras_rfid_tipo_mov.IdRfidTipoMov))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeI_nav_barras_rfid_tipo_mov.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeI_nav_barras_rfid_tipo_mov.Nombre))
			cmd.Parameters.Add(New SqlParameter("@SIGNO", oBeI_nav_barras_rfid_tipo_mov.Signo))
			cmd.Parameters.Add(New SqlParameter("@AFECTA_STOCK", oBeI_nav_barras_rfid_tipo_mov.Afecta_stock))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_rfid_tipo_mov.Activo))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_tipo_mov.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_tipo_mov.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_barras_rfid_tipo_mov.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_tipo_mov.Fec_mod))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_tipo_mov As clsBeI_nav_barras_rfid_tipo_mov,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_tipo_mov" & _ 
			 "  Where(IdRfidTipoMov = @IdRfidTipoMov)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDTIPOMOV", oBeI_nav_barras_rfid_tipo_mov.IdRfidTipoMov))

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_tipo_mov"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_tipo_mov)
		
		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_tipo_mov)
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_tipo_mov"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_tipo_mov As New clsBeI_nav_barras_rfid_tipo_mov

						For Each dr As DataRow In lDataTable.Rows
						vBeI_nav_barras_rfid_tipo_mov = New clsBeI_nav_barras_rfid_tipo_mov()
						Cargar(vBeI_nav_barras_rfid_tipo_mov, dr)
						lReturnList.Add(vBeI_nav_barras_rfid_tipo_mov)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_tipo_mov As clsBeI_nav_barras_rfid_tipo_mov) 
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_tipo_mov" & _ 
			" Where(IdRfidTipoMov = @IdRfidTipoMov)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_tipo_mov As New clsBeI_nav_barras_rfid_tipo_mov

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeI_nav_barras_rfid_tipo_mov, lDataTable.Rows(0))
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

	Public Shared Function MaxID() as Integer 
		
		Try
		
			Dim lMax As Integer = 0
		
			Const sp As String = "SELECT ISNULL(Max(IdRfidTipoMov),0) FROM I_nav_barras_rfid_tipo_mov"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

				Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType=CommandType.Text}

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
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

End Class
