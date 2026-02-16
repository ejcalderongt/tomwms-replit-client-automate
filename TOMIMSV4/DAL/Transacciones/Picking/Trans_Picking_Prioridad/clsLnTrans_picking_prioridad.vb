Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnTrans_picking_prioridad

	Public Shared Sub Cargar(ByRef oBeTrans_picking_prioridad As clsBeTrans_picking_prioridad, ByRef dr As DataRow)
		Try
			With oBeTrans_picking_prioridad
				.IdPrioridadPicking = IIf(IsDBNull(dr.Item("IdPrioridadPicking")), 0, dr.Item("IdPrioridadPicking"))
				.Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
				.Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeTrans_picking_prioridad As clsBeTrans_picking_prioridad, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("trans_picking_prioridad")
			Ins.Add("idprioridadpicking", "@idprioridadpicking", DataType.Parametro)
			Ins.Add("codigo", "@codigo", DataType.Parametro)
			Ins.Add("nombre", "@nombre", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("user_mod", "@user_mod", DataType.Parametro)
			Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
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

			cmd.Parameters.Add(New SqlParameter("@IDPRIORIDADPICKING", oBeTrans_picking_prioridad.IdPrioridadPicking))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_picking_prioridad.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_picking_prioridad.Nombre))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_prioridad.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_prioridad.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_prioridad.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_prioridad.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_prioridad.Activo))

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

	Public Shared Function Actualizar(ByRef oBeTrans_picking_prioridad As clsBeTrans_picking_prioridad, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_picking_prioridad")
			Upd.Add("idprioridadpicking", "@idprioridadpicking", DataType.Parametro)
			Upd.Add("codigo", "@codigo", DataType.Parametro)
			Upd.Add("nombre", "@nombre", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Add("activo", "@activo", DataType.Parametro)
			Upd.Where("IdPrioridadPicking = @IdPrioridadPicking")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPRIORIDADPICKING", oBeTrans_picking_prioridad.IdPrioridadPicking))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_picking_prioridad.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_picking_prioridad.Nombre))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_prioridad.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_prioridad.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_prioridad.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_prioridad.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_prioridad.Activo))

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


	Public Shared Function Eliminar(ByRef oBeTrans_picking_prioridad As clsBeTrans_picking_prioridad,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_picking_prioridad" & _ 
			 "  Where(IdPrioridadPicking = @IdPrioridadPicking)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPRIORIDADPICKING", oBeTrans_picking_prioridad.IdPrioridadPicking))

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

			Const sp As String = "SELECT * FROM Trans_picking_prioridad"
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

	Public Shared Function Get_All() As List(Of clsBeTrans_picking_prioridad)
		
		Dim lReturnList As New List(Of clsBeTrans_picking_prioridad)
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_picking_prioridad"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_picking_prioridad As New clsBeTrans_picking_prioridad

						For Each dr As DataRow In lDataTable.Rows
						vBeTrans_picking_prioridad = New clsBeTrans_picking_prioridad()
						Cargar(vBeTrans_picking_prioridad, dr)
						lReturnList.Add(vBeTrans_picking_prioridad)
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

	Public Shared Sub GetSingle(ByRef pBeTrans_picking_prioridad As clsBeTrans_picking_prioridad) 
		
		Try

			Const sp As String = "SELECT * FROM Trans_picking_prioridad Where(IdPrioridadPicking = @IdPrioridadPicking)"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdPrioridadPicking", pBeTrans_picking_prioridad.IdPrioridadPicking)

						Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_picking_prioridad As New clsBeTrans_picking_prioridad

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeTrans_picking_prioridad, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdPrioridadPicking),0) FROM Trans_picking_prioridad"

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

	Public Shared Function GetSingle_By_IdPickingPrioridad(ByVal pIdPickingPrioridad As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_prioridad

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Dim lDTA As New SqlDataAdapter
		GetSingle_By_IdPickingPrioridad = Nothing

		Try

			Const sp As String = "SELECT * FROM Trans_picking_prioridad Where(IdPrioridadPicking = @pIdPickingPrioridad)"

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				lDTA = New SqlDataAdapter(sp, pConection)
				lDTA.SelectCommand.Transaction = pTransaction
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lDTA = New SqlDataAdapter(sp, lConnection)
			End If

			lDTA.SelectCommand.CommandType = CommandType.Text
			lDTA.SelectCommand.Parameters.AddWithValue("@pIdPickingPrioridad", pIdPickingPrioridad)

			Dim lDataTable As New DataTable
			lDTA.Fill(lDataTable)

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Dim vBeTrans_picking_prioridad As New clsBeTrans_picking_prioridad

				Cargar(vBeTrans_picking_prioridad, lDataTable.Rows(0))
				GetSingle_By_IdPickingPrioridad = vBeTrans_picking_prioridad
			End If

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

End Class
