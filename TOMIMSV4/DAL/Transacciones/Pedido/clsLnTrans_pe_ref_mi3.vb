Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_pe_ref_mi3

	Public Shared Sub Cargar(ByRef oBeTrans_pe_ref_mi3 As clsBeTrans_pe_ref_mi3, ByRef dr As DataRow)
		Try
			With oBeTrans_pe_ref_mi3
				.Idpedidoencrefmi3 = IIf(IsDBNull(dr.Item("idpedidoencrefmi3")), 0, dr.Item("idpedidoencrefmi3"))
				.Idpedidoenc = IIf(IsDBNull(dr.Item("idpedidoenc")), 0, dr.Item("idpedidoenc"))
				.Iddespachoenc = IIf(IsDBNull(dr.Item("iddespachoenc")), 0, dr.Item("iddespachoenc"))
				.Docnumtraslado = IIf(IsDBNull(dr.Item("docnumtraslado")), 0, dr.Item("docnumtraslado"))
				.Docentrytraslado = IIf(IsDBNull(dr.Item("docentrytraslado")), 0, dr.Item("docentrytraslado"))
				.Docnumentrega = IIf(IsDBNull(dr.Item("docnumentrega")), 0, dr.Item("docnumentrega"))
				.Docentryentrega = IIf(IsDBNull(dr.Item("docentryentrega")), 0, dr.Item("docentryentrega"))
				.Referencia_documento_origen = IIf(IsDBNull(dr.Item("referencia_documento_origen")), "", dr.Item("referencia_documento_origen"))
				.Referencia_documento_destino = IIf(IsDBNull(dr.Item("referencia_documento_destino")), "", dr.Item("referencia_documento_destino"))
				.Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
				.Codigo_bodega_origen = IIf(IsDBNull(dr.Item("codigo_bodega_origen")), "", dr.Item("codigo_bodega_origen"))
				.Codigo_bodega_destino = IIf(IsDBNull(dr.Item("codigo_bodega_destino")), "", dr.Item("codigo_bodega_destino"))
				.Codigo_bodega_virtual = IIf(IsDBNull(dr.Item("codigo_bodega_virtual")), "", dr.Item("codigo_bodega_virtual"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.Usr_agr = IIf(IsDBNull(dr.Item("usr_agr")), "", dr.Item("usr_agr"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeTrans_pe_ref_mi3 As clsBeTrans_pe_ref_mi3, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("trans_pe_ref_mi3")
			Ins.Add("idpedidoencrefmi3", "@idpedidoencrefmi3", WMSTipoDato.Tipo.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", WMSTipoDato.Tipo.Parametro)
			Ins.Add("iddespachoenc", "@iddespachoenc", WMSTipoDato.Tipo.Parametro)
			Ins.Add("docnumtraslado", "@docnumtraslado", WMSTipoDato.Tipo.Parametro)
			Ins.Add("docentrytraslado", "@docentrytraslado", WMSTipoDato.Tipo.Parametro)
			Ins.Add("docnumentrega", "@docnumentrega", WMSTipoDato.Tipo.Parametro)
			Ins.Add("docentryentrega", "@docentryentrega", WMSTipoDato.Tipo.Parametro)
			Ins.Add("referencia_documento_origen", "@referencia_documento_origen", WMSTipoDato.Tipo.Parametro)
			Ins.Add("referencia_documento_destino", "@referencia_documento_destino", WMSTipoDato.Tipo.Parametro)
			Ins.Add("observacion", "@observacion", WMSTipoDato.Tipo.Parametro)
			Ins.Add("codigo_bodega_origen", "@codigo_bodega_origen", WMSTipoDato.Tipo.Parametro)
			Ins.Add("codigo_bodega_destino", "@codigo_bodega_destino", WMSTipoDato.Tipo.Parametro)
			Ins.Add("codigo_bodega_virtual", "@codigo_bodega_virtual", WMSTipoDato.Tipo.Parametro)
			Ins.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
			Ins.Add("usr_agr", "@usr_agr", WMSTipoDato.Tipo.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCREFMI3", oBeTrans_pe_ref_mi3.Idpedidoencrefmi3))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_ref_mi3.Idpedidoenc))
			cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_pe_ref_mi3.Iddespachoenc))
			cmd.Parameters.Add(New SqlParameter("@DOCNUMTRASLADO", oBeTrans_pe_ref_mi3.Docnumtraslado))
			cmd.Parameters.Add(New SqlParameter("@DOCENTRYTRASLADO", oBeTrans_pe_ref_mi3.Docentrytraslado))
			cmd.Parameters.Add(New SqlParameter("@DOCNUMENTREGA", oBeTrans_pe_ref_mi3.Docnumentrega))
			cmd.Parameters.Add(New SqlParameter("@DOCENTRYENTREGA", oBeTrans_pe_ref_mi3.Docentryentrega))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_ORIGEN", oBeTrans_pe_ref_mi3.Referencia_documento_origen))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_DESTINO", oBeTrans_pe_ref_mi3.Referencia_documento_destino))
			cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_ref_mi3.Observacion))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ORIGEN", oBeTrans_pe_ref_mi3.Codigo_bodega_origen))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_DESTINO", oBeTrans_pe_ref_mi3.Codigo_bodega_destino))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_VIRTUAL", oBeTrans_pe_ref_mi3.Codigo_bodega_virtual))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_ref_mi3.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USR_AGR", oBeTrans_pe_ref_mi3.Usr_agr))

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

	Public Shared Function Actualizar(ByRef oBeTrans_pe_ref_mi3 As clsBeTrans_pe_ref_mi3, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("trans_pe_ref_mi3")
			Upd.Add("idpedidoencrefmi3", "@idpedidoencrefmi3", WMSTipoDato.Tipo.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", WMSTipoDato.Tipo.Parametro)
			Upd.Add("iddespachoenc", "@iddespachoenc", WMSTipoDato.Tipo.Parametro)
			Upd.Add("docnumtraslado", "@docnumtraslado", WMSTipoDato.Tipo.Parametro)
			Upd.Add("docentrytraslado", "@docentrytraslado", WMSTipoDato.Tipo.Parametro)
			Upd.Add("docnumentrega", "@docnumentrega", WMSTipoDato.Tipo.Parametro)
			Upd.Add("docentryentrega", "@docentryentrega", WMSTipoDato.Tipo.Parametro)
			Upd.Add("referencia_documento_origen", "@referencia_documento_origen", WMSTipoDato.Tipo.Parametro)
			Upd.Add("referencia_documento_destino", "@referencia_documento_destino", WMSTipoDato.Tipo.Parametro)
			Upd.Add("observacion", "@observacion", WMSTipoDato.Tipo.Parametro)
			Upd.Add("codigo_bodega_origen", "@codigo_bodega_origen", WMSTipoDato.Tipo.Parametro)
			Upd.Add("codigo_bodega_destino", "@codigo_bodega_destino", WMSTipoDato.Tipo.Parametro)
			Upd.Add("codigo_bodega_virtual", "@codigo_bodega_virtual", WMSTipoDato.Tipo.Parametro)
			Upd.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
			Upd.Add("usr_agr", "@usr_agr", WMSTipoDato.Tipo.Parametro)
			Upd.Where("idpedidoencrefmi3 = @idpedidoencrefmi3")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCREFMI3", oBeTrans_pe_ref_mi3.Idpedidoencrefmi3))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_ref_mi3.Idpedidoenc))
			cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_pe_ref_mi3.Iddespachoenc))
			cmd.Parameters.Add(New SqlParameter("@DOCNUMTRASLADO", oBeTrans_pe_ref_mi3.Docnumtraslado))
			cmd.Parameters.Add(New SqlParameter("@DOCENTRYTRASLADO", oBeTrans_pe_ref_mi3.Docentrytraslado))
			cmd.Parameters.Add(New SqlParameter("@DOCNUMENTREGA", oBeTrans_pe_ref_mi3.Docnumentrega))
			cmd.Parameters.Add(New SqlParameter("@DOCENTRYENTREGA", oBeTrans_pe_ref_mi3.Docentryentrega))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_ORIGEN", oBeTrans_pe_ref_mi3.Referencia_documento_origen))
			cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_DESTINO", oBeTrans_pe_ref_mi3.Referencia_documento_destino))
			cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_ref_mi3.Observacion))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_ORIGEN", oBeTrans_pe_ref_mi3.Codigo_bodega_origen))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_DESTINO", oBeTrans_pe_ref_mi3.Codigo_bodega_destino))
			cmd.Parameters.Add(New SqlParameter("@CODIGO_BODEGA_VIRTUAL", oBeTrans_pe_ref_mi3.Codigo_bodega_virtual))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_ref_mi3.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@USR_AGR", oBeTrans_pe_ref_mi3.Usr_agr))

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


	Public Shared Function Eliminar(ByRef oBeTrans_pe_ref_mi3 As clsBeTrans_pe_ref_mi3,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Trans_pe_ref_mi3" & _ 
			 "  Where(idpedidoencrefmi3 = @idpedidoencrefmi3)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCREFMI3", oBeTrans_pe_ref_mi3.Idpedidoencrefmi3))

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

			Const sp As String = "SELECT * FROM Trans_pe_ref_mi3"
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

	Public Shared Function Get_All() As List(Of clsBeTrans_pe_ref_mi3)
		
		Dim lReturnList As New List(Of clsBeTrans_pe_ref_mi3)
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_pe_ref_mi3"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_pe_ref_mi3 As New clsBeTrans_pe_ref_mi3

						For Each dr As DataRow In lDataTable.Rows
						vBeTrans_pe_ref_mi3 = New clsBeTrans_pe_ref_mi3()
						Cargar(vBeTrans_pe_ref_mi3, dr)
						lReturnList.Add(vBeTrans_pe_ref_mi3)
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

	Public Shared Sub GetSingle(ByRef pBeTrans_pe_ref_mi3 As clsBeTrans_pe_ref_mi3) 
		
		Try
		
			Const sp As String = "SELECT * FROM Trans_pe_ref_mi3" & _ 
			" Where(idpedidoencrefmi3 = @idpedidoencrefmi3)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeTrans_pe_ref_mi3 As New clsBeTrans_pe_ref_mi3

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeTrans_pe_ref_mi3, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(idpedidoencrefmi3),0) FROM Trans_pe_ref_mi3"

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

	Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(idpedidoencrefmi3),0) FROM Trans_pe_ref_mi3 "

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

End Class
