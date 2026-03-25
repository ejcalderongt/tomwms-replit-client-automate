Imports System.Data.SqlClient

Public Class clsLnBodegaNotificacionContacto

    Public Shared Sub Cargar(ByRef oBeBodegaNotificacionContacto As clsBEBodegaNotificacionContacto, ByRef dr As DataRow)
        Try
            With oBeBodegaNotificacionContacto
                .IdBodegaContacto = IIf(IsDBNull(dr.Item("IdBodegaContacto")), 0, dr.Item("IdBodegaContacto"))
                .EmpresaCodigo = IIf(IsDBNull(dr.Item("EmpresaCodigo")), "", dr.Item("EmpresaCodigo"))
                .SucursalCodigo = IIf(IsDBNull(dr.Item("SucursalCodigo")), "", dr.Item("SucursalCodigo"))
                .BodegaCodigo = IIf(IsDBNull(dr.Item("BodegaCodigo")), "", dr.Item("BodegaCodigo"))
                .TipoContacto = IIf(IsDBNull(dr.Item("TipoContacto")), "", dr.Item("TipoContacto"))
                .NombreContacto = IIf(IsDBNull(dr.Item("NombreContacto")), "", dr.Item("NombreContacto"))
                .Correo = IIf(IsDBNull(dr.Item("Correo")), "", dr.Item("Correo"))
                .PermiteTo = IIf(IsDBNull(dr.Item("PermiteTo")), False, dr.Item("PermiteTo"))
                .PermiteCc = IIf(IsDBNull(dr.Item("PermiteCc")), False, dr.Item("PermiteCc"))
                .PermiteBcc = IIf(IsDBNull(dr.Item("PermiteBcc")), False, dr.Item("PermiteBcc"))
                .EsPrincipal = IIf(IsDBNull(dr.Item("EsPrincipal")), False, dr.Item("EsPrincipal"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .Observaciones = IIf(IsDBNull(dr.Item("Observaciones")), "", dr.Item("Observaciones"))
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), Date.Now, dr.Item("FechaCreacion"))
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion"))
                .FechaModificacion = IIf(IsDBNull(dr.Item("FechaModificacion")), Date.MinValue, dr.Item("FechaModificacion"))
                .UsuarioModificacion = IIf(IsDBNull(dr.Item("UsuarioModificacion")), "", dr.Item("UsuarioModificacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeBodegaNotificacionContacto As clsBEBodegaNotificacionContacto,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("BodegaNotificacionContacto")
            Ins.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Ins.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Ins.Add("BodegaCodigo", "@BodegaCodigo", DataType.Parametro)
            Ins.Add("TipoContacto", "@TipoContacto", DataType.Parametro)
            Ins.Add("NombreContacto", "@NombreContacto", DataType.Parametro)
            Ins.Add("Correo", "@Correo", DataType.Parametro)
            Ins.Add("PermiteTo", "@PermiteTo", DataType.Parametro)
            Ins.Add("PermiteCc", "@PermiteCc", DataType.Parametro)
            Ins.Add("PermiteBcc", "@PermiteBcc", DataType.Parametro)
            Ins.Add("EsPrincipal", "@EsPrincipal", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)
            Ins.Add("Observaciones", "@Observaciones", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Ins.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Ins.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdBodegaContacto")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.EmpresaCodigo), DBNull.Value, oBeBodegaNotificacionContacto.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.SucursalCodigo), DBNull.Value, oBeBodegaNotificacionContacto.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@BODEGACODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.BodegaCodigo), DBNull.Value, oBeBodegaNotificacionContacto.BodegaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPOCONTACTO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.TipoContacto), DBNull.Value, oBeBodegaNotificacionContacto.TipoContacto)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRECONTACTO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.NombreContacto), DBNull.Value, oBeBodegaNotificacionContacto.NombreContacto)))
            cmd.Parameters.Add(New SqlParameter("@CORREO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.Correo), DBNull.Value, oBeBodegaNotificacionContacto.Correo)))
            cmd.Parameters.Add(New SqlParameter("@PERMITETO", oBeBodegaNotificacionContacto.PermiteTo))
            cmd.Parameters.Add(New SqlParameter("@PERMITECC", oBeBodegaNotificacionContacto.PermiteCc))
            cmd.Parameters.Add(New SqlParameter("@PERMITEBCC", oBeBodegaNotificacionContacto.PermiteBcc))
            cmd.Parameters.Add(New SqlParameter("@ESPRINCIPAL", oBeBodegaNotificacionContacto.EsPrincipal))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodegaNotificacionContacto.Activo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.Observaciones), DBNull.Value, oBeBodegaNotificacionContacto.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeBodegaNotificacionContacto.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.UsuarioCreacion), DBNull.Value, oBeBodegaNotificacionContacto.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeBodegaNotificacionContacto.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeBodegaNotificacionContacto.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.UsuarioModificacion), DBNull.Value, oBeBodegaNotificacionContacto.UsuarioModificacion)))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeBodegaNotificacionContacto.IdBodegaContacto = newId

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

    Public Shared Function Actualizar(ByRef oBeBodegaNotificacionContacto As clsBEBodegaNotificacionContacto,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("BodegaNotificacionContacto")
            Upd.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Upd.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Upd.Add("BodegaCodigo", "@BodegaCodigo", DataType.Parametro)
            Upd.Add("TipoContacto", "@TipoContacto", DataType.Parametro)
            Upd.Add("NombreContacto", "@NombreContacto", DataType.Parametro)
            Upd.Add("Correo", "@Correo", DataType.Parametro)
            Upd.Add("PermiteTo", "@PermiteTo", DataType.Parametro)
            Upd.Add("PermiteCc", "@PermiteCc", DataType.Parametro)
            Upd.Add("PermiteBcc", "@PermiteBcc", DataType.Parametro)
            Upd.Add("EsPrincipal", "@EsPrincipal", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Add("Observaciones", "@Observaciones", DataType.Parametro)
            Upd.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)
            Upd.Where("IdBodegaContacto = @IdBodegaContacto")

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

            cmd.Parameters.Add(New SqlParameter("@IDBODEGACONTACTO", oBeBodegaNotificacionContacto.IdBodegaContacto))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.EmpresaCodigo), DBNull.Value, oBeBodegaNotificacionContacto.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.SucursalCodigo), DBNull.Value, oBeBodegaNotificacionContacto.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@BODEGACODIGO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.BodegaCodigo), DBNull.Value, oBeBodegaNotificacionContacto.BodegaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPOCONTACTO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.TipoContacto), DBNull.Value, oBeBodegaNotificacionContacto.TipoContacto)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRECONTACTO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.NombreContacto), DBNull.Value, oBeBodegaNotificacionContacto.NombreContacto)))
            cmd.Parameters.Add(New SqlParameter("@CORREO", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.Correo), DBNull.Value, oBeBodegaNotificacionContacto.Correo)))
            cmd.Parameters.Add(New SqlParameter("@PERMITETO", oBeBodegaNotificacionContacto.PermiteTo))
            cmd.Parameters.Add(New SqlParameter("@PERMITECC", oBeBodegaNotificacionContacto.PermiteCc))
            cmd.Parameters.Add(New SqlParameter("@PERMITEBCC", oBeBodegaNotificacionContacto.PermiteBcc))
            cmd.Parameters.Add(New SqlParameter("@ESPRINCIPAL", oBeBodegaNotificacionContacto.EsPrincipal))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodegaNotificacionContacto.Activo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.Observaciones), DBNull.Value, oBeBodegaNotificacionContacto.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeBodegaNotificacionContacto.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeBodegaNotificacionContacto.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeBodegaNotificacionContacto.UsuarioModificacion), DBNull.Value, oBeBodegaNotificacionContacto.UsuarioModificacion)))

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

    Public Shared Function Eliminar(ByRef oBeBodegaNotificacionContacto As clsBEBodegaNotificacionContacto,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM BodegaNotificacionContacto WHERE (IdBodegaContacto = @IdBodegaContacto)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGACONTACTO", oBeBodegaNotificacionContacto.IdBodegaContacto))

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
    IdBodegaContacto,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoContacto,
    NombreContacto,
    Correo,
    PermiteTo,
    PermiteCc,
    PermiteBcc,
    EsPrincipal,
    Activo,
    Observaciones,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM BodegaNotificacionContacto
ORDER BY EmpresaCodigo, SucursalCodigo, BodegaCodigo, TipoContacto, NombreContacto"

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

    Public Shared Function Obtener(ByRef oBeBodegaNotificacionContacto As clsBEBodegaNotificacionContacto) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdBodegaContacto,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoContacto,
    NombreContacto,
    Correo,
    PermiteTo,
    PermiteCc,
    PermiteBcc,
    EsPrincipal,
    Activo,
    Observaciones,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM BodegaNotificacionContacto
WHERE (IdBodegaContacto = @IdBodegaContacto)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGACONTACTO", oBeBodegaNotificacionContacto.IdBodegaContacto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodegaNotificacionContacto, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteContacto(ByVal pEmpresaCodigo As String,
                                   ByVal pSucursalCodigo As String,
                                   ByVal pBodegaCodigo As String,
                                   ByVal pTipoContacto As String,
                                   ByVal pCorreo As String,
                                   Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM BodegaNotificacionContacto
WHERE EmpresaCodigo = @EmpresaCodigo
  AND ISNULL(SucursalCodigo, '') = ISNULL(@SucursalCodigo, '')
  AND BodegaCodigo = @BodegaCodigo
  AND TipoContacto = @TipoContacto
  AND Correo = @Correo
  AND (@IdExcluir = 0 OR IdBodegaContacto <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", pEmpresaCodigo))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(pSucursalCodigo), CType(DBNull.Value, Object), pSucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@BODEGACODIGO", pBodegaCodigo))
            cmd.Parameters.Add(New SqlParameter("@TIPOCONTACTO", pTipoContacto))
            cmd.Parameters.Add(New SqlParameter("@CORREO", pCorreo))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class