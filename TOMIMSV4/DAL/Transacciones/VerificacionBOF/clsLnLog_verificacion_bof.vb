Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnLog_verificacion_bof

	Public Shared Sub Cargar(ByRef oBeLog_verificacion_bof As clsBeLog_verificacion_bof, ByRef dr As DataRow)
		Try
			With oBeLog_verificacion_bof
				.IdLogVerificacion = IIf(IsDBNull(dr.Item("IdLogVerificacion")), 0, dr.Item("IdLogVerificacion"))
				.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
				.IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
				.IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
				.IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
				.IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
				.IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
				.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
				.Sku = IIf(IsDBNull(dr.Item("Sku")), "", dr.Item("Sku"))
				.Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
				.IdMotivo = IIf(IsDBNull(dr.Item("IdMotivo")), 0, dr.Item("IdMotivo"))
				.IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
				.User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
				.IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
			End With
		Catch ex As Exception
			Throw New Exception (String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeLog_verificacion_bof As clsBeLog_verificacion_bof, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction= Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("log_verificacion_bof")
			Ins.Add("idlogverificacion", "@idlogverificacion", DataType.Parametro)
			Ins.Add("idbodega", "@idbodega", DataType.Parametro)
			Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
			Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
			Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
			Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
			Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Ins.Add("sku", "@sku", DataType.Parametro)
			Ins.Add("cantidad", "@cantidad", DataType.Parametro)
			Ins.Add("idmotivo", "@idmotivo", DataType.Parametro)
			Ins.Add("idestado", "@idestado", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGVERIFICACION", oBeLog_verificacion_bof.IdLogVerificacion))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeLog_verificacion_bof.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_verificacion_bof.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeLog_verificacion_bof.IdPedidoDet))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeLog_verificacion_bof.IdPickingUbic))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeLog_verificacion_bof.IdPickingEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeLog_verificacion_bof.IdPickingDet))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeLog_verificacion_bof.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@SKU", oBeLog_verificacion_bof.Sku))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeLog_verificacion_bof.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@IDMOTIVO", oBeLog_verificacion_bof.IdMotivo))
			cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeLog_verificacion_bof.IdEstado))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeLog_verificacion_bof.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_verificacion_bof.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeLog_verificacion_bof.IdProductoTallaColor))

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

	Public Shared Function Actualizar(ByRef oBeLog_verificacion_bof As clsBeLog_verificacion_bof, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("log_verificacion_bof")
			Upd.Add("idlogverificacion", "@idlogverificacion", DataType.Parametro)
			Upd.Add("idbodega", "@idbodega", DataType.Parametro)
			Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
			Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
			Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
			Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
			Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
			Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
			Upd.Add("sku", "@sku", DataType.Parametro)
			Upd.Add("cantidad", "@cantidad", DataType.Parametro)
			Upd.Add("idmotivo", "@idmotivo", DataType.Parametro)
			Upd.Add("idestado", "@idestado", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("IdProductoTallaColor", "@fec_agr", DataType.Parametro)
			Upd.Where("IdLogVerificacion = @IdLogVerificacion")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGVERIFICACION", oBeLog_verificacion_bof.IdLogVerificacion))
			cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeLog_verificacion_bof.IdBodega))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeLog_verificacion_bof.IdPedidoEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeLog_verificacion_bof.IdPedidoDet))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeLog_verificacion_bof.IdPickingUbic))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeLog_verificacion_bof.IdPickingEnc))
			cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeLog_verificacion_bof.IdPickingDet))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeLog_verificacion_bof.IdProductoBodega))
			cmd.Parameters.Add(New SqlParameter("@SKU", oBeLog_verificacion_bof.Sku))
			cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeLog_verificacion_bof.Cantidad))
			cmd.Parameters.Add(New SqlParameter("@IDMOTIVO", oBeLog_verificacion_bof.IdMotivo))
			cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeLog_verificacion_bof.IdEstado))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeLog_verificacion_bof.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_verificacion_bof.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeLog_verificacion_bof.IdProductoTallaColor))

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


	Public Shared Function Eliminar(ByRef oBeLog_verificacion_bof As clsBeLog_verificacion_bof,Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Log_verificacion_bof" & _ 
			 "  Where(IdLogVerificacion = @IdLogVerificacion)"

			Dim cmd As New SqlCommand With {.CommandType=CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota then 
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOGVERIFICACION", oBeLog_verificacion_bof.IdLogVerificacion))

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

			Const sp As String = "SELECT * FROM Log_verificacion_bof"
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

	Public Shared Function Get_All() As List(Of clsBeLog_verificacion_bof)
		
		Dim lReturnList As New List(Of clsBeLog_verificacion_bof)
		
		Try
		
			Const sp As String = "SELECT * FROM Log_verificacion_bof"
		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeLog_verificacion_bof As New clsBeLog_verificacion_bof

						For Each dr As DataRow In lDataTable.Rows
						vBeLog_verificacion_bof = New clsBeLog_verificacion_bof()
						Cargar(vBeLog_verificacion_bof, dr)
						lReturnList.Add(vBeLog_verificacion_bof)
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

	Public Shared Sub GetSingle(ByRef pBeLog_verificacion_bof As clsBeLog_verificacion_bof) 
		
		Try
		
			Const sp As String = "SELECT * FROM Log_verificacion_bof" & _ 
			" Where(IdLogVerificacion = @IdLogVerificacion)"

		
			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		
				lConnection.Open()
		
				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
		
					Using lDTA As New SqlDataAdapter(sp, lConnection)
		
					lDTA.SelectCommand.CommandType = CommandType.Text
					lDTA.SelectCommand.Transaction = lTransaction
					Dim lDataTable As New DataTable
					lDTA.Fill(lDataTable)
		
					Dim vBeLog_verificacion_bof As New clsBeLog_verificacion_bof

					If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
						Cargar(vBeLog_verificacion_bof, lDataTable.Rows(0))
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
		
			Const sp As String = "SELECT ISNULL(Max(IdLogVerificacion),0) FROM Log_verificacion_bof"

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

	Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdLogVerificacion),0) FROM Log_verificacion_bof"

			Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

				lCommand.CommandType = CommandType.Text

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

	Public Shared Function Guardar_Log(ByVal BeLogVerificacion As clsBeLog_verificacion_bof) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			lConnection.Open()
			lTransaction = lConnection.BeginTransaction()

			BeLogVerificacion.IdLogVerificacion = MaxID(lConnection, lTransaction) + 1

			Insertar(BeLogVerificacion, lConnection, lTransaction)

			lTransaction.Commit()

			Return BeLogVerificacion.IdLogVerificacion


		Catch ex As Exception
			If Not lTransaction Is Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close
			If Not lConnection Is Nothing Then lConnection.Dispose()
			If Not lTransaction Is Nothing Then lTransaction.Dispose()
		End Try
	End Function

End Class
