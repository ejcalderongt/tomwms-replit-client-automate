
Imports System.Data.SqlClient

Public Class clsLnMotivo_anulacion

    Public Shared Sub Cargar(ByRef oBeMotivo_anulacion As clsBeMotivo_anulacion, ByRef dr As DataRow)

        Try

            With oBeMotivo_anulacion
                .IdMotivoAnulacion = IIf(IsDBNull(dr.Item("IdMotivoAnulacion")), 0, dr.Item("IdMotivoAnulacion"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public shared Function Insertar(ByRef oBeMotivo_anulacion As clsBeMotivo_anulacion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("motivo_anulacion")
            Ins.Add("idmotivoanulacion", "@idmotivoanulacion", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion.IdMotivoAnulacion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_anulacion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_anulacion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_anulacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_anulacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_anulacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_anulacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_anulacion.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Function Actualizar(ByRef oBeMotivo_anulacion As clsBeMotivo_anulacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("motivo_anulacion")
            Upd.Add("idmotivoanulacion", "@idmotivoanulacion", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMotivoAnulacion = @IdMotivoAnulacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion.IdMotivoAnulacion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_anulacion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_anulacion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_anulacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_anulacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_anulacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_anulacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_anulacion.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

    Public Shared Function Eliminar(ByRef oBeMotivo_anulacion As clsBeMotivo_anulacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Motivo_anulacion" &
             "  Where(IdMotivoAnulacion = @IdMotivoAnulacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion.IdMotivoAnulacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
            cmd.Dispose
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion"
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

    Public Function Obtener(ByRef oBeMotivo_anulacion As clsBeMotivo_anulacion) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Motivo_anulacion" &
            " Where(IdMotivoAnulacion = @IdMotivoAnulacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion.IdMotivoAnulacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_anulacion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
