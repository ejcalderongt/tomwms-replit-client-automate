Imports System.Data.SqlClient

Public Class clsLnProducto_codigos_barra

    Public Shared Sub Cargar(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra, ByRef dr As DataRow)
        Try
            With oBeProducto_codigos_barra
                .IdProductoCodigoBarra = IIf(IsDBNull(dr.Item("IdProductoCodigoBarra")), 0, dr.Item("IdProductoCodigoBarra"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdColor = IIf(IsDBNull(dr.Item("IdColor")), 0, dr.Item("IdColor"))
                .IdTalla = IIf(IsDBNull(dr.Item("IdTalla")), 0, dr.Item("IdTalla"))
            End With
        Catch ex As Exception
            Throw New Exception("Producto_codigos_barra_Cargar: " & ex.message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("producto_codigos_barra")
            Ins.Add("idproductocodigobarra", "@idproductocodigobarra", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idcolor", "@idcolor", DataType.Parametro)
            Ins.Add("idtalla", "@idtalla", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOCODIGOBARRA", oBeProducto_codigos_barra.IdProductoCodigoBarra))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_codigos_barra.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProducto_codigos_barra.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_codigos_barra.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_codigos_barra.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_codigos_barra.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_codigos_barra.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_codigos_barra.User_agr))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_codigos_barra.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_codigos_barra.IdColor))
            cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_codigos_barra.IdTalla))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Producto_codigos_barra_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("producto_codigos_barra")
            Upd.Add("idproductocodigobarra", "@idproductocodigobarra", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idcolor", "@idcolor", DataType.Parametro)
            Upd.Add("idtalla", "@idtalla", DataType.Parametro)
            Upd.Where("IdProductoCodigoBarra = @IdProductoCodigoBarra")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOCODIGOBARRA", oBeProducto_codigos_barra.IdProductoCodigoBarra))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_codigos_barra.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProducto_codigos_barra.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_codigos_barra.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_codigos_barra.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_codigos_barra.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_codigos_barra.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_codigos_barra.User_agr))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_codigos_barra.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_codigos_barra.IdColor))
            cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_codigos_barra.IdTalla))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Producto_codigos_barra_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra,
                             Optional ByVal pConection As SqlConnection = Nothing,
                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Producto_codigos_barra" &
             "  Where(IdProductoCodigoBarra = @IdProductoCodigoBarra)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOCODIGOBARRA", oBeProducto_codigos_barra.IdProductoCodigoBarra))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Producto_codigos_barra_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_codigos_barra"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("Producto_codigos_barra_Listar: " & ex.message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Producto_codigos_barra" &
            " Where(IdProductoCodigoBarra = @IdProductoCodigoBarra)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOCODIGOBARRA", oBeProducto_codigos_barra.IdProductoCodigoBarra))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_codigos_barra, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
