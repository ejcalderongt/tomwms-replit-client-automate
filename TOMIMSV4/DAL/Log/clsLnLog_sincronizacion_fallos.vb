Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnLog_sincronizacion_fallos

	Public Shared Sub Cargar(ByRef oBeLog_sincronizacion_fallos As clsBeLog_sincronizacion_fallos, ByRef dr As DataRow)
		Try
			With oBeLog_sincronizacion_fallos
				.IdLogFallo = IIf(IsDBNull(dr.Item("IdLogFallo")), 0, dr.Item("IdLogFallo"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
				.Mensaje_error = IIf(IsDBNull(dr.Item("Mensaje_error")), "", dr.Item("Mensaje_error"))
				.Fec_agr = IIf(IsDBNull(dr.Item("Fec_agr")), Date.Now, dr.Item("Fec_agr"))
				.IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeLog_sincronizacion_fallos As clsBeLog_sincronizacion_fallos, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("log_sincronizacion_fallos")
			Ins.Add("idlogfallo", "@idlogfallo", DataType.Parametro)
			Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("estado", "@estado", DataType.Parametro)
			Ins.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Ins.Add("Fec_agr", "@Fec_agr", DataType.Parametro)
			Ins.Add("IdProducto", "@IdProducto", DataType.Parametro)

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
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeLog_sincronizacion_fallos.IdProducto))

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

	Public Shared Function Actualizar(ByRef oBeLog_sincronizacion_fallos As clsBeLog_sincronizacion_fallos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("log_sincronizacion_fallos")
			Upd.Add("idlogfallo", "@idlogfallo", DataType.Parametro)
			Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("IdProducto", "@IdProducto", DataType.Parametro)
			Upd.Where("IdLogFallo = @IdLogFallo")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGFALLO", oBeLog_sincronizacion_fallos.IdLogFallo))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeLog_sincronizacion_fallos.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_sincronizacion_fallos.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_fallos.Estado))
			cmd.Parameters.Add(New SqlParameter("@MENSAJE_ERROR", oBeLog_sincronizacion_fallos.Mensaje_error))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_sincronizacion_fallos.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeLog_sincronizacion_fallos.IdProducto))

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


	Public Shared Function Eliminar(ByRef oBeLog_sincronizacion_fallos As clsBeLog_sincronizacion_fallos,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Log_sincronizacion_fallos" & _ 
			 "  Where(IdLogFallo = @IdLogFallo)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGFALLO", oBeLog_sincronizacion_fallos.IdLogFallo))

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

			Const sp As String = "SELECT * FROM Log_sincronizacion_fallos"
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

	Public Shared Function Get_All() As List(Of clsBeLog_sincronizacion_fallos)
		
		Dim lReturnList As New List(Of clsBeLog_sincronizacion_fallos)
		
		Try
		
			Const sp As String = "SELECT * FROM Log_sincronizacion_fallos"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeLog_sincronizacion_fallos As New clsBeLog_sincronizacion_fallos

						For Each dr As DataRow In lDataTable.Rows
						vBeLog_sincronizacion_fallos = New clsBeLog_sincronizacion_fallos()
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
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Sub GetSingle(ByRef pBeLog_sincronizacion_fallos As clsBeLog_sincronizacion_fallos) 
		
		Try
		
			Const sp As String = "SELECT * FROM Log_sincronizacion_fallos" & _ 
			" Where(IdLogFallo = @IdLogFallo)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeLog_sincronizacion_fallos As New clsBeLog_sincronizacion_fallos

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeLog_sincronizacion_fallos, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdLogFallo),0) FROM Log_sincronizacion_fallos"

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
