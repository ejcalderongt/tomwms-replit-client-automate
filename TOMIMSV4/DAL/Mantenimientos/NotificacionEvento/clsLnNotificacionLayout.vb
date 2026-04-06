Imports System.Data.SqlClient
Public Class clsLnNotificacionLayout

    Public Shared Sub Cargar(ByRef oBeNotificacionLayout As clsBENotificacionLayout, ByRef dr As DataRow)
        Try
            With oBeNotificacionLayout
                .IdLayout = IIf(IsDBNull(dr.Item("IdLayout")), 0, dr.Item("IdLayout"))
                .CodigoLayout = IIf(IsDBNull(dr.Item("CodigoLayout")), "", dr.Item("CodigoLayout"))
                .NombreLayout = IIf(IsDBNull(dr.Item("NombreLayout")), "", dr.Item("NombreLayout"))
                .HeaderHtml = IIf(IsDBNull(dr.Item("HeaderHtml")), "", dr.Item("HeaderHtml"))
                .FooterHtml = IIf(IsDBNull(dr.Item("FooterHtml")), "", dr.Item("FooterHtml"))
                .CssInline = IIf(IsDBNull(dr.Item("CssInline")), "", dr.Item("CssInline"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .EsDefault = IIf(IsDBNull(dr.Item("EsDefault")), False, dr.Item("EsDefault"))
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), Date.Now, dr.Item("FechaCreacion"))
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion"))
                .FechaModificacion = IIf(IsDBNull(dr.Item("FechaModificacion")), Date.MinValue, dr.Item("FechaModificacion"))
                .UsuarioModificacion = IIf(IsDBNull(dr.Item("UsuarioModificacion")), "", dr.Item("UsuarioModificacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeNotificacionLayout As clsBENotificacionLayout,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionLayout")
            Ins.Add("CodigoLayout", "@CodigoLayout", DataType.Parametro)
            Ins.Add("NombreLayout", "@NombreLayout", DataType.Parametro)
            Ins.Add("HeaderHtml", "@HeaderHtml", DataType.Parametro)
            Ins.Add("FooterHtml", "@FooterHtml", DataType.Parametro)
            Ins.Add("CssInline", "@CssInline", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)
            Ins.Add("EsDefault", "@EsDefault", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Ins.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Ins.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdLayout")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGOLAYOUT", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.CodigoLayout), DBNull.Value, oBeNotificacionLayout.CodigoLayout)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRELAYOUT", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.NombreLayout), DBNull.Value, oBeNotificacionLayout.NombreLayout)))
            cmd.Parameters.Add(New SqlParameter("@HEADERHTML", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.HeaderHtml), DBNull.Value, oBeNotificacionLayout.HeaderHtml)))
            cmd.Parameters.Add(New SqlParameter("@FOOTERHTML", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.FooterHtml), DBNull.Value, oBeNotificacionLayout.FooterHtml)))
            cmd.Parameters.Add(New SqlParameter("@CSSINLINE", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.CssInline), DBNull.Value, oBeNotificacionLayout.CssInline)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionLayout.Activo))
            cmd.Parameters.Add(New SqlParameter("@ESDEFAULT", oBeNotificacionLayout.EsDefault))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionLayout.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.UsuarioCreacion), DBNull.Value, oBeNotificacionLayout.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionLayout.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionLayout.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.UsuarioModificacion), DBNull.Value, oBeNotificacionLayout.UsuarioModificacion)))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionLayout.IdLayout = newId

            Return 1

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

    Public Shared Function Actualizar(ByRef oBeNotificacionLayout As clsBENotificacionLayout,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionLayout")
            Upd.Add("CodigoLayout", "@CodigoLayout", DataType.Parametro)
            Upd.Add("NombreLayout", "@NombreLayout", DataType.Parametro)
            Upd.Add("HeaderHtml", "@HeaderHtml", DataType.Parametro)
            Upd.Add("FooterHtml", "@FooterHtml", DataType.Parametro)
            Upd.Add("CssInline", "@CssInline", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Add("EsDefault", "@EsDefault", DataType.Parametro)
            Upd.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)
            Upd.Where("IdLayout = @IdLayout")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", oBeNotificacionLayout.IdLayout))
            cmd.Parameters.Add(New SqlParameter("@CODIGOLAYOUT", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.CodigoLayout), DBNull.Value, oBeNotificacionLayout.CodigoLayout)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRELAYOUT", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.NombreLayout), DBNull.Value, oBeNotificacionLayout.NombreLayout)))
            cmd.Parameters.Add(New SqlParameter("@HEADERHTML", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.HeaderHtml), DBNull.Value, oBeNotificacionLayout.HeaderHtml)))
            cmd.Parameters.Add(New SqlParameter("@FOOTERHTML", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.FooterHtml), DBNull.Value, oBeNotificacionLayout.FooterHtml)))
            cmd.Parameters.Add(New SqlParameter("@CSSINLINE", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.CssInline), DBNull.Value, oBeNotificacionLayout.CssInline)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionLayout.Activo))
            cmd.Parameters.Add(New SqlParameter("@ESDEFAULT", oBeNotificacionLayout.EsDefault))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionLayout.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionLayout.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionLayout.UsuarioModificacion), DBNull.Value, oBeNotificacionLayout.UsuarioModificacion)))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

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

    Public Shared Function Eliminar(ByRef oBeNotificacionLayout As clsBENotificacionLayout,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionLayout WHERE (IdLayout = @IdLayout)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", oBeNotificacionLayout.IdLayout))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

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

    Public Shared Function Listar() As DataTable
        Try
            Const sp As String =
"SELECT
    IdLayout,
    CodigoLayout,
    NombreLayout,
    HeaderHtml,
    FooterHtml,
    CssInline,
    Activo,
    EsDefault,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionLayout
ORDER BY CodigoLayout"

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

    Public Shared Function Obtener(ByRef oBeNotificacionLayout As clsBENotificacionLayout) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdLayout,
    CodigoLayout,
    NombreLayout,
    HeaderHtml,
    FooterHtml,
    CssInline,
    Activo,
    EsDefault,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionLayout
WHERE (IdLayout = @IdLayout)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDLAYOUT", oBeNotificacionLayout.IdLayout))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionLayout, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteCodigo(ByVal pCodigoLayout As String, Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionLayout
WHERE CodigoLayout = @CodigoLayout
  AND (@IdExcluir = 0 OR IdLayout <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@CODIGOLAYOUT", pCodigoLayout))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function QuitarDefaultOtros(ByVal pIdLayoutActual As Integer,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String =
                "UPDATE NotificacionLayout
                SET EsDefault = 0
                WHERE IdLayout <> @IdLayout"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", pIdLayoutActual))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

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

    Public Shared Function ObtenerPorId(ByVal pIdLayout As Integer) As clsBENotificacionLayout
        Try
            Dim sp As String =
                            "SELECT
                                IdLayout,
                                CodigoLayout,
                                NombreLayout,
                                HeaderHtml,
                                FooterHtml,
                                CssInline,
                                Activo,
                                EsDefault,
                                FechaCreacion,
                                UsuarioCreacion,
                                FechaModificacion,
                                UsuarioModificacion
                            FROM NotificacionLayout
                            WHERE IdLayout = @IdLayout"

            Dim oBeNotificacionLayout As New clsBENotificacionLayout()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand(sp, lConnection)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", pIdLayout))

                    Using dad As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        dad.Fill(dt)

                        If dt.Rows.Count = 1 Then
                            Cargar(oBeNotificacionLayout, dt.Rows(0))
                        ElseIf dt.Rows.Count = 0 Then
                            Throw New Exception($"No se encontró el layout con Id: {pIdLayout}")
                        Else
                            Throw New Exception($"Se encontraron múltiples layouts con el Id: {pIdLayout}")
                        End If
                    End Using
                End Using
            End Using

            Return oBeNotificacionLayout

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class