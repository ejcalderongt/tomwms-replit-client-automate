Imports System.Data.SqlClient
Public Class clsLnNotificacionEventoVariable

    Public Shared Sub Cargar(ByRef oBeNotificacionEventoVariable As clsBENotificacionEventoVariable, ByRef dr As DataRow)
        Try
            With oBeNotificacionEventoVariable
                .IdEventoVariable = IIf(IsDBNull(dr.Item("IdEventoVariable")), 0, dr.Item("IdEventoVariable"))
                .IdEvento = IIf(IsDBNull(dr.Item("IdEvento")), 0, dr.Item("IdEvento"))
                .NombreVariable = IIf(IsDBNull(dr.Item("NombreVariable")), "", dr.Item("NombreVariable"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .EjemploValor = IIf(IsDBNull(dr.Item("EjemploValor")), "", dr.Item("EjemploValor"))
                .Obligatoria = IIf(IsDBNull(dr.Item("Obligatoria")), False, dr.Item("Obligatoria"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeNotificacionEventoVariable As clsBENotificacionEventoVariable,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionEventoVariable")
            Ins.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Ins.Add("NombreVariable", "@NombreVariable", DataType.Parametro)
            Ins.Add("Descripcion", "@Descripcion", DataType.Parametro)
            Ins.Add("EjemploValor", "@EjemploValor", DataType.Parametro)
            Ins.Add("Obligatoria", "@Obligatoria", DataType.Parametro)
            Ins.Add("Activo", "@Activo", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdEventoVariable")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionEventoVariable.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@NOMBREVARIABLE", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.NombreVariable), DBNull.Value, oBeNotificacionEventoVariable.NombreVariable)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.Descripcion), DBNull.Value, oBeNotificacionEventoVariable.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@EJEMPLOVALOR", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.EjemploValor), DBNull.Value, oBeNotificacionEventoVariable.EjemploValor)))
            cmd.Parameters.Add(New SqlParameter("@OBLIGATORIA", oBeNotificacionEventoVariable.Obligatoria))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionEventoVariable.Activo))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeNotificacionEventoVariable.IdEventoVariable = newId

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

    Public Shared Function Actualizar(ByRef oBeNotificacionEventoVariable As clsBENotificacionEventoVariable,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionEventoVariable")
            Upd.Add("IdEvento", "@IdEvento", DataType.Parametro)
            Upd.Add("NombreVariable", "@NombreVariable", DataType.Parametro)
            Upd.Add("Descripcion", "@Descripcion", DataType.Parametro)
            Upd.Add("EjemploValor", "@EjemploValor", DataType.Parametro)
            Upd.Add("Obligatoria", "@Obligatoria", DataType.Parametro)
            Upd.Add("Activo", "@Activo", DataType.Parametro)
            Upd.Where("IdEventoVariable = @IdEventoVariable")

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

            cmd.Parameters.Add(New SqlParameter("@IDEVENTOVARIABLE", oBeNotificacionEventoVariable.IdEventoVariable))
            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", oBeNotificacionEventoVariable.IdEvento))
            cmd.Parameters.Add(New SqlParameter("@NOMBREVARIABLE", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.NombreVariable), DBNull.Value, oBeNotificacionEventoVariable.NombreVariable)))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.Descripcion), DBNull.Value, oBeNotificacionEventoVariable.Descripcion)))
            cmd.Parameters.Add(New SqlParameter("@EJEMPLOVALOR", If(String.IsNullOrWhiteSpace(oBeNotificacionEventoVariable.EjemploValor), DBNull.Value, oBeNotificacionEventoVariable.EjemploValor)))
            cmd.Parameters.Add(New SqlParameter("@OBLIGATORIA", oBeNotificacionEventoVariable.Obligatoria))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeNotificacionEventoVariable.Activo))

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

    Public Shared Function Eliminar(ByRef oBeNotificacionEventoVariable As clsBENotificacionEventoVariable,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionEventoVariable WHERE (IdEventoVariable = @IdEventoVariable)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEVENTOVARIABLE", oBeNotificacionEventoVariable.IdEventoVariable))

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
    IdEventoVariable,
    IdEvento,
    NombreVariable,
    Descripcion,
    EjemploValor,
    Obligatoria,
    Activo
FROM NotificacionEventoVariable
ORDER BY IdEvento, NombreVariable"

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

    Public Shared Function ListarPorEvento(ByVal pIdEvento As Integer) As DataTable
        Try
            Const sp As String =
"SELECT
    IdEventoVariable,
    IdEvento,
    NombreVariable,
    Descripcion,
    EjemploValor,
    Obligatoria,
    Activo
FROM NotificacionEventoVariable
WHERE IdEvento = @IdEvento
ORDER BY NombreVariable"

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

    Public Shared Function Obtener(ByRef oBeNotificacionEventoVariable As clsBENotificacionEventoVariable) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdEventoVariable,
    IdEvento,
    NombreVariable,
    Descripcion,
    EjemploValor,
    Obligatoria,
    Activo
FROM NotificacionEventoVariable
WHERE (IdEventoVariable = @IdEventoVariable)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEVENTOVARIABLE", oBeNotificacionEventoVariable.IdEventoVariable))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeNotificacionEventoVariable, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteVariable(ByVal pIdEvento As Integer,
                                   ByVal pNombreVariable As String,
                                   Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionEventoVariable
WHERE IdEvento = @IdEvento
  AND NombreVariable = @NombreVariable
  AND (@IdExcluir = 0 OR IdEventoVariable <> @IdExcluir)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEVENTO", pIdEvento))
            cmd.Parameters.Add(New SqlParameter("@NOMBREVARIABLE", pNombreVariable))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class