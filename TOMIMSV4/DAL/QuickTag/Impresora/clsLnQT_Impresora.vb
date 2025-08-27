Imports System.Data.SqlClient

Public Class clsLnQT_Impresora

    Public Shared Sub Cargar(ByRef oBeImpresora As clsBeQT_Impresora, ByRef dr As DataRow)
        Try
            With oBeImpresora
                .IdImpresora = IIf(IsDBNull(dr.Item("IdImpresora")), 0, dr.Item("IdImpresora"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Predeterminada = IIf(IsDBNull(dr.Item("predeterminada")), False, dr.Item("predeterminada"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IP = IIf(IsDBNull(dr.Item("IP")), "", dr.Item("IP"))
                .Conexion = IIf(IsDBNull(dr.Item("conexion")), "", dr.Item("conexion"))
                .user_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Now, dr.Item("fec_agr"))
                .user_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeImpresora As clsBeQT_Impresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresora")
            Ins.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("predeterminada", "@predeterminada", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("IP", "@IP", DataType.Parametro)
            Ins.Add("conexion", "@conexion", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Ins.Add("velocidad_impresion", "@velocidad_impresion", DataType.Parametro)
            Ins.Add("reintentos_impresion", "@reintentos_impresion", DataType.Parametro)
            Ins.Add("delay_impresion", "@delay_impresion", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeImpresora.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PREDETERMINADA", oBeImpresora.Predeterminada))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora.Activo))
            cmd.Parameters.Add(New SqlParameter("@IP", oBeImpresora.IP))
            cmd.Parameters.Add(New SqlParameter("@CONEXION", oBeImpresora.Conexion))

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora.user_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora.fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora.user_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora.fec_mod))

            '#GT10032025: parametrizaci�n de la printer

            cmd.Parameters.Add(New SqlParameter("@VELOCIDAD_IMPRESION", oBeImpresora.Velocidad_Impresion))
            cmd.Parameters.Add(New SqlParameter("@REINTENTOS_IMPRESION", oBeImpresora.Reintentos_Impresion))
            cmd.Parameters.Add(New SqlParameter("@DELAY_IMPRESION", oBeImpresora.Delay_Impresion))



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

    Public Shared Function Actualizar(ByRef oBeImpresora As clsBeQT_Impresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora")
            'Upd.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("predeterminada", "@predeterminada", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("IP", "@IP", DataType.Parametro)
            Upd.Add("conexion", "@conexion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Upd.Add("velocidad_impresion", "@velocidad_impresion", DataType.Parametro)
            Upd.Add("reintentos_impresion", "@reintentos_impresion", DataType.Parametro)
            Upd.Add("delay_impresion", "@delay_impresion", DataType.Parametro)

            Upd.Where("IdImpresora = @IdImpresora")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeImpresora.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PREDETERMINADA", oBeImpresora.Predeterminada))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora.Activo))
            cmd.Parameters.Add(New SqlParameter("@conexion", oBeImpresora.Conexion))

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora.user_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora.fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora.user_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora.fec_mod))

            '#GT10032025: parametrizaci�n de la printer

            cmd.Parameters.Add(New SqlParameter("@VELOCIDAD_IMPRESION", oBeImpresora.Velocidad_Impresion))
            cmd.Parameters.Add(New SqlParameter("@REINTENTOS_IMPRESION", oBeImpresora.Reintentos_Impresion))
            cmd.Parameters.Add(New SqlParameter("@DELAY_IMPRESION", oBeImpresora.Delay_Impresion))


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


    Public Shared Function Eliminar(ByRef oBeImpresora As clsBeQT_Impresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresora" &
             "  Where(IdImpresora = @IdImpresora)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))

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

            Const sp As String = "SELECT * FROM Impresora"
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

    Public Shared Function Get_All() As List(Of clsBeQT_Impresora)

        Dim lReturnList As New List(Of clsBeQT_Impresora)

        Try

            Const sp As String = "SELECT * FROM Impresora"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora As New clsBeQT_Impresora

                        For Each dr As DataRow In lDataTable.Rows
                            vBeImpresora = New clsBeQT_Impresora()
                            Cargar(vBeImpresora, dr)
                            lReturnList.Add(vBeImpresora)
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

    Public Shared Sub GetSingle(ByRef pBeImpresora As clsBeQT_Impresora)

        Try

            Const sp As String = "SELECT * FROM Impresora" &
            " Where(IdImpresora = @IdImpresora)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", pBeImpresora.IdImpresora))
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora As New clsBeQT_Impresora

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresora, lDataTable.Rows(0))
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


    Public Shared Sub GetSingle_By_Name(ByRef pBeImpresora As clsBeQT_Impresora)

        Try

            Const sp As String = "SELECT * FROM Impresora" &
            " Where(descripcion = @descripcion)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@descripcion", pBeImpresora.Descripcion))
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora As New clsBeQT_Impresora

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresora, lDataTable.Rows(0))
                            pBeImpresora = vBeImpresora
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

            Const sp As String = "SELECT ISNULL(Max(IdImpresora),0) FROM Impresora"

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


    Public Shared Function GetList_By_Predeterminada() As List(Of clsBeQT_Impresora)

        Dim lReturnList As New List(Of clsBeQT_Impresora)

        Try

            Const sp As String = "SELECT * FROM Impresora where activo=1 and predeterminada=1"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora As New clsBeQT_Impresora

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each dr As DataRow In lDataTable.Rows
                                vBeImpresora = New clsBeQT_Impresora()
                                Cargar(vBeImpresora, dr)
                                lReturnList.Add(vBeImpresora)
                            Next

                        Else

                            lReturnList = Nothing
                        End If

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

End Class
