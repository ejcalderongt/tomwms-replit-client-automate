Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnI_nav_barras_rfid_det

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_det
				.IdRFIDEnc = IIf(IsDBNull(dr.Item("IdRFIDEnc")), 0, dr.Item("IdRFIDEnc"))
				.Barra_epc = IIf(IsDBNull(dr.Item("barra_epc")), "", dr.Item("barra_epc"))
				.Tagid = IIf(IsDBNull(dr.Item("tagid")), "", dr.Item("tagid"))
				.IdDispositivo = IIf(IsDBNull(dr.Item("IdDispositivo")), Date.Now, dr.Item("IdDispositivo"))
				.IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_det")
			Ins.Add("idrfidenc","@idrfidenc","F")
			Ins.Add("barra_epc","@barra_epc","F")
			Ins.Add("tagid","@tagid","F")
			Ins.Add("iddispositivo","@iddispositivo","F")
			Ins.Add("idoperador","@idoperador","F")

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_det.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_det.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_det.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeI_nav_barras_rfid_det.IdDispositivo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeI_nav_barras_rfid_det.IdOperador))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_det")
			Upd.Add("idrfidenc","@idrfidenc","F")
			Upd.Add("barra_epc","@barra_epc","F")
			Upd.Add("tagid","@tagid","F")
			Upd.Add("iddispositivo","@iddispositivo","F")
			Upd.Add("idoperador","@idoperador","F")
			Upd.Add("idoperador","@idoperador","F")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_det.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_det.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_det.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeI_nav_barras_rfid_det.IdDispositivo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeI_nav_barras_rfid_det.IdOperador))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_det"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If


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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_det)
		
		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_det)
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_det As New clsBeI_nav_barras_rfid_det

						For Each dr As DataRow In lDataTable.Rows
						vBeI_nav_barras_rfid_det = New clsBeI_nav_barras_rfid_det()
						Cargar(vBeI_nav_barras_rfid_det, dr)
						lReturnList.Add(vBeI_nav_barras_rfid_det)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det) 
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_det As New clsBeI_nav_barras_rfid_det

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeI_nav_barras_rfid_det, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det"

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
