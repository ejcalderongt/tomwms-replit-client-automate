Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnProducto_Finca

	Public Shared Sub Cargar(ByRef oBeProducto_Finca As clsBeProducto_Finca, ByRef dr As DataRow)

		Try

			With oBeProducto_Finca

				.IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
				.Codigo = IIf(IsDBNull(dr.Item("codigo")), 0, dr.Item("codigo"))
				.Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
				.IdFinca = IIf(IsDBNull(dr.Item("IdFinca")), 0, dr.Item("IdFinca"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), New Date(1900, 1, 1), dr.Item("Fecha_Ingreso"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), New Date(1900, 1, 1), dr.Item("fec_mod"))
				.Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))

			End With

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Sub








	Public Shared Function Actualizar_Tms_Ticket_Procesado_Por_Stock_Jornada(ByVal IdTicketTMS As Integer,
																			 Optional ByVal pConection As SqlConnection = Nothing,
																			 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("tms_ticket")
			Upd.Add("procesado_stock_jornada", "@procesado_stock_jornada", DataType.Parametro)
			Upd.Add("fecha_procesado_stock_jornada", "@fecha_procesado_stock_jornada", DataType.Parametro)
			Upd.Where("IdTicket = @IdTicket")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@PROCESADO_STOCK_JORNADA", 1))
			cmd.Parameters.Add(New SqlParameter("@IDTICKET", IdTicketTMS))
			cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESADO_STOCK_JORNADA", Now))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Actualizar_Tms_Ticket_Asignado(ByRef BeTms_ticket As Integer,
														  Optional ByVal pConection As SqlConnection = Nothing,
														  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("tms_ticket")
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("fecha_asignado", "@fecha_asignado", DataType.Parametro)
			Upd.Where("IdTicket = @IdTicket")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@ESTADO", "Asignado"))
			cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))
			cmd.Parameters.Add(New SqlParameter("@FECHA_ASIGNADO", Now))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function
	Public Shared Function Eliminar(ByRef oBeTms_ticket As clsBeTms_ticket, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Tms_ticket" &
			 "  Where(IdTicket = @IdTicket)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDTICKET", oBeTms_ticket.IdTicket))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Listar() As DataTable

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = "SELECT * FROM Tms_ticket"
			lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
			Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
			Dim dad As New SqlDataAdapter(cmd)
			Dim dt As New DataTable
			dad.Fill(dt)

			lTransaction.Commit()

			Return dt

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Get_All() As List(Of clsBeTms_ticket)

		Dim lReturnList As New List(Of clsBeTms_ticket)

		Try

			Const sp As String = "SELECT * FROM Tms_ticket"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeTms_ticket As New clsBeTms_ticket

						For Each dr As DataRow In lDataTable.Rows
							vBeTms_ticket = New clsBeTms_ticket()
							Cargar(vBeTms_ticket, dr)
							lReturnList.Add(vBeTms_ticket)
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

	Public Shared Function Ticket_Procesado_Stock_Jornada(ByVal IdTicket As Integer,
														  ByVal lConnection As SqlConnection,
														  ByVal lTransaction As SqlTransaction) As Boolean

		Ticket_Procesado_Stock_Jornada = False

		Try

			Const sp As String = "SELECT * FROM Tms_ticket " &
								 " Where(IdTicket = @IdTicket AND procesado_stock_jornada =1) "

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdTicket", IdTicket)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Ticket_Procesado_Stock_Jornada = True
				End If

			End Using

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function MaxID() As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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

	Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdTicket),0) FROM Tms_ticket"

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

	Public Shared Function Get_All_For_Grid(ByVal pIdEmpresa As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

		Get_All_For_Grid = Nothing

		Try

			Dim vSQL As String = "SELECT IdTicket, 
										 Nombre_Piloto, 
									     Apellidos_Piloto, 
									     Placa_Vehiculo, 
									     Placa_TC, 
									     Empresa_Transporte, 
									     tipo_operacion, 
									     Fecha_Ingreso, 
									     Fecha_Salida,
										 Estado
									     FROM VW_TMS_Tikcet 
										 WHERE IdEmpresa = @IdEmpresa "

			vSQL += String.Format(" AND cast(Fecha_Ingreso AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

					Using lDTA As New SqlDataAdapter(vSQL, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Get_All_For_Grid = lDataTable

					End Using

					lTransaction.Commit()

				End Using

				lConnection.Close()

			End Using

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function TMS_MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer


		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdOrdenTmsEnc),0) FROM Tms_ticket_pol"

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

	Public Shared Function Actualizar_Fecha_Ingreso_Tms_Ticket(ByRef BeTms_ticket As clsBeTms_ticket,
															   Optional ByVal pConection As SqlConnection = Nothing,
															   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("tms_ticket")
			Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
			Upd.Where("IdTicket = @IdTicket")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", BeTms_ticket.Fecha_Ingreso))
			cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket.IdTicket))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function

	Public Shared Function Actualizar_Tms_Ticket_Finalizado(ByRef BeTms_ticket As Integer,
															Optional ByVal pConection As SqlConnection = Nothing,
															Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("tms_ticket")
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("fecha_finalizado", "@fecha_finalizado", DataType.Parametro)
			Upd.Where("IdTicket = @IdTicket")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@ESTADO", "Finalizado"))
			cmd.Parameters.Add(New SqlParameter("@IDTICKET", BeTms_ticket))
			cmd.Parameters.Add(New SqlParameter("@FECHA_FINALIZADO", Now))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

			Return rowsAffected

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lConnection IsNot Nothing Then lConnection.Dispose()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
		End Try

	End Function

End Class
