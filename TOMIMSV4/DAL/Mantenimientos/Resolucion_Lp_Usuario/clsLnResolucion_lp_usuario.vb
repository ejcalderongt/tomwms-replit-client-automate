Imports System.Data.SqlClient

Public Class clsLnResolucion_lp_usuario
    Public Shared Sub Cargar(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario, ByRef dr As DataRow)
        Try
            With oBeResolucion_lp_usuario
                .Idresolucionlpusuario = IIf(IsDBNull(dr.Item("idresolucionlpusuario")), 0, dr.Item("idresolucionlpusuario"))
                .Idusuario = IIf(IsDBNull(dr.Item("idusuario")), 0, dr.Item("idusuario"))
                .Idbodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Serie = IIf(IsDBNull(dr.Item("serie")), "", dr.Item("serie"))
                .Correlativo_inicial = IIf(IsDBNull(dr.Item("correlativo_inicial")), 0, dr.Item("correlativo_inicial"))
                .Correlativo_final = IIf(IsDBNull(dr.Item("correlativo_final")), 0, dr.Item("correlativo_final"))
                .Correlativo_actual = IIf(IsDBNull(dr.Item("correlativo_actual")), 0, dr.Item("correlativo_actual"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Try

            Ins.Init("resolucion_lp_usuario")
            Ins.Add("idresolucionlpusuario", "@idresolucionlpusuario", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("correlativo_inicial", "@correlativo_inicial", DataType.Parametro)
            Ins.Add("correlativo_final", "@correlativo_final", DataType.Parametro)
            Ins.Add("correlativo_actual", "@correlativo_actual", DataType.Parametro)
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
            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLPUSUARIO", oBeResolucion_lp_usuario.Idresolucionlpusuario))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeResolucion_lp_usuario.Idusuario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeResolucion_lp_usuario.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeResolucion_lp_usuario.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_INICIAL", oBeResolucion_lp_usuario.Correlativo_inicial))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_FINAL", oBeResolucion_lp_usuario.Correlativo_final))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_usuario.Correlativo_actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeResolucion_lp_usuario.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeResolucion_lp_usuario.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeResolucion_lp_usuario.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_usuario.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeResolucion_lp_usuario.Fec_mod))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            If Not Es_Transaccion_Remota Then lTransaction.Commit()
            Return rowsAffected
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Actualizar(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer
        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Try

            Upd.Init("resolucion_lp_usuario")
            Upd.Add("idresolucionlpusuario", "@idresolucionlpusuario", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("correlativo_inicial", "@correlativo_inicial", DataType.Parametro)
            Upd.Add("correlativo_final", "@correlativo_final", DataType.Parametro)
            Upd.Add("correlativo_actual", "@correlativo_actual", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idresolucionlpusuario = @idresolucionlpusuario")
            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLPUSUARIO", oBeResolucion_lp_usuario.Idresolucionlpusuario))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeResolucion_lp_usuario.Idusuario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeResolucion_lp_usuario.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeResolucion_lp_usuario.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_INICIAL", oBeResolucion_lp_usuario.Correlativo_inicial))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_FINAL", oBeResolucion_lp_usuario.Correlativo_final))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_usuario.Correlativo_actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeResolucion_lp_usuario.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeResolucion_lp_usuario.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeResolucion_lp_usuario.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_usuario.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeResolucion_lp_usuario.Fec_mod))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            If Not Es_Transaccion_Remota Then lTransaction.Commit()
            Return rowsAffected
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Function
    Public Shared Function Eliminar(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer
        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Try
            Const sp As String = " Delete from Resolucion_lp_usuario" &
"  Where(idresolucionlpusuario = @idresolucionlpusuario)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLPUSUARIO", oBeResolucion_lp_usuario.Idresolucionlpusuario))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            If Not Es_Transaccion_Remota Then lTransaction.Commit()
            Return rowsAffected
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
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
            Const sp As String = "SELECT * FROM Resolucion_lp_usuario"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)
            lTransaction.Commit()
            Return dt
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Get_All() As List(Of clsBeResolucion_lp_usuario)
        Dim lReturnList As New List(Of clsBeResolucion_lp_usuario)
        Try
            Const sp As String = "SELECT * FROM Resolucion_lp_usuario"
            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_usuario As New clsBeResolucion_lp_usuario

                        For Each dr As DataRow In lDataTable.Rows
                            vBeResolucion_lp_usuario = New clsBeResolucion_lp_usuario()
                            Cargar(vBeResolucion_lp_usuario, dr)
                            lReturnList.Add(vBeResolucion_lp_usuario)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeResolucion_lp_usuario As clsBeResolucion_lp_usuario)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_usuario" &
            " Where(idresolucionlpusuario = @idresolucionlpusuario)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()
                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Using lDTA As New SqlDataAdapter(sp, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Dim vBeResolucion_lp_usuario As New clsBeResolucion_lp_usuario
                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeResolucion_lp_usuario, lDataTable.Rows(0))
                        End If
                    End Using
                    lTransaction.Commit()
                End Using
                lConnection.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function MaxID() As Integer
        Try
            Dim lMax As Integer = 0
            Const sp As String = "SELECT ISNULL(Max(idresolucionlpusuario),0) FROM Resolucion_lp_usuario"
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
            Throw ex
        End Try
    End Function
    Public Shared Function Get_Resolucion_By_IdOperador_And_IdBodega(ByVal pIdBodega As Integer,
ByVal pIdUsuario As Integer) As clsBeResolucion_lp_usuario
        Get_Resolucion_By_IdOperador_And_IdBodega = Nothing
        Dim lReturnList As New List(Of clsBeResolucion_lp_usuario)
        Try
            Const sp As String = "SELECT * FROM Resolucion_lp_usuario
								  WHERE IdUsuario = @IdUsuario 
								  AND IdBodega = @IdBodega
								  AND Activo =1"
            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Using lDTA As New SqlDataAdapter(sp, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        If lDataTable.Rows.Count = 1 Then
                            Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_usuario
                            vBeResolucion_lp_operador = New clsBeResolucion_lp_usuario()
                            Dim dr As DataRow = lDataTable.Rows(0)
                            Cargar(vBeResolucion_lp_operador, dr)
                            Get_Resolucion_By_IdOperador_And_IdBodega = vBeResolucion_lp_operador
                        End If
                    End Using

                    lTransaction.Commit()
                End Using
                lConnection.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Shared Function Get_Nuevo_Correlativo_LP_BOF(ByRef Resolucion_Usuario As clsBeResolucion_lp_usuario) As String

    '	'Get_Nuevo_Correlativo_LP_BOF = ""

    '	Try

    '		Dim pLpSiguiente As Long = Resolucion_Operador.Correlativo_Actual + 1
    '		Dim largoMaximo As Integer = Resolucion_Operador.Correlativo_Final.ToString("D").Length
    '		Dim pSerie As String = Resolucion_Operador.Serie
    '		Dim pLicencia = pSerie + pLpSiguiente.ToString("D" + largoMaximo.ToString())

    '		Return pLicencia

    '	Catch ex As Exception
    '		Throw ex
    '	End Try

    'End Function

    ''' <summary>
    ''' #EJC202404151348: Get nueva licencia por usuario (BOF).
    ''' </summary>
    ''' <param name="Resolucion_Usuario"></param>
    ''' <returns></returns>
    Public Shared Function Get_Nuevo_Correlativo_LP_BOF(ByRef Resolucion_Usuario As clsBeResolucion_lp_usuario) As String
        Try

            Dim pLpSiguiente As Long = Resolucion_Usuario.Correlativo_actual + 1
            Dim largoMaximo As Integer = Resolucion_Usuario.Correlativo_final.ToString("D").Length
            Dim pSerie As String = Resolucion_Usuario.Serie
            Dim pLicencia = pSerie + pLpSiguiente.ToString("D" + largoMaximo.ToString())
            Return pLicencia
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Get_Resolucion_By_IdUsuario_And_IdBodega(ByVal pIdBodega As Integer,
ByVal pIdUsuario As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeResolucion_lp_usuario

        Get_Resolucion_By_IdUsuario_And_IdBodega = Nothing
        Dim lReturnList As New List(Of clsBeResolucion_lp_usuario)
        Try
            Const sp As String = "SELECT * FROM Resolucion_lp_usuario
								  WHERE IdUsuario = @IdUsuario 
								  AND IdBodega = @IdBodega
								  AND Activo =1"

            Using lDTA As New SqlDataAdapter(sp, lConnection)
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                If lDataTable.Rows.Count = 1 Then
                    Dim vBeResolucion_lp_usuario As New clsBeResolucion_lp_usuario
                    vBeResolucion_lp_usuario = New clsBeResolucion_lp_usuario()
                    Dim dr As DataRow = lDataTable.Rows(0)
                    Cargar(vBeResolucion_lp_usuario, dr)
                    Get_Resolucion_By_IdUsuario_And_IdBodega = vBeResolucion_lp_usuario
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Actualizar_Correlativo_Actual(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer
        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Try
            Upd.Init("resolucion_lp_usuario")
            Upd.Add("correlativo_actual", "@correlativo_actual", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idresolucionlpusuario = @idresolucionlpusuario")
            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLPUSUARIO", oBeResolucion_lp_usuario.Idresolucionlpusuario))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_usuario.Correlativo_actual))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_All_By_IdUsuario(ByVal pIdUsuario As Integer) As List(Of clsBeResolucion_lp_usuario)
        Dim lReturnList As New List(Of clsBeResolucion_lp_usuario)
        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_usuario WHERE IdUsuario = @IdUsuario"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Using lDTA As New SqlDataAdapter(sp, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_usuario As New clsBeResolucion_lp_usuario
                        For Each dr As DataRow In lDataTable.Rows
                            vBeResolucion_lp_usuario = New clsBeResolucion_lp_usuario()
                            Cargar(vBeResolucion_lp_usuario, dr)
                            lReturnList.Add(vBeResolucion_lp_usuario)
                        Next
                    End Using
                    lTransaction.Commit()
                End Using
                lConnection.Close()

            End Using

            Return lReturnList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Desactivar(ByRef oBeResolucion_lp_usuario As clsBeResolucion_lp_usuario,
                                      Optional ByVal pConection As SqlConnection = Nothing,
Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Try
            Upd.Init("resolucion_lp_usuario")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idresolucionlpusuario = @idresolucionlpusuario")
            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLPUSUARIO", oBeResolucion_lp_usuario.Idresolucionlpusuario))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", 0))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeResolucion_lp_usuario.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_usuario.User_mod))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()
            Return rowsAffected
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Existe_Serie_By_IdUsuario_And_IdBodega(ByVal pSerie As String,
                                                                  ByVal pIdUsuario As Integer,
                                                                  ByVal pIdBodega As Integer) As Boolean

        Existe_Serie_By_IdUsuario_And_IdBodega = False

        Try

            Const sp As String = "SELECT * FROM resolucion_lp_usuario 
                                  WHERE serie = @Serie AND 
                                       ( (IdOperador = @IdUsuario AND 
                                        IdBodega <> @IdBodega)OR
										(IdOperador <> @IdUsuario AND 
                                        IdBodega = @IdBodega) OR
										(IdOperador <> @IdUsuario AND 
                                        IdBodega <>  @IdBodega))"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Serie", pSerie)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Existe_Serie_By_IdUsuario_And_IdBodega = (lDataTable.Rows.Count = 1)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Serie(ByVal pSerie As String) As Boolean


        Existe_Serie = False

        Try

            Const sp As String = "SELECT * FROM resolucion_lp_usuario WHERE serie = @Serie"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Serie", pSerie)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Existe_Serie = (lDataTable.Rows.Count = 1)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
