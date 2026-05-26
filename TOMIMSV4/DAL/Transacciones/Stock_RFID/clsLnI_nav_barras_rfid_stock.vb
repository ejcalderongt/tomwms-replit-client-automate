Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnI_nav_barras_rfid_stock

	Public Shared Sub Cargar(ByRef oBeI_nav_barras_rfid_stock As clsBeI_nav_barras_rfid_stock, ByRef dr As DataRow)
		Try
			With oBeI_nav_barras_rfid_stock
				.IdRfidStock = IIf(IsDBNull(dr.Item("IdRfidStock")), 0, dr.Item("IdRfidStock"))
				.Barra_epc = IIf(IsDBNull(dr.Item("barra_epc")), "", dr.Item("barra_epc"))
				.Tagid = IIf(IsDBNull(dr.Item("tagid")), "", dr.Item("tagid"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
				.Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
				.IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
				.IdRFIDEncOrigen = IIf(IsDBNull(dr.Item("IdRFIDEncOrigen")), 0, dr.Item("IdRFIDEncOrigen"))
				.IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.IdRFIDEncSalida = IIf(IsDBNull(dr.Item("IdRFIDEncSalida")), 0, dr.Item("IdRFIDEncSalida"))
				.Fec_Salida = IIf(IsDBNull(dr.Item("fec_Salida")), Date.Now, dr.Item("fec_Salida"))
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

	Public Shared Function Insertar(ByRef oBeI_nav_barras_rfid_stock As clsBeI_nav_barras_rfid_stock, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("i_nav_barras_rfid_stock")
			'Ins.Add("idrfidstock","@idrfidstock",DataType.Parametro)
			Ins.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Ins.Add("tagid", "@tagid", DataType.Parametro)
			Ins.Add("idbodega", "@idbodega", DataType.Parametro)
			Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Ins.Add("lote", "@lote", DataType.Parametro)
			Ins.Add("cantidad", "@cantidad", DataType.Parametro)
			Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
			Ins.Add("idrfidencorigen", "@idrfidencorigen", DataType.Parametro)
			Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("idrfidencsalida", "@idrfidencsalida", DataType.Parametro)
			Ins.Add("fec_salida", "@fec_salida", DataType.Parametro)
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

			'cmd.Parameters.Add(New SqlParameter("@IDRFIDSTOCK", oBeI_nav_barras_rfid_stock.IdRfidStock))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_stock.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_stock.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_stock.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_barras_rfid_stock.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_rfid_stock.Lote))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_barras_rfid_stock.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeI_nav_barras_rfid_stock.IdUbicacion))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENCORIGEN", oBeI_nav_barras_rfid_stock.IdRFIDEncOrigen))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_stock.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_stock.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENCSALIDA", oBeI_nav_barras_rfid_stock.IdRFIDEncSalida))
			cmd.Parameters.Add(New SqlParameter("@FEC_SALIDA", oBeI_nav_barras_rfid_stock.Fec_Salida))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_rfid_stock.Activo))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_stock.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_stock.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_barras_rfid_stock.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_stock.Fec_mod))

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

	Public Shared Function Actualizar(ByRef oBeI_nav_barras_rfid_stock As clsBeI_nav_barras_rfid_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("i_nav_barras_rfid_stock")
			Upd.Add("idrfidstock", "@idrfidstock", DataType.Parametro)
			Upd.Add("barra_epc", "@barra_epc", DataType.Parametro)
			Upd.Add("tagid", "@tagid", DataType.Parametro)
			Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Upd.Add("lote", "@lote", DataType.Parametro)
			Upd.Add("cantidad", "@cantidad", DataType.Parametro)
			Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
			Upd.Add("idrfidencorigen", "@idrfidencorigen", DataType.Parametro)
			Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("idrfidencsalida", "@idrfidencsalida", DataType.Parametro)
			Upd.Add("fec_salida", "@fec_salida", DataType.Parametro)
			Upd.Add("activo", "@activo", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("user_mod", "@user_mod", DataType.Parametro)
			Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
			Upd.Where("IdRfidStock = @IdRfidStock")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDSTOCK", oBeI_nav_barras_rfid_stock.IdRfidStock))
			cmd.Parameters.Add(New SqlParameter("@BARRA_EPC", oBeI_nav_barras_rfid_stock.Barra_epc))
			cmd.Parameters.Add(New SqlParameter("@TAGID", oBeI_nav_barras_rfid_stock.Tagid))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_barras_rfid_stock.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeI_nav_barras_rfid_stock.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_rfid_stock.Lote))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeI_nav_barras_rfid_stock.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeI_nav_barras_rfid_stock.IdUbicacion))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENCORIGEN", oBeI_nav_barras_rfid_stock.IdRFIDEncOrigen))
			cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_rfid_stock.IdOrdenCompraEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeI_nav_barras_rfid_stock.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDRFIDENCSALIDA", oBeI_nav_barras_rfid_stock.IdRFIDEncSalida))
			cmd.Parameters.Add(New SqlParameter("@FEC_SALIDA", oBeI_nav_barras_rfid_stock.Fec_Salida))
			cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_rfid_stock.Activo))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_barras_rfid_stock.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_barras_rfid_stock.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_barras_rfid_stock.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_barras_rfid_stock.Fec_mod))

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


	Public Shared Function Eliminar(ByRef oBeI_nav_barras_rfid_stock As clsBeI_nav_barras_rfid_stock,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from I_nav_barras_rfid_stock" & _ 
			 "  Where(IdRfidStock = @IdRfidStock)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDRFIDSTOCK", oBeI_nav_barras_rfid_stock.IdRfidStock))

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

			Const sp As String = "SELECT * FROM I_nav_barras_rfid_stock"
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

	Public Shared Function Get_All() As List(Of clsBeI_nav_barras_rfid_stock)
		
		Dim lReturnList As New List(Of clsBeI_nav_barras_rfid_stock)
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_stock"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_stock As New clsBeI_nav_barras_rfid_stock

						For Each dr As DataRow In lDataTable.Rows
						vBeI_nav_barras_rfid_stock = New clsBeI_nav_barras_rfid_stock()
						Cargar(vBeI_nav_barras_rfid_stock, dr)
						lReturnList.Add(vBeI_nav_barras_rfid_stock)
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

	Public Shared Sub GetSingle(ByRef pBeI_nav_barras_rfid_stock As clsBeI_nav_barras_rfid_stock) 
		
		Try
		
			Const sp As String = "SELECT * FROM I_nav_barras_rfid_stock" & _ 
			" Where(IdRfidStock = @IdRfidStock)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeI_nav_barras_rfid_stock As New clsBeI_nav_barras_rfid_stock

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeI_nav_barras_rfid_stock, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdRfidStock),0) FROM I_nav_barras_rfid_stock"

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
