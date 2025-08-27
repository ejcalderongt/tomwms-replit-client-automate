Imports System.Data.SqlClient
Imports System.Reflection
Imports System

Public Class clsLnDMS_Log_sincronizacion_nube

	Public Shared Sub Cargar(ByRef oBeLog_sincronizacion_nube As clsBeDMS_Log_sincronizacion_nube, ByRef dr As DataRow)
		Try
			With oBeLog_sincronizacion_nube
				.IdLog = IIf(IsDBNull(dr.Item("IdLog")), 0, dr.Item("IdLog"))
				.Fecha_sincronizacion = IIf(IsDBNull(dr.Item("Fecha_sincronizacion")), Date.Now, dr.Item("Fecha_sincronizacion"))
				.Registros_enviados = IIf(IsDBNull(dr.Item("Registros_enviados")), 0, dr.Item("Registros_enviados"))
				.Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
				.Mensaje_error = IIf(IsDBNull(dr.Item("Mensaje_error")), "", dr.Item("Mensaje_error"))
				.Tiempo_de_envio = IIf(IsDBNull(dr.Item("Tiempo_de_envio")), 0, dr.Item("Tiempo_de_envio"))
				.User_agr = IIf(IsDBNull(dr.Item("User_agr")), "", dr.Item("User_agr"))
				.Fec_agr = IIf(IsDBNull(dr.Item("Fec_agr")), Date.Now, dr.Item("Fec_agr"))
				.Entidad = IIf(IsDBNull(dr.Item("Entidad")), "", dr.Item("Entidad"))
				.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
			End With
		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try
	End Sub

	Public Shared Function Insertar(ByRef oBeLog_sincronizacion_nube As clsBeDMS_Log_sincronizacion_nube, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Ins.Init("log_dms_sincronizacion_nube")
			Ins.Add("idlog", "@idlog", DataType.Parametro)
			Ins.Add("fecha_sincronizacion", "@fecha_sincronizacion", DataType.Parametro)
			Ins.Add("registros_enviados", "@registros_enviados", DataType.Parametro)
			Ins.Add("estado", "@estado", DataType.Parametro)
			Ins.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Ins.Add("tiempo_de_envio", "@tiempo_de_envio", DataType.Parametro)
			Ins.Add("user_agr", "@user_agr", DataType.Parametro)
			Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Ins.Add("Entidad", "@Entidad", DataType.Parametro)
			Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)

			Dim sp As String = Ins.SQL()
			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOG", oBeLog_sincronizacion_nube.IdLog))
			cmd.Parameters.Add(New SqlParameter("@FECHA_SINCRONIZACION", oBeLog_sincronizacion_nube.Fecha_sincronizacion))
			cmd.Parameters.Add(New SqlParameter("@REGISTROS_ENVIADOS", oBeLog_sincronizacion_nube.Registros_enviados))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_nube.Estado))
			cmd.Parameters.Add(New SqlParameter("@MENSAJE_ERROR", oBeLog_sincronizacion_nube.Mensaje_error))
			cmd.Parameters.Add(New SqlParameter("@TIEMPO_DE_ENVIO", oBeLog_sincronizacion_nube.Tiempo_de_envio))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeLog_sincronizacion_nube.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_sincronizacion_nube.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@ENTIDAD", oBeLog_sincronizacion_nube.Entidad))
			cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeLog_sincronizacion_nube.IdPropietario))

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

	Public Shared Function Actualizar(ByRef oBeLog_sincronizacion_nube As clsBeDMS_Log_sincronizacion_nube, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Upd.Init("log_dms_sincronizacion_nube")
			Upd.Add("idlog", "@idlog", DataType.Parametro)
			Upd.Add("fecha_sincronizacion", "@fecha_sincronizacion", DataType.Parametro)
			Upd.Add("registros_enviados", "@registros_enviados", DataType.Parametro)
			Upd.Add("estado", "@estado", DataType.Parametro)
			Upd.Add("mensaje_error", "@mensaje_error", DataType.Parametro)
			Upd.Add("tiempo_de_envio", "@tiempo_de_envio", DataType.Parametro)
			Upd.Add("user_agr", "@user_agr", DataType.Parametro)
			Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
			Upd.Add("Entidad", "@Entidad", DataType.Parametro)
			Upd.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
			Upd.Where("IdLog = @IdLog")

			Dim sp As String = Upd.SQL()

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOG", oBeLog_sincronizacion_nube.IdLog))
			cmd.Parameters.Add(New SqlParameter("@FECHA_SINCRONIZACION", oBeLog_sincronizacion_nube.Fecha_sincronizacion))
			cmd.Parameters.Add(New SqlParameter("@REGISTROS_ENVIADOS", oBeLog_sincronizacion_nube.Registros_enviados))
			cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLog_sincronizacion_nube.Estado))
			cmd.Parameters.Add(New SqlParameter("@MENSAJE_ERROR", oBeLog_sincronizacion_nube.Mensaje_error))
			cmd.Parameters.Add(New SqlParameter("@TIEMPO_DE_ENVIO", oBeLog_sincronizacion_nube.Tiempo_de_envio))
			cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeLog_sincronizacion_nube.User_agr))
			cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeLog_sincronizacion_nube.Fec_agr))
			cmd.Parameters.Add(New SqlParameter("@ENTIDAD", oBeLog_sincronizacion_nube.Entidad))
			cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeLog_sincronizacion_nube.IdPropietario))

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


	Public Shared Function Eliminar(ByRef oBeLog_sincronizacion_nube As clsBeDMS_Log_sincronizacion_nube, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Log_dms_sincronizacion_nube" &
			 "  Where(IdLog = @IdLog)"

			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

			cmd.Parameters.Add(New SqlParameter("@IDLOG", oBeLog_sincronizacion_nube.IdLog))

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


	Public Shared Function Eliminar(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing

		Try

			Const sp As String = " Delete from Log_dms_sincronizacion_nube "


			Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				cmd = New SqlCommand(sp, pConection, pTransaction)
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				cmd = New SqlCommand(sp, lConnection, lTransaction)
			End If

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

			Const sp As String = "SELECT * FROM Log_dms_sincronizacion_nube"
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

	Public Shared Function Get_All() As List(Of clsBeDMS_Log_sincronizacion_nube)

		Dim lReturnList As New List(Of clsBeDMS_Log_sincronizacion_nube)

		Try

			Const sp As String = "SELECT * FROM Log_dms_sincronizacion_nube"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeLog_sincronizacion_nube As New clsBeDMS_Log_sincronizacion_nube

						For Each dr As DataRow In lDataTable.Rows
							vBeLog_sincronizacion_nube = New clsBeDMS_Log_sincronizacion_nube()
							Cargar(vBeLog_sincronizacion_nube, dr)
							lReturnList.Add(vBeLog_sincronizacion_nube)
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

	Public Shared Sub GetSingle(ByRef pBeLog_sincronizacion_nube As clsBeDMS_Log_sincronizacion_nube)

		Try

			Const sp As String = "SELECT * FROM Log_dms_sincronizacion_nube  Where(IdLog = @IdLog)"

			Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

				lConnection.Open()

				Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

					Using lDTA As New SqlDataAdapter(sp, lConnection)

						lDTA.SelectCommand.CommandType = CommandType.Text
						lDTA.SelectCommand.Transaction = lTransaction
						Dim lDataTable As New DataTable
						lDTA.Fill(lDataTable)

						Dim vBeLog_sincronizacion_nube As New clsBeDMS_Log_sincronizacion_nube

						If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
							Cargar(vBeLog_sincronizacion_nube, lDataTable.Rows(0))
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

			Const sp As String = "SELECT ISNULL(Max(IdLog),0) FROM Log_dms_sincronizacion_nube"

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

	Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

		Try

			Dim lMax As Integer = 0

			Const sp As String = "SELECT ISNULL(Max(IdLog),0) FROM Log_dms_sincronizacion_nube"

			Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

				Dim lReturnValue As Object = lCommand.ExecuteScalar()

				If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
					lMax = CInt(lReturnValue) + 1
				End If

			End Using

			Return lMax

		Catch ex As Exception
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		End Try

	End Function

	Public Shared Function GetLastSync(ByVal pTablaSincronizada As String,
									   ByVal pPropietario As Integer,
									   Optional ByVal pConection As SqlConnection = Nothing,
									   Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeDMS_Log_sincronizacion_nube

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Dim lDTA As New SqlDataAdapter
		GetLastSync = Nothing

		Try

			Dim vSQL As String = "SELECT TOP 1 * FROM Log_dms_sincronizacion_nube where (Estado = 'Ok' OR Estado = 'Parcial')  and Entidad=@pTablaSincronizada "

			'#GT15072025: iteramos lista de propietarios Integer
			If pPropietario > 0 Then
				vSQL &= " AND IdPropietario =@pPropietario"
			End If

			vSQL &= " ORDER BY Fecha_sincronizacion DESC "

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				lDTA = New SqlDataAdapter(vSQL, pConection)
				lDTA.SelectCommand.Transaction = pTransaction
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lDTA = New SqlDataAdapter(vSQL, lConnection)
			End If

			lDTA.SelectCommand.CommandType = CommandType.Text
			lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pTablaSincronizada", pTablaSincronizada))

			If pPropietario > 0 Then
				lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pPropietario", pPropietario))
			End If

			Dim lDataTable As New DataTable
			lDTA.Fill(lDataTable)

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				GetLastSync = New clsBeDMS_Log_sincronizacion_nube
				Cargar(GetLastSync, lDataTable.Rows(0))
			End If

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

	Public Shared Function Registros_Previos_By_TablaSync(ByVal pTablaSincronizada As String, ByVal listaPropietarios As List(Of Integer), Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Dim lDTA As New SqlDataAdapter
		Registros_Previos_By_TablaSync = 0

		Try

			Dim vSQL As String = "SELECT count(*) FROM Log_dms_sincronizacion_nube where Estado='Ok' and Entidad=@pTablaSincronizada"


			'#GT15072025: iteramos lista de propietarios Integer
			If listaPropietarios IsNot Nothing AndAlso listaPropietarios.Count > 0 Then
				Dim propietarioIds As String = String.Join(",", listaPropietarios.Select(Function(p) p.ToString()))
				vSQL &= " AND IdPropietario IN (" & propietarioIds & ")"
			End If


			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				lDTA = New SqlDataAdapter(vSQL, pConection)
				lDTA.SelectCommand.Transaction = pTransaction
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lDTA = New SqlDataAdapter(vSQL, lConnection)
			End If

			lDTA.SelectCommand.CommandType = CommandType.Text
			lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pTablaSincronizada", pTablaSincronizada))
			Dim lDataTable As New DataTable
			lDTA.Fill(lDataTable)

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Registros_Previos_By_TablaSync = lDataTable.Rows.Count
			End If

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

	Public Shared Function Existe_By_Tabla_and_IdPropietario(ByVal pTablaSincronizada As String, ByVal pIdPropietario As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

		Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
		Dim lTransaction As SqlTransaction = Nothing
		Dim lDTA As New SqlDataAdapter
		Existe_By_Tabla_and_IdPropietario = False

		Try

			Const sp As String = "SELECT TOP 1 * FROM Log_dms_sincronizacion_nube where (Entidad=@pTablaSincronizada and IdPropietario=@pIdPropietario) "

			Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

			If Es_Transaccion_Remota Then
				lDTA = New SqlDataAdapter(sp, pConection)
				lDTA.SelectCommand.Transaction = pTransaction
			Else
				lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
				lDTA = New SqlDataAdapter(sp, lConnection)
			End If

			lDTA.SelectCommand.CommandType = CommandType.Text
			lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pTablaSincronizada", pTablaSincronizada))
			lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pIdPropietario", pIdPropietario))
			Dim lDataTable As New DataTable
			lDTA.Fill(lDataTable)

			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
				Existe_By_Tabla_and_IdPropietario = True
			End If

			If Not Es_Transaccion_Remota Then lTransaction.Commit()

		Catch ex As Exception
			If lTransaction IsNot Nothing Then lTransaction.Rollback()
			Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
		Finally
			If lConnection.State = ConnectionState.Open Then lConnection.Close()
			If lTransaction IsNot Nothing Then lTransaction.Dispose()
			If lConnection IsNot Nothing Then lConnection.Dispose()
		End Try

	End Function

End Class
