Imports System.Data.SqlClient

Public Class clsLnNotificacionDestinatarioRegla

    Public Shared Sub Cargar(ByRef oBeNotificacionDestinatarioRegla As clsBENotificacionDestinatarioRegla, ByRef dr As DataRow)
        Try
            With oBeNotificacionDestinatarioRegla
                .IdReglaDestinatario = IIf(IsDBNull(dr.Item("IdReglaDestinatario")), 0, dr.Item("IdReglaDestinatario"))
                .IdEvento = IIf(IsDBNull(dr.Item("IdEvento")), 0, dr.Item("IdEvento"))
                .TipoDestinatario = IIf(IsDBNull(dr.Item("TipoDestinatario")), "", dr.Item("TipoDestinatario"))
                .OrigenDestinatario = IIf(IsDBNull(dr.Item("OrigenDestinatario")), "", dr.Item("OrigenDestinatario"))
                .ValorOrigen = IIf(IsDBNull(dr.Item("ValorOrigen")), "", dr.Item("ValorOrigen"))
                .EmpresaCodigo = IIf(IsDBNull(dr.Item("EmpresaCodigo")), "", dr.Item("EmpresaCodigo"))
                .SucursalCodigo = IIf(IsDBNull(dr.Item("SucursalCodigo")), "", dr.Item("SucursalCodigo"))
                .TipoDocumento = IIf(IsDBNull(dr.Item("TipoDocumento")), "", dr.Item("TipoDocumento"))
                .ContextoBodega = IIf(IsDBNull(dr.Item("ContextoBodega")), "", dr.Item("ContextoBodega"))
                .CodigoBodegaFiltro = IIf(IsDBNull(dr.Item("CodigoBodegaFiltro")), "", dr.Item("CodigoBodegaFiltro"))
                .Prioridad = IIf(IsDBNull(dr.Item("Prioridad")), 0, dr.Item("Prioridad"))
                .RequiereCoincidenciaExacta = IIf(IsDBNull(dr.Item("RequiereCoincidenciaExacta")), False, dr.Item("RequiereCoincidenciaExacta"))
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

    Public Shared Function Insertar(ByRef oBeNotificacionDestinatarioRegla As clsBENotificacionDestinatarioRegla,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionDestinatarioRegla")
            Ins.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Ins.Add("TipoDestinatario", "@TipoDestinatario", DataType.Parametro)
            Ins.Add("OrigenDestinatario", "@OrigenDestinatario", DataType.Parametro)
            Ins.Add("ValorOrigen", "@ValorOrigen", DataType.Parametro)
            Ins.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Ins.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Ins.Add("TipoDocumento", "@TipoDocumento", DataType.Parametro)
            Ins.Add("ContextoBodega", "@ContextoBodega", DataType.Parametro)
            Ins.Add("CodigoBodegaFiltro", "@CodigoBodegaFiltro", DataType.Parametro)
            Ins.Add("Prioridad", "@Prioridad", DataType.Parametro)
            Ins.Add("RequiereCoincidenciaExacta", "@RequiereCoincidenciaExacta", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)
            Ins.Add("Observaciones", "@Observaciones", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Ins.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Ins.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdReglaDestinatario")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionDestinatarioRegla.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@TIPODESTINATARIO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.TipoDestinatario), DBNull.Value, oBeNotificacionDestinatarioRegla.TipoDestinatario)))
            cmd.Parameters.Add(New SqlParameter("@ORIGENDESTINATARIO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.OrigenDestinatario), DBNull.Value, oBeNotificacionDestinatarioRegla.OrigenDestinatario)))
            cmd.Parameters.Add(New SqlParameter("@VALORORIGEN", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.ValorOrigen), DBNull.Value, oBeNotificacionDestinatarioRegla.ValorOrigen)))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.EmpresaCodigo), DBNull.Value, oBeNotificacionDestinatarioRegla.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.SucursalCodigo), DBNull.Value, oBeNotificacionDestinatarioRegla.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPODOCUMENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.TipoDocumento), DBNull.Value, oBeNotificacionDestinatarioRegla.TipoDocumento)))
            cmd.Parameters.Add(New SqlParameter("@CONTEXTOBODEGA", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.ContextoBodega), DBNull.Value, oBeNotificacionDestinatarioRegla.ContextoBodega)))
            cmd.Parameters.Add(New SqlParameter("@CODIGOBODEGAFILTRO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.CodigoBodegaFiltro), DBNull.Value, oBeNotificacionDestinatarioRegla.CodigoBodegaFiltro)))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeNotificacionDestinatarioRegla.Prioridad))
            cmd.Parameters.Add(New SqlParameter("@REQUIERECOINCIDENCIAEXACTA", oBeNotificacionDestinatarioRegla.RequiereCoincidenciaExacta))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionDestinatarioRegla.Activo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.Observaciones), DBNull.Value, oBeNotificacionDestinatarioRegla.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionDestinatarioRegla.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.UsuarioCreacion), DBNull.Value, oBeNotificacionDestinatarioRegla.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionDestinatarioRegla.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionDestinatarioRegla.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.UsuarioModificacion), DBNull.Value, oBeNotificacionDestinatarioRegla.UsuarioModificacion)))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionDestinatarioRegla.IdReglaDestinatario = newId

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

    Public Shared Function Actualizar(ByRef oBeNotificacionDestinatarioRegla As clsBENotificacionDestinatarioRegla,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionDestinatarioRegla")
            Upd.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Upd.Add("TipoDestinatario", "@TipoDestinatario", DataType.Parametro)
            Upd.Add("OrigenDestinatario", "@OrigenDestinatario", DataType.Parametro)
            Upd.Add("ValorOrigen", "@ValorOrigen", DataType.Parametro)
            Upd.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Upd.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Upd.Add("TipoDocumento", "@TipoDocumento", DataType.Parametro)
            Upd.Add("ContextoBodega", "@ContextoBodega", DataType.Parametro)
            Upd.Add("CodigoBodegaFiltro", "@CodigoBodegaFiltro", DataType.Parametro)
            Upd.Add("Prioridad", "@Prioridad", DataType.Parametro)
            Upd.Add("RequiereCoincidenciaExacta", "@RequiereCoincidenciaExacta", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Add("Observaciones", "@Observaciones", DataType.Parametro)
            Upd.Add("FechaModificacion", "@FechaModificacion", DataType.Parametro)
            Upd.Add("UsuarioModificacion", "@UsuarioModificacion", DataType.Parametro)
            Upd.Where("IdReglaDestinatario = @IdReglaDestinatario")

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

            cmd.Parameters.Add(New SqlParameter("@IDREGLADESTINATARIO", oBeNotificacionDestinatarioRegla.IdReglaDestinatario))
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionDestinatarioRegla.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@TIPODESTINATARIO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.TipoDestinatario), DBNull.Value, oBeNotificacionDestinatarioRegla.TipoDestinatario)))
            cmd.Parameters.Add(New SqlParameter("@ORIGENDESTINATARIO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.OrigenDestinatario), DBNull.Value, oBeNotificacionDestinatarioRegla.OrigenDestinatario)))
            cmd.Parameters.Add(New SqlParameter("@VALORORIGEN", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.ValorOrigen), DBNull.Value, oBeNotificacionDestinatarioRegla.ValorOrigen)))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.EmpresaCodigo), DBNull.Value, oBeNotificacionDestinatarioRegla.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.SucursalCodigo), DBNull.Value, oBeNotificacionDestinatarioRegla.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPODOCUMENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.TipoDocumento), DBNull.Value, oBeNotificacionDestinatarioRegla.TipoDocumento)))
            cmd.Parameters.Add(New SqlParameter("@CONTEXTOBODEGA", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.ContextoBodega), DBNull.Value, oBeNotificacionDestinatarioRegla.ContextoBodega)))
            cmd.Parameters.Add(New SqlParameter("@CODIGOBODEGAFILTRO", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.CodigoBodegaFiltro), DBNull.Value, oBeNotificacionDestinatarioRegla.CodigoBodegaFiltro)))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeNotificacionDestinatarioRegla.Prioridad))
            cmd.Parameters.Add(New SqlParameter("@REQUIERECOINCIDENCIAEXACTA", oBeNotificacionDestinatarioRegla.RequiereCoincidenciaExacta))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionDestinatarioRegla.Activo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.Observaciones), DBNull.Value, oBeNotificacionDestinatarioRegla.Observaciones)))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", If(oBeNotificacionDestinatarioRegla.FechaModificacion = Date.MinValue, CType(DBNull.Value, Object), oBeNotificacionDestinatarioRegla.FechaModificacion)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", If(String.IsNullOrWhiteSpace(oBeNotificacionDestinatarioRegla.UsuarioModificacion), DBNull.Value, oBeNotificacionDestinatarioRegla.UsuarioModificacion)))

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

    Public Shared Function Eliminar(ByRef oBeNotificacionDestinatarioRegla As clsBENotificacionDestinatarioRegla,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionDestinatarioRegla WHERE (IdReglaDestinatario = @IdReglaDestinatario)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLADESTINATARIO", oBeNotificacionDestinatarioRegla.IdReglaDestinatario))

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
    IdReglaDestinatario,
    IdEvento,
    TipoDestinatario,
    OrigenDestinatario,
    ValorOrigen,
    EmpresaCodigo,
    SucursalCodigo,
    TipoDocumento,
    ContextoBodega,
    CodigoBodegaFiltro,
    Prioridad,
    RequiereCoincidenciaExacta,
    Activo,
    Observaciones,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionDestinatarioRegla
ORDER BY IdEvento, Prioridad, TipoDestinatario"

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

    Public Shared Function Obtener(ByRef oBeNotificacionDestinatarioRegla As clsBENotificacionDestinatarioRegla) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdReglaDestinatario,
    IdEvento,
    TipoDestinatario,
    OrigenDestinatario,
    ValorOrigen,
    EmpresaCodigo,
    SucursalCodigo,
    TipoDocumento,
    ContextoBodega,
    CodigoBodegaFiltro,
    Prioridad,
    RequiereCoincidenciaExacta,
    Activo,
    Observaciones,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionDestinatarioRegla
WHERE (IdReglaDestinatario = @IdReglaDestinatario)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLADESTINATARIO", oBeNotificacionDestinatarioRegla.IdReglaDestinatario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionDestinatarioRegla, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteRegla(ByVal pIdEvento As Integer,
                                ByVal pTipoDestinatario As String,
                                ByVal pOrigenDestinatario As String,
                                ByVal pValorOrigen As String,
                                ByVal pEmpresaCodigo As String,
                                ByVal pSucursalCodigo As String,
                                ByVal pTipoDocumento As String,
                                ByVal pContextoBodega As String,
                                ByVal pCodigoBodegaFiltro As String,
                                Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionDestinatarioRegla
WHERE IdEvento = @IdEvento
  AND TipoDestinatario = @TipoDestinatario
  AND OrigenDestinatario = @OrigenDestinatario
  AND ValorOrigen = @ValorOrigen
  AND ISNULL(EmpresaCodigo, '') = ISNULL(@EmpresaCodigo, '')
  AND ISNULL(SucursalCodigo, '') = ISNULL(@SucursalCodigo, '')
  AND ISNULL(TipoDocumento, '') = ISNULL(@TipoDocumento, '')
  AND ISNULL(ContextoBodega, '') = ISNULL(@ContextoBodega, '')
  AND ISNULL(CodigoBodegaFiltro, '') = ISNULL(@CodigoBodegaFiltro, '')
  AND (@IdExcluir = 0 OR IdReglaDestinatario <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))
            cmd.Parameters.Add(New SqlParameter("@TIPODESTINATARIO", pTipoDestinatario))
            cmd.Parameters.Add(New SqlParameter("@ORIGENDESTINATARIO", pOrigenDestinatario))
            cmd.Parameters.Add(New SqlParameter("@VALORORIGEN", pValorOrigen))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(pEmpresaCodigo), CType(DBNull.Value, Object), pEmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(pSucursalCodigo), CType(DBNull.Value, Object), pSucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPODOCUMENTO", If(String.IsNullOrWhiteSpace(pTipoDocumento), CType(DBNull.Value, Object), pTipoDocumento)))
            cmd.Parameters.Add(New SqlParameter("@CONTEXTOBODEGA", If(String.IsNullOrWhiteSpace(pContextoBodega), CType(DBNull.Value, Object), pContextoBodega)))
            cmd.Parameters.Add(New SqlParameter("@CODIGOBODEGAFILTRO", If(String.IsNullOrWhiteSpace(pCodigoBodegaFiltro), CType(DBNull.Value, Object), pCodigoBodegaFiltro)))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ResolverDestinatarios(ByVal pIdEvento As Integer,
                                                 ByVal pTipoDocumento As String,
                                                 ByVal pEmpresaCodigo As String,
                                                 ByVal pSucursalCodigo As String,
                                                 ByVal pIdBodegaOrigen As Integer,
                                                 ByVal pIdBodegaDestino As Integer) As DataTable
        Try
            Dim dtResultado As New DataTable()

            ' Crear estructura de la tabla resultado
            dtResultado.Columns.Add("IdReglaDestinatario", GetType(Integer))
            dtResultado.Columns.Add("TipoDestinatario", GetType(String))
            dtResultado.Columns.Add("ContextoBodega", GetType(String))
            dtResultado.Columns.Add("OrigenDestinatario", GetType(String))
            dtResultado.Columns.Add("ValorOrigen", GetType(String))
            dtResultado.Columns.Add("IdContacto", GetType(Integer))
            dtResultado.Columns.Add("NombreContacto", GetType(String))
            dtResultado.Columns.Add("CorreoContacto", GetType(String))
            dtResultado.Columns.Add("Prioridad", GetType(Integer))

            ' Obtener todas las reglas activas para el evento, ordenadas por prioridad
            Dim spReglas As String =
"SELECT
    IdReglaDestinatario,
    IdEvento,
    TipoDestinatario,
    OrigenDestinatario,
    ValorOrigen,
    EmpresaCodigo,
    SucursalCodigo,
    TipoDocumento,
    ContextoBodega,
    CodigoBodegaFiltro,
    Prioridad,
    RequiereCoincidenciaExacta,
    Activo,
    Observaciones,
    FechaCreacion,
    UsuarioCreacion,
    FechaModificacion,
    UsuarioModificacion
FROM NotificacionDestinatarioRegla
WHERE IdEvento = @IdEvento
  AND Activo = 1
ORDER BY Prioridad ASC, IdReglaDestinatario ASC"

            Dim dtReglas As New DataTable()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmdReglas As New SqlCommand(spReglas, lConnection)
                    cmdReglas.CommandType = CommandType.Text
                    cmdReglas.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))

                    Using dadReglas As New SqlDataAdapter(cmdReglas)
                        dadReglas.Fill(dtReglas)
                    End Using
                End Using

                ' Procesar cada regla
                For Each drRegla As DataRow In dtReglas.Rows
                    Dim oBeRegla As New clsBENotificacionDestinatarioRegla()
                    Cargar(oBeRegla, drRegla)

                    ' Verificar si la regla aplica según los parámetros
                    If AplicaRegla(oBeRegla, pTipoDocumento, pEmpresaCodigo, pSucursalCodigo, pIdBodegaOrigen, pIdBodegaDestino) Then
                        ' Obtener los contactos que cumplen con la regla
                        Dim dtContactos As DataTable = ObtenerContactosPorRegla(oBeRegla, pIdBodegaOrigen, pIdBodegaDestino)

                        ' Agregar los contactos al resultado
                        For Each drContacto As DataRow In dtContactos.Rows
                            Dim drNueva As DataRow = dtResultado.NewRow()
                            drNueva("IdReglaDestinatario") = oBeRegla.IdReglaDestinatario
                            drNueva("TipoDestinatario") = oBeRegla.TipoDestinatario
                            drNueva("ContextoBodega") = oBeRegla.ContextoBodega
                            drNueva("OrigenDestinatario") = oBeRegla.OrigenDestinatario
                            drNueva("ValorOrigen") = oBeRegla.ValorOrigen
                            drNueva("IdContacto") = drContacto("IdContacto")
                            drNueva("NombreContacto") = drContacto("Nombre")
                            drNueva("CorreoContacto") = drContacto("Correo")
                            drNueva("Prioridad") = oBeRegla.Prioridad
                            dtResultado.Rows.Add(drNueva)
                        Next
                    End If
                Next
            End Using

            ' Eliminar duplicados por correo y tipo destinatario (mantener el de mayor prioridad)
            Dim dtFinal As DataTable = EliminarDuplicados(dtResultado)

            Return dtFinal

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function AplicaRegla(ByVal oBeRegla As clsBENotificacionDestinatarioRegla,
                                        ByVal pTipoDocumento As String,
                                        ByVal pEmpresaCodigo As String,
                                        ByVal pSucursalCodigo As String,
                                        ByVal pIdBodegaOrigen As Integer,
                                        ByVal pIdBodegaDestino As Integer) As Boolean
        Try
            ' Validar TipoDocumento si la regla lo especifica
            If Not String.IsNullOrWhiteSpace(oBeRegla.TipoDocumento) Then
                If oBeRegla.TipoDocumento <> pTipoDocumento Then
                    Return False
                End If
            End If

            ' Validar EmpresaCodigo si la regla lo especifica
            If Not String.IsNullOrWhiteSpace(oBeRegla.EmpresaCodigo) Then
                If oBeRegla.EmpresaCodigo <> pEmpresaCodigo Then
                    Return False
                End If
            End If

            ' Validar SucursalCodigo si la regla lo especifica
            If Not String.IsNullOrWhiteSpace(oBeRegla.SucursalCodigo) Then
                If oBeRegla.SucursalCodigo <> pSucursalCodigo Then
                    Return False
                End If
            End If

            ' Validar contexto de bodega
            Select Case oBeRegla.ContextoBodega
                Case "ORIGEN"
                    ' Verificar que la regla aplica para la bodega de origen
                    If Not String.IsNullOrWhiteSpace(oBeRegla.CodigoBodegaFiltro) Then
                        Dim codigoBodegaOrigen As String = ObtenerCodigoBodegaPorId(pIdBodegaOrigen)
                        If oBeRegla.CodigoBodegaFiltro <> codigoBodegaOrigen Then
                            Return False
                        End If
                    End If

                Case "DESTINO"
                    ' Verificar que la regla aplica para la bodega de destino
                    If Not String.IsNullOrWhiteSpace(oBeRegla.CodigoBodegaFiltro) Then
                        Dim codigoBodegaDestino As String = ObtenerCodigoBodegaPorId(pIdBodegaDestino)
                        If oBeRegla.CodigoBodegaFiltro <> codigoBodegaDestino Then
                            Return False
                        End If
                    End If

                Case "AMBOS", ""
                    ' Aplica para ambas bodegas o sin filtro de bodega
                    ' Si tiene filtro de bodega, verificar que aplique a alguna de las dos
                    If Not String.IsNullOrWhiteSpace(oBeRegla.CodigoBodegaFiltro) Then
                        Dim codigoBodegaOrigen As String = ObtenerCodigoBodegaPorId(pIdBodegaOrigen)
                        Dim codigoBodegaDestino As String = ObtenerCodigoBodegaPorId(pIdBodegaDestino)

                        If oBeRegla.CodigoBodegaFiltro <> codigoBodegaOrigen AndAlso
                       oBeRegla.CodigoBodegaFiltro <> codigoBodegaDestino Then
                            Return False
                        End If
                    End If

                Case Else
                    ' Contexto no reconocido, no aplica
                    Return False
            End Select

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function ObtenerContactosPorRegla(ByVal oBeRegla As clsBENotificacionDestinatarioRegla,
                                                 ByVal pIdBodegaOrigen As Integer,
                                                 ByVal pIdBodegaDestino As Integer) As DataTable
        Try
            Dim dtContactos As New DataTable()
            Dim spContactos As String = String.Empty

            ' Determinar qué bodegas considerar según el contexto
            Select Case oBeRegla.ContextoBodega
                Case "ORIGEN"
                    spContactos =
"SELECT DISTINCT
    C.IdContacto,
    C.Nombre,
    C.Correo
FROM NotificacionContacto C
INNER JOIN NotificacionContactoBodega CB ON CB.IdContacto = C.IdContacto
WHERE C.Activo = 1
  AND CB.Activo = 1
  AND CB.IdBodega = @IdBodega
  AND C.Permite" & oBeRegla.TipoDestinatario & " = 1
ORDER BY C.Nombre"

                Case "DESTINO"
                    spContactos =
"SELECT DISTINCT
    C.IdContacto,
    C.Nombre,
    C.Correo
FROM NotificacionContacto C
INNER JOIN NotificacionContactoBodega CB ON CB.IdContacto = C.IdContacto
WHERE C.Activo = 1
  AND CB.Activo = 1
  AND CB.IdBodega = @IdBodega
  AND C.Permite" & oBeRegla.TipoDestinatario & " = 1
ORDER BY C.Nombre"

                Case "AMBOS", ""
                    spContactos =
"SELECT DISTINCT
    C.IdContacto,
    C.Nombre,
    C.Correo
FROM NotificacionContacto C
INNER JOIN NotificacionContactoBodega CB ON CB.IdContacto = C.IdContacto
WHERE C.Activo = 1
  AND CB.Activo = 1
  AND (CB.IdBodega = @IdBodegaOrigen OR CB.IdBodega = @IdBodegaDestino)
  AND C.Permite" & oBeRegla.TipoDestinatario & " = 1
ORDER BY C.Nombre"
            End Select

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand(spContactos, lConnection)
                    cmd.CommandType = CommandType.Text

                    Select Case oBeRegla.ContextoBodega
                        Case "ORIGEN"
                            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodegaOrigen))
                        Case "DESTINO"
                            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodegaDestino))
                        Case "AMBOS", ""
                            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", pIdBodegaOrigen))
                            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", pIdBodegaDestino))
                    End Select

                    Using dad As New SqlDataAdapter(cmd)
                        dad.Fill(dtContactos)
                    End Using
                End Using
            End Using

            Return dtContactos

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function EliminarDuplicados(ByVal dtContactos As DataTable) As DataTable
        Try
            ' Agrupar por Correo y TipoDestinatario, mantener el de menor prioridad (menor número = mayor prioridad)
            Dim dtFinal As DataTable = dtContactos.Clone()

            Dim contactosUnicos As New Dictionary(Of String, DataRow)()

            For Each dr As DataRow In dtContactos.Rows
                Dim clave As String = dr("CorreoContacto").ToString() & "|" & dr("TipoDestinatario").ToString()

                If Not contactosUnicos.ContainsKey(clave) Then
                    contactosUnicos.Add(clave, dr)
                Else
                    ' Si ya existe, comparar prioridades (menor número = mayor prioridad)
                    Dim drExistente As DataRow = contactosUnicos(clave)
                    If CInt(dr("Prioridad")) < CInt(drExistente("Prioridad")) Then
                        contactosUnicos(clave) = dr
                    End If
                End If
            Next

            ' Agregar los contactos únicos al resultado final
            For Each kvp As KeyValuePair(Of String, DataRow) In contactosUnicos
                dtFinal.ImportRow(kvp.Value)
            Next

            Return dtFinal

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function ObtenerCodigoBodegaPorId(ByVal pIdBodega As Integer) As String
        Try
            If pIdBodega <= 0 Then
                Return String.Empty
            End If

            Dim sp As String = "SELECT codigo FROM bodega WHERE IdBodega = @IdBodega"
            Dim codigoBodega As String = String.Empty

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand(sp, lConnection)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

                    lConnection.Open()
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado IsNot Nothing AndAlso Not IsDBNull(resultado) Then
                        codigoBodega = resultado.ToString()
                    End If
                End Using
            End Using

            Return codigoBodega

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class