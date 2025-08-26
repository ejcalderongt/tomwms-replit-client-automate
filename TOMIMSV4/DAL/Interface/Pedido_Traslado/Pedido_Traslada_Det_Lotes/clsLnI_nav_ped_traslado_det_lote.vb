Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ped_traslado_det_lote

	Public Shared Sub Cargar(ByRef oBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote, ByRef dr As DataRow)
		Try
			With oBeI_nav_ped_traslado_det_lote
				.NoEnc = IIf(IsDBNull(dr.Item("NoEnc")), "", dr.Item("NoEnc"))
				.Line_No = IIf(IsDBNull(dr.Item("Line_No")), 0, dr.Item("Line_No"))
				.No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
				.Batch_No = IIf(IsDBNull(dr.Item("Batch_No")), "", dr.Item("Batch_No"))
				.Serial_No = IIf(IsDBNull(dr.Item("Serial_No")), "", dr.Item("Serial_No"))
				.Expiration_Date = IIf(IsDBNull(dr.Item("Expiration_Date")), Date.Now, dr.Item("Expiration_Date"))
				.Quantity_Base = IIf(IsDBNull(dr.Item("Quantity_Base")), 0.0, dr.Item("Quantity_Base"))
				.Variant_Code = IIf(IsDBNull(dr.Item("Variant_Code")), "", dr.Item("Variant_Code"))
				.WhsFrom = IIf(IsDBNull(dr.Item("WhsFrom")), "", dr.Item("WhsFrom"))
				.WhsTo = IIf(IsDBNull(dr.Item("WhsTo")), "", dr.Item("WhsTo"))
				.Fec_Agr = IIf(IsDBNull(dr.Item("Fec_Agr")), Date.Now, dr.Item("Fec_Agr"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_ped_traslado_det_lote")
			Ins.Add("noenc", "@noenc", DataType.Parametro)
			Ins.Add("line_no", "@line_no", DataType.Parametro)
			Ins.Add("no", "@no", DataType.Parametro)
			Ins.Add("batch_no", "@batch_no", DataType.Parametro)
			Ins.Add("serial_no", "@serial_no", DataType.Parametro)
			Ins.Add("expiration_date", "@expiration_date", DataType.Parametro)
			Ins.Add("quantity_base", "@quantity_base", DataType.Parametro)
			Ins.Add("variant_code", "@variant_code", DataType.Parametro)
			Ins.Add("whsfrom", "@whsfrom", DataType.Parametro)
			Ins.Add("whsto", "@whsto", DataType.Parametro)
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

			cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det_lote.NoEnc))
			cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det_lote.Line_No))
			cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det_lote.No))
			cmd.Parameters.Add(New SqlParameter("@BATCH_NO", oBeI_nav_ped_traslado_det_lote.Batch_No))
			cmd.Parameters.Add(New SqlParameter("@SERIAL_NO", oBeI_nav_ped_traslado_det_lote.Serial_No))
			cmd.Parameters.Add(New SqlParameter("@EXPIRATION_DATE", oBeI_nav_ped_traslado_det_lote.Expiration_Date))
			cmd.Parameters.Add(New SqlParameter("@QUANTITY_BASE", oBeI_nav_ped_traslado_det_lote.Quantity_Base))
			cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeI_nav_ped_traslado_det_lote.Variant_Code))
			cmd.Parameters.Add(New SqlParameter("@WHSFROM", oBeI_nav_ped_traslado_det_lote.WhsFrom))
			cmd.Parameters.Add(New SqlParameter("@WHSTO", oBeI_nav_ped_traslado_det_lote.WhsTo))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_ped_traslado_det_lote.Fec_Agr))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_ped_traslado_det_lote")
			Upd.Add("noenc", "@noenc", DataType.Parametro)
			Upd.Add("line_no", "@line_no", DataType.Parametro)
			Upd.Add("no", "@no", DataType.Parametro)
			Upd.Add("batch_no", "@batch_no", DataType.Parametro)
			Upd.Add("serial_no", "@serial_no", DataType.Parametro)
			Upd.Add("expiration_date", "@expiration_date", DataType.Parametro)
			Upd.Add("quantity_base", "@quantity_base", DataType.Parametro)
			Upd.Add("variant_code", "@variant_code", DataType.Parametro)
			Upd.Add("whsfrom", "@whsfrom", DataType.Parametro)
			Upd.Add("whsto", "@whsto", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Where("NoEnc = @NoEnc" &
				" AND Line_No = @Line_No" &
				" AND No = @No" &
				" AND Batch_No = @Batch_No")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det_lote.NoEnc))
			cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det_lote.Line_No))
			cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det_lote.No))
			cmd.Parameters.Add(New SqlParameter("@BATCH_NO", oBeI_nav_ped_traslado_det_lote.Batch_No))
			cmd.Parameters.Add(New SqlParameter("@SERIAL_NO", oBeI_nav_ped_traslado_det_lote.Serial_No))
			cmd.Parameters.Add(New SqlParameter("@EXPIRATION_DATE", oBeI_nav_ped_traslado_det_lote.Expiration_Date))
			cmd.Parameters.Add(New SqlParameter("@QUANTITY_BASE", oBeI_nav_ped_traslado_det_lote.Quantity_Base))
			cmd.Parameters.Add(New SqlParameter("@VARIANT_CODE", oBeI_nav_ped_traslado_det_lote.Variant_Code))
			cmd.Parameters.Add(New SqlParameter("@WHSFROM", oBeI_nav_ped_traslado_det_lote.WhsFrom))
			cmd.Parameters.Add(New SqlParameter("@WHSTO", oBeI_nav_ped_traslado_det_lote.WhsTo))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_ped_traslado_det_lote.Fec_Agr))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_ped_traslado_det_lote" &
			 "  Where(NoEnc = @NoEnc)" &
			 "  AND (Line_No = @Line_No)" &
			 "  AND (No = @No)" &
			 "  AND (Batch_No = @Batch_No)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det_lote.NoEnc))
			cmd.Parameters.Add(New SqlParameter("@LINE_NO", oBeI_nav_ped_traslado_det_lote.Line_No))
			cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det_lote.No))
			cmd.Parameters.Add(New SqlParameter("@BATCH_NO", oBeI_nav_ped_traslado_det_lote.Batch_No))

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

			Const sp As String = "SELECT * FROM I_nav_ped_traslado_det_lote"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_ped_traslado_det_lote)

		Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_det_lote)

		Try

			Const sp As String = "SELECT * FROM I_nav_ped_traslado_det_lote"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_ped_traslado_det_lote As New clsBeI_nav_ped_traslado_det_lote

						For Each dr As DataRow In lDataTable.Rows
							vBeI_nav_ped_traslado_det_lote = New clsBeI_nav_ped_traslado_det_lote()
							Cargar(vBeI_nav_ped_traslado_det_lote, dr)
							lReturnList.Add(vBeI_nav_ped_traslado_det_lote)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote)

		Try

			Const sp As String = "SELECT * FROM I_nav_ped_traslado_det_lote" &
			" Where(NoEnc = @NoEnc)" &
			" AND (Line_No = @Line_No)" &
			" AND (No = @No)" &
			" AND (Batch_No = @Batch_No)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_ped_traslado_det_lote As New clsBeI_nav_ped_traslado_det_lote

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeI_nav_ped_traslado_det_lote, lDataTable.Rows(0))
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

			Const sp As String = "SELECT ISNULL(Max(NoEnc),0) FROM I_nav_ped_traslado_det_lote"

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

End Class