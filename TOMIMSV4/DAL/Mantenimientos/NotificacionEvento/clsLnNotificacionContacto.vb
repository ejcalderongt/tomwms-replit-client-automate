Imports System.Data.SqlClient
Imports System.Configuration

Public Class clsLnNotificacionContacto

    Public Shared Sub Cargar(ByRef oBe As clsBENotificacionContacto, ByRef dr As DataRow)
        If dr Is Nothing Then Exit Sub

        oBe.IdContacto = If(IsDBNull(dr("IdContacto")), 0, dr("IdContacto"))
        oBe.Nombre = If(IsDBNull(dr("Nombre")), "", dr("Nombre"))
        oBe.Correo = If(IsDBNull(dr("Correo")), "", dr("Correo"))
        oBe.TipoContacto = If(IsDBNull(dr("TipoContacto")), "", dr("TipoContacto"))
        oBe.PermiteTo = If(IsDBNull(dr("PermiteTo")), False, dr("PermiteTo"))
        oBe.PermiteCc = If(IsDBNull(dr("PermiteCc")), False, dr("PermiteCc"))
        oBe.PermiteBcc = If(IsDBNull(dr("PermiteBcc")), False, dr("PermiteBcc"))
        oBe.EsPrincipal = If(IsDBNull(dr("EsPrincipal")), False, dr("EsPrincipal"))
        oBe.Activo = If(IsDBNull(dr("Activo")), False, dr("Activo"))
        oBe.Observaciones = If(IsDBNull(dr("Observaciones")), "", dr("Observaciones"))

        oBe.FechaCreacion = If(IsDBNull(dr("FechaCreacion")), Nothing, dr("FechaCreacion"))
        oBe.UsuarioCreacion = If(IsDBNull(dr("UsuarioCreacion")), "", dr("UsuarioCreacion"))
        oBe.FechaModificacion = If(IsDBNull(dr("FechaModificacion")), Nothing, dr("FechaModificacion"))
        oBe.UsuarioModificacion = If(IsDBNull(dr("UsuarioModificacion")), "", dr("UsuarioModificacion"))
    End Sub

    Public Shared Function Listar() As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Using da As New SqlDataAdapter("
                SELECT *
                FROM NotificacionContacto
                ORDER BY Nombre", cn)

                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function Obtener(ByRef oBe As clsBENotificacionContacto) As Boolean
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Using da As New SqlDataAdapter("
                SELECT *
                FROM NotificacionContacto
                WHERE IdContacto = " & oBe.IdContacto, cn)

                da.Fill(dt)
            End Using
        End Using

        If dt.Rows.Count = 0 Then Return False

        Cargar(oBe, dt.Rows(0))
        Return True
    End Function

    Public Shared Function Insertar(ByRef oBe As clsBENotificacionContacto,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As SqlConnection = Nothing
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            Ins.Init("NotificacionContacto")
            Ins.Add("Nombre", "@Nombre")
            Ins.Add("Correo", "@Correo")
            Ins.Add("TipoContacto", "@TipoContacto")
            Ins.Add("PermiteTo", "@PermiteTo")
            Ins.Add("PermiteCc", "@PermiteCc")
            Ins.Add("PermiteBcc", "@PermiteBcc")
            Ins.Add("EsPrincipal", "@EsPrincipal")
            Ins.Add("Activo", "@Activo")
            Ins.Add("Observaciones", "@Observaciones")
            Ins.Add("FechaCreacion", "@FechaCreacion")
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion")
            Ins.Add("FechaModificacion", "@FechaModificacion")
            Ins.Add("UsuarioModificacion", "@UsuarioModificacion")

            Dim sp As String = Ins.SQLIdentity("IdContacto")

            Dim esTransaccionRemota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If esTransaccionRemota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@Nombre", oBe.Nombre))
            cmd.Parameters.Add(New SqlParameter("@Correo", oBe.Correo))
            cmd.Parameters.Add(New SqlParameter("@TipoContacto", oBe.TipoContacto))
            cmd.Parameters.Add(New SqlParameter("@PermiteTo", oBe.PermiteTo))
            cmd.Parameters.Add(New SqlParameter("@PermiteCc", oBe.PermiteCc))
            cmd.Parameters.Add(New SqlParameter("@PermiteBcc", oBe.PermiteBcc))
            cmd.Parameters.Add(New SqlParameter("@EsPrincipal", oBe.EsPrincipal))
            cmd.Parameters.Add(New SqlParameter("@Activo", oBe.Activo))
            cmd.Parameters.Add(New SqlParameter("@Observaciones", If(String.IsNullOrEmpty(oBe.Observaciones), DBNull.Value, oBe.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FechaCreacion", oBe.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@UsuarioCreacion", oBe.UsuarioCreacion))
            cmd.Parameters.Add(New SqlParameter("@FechaModificacion", oBe.FechaModificacion))
            cmd.Parameters.Add(New SqlParameter("@UsuarioModificacion", oBe.UsuarioModificacion))

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBe.IdContacto = id

            If Not esTransaccionRemota Then lTransaction.Commit()

            Return id

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection IsNot Nothing Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                lConnection.Dispose()
            End If
        End Try
    End Function

    Public Shared Function Actualizar(ByRef oBe As clsBENotificacionContacto,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As SqlConnection = Nothing
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            Upd.Init("NotificacionContacto")

            Upd.Add("Nombre", "@Nombre")
            Upd.Add("Correo", "@Correo")
            Upd.Add("TipoContacto", "@TipoContacto")
            Upd.Add("PermiteTo", "@PermiteTo")
            Upd.Add("PermiteCc", "@PermiteCc")
            Upd.Add("PermiteBcc", "@PermiteBcc")
            Upd.Add("EsPrincipal", "@EsPrincipal")
            Upd.Add("Activo", "@Activo")
            Upd.Add("Observaciones", "@Observaciones")
            Upd.Add("FechaModificacion", "@FechaModificacion")
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion")

            Upd.Where("IdContacto = @IdContacto")

            Dim sp As String = Upd.SQL()

            Dim esTransaccionRemota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If esTransaccionRemota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdContacto", oBe.IdContacto))
            cmd.Parameters.Add(New SqlParameter("@Nombre", oBe.Nombre))
            cmd.Parameters.Add(New SqlParameter("@Correo", oBe.Correo))
            cmd.Parameters.Add(New SqlParameter("@TipoContacto", oBe.TipoContacto))
            cmd.Parameters.Add(New SqlParameter("@PermiteTo", oBe.PermiteTo))
            cmd.Parameters.Add(New SqlParameter("@PermiteCc", oBe.PermiteCc))
            cmd.Parameters.Add(New SqlParameter("@PermiteBcc", oBe.PermiteBcc))
            cmd.Parameters.Add(New SqlParameter("@EsPrincipal", oBe.EsPrincipal))
            cmd.Parameters.Add(New SqlParameter("@Activo", oBe.Activo))
            cmd.Parameters.Add(New SqlParameter("@Observaciones", If(String.IsNullOrEmpty(oBe.Observaciones), DBNull.Value, oBe.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FechaModificacion", oBe.FechaModificacion))
            cmd.Parameters.Add(New SqlParameter("@UsuarioModificacion", oBe.UsuarioModificacion))

            Dim rows As Integer = cmd.ExecuteNonQuery()

            If Not esTransaccionRemota Then lTransaction.Commit()

            Return rows

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection IsNot Nothing Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                lConnection.Dispose()
            End If
        End Try
    End Function

    Public Shared Function Eliminar(ByRef oBe As clsBENotificacionContacto) As Integer
        Using cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Using cmd As New SqlCommand("
                DELETE FROM NotificacionContacto
                WHERE IdContacto = @IdContacto", cn)

                cmd.Parameters.AddWithValue("@IdContacto", oBe.IdContacto)

                cn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Shared Function ExisteCorreo(ByVal correo As String, ByVal idExcluir As Integer) As Boolean
        Using cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Using cmd As New SqlCommand("
                SELECT COUNT(1)
                FROM NotificacionContacto
                WHERE Correo = @Correo
                  AND IdContacto <> @Id", cn)

                cmd.Parameters.AddWithValue("@Correo", correo)
                cmd.Parameters.AddWithValue("@Id", idExcluir)

                cn.Open()

                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

End Class