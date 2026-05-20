Imports System.Data.SqlClient
Imports System.Configuration

Public Class clsLnNotificacionContactoBodega

    Public Shared Sub Cargar(ByRef oBe As clsBENotificacionContactoBodega, ByRef dr As DataRow)
        Try
            With oBe
                .IdContactoBodega = IIf(IsDBNull(dr.Item("IdContactoBodega")), 0, dr.Item("IdContactoBodega"))
                .IdContacto = IIf(IsDBNull(dr.Item("IdContacto")), 0, dr.Item("IdContacto"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
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

    Public Shared Function Insertar(ByRef oBe As clsBENotificacionContactoBodega,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Ins.Init("NotificacionContactoBodega")
            Ins.Add("IdContacto", "@IdContacto", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("IdContactoBodega")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONTACTO", oBe.IdContacto))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBe.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", If(String.IsNullOrWhiteSpace(oBe.User_agr), CType(DBNull.Value, Object), oBe.User_agr)))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBe.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", If(String.IsNullOrWhiteSpace(oBe.User_mod), CType(DBNull.Value, Object), oBe.User_mod)))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", If(oBe.Fec_mod = Date.MinValue, CType(DBNull.Value, Object), oBe.Fec_mod)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBe.Activo))

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBe.IdContactoBodega = newId

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

    Public Shared Function Actualizar(ByRef oBe As clsBENotificacionContactoBodega,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try
            Upd.Init("NotificacionContactoBodega")
            Upd.Add("IdContacto", "@IdContacto", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdContactoBodega = @IdContactoBodega")

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

            cmd.Parameters.Add(New SqlParameter("@IDCONTACTOBODEGA", oBe.IdContactoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDCONTACTO", oBe.IdContacto))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBe.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", If(String.IsNullOrWhiteSpace(oBe.User_mod), CType(DBNull.Value, Object), oBe.User_mod)))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", If(oBe.Fec_mod = Date.MinValue, CType(DBNull.Value, Object), oBe.Fec_mod)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBe.Activo))

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

    Public Shared Function Eliminar(ByRef oBe As clsBENotificacionContactoBodega,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionContactoBodega WHERE (IdContactoBodega = @IdContactoBodega)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONTACTOBODEGA", oBe.IdContactoBodega))

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

    Public Shared Function EliminarPorContacto(ByVal pIdContacto As Integer,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try
            cmd.CommandType = CommandType.Text

            Dim sp As String = "DELETE FROM NotificacionContactoBodega WHERE (IdContacto = @IdContacto)"
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONTACTO", pIdContacto))

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
    IdContactoBodega,
    IdContacto,
    IdBodega,
    user_agr,
    fec_agr,
    user_mod,
    fec_mod,
    activo
FROM NotificacionContactoBodega
ORDER BY IdContacto, IdBodega"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ListarPorContacto(ByVal pIdContacto As Integer) As DataTable
        Try
            Const sp As String =
"SELECT
    IdContactoBodega,
    IdContacto,
    IdBodega,
    user_agr,
    fec_agr,
    user_mod,
    fec_mod,
    activo
FROM NotificacionContactoBodega
WHERE IdContacto = @IdContacto
ORDER BY IdBodega"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IDCONTACTO", pIdContacto))

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Obtener(ByRef oBe As clsBENotificacionContactoBodega) As Boolean
        Try
            Dim sp As String =
"SELECT
    IdContactoBodega,
    IdContacto,
    IdBodega,
    user_agr,
    fec_agr,
    user_mod,
    fec_mod,
    activo
FROM NotificacionContactoBodega
WHERE (IdContactoBodega = @IdContactoBodega)"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONTACTOBODEGA", oBe.IdContactoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBe, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExisteAsignacion(ByVal pIdContacto As Integer,
                                            ByVal pIdBodega As Integer,
                                            Optional ByVal pIdExcluir As Integer = 0) As Boolean
        Try
            Dim sp As String =
"SELECT COUNT(1)
FROM NotificacionContactoBodega
WHERE IdContacto = @IdContacto
  AND IdBodega = @IdBodega
  AND (@IdExcluir = 0 OR IdContactoBodega <> @IdExcluir)"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDCONTACTO", pIdContacto))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDEXCLUIR", pIdExcluir))

            lConnection.Open()

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_All_Bodegas_By_IdContacto(ByVal pIdContacto As Integer) As List(Of clsBENotificacionContactoBodega)

        Dim lList As New List(Of clsBENotificacionContactoBodega)

        Try
            Dim sp As String =
            "SELECT " &
            "   IdContactoBodega, " &
            "   IdContacto, " &
            "   IdBodega, " &
            "   user_agr, " &
            "   fec_agr, " &
            "   user_mod, " &
            "   fec_mod, " &
            "   activo " &
            "FROM NotificacionContactoBodega " &
            "WHERE IdContacto = @IdContacto " &
            "ORDER BY IdBodega"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONTACTO", pIdContacto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim oBe As New clsBENotificacionContactoBodega
                    Cargar(oBe, dr)
                    lList.Add(oBe)
                Next
            End If

            Return lList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarAsignaciones(ByVal pIdContacto As Integer,
                                          ByVal pDtBodegas As DataTable,
                                          ByVal pUsuario As String)

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            EliminarPorContacto(pIdContacto, lConnection, lTransaction)

            If pDtBodegas IsNot Nothing AndAlso pDtBodegas.Rows.Count > 0 Then
                For Each dr As DataRow In pDtBodegas.Rows

                    Dim asignado As Boolean = False

                    If pDtBodegas.Columns.Contains("Seleccion") AndAlso Not IsDBNull(dr("Seleccion")) Then
                        asignado = Convert.ToBoolean(dr("Seleccion"))
                    End If

                    If asignado Then
                        Dim oBe As New clsBENotificacionContactoBodega With {
                            .IdContacto = pIdContacto,
                            .IdBodega = If(pDtBodegas.Columns.Contains("IdBodega"), Convert.ToInt32(dr("IdBodega")), 0),
                            .User_agr = pUsuario,
                            .Fec_agr = Date.Now,
                            .User_mod = pUsuario,
                            .Fec_mod = Date.Now,
                            .Activo = True
                        }

                        If oBe.IdBodega > 0 Then
                            Insertar(oBe, lConnection, lTransaction)
                        End If
                    End If

                Next
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            lConnection.Dispose()
        End Try
    End Sub

End Class