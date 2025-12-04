Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnVerificacion_estado

	Public Shared Sub Cargar(ByRef oBeVerificacion_estado As clsBeVerificacion_estado, ByRef dr As DataRow)
		Try
			With oBeVerificacion_estado
				.IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
				.Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeVerificacion_estado As clsBeVerificacion_estado, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("verificacion_estado")
			Ins.Add("idestado", "@idestado", DataType.Parametro)
			Ins.Add("descripcion", "@descripcion", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("activo", "@activo", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeVerificacion_estado.IdEstado))
			cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeVerificacion_estado.Descripcion))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeVerificacion_estado.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeVerificacion_estado.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeVerificacion_estado.Activo))

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

	Public Shared Function Actualizar(ByRef oBeVerificacion_estado As clsBeVerificacion_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("verificacion_estado")
			Upd.Add("idestado", "@idestado", DataType.Parametro)
			Upd.Add("descripcion", "@descripcion", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("activo", "@activo", DataType.Parametro)
			Upd.Where("IdEstado = @IdEstado")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeVerificacion_estado.IdEstado))
			cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeVerificacion_estado.Descripcion))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeVerificacion_estado.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeVerificacion_estado.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeVerificacion_estado.Activo))

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


	Public Shared Function Eliminar(ByRef oBeVerificacion_estado As clsBeVerificacion_estado,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Verificacion_estado" & _ 
			 "  Where(IdEstado = @IdEstado)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeVerificacion_estado.IdEstado))

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

			Const sp As String = "SELECT * FROM Verificacion_estado"
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

	Public Shared Function Get_All() As List(Of clsBeVerificacion_estado)
		
		Dim lReturnList As New List(Of clsBeVerificacion_estado)
		
		Try
		
			Const sp As String = "SELECT * FROM Verificacion_estado"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeVerificacion_estado As New clsBeVerificacion_estado

						For Each dr As DataRow In lDataTable.Rows
						vBeVerificacion_estado = New clsBeVerificacion_estado()
						Cargar(vBeVerificacion_estado, dr)
						lReturnList.Add(vBeVerificacion_estado)
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

	Public Shared Sub GetSingle(ByRef pBeVerificacion_estado As clsBeVerificacion_estado) 
		
		Try
		
			Const sp As String = "SELECT * FROM Verificacion_estado" & _ 
			" Where(IdEstado = @IdEstado)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeVerificacion_estado As New clsBeVerificacion_estado

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeVerificacion_estado, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdEstado),0) FROM Verificacion_estado"

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
