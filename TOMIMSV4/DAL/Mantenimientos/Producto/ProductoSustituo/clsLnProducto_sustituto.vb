
Imports System.Data.SqlClient

Public Class clsLnProducto_sustituto

    Public Shared Sub Cargar(ByRef oBeProducto_sustituto As clsBeProducto_sustituto, ByRef dr As DataRow)

        Try

            With oBeProducto_sustituto
                .IdProductoSustituto = IIf(IsDBNull(dr.Item("IdProductoSustituto")), 0, dr.Item("IdProductoSustituto"))
                .IdProductoOriginal = IIf(IsDBNull(dr.Item("IdProductoOriginal")), 0, dr.Item("IdProductoOriginal"))
                .IdProductoPresentacionOriginal = IIf(IsDBNull(dr.Item("IdProductoPresentacionOriginal")), 0, dr.Item("IdProductoPresentacionOriginal"))
                .IdProductoReemplazo = IIf(IsDBNull(dr.Item("IdProductoReemplazo")), 0, dr.Item("IdProductoReemplazo"))
                .IdProductoPresentacionReemplazo = IIf(IsDBNull(dr.Item("IdProductoPresentacionReemplazo")), 0, dr.Item("IdProductoPresentacionReemplazo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_sustituto As clsBeProducto_sustituto, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("producto_sustituto")
            Ins.Add("idproductosustituto", "@idproductosustituto", DataType.Parametro)
            Ins.Add("idproductooriginal", "@idproductooriginal", DataType.Parametro)
            Ins.Add("idproductopresentacionoriginal", "@idproductopresentacionoriginal", DataType.Parametro)
            Ins.Add("idproductoreemplazo", "@idproductoreemplazo", DataType.Parametro)
            Ins.Add("idproductopresentacionreemplazo", "@idproductopresentacionreemplazo", DataType.Parametro)
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
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOSUSTITUTO", oBeProducto_sustituto.IdProductoSustituto))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOORIGINAL", oBeProducto_sustituto.IdProductoOriginal))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPRESENTACIONORIGINAL", oBeProducto_sustituto.IdProductoPresentacionOriginal))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOREEMPLAZO", oBeProducto_sustituto.IdProductoReemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPRESENTACIONREEMPLAZO", oBeProducto_sustituto.IdProductoPresentacionReemplazo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_sustituto.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_sustituto.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_sustituto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_sustituto.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_sustituto.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBeProducto_sustituto.IdProductoSustituto = CInt(cmd.Parameters("@IDPRODUCTOSUSTITUTO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_sustituto As clsBeProducto_sustituto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_sustituto")
            Upd.Add("idproductosustituto", "@idproductosustituto", DataType.Parametro)
            Upd.Add("idproductooriginal", "@idproductooriginal", DataType.Parametro)
            Upd.Add("idproductopresentacionoriginal", "@idproductopresentacionoriginal", DataType.Parametro)
            Upd.Add("idproductoreemplazo", "@idproductoreemplazo", DataType.Parametro)
            Upd.Add("idproductopresentacionreemplazo", "@idproductopresentacionreemplazo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdProductoSustituto = @IdProductoSustituto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOSUSTITUTO", oBeProducto_sustituto.IdProductoSustituto))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOORIGINAL", oBeProducto_sustituto.IdProductoOriginal))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPRESENTACIONORIGINAL", oBeProducto_sustituto.IdProductoPresentacionOriginal))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOREEMPLAZO", oBeProducto_sustituto.IdProductoReemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPRESENTACIONREEMPLAZO", oBeProducto_sustituto.IdProductoPresentacionReemplazo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_sustituto.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_sustituto.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_sustituto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_sustituto.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_sustituto.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeProducto_sustituto As clsBeProducto_sustituto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_sustituto" &
             "  Where(IdProductoSustituto = @IdProductoSustituto)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOSUSTITUTO", oBeProducto_sustituto.IdProductoSustituto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
            cmd.Dispose
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_sustituto"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeProducto_sustituto As clsBeProducto_sustituto) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Producto_sustituto" &
            " Where(IdProductoSustituto = @IdProductoSustituto)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOSUSTITUTO", oBeProducto_sustituto.IdProductoSustituto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_sustituto, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
