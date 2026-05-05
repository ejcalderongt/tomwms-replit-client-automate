Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnTrans_inv_ciclico_rfid

	Public Shared Sub Cargar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, ByRef dr As DataRow)
		Try
			With oBeTrans_inv_ciclico_rfid
				.Idinvciclico = IIf(IsDBNull(dr.Item("idinvciclico")), 0, dr.Item("idinvciclico"))
				.Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
				.IdPallet = IIf(IsDBNull(dr.Item("IdPallet")), 0, dr.Item("IdPallet"))
				.Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
				.Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
				.Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
				.Codigo_Barra = IIf(IsDBNull(dr.Item("Codigo_Barra")), "", dr.Item("Codigo_Barra"))
				.SSCC = IIf(IsDBNull(dr.Item("SSCC")), "", dr.Item("SSCC"))
				.GTIN = IIf(IsDBNull(dr.Item("GTIN")), "", dr.Item("GTIN"))
				.Fecha_Produccion = IIf(IsDBNull(dr.Item("Fecha_Produccion")), Date.Now, dr.Item("Fecha_Produccion"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
				.Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
				.IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
				.Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
				.EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("trans_inv_ciclico_rfid")
			Ins.Add("idinvciclico","@idinvciclico","F")
			Ins.Add("idinventarioenc","@idinventarioenc","F")
			Ins.Add("idpallet","@idpallet","F")
			Ins.Add("codigo","@codigo","F")
			Ins.Add("nombre","@nombre","F")
			Ins.Add("lote","@lote","F")
			Ins.Add("codigo_barra","@codigo_barra","F")
			Ins.Add("sscc","@sscc","F")
			Ins.Add("gtin","@gtin","F")
			Ins.Add("fecha_produccion","@fecha_produccion","F")
			Ins.Add("idproductobodega","@idproductobodega","F")
			Ins.Add("user_agr","@user_agr","F")
			Ins.Add("fec_agr","@fec_agr","F")
			Ins.Add("user_mod","@user_mod","F")
			Ins.Add("fec_mod","@fec_mod","F")
			Ins.Add("idoperador","@idoperador","F")
			Ins.Add("cantidad","@cantidad","F")
			Ins.Add("espallet","@espallet","F")

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))
			cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_rfid.Idinventarioenc))
			cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeTrans_inv_ciclico_rfid.IdPallet))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_ciclico_rfid.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_inv_ciclico_rfid.Nombre))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico_rfid.Lote))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_inv_ciclico_rfid.Codigo_Barra))
			cmd.Parameters.Add(New SqlParameter("@SSCC", oBeTrans_inv_ciclico_rfid.SSCC))
			cmd.Parameters.Add(New SqlParameter("@GTIN", oBeTrans_inv_ciclico_rfid.GTIN))
			cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeTrans_inv_ciclico_rfid.Fecha_Produccion))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico_rfid.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico_rfid.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico_rfid.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_ciclico_rfid.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico_rfid.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico_rfid.IdOperador))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico_rfid.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico_rfid.EsPallet))

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

	Public Shared Function Actualizar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_inv_ciclico_rfid")
			Upd.Add("idinvciclico","@idinvciclico","F")
			Upd.Add("idinventarioenc","@idinventarioenc","F")
			Upd.Add("idpallet","@idpallet","F")
			Upd.Add("codigo","@codigo","F")
			Upd.Add("nombre","@nombre","F")
			Upd.Add("lote","@lote","F")
			Upd.Add("codigo_barra","@codigo_barra","F")
			Upd.Add("sscc","@sscc","F")
			Upd.Add("gtin","@gtin","F")
			Upd.Add("fecha_produccion","@fecha_produccion","F")
			Upd.Add("idproductobodega","@idproductobodega","F")
			Upd.Add("user_agr","@user_agr","F")
			Upd.Add("fec_agr","@fec_agr","F")
			Upd.Add("user_mod","@user_mod","F")
			Upd.Add("fec_mod","@fec_mod","F")
			Upd.Add("idoperador","@idoperador","F")
			Upd.Add("cantidad","@cantidad","F")
			Upd.Add("espallet","@espallet","F")
			Upd.Where("idinvciclico = @idinvciclico")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))
			cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_rfid.Idinventarioenc))
			cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeTrans_inv_ciclico_rfid.IdPallet))
			cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_ciclico_rfid.Codigo))
			cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_inv_ciclico_rfid.Nombre))
			cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico_rfid.Lote))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_inv_ciclico_rfid.Codigo_Barra))
			cmd.Parameters.Add(New SqlParameter("@SSCC", oBeTrans_inv_ciclico_rfid.SSCC))
			cmd.Parameters.Add(New SqlParameter("@GTIN", oBeTrans_inv_ciclico_rfid.GTIN))
			cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeTrans_inv_ciclico_rfid.Fecha_Produccion))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico_rfid.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico_rfid.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico_rfid.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_ciclico_rfid.User_mod))
			cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico_rfid.Fec_mod))
			cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico_rfid.IdOperador))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico_rfid.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico_rfid.EsPallet))

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


	Public Shared Function Eliminar(ByRef oBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_inv_ciclico_rfid" & _ 
			 "  Where(idinvciclico = @idinvciclico)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico_rfid.Idinvciclico))

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

			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid"
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

	Public Shared Function Get_All() As List(Of clsBeTrans_inv_ciclico_rfid)
		
		Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_rfid)
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_inv_ciclico_rfid As New clsBeTrans_inv_ciclico_rfid

						For Each dr As DataRow In lDataTable.Rows
						vBeTrans_inv_ciclico_rfid = New clsBeTrans_inv_ciclico_rfid()
						Cargar(vBeTrans_inv_ciclico_rfid, dr)
						lReturnList.Add(vBeTrans_inv_ciclico_rfid)
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

	Public Shared Sub GetSingle(ByRef pBeTrans_inv_ciclico_rfid As clsBeTrans_inv_ciclico_rfid) 
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_inv_ciclico_rfid" & _ 
			" Where(idinvciclico = @idinvciclico)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_inv_ciclico_rfid As New clsBeTrans_inv_ciclico_rfid

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeTrans_inv_ciclico_rfid, lDataTable.Rows(0))
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

	Public Shared Function MaxID() As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico_rfid"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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

	Public Shared Function MaxID(ByRef lConnection As SqlConnection,
								 ByRef lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico_rfid"

			Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
				Dim lReturnValue As Object = lCommand.ExecuteScalar()
				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = Convert.ToInt32(lReturnValue) + 1
				End If
			End Using

			Return lMax

		Catch ex As Exception
			Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
			clsLnLog_error_wms.Agregar_Error(vMsgError)
			Throw ex
		End Try

	End Function


End Class
