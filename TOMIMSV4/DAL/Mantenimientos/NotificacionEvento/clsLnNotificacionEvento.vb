Imports System.Data.SqlClient

Public Class clsLnNotificacionEvento

    Public Shared Sub Cargar(ByRef oBeNotificacionEvento As clsBENotificacionEvento, ByRef dr As DataRow)
        Try
            With oBeNotificacionEvento
                .IdEvento = IIf(IsDBNull(dr.Item("IdEvento")), 0, dr.Item("IdEvento"))
                .CodigoEvento = IIf(IsDBNull(dr.Item("CodigoEvento")), "", dr.Item("CodigoEvento"))
                .NombreEvento = IIf(IsDBNull(dr.Item("NombreEvento")), "", dr.Item("NombreEvento"))
                .Modulo = IIf(IsDBNull(dr.Item("Modulo")), "", dr.Item("Modulo"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), Date.Now, dr.Item("FechaCreacion"))
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion"))
                .FechaModificacion = IIf(IsDBNull(dr.Item("FechaModificacion")), CType(Nothing, Nullable(Of DateTime)), dr.Item("FechaModificacion"))
                .UsuarioModificacion = IIf(IsDBNull(dr.Item("UsuarioModificacion")), "", dr.Item("UsuarioModificacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeNotificacionEvento As clsBENotificacionEvento,
                                Optional ByVal pConection As SqlConnection = Nothing,
                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionEvento")
            Ins.Add("CodigoEvento", "@CodigoEvento", DataType.Parametro)
            Ins.Add("NombreEvento", "@NombreEvento", DataType.Parametro)
            Ins.Add("Modulo", "@Modulo", DataType.Parametro)
            Ins.Add("Descripcion", "@Descripcion", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdEvento")

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGOEVENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.CodigoEvento), DBNull.Value, oBeNotificacionEvento.CodigoEvento)))
            cmd.Parameters.Add(New SqlParameter("@NOMBREEVENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.NombreEvento), DBNull.Value, oBeNotificacionEvento.NombreEvento)))
            cmd.Parameters.Add(New SqlParameter("@MODULO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.Modulo), DBNull.Value, oBeNotificacionEvento.Modulo)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.Descripcion), DBNull.Value, oBeNotificacionEvento.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionEvento.Activo))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionEvento.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.UsuarioCreacion), DBNull.Value, oBeNotificacionEvento.UsuarioCreacion)))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionEvento.IdEvento = newId

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

    Public Shared Function Actualizar(ByRef oBeNotificacionEvento As clsBENotificacionEvento,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionEvento")
            Upd.Add("CodigoEvento", "@CodigoEvento", DataType.Parametro)
            Upd.Add("NombreEvento", "@NombreEvento", DataType.Parametro)
            Upd.Add("Modulo", "@Modulo", DataType.Parametro)
            Upd.Add("Descripcion", "@Descripcion", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)
            Upd.Where("IdEvento = @IdEvento")

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

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionEvento.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@CODIGOEVENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.CodigoEvento), DBNull.Value, oBeNotificacionEvento.CodigoEvento)))
            cmd.Parameters.Add(New SqlParameter("@NOMBREEVENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.NombreEvento), DBNull.Value, oBeNotificacionEvento.NombreEvento)))
            cmd.Parameters.Add(New SqlParameter("@MODULO", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.Modulo), DBNull.Value, oBeNotificacionEvento.Modulo)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.Descripcion), DBNull.Value, oBeNotificacionEvento.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionEvento.Activo))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION",
            If(oBeNotificacionEvento.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), CType(oBeNotificacionEvento.FechaModificacion, Object))))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionEvento.UsuarioModificacion), DBNull.Value, oBeNotificacionEvento.UsuarioModificacion)))

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

    Public Shared Function Eliminar(ByRef oBeNotificacionEvento As clsBENotificacionEvento,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionEvento WHERE (IdEvento = @IdEvento)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionEvento.IdEvento))

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
    IdEvento,
    CodigoEvento,
    NombreEvento,
    Modulo,
    Descripcion,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionEvento
ORDER BY CodigoEvento"

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

    Public Shared Function Obtener(ByRef oBeNotificacionEvento As clsBENotificacionEvento) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdEvento,
    CodigoEvento,
    NombreEvento,
    Modulo,
    Descripcion,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionEvento
WHERE (IdEvento = @IdEvento)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionEvento.IdEvento))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionEvento, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteCodigo(ByVal pCodigoEvento As String, Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionEvento
WHERE CodigoEvento = @CodigoEvento
  AND (@IdExcluir = 0 OR IdEvento <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@CODIGOEVENTO", pCodigoEvento))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function TieneDependencias(ByVal pIdEvento As Integer) As Boolean
        Try
            Dim sp As String =
"SELECT CASE
    WHEN EXISTS (SELECT 1 FROM NotificacionPlantilla WHERE IdEvento = @IdEvento) THEN 1
    WHEN EXISTS (SELECT 1 FROM NotificacionEventoVariable WHERE IdEvento = @IdEvento) THEN 1
    WHEN EXISTS (SELECT 1 FROM NotificacionDestinatarioRegla WHERE IdEvento = @IdEvento) THEN 1
    WHEN EXISTS (SELECT 1 FROM NotificacionCola WHERE IdEvento = @IdEvento) THEN 1
    WHEN EXISTS (SELECT 1 FROM NotificacionLog WHERE IdEvento = @IdEvento) THEN 1
    ELSE 0
END"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) = 1

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ObtenerPorCodigo(ByVal pCodigoEvento As String) As clsBENotificacionEvento
        Try
            If String.IsNullOrWhiteSpace(pCodigoEvento) Then
                Return Nothing
            End If

            Dim sp As String = "SELECT *
                                FROM NotificacionEvento
                                WHERE CodigoEvento = @CodigoEvento"

            Dim oBeEvento As New clsBENotificacionEvento()

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGOEVENTO", pCodigoEvento))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Cargar(oBeEvento, dt.Rows(0))
                Return oBeEvento
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class