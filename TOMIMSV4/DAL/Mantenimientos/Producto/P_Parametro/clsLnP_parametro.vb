Imports System.Data.SqlClient

Public Class clsLnP_parametro

    Public Shared Sub Cargar(ByRef oBeP_parametro As clsBeP_parametro, ByRef dr As DataRow)
        Try
            With oBeP_parametro
                .IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), 0, dr.Item("IdParametro"))
                .Tipo = IIf(IsDBNull(dr.Item("tipo")), "", dr.Item("tipo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), New Date(1900, 1, 1), dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), New Date(1900, 1, 1), dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), New Date(1900, 1, 1), dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception("P_parametro_Cargar: " & ex.message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeP_parametro As clsBeP_parametro, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("p_parametro")
            Ins.Add("idparametro", "@idparametro", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Ins.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Ins.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Ins.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeP_parametro.IdParametro))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_parametro.Tipo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeP_parametro.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", oBeP_parametro.Valor_texto))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", oBeP_parametro.Valor_numerico))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeP_parametro.Valor_fecha = Nothing, DBNull.Value, oBeP_parametro.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", oBeP_parametro.Valor_logico))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeP_parametro.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeP_parametro.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeP_parametro.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeP_parametro.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeP_parametro.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("P_parametro_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeP_parametro As clsBeP_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("p_parametro")
            Upd.Add("idparametro", "@idparametro", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Upd.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Upd.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Upd.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdParametro = @IdParametro")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeP_parametro.IdParametro))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_parametro.Tipo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeP_parametro.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", oBeP_parametro.Valor_texto))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", oBeP_parametro.Valor_numerico))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", oBeP_parametro.Valor_fecha))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", oBeP_parametro.Valor_logico))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeP_parametro.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeP_parametro.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeP_parametro.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeP_parametro.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeP_parametro.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("P_parametro_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeP_parametro As clsBeP_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from P_parametro" &
             "  Where(IdParametro = @IdParametro)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeP_parametro.IdParametro))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("P_parametro_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from P_parametro"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("P_parametro_Eliminar: " & ex.message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM P_parametro"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("P_parametro_Listar: " & ex.Message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeP_parametro As clsBeP_parametro) As Boolean

        Try

            Const sp As String = "SELECT * FROM P_parametro" &
            " Where(IdParametro = @IdParametro)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeP_parametro.IDPARAMETRO))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeP_parametro, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Obtener(ByRef oBeP_parametro As clsBeP_parametro,
                                   ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM P_parametro" &
            " Where(IdParametro = @IdParametro)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeP_parametro.IdParametro))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeP_parametro, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function GetAll() As List(Of clsBeP_parametro)

        Try

            Dim lReturnList As New List(Of clsBeP_parametro)
            Const sp As String = "SELECT * FROM P_parametro"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeP_parametro As New clsBeP_parametro

            For Each dr As DataRow In dt.Rows

                vBeP_parametro = New clsBeP_parametro
                Cargar(vBeP_parametro, dr)
                lReturnList.Add(vBeP_parametro)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("P_parametro_GetAll: " & ex.message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeP_parametro As clsBeP_parametro)

        Try

            Const sp As String = "SELECT * FROM P_parametro " & _
            " Where(IdParametro = @IdParametro)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPARAMETRO", pBeP_parametro.IDPARAMETRO))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then Cargar(pBeP_parametro, dt.Rows(0))

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
