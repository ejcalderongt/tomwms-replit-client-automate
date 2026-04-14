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
			Ins.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Ins.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Ins.Add("tagid", "@tagid", DataType.Parametro)
			Ins.Add("iddispositivo", "@iddispositivo", DataType.Parametro)
			Ins.Add("idoperador", "@idoperador", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_det.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_det.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_det.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeI_nav_barras_rfid_det.IdDispositivo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeI_nav_barras_rfid_det.IdOperador))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_det")
			Upd.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Upd.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Upd.Add("tagid", "@tagid", DataType.Parametro)
			Upd.Add("iddispositivo", "@iddispositivo", DataType.Parametro)
			Upd.Add("idoperador", "@idoperador", DataType.Parametro)
			Upd.Add("idoperador", "@idoperador", DataType.Parametro)

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det where ( IdRFIDEnc= @pIdRFIDEnc and barra_epc=@pBarra_epc)"


			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@pIdRFIDEnc", pBeI_nav_barras_rfid_det.IdRFIDEnc)
						lDTA.SelectCommand.Parameters.AddWithValue("@pBarra_epc", pBeI_nav_barras_rfid_det.Barra_epc)
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

	Public Shared Function Get_All_By_IdRFIDEnc(ByVal pIdRFIDEnc As Integer) As List(Of clsBeI_nav_barras_rfid_det)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_det)

		Try

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det where IdRFIDEnc=@IdRFIDEnc "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.Parameters.AddWithValue("@IdRFIDEnc", pIdRFIDEnc)

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
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function Get_All_By_IdRFIDEnc(ByVal pIdRFIDEnc As Integer,
											ByVal lConnection As SqlConnection,
											ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_barras_rfid_det)

		Get_All_By_IdRFIDEnc = Nothing

		Try

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_det where IdRFIDEnc=@IdRFIDEnc "

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@IdRFIDEnc", pIdRFIDEnc)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_det)
				Dim vBeI_nav_barras_rfid_det As New clsBeI_nav_barras_rfid_det

				For Each dr As DataRow In lDataTable.Rows
					vBeI_nav_barras_rfid_det = New clsBeI_nav_barras_rfid_det()
					Cargar(vBeI_nav_barras_rfid_det, dr)

					vBeI_nav_barras_rfid_det.Producto = New clsBeProducto
					Dim vCodigoProducto = clsLnI_nav_barras_pallet.Get_CodigoProducto_By_Barra_EPC(vBeI_nav_barras_rfid_det.Barra_epc, lConnection, lTransaction)

					vBeI_nav_barras_rfid_det.Operador = New clsBeOperador
					vBeI_nav_barras_rfid_det.Operador = clsLnOperador.Get_Single_By_IdOperador(vBeI_nav_barras_rfid_det.IdOperador, lConnection, lTransaction)


					If String.IsNullOrWhiteSpace(vCodigoProducto) Then
						Throw New ApplicationException(String.Format("GT08042026: No existe código de producto para la barra EPC: {0}", vBeI_nav_barras_rfid_det.Barra_epc))
					End If

					vBeI_nav_barras_rfid_det.Producto = clsLnProducto.Get_Single_By_Codigo(vCodigoProducto, lConnection, lTransaction)
					lReturnList.Add(vBeI_nav_barras_rfid_det)

				Next

				Get_All_By_IdRFIDEnc = lReturnList

			End Using

		Catch ex As Exception
			Throw ex
		End Try

	End Function

	'#GT23032026: retorna true si inserta el detalle, para que actualice también barra_pallet con el mismo EPC (Codigo_barra).
	Public Shared Function Insertar_Detalle_RFID(ByRef oBeI_nav_barras_rfid_det As clsBeI_nav_barras_rfid_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Insertar_Detalle_RFID = False

		Try

			Ins.Init("i_nav_barras_rfid_det")
			Ins.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Ins.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Ins.Add("tagid", "@tagid", DataType.Parametro)
			Ins.Add("iddispositivo", "@iddispositivo", DataType.Parametro)
			Ins.Add("idoperador", "@idoperador", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_det.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_det.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_det.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDDISPOSITIVO", oBeI_nav_barras_rfid_det.IdDispositivo))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeI_nav_barras_rfid_det.IdOperador))

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
			Insertar_Detalle_RFID = (rowsAffected > 0)

			cmd.Dispose()

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try

	End Function

	'#GT26032026: valida si ya existe el tag en un registro previo (sea ingreso o salida).

	Public Shared Function Exist_By_EPC(ByVal pCodigo_barra As String,
										ByVal pTipo As String,
										ByVal lConnection As SqlConnection,
										ByVal lTransaction As SqlTransaction) As Boolean

		Exist_By_EPC = False

		Try

			Const sp As String = "SELECT TOP 1 1 " &
								 "FROM I_nav_barras_rfid_det d " &
								 "INNER JOIN I_nav_barras_rfid_enc e ON e.IdRFIDEnc = d.IdRFIDEnc " &
								 "WHERE d.barra_epc = @pCodigo_barra " &
								 "AND e.Tipo = @pTipo"

			Using lDTA As New SqlDataAdapter(sp, lConnection)

				lDTA.SelectCommand.CommandType = CommandType.Text
				lDTA.SelectCommand.Transaction = lTransaction
				lDTA.SelectCommand.Parameters.AddWithValue("@pCodigo_barra", pCodigo_barra)
				lDTA.SelectCommand.Parameters.AddWithValue("@pTipo", pTipo)

				Dim lDataTable As New DataTable
				lDTA.Fill(lDataTable)

				If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
					Exist_By_EPC = True
				End If

			End Using

		Catch ex As Exception
			Throw
		End Try

	End Function


End Class
