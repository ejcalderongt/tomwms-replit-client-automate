Imports System.Data.SqlClient
Public Class clsLnNotificacionPlantilla

    Public Shared Sub Cargar(ByRef oBeNotificacionPlantilla As clsBENotificacionPlantilla, ByRef dr As DataRow)
        Try
            With oBeNotificacionPlantilla
                .IdPlantilla = IIf(IsDBNull(dr.Item("IdPlantilla")), 0, dr.Item("IdPlantilla"))
                .IdEvento = IIf(IsDBNull(dr.Item("IdEvento")), 0, dr.Item("IdEvento"))
                .IdLayout = IIf(IsDBNull(dr.Item("IdLayout")), 0, dr.Item("IdLayout"))
                .CodigoPlantilla = IIf(IsDBNull(dr.Item("CodigoPlantilla")), "", dr.Item("CodigoPlantilla"))
                .NombrePlantilla = IIf(IsDBNull(dr.Item("NombrePlantilla")), "", dr.Item("NombrePlantilla"))
                .Canal = IIf(IsDBNull(dr.Item("Canal")), "", dr.Item("Canal"))
                .AsuntoTemplate = IIf(IsDBNull(dr.Item("AsuntoTemplate")), "", dr.Item("AsuntoTemplate"))
                .BodyHtmlTemplate = IIf(IsDBNull(dr.Item("BodyHtmlTemplate")), "", dr.Item("BodyHtmlTemplate"))
                .UsaLayoutComun = IIf(IsDBNull(dr.Item("UsaLayoutComun")), False, dr.Item("UsaLayoutComun"))
                .VersionPlantilla = IIf(IsDBNull(dr.Item("VersionPlantilla")), 0, dr.Item("VersionPlantilla"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), Date.Now, dr.Item("FechaCreacion"))
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion"))
                .FechaModificacion = IIf(IsDBNull(dr.Item("FechaModificacion")), Date.MinValue, dr.Item("FechaModificacion"))
                .UsuarioModificacion = IIf(IsDBNull(dr.Item("UsuarioModificacion")), "", dr.Item("UsuarioModificacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeNotificacionPlantilla As clsBENotificacionPlantilla,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionPlantilla")
            Ins.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Ins.Add("IdLayout", "@IdLayout", DataType.Parametro)
            Ins.Add("CodigoPlantilla", "@CodigoPlantilla", DataType.Parametro)
            Ins.Add("NombrePlantilla", "@NombrePlantilla", DataType.Parametro)
            Ins.Add("Canal", "@Canal", DataType.Parametro)
            Ins.Add("AsuntoTemplate", "@AsuntoTemplate", DataType.Parametro)
            Ins.Add("BodyHtmlTemplate", "@BodyHtmlTemplate", DataType.Parametro)
            Ins.Add("UsaLayoutComun", "@UsaLayoutComun", DataType.Parametro)
            Ins.Add("VersionPlantilla", "@VersionPlantilla", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Ins.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Ins.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdPlantilla")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionPlantilla.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", If(oBeNotificacionPlantilla.IdLayout <= 0, CType(DBNull.Value, Object), oBeNotificacionPlantilla.IdLayout)))
            cmd.Parameters.Add(New SqlParameter("@CODIGOPLANTILLA", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.CodigoPlantilla), DBNull.Value, oBeNotificacionPlantilla.CodigoPlantilla)))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPLANTILLA", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.NombrePlantilla), DBNull.Value, oBeNotificacionPlantilla.NombrePlantilla)))
            cmd.Parameters.Add(New SqlParameter("@CANAL", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.Canal), DBNull.Value, oBeNotificacionPlantilla.Canal)))
            cmd.Parameters.Add(New SqlParameter("@ASUNTOTEMPLATE", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.AsuntoTemplate), DBNull.Value, oBeNotificacionPlantilla.AsuntoTemplate)))
            cmd.Parameters.Add(New SqlParameter("@BODYHTMLTEMPLATE", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.BodyHtmlTemplate), DBNull.Value, oBeNotificacionPlantilla.BodyHtmlTemplate)))
            cmd.Parameters.Add(New SqlParameter("@USALAYOUTCOMUN", oBeNotificacionPlantilla.UsaLayoutComun))
            cmd.Parameters.Add(New SqlParameter("@VERSIONPLANTILLA", oBeNotificacionPlantilla.VersionPlantilla))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionPlantilla.Activo))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionPlantilla.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.UsuarioCreacion), DBNull.Value, oBeNotificacionPlantilla.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionPlantilla.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionPlantilla.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.UsuarioModificacion), DBNull.Value, oBeNotificacionPlantilla.UsuarioModificacion)))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionPlantilla.IdPlantilla = newId

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

    Public Shared Function Actualizar(ByRef oBeNotificacionPlantilla As clsBENotificacionPlantilla,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionPlantilla")
            Upd.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Upd.Add("IdLayout", "@IdLayout", DataType.Parametro)
            Upd.Add("CodigoPlantilla", "@CodigoPlantilla", DataType.Parametro)
            Upd.Add("NombrePlantilla", "@NombrePlantilla", DataType.Parametro)
            Upd.Add("Canal", "@Canal", DataType.Parametro)
            Upd.Add("AsuntoTemplate", "@AsuntoTemplate", DataType.Parametro)
            Upd.Add("BodyHtmlTemplate", "@BodyHtmlTemplate", DataType.Parametro)
            Upd.Add("UsaLayoutComun", "@UsaLayoutComun", DataType.Parametro)
            Upd.Add("VersionPlantilla", "@VersionPlantilla", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)
            Upd.Where("IdPlantilla = @IdPlantilla")

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

            cmd.Parameters.Add(New SqlParameter("@IDPLANTILLA", oBeNotificacionPlantilla.IdPlantilla))
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionPlantilla.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@IDLAYOUT", If(oBeNotificacionPlantilla.IdLayout <= 0, CType(DBNull.Value, Object), oBeNotificacionPlantilla.IdLayout)))
            cmd.Parameters.Add(New SqlParameter("@CODIGOPLANTILLA", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.CodigoPlantilla), DBNull.Value, oBeNotificacionPlantilla.CodigoPlantilla)))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPLANTILLA", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.NombrePlantilla), DBNull.Value, oBeNotificacionPlantilla.NombrePlantilla)))
            cmd.Parameters.Add(New SqlParameter("@CANAL", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.Canal), DBNull.Value, oBeNotificacionPlantilla.Canal)))
            cmd.Parameters.Add(New SqlParameter("@ASUNTOTEMPLATE", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.AsuntoTemplate), DBNull.Value, oBeNotificacionPlantilla.AsuntoTemplate)))
            cmd.Parameters.Add(New SqlParameter("@BODYHTMLTEMPLATE", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.BodyHtmlTemplate), DBNull.Value, oBeNotificacionPlantilla.BodyHtmlTemplate)))
            cmd.Parameters.Add(New SqlParameter("@USALAYOUTCOMUN", oBeNotificacionPlantilla.UsaLayoutComun))
            cmd.Parameters.Add(New SqlParameter("@VERSIONPLANTILLA", oBeNotificacionPlantilla.VersionPlantilla))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionPlantilla.Activo))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionPlantilla.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionPlantilla.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionPlantilla.UsuarioModificacion), DBNull.Value, oBeNotificacionPlantilla.UsuarioModificacion)))

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

    Public Shared Function Eliminar(ByRef oBeNotificacionPlantilla As clsBENotificacionPlantilla,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionPlantilla WHERE (IdPlantilla = @IdPlantilla)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPLANTILLA", oBeNotificacionPlantilla.IdPlantilla))

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
    IdPlantilla,
    IdEvento,
    IdLayout,
    CodigoPlantilla,
    NombrePlantilla,
    Canal,
    AsuntoTemplate,
    BodyHtmlTemplate,
    UsaLayoutComun,
    VersionPlantilla,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionPlantilla
ORDER BY CodigoPlantilla"

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

    Public Shared Function Obtener(ByRef oBeNotificacionPlantilla As clsBENotificacionPlantilla) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdPlantilla,
    IdEvento,
    IdLayout,
    CodigoPlantilla,
    NombrePlantilla,
    Canal,
    AsuntoTemplate,
    BodyHtmlTemplate,
    UsaLayoutComun,
    VersionPlantilla,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionPlantilla
WHERE (IdPlantilla = @IdPlantilla)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPLANTILLA", oBeNotificacionPlantilla.IdPlantilla))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionPlantilla, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteCodigo(ByVal pCodigoPlantilla As String, Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionPlantilla
WHERE CodigoPlantilla = @CodigoPlantilla
  AND (@IdExcluir = 0 OR IdPlantilla <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@CODIGOPLANTILLA", pCodigoPlantilla))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ObtenerPlantillaActivaPorEvento(ByVal pIdEvento As Integer,
                                                           ByVal pCanal As String,
                                                           Optional ByVal pVersionEspecifica As Integer = 0) As clsBENotificacionPlantilla
        Try
            Dim sp As String = String.Empty

            If pVersionEspecifica > 0 Then
                ' Obtener una versión específica
                sp =
"SELECT TOP 1
    IdPlantilla,
    IdEvento,
    IdLayout,
    CodigoPlantilla,
    NombrePlantilla,
    Canal,
    AsuntoTemplate,
    BodyHtmlTemplate,
    UsaLayoutComun,
    VersionPlantilla,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionPlantilla
WHERE IdEvento = @IdEvento
  AND Canal = @Canal
  AND Activo = 1
  AND VersionPlantilla = @VersionPlantilla"
            Else
                ' Obtener la versión más reciente
                sp =
"SELECT TOP 1
    IdPlantilla,
    IdEvento,
    IdLayout,
    CodigoPlantilla,
    NombrePlantilla,
    Canal,
    AsuntoTemplate,
    BodyHtmlTemplate,
    UsaLayoutComun,
    VersionPlantilla,
    Activo,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionPlantilla
WHERE IdEvento = @IdEvento
  AND Canal = @Canal
  AND Activo = 1
ORDER BY VersionPlantilla DESC"
            End If

            Dim oBeNotificacionPlantilla As New clsBENotificacionPlantilla()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand(sp, lConnection)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))
                    cmd.Parameters.Add(New SqlParameter("@CANAL", pCanal))

                    If pVersionEspecifica > 0 Then
                        cmd.Parameters.Add(New SqlParameter("@VERSIONPLANTILLA", pVersionEspecifica))
                    End If

                    Using dad As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        dad.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            Cargar(oBeNotificacionPlantilla, dt.Rows(0))
                            Return oBeNotificacionPlantilla
                        End If
                    End Using
                End Using
            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class