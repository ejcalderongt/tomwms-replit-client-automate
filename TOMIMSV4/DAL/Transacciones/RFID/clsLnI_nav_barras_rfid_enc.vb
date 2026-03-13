Imports System.Data.SqlClient
Imports System.Reflection
Imports System
Imports DevExpress.Xpo.Helpers

Public Class clsLnI_nav_barras_rfid_enc

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_enc
				.IdRFIDEnc = IIf(IsDBNull(dr.Item("IdRFIDEnc")), 0, dr.Item("IdRFIDEnc"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
				.Tipo = IIf(IsDBNull(dr.Item("Tipo")), "", dr.Item("Tipo"))
				.IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
				.IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_enc")
			Ins.Add("idrfidenc", "@idrfidenc", "F")
			Ins.Add("idordencompraenc", "@idordencompraenc", "F")
			Ins.Add("idrecepcionenc", "@idrecepcionenc", "F")
			Ins.Add("idbodega", "@idbodega", "F")
			Ins.Add("fec_agr", "@fec_agr", "F")
			Ins.Add("fec_mod", "@fec_mod", "F")
			Ins.Add("estado", "@estado", "F")
			Ins.Add("tipo", "@tipo", "F")
			Ins.Add("idproveedor", "@idproveedor", "F")
			Ins.Add("idcliente", "@idcliente", "F")

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_enc.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_barras_rfid_enc.IdRecepcionEnc))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_enc.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_enc.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_enc.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_barras_rfid_enc.Estado))
			cmd.Parameters.Add(New SqlParameter("@TIPO", oBeI_nav_barras_rfid_enc.Tipo))
			cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeI_nav_barras_rfid_enc.IdProveedor))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_barras_rfid_enc.IdCliente))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_enc")
			Upd.Add("idrfidenc", "@idrfidenc", "F")
			Upd.Add("idordencompraenc", "@idordencompraenc", "F")
			Upd.Add("idrecepcionenc", "@idrecepcionenc", "F")
			Upd.Add("idbodega", "@idbodega", "F")
			Upd.Add("fec_agr", "@fec_agr", "F")
			Upd.Add("fec_mod", "@fec_mod", "F")
			Upd.Add("estado", "@estado", "F")
			Upd.Add("tipo", "@tipo", "F")
			Upd.Add("idproveedor", "@idproveedor", "F")
			Upd.Add("idcliente", "@idcliente", "F")
			Upd.Where("IdRFIDEnc = @IdRFIDEnc")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_enc.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeI_nav_barras_rfid_enc.IdRecepcionEnc))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_enc.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_enc.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_enc.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_barras_rfid_enc.Estado))
			cmd.Parameters.Add(New SqlParameter("@TIPO", oBeI_nav_barras_rfid_enc.Tipo))
			cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeI_nav_barras_rfid_enc.IdProveedor))
			cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_barras_rfid_enc.IdCliente))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_enc" &
			 "  Where(IdRFIDEnc = @IdRFIDEnc)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_enc.IdRFIDEnc))

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_enc"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_enc"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						For Each dr As DataRow In lDataTable.Rows
							vBeI_nav_barras_rfid_enc = New clsBeI_nav_barras_rfid_enc()
							Cargar(vBeI_nav_barras_rfid_enc, dr)
							lReturnList.Add(vBeI_nav_barras_rfid_enc)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_enc As clsBeI_nav_barras_rfid_enc)
		Try

			Dim sp As String = "SELECT * FROM I_nav_barras_rfid_enc WHERE (IdRFIDEnc = @IdRFIDEnc)"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter()

						lDTA.SelectCommand = New SqlCommand()
						lDTA.SelectCommand.Connection = lConnection
						lDTA.SelectCommand.Transaction = lTransaction
						lDTA.SelectCommand.CommandType = CommandType.Text

						lDTA.SelectCommand.Parameters.AddWithValue("@IdRFIDEnc", pBeI_nav_barras_rfid_enc.IdRFIDEnc)

						If pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdPedidoEnc <= 0 Then
							sp &= " AND (IdOrdenCompraEnc = @IdOrdenCompraEnc)"
							lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc)

						ElseIf pBeI_nav_barras_rfid_enc.IdPedidoEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc <= 0 Then
							sp &= " AND (IdPedidoEnc = @IdPedidoEnc)"
							lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pBeI_nav_barras_rfid_enc.IdPedidoEnc)

						ElseIf pBeI_nav_barras_rfid_enc.IdOrdenCompraEnc > 0 AndAlso pBeI_nav_barras_rfid_enc.IdPedidoEnc > 0 Then
							Throw New Exception("IdOrdenCompraEnc e IdPedidoEnc no pueden enviarse al mismo tiempo.")
						End If

						lDTA.SelectCommand.CommandText = sp

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeI_nav_barras_rfid_enc, lDataTable.Rows(0))

							pBeI_nav_barras_rfid_enc = vBeI_nav_barras_rfid_enc
							pBeI_nav_barras_rfid_enc.Detalle = New List(Of clsBeI_nav_barras_rfid_det)
							pBeI_nav_barras_rfid_enc.Detalle = clsLnI_nav_barras_rfid_det.Get_All_By_IdRFIDEnc(pBeI_nav_barras_rfid_enc.IdRFIDEnc, lConnection, lTransaction)

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

	Public Shared Function MaxID() as Integer 
		
		Try
		
			Dim lMax As Integer = 0
		
			Const sp As String = "SELECT ISNULL(Max(IdRFIDEnc),0) FROM I_nav_barras_rfid_enc"

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


	Public Shared Function Get_All_Ingresos() As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			Const sp As String = "SELECT IdRFIDEnc as Correlativo,IdOrdenCompraEnc IdIngreso,IdRecepcionEnc,IdBodega,fec_agr,fec_mod,Estado,Tipo,IdProveedor,IdCliente,IdPedidoEnc IdSalida
										 FROM I_nav_barras_rfid_enc where ISNULL(IdOrdenCompraEnc, 0) > 0 "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						'lDTA.SelectCommand.Parameters.AddWithValue("@NombreParametro", valor)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						For Each dr As DataRow In lDataTable.Rows
							vBeI_nav_barras_rfid_enc = New clsBeI_nav_barras_rfid_enc()
							Cargar(vBeI_nav_barras_rfid_enc, dr)
							lReturnList.Add(vBeI_nav_barras_rfid_enc)
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

	Public Shared Function Get_All_Salidas() As List(Of clsBeI_nav_barras_rfid_enc)

		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_enc)

		Try

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_enc where ISNULL(IdPedidoEnc, 0) > 0 "

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						'lDTA.SelectCommand.Parameters.AddWithValue("@NombreParametro", valor)

						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeI_nav_barras_rfid_enc As New clsBeI_nav_barras_rfid_enc

						For Each dr As DataRow In lDataTable.Rows
							vBeI_nav_barras_rfid_enc = New clsBeI_nav_barras_rfid_enc()
							Cargar(vBeI_nav_barras_rfid_enc, dr)
							lReturnList.Add(vBeI_nav_barras_rfid_enc)
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

End Class
