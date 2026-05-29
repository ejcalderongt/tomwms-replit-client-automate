Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnI_nav_barras_rfid_mov

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_mov As clsBeI_nav_barras_rfid_mov, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_mov
				.IdRfidMovimiento = IIf(IsDBNull(dr.Item("IdRfidMovimiento")), 0, dr.Item("IdRfidMovimiento"))
				.IdRFIDEnc = IIf(IsDBNull(dr.Item("IdRFIDEnc")), 0, dr.Item("IdRFIDEnc"))
				.IdRfidTipoMov = IIf(IsDBNull(dr.Item("IdRfidTipoMov")), 0, dr.Item("IdRfidTipoMov"))
				.IdRfidStock = IIf(IsDBNull(dr.Item("IdRfidStock")), 0, dr.Item("IdRfidStock"))
				.Barra_epc = IIf(IsDBNull(dr.Item("barra_epc")), "", dr.Item("barra_epc"))
				.Tagid = IIf(IsDBNull(dr.Item("tagid")), "", dr.Item("tagid"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
				.Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
				.Fecha = IIf(IsDBNull(dr.Item("fecha")), New Date (1900,1,1), dr.Item("fecha"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), 0, dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_mov As clsBeI_nav_barras_rfid_mov, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_mov")
			'Ins.Add("idrfidmovimiento","@idrfidmovimiento",DataType.Parametro)
			Ins.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Ins.Add("idrfidtipomov", "@idrfidtipomov", DataType.Parametro)
			Ins.Add("idrfidstock", "@idrfidstock", DataType.Parametro)
			Ins.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Ins.Add("tagid", "@tagid", DataType.Parametro)
			Ins.Add("idbodega", "@idbodega", DataType.Parametro)
			Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Ins.Add("lote", "@lote", DataType.Parametro)
			Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
			Ins.Add("cantidad", "@cantidad", DataType.Parametro)
			Ins.Add("fecha", "@fecha", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
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

			'cmd.Parameters.Add(New SqlParameter("@IDRFIDMOVIMIENTO", oBeI_nav_barras_rfid_mov.IdRfidMovimiento))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_mov.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDTIPOMOV", oBeI_nav_barras_rfid_mov.IdRfidTipoMov))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDSTOCK", oBeI_nav_barras_rfid_mov.IdRfidStock))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_mov.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_mov.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_mov.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_barras_rfid_mov.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_rfid_mov.Lote))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_mov.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_mov.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeI_nav_barras_rfid_mov.IdUbicacion))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_barras_rfid_mov.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_barras_rfid_mov.Fecha))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_mov.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_mov.Fec_agr))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_mov As clsBeI_nav_barras_rfid_mov, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_mov")
			Upd.Add("idrfidmovimiento", "@idrfidmovimiento", DataType.Parametro)
			Upd.Add("idrfidenc", "@idrfidenc", DataType.Parametro)
			Upd.Add("idrfidtipomov", "@idrfidtipomov", DataType.Parametro)
			Upd.Add("idrfidstock", "@idrfidstock", DataType.Parametro)
			Upd.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Upd.Add("tagid", "@tagid", DataType.Parametro)
			Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Upd.Add("lote", "@lote", DataType.Parametro)
			Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
			Upd.Add("cantidad", "@cantidad", DataType.Parametro)
			Upd.Add("fecha", "@fecha", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Where("IdRfidMovimiento = @IdRfidMovimiento")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDMOVIMIENTO", oBeI_nav_barras_rfid_mov.IdRfidMovimiento))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENC", oBeI_nav_barras_rfid_mov.IdRFIDEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDTIPOMOV", oBeI_nav_barras_rfid_mov.IdRfidTipoMov))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDSTOCK", oBeI_nav_barras_rfid_mov.IdRfidStock))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_mov.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_mov.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_mov.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_barras_rfid_mov.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_rfid_mov.Lote))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_mov.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_mov.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeI_nav_barras_rfid_mov.IdUbicacion))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_barras_rfid_mov.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@FECHA", oBeI_nav_barras_rfid_mov.Fecha))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_mov.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_mov.Fec_agr))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_mov As clsBeI_nav_barras_rfid_mov,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_mov" & _ 
			 "  Where(IdRfidMovimiento = @IdRfidMovimiento)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDMOVIMIENTO", oBeI_nav_barras_rfid_mov.IdRfidMovimiento))

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_mov"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_mov)
		
		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_mov)
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_mov"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_mov As New clsBeI_nav_barras_rfid_mov

						For Each dr As DataRow In lDataTable.Rows
						vBeI_nav_barras_rfid_mov = New clsBeI_nav_barras_rfid_mov()
						Cargar(vBeI_nav_barras_rfid_mov, dr)
						lReturnList.Add(vBeI_nav_barras_rfid_mov)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_mov As clsBeI_nav_barras_rfid_mov) 
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_mov" & _ 
			" Where(IdRfidMovimiento = @IdRfidMovimiento)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_mov As New clsBeI_nav_barras_rfid_mov

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeI_nav_barras_rfid_mov, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdRfidMovimiento),0) FROM I_nav_barras_rfid_mov"

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
