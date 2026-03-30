Imports System.Data.SqlClient

Public Class clsLnMotivo_ubicacion

    Public Shared Sub Cargar(ByRef oBeMotivo_ubicacion As clsBeMotivo_ubicacion, ByRef dr As DataRow)
        Try
            With oBeMotivo_ubicacion
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdMotivoUbicacion = IIf(IsDBNull(dr.Item("IdMotivoUbicacion")), 0, dr.Item("IdMotivoUbicacion"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
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

    Public Shared Function Insertar(ByRef oBeMotivo_ubicacion As clsBeMotivo_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("motivo_ubicacion")
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idmotivoubicacion", "@idmotivoubicacion", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_ubicacion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeMotivo_ubicacion.IdMotivoUbicacion))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_ubicacion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_ubicacion.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            oBeMotivo_ubicacion.IdMotivoUbicacion = CInt(cmd.Parameters("@IDMOTIVOUBICACION").Value)

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

    Public Shared Function Actualizar(ByRef oBeMotivo_ubicacion As clsBeMotivo_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("motivo_ubicacion")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idmotivoubicacion", "@idmotivoubicacion", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdMotivoUbicacion = @IdMotivoUbicacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_ubicacion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeMotivo_ubicacion.IdMotivoUbicacion))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_ubicacion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_ubicacion.Activo))

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

    Public Shared Function Eliminar(ByRef oBeMotivo_ubicacion As clsBeMotivo_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Motivo_ubicacion" &
             "  Where(IdMotivoUbicacion = @IdMotivoUbicacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeMotivo_ubicacion.IdMotivoUbicacion))

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

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Motivo_ubicacion"
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

    Public Function Obtener(ByRef oBeMotivo_ubicacion As clsBeMotivo_ubicacion) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Motivo_ubicacion" &
            " Where(IdMotivoUbicacion = @IdMotivoUbicacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeMotivo_ubicacion.IdMotivoUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_ubicacion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
