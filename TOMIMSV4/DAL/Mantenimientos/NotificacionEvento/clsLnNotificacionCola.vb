Imports System.Data.SqlClient

Public Class clsLnNotificacionCola

#Region "Métodos de Carga"

    Public Shared Sub Cargar(ByRef oBeNotificacionCola As clsBENotificacionCola, ByRef dr As DataRow)
        Try
            With oBeNotificacionCola
                .IdColaNotificacion = IIf(IsDBNull(dr.Item("IdColaNotificacion")), 0, Convert.ToInt64(dr.Item("IdColaNotificacion")))
                .IdEvento = IIf(IsDBNull(dr.Item("IdEvento")), 0, Convert.ToInt32(dr.Item("IdEvento")))
                .IdPlantilla = IIf(IsDBNull(dr.Item("IdPlantilla")), 0, Convert.ToInt32(dr.Item("IdPlantilla")))
                .OrigenSistema = IIf(IsDBNull(dr.Item("OrigenSistema")), "", dr.Item("OrigenSistema").ToString())
                .LlaveNegocio = IIf(IsDBNull(dr.Item("LlaveNegocio")), "", dr.Item("LlaveNegocio").ToString())
                .EmpresaCodigo = IIf(IsDBNull(dr.Item("EmpresaCodigo")), "", dr.Item("EmpresaCodigo").ToString())
                .SucursalCodigo = IIf(IsDBNull(dr.Item("SucursalCodigo")), "", dr.Item("SucursalCodigo").ToString())
                .BodegaCodigo = IIf(IsDBNull(dr.Item("BodegaCodigo")), "", dr.Item("BodegaCodigo").ToString())
                .TipoDocumento = IIf(IsDBNull(dr.Item("TipoDocumento")), "", dr.Item("TipoDocumento").ToString())
                .Referencia1 = IIf(IsDBNull(dr.Item("Referencia1")), "", dr.Item("Referencia1").ToString())
                .Referencia2 = IIf(IsDBNull(dr.Item("Referencia2")), "", dr.Item("Referencia2").ToString())
                .DataJson = IIf(IsDBNull(dr.Item("DataJson")), "", dr.Item("DataJson").ToString())
                .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado").ToString())
                .Intentos = IIf(IsDBNull(dr.Item("Intentos")), 0, Convert.ToInt32(dr.Item("Intentos")))
                .MaxIntentos = IIf(IsDBNull(dr.Item("MaxIntentos")), 3, Convert.ToInt32(dr.Item("MaxIntentos")))
                .FechaProgramada = IIf(IsDBNull(dr.Item("FechaProgramada")), DateTime.Now, Convert.ToDateTime(dr.Item("FechaProgramada")))
                .FechaUltimoIntento = IIf(IsDBNull(dr.Item("FechaUltimoIntento")), Nothing, Convert.ToDateTime(dr.Item("FechaUltimoIntento")))
                .FechaProcesado = IIf(IsDBNull(dr.Item("FechaProcesado")), Nothing, Convert.ToDateTime(dr.Item("FechaProcesado")))
                .ErrorUltimo = IIf(IsDBNull(dr.Item("ErrorUltimo")), "", dr.Item("ErrorUltimo").ToString())
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion").ToString())
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), DateTime.Now, Convert.ToDateTime(dr.Item("FechaCreacion")))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Métodos CRUD"

    Public Shared Function Insertar(ByRef oBeNotificacionCola As clsBENotificacionCola,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Long

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionCola")
            Ins.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Ins.Add("IdPlantilla", "@IdPlantilla", DataType.Parametro)
            Ins.Add("OrigenSistema", "@OrigenSistema", DataType.Parametro)
            Ins.Add("LlaveNegocio", "@LlaveNegocio", DataType.Parametro)
            Ins.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Ins.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Ins.Add("BodegaCodigo", "@BodegaCodigo", DataType.Parametro)
            Ins.Add("TipoDocumento", "@TipoDocumento", DataType.Parametro)
            Ins.Add("Referencia1", "@Referencia1", DataType.Parametro)
            Ins.Add("Referencia2", "@Referencia2", DataType.Parametro)
            Ins.Add("DataJson", "@DataJson", DataType.Parametro)
            Ins.Add("Estado", "@Estado", DataType.Parametro)
            Ins.Add("Intentos", "@Intentos", DataType.Parametro)
            Ins.Add("MaxIntentos", "@MaxIntentos", DataType.Parametro)
            Ins.Add("FechaProgramada", "@FechaProgramada", DataType.Parametro)
            Ins.Add("FechaUltimoIntento", "@FechaUltimoIntento", DataType.Parametro)
            Ins.Add("FechaProcesado", "@FechaProcesado", DataType.Parametro)
            Ins.Add("ErrorUltimo", "@ErrorUltimo", DataType.Parametro)
            Ins.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Ins.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdColaNotificacion")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionCola.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@IDPLANTILLA", If(oBeNotificacionCola.IdPlantilla <= 0, CType(DBNull.Value, Object), oBeNotificacionCola.IdPlantilla)))
            cmd.Parameters.Add(New SqlParameter("@ORIGENSISTEMA", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.OrigenSistema), DBNull.Value, oBeNotificacionCola.OrigenSistema)))
            cmd.Parameters.Add(New SqlParameter("@LLAVENEGOCIO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.LlaveNegocio), DBNull.Value, oBeNotificacionCola.LlaveNegocio)))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.EmpresaCodigo), DBNull.Value, oBeNotificacionCola.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.SucursalCodigo), DBNull.Value, oBeNotificacionCola.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@BODEGACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.BodegaCodigo), DBNull.Value, oBeNotificacionCola.BodegaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPODOCUMENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.TipoDocumento), DBNull.Value, oBeNotificacionCola.TipoDocumento)))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA1", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Referencia1), DBNull.Value, oBeNotificacionCola.Referencia1)))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA2", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Referencia2), DBNull.Value, oBeNotificacionCola.Referencia2)))
            cmd.Parameters.Add(New SqlParameter("@DATAJSON", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.DataJson), DBNull.Value, oBeNotificacionCola.DataJson)))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Estado), "PENDIENTE", oBeNotificacionCola.Estado)))
            cmd.Parameters.Add(New SqlParameter("@INTENTOS", oBeNotificacionCola.Intentos))
            cmd.Parameters.Add(New SqlParameter("@MAXINTENTOS", oBeNotificacionCola.MaxIntentos))
            cmd.Parameters.Add(New SqlParameter("@FECHAPROGRAMADA", oBeNotificacionCola.FechaProgramada))
            cmd.Parameters.Add(New SqlParameter("@FECHAULTIMOINTENTO", If(oBeNotificacionCola.FechaUltimoIntento.HasValue, oBeNotificacionCola.FechaUltimoIntento.Value, CType(DBNull.Value, Object))))
            cmd.Parameters.Add(New SqlParameter("@FECHAPROCESADO", If(oBeNotificacionCola.FechaProcesado.HasValue, oBeNotificacionCola.FechaProcesado.Value, CType(DBNull.Value, Object))))
            cmd.Parameters.Add(New SqlParameter("@ERRORULTIMO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.ErrorUltimo), DBNull.Value, oBeNotificacionCola.ErrorUltimo)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.UsuarioCreacion), DBNull.Value, oBeNotificacionCola.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionCola.FechaCreacion))

            Dim newId As Long = Convert.ToInt64(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionCola.IdColaNotificacion = newId

            Return newId

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

    Public Shared Function Actualizar(ByRef oBeNotificacionCola As clsBENotificacionCola,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionCola")
            Upd.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Upd.Add("IdPlantilla", "@IdPlantilla", DataType.Parametro)
            Upd.Add("OrigenSistema", "@OrigenSistema", DataType.Parametro)
            Upd.Add("LlaveNegocio", "@LlaveNegocio", DataType.Parametro)
            Upd.Add("EmpresaCodigo", "@EmpresaCodigo", DataType.Parametro)
            Upd.Add("SucursalCodigo", "@SucursalCodigo", DataType.Parametro)
            Upd.Add("BodegaCodigo", "@BodegaCodigo", DataType.Parametro)
            Upd.Add("TipoDocumento", "@TipoDocumento", DataType.Parametro)
            Upd.Add("Referencia1", "@Referencia1", DataType.Parametro)
            Upd.Add("Referencia2", "@Referencia2", DataType.Parametro)
            Upd.Add("DataJson", "@DataJson", DataType.Parametro)
            Upd.Add("Estado", "@Estado", DataType.Parametro)
            Upd.Add("Intentos", "@Intentos", DataType.Parametro)
            Upd.Add("MaxIntentos", "@MaxIntentos", DataType.Parametro)
            Upd.Add("FechaProgramada", "@FechaProgramada", DataType.Parametro)
            Upd.Add("FechaUltimoIntento", "@FechaUltimoIntento", DataType.Parametro)
            Upd.Add("FechaProcesado", "@FechaProcesado", DataType.Parametro)
            Upd.Add("ErrorUltimo", "@ErrorUltimo", DataType.Parametro)
            Upd.Add("UsuarioCreacion", "@UsuarioCreacion", DataType.Parametro)
            Upd.Add("FechaCreacion", "@FechaCreacion", DataType.Parametro)
            Upd.Where("IdColaNotificacion = @IdColaNotificacion")

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

            cmd.Parameters.Add(New SqlParameter("@IDCOLANOTIFICACION", oBeNotificacionCola.IdColaNotificacion))
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionCola.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@IDPLANTILLA", If(oBeNotificacionCola.IdPlantilla <= 0, CType(DBNull.Value, Object), oBeNotificacionCola.IdPlantilla)))
            cmd.Parameters.Add(New SqlParameter("@ORIGENSISTEMA", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.OrigenSistema), DBNull.Value, oBeNotificacionCola.OrigenSistema)))
            cmd.Parameters.Add(New SqlParameter("@LLAVENEGOCIO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.LlaveNegocio), DBNull.Value, oBeNotificacionCola.LlaveNegocio)))
            cmd.Parameters.Add(New SqlParameter("@EMPRESACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.EmpresaCodigo), DBNull.Value, oBeNotificacionCola.EmpresaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@SUCURSALCODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.SucursalCodigo), DBNull.Value, oBeNotificacionCola.SucursalCodigo)))
            cmd.Parameters.Add(New SqlParameter("@BODEGACODIGO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.BodegaCodigo), DBNull.Value, oBeNotificacionCola.BodegaCodigo)))
            cmd.Parameters.Add(New SqlParameter("@TIPODOCUMENTO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.TipoDocumento), DBNull.Value, oBeNotificacionCola.TipoDocumento)))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA1", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Referencia1), DBNull.Value, oBeNotificacionCola.Referencia1)))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA2", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Referencia2), DBNull.Value, oBeNotificacionCola.Referencia2)))
            cmd.Parameters.Add(New SqlParameter("@DATAJSON", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.DataJson), DBNull.Value, oBeNotificacionCola.DataJson)))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.Estado), "PENDIENTE", oBeNotificacionCola.Estado)))
            cmd.Parameters.Add(New SqlParameter("@INTENTOS", oBeNotificacionCola.Intentos))
            cmd.Parameters.Add(New SqlParameter("@MAXINTENTOS", oBeNotificacionCola.MaxIntentos))
            cmd.Parameters.Add(New SqlParameter("@FECHAPROGRAMADA", oBeNotificacionCola.FechaProgramada))
            cmd.Parameters.Add(New SqlParameter("@FECHAULTIMOINTENTO", If(oBeNotificacionCola.FechaUltimoIntento.HasValue, oBeNotificacionCola.FechaUltimoIntento.Value, CType(DBNull.Value, Object))))
            cmd.Parameters.Add(New SqlParameter("@FECHAPROCESADO", If(oBeNotificacionCola.FechaProcesado.HasValue, oBeNotificacionCola.FechaProcesado.Value, CType(DBNull.Value, Object))))
            cmd.Parameters.Add(New SqlParameter("@ERRORULTIMO", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.ErrorUltimo), DBNull.Value, oBeNotificacionCola.ErrorUltimo)))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", If(String.IsNullOrWhiteSpace(oBeNotificacionCola.UsuarioCreacion), DBNull.Value, oBeNotificacionCola.UsuarioCreacion)))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeNotificacionCola.FechaCreacion))

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

    Public Shared Function Eliminar(ByRef oBeNotificacionCola As clsBENotificacionCola,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionCola WHERE (IdColaNotificacion = @IdColaNotificacion)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCOLANOTIFICACION", oBeNotificacionCola.IdColaNotificacion))

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

    Public Shared Function Obtener(ByRef oBeNotificacionCola As clsBENotificacionCola) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdColaNotificacion,
    IdEvento,
    IdPlantilla,
    OrigenSistema,
    LlaveNegocio,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoDocumento,
    Referencia1,
    Referencia2,
    DataJson,
    Estado,
    Intentos,
    MaxIntentos,
    FechaProgramada,
    FechaUltimoIntento,
    FechaProcesado,
    ErrorUltimo,
    UsuarioCreacion,
    FechaCreacion
FROM NotificacionCola
WHERE (IdColaNotificacion = @IdColaNotificacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCOLANOTIFICACION", oBeNotificacionCola.IdColaNotificacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionCola, dt.Rows(0))
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Métodos Específicos de Cola"

    Public Shared Function ListarPendientes(ByVal pMaxRegistros As Integer) As DataTable
        Try
            Dim sp As String =
"SELECT TOP (@MaxRegistros)
    IdColaNotificacion,
    IdEvento,
    IdPlantilla,
    OrigenSistema,
    LlaveNegocio,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoDocumento,
    Referencia1,
    Referencia2,
    DataJson,
    Estado,
    Intentos,
    MaxIntentos,
    FechaProgramada,
    FechaUltimoIntento,
    FechaProcesado,
    ErrorUltimo,
    UsuarioCreacion,
    FechaCreacion
FROM NotificacionCola
WHERE Estado = 'PENDIENTE'
  AND FechaProgramada <= GETDATE()
ORDER BY FechaProgramada ASC, IdColaNotificacion ASC"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@MAXREGISTROS", pMaxRegistros))

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ListarPorEstado(ByVal pEstado As String) As DataTable
        Try
            Dim sp As String =
"SELECT
    IdColaNotificacion,
    IdEvento,
    IdPlantilla,
    OrigenSistema,
    LlaveNegocio,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoDocumento,
    Referencia1,
    Referencia2,
    DataJson,
    Estado,
    Intentos,
    MaxIntentos,
    FechaProgramada,
    FechaUltimoIntento,
    FechaProcesado,
    ErrorUltimo,
    UsuarioCreacion,
    FechaCreacion
FROM NotificacionCola
WHERE Estado = @Estado
ORDER BY FechaCreacion DESC"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@ESTADO", pEstado))

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ListarPorEvento(ByVal pIdEvento As Integer) As DataTable
        Try
            Dim sp As String =
"SELECT
    IdColaNotificacion,
    IdEvento,
    IdPlantilla,
    OrigenSistema,
    LlaveNegocio,
    EmpresaCodigo,
    SucursalCodigo,
    BodegaCodigo,
    TipoDocumento,
    Referencia1,
    Referencia2,
    DataJson,
    Estado,
    Intentos,
    MaxIntentos,
    FechaProgramada,
    FechaUltimoIntento,
    FechaProcesado,
    ErrorUltimo,
    UsuarioCreacion,
    FechaCreacion
FROM NotificacionCola
WHERE IdEvento = @IdEvento
ORDER BY FechaCreacion DESC"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ActualizarEstado(ByVal pIdColaNotificacion As Long,
                                            ByVal pEstado As String,
                                            Optional ByVal pError As String = "",
                                            Optional ByVal pConection As SqlConnection = Nothing,
                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            Dim sp As String = String.Empty

            Select Case pEstado.ToUpper()
                Case "PROCESADO"
                    sp = "UPDATE NotificacionCola SET Estado = @Estado, FechaProcesado = GETDATE() WHERE IdColaNotificacion = @IdColaNotificacion"
                Case "ERROR"
                    sp = "UPDATE NotificacionCola SET Estado = @Estado, ErrorUltimo = @Error, FechaUltimoIntento = GETDATE(), Intentos = Intentos + 1 WHERE IdColaNotificacion = @IdColaNotificacion"
                Case "PENDIENTE"
                    sp = "UPDATE NotificacionCola SET Estado = @Estado, ErrorUltimo = NULL, FechaProcesado = NULL WHERE IdColaNotificacion = @IdColaNotificacion"
                Case Else
                    sp = "UPDATE NotificacionCola SET Estado = @Estado WHERE IdColaNotificacion = @IdColaNotificacion"
            End Select

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDCOLANOTIFICACION", pIdColaNotificacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", pEstado))

            If pEstado.ToUpper() = "ERROR" Then
                cmd.Parameters.Add(New SqlParameter("@ERROR", If(String.IsNullOrWhiteSpace(pError), DBNull.Value, pError)))
            End If

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

    Public Shared Function RegistrarIntento(ByVal pIdColaNotificacion As Long,
                                           ByVal pError As String,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            Dim sp As String =
"UPDATE NotificacionCola 
SET Intentos = Intentos + 1,
    FechaUltimoIntento = GETDATE(),
    ErrorUltimo = @Error,
    Estado = CASE 
                WHEN Intentos + 1 >= MaxIntentos THEN 'FINALIZADO'
                ELSE 'ERROR'
             END
WHERE IdColaNotificacion = @IdColaNotificacion"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDCOLANOTIFICACION", pIdColaNotificacion))
            cmd.Parameters.Add(New SqlParameter("@ERROR", If(String.IsNullOrWhiteSpace(pError), DBNull.Value, pError)))

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

    Public Shared Function LimpiarCola(ByVal pDiasAntiguedad As Integer) As Integer
        Try
            Dim sp As String =
"DELETE FROM NotificacionCola 
WHERE Estado IN ('PROCESADO', 'FINALIZADO')
  AND FechaCreacion < DATEADD(DAY, -@DiasAntiguedad, GETDATE())"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@DIASANTIGUEDAD", pDiasAntiguedad))

            lConnection.Open()
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteEnCola(ByVal pOrigenSistema As String,
                                        ByVal pLlaveNegocio As String,
                                        ByVal pIdEvento As Integer) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionCola
WHERE OrigenSistema = @OrigenSistema
  AND LlaveNegocio = @LlaveNegocio
  AND IdEvento = @IdEvento
  AND Estado IN ('PENDIENTE', 'ERROR')"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@ORIGENSISTEMA", pOrigenSistema))
            cmd.Parameters.Add(New SqlParameter("@LLAVENEGOCIO", pLlaveNegocio))
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class