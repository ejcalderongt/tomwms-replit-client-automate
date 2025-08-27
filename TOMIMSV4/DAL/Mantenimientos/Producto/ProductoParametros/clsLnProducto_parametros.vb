Imports System.Data.SqlClient

Public Class clsLnProducto_parametros

    Public Shared Sub Cargar(ByRef oBeProducto_parametros As clsBeProducto_parametros, ByRef dr As DataRow)

        Try

            With oBeProducto_parametros
                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), 0, dr.Item("IdParametro"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), Date.Now, dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .Capturar_siempre = IIf(IsDBNull(dr.Item("capturar_siempre")), False, dr.Item("capturar_siempre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Cargar: " & ex.message)
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_parametros As clsBeProducto_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("producto_parametros")
            Ins.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Ins.Add("idparametro", "@idparametro", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Ins.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Ins.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Ins.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Ins.Add("capturar_siempre", "@capturar_siempre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeProducto_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeProducto_parametros.IdParametro))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_parametros.IdProducto))

            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeProducto_parametros.Valor_texto.Trim = "", DBNull.Value, oBeProducto_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeProducto_parametros.Valor_numerico = 0, DBNull.Value, oBeProducto_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeProducto_parametros.Valor_fecha = New Date(1900, 1, 1), DBNull.Value, oBeProducto_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeProducto_parametros.Valor_logico, True, oBeProducto_parametros.Valor_logico)))

            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_SIEMPRE", oBeProducto_parametros.Capturar_siempre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_parametros.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_parametros.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_parametros.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_parametros.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

            oBeProducto_parametros.IdProductoParametro = CInt(cmd.Parameters("@IDPRODUCTOPARAMETRO").Value)

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_parametros As clsBeProducto_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("producto_parametros")
            Upd.Add("idproductoparametro", "@idproductoparametro", DataType.Parametro)
            Upd.Add("idparametro", "@idparametro", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("valor_texto", "@valor_texto", DataType.Parametro)
            Upd.Add("valor_numerico", "@valor_numerico", DataType.Parametro)
            Upd.Add("valor_fecha", "@valor_fecha", DataType.Parametro)
            Upd.Add("valor_logico", "@valor_logico", DataType.Parametro)
            Upd.Add("capturar_siempre", "@capturar_siempre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdProductoParametro = @IdProductoParametro")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeProducto_parametros.IdProductoParametro))
            cmd.Parameters.Add(New SqlParameter("@IDPARAMETRO", oBeProducto_parametros.IdParametro))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_parametros.IdProducto))

            cmd.Parameters.Add(New SqlParameter("@VALOR_TEXTO", IIf(oBeProducto_parametros.Valor_texto.Trim = "", DBNull.Value, oBeProducto_parametros.Valor_texto)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_NUMERICO", IIf(oBeProducto_parametros.Valor_numerico = 0, DBNull.Value, oBeProducto_parametros.Valor_numerico)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FECHA", IIf(oBeProducto_parametros.Valor_fecha = New Date(1900, 1, 1), DBNull.Value, oBeProducto_parametros.Valor_fecha)))
            cmd.Parameters.Add(New SqlParameter("@VALOR_LOGICO", IIf(oBeProducto_parametros.Valor_logico, True, oBeProducto_parametros.Valor_logico)))

            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_SIEMPRE", oBeProducto_parametros.Capturar_siempre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_parametros.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_parametros.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_parametros.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_parametros.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_parametros.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeProducto_parametros As clsBeProducto_parametros, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Producto_parametros" &
             "  Where(IdProductoParametro = @IdProductoParametro)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeProducto_parametros.IdProductoParametro))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Eliminar: " & ex.message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try
    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_parametros"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Listar: " & ex.message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_parametros As clsBeProducto_parametros) As Boolean

        Try

            Dim sp As String = " SELECT * FROM Producto_parametros" &
                               " Where(IdProductoParametro = @IdProductoParametro)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeProducto_parametros.IdProductoParametro))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_parametros, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_parametros As clsBeProducto_parametros,
                                   ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = " SELECT * FROM Producto_parametros" &
                               " Where(IdProductoParametro = @IdProductoParametro)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETRO", oBeProducto_parametros.IdProductoParametro))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_parametros, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
