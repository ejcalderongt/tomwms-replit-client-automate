Imports System.Data.SqlClient

Public Class clsLnResolucion_lp_operador

    Public Shared Sub Cargar(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador, ByRef dr As DataRow)
        Try
            With oBeResolucion_lp_operador
                .Idresolucionlp = IIf(IsDBNull(dr.Item("idresolucionlp")), 0, dr.Item("idresolucionlp"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
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

    Public Shared Function Insertar(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("resolucion_lp_operador")
            Ins.Add("idresolucionlp", "@idresolucionlp", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
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
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLP", oBeResolucion_lp_operador.Idresolucionlp))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeResolucion_lp_operador.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeResolucion_lp_operador.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeResolucion_lp_operador.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_INICIAL", oBeResolucion_lp_operador.Correlativo_inicial))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_FINAL", oBeResolucion_lp_operador.Correlativo_final))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_operador.Correlativo_actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeResolucion_lp_operador.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeResolucion_lp_operador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeResolucion_lp_operador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_operador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeResolucion_lp_operador.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("resolucion_lp_operador")
            Upd.Add("idresolucionlp", "@idresolucionlp", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
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
            Upd.Where("idresolucionlp = @idresolucionlp")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLP", oBeResolucion_lp_operador.Idresolucionlp))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeResolucion_lp_operador.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeResolucion_lp_operador.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeResolucion_lp_operador.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_INICIAL", oBeResolucion_lp_operador.Correlativo_inicial))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_FINAL", oBeResolucion_lp_operador.Correlativo_final))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_operador.Correlativo_actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeResolucion_lp_operador.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeResolucion_lp_operador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeResolucion_lp_operador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_operador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeResolucion_lp_operador.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Correlativo_Actual(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("resolucion_lp_operador")
            Upd.Add("correlativo_actual", "@correlativo_actual", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idresolucionlp = @idresolucionlp")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLP", oBeResolucion_lp_operador.IdResolucionlp))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_ACTUAL", oBeResolucion_lp_operador.Correlativo_Actual))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

        'Catch ex As Exception
        '	If Not lTransaction Is Nothing Then lTransaction.Rollback()
        '	Throw ex
        'Finally
        '	If lConnection.State = ConnectionState.Open Then lConnection.Close()
        '	If Not lConnection Is Nothing Then lConnection.Dispose()
        '	If Not lTransaction Is Nothing Then lTransaction.Dispose()
        'End Try

    End Function

    Public Shared Function Desactivar(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("resolucion_lp_operador")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idresolucionlp = @idresolucionlp")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLP", oBeResolucion_lp_operador.IdResolucionlp))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", 0))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeResolucion_lp_operador.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeResolucion_lp_operador As clsBeResolucion_lp_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Resolucion_lp_operador" &
             "  Where(idresolucionlp = @idresolucionlp)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRESOLUCIONLP", oBeResolucion_lp_operador.Idresolucionlp))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeResolucion_lp_operador)

        Dim lReturnList As New List(Of clsBeResolucion_lp_operador)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador

                        For Each dr As DataRow In lDataTable.Rows
                            vBeResolucion_lp_operador = New clsBeResolucion_lp_operador()
                            Cargar(vBeResolucion_lp_operador, dr)
                            lReturnList.Add(vBeResolucion_lp_operador)
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

    Public Shared Sub GetSingle(ByRef pBeResolucion_lp_operador As clsBeResolucion_lp_operador)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador" &
            " Where(idresolucionlp = @idresolucionlp)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeResolucion_lp_operador, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idresolucionlp),0) FROM Resolucion_lp_operador"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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

    Public Shared Function GetSingle(ByRef IdResolucionLp As Integer) As clsBeResolucion_lp_operador

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador " &
            " Where(idresolucionlp = @idresolucionlp)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idresolucionlp", IdResolucionLp)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeResolucion_lp_operador, lDataTable.Rows(0))
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

    Public Shared Function GetSingle(ByRef IdResolucionLp As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeResolucion_lp_operador

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador " &
            " Where(idresolucionlp = @idresolucionlp)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idresolucionlp", IdResolucionLp)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeResolucion_lp_operador, lDataTable.Rows(0))
                    GetSingle = vBeResolucion_lp_operador
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
